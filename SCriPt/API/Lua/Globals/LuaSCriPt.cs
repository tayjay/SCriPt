using System;
using System.IO;
using System.Linq;
using Exiled.API.Features;
using MoonSharp.Interpreter;
using SCriPt.API.Lua.Objects;

namespace SCriPt.API.Lua.Globals;

[MoonSharpUserData]
public class LuaSCriPt
{
    public static string Version => SCriPt.Instance.Version.ToString();
    public static int LoadedScripts => SCriPt.Instance.LoadedScripts.Count();
    public static string MoonSharpVersion => MoonSharp.Interpreter.Script.VERSION;
    public static string LuaVersion => MoonSharp.Interpreter.Script.LUA_VERSION;
    
    private ScriptWrapper Script { get; set; }
    
    
    public LuaSCriPt(ScriptWrapper script)
    {
        Script = script;
    }
    
    public void require(string scriptName)
    {
        if(NewScriptLoader.Globals.TryGetValue(scriptName, out DynValue value))
        {
            Script.Globals[scriptName] = value;
            Log.Debug("Loaded Global "+scriptName+" into script.");
            return;
        }
        
        if(File.Exists(SCriPt.Instance.Config.ScriptPath+"Globals/"+scriptName+".lua"))
        {
            Script.DoFile("Scripts/Globals/"+scriptName+".lua");
            Log.Debug("Loaded Script "+scriptName+" into script.");
        }
        else
        {
            Log.Error("Script "+scriptName+" does not exist.");
        }
    }
    

    //todo: This is going to break sandboxing, so need to add config option to enable this
    public static DynValue ExecuteInScript(string scriptName, string code)
    {
        if(!SCriPt.Instance.Config.FullAccess)
        {
            Log.Error("FullAccess is not enabled, cannot execute in script.");
            return DynValue.Nil;
        }
        if (!SCriPt.Instance.LoadedScripts.TryGetValue(scriptName, out Script script))
        {
            return DynValue.Nil;
        }

        return script.DoString(code);
        //SCriPt:ExecuteInScript('AutoLoad', 'print("Hello World!")')
    }
    
    public DynValue ExecuteInSandbox(string code)
    {
        ScriptWrapper wrapper = new ScriptWrapper(Script.Name+"-Sandbox-"+DateTime.Now.GetHashCode(), CoreModules.Preset_SoftSandbox);
        ScriptLoader.RegisterAPI(wrapper);
        SCriPt.Instance.LoadedScripts[wrapper.Name] = wrapper;
        return ((Script)wrapper).DoString(code);
        //SCriPt:ExecuteInSandbox('print("Hello World!")')
    }
}