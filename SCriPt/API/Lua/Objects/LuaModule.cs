using MoonSharp.Interpreter;

namespace SCriPt.API.Lua.Objects;

[MoonSharpUserData]
public class LuaModule
{
    private Script script;
    
    public string Name { get; private set; }
    public string Author { get; private set; }
    public string Version { get; private set; }
    public string Description { get; private set; }
    
    public LuaModule(Script script, string name, string author, string version, string description)
    {
        this.script = script;
        Name = name;
        Author = author;
        Version = version;
        Description = description;
    }

    

    
}