using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Roles;

namespace SCriPt.API.Lua.Proxy.Roles;

public class ProxyScp096Role : ProxyFpcRole
{
    
    public Scp096Role Scp096Role => (Scp096Role) Role;
    
    public ProxyScp096Role(Scp096Role role) : base(role)
    {
    }
    
    public bool CanReceiveTargets => Scp096Role.CanReceiveTargets;
    
    public bool AttackPossible => Scp096Role.AttackPossible;
    
    public float ChargeCooldown
    {
        get => Scp096Role.ChargeCooldown;
        set => Scp096Role.ChargeCooldown = value;
    }
    
    public float RemainingChargeDuration
    {
        get => Scp096Role.RemainingChargeDuration;
        set => Scp096Role.RemainingChargeDuration = value;
    }
    
    public float EnrageCooldown
    {
        get => Scp096Role.EnrageCooldown;
        set => Scp096Role.EnrageCooldown = value;
    }
    
    public float EnragedTimeLeft
    {
        get => Scp096Role.EnragedTimeLeft;
        set => Scp096Role.EnragedTimeLeft = value;
    }
    
    public float TotalEnrageTime
    {
        get => Scp096Role.TotalEnrageTime;
        set => Scp096Role.TotalEnrageTime = value;
    }
    
    public bool TryNotToCryActive
    {
        get => Scp096Role.TryNotToCryActive;
        set => Scp096Role.TryNotToCryActive = value;
    }
    
    public List<Player> Targets => Scp096Role.Targets.ToList();
    
    public bool AddTarget(Player player) => Scp096Role.AddTarget(player);
    
    public bool AddTarget(Player player, bool isLooking) => Scp096Role.AddTarget(player, isLooking);
    
    public bool RemoveTarget(Player player) => Scp096Role.RemoveTarget(player);
    
    public void Enrage(float time = 20f) => Scp096Role.Enrage(time);
    
    public void Calm(bool clearTime = true) => Scp096Role.Calm(clearTime);
    
    public bool HasTarget(Player player) => Scp096Role.HasTarget(player);
    
    public bool IsObserved(Player player) => Scp096Role.IsObserved(player);
    
    public void ClearTargets() => Scp096Role.ClearTargets();
    
    public void Attack() => Scp096Role.Attack();
    
    public void Charge(float cooldown = 1f) => Scp096Role.Charge(cooldown);
    
    public void ShowRageInput(float duration = 10f) => Scp096Role.ShowRageInput(duration);
    
    
    
}