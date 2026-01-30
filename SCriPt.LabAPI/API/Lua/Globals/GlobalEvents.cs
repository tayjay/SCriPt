using MoonSharp.Interpreter;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI.API.Lua.Globals;

/// <summary>
/// Entry point for Lua event access: Events.Player, Events.Warhead, Events.Scp049, etc.
/// Member resolution is handled by GlobalEventsDescriptor, which resolves aliases
/// and falls back to auto-discovered LabAPI event categories.
/// </summary>
public class GlobalEvents
{
}
