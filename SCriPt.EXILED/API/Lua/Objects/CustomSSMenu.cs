using System.Collections.Generic;
using Exiled.API.Features;
using HarmonyLib;
using MoonSharp.Interpreter;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace SCriPt.EXILED.API.Lua.Objects;

public class CustomSSMenu
{
    private Script script;
    
    public List<ServerSpecificSettingBase> Settings { get; set; }
    public Dictionary<int, Closure> Events { get; set; }
    
    
    public CustomSSMenu(Script script)
    {
        this.script = script;
        Settings = new List<ServerSpecificSettingBase>();
        Events = new Dictionary<int, Closure>();
    }

    public void GenerateMenu()
    {
        Log.Debug("Attempting to generate Menu settings for script.");
        ServerSpecificSettingsSync.DefinedSettings.AddRangeToArray(Settings.ToArray());
        
        //ServerSpecificSettingsSync.DefinedSettings = Settings.ToArray();
        
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += (hub, value) =>
        {
            foreach(KeyValuePair<int,Closure> entry in Events)
            {
                if(entry.Key == value.SettingId)
                {
                    this.script.Call(entry.Value, new object[] {value, hub});
                    //entry.Value.Call(value, hub);
                }
            }
        };
        
        ServerSpecificSettingsSync.UpdateDefinedSettings();
    }
    
    /*
     * mod = {}
     * mod.menu = {
     *    SSHeader("Header"),
     *    SSTextArea("Text"),
     *    SSButton("Label", "ButtonText", function(setting, hub) end)
     * }
     * 
     */
    
    public void AddHeader(string text)
    {
        Settings.Add(new SSGroupHeader(text));
    }

    public void AddTextArea(string text)
    {
        Settings.Add(new SSTextArea(null, text));
    }
    
    public void AddButton(string label, string buttonText, Closure callback)
    {
        SSButton button = new SSButton(null, label, buttonText);
        Events.Add(button.SettingId, callback);
        Settings.Add(button);
    }
    
    public void AddKeyBind(string label, Closure callback, int defaultKey)
    {
        KeyCode key = (KeyCode)defaultKey;
        SSKeybindSetting keybind = new SSKeybindSetting(null, label, key);
        Events.Add(keybind.SettingId, callback);
        Settings.Add(keybind);
    }
    
    
}