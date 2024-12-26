using System;
using System.IO;
using Exiled.API.Features;
using MoonSharp.Interpreter;
using SCriPt.API.Lua.Objects;
using UnityEngine;
using Utf8Json;

namespace SCriPt.API.Lua.Globals;

[MoonSharpUserData]
public class LuaConfig
{
    [MoonSharpHidden] 
    public static CustomConfig Config { get; set; }

    public static string ScriptPath => NewScriptLoader.ScriptPath;
    
    public static int LoadInt(string key, int defaultValue)
    {
        Log.Debug("Loading config int value. Key: "+key+" Default: "+defaultValue);
        if(Config.GetInt(key,defaultValue, out int value))
        {
            SaveConfig();
            Log.Debug("Config value not found, saving default value.");
            return defaultValue;
        }
        Log.Debug("Config value found: "+value);
        return value;
    }
    
    public static float LoadFloat(string key, float defaultValue)
    {
        Log.Debug("Loading config float value. Key: "+key+" Default: "+defaultValue);
        if(Config.GetFloat(key,defaultValue, out float value))
        {
            SaveConfig();
            Log.Debug("Config value not found, saving default value.");
            return defaultValue;
        }
        Log.Debug("Config value found: "+value);
        return value;
    }
    
    public static string LoadString(string key, string defaultValue)
    {
        if(Config.GetString(key,defaultValue, out string value))
        {
            SaveConfig();
            return defaultValue;
        }

        return value;
    }
    
    public static bool LoadBool(string key, bool defaultValue)
    {
        if(Config.GetBool(key,defaultValue, out bool value))
        {
            SaveConfig();
            return defaultValue;
        }

        return value;
    }
    
    public static int Load(string key, int defaultValue)
    {
        return LoadInt(key, defaultValue);
    }
    
    public static float Load(string key, float defaultValue)
    {
        return LoadFloat(key, defaultValue);
    }
    
    public static string Load(string key, string defaultValue)
    {
        return LoadString(key, defaultValue);
    }
    
    public static bool Load(string key, bool defaultValue)
    {
        return LoadBool(key, defaultValue);
    }
    
    public static Vector3 Load(string key, Vector3 defaultValue)
    {
        if(Config.GetVector3(key,defaultValue, out Vector3 value))
        {
            SaveConfig();
            return defaultValue;
        }

        return value;
    }
    
    [MoonSharpHidden]
    public static void LoadConfig()
    {
        try
        {
            if(!Directory.Exists(ScriptPath+"Data"))
                Directory.CreateDirectory(ScriptPath+"Data");
            if (!File.Exists(ScriptPath+"Data/config.json"))
            {
                CustomConfig newConfig = new CustomConfig();
                //string json = JsonSerialize.ToJson(newConfig);
                string json = JsonSerializer.ToJsonString(newConfig);
                File.WriteAllText(ScriptPath+"Data/config.json", JsonSerializer.PrettyPrint(json));
            }
        
            string data = File.ReadAllText(ScriptPath+"Data/config.json");
            Config = JsonSerializer.Deserialize<CustomConfig>(data);
            Log.Debug("Lua Config loaded.");
        } catch (Exception e)
        {
            Log.Error("Error loading Lua Config: "+e);
        }
    }
    
    [MoonSharpHidden]
    public static void SaveConfig()
    {
        try
        {
            string json = JsonSerializer.ToJsonString(Config);
            File.WriteAllText(ScriptPath+"Data/config.json", JsonSerializer.PrettyPrint(json));
            Log.Debug("Lua Config saved.");
        } catch (Exception e)
        {
            Log.Error("Error saving Lua Config: "+e);
        }
        
    }
}