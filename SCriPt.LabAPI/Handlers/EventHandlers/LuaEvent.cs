using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using SCriPt.LabAPI.API.Lua;
using Logger = LabApi.Features.Console.Logger;

namespace SCriPt.LabAPI.Handlers;

/// <summary>
/// Represents a single event that Lua scripts can subscribe to via .add(callback) and .remove(callback).
/// Wraps a list of Lua closures and invokes them when the corresponding LabAPI event fires.
/// </summary>
[MoonSharpUserData]
public class LuaEvent
{
    private readonly List<Closure> _callbacks = new List<Closure>();

    [MoonSharpHidden]
    public string Name { get; }

    [MoonSharpHidden]
    public LuaEvent(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Subscribe a Lua function to this event.
    /// Usage in Lua: Events.Player.Joined.add(myCallback)
    /// </summary>
    public void add(Closure callback)
    {
        if (callback != null)
            _callbacks.Add(callback);
    }

    /// <summary>
    /// Unsubscribe a Lua function from this event.
    /// Usage in Lua: Events.Player.Joined.remove(myCallback)
    /// </summary>
    public void remove(Closure callback)
    {
        _callbacks.Remove(callback);
    }

    /// <summary>
    /// Remove all callbacks that belong to the given Script instance.
    /// Called automatically when a script is unloaded.
    /// </summary>
    [MoonSharpHidden]
    public int RemoveCallbacksForScript(Script script)
    {
        return _callbacks.RemoveAll(c => c.OwnerScript == script);
    }

    /// <summary>
    /// Called by DynamicLuaEventHandler when a LabEventHandler&lt;TEventArgs&gt; fires.
    /// </summary>
    [MoonSharpHidden]
    public void Invoke<T>(T eventArgs)
    {
        var actualType = eventArgs?.GetType() ?? typeof(T);
        if (!UserData.IsTypeRegistered(actualType))
        {
            //Logger.Warn($"[LuaEvent:{Name}] Event args type '{actualType.Name}' is not registered, registering now...");
            UserData.RegisterType(actualType,
                new SafeUserDataDescriptor(actualType, InteropAccessMode.LazyOptimized));
        }

        for (int i = _callbacks.Count - 1; i >= 0; i--)
        {
            try
            {
                var script = _callbacks[i].OwnerScript;
                var dynArg = DynValue.FromObject(script, eventArgs);
                //Logger.Debug($"[LuaEvent:{Name}] dynArg type={dynArg.Type}, actualType={actualType.Name}, isRegistered={UserData.IsTypeRegistered(actualType)}");
                script.Call(_callbacks[i], dynArg);
            }
            catch (ScriptRuntimeException ex)
            {
                Logger.Error($"[LuaEvent:{Name}] Lua runtime error: {ex.DecoratedMessage}");
            }
            catch (Exception ex)
            {
                Logger.Error($"[LuaEvent:{Name}] Error invoking callback: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Called by DynamicLuaEventHandler when a parameterless LabEventHandler fires.
    /// </summary>
    [MoonSharpHidden]
    public void InvokeParameterless()
    {
        for (int i = _callbacks.Count - 1; i >= 0; i--)
        {
            try
            {
                _callbacks[i].Call();
            }
            catch (ScriptRuntimeException ex)
            {
                Logger.Error($"[LuaEvent:{Name}] Lua runtime error: {ex.DecoratedMessage}");
            }
            catch (Exception ex)
            {
                Logger.Error($"[LuaEvent:{Name}] Error invoking callback: {ex.Message}");
            }
        }
    }
}
