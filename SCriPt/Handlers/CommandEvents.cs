using System;
using System.Diagnostics.Tracing;
using CommandSystem;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Enums;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.Handlers
{
    [MoonSharpUserData]
    public class CommandEvents : CustomEventsHandler, IEventHandler
    {
        // Triggers when a Remote Admin command is recieved
        [MoonSharpVisible(true)]
        public event EventHandler<CommandExecutingEventArgs> CommandExecuting;
        
        
        public override void OnServerCommandExecuting(CommandExecutingEventArgs ev)
        {
            base.OnServerCommandExecuting(ev);
            CommandExecuting?.Invoke(this, ev);
        }

        [MoonSharpVisible(true)]
        public event EventHandler<CommandExecutedEventArgs> CommandExecuted;

        public override void OnServerCommandExecuted(CommandExecutedEventArgs ev)
        {
            base.OnServerCommandExecuted(ev);
            CommandExecuted?.Invoke(this, ev);
        }


        public void RegisterEvents()
        {
            CustomHandlersManager.RegisterEventsHandler(this);
            Exiled.API.Features.Log.Info("CommandEvents registered!");
        }
        
        

        public void RegisterEventTypes()
        {
            UserData.RegisterType<CommandExecutedEventArgs>();
            UserData.RegisterType<CommandExecutingEventArgs>();
            UserData.RegisterType<CommandType>();
            UserData.RegisterType<ICommand>();
        }

        public void UnregisterEvents()
        {
            CustomHandlersManager.UnregisterEventsHandler(this);
        }
    }
}