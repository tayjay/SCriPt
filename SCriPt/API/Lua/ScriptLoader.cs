using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Interfaces;
using Exiled.Events.EventArgs.Player;
using GameCore;
using InventorySystem;
using InventorySystem.Items.Pickups;
using LightContainmentZoneDecontamination;
using MEC;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter.Platforms;
using PlayerRoles;
using SCriPt.API.Connectors;
using SCriPt.API.Lua.Proxy;
using SCriPt.API.Lua.Proxy.Events;
using SCriPt.API.Lua.Globals;
using UnityEngine;
using Log = Exiled.API.Features.Log;

namespace SCriPt.API.Lua
{
    public class ScriptLoader
    {
        
        public static Script AutoLoadScript = new Script();

        public static string ScriptPath => SCriPt.Instance.Config.ScriptPath;

        public static CoreModules SandboxLevel
        {
            get
            {
                switch (SCriPt.Instance.Config.SandboxLevel)
                {
                    case "Hard":
                        return CoreModules.Preset_HardSandbox;
                    case "Soft":
                        return CoreModules.Preset_SoftSandbox;
                    case "Default":
                        return CoreModules.Preset_Default;
                    case "Complete":
                        return CoreModules.Preset_Complete;
                    default:
                        Log.Error("Incorrect SandboxLevel in config. Defaulting to 'Soft'.");
                        return CoreModules.Preset_SoftSandbox;
                }
            
            }
        }

        public static string SystemAccessLevel => SCriPt.Instance.Config.SystemAccessLevel;
        
        public static void AutoLoad()
        {
            Script.DefaultOptions.DebugPrint = s => Log.Send("[Lua] " + s, Discord.LogLevel.Debug, ConsoleColor.Green);;
            Script.DefaultOptions.ScriptLoader = new FileSystemScriptLoader();
            switch (SystemAccessLevel)
            {
                case "Standard":
                    Script.GlobalOptions.Platform = new StandardPlatformAccessor();
                    break;
                case "Limited":
                    Script.GlobalOptions.Platform = new LimitedPlatformAccessor();
                    break;
                default:
                    Script.GlobalOptions.Platform = new LimitedPlatformAccessor();
                    Log.Error("Incorrect SystemAccessLevel in config. Defaulting to 'Limited'.");
                    break;
            }
            //UserData.RegistrationPolicy = InteropRegistrationPolicy.Automatic;
            RegisterTypes();
            
            if(!Directory.Exists(ScriptPath+"Scripts"))
            {
                Directory.CreateDirectory(ScriptPath+"Scripts");
            }
            if(!Directory.Exists(ScriptPath+"Scripts/AutoLoad"))
            {
                Directory.CreateDirectory(ScriptPath+"Scripts/AutoLoad");
            }
            if(!Directory.Exists(ScriptPath+"Scripts/Globals"))
            {
                Directory.CreateDirectory(ScriptPath+"Scripts/Globals");
            }

            AutoLoadScript = new Script(SandboxLevel);
            RegisterAPI(AutoLoadScript);
            foreach(string file in Directory.GetFiles(ScriptPath+"Scripts/AutoLoad"))
            {
                if(file.EndsWith(".lua"))
                {
                    try
                    {
                        //Script script = new Script();
                        AutoLoadScript.DoFile(file);
                        //SCriPt.Instance.LoadedScripts.Add(script);
                        Log.Info("Loaded script: " + file);
                    }
                    catch (ScriptRuntimeException e)
                    {
                        Log.Error(e.DecoratedMessage);
                    }
                }
            }
            
            ExecuteLoad(AutoLoadScript);
            
            SCriPt.Instance.LoadedScripts.Add("AutoLoad",AutoLoadScript);
        }

        public static string[] GetScriptsDir()
        {
            return Directory.GetFiles(ScriptPath+"Scripts");
        }

        private static void ExecuteLoad(Script script)
        {
            // Get the global table
            Table globals = script.Globals;

            // Iterate 
            foreach (var pair in globals.Pairs)
            {
                // Check if the value is a table
                if (pair.Value.Type == DataType.Table)
                {
                    Table table = pair.Value.Table;
                    
                    if(table.Get("loaded").Type == DataType.Boolean)
                    {
                        if(table.Get("loaded").Boolean)
                        {
                            Log.Info("Skipping loaded table: "+pair.Key);
                            continue;
                        }
                    }
                    
                    // Check for the "load" function
                    if (table.Get("load").Type == DataType.Function)
                    {
                        DynValue loadFunction = table.Get("load");
                        try
                        {
                            // Execute the load function
                            script.Call(loadFunction); 
                            Log.Info("Loaded table: "+pair.Key);
                            table.Set("loaded", DynValue.True);
                        }
                        catch (ScriptRuntimeException ex)
                        {
                            // Handle potential exceptions during load function execution
                            //Console.WriteLine($"Error executing load function in table '{pair.Key}': {ex.Message}");
                            Log.Error(ex.DecoratedMessage);
                        }
                    }
                }
            }
        }
        
        public static void ExecuteUnload(Script script)
        {
            // Get the global table
            Table globals = script.Globals;

            // Iterate 
            foreach (var pair in globals.Pairs)
            {
                // Check if the value is a table
                if (pair.Value.Type == DataType.Table)
                {
                    Table table = pair.Value.Table;
                    
                    if(table.Get("loaded").Type == DataType.Boolean)
                    {
                        if(!table.Get("loaded").Boolean)
                        {
                            Log.Info("Skipping unloaded script: "+pair.Key);
                            continue;
                        }
                    }
                    
                    // Check for the "load" function
                    if (table.Get("unload").Type == DataType.Function)
                    {
                        DynValue unloadFunction = table.Get("unload");
                        try
                        {
                            // Execute the load function
                            script.Call(unloadFunction); 
                            Log.Info("Unloaded table: "+pair.Key);
                            table.Set("loaded", DynValue.False);
                        }
                        catch (ScriptRuntimeException ex)
                        {
                            // Handle potential exceptions during load function execution
                            //Console.WriteLine($"Error executing load function in table '{pair.Key}': {ex.Message}");
                            Log.Error(ex.DecoratedMessage);
                        }
                    }
                }
            }
        }

        public static void CustomAPIs(Script script)
        {
            foreach(string file in Directory.GetFiles("Scripts/Globals"))
            {
                if(file.EndsWith(".lua"))
                {
                    script.DoFile(file);
                    Log.Info("Loaded Custom Global: "+file);
                }
            }
        }
        
        public static bool LoadFile(string fileName)
        {
            foreach(string file in Directory.GetFiles(ScriptPath+"Scripts"))
            {
                Log.Debug("Checking file: "+file);
                if(file.EndsWith(".lua"))
                {
                    if(file.ToLower().Contains(fileName.ToLower()))
                    {
                        try
                        {
                            Script script = new Script(SandboxLevel);
                            RegisterAPI(script);
                            //Script script = new Script();
                            script.DoFile(file);
                            //SCriPt.Instance.LoadedScripts.Add(script);
                            ExecuteLoad(script);
                            SCriPt.Instance.LoadedScripts.Add(file,script);
                            return true;
                        }
                        catch (ScriptRuntimeException e)
                        {
                            Log.Error(e.DecoratedMessage);
                        }
                    }
                }
            }

            return false;
        }

        
        private static Dictionary<string, DynValue> Globals = new Dictionary<string, DynValue>();

        public static void RegisterAPI(Script script)
        {
            AddGlobal<LuaAdminToys>("AdminToys");
            AddGlobal<LuaAPI>("API");
            AddGlobal<LuaWarhead>("Warhead");
            AddGlobal<LuaDecon>("Decon");
            AddGlobal<LuaLobby>("Lobby");
            AddGlobal<LuaRound>("Round");
            AddGlobal<LuaFacility>("Facility");
            AddGlobal<LuaCassie>("Cassie");
            AddGlobal<LuaServer>("Server");
            AddGlobal<LuaEvents>("Events");
            AddGlobal<LuaPlayer>("Player");
            AddGlobal<LuaCoroutines>("Timing");
            AddGlobal<LuaRole>("Role");
            AddGlobal<LuaSCriPt>("SCriPt");
            script.Globals["RegisterType"] = (Action<string,string>) RegisterType;
            
            foreach(var global in Globals)
            {
                script.Globals[global.Key] = global.Value;
            }
            CustomAPIs(script); 
            
            SCriPt.Instance.RegisterAllGlobals(script);
        }
        
        public static void AddGlobal<T>(string globalName)
        {
            if (Attribute.GetCustomAttribute(typeof(T), typeof(MoonSharpUserDataAttribute)) == null)
                UserData.RegisterType<T>();
            if(Globals.ContainsKey(globalName)) return;
            UserData.CreateStatic(typeof(T));
            Globals[globalName] = UserData.CreateStatic(typeof(T));
        }

        private static void RegisterTypes()
        {
            //UserData.RegisterType<Player>();
            UserData.RegisterProxyType<ProxyPlayer, Player>(p => new ProxyPlayer(p));
            UserData.RegisterProxyType<ProxyPlayer, PluginAPI.Core.Player>(p => new ProxyPlayer(Exiled.API.Features.Player.Get(p.ReferenceHub)));
            UserData.RegisterProxyType<ProxyPickup, Pickup>(p => new ProxyPickup(p));
            UserData.RegisterProxyType<ProxyRoom, Room>(r => new ProxyRoom(r));
            UserData.RegisterProxyType<ProxyTeslaGate, Exiled.API.Features.TeslaGate>(t => new ProxyTeslaGate(t));
            UserData.RegisterProxyType<ProxyItem, Item>(i => new ProxyItem(i));
            UserData.RegisterProxyType<ProxyDoor, Door>(d => new ProxyDoor(d));
            UserData.RegisterProxyType<ProxyNpc, Npc>(n => new ProxyNpc(n));
            //UserData.RegisterType<Npc>();
            UserData.RegisterType<RoleTypeId>();
            UserData.RegisterType<DoorLockType>();
            UserData.RegisterType<DoorType>();
            UserData.RegisterType<Role>();
            UserData.RegisterType<Vector3>();
            UserData.RegisterType<RoleTypeId>();
            UserData.RegisterType<ItemType>();
            UserData.RegisterType<Quaternion>();
            UserData.RegisterType<Transform>();
            UserData.RegisterType<ReferenceHub>();
            UserData.RegisterType<CharacterClassManager>();
            UserData.RegisterType<Inventory>();
            UserData.RegisterType<Team>();
            UserData.RegisterType<Side>();
            UserData.RegisterType<IExiledEvent>();
            UserData.RegisterType<IDeniableEvent>();
            UserData.RegisterType<IPlayerEvent>();
            UserData.RegisterType<EventArgs>();
            UserData.RegisterType<ItemPickupBase>();
            UserData.RegisterType<PickupSyncInfo>();
            UserData.RegisterType<CoroutineHandle>();
            UserData.RegisterType<EventHandler>();
            UserData.RegisterType<IExiledEvent>();
            UserData.RegisterAssembly();
            
            SCriPt.Instance.RegisterAllTypes();
        }
        
        public static void RegisterType(string type, string assembly = "Exiled.API")
        {
            //UserData.RegisterType(Type.GetType($"{type},Exiled.API"));//NRE
            Type classType = Type.GetType($"{type}, {assembly}");
            if (classType != null)
            {
                // Successfully retrieved the Type object for Ammo class
                // You can now use 'ammoType' to create instances, invoke methods, etc.
                //Log.Info("Type: "+classType.ToString());
                try
                {
                    if (UserData.IsTypeRegistered(classType))
                    {
                        Log.Error(classType + " is already registered.");
                        return;
                    }
                    UserData.RegisterType(classType);
                    Log.Info("Registered: "+classType.ToString());
                } catch (Exception e)
                {
                    Log.Error(e);
                }
            }
            else
            {
                // Failed to retrieve the Type
                // Check the assembly-qualified name and assembly availability
                Log.Info("Type: null");
            }
        }
        
    }
}