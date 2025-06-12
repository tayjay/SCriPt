using System;
using System.Collections.Generic;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using SCriPt.LabAPI.API.Lua.Objects;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI
{
    public class SCriPt : Plugin
    {

        public static LuaServerEvents ServerEvents { get; set; }
        public static LuaWarheadEvents WarheadEvents { get; set; }
        public static LuaPlayerEvents PlayerEvents { get; set; }
        
        public static LuaScp049Events Scp049Events { get; set; }
        
        public static LuaScp096Events Scp096Events { get; set; }
        
        public static LuaScp173Events Scp173Events { get; set; }
        
        public static LuaScp939Events Scp939Events { get; set; }
        
        public static LuaScp106Events Scp106Events { get; set; }
        
        public static LuaScp0492Events Scp0492Events { get; set; }
        
        public static LuaScp079Events Scp079Events { get; set; }
        
        public static LuaScp914Events Scp914Events { get; set; }
        
        
        
        public static Dictionary<string,ScriptHandler> Scripts;
        
        public override void Enable()
        {
            if (!CheckForMoonSharp())
            {
                return;
            }
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
        public override Version Version { get; } = new (0, 1, 0);
        public override Version RequiredApiVersion { get; } = new (LabApiProperties.CompiledVersion);
        public override LoadPriority Priority { get; } = LoadPriority.Low;
    }
}