using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI.API.Lua.Globals;

/// <summary>
/// Custom descriptor for GlobalEvents that resolves known aliases via a static map,
/// and falls back to the DynamicEventHandlers dictionary for any unknown category.
/// This means new LabAPI event classes (e.g. Scp123Events) are accessible as Events.Scp123
/// without a code update.
/// </summary>
public class GlobalEventsDescriptor : IUserDataDescriptor
{
    public string Name => "GlobalEvents";
    public Type Type => typeof(GlobalEvents);

    /// <summary>
    /// Nickname aliases that map to a canonical category name.
    /// </summary>
    private static readonly Dictionary<string, string> Aliases = new Dictionary<string, string>(StringComparer.Ordinal)
    {
        { "Doctor", "Scp049" },
        { "ShyGuy", "Scp096" },
        { "Peanut", "Scp173" },
        { "Larry", "Scp106" },
        { "Dog", "Scp939" },
        { "Zombie", "Scp0492" },
        { "Skeleton", "Scp3114" },
    };

    public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
    {
        if (index.Type != DataType.String)
            return DynValue.Nil;

        var name = index.String;

        // Resolve alias to canonical name
        if (Aliases.TryGetValue(name, out var canonical))
            name = canonical;

        if (SCriPt.Instance.DynamicEventHandlers.TryGetValue(name, out var handler))
            return UserData.Create(handler);

        return DynValue.Nil;
    }

    public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
    {
        return false;
    }

    public string AsString(object obj)
    {
        return "GlobalEvents";
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
