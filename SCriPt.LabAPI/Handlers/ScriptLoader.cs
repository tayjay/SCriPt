using System;
using System.Collections.Generic;
using System.IO;
using CustomPlayerEffects;
using GameObjectPools;
using InventorySystem;
using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
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
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace SCriPt.LabAPI.Handlers;

public class ScriptLoader
{

    public static DirectoryInfo ScriptPathParent => LabApi.Loader.Features.Paths.PathManager.LabApi;
    
    public static CoreModules SandboxLevel => CoreModules.Preset_SoftSandbox;
    
    public static Dictionary<string, DynValue> Globals { get; } = new Dictionary<string, DynValue>();

    public static void Initialize()
    {
        // Configure Default options for MoonSharp
        Script.DefaultOptions.DebugPrint = s => Logger.Debug("[Lua] " + s);
        Script.DefaultOptions.ScriptLoader = new FileSystemScriptLoader();
        Script.GlobalOptions.Platform = new LimitedPlatformAccessor();
        
        // Register the core modules
        RegisterProxies();
        RegisterTypes();
        SetupStaticGlobals();
        
        // Create Script folders if not already exist
        ScriptPathParent.CreateSubdirectory("SCriPt");
        ScriptPathParent.CreateSubdirectory("SCriPt/Scripts");
        ScriptPathParent.CreateSubdirectory("SCriPt/Scripts/Global");
        
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
                var script = new Script(SandboxLevel);
                script.Options.ScriptLoader = new FileSystemScriptLoader();
                script.Globals["ScriptName"] = file.Name;
                script.DoFile(file.FullName);
            }
            catch (Exception e)
            {
                Logger.Error($"Error loading script {file.Name}: {e.Message}");
            }
        }
    }
    
    

    public static void UnloadAllScripts()
    {
        
    }

    private static void RegisterProxies()
    {
        
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
        
        UserData.RegisterAssembly();
    }

    public static void SetupStaticGlobals()
    {
        AddStaticGlobal<GlobalAdminToys>("AdminToys");
        AddStaticGlobal<GlobalDeadmanSwitch>("DeadmanSwitch");
        AddStaticGlobal<GlobalEvents>("Events");
        AddStaticGlobal<GlobalDecon>("Decon");

        //ENUMS https://github.com/moonsharp-devs/moonsharp/blob/master/src/MoonSharp.Interpreter.Tests/EndToEnd/UserDataEnumsTest.cs
        AddStaticGlobal<RoleTypeId>("RoleTypeId");
        AddStaticGlobal<RoleTypeId>("RoleType");
        AddStaticGlobal<ItemType>("ItemType");
        AddStaticGlobal<Team>("Team");
        AddStaticGlobal<FacilityZone>("FacilityZone");
        AddStaticGlobal<StatusEffectBase.EffectClassification>("EffectClassification");
        AddStaticGlobal<CommandType>("CommandType");
    }
    
    
    private static void AddStaticGlobal<T>(string globalName)
    {
        if (Attribute.GetCustomAttribute(typeof(T), typeof(MoonSharpUserDataAttribute)) == null)
            UserData.RegisterType<T>();
        if(Globals.ContainsKey(globalName)) return;
        UserData.CreateStatic(typeof(T));
        Globals[globalName] = UserData.CreateStatic(typeof(T));
    }
    
    // lua var items [ItemType.Jailbird, ItemType.Scp018]
    // lua var welcome "Welcome to the server!"
    // lua exec Players.All.Give(items);
    // lua exec Players.All.SendMessage(welcome);
    // lua exec Players.By(Team.SCP).Give(items);
}