using System;
using LabApi.Events.Arguments.Scp914Events;
using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaScp914Events : ILuaEventHandler
{
    public event EventHandler<Scp914ActivatingEventArgs> Activating;

    public event EventHandler<Scp914ActivatedEventArgs> Activated;

    public event EventHandler<Scp914KnobChangingEventArgs> KnobChanging;

    public event EventHandler<Scp914KnobChangedEventArgs> KnobChanged;

    public event EventHandler<Scp914ProcessingPickupEventArgs> ProcessingPickup;

    public event EventHandler<Scp914ProcessedPickupEventArgs> ProcessedPickup;

    public event EventHandler<Scp914ProcessingPlayerEventArgs> ProcessingPlayer;

    public event EventHandler<Scp914ProcessedPlayerEventArgs> ProcessedPlayer;

    public event EventHandler<Scp914ProcessingInventoryItemEventArgs> ProcessingInventoryItem;

    public event EventHandler<Scp914ProcessedInventoryItemEventArgs> ProcessedInventoryItem;
    
    [MoonSharpHidden]
    public void OnActivating(Scp914ActivatingEventArgs ev)
    {
        Activating?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnActivated(Scp914ActivatedEventArgs ev)
    {
        Activated?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnKnobChanging(Scp914KnobChangingEventArgs ev)
    {
        KnobChanging?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnKnobChanged(Scp914KnobChangedEventArgs ev)
    {
        KnobChanged?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnProcessingPickup(Scp914ProcessingPickupEventArgs ev)
    {
        ProcessingPickup?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnProcessedPickup(Scp914ProcessedPickupEventArgs ev)
    {
        ProcessedPickup?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnProcessingPlayer(Scp914ProcessingPlayerEventArgs ev)
    {
        ProcessingPlayer?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnProcessedPlayer(Scp914ProcessedPlayerEventArgs ev)
    {
        ProcessedPlayer?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnProcessingInventoryItem(Scp914ProcessingInventoryItemEventArgs ev)
    {
        ProcessingInventoryItem?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnProcessedInventoryItem(Scp914ProcessedInventoryItemEventArgs ev)
    {
        ProcessedInventoryItem?.Invoke(this, ev);
    }
    
    public void RegisterEventTypes()
    {
        UserData.RegisterType<Scp914ActivatingEventArgs>();
        UserData.RegisterType<Scp914ActivatedEventArgs>();
        UserData.RegisterType<Scp914KnobChangingEventArgs>();
        UserData.RegisterType<Scp914KnobChangedEventArgs>();
        UserData.RegisterType<Scp914ProcessingPickupEventArgs>();
        UserData.RegisterType<Scp914ProcessedPickupEventArgs>();
        UserData.RegisterType<Scp914ProcessingPlayerEventArgs>();
        UserData.RegisterType<Scp914ProcessedPlayerEventArgs>();
        UserData.RegisterType<Scp914ProcessingInventoryItemEventArgs>();
        UserData.RegisterType<Scp914ProcessedInventoryItemEventArgs>();
    }
    
    public void RegisterEvents()
    {
        LabApi.Events.Handlers.Scp914Events.Activating += OnActivating;
        LabApi.Events.Handlers.Scp914Events.Activated += OnActivated;
        LabApi.Events.Handlers.Scp914Events.KnobChanging += OnKnobChanging;
        LabApi.Events.Handlers.Scp914Events.KnobChanged += OnKnobChanged;
        LabApi.Events.Handlers.Scp914Events.ProcessingPickup += OnProcessingPickup;
        LabApi.Events.Handlers.Scp914Events.ProcessedPickup += OnProcessedPickup;
        LabApi.Events.Handlers.Scp914Events.ProcessingPlayer += OnProcessingPlayer;
        LabApi.Events.Handlers.Scp914Events.ProcessedPlayer += OnProcessedPlayer;
        LabApi.Events.Handlers.Scp914Events.ProcessingInventoryItem += OnProcessingInventoryItem;
        LabApi.Events.Handlers.Scp914Events.ProcessedInventoryItem += OnProcessedInventoryItem;
    }
    
    public void UnregisterEvents()
    {
        LabApi.Events.Handlers.Scp914Events.Activating -= OnActivating;
        LabApi.Events.Handlers.Scp914Events.Activated -= OnActivated;
        LabApi.Events.Handlers.Scp914Events.KnobChanging -= OnKnobChanging;
        LabApi.Events.Handlers.Scp914Events.KnobChanged -= OnKnobChanged;
        LabApi.Events.Handlers.Scp914Events.ProcessingPickup -= OnProcessingPickup;
        LabApi.Events.Handlers.Scp914Events.ProcessedPickup -= OnProcessedPickup;
        LabApi.Events.Handlers.Scp914Events.ProcessingPlayer -= OnProcessingPlayer;
        LabApi.Events.Handlers.Scp914Events.ProcessedPlayer -= OnProcessedPlayer;
        LabApi.Events.Handlers.Scp914Events.ProcessingInventoryItem -= OnProcessingInventoryItem;
        LabApi.Events.Handlers.Scp914Events.ProcessedInventoryItem -= OnProcessedInventoryItem;
    }
}