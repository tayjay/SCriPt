using System;
using LabApi.Events.Arguments.Scp079Events;
using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaScp079Events : ILuaEventHandler
{
    public event EventHandler<Scp079BlackingOutRoomEventsArgs> BlackingOutRoom;

    public event EventHandler<Scp079BlackedOutRoomEventArgs> BlackedOutRoom;

    public event EventHandler<Scp079BlackingOutZoneEventArgs> BlackingOutZone;

    public event EventHandler<Scp079BlackedOutZoneEventArgs> BlackedOutZone;

    public event EventHandler<Scp079ChangingCameraEventArgs> ChangingCamera;

    public event EventHandler<Scp079ChangedCameraEventArgs> ChangedCamera;

    public event EventHandler<Scp079CancellingRoomLockdownEventArgs> CancellingRoomLockdown;

    public event EventHandler<Scp079CancelledRoomLockdownEventArgs> CancelledRoomLockdown;

    public event EventHandler<Scp079GainingExperienceEventArgs> GainingExperience;

    public event EventHandler<Scp079GainedExperienceEventArgs> GainedExperience;

    public event EventHandler<Scp079LevelingUpEventArgs> LevelingUp;

    public event EventHandler<Scp079LeveledUpEventArgs> LeveledUp;

    public event EventHandler<Scp079LockingDoorEventArgs> LockingDoor;

    public event EventHandler<Scp079LockedDoorEventArgs> LockedDoor;

    public event EventHandler<Scp079LockingDownRoomEventArgs> LockingDownRoom;

    public event EventHandler<Scp079LockedDownRoomEventArgs> LockedDownRoom;

    public event EventHandler<Scp079RecontainingEventArgs> Recontaining;

    public event EventHandler<Scp079RecontainedEventArgs> Recontained;

    public event EventHandler<Scp079UnlockingDoorEventArgs> UnlockingDoor;

    public event EventHandler<Scp079UnlockedDoorEventArgs> UnlockedDoor;

    public event EventHandler<Scp079UsingTeslaEventArgs> UsingTesla;

    public event EventHandler<Scp079UsedTeslaEventArgs> UsedTesla;
    
    public event EventHandler<Scp079PingingEventArgs> Pinging;
    
    public event EventHandler<Scp079PingedEventArgs> Pinged;
    
    [MoonSharpHidden]
    public void OnBlackingOutRoom(Scp079BlackingOutRoomEventsArgs ev)
    {
        BlackingOutRoom?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnBlackedOutRoom(Scp079BlackedOutRoomEventArgs ev)
    {
        BlackedOutRoom?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnBlackingOutZone(Scp079BlackingOutZoneEventArgs ev)
    {
        BlackingOutZone?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnBlackedOutZone(Scp079BlackedOutZoneEventArgs ev)
    {
        BlackedOutZone?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnChangingCamera(Scp079ChangingCameraEventArgs ev)
    {
        ChangingCamera?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnChangedCamera(Scp079ChangedCameraEventArgs ev)
    {
        ChangedCamera?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnCancellingRoomLockdown(Scp079CancellingRoomLockdownEventArgs ev)
    {
        CancellingRoomLockdown?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnCancelledRoomLockdown(Scp079CancelledRoomLockdownEventArgs ev)
    {
        CancelledRoomLockdown?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnGainingExperience(Scp079GainingExperienceEventArgs ev)
    {
        GainingExperience?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnGainedExperience(Scp079GainedExperienceEventArgs ev)
    {
        GainedExperience?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnLevelingUp(Scp079LevelingUpEventArgs ev)
    {
        LevelingUp?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnLeveledUp(Scp079LeveledUpEventArgs ev)
    {
        LeveledUp?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnLockingDoor(Scp079LockingDoorEventArgs ev)
    {
        LockingDoor?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]   
    public void OnLockedDoor(Scp079LockedDoorEventArgs ev)
    {
        LockedDoor?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnLockingDownRoom(Scp079LockingDownRoomEventArgs ev)
    {
        LockingDownRoom?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnLockedDownRoom(Scp079LockedDownRoomEventArgs ev)
    {
        LockedDownRoom?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnRecontaining(Scp079RecontainingEventArgs ev)
    {
        Recontaining?.Invoke(this, ev);
    }
    [MoonSharpHidden]
    public void OnRecontained(Scp079RecontainedEventArgs ev)
    {
        Recontained?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnUnlockingDoor(Scp079UnlockingDoorEventArgs ev)
    {
        UnlockingDoor?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnUnlockedDoor(Scp079UnlockedDoorEventArgs ev)
    {
        UnlockedDoor?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnUsingTesla(Scp079UsingTeslaEventArgs ev)
    {
        UsingTesla?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnUsedTesla(Scp079UsedTeslaEventArgs ev)
    {
        UsedTesla?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnPinging(Scp079PingingEventArgs ev)
    {
        Pinging?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnPinged(Scp079PingedEventArgs ev)
    {
        Pinged?.Invoke(this, ev);
    }


    public void RegisterEventTypes()
    {
        UserData.RegisterType<Scp079BlackingOutRoomEventsArgs>();
        UserData.RegisterType<Scp079BlackedOutRoomEventArgs>();
        UserData.RegisterType<Scp079BlackingOutZoneEventArgs>();
        UserData.RegisterType<Scp079BlackedOutZoneEventArgs>();
        UserData.RegisterType<Scp079ChangingCameraEventArgs>();
        UserData.RegisterType<Scp079ChangedCameraEventArgs>();
        UserData.RegisterType<Scp079CancellingRoomLockdownEventArgs>();
        UserData.RegisterType<Scp079CancelledRoomLockdownEventArgs>();
        UserData.RegisterType<Scp079GainingExperienceEventArgs>();
        UserData.RegisterType<Scp079GainedExperienceEventArgs>();
        UserData.RegisterType<Scp079LevelingUpEventArgs>();
        UserData.RegisterType<Scp079LeveledUpEventArgs>();
        UserData.RegisterType<Scp079LockingDoorEventArgs>();
        UserData.RegisterType<Scp079LockedDoorEventArgs>();
        UserData.RegisterType<Scp079LockingDownRoomEventArgs>();
        UserData.RegisterType<Scp079LockedDownRoomEventArgs>();
        UserData.RegisterType<Scp079RecontainingEventArgs>();
        UserData.RegisterType<Scp079RecontainedEventArgs>();
        UserData.RegisterType<Scp079UnlockingDoorEventArgs>();
        UserData.RegisterType<Scp079UnlockedDoorEventArgs>();
        UserData.RegisterType<Scp079UsingTeslaEventArgs>();
        UserData.RegisterType<Scp079UsedTeslaEventArgs>();
        UserData.RegisterType<Scp079PingingEventArgs>();
        UserData.RegisterType<Scp079PingedEventArgs>();
    }

    public void RegisterEvents()
    {
        LabApi.Events.Handlers.Scp079Events.BlackingOutRoom += OnBlackingOutRoom;
        LabApi.Events.Handlers.Scp079Events.BlackedOutRoom += OnBlackedOutRoom;
        LabApi.Events.Handlers.Scp079Events.BlackingOutZone += OnBlackingOutZone;
        LabApi.Events.Handlers.Scp079Events.BlackedOutZone += OnBlackedOutZone;
        LabApi.Events.Handlers.Scp079Events.ChangingCamera += OnChangingCamera;
        LabApi.Events.Handlers.Scp079Events.ChangedCamera += OnChangedCamera;
        LabApi.Events.Handlers.Scp079Events.CancellingRoomLockdown += OnCancellingRoomLockdown;
        LabApi.Events.Handlers.Scp079Events.CancelledRoomLockdown += OnCancelledRoomLockdown;
        LabApi.Events.Handlers.Scp079Events.GainingExperience += OnGainingExperience;
        LabApi.Events.Handlers.Scp079Events.GainedExperience += OnGainedExperience;
        LabApi.Events.Handlers.Scp079Events.LevelingUp += OnLevelingUp;
        LabApi.Events.Handlers.Scp079Events.LeveledUp += OnLeveledUp;
        LabApi.Events.Handlers.Scp079Events.LockingDoor += OnLockingDoor;
        LabApi.Events.Handlers.Scp079Events.LockedDoor += OnLockedDoor;
        LabApi.Events.Handlers.Scp079Events.LockingDownRoom += OnLockingDownRoom;
        LabApi.Events.Handlers.Scp079Events.LockedDownRoom += OnLockedDownRoom;
        LabApi.Events.Handlers.Scp079Events.Recontaining += OnRecontaining;
        LabApi.Events.Handlers.Scp079Events.Recontained += OnRecontained;
        LabApi.Events.Handlers.Scp079Events.UnlockingDoor += OnUnlockingDoor;
        LabApi.Events.Handlers.Scp079Events.UnlockedDoor += OnUnlockedDoor;
        LabApi.Events.Handlers.Scp079Events.UsingTesla += OnUsingTesla;
        LabApi.Events.Handlers.Scp079Events.UsedTesla += OnUsedTesla;
        LabApi.Events.Handlers.Scp079Events.Pinging += OnPinging;
        LabApi.Events.Handlers.Scp079Events.Pinged += OnPinged; 
    }

    public void UnregisterEvents()
    {
        LabApi.Events.Handlers.Scp079Events.BlackingOutRoom -= OnBlackingOutRoom;
        LabApi.Events.Handlers.Scp079Events.BlackedOutRoom -= OnBlackedOutRoom;
        LabApi.Events.Handlers.Scp079Events.BlackingOutZone -= OnBlackingOutZone;
        LabApi.Events.Handlers.Scp079Events.BlackedOutZone -= OnBlackedOutZone;
        LabApi.Events.Handlers.Scp079Events.ChangingCamera -= OnChangingCamera;
        LabApi.Events.Handlers.Scp079Events.ChangedCamera -= OnChangedCamera;
        LabApi.Events.Handlers.Scp079Events.CancellingRoomLockdown -= OnCancellingRoomLockdown;
        LabApi.Events.Handlers.Scp079Events.CancelledRoomLockdown -= OnCancelledRoomLockdown;
        LabApi.Events.Handlers.Scp079Events.GainingExperience -= OnGainingExperience;
        LabApi.Events.Handlers.Scp079Events.GainedExperience -= OnGainedExperience;
        LabApi.Events.Handlers.Scp079Events.LevelingUp -= OnLevelingUp;
        LabApi.Events.Handlers.Scp079Events.LeveledUp -= OnLeveledUp;
        LabApi.Events.Handlers.Scp079Events.LockingDoor -= OnLockingDoor;
        LabApi.Events.Handlers.Scp079Events.LockedDoor -= OnLockedDoor;
        LabApi.Events.Handlers.Scp079Events.LockingDownRoom -= OnLockingDownRoom;
        LabApi.Events.Handlers.Scp079Events.LockedDownRoom -= OnLockedDownRoom;
        LabApi.Events.Handlers.Scp079Events.Recontaining -= OnRecontaining;
        LabApi.Events.Handlers.Scp079Events.Recontained -= OnRecontained;
        LabApi.Events.Handlers.Scp079Events.UnlockingDoor -= OnUnlockingDoor;
        LabApi.Events.Handlers.Scp079Events.UnlockedDoor -= OnUnlockedDoor;
        LabApi.Events.Handlers.Scp079Events.UsingTesla -= OnUsingTesla;
        LabApi.Events.Handlers.Scp079Events.UsedTesla -= OnUsedTesla;
        LabApi.Events.Handlers.Scp079Events.Pinging -= OnPinging;
        LabApi.Events.Handlers.Scp079Events.Pinged -= OnPinged;
    }
}