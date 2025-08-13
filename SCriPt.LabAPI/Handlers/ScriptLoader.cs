using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AdminToys;
using CustomPlayerEffects;
using GameObjectPools;
using InventorySystem;
using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Events.Handlers;
using LabApi.Features.Enums;
using LabApi.Features.Wrappers;
using MapGeneration;
using MEC;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter.Platforms;
using PlayerRoles;
using PlayerRoles.PlayableScps.HumeShield;
using SCriPt.LabAPI.API.Lua.Globals;
using SCriPt.LabAPI.API.Lua.Objects;
using SCriPt.LabAPI.API.Lua.Proxies;
using TMPro;
using UnityEngine;
using CapybaraToy = LabApi.Features.Wrappers.CapybaraToy;
using Logger = LabApi.Features.Console.Logger;


namespace SCriPt.LabAPI.Handlers;

public class ScriptLoader
{

    public static DirectoryInfo ScriptPathParent => LabApi.Loader.Features.Paths.PathManager.LabApi;

    public static CoreModules SandboxLevel => SCriPt.Instance.Config!.FullAccess ? CoreModules.Preset_Complete : CoreModules.Preset_SoftSandbox;
    
    public static Dictionary<string, DynValue> Globals { get; } = new Dictionary<string, DynValue>();

    public static void Initialize()
    {
        // Configure Default options for MoonSharp
        Script.DefaultOptions.DebugPrint = s => Logger.Debug("[Lua] " + s);
        Script.DefaultOptions.ScriptLoader = new FileSystemScriptLoader();
        Script.GlobalOptions.Platform = SCriPt.Instance.Config!.FullAccess
            ? new StandardPlatformAccessor()
            : new LimitedPlatformAccessor();
        
        // Register the core modules
        RegisterProxies();
        RegisterTypes();
        SetupStaticGlobals();
        
        
        // Create Script folders if not already exist
        ScriptPathParent.CreateSubdirectory("SCriPt");
        ScriptPathParent.CreateSubdirectory("SCriPt/Scripts");
        ScriptPathParent.CreateSubdirectory("SCriPt/Data");
        GlobalData.LoadFromDisk();
        
    }

    public static void CreateScriptByCommand(string command)
    {
        ScriptHandler script = ScriptHandler.CreateFromCommand(command, CoreModules.Preset_SoftSandbox);
    }

    public static void LoadScripts()
    {
        Logger.Info("Loading scripts...");
        // Load all scripts from the SCriPt/Scripts folder
        
        foreach (var file in ScriptPathParent.GetFiles("*.lua", SearchOption.AllDirectories))
        {
            try
            {
                Logger.Debug($"Loading script: {file.FullName}");
                ScriptHandler script = ScriptHandler.Create(file.FullName, SandboxLevel);
                SCriPt.Instance.Scripts[file.Name] = script;
            }
            catch (Exception e)
            {
                Logger.Error($"Error loading script {file.Name}: {e.Message}");
            }
        }
    }
    
    

    public static void UnloadAllScripts()
    {
        Logger.Info("Unloading all scripts...");
        foreach(var script in SCriPt.Instance.Scripts.Values)
        {
            try
            {
                script.ExecuteUnload();
            }
            catch (Exception e)
            {
                Logger.Error($"Error unloading script {script.Name}: {e.Message}");
            }
        }
        SCriPt.Instance.Scripts.Clear();
    }
    
    public static void UnloadScript(string scriptName)
    {
        if (SCriPt.Instance.Scripts.TryGetValue(scriptName, out ScriptHandler script))
        {
            try
            {
                script.ExecuteUnload();
                SCriPt.Instance.Scripts.Remove(scriptName);
                Logger.Info($"Unloaded script: {scriptName}");
            }
            catch (Exception e)
            {
                Logger.Error($"Error unloading script {scriptName}: {e.Message}");
            }
        }
        else
        {
            Logger.Warn($"Script {scriptName} not found.");
        }
    }

    private static void RegisterProxies()
    {
        //UserData.RegisterProxyType<ProxyPlayer, Player>(p => new ProxyPlayer(p));
        UserData.RegisterProxyType<ProxyAdminToy, AdminToy>(p => new ProxyAdminToy(p));
        UserData.RegisterProxyType<ProxyCapybaraToy, CapybaraToy>(p => new ProxyCapybaraToy(p));
    }
    
    public static void AddGlobalsToScript(ScriptHandler script)
    {
        foreach (var global in Globals)
        {
            ((Script)script).Globals[global.Key] = global.Value;
        }
    }
    

    private static void RegisterTypes()
    {
        UserData.RegisterType<Player>();
        UserData.RegisterType<Room>();
        
        
        UserData.RegisterType<RoleTypeId>();
        UserData.RegisterType<Component>();
        UserData.RegisterType<Behaviour>();
        UserData.RegisterType<MonoBehaviour>();
        UserData.RegisterType<PoolObject>();
        UserData.RegisterType<HumeShieldModuleBase>();
        UserData.RegisterType<GlobalSCriPt>();
        UserData.RegisterType<GlobalServer>();
        
        UserData.RegisterType<Vector3>();
        UserData.RegisterType<Quaternion>();
        UserData.RegisterType<Transform>();
        UserData.RegisterType<ReferenceHub>();
        UserData.RegisterType<CharacterClassManager>();
        UserData.RegisterType<Inventory>();
        UserData.RegisterType<IPlayerEvent>();
        UserData.RegisterType<EventArgs>();
        UserData.RegisterType<ItemPickupBase>();
        UserData.RegisterType<PickupSyncInfo>();
        UserData.RegisterType<CoroutineHandle>();
        UserData.RegisterType<EventHandler>();
        UserData.RegisterType<CommandSender>();
        UserData.RegisterType<CommandType>();
        UserData.RegisterType<FacilityZone>();
        UserData.RegisterType<DoorName>();
        UserData.RegisterType<StatusEffectBase>();
        UserData.RegisterType<PlayerRoleBase>();
        UserData.RegisterType<DateTime>();
        UserData.RegisterType<TimeSpan>();
        /*UserData.RegisterType<AdminToys.PrimitiveObjectToy>();
        UserData.RegisterType<LabApi.Features.Wrappers.PrimitiveObjectToy>();
        UserData.RegisterType<PrimitiveFlags>();
        UserData.RegisterType<AdminToys.SpeakerToy>();
        UserData.RegisterType<LabApi.Features.Wrappers.SpeakerToy>();
        UserData.RegisterType<AdminToys.LightSourceToy>();
        UserData.RegisterType<LabApi.Features.Wrappers.LightSourceToy>();
        UserData.RegisterType<AdminToys.ShootingTarget>();
        UserData.RegisterType<LabApi.Features.Wrappers.ShootingTargetToy>();
        UserData.RegisterType<AdminToys.TextToy>();
        UserData.RegisterType<LabApi.Features.Wrappers.TextToy>();*/
        
        UserData.RegisterAssembly();
        
    }

    public static void SetupStaticGlobals()
    {
        AddStaticGlobal<GlobalAdminToys>("AdminToys");
        AddStaticGlobal<GlobalDeadmanSwitch>("DeadmanSwitch");
        AddStaticGlobal<GlobalDeadmanSwitch>("DMS");
        AddStaticGlobal<GlobalEvents>("Events");
        AddStaticGlobal<GlobalDecon>("Decon");
        AddStaticGlobal<GlobalData>("Data");
        AddStaticGlobal<GlobalNew>("New");
        AddStaticGlobal<GlobalLobby>("Lobby");
        AddStaticGlobal<GlobalRound>("Round");
        AddStaticGlobal<GlobalServer>("Server");
        AddStaticGlobal<GlobalSettings>("Settings");

        //ENUMS https://github.com/moonsharp-devs/moonsharp/blob/master/src/MoonSharp.Interpreter.Tests/EndToEnd/UserDataEnumsTest.cs
        AddStaticGlobal<RoleTypeId>("RoleTypeId");
        AddStaticGlobal<RoleTypeId>("RoleType");
        AddStaticGlobal<ItemType>("ItemType");
        AddStaticGlobal<Team>("Team");
        AddStaticGlobal<FacilityZone>("FacilityZone");
        AddStaticGlobal<StatusEffectBase.EffectClassification>("EffectClassification");
        AddStaticGlobal<CommandType>("CommandType");
        AddStaticGlobal<KeyCode>("KeyCode");
        AddStaticGlobal<TMP_InputField.ContentType>("ContentType");

        
        Globals["PlayerEvents"] = UserData.CreateStatic(typeof(PlayerEvents));
    }
    
    
    private static void AddStaticGlobal<T>(string globalName)
    {
        if (Attribute.GetCustomAttribute(typeof(T), typeof(MoonSharpUserDataAttribute)) == null)
            UserData.RegisterType<T>();
        if(Globals.ContainsKey(globalName)) return;
        //UserData.CreateStatic(typeof(T));
        Globals[globalName] = UserData.CreateStatic(typeof(T));
    }
    
    // lua var items [ItemType.Jailbird, ItemType.Scp018]
    // lua var welcome "Welcome to the server!"
    // lua exec Players.All.Give(items);
    // lua exec Players.All.SendMessage(welcome);
    // lua exec Players.By(Team.SCP).Give(items);
}