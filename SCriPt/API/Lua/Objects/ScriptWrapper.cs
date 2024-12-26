using System;
using System.Collections.Generic;
using System.IO;
using Exiled.API.Features;
using MoonSharp.Interpreter;
using SCriPt.API.Lua.Globals;
using UnityEngine;

namespace SCriPt.API.Lua.Objects;

public class ScriptWrapper : Script
{
    public string Name { get; set; }
    
    LuaCoroutines coroutines;
    LuaSCriPt luaSCriPt;
    LuaCustomItems customItems;
    
    List<CustomCommand> customCommands = new List<CustomCommand>();
    
    //Here we can add more properties to a script that cannot be manipulated by the script itself
    //If there is something sensitive we want to keep track of on a script but cannot trust the script author or a malicious actor from changing it we can add it here

    public ScriptWrapper(string name, CoreModules sandboxLevel) : base(sandboxLevel)
    {
        Name = name;
    }

    public void SetupAPI()
    {
        Log.Debug("Starting API setup...");
        Options.DebugPrint = s => Log.Send($"[Lua-{Name}] {s}", Discord.LogLevel.Debug, ConsoleColor.Green);
        NewScriptLoader.AddGlobalsToScript(this);
        //NewScriptLoader.AddCustomGlobalsToScript(this); todo: Add a requires API to the script instead
        SCriPt.Instance.RegisterAllGlobals(this); //To register external globals if any
        coroutines = new LuaCoroutines(this);
        Globals["Timing"] = coroutines;
        luaSCriPt = new LuaSCriPt(this);
        Globals["SCriPt"] = luaSCriPt;
        customItems = new LuaCustomItems();
        customItems.AddScript(this);
        Globals["CustomItems"] = customItems;
        
        //script.Globals["RegisterType"] = (Action<string,string>) RegisterType;
        //new:Vector3(1,2,3)
        //Globals["Vector3"] = (Func<float, float, float, Vector3>) ((x, y, z) => new Vector3(x, y, z));
        //Globals["Quaternion"] = (Func<float, float, float, float, Quaternion>) ((x, y, z, w) => new Quaternion(x, y, z, w));
        //Globals["Color"] = (Func<float, float, float, float, Color>) ((r, g, b, a) => new Color(r, g, b, a));
        //script.Globals["PerformanceLog"] = (Func<string>) (() => script.PerformanceStats.GetPerformanceLog());
    }
    
    public void FromFile(string path)
    {
        if (!path.EndsWith(".lua"))
        {
            Log.Error("Script file must be a .lua file");
        }
        DoFile(path);
    }
    
    public static ScriptWrapper FromFile(string path, CoreModules sandboxLevel)
    {
        if (!path.EndsWith(".lua"))
        {
            Log.Error("Script file must be a .lua file");
            throw new Exception("Script file must be a .lua file");
        }
        string name = path.Replace('\\', '/');
        name = name.Substring(name.LastIndexOf('/') + 1);
        name = name.Substring(0, name.Length - 4);
        if(SCriPt.Instance.LoadedWrappers.ContainsKey(name))
        {
            Log.Error("Script already loaded: "+name);
            throw new Exception("Script already loaded: "+name);
        }
        ScriptWrapper wrapper = new ScriptWrapper(name, sandboxLevel);
        wrapper.SetupAPI();
        Log.Debug("Loaded API...");
        wrapper.FromFile(path);
        Log.Debug("Loaded file...");
        wrapper.ExecuteLoad();
        Log.Debug("Executed load...");
        wrapper.customCommands = CustomCommand.CreateAll(wrapper);
        wrapper.RegisterCommands();
        Log.Debug($"Created {wrapper.customCommands.Count} commands...");
        Log.Info("Loaded script: " + name);
        return wrapper;
    }
    
    public static ScriptWrapper FromString(string script, CoreModules sandboxLevel)
    {
        string name = "StringScript";
        if(SCriPt.Instance.LoadedWrappers.ContainsKey(name))
        {
            Log.Error("Script already loaded: "+name);
            throw new Exception("Script already loaded: "+name);
        }
        ScriptWrapper wrapper = new ScriptWrapper(name, sandboxLevel);
        wrapper.SetupAPI();
        Log.Debug("Loaded API...");
        wrapper.DoString(script);
        Log.Debug("Loaded file...");
        wrapper.ExecuteLoad();
        Log.Debug("Executed load...");
        wrapper.customCommands = CustomCommand.CreateAll(wrapper);
        wrapper.RegisterCommands();
        Log.Debug($"Created {wrapper.customCommands.Count} commands...");
        Log.Info("Loaded script: " + name);
        return wrapper;
    }

    public static ScriptWrapper FromWeb(string path, CoreModules sandboxLevel = CoreModules.Preset_SoftSandbox)
    {
        if (!path.EndsWith(".pastebin"))
        {
            Log.Error("Script file must be a .pastebin file");
            throw new Exception("Script file must be a .pastebin file");
        }
        string name = path.Replace('\\', '/');
        name = name.Substring(name.LastIndexOf('/') + 1);
        name = name.Substring(0, name.Length - 9);
        if(SCriPt.Instance.LoadedWrappers.ContainsKey(name))
        {
            Log.Error("Script already loaded: "+name);
            throw new Exception("Script already loaded: "+name);
        }
        
        string script = WebLoader.GetFromPastebin(name);
        if (script == null)
        {
            Log.Error("Failed to load script from pastebin: " + name);
            throw new Exception("Failed to load script from pastebin: " + name);
        }
        Log.Info("Loaded script from pastebin: " + name);
        
        ScriptWrapper wrapper = new ScriptWrapper(name, sandboxLevel);
        wrapper.SetupAPI();
        Log.Debug("Loaded API...");
        wrapper.DoString(script);
        Log.Debug("Loaded file...");
        wrapper.ExecuteLoad();
        Log.Debug("Executed load...");
        wrapper.customCommands = CustomCommand.CreateAll(wrapper);
        wrapper.RegisterCommands();
        Log.Debug($"Created {wrapper.customCommands.Count} commands...");
        Log.Info("Loaded script: " + name);
        return wrapper;
    }

    public void ExecuteLoad()
    {
        // Get the global table
        Table globals = Globals;

        // Iterate 
        foreach (var pair in globals.Pairs)
        {
            // Check if the value is a table
            if (pair.Value.Type == DataType.Table)
            {
                Table table = pair.Value.Table;

                if (table.Get("loaded").Type == DataType.Boolean)
                {
                    if (table.Get("loaded").Boolean)
                    {
                        Log.Info("Skipping loaded table: " + pair.Key);
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
                        Call(loadFunction);
                        Log.Info("Loaded table: " + pair.Key);
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
    
    public void ExecuteUnload()
    {
        // Get the global table
        Table globals = Globals;

        // Iterate 
        foreach (var pair in globals.Pairs)
        {
            // Check if the value is a table
            if (pair.Value.Type == DataType.Table)
            {
                Table table = pair.Value.Table;

                if (table.Get("loaded").Type == DataType.Boolean)
                {
                    if (!table.Get("loaded").Boolean)
                    {
                        Log.Info("Skipping unloaded script: " + pair.Key);
                        continue;
                    }
                }

                // Check for the "unload" function
                if (table.Get("unload").Type == DataType.Function)
                {
                    DynValue unloadFunction = table.Get("unload");
                    try
                    {
                        // Execute the load function
                        Call(unloadFunction);
                        Log.Info("Unloaded table: " + pair.Key);
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

        UnregisterCommands();
        coroutines.KillAll();
            
    }
    
    public void RegisterCommands()
    {
        foreach(CustomCommand command in customCommands)
        {
            command.Register();
        }
    }
    
    public void UnregisterCommands()
    {
        foreach(CustomCommand command in customCommands)
        {
            command.Unregister();
        }
    }
    
}