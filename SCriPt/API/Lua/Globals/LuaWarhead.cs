using Exiled.API.Features;
using MoonSharp.Interpreter;

namespace SCriPt.API.Lua.Globals
{
    [MoonSharpUserData]
    public class LuaWarhead
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
        
        public static float DetonationTimer
        {
            get => Warhead.DetonationTimer;
            set => Warhead.DetonationTimer = value;
        }
        
        public static bool LevelStatus
        {
            get => Warhead.LeverStatus;
            set => Warhead.LeverStatus = value;
        }
    }
}