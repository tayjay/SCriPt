using LabApi.Features.Wrappers;
using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.API.Lua.Globals;
[MoonSharpUserData]
public class GlobalWarhead
{
    public static void Lock()
    {
        Warhead.IsLocked = true;
    }
        
    public static void Unlock()
    {
        Warhead.IsLocked = false;
    }
        
    public static void Start()
    {
        Warhead.Start();
    }
        
    public static void Stop()
    {
        Warhead.Stop();
    }

    public static void Shake()
    {
        Warhead.Shake();
    }
    
    public static double CooldownTime
    {
        get => Warhead.CooldownTime;
        set => Warhead.CooldownTime = value;
    }
        
    public static float DetonationTime
    {
        get => Warhead.DetonationTime;
        set => Warhead.DetonationTime = value;
    }
        
    public static bool LeverStatus
    {
        get => Warhead.LeverStatus;
        set => Warhead.LeverStatus = value;
    }
}