using System;
using LabApi.Events.Arguments.Scp0492Events;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaScp0492Events : ILuaEventHandler
{
    [MoonSharpVisible(true)] public event EventHandler<Scp0492StartingConsumingCorpseEventArgs> StartingConsumingCorpse;

    [MoonSharpVisible(true)] public event EventHandler<Scp0492StartedConsumingCorpseEventArgs> StartedConsumingCorpse;

    [MoonSharpVisible(true)] public event EventHandler<Scp0492ConsumingCorpseEventArgs> ConsumingCorpse;

    [MoonSharpVisible(true)] public event EventHandler<Scp0492ConsumedCorpseEventArgs> ConsumedCorpse;
    
    [MoonSharpHidden]
    public void OnStartingConsumingCorpse(Scp0492StartingConsumingCorpseEventArgs ev)
    {
        StartingConsumingCorpse?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnStartedConsumingCorpse(Scp0492StartedConsumingCorpseEventArgs ev)
    {
        StartedConsumingCorpse?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnConsumingCorpse(Scp0492ConsumingCorpseEventArgs ev)
    {
        ConsumingCorpse?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnConsumedCorpse(Scp0492ConsumedCorpseEventArgs ev)
    {
        ConsumedCorpse?.Invoke(this, ev);
    }


    public void RegisterEventTypes()
    {
        UserData.RegisterType<Scp0492StartingConsumingCorpseEventArgs>();
        UserData.RegisterType<Scp0492StartedConsumingCorpseEventArgs>();
        UserData.RegisterType<Scp0492ConsumingCorpseEventArgs>();
        UserData.RegisterType<Scp0492ConsumedCorpseEventArgs>();
    }

    public void RegisterEvents()
    {
        LabApi.Events.Handlers.Scp0492Events.StartingConsumingCorpse += OnStartingConsumingCorpse;
        LabApi.Events.Handlers.Scp0492Events.StartedConsumingCorpse += OnStartedConsumingCorpse;
        LabApi.Events.Handlers.Scp0492Events.ConsumingCorpse += OnConsumingCorpse;
        LabApi.Events.Handlers.Scp0492Events.ConsumedCorpse += OnConsumedCorpse;
    }

    public void UnregisterEvents()
    {
        LabApi.Events.Handlers.Scp0492Events.StartingConsumingCorpse -= OnStartingConsumingCorpse;
        LabApi.Events.Handlers.Scp0492Events.StartedConsumingCorpse -= OnStartedConsumingCorpse;
        LabApi.Events.Handlers.Scp0492Events.ConsumingCorpse -= OnConsumingCorpse;
        LabApi.Events.Handlers.Scp0492Events.ConsumedCorpse -= OnConsumedCorpse;
    }
}