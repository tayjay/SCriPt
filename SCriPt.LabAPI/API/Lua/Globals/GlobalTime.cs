using System;
using LabApi.Features.Wrappers;

namespace SCriPt.LabAPI.API.Lua.Globals;

public class GlobalTime
{
    public long ServerTimeUtc
    {
        get => System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
    public long ServerTime
    {
        get => System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    public double RoundTime
    {
        get => Round.Duration.TotalMilliseconds;
    }
    
    public DateTime ServerDateTimeUtc
    {
        get => System.DateTimeOffset.UtcNow.DateTime;
    }
    
    public DateTime ServerDateTime
    {
        get => System.DateTimeOffset.Now.DateTime;
    }
    
    public TimeSpan RoundTimeSpan
    {
        get => Round.Duration;
    }
    
    public TimeSpan MillisecondsToTimeSpan(long milliseconds)
    {
        return TimeSpan.FromMilliseconds(milliseconds);
    }
    
    public TimeSpan SecondsToTimeSpan(double seconds)
    {
        return TimeSpan.FromSeconds(seconds);
    }
    
    
}