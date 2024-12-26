using Exiled.API.Features;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp3114;
using PlayerRoles.Ragdolls;
using Scp3114Role = Exiled.API.Features.Roles.Scp3114Role;

namespace SCriPt.API.Lua.Proxy.Roles;

public class ProxyScp3114Role : ProxyFpcRole
{
    Scp3114Role Scp3114Role => (Scp3114Role) Role;
    
    public ProxyScp3114Role(Scp3114Role role) : base(role)
    {
    }
    
    public float SlapDamage => Scp3114Role.SlapDamage;
    
    public Player StrangleTarget => Scp3114Role.StrangleTarget;
    
    public RoleTypeId StolenRole
    {
        get => Scp3114Role.StolenRole;
        set => Scp3114Role.StolenRole = value;
    }
    
    public BasicRagdoll Ragdoll
    {
        get => Scp3114Role.Ragdoll;
        set => Scp3114Role.Ragdoll = value;
    }
    
    public byte UnitId
    {
        get => Scp3114Role.UnitId;
        set => Scp3114Role.UnitId = value;
    }
    
    public Scp3114Identity.DisguiseStatus DisguiseStatus
    {
        get => Scp3114Role.DisguiseStatus;
        set => Scp3114Role.DisguiseStatus = value;
    }
    
    public float DisguiseDuration
    {
        get => Scp3114Role.DisguiseDuration;
        set => Scp3114Role.DisguiseDuration = value;
    }
    
    public float WarningTime
    {
        get => Scp3114Role.WarningTime;
        set => Scp3114Role.WarningTime = value;
    }
    
    public void ResetIdentity() => Scp3114Role.ResetIdentity();
    
    //public void PlaySound(Scp3114VoiceLines.VoiceLinesName voiceLine = Scp3114VoiceLines.VoiceLinesName.RandomIdle) => Scp3114Role.PlaySound(voiceLine);
    
    
    
    
}