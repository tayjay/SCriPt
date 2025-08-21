using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;

namespace SCriPt.LabAPI.Commands;
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class LuaCommand : ParentCommand
{

    public LuaCommand()
    {
        LoadGeneratedCommands();
    }

    public override string Command { get; } = "lua";
    public override string[] Aliases { get; } = new string[] { "script" };
    public override string Description { get; } = "Lua command for executing Lua scripts and commands within the SCriPt environment.";

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        response = "This command is a parent command for Lua scripts and commands. Use subcommands to execute specific Lua scripts or commands.";
        return false; // Indicating that no subcommand was executed
    }

   public override void LoadGeneratedCommands()
    {
        RegisterCommand(new LuaExecuteCommand());
        RegisterCommand(new LuaReloadCommand());
        RegisterCommand(new LuaListCommand());
        RegisterCommand(new LuaDocsCommand());
        RegisterCommand(new LuaDisableScriptCommand());
        RegisterCommand(new LuaEnableScriptCommand());
        RegisterCommand(new LuaPastebinCommand());
    }
}