using System;
using LabApi.Events.Arguments.Scp173Events;
using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.Handlers;

public class LuaScp173Events : ILuaEventHandler
{
    public event EventHandler<Scp173BreakneckSpeedChangingEventArgs> BreakneckSpeedChanging;

    public event EventHandler<Scp173BreakneckSpeedChangedEventArgs> BreakneckSpeedChanged;

    public event EventHandler<Scp173AddingObserverEventArgs> AddingObserver;

    public event EventHandler<Scp173AddedObserverEventArgs> AddedObserver;

    public event EventHandler<Scp173RemovingObserverEventArgs> RemovingObserver;

    public event EventHandler<Scp173RemovedObserverEventArgs> RemovedObserver;

    public event EventHandler<Scp173CreatingTantrumEventArgs> CreatingTantrum;

    public event EventHandler<Scp173CreatedTantrumEventArgs> CreatedTantrum;

    public event EventHandler<Scp173PlayingSoundEventArgs> PlayingSound;

    public event EventHandler<Scp173PlayedSoundEventArgs> PlayedSound;
    
    [MoonSharpHidden]
    public void OnBreakneckSpeedChanging(Scp173BreakneckSpeedChangingEventArgs ev)
    {
        BreakneckSpeedChanging?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnBreakneckSpeedChanged(Scp173BreakneckSpeedChangedEventArgs ev)
    {
        BreakneckSpeedChanged?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnAddingObserver(Scp173AddingObserverEventArgs ev)
    {
        AddingObserver?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnAddedObserver(Scp173AddedObserverEventArgs ev)
    {
        AddedObserver?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnRemovingObserver(Scp173RemovingObserverEventArgs ev)
    {
        RemovingObserver?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnRemovedObserver(Scp173RemovedObserverEventArgs ev)
    {
        RemovedObserver?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnCreatingTantrum(Scp173CreatingTantrumEventArgs ev)
    {
        CreatingTantrum?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnCreatedTantrum(Scp173CreatedTantrumEventArgs ev)
    {
        CreatedTantrum?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnPlayingSound(Scp173PlayingSoundEventArgs ev)
    {
        PlayingSound?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnPlayedSound(Scp173PlayedSoundEventArgs ev)
    {
        PlayedSound?.Invoke(this, ev);
    }
    
    public void RegisterEventTypes()
    {
        UserData.RegisterType<Scp173BreakneckSpeedChangingEventArgs>();
        UserData.RegisterType<Scp173BreakneckSpeedChangedEventArgs>();
        UserData.RegisterType<Scp173AddingObserverEventArgs>();
        UserData.RegisterType<Scp173AddedObserverEventArgs>();
        UserData.RegisterType<Scp173RemovingObserverEventArgs>();
        UserData.RegisterType<Scp173RemovedObserverEventArgs>();
        UserData.RegisterType<Scp173CreatingTantrumEventArgs>();
        UserData.RegisterType<Scp173CreatedTantrumEventArgs>();
        UserData.RegisterType<Scp173PlayingSoundEventArgs>();
        UserData.RegisterType<Scp173PlayedSoundEventArgs>();
    }
    
    public void RegisterEvents()
    {
        LabApi.Events.Handlers.Scp173Events.BreakneckSpeedChanging += OnBreakneckSpeedChanging;
        LabApi.Events.Handlers.Scp173Events.BreakneckSpeedChanged += OnBreakneckSpeedChanged;
        LabApi.Events.Handlers.Scp173Events.AddingObserver += OnAddingObserver;
        LabApi.Events.Handlers.Scp173Events.AddedObserver += OnAddedObserver;
        LabApi.Events.Handlers.Scp173Events.RemovingObserver += OnRemovingObserver;
        LabApi.Events.Handlers.Scp173Events.RemovedObserver += OnRemovedObserver;
        LabApi.Events.Handlers.Scp173Events.CreatingTantrum += OnCreatingTantrum;
        LabApi.Events.Handlers.Scp173Events.CreatedTantrum += OnCreatedTantrum;
        LabApi.Events.Handlers.Scp173Events.PlayingSound += OnPlayingSound;
        LabApi.Events.Handlers.Scp173Events.PlayedSound += OnPlayedSound;
    }
    
    public void UnregisterEvents()
    {
        LabApi.Events.Handlers.Scp173Events.BreakneckSpeedChanging -= OnBreakneckSpeedChanging;
        LabApi.Events.Handlers.Scp173Events.BreakneckSpeedChanged -= OnBreakneckSpeedChanged;
        LabApi.Events.Handlers.Scp173Events.AddingObserver -= OnAddingObserver;
        LabApi.Events.Handlers.Scp173Events.AddedObserver -= OnAddedObserver;
        LabApi.Events.Handlers.Scp173Events.RemovingObserver -= OnRemovingObserver;
        LabApi.Events.Handlers.Scp173Events.RemovedObserver -= OnRemovedObserver;
        LabApi.Events.Handlers.Scp173Events.CreatingTantrum -= OnCreatingTantrum;
        LabApi.Events.Handlers.Scp173Events.CreatedTantrum -= OnCreatedTantrum;
        LabApi.Events.Handlers.Scp173Events.PlayingSound -= OnPlayingSound;
        LabApi.Events.Handlers.Scp173Events.PlayedSound -= OnPlayedSound;
    }
    
    
}