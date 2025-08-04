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
    [MoonSharpVisible(true)] public event EventHandler<EnemyKillingObjectiveEventArgs> KillingEnemy;
    [MoonSharpVisible(true)] public event EventHandler<EnemyKilledObjectiveEventArgs> KilledEnemy;
    [MoonSharpVisible(true)] public event EventHandler<EscapingObjectiveEventArgs> Escaping;
    [MoonSharpVisible(true)] public event EventHandler<EscapedObjectiveEventArgs> Escaped;
    [MoonSharpVisible(true)] public event EventHandler<GeneratorActivatingObjectiveEventArgs> ActivatingGenerator;
    [MoonSharpVisible(true)] public event EventHandler<GeneratorActivatedObjectiveEventArgs> ActivatedGenerator;
    [MoonSharpVisible(true)] public event EventHandler<ScpDamagingObjectiveEventArgs> DamagingScp;
    [MoonSharpVisible(true)] public event EventHandler<ScpDamagedObjectiveEventArgs> DamagedScp;
    [MoonSharpVisible(true)] public event EventHandler<ScpItemPickingObjectiveEventArgs> PickingScpItem;
    [MoonSharpVisible(true)] public event EventHandler<ScpItemPickedObjectiveEventArgs> PickedScpItem;
    
    
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
        KillingEnemy?.Invoke(this, ev);
    }
    [MoonSharpHidden]
    public void OnKilledEnemy(EnemyKilledObjectiveEventArgs ev)
    {
        KilledEnemy?.Invoke(this, ev);
    }
    [MoonSharpHidden]
    public void OnEscaping(EscapingObjectiveEventArgs ev)
    {
        Escaping?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnEscaped(EscapedObjectiveEventArgs ev)
    {
        Escaped?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnActivatingGenerator(GeneratorActivatingObjectiveEventArgs ev)
    {
        ActivatingGenerator?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnActivatedGenerator(GeneratorActivatedObjectiveEventArgs ev)
    {
        ActivatedGenerator?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnDamagingScp(ScpDamagingObjectiveEventArgs ev)
    {
        DamagingScp?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnDamagedScp(ScpDamagedObjectiveEventArgs ev)
    {
        DamagedScp?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnPickingScpItem(ScpItemPickingObjectiveEventArgs ev)
    {
        PickingScpItem?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnPickedScpItem(ScpItemPickedObjectiveEventArgs ev)
    {
        PickedScpItem?.Invoke(this, ev);
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