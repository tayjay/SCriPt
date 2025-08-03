using AdminToys;
using LabApi.Features.Wrappers;
using MoonSharp.Interpreter;
using UnityEngine;
using CapybaraToy = LabApi.Features.Wrappers.CapybaraToy;
using LightSourceToy = LabApi.Features.Wrappers.LightSourceToy;
using PrimitiveObjectToy = LabApi.Features.Wrappers.PrimitiveObjectToy;
using SpeakerToy = LabApi.Features.Wrappers.SpeakerToy;
using TextToy = LabApi.Features.Wrappers.TextToy;

namespace SCriPt.LabAPI.API.Lua.Globals;

[MoonSharpUserData]
public class GlobalAdminToys
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
        
        public static ShootingTargetToy SpawnShootingTarget(Vector3 position, Quaternion rotation = default, Vector3 scale = default)
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
        
        public static CapybaraToy SpawnCapybara(Vector3 position, Quaternion rotation = default, Vector3 scale = default, bool collidable = true)
        {
            if (rotation == default)
                rotation = Quaternion.identity;
            if (scale == default)
                scale = Vector3.one;

            CapybaraToy capybaraToy = CapybaraToy.Create(position, rotation, scale, networkSpawn:false);
            capybaraToy.CollidersEnabled = collidable;
            capybaraToy.Spawn();
            return capybaraToy;
        }

        public static PrimitiveObjectToy SpawnPrimitiveObject(PrimitiveType type, Vector3 position, Quaternion rotation = default,
            Vector3 scale = default, Color color = default, bool collidable = true)
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
            primitiveObjectToy.Flags = collidable ? primitiveObjectToy.Flags | PrimitiveFlags.Collidable : primitiveObjectToy.Flags & ~PrimitiveFlags.Collidable;
            primitiveObjectToy.Spawn();
            return primitiveObjectToy;
        }
        
}