﻿using System;
using System.Collections.Generic;
using Exiled.Events.EventArgs.Scp939;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.EXILED.Handlers
{
    [MoonSharpUserData]
    public class Scp939Events : IEventHandler
    {
        
        [MoonSharpVisible(true)]
        public event EventHandler<ChangingFocusEventArgs> ChangingFocus;
        
        [MoonSharpHidden]
        public void OnChangingFocus(ChangingFocusEventArgs ev)
        {
            ChangingFocus?.Invoke(this, ev);
        }
        
        [MoonSharpVisible(true)]
        public event EventHandler<LungingEventArgs> Lunging;
        
        [MoonSharpHidden]
        public void OnLunging(LungingEventArgs ev)
        {
            Lunging?.Invoke(this, ev);
        }
        
        [MoonSharpVisible(true)]
        public event EventHandler<PlacingAmnesticCloudEventArgs> PlacingAmnesticCloud;
        
        [MoonSharpHidden]
        public void OnPlacingAmnesticCloud(PlacingAmnesticCloudEventArgs ev)
        {
            PlacingAmnesticCloud?.Invoke(this, ev);
        }
        
        [MoonSharpVisible(true)]
        public event EventHandler<PlayingVoiceEventArgs> PlayingVoice;
        
        [MoonSharpHidden]
        public void OnPlayingVoice(PlayingVoiceEventArgs ev)
        {
            PlayingVoice?.Invoke(this, ev);
        }
        
        [MoonSharpVisible(true)]
        public event EventHandler<SavingVoiceEventArgs> SavingVoice;
        
        [MoonSharpHidden]
        public void OnSavingVoice(SavingVoiceEventArgs ev)
        {
            SavingVoice?.Invoke(this, ev);
        }
        
        [MoonSharpVisible(true)]
        public event EventHandler<PlayingSoundEventArgs> PlayingSound;
        
        [MoonSharpHidden]
        public void OnPlayingSound(PlayingSoundEventArgs ev)
        {
            PlayingSound?.Invoke(this, ev);
        }
        
        [MoonSharpVisible(true)]
        public event EventHandler<ClawedEventArgs> Clawed;
        
        [MoonSharpHidden]
        public void OnClawed(ClawedEventArgs ev)
        {
            Clawed?.Invoke(this, ev);
        }
        
        [MoonSharpVisible(true)]
        public event EventHandler<ValidatingVisibilityEventArgs> ValidatingVisibility;
        
        [MoonSharpHidden]
        public void OnValidatingVisibility(ValidatingVisibilityEventArgs ev)
        {
            ValidatingVisibility?.Invoke(this, ev);
        }
        
        [MoonSharpVisible(true)]
        public event EventHandler<PlayingFootstepEventArgs> PlayingFootstep;
        
        [MoonSharpHidden]
        public void OnPlayingFootstep(PlayingFootstepEventArgs ev)
        {
            PlayingFootstep?.Invoke(this, ev);
        }
        
        
        public void RegisterEvents()
        {
            Exiled.Events.Handlers.Scp939.ChangingFocus += OnChangingFocus;
            Exiled.Events.Handlers.Scp939.Lunging += OnLunging;
            Exiled.Events.Handlers.Scp939.PlacingAmnesticCloud += OnPlacingAmnesticCloud;
            Exiled.Events.Handlers.Scp939.PlayingVoice += OnPlayingVoice;
            Exiled.Events.Handlers.Scp939.SavingVoice += OnSavingVoice;
            Exiled.Events.Handlers.Scp939.PlayingSound += OnPlayingSound;
            Exiled.Events.Handlers.Scp939.Clawed += OnClawed;
            Exiled.Events.Handlers.Scp939.ValidatingVisibility += OnValidatingVisibility;
            Exiled.Events.Handlers.Scp939.PlayingFootstep += OnPlayingFootstep;
        }

        

        public void RegisterEventTypes()
        {
            UserData.RegisterType<ChangingFocusEventArgs>();
            UserData.RegisterType<LungingEventArgs>();
            UserData.RegisterType<PlacingAmnesticCloudEventArgs>();
            UserData.RegisterType<PlayingVoiceEventArgs>();
            UserData.RegisterType<SavingVoiceEventArgs>();
            UserData.RegisterType<PlayingSoundEventArgs>();
            UserData.RegisterType<ClawedEventArgs>();
            UserData.RegisterType<ValidatingVisibilityEventArgs>();
            UserData.RegisterType<PlayingFootstepEventArgs>();
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Scp939.ChangingFocus -= OnChangingFocus;
            Exiled.Events.Handlers.Scp939.Lunging -= OnLunging;
            Exiled.Events.Handlers.Scp939.PlacingAmnesticCloud -= OnPlacingAmnesticCloud;
            Exiled.Events.Handlers.Scp939.PlayingVoice -= OnPlayingVoice;
            Exiled.Events.Handlers.Scp939.SavingVoice -= OnSavingVoice;
            Exiled.Events.Handlers.Scp939.PlayingSound -= OnPlayingSound;
            Exiled.Events.Handlers.Scp939.Clawed -= OnClawed;
            Exiled.Events.Handlers.Scp939.ValidatingVisibility -= OnValidatingVisibility;
            Exiled.Events.Handlers.Scp939.PlayingFootstep -= OnPlayingFootstep;
        }
    }
}