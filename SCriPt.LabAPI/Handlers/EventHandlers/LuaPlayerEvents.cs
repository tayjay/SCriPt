using System;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaPlayerEvents : ILuaEventHandler
{
    [MoonSharpVisible(true)] public event EventHandler<PlayerJoinedEventArgs> Joined;

    [MoonSharpVisible(true)] public event EventHandler<PlayerLeftEventArgs> Left;

    [MoonSharpVisible(true)] public event EventHandler<PlayerReceivingVoiceMessageEventArgs> ReceivingVoiceMessage;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSendingVoiceMessageEventArgs> SendingVoiceMessage;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPreAuthenticatingEventArgs> PreAuthenticating;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPreAuthenticatedEventArgs> PreAuthenticated;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUsingIntercomEventArgs> UsingIntercom;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUsedIntercomEventArgs> UsedIntercom;

    [MoonSharpVisible(true)] public event EventHandler<PlayerBanningEventArgs> Banning;

    [MoonSharpVisible(true)] public event EventHandler<PlayerBannedEventArgs> Banned;

    [MoonSharpVisible(true)] public event EventHandler<PlayerKickingEventArgs> Kicking;

    [MoonSharpVisible(true)] public event EventHandler<PlayerKickedEventArgs> Kicked;

    [MoonSharpVisible(true)] public event EventHandler<PlayerMutingEventArgs> Muting;

    [MoonSharpVisible(true)] public event EventHandler<PlayerMutedEventArgs> Muted;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUnmutingEventArgs> Unmuting;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUnmutedEventArgs> Unmuted;

    [MoonSharpVisible(true)] public event EventHandler<PlayerReportingCheaterEventArgs> ReportingCheater;

    [MoonSharpVisible(true)] public event EventHandler<PlayerReportedCheaterEventArgs> ReportedCheater;

    [MoonSharpVisible(true)] public event EventHandler<PlayerReportingPlayerEventArgs> ReportingPlayer;

    [MoonSharpVisible(true)] public event EventHandler<PlayerReportedPlayerEventArgs> ReportedPlayer;

    [MoonSharpVisible(true)] public event EventHandler<PlayerTogglingNoclipEventArgs> TogglingNoclip;

    [MoonSharpVisible(true)] public event EventHandler<PlayerToggledNoclipEventArgs> ToggledNoclip;

    [MoonSharpVisible(true)] public event EventHandler<PlayerChangingNicknameEventArgs> ChangingNickname;

    [MoonSharpVisible(true)] public event EventHandler<PlayerChangedNicknameEventArgs> ChangedNickname;

    [MoonSharpVisible(true)] public event EventHandler<PlayerGroupChangingEventArgs> GroupChanging;

    [MoonSharpVisible(true)] public event EventHandler<PlayerGroupChangedEventArgs> GroupChanged;

    [MoonSharpVisible(true)] public event EventHandler<PlayerEffectUpdatingEventArgs> UpdatingEffect;

    [MoonSharpVisible(true)] public event EventHandler<PlayerEffectUpdatedEventArgs> UpdatedEffect;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDyingEventArgs> Dying;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDeathEventArgs> Death;

    [MoonSharpVisible(true)] public event EventHandler<PlayerHurtingEventArgs> Hurting;

    [MoonSharpVisible(true)] public event EventHandler<PlayerHurtEventArgs> Hurt;

    [MoonSharpVisible(true)] public event EventHandler<PlayerChangingRoleEventArgs> ChangingRole;

    [MoonSharpVisible(true)] public event EventHandler<PlayerChangedRoleEventArgs> ChangedRole;

    [MoonSharpVisible(true)] public event EventHandler<PlayerCuffingEventArgs> Cuffing;

    [MoonSharpVisible(true)] public event EventHandler<PlayerCuffedEventArgs> Cuffed;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUncuffingEventArgs> Uncuffing;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUncuffedEventArgs> Uncuffed;

    [MoonSharpVisible(true)] public event EventHandler<PlayerReceivingLoadoutEventArgs> ReceivingLoadout;

    [MoonSharpVisible(true)] public event EventHandler<PlayerReceivedLoadoutEventArgs> ReceivedLoadout;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSpawningEventArgs> Spawning;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSpawnedEventArgs> Spawned;

    [MoonSharpVisible(true)] public event EventHandler<PlayerChangingItemEventArgs> ChangingItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerChangedItemEventArgs> ChangedItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDroppingAmmoEventArgs> DroppingAmmo;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDroppedAmmoEventArgs> DroppedAmmo;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDroppingItemEventArgs> DroppingItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDroppedItemEventArgs> DroppedItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPickingUpAmmoEventArgs> PickingUpAmmo;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPickedUpAmmoEventArgs> PickedUpAmmo;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPickingUpArmorEventArgs> PickingUpArmor;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPickedUpArmorEventArgs> PickedUpArmor;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPickingUpItemEventArgs> PickingUpItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPickedUpItemEventArgs> PickedUpItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPickingUpScp330EventArgs> PickingUpScp330;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPickedUpScp330EventArgs> PickedUpScp330;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSearchedAmmoEventArgs> SearchedAmmo;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSearchingArmorEventArgs> SearchingArmor;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSearchedArmorEventArgs> SearchedArmor;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSearchingPickupEventArgs> SearchingPickup;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractedToyEventArgs> InteractedToy;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSearchedPickupEventArgs> SearchedPickup;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSearchingAmmoEventArgs> SearchingAmmo;

    [MoonSharpVisible(true)] public event EventHandler<PlayerThrowingItemEventArgs> ThrowingItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerThrewItemEventArgs> ThrewItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerThrowingProjectileEventArgs> ThrowingProjectile;

    [MoonSharpVisible(true)] public event EventHandler<PlayerThrewProjectileEventArgs> ThrewProjectile;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUsingItemEventArgs> UsingItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUsedItemEventArgs> UsedItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUsingRadioEventArgs> UsingRadio;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUsedRadioEventArgs> UsedRadio;

    [MoonSharpVisible(true)] public event EventHandler<PlayerAimedWeaponEventArgs> AimedWeapon;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDryFiringWeaponEventArgs> DryFiringWeapon;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDryFiredWeaponEventArgs> DryFiredWeapon;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUnloadingWeaponEventArgs> UnloadingWeapon;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUnloadedWeaponEventArgs> UnloadedWeapon;

    [MoonSharpVisible(true)] public event EventHandler<PlayerReloadingWeaponEventArgs> ReloadingWeapon;

    [MoonSharpVisible(true)] public event EventHandler<PlayerReloadedWeaponEventArgs> ReloadedWeapon;

    [MoonSharpVisible(true)] public event EventHandler<PlayerShootingWeaponEventArgs> ShootingWeapon;

    [MoonSharpVisible(true)] public event EventHandler<PlayerShotWeaponEventArgs> ShotWeapon;

    [MoonSharpVisible(true)] public event EventHandler<PlayerCancellingUsingItemEventArgs> CancellingUsingItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerCancelledUsingItemEventArgs> CancelledUsingItem;

    [MoonSharpVisible(true)] public event EventHandler<PlayerChangingRadioRangeEventArgs> ChangingRadioRange;

    [MoonSharpVisible(true)] public event EventHandler<PlayerChangedRadioRangeEventArgs> ChangedRadioRange;

    [MoonSharpVisible(true)] public event EventHandler<PlayerTogglingFlashlightEventArgs> TogglingFlashlight;

    [MoonSharpVisible(true)] public event EventHandler<PlayerToggledFlashlightEventArgs> ToggledFlashlight;

    [MoonSharpVisible(true)] public event EventHandler<PlayerTogglingWeaponFlashlightEventArgs> TogglingWeaponFlashlight;

    [MoonSharpVisible(true)] public event EventHandler<PlayerToggledWeaponFlashlightEventArgs> ToggledWeaponFlashlight;

    [MoonSharpVisible(true)] public event EventHandler<PlayerTogglingRadioEventArgs> TogglingRadio;

    [MoonSharpVisible(true)] public event EventHandler<PlayerToggledRadioEventArgs> ToggledRadio;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDamagingShootingTargetEventArgs> DamagingShootingTarget;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDamagedShootingTargetEventArgs> DamagedShootingTarget;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDamagingWindowEventArgs> DamagingWindow;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDamagedWindowEventArgs> DamagedWindow;

    [MoonSharpVisible(true)] public event EventHandler<PlayerEnteringPocketDimensionEventArgs> EnteringPocketDimension;

    [MoonSharpVisible(true)] public event EventHandler<PlayerEnteredPocketDimensionEventArgs> EnteredPocketDimension;

    [MoonSharpVisible(true)] public event EventHandler<PlayerLeavingPocketDimensionEventArgs> LeavingPocketDimension;

    [MoonSharpVisible(true)] public event EventHandler<PlayerLeftPocketDimensionEventArgs> LeftPocketDimension;

    [MoonSharpVisible(true)] public event EventHandler<PlayerTriggeringTeslaEventArgs> TriggeringTesla;

    [MoonSharpVisible(true)] public event EventHandler<PlayerTriggeredTeslaEventArgs> TriggeredTesla;

    [MoonSharpVisible(true)] public event EventHandler<PlayerEscapingEventArgs> Escaping;

    [MoonSharpVisible(true)] public event EventHandler<PlayerEscapedEventArgs> Escaped;

    [MoonSharpVisible(true)] public event EventHandler<PlayerFlippingCoinEventArgs> FlippingCoin;

    [MoonSharpVisible(true)] public event EventHandler<PlayerFlippedCoinEventArgs> FlippedCoin;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSearchingToyEventArgs> SearchingToy;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSearchedToyEventArgs> SearchedToy;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSearchToyAbortedEventArgs> SearchToyAborted;

    [MoonSharpVisible(true)] public event EventHandler<PlayerIdlingTeslaEventArgs> IdlingTesla;

    [MoonSharpVisible(true)] public event EventHandler<PlayerIdledTeslaEventArgs> IdledTesla;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractingDoorEventArgs> InteractingDoor;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractedDoorEventArgs> InteractedDoor;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractingElevatorEventArgs> InteractingElevator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractedElevatorEventArgs> InteractedElevator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractingGeneratorEventArgs> InteractingGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractedGeneratorEventArgs> InteractedGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerOpeningGeneratorEventArgs> OpeningGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerOpenedGeneratorEventArgs> OpenedGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerActivatingGeneratorEventArgs> ActivatingGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerActivatedGeneratorEventArgs> ActivatedGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDeactivatingGeneratorEventArgs> DeactivatingGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerDeactivatedGeneratorEventArgs> DeactivatedGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUnlockingGeneratorEventArgs> UnlockingGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUnlockedGeneratorEventArgs> UnlockedGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerClosingGeneratorEventArgs> ClosingGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerClosedGeneratorEventArgs> ClosedGenerator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractingLockerEventArgs> InteractingLocker;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractedLockerEventArgs> InteractedLocker;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractingScp330EventArgs> InteractingScp330;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractedScp330EventArgs> InteractedScp330;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractingShootingTargetEventArgs> InteractingShootingTarget;

    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractedShootingTargetEventArgs> InteractedShootingTarget;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPlacingBloodEventArgs> PlacingBlood;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPlacedBloodEventArgs> PlacedBlood;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPlacingBulletHoleEventArgs> PlacingBulletHole;

    [MoonSharpVisible(true)] public event EventHandler<PlayerPlacedBulletHoleEventArgs> PlacedBulletHole;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSpawningRagdollEventArgs> SpawningRagdoll;

    [MoonSharpVisible(true)] public event EventHandler<PlayerSpawnedRagdollEventArgs> SpawnedRagdoll;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUnlockingWarheadButtonEventArgs> UnlockingWarheadButton;

    [MoonSharpVisible(true)] public event EventHandler<PlayerUnlockedWarheadButtonEventArgs> UnlockedWarheadButton;

    [MoonSharpVisible(true)] public event EventHandler<PlayerChangedSpectatorEventArgs> ChangedSpectator;

    [MoonSharpVisible(true)] public event EventHandler<PlayerEnteringHazardEventArgs> EnteringHazard;

    [MoonSharpVisible(true)] public event EventHandler<PlayerEnteredHazardEventArgs> EnteredHazard;

    [MoonSharpVisible(true)] public event EventHandler<PlayersStayingInHazardEventArgs> StayingInHazard;

    [MoonSharpVisible(true)] public event EventHandler<PlayerLeavingHazardEventArgs> LeavingHazard;

    [MoonSharpVisible(true)] public event EventHandler<PlayerLeftHazardEventArgs> LeftHazard;

    [MoonSharpVisible(true)] public event EventHandler<PlayerValidatedVisibilityEventArgs> ValidatedVisibility;
    
    [MoonSharpVisible(true)] public event EventHandler<PlayerJumpedEventArgs> Jumped;
    //MovementStateChanged
    [MoonSharpVisible(true)] public event EventHandler<PlayerMovementStateChangedEventArgs> MovementStateChanged;
    // ChangingAttachments
    [MoonSharpVisible(true)] public event EventHandler<PlayerChangingAttachmentsEventArgs> ChangingAttachments;
    //ChangedAttachments
    [MoonSharpVisible(true)] public event EventHandler<PlayerChangedAttachmentsEventArgs> ChangedAttachments;
    //SendingAttachmentsPrefs
    [MoonSharpVisible(true)] public event EventHandler<PlayerSendingAttachmentsPrefsEventArgs> SendingAttachmentsPrefs;
    //SentAttachmentsPrefs
    [MoonSharpVisible(true)] public event EventHandler<PlayerSentAttachmentsPrefsEventArgs> SentAttachmentsPrefs;
    //InteractingWarheadLever
    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractingWarheadLeverEventArgs> InteractingWarheadLever;
    //InteractedWarheadLever
    [MoonSharpVisible(true)] public event EventHandler<PlayerInteractedWarheadLeverEventArgs> InteractedWarheadLever;
    //PlayerSpinningRevolver
    [MoonSharpVisible(true)] public event EventHandler<PlayerSpinningRevolverEventArgs> SpinningRevolver;
    //PlayerSpunRevolver
    [MoonSharpVisible(true)] public event EventHandler<PlayerSpinnedRevolverEventArgs> SpinnedRevolver;
    //PlayerToggggledDisruptorMode
    [MoonSharpVisible(true)] public event EventHandler<PlayerToggledDisruptorFiringModeEventArgs> ToggledDisruptorFiringMode;
    //PlayerChangedBadgeVisibility
    [MoonSharpVisible(true)] public event EventHandler<PlayerChangedBadgeVisibilityEventArgs> ChangedBadgeVisibility;
    //PlayerChangingBadgeVisibility
    [MoonSharpVisible(true)] public event EventHandler<PlayerChangingBadgeVisibilityEventArgs> ChangingBadgeVisibility;
    //PlayerProcessingJailbirdMessage
    [MoonSharpVisible(true)] public event EventHandler<PlayerProcessingJailbirdMessageEventArgs> ProcessingJailbirdMessage;
    //PlayerProcessedJailbirdMessage
    [MoonSharpVisible(true)] public event EventHandler<PlayerProcessedJailbirdMessageEventArgs> ProcessedJailbirdMessage;
    //PlayerInspectingKeycard
    [MoonSharpVisible(true)] public event EventHandler<PlayerInspectingKeycardEventArgs> InspectingKeycard;
    //PlayerInspectedKeycard
    [MoonSharpVisible(true)] public event EventHandler<PlayerInspectedKeycardEventArgs> InspectedKeycard;
    //RoomChanged
    [MoonSharpVisible(true)] public event EventHandler<PlayerRoomChangedEventArgs> RoomChanged;
    //ZoneChanged
    [MoonSharpVisible(true)] public event EventHandler<PlayerZoneChangedEventArgs> ZoneChanged;
    //PlayerRaPlayerListAddedPlayer
    [MoonSharpVisible(true)] public event EventHandler<PlayerRaPlayerListAddedPlayerEventArgs> RaPlayerListAddedPlayer;
    //PlayerRaPlayerListAddingPlayer
    [MoonSharpVisible(true)] public event EventHandler<PlayerRaPlayerListAddingPlayerEventArgs> RaPlayerListAddingPlayer;
    //PlayerReceivedAchievement
    [MoonSharpVisible(true)] public event EventHandler<PlayerReceivedAchievementEventArgs> ReceivedAchievement;
    //PlayerRequestedRaPlayerInfo
    [MoonSharpVisible(true)] public event EventHandler<PlayerRequestedRaPlayerInfoEventArgs> RequestedRaPlayerInfo;
    //PlayerRequestingRaPlayerInfo
    [MoonSharpVisible(true)] public event EventHandler<PlayerRequestingRaPlayerInfoEventArgs> RequestingRaPlayerInfo;
    //PlayerRequestedCustomRaInfo
    [MoonSharpVisible(true)] public event EventHandler<PlayerRequestedCustomRaInfoEventArgs> RequestedCustomRaInfo;
    //PlayerRequestedRaPlayerList
    [MoonSharpVisible(true)] public event EventHandler<PlayerRequestedRaPlayerListEventArgs> RequestedRaPlayerList;
    //PlayerRequestingRaPlayerList
    [MoonSharpVisible(true)] public event EventHandler<PlayerRequestingRaPlayerListEventArgs> RequestingRaPlayerList;
    //PlayerRequestedRaPlayersInfo
    [MoonSharpVisible(true)] public event EventHandler<PlayerRequestedRaPlayersInfoEventArgs> RequestedRaPlayersInfo;
    //PlayerRequestingRaPlayersInfo
    [MoonSharpVisible(true)] public event EventHandler<PlayerRequestingRaPlayersInfoEventArgs> RequestingRaPlayersInfo;
    
    
    
    [MoonSharpHidden]
public void OnJoined(PlayerJoinedEventArgs ev) => Joined?.Invoke(null, ev);
[MoonSharpHidden]
public void OnLeft(PlayerLeftEventArgs ev) => Left?.Invoke(null, ev);
[MoonSharpHidden]
public void OnReceivingVoiceMessage(PlayerReceivingVoiceMessageEventArgs ev) => ReceivingVoiceMessage?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSendingVoiceMessage(PlayerSendingVoiceMessageEventArgs ev) => SendingVoiceMessage?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPreAuthenticating(PlayerPreAuthenticatingEventArgs ev) => PreAuthenticating?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPreAuthenticated(PlayerPreAuthenticatedEventArgs ev) => PreAuthenticated?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUsingIntercom(PlayerUsingIntercomEventArgs ev) => UsingIntercom?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUsedIntercom(PlayerUsedIntercomEventArgs ev) => UsedIntercom?.Invoke(null, ev);
[MoonSharpHidden]
public void OnBanning(PlayerBanningEventArgs ev) => Banning?.Invoke(null, ev);
[MoonSharpHidden]
public void OnBanned(PlayerBannedEventArgs ev) => Banned?.Invoke(null, ev);
[MoonSharpHidden]
public void OnKicking(PlayerKickingEventArgs ev) => Kicking?.Invoke(null, ev);
[MoonSharpHidden]
public void OnKicked(PlayerKickedEventArgs ev) => Kicked?.Invoke(null, ev);
[MoonSharpHidden]
public void OnMuting(PlayerMutingEventArgs ev) => Muting?.Invoke(null, ev);
[MoonSharpHidden]
public void OnMuted(PlayerMutedEventArgs ev) => Muted?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUnmuting(PlayerUnmutingEventArgs ev) => Unmuting?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUnmuted(PlayerUnmutedEventArgs ev) => Unmuted?.Invoke(null, ev);
[MoonSharpHidden]
public void OnReportingCheater(PlayerReportingCheaterEventArgs ev) => ReportingCheater?.Invoke(null, ev);
[MoonSharpHidden]
public void OnReportedCheater(PlayerReportedCheaterEventArgs ev) => ReportedCheater?.Invoke(null, ev);
[MoonSharpHidden]
public void OnReportingPlayer(PlayerReportingPlayerEventArgs ev) => ReportingPlayer?.Invoke(null, ev);
[MoonSharpHidden]
public void OnReportedPlayer(PlayerReportedPlayerEventArgs ev) => ReportedPlayer?.Invoke(null, ev);
[MoonSharpHidden]
public void OnTogglingNoclip(PlayerTogglingNoclipEventArgs ev) => TogglingNoclip?.Invoke(null, ev);
[MoonSharpHidden]
public void OnToggledNoclip(PlayerToggledNoclipEventArgs ev) => ToggledNoclip?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangingNickname(PlayerChangingNicknameEventArgs ev) => ChangingNickname?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangedNickname(PlayerChangedNicknameEventArgs ev) => ChangedNickname?.Invoke(null, ev);
[MoonSharpHidden]
public void OnGroupChanging(PlayerGroupChangingEventArgs ev) => GroupChanging?.Invoke(null, ev);
[MoonSharpHidden]
public void OnGroupChanged(PlayerGroupChangedEventArgs ev) => GroupChanged?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUpdatingEffect(PlayerEffectUpdatingEventArgs ev) => UpdatingEffect?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUpdatedEffect(PlayerEffectUpdatedEventArgs ev) => UpdatedEffect?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDying(PlayerDyingEventArgs ev) => Dying?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDeath(PlayerDeathEventArgs ev) => Death?.Invoke(null, ev);
[MoonSharpHidden]
public void OnHurting(PlayerHurtingEventArgs ev) => Hurting?.Invoke(null, ev);
[MoonSharpHidden]
public void OnHurt(PlayerHurtEventArgs ev) => Hurt?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangingRole(PlayerChangingRoleEventArgs ev) => ChangingRole?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangedRole(PlayerChangedRoleEventArgs ev) => ChangedRole?.Invoke(null, ev);
[MoonSharpHidden]
public void OnCuffing(PlayerCuffingEventArgs ev) => Cuffing?.Invoke(null, ev);
[MoonSharpHidden]
public void OnCuffed(PlayerCuffedEventArgs ev) => Cuffed?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUncuffing(PlayerUncuffingEventArgs ev) => Uncuffing?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUncuffed(PlayerUncuffedEventArgs ev) => Uncuffed?.Invoke(null, ev);
[MoonSharpHidden]
public void OnReceivingLoadout(PlayerReceivingLoadoutEventArgs ev) => ReceivingLoadout?.Invoke(null, ev);
[MoonSharpHidden]
public void OnReceivedLoadout(PlayerReceivedLoadoutEventArgs ev) => ReceivedLoadout?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSpawning(PlayerSpawningEventArgs ev) => Spawning?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSpawned(PlayerSpawnedEventArgs ev) => Spawned?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangingItem(PlayerChangingItemEventArgs ev) => ChangingItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangedItem(PlayerChangedItemEventArgs ev) => ChangedItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDroppingAmmo(PlayerDroppingAmmoEventArgs ev) => DroppingAmmo?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDroppedAmmo(PlayerDroppedAmmoEventArgs ev) => DroppedAmmo?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDroppingItem(PlayerDroppingItemEventArgs ev) => DroppingItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDroppedItem(PlayerDroppedItemEventArgs ev) => DroppedItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPickingUpAmmo(PlayerPickingUpAmmoEventArgs ev) => PickingUpAmmo?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPickedUpAmmo(PlayerPickedUpAmmoEventArgs ev) => PickedUpAmmo?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPickingUpArmor(PlayerPickingUpArmorEventArgs ev) => PickingUpArmor?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPickedUpArmor(PlayerPickedUpArmorEventArgs ev) => PickedUpArmor?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPickingUpItem(PlayerPickingUpItemEventArgs ev) => PickingUpItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPickedUpItem(PlayerPickedUpItemEventArgs ev) => PickedUpItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPickingUpScp330(PlayerPickingUpScp330EventArgs ev) => PickingUpScp330?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPickedUpScp330(PlayerPickedUpScp330EventArgs ev) => PickedUpScp330?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSearchedAmmo(PlayerSearchedAmmoEventArgs ev) => SearchedAmmo?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSearchingArmor(PlayerSearchingArmorEventArgs ev) => SearchingArmor?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSearchedArmor(PlayerSearchedArmorEventArgs ev) => SearchedArmor?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSearchingPickup(PlayerSearchingPickupEventArgs ev) => SearchingPickup?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractedToy(PlayerInteractedToyEventArgs ev) => InteractedToy?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSearchedPickup(PlayerSearchedPickupEventArgs ev) => SearchedPickup?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSearchingAmmo(PlayerSearchingAmmoEventArgs ev) => SearchingAmmo?.Invoke(null, ev);
[MoonSharpHidden]
public void OnThrowingItem(PlayerThrowingItemEventArgs ev) => ThrowingItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnThrewItem(PlayerThrewItemEventArgs ev) => ThrewItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnThrowingProjectile(PlayerThrowingProjectileEventArgs ev) => ThrowingProjectile?.Invoke(null, ev);
[MoonSharpHidden]
public void OnThrewProjectile(PlayerThrewProjectileEventArgs ev) => ThrewProjectile?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUsingItem(PlayerUsingItemEventArgs ev) => UsingItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUsedItem(PlayerUsedItemEventArgs ev) => UsedItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUsingRadio(PlayerUsingRadioEventArgs ev) => UsingRadio?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUsedRadio(PlayerUsedRadioEventArgs ev) => UsedRadio?.Invoke(null, ev);
[MoonSharpHidden]
public void OnAimedWeapon(PlayerAimedWeaponEventArgs ev) => AimedWeapon?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDryFiringWeapon(PlayerDryFiringWeaponEventArgs ev) => DryFiringWeapon?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDryFiredWeapon(PlayerDryFiredWeaponEventArgs ev) => DryFiredWeapon?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUnloadingWeapon(PlayerUnloadingWeaponEventArgs ev) => UnloadingWeapon?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUnloadedWeapon(PlayerUnloadedWeaponEventArgs ev) => UnloadedWeapon?.Invoke(null, ev);
[MoonSharpHidden]
public void OnReloadingWeapon(PlayerReloadingWeaponEventArgs ev) => ReloadingWeapon?.Invoke(null, ev);
[MoonSharpHidden]
public void OnReloadedWeapon(PlayerReloadedWeaponEventArgs ev) => ReloadedWeapon?.Invoke(null, ev);
[MoonSharpHidden]
public void OnShootingWeapon(PlayerShootingWeaponEventArgs ev) => ShootingWeapon?.Invoke(null, ev);
[MoonSharpHidden]
public void OnShotWeapon(PlayerShotWeaponEventArgs ev) => ShotWeapon?.Invoke(null, ev);
[MoonSharpHidden]
public void OnCancellingUsingItem(PlayerCancellingUsingItemEventArgs ev) => CancellingUsingItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnCancelledUsingItem(PlayerCancelledUsingItemEventArgs ev) => CancelledUsingItem?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangingRadioRange(PlayerChangingRadioRangeEventArgs ev) => ChangingRadioRange?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangedRadioRange(PlayerChangedRadioRangeEventArgs ev) => ChangedRadioRange?.Invoke(null, ev);
[MoonSharpHidden]
public void OnTogglingFlashlight(PlayerTogglingFlashlightEventArgs ev) => TogglingFlashlight?.Invoke(null, ev);
[MoonSharpHidden]
public void OnToggledFlashlight(PlayerToggledFlashlightEventArgs ev) => ToggledFlashlight?.Invoke(null, ev);
[MoonSharpHidden]
public void OnTogglingWeaponFlashlight(PlayerTogglingWeaponFlashlightEventArgs ev) => TogglingWeaponFlashlight?.Invoke(null, ev);
[MoonSharpHidden]
public void OnToggledWeaponFlashlight(PlayerToggledWeaponFlashlightEventArgs ev) => ToggledWeaponFlashlight?.Invoke(null, ev);
[MoonSharpHidden]
public void OnTogglingRadio(PlayerTogglingRadioEventArgs ev) => TogglingRadio?.Invoke(null, ev);
[MoonSharpHidden]
public void OnToggledRadio(PlayerToggledRadioEventArgs ev) => ToggledRadio?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDamagingShootingTarget(PlayerDamagingShootingTargetEventArgs ev) => DamagingShootingTarget?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDamagedShootingTarget(PlayerDamagedShootingTargetEventArgs ev) => DamagedShootingTarget?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDamagingWindow(PlayerDamagingWindowEventArgs ev) => DamagingWindow?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDamagedWindow(PlayerDamagedWindowEventArgs ev) => DamagedWindow?.Invoke(null, ev);
    [MoonSharpHidden]
public void OnEnteringPocketDimension(PlayerEnteringPocketDimensionEventArgs ev) => EnteringPocketDimension?.Invoke(null, ev);
[MoonSharpHidden]
public void OnEnteredPocketDimension(PlayerEnteredPocketDimensionEventArgs ev) => EnteredPocketDimension?.Invoke(null, ev);
[MoonSharpHidden]
public void OnLeavingPocketDimension(PlayerLeavingPocketDimensionEventArgs ev) => LeavingPocketDimension?.Invoke(null, ev);
[MoonSharpHidden]
public void OnLeftPocketDimension(PlayerLeftPocketDimensionEventArgs ev) => LeftPocketDimension?.Invoke(null, ev);
[MoonSharpHidden]
public void OnTriggeringTesla(PlayerTriggeringTeslaEventArgs ev) => TriggeringTesla?.Invoke(null, ev);
[MoonSharpHidden]
public void OnTriggeredTesla(PlayerTriggeredTeslaEventArgs ev) => TriggeredTesla?.Invoke(null, ev);
[MoonSharpHidden]
public void OnEscaping(PlayerEscapingEventArgs ev) => Escaping?.Invoke(null, ev);
[MoonSharpHidden]
public void OnEscaped(PlayerEscapedEventArgs ev) => Escaped?.Invoke(null, ev);
[MoonSharpHidden]
public void OnFlippingCoin(PlayerFlippingCoinEventArgs ev) => FlippingCoin?.Invoke(null, ev);
[MoonSharpHidden]
public void OnFlippedCoin(PlayerFlippedCoinEventArgs ev) => FlippedCoin?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSearchingToy(PlayerSearchingToyEventArgs ev) => SearchingToy?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSearchedToy(PlayerSearchedToyEventArgs ev) => SearchedToy?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSearchToyAborted(PlayerSearchToyAbortedEventArgs ev) => SearchToyAborted?.Invoke(null, ev);
[MoonSharpHidden]
public void OnIdlingTesla(PlayerIdlingTeslaEventArgs ev) => IdlingTesla?.Invoke(null, ev);
[MoonSharpHidden]
public void OnIdledTesla(PlayerIdledTeslaEventArgs ev) => IdledTesla?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractingDoor(PlayerInteractingDoorEventArgs ev) => InteractingDoor?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractedDoor(PlayerInteractedDoorEventArgs ev) => InteractedDoor?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractingElevator(PlayerInteractingElevatorEventArgs ev) => InteractingElevator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractedElevator(PlayerInteractedElevatorEventArgs ev) => InteractedElevator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractingGenerator(PlayerInteractingGeneratorEventArgs ev) => InteractingGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractedGenerator(PlayerInteractedGeneratorEventArgs ev) => InteractedGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnOpeningGenerator(PlayerOpeningGeneratorEventArgs ev) => OpeningGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnOpenedGenerator(PlayerOpenedGeneratorEventArgs ev) => OpenedGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnActivatingGenerator(PlayerActivatingGeneratorEventArgs ev) => ActivatingGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnActivatedGenerator(PlayerActivatedGeneratorEventArgs ev) => ActivatedGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDeactivatingGenerator(PlayerDeactivatingGeneratorEventArgs ev) => DeactivatingGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnDeactivatedGenerator(PlayerDeactivatedGeneratorEventArgs ev) => DeactivatedGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUnlockingGenerator(PlayerUnlockingGeneratorEventArgs ev) => UnlockingGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUnlockedGenerator(PlayerUnlockedGeneratorEventArgs ev) => UnlockedGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnClosingGenerator(PlayerClosingGeneratorEventArgs ev) => ClosingGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnClosedGenerator(PlayerClosedGeneratorEventArgs ev) => ClosedGenerator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractingLocker(PlayerInteractingLockerEventArgs ev) => InteractingLocker?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractedLocker(PlayerInteractedLockerEventArgs ev) => InteractedLocker?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractingScp330(PlayerInteractingScp330EventArgs ev) => InteractingScp330?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractedScp330(PlayerInteractedScp330EventArgs ev) => InteractedScp330?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractingShootingTarget(PlayerInteractingShootingTargetEventArgs ev) => InteractingShootingTarget?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractedShootingTarget(PlayerInteractedShootingTargetEventArgs ev) => InteractedShootingTarget?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPlacingBlood(PlayerPlacingBloodEventArgs ev) => PlacingBlood?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPlacedBlood(PlayerPlacedBloodEventArgs ev) => PlacedBlood?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPlacingBulletHole(PlayerPlacingBulletHoleEventArgs ev) => PlacingBulletHole?.Invoke(null, ev);
[MoonSharpHidden]
public void OnPlacedBulletHole(PlayerPlacedBulletHoleEventArgs ev) => PlacedBulletHole?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSpawningRagdoll(PlayerSpawningRagdollEventArgs ev) => SpawningRagdoll?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSpawnedRagdoll(PlayerSpawnedRagdollEventArgs ev) => SpawnedRagdoll?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUnlockingWarheadButton(PlayerUnlockingWarheadButtonEventArgs ev) => UnlockingWarheadButton?.Invoke(null, ev);
[MoonSharpHidden]
public void OnUnlockedWarheadButton(PlayerUnlockedWarheadButtonEventArgs ev) => UnlockedWarheadButton?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangedSpectator(PlayerChangedSpectatorEventArgs ev) => ChangedSpectator?.Invoke(null, ev);
[MoonSharpHidden]
public void OnEnteringHazard(PlayerEnteringHazardEventArgs ev) => EnteringHazard?.Invoke(null, ev);
[MoonSharpHidden]
public void OnEnteredHazard(PlayerEnteredHazardEventArgs ev) => EnteredHazard?.Invoke(null, ev);
[MoonSharpHidden]
public void OnStayingInHazard(PlayersStayingInHazardEventArgs ev) => StayingInHazard?.Invoke(null, ev);
[MoonSharpHidden]
public void OnLeavingHazard(PlayerLeavingHazardEventArgs ev) => LeavingHazard?.Invoke(null, ev);
[MoonSharpHidden]
public void OnLeftHazard(PlayerLeftHazardEventArgs ev) => LeftHazard?.Invoke(null, ev);
[MoonSharpHidden]
public void OnValidatedVisibility(PlayerValidatedVisibilityEventArgs ev) => ValidatedVisibility?.Invoke(null, ev);
[MoonSharpHidden]
public void OnJumped(PlayerJumpedEventArgs ev) => Jumped?.Invoke(null, ev);
[MoonSharpHidden]
public void OnMovementStateChanged(PlayerMovementStateChangedEventArgs ev) => MovementStateChanged?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangingAttachments(PlayerChangingAttachmentsEventArgs ev) => ChangingAttachments?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangedAttachments(PlayerChangedAttachmentsEventArgs ev) => ChangedAttachments?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSendingAttachmentsPrefs(PlayerSendingAttachmentsPrefsEventArgs ev) => SendingAttachmentsPrefs?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSentAttachmentsPrefs(PlayerSentAttachmentsPrefsEventArgs ev) => SentAttachmentsPrefs?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractingWarheadLever(PlayerInteractingWarheadLeverEventArgs ev) => InteractingWarheadLever?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInteractedWarheadLever(PlayerInteractedWarheadLeverEventArgs ev) => InteractedWarheadLever?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSpinningRevolver(PlayerSpinningRevolverEventArgs ev) => SpinningRevolver?.Invoke(null, ev);
[MoonSharpHidden]
public void OnSpinnedRevolver(PlayerSpinnedRevolverEventArgs ev) => SpinnedRevolver?.Invoke(null, ev);
[MoonSharpHidden]
public void OnToggledDisruptorFiringMode(PlayerToggledDisruptorFiringModeEventArgs ev) => ToggledDisruptorFiringMode?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangedBadgeVisibility(PlayerChangedBadgeVisibilityEventArgs ev) => ChangedBadgeVisibility?.Invoke(null, ev);
[MoonSharpHidden]
public void OnChangingBadgeVisibility(PlayerChangingBadgeVisibilityEventArgs ev) => ChangingBadgeVisibility?.Invoke(null, ev);
[MoonSharpHidden]
public void OnProcessingJailbirdMessage(PlayerProcessingJailbirdMessageEventArgs ev) => ProcessingJailbirdMessage?.Invoke(null, ev);
[MoonSharpHidden]
public void OnProcessedJailbirdMessage(PlayerProcessedJailbirdMessageEventArgs ev) => ProcessedJailbirdMessage?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInspectingKeycard(PlayerInspectingKeycardEventArgs ev) => InspectingKeycard?.Invoke(null, ev);
[MoonSharpHidden]
public void OnInspectedKeycard(PlayerInspectedKeycardEventArgs ev) => InspectedKeycard?.Invoke(null, ev);
[MoonSharpHidden]
public void OnRoomChanged(PlayerRoomChangedEventArgs ev) => RoomChanged?.Invoke(null, ev);
[MoonSharpHidden]
public void OnZoneChanged(PlayerZoneChangedEventArgs ev) => ZoneChanged?.Invoke(null, ev);
[MoonSharpHidden]
public void OnRaPlayerListAddedPlayer(PlayerRaPlayerListAddedPlayerEventArgs ev) => RaPlayerListAddedPlayer?.Invoke(null, ev);
[MoonSharpHidden]
public void OnRaPlayerListAddingPlayer(PlayerRaPlayerListAddingPlayerEventArgs ev) => RaPlayerListAddingPlayer?.Invoke(null, ev);
[MoonSharpHidden]
public void OnReceivedAchievement(PlayerReceivedAchievementEventArgs ev) => ReceivedAchievement?.Invoke(null, ev);
[MoonSharpHidden]
public void OnRequestedRaPlayerInfo(PlayerRequestedRaPlayerInfoEventArgs ev) => RequestedRaPlayerInfo?.Invoke(null, ev);
[MoonSharpHidden]
public void OnRequestingRaPlayerInfo(PlayerRequestingRaPlayerInfoEventArgs ev) => RequestingRaPlayerInfo?.Invoke(null, ev);
[MoonSharpHidden]
public void OnRequestedCustomRaInfo(PlayerRequestedCustomRaInfoEventArgs ev) => RequestedCustomRaInfo?.Invoke(null, ev);
[MoonSharpHidden]
public void OnRequestedRaPlayerList(PlayerRequestedRaPlayerListEventArgs ev) => RequestedRaPlayerList?.Invoke(null, ev);
[MoonSharpHidden]
public void OnRequestingRaPlayerList(PlayerRequestingRaPlayerListEventArgs ev) => RequestingRaPlayerList?.Invoke(null, ev);
[MoonSharpHidden]
public void OnRequestedRaPlayersInfo(PlayerRequestedRaPlayersInfoEventArgs ev) => RequestedRaPlayersInfo?.Invoke(null, ev);
[MoonSharpHidden]
public void OnRequestingRaPlayersInfo(PlayerRequestingRaPlayersInfoEventArgs ev) => RequestingRaPlayersInfo?.Invoke(null, ev);


[MoonSharpHidden]
public void RegisterEventTypes()
{
    UserData.RegisterType<PlayerJoinedEventArgs>();
    UserData.RegisterType<PlayerLeftEventArgs>();
    UserData.RegisterType<PlayerReceivingVoiceMessageEventArgs>();
    UserData.RegisterType<PlayerSendingVoiceMessageEventArgs>();
    UserData.RegisterType<PlayerPreAuthenticatingEventArgs>();
    UserData.RegisterType<PlayerPreAuthenticatedEventArgs>();
    UserData.RegisterType<PlayerUsingIntercomEventArgs>();
    UserData.RegisterType<PlayerUsedIntercomEventArgs>();
    UserData.RegisterType<PlayerBanningEventArgs>();
    UserData.RegisterType<PlayerBannedEventArgs>();
    UserData.RegisterType<PlayerKickingEventArgs>();
    UserData.RegisterType<PlayerKickedEventArgs>();
    UserData.RegisterType<PlayerMutingEventArgs>();
    UserData.RegisterType<PlayerMutedEventArgs>();
    UserData.RegisterType<PlayerUnmutingEventArgs>();
    UserData.RegisterType<PlayerUnmutedEventArgs>();
    UserData.RegisterType<PlayerReportingCheaterEventArgs>();
    UserData.RegisterType<PlayerReportedCheaterEventArgs>();
    UserData.RegisterType<PlayerReportingPlayerEventArgs>();
    UserData.RegisterType<PlayerReportedPlayerEventArgs>();
    UserData.RegisterType<PlayerTogglingNoclipEventArgs>();
    UserData.RegisterType<PlayerToggledNoclipEventArgs>();
    UserData.RegisterType<PlayerChangingNicknameEventArgs>();
    UserData.RegisterType<PlayerChangedNicknameEventArgs>();
    UserData.RegisterType<PlayerGroupChangingEventArgs>();
    UserData.RegisterType<PlayerGroupChangedEventArgs>();
    UserData.RegisterType<PlayerEffectUpdatingEventArgs>();
    UserData.RegisterType<PlayerEffectUpdatedEventArgs>();
    UserData.RegisterType<PlayerDyingEventArgs>();
    UserData.RegisterType<PlayerDeathEventArgs>();
    UserData.RegisterType<PlayerHurtingEventArgs>();
    UserData.RegisterType<PlayerHurtEventArgs>();
    UserData.RegisterType<PlayerChangingRoleEventArgs>();
    UserData.RegisterType<PlayerChangedRoleEventArgs>();
    UserData.RegisterType<PlayerCuffingEventArgs>();
    UserData.RegisterType<PlayerCuffedEventArgs>();
    UserData.RegisterType<PlayerUncuffingEventArgs>();
    UserData.RegisterType<PlayerUncuffedEventArgs>();
    UserData.RegisterType<PlayerReceivingLoadoutEventArgs>();
    UserData.RegisterType<PlayerReceivedLoadoutEventArgs>();
    UserData.RegisterType<PlayerSpawningEventArgs>();
    UserData.RegisterType<PlayerSpawnedEventArgs>();
    UserData.RegisterType<PlayerChangingItemEventArgs>();
    UserData.RegisterType<PlayerChangedItemEventArgs>();
    UserData.RegisterType<PlayerDroppingAmmoEventArgs>();
    UserData.RegisterType<PlayerDroppedAmmoEventArgs>();
    UserData.RegisterType<PlayerDroppingItemEventArgs>();
    UserData.RegisterType<PlayerDroppedItemEventArgs>();
    UserData.RegisterType<PlayerPickingUpAmmoEventArgs>();
    UserData.RegisterType<PlayerPickedUpAmmoEventArgs>();
    UserData.RegisterType<PlayerPickingUpArmorEventArgs>();
    UserData.RegisterType<PlayerPickedUpArmorEventArgs>();
    UserData.RegisterType<PlayerPickingUpItemEventArgs>();
    UserData.RegisterType<PlayerPickedUpItemEventArgs>();
    UserData.RegisterType<PlayerPickingUpScp330EventArgs>();
    UserData.RegisterType<PlayerPickedUpScp330EventArgs>();
    UserData.RegisterType<PlayerSearchedAmmoEventArgs>();
    UserData.RegisterType<PlayerSearchingArmorEventArgs>();
    UserData.RegisterType<PlayerSearchedArmorEventArgs>();
    UserData.RegisterType<PlayerSearchingPickupEventArgs>();
    UserData.RegisterType<PlayerInteractedToyEventArgs>();
    UserData.RegisterType<PlayerSearchedPickupEventArgs>();
    UserData.RegisterType<PlayerSearchingAmmoEventArgs>();
    UserData.RegisterType<PlayerThrowingItemEventArgs>();
    UserData.RegisterType<PlayerThrewItemEventArgs>();
    UserData.RegisterType<PlayerThrowingProjectileEventArgs>();
    UserData.RegisterType<PlayerThrewProjectileEventArgs>();
    UserData.RegisterType<PlayerUsingItemEventArgs>();
    UserData.RegisterType<PlayerUsedItemEventArgs>();
    UserData.RegisterType<PlayerUsingRadioEventArgs>();
    UserData.RegisterType<PlayerUsedRadioEventArgs>();
    UserData.RegisterType<PlayerAimedWeaponEventArgs>();
    UserData.RegisterType<PlayerDryFiringWeaponEventArgs>();
    UserData.RegisterType<PlayerDryFiredWeaponEventArgs>();
    UserData.RegisterType<PlayerUnloadingWeaponEventArgs>();
    UserData.RegisterType<PlayerUnloadedWeaponEventArgs>();
    UserData.RegisterType<PlayerReloadingWeaponEventArgs>();
    UserData.RegisterType<PlayerReloadedWeaponEventArgs>();
    UserData.RegisterType<PlayerShootingWeaponEventArgs>();
    UserData.RegisterType<PlayerShotWeaponEventArgs>();
    UserData.RegisterType<PlayerCancellingUsingItemEventArgs>();
    UserData.RegisterType<PlayerCancelledUsingItemEventArgs>();
    UserData.RegisterType<PlayerChangingRadioRangeEventArgs>();
    UserData.RegisterType<PlayerChangedRadioRangeEventArgs>();
    UserData.RegisterType<PlayerTogglingFlashlightEventArgs>();
    UserData.RegisterType<PlayerToggledFlashlightEventArgs>();
    UserData.RegisterType<PlayerTogglingWeaponFlashlightEventArgs>();
    UserData.RegisterType<PlayerToggledWeaponFlashlightEventArgs>();
    UserData.RegisterType<PlayerTogglingRadioEventArgs>();
    UserData.RegisterType<PlayerToggledRadioEventArgs>();
    UserData.RegisterType<PlayerDamagingShootingTargetEventArgs>();
    UserData.RegisterType<PlayerDamagedShootingTargetEventArgs>();
    UserData.RegisterType<PlayerDamagingWindowEventArgs>();
    UserData.RegisterType<PlayerDamagedWindowEventArgs>();
    UserData.RegisterType<PlayerEnteringPocketDimensionEventArgs>();
    UserData.RegisterType<PlayerEnteredPocketDimensionEventArgs>();
    UserData.RegisterType<PlayerLeavingPocketDimensionEventArgs>();
    UserData.RegisterType<PlayerLeftPocketDimensionEventArgs>();
    UserData.RegisterType<PlayerTriggeringTeslaEventArgs>();
    UserData.RegisterType<PlayerTriggeredTeslaEventArgs>();
    UserData.RegisterType<PlayerEscapingEventArgs>();
    UserData.RegisterType<PlayerEscapedEventArgs>();
    UserData.RegisterType<PlayerFlippingCoinEventArgs>();
    UserData.RegisterType<PlayerFlippedCoinEventArgs>();
    UserData.RegisterType<PlayerSearchingToyEventArgs>();
    UserData.RegisterType<PlayerSearchedToyEventArgs>();
    UserData.RegisterType<PlayerSearchToyAbortedEventArgs>();
    UserData.RegisterType<PlayerIdlingTeslaEventArgs>();
    UserData.RegisterType<PlayerIdledTeslaEventArgs>();
    UserData.RegisterType<PlayerInteractingDoorEventArgs>();
    UserData.RegisterType<PlayerInteractedDoorEventArgs>();
    UserData.RegisterType<PlayerInteractingElevatorEventArgs>();
    UserData.RegisterType<PlayerInteractedElevatorEventArgs>();
    UserData.RegisterType<PlayerInteractingGeneratorEventArgs>();
    UserData.RegisterType<PlayerInteractedGeneratorEventArgs>();
    UserData.RegisterType<PlayerOpeningGeneratorEventArgs>();
    UserData.RegisterType<PlayerOpenedGeneratorEventArgs>();
    UserData.RegisterType<PlayerActivatingGeneratorEventArgs>();
    UserData.RegisterType<PlayerActivatedGeneratorEventArgs>();
    UserData.RegisterType<PlayerDeactivatingGeneratorEventArgs>();
    UserData.RegisterType<PlayerDeactivatedGeneratorEventArgs>();
    UserData.RegisterType<PlayerUnlockingGeneratorEventArgs>();
    UserData.RegisterType<PlayerUnlockedGeneratorEventArgs>();
    UserData.RegisterType<PlayerClosingGeneratorEventArgs>();
    UserData.RegisterType<PlayerClosedGeneratorEventArgs>();
    UserData.RegisterType<PlayerInteractingLockerEventArgs>();
    UserData.RegisterType<PlayerInteractedLockerEventArgs>();
    UserData.RegisterType<PlayerInteractingScp330EventArgs>();
    UserData.RegisterType<PlayerInteractedScp330EventArgs>();
    UserData.RegisterType<PlayerInteractingShootingTargetEventArgs>();
    UserData.RegisterType<PlayerInteractedShootingTargetEventArgs>();
    UserData.RegisterType<PlayerPlacingBloodEventArgs>();
    UserData.RegisterType<PlayerPlacedBloodEventArgs>();
    UserData.RegisterType<PlayerPlacingBulletHoleEventArgs>();
    UserData.RegisterType<PlayerPlacedBulletHoleEventArgs>();
    UserData.RegisterType<PlayerSpawningRagdollEventArgs>();
    UserData.RegisterType<PlayerSpawnedRagdollEventArgs>();
    UserData.RegisterType<PlayerUnlockingWarheadButtonEventArgs>();
    UserData.RegisterType<PlayerUnlockedWarheadButtonEventArgs>();
    UserData.RegisterType<PlayerChangedSpectatorEventArgs>();
    UserData.RegisterType<PlayerEnteringHazardEventArgs>();
    UserData.RegisterType<PlayerEnteredHazardEventArgs>();
    UserData.RegisterType<PlayersStayingInHazardEventArgs>();
    UserData.RegisterType<PlayerLeavingHazardEventArgs>();
    UserData.RegisterType<PlayerLeftHazardEventArgs>();
    UserData.RegisterType<PlayerValidatedVisibilityEventArgs>();
    UserData.RegisterType<PlayerJumpedEventArgs>();
    UserData.RegisterType<PlayerMovementStateChangedEventArgs>();
    UserData.RegisterType<PlayerChangingAttachmentsEventArgs>();
    UserData.RegisterType<PlayerChangedAttachmentsEventArgs>();
    UserData.RegisterType<PlayerSendingAttachmentsPrefsEventArgs>();
    UserData.RegisterType<PlayerSentAttachmentsPrefsEventArgs>();
    UserData.RegisterType<PlayerInteractingWarheadLeverEventArgs>();
    UserData.RegisterType<PlayerInteractedWarheadLeverEventArgs>();
    UserData.RegisterType<PlayerSpinningRevolverEventArgs>();
    UserData.RegisterType<PlayerSpinnedRevolverEventArgs>();
    UserData.RegisterType<PlayerToggledDisruptorFiringModeEventArgs>();
    UserData.RegisterType<PlayerChangedBadgeVisibilityEventArgs>();
    UserData.RegisterType<PlayerChangingBadgeVisibilityEventArgs>();
    UserData.RegisterType<PlayerProcessingJailbirdMessageEventArgs>();
    UserData.RegisterType<PlayerProcessedJailbirdMessageEventArgs>();
    UserData.RegisterType<PlayerInspectingKeycardEventArgs>();
    UserData.RegisterType<PlayerInspectedKeycardEventArgs>();
    UserData.RegisterType<PlayerRoomChangedEventArgs>();
    UserData.RegisterType<PlayerZoneChangedEventArgs>();
    UserData.RegisterType<PlayerRaPlayerListAddedPlayerEventArgs>();
    UserData.RegisterType<PlayerRaPlayerListAddingPlayerEventArgs>();
    UserData.RegisterType<PlayerReceivedAchievementEventArgs>();
    UserData.RegisterType<PlayerRequestedRaPlayerInfoEventArgs>();
    UserData.RegisterType<PlayerRequestingRaPlayerInfoEventArgs>();
    UserData.RegisterType<PlayerRequestedCustomRaInfoEventArgs>();
    UserData.RegisterType<PlayerRequestedRaPlayerListEventArgs>();
    UserData.RegisterType<PlayerRequestingRaPlayerListEventArgs>();
    UserData.RegisterType<PlayerRequestedRaPlayersInfoEventArgs>();
    UserData.RegisterType<PlayerRequestingRaPlayersInfoEventArgs>();
}



[MoonSharpHidden]
    public void RegisterEvents()
    {
        PlayerEvents.Joined += OnJoined;
        PlayerEvents.Left += OnLeft;
        PlayerEvents.ReceivingVoiceMessage += OnReceivingVoiceMessage;
        PlayerEvents.SendingVoiceMessage += OnSendingVoiceMessage;
        PlayerEvents.PreAuthenticating += OnPreAuthenticating;
        PlayerEvents.PreAuthenticated += OnPreAuthenticated;
        PlayerEvents.UsingIntercom += OnUsingIntercom;
        PlayerEvents.UsedIntercom += OnUsedIntercom;
        PlayerEvents.Banning += OnBanning;
        PlayerEvents.Banned += OnBanned;
        PlayerEvents.Kicking += OnKicking;
        PlayerEvents.Kicked += OnKicked;
        PlayerEvents.Muting += OnMuting;
        PlayerEvents.Muted += OnMuted;
        PlayerEvents.Unmuting += OnUnmuting;
        PlayerEvents.Unmuted += OnUnmuted;
        PlayerEvents.ReportingCheater += OnReportingCheater;
        PlayerEvents.ReportedCheater += OnReportedCheater;
        PlayerEvents.ReportingPlayer += OnReportingPlayer;
        PlayerEvents.ReportedPlayer += OnReportedPlayer;
        PlayerEvents.TogglingNoclip += OnTogglingNoclip;
        PlayerEvents.ToggledNoclip += OnToggledNoclip;
        PlayerEvents.ChangingNickname += OnChangingNickname;
        PlayerEvents.ChangedNickname += OnChangedNickname;
        PlayerEvents.GroupChanging += OnGroupChanging;
        PlayerEvents.GroupChanged += OnGroupChanged;
        PlayerEvents.UpdatingEffect += OnUpdatingEffect;
        PlayerEvents.UpdatedEffect += OnUpdatedEffect;
        PlayerEvents.Dying += OnDying;
        PlayerEvents.Death += OnDeath;
        PlayerEvents.Hurting += OnHurting;
        PlayerEvents.Hurt += OnHurt;
        PlayerEvents.ChangingRole += OnChangingRole;
        PlayerEvents.ChangedRole += OnChangedRole;
        PlayerEvents.Cuffing += OnCuffing;
        PlayerEvents.Cuffed += OnCuffed;
        PlayerEvents.Uncuffing += OnUncuffing;
        PlayerEvents.Uncuffed += OnUncuffed;
        PlayerEvents.ReceivingLoadout += OnReceivingLoadout;
        PlayerEvents.ReceivedLoadout += OnReceivedLoadout;
        PlayerEvents.Spawning += OnSpawning;
        PlayerEvents.Spawned += OnSpawned;
        PlayerEvents.ChangingItem += OnChangingItem;
        PlayerEvents.ChangedItem += OnChangedItem;
        PlayerEvents.DroppingAmmo += OnDroppingAmmo;
        PlayerEvents.DroppedAmmo += OnDroppedAmmo;
        PlayerEvents.DroppingItem += OnDroppingItem;
        PlayerEvents.DroppedItem += OnDroppedItem;
        PlayerEvents.PickingUpAmmo += OnPickingUpAmmo;
        PlayerEvents.PickedUpAmmo += OnPickedUpAmmo;
        PlayerEvents.PickingUpArmor += OnPickingUpArmor;
        PlayerEvents.PickedUpArmor += OnPickedUpArmor;
        PlayerEvents.PickingUpItem += OnPickingUpItem;
        PlayerEvents.PickedUpItem += OnPickedUpItem;
        PlayerEvents.PickingUpScp330 += OnPickingUpScp330;
        PlayerEvents.PickedUpScp330 += OnPickedUpScp330;
        PlayerEvents.SearchedAmmo += OnSearchedAmmo;
        PlayerEvents.SearchingArmor += OnSearchingArmor;
        PlayerEvents.SearchedArmor += OnSearchedArmor;
        PlayerEvents.SearchingPickup += OnSearchingPickup;
        PlayerEvents.InteractedToy += OnInteractedToy;
        PlayerEvents.SearchedPickup += OnSearchedPickup;
        PlayerEvents.SearchingAmmo += OnSearchingAmmo;
        PlayerEvents.ThrowingItem += OnThrowingItem;
        PlayerEvents.ThrewItem += OnThrewItem;
        PlayerEvents.ThrowingProjectile += OnThrowingProjectile;
        PlayerEvents.ThrewProjectile += OnThrewProjectile;
        PlayerEvents.UsingItem += OnUsingItem;
        PlayerEvents.UsedItem += OnUsedItem;
        PlayerEvents.UsingRadio += OnUsingRadio;
        PlayerEvents.UsedRadio += OnUsedRadio;
        PlayerEvents.AimedWeapon += OnAimedWeapon;
        PlayerEvents.DryFiringWeapon += OnDryFiringWeapon;
        PlayerEvents.DryFiredWeapon += OnDryFiredWeapon;
        PlayerEvents.UnloadingWeapon += OnUnloadingWeapon;
        PlayerEvents.UnloadedWeapon += OnUnloadedWeapon;
        PlayerEvents.ReloadingWeapon += OnReloadingWeapon;
        PlayerEvents.ReloadedWeapon += OnReloadedWeapon;
        PlayerEvents.ShootingWeapon += OnShootingWeapon;
        PlayerEvents.ShotWeapon += OnShotWeapon;
        PlayerEvents.CancellingUsingItem += OnCancellingUsingItem;
        PlayerEvents.CancelledUsingItem += OnCancelledUsingItem;
        PlayerEvents.ChangingRadioRange += OnChangingRadioRange;
        PlayerEvents.ChangedRadioRange += OnChangedRadioRange;
        PlayerEvents.TogglingFlashlight += OnTogglingFlashlight;
        PlayerEvents.ToggledFlashlight += OnToggledFlashlight;
        PlayerEvents.TogglingWeaponFlashlight += OnTogglingWeaponFlashlight;
        PlayerEvents.ToggledWeaponFlashlight += OnToggledWeaponFlashlight;
        PlayerEvents.TogglingRadio += OnTogglingRadio;
        PlayerEvents.ToggledRadio += OnToggledRadio;
        PlayerEvents.DamagingShootingTarget += OnDamagingShootingTarget;
        PlayerEvents.DamagedShootingTarget += OnDamagedShootingTarget;
        PlayerEvents.DamagingWindow += OnDamagingWindow;
        PlayerEvents.DamagedWindow += OnDamagedWindow;
        PlayerEvents.EnteringPocketDimension += OnEnteringPocketDimension;
        PlayerEvents.EnteredPocketDimension += OnEnteredPocketDimension;
        PlayerEvents.LeavingPocketDimension += OnLeavingPocketDimension;
        PlayerEvents.LeftPocketDimension += OnLeftPocketDimension;
        PlayerEvents.TriggeringTesla += OnTriggeringTesla;
        PlayerEvents.TriggeredTesla += OnTriggeredTesla;
        PlayerEvents.Escaping += OnEscaping;
        PlayerEvents.Escaped += OnEscaped;
        PlayerEvents.FlippingCoin += OnFlippingCoin;
        PlayerEvents.FlippedCoin += OnFlippedCoin;
        PlayerEvents.SearchingToy += OnSearchingToy;
        PlayerEvents.SearchedToy += OnSearchedToy;
        PlayerEvents.SearchToyAborted += OnSearchToyAborted;
        PlayerEvents.IdlingTesla += OnIdlingTesla;
        PlayerEvents.IdledTesla += OnIdledTesla;
        PlayerEvents.InteractingDoor += OnInteractingDoor;
        PlayerEvents.InteractedDoor += OnInteractedDoor;
        PlayerEvents.InteractingElevator += OnInteractingElevator;
        PlayerEvents.InteractedElevator += OnInteractedElevator;
        PlayerEvents.InteractingGenerator += OnInteractingGenerator;
        PlayerEvents.InteractedGenerator += OnInteractedGenerator;
        PlayerEvents.OpeningGenerator += OnOpeningGenerator;
        PlayerEvents.OpenedGenerator += OnOpenedGenerator;
        PlayerEvents.ActivatingGenerator += OnActivatingGenerator;
        PlayerEvents.ActivatedGenerator += OnActivatedGenerator;
        PlayerEvents.DeactivatingGenerator += OnDeactivatingGenerator;
        PlayerEvents.DeactivatedGenerator += OnDeactivatedGenerator;
        PlayerEvents.UnlockingGenerator += OnUnlockingGenerator;
        PlayerEvents.UnlockedGenerator += OnUnlockedGenerator;
        PlayerEvents.ClosingGenerator += OnClosingGenerator;
        PlayerEvents.ClosedGenerator += OnClosedGenerator;
        PlayerEvents.InteractingLocker += OnInteractingLocker;
        PlayerEvents.InteractedLocker += OnInteractedLocker;
        PlayerEvents.InteractingScp330 += OnInteractingScp330;
        PlayerEvents.InteractedScp330 += OnInteractedScp330;
        PlayerEvents.InteractingShootingTarget += OnInteractingShootingTarget;
        PlayerEvents.InteractedShootingTarget += OnInteractedShootingTarget;
        PlayerEvents.PlacingBlood += OnPlacingBlood;
        PlayerEvents.PlacedBlood += OnPlacedBlood;
        PlayerEvents.PlacingBulletHole += OnPlacingBulletHole;
        PlayerEvents.PlacedBulletHole += OnPlacedBulletHole;
        PlayerEvents.SpawningRagdoll += OnSpawningRagdoll;
        PlayerEvents.SpawnedRagdoll += OnSpawnedRagdoll;
        PlayerEvents.UnlockingWarheadButton += OnUnlockingWarheadButton;
        PlayerEvents.UnlockedWarheadButton += OnUnlockedWarheadButton;
        PlayerEvents.ChangedSpectator += OnChangedSpectator;
        PlayerEvents.EnteringHazard += OnEnteringHazard;
        PlayerEvents.EnteredHazard += OnEnteredHazard;
        PlayerEvents.StayingInHazard += OnStayingInHazard;
        PlayerEvents.LeavingHazard += OnLeavingHazard;
        PlayerEvents.LeftHazard += OnLeftHazard;
        PlayerEvents.ValidatedVisibility += OnValidatedVisibility;
        PlayerEvents.Jumped += OnJumped;
        PlayerEvents.MovementStateChanged += OnMovementStateChanged;
        PlayerEvents.ChangingAttachments += OnChangingAttachments;
        PlayerEvents.ChangedAttachments += OnChangedAttachments;
        PlayerEvents.SendingAttachmentsPrefs += OnSendingAttachmentsPrefs;
        PlayerEvents.SentAttachmentsPrefs += OnSentAttachmentsPrefs;
        PlayerEvents.InteractingWarheadLever += OnInteractingWarheadLever;
        PlayerEvents.InteractedWarheadLever += OnInteractedWarheadLever;
        PlayerEvents.SpinningRevolver += OnSpinningRevolver;
        PlayerEvents.SpinnedRevolver += OnSpinnedRevolver;
        PlayerEvents.ToggledDisruptorFiringMode += OnToggledDisruptorFiringMode;
        PlayerEvents.ChangedBadgeVisibility += OnChangedBadgeVisibility;
        PlayerEvents.ChangingBadgeVisibility += OnChangingBadgeVisibility;
        PlayerEvents.ProcessingJailbirdMessage += OnProcessingJailbirdMessage;
        PlayerEvents.ProcessedJailbirdMessage += OnProcessedJailbirdMessage;
        PlayerEvents.InspectingKeycard += OnInspectingKeycard;
        PlayerEvents.InspectedKeycard += OnInspectedKeycard;
        PlayerEvents.RoomChanged += OnRoomChanged;
        PlayerEvents.ZoneChanged += OnZoneChanged;
        PlayerEvents.RaPlayerListAddedPlayer += OnRaPlayerListAddedPlayer;
        PlayerEvents.RaPlayerListAddingPlayer += OnRaPlayerListAddingPlayer;
        PlayerEvents.ReceivedAchievement += OnReceivedAchievement;
        PlayerEvents.RequestedRaPlayerInfo += OnRequestedRaPlayerInfo;
        PlayerEvents.RequestingRaPlayerInfo += OnRequestingRaPlayerInfo;
        PlayerEvents.RequestedCustomRaInfo += OnRequestedCustomRaInfo;
        PlayerEvents.RequestedRaPlayerList += OnRequestedRaPlayerList;
        PlayerEvents.RequestingRaPlayerList += OnRequestingRaPlayerList;
        PlayerEvents.RequestedRaPlayersInfo += OnRequestedRaPlayersInfo;
        PlayerEvents.RequestingRaPlayersInfo += OnRequestingRaPlayersInfo;
    }

    [MoonSharpHidden]
    public void UnregisterEvents()
    {
        PlayerEvents.Joined -= OnJoined;
        PlayerEvents.Left -= OnLeft;
        PlayerEvents.ReceivingVoiceMessage -= OnReceivingVoiceMessage;
        PlayerEvents.SendingVoiceMessage -= OnSendingVoiceMessage;
        PlayerEvents.PreAuthenticating -= OnPreAuthenticating;
        PlayerEvents.PreAuthenticated -= OnPreAuthenticated;
        PlayerEvents.UsingIntercom -= OnUsingIntercom;
        PlayerEvents.UsedIntercom -= OnUsedIntercom;
        PlayerEvents.Banning -= OnBanning;
        PlayerEvents.Banned -= OnBanned;
        PlayerEvents.Kicking -= OnKicking;
        PlayerEvents.Kicked -= OnKicked;
        PlayerEvents.Muting -= OnMuting;
        PlayerEvents.Muted -= OnMuted;
        PlayerEvents.Unmuting -= OnUnmuting;
        PlayerEvents.Unmuted -= OnUnmuted;
        PlayerEvents.ReportingCheater -= OnReportingCheater;
        PlayerEvents.ReportedCheater -= OnReportedCheater;
        PlayerEvents.ReportingPlayer -= OnReportingPlayer;
        PlayerEvents.ReportedPlayer -= OnReportedPlayer;
        PlayerEvents.TogglingNoclip -= OnTogglingNoclip;
        PlayerEvents.ToggledNoclip -= OnToggledNoclip;
        PlayerEvents.ChangingNickname -= OnChangingNickname;
        PlayerEvents.ChangedNickname -= OnChangedNickname;
        PlayerEvents.GroupChanging -= OnGroupChanging;
        PlayerEvents.GroupChanged -= OnGroupChanged;
        PlayerEvents.UpdatingEffect -= OnUpdatingEffect;
        PlayerEvents.UpdatedEffect -= OnUpdatedEffect;
        PlayerEvents.Dying -= OnDying;
        PlayerEvents.Death -= OnDeath;
        PlayerEvents.Hurting -= OnHurting;
        PlayerEvents.Hurt -= OnHurt;
        PlayerEvents.ChangingRole -= OnChangingRole;
        PlayerEvents.ChangedRole -= OnChangedRole;
        PlayerEvents.Cuffing -= OnCuffing;
        PlayerEvents.Cuffed -= OnCuffed;
        PlayerEvents.Uncuffing -= OnUncuffing;
        PlayerEvents.Uncuffed -= OnUncuffed;
        PlayerEvents.ReceivingLoadout -= OnReceivingLoadout;
        PlayerEvents.ReceivedLoadout -= OnReceivedLoadout;
        PlayerEvents.Spawning -= OnSpawning;
        PlayerEvents.Spawned -= OnSpawned;
        PlayerEvents.ChangingItem -= OnChangingItem;
        PlayerEvents.ChangedItem -= OnChangedItem;
        PlayerEvents.DroppingAmmo -= OnDroppingAmmo;
        PlayerEvents.DroppedAmmo -= OnDroppedAmmo;
        PlayerEvents.DroppingItem -= OnDroppingItem;
        PlayerEvents.DroppedItem -= OnDroppedItem;
        PlayerEvents.PickingUpAmmo -= OnPickingUpAmmo;
        PlayerEvents.PickedUpAmmo -= OnPickedUpAmmo;
        PlayerEvents.PickingUpArmor -= OnPickingUpArmor;
        PlayerEvents.PickedUpArmor -= OnPickedUpArmor;
        PlayerEvents.PickingUpItem -= OnPickingUpItem;
        PlayerEvents.PickedUpItem -= OnPickedUpItem;
        PlayerEvents.PickingUpScp330 -= OnPickingUpScp330;
        PlayerEvents.PickedUpScp330 -= OnPickedUpScp330;
        PlayerEvents.SearchedAmmo -= OnSearchedAmmo;
        PlayerEvents.SearchingArmor -= OnSearchingArmor;
        PlayerEvents.SearchedArmor -= OnSearchedArmor;
        PlayerEvents.SearchingPickup -= OnSearchingPickup;
        PlayerEvents.InteractedToy -= OnInteractedToy;
        PlayerEvents.SearchedPickup -= OnSearchedPickup;
        PlayerEvents.SearchingAmmo -= OnSearchingAmmo;
        PlayerEvents.ThrowingItem -= OnThrowingItem;
        PlayerEvents.ThrewItem -= OnThrewItem;
        PlayerEvents.ThrowingProjectile -= OnThrowingProjectile;
        PlayerEvents.ThrewProjectile -= OnThrewProjectile;
        PlayerEvents.UsingItem -= OnUsingItem;
        PlayerEvents.UsedItem -= OnUsedItem;
        PlayerEvents.UsingRadio -= OnUsingRadio;
        PlayerEvents.UsedRadio -= OnUsedRadio;
        PlayerEvents.AimedWeapon -= OnAimedWeapon;
        PlayerEvents.DryFiringWeapon -= OnDryFiringWeapon;
        PlayerEvents.DryFiredWeapon -= OnDryFiredWeapon;
        PlayerEvents.UnloadingWeapon -= OnUnloadingWeapon;
        PlayerEvents.UnloadedWeapon -= OnUnloadedWeapon;
        PlayerEvents.ReloadingWeapon -= OnReloadingWeapon;
        PlayerEvents.ReloadedWeapon -= OnReloadedWeapon;
        PlayerEvents.ShootingWeapon -= OnShootingWeapon;
        PlayerEvents.ShotWeapon -= OnShotWeapon;
        PlayerEvents.CancellingUsingItem -= OnCancellingUsingItem;
        PlayerEvents.CancelledUsingItem -= OnCancelledUsingItem;
        PlayerEvents.ChangingRadioRange -= OnChangingRadioRange;
        PlayerEvents.ChangedRadioRange -= OnChangedRadioRange;
        PlayerEvents.TogglingFlashlight -= OnTogglingFlashlight;
        PlayerEvents.ToggledFlashlight -= OnToggledFlashlight;
        PlayerEvents.TogglingWeaponFlashlight -= OnTogglingWeaponFlashlight;
        PlayerEvents.ToggledWeaponFlashlight -= OnToggledWeaponFlashlight;
        PlayerEvents.TogglingRadio -= OnTogglingRadio;
        PlayerEvents.ToggledRadio -= OnToggledRadio;
        PlayerEvents.DamagingShootingTarget -= OnDamagingShootingTarget;
        PlayerEvents.DamagedShootingTarget -= OnDamagedShootingTarget;
        PlayerEvents.DamagingWindow -= OnDamagingWindow;
        PlayerEvents.DamagedWindow -= OnDamagedWindow;
        PlayerEvents.EnteringPocketDimension -= OnEnteringPocketDimension;
        PlayerEvents.EnteredPocketDimension -= OnEnteredPocketDimension;
        PlayerEvents.LeavingPocketDimension -= OnLeavingPocketDimension;
        PlayerEvents.LeftPocketDimension -= OnLeftPocketDimension;
        PlayerEvents.TriggeringTesla -= OnTriggeringTesla;
        PlayerEvents.TriggeredTesla -= OnTriggeredTesla;
        PlayerEvents.Escaping -= OnEscaping;
        PlayerEvents.Escaped -= OnEscaped;
        PlayerEvents.FlippingCoin -= OnFlippingCoin;
        PlayerEvents.FlippedCoin -= OnFlippedCoin;
        PlayerEvents.SearchingToy -= OnSearchingToy;
        PlayerEvents.SearchedToy -= OnSearchedToy;
        PlayerEvents.SearchToyAborted -= OnSearchToyAborted;
        PlayerEvents.IdlingTesla -= OnIdlingTesla;
        PlayerEvents.IdledTesla -= OnIdledTesla;
        PlayerEvents.InteractingDoor -= OnInteractingDoor;
        PlayerEvents.InteractedDoor -= OnInteractedDoor;
        PlayerEvents.InteractingElevator -= OnInteractingElevator;
        PlayerEvents.InteractedElevator -= OnInteractedElevator;
        PlayerEvents.InteractingGenerator -= OnInteractingGenerator;
        PlayerEvents.InteractedGenerator -= OnInteractedGenerator;
        PlayerEvents.OpeningGenerator -= OnOpeningGenerator;
        PlayerEvents.OpenedGenerator -= OnOpenedGenerator;
        PlayerEvents.ActivatingGenerator -= OnActivatingGenerator;
        PlayerEvents.ActivatedGenerator -= OnActivatedGenerator;
        PlayerEvents.DeactivatingGenerator -= OnDeactivatingGenerator;
        PlayerEvents.DeactivatedGenerator -= OnDeactivatedGenerator;
        PlayerEvents.UnlockingGenerator -= OnUnlockingGenerator;
        PlayerEvents.UnlockedGenerator -= OnUnlockedGenerator;
        PlayerEvents.ClosingGenerator -= OnClosingGenerator;
        PlayerEvents.ClosedGenerator -= OnClosedGenerator;
        PlayerEvents.InteractingLocker -= OnInteractingLocker;
        PlayerEvents.InteractedLocker -= OnInteractedLocker;
        PlayerEvents.InteractingScp330 -= OnInteractingScp330;
        PlayerEvents.InteractedScp330 -= OnInteractedScp330;
        PlayerEvents.InteractingShootingTarget -= OnInteractingShootingTarget;
        PlayerEvents.InteractedShootingTarget -= OnInteractedShootingTarget;
        PlayerEvents.PlacingBlood -= OnPlacingBlood;
        PlayerEvents.PlacedBlood -= OnPlacedBlood;
        PlayerEvents.PlacingBulletHole -= OnPlacingBulletHole;
        PlayerEvents.PlacedBulletHole -= OnPlacedBulletHole;
        PlayerEvents.SpawningRagdoll -= OnSpawningRagdoll;
        PlayerEvents.SpawnedRagdoll -= OnSpawnedRagdoll;
        PlayerEvents.UnlockingWarheadButton -= OnUnlockingWarheadButton;
        PlayerEvents.UnlockedWarheadButton -= OnUnlockedWarheadButton;
        PlayerEvents.ChangedSpectator -= OnChangedSpectator;
        PlayerEvents.EnteringHazard -= OnEnteringHazard;
        PlayerEvents.EnteredHazard -= OnEnteredHazard;
        PlayerEvents.StayingInHazard -= OnStayingInHazard;
        PlayerEvents.LeavingHazard -= OnLeavingHazard;
        PlayerEvents.LeftHazard -= OnLeftHazard;
        PlayerEvents.ValidatedVisibility -= OnValidatedVisibility;
        PlayerEvents.Jumped -= OnJumped;
        PlayerEvents.MovementStateChanged -= OnMovementStateChanged;
        PlayerEvents.ChangingAttachments -= OnChangingAttachments;
        PlayerEvents.ChangedAttachments -= OnChangedAttachments;
        PlayerEvents.SendingAttachmentsPrefs -= OnSendingAttachmentsPrefs;
        PlayerEvents.SentAttachmentsPrefs -= OnSentAttachmentsPrefs;
        PlayerEvents.InteractingWarheadLever -= OnInteractingWarheadLever;
        PlayerEvents.InteractedWarheadLever -= OnInteractedWarheadLever;
        PlayerEvents.SpinningRevolver -= OnSpinningRevolver;
        PlayerEvents.SpinnedRevolver -= OnSpinnedRevolver;
        PlayerEvents.ToggledDisruptorFiringMode -= OnToggledDisruptorFiringMode;
        PlayerEvents.ChangedBadgeVisibility -= OnChangedBadgeVisibility;
        PlayerEvents.ChangingBadgeVisibility -= OnChangingBadgeVisibility;
        PlayerEvents.ProcessingJailbirdMessage -= OnProcessingJailbirdMessage;
        PlayerEvents.ProcessedJailbirdMessage -= OnProcessedJailbirdMessage;
        PlayerEvents.InspectingKeycard -= OnInspectingKeycard;
        PlayerEvents.InspectedKeycard -= OnInspectedKeycard;
        PlayerEvents.RoomChanged -= OnRoomChanged;
        PlayerEvents.ZoneChanged -= OnZoneChanged;
        PlayerEvents.RaPlayerListAddedPlayer -= OnRaPlayerListAddedPlayer;
        PlayerEvents.RaPlayerListAddingPlayer -= OnRaPlayerListAddingPlayer;
        PlayerEvents.ReceivedAchievement -= OnReceivedAchievement;
        PlayerEvents.RequestedRaPlayerInfo -= OnRequestedRaPlayerInfo;
        PlayerEvents.RequestingRaPlayerInfo -= OnRequestingRaPlayerInfo;
        PlayerEvents.RequestedCustomRaInfo -= OnRequestedCustomRaInfo;
        PlayerEvents.RequestedRaPlayerList -= OnRequestedRaPlayerList;
        PlayerEvents.RequestingRaPlayerList -= OnRequestingRaPlayerList;
        PlayerEvents.RequestedRaPlayersInfo -= OnRequestedRaPlayersInfo;
        PlayerEvents.RequestingRaPlayersInfo -= OnRequestingRaPlayersInfo;
    }

    // mod.on(Events.Player, "Joined", function(ev))
    // mod.on(Events.Player.Left, function(ev))
    // Events.Player.Left.add(function(ev))
    /*
     * lua
     * mod = New.Module("MyModule")
     *
     * function mod.load()
     *  Events.Player.Joined.add(mod.onJoined)
     * end
     *
     * function mod.onJoined(ev)
     *  print("Player joined: " .. ev.Player.DisplayName)
     *  ev.Player.Broadcast(5, "Welcome to the server!")
     *  AdminToys.Text(ev.Player.Position, "Welcome to the server!")
     * end
     */
}