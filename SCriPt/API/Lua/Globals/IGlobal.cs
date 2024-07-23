using MoonSharp.Interpreter;

namespace SCriPt.API.Lua.Globals;

public interface IGlobal
{
    Script Owner { get; }
}