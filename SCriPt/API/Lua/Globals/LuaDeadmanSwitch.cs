using MoonSharp.Interpreter;

namespace SCriPt.API.Lua.Globals;

[MoonSharpUserData]
public class LuaDeadmanSwitch
{

    public static float TimeLeft
    {
        get => DeadmanSwitch._dmsTime;
        set => DeadmanSwitch._dmsTime = value;
    }
    
    public static void Initiate(float addDelay = 0f)
    {
        DeadmanSwitch.InitiateProtocol();
        if(addDelay<=0) return;
        DeadmanSwitch._dmsTime += addDelay;
    }
    
    public static void Disable()
    {
        DeadmanSwitch._dmsActive = false;
        DeadmanSwitch._dmsTime = float.MaxValue;
    }
}