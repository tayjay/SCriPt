using System.Collections.Generic;
using System.Linq;
using LabApi.Features.Wrappers;
using MapGeneration;
using PlayerRoles;

namespace SCriPt.LabAPI.API.Lua.Globals;

public class GlobalPlayers
{
    public static IReadOnlyCollection<Player> List
    {
        get
        {
            return Player.List.Where(player => !player.IsHost).ToList();
        }
    }

    public static IReadOnlyCollection<Player> All()
    {
        return List;
    }

    public static Player ByName(string name)
    {
        Player.TryGetPlayersByName(name, out var list);
        return list.FirstOrDefault() ?? null;
    }
    
    public static Player ByUserId(string id)
    {
        return Player.Get(id);
    }
    
    public static Player ById(int id)
    {
        return Player.Get(id);
    }
    
    public static Player ByNetId(int netId)
    {
        return Player.Get((uint)netId);
    }
    
    public static Player ByReferenceHub(ReferenceHub hub)
    {
        return Player.Get(hub);
    }
    
    public static IReadOnlyCollection<Player> ByRole(RoleTypeId role)
    {
        return Player.List.Where(player => player.Role == role&& !player.IsHost).ToList();
    }
    
    public static IReadOnlyCollection<Player> ByTeam(Team team)
    {
        return Player.List.Where(player => player.Team == team&& !player.IsHost).ToList();
    }

    public static IReadOnlyCollection<Player> ByZone(FacilityZone zone)
    {
        return Player.List.Where(player => player.Zone == zone && !player.IsHost).ToList();
    }

    public static Player Random()
    {
        return List
            .OrderBy(_ => UnityEngine.Random.value)
            .FirstOrDefault() ?? null;
    }
    
    
    
    
}