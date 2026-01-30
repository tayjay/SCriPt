using System.Collections.Generic;
using LabApi.Features.Console;
using MoonSharp.Interpreter;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI.API.Lua.Objects;

[MoonSharpUserData]
public class LuaModule : Table
{
    private readonly List<(LuaEvent luaEvent, Closure callback)> _registeredEvents = new List<(LuaEvent, Closure)>();

    public int Priority
    {
        get => (int)this["Priority"];
    }

    public string Name
    {
        get => (string)this["Name"];
    }

    public LuaModule(Script owner, string name, int priority = 3) : base(owner)
    {
        this["Name"] = DynValue.NewString(name);
        this["Type"] = DynValue.NewString("Module");
        this["Priority"] = DynValue.NewNumber(priority);
        InjectMethods(owner);
    }

    public LuaModule(Script owner, DynValue[] arrayValues, string name, int priority = 3) : base(owner, arrayValues)
    {
        this["Name"] = DynValue.NewString(name);
        this["Type"] = DynValue.NewString("Module");
        this["Priority"] = DynValue.NewNumber(priority);
        InjectMethods(owner);
    }

    private void InjectMethods(Script owner)
    {
        this["on"] = DynValue.NewCallback((ctx, args) =>
        {
            var luaEvent = args[0].ToObject<LuaEvent>();
            var callback = args[1].Function;
            OnEvent(luaEvent, callback);
            return DynValue.Nil;
        });
    }

    private void OnEvent(LuaEvent luaEvent, Closure callback)
    {
        if (luaEvent == null)
        {
            Logger.Error($"[LuaModule:{Name}] on() called with nil event");
            return;
        }
        if (callback == null)
        {
            Logger.Error($"[LuaModule:{Name}] on() called with nil callback");
            return;
        }

        luaEvent.add(callback);
        _registeredEvents.Add((luaEvent, callback));
    }

    [MoonSharpHidden]
    public void LoadModule()
    {
        Logger.Debug("Loading module...");
        //Look for .load function in the module and run it
        foreach(var kvp in this.Pairs)
        {
            Logger.Debug(kvp.Key + ": " + kvp.Value);
            if (kvp.Key.String == "load" && kvp.Value.Type == DataType.Function)
            {
                var loadFunction = kvp.Value.Function;
                loadFunction.Call();
                return;
            }
        }
    }

    [MoonSharpHidden]
    public void UnloadModule()
    {
        // Remove all events registered via on()
        foreach (var (luaEvent, callback) in _registeredEvents)
        {
            luaEvent.remove(callback);
        }
        _registeredEvents.Clear();

        //Look for .unload function in the module and run it
        foreach(var kvp in this.Pairs)
        {
            if (kvp.Key.String == "unload" && kvp.Value.Type == DataType.Function)
            {
                var unloadFunction = kvp.Value.Function;
                unloadFunction.Call();
                return;
            }
        }
    }

}