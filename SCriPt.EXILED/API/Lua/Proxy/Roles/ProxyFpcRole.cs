using System;
using Exiled.API.Features.Roles;
using PlayerRoles.FirstPersonControl;
using RelativePositioning;
using UnityEngine;

namespace SCriPt.EXILED.API.Lua.Proxy.Roles;

public class ProxyFpcRole : ProxyRole
{
    public FpcRole FpcRole => (FpcRole) Role;
    
    
    public ProxyFpcRole(FpcRole role) : base(role)
    {
        
    }
    
    public RelativePosition RelativePosition
    {
        get => FpcRole.RelativePosition;
        set => FpcRole.RelativePosition = value;
    }
    
    public bool RotationDetected
    {
        get => FpcRole.RotationDetected;
        set => FpcRole.RotationDetected = value;
    }
    
    public float WalkingSpeed
    {
        get => FpcRole.WalkingSpeed;
        set => FpcRole.WalkingSpeed = value;
    }
    
    public float SprintingSpeed
    {
        get => FpcRole.SprintingSpeed;
        set => FpcRole.SprintingSpeed = value;
    }
    
    public float JumpingSpeed
    {
        get => FpcRole.JumpingSpeed;
        set => FpcRole.JumpingSpeed = value;
    }
    
    public float CrouchingSpeed
    {
        get => FpcRole.CrouchingSpeed;
        set => FpcRole.CrouchingSpeed = value;
    }
    
    public Vector3 Velocity
    {
        get => FpcRole.Velocity;
        set => FpcRole.Velocity = value;
    }
    
    public bool MovementDetected
    {
        get => FpcRole.MovementDetected;
        set => FpcRole.MovementDetected = value;
    }
    
    public bool CanSendInputs => FpcRole.CanSendInputs;

    public bool IsInvisible
    {
        get => FpcRole.IsInvisible;
        set => FpcRole.IsInvisible = value;
    }
    
    public bool IsUsingStamina
    {
        get => FpcRole.IsUsingStamina;
        set => FpcRole.IsUsingStamina = value;
    }
    
    public float StaminaUsageMultiplier
    {
        get => FpcRole.StaminaUsageMultiplier;
        set => FpcRole.StaminaUsageMultiplier = value;
    }
    
    public float StaminaRegenMultiplier
    {
        get => FpcRole.StaminaRegenMultiplier;
        set => FpcRole.StaminaRegenMultiplier = value;
    }
    
    public PlayerMovementState MoveState
    {
        get => FpcRole.MoveState;
        set => FpcRole.MoveState = value;
    }
    
    public bool IsCrouching => FpcRole.IsCrouching;
    
    public bool IsGrounded => FpcRole.IsGrounded;
    
    public float MovementSpeed => FpcRole.MovementSpeed;
    
    public bool IsInDarkness => FpcRole.IsInDarkness;
    
    public float VerticalRotation => FpcRole.VerticalRotation;
    
    public float HorizontalRotation => FpcRole.HorizontalRotation;
    
    public bool IsAfk => FpcRole.IsAfk;

    public bool IsHumeShieldedRole => FpcRole.IsHumeShieldedRole;
    
    /*public bool IsNoclipEnabled
    {
        get => FpcRole.IsNoclipEnabled;
        set => FpcRole.IsNoclipEnabled = value;
    }*/
    
    public void ResetStamina(bool multipliers = false) => FpcRole.ResetStamina(multipliers);

    public void Move(Vector3 direction)
    {
        if (!FpcRole.FirstPersonController.FpcModule.CharController.SimpleMove(Vector3.Normalize(direction) *
                                                                               (MovementSpeed * Time.deltaTime)))
        {
            FpcRole.FirstPersonController.FpcModule.CharController.Move(Vector3.Normalize(direction) * (MovementSpeed * Time.deltaTime));
        }
    }

    public void LookAt(Vector3 position)
    {
        Vector3 direction = (position+(Vector3.up*0.1f)) - FpcRole.RelativePosition.Position;
        Quaternion quat = Quaternion.LookRotation(direction, Vector3.up);
        FpcMouseLook mouseLook = FpcRole.FirstPersonController.FpcModule.MouseLook;
        (ushort horizontal, ushort vertical) = ToClientUShorts(quat);
        mouseLook.ApplySyncValues(horizontal, vertical);
    }

    public (ushort horizonal, ushort vertical) ToClientUShorts(Quaternion rotation)
    {
        const float ToHorizontal = ushort.MaxValue / 360f;
        const float ToVertical = ushort.MaxValue / 176f;

        float fixVertical = -rotation.eulerAngles.x;

        if (fixVertical < -90f)
        {
            fixVertical += 360f;
        }
        else if (fixVertical > 270f)
        {
            fixVertical -= 360f;
        }

        float horizontal = Mathf.Clamp(rotation.eulerAngles.y, 0f, 360f);
        float vertical = Mathf.Clamp(fixVertical, -88f, 88f) + 88f;

        return ((ushort)Math.Round(horizontal * ToHorizontal), (ushort)Math.Round(vertical * ToVertical));

    }
    
}