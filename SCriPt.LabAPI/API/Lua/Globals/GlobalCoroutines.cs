using System;
using System.Collections.Generic;
using GameCore;
using LabApi.Features.Console;
using MEC;
using MoonSharp.Interpreter;
using SCriPt.LabAPI.API.Lua.Objects;

namespace SCriPt.LabAPI.API.Lua.Globals;

[MoonSharpUserData]
public class GlobalCoroutines
{
    //public static int CoroutineAutoYield => SCriPt.Instance.Config.CoroutineAutoYield;
        //public static Dictionary<string,LuaCoroutines> Instances { get; } = new Dictionary<string, LuaCoroutines>();
        
        public ScriptHandler Owner { get; }
        public List<CoroutineHandle> Handles { get; } = new List<CoroutineHandle>();
        
        public GlobalCoroutines(ScriptHandler owner)
        {
            Owner = owner;
            //Instances.Add(owner.Globals.Get("ScriptName").String,this);
        }
        
        public CoroutineHandle CallDelayed(float delay, Closure closure)
        {
            CoroutineHandle handle = Timing.CallDelayed(delay, () => closure.Call());
            Handles.Add(handle);
            return handle;
        }
        
        public CoroutineHandle CallDelayed(float delay, Closure closure, object[] args)
        {
            CoroutineHandle handle = Timing.CallDelayed(delay, () => closure.Call(args));
            Handles.Add(handle);
            return handle;
        }
        
        private IEnumerator<float> Coroutine(DynValue coroutine)
        {
            DynValue result = null;
            while (true)
            {
                DynValue x = coroutine.Coroutine.Resume();
                //Log.Debug(x.ToString());
                if(x.IsNil() || x.Number == 0)
                    break;
                yield return Timing.WaitForSeconds((float)x.Number);
            }
            Logger.Debug("Coroutine finished.");
            yield return Timing.WaitForOneFrame;
        }

        private IEnumerator<float> Coroutine(DynValue coroutine, object[] args)
        {
            DynValue result = null;
            while (true)
            {
                DynValue x;
                if (coroutine.Coroutine.State == CoroutineState.NotStarted)
                {
                    x = coroutine.Coroutine.Resume(args);
                }
                else
                {
                    x = coroutine.Coroutine.Resume();
                }
                if(x.IsNil() || x.Number == 0)
                    break;
                yield return Timing.WaitForSeconds((float)x.Number);
            }
            Logger.Debug("Coroutine finished.");
            yield return Timing.WaitForOneFrame;
        }

        public CoroutineHandle CallCoroutine(Closure closure)
        {
            if (closure == null)
                throw new NullReferenceException("Closure cannot be null.");
            DynValue coroutine = closure.OwnerScript.CreateCoroutine(closure);
            coroutine.Coroutine.AutoYieldCounter = 0;//CoroutineAutoYield;
            CoroutineHandle handle = Timing.RunCoroutine(Coroutine(coroutine));
            Handles.Add(handle);
            return handle;
        }
        
        public CoroutineHandle CallCoroutine(Closure closure, object[] args)
        {
            if (closure == null)
                throw new NullReferenceException("Closure cannot be null.");
            //Exiled.API.Features.Log.Debug("Calling coroutine with args. "+args.Length+" args.");
            if (args == null || args.Length == 0)
            {
                return CallCoroutine(closure);
            }
            DynValue coroutine = closure.OwnerScript.CreateCoroutine(closure);
            coroutine.Coroutine.AutoYieldCounter = 0;//CoroutineAutoYield;
            CoroutineHandle handle = Timing.RunCoroutine(Coroutine(coroutine, args));
            Handles.Add(handle);
            return handle;
        }

        public CoroutineHandle RunCoroutine(Closure closure)
        {
            return CallCoroutine(closure);
        }
        
        public CoroutineHandle RunCoroutine(Closure closure, object[] args)
        {
            return CallCoroutine(closure, args);
        }
        
        public CoroutineHandle CallContinuously(float timeframe, Closure closure)
        {
            if (closure == null)
                throw new NullReferenceException("Closure cannot be null.");
            CoroutineHandle handle = Timing.CallContinuously(timeframe, () => closure.Call());
            Handles.Add(handle);
            return handle;
        }
        
        public CoroutineHandle CallContinuously(float timeframe, Closure closure, object[] args)
        {
            if (closure == null)
                throw new NullReferenceException("Closure cannot be null.");
            CoroutineHandle handle = Timing.CallContinuously(timeframe, () => closure.Call(args));
            Handles.Add(handle);
            return handle;
        }
        
        public CoroutineHandle CallPeriodically(float timeframe, float period, Closure closure)
        {
            if (closure == null)
                throw new NullReferenceException("Closure cannot be null.");
            CoroutineHandle handle = Timing.CallPeriodically(timeframe, period, () => closure.Call());
            Handles.Add(handle);
            return handle;
        }
        
        public CoroutineHandle CallPeriodically(float timeframe, float period, Closure closure, object[] args)
        {
            if (closure == null)
                throw new NullReferenceException("Closure cannot be null.");
            CoroutineHandle handle = Timing.CallPeriodically(timeframe, period, () => closure.Call(args));
            Handles.Add(handle);
            return handle;
        }
        
        public void Kill(CoroutineHandle handle)
        {
            KillCoroutine(handle);
        }
        
        public void KillCoroutine(CoroutineHandle handle)
        {
            if (Handles.Contains(handle))
                Handles.Remove(handle);
            Timing.KillCoroutines(handle);
        }
        
        
        [MoonSharpHidden]
        public void KillAll()
        {
            foreach (CoroutineHandle handle in Handles)
            {
                Timing.KillCoroutines(handle);
            }
            Handles.Clear();
        }
        
        public float WaitForSeconds(float seconds)
        {
            return Timing.WaitForSeconds(seconds);
        }
        
        public float WaitForOneFrame => Timing.WaitForOneFrame;
}