using System;
using MoonSharp.Interpreter;
using SCriPt.API.Lua.Objects;
using UnityEngine;

namespace SCriPt.API.Lua.Globals;

[MoonSharpUserData]
public class LuaNew
{
    public static Func<float, float, float, Vector3> Vector3 = ((x, y, z) => new Vector3(x, y, z));
    public static Func<float, float, Vector2> Vector2 = ((x, y) => new Vector2(x, y));
    public static Func<float, float, float, float, Quaternion> Quaternion = ((x, y, z, w) => new Quaternion(x, y, z, w));
    public static Func<float, float, float, float, Color> Color = ((r, g, b, a) => new Color(r, g, b, a));
    
}