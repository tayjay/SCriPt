using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using LabApi.Features.Permissions;
using MEC;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI.Commands;

public class LuaReloadCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if (!sender.HasPermissions("script.reload"))
        {
            response = "You do not have permission to use this command.";
            return false;
        }

        Timing.CallDelayed(Timing.WaitForOneFrame, () =>
        {
            ScriptLoader.UnloadAllScripts();
            ScriptLoader.Initialize();
            ScriptLoader.LoadScripts();
            sender.Respond("Reload complete!");
        });
        response = "Lua Scripts are now being reloaded...";
        return true;

    }

    public string Command { get; } = "reload";
    public string[] Aliases { get; } = new [] { "rl" };
    public string Description { get; } = "Reloads the Lua scripts in the LabAPI plugin. This is useful for development purposes when you want to test changes without restarting the server.";
}