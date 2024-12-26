using Exiled.API.Features;
using Exiled.API.Features.Roles;
using UnityEngine;

namespace SCriPt.API.Lua.Proxy.Roles;

public class ProxyScp106Role : ProxyFpcRole
{
    public Scp106Role Scp106Role => (Scp106Role) Role;
    public ProxyScp106Role(Scp106Role role) : base(role)
    {
    }
    
    public float Vigor
    {
        get => Scp106Role.Vigor;
        set => Scp106Role.Vigor = value;
    }
    
    public bool IsSubmerged
    {
        get => Scp106Role.IsSubmerged;
        set => Scp106Role.IsSubmerged = value;
    }
    
    public bool CanActivateTesla => Scp106Role.CanActivateTesla;
    
    public bool CanStopStalk => Scp106Role.CanStopStalk;
    
    public bool IsSlowdown => Scp106Role.IsSlowdown;
    
    public float SinkholeCurrentTime => Scp106Role.SinkholeCurrentTime;
    
    //public float SinkholeNormalizedState => Scp106Role.SinkholeNormalizedState;
    
    public bool IsDuringAnimation => Scp106Role.IsDuringAnimation;
    
    public bool IsSinkholeHidden => Scp106Role.IsSinkholeHidden;
    
    public bool SinkholeState
    {
        get => Scp106Role.SinkholeState;
        set => Scp106Role.SinkholeState = value;
    }
    
    public float SinkholeTargetDuration => Scp106Role.SinkholeTargetDuration;
    
    //public float SinkholeSpeedMultiplier => Scp106Role.SinkholeSpeedMultiplier;
    
    public int AttackDamage
    {
        get => Scp106Role.AttackDamage;
        set => Scp106Role.AttackDamage = value;
    }
    
    public float CaptureCooldown
    {
        get => Scp106Role.CaptureCooldown;
        set => Scp106Role.CaptureCooldown = value;
    }
    
    public float RemainingSinkholeCooldown
    {
        get => Scp106Role.RemainingSinkholeCooldown;
        set => Scp106Role.RemainingSinkholeCooldown = value;
    }
    
    public bool IsStalking
    {
        get => Scp106Role.IsStalking;
        set => Scp106Role.IsStalking = value;
    }
    
    public bool UsePortal(Vector3 position, float cost = 0.0f) => Scp106Role.UsePortal(position, cost);
    
    public void CapturePlayer(Player player) => Scp106Role.CapturePlayer(player);
    
    
    
}