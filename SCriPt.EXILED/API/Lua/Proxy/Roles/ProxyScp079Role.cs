using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Roles;
using Interactables.Interobjects.DoorUtils;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp079;
using UnityEngine;
using Scp079Role = Exiled.API.Features.Roles.Scp079Role;

namespace SCriPt.EXILED.API.Lua.Proxy.Roles;

public class ProxyScp079Role : ProxyRole
{
    public Scp079Role Scp079Role => (Scp079Role) Role;
    public ProxyScp079Role(Scp079Role role) : base(role)
    {
        
    }
    
    public Exiled.API.Features.Camera Camera
    {
        get => Scp079Role.Camera;
        set => Scp079Role.Camera = value;
    }
    
    public bool CanTransmit => Scp079Role.CanTransmit;
    
    public IEnumerable<Exiled.API.Features.Room> MarkedRooms => Scp079Role.MarkedRooms;
    
    public Scp079Speaker Speaker => Scp079Role.Speaker;
    
    public Door LockedDoor => Scp079Role.LockedDoor;
    
    public int Experience
    {
        get => Scp079Role.Experience;
        set => Scp079Role.Experience = value;
    }
    
    public int Level
    {
        get => Scp079Role.Level;
        set => Scp079Role.Level = value;
    }
    
    public int LevelIndex => Scp079Role.LevelIndex;
    
    public Vector3 CameraPosition => Scp079Role.CameraPosition;
    
    public int NextLevelThreshold => Scp079Role.NextLevelThreshold;
    
    public float Energy
    {
        get => Scp079Role.Energy;
        set => Scp079Role.Energy = value;
    }
    
    public float MaxEnergy
    {
        get => Scp079Role.MaxEnergy;
        set => Scp079Role.MaxEnergy = value;
    }
    
    public float RoomLockdownCooldown
    {
        get => Scp079Role.RoomLockdownCooldown;
        set => Scp079Role.RoomLockdownCooldown = value;
    }
    
    public float RemainingLockdownDuration => Scp079Role.RemainingLockdownDuration;
    
    public int BlackoutCount => Scp079Role.BlackoutCount;
    
    public int BlackoutCapacity => Scp079Role.BlackoutCapacity;
    
    public float BlackoutZoneCooldown
    {
        get => Scp079Role.BlackoutZoneCooldown;
        set => Scp079Role.BlackoutZoneCooldown = value;
    }
    
    public float Scp2176LostTime
    {
        get => Scp079Role.Scp2176LostTime;
        set => Scp079Role.Scp2176LostTime = value;
    }
    
    public float RollRotation => Scp079Role.RollRotation;
    
    public bool IsLost => Scp079Role.IsLost;
    
    public float LostTime => Scp079Role.LostTime;
    
    public float EnergyRegenerationSpeed => Scp079Role.EnergyRegenerationSpeed;
    
    public void UnlockAllDoors() => Scp079Role.UnlockAllDoors();
    
    public void LoseSignal(float duration) => Scp079Role.LoseSignal(duration);
    
    public void AddExperience(int amount, Scp079HudTranslation reason = Scp079HudTranslation.ExpGainAdminCommand) => Scp079Role.AddExperience(amount, reason);
    
    public void AddExperience(int amount, Scp079HudTranslation reason, RoleTypeId subject) => Scp079Role.AddExperience(amount, reason, subject);
    
    public bool LockDoor(Door door) => Scp079Role.LockDoor(door);
    
    public bool LockDoor(Door door, bool consumeEnergy = true) => Scp079Role.LockDoor(door, consumeEnergy);
    
    public void UnlockDoor() => Scp079Role.UnlockDoor();
    
    public void UnlockDoor(Door door) => Scp079Role.UnlockDoor(door);
    
    public void MarkRoom(Exiled.API.Features.Room room) => Scp079Role.MarkRoom(room);
    
    public void MarkRooms(IEnumerable<Exiled.API.Features.Room> rooms) => Scp079Role.MarkRooms(rooms);
    
    public void UnmarkRoom(Exiled.API.Features.Room room) => Scp079Role.UnmarkRoom(room);
    
    public void ClearMarkedRooms() => Scp079Role.ClearMarkedRooms();
    
    public int GetSwitchCost(Exiled.API.Features.Camera camera) => Scp079Role.GetSwitchCost(camera);
    
    public int GetCost(Door door, DoorAction action) => Scp079Role.GetCost(door, action);
    
    public void BlackoutRoom(bool consumeEnergy = true) => Scp079Role.BlackoutRoom(consumeEnergy);
    
    public void BlackoutZone(bool consumeEnergy = true) => Scp079Role.BlackoutZone(consumeEnergy);
    
    public void Ping(Vector3 position, PingType pingType = PingType.Default, bool consumeEnergy = true) => Scp079Role.Ping(position, pingType, consumeEnergy);
    
    public void LockdownRoom() => Scp079Role.LockdownRoom();
    
    public void CancelLockdown() => Scp079Role.CancelLockdown();
    
    public void ActivateTesla(bool consumeEnergy = true) => Scp079Role.ActivateTesla(consumeEnergy);
    
    public float GetSpawnChance(List<RoleTypeId> alreadySpawned) => Scp079Role.GetSpawnChance(alreadySpawned);
    
    
    
    
    
}