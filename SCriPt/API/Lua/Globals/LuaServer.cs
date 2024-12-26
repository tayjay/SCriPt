using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Exiled.API.Enums;
using Exiled.Events.Handlers;
using MoonSharp.Interpreter;
using PlayerRoles;
using RemoteAdmin;
using SCriPt.API.Lua.Objects;
using Player = Exiled.API.Features.Player;
using Server = Exiled.API.Features.Server;

namespace SCriPt.API.Lua.Globals
{
    [MoonSharpUserData]
    public class LuaServer
    {
        private static ServerConsoleSender _serverConsoleSender = new ServerConsoleSender();
        private static ServerDiagnostics _serverDiagnostics = new ServerDiagnostics();
        
        public static int PlayerCount => Exiled.API.Features.Player.List.Count;
        public static int MaxPlayerCount => Server.MaxPlayerCount;
        public static int NpcCount => Exiled.API.Features.Npc.List.Count;
        public static int ScpCount => Player.List.Count(p => p.Role.Team == Team.SCPs);
        public static int HumanCount => Player.List.Count(p => p.Role.Side == Side.Mtf || p.Role.Side == Side.ChaosInsurgency);
        public static int DClassCount => Player.List.Count(p => p.Role.Team == Team.ClassD);
        public static int ScientistCount => Player.List.Count(p => p.Role.Team == Team.Scientists);
        public static int FoundationCount => Player.List.Count(p => p.Role.Team == Team.FoundationForces);
        public static int ChaosCount => Player.List.Count(p => p.Role.Team == Team.ChaosInsurgency);

        public static double Tps => Server.Tps;
        public static double Frametime => Server.Frametime;
        public static string Name => Server.Name;
        public static string Ip => Server.IpAddress;
        public static int Port => Server.Port;
        public static string Version => Server.Version;
        public static bool StreamingAllowed => Server.StreamingAllowed;
        public static bool IsBeta => Server.IsBeta;
        
        public static ServerDiagnostics Diagnostics
        {
            get
            {
                if(!_serverDiagnostics.IsRunning)
                    _serverDiagnostics.Start();
                return _serverDiagnostics;
            }
        }

        public static bool IsIdleModeEnabled
        {
            get => Server.IsIdleModeEnabled;
            set => Server.IsIdleModeEnabled = value;
        }
        
        
        public static bool FriendlyFire
        {
            get => Server.FriendlyFire;
            set => Server.FriendlyFire = value;
        }

        public static List<Player> Players => Player.List.ToList();
        
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
            //return RemoteAdmin.CommandProcessor.ProcessQuery(command, _serverConsoleSender);
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
            return Exiled.API.Features.Server.ExecuteCommand(command);
        }

        public static void SendStaffMessage(string message)
        {
            foreach(Player player in RemoteAdmins)
                player.SendStaffMessage(message);
        }

        public static void Restart()
        {
            Server.Restart();
        }
        
        public static void Shutdown()
        {
            Server.Shutdown();
        }
        
        public static void SendBroadcast(string message, ushort duration)
        {
            Server.Broadcast.RpcAddElement(message,duration,Broadcast.BroadcastFlags.Normal);
        }
        
        public static object GetSessionVariable(string key)
        {
            if(Server.TryGetSessionVariable(key, out object value))
                return value;
            return null;
        }
        
        public static void SetSessionVariable(string key, object value)
        {
            Server.SessionVariables[key] = value;
        }
    }
}