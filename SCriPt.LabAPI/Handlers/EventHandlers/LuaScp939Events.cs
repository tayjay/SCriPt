using System;
using LabApi.Events.Arguments.Scp939Events;
using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.Handlers;

[MoonSharp.Interpreter.MoonSharpUserData]
public class LuaScp939Events : ILuaEventHandler
{
    public event EventHandler<Scp939AttackingEventArgs> Attacking;

    public event EventHandler<Scp939AttackedEventArgs> Attacked;

    public event EventHandler<Scp939CreatingAmnesticCloudEventArgs> CreatingAmnesticCloud;

    public event EventHandler<Scp939CreatedAmnesticCloudEventArgs> CreatedAmnesticCloud;

    public event EventHandler<Scp939LungingEventArgs> Lunging;

    public event EventHandler<Scp939LungedEventArgs> Lunged;
    
    [MoonSharpHidden]
    public void OnAttacking(Scp939AttackingEventArgs ev)
    {
        Attacking?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnAttacked(Scp939AttackedEventArgs ev)
    {
        Attacked?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnCreatingAmnesticCloud(Scp939CreatingAmnesticCloudEventArgs ev)
    {
        CreatingAmnesticCloud?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnCreatedAmnesticCloud(Scp939CreatedAmnesticCloudEventArgs ev)
    {
        CreatedAmnesticCloud?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnLunging(Scp939LungingEventArgs ev)
    {
        Lunging?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnLunged(Scp939LungedEventArgs ev)
    {
        Lunged?.Invoke(this, ev);
    }
    
    public void RegisterEventTypes()
    {
        UserData.RegisterType<Scp939AttackingEventArgs>();
        UserData.RegisterType<Scp939AttackedEventArgs>();
        UserData.RegisterType<Scp939CreatingAmnesticCloudEventArgs>();
        UserData.RegisterType<Scp939CreatedAmnesticCloudEventArgs>();
        UserData.RegisterType<Scp939LungingEventArgs>();
        UserData.RegisterType<Scp939LungedEventArgs>();
    }
    
    public void RegisterEvents()
    {
        LabApi.Events.Handlers.Scp939Events.Attacking += OnAttacking;
        LabApi.Events.Handlers.Scp939Events.Attacked += OnAttacked;
        LabApi.Events.Handlers.Scp939Events.CreatingAmnesticCloud += OnCreatingAmnesticCloud;
        LabApi.Events.Handlers.Scp939Events.CreatedAmnesticCloud += OnCreatedAmnesticCloud;
        LabApi.Events.Handlers.Scp939Events.Lunging += OnLunging;
        LabApi.Events.Handlers.Scp939Events.Lunged += OnLunged;
    }
    
    public void UnregisterEvents()
    {
        LabApi.Events.Handlers.Scp939Events.Attacking -= OnAttacking;
        LabApi.Events.Handlers.Scp939Events.Attacked -= OnAttacked;
        LabApi.Events.Handlers.Scp939Events.CreatingAmnesticCloud -= OnCreatingAmnesticCloud;
        LabApi.Events.Handlers.Scp939Events.CreatedAmnesticCloud -= OnCreatedAmnesticCloud;
        LabApi.Events.Handlers.Scp939Events.Lunging -= OnLunging;
        LabApi.Events.Handlers.Scp939Events.Lunged -= OnLunged;
    }
}