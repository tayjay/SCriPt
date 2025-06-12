using System;
using LabApi.Events.Arguments.Scp096Events;
using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaScp096Events : ILuaEventHandler
{
    public event EventHandler<Scp096AddingTargetEventArgs> AddingTarget;

    public event EventHandler<Scp096AddedTargetEventArgs> AddedTarget;

    public event EventHandler<Scp096ChangingStateEventArgs> ChangingState;

    public event EventHandler<Scp096ChangedStateEventArgs> ChangedState;

    public event EventHandler<Scp096ChargingEventArgs> Charging;

    public event EventHandler<Scp096ChargedEventArgs> Charged;

    public event EventHandler<Scp096EnragingEventArgs> Enraging;

    public event EventHandler<Scp096EnragedEventArgs> Enraged;

    public event EventHandler<Scp096PryingGateEventArgs> PryingGate;

    public event EventHandler<Scp096PriedGateEventArgs> PriedGate;

    public event EventHandler<Scp096StartCryingEventArgs> StartCrying;

    public event EventHandler<Scp096StartedCryingEventArgs> StartedCrying;

    public event EventHandler<Scp096TryingNotToCryEventArgs> TryingNotToCry;

    public event EventHandler<Scp096TriedNotToCryEventArgs> TriedNotToCry;
    
    [MoonSharpHidden]
    public void OnAddingTarget(Scp096AddingTargetEventArgs ev)
    {
        AddingTarget?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnAddedTarget(Scp096AddedTargetEventArgs ev)
    {
        AddedTarget?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnChangingState(Scp096ChangingStateEventArgs ev)
    {
        ChangingState?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnChangedState(Scp096ChangedStateEventArgs ev)
    {
        ChangedState?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnCharging(Scp096ChargingEventArgs ev)
    {
        Charging?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnCharged(Scp096ChargedEventArgs ev)
    {
        Charged?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnEnraging(Scp096EnragingEventArgs ev)
    {
        Enraging?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnEnraged(Scp096EnragedEventArgs ev)
    {
        Enraged?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnPryingGate(Scp096PryingGateEventArgs ev)
    {
        PryingGate?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnPriedGate(Scp096PriedGateEventArgs ev)
    {
        PriedGate?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnStartCrying(Scp096StartCryingEventArgs ev)
    {
        StartCrying?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnStartedCrying(Scp096StartedCryingEventArgs ev)
    {
        StartedCrying?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnTryingNotToCry(Scp096TryingNotToCryEventArgs ev)
    {
        TryingNotToCry?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnTriedNotToCry(Scp096TriedNotToCryEventArgs ev)
    {
        TriedNotToCry?.Invoke(this, ev);
    }
    
    public void RegisterEventTypes()
    {
        UserData.RegisterType<Scp096AddingTargetEventArgs>();
        UserData.RegisterType<Scp096AddedTargetEventArgs>();
        UserData.RegisterType<Scp096ChangingStateEventArgs>();
        UserData.RegisterType<Scp096ChangedStateEventArgs>();
        UserData.RegisterType<Scp096ChargingEventArgs>();
        UserData.RegisterType<Scp096ChargedEventArgs>();
        UserData.RegisterType<Scp096EnragingEventArgs>();
        UserData.RegisterType<Scp096EnragedEventArgs>();
        UserData.RegisterType<Scp096PryingGateEventArgs>();
        UserData.RegisterType<Scp096PriedGateEventArgs>();
        UserData.RegisterType<Scp096StartCryingEventArgs>();
        UserData.RegisterType<Scp096StartedCryingEventArgs>();
        UserData.RegisterType<Scp096TryingNotToCryEventArgs>();
        UserData.RegisterType<Scp096TriedNotToCryEventArgs>();
    }

    public void RegisterEvents()
    {
        LabApi.Events.Handlers.Scp096Events.AddingTarget += OnAddingTarget;
        LabApi.Events.Handlers.Scp096Events.AddedTarget += OnAddedTarget;
        LabApi.Events.Handlers.Scp096Events.ChangingState += OnChangingState;
        LabApi.Events.Handlers.Scp096Events.ChangedState += OnChangedState;
        LabApi.Events.Handlers.Scp096Events.Charging += OnCharging;
        LabApi.Events.Handlers.Scp096Events.Charged += OnCharged;
        LabApi.Events.Handlers.Scp096Events.Enraging += OnEnraging;
        LabApi.Events.Handlers.Scp096Events.Enraged += OnEnraged;
        LabApi.Events.Handlers.Scp096Events.PryingGate += OnPryingGate;
        LabApi.Events.Handlers.Scp096Events.PriedGate += OnPriedGate;
        LabApi.Events.Handlers.Scp096Events.StartCrying += OnStartCrying;
        LabApi.Events.Handlers.Scp096Events.StartedCrying += OnStartedCrying;
        LabApi.Events.Handlers.Scp096Events.TryingNotToCry += OnTryingNotToCry;
        LabApi.Events.Handlers.Scp096Events.TriedNotToCry += OnTriedNotToCry;
    }

    public void UnregisterEvents()
    {
        LabApi.Events.Handlers.Scp096Events.AddingTarget -= OnAddingTarget;
        LabApi.Events.Handlers.Scp096Events.AddedTarget -= OnAddedTarget;
        LabApi.Events.Handlers.Scp096Events.ChangingState -= OnChangingState;
        LabApi.Events.Handlers.Scp096Events.ChangedState -= OnChangedState;
        LabApi.Events.Handlers.Scp096Events.Charging -= OnCharging;
        LabApi.Events.Handlers.Scp096Events.Charged -= OnCharged;
        LabApi.Events.Handlers.Scp096Events.Enraging -= OnEnraging;
        LabApi.Events.Handlers.Scp096Events.Enraged -= OnEnraged;
        LabApi.Events.Handlers.Scp096Events.PryingGate -= OnPryingGate;
        LabApi.Events.Handlers.Scp096Events.PriedGate -= OnPriedGate;
        LabApi.Events.Handlers.Scp096Events.StartCrying -= OnStartCrying;
        LabApi.Events.Handlers.Scp096Events.StartedCrying -= OnStartedCrying;
        LabApi.Events.Handlers.Scp096Events.TryingNotToCry -= OnTryingNotToCry;
        LabApi.Events.Handlers.Scp096Events.TriedNotToCry -= OnTriedNotToCry;
    }
}