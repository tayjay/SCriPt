using MoonSharp.Interpreter;

namespace SCriPt.API.Lua.Globals;

[MoonSharpUserData]
public class LuaSCriPt
{
    public static Table Register(string name)
    {
        string scriptCode = @"
                return {test=true, name="+name+"}";
        return Script.RunString(scriptCode).Table;
        
    }
    
    /*public static Table RegisterCommand(string command)
    {
        Script
        string scriptCode = @"
                return {load=function() print('Loading command') Events.Command.RemoteAdminCommandExecuted:add(function(ev) print(ev) end) end}";
        return Script.RunString(scriptCode).Table;
    }*/
}