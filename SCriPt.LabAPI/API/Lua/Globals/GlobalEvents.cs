using MoonSharp.Interpreter;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI.API.Lua.Globals;

[MoonSharpUserData]
public class GlobalEvents
{
    public static LuaServerEvents Server => SCriPt.Instance.ServerEvents;
    
    public static LuaPlayerEvents Player => SCriPt.Instance.PlayerEvents;
    
    public static LuaWarheadEvents Warhead => SCriPt.Instance.WarheadEvents;
    
    public static LuaScp049Events Scp049 => SCriPt.Instance.Scp049Events;
    public static LuaScp049Events Doctor => SCriPt.Instance.Scp049Events;
    
    public static LuaScp096Events Scp096 => SCriPt.Instance.Scp096Events;
    public static LuaScp096Events ShyGuy => SCriPt.Instance.Scp096Events;
    
    public static LuaScp173Events Scp173 => SCriPt.Instance.Scp173Events;
    public static LuaScp173Events Peanut => SCriPt.Instance.Scp173Events;
    
    public static LuaScp106Events Scp106 => SCriPt.Instance.Scp106Events;
    public static LuaScp106Events Larry => SCriPt.Instance.Scp106Events;
    
    public static LuaScp939Events Scp939 => SCriPt.Instance.Scp939Events;
    public static LuaScp939Events Dog => SCriPt.Instance.Scp939Events;
    
    public static LuaScp0492Events Scp0492 => SCriPt.Instance.Scp0492Events;
    public static LuaScp0492Events Zombie => SCriPt.Instance.Scp0492Events;
    
    public static LuaScp914Events Scp914 => SCriPt.Instance.Scp914Events;
    public static LuaObjectiveEvents Objective => SCriPt.Instance.ObjectiveEvents;
    public static LuaScp079Events Scp079 => SCriPt.Instance.Scp079Events;
    public static LuaScp127Events Scp127 => SCriPt.Instance.Scp127Events;
    public static LuaScp3114Events Scp3114 => SCriPt.Instance.Scp3114Events;
    public static LuaScp3114Events Skeleton => SCriPt.Instance.Scp3114Events;
    
    
    
}