using System;
using LabApi.Events.Arguments.ObjectiveEvents;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaObjectiveEvents : ILuaEventHandler
{
    
    [MoonSharpVisible(true)] public event EventHandler<ObjectiveCompletingBaseEventArgs> Completing;
    [MoonSharpVisible(true)] public event EventHandler<ObjectiveCompletedBaseEventArgs> Completed;
    [MoonSharpVisible(true)] public event EventHandler<EnemyKillingObjectiveEventArgs> KillingEnemyCompleting;
    [MoonSharpVisible(true)] public event EventHandler<EnemyKilledObjectiveEventArgs> KilledEnemyCompleted;
    [MoonSharpVisible(true)] public event EventHandler<EscapingObjectiveEventArgs> EscapingCompleting;
    [MoonSharpVisible(true)] public event EventHandler<EscapedObjectiveEventArgs> EscapedCompleted;
    [MoonSharpVisible(true)] public event EventHandler<GeneratorActivatingObjectiveEventArgs> ActivatingGeneratorCompleting;
    [MoonSharpVisible(true)] public event EventHandler<GeneratorActivatedObjectiveEventArgs> ActivatedGeneratorCompleted;
    [MoonSharpVisible(true)] public event EventHandler<ScpDamagingObjectiveEventArgs> DamagingScpCompleting;
    [MoonSharpVisible(true)] public event EventHandler<ScpDamagedObjectiveEventArgs> DamagedScpCompleted;
    [MoonSharpVisible(true)] public event EventHandler<ScpItemPickingObjectiveEventArgs> PickingScpItemCompleting;
    [MoonSharpVisible(true)] public event EventHandler<ScpItemPickedObjectiveEventArgs> PickedScpItemCompleted;
    
    
    [MoonSharpHidden]
    public void OnCompleting(ObjectiveCompletingBaseEventArgs ev)
    {
        Completing?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnCompleted(ObjectiveCompletedBaseEventArgs ev)
    {
        Completed?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnKillingEnemy(EnemyKillingObjectiveEventArgs ev)
    {
        KillingEnemyCompleting?.Invoke(this, ev);
    }
    [MoonSharpHidden]
    public void OnKilledEnemy(EnemyKilledObjectiveEventArgs ev)
    {
        KilledEnemyCompleted?.Invoke(this, ev);
    }
    [MoonSharpHidden]
    public void OnEscaping(EscapingObjectiveEventArgs ev)
    {
        EscapingCompleting?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnEscaped(EscapedObjectiveEventArgs ev)
    {
        EscapedCompleted?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnActivatingGenerator(GeneratorActivatingObjectiveEventArgs ev)
    {
        ActivatingGeneratorCompleting?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnActivatedGenerator(GeneratorActivatedObjectiveEventArgs ev)
    {
        ActivatedGeneratorCompleted?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnDamagingScp(ScpDamagingObjectiveEventArgs ev)
    {
        DamagingScpCompleting?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnDamagedScp(ScpDamagedObjectiveEventArgs ev)
    {
        DamagedScpCompleted?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnPickingScpItem(ScpItemPickingObjectiveEventArgs ev)
    {
        PickingScpItemCompleting?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnPickedScpItem(ScpItemPickedObjectiveEventArgs ev)
    {
        PickedScpItemCompleted?.Invoke(this, ev);
    }
    
    
    
    public void RegisterEventTypes()
    {
        UserData.RegisterType<ObjectiveCompletingBaseEventArgs>();
        UserData.RegisterType<ObjectiveCompletedBaseEventArgs>();
        UserData.RegisterType<EnemyKillingObjectiveEventArgs>();
        UserData.RegisterType<EnemyKilledObjectiveEventArgs>();
        UserData.RegisterType<EscapingObjectiveEventArgs>();
        UserData.RegisterType<EscapedObjectiveEventArgs>();
        UserData.RegisterType<GeneratorActivatingObjectiveEventArgs>();
        UserData.RegisterType<GeneratorActivatedObjectiveEventArgs>();
        UserData.RegisterType<ScpDamagingObjectiveEventArgs>();
        UserData.RegisterType<ScpDamagedObjectiveEventArgs>();
        UserData.RegisterType<ScpItemPickingObjectiveEventArgs>();
        UserData.RegisterType<ScpItemPickedObjectiveEventArgs>();
    }

    public void RegisterEvents()
    {
        LabApi.Events.Handlers.ObjectiveEvents.Completing += OnCompleting;
        LabApi.Events.Handlers.ObjectiveEvents.Completed += OnCompleted;
        LabApi.Events.Handlers.ObjectiveEvents.KillingEnemyCompleting += OnKillingEnemy;
        LabApi.Events.Handlers.ObjectiveEvents.KilledEnemyCompleted += OnKilledEnemy;
        LabApi.Events.Handlers.ObjectiveEvents.EscapingCompleting += OnEscaping;
        LabApi.Events.Handlers.ObjectiveEvents.EscapedCompleted += OnEscaped;
        LabApi.Events.Handlers.ObjectiveEvents.ActivatingGeneratorCompleting += OnActivatingGenerator;
        LabApi.Events.Handlers.ObjectiveEvents.ActivatedGeneratorCompleted += OnActivatedGenerator;
        LabApi.Events.Handlers.ObjectiveEvents.DamagingScpCompleting += OnDamagingScp;
        LabApi.Events.Handlers.ObjectiveEvents.DamagedScpCompleted += OnDamagedScp;
        LabApi.Events.Handlers.ObjectiveEvents.PickingScpItemCompleting += OnPickingScpItem;
        LabApi.Events.Handlers.ObjectiveEvents.PickedScpItemCompleted += OnPickedScpItem;
    }

    public void UnregisterEvents()
    {
        LabApi.Events.Handlers.ObjectiveEvents.Completing -= OnCompleting;
        LabApi.Events.Handlers.ObjectiveEvents.Completed -= OnCompleted;
        LabApi.Events.Handlers.ObjectiveEvents.KillingEnemyCompleting -= OnKillingEnemy;
        LabApi.Events.Handlers.ObjectiveEvents.KilledEnemyCompleted -= OnKilledEnemy;
        LabApi.Events.Handlers.ObjectiveEvents.EscapingCompleting -= OnEscaping;
        LabApi.Events.Handlers.ObjectiveEvents.EscapedCompleted -= OnEscaped;
        LabApi.Events.Handlers.ObjectiveEvents.ActivatingGeneratorCompleting -= OnActivatingGenerator;
        LabApi.Events.Handlers.ObjectiveEvents.ActivatedGeneratorCompleted -= OnActivatedGenerator;
        LabApi.Events.Handlers.ObjectiveEvents.DamagingScpCompleting -= OnDamagingScp;
        LabApi.Events.Handlers.ObjectiveEvents.DamagedScpCompleted -= OnDamagedScp;
        LabApi.Events.Handlers.ObjectiveEvents.PickingScpItemCompleting -= OnPickingScpItem;
        LabApi.Events.Handlers.ObjectiveEvents.PickedScpItemCompleted -= OnPickedScpItem;
    }
}