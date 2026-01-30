using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using SCriPt.LabAPI.API.Lua;
using SCriPt.LabAPI.API.Lua.Globals;
using SCriPt.LabAPI.API.Lua.Objects;
using SCriPt.LabAPI.API.Lua.Proxies;
using SCriPt.LabAPI.Utils;
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

        // Generate Lua API documentation
        LuaDocGenerator.Generate();
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
        UserData.RegisterProxyType<ProxyAdminToy, AdminToy>(p => new ProxyAdminToy(p));
        UserData.RegisterProxyType<ProxyCapybaraToy, CapybaraToy>(p => new ProxyCapybaraToy(p));
    }

    private static void RegisterSafe<T>()
    {
        RegisterSafe(typeof(T));
    }

    private static void RegisterSafe(Type type)
    {
        if (UserData.IsTypeRegistered(type)) return;
        UserData.RegisterType(type, new SafeUserDataDescriptor(type, InteropAccessMode.LazyOptimized));
    }

    private static void RegisterLabApiWrappers()
    {
        var wrapperAssembly = typeof(Player).Assembly;
        var wrapperTypes = wrapperAssembly.GetTypes()
            .Where(t => t.Namespace == "LabApi.Features.Wrappers" && t.IsPublic && !t.IsEnum && !t.IsInterface);

        foreach (var type in wrapperTypes)
        {
            try
            {
                RegisterSafe(type);
                if(HasStaticMembers(type))
                    AddStaticGlobal(type.Name, type);
            }
            catch (Exception e)
            {
                Logger.Warn($"[ScriptLoader] Failed to register wrapper type '{type.Name}': {e.Message}");
            }
        }
    }

    private static bool HasStaticMembers(Type type)
    {
        string[] ignoredStaticNames = new[] { "get_Dictionary", "get_List", "Get", "TryGet", "Dictionary", "List" };
        return type.GetMembers(BindingFlags.Public | BindingFlags.Static)
            .Any(info => !ignoredStaticNames.Contains(info.Name));
    }

    private static void RegisterLabApiEnums()
    {
        var labApiAssembly = typeof(Player).Assembly;
        var enumTypes = labApiAssembly.GetTypes()
            .Where(t => t.IsPublic && t.IsEnum);

        foreach (var type in enumTypes)
        {
            try
            {
                AddStaticGlobal(type.Name, type);
            }
            catch (Exception e)
            {
                Logger.Warn($"[ScriptLoader] Failed to register enum '{type.Name}': {e.Message}");
            }
        }
    }

    private static void RegisterBaseGameEnums()
    {
        var baseGameAssembly = typeof(ReferenceHub).Assembly;
        var enumTypes = baseGameAssembly.GetTypes()
            .Where(t => t.IsPublic && t.IsEnum);

        foreach (var type in enumTypes)
        {
            try
            {
                AddStaticGlobal(type.Name, type);
            }
            catch (Exception e)
            {
                Logger.Warn($"[ScriptLoader] Failed to register enum '{type.Name}': {e.Message}");
            }
        }
    }
    
    public static void AddGlobalsToScript(ScriptHandler script)
    {
        foreach (var global in Globals)
        {
            ((Script)script).Globals[global.Key] = global.Value;
        }
    }
    
//ENUMS https://github.com/moonsharp-devs/moonsharp/blob/master/src/MoonSharp.Interpreter.Tests/EndToEnd/UserDataEnumsTest.cs
    private static void RegisterTypes()
    {
        RegisterLabApiWrappers();
        UserData.RegisterType<CommandSender>();
        
        
        
        
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

        UserData.RegisterAssembly();
        
        Logger.Info("Completed loading Types...");
        
    }

    public static void SetupStaticGlobals()
    {
        AddStaticGlobal<GlobalAdminToys>("AdminToys");
        AddStaticGlobal<GlobalCassie>("CASSIE");
        AddStaticGlobal<GlobalCassie>("Cassie");
        AddStaticGlobal<GlobalCassie>("Announcer");
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
        AddStaticGlobal<GlobalPlayers>("Players");

        // Auto-register all LabAPI enums
        RegisterLabApiEnums();
        RegisterBaseGameEnums();

        // Game/Unity enums (not in LabAPI assembly)
        AddStaticGlobal<RoleTypeId>("RoleTypeId");
        AddStaticGlobal<RoleTypeId>("RoleType");
        AddStaticGlobal<ItemType>("ItemType");
        AddStaticGlobal<Team>("Team");
        AddStaticGlobal<Faction>("Faction");
        AddStaticGlobal<FacilityZone>("FacilityZone");
        AddStaticGlobal<StatusEffectBase.EffectClassification>("EffectClassification");
        AddStaticGlobal<KeyCode>("KeyCode");
        AddStaticGlobal<TMP_InputField.ContentType>("ContentType");
        AddStaticGlobal<PrimitiveFlags>("PrimitiveFlags");
        AddStaticGlobal<RoleChangeReason>("RoleChangeReason");
        
        AddStaticGlobal<Player>("Player");
        AddStaticGlobal<Door>("Door");
        AddStaticGlobal<Pickup>("Pickup");

        
        Globals["PlayerEvents"] = UserData.CreateStatic(typeof(PlayerEvents));
    }
    
    
    public static void AddStaticGlobal<T>(string globalName)
    {
        if (!UserData.IsTypeRegistered<T>())
            UserData.RegisterType<T>();
        if(Globals.ContainsKey(globalName)) return;
        //UserData.CreateStatic(typeof(T));
        Globals[globalName] = UserData.CreateStatic(typeof(T));
    }
    
    public static void AddStaticGlobal(string globalName, Type type)
    {
        if (!UserData.IsTypeRegistered(type))
            UserData.RegisterType(type);
        if(Globals.ContainsKey(globalName)) return;
        Globals[globalName] = UserData.CreateStatic(type);
    }
    
    // lua var items [ItemType.Jailbird, ItemType.Scp018]
    // lua var welcome "Welcome to the server!"
    // lua exec Players.All.Give(items);
    // lua exec Players.All.SendMessage(welcome);
    // lua exec Players.By(Team.SCP).Give(items);
}