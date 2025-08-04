using System;
using LabApi.Events.Arguments.Scp3114Events;
using LabApi.Events.Handlers;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaScp3114Events : ILuaEventHandler
{
    
    [MoonSharpVisible(true)] public event EventHandler<Scp3114DisguisingEventArgs> Disguising;
    [MoonSharpVisible(true)] public event EventHandler<Scp3114DisguisedEventArgs> Disguised;
    [MoonSharpVisible(true)] public event EventHandler<Scp3114RevealingEventArgs> Revealing;
    [MoonSharpVisible(true)] public event EventHandler<Scp3114RevealedEventArgs> Revealed;
    [MoonSharpVisible(true)] public event EventHandler<Scp3114StartedDanceEventArgs> StartedDance;
    [MoonSharpVisible(true)] public event EventHandler<Scp3114StartingDanceEventArgs> StartingDance;
    [MoonSharpVisible(true)] public event EventHandler<Scp3114StrangleAbortingEventArgs> StrangleAborting;
    [MoonSharpVisible(true)] public event EventHandler<Scp3114StrangleAbortedEventArgs> StrangleAborted;
    [MoonSharpVisible(true)] public event EventHandler<Scp3114StrangleStartingEventArgs> StrangleStarting; 
    [MoonSharpVisible(true)] public event EventHandler<Scp3114StrangleStartedEventArgs> StrangleStarted;
    
    [MoonSharpHidden]
    public void OnDisguising(Scp3114DisguisingEventArgs ev)
    {
        Disguising?.Invoke(null, ev);
    }
    
    [MoonSharpHidden]
    public void OnDisguised(Scp3114DisguisedEventArgs ev)
    {
        Disguised?.Invoke(null, ev);
    }
    
    [MoonSharpHidden]
    public void OnRevealing(Scp3114RevealingEventArgs ev)
    {
        Revealing?.Invoke(null, ev);
    }
    
    [MoonSharpHidden]
    public void OnRevealed(Scp3114RevealedEventArgs ev)
    {
        Revealed?.Invoke(null, ev);
    }
    
    [MoonSharpHidden]
    public void OnStartedDance(Scp3114StartedDanceEventArgs ev)
    {
        StartedDance?.Invoke(null, ev);
    }
    
    [MoonSharpHidden]
    public void OnStartingDance(Scp3114StartingDanceEventArgs ev)
    {
        StartingDance?.Invoke(null, ev);
    }
    
    [MoonSharpHidden]
    public void OnStrangleAborting(Scp3114StrangleAbortingEventArgs ev)
    {
        StrangleAborting?.Invoke(null, ev);
    }
    
    [MoonSharpHidden]
    public void OnStrangleAborted(Scp3114StrangleAbortedEventArgs ev)
    {
        StrangleAborted?.Invoke(null, ev);
    }
    
    [MoonSharpHidden]
    public void OnStrangleStarting(Scp3114StrangleStartingEventArgs ev)
    {
        StrangleStarting?.Invoke(null, ev);
    }
    
    [MoonSharpHidden]
    public void OnStrangleStarted(Scp3114StrangleStartedEventArgs ev)
    {
        StrangleStarted?.Invoke(null, ev);
    }
    
    
    
    public void RegisterEventTypes()
    {
        UserData.RegisterType<Scp3114DisguisingEventArgs>();
        UserData.RegisterType<Scp3114DisguisedEventArgs>();
        UserData.RegisterType<Scp3114RevealingEventArgs>();
        UserData.RegisterType<Scp3114RevealedEventArgs>();
        UserData.RegisterType<Scp3114StartedDanceEventArgs>();
        UserData.RegisterType<Scp3114StartingDanceEventArgs>();
        UserData.RegisterType<Scp3114StrangleAbortingEventArgs>();
        UserData.RegisterType<Scp3114StrangleAbortedEventArgs>();
        UserData.RegisterType<Scp3114StrangleStartingEventArgs>();
        UserData.RegisterType<Scp3114StrangleStartedEventArgs>();
    }

    public void RegisterEvents()
    {
        Scp3114Events.Disguising += OnDisguising;
        Scp3114Events.Disguised += OnDisguised;
        Scp3114Events.Revealing += OnRevealing;
        Scp3114Events.Revealed += OnRevealed;
        Scp3114Events.Dance += OnStartedDance;
        Scp3114Events.StartDancing += OnStartingDance;
        Scp3114Events.StrangleAborting += OnStrangleAborting;
        Scp3114Events.StrangleAborted += OnStrangleAborted;
        Scp3114Events.StrangleStarting += OnStrangleStarting;
        Scp3114Events.StrangleStarted += OnStrangleStarted;
    }

    public void UnregisterEvents()
    {
        Scp3114Events.Disguising -= OnDisguising;
        Scp3114Events.Disguised -= OnDisguised;
        Scp3114Events.Revealing -= OnRevealing;
        Scp3114Events.Revealed -= OnRevealed;
        Scp3114Events.Dance -= OnStartedDance;
        Scp3114Events.StartDancing -= OnStartingDance;
        Scp3114Events.StrangleAborting -= OnStrangleAborting;
        Scp3114Events.StrangleAborted -= OnStrangleAborted;
        Scp3114Events.StrangleStarting -= OnStrangleStarting;
        Scp3114Events.StrangleStarted -= OnStrangleStarted;
    }
}