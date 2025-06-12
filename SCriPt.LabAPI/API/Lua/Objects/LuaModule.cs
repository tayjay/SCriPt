using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.API.Lua.Objects;

public class LuaModule : Table
{
    public int Priority
    {
        get => (int)this["Priority"];
    }
    public LuaModule(Script owner, string name, int priority = 3) : base(owner)
    {
        this["Name"] = DynValue.NewString(name);
        this["Type"] = DynValue.NewString("Module");
        this["Priority"] = DynValue.NewNumber(priority);
    }

    public LuaModule(Script owner, DynValue[] arrayValues, string name, int priority = 3) : base(owner, arrayValues)
    {
        this["Name"] = DynValue.NewString(name);
        this["Type"] = DynValue.NewString("Module");
        this["Priority"] = DynValue.NewNumber(priority);
    }

    [MoonSharpHidden]
    public void LoadModule()
    {
        //Look for .load function in the module and run it
        foreach(var kvp in this.Pairs)
        {
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