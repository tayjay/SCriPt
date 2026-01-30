using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using SCriPt.LabAPI.API.Lua.Globals;
using SCriPt.LabAPI.API.Lua.Objects;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI
{
    public class SCriPt : Plugin<Config>
    {
        public static SCriPt Instance { get; private set; }
        

        /// <summary>
        /// Dynamic event handlers keyed by category name (e.g. "Player", "Server", "Warhead", "Scp049", etc.)
        /// </summary>
        public Dictionary<string, DynamicLuaEventHandler> DynamicEventHandlers { get; } = new Dictionary<string, DynamicLuaEventHandler>();

        public Dictionary<string,ScriptHandler> Scripts;
        
        public override void Enable()
        {
            if (!CheckForMoonSharp())
            {
                return;
            }
            if(Config!.FullAccess)
                Logger.Error("You have enabled FullAccess, this is not recommended and can cause security issues. If you do not know what this means, please disable it in the config.");
            
            Instance = this;
            RegisterEvents();
            
            Scripts = new Dictionary<string, ScriptHandler>();
            ScriptLoader.Initialize();
        }

        

        public override void Disable()
        {
            if (!CheckForMoonSharp())
            {
                return;
            }
            UnregisterEvents();
            
        }
        
        private void RegisterEvents()
        {
            // Register custom descriptors for the dynamic event system
            UserData.RegisterType(typeof(DynamicLuaEventHandler), new DynamicLuaEventDescriptor());
            UserData.RegisterType(typeof(GlobalEvents), new GlobalEventsDescriptor());
            UserData.RegisterType<LuaEvent>();

            GlobalSettings.RegisterTypes();

            // Auto-discover all LabAPI event handler classes and register them dynamically
            RegisterDynamicEvents();

            Logger.Info("Has PlayerSpawnEvent been registered? " + UserData.IsTypeRegistered<PlayerSpawnedEventArgs>());
        }

        private void RegisterDynamicEvents()
        {
            var labApiAssembly = typeof(Player).Assembly;
            var handlerTypes = labApiAssembly.GetTypes()
                .Where(t => t.Namespace == "LabApi.Events.Handlers"
                            && t.IsClass && t.IsAbstract && t.IsSealed // static classes
                            && t.Name.EndsWith("Events"));

            foreach (var handlerType in handlerTypes)
            {
                try
                {
                    // Derive category name: "PlayerEvents" -> "Player", "WarheadEvents" -> "Warhead"
                    var categoryName = handlerType.Name;
                    if (categoryName.EndsWith("Events"))
                        categoryName = categoryName.Substring(0, categoryName.Length - "Events".Length);

                    var handler = new DynamicLuaEventHandler(handlerType);
                    handler.RegisterEventTypes();
                    handler.RegisterEvents();
                    DynamicEventHandlers[categoryName] = handler;

                    Logger.Debug($"[DynamicEvents] Registered {handlerType.Name} as '{categoryName}' with {handler.Events.Count} events");
                }
                catch (Exception e)
                {
                    Logger.Error($"[DynamicEvents] Failed to register {handlerType.Name}: {e.Message}");
                }
            }
        }

        private void UnregisterEvents()
        {
            foreach (var handler in DynamicEventHandlers.Values)
            {
                handler.UnregisterEvents();
            }
            DynamicEventHandlers.Clear();
        }

        public bool CheckForMoonSharp()
        {
            foreach (var assembly in LabApi.Loader.PluginLoader.Dependencies)
            {
                if (assembly.FullName.Contains("MoonSharp"))
                    return true;
            }
            Logger.Error("Required Dependency MoonSharp not found, please install it following the documentation https://github.com/tayjay/SCriPt/wiki/Getting-Started");
            return false;
        }

        public override string Name { get; } = "SCriPt.LabAPI";
        public override string Description { get; } = "A plugin for Lua programming with LabAPI.";
        public override string Author { get; } = "TayTay";
        public override Version Version { get; } = typeof(SCriPt).Assembly.GetName().Version;
        public override Version RequiredApiVersion { get; } = new (LabApiProperties.CompiledVersion);
        public override LoadPriority Priority { get; } = LoadPriority.High;
        
        
        public static void RegisterType<T>() where T : class
        {
            if (UserData.IsTypeRegistered<T>())
            {
                Logger.Warn($"Type {typeof(T).Name} is already registered.");
                return;
            }
            
            UserData.RegisterType<T>();
            Logger.Info($"Registered type: {typeof(T).Name}");
        }
        
        public static void RegisterType(Type type)
        {
            if (UserData.IsTypeRegistered(type))
            {
                Logger.Warn($"Type {type.Name} is already registered.");
                return;
            }
            
            UserData.RegisterType(type);
            Logger.Info($"Registered type: {type.Name}");
        }

        public static void RegisterGlobal<T>(string name)
        {
            ScriptLoader.AddStaticGlobal<T>(name);
        }
        
        public static void RegisterGlobal(string name, Type type)
        {
            ScriptLoader.AddStaticGlobal(name, type);
        }
        
    }
}