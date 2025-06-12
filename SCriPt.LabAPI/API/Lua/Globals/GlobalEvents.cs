using MoonSharp.Interpreter;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI.API.Lua.Globals;

[MoonSharpUserData]
public class GlobalEvents
{
    public static LuaServerEvents Server => SCriPt.ServerEvents;
    
    public static LuaPlayerEvents Player => SCriPt.PlayerEvents;
    
    public static LuaWarheadEvents Warhead => SCriPt.WarheadEvents;
    
    
}