using System;
using CommandSystem;
using Exiled.Permissions.Extensions;
using MoonSharp.Interpreter;
using SCriPt.API.Lua;

namespace SCriPt.Commands.RemoteAdmin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ScriptCommand : ParentCommand
    {
        
        public ScriptCommand() => LoadGeneratedCommands();
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(!sender.CheckPermission("SCriPt"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            /*
            if (arguments.Count == 0)
            {
                response = "Usage: script <lua>";
                return false;
            }
            
            string lua = string.Join(" ", arguments);
            
            try
            {
                Script script = new Script();
                ScriptLoader.RegisterAPI(script);
                DynValue res = script.DoString(lua);
                response = "Result: " + res.String;
                return true;
            }
            catch (Exception e)
            {
                response = e.Message;
                return false;
            }*/
            
            response = "Available commands: dir, load, unload, list, run";
            return true;
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(!sender.CheckPermission("SCriPt"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }
            
            response = "Available commands: dir, load, unload, reload, list, run";
            return true;
        }

        public override string Command { get; } = "script";
        public override string[] Aliases { get; } = {"lua"};
        public override string Description { get; } = "Interact with the SCriPt environment.";
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new ScriptDirCommand());
            RegisterCommand(new ScriptLoadCommand());
            RegisterCommand(new ScriptUnloadCommand());
            RegisterCommand(new ScriptListCommand());
            RegisterCommand(new ScriptReloadCommand());
        }
    }
}