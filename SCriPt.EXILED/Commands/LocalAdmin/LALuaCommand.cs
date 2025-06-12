using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using MoonSharp.Interpreter;
using SCriPt.EXILED.API.Lua;
using SCriPt.EXILED.API.Lua.Objects;

namespace SCriPt.EXILED.Commands.LocalAdmin;

[CommandHandler(typeof(GameConsoleCommandHandler))]
public class LALuaCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        ScriptWrapper wrapper = new ScriptWrapper("ConsoleCommand", NewScriptLoader.SandboxLevel);
        wrapper.SetupAPI();
        DynValue value = wrapper.DoString(string.Join(" ", arguments));
        if (value.Type == DataType.Void)
        {
            response = "Executed.";
            return true;
        }
        response = value.ToPrintString();
        return true;
    }

    public string Command { get; } = "lua";
    public string[] Aliases { get; } = {"code", "run"};
    public string Description { get; } = "Run Lua code through SCriPt.";
}