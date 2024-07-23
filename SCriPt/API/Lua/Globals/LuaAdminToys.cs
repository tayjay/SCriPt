using Exiled.API.Enums;
using Exiled.API.Features.Toys;
using Exiled.API.Structs;
using UnityEngine;

namespace SCriPt.API.Lua.Globals
{
    public class LuaAdminToys
    {
        //todo: implement
        
        /*Methods to add:
            * SpawnToy
         */

        public static void Create(PrimitiveType type, Color color, Vector3 position, Vector3 rotation, Vector3 scale, bool spawn)
        {
            PrimitiveSettings settings = new PrimitiveSettings(type, color, position, rotation, scale, spawn);
            Primitive.Create(settings);
        }
        
        
        
        public static void SpawnCube(Vector3 position, Vector3 rotation, Vector3 scale, Color color = default)
        {
            if(color==default)
                color = Color.white;
            PrimitiveSettings settings = new PrimitiveSettings(PrimitiveType.Cube, color, position, rotation, scale, true);
            Primitive.Create(settings);
        }
        
        public static void ShootingTarget(Vector3 position, Vector3 rotation=default, Vector3 scale=default)
        {
            if(rotation==default)
                rotation = Vector3.zero;
            if(scale==default)
                scale = Vector3.one;
            ShootingTargetToy.Create(ShootingTargetType.ClassD, position, rotation, scale, true);
        }
        
    }
}