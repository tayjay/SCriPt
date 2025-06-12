using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.LabAPI.Handlers;


public interface ILuaEventHandler
{
    [MoonSharpHidden]
    void RegisterEventTypes();

    [MoonSharpHidden]
    void RegisterEvents();
    
    [MoonSharpHidden]
    void UnregisterEvents();
}