using System;
using CustomPlayerEffects;
using LabApi.Events.Arguments.Scp127Events;
using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.Handlers;

[MoonSharp.Interpreter.MoonSharpUserData]
public class LuaScp127Events : ILuaEventHandler
{
    public event EventHandler<Scp127GainingExperienceEventArgs> GainingExperience;
    public event EventHandler<Scp127GainExperienceEventArgs> GainedExperience;
    public event EventHandler<Scp127LevellingUpEventArgs> LevellingUp;
    public event EventHandler<Scp127LevelUpEventArgs> LeveledUp;
    public event EventHandler<Scp127TalkingEventArgs> Talking;
    public event EventHandler<Scp127TalkedEventArgs> Talked;
    
    [MoonSharpHidden]
    public void OnGainingExperience(Scp127GainingExperienceEventArgs ev)
    {
        GainingExperience?.Invoke(this, ev);
    }
    [MoonSharpHidden]
    public void OnGainedExperience(Scp127GainExperienceEventArgs ev)
    {
        GainedExperience?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnLevellingUp(Scp127LevellingUpEventArgs ev)
    {
        LevellingUp?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnLeveledUp(Scp127LevelUpEventArgs ev)
    {
        LeveledUp?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnTalking(Scp127TalkingEventArgs ev)
    {
        Talking?.Invoke(this, ev);
    }
    
    [MoonSharpHidden]
    public void OnTalked(Scp127TalkedEventArgs ev)
    {
        Talked?.Invoke(this, ev);
    }
    
    
    
    public void RegisterEventTypes()
    {
        MoonSharp.Interpreter.UserData.RegisterType<Scp127GainingExperienceEventArgs>();
        MoonSharp.Interpreter.UserData.RegisterType<Scp127GainExperienceEventArgs>();
        MoonSharp.Interpreter.UserData.RegisterType<Scp127LevellingUpEventArgs>();
        MoonSharp.Interpreter.UserData.RegisterType<Scp127LevelUpEventArgs>();
        MoonSharp.Interpreter.UserData.RegisterType<Scp127TalkingEventArgs>();
        MoonSharp.Interpreter.UserData.RegisterType<Scp127TalkedEventArgs>();
    }

    public void RegisterEvents()
    {
        LabApi.Events.Handlers.Scp127Events.GainingExperience += OnGainingExperience;
        LabApi.Events.Handlers.Scp127Events.GainExperience += OnGainedExperience;
        LabApi.Events.Handlers.Scp127Events.LevellingUp += OnLevellingUp;
        LabApi.Events.Handlers.Scp127Events.LevelUp += OnLeveledUp;
        LabApi.Events.Handlers.Scp127Events.Talking += OnTalking;
        LabApi.Events.Handlers.Scp127Events.Talked += OnTalked;
    }

    public void UnregisterEvents()
    {
        throw new System.NotImplementedException();
    }
}