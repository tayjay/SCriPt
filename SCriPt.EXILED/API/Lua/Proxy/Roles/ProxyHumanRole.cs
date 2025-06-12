using Exiled.API.Features.Roles;
using Respawning;

namespace SCriPt.EXILED.API.Lua.Proxy.Roles;

public class ProxyHumanRole : ProxyFpcRole
{
    public HumanRole HumanRole => (HumanRole) Role;
    
    public ProxyHumanRole(HumanRole role) : base(role)
    {
        
    }
    
    //todo replace with new team type
    /*public SpawnableTeamType SpawnableTeamType
    {
        get => HumanRole.SpawnableTeamType;
        set => HumanRole.SpawnableTeamType = value;
    }*/
    
    public string UnitName => HumanRole.UnitName;
    
    public byte UnitNameId
    {
        get => HumanRole.UnitNameId;
        set => HumanRole.UnitNameId = value;
    }
    
    public bool UsesUnitNames => HumanRole.UsesUnitNames;
    
}