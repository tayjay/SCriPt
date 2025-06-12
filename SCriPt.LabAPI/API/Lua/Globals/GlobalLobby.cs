using GameCore;
using LabApi.Features.Wrappers;

namespace SCriPt.LabAPI.API.Lua.Globals;

public class GlobalLobby
{
    public bool IsLocked
    {
        get => RoundStart.LobbyLock;
        set
        {
            if (value)
                Lock();
            else
                Unlock();
        }
    }
    public bool IsInLobby => !RoundStart.RoundStarted;
        
    public static void Lock()
    {
        RoundStart.LobbyLock = true;
    }
        
    public static void Unlock()
    {
        RoundStart.LobbyLock = false;
    }

    public float TimeLeft { get => RoundStart.singleton.NetworkTimer; set => RoundStart.singleton.NetworkTimer = (short)value;}
}