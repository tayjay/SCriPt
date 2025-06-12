using System;
using MoonSharp.Interpreter;

namespace SCriPt.EXILED.API.Lua.Globals;

[MoonSharpUserData]
public class LuaEnums
{
    public static string[] GetValues(Type type)
    {
        return Enum.GetNames(type);
    } 
}