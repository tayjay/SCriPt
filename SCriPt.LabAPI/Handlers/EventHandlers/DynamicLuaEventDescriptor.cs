using System;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.LabAPI.Handlers;

/// <summary>
/// Custom MoonSharp descriptor for DynamicLuaEventHandler.
/// Allows Lua to access events by name: Events.Player.Joined -> LuaEvent
/// </summary>
public class DynamicLuaEventDescriptor : IUserDataDescriptor
{
    public string Name => "DynamicLuaEventHandler";
    public Type Type => typeof(DynamicLuaEventHandler);

    public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
    {
        if (obj is DynamicLuaEventHandler handler && index.Type == DataType.String)
        {
            if (handler.Events.TryGetValue(index.String, out var luaEvent))
            {
                return UserData.Create(luaEvent);
            }
        }

        return DynValue.Nil;
    }

    public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
    {
        return false;
    }

    public string AsString(object obj)
    {
        if (obj is DynamicLuaEventHandler handler)
            return $"DynamicLuaEventHandler({handler.SourceType.Name})";
        return "DynamicLuaEventHandler";
    }

    public DynValue MetaIndex(Script script, object obj, string metaname)
    {
        return null;
    }

    public bool IsTypeCompatible(Type type, object obj)
    {
        return type.IsInstanceOfType(obj);
    }
}
