using Exiled.API.Features;
using MoonSharp.Interpreter;
using RoundRestarting;

namespace SCriPt.API.Lua.Globals
{
    [MoonSharpUserData]
    public class LuaRound
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

        public static bool InProgress => RoundSummary.RoundInProgress();
        
        public static bool IsLocked => RoundSummary.RoundLock;
        
        public static bool IsEnded => Round.IsEnded;
    }
}