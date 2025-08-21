using LabApi.Features.Wrappers;
using MoonSharp.Interpreter;
using TMPro;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace SCriPt.LabAPI.API.Lua.Globals;

[MoonSharpUserData]
public class GlobalSettings
{

    public static SSButton Button(string label, string buttonText, string hint = null)
    {
        return new SSButton(null, label, buttonText, null, hint);
    }

    public static SSGroupHeader GroupHeader(string label, string hint = null)
    {
        return new SSGroupHeader(null, label, hint:hint);
    }
    
    public static SSTextArea TextArea(string content)
    {
        return new SSTextArea(null, content);
    }
    
    public static SSDropdownSetting Dropdown(string label, string[] options, int selectedIndex = 0, string hint = null)
    {
        return new SSDropdownSetting(null, label, options, selectedIndex, hint:hint);
    }
    
    public static SSKeybindSetting Keybind(string label, KeyCode defaultKey = KeyCode.None, string hint = null)
    {
        return new SSKeybindSetting(null, label, defaultKey, hint:hint);
    }
    
    public static SSPlaintextSetting Plaintext(string label, string content, string hint = null, TMP_InputField.ContentType contentType = TMP_InputField.ContentType.Standard)
    {
        return new SSPlaintextSetting(null, label, content, contentType:contentType, hint:hint);
    }
    
    public static SSSliderSetting Slider(string label, float minValue, float maxValue, float defaultValue, string hint = null)
    {
        return new SSSliderSetting(null, label, minValue, maxValue, defaultValue, hint:hint);
    }
    
    public static SSTwoButtonsSetting TwoButtons(string label, string buttonAText, string buttonBText, bool defaultIsB = false, string hint = null)
    {
        return new SSTwoButtonsSetting(null, label, buttonAText, buttonBText, defaultIsB, hint:hint);
    }


    public static ServerSpecificSettingBase GetSettingOfPlayer(Player player, int id)
    {
        ServerSpecificSettingsSync.TryGetSettingOfUser(player.ReferenceHub, id, out ServerSpecificSettingBase setting);
        return setting;
    }
    
    
    [MoonSharpHidden]
    public static void RegisterTypes()
    {
        UserData.RegisterType<ServerSpecificSettingBase>();
        UserData.RegisterType<SSGroupHeader>();
        UserData.RegisterType<SSButton>();
        UserData.RegisterType<SSTextArea>();
        UserData.RegisterType<SSDropdownSetting>();
        UserData.RegisterType<SSKeybindSetting>();
        UserData.RegisterType<SSPlaintextSetting>();
        UserData.RegisterType<SSSliderSetting>();
        UserData.RegisterType<SSTwoButtonsSetting>();
    }
}