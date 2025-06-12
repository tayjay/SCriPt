using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.Features;
using MEC;
using MoonSharp.Interpreter;
using UnityEngine;

namespace SCriPt.EXILED.API.Lua.Objects;

[MoonSharpUserData]
public class ServerDiagnostics
{

    private double[] _tps = new double[60];
    
    [MoonSharpHidden]
    public bool IsRunning { get; private set; } = false;
    
    public double TpsVariance {
        get
        {
            double average = _tps.Average();
            double sum = 0;
            foreach (double tps in _tps)
            {
                sum += Math.Pow(tps - average, 2);
            }
            return sum / _tps.Length;
        }
    }
    
    
    
    [MoonSharpHidden]
    private IEnumerator<float> Update()
    {
        for(int i = 0; i < 60; i++)
        {
            _tps[i] = Server.Tps;
        }
        while (true)
        {
            yield return Timing.WaitForSeconds(1f);
            // Do stuff
            _tps[Time.frameCount % 60] = Server.Tps;
        }
    }
    [MoonSharpHidden]
    CoroutineHandle _coroutineHandle;
    [MoonSharpHidden]
    public void Start()
    {
        _coroutineHandle = Timing.RunCoroutine(Update());
        IsRunning = true;
    }
    [MoonSharpHidden]
    public void Stop()
    {
        Timing.KillCoroutines(_coroutineHandle);
        IsRunning = false;
    }
}