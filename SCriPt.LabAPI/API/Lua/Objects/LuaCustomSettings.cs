using System;
using System.Collections.Generic;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MoonSharp.Interpreter;
using UserSettings.ServerSpecific;

namespace SCriPt.LabAPI.API.Lua.Objects;

[MoonSharpUserData]
public class LuaCustomSettings
{
    private ScriptHandler Owner { get; set; }
    
    public Table SettingsTable;
    private Closure SettingsUpdateCallback;

    public LuaCustomSettings(ScriptHandler owner, Table settingsTable, Closure settingsUpdateCallback)
    {
        // Constructor for LuaCustomSettings
        Owner = owner ?? throw new ArgumentNullException(nameof(owner), "Owner cannot be null");
        SettingsTable = settingsTable;
        SettingsUpdateCallback = settingsUpdateCallback;
    }

    public void Activate()
    {
        List<ServerSpecificSettingBase> settings = new List<ServerSpecificSettingBase>();
        foreach (var pair in SettingsTable.Pairs)
        {
            settings.Add((ServerSpecificSettingBase)pair.Value.ToObject());
        }
        // Register the settings with the server
        ServerSpecificSettingsSync.DefinedSettings = settings.ToArray();
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += ServerOnSettingValueReceived;
        ServerSpecificSettingsSync.SendToAll();
    }
    
    public void Deactivate()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= ServerOnSettingValueReceived;
    }
    
    private void ServerOnSettingValueReceived(ReferenceHub hub, ServerSpecificSettingBase modifiedSetting)
    {

        try
        {
            // Handle the setting value received event
            // This method can be used to process the modified settings
            Owner.Call(SettingsUpdateCallback, Player.Get(hub), modifiedSetting.SettingId);
            // function mod.settingUpdated(player, settingId)
        }
        catch (ScriptRuntimeException ex)
        {
            Logger.Error("ScriptRuntimeError in ServerOnSettingValueReceived: " + ex.DecoratedMessage);
        }
        catch (Exception ex)
        {
            Logger.Error("Generated error in ServerOnSettingValueReceived: " + ex);
        }
        
    }

    public DynValue this[string key]
    {
        get => SettingsTable.Get(key);
        set => SettingsTable.Set(key, value);
    }
}