using System;
using LabApi.Events.Arguments.WarheadEvents;
using LabApi.Events.Handlers;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaWarheadEvents : ILuaEventHandler
{
    [MoonSharpVisible(true)] public event EventHandler<WarheadStartingEventArgs> Starting;

    [MoonSharpVisible(true)] public event EventHandler<WarheadStartedEventArgs> Started;

    [MoonSharpVisible(true)] public event EventHandler<WarheadStoppingEventArgs> Stopping;

    [MoonSharpVisible(true)] public event EventHandler<WarheadStoppedEventArgs> Stopped;

    [MoonSharpVisible(true)] public event EventHandler<WarheadDetonatingEventArgs> Detonating;

    [MoonSharpVisible(true)] public event EventHandler<WarheadDetonatedEventArgs> Detonated;
    
    [MoonSharpHidden]
    public void OnStarting(WarheadStartingEventArgs ev)
    {
        Starting?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnStarted(WarheadStartedEventArgs ev)
    {
        Started?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnStopping(WarheadStoppingEventArgs ev)
    {
        Stopping?.Invoke(this, ev);
    }   
    
    [MoonSharpHidden]
    public void OnStopped(WarheadStoppedEventArgs ev)
    {
        Stopped?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnDetonating(WarheadDetonatingEventArgs ev)
    {
        Detonating?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnDetonated(WarheadDetonatedEventArgs ev)
    {
        Detonated?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void RegisterEventTypes()
    {
        // Register event types for MoonSharp
        UserData.RegisterType<WarheadStartingEventArgs>();
        UserData.RegisterType<WarheadStartedEventArgs>();
        UserData.RegisterType<WarheadStoppingEventArgs>();
        UserData.RegisterType<WarheadStoppedEventArgs>();
        UserData.RegisterType<WarheadDetonatingEventArgs>();
        UserData.RegisterType<WarheadDetonatedEventArgs>();
    }

    [MoonSharpHidden]
    public void RegisterEvents()
    {
        WarheadEvents.Detonated += OnDetonated;
        WarheadEvents.Detonating += OnDetonating;
        WarheadEvents.Started += OnStarted;
        WarheadEvents.Starting += OnStarting;
        WarheadEvents.Stopped += OnStopped;
        WarheadEvents.Stopping += OnStopping;
    }
    
    [MoonSharpHidden]
    public void UnregisterEvents()
    {
        WarheadEvents.Detonated -= OnDetonated;
        WarheadEvents.Detonating -= OnDetonating;
        WarheadEvents.Started -= OnStarted;
        WarheadEvents.Starting -= OnStarting;
        WarheadEvents.Stopped -= OnStopped;
        WarheadEvents.Stopping -= OnStopping;
    }
    
    
}