using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using LabApi.Features.Permissions;

namespace SCriPt.LabAPI.Commands;

public class LuaListCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if(!sender.HasPermissions("script.list"))
        {
            response = "You do not have permission to use this command.";
            return false;
        }
        response = "Loaded scripts:\n";
        foreach (var script in SCriPt.Instance.Scripts)
        {
            response += $"- {script.Key}\n";
            foreach (var module in script.Value.Modules)
            {
                response += $"  - {module.Name}\n";
                foreach (var entry in module.Pairs)
                {
                    response += $"    + {entry.Key} ({entry.Value.Type})\n";
                }
            }
        }
        if (SCriPt.Instance.Scripts.Count == 0)
        {
            response += "No scripts loaded.";
        }
        return true;
    }

    public string Command { get; } = "list";
    public string[] Aliases { get; } = new[] { "ls" };
    public string Description { get; } = "Lists all loaded scripts";
}