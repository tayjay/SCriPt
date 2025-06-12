using MoonSharp.Interpreter;

namespace SCriPt.EXILED.API.Lua.Globals;

[MoonSharpUserData]
public class LuaDeadmanSwitch
{

    public static float TimeLeft
    {
        get => DeadmanSwitch.CountdownTimeLeft;
        set => DeadmanSwitch.CountdownTimeLeft = value;
    }
    
    public static void Initiate(float addDelay = 0f)
    {
        DeadmanSwitch.InitiateProtocol();
        if(addDelay<=0) return;
        DeadmanSwitch.CountdownTimeLeft += addDelay;
    }
    
    public static void Disable()
    {
        DeadmanSwitch.IsSequenceActive = false;
        DeadmanSwitch.CountdownTimeLeft = float.MaxValue;
    }
}