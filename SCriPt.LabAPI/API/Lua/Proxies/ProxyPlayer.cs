using LabApi.Features.Wrappers;
using MapGeneration;
using MoonSharp.Interpreter;

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

    public Room Room => Player.Room;

    public FacilityZone Zone => Player.Zone;
    
}