using Exiled.API.Enums;
using Exiled.API.Structs;
using LabApi.Features.Wrappers;
using UnityEngine;
using TextToy = LabApi.Features.Wrappers.TextToy;

namespace SCriPt.EXILED.API.Lua.Globals
{
    public class LuaAdminToys
    {
        
        //todo: implement
        
        
        /*Types to add:
         * PrimitiveObject
         * LightSource
         * ShootingTarget
         * Speaker
         * IOnvisibleInteractable
         * Capybara
         * Text
         */

        public static TextToy SpawnText(Vector3 position, Quaternion rotation, string text)
        {
            TextToy textToy = TextToy.Create(position, rotation, networkSpawn:false);
            textToy.TextFormat = text;
            textToy.Spawn();
            return textToy;
        }

        public static LightSourceToy SpawnLightSource(Vector3 position, float intensity = 1f, Color? color = null, float range = 10f)
        {
            LightSourceToy lightSourceToy = LightSourceToy.Create(position, Quaternion.identity, networkSpawn:false);
            lightSourceToy.Intensity = intensity;
            lightSourceToy.Color = color ?? Color.white;
            lightSourceToy.Range = range;
            lightSourceToy.Spawn();
            return lightSourceToy;
        }
        
        public static ShootingTargetToy SpawnShootingTarget(Vector3 position, Quaternion rotation = default, Vector3 scale = default, ShootingTargetType type = ShootingTargetType.ClassD)
        {
            if (rotation == default)
                rotation = Quaternion.identity;
            if (scale == default)
                scale = Vector3.one;

            ShootingTargetToy shootingTargetToy = ShootingTargetToy.Create(position, rotation, scale, networkSpawn:false);
            shootingTargetToy.Spawn();
            return shootingTargetToy;
        }
        
        public static SpeakerToy SpawnSpeaker(Vector3 position, Quaternion rotation = default, Vector3 scale = default)
        {
            if (rotation == default)
                rotation = Quaternion.identity;
            if (scale == default)
                scale = Vector3.one;

            SpeakerToy speakerToy = SpeakerToy.Create(position, rotation, scale, networkSpawn:false);
            speakerToy.Spawn();
            return speakerToy;
        }
        
        public static CapybaraToy SpawnCapybara(Vector3 position, Quaternion rotation = default, Vector3 scale = default)
        {
            if (rotation == default)
                rotation = Quaternion.identity;
            if (scale == default)
                scale = Vector3.one;

            CapybaraToy capybaraToy = CapybaraToy.Create(position, rotation, scale, networkSpawn:false);
            capybaraToy.Spawn();
            return capybaraToy;
        }

        public static PrimitiveObjectToy SpawnPrimitiveObject(PrimitiveType type, Vector3 position, Quaternion rotation = default,
            Vector3 scale = default, Color color = default)
        {
            if(rotation == default)
                rotation = Quaternion.identity;
            if(scale == default)
                scale = Vector3.one;
            if(color == default)
                color = Color.white;
            
            PrimitiveObjectToy primitiveObjectToy = PrimitiveObjectToy.Create(position, rotation, scale, networkSpawn:false);
            primitiveObjectToy.Type = type;
            primitiveObjectToy.Color = color;
            primitiveObjectToy.Spawn();
            return primitiveObjectToy;
        }
        

        /*public static void Create(PrimitiveType type, Vector3 position, Quaternion rotation, Vector3 scale,PrimitiveFlags flags = PrimitiveFlags.None, Color color=default,  bool spawn=true, bool isStatic = false)
        {
            if(color==default)
                color = Color.white;
            PrimitiveSettings settings = new PrimitiveSettings(type, flags,color,position,rotation,scale,isStatic,spawn);
            Primitive p = Primitive.Create(settings);
        }*/
        
        
        
        /*
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
        }*/
        
    }
}