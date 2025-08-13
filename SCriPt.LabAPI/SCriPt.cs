using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using MoonSharp.Interpreter;
using SCriPt.LabAPI.API.Lua.Globals;
using SCriPt.LabAPI.API.Lua.Objects;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI
{
    public class SCriPt : Plugin<Config>
    {
        public static SCriPt Instance { get; private set; }

        public LuaServerEvents ServerEvents { get; set; }
        public LuaWarheadEvents WarheadEvents { get; set; }
        public LuaPlayerEvents PlayerEvents { get; set; }
        
        public LuaScp049Events Scp049Events { get; set; }
        
        public LuaScp096Events Scp096Events { get; set; }
        
        public LuaScp173Events Scp173Events { get; set; }
        
        public LuaScp939Events Scp939Events { get; set; }
        
        public LuaScp106Events Scp106Events { get; set; }
        
        public LuaScp0492Events Scp0492Events { get; set; }
        
        public LuaScp079Events Scp079Events { get; set; }
        
        public LuaScp914Events Scp914Events { get; set; }
        public LuaScp127Events Scp127Events { get; set; }
        public LuaScp3114Events Scp3114Events { get; set; }
        public LuaObjectiveEvents ObjectiveEvents { get; set; }
        
        
        
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
            ScriptLoader.LoadScripts();
            
            
        }

        

        public override void Disable()
        {
            if (!CheckForMoonSharp())
            {
                return;
            }
            UnregisterEvents();
            ScriptLoader.UnloadAllScripts();
        }
        
        private void RegisterEvents()
        {
            ServerEvents = new LuaServerEvents();
            WarheadEvents = new LuaWarheadEvents();
            PlayerEvents = new LuaPlayerEvents();
            Scp049Events = new LuaScp049Events();
            Scp096Events = new LuaScp096Events();
            Scp173Events = new LuaScp173Events();
            Scp939Events = new LuaScp939Events();
            Scp106Events = new LuaScp106Events();
            Scp0492Events = new LuaScp0492Events();
            Scp079Events = new LuaScp079Events();
            Scp914Events = new LuaScp914Events();
            Scp127Events = new LuaScp127Events();
            Scp3114Events = new LuaScp3114Events();
            ObjectiveEvents = new LuaObjectiveEvents();
            
            ServerEvents.RegisterEventTypes();
            WarheadEvents.RegisterEventTypes();
            PlayerEvents.RegisterEventTypes();
            Scp049Events.RegisterEventTypes();
            Scp096Events.RegisterEventTypes();
            Scp173Events.RegisterEventTypes();
            Scp939Events.RegisterEventTypes();
            Scp106Events.RegisterEventTypes();
            Scp0492Events.RegisterEventTypes();
            Scp079Events.RegisterEventTypes();
            Scp914Events.RegisterEventTypes();
            Scp127Events.RegisterEventTypes();
            Scp3114Events.RegisterEventTypes();
            ObjectiveEvents.RegisterEventTypes();
            
            GlobalSettings.RegisterTypes();
            
            ServerEvents.RegisterEvents();
            WarheadEvents.RegisterEvents();
            PlayerEvents.RegisterEvents();
            Scp049Events.RegisterEvents();
            Scp096Events.RegisterEvents();
            Scp173Events.RegisterEvents();
            Scp939Events.RegisterEvents();
            Scp106Events.RegisterEvents();
            Scp0492Events.RegisterEvents();
            Scp079Events.RegisterEvents();
            Scp914Events.RegisterEvents();
            Scp127Events.RegisterEvents();
            Scp3114Events.RegisterEvents();
            ObjectiveEvents.RegisterEvents();
            
            
            
            Logger.Info("Has PlayerSpawnEvent been registered? " + UserData.IsTypeRegistered<PlayerSpawnedEventArgs>());
        }
        
        private void UnregisterEvents()
        {
            ServerEvents.UnregisterEvents();
            WarheadEvents.UnregisterEvents();
            PlayerEvents.UnregisterEvents();
            Scp049Events.UnregisterEvents();
            Scp096Events.UnregisterEvents();
            Scp173Events.UnregisterEvents();
            Scp939Events.UnregisterEvents();
            Scp106Events.UnregisterEvents();
            Scp0492Events.UnregisterEvents();
            Scp079Events.UnregisterEvents();
            Scp914Events.UnregisterEvents();
            Scp127Events.UnregisterEvents();
            Scp3114Events.UnregisterEvents();
            ObjectiveEvents.UnregisterEvents();
            
            ServerEvents = null;
            WarheadEvents = null;
            PlayerEvents = null;
            Scp049Events = null;
            Scp096Events = null;
            Scp173Events = null;
            Scp939Events = null;
            Scp106Events = null;
            Scp0492Events = null;
            Scp079Events = null;
            Scp914Events = null;
            Scp127Events = null;
            Scp3114Events = null;
            ObjectiveEvents = null;
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
        public override Version Version { get; } = new (0, 5, 2);
        public override Version RequiredApiVersion { get; } = new (LabApiProperties.CompiledVersion);
        public override LoadPriority Priority { get; } = LoadPriority.Low;
        
        
        
        
        
    }
}