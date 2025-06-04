using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Core;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Interfaces;
using GameObjectPools;
using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using InventorySystem.Items.Pickups;
using LabApi.Features.Enums;
using MEC;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter.Platforms;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp049;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using PlayerRoles.PlayableScps.Scp079.Pinging;
using PlayerRoles.PlayableScps.Scp079.Rewards;
using PlayerRoles.PlayableScps.Scp096;
using PlayerRoles.PlayableScps.Scp106;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles.PlayableScps.Scp3114;
using PlayerRoles.PlayableScps.Scp939;
using PlayerRoles.PlayableScps.Scp939.Mimicry;
using PlayerRoles.PlayableScps.Scp939.Ripples;
using SCriPt.API.Lua.Globals;
using SCriPt.API.Lua.Objects;
using SCriPt.API.Lua.Proxy;
using SCriPt.API.Lua.Proxy.Roles;
using UnityEngine;
using HumanRole = Exiled.API.Features.Roles.HumanRole;
using KeycardPermissions = Exiled.API.Enums.KeycardPermissions;
using Scp049Role = Exiled.API.Features.Roles.Scp049Role;
using Scp079Role = Exiled.API.Features.Roles.Scp079Role;
using Scp096Role = Exiled.API.Features.Roles.Scp096Role;
using Scp106Role = Exiled.API.Features.Roles.Scp106Role;
using Scp173Role = Exiled.API.Features.Roles.Scp173Role;
using Scp3114Role = Exiled.API.Features.Roles.Scp3114Role;
using Scp939Role = Exiled.API.Features.Roles.Scp939Role;

namespace SCriPt.API.Lua;

public class NewScriptLoader
{
    public static string ScriptPath => Paths.Exiled + "/SCriPt/";//SCriPt.Instance.Config.ScriptPath;

    public static CoreModules SandboxLevel
    {
        get
        {
            /*switch (SCriPt.Instance.Config.SandboxLevel)
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
            }*/
            if (SCriPt.Instance.Config.FullAccess)
                return CoreModules.Preset_Default;
            else
                return CoreModules.Preset_SoftSandbox;

        }
    }

    //public static string SystemAccessLevel => SCriPt.Instance.Config.SystemAccessLevel;
    
    public static Dictionary<string, DynValue> Globals = new Dictionary<string, DynValue>();

    public static void Initialize()
    {
        Script.DefaultOptions.DebugPrint = s => Log.Send("[Lua] " + s, Discord.LogLevel.Debug, ConsoleColor.Green);;
        Script.DefaultOptions.ScriptLoader = new FileSystemScriptLoader();
        /*switch (SystemAccessLevel)
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
        }*/
        if (SCriPt.Instance.Config.FullAccess)
            Script.GlobalOptions.Platform = new StandardPlatformAccessor();
        else
            Script.GlobalOptions.Platform = new LimitedPlatformAccessor();
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


        if (SCriPt.Instance.Config.AllowPastebin)
        {
            foreach (string file in Directory.GetFiles(ScriptPath, "*.pastebin", SearchOption.AllDirectories))
            {
                if(file.Contains("/Data/")) continue;
                //if(file.Contains("/Globals/")) continue;
                if (file.EndsWith(".pastebin"))
                {
                    try
                    {
                        if (File.Exists(file.Replace(".pastebin", ".lua")))
                        {
                            Log.Debug("Skipping web script: "+file+" already downloaded.");
                            continue;
                        }

                        if (SCriPt.Instance.Config.FullAccess)
                        {
                            Log.Error("Pastebin is disabled in this environment. Please disable 'full_access' config.");
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
        }
        else
        {
            Log.Info("Pastebin is disabled. Skipping...");
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
        UserData.RegisterProxyType<ProxyPlayer, LabApi.Features.Wrappers.Player>(p => new ProxyPlayer(Exiled.API.Features.Player.Get(p.ReferenceHub)));
        UserData.RegisterProxyType<ProxyPickup, Pickup>(p => new ProxyPickup(p));
        UserData.RegisterProxyType<ProxyRoom, Room>(r => new ProxyRoom(r));
        UserData.RegisterProxyType<ProxyTeslaGate, Exiled.API.Features.TeslaGate>(t => new ProxyTeslaGate(t));
        UserData.RegisterProxyType<ProxyItem, Item>(i => new ProxyItem(i));
        UserData.RegisterProxyType<ProxyDoor, Door>(d => new ProxyDoor(d));
        UserData.RegisterProxyType<ProxyNpc, Npc>(n => new ProxyNpc(n));
        UserData.RegisterProxyType<ProxyRole, Role>(r => new ProxyRole(r));
        UserData.RegisterProxyType<ProxyFpcRole, FpcRole>(r => new ProxyFpcRole(r));
        UserData.RegisterProxyType<ProxyHumanRole, HumanRole>(r => new ProxyHumanRole(r));
        UserData.RegisterProxyType<ProxyScp049Role, Scp049Role>(r => new ProxyScp049Role(r));
        UserData.RegisterProxyType<ProxyScp0492Role, Scp0492Role>(r => new ProxyScp0492Role(r));
        UserData.RegisterProxyType<ProxyScp079Role, Scp079Role>(r => new ProxyScp079Role(r));
        UserData.RegisterProxyType<ProxyScp096Role, Scp096Role>(r => new ProxyScp096Role(r));
        UserData.RegisterProxyType<ProxyScp106Role, Scp106Role>(r => new ProxyScp106Role(r));
        UserData.RegisterProxyType<ProxyScp173Role, Scp173Role>(r => new ProxyScp173Role(r));
        UserData.RegisterProxyType<ProxyScp3114Role, Scp3114Role>(r => new ProxyScp3114Role(r));
        UserData.RegisterProxyType<ProxyScp939Role, Scp939Role>(r => new ProxyScp939Role(r));
    }
    
    private static void RegisterTypes()
    {
        UserData.RegisterType<RoleTypeId>();
        UserData.RegisterType<DoorLockType>();
        UserData.RegisterType<DoorType>();
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
        UserData.RegisterType<PlayerMovementState>();
        UserData.RegisterType<Scp079HudTranslation>();
        UserData.RegisterType<DoorAction>();
        UserData.RegisterType<PingType>();
        UserData.RegisterType<Scp173AudioPlayer.Scp173SoundId>();
        UserData.RegisterType<UsableRippleType>();
        UserData.RegisterType<Scp939LungeState>();
        UserData.RegisterType<Scp3114Identity.DisguiseStatus>();
        UserData.RegisterType<Scp3114VoiceLines.VoiceLinesName>();
        UserData.RegisterType<EmotionPresetType>();
        
        
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
        //AddStaticGlobal<LuaEnums>("Enum");
        AddStaticGlobal<LuaNew>("New");
        //AddStaticGlobal<LuaSCriPt>("SCriPt");
        AddStaticGlobal<LuaConfig>("Config");
        if(SCriPt.Instance.Config.EnableStorage)
            AddStaticGlobal<LuaStore>("Store");
        
        //ENUMS https://github.com/moonsharp-devs/moonsharp/blob/master/src/MoonSharp.Interpreter.Tests/EndToEnd/UserDataEnumsTest.cs
        AddStaticGlobal<RoleTypeId>("RoleType");
        AddStaticGlobal<DoorLockType>("DoorLockType");
        AddStaticGlobal<DoorType>("DoorType");
        AddStaticGlobal<ItemType>("ItemType");
        AddStaticGlobal<Team>("Team");
        AddStaticGlobal<Side>("Side");
        AddStaticGlobal<EffectType>("EffectType");
        AddStaticGlobal<PrimitiveType>("PrimitiveType");
        AddStaticGlobal<DamageType>("DamageType");
        AddStaticGlobal<AmmoType>("AmmoType");
        AddStaticGlobal<DangerType>("DangerType");
        AddStaticGlobal<DanceType>("DanceType");
        //AddStaticGlobal<DecontaminationState>("DecontaminationState");
        AddStaticGlobal<EffectCategory>("EffectCategory");
        AddStaticGlobal<GeneratorState>("GeneratorState");
        AddStaticGlobal<PrimitiveType>("KeycardPermissions");
        AddStaticGlobal<PlayerMovementState>("PlayerMovementState");
        AddStaticGlobal<Scp079HudTranslation>("Scp079HudTranslation");
        AddStaticGlobal<DoorAction>("DoorAction");
        AddStaticGlobal<PingType>("PingType");
        AddStaticGlobal<Scp173AudioPlayer.Scp173SoundId>("Scp173SoundId");
        AddStaticGlobal<UsableRippleType>("UsableRippleType");
        AddStaticGlobal<Scp939LungeState>("Scp939LungeState");
        AddStaticGlobal<Scp3114Identity.DisguiseStatus>("DisguiseStatus");
        //AddStaticGlobal<Scp3114VoiceLines.VoiceLinesName>("VoiceLinesName");
        AddStaticGlobal<CommandType>("CommandType");
        
        AddStaticGlobal<LuaDeadmanSwitch>("DeadmanSwitch");
        
        
        
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