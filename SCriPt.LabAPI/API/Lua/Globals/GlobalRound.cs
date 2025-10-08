using CommandSystem.Commands.RemoteAdmin;
using LabApi.Features.Wrappers;

namespace SCriPt.LabAPI.API.Lua.Globals;

public class GlobalRound
{
    public static void Lock()
    {
        Round.IsLocked = true;
    }
        
    public static void Unlock()
    {
        Round.IsLocked = false;
    }
        
    public static void Start()
    {
        //RoundSummary.singleton.Start();
        Round.Start();
    }
        
    public static void Restart()
    {
        //RoundRestart.InitiateRoundRestart();
        Round.Restart();
    }
    
    public static void End()
    {
        //RoundSummary.singleton.End();
        Round.End();
    }

    public static bool CanRoundEnd => Round.CanRoundEnd;
    
    public static bool InProgress => Round.IsRoundInProgress;
        
    public static bool IsLocked => Round.IsLocked;
        
    public static bool IsEnded => Round.IsRoundEnded;

    public static int ExtraTargets
    {
        get => Round.ExtraTargets;
        set => Round.ExtraTargets = value;
    }
    
    public static int TargetCount
    {
        get => RoundSummary.singleton.Network_targetCount;
        set => RoundSummary.singleton.Network_targetCount = value;
    }
}