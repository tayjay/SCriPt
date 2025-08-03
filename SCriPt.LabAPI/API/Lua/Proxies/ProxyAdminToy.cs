using LabApi.Features.Wrappers;
using MoonSharp.Interpreter;
using UnityEngine;

namespace SCriPt.LabAPI.API.Lua.Proxies;

[MoonSharpUserData]
public class ProxyAdminToy
{
    AdminToy AdminToy { get; }
        
    [MoonSharpHidden]
    public ProxyAdminToy(AdminToy adminToy)
    {
        AdminToy = adminToy;
        
    }
    
    public Vector3 Position
    {
        get => AdminToy.Position;
        set => AdminToy.Position = value;
    }
    
    public Quaternion Rotation
    {
        get => AdminToy.Rotation;
        set => AdminToy.Rotation = value;
    }
    
    public Vector3 Scale
    {
        get => AdminToy.Scale;
        set => AdminToy.Scale = value;
    }

    public byte MovementSmoothing
    {
        get => AdminToy.MovementSmoothing;
        set => AdminToy.MovementSmoothing = value;
    }
    
    public bool IsStatic
    {
        get => AdminToy.IsStatic;
        set => AdminToy.IsStatic = value;
    }
    
    public float SyncInterval
    {
        get => AdminToy.SyncInterval;
        set => AdminToy.SyncInterval = value;
    }

    public void Spawn()
    {
        AdminToy.Spawn();
    }
}