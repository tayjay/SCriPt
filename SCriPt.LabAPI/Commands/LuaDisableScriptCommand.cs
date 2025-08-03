using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using LabApi.Features.Permissions;
using MoonSharp.Interpreter;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class LuaDisableScriptCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if (!sender.HasPermissions("script.disable"))
        {
            response = "You do not have permission to execute this command.";
            return false;
        }
        if (arguments.Count < 1)
        {
            response = "Usage: script disable <script_name>";
            return false;
        }
        
        string scriptName = arguments.Array[arguments.Offset];
        if (string.IsNullOrEmpty(scriptName))
        {
            response = "Script name cannot be empty.";
            return false;
        }
        ScriptLoader.UnloadScript(scriptName);
        response = $"Script '{scriptName}' has been disabled.";
        return true;
    }

    public string Command { get; } = "disable";
    public string[] Aliases { get; } = [];
    public string Description { get; } = "Disables a Lua script by its name.";
}