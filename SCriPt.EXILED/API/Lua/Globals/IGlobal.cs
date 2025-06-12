using MoonSharp.Interpreter;

namespace SCriPt.EXILED.API.Lua.Globals;

public interface IGlobal
{
    Script Owner { get; }
}