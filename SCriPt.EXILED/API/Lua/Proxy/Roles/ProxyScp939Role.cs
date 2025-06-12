using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles.PlayableScps.Scp939;
using UnityEngine;
using Scp939Role = Exiled.API.Features.Roles.Scp939Role;

namespace SCriPt.EXILED.API.Lua.Proxy.Roles;

public class ProxyScp939Role : ProxyFpcRole
{
    public Scp939Role Scp939Role => (Scp939Role) Role;
    
    public ProxyScp939Role(Scp939Role role) : base(role)
    {
    }
    
    public float AttackCooldown
    {
        get => Scp939Role.AttackCooldown;
        set => Scp939Role.AttackCooldown = value;
    }
    
    public bool IsFocused => Scp939Role.IsFocused;
    
    public bool IsLunging => Scp939Role.IsLunging;
    
    public Scp939LungeState LungeState => Scp939Role.LungeState;
    
    public float AmnesticCloudCooldown
    {
        get => Scp939Role.AmnesticCloudCooldown;
        set => Scp939Role.AmnesticCloudCooldown = value;
    }
    
    public float AmnesticCloudDuration
    {
        get => Scp939Role.AmnesticCloudDuration;
        set => Scp939Role.AmnesticCloudDuration = value;
    }
    
    public float MimicryCooldown
    {
        get => Scp939Role.MimicryCooldown;
        set => Scp939Role.MimicryCooldown = value;
    }
    
    public int SavedVoices => Scp939Role.SavedVoices;
    
    public Vector3? MimicryPointPosition => Scp939Role.MimicryPointPosition;
    
    public List<Player> VisiblePlayers => Scp939Role.VisiblePlayers.ToList();
    
    public void ClearRecordings(Player target) => Scp939Role.ClearRecordings(target);
    
    public void ClearRecordings() => Scp939Role.ClearRecordings();
    
    public void PlayRippleSound(UsableRippleType ripple, Vector3 position, Player playerToSend) => Scp939Role.PlayRippleSound(ripple, position, playerToSend);
    
    public void CreateCloud(float duration) => Scp939Role.CreateCloud(duration);
    
    public void PlaceMimicPoint(Vector3 mimicPointPosition) => Scp939Role.PlaceMimicPoint(mimicPointPosition);
    
    public void DestroyCurrentMimicPoint() => Scp939Role.DestroyCurrentMimicPoint();
    
    
    
    
}