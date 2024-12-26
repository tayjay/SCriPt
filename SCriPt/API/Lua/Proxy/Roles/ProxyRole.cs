using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.API.Features.Spawn;
using PlayerRoles;
using UnityEngine;

namespace SCriPt.API.Lua.Proxy.Roles;

public class ProxyRole
{
    public Role Role { get; }
    
    public ProxyRole(Role role)
    {
        Role = role;
    }
    
    public RoleTypeId Id => Role.Type;
    
    public RoleTypeId Type => Role.Type;
    
    public string Name => Role.Name;
    
    public Player Owner => Role.Owner;
    
    public Color Color => Role.Color;
    
    public Side Side => Role.Side;
    
    public Team Team => Role.Team;
    
    public TimeSpan ActiveTime => Role.ActiveTime;
    
    public bool IsAlive => Role.IsAlive;
    
    public bool IsDead => Role.IsDead;
    
    public bool IsValid => Role.IsValid;
    
    public RoleSpawnFlags SpawnFlags => Role.SpawnFlags;
    
    public RoleChangeReason SpawnReason => Role.SpawnReason;
    
    public SpawnLocation RandomSpawnLocation => Role.RandomSpawnLocation;
    
}