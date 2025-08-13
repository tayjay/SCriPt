using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LabApi.Features.Wrappers;
using MoonSharp.Interpreter;
using RemoteAdmin;

namespace SCriPt.LabAPI.API.Lua.Globals;

[MoonSharpUserData]
public class GlobalServer
{

    public static int PlayerCount => Server.PlayerCount;

    public static int MaxPlayers => Server.MaxPlayers;
    
    public static double Tps => Server.Tps;
    
    public static string Name => Server.ServerListName;

    public static string IpAddress => Server.IpAddress;
    
    public static int Port => Server.Port;

    
    public static bool IdleModeAvailable
    {
        get => Server.IdleModeAvailable;
        set => Server.IdleModeAvailable = value;
    }
    
    public static bool PauseIdleMode
    {
        get => Server.PauseIdleMode;
        set => Server.PauseIdleMode = value;
    }
    
    public bool IdleModeActive => Server.IdleModeActive;

    public static bool FriendlyFire
    {
        get => Server.FriendlyFire;
        set => Server.FriendlyFire = value;
    }

    public static List<Player> RemoteAdmins => Player.List.Where(p => p.RemoteAdminAccess).ToList();

    public static string RACommand(string command)
    {
        var _serverConsoleSender = new ServerConsoleSender(); // Depends on how you usually instantiate this

        // Get the RemoteAdmin.CommandProcessor type
        Type commandProcessorType = typeof(RemoteAdmin.CommandProcessor);

        // Get the ProcessQuery method
        MethodInfo processQueryMethod =
            commandProcessorType.GetMethod("ProcessQuery", BindingFlags.NonPublic | BindingFlags.Static);

        if (processQueryMethod != null)
        {
            // Invoking the ProcessQuery method
            var result = processQueryMethod.Invoke(_serverConsoleSender,
                new object[] { command, _serverConsoleSender });
            return result as string;
        }

        return null;
    }
    
    public static string RACommandAs(string command, Player player)
    {
        var _playerCommandSender = new PlayerCommandSender(player.ReferenceHub);

        // Get the RemoteAdmin.CommandProcessor type
        Type commandProcessorType = typeof(RemoteAdmin.CommandProcessor);

        // Get the ProcessQuery method
        MethodInfo processQueryMethod =
            commandProcessorType.GetMethod("ProcessQuery", BindingFlags.NonPublic | BindingFlags.Static);

        if (processQueryMethod != null)
        {
            // Invoking the ProcessQuery method
            var result = processQueryMethod.Invoke(_playerCommandSender,
                new object[] { command, _playerCommandSender });
            return result as string;
        }

        return null;
        //return CommandProcessor.ProcessQuery(command, new PlayerCommandSender(player.ReferenceHub));
    }

    public static string LACommand(string command)
    {
        return Server.RunCommand(command);
    }

    public static void SendBroadcast(string message, ushort duration = 5)
    {
        Server.SendBroadcast(message, duration);
    }

    public static void Restart()
    {
        Server.Restart();
    }

    public static void Shutdown()
    {
        Server.Shutdown();
    }
    
    
}