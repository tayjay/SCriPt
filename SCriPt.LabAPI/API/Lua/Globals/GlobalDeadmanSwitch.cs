namespace SCriPt.LabAPI.API.Lua.Globals;

[MoonSharp.Interpreter.MoonSharpUserData]
public class GlobalDeadmanSwitch
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
    
    //todo: Temp, likely won't work this way as TimeLeft is changed on spawn wave
    public static bool IsDeadmanSwitchEnabled
    {
        get => DeadmanSwitch.IsDeadmanSwitchEnabled;
        set => DeadmanSwitch.ForceCountdownToggle = value;
    }
    
    public static void Disable()
    {
        IsDeadmanSwitchEnabled = false;
    }
    
    public static void Enable()
    {
        IsDeadmanSwitchEnabled = true;
    }
}