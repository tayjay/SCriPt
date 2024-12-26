using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Hazards;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp173;
using UnityEngine;
using Scp173Role = Exiled.API.Features.Roles.Scp173Role;

namespace SCriPt.API.Lua.Proxy.Roles;

public class ProxyScp173Role : ProxyRole
{
    public Scp173Role Scp173 { get; }
    
    public ProxyScp173Role(Scp173Role role) : base(role)
    {
        Scp173 = role;
    }

    public bool BlinkReady
    {
        get => Scp173.BlinkReady;
        set => Scp173.BlinkReady = value;
    }

    public float BlinkCooldown
    {
        get => Scp173.BlinkCooldown;
        set => Scp173.BlinkCooldown = value;
    }
    
    public bool BreakneckActive
    {
        get => Scp173.BreakneckActive;
        set => Scp173.BreakneckActive = value;
    }

    public float RemainingBreakneckCooldown
    {
        get => Scp173.RemainingBreakneckCooldown;
        set => Scp173.RemainingBreakneckCooldown = value;
    }
    
    public float RemainingTantrumCooldown
    {
        get => Scp173.RemainingTantrumCooldown;
        set => Scp173.RemainingTantrumCooldown = value;
    }

    public float SimulatedStare
    {
        get => Scp173.SimulatedStare;
        set => Scp173.SimulatedStare = value;
    }

    public List<Player> ObservingPlayers => Scp173.ObservingPlayers.ToList();


    public bool IsObserved => Scp173.IsObserved;
    
    public void Blink(Vector3 position) => Scp173.Blink(position);
    
    public TantrumHazard PlaceTantrum(bool failIfObserved = false, float cooldown = 0.0f) => Scp173.PlaceTantrum(failIfObserved, cooldown);
    
    public void SendAudio(Scp173AudioPlayer.Scp173SoundId soundId) => Scp173.SendAudio(soundId);
    
    
}