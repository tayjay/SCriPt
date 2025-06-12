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
    
    public static bool InProgress => RoundSummary.RoundInProgress();
        
    public static bool IsLocked => RoundSummary.RoundLock;
        
    public static bool IsEnded => Round.IsRoundEnded;
}