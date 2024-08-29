using System.Reflection;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Pickups;
using MoonSharp.Interpreter;
using PlayerRoles.FirstPersonControl;
using UnityEngine;

namespace SCriPt.API.Lua.Globals
{
    [MoonSharpUserData]
    public class LuaFacility
    {
        public static void TurnOffLights(float time)
        {
            Map.TurnOffAllLights(time);
        }
        
        public static void LockAllDoors(float duration)
        {
            Door.LockAll(duration,DoorLockType.AdminCommand);
        }
        
        public static void UnlockAllDoors()
        {
            Door.UnlockAll();
        }

        public static void SpawnPickup(ItemType item, Vector3 position, Quaternion rotation = default)
        {
            if(rotation==default)
                rotation = Quaternion.identity;
            Pickup.CreateAndSpawn(item,position,rotation);
        }
        
        public static int Seed
        {
            get => Map.Seed;
            set => Map.Seed = value;
        }
        
        /*public static Vector3 Gravity
        {
            get => FpcMotor.Gravity;
            set
            {
                var Gravity = typeof(FpcMotor).GetField("Gravity", BindingFlags.Static | BindingFlags.NonPublic);
                Gravity?.SetValue(null, value);
            }
        }*/
        
        public static Room GetRoom(Vector3 position)
        {
            return Room.Get(position);
        }
        
        
    }
}