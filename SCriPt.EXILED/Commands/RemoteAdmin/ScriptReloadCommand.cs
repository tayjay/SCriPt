﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MoonSharp.Interpreter;
using SCriPt.EXILED.API.Lua;

namespace SCriPt.EXILED.Commands.RemoteAdmin;

public class ScriptReloadCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if(!sender.CheckPermission("SCriPt.reload"))
        {
            response = "You do not have permission to use this command.";
            return false;
        }
        
        if (arguments.Count != 0)
        {
            response = "Usage: script reload";
            return false;
        }
        
        /*foreach(var script in SCriPt.Instance.LoadedWrappers)
        {
            try
            {
                script.Value.ExecuteUnload();
                //ScriptLoader.ExecuteUnload(script.Value);
            } catch (IOException e)
            {
                Log.Error("Error unloading script: "+e.Message);
                response = $"Error reloading scripts: {e.Message}";
                return false;
            }
        }
        SCriPt.Instance.LoadedScripts.Clear();*/
        NewScriptLoader.UnloadAllScripts();
        
        try
        {
            //ScriptLoader.AutoLoad();
            NewScriptLoader.LoadScripts();
        } catch (IOException e)
        {
            Log.Error("Error reading script file: "+e.Message);
            response = $"Error reloading scripts: {e.Message}";
            return false;
        }
        
        response = $"Reloaded scripts successfully.";
        return true;
        
    }

    public string Command { get; } = "reload";
    public string[] Aliases { get; } = { "restart" };
    public string Description { get; } = "Reloads a loaded script.";
    public bool SanitizeResponse { get; } = true;
}