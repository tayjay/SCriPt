using System.Collections.Generic;
using LabApi.Features.Wrappers;
using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.API.Lua.Globals;
[MoonSharpUserData]
public class GlobalCassie
{
    public static bool IsSpeaking => Cassie.IsSpeaking;
        
    public static void Message(string message, bool isHeld = false, bool isNoisy= true, bool isSubtitles= false)
    {
        Cassie.Message(message, isHeld, isNoisy, isSubtitles);
    }
        
    public static void GlitchyMessage(string message, float glitchChance, float jamChance)
    {
        Cassie.GlitchyMessage(message, glitchChance, jamChance);
    }
        
    public static bool IsValidWord(string word)
    {
        return Cassie.IsValid(word);
    }

    public static string[] GetAllWords()
    {
        List<string> voiceLines = new List<string>();
        foreach (var kvp in NineTailedFoxAnnouncer.singleton.voiceLines)
        {
            voiceLines.Add(kvp.apiName);
        }
        return voiceLines.ToArray();
    }
}