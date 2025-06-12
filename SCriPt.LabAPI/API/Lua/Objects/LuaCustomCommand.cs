

using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using LabApi.Features.Console;
using LabApi.Features.Enums;
using LabApi.Features.Wrappers;
using MoonSharp.Interpreter;
using RemoteAdmin;

namespace SCriPt.LabAPI.API.Lua.Objects;

public class LuaCustomCommand : ICommand
{
    public string Command { get; }
    public string[] Aliases { get; }
    public string Description { get; }
    Script Owner { get; set; }
    
    Closure ExecuteFunction { get; set; }
    
    string Permission { get; set; }
    
    CommandType CommandType { get; set; }
    
    public LuaCustomCommand(Script owner, CommandType type, string command, string[] aliases, string description, string permission, Closure execute)
    {
        Owner = owner;
        CommandType = type; //Client, Console, RemoteAdmin
        Command = command;
        Aliases = aliases ?? Array.Empty<string>();
        Description = description;
        Permission = permission;
        ExecuteFunction = execute;
    }
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        try
        {
            Table output = Owner.Call(ExecuteFunction, arguments.Array, new ScriptCommandSender(sender)).Table;
            if (!output.Get("response").IsNil() && !output.Get("result").IsNil())
            {
                response = output.Get("response").String;
                return output.Get("result").Boolean;
            }
            response = "SCriPt command execution failed! Error: Invalid return table. Missing 'response' or 'result' keys.";
            return false;
        } catch(ScriptRuntimeException ex)
        {
            response = "Script command execution failed! Error: " + ex.Message;
            return false;
        } catch(Exception ex)
        {
            response = $"General command execution failed! {ex.GetType()}: {ex.Message}";
            return false;
        }
    }



    public void Register()
    {
        if (CommandType == CommandType.RemoteAdmin)
        {
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(this);
            Logger.Debug("Registered RemoteAdmin command: " + Command);
        }
        else if (CommandType == CommandType.Client)
        {
            QueryProcessor.DotCommandHandler.RegisterCommand(this);
            Logger.Debug("Registered Client command: " + Command);
        }
        else
        {
            Logger.Error("Invalid command type specified.");
        }
    }
    
    public void Unregister()
    {
        if (CommandType == CommandType.RemoteAdmin)
        {
            CommandProcessor.RemoteAdminCommandHandler.UnregisterCommand(this);
            Logger.Debug("Unregistered RemoteAdmin command: " + Command);
        }
        else if (CommandType == CommandType.Client)
        {
            QueryProcessor.DotCommandHandler.UnregisterCommand(this);
            Logger.Debug("Unregistered Client command: " + Command);
        }
        else
        {
            Logger.Error("Invalid command type specified.");
        }
    }

    [MoonSharpUserData]
    public class ScriptCommandSender
    {
        ICommandSender _sender;
        
        public Player Player => Player.Get(_sender);

        public string Name => _sender.LogName;
        
        public void Respond(string message, bool success = true)
        {
            _sender.Respond(message, success);
        }
        
        public ScriptCommandSender(ICommandSender sender)
        {
            _sender = sender;
        }
    }
    
}