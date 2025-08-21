using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using LabApi.Features.Permissions;

namespace SCriPt.LabAPI.Commands;


public class LuaEnableScriptCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if (!sender.HasPermissions("script.enable"))
        {
            response = "You do not have permission to execute this command.";
            return false;
        }
        if (arguments.Count < 1)
        {
            response = "Usage: script enable <script_name>";
            return false;
        }
        // Find script by name
        string scriptName = arguments.Array[arguments.Offset];
        
        
        
        
        response = string.Empty;
        return true;
    }

    public string Command { get; } = "enable";
    public string[] Aliases { get; } = [];
    public string Description { get; } = "Enables a Lua script by name.";
}