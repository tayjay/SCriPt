using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using SCriPt.LabAPI.API.Lua;
using Logger = LabApi.Features.Console.Logger;

namespace SCriPt.LabAPI.Handlers;

/// <summary>
/// Dynamically bridges all static events from a LabAPI event handler class (e.g. PlayerEvents, WarheadEvents)
/// to Lua-accessible events without requiring manual per-event boilerplate.
///
/// Each static event on the source class becomes a LuaEvent that Lua scripts can subscribe to via .add().
/// </summary>
public class DynamicLuaEventHandler : ILuaEventHandler
{
    /// <summary>
    /// The LabAPI static event handler class being wrapped (e.g. typeof(LabApi.Events.Handlers.PlayerEvents)).
    /// </summary>
    public Type SourceType { get; }

    /// <summary>
    /// Map of event name -> LuaEvent wrapper.
    /// </summary>
    public Dictionary<string, LuaEvent> Events { get; } = new Dictionary<string, LuaEvent>(StringComparer.Ordinal);

    /// <summary>
    /// Delegates subscribed to the LabAPI events, kept for unsubscription.
    /// </summary>
    private readonly Dictionary<string, Delegate> _subscribedDelegates = new Dictionary<string, Delegate>();

    public DynamicLuaEventHandler(Type sourceType)
    {
        SourceType = sourceType;
    }

    public void RegisterEventTypes()
    {
        foreach (var eventInfo in GetSourceEvents())
        {
            var argsType = GetEventArgsType(eventInfo);
            if (argsType != null && argsType != typeof(void))
            {
                if (!UserData.IsTypeRegistered(argsType))
                {
                    try
                    {
                        UserData.RegisterType(argsType,
                            new SafeUserDataDescriptor(argsType, InteropAccessMode.LazyOptimized));
                    }
                    catch (Exception e)
                    {
                        Logger.Warn(
                            $"[DynamicLuaEventHandler] Failed to register event args type '{argsType.Name}': {e.Message}");
                    }
                }
            }

            Events[eventInfo.Name] = new LuaEvent(eventInfo.Name);
        }
    }

    public void RegisterEvents()
    {
        foreach (var eventInfo in GetSourceEvents())
        {
            if (!Events.TryGetValue(eventInfo.Name, out var luaEvent))
                continue;

            try
            {
                var argsType = GetEventArgsType(eventInfo);
                Delegate handler;

                if (argsType != null)
                {
                    // LabEventHandler<TEventArgs> - create delegate matching the actual event handler type
                    var invokeMethod = typeof(LuaEvent).GetMethod(nameof(LuaEvent.Invoke))!
                        .MakeGenericMethod(argsType);
                    handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, luaEvent, invokeMethod);
                }
                else
                {
                    // LabEventHandler - parameterless
                    handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, luaEvent,
                        typeof(LuaEvent).GetMethod(nameof(LuaEvent.InvokeParameterless))!);
                }

                eventInfo.AddEventHandler(null, handler); // null because static events
                _subscribedDelegates[eventInfo.Name] = handler;
            }
            catch (Exception e)
            {
                Logger.Warn(
                    $"[DynamicLuaEventHandler] Failed to subscribe to '{SourceType.Name}.{eventInfo.Name}': {e.Message}");
            }
        }
    }

    public void UnregisterEvents()
    {
        foreach (var eventInfo in GetSourceEvents())
        {
            if (_subscribedDelegates.TryGetValue(eventInfo.Name, out var handler))
            {
                try
                {
                    eventInfo.RemoveEventHandler(null, handler);
                }
                catch (Exception e)
                {
                    Logger.Warn(
                        $"[DynamicLuaEventHandler] Failed to unsubscribe from '{SourceType.Name}.{eventInfo.Name}': {e.Message}");
                }
            }
        }

        _subscribedDelegates.Clear();
    }

    /// <summary>
    /// Remove all event callbacks belonging to the given script from all events in this handler.
    /// </summary>
    public void RemoveCallbacksForScript(Script script)
    {
        foreach (var luaEvent in Events.Values)
        {
            luaEvent.RemoveCallbacksForScript(script);
        }
    }

    private EventInfo[] GetSourceEvents()
    {
        return SourceType.GetEvents(BindingFlags.Public | BindingFlags.Static);
    }

    /// <summary>
    /// Gets the TEventArgs type from LabEventHandler&lt;TEventArgs&gt;, or null for parameterless LabEventHandler.
    /// </summary>
    private static Type GetEventArgsType(EventInfo eventInfo)
    {
        var handlerType = eventInfo.EventHandlerType;
        if (handlerType.IsGenericType)
        {
            return handlerType.GetGenericArguments()[0];
        }

        return null;
    }
}
