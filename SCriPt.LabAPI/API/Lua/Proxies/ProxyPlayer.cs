using JetBrains.Annotations;
using LabApi.Features.Extensions;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using MapGeneration;
using Mirror;
using MoonSharp.Interpreter;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Thirdperson;
using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace SCriPt.LabAPI.API.Lua.Proxies;

[MoonSharpUserData]
public class ProxyPlayer
{
    Player Player { get; }
        
    [MoonSharpHidden]
    public ProxyPlayer(Player player)
    {
        Player = player;
    }
    
    public string DisplayName
    {
        get => Player.DisplayName;
        set => Player.DisplayName = value;
    }

    public int PlayerId => Player.PlayerId;
    
    public string UserId => Player.UserId;
    
    public string IpAddress => Player.IpAddress;
    
    public bool IsHost => Player.IsHost;
    
    public float Health
    {
        get => Player.Health;
        set => Player.Health = value;
    }
    
    public float MaxHealth
    {
        get => Player.MaxHealth;
        set => Player.MaxHealth = value;
    }
    
    public float ArtificialHealth
    {
        get => Player.ArtificialHealth;
        set => Player.ArtificialHealth = value;
    }
    
    public float MaxArtificialHealth
    {
        get => Player.MaxArtificialHealth;
        set => Player.MaxArtificialHealth = value;
    }
    
    public float HumeShield
    {
        get => Player.HumeShield;
        set => Player.HumeShield = value;
    }

    public Vector3 Position 
    {
        get => Player.Position;
        set => Player.Position = value;
    }
    
    public Quaternion Rotation
    {
        get => Player.Rotation;
        set => Player.Rotation = value;
    }
    public Room Room => Player.Room;

    public FacilityZone Zone => Player.Zone;

    public Vector3 Gravity
    {
        get => Player.Gravity;
        set => Player.Gravity = value;
    }

    public bool DoNotTrack => Player.DoNotTrack;
    
    public bool IsDummy => Player.IsDummy;

    public bool IsDead => !Player.IsAlive;
    public bool IsAlive => Player.IsAlive;
    
    public RoleTypeId Role => Player.Role;

    public bool IsScp => Player.Role.IsScp();
    
    public bool IsHuman => Player.Role.IsHuman();
    public bool IsSpeaking => Player.IsSpeaking;
    
    [CanBeNull] public Player CurrentlySpectating => Player.CurrentlySpectating;
    
    public Item CurrentItem
    {
        get => Player.CurrentItem;
        set => Player.CurrentItem = value;
    }

    public EmotionPresetType Emotion
    {
        get => EmotionSync.GetEmotionPreset(Player.ReferenceHub);
        set => Player.ReferenceHub.ServerSetEmotionPreset(value);
    }
    
    public void SetRole(RoleTypeId newRole, RoleChangeReason reason = RoleChangeReason.RemoteAdmin, RoleSpawnFlags flags = RoleSpawnFlags.All)
    {
        Player.SetRole(newRole, reason, flags);
    }

    public void Move(Vector3 delta)
    {
        Player.Move(delta);
    }
    
    public Faction Faction
    {
        get => Player.Faction;
    }
    
    public NetworkConnectionToClient ConnectionToClient => Player.ConnectionToClient;
    
    public int LifeId => Player.LifeId;
    
    public void Jump()
    {
        Player.Jump();
    }
    
    public void Jump(float force)
    {
        Player.Jump(force);
    }
    
    
    public string[] GetPermissions()
    {
        return Player.GetPermissions();
    }
    
    
    public void Test()
    {
    }
    
    public void SendConsoleMessage(string message, string color = "green")
    {
        Player.SendConsoleMessage(message, color);
    }
    
    public void EnableEffect(string effectName, byte intensity, float duration)
    {
        Player.TryGetEffect(effectName, out var effect);
        if (effect == null)
        {
            Logger.Error("Effect not found: " + effectName);
            return;
        }
        Player.EnableEffect(effect,intensity, duration);
    }
    
    public void DisableEffect(string effectName)
    {
        Player.TryGetEffect(effectName, out var effect);
        if (effect == null)
        {
            Logger.Error("Effect not found: " + effectName);
            return;
        }
        Player.DisableEffect(effect);
    }
    
    public void SendHint(string message, float duration = 5f)
    {
        Player.SendHint(message, duration);
    }

    public void SendHitMarker(float size = 1f)
    {
        Player.SendHitMarker(size);
    }
    
}