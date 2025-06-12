using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MoonSharp.Interpreter;
using RemoteAdmin;

namespace SCriPt.EXILED.API.Lua.Objects;

[MoonSharpUserData]
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
        Closure executeFunction;
        
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
                    executeFunction = table.Get("execute").Function;
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

    public static List<CustomCommand> CreateAll(Script script)
    {
        List<CustomCommand> customCommands = new List<CustomCommand>();
        foreach(var pair in script.Globals.Pairs)
        {
            if(pair.Value.Type == DataType.Table)
            {
                if(TryCreateFromTable(script, pair.Value.Table, out CustomCommand customCommand))
                {
                    customCommands.Add(customCommand);
                }
            }
        }
        return customCommands;
    }

    private static bool TryCreateFromTable(Script script, Table table, out CustomCommand customCommand)
    {
        string commandType = "";
        string command = "";
        string[] aliases = Array.Empty<string>();
        string description = "";
        bool sanitizeResponse = true;
        string permission = "";
        customCommand = null;
        Closure executeFunction;
        if(table.Get("command").Type == DataType.String)
        {
            command = table.Get("command").String;
        }
        else
        {
            return false;
        }
        
        if(table.Get("execute").Type == DataType.Function)
        {
            executeFunction = table.Get("execute").Function;
        }
        else
        {
            command = "";
            return false;
        }
        
        if(table.Get("command_type").Type == DataType.String)
        {
            commandType = table.Get("command_type").String;
        }
        else
        {
            commandType = "RemoteAdmin";
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
        
        
        customCommand = new CustomCommand(script, command, commandType,aliases, description, permission, executeFunction, sanitizeResponse);
        return true;
    }

    public CustomCommand(Script script, string command, string commandType, string[] aliases, string description, string permission, Closure executeFunction, bool sanitizeResponse = true)
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
    Closure _executeFunction;
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
            Log.Debug("Executing command: "+_command);
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
            response = "Script command execution failed! Error: " + ex.Message;
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
    
    public void Register()
    {
        if (_commandType.ToLower() == "remoteadmin" || _commandType.ToLower() == "ra" || _commandType.ToLower() == "admin")
        {
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(this);
            Log.Info("Loaded RA command: " + Command);
        }
        else if (_commandType.ToLower() == "client" || _commandType.ToLower()=="player" || _commandType.ToLower()=="dot")
        {
            QueryProcessor.DotCommandHandler.RegisterCommand(this);
            Log.Info("Loaded Client command: " + Command);
        } else
        {
            Log.Error("Unknown command type: "+_commandType);
        }
    }

    public void Unregister()
    {
        if (_commandType.ToLower() == "remoteadmin" || _commandType.ToLower() == "ra" || _commandType.ToLower() == "admin")
        {
            CommandProcessor.RemoteAdminCommandHandler.UnregisterCommand(this);
            Log.Info("Loaded RA command: " + Command);
        }
        else if (_commandType.ToLower() == "client" || _commandType.ToLower()=="player" || _commandType.ToLower()=="dot")
        {
            QueryProcessor.DotCommandHandler.UnregisterCommand(this);
            Log.Info("Loaded Client command: " + Command);
        } else
        {
            Log.Error("Unknown command type: "+_commandType);
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