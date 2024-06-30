using System;
using System.Collections.Generic;
using System.Reflection;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using MoonSharp.Interpreter;

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
    
    
    
    
}