using System;
using LabApi.Events.Arguments.Scp049Events;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaScp049Events : ILuaEventHandler
{
    [MoonSharpVisible(true)] public event EventHandler<Scp049StartingResurrectionEventArgs> StartingResurrection;

    [MoonSharpVisible(true)] public event EventHandler<Scp049ResurrectingBodyEventArgs> ResurrectingBody;

    [MoonSharpVisible(true)] public event EventHandler<Scp049ResurrectedBodyEventArgs> ResurrectedBody;

    [MoonSharpVisible(true)] public event EventHandler<Scp049UsingDoctorsCallEventArgs> UsingDoctorsCall;

    [MoonSharpVisible(true)] public event EventHandler<Scp049UsedDoctorsCallEventArgs> UsedDoctorsCall;

    [MoonSharpVisible(true)] public event EventHandler<Scp049UsingSenseEventArgs> UsingSense;

    [MoonSharpVisible(true)] public event EventHandler<Scp049UsedSenseEventArgs> UsedSense;
    
    [MoonSharpHidden]
    public void OnStartingResurrection(Scp049StartingResurrectionEventArgs ev)
    {
        StartingResurrection?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnResurrectingBody(Scp049ResurrectingBodyEventArgs ev)
    {
        ResurrectingBody?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnResurrectedBody(Scp049ResurrectedBodyEventArgs ev)
    {
        ResurrectedBody?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnUsingDoctorsCall(Scp049UsingDoctorsCallEventArgs ev)
    {
        UsingDoctorsCall?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnUsedDoctorsCall(Scp049UsedDoctorsCallEventArgs ev)
    {
        UsedDoctorsCall?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnUsingSense(Scp049UsingSenseEventArgs ev)
    {
        UsingSense?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnUsedSense(Scp049UsedSenseEventArgs ev)
    {
        UsedSense?.Invoke(this, ev);
    }

    
    public void RegisterEventTypes()
    {
        // Register event types for MoonSharp
        UserData.RegisterType<Scp049StartingResurrectionEventArgs>();
        UserData.RegisterType<Scp049ResurrectingBodyEventArgs>();
        UserData.RegisterType<Scp049ResurrectedBodyEventArgs>();
        UserData.RegisterType<Scp049UsingDoctorsCallEventArgs>();
        UserData.RegisterType<Scp049UsedDoctorsCallEventArgs>();
        UserData.RegisterType<Scp049UsingSenseEventArgs>();
        UserData.RegisterType<Scp049UsedSenseEventArgs>();
    }

    public void RegisterEvents()
    {
        LabApi.Events.Handlers.Scp049Events.StartingResurrection += OnStartingResurrection;
        LabApi.Events.Handlers.Scp049Events.ResurrectingBody += OnResurrectingBody;
        LabApi.Events.Handlers.Scp049Events.ResurrectedBody += OnResurrectedBody;
        LabApi.Events.Handlers.Scp049Events.UsingDoctorsCall += OnUsingDoctorsCall;
        LabApi.Events.Handlers.Scp049Events.UsedDoctorsCall += OnUsedDoctorsCall;
        LabApi.Events.Handlers.Scp049Events.UsingSense += OnUsingSense;
        LabApi.Events.Handlers.Scp049Events.UsedSense += OnUsedSense;
    }

    public void UnregisterEvents()
    {
        LabApi.Events.Handlers.Scp049Events.StartingResurrection -= OnStartingResurrection;
        LabApi.Events.Handlers.Scp049Events.ResurrectingBody -= OnResurrectingBody;
        LabApi.Events.Handlers.Scp049Events.ResurrectedBody -= OnResurrectedBody;
        LabApi.Events.Handlers.Scp049Events.UsingDoctorsCall -= OnUsingDoctorsCall;
        LabApi.Events.Handlers.Scp049Events.UsedDoctorsCall -= OnUsedDoctorsCall;
        LabApi.Events.Handlers.Scp049Events.UsingSense -= OnUsingSense;
        LabApi.Events.Handlers.Scp049Events.UsedSense -= OnUsedSense;
    }
}