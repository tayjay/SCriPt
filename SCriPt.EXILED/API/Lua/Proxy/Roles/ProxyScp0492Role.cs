using Exiled.API.Features;
using Exiled.API.Features.Roles;

namespace SCriPt.EXILED.API.Lua.Proxy.Roles;

public class ProxyScp0492Role : ProxyFpcRole
{
    public Scp0492Role Scp0492Role => (Scp0492Role) Role;
    public ProxyScp0492Role(Scp0492Role role) : base(role)
    {
    }
    
    public int ResurrectNumber
    {
        get => Scp0492Role.ResurrectNumber;
        set => Scp0492Role.ResurrectNumber = value;
    }
    
    public float AttackDamage => Scp0492Role.AttackDamage;
    
    public float SimulatedStare
    {
        get => Scp0492Role.SimulatedStare;
        set => Scp0492Role.SimulatedStare = value;
    }
    
    public bool BloodlustActive => Scp0492Role.BloodlustActive;
    
    public bool IsConsuming => Scp0492Role.IsConsuming;
    
    public Ragdoll RagdollConsuming => Scp0492Role.RagdollConsuming;
    
    public float AttackCooldown => Scp0492Role.AttackCooldown;
    
    public bool IsInConsumeRange(Ragdoll ragdoll) => Scp0492Role.IsInConsumeRange(ragdoll);
    
    
    
    
}