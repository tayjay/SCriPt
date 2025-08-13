using System;
using System.Linq;
using LabApi.Features.Console;
using LabApi.Features.Enums;
using MoonSharp.Interpreter;
using SCriPt.LabAPI.API.Lua.Objects;

namespace SCriPt.LabAPI.API.Lua.Globals;

[MoonSharpUserData]
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
        Owner.Modules.Add(module);
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
        Owner.CustomCommands.Add(command);
        return command;
    }

    public void RegisterType(string typeName)
    {
        Type type = Type.GetType(typeName);
        if (type == null)
        {
            Logger.Error("Failed to register type: " + typeName + ". Type not found.");
            return;
        }
        UserData.RegisterType(type);
        Logger.Info("Registered type: " + typeName);
    }

    public LuaCustomSettings Settings(Table table, Closure callback)
    {
        LuaCustomSettings settings = new LuaCustomSettings(Owner, table, callback);
        Owner.CustomSettings.Add(settings);
        return settings;
    }

    public string Docs(object obj)
    {
        // Generate documentation for the given object.
        // Get the class type of the object.
        Type type = obj.GetType();
        // Use reflection to get the properties and methods of the type.
        string documentation = $"Documentation for {type.Name}:\n";
        documentation += "Properties:\n";
        foreach (var prop in type.GetProperties())
        {
            documentation += $"- {prop.Name}: {prop.PropertyType.Name}\n";
        }
        documentation += "Methods:\n";
        foreach (var method in type.GetMethods())
        {
            if (method.IsPublic)
            {
                documentation += $"- {method.Name}({string.Join(", ", method.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name))}): {method.ReturnType.Name}\n";
            }
        }
        // Get the events of the type.
        documentation += "Events:\n";
        foreach (var evt in type.GetEvents())
        {
            documentation += $"- {evt.Name}: {evt.EventHandlerType.Name}\n";
        }
        // Get member fields of the type.
        documentation += "Fields:\n";
        foreach (var field in type.GetFields())
        {
            documentation += $"- {field.Name}: {field.FieldType.Name}\n";
        }
        // Return the generated documentation.
        Logger.Info(documentation);
        return documentation;
    }
    
    
}