using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using PlayerRoles;
using PlayerRoles.Ragdolls;

namespace SCriPt.API.Lua.Proxy.Roles;

public class ProxyScp049Role : ProxyFpcRole
{
    
    public Scp049Role Scp049Role => (Scp049Role) Role;
    
    public ProxyScp049Role(Scp049Role role) : base(role)
    {
    }
    
    public static HashSet<Player> TurnedPlayers => Scp049Role.TurnedPlayers;
    
    public bool IsRecalling => Scp049Role.IsRecalling;
    
    public bool IsCallActive => Scp049Role.IsCallActive;
    
    public Player RecallingPlayer => Scp049Role.RecallingPlayer;
    
    public Ragdoll RecallingRagdoll => Scp049Role.RecallingRagdoll;
    
    public IEnumerable<Player> DeadZombies => Scp049Role.DeadZombies;
    
    public Dictionary<Player, int> ResurrectedPlayers => Scp049Role.ResurrectedPlayers;
    
    public float CallCooldown
    {
        get => Scp049Role.CallCooldown;
        set => Scp049Role.CallCooldown = value;
    }
    
    public float GoodSenseCooldown
    {
        get => Scp049Role.GoodSenseCooldown;
        set => Scp049Role.GoodSenseCooldown = value;
    }
    
    public float RemainingAttackCooldown
    {
        get => Scp049Role.RemainingAttackCooldown;
        set => Scp049Role.RemainingAttackCooldown = value;
    }
    
    public float RemainingCallDuration
    {
        get => Scp049Role.RemainingCallDuration;
        set => Scp049Role.RemainingCallDuration = value;
    }
    
    public float RemainingGoodSenseDuration
    {
        get => Scp049Role.RemainingGoodSenseDuration;
        set => Scp049Role.RemainingGoodSenseDuration = value;
    }
    
    public float SenseDistance
    {
        get => Scp049Role.SenseDistance;
        set => Scp049Role.SenseDistance = value;
    }
    
    public void LoseSenseTarget() => Scp049Role.LoseSenseTarget();
    
    public bool Resurrect(Player player) => Scp049Role.Resurrect(player);
    
    public bool Resurrect(Ragdoll ragdoll) => Scp049Role.Resurrect(ragdoll);
    
    public void Attack(Player player) => Scp049Role.Attack(player);
    
    public void Sense(Player player) => Scp049Role.Sense(player);
    
    public void RefreshCallDuration() => Scp049Role.RefreshCallDuration();
    
    public int GetResurrectionCount(Player player) => Scp049Role.GetResurrectionCount(player);
    
    public bool CanResurrect(BasicRagdoll ragdoll) => Scp049Role.CanResurrect(ragdoll);
    
    public bool CanResurrect(Ragdoll ragdoll) => Scp049Role.CanResurrect(ragdoll);
    
    public bool IsInRecallRange(BasicRagdoll ragdoll) => Scp049Role.IsInRecallRange(ragdoll);
    
    public bool IsInRecallRange(Ragdoll ragdoll) => Scp049Role.IsInRecallRange(ragdoll);
    
    public float GetSpawnChance(List<RoleTypeId> alreadySpawned) => Scp049Role.GetSpawnChance(alreadySpawned);
}