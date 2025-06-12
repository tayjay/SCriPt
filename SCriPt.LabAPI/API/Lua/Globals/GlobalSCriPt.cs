using LabApi.Features.Enums;
using MoonSharp.Interpreter;
using SCriPt.LabAPI.API.Lua.Objects;

namespace SCriPt.LabAPI.API.Lua.Globals;

public class GlobalSCriPt
{
    private ScriptHandler Owner { get; set; }
    
    public GlobalSCriPt(ScriptHandler owner)
    {
        Owner = owner;
        Owner.Globals["SCriPt"] = this;
    }

    public LuaModule Module(string name, int priority = 3)
    {
        LuaModule module = new LuaModule(Owner, name, priority);
        return module;
    }
    
    /*
     * my_mod = SCriPt.Mod("my_mod")
     * my_mod.custom = "This is my custom module."
     */
    
    public LuaModule Mod(string name, int priority = 3) 
    {
        return Module(name, priority);
    }

    public LuaModule Global(string name, int priority = 1)
    {
        return Module(name, priority);
    }

    // my_command = SCriPt.Command(CommandType.RemoteAdmin, "my_command", new[] { "mycmd", "mc" }, "This is my custom command.", "script.mycommand", function(args) return { response = "Command executed!", result = true } end)
    
    
    public LuaCustomCommand Command(CommandType type, string name, string[] aliases, string description, string permission, Closure execute)
    {
        LuaCustomCommand command = new LuaCustomCommand(Owner, type, name, aliases, description, permission, execute);
        return command;
    }
    
}