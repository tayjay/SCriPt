﻿using System.Collections.Generic;
using Exiled.API.Features;
using MoonSharp.Interpreter;

namespace SCriPt.EXILED.API.Lua.Globals
{
    [MoonSharpUserData]
    public class LuaCassie
    {
        public static bool IsSpeaking => Cassie.IsSpeaking;
        
        public static void Message(string message, bool isHeld = false, bool isNoisy= true, bool isSubtitles= false)
        {
            Exiled.API.Features.Cassie.Message(message, isHeld, isNoisy, isSubtitles);
        }
        
        public static void GlitchyMessage(string message, float glitchChance, float jamChance)
        {
            Exiled.API.Features.Cassie.GlitchyMessage(message, glitchChance, jamChance);
        }
        
        public static bool IsValidWord(string word)
        {
            return Exiled.API.Features.Cassie.IsValid(word);
        }
        
        public static bool IsValidSentence(string sentence)
        {
            return Exiled.API.Features.Cassie.IsValidSentence(sentence);
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
}