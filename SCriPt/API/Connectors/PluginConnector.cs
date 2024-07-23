using System;
using System.Collections.Generic;
using System.Reflection;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using MoonSharp.Interpreter;
using SCriPt.API.Lua.Globals;

namespace SCriPt.API.Connectors;

public class PluginConnector
{
    public string Name { get; private set; }
    public IPlugin<IConfig> Plugin { get; private set; }
    public Assembly Assembly { get; private set; }
    public List<Type> Types { get; private set; } = new List<Type>();
    public Dictionary<Type,Type> Proxies { get; private set; } = new Dictionary<Type, Type>();
    public List<Type> Events { get; private set; } = new List<Type>();
    public Dictionary<string, Type> Globals { get; private set; } = new Dictionary<string, Type>();
    
    public PluginConnector(IPlugin<IConfig> plugin, bool registerAll = true)
    {
        Plugin = plugin;
        Assembly = null;
        if(registerAll) Assembly = plugin.GetType().Assembly;
        Name = plugin.Name;
    }
    
    
    public void AddType(Type classType)
    {
        Types.Add(classType);
        UserData.RegisterType(classType);
    }
    
    public void AddGlobal(Type classType, string globalName)
    {
        
        Globals.Add(globalName, classType);
        Log.Debug("Added global: " + globalName);
    }

    public void AddEvents(Type classType, string eventName)
    {
        Globals.Add(eventName, classType);
    }

    public void AddProxy<TProxy,TTarget>()
        where TProxy : class
        where TTarget : class
    {
        UserData.RegisterType(typeof(TProxy));
        UserData.RegisterType(typeof(TTarget));
        UserData.RegisterProxyType<TProxy, TTarget>(target=>Activator.CreateInstance(typeof(TProxy), target) as TProxy);
    }

    public void RegisterAssembly()
    {
        if(Assembly == null) return;
        UserData.RegisterAssembly(Assembly);
    }
    
    public void RegisterTypes()
    {
        foreach (var type in Types)
        {
            UserData.RegisterType(type);
        }
    }

    public void RegisterEvents()
    {
        foreach (var eventClass in Events)
        {
            UserData.RegisterType(eventClass);
        }
    }
    
    public void RegisterGlobals(Script script)
    {
        foreach (var global in Globals)
        {
            UserData.RegisterType(global.Value);
            script.Globals[global.Key] = UserData.CreateStatic(global.Value);
            Log.Debug("Registered global: " + global.Key);
        }
    }
    
    public double GetStoredNumber(string table, string key)
    {
        if(!LuaStore.Tables.TryGetValue(table, out var storage)) return 0;
        return LuaStore.ExternalGetNumber(table, key);
    }
    
    public void SetStoredNumber(string table, string key, double value)
    {
        LuaStore.ExternalSetNumber(table, key, value);
    }
    
    public string GetStoredString(string table, string key)
    {
        if(!LuaStore.Tables.TryGetValue(table, out var storage)) return "";
        return LuaStore.ExternalGetString(table, key);
    }
    
    public void SetStoredString(string table, string key, string value)
    {
        LuaStore.ExternalSetString(table, key, value);
    }
    
    public bool GetStoredBool(string table, string key)
    {
        if(!LuaStore.Tables.TryGetValue(table, out var storage)) return false;
        return LuaStore.ExternalGetBool(table, key);
    }
    
    public void SetStoredBool(string table, string key, bool value)
    {
        LuaStore.ExternalSetBool(table, key, value);
    }
    
    public Dictionary<string,object> GetStoredTable(string table, string key)
    {
        if(!LuaStore.Tables.TryGetValue(table, out var storage)) return new Dictionary<string, object>();
        return LuaStore.ExternalGetTable(table, key);
    }
    
    public void SetStoredTable(string table, string key, Dictionary<string,object> value)
    {
        LuaStore.ExternalSetTable(table, key, value);
    }


    public int GetConfigInt(string key)
    {
        if(!LuaConfig.Config.Config.ContainsKey(key)) return 0;
        return LuaConfig.LoadInt(key, 0);
    }
    
    public float GetConfigFloat(string key)
    {
        if(!LuaConfig.Config.Config.ContainsKey(key)) return 0;
        return LuaConfig.LoadFloat(key, 0);
    }
    
    public string GetConfigString(string key)
    {
        if(!LuaConfig.Config.Config.ContainsKey(key)) return "";
        return LuaConfig.LoadString(key, "");
    }
    
    public bool GetConfigBool(string key)
    {
        if(!LuaConfig.Config.Config.ContainsKey(key)) return false;
        return LuaConfig.LoadBool(key, false);
    }
    
}