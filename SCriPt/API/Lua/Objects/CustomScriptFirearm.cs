using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.EventArgs;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MoonSharp.Interpreter;

namespace SCriPt.API.Lua.Objects;
[MoonSharpUserData]
public sealed class CustomScriptFirearm
    : CustomWeapon
{
    public override uint Id { get; set; }
    public override string Name { get; set; }
    public override string Description { get; set; }
    public override float Weight { get; set; }
    public override SpawnProperties SpawnProperties { get; set; }
    public override float Damage { get; set; }
    
    private Script script;

    private Dictionary<EventType, DynValue> events = new Dictionary<EventType, DynValue>();
    
    public CustomScriptFirearm(Script script, FirearmType type, uint id, string name, string description, float weight, float damage)
    {
        Id = id;
        Type = type.GetItemType();
        Name = name;
        Description = description;
        Weight = weight;
        Damage = damage;
    }
    
    
    public void ScriptRegister()
    {
        try
        {
            var method = typeof(CustomItem).GetMethod("TryRegister");
            method?.Invoke(this, null);
        } catch (System.Exception e)
        {
            Log.Error("Failed to register custom item: "+Name);
            Log.Error(e.Message);
        }
    }

    protected override void OnOwnerDying(OwnerDyingEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnOwnerDying, out DynValue executeOwnerDying))
                script.Call(executeOwnerDying, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnOwnerDying for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnOwnerChangingRole(OwnerChangingRoleEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnOwnerChangingRole, out DynValue executeOwnerChangingRole))
                script.Call(executeOwnerChangingRole, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnOwnerChangingRole for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnOwnerEscaping(OwnerEscapingEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnOwnerEscaping, out DynValue executeOwnerEscaping))
                script.Call(executeOwnerEscaping, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnOwnerEscaping for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnOwnerHandcuffing(OwnerHandcuffingEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnOwnerHandcuffing, out DynValue executeOwnerHandcuffing))
                script.Call(executeOwnerHandcuffing, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnOwnerHandcuffing for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnDropping(DroppingItemEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnDropping, out DynValue executeDropping))
                script.Call(executeDropping, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnDropping for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnPickingUp(PickingUpItemEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnPickingUp, out DynValue executePickingUp))
                script.Call(executePickingUp, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnPickingUp for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnChanging(ChangingItemEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnChanging, out DynValue executeChanging))
                script.Call(executeChanging, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnChanging for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnUpgrading(UpgradingEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnUpgrading, out DynValue executeUpgrading))
                script.Call(executeUpgrading, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnUpgrading for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnUpgrading(UpgradingItemEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnUpgradingItem, out DynValue executeUpgradingItem))
                script.Call(executeUpgradingItem, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnUpgradingItem for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnReloading(ReloadingWeaponEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnReloading, out DynValue executeReloading))
                script.Call(executeReloading, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnReloading for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnShooting(ShootingEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnShooting, out DynValue executeShooting))
                script.Call(executeShooting, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnShooting for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnShot(ShotEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnShot, out DynValue executeShot))
                script.Call(executeShot, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnShot for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    protected override void OnHurting(HurtingEventArgs ev)
    {
        try
        {
            if(this.events.TryGetValue(EventType.OnHurting, out DynValue executeHurting))
                script.Call(executeHurting, ev);
        } catch (System.Exception e)
        {
            Log.Error("Failed to call OnHurting for custom item: "+Name);
            Log.Error(e.Message);
        }
    }
    
    public void AddEvent(EventType eventType, DynValue dynValue)
    {
        events.Add(eventType, dynValue);
    }
    
    public enum EventType
    {
        OnOwnerDying,
        OnOwnerChangingRole,
        OnOwnerEscaping,
        OnOwnerHandcuffing,
        OnDropping,
        OnPickingUp,
        OnChanging,
        OnUpgrading,
        OnUpgradingItem,
        OnReloading,
        OnShooting,
        OnShot,
        OnHurting
    }
}