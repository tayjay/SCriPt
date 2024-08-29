using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Interfaces;
using InventorySystem;
using InventorySystem.Items.Pickups;
using MEC;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter.Platforms;
using PlayerRoles;
using SCriPt.API.Lua.Globals;
using SCriPt.API.Lua.Objects;
using SCriPt.API.Lua.Proxy;
using UnityEngine;

namespace SCriPt.API.Lua;

public class NewScriptLoader
{
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
    
    public static Dictionary<string, DynValue> Globals = new Dictionary<string, DynValue>();

    public static void Initialize()
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
        RegisterProxies();
        RegisterTypes();
        SetupStaticGlobals();
        
        if(!Directory.Exists(ScriptPath))
        {
            Directory.CreateDirectory(ScriptPath);
        }
        if(!Directory.Exists(ScriptPath+"Data"))
        {
            Directory.CreateDirectory(ScriptPath+"Data");
        }
        if(!Directory.Exists(ScriptPath+"Globals"))
        {
            Directory.CreateDirectory(ScriptPath+"Globals");
        }
        
    }

    public static void LoadScripts()
    {
        Log.Info("Loading scripts...");
        
        foreach (string file in Directory.GetFiles(ScriptPath, "*.pastebin", SearchOption.AllDirectories))
        {
            if(file.Contains("/Data/")) continue;
            if(file.Contains("/Globals/")) continue;
            if (file.EndsWith(".pastebin"))
            {
                try
                {
                    if (File.Exists(file.Replace(".pastebin", ".lua")))
                    {
                        Log.Debug("Skipping web script: "+file+" already downloaded.");
                        continue;
                    }
                    string name = file.Replace('\\', '/');
                    name = name.Substring(name.LastIndexOf('/') + 1);
                    name = name.Substring(0, name.Length - 9);
                    
                    Log.Debug("Downloading web script: "+name);
                    string script = WebLoader.GetFromPastebin(name);
                    if (script == null)
                    {
                        Log.Error("Failed to download web script from pastebin: " + name);
                        continue;
                    }
                    File.WriteAllText(file.Replace(".pastebin",".lua"), script);
                } catch (Exception e)
                {
                    Log.Error("Failed to download web script: " + file);
                    Log.Error(e.Message);
                }
                
            }
        }
        
        foreach (string file in Directory.GetFiles(ScriptPath,"*.lua", SearchOption.AllDirectories))
        {
            if(file.Contains("/Data/")) continue;
            if(file.Contains("/Globals/")) continue;
            
            if (file.EndsWith(".lua"))
            {
                try
                {
                    ScriptWrapper wrapper = ScriptWrapper.FromFile(file, SandboxLevel);
                    SCriPt.Instance.LoadedWrappers.Add(wrapper.Name, wrapper);
                }
                catch (Exception e)
                {
                    Log.Error("Failed to load script: " + file);
                    Log.Error(e.Message);
                }
            }
        }

        
        Log.Info("Loaded scripts.");
        
    }
    
    public static void UnloadAllScripts()
    {
        foreach (var wrapper in SCriPt.Instance.LoadedWrappers)
        {
            wrapper.Value.ExecuteUnload();
        }
        SCriPt.Instance.LoadedWrappers.Clear();
    }

    private static void RegisterProxies()
    {
        UserData.RegisterProxyType<ProxyPlayer, Player>(p => new ProxyPlayer(p));
        UserData.RegisterProxyType<ProxyPlayer, PluginAPI.Core.Player>(p => new ProxyPlayer(Exiled.API.Features.Player.Get(p.ReferenceHub)));
        UserData.RegisterProxyType<ProxyPickup, Pickup>(p => new ProxyPickup(p));
        UserData.RegisterProxyType<ProxyRoom, Room>(r => new ProxyRoom(r));
        UserData.RegisterProxyType<ProxyTeslaGate, Exiled.API.Features.TeslaGate>(t => new ProxyTeslaGate(t));
        UserData.RegisterProxyType<ProxyItem, Item>(i => new ProxyItem(i));
        UserData.RegisterProxyType<ProxyDoor, Door>(d => new ProxyDoor(d));
        UserData.RegisterProxyType<ProxyNpc, Npc>(n => new ProxyNpc(n));
    }
    
    private static void RegisterTypes()
    {
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
        UserData.RegisterType<ICommandSender>();
        
        UserData.RegisterAssembly();

        SCriPt.Instance.RegisterAllTypes();
        
        //ENUMS https://github.com/moonsharp-devs/moonsharp/blob/master/src/MoonSharp.Interpreter.Tests/EndToEnd/UserDataEnumsTest.cs
        UserData.RegisterType<DoorLockType>();
        UserData.RegisterType<DoorType>();
        UserData.RegisterType<RoleTypeId>();
        UserData.RegisterType<ItemType>();
        UserData.RegisterType<Team>();
        UserData.RegisterType<Side>();
        UserData.RegisterType<EffectType>();
        UserData.RegisterType<DamageType>();
        UserData.RegisterType<PrimitiveType>();
        UserData.RegisterType<Enum>();
        UserData.RegisterType<AmmoType>();
        UserData.RegisterType<DangerType>();
        UserData.RegisterType<DanceType>();
        UserData.RegisterType<DecontaminationState>();
        UserData.RegisterType<EffectCategory>();
        UserData.RegisterType<GeneratorState>();
        UserData.RegisterType<KeycardPermissions>();
        
    }

    private static void SetupStaticGlobals()
    {
        AddStaticGlobal<LuaAdminToys>("AdminToys");
        AddStaticGlobal<LuaAPI>("API");
        AddStaticGlobal<LuaWarhead>("Warhead");
        AddStaticGlobal<LuaDecon>("Decon");
        AddStaticGlobal<LuaLobby>("Lobby");
        AddStaticGlobal<LuaRound>("Round");
        AddStaticGlobal<LuaFacility>("Facility");
        AddStaticGlobal<LuaCassie>("Cassie");
        AddStaticGlobal<LuaServer>("Server");
        AddStaticGlobal<LuaEvents>("Events");
        AddStaticGlobal<LuaPlayer>("Player");
        //AddStaticGlobal<LuaCoroutines>("Timing");
        AddStaticGlobal<LuaRole>("Role");
        AddStaticGlobal<LuaEnums>("Enum");
        AddStaticGlobal<LuaNew>("New");
        //AddStaticGlobal<LuaSCriPt>("SCriPt");
        AddStaticGlobal<LuaConfig>("Config");
        if(SCriPt.Instance.Config.EnableStorage)
            AddStaticGlobal<LuaStore>("Store");
        
        //ENUMS https://github.com/moonsharp-devs/moonsharp/blob/master/src/MoonSharp.Interpreter.Tests/EndToEnd/UserDataEnumsTest.cs
        AddStaticGlobal<RoleTypeId>("RoleTypeId");
        AddStaticGlobal<DoorLockType>("DoorLockType");
        AddStaticGlobal<DoorType>("DoorType");
        AddStaticGlobal<ItemType>("ItemType");
        AddStaticGlobal<Team>("Team");
        AddStaticGlobal<Side>("Side");
        AddStaticGlobal<EffectType>("EffectType");
        AddStaticGlobal<PrimitiveType>("PrimitiveType");
        AddStaticGlobal<DamageType>("DamageType");
        
    }
    
    private static void AddStaticGlobal<T>(string globalName)
    {
        if (Attribute.GetCustomAttribute(typeof(T), typeof(MoonSharpUserDataAttribute)) == null)
            UserData.RegisterType<T>();
        if(Globals.ContainsKey(globalName)) return;
        UserData.CreateStatic(typeof(T));
        Globals[globalName] = UserData.CreateStatic(typeof(T));
    }
    
    public static void AddGlobalsToScript(ScriptWrapper script)
    {
        foreach (var global in Globals)
        {
            ((Script)script).Globals[global.Key] = global.Value;
        }
        
    }

    public static void AddCustomGlobalsToScript(ScriptWrapper script)
    {
        foreach(string file in Directory.GetFiles(ScriptPath+"Globals"))
        {
            if(file.EndsWith(".lua"))
            {
                ((Script)script).DoFile(file);
                Log.Info("Loaded Custom Global: "+file);
            }
        }
    }
}