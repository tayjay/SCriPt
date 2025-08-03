# Events

Events can be subscribed to to run code when a certain action is taken in game. To use this, you need to create a function that will accept the arguments given by the event
```lua
function MyFunction(args) -- If the event table below has values
    -- Do something
end

function MyFunction() -- If the event table below has no values
    -- Do something
end
```
Then you can subscribe/unsubscribed to the event like so
```lua
Events.Item.KeycardInteracting:add(MyFunction)
Events.Item.KeycardInteracting:remove(MyFunction)
```
When the event is triggered, your function will be called. Information on what arguments are passed to the function can be found in the event's documentation below.

Some events have an alias associated. For example, `Events.Scp049...` can also be accessed as `Events.Doctor...`

Some events have an `IsAllowed` argument. If you set this to false, the action will be blocked. This is useful for preventing certain actions from happening.
Events that don't include this variable are usuall called after the event has occured.
You can usually tell by the 'tense' of the event name.

# **Event Documentation**

## **Player**

### **Joined**

A player has joined the server.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |

### **Left**

A player has left the server.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |

### **ReceivingVoiceMessage**

The server is receiving a voice packet from a player.

| **Type**        | **Name**  |
|-----------------|-----------|
| *Player*        | Player    |
| *Player*        | Sender    |
| *VoiceMessage* | Message   |
| *bool*          | IsAllowed |

### **SendingVoiceMessage**

The server is sending a voice packet to a player.

| **Type**        | **Name**  |
|-----------------|-----------|
| *Player*        | Player    |
| *VoiceMessage* | Message   |
| *bool*          | IsAllowed |

### **PreAuthenticating**

A player is about to be authenticated. This is called before the player
is added to the server.

| **Type**                  | **Name**            |
|---------------------------|---------------------|
| *bool*                    | CanJoin             |
| *string*                  | UserId              |
| *string*                  | IpAddress           |
| *long*                    | Expiration          |
| *CentralAuthPreauthFlags* | Flags               |
| *string*                  | Region              |
| *byte\[\]*                | Signature           |
| *ConnectionRequest*       | ConnectionRequest   |
| *int*                     | ReaderStartPosition |
| *bool*                    | IsAllowed           |
| *bool*                    | ForceReject         |
| *NetDataWriter*           | CustomReject        |

### **PreAuthenticated**

A player has been successfully pre-authenticated.

| **Type**                  | **Name**            |
|---------------------------|---------------------|
| *string*                  | UserId              |
| *string*                  | IpAddress           |
| *long*                    | Expiration          |
| *CentralAuthPreauthFlags* | Flags               |
| *string*                  | Region              |
| *byte\[\]*                | Signature           |
| *ConnectionRequest*       | ConnectionRequest   |
| *int*                     | ReaderStartPosition |

### **UsingIntercom**

A player is starting to use the intercom.

| **Type**        | **Name**  |
|-----------------|-----------|
| *Player*        | Player    |
| *IntercomState* | State     |
| *bool*          | IsAllowed |

### **UsedIntercom**

A player has used the intercom.

| **Type**        | **Name** |
|-----------------|----------|
| *Player*        | Player   |
| *IntercomState* | State    |

### **Banning**

A player is about to be banned.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *string* | PlayerId  |
| *Player* | Issuer    |
| *string* | Reason    |
| *long*   | Duration  |
| *bool*   | IsAllowed |

### **Banned**

A player has been banned.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *string* | PlayerId |
| *Player* | Issuer   |
| *string* | Reason   |
| *long*   | Duration |

### **Kicking**

A player is about to be kicked.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Player* | Issuer    |
| *string* | Reason    |
| *bool*   | IsAllowed |

### **Kicked**

A player has been kicked.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Player* | Issuer   |
| *string* | Reason   |

### **Muting**

A player is about to be muted.

| **Type** | **Name**   |
|----------|------------|
| *Player* | Player     |
| *Player* | Issuer     |
| *bool*   | IsIntercom |
| *bool*   | IsAllowed  |

### **Muted**

A player has been muted.

| **Type** | **Name**   |
|----------|------------|
| *Player* | Player     |
| *Player* | Issuer     |
| *bool*   | IsIntercom |

### **Unmuting**

A player is about to be unmuted.

| **Type** | **Name**   |
|----------|------------|
| *Player* | Player     |
| *Player* | Issuer     |
| *bool*   | IsIntercom |
| *bool*   | IsAllowed  |

### **Unmuted**

A player has been unmuted.

| **Type** | **Name**   |
|----------|------------|
| *Player* | Player     |
| *Player* | Issuer     |
| *bool*   | IsIntercom |

### **ReportingCheater**

A player is reporting another player for cheating.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Player* | Target    |
| *string* | Reason    |
| *bool*   | IsAllowed |

### **ReportedCheater**

A player has reported another player for cheating.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Player* | Target   |
| *string* | Reason   |

### **ReportingPlayer**

A player is reporting another player.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Player* | Target    |
| *string* | Reason    |
| *bool*   | IsAllowed |

### **ReportedPlayer**

A player has reported another player.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Player* | Target   |
| *string* | Reason   |

### **TogglingNoclip**

A player is attempting to toggle noclip.

| **Type** | **Name**       |
|----------|----------------|
| *Player* | Player         |
| *bool*   | NewNoclipState |
| *bool*   | IsAllowed      |

### **ToggledNoclip**

A player has toggled noclip.

| **Type** | **Name**     |
|----------|--------------|
| *Player* | Player       |
| *bool*   | IsNoclipping |

### **ChangingNickname**

A player is changing their nickname.

| **Type** | **Name**    |
|----------|-------------|
| *Player* | Player      |
| *string* | OldNickname |
| *string* | NewNickname |
| *bool*   | IsAllowed   |

### **ChangedNickname**

A player has changed their nickname.

| **Type** | **Name**    |
|----------|-------------|
| *Player* | Player      |
| *string* | OldNickname |
| *string* | NewNickname |

### **GroupChanging**

A player\'s group is about to be changed.

| **Type**    | **Name**  |
|-------------|-----------|
| *Player*    | Player    |
| *UserGroup* | Group     |
| *bool*      | IsAllowed |

### **GroupChanged**

A player\'s group has been changed.

| **Type**    | **Name** |
|-------------|----------|
| *Player*    | Player   |
| *UserGroup* | Group    |

### **Hurting**

A player is about to be hurt.

| **Type**            | **Name**      |
|---------------------|---------------|
| *Player*            | Player        |
| *Player*            | Attacker      |
| *DamageHandlerBase* | DamageHandler |
| *bool*              | IsAllowed     |

### **Hurt**

A player has been hurt.

| **Type**            | **Name**      |
|---------------------|---------------|
| *Player*            | Player        |
| *Player*            | Attacker      |
| *DamageHandlerBase* | DamageHandler |

### **Dying**

A player is about to die.

| **Type**            | **Name**      |
|---------------------|---------------|
| *Player*            | Player        |
| *Player*            | Attacker      |
| *DamageHandlerBase* | DamageHandler |
| *bool*              | IsAllowed     |

### **Death**

A player has died.

| **Type**            | **Name**          |
|---------------------|-------------------|
| *Player*            | Player            |
| *Player*            | Attacker          |
| *DamageHandlerBase* | DamageHandler     |
| *RoleTypeId*        | OldRole           |
| *Vector3*           | OldPosition       |
| *Vector3*           | OldVelocity       |
| *Quaternion*        | OldCameraRotation |

### **Spawning**

A player is about to be spawned.

| **Type**         | **Name**           |
|------------------|--------------------|
| *Player*         | Player             |
| *PlayerRoleBase* | Role               |
| *bool*           | UseSpawnPoint      |
| *Vector3*        | SpawnLocation      |
| *float*          | HorizontalRotation |
| *bool*           | IsAllowed          |

### **Spawned**

A player has been spawned.

| **Type**         | **Name**           |
|------------------|--------------------|
| *Player*         | Player             |
| *PlayerRoleBase* | Role               |
| *bool*           | UseSpawnPoint      |
| *Vector3*        | SpawnLocation      |
| *float*          | HorizontalRotation |

### **ChangingRole**

A player\'s role is about to be changed.

| **Type**           | **Name**     |
|--------------------|--------------|
| *Player*           | Player       |
| *PlayerRoleBase*   | OldRole      |
| *RoleTypeId*       | NewRole      |
| *RoleChangeReason* | ChangeReason |
| *RoleSpawnFlags*   | SpawnFlags   |
| *bool*             | IsAllowed    |

### **ChangedRole**

A player\'s role has been changed.

| **Type**           | **Name**     |
|--------------------|--------------|
| *Player*           | Player       |
| *RoleTypeId*       | OldRole      |
| *PlayerRoleBase*   | NewRole      |
| *RoleChangeReason* | ChangeReason |
| *RoleSpawnFlags*   | SpawnFlags   |

### **Cuffing**

A player is about to cuff another player.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Player* | Target    |
| *bool*   | IsAllowed |

### **Cuffed**

A player has cuffed another player.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Player* | Target   |

### **Uncuffing**

A player is about to uncuff another player.

| **Type** | **Name**         |
|----------|------------------|
| *Player* | Player           |
| *Player* | Target           |
| *bool*   | CanUnDetainAsScp |
| *bool*   | IsAllowed        |

### **Uncuffed**

A player has uncuffed another player.

| **Type** | **Name**         |
|----------|------------------|
| *Player* | Player           |
| *Player* | Target           |
| *bool*   | CanUnDetainAsScp |

### **InteractingDoor**

A player is interacting with a door.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Door*   | Door      |
| *bool*   | CanOpen   |
| *bool*   | IsAllowed |

### **InteractedDoor**

A player has interacted with a door.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Door*   | Door     |
| *bool*   | CanOpen  |

### **InteractingLocker**

A player is interacting with a locker.

| **Type**        | **Name**  |
|-----------------|-----------|
| *Player*        | Player    |
| *Locker*        | Locker    |
| *LockerChamber* | Chamber   |
| *bool*          | CanOpen   |
| *bool*          | IsAllowed |

### **InteractedLocker**

A player has interacted with a locker.

| **Type**        | **Name** |
|-----------------|----------|
| *Player*        | Player   |
| *Locker*        | Locker   |
| *LockerChamber* | Chamber  |
| *bool*          | CanOpen  |

### **InteractingElevator**

A player is interacting with an elevator.

| **Type**        | **Name**  |
|-----------------|-----------|
| *Player*        | Player    |
| *Elevator*      | Elevator  |
| *ElevatorPanel* | Panel     |
| *bool*          | IsAllowed |

### **InteractedElevator**

A player has interacted with an elevator.

| **Type**        | **Name** |
|-----------------|----------|
| *Player*        | Player   |
| *Elevator*      | Elevator |
| *ElevatorPanel* | Panel    |

### **Escaping**

A player is attempting to escape.

| **Type**             | **Name**       |
|----------------------|----------------|
| *Player*             | Player         |
| *RoleTypeId*         | NewRole        |
| *EscapeScenarioType* | EscapeScenario |
| *bool*               | IsAllowed      |

### **Escaped**

A player has escaped.

| **Type**             | **Name**           |
|----------------------|--------------------|
| *Player*             | Player             |
| *RoleTypeId*         | NewRole            |
| *EscapeScenarioType* | EscapeScenarioType |

## **Scp049/Doctor**

### **StartingResurrection**

SCP-049 is starting to resurrect a body.

| **Type**  | **Name**     |
|-----------|--------------|
| *bool*    | CanResurrect |
| *Ragdoll* | Ragdoll      |
| *Player*  | Target       |
| *Player*  | Player       |
| *bool*    | IsAllowed    |

### **ResurrectingBody**

SCP-049 is resurrecting a body.

| **Type**  | **Name**  |
|-----------|-----------|
| *Ragdoll* | Ragdoll   |
| *Player*  | Target    |
| *Player*  | Player    |
| *bool*    | IsAllowed |

### **ResurrectedBody**

SCP-049 has resurrected a body.

| **Type** | **Name** |
|----------|----------|
| *Player* | Target   |
| *Player* | Player   |

### **UsingDoctorsCall**

SCP-049 is using its \"Doctor\'s Call\" ability.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *bool*   | IsAllowed |

### **UsedDoctorsCall**

SCP-049 has used its \"Doctor\'s Call\" ability.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |

### **UsingSense**

SCP-049 is using its sense ability.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Target    |
| *Player* | Player    |
| *bool*   | IsAllowed |

### **UsedSense**

SCP-049 has used its sense ability.

| **Type** | **Name** |
|----------|----------|
| *Player* | Target   |
| *Player* | Player   |

## **Scp0492/Zombie**

### **StartingConsumingCorpse**

SCP-049-2 is starting to consume a corpse.

| **Type**       | **Name**  |
|----------------|-----------|
| *ConsumeError* | Error     |
| *Player*       | Player    |
| *Ragdoll*      | Ragdoll   |
| *bool*         | IsAllowed |

### **StartedConsumingCorpse**

SCP-049-2 has started to consume a corpse.

| **Type**  | **Name** |
|-----------|----------|
| *Player*  | Player   |
| *Ragdoll* | Ragdoll  |

### **ConsumingCorpse**

SCP-049-2 is consuming a corpse.

| **Type**  | **Name**                 |
|-----------|--------------------------|
| *float*   | HealAmount               |
| *bool*    | AddToConsumedRagdollList |
| *bool*    | HealIfAlreadyConsumed    |
| *Player*  | Player                   |
| *Ragdoll* | Ragdoll                  |
| *bool*    | IsAllowed                |

### **ConsumedCorpse**

SCP-049-2 has consumed a corpse.

| **Type**  | **Name** |
|-----------|----------|
| *Player*  | Player   |
| *Ragdoll* | Ragdoll  |

## **Scp079/Computer/Camera**

### **BlackingOutRoom**

SCP-079 is blacking out a room.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Room*   | Room      |
| *bool*   | IsAllowed |

### **BlackedOutRoom**

SCP-079 has blacked out a room.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Room*   | Room     |

### **BlackingOutZone**

SCP-079 is blacking out a zone.

| **Type**       | **Name**  |
|----------------|-----------|
| *Player*       | Player    |
| *FacilityZone* | Zone      |
| *bool*         | IsAllowed |

### **BlackedOutZone**

SCP-079 has blacked out a zone.

| **Type**       | **Name** |
|----------------|----------|
| *Player*       | Player   |
| *FacilityZone* | Zone     |

### **ChangingCamera**

SCP-079 is changing its camera.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Camera* | Camera    |
| *bool*   | IsAllowed |

### **ChangedCamera**

SCP-079 has changed its camera.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Camera* | Camera   |

### **CancellingRoomLockdown**

SCP-079 is cancelling a room lockdown.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Room*   | Room      |
| *bool*   | IsAllowed |

### **CancelledRoomLockdown**

SCP-079 has cancelled a room lockdown.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Room*   | Room     |

### **GainingExperience**

SCP-079 is gaining experience.

| **Type**               | **Name**  |
|------------------------|-----------|
| *Player*               | Player    |
| *float*                | Amount    |
| *Scp079HudTranslation* | Reason    |
| *bool*                 | IsAllowed |

### **GainedExperience**

SCP-079 has gained experience.

| **Type**               | **Name** |
|------------------------|----------|
| *Player*               | Player   |
| *float*                | Amount   |
| *Scp079HudTranslation* | Reason   |

### **LevelingUp**

SCP-079 is leveling up.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *int*    | Tier      |
| *bool*   | IsAllowed |

### **LeveledUp**

SCP-079 has leveled up.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *int*    | Tier     |

### **LockingDoor**

SCP-079 is locking a door.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Door*   | Door      |
| *bool*   | IsAllowed |

### **LockedDoor**

SCP-079 has locked a door.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Door*   | Door     |

### **LockingDownRoom**

SCP-079 is locking down a room.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Room*   | Room      |
| *bool*   | IsAllowed |

### **LockedDownRoom**

SCP-079 has locked down a room.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Room*   | Room     |

### **Recontaining**

SCP-079 is being recontained.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Player* | Activator |
| *bool*   | IsAllowed |

### **Recontained**

SCP-079 has been recontained.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Player* | Activator |

### **UnlockingDoor**

SCP-079 is unlocking a door.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Door*   | Door      |
| *bool*   | IsAllowed |

### **UnlockedDoor**

SCP-079 has unlocked a door.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Door*   | Door     |

### **UsingTesla**

SCP-079 is using a tesla gate.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Tesla*  | Tesla     |
| *bool*   | IsAllowed |

### **UsedTesla**

SCP-079 has used a tesla gate.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Tesla*  | Tesla    |

### **Pinging**

SCP-079 is pinging a location.

| **Type**         | **Name**  |
|------------------|-----------|
| *Player*         | Player    |
| *Vector3*        | Position  |
| *Vector3*        | Normal    |
| *Scp079PingType* | PingType  |
| *bool*           | IsAllowed |

### **Pinged**

SCP-079 has pinged a location.

| **Type**         | **Name** |
|------------------|----------|
| *Player*         | Player   |
| *Vector3*        | Position |
| *Vector3*        | Normal   |
| *Scp079PingType* | PingType |

## **Scp096/ShyGuy**

### **AddingTarget**

SCP-096 is adding a target.

| **Type** | **Name**   |
|----------|------------|
| *Player* | Player     |
| *Player* | Target     |
| *bool*   | WasLooking |
| *bool*   | IsAllowed  |

### **AddedTarget**

SCP-096 has added a target.

| **Type** | **Name**   |
|----------|------------|
| *Player* | Player     |
| *Player* | Target     |
| *bool*   | WasLooking |

### **ChangingState**

SCP-096 is changing its state.

| **Type**          | **Name**  |
|-------------------|-----------|
| *Player*          | Player    |
| *Scp096RageState* | State     |
| *bool*            | IsAllowed |

### **ChangedState**

SCP-096 has changed its state.

| **Type**          | **Name** |
|-------------------|----------|
| *Player*          | Player   |
| *Scp096RageState* | State    |

### **Charging**

SCP-096 is charging.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *bool*   | IsAllowed |

### **Charged**

SCP-096 has charged.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |

### **Enraging**

SCP-096 is enraging.

| **Type** | **Name**        |
|----------|-----------------|
| *Player* | Player          |
| *float*  | InitialDuration |
| *bool*   | IsAllowed       |

### **Enraged**

SCP-096 has enraged.

| **Type** | **Name**        |
|----------|-----------------|
| *Player* | Player          |
| *float*  | InitialDuration |

### **PryingGate**

SCP-096 is prying a gate.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Gate*   | Gate      |
| *bool*   | IsAllowed |

### **PriedGate**

SCP-096 has pried a gate.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Gate*   | Gate     |

### **StartCrying**

SCP-096 is starting to cry.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *bool*   | IsAllowed |

### **StartedCrying**

SCP-096 has started crying.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |

### **TryingNotToCry**

SCP-096 is trying not to cry.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *bool*   | IsAllowed |

### **TriedNotToCry**

SCP-096 has tried not to cry.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |

## **Scp106/Larry**

### **ChangingStalkMode**

SCP-106 is changing its stalk mode.

| **Type** | **Name**      |
|----------|---------------|
| *bool*   | IsStalkActive |
| *Player* | Player        |
| *bool*   | IsAllowed     |

### **ChangedStalkMode**

SCP-106 has changed its stalk mode.

| **Type** | **Name**      |
|----------|---------------|
| *bool*   | IsStalkActive |
| *Player* | Player        |

### **ChangingVigor**

SCP-106 is changing its vigor.

| **Type** | **Name**  |
|----------|-----------|
| *float*  | OldValue  |
| *float*  | Value     |
| *Player* | Player    |
| *bool*   | IsAllowed |

### **ChangedVigor**

SCP-106 has changed its vigor.

| **Type** | **Name** |
|----------|----------|
| *float*  | OldValue |
| *float*  | Value    |
| *Player* | Player   |

### **UsingHunterAtlas**

SCP-106 is using its Hunter\'s Atlas.

| **Type**  | **Name**            |
|-----------|---------------------|
| *Player*  | Player              |
| *Vector3* | DestinationPosition |
| *bool*    | IsAllowed           |

### **UsedHunterAtlas**

SCP-106 has used its Hunter\'s Atlas.

| **Type**  | **Name**         |
|-----------|------------------|
| *Player*  | Player           |
| *Vector3* | OriginalPosition |

### **ChangingSubmersionStatus**

SCP-106 is changing its submersion status.

| **Type** | **Name**     |
|----------|--------------|
| *bool*   | IsSubmerging |
| *Player* | Player       |
| *bool*   | IsAllowed    |

### **ChangedSubmersionStatus**

SCP-106 has changed its submersion status.

| **Type** | **Name**     |
|----------|--------------|
| *bool*   | IsSubmerging |
| *Player* | Player       |

### **TeleportingPlayer**

SCP-106 is teleporting a player.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *Player* | Target    |
| *bool*   | IsAllowed |

### **TeleportedPlayer**

SCP-106 has teleported a player.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Player* | Target   |

## **Scp173/Peanut**

### **BreakneckSpeedChanging**

SCP-173 is changing its Breakneck Speed state.

| **Type** | **Name**  |
|----------|-----------|
| *bool*   | Active    |
| *Player* | Player    |
| *bool*   | IsAllowed |

### **BreakneckSpeedChanged**

SCP-173 has changed its Breakneck Speed state.

| **Type** | **Name** |
|----------|----------|
| *bool*   | Active   |
| *Player* | Player   |

### **AddingObserver**

SCP-173 is adding an observer.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Target    |
| *Player* | Player    |
| *bool*   | IsAllowed |

### **AddedObserver**

SCP-173 has added an observer.

| **Type** | **Name** |
|----------|----------|
| *Player* | Target   |
| *Player* | Player   |

### **RemovingObserver**

SCP-173 is removing an observer.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Target    |
| *Player* | Player    |
| *bool*   | IsAllowed |

### **RemovedObserver**

SCP-173 has removed an observer.

| **Type** | **Name** |
|----------|----------|
| *Player* | Target   |
| *Player* | Player   |

### **CreatingTantrum**

SCP-173 is creating a tantrum.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Player    |
| *bool*   | IsAllowed |

### **CreatedTantrum**

SCP-173 has created a tantrum.

| **Type**                     | **Name**        |
|------------------------------|-----------------|
| *TantrumHazard*              | Tantrum         |
| *Player*                     | Player          |
| *TantrumEnvironmentalHazard* | TantrumInstance |

### **PlayingSound**

SCP-173 is playing a sound.

| **Type**        | **Name**  |
|-----------------|-----------|
| *Scp173SoundId* | SoundId   |
| *Player*        | Player    |
| *bool*          | IsAllowed |

### **PlayedSound**

SCP-173 has played a sound.

| **Type**        | **Name** |
|-----------------|----------|
| *Scp173SoundId* | SoundId  |
| *Player*        | Player   |

## **Scp914**

### **Activating**

SCP-914 is activating.

| **Type**            | **Name**    |
|---------------------|-------------|
| *Scp914KnobSetting* | KnobSetting |
| *Player*            | Player      |
| *bool*              | IsAllowed   |

### **Activated**

SCP-914 has been activated.

| **Type**            | **Name**    |
|---------------------|-------------|
| *Scp914KnobSetting* | KnobSetting |
| *Player*            | Player      |

### **KnobChanging**

SCP-914\'s knob setting is changing.

| **Type**            | **Name**       |
|---------------------|----------------|
| *Scp914KnobSetting* | OldKnobSetting |
| *Scp914KnobSetting* | KnobSetting    |
| *Player*            | Player         |
| *bool*              | IsAllowed      |

### **KnobChanged**

SCP-914\'s knob setting has changed.

| **Type**            | **Name**       |
|---------------------|----------------|
| *Scp914KnobSetting* | OldKnobSetting |
| *Scp914KnobSetting* | KnobSetting    |
| *Player*            | Player         |

### **ProcessingPickup**

SCP-914 is processing a pickup.

| **Type**            | **Name**    |
|---------------------|-------------|
| *Vector3*           | NewPosition |
| *Scp914KnobSetting* | KnobSetting |
| *Pickup*            | Pickup      |
| *bool*              | IsAllowed   |

### **ProcessedPickup**

SCP-914 has processed a pickup.

| **Type**            | **Name**    |
|---------------------|-------------|
| *ItemType*          | OldItemType |
| *Vector3*           | NewPosition |
| *Scp914KnobSetting* | KnobSetting |
| *Pickup*            | Pickup      |

### **ProcessingPlayer**

SCP-914 is processing a player.

| **Type**            | **Name**    |
|---------------------|-------------|
| *Vector3*           | NewPosition |
| *Scp914KnobSetting* | KnobSetting |
| *Player*            | Player      |
| *bool*              | IsAllowed   |

### **ProcessedPlayer**

SCP-914 has processed a player.

| **Type**            | **Name**    |
|---------------------|-------------|
| *Vector3*           | NewPosition |
| *Scp914KnobSetting* | KnobSetting |
| *Player*            | Player      |

### **ProcessingInventoryItem**

SCP-914 is processing an inventory item.

| **Type**            | **Name**    |
|---------------------|-------------|
| *Scp914KnobSetting* | KnobSetting |
| *Item*              | Item        |
| *Player*            | Player      |
| *bool*              | IsAllowed   |

### **ProcessedInventoryItem**

SCP-914 has processed an inventory item.

| **Type**            | **Name**    |
|---------------------|-------------|
| *ItemType*          | OldItemType |
| *Scp914KnobSetting* | KnobSetting |
| *Item*              | Item        |
| *Player*            | Player      |

## **Scp939/Dog**

### **Attacking**

SCP-939 is attacking a player.

| **Type** | **Name**  |
|----------|-----------|
| *Player* | Target    |
| *Player* | Player    |
| *float*  | Damage    |
| *bool*   | IsAllowed |

### **Attacked**

SCP-939 has attacked a player.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
| *Player* | Target   |
| *float*  | Damage   |

### **CreatingAmnesticCloud**

SCP-939 is creating an amnestic cloud.

| **Type** | **Name**  |
|----------|-----------|
| *bool*   | IsAllowed |
| *Player* | Player    |

### **CreatedAmnesticCloud**

SCP-939 has created an amnestic cloud.

| **Type**              | **Name**              |
|-----------------------|-----------------------|
| *Player*              | Player                |
| *AmnesticCloudHazard* | AmnesticCloud         |
| *AmnesticCloudHazard* | AmnesticCloudInstance |

### **Lunging**

SCP-939 is lunging.

| **Type**           | **Name**   |
|--------------------|------------|
| *bool*             | IsAllowed  |
| *Player*           | Player     |
| *Scp939LungeState* | LungeState |

### **Lunged**

SCP-939 has lunged.

| **Type**           | **Name**   |
|--------------------|------------|
| *Player*           | Player     |
| *Scp939LungeState* | LungeState |

## **Server**

### **WaitingForPlayers**

The server is waiting for players to join.

*(No arguments)*

### **RoundRestarted**

The round has restarted.

*(No arguments)*

### **RoundEndingConditionsCheck**

The server is checking if the round should end.

| **Type** | **Name** |
|----------|----------|
| *bool*   | CanEnd   |

### **RoundEnding**

The round is about to end.

| **Type**      | **Name**    |
|---------------|-------------|
| *bool*        | IsAllowed   |
| *LeadingTeam* | LeadingTeam |

### **RoundEnded**

The round has ended.

| **Type**      | **Name**    |
|---------------|-------------|
| *LeadingTeam* | LeadingTeam |
| *bool*        | ShowSummary |

### **RoundStarting**

The round is about to start.

| **Type** | **Name**  |
|----------|-----------|
| *bool*   | IsAllowed |

### **RoundStarted**

The round has started.

*(No arguments)*

### **BanIssuing**

A ban is being issued from the server console.

| **Type**     | **Name**   |
|--------------|------------|
| *bool*       | IsAllowed  |
| *BanType*    | BanType    |
| *BanDetails* | BanDetails |

### **BanIssued**

A ban has been issued from the server console.

| **Type**     | **Name**   |
|--------------|------------|
| *BanType*    | BanType    |
| *BanDetails* | BanDetails |

### **BanRevoking**

A ban is being revoked.

| **Type**     | **Name**   |
|--------------|------------|
| *bool*       | IsAllowed  |
| *BanType*    | BanType    |
| *BanDetails* | BanDetails |

### **BanRevoked**

A ban has been revoked.

| **Type**     | **Name**   |
|--------------|------------|
| *BanType*    | BanType    |
| *BanDetails* | BanDetails |

### **BanUpdating**

A ban is being updated.

| **Type**     | **Name**      |
|--------------|---------------|
| *bool*       | IsAllowed     |
| *BanType*    | BanType       |
| *BanDetails* | BanDetails    |
| *BanDetails* | OldBanDetails |

### **BanUpdated**

A ban has been updated.

| **Type**     | **Name**      |
|--------------|---------------|
| *BanType*    | BanType       |
| *BanDetails* | BanDetails    |
| *BanDetails* | OldBanDetails |

### **CommandExecuting**

A command is about to be executed.

| **Type**          | **Name**     |
|-------------------|--------------|
| *bool*            | IsAllowed    |
| *CommandSender*   | Sender       |
| *CommandType*     | CommandType  |
| *bool*            | CommandFound |
| *ICommand*        | Command      |
| *Array* | Arguments    |
| *string*          | CommandName  |

### **CommandExecuted**

A command has been executed.

| **Type**          | **Name**             |
|-------------------|----------------------|
| *CommandSender*   | Sender               |
| *CommandType*     | CommandType          |
| *ICommand*        | Command              |
| *Array* | Arguments            |
| *bool*            | ExecutedSuccessfully |
| *string*          | Response             |
| *string*          | CommandName          |

### **CassieQueuingScpTermination**

A CASSIE announcement for an SCP termination is being queued.

| **Type**           | **Name**      |
|--------------------|---------------|
| *Player*           | Player        |
| *string*           | Announcement  |
| *SubtitlePart\[\]* | SubtitleParts |
| *bool*             | IsAllowed     |

### **CassieQueuedScpTermination**

A CASSIE announcement for an SCP termination has been queued.

| **Type**           | **Name**      |
|--------------------|---------------|
| *Player*           | Player        |
| *string*           | Announcement  |
| *SubtitlePart\[\]* | SubtitleParts |

### **WaveRespawning**

A respawn wave is about to occur.

| **Type**         | **Name**        |
|------------------|-----------------|
| *bool*           | IsAllowed       |
| *RespawnWave*    | Wave            |
| *IEnumerable* | SpawningPlayers |
| *Dictionary*  | Roles           |

### **WaveRespawned**

A respawn wave has occurred.

| **Type**           | **Name** |
|--------------------|----------|
| *RespawnWave*      | Wave     |
| *IReadOnlyList* | Players  |

### **WaveTeamSelecting**

A team is being selected for a respawn wave.

| **Type**            | **Name**  |
|---------------------|-----------|
| *SpawnableWaveBase* | Wave      |
| *bool*              | IsAllowed |

### **WaveTeamSelected**

A team has been selected for a respawn wave.

| **Type**      | **Name** |
|---------------|----------|
| *RespawnWave* | Wave     |

### **LczDecontaminationAnnounced**

An announcement for the Light Containment Zone decontamination has been
made.

| **Type** | **Name** |
|----------|----------|
| *int*    | Phase    |

### **LczDecontaminationStarting**

The Light Containment Zone decontamination is about to start.

| **Type** | **Name**  |
|----------|-----------|
| *bool*   | IsAllowed |

### **LczDecontaminationStarted**

The Light Containment Zone decontamination has started.

*(No arguments)*

### **MapGenerating**

The map is about to be generated.

| **Type** | **Name**  |
|----------|-----------|
| *int*    | Seed      |
| *bool*   | IsAllowed |

### **MapGenerated**

The map has been generated.

| **Type** | **Name** |
|----------|----------|
| *int*    | Seed     |

### **PickupCreated**

A pickup has been created.

| **Type** | **Name** |
|----------|----------|
| *Pickup* | Pickup   |

### **PickupDestroyed**

A pickup has been destroyed.

| **Type** | **Name** |
|----------|----------|
| *Pickup* | Pickup   |

### **SendingAdminChat**

An admin chat message is being sent.

| **Type**        | **Name**  |
|-----------------|-----------|
| *string*        | Message   |
| *CommandSender* | Sender    |
| *bool*          | IsAllowed |

### **SentAdminChat**

An admin chat message has been sent.

| **Type**        | **Name** |
|-----------------|----------|
| *string*        | Message  |
| *CommandSender* | Sender   |

### **ItemSpawning**

An item is about to be spawned.

| **Type**   | **Name**  |
|------------|-----------|
| *ItemType* | ItemType  |
| *bool*     | IsAllowed |

### **ItemSpawned**

An item has been spawned.

| **Type** | **Name** |
|----------|----------|
| *Pickup* | Pickup   |

### **CassieAnnouncing**

A CASSIE announcement is being made.

| **Type** | **Name**           |
|----------|--------------------|
| *string* | Words              |
| *bool*   | MakeHold           |
| *bool*   | MakeNoise          |
| *bool*   | CustomAnnouncement |
| *string* | CustomSubtitles    |
| *bool*   | IsAllowed          |

### **CassieAnnounced**

A CASSIE announcement has been made.

| **Type** | **Name**           |
|----------|--------------------|
| *string* | Words              |
| *bool*   | MakeHold           |
| *bool*   | MakeNoise          |
| *bool*   | CustomAnnouncement |
| *string* | CustomSubtitles    |

### **ProjectileExploding**

A projectile is about to explode.

| **Type**                 | **Name**     |
|--------------------------|--------------|
| *TimedGrenadeProjectile* | TimedGrenade |
| *Player*                 | Player       |
| *Vector3*                | Position     |
| *bool*                   | IsAllowed    |
| *TimedGrenadeProjectile* | Grenade      |

### **ProjectileExploded**

A projectile has exploded.

| **Type**                 | **Name**     |
|--------------------------|--------------|
| *TimedGrenadeProjectile* | TimedGrenade |
| *Player*                 | Player       |
| *Vector3*                | Position     |
| *TimedGrenadeProjectile* | Projectile   |

### **ExplosionSpawning**

An explosion is about to be spawned.

| **Type**           | **Name**      |
|--------------------|---------------|
| *Player*           | Player        |
| *Vector3*          | Position      |
| *ExplosionGrenade* | Settings      |
| *ExplosionType*    | ExplosionType |
| *bool*             | DestroyDoors  |
| *bool*             | IsAllowed     |

### **ExplosionSpawned**

An explosion has been spawned.

| **Type**           | **Name**      |
|--------------------|---------------|
| *Player*           | Player        |
| *Vector3*          | Position      |
| *ExplosionGrenade* | Settings      |
| *ExplosionType*    | ExplosionType |
| *bool*             | DestroyDoors  |

### **GeneratorActivating**

A generator is about to be activated.

| **Type**    | **Name**  |
|-------------|-----------|
| *Generator* | Generator |
| *bool*      | IsAllowed |

### **GeneratorActivated**

A generator has been activated.

| **Type**    | **Name**  |
|-------------|-----------|
| *Generator* | Generator |

## **Warhead**

### **Starting**

The Alpha Warhead detonation sequence is starting.

| **Type**               | **Name**          |
|------------------------|-------------------|
| *bool*                 | IsAutomatic       |
| *bool*                 | SuppressSubtitles |
| *AlphaWarheadSyncInfo* | WarheadState      |
| *Player*               | Player            |
| *bool*                 | IsAllowed         |

### **Started**

The Alpha Warhead detonation sequence has started.

| **Type**               | **Name**          |
|------------------------|-------------------|
| *bool*                 | IsAutomatic       |
| *bool*                 | SuppressSubtitles |
| *AlphaWarheadSyncInfo* | WarheadState      |
| *Player*               | Player            |

### **Stopping**

The Alpha Warhead detonation sequence is being stopped.

| **Type**               | **Name**     |
|------------------------|--------------|
| *AlphaWarheadSyncInfo* | WarheadState |
| *bool*                 | IsAllowed    |
| *Player*               | Player       |

### **Stopped**

The Alpha Warhead detonation sequence has been stopped.

| **Type**               | **Name**     |
|------------------------|--------------|
| *AlphaWarheadSyncInfo* | WarheadState |
| *Player*               | Player       |

### **Detonating**

The Alpha Warhead is about to detonate.

| **Type** | **Name**  |
|----------|-----------|
| *bool*   | IsAllowed |
| *Player* | Player    |

### **Detonated**

The Alpha Warhead has detonated.

| **Type** | **Name** |
|----------|----------|
| *Player* | Player   |
