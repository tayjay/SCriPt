using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using LabApi.Features.Permissions;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI.Commands;

public class LuaExecuteCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if (!sender.HasPermissions("script.execute"))
        {
            response = "You do not have permission to execute this command.";
            return false;
        }
        if (arguments.Count == 0)
        {
            response = "No Lua code provided.";
            return false;
        }
        string luaCode = string.Join(" ", arguments.Array, arguments.Offset, arguments.Count);
        
        ScriptLoader.CreateScriptByCommand(luaCode);
        
        
        
        response = "Command not implemented yet.";
        return false;
    }

    public string Command { get; } = "execute";
    public string[] Aliases { get; } = new[] { "exec" };
    public string Description { get; } = "Executes some lua code directly.";
}