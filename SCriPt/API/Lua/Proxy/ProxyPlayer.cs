using System;
using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Hazards;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.Permissions.Extensions;
using MoonSharp.Interpreter;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers;
using PlayerRoles.RoleAssign;
using UnityEngine;

namespace SCriPt.API.Lua.Proxy
{
    public class ProxyPlayer
    {
        Player Player { get; }
        
        [MoonSharpHidden]
        public ProxyPlayer(Player player)
        {
            Player = player;
        }
        
        public string Nickname => Player.Nickname;
        public int Id => Player.Id;
        public string UserId => Player.UserId;
        public string IPAddress => Player.IPAddress;

        public string DisplayNickname
        {
            get => Player.DisplayNickname;
            set => Player.DisplayNickname = value;
        } 
        public uint NetId => Player.NetId;
        
        public float Health
        {
            get => Player.Health;
            set => Player.Health = value;
        }
        
        /// <summary>Gets or sets the player's maximum health.</summary>
        public float MaxHealth
        {
            get => Player.MaxHealth;
            set => Player.MaxHealth = value;
        }
        
        public float ArtificialHealth
        {
            get => Player.ArtificialHealth;
            set => Player.ArtificialHealth = value;
        }
        
        public float MaxArtificialHealth
        {
            get => Player.MaxArtificialHealth;
            set => Player.MaxArtificialHealth = value;
        }
        
        public float HumeShield
        {
            get => Player.HumeShield;
            set => Player.HumeShield = value;
        }

        public TantrumHazard PlaceTantrum(bool isActive = true)
        {
            return Player.PlaceTantrum(isActive);
        }

        public Room Room => Player.CurrentRoom;
        public ZoneType Zone => Player.Zone;
        public Lift Lift => Player.Lift;
        
        public List<StatusEffectBase> StatusEffects => Player.ActiveEffects.ToList();
        
        public void EnableEffect(EffectType effectType, int intensity=1, float duration=0)
        {
            if (effectType == EffectType.None)
                return;
            if (intensity < 0 || intensity > 5)
                throw new ArgumentOutOfRangeException(nameof(intensity));
            Log.Debug("SCriPt giving effect: "+effectType+" intensity: "+intensity+" duration: "+duration);
            Player.EnableEffect(effectType, (byte)intensity, duration);
        }
        
        public void DisableEffect(EffectType effectType)
        {
            if (effectType == EffectType.None)
                return;
            Player.DisableEffect(effectType);
        }
        
        public void DisableAllEffects()
        {
            Player.DisableAllEffects();
        }
        
        public bool IsEffectActive(EffectType effectType)
        {
            if (effectType == EffectType.None)
                return false;
            return Player.GetEffect(effectType).Intensity > 0;
        }
        
        public bool IsInPocketDimension => Player.IsInPocketDimension;

        public bool IsInventoryEmpty => Player.IsInventoryEmpty;
        public bool IsInventoryFull => Player.IsInventoryFull;
        
        public List<Player> CurrentSpectatingPlayers => Player.CurrentSpectatingPlayers.ToList();
        
        public bool IsSpawnProtected
        {
            get => Player.IsSpawnProtected;
            set => Player.IsSpawnProtected = value;
        }
        
        public bool GodMode
        {
            get => Player.IsGodModeEnabled;
            set => Player.IsGodModeEnabled = value;
        }
        
        public bool IsNoclipPermitted
        {
            get => Player.IsNoclipPermitted;
            set => Player.IsNoclipPermitted = value;
        }
        
        public bool IsNoClipActive
        {
            get => (Player.Role is FpcRole role) && role.FirstPersonController.FpcModule.Noclip.IsActive;
            set => ((FpcRole)Player.Role).FirstPersonController.FpcModule.Noclip.IsActive = value;
        }

        public Role Role
        {
            get => Player.Role;
        }
        
        public RoleTypeId RoleTypeId => Player.Role.Type;
        
        public Transform CameraTransform => Player.CameraTransform;
        
        public string AuthenticationType => Player.AuthenticationType.ToString();
        
        public Player Cuffer => Player.Cuffer;
        
        public void Kill()
        {
            Player.Kill(DamageType.Custom);
        }
        
        public void Hurt(float damage, string damageReason = "Admin")
        {
            Player.Hurt(damage, damageReason);
        }
        
        public void Heal(float amount, bool overrideMax = false)
        {
            Player.Heal(amount, overrideMax);
        }

        public void SetRole(string role, int flag)
        {
            if(Enum.TryParse(role, out RoleTypeId roleTypeId) && Enum.IsDefined(typeof(RoleSpawnFlags),flag))
                Player.RoleManager.ServerSetRole(roleTypeId, RoleChangeReason.RemoteAdmin, (RoleSpawnFlags)flag);
        }
        
        public void SetRole(string role)
        {
            SetRole(role,(int)RoleSpawnFlags.All);
        }
        
        public void SetRole(RoleTypeId roleTypeId, int flag)
        {
            Player.RoleManager.ServerSetRole(roleTypeId, RoleChangeReason.RemoteAdmin, (RoleSpawnFlags)flag);
        }
        
        public void SetRole(RoleTypeId roleTypeId, RoleSpawnFlags flag=RoleSpawnFlags.All)
        {
            Player.RoleManager.ServerSetRole(roleTypeId, RoleChangeReason.RemoteAdmin, flag);
        }
        
        
        
        public void ShowHint(string message, float duration=3f)
        {
            Player.ShowHint(message, duration);
        }
        
        public void Broadcast(string message, ushort duration=5)
        {
            if (duration <= 0) throw new ArgumentOutOfRangeException(nameof(duration));
            Player.Broadcast(duration, message);
        }
        
        public bool CheckPermission(string permission) => Player.CheckPermission(permission);
        
        public void Handcuff() => Player.Handcuff();
        
        public void Teleport(Vector3 position) => Player.Teleport(position);

        public bool IsJumping
        {
            get => Player.IsJumping;
            set
            {
                var role = (Player.Role as FpcRole);
                if (role != null) role.FirstPersonController.FpcModule.Motor.WantsToJump = value;
            }
        }
        
        public float Stamina
        {
            get => Player.Stamina;
            set => Player.Stamina = value;
        }

        public Vector3 Position
        {
            get => Player.Position;
            set => Player.Position = value;
        }
        
        public float PosX
        {
            get => Player.Position.x;
            set => Player.Position = new Vector3(value,Player.Position.y,Player.Position.z);
        }
        
        public float PosY
        {
            get => Player.Position.y;
            set => Player.Position = new Vector3(Player.Position.x,value,Player.Position.z);
        }
        
        public float PosZ
        {
            get => Player.Position.z;
            set => Player.Position = new Vector3(Player.Position.x,Player.Position.y,value);
        }
        
        public Vector3 Scale
        {
            get => Player.Scale;
            set => Player.Scale = value;
        }
        
        
        public float ScaleX
        {
            get => Player.Scale.x;
            set => Player.Scale = new Vector3(value,Player.Scale.y,Player.Scale.z);
        }
        
        public float ScaleY
        {
            get => Player.Scale.y;
            set => Player.Scale = new Vector3(Player.Scale.x,value,Player.Scale.z);
        }
        
        public float ScaleZ
        {
            get => Player.Scale.z;
            set => Player.Scale = new Vector3(Player.Scale.x,Player.Scale.y,value);
        }

        public Item CurrentItem => Player.CurrentItem;
        
        public void GiveItem(ItemType type)
        {
            Player.AddItem(type);
        }

        public void GiveItem(string type)
        {
            if(Enum.TryParse(type, out ItemType itemType))
                Player.AddItem(itemType);
        }
        
        public bool ReloadWeapon()
        {
            return Player.ReloadWeapon();
        }
        
        public bool UseCurrentItem()
        {
            return Player.UseItem(Player.CurrentItem);
        }

        public bool UseItem(Item item)
        {
            return Player.UseItem(item);
        }
        
        public void Broadcast(string message)
        {
            Player.Broadcast(5, message);
        }

        public void RemoteAdminMessage(string message, bool success = true, string pluginName = null)
        {
            Player.RemoteAdminMessage(message, success, pluginName);
        }
        
        public bool SendStaffMessage(string message, EncryptedChannelManager.EncryptedChannel channel = EncryptedChannelManager.EncryptedChannel.AdminChat)
        {
            return Player.SendStaffMessage(message, channel);
        }
        
        public bool SendStaffPing(string message, EncryptedChannelManager.EncryptedChannel channel = EncryptedChannelManager.EncryptedChannel.AdminChat)
        {
            return Player.SendStaffPing(message, channel);
        }
        
        public void DropHeldItem(bool isThrown = false)
        {
            Player.DropHeldItem(isThrown);
        }
        
        public Item AddItem(ItemType itemType)
        {
            return Player.AddItem(itemType);
        }
        
        
        public void ChangeAppearance(RoleTypeId roleType)
        {
            Player.ChangeAppearance(roleType);
        }
        
        public void ChangeAppearance(string roleType)
        {
            if(Enum.TryParse(roleType, out RoleTypeId roleTypeId))
                Player.ChangeAppearance(roleTypeId);
        }
        
        public bool IsAlive => Player.IsAlive;
        public bool IsDead => Player.IsDead;
        public bool IsScp => Player.IsScp;
        public bool IsHuman => Player.IsHuman;
        public bool IsNTF => Player.IsNTF;
        public bool IsCHI => Player.IsCHI;
        
        public bool IsClassD => Player.Role == RoleTypeId.ClassD;
        public bool IsScientist => Player.Role == RoleTypeId.Scientist;
        public bool IsFacilityGuard => Player.Role == RoleTypeId.FacilityGuard;
        
        public void MoveToward(Vector3 position)
        {
            if (Player.Role is FpcRole role)
            {
                Vector3 currentPos = Player.Position;
                Vector3 direction = (position - currentPos).normalized;
                role.FirstPersonController.FpcModule.CharController.Move(direction);
            }
        }
        
        public void LookAtPoint(Vector3 point, float lerp = 1f)
        {
            if (Player.Role is FpcRole role)
            {
                role.FirstPersonController.LookAtPoint(point,lerp);
            }
        }
        
        public void LookAtDirection(Vector3 direction, float lerp = 1f)
        {
            if (Player.Role is FpcRole role)
            {
                role.FirstPersonController.LookAtDirection(direction,lerp);
            }
        }
        
        public int ScpTickets
        {
            get
            {
                using(ScpTicketsLoader loader = new ScpTicketsLoader())
                {
                    return loader.GetTickets(Player.ReferenceHub,10);
                }
            }
            set
            {
                using(ScpTicketsLoader loader = new ScpTicketsLoader())
                {
                    loader.ModifyTickets(Player.ReferenceHub, value);
                }
            }
        }
        
        public bool DoNotTrack { get => Player.DoNotTrack; }
        
        public void SendConsoleMessage(string message)
        {
            Player.SendConsoleMessage(message, "green");
        }
        
        public bool UseHeldItem()
        {
            return Player.UseItem(Player.CurrentItem);
        }

        public bool UseItem(ItemType type)
        {
            return Player.UseItem(type);
        } 
        
        public bool HasItem(ItemType type)
        {
            return Player.HasItem(type);
        }
        
        /*public float MoveSpeed
        {
            get
            {
                if (Player.Role is FpcRole role)
                {
                    return role.FirstPersonController.FpcModule.Motor.Speed;
                }
                return 0f;
            }
        }*/
        
        public PlayerMovementState MovementState
        {
            get
            {
                if (Player.Role is FpcRole role)
                {
                    return role.FirstPersonController.FpcModule.CurrentMovementState;
                }
                return PlayerMovementState.Walking;
            }
            
            set
            {
                if (Player.Role is FpcRole role)
                {
                    role.FirstPersonController.FpcModule.CurrentMovementState = value;
                }
            }
        }
        
        /*public bool EnableFallDamage
        {
            get
            {
                if (Player.Role is FpcRole role)
                {
                    return role.FirstPersonController.FpcModule.Motor._enableFallDamage;
                }
                return false;
            }
            set
            {
                if (Player.Role is FpcRole role)
                {
                    try
                    {
                        var property = typeof(FpcMotor).GetProperty("_enableFallDamage");
                        property?.SetValue(role.FirstPersonController.FpcModule.Motor, value);
                    } catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }
        }*/


        public Dictionary<string, object> SessionVariables => Player.SessionVariables;


        public EmotionPresetType Emotion
        {
            get => Player.Emotion;
            set => Player.Emotion = value;
        }

    }
}