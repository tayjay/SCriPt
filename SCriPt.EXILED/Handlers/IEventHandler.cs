using Exiled.Events.EventArgs.Warhead;
using MoonSharp.Interpreter;

namespace SCriPt.EXILED.Handlers
{
    public interface IEventHandler
    {

        [MoonSharpHidden]
        void RegisterEvents();

        [MoonSharpHidden]
        void RegisterEventTypes();

        [MoonSharpHidden]
        void UnregisterEvents();
    }
}