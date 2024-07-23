using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MoonSharp.Interpreter;

namespace SCriPt.API.Lua.Objects;

public class CustomCommand : ICommand
{

    //public static Dictionary<string, CustomCommand> CustomCommands = new Dictionary<string, CustomCommand>();
    
    public static bool TryCreate(Script script, out CustomCommand customCommand, out string commandType)
    {
        string commandTypeTemp = "";
        string command = "";
        string[] aliases = Array.Empty<string>();
        string description = "";
        bool sanitizeResponse = true;
        string permission = "";
        DynValue executeFunction;
        
        // Get the global table
        Table globals = script.Globals;

        // Iterate 
        foreach (var pair in globals.Pairs)
        {
            // Check if the value is a table
            if (pair.Value.Type == DataType.Table)
            {
                Table table = pair.Value.Table;
                
                if(table.Get("command").Type == DataType.String)
                {
                    command = table.Get("command").String;
                }
                else
                {
                    continue;
                }
                
                if(table.Get("execute").Type == DataType.Function)
                {
                    executeFunction = table.Get("execute");
                }
                else
                {
                    command = "";
                    continue;
                }
                
                if(table.Get("command_type").Type == DataType.String)
                {
                    commandTypeTemp = table.Get("command_type").String;
                }
                else
                {
                    commandTypeTemp = "RemoteAdmin";
                }
                    
                
                
                if(table.Get("aliases").Type == DataType.Table)
                {
                    aliases = table.Get("aliases").Table.Values.Select(x => x.String).ToArray();
                }
                
                if(table.Get("description").Type == DataType.String)
                {
                    description = table.Get("description").String;
                } else
                {
                    description = "A custom SCriPt command. Add a 'description' key to the table to change this message.";
                }
                
                if(table.Get("permission").Type == DataType.String)
                {
                    permission = table.Get("permission").String;
                } else
                {
                    permission = "";
                }
                
                if(table.Get("sanitize_response").Type == DataType.Boolean)
                {
                    sanitizeResponse = table.Get("sanitize_response").Boolean;
                } else
                {
                    sanitizeResponse = true;
                }
                
                
                commandType = commandTypeTemp;
                customCommand = new CustomCommand(script, command, commandTypeTemp,aliases, description, permission, executeFunction, sanitizeResponse);
                //CustomCommands.Add(command, customCommand);
                return true;
            }
        }
        
        Log.Error("Custom command table is missing.");
        commandType = null;
        customCommand = null;
        return false;
    }

    public CustomCommand(Script script, string command, string commandType, string[] aliases, string description, string permission, DynValue executeFunction, bool sanitizeResponse = true)
    {
        _script = script;
        _command = command;
        _commandType = commandType;
        _aliases = aliases;
        _description = description;
        _permission = permission;
        _sanitizeResponse = sanitizeResponse;
        _executeFunction = executeFunction;
    }
    
    string _command;
    string _commandType;
    string[] _aliases;
    string _description;
    bool _sanitizeResponse;
    string _permission;
    DynValue _executeFunction;
    Script _script;
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if (_commandType.ToLower() == "client" || _commandType.ToLower() == "player" || _commandType.ToLower() == "dot")
        {
            //todo: Investigate if there is a need for permission checking on client commands
            return ExecuteCommand(arguments, sender, out response);
        }
        if(string.IsNullOrEmpty(Permission) || sender.CheckPermission(Permission))
        {
            // Perform a permissions check for RA commands
            return ExecuteCommand(arguments, sender, out response);
        }
        response = "You do not have permission to execute this command.";
        return false;
    }

    private bool ExecuteCommand(ArraySegment<string> arguments, ICommandSender sender,
        [UnscopedRef] out string response)
    {
        try
        {
            //todo: Look into removing first element from arguments
            Table output = _script.Call(_executeFunction, arguments.Array, new ScriptCommandSender(sender)).Table;
            if (!output.Get("response").IsNil() && !output.Get("result").IsNil())
            {
                response = output.Get("response").String;
                return output.Get("result").Boolean;
            }
            response = "Script command execution failed! Error: Invalid return table. Missing 'response' or 'result' key.";
            return false;
            
        } catch(ScriptRuntimeException ex)
        {
            response = "Script command execution failed! Error: " + ex.DecoratedMessage;
            return false;
        } catch(Exception ex)
        {
            response = $"General command execution failed! {ex.GetType()}: {ex.Message}";
            return false;
        }
    }

    public string Command => _command;
    public string[] Aliases => _aliases;
    public string Description => _description;
    public string Permission => _permission;
    public bool SanitizeResponse => _sanitizeResponse;

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