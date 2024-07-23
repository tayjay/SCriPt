using System.Linq;
using MoonSharp.Interpreter;

namespace SCriPt.API.Lua.Globals;

[MoonSharpUserData]
public class LuaSCriPt
{
    public static string Version => SCriPt.Instance.Version.ToString();
    public static int LoadedScripts => SCriPt.Instance.LoadedScripts.Count();
    public static string MoonSharpVersion => MoonSharp.Interpreter.Script.VERSION;
    public static string LuaVersion => MoonSharp.Interpreter.Script.LUA_VERSION;
}