using LabApi.Features.Console;
using MoonSharp.Interpreter;
using SCriPt.LabAPI.API.Lua.Globals;
using UserSettings.ServerSpecific.Examples;

namespace SCriPt.LabAPI.API.Lua.Objects;

// Custom table that can be used to store custom data in Lua scripts.
// This data is saved to disk when changes are made and can be called upon later.

[MoonSharpUserData]
public class LuaCustomData
{
    [MoonSharpHidden]
    public DynValue table;

    public LuaCustomData()
    {
        table = DynValue.NewPrimeTable();
    }
    
    public LuaCustomData(DynValue table)
    {
        this.table = table;
    }

    public DynValue this[string key]
    {
        get => Load(key);
        set => Save(key,value);
    }
    
    [MoonSharpHidden]
    public DynValue Load(string key)
    {
        return table.Table.Get(key);
    }

    [MoonSharpHidden]
    public void Save(string key, DynValue value)
    {
        // Handle immutible values
        // If a key name starts with an underscore or a dot, it is considered it can only be set once.
        // This will prevent accidental overwrites of important data or modifiactions to configuration settings.
        if (key.StartsWith("_") || key.StartsWith("."))
        {
            Logger.Debug($"{key} = {value.Type}");
            if (table.Table.Get(key).Type != DataType.Nil)
                return;
        }
        
        table.Table.Set(key, value);
        // todo: Perform save operation to disk
        GlobalData.SaveDataToDisk(this);
    }
    
    /* Usage example lua:
     my_mod = SCriPt.Mod("my_mod")
     my_mod.custom_data = Data.Get("my_mod")
     
     my_mod.custom_data["my_key"] = "my_value"
     my_mod.custom_data["my_number"] = 123
    
    */

}