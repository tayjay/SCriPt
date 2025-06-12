using System;
using System.Collections.Generic;
using GameCore;
using LabApi.Features.Console;
using MoonSharp.Interpreter;
using SCriPt.LabAPI.API.Lua.Globals;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI.API.Lua.Objects;

public class ScriptHandler : Script
{
    public string Name { get; set; }
    
    GlobalCoroutines Coroutines;
    GlobalSCriPt SCriPt;
    
    public List<LuaModule> Modules;
    public List<LuaCustomCommand> CustomCommands;
    
    public ScriptHandler(string name, CoreModules sandbox) : base(sandbox)
    {
        Name = name;
        Modules = new List<LuaModule>();
        CustomCommands = new List<LuaCustomCommand>();
    }

    public void SetupAPI()
    {
        Logger.Debug("Setting up API for script: " + Name);
        Options.DebugPrint = s => Logger.Debug($"[Lua-{Name}] {s}");
        ScriptLoader.AddGlobalsToScript(this);
        Coroutines = new GlobalCoroutines(this);
        Globals["Timing"] = Coroutines;
        SCriPt = new GlobalSCriPt(this);
        Globals["SCriPt"] = SCriPt;
    }

    public void ExecuteLoad()
    {
        Logger.Debug("Executing load for script: " + Name);
        foreach (LuaModule module in Modules)
        {
            module.LoadModule();
        }
    }
    
    public void ExecuteUnload()
    {
        Logger.Debug("Executing unload for script: " + Name);
        foreach (LuaModule module in Modules)
        {
            module.UnloadModule();
        }
    }


    public static ScriptHandler Create(string path, CoreModules sandbox)
    {
        if (!path.EndsWith(".lua"))
        {
            Logger.Error("Script file must be a .lua file");
            throw new Exception("Script file must be a .lua file");
        }
        string name = path.Replace('\\', '/');
        name = name.Substring(name.LastIndexOf('/') + 1);
        name = name.Substring(0, name.Length - 4);
        if(LabAPI.SCriPt.Scripts.ContainsKey(name))
        {
            Logger.Error("Script already loaded: "+name);
            throw new Exception("Script already loaded: "+name);
        }
        ScriptHandler handler = new ScriptHandler(name, sandbox);
        handler.SetupAPI();
        Logger.Debug("Loaded API...");
        handler.DoFile(path);
        Logger.Debug("Loaded file...");
        
        
        handler.ExecuteLoad();
        Logger.Debug("Executed load functions...");
        
        Logger.Info("Loaded script: " + name);
        return handler;
    }
    
}