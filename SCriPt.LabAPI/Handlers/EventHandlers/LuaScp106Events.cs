using System;
using LabApi.Events.Arguments.Scp106Events;
using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaScp106Events : ILuaEventHandler
{
    
    public event EventHandler<Scp106ChangingStalkModeEventArgs> ChangingStalkMode;

    public event EventHandler<Scp106ChangedStalkModeEventArgs> ChangedStalkMode;

    public event EventHandler<Scp106ChangingVigorEventArgs> ChangingVigor;

    public event EventHandler<Scp106ChangedVigorEventArgs> ChangedVigor;

    public event EventHandler<Scp106UsedHunterAtlasEventArgs> UsedHunterAtlas;

    public event EventHandler<Scp106UsingHunterAtlasEventArgs> UsingHunterAtlas;

    public event EventHandler<Scp106ChangingSubmersionStatusEventArgs> ChangingSubmersionStatus;

    public event EventHandler<Scp106ChangedSubmersionStatusEventArgs> ChangedSubmersionStatus;

    public event EventHandler<Scp106TeleportingPlayerEvent> TeleportingPlayer;

    public event EventHandler<Scp106TeleportedPlayerEvent> TeleportedPlayer;
    
    [MoonSharpHidden]
    public void OnChangingStalkMode(Scp106ChangingStalkModeEventArgs ev)
    {
        ChangingStalkMode?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnChangedStalkMode(Scp106ChangedStalkModeEventArgs ev)
    {
        ChangedStalkMode?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnChangingVigor(Scp106ChangingVigorEventArgs ev)
    {
        ChangingVigor?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnChangedVigor(Scp106ChangedVigorEventArgs ev)
    {
        ChangedVigor?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnUsedHunterAtlas(Scp106UsedHunterAtlasEventArgs ev)
    {
        UsedHunterAtlas?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnUsingHunterAtlas(Scp106UsingHunterAtlasEventArgs ev)
    {
        UsingHunterAtlas?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnChangingSubmersionStatus(Scp106ChangingSubmersionStatusEventArgs ev)
    {
        ChangingSubmersionStatus?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnChangedSubmersionStatus(Scp106ChangedSubmersionStatusEventArgs ev)
    {
        ChangedSubmersionStatus?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnTeleportingPlayer(Scp106TeleportingPlayerEvent ev)
    {
        TeleportingPlayer?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnTeleportedPlayer(Scp106TeleportedPlayerEvent ev)
    {
        TeleportedPlayer?.Invoke(this, ev);
    }
    
    public void RegisterEventTypes()
    {
        UserData.RegisterType<Scp106ChangingStalkModeEventArgs>();
        UserData.RegisterType<Scp106ChangedStalkModeEventArgs>();
        UserData.RegisterType<Scp106ChangingVigorEventArgs>();
        UserData.RegisterType<Scp106ChangedVigorEventArgs>();
        UserData.RegisterType<Scp106UsedHunterAtlasEventArgs>();
        UserData.RegisterType<Scp106UsingHunterAtlasEventArgs>();
        UserData.RegisterType<Scp106ChangingSubmersionStatusEventArgs>();
        UserData.RegisterType<Scp106ChangedSubmersionStatusEventArgs>();
        UserData.RegisterType<Scp106TeleportingPlayerEvent>();
        UserData.RegisterType<Scp106TeleportedPlayerEvent>();
    }
    
    public void RegisterEvents()
    {
        LabApi.Events.Handlers.Scp106Events.ChangingStalkMode += OnChangingStalkMode;
        LabApi.Events.Handlers.Scp106Events.ChangedStalkMode += OnChangedStalkMode;
        LabApi.Events.Handlers.Scp106Events.ChangingVigor += OnChangingVigor;
        LabApi.Events.Handlers.Scp106Events.ChangedVigor += OnChangedVigor;
        LabApi.Events.Handlers.Scp106Events.UsedHunterAtlas += OnUsedHunterAtlas;
        LabApi.Events.Handlers.Scp106Events.UsingHunterAtlas += OnUsingHunterAtlas;
        LabApi.Events.Handlers.Scp106Events.ChangingSubmersionStatus += OnChangingSubmersionStatus;
        LabApi.Events.Handlers.Scp106Events.ChangedSubmersionStatus += OnChangedSubmersionStatus;
        LabApi.Events.Handlers.Scp106Events.TeleportingPlayer += OnTeleportingPlayer;
        LabApi.Events.Handlers.Scp106Events.TeleportedPlayer += OnTeleportedPlayer;
    }
    
    public void UnregisterEvents()
    {
        LabApi.Events.Handlers.Scp106Events.ChangingStalkMode -= OnChangingStalkMode;
        LabApi.Events.Handlers.Scp106Events.ChangedStalkMode -= OnChangedStalkMode;
        LabApi.Events.Handlers.Scp106Events.ChangingVigor -= OnChangingVigor;
        LabApi.Events.Handlers.Scp106Events.ChangedVigor -= OnChangedVigor;
        LabApi.Events.Handlers.Scp106Events.UsedHunterAtlas -= OnUsedHunterAtlas;
        LabApi.Events.Handlers.Scp106Events.UsingHunterAtlas -= OnUsingHunterAtlas;
        LabApi.Events.Handlers.Scp106Events.ChangingSubmersionStatus -= OnChangingSubmersionStatus;
        LabApi.Events.Handlers.Scp106Events.ChangedSubmersionStatus -= OnChangedSubmersionStatus;
        LabApi.Events.Handlers.Scp106Events.TeleportingPlayer -= OnTeleportingPlayer;
        LabApi.Events.Handlers.Scp106Events.TeleportedPlayer -= OnTeleportedPlayer;
    }
}