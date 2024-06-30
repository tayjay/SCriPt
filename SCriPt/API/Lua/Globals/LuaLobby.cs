﻿using GameCore;
using MoonSharp.Interpreter;

namespace SCriPt.API.Lua.Globals
{
    [MoonSharpUserData]
    public class LuaLobby
    {
        public bool IsLocked => RoundStart.LobbyLock;
        public bool IsInLobby => !RoundStart.RoundStarted;
        
        public static void Lock()
        {
            RoundStart.LobbyLock = true;
        }
        
        public static void Unlock()
        {
            RoundStart.LobbyLock = false;
        }
    }
}