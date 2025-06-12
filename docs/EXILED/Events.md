Events can be subscribed to in order to run code when a certain action is taken in game. To use this, you need to create a function that will accept the arguments given by the event
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

## Cassie
Events related to the in-game announcer.

`Events.Cassie.*`

### SendingCassieMessage
Called before CASSIE broadcasts an announcement to the facility

| Type | Member   |
| --- |----------|
| string | Words    |
| bool | MakeHold |
| bool | MakeNoise |
| bool | IsAllowed |
---

## Command
Events related to commands being received and executed.

`Events.Command.*`

### RemoteAdminCommand
The server received a command from RemoteAdmin

| Type | Member   |
| --- |----------|
| ICommandSender | Sender |
| string | Command |
| string[] | Arguments |

### RemoteAdminCommandExecuted
The server has processed and is returning the result of a RemoteAdmin command

| Type | Member    |
| --- |-----------|
| ICommandSender | Sender    |
| string | Command   |
| string[] | Arguments |
| bool | Result    |
| string | Response  |

### ConsoleCommand
A local admin command has been received

| Type | Member   |
| --- |----------|
| ICommandSender | Sender |
| string | Command |
| string[] | Arguments |

### ConsoleCommandExecuted
Responding to local admin command

| Type | Member    |
| --- |-----------|
| ICommandSender | Sender    |
| string | Command   |
| string[] | Arguments |
| bool | Result    |
| string | Response  |

### PlayerGameConsoleCommand
A player has sent a command to the server using the `~` terminal

| Type | Member   |
| --- |----------|
| Player | Player |
| string | Command |
| string[] | Arguments |

### PlayerGameConsoleCommandExecuted
The server is responding to the player's `~` command

| Type | Member    |
| --- |-----------|
| Player | Player    |
| string | Command   |
| string[] | Arguments |
| bool | Result    |
| string | Response  |


## Item

`Events.Item.*`

### ChangingAmmo
A player's Ammo count is changing

| Type | Member   |
| --- |----------|
| Player | Player |
| Firearm | Firearm |
| Item | Item |
| byte | OldAmmo |
| byte | NewAmmo |
| bool | IsAllowed |

### ChangingAttachments
A player is attempting to change their weapon attachments

| Type | Member   |
| --- |----------|
| IEnumerable<AttachmentIdentifier> | CurrentAttachmentIdentifiers |
| List<AttachmentIdentifier> | NewAttachmentIdentifiers |
| uint | CurrentCode |
| uint | NewCode |
| bool | IsAllowed |
| Firearm | Firearm |
| Item | Item |
| Player | Player |

### ReceivingPreference
The server has receieved the players prefered attachments

| Type | Member   |
| --- |----------|
| FirearmType | Item |
| IEnumerable<AttachmentIdentifier> | CurrentAttachmentIdentifiers |
| List<AttachmentIdentifier> | NewAttachmentIdentifiers |
| uint | CurrentCode |
| uint | NewCode |
| bool | IsAllowed |
| Player | Player |

### KeycardInteracting
A player is trying to use a Keycard

| Type | Member   |
| --- |----------|
| Pickup | Pickup |
| Player | Player |
| Door | Door |
| bool | IsAllowed |

### Swinging
A player is attempting to swing the Jailbird

| Type | Member   |
| --- |----------|
| Player | Player |
| Item | Item |
| bool | IsAllowed |

### ChargingJailbird
A player is attempting to charge the Jailbird (Right-click)

| Type | Member   |
| --- |----------|
| Player | Player |
| Item | Item |
| bool | IsAllowed |

### UsingRadioPickupBattery
The battery from a radio on the ground is trying to drain

| Type | Member   |
| --- |----------|
| bool | IsAllowed |
| Pickup | Pickup |
| RadioPickup | RadioPickup |
| float | Drain |
---
## Map

`Events.Map.*`

### PlacingBulletHole
Placing a bullet hole decal on the map

| Type | Member   |
| --- |----------|
| Vector3 | Position |
| Quaternion | Rotation |
| bool | IsAllowed |
| Player | Player |
    
### PlacingBlood
Placing blood on the map
    
| Type | Member   |
| --- |----------|
| Player | Player |
| Player | Target |
| Vector3 | Position |
| bool | IsAllowed |
    
### AnnouncingDecontamination
The current state of LCZ Decon is being announced

| Type | Member   |
| --- |----------|
| int | Id |
| DecontaminationState | State |
| DecontaminationController.DecontaminationPhase.PhaseFunction | PhaseFunction |

### AnnouncingScpTermination
CASSIE is attempting to announce an SCP has died.

| Type | Member   |
| --- |----------|
| Role | Role |
| string | TerminationCause |
| Player | Player |
| Player | Attacker |
| CustomDamageHandler | DamageHandler |
| bool | IsAllowed |

### AnnouncingNtfEntrance
CASSIE is attempting to announce NTF has arrived

| Type | Member   |
| --- |----------|
| int | ScpsLeft |
| string | UnitName |
| int | UnitNumber |
| bool | IsAllowed |

### GeneratorActivating
A player is attempting to turn on a generator

| Type | Member   |
| --- |----------|
| Generator | Generator |
| bool | IsAllowed |

### Decontaminating
The map is attempting to decontaminate LCZ. Locking elevators and hurting players in the zone.

| Type | Member   |
| --- |----------|
| bool | IsAllowed |

### ExplodingGrenade
A grenade is attempting to explode.

| Type | Member   |
| --- |----------|
| Vector3 | Position |
| List<Player> | TargetsToAffect |
| EffectGrenadeProjectile | Projectile |
| bool | IsAllowed |
| Player | Player |

### SpawningItem
Trying to spawn an item in the facility

| Type | Member   |
| --- |----------|
| Pickup | Pickup |
| bool | ShouldInitiallySpawn |
| Door | TriggerDoor |
| bool | IsAllowed |

### FillingLocker
Choosing what will be placed in a locker. This is called during facility generation.

| Type | Member   |
| --- |----------|
| Pickup | Pickup |
| LockerChamber | LockerChamber |
| bool | IsAllowed |

### Generated
The facility has completed being built based off the seed. There are no arguments to this event.

| Type | Member   |
| --- |----------|


### ChangingIntoGrenade
A grenade pickup in the facility is trying to arm. 

| Type | Member   |
| --- |----------|
| Pickup | Pickup |
| ItemType | Type |
| bool | IsAllowed |

### ChangedIntoGrenade
A grenade pickup in the facility has been armed.

| Type | Member   |
| --- |----------|
| GrenadePickup | Pickup |
| Projectile | Projectile |
| double | FuseTime (DEPRICATED) |

### TurningOffLights
Attempting to turn off all lights in a room

| Type | Member   |
| --- |----------|
| RoomLightController | RoomLightController |
| float | Duration |
| bool | IsAllowed |

### PickupAdded
A pickup has been added to the facility

| Type | Member   |
| --- |----------|
| Pickup | Pickup |

### PickupDestroyed
A pickup has been removed from the facility
    
| Type | Member   |
| --- |----------|
| Pickup | Pickup |

### SpawningTeamVehicle
Attempting to spawn the Helicoptor or Choas car.

| Type | Member   |
| --- |----------|
| SpawnableTeamType | Team |
| bool | IsAllowed |
---
## Player

`Events.Player.*`

### PreAuthenticating
The player has attempted to connect to the server

| Type | Member   |
| --- |----------|
| string | UserId |
| string | IpAddress |
| long | Expiration |
| CentralAuthPreauthFlags | Flags |
| string | Country |
| byte[] | Signature |
| int | ReaderStartPosition |
| ConnectionRequest | Request |
| bool | IsAllowed |

### ReservedSlot
Checking if a player has a reserved slot on the server based on their UserId (#####@steam for example)

| Type | Member   |
| --- |----------|
| string | UserId |
| bool | HasReservedSlot |

### Kicking
Attempting to kick a player from the server

| Type | Member   |
| --- |----------|
| Player | Target |
| string | Reason |
| string | FullMessage |
| bool | IsAllowed |

### Kicked
A player has been kicked

| Type | Member   |
| --- |----------|
| Player | Player |
| string | Reason |
    
### Banning
Attempting to ban a player from the server
    
| Type | Member   |
| --- |----------|
| long | Duration |
| Player | Target |
| string | Reason |
| string | FullMessage |
| bool | IsAllowed |
| Player | Player |

### Banned
A player has been banned

| Type | Member   |
| --- |----------|
| Player | Target |
| Player | Player |
| BanDetails | Details |
| BanHandler.BanType | Type |
| bool | IsForced |
    
### EarningAchievement
Trying to give a player an achivement

| Type | Member   |
| --- |----------|
| AchievementName | AchievementName |
| bool | IsAllowed |
| Player | Player |

### UsingItem
A player is trying to use an item

| Type | Member   |
| --- |----------|
| Usable | Usable |
| Item | Item |
| Player | Player |
| float | Cooldown |
| bool | IsAllowed |

### UsingItemCompleted
A player is on the last frame of using an item

| Type | Member   |
| --- |----------|
| Usable | Usable |
| Item | Item |
| Player | Player |
| bool | IsAllowed |

### UsedItem
A player has finished using an item.

| Type | Member   |
| --- |----------|
| Usable | Usable |
| Item | Item |
| Player | Player |


### CancellingItemUse
A player has attempted to stop using an item.

| Type | Member   |
| --- |----------|
| Usable | Usable |
| Item | Item |
| Player | Player |
| bool | IsAllowed |

### CancelledItemUse
A player has stopped using an item.

| Type | Member   |
| --- |----------|
| Usable | Usable |
| Item | Item |
| Player | Player |

### Interacted
A player has interacted with a door or object in the facility

| Type | Member   |
| --- |----------|
| Player | Player |

### SpawnedRagdoll
A ragdoll of a decesed player has been spawned.

| Type | Member   |
| --- |----------|
| Vector3 | Position |
| Quaternion | Rotation |
| RoleTypeId | Role |
| double | CreationTime |
| string | Nickname |
| RagdollData | Info |
| DamageHandlerBase | DamageHandlerBase |
| Ragdoll | Ragdoll |
| Player | Player |

### ActivatingWarheadPanel
A player is pressing the warhead button on surface

| Type | Member   |
| --- |----------|
| Player | Player |
| bool | IsAllowed |

### ActivatingWorkstation
A player is trying to turn on a weapon workstation

| Type | Member   |
| --- |----------|
| Player | Player |
| WorkstationController | WorkstationController |
| WorkstationController.WorkstationStatus| Status |
| bool | IsAllowed |

### DeactivatingWorkstation
A player has walked away from a weapon workstation

| Type | Member   |
| --- |----------|
| Player | Player |
| WorkstationController | WorkstationController |
| WorkstationController.WorkstationStatus| Status |
| bool | IsAllowed |

### Joined
A player has joined the server

| Type | Member   |
| --- |----------|
| Player | Player |

### Verified
A player's authentication has been verified by the central server

| Type | Member   |
| --- |----------|
| Player | Player |

### Left
A player has left the server

| Type | Member   |
| --- |----------|
| Player | Player |

### Destroying
A player's instance on the server has been destroyed following their departure

| Type | Member   |
| --- |----------|
| Player | Player |

### Hurting
A player is attempting to attack another player.

| Type | Member   |
| --- |----------|
| Player | Player   |
| Player | Attacker |
| float | Amount |
| CustomDamageHandler | DamageHandler |
| bool | IsAllowed |

### Hurt
A player has attacked and hurt another player

| Type | Member   |
| --- |----------|
| Player | Player   |
| Player | Attacker |
| float | Amount |
| PlayerStatsSystem.DamageHandlerBase.HandlerOutput | HandlerOutput |
| CustomDamageHandler | DamageHandler |

### Dying
A player is about to die.

| Type | Member                     |
| --- |----------------------------|
| List<Item> | ItemsToDrop (Set Obsolete) |
| Player | Player                   |
| Player | Attacker                 |
| CustomDamageHandler | DamageHandler |
| bool | IsAllowed                 |

### Died
A player has died.

| Type | Member   |
| --- |----------|
| Player | Player   |
| Player | Attacker |
| RoleTypeId | TargetOldRole |
| CustomDamageHandler | DamageHandler |

### ChangingRole
A player is changing to a new role. **Careful**: if you plan to change this, only change `NewRole`. Changing the role of the player directly will result in an infinte loop.

| Type       | Member   |
|------------|----------|
| Player     | Player |
| RoleTypeId | NewRole |
| SpawnReason | Reason |
| List<ItemType> | Items |
| Dictionary<ItemType, short> | Ammo |
| bool | ShouldPreserveInventory |
| RoleSpawnFlags | SpawnFlags |
| bool | IsAllowed |

### ThrownProjectile
A player has thrown a projectile (Grenade, Throwable SCPs, etc.)

| Type | Member   |
| --- |----------|
| Player | Player |
| Throwable | Throwable |
| Item | Item |
| Pickup | Pickup |

### ThrowingRequest
A player has sent a throwing request to the server. This could include starting, canceling, or throwing. Modifying this value will desync the client.

| Type | Member   |
| --- |----------|
| Player | Player |
| Throwable | Throwable |
| Item | Item |
| ThrowRequest | RequestType |

### DroppingItem
A player is trying to drop an item from their inventory

| Type | Member   |
| --- |----------|
| Player | Player |
| Item | Item |
| bool | IsAllowed |
| bool | IsThrown |

### DroppedItem
A player has dropped an item from their inventory

| Type | Member   |
| --- |----------|
| Player | Player |
| Pickup | Pickup |
| bool | WasThrown |

### DroppingNothing
A player has dropped nothing, whatever that means.

| Type | Member   |
| --- |----------|
| Player | Player |

### PickingUpItem
A player is trying to pickup an item from the facility.

| Type | Member   |
| --- |----------|
| Player | Player |
| Pickup | Pickup |
| bool | IsAllowed |

### Handcuffing
An player is attempting to handcuff another player.

| Type | Member   |
| --- |----------|
| Player | Player |
| Player | Target |
| bool | IsAllowed |

### RemovingHandcuffs
A player is attempting to remove the handcuffs from another player

| Type | Member   |
| --- |----------|
| Player | Player |
| Player | Target |
| bool | IsAllowed |

### IntercomSpeaking
A player is trying to talk on the intercom in Entrance.

| Type | Member   |
| --- |----------|
| Player | Player |
| bool | IsAllowed |

### Shot
A player has shot their gun

| Type | Member   |
| --- |----------|
| Player | Player |
| Firearm | Firearm |
| Item | Item |
| HitboxIdentity | Hitbox |
| float | Damage |
| float | Distance |
| Vector3 | Position |
| RaycastHit | RaycastHit |
| Player | Target |
| bool | CanHurt |

### Shooting
A player is about to shoot their gun

| Type | Member   |
| --- |----------|
| Player | Player |
| Firearm | Firearm |
| Item | Item |
| ShotMessage | ShotMessage |
| uint | TargetNetId |
| bool | IsAllowed |


### EnteringPocketDimension
Scp106 is attempting to teleport a player to his Pocket Dimension

| Type | Member   |
| --- |----------|
| Player | Player |
| bool | IsAllowed |
| Player | Scp106 |

### EscapingPocketDimension
A player is successfully escapting the Pocket Dimension.

| Type | Member   |
| --- |----------|
| Player | Player |
| Vector3 | TeleportPosition |
| bool | IsAllowed |

### FailingEscapePocketDimension
A player is unsuccessfully escaping the Pocket Dimension

| Type | Member   |
| --- |----------|
| Player | Player |
| bool | IsAllowed |
| PocketDimensionTeleport | Teleporter |

### EnteringKillerCollision
A player has entered a kill cude/plane

| Type | Member   |
| --- |----------|
| Player | Player |
| bool | IsAllowed |

### ReloadingWeapon
A player is trying to reload their gun

| Type | Member   |
| --- |----------|
| Player | Player |
| Firearm | Firearm |
| Item | Item |
| bool | IsAllowed |

### Spawning
A player is attempting to spawn into the facility

| Type | Member   |
| --- |----------|
| Player | Player |
| Role | OldRole |
| Vector3 | Position |
| float | HorizontalRotation |

### Spawned
A player has been spawned into the faciliity

| Type | Member   |
| --- |----------|
| Player | Player |
| Role | OldRole |
| SpawnReason | Reason |
| RoleSpawnFlags | SpawnFlags |

### ChangedItem
A player is trying to change their held item

| Type | Member   |
| --- |----------|
| Player | Player |
| Item | Item |
| Item | OldItem |

### ChangingItem
A player is trying to change their held item

| Type | Member   |
| --- |----------|
| Player | Player |
| Item | Item |
| bool | IsAllowed |

### ChangingGroup
Attempting to change the user group a player is in.

| Type | Member   |
| --- |----------|
| Player | Player |
| UserGroup | NewGroup |
| bool | IsAllowed |

### InteractingDoor
A player is trying to interact (Open/Close/Unlock) with a door.

| Type | Member   |
| --- |----------|
| Player | Player |
| Door | Door |
| bool | IsAllowed |

### InteractingElevator
A player is trying to interact (Call/Send) an elevator

| Type | Member   |
| --- |----------|
| Player | Player |
| ElevatorChamber | Elevator |
| Lift | Lift |
| bool | IsAllowed |

### InteractingLocker
A player is attempting to interact (Open/Close/Unlock) a Locker. This includes SCP, Weapon, and standard lockers

| Type | Member   |
| --- |----------|
| Player | Player |
| Locker | Locker |
| LockerChamber | Chamber |
| byte | ChamberId |
| bool | IsAllowed |


### TriggeringTesla
A tesla gate is attempting to shock in the presence of a player

| Type | Member   |
| --- |----------|
| Player | Player |
| TeslaGate | Tesla |
| bool | IsInHurtingRange |
| bool | IsTriggerable |
| bool | IsInIdleRange |
| bool | IsAllowed |
| bool | DisableTesla |

### UnlockingGenerator
A player is unlocking a generator in HCZ

| Type | Member   |
| --- |----------|
| Player | Player |
| Generator | Generator |
| bool | IsAllowed |

### OpeningGenerator
A player is opening the door on a generator

| Type | Member   |
| --- |----------|
| Player | Player |
| Generator | Generator |
| bool | IsAllowed |

### ClosingGenerator
A player is closing the door on a generator

| Type | Member   |
| --- |----------|
| Player | Player |
| Generator | Generator |
| bool | IsAllowed |

### ActivatingGenerator
A player is turning on a generator

| Type | Member   |
| --- |----------|
| Player | Player |
| Generator | Generator |
| bool | IsAllowed |

### StoppingGenerator
A player is turning off a generator

| Type | Member   |
| --- |----------|
| Player | Player |
| Generator | Generator |
| bool | IsAllowed |

### ReceivingEffect
A player is receiving a status effect

| Type | Member   |
| --- |----------|
| Player | Player |
| StatusEffectBase | Effect |
| float | Duration |
| byte | Intensity |
| byte | CurrentIntensity |
| bool | IsAllowed |

### IssuingMute
Attempting to mute a player

| Type | Member   |
| --- |----------|
| Player | Player |
| bool | IsAllowed |
| bool | IsIntercom |

### RevokingMute
Attempting to remove the mute on a player

| Type | Member   |
| --- |----------|
| Player | Player |
| bool | IsAllowed |
| bool | IsIntercom |

### UsingRadioBattery
Attempting to drain the battery of a radio a player is holding

| Type | Member   |
| --- |----------|
| Player | Player |
| Radio | Radio |
| float | Drain |
| bool | IsAllowed |

### ChangingRadioPreset
A player is trying to change the range on their held radio

| Type | Member   |
| --- |----------|
| Player | Player |
| Radio | Radio |
| RadioRange | OldValue |
| RadioRange | NewValue |
| bool | IsAllowed |

"Short", "Medium", "Long", "Ultra"

### UsingMicroHIDEnergy
A player is using a MicroHID and is attempting to drain energy.

| Type | Member   |
| --- |----------|
| Player | Player |
| MicroHID | MicroHID |
| HidState | CurrentState |
| float | Drain |
| bool | IsAllowed |



### DroppingAmmo
A player is trying to drop ammo

| Type | Member   |
| --- |----------|
| Player | Player |
| AmmoType | AmmoType |
| ushort | Amount |
| bool | IsAllowed |


### DroppedAmmo
A player has dropped ammo

| Type               | Member      |
|--------------------|-------------|
| Player             | Player      |
| AmmoType           | AmmoType    |
| ushort             | Amount      |
| List\<AmmoPickup\> | AmmoPickups |

### InteractingShootingTarget
A player is trying to interact with a shooting target

| Type | Member         |
| --- |----------------|
| Player | Player         |
| ShootingTarget | ShootingTarget |
| ShootingTargetButton | TargetButton   |
| bool | IsAllowed       |
| int | maxHp           |
| int | autoResetTime   |

### DamagingShootingTarget
A player is damaing a shooting target

| Type | Member         |
| --- |----------------|
| Player | Player         |
| ShootingTarget | ShootingTarget |
| float | Amount         |
| float | Distance       |
| Item | Item           |
| AttackerDamageHandler | DamageHandler |
| Vector3 | HitLocation  |
| bool | IsAllowed       |

### FlippingCoin
A player is flipping a coin

| Type | Member         |
| --- |----------------|
| Player | Player         |
| Item | Item           |
| bool | IsTails        |
| bool | IsAllowed       |

### TogglingFlashlight
A player has turned on/off their held flashlight

| Type       | Member     |
|------------|------------|
| Player     | Player     |
| Flashlight | Flashlight |
| Item       | Item       |
| bool | NewState   |
| bool | IsAllowed   |

### UnloadingWeapon
A player is trying to unload the clip from their gun (Holding reload)

| Type | Member   |
| --- |----------|
| Player | Player |
| Firearm | Firearm |
| Item | Item |
| bool | IsAllowed |

### AimingDownSight
A player is trying to ADS

| Type | Member   |
| --- |----------|
| Player | Player |
| Firearm | Firearm |
| Item | Item |
| bool | AdsIn |
| bool | AdsOut |

### TogglingWeaponFlashlight
A player is trying to turn on/off the flashlight on their gun, if available.

| Type | Member   |
| --- |----------|
| Player | Player |
| Firearm | Firearm |
| Item | Item |
| bool | IsAllowed |
| bool | NewState |

### DryfiringWeapon
A player is trying to shoot their gun with no ammo

| Type | Member   |
| --- |----------|
| Player | Player |
| Firearm | Firearm |
| Item | Item |
| bool | IsAllowed |

### VoiceChatting
A player is trying to talk in-game

| Type | Member       |
| --- |--------------|
| Player | Player       |
| VoiceMessage | VoiceMessage |
| VoiceModuleBase | VoiceModule  |
| bool | IsAllowed     |

### MakingNoise
A player is making a noise that SCP-939 can see

| Type | Member       |
| --- |--------------|
| Player | Player       |
| float | Distance     |
| bool | IsAllowed     |

### Jumping
A player is trying to Jump

| Type | Member       |
| --- |--------------|
| Player | Player       |
| Vector3 | Direction   |
| float | Speed         |
| bool | IsAllowed     |

### Landing
A player has landed on the ground.

| Type | Member       |
| --- |--------------|
| Player | Player       |

### Transmitting
A player is using a radio to talk to other players.

| Type | Member       |
| --- |--------------|
| Player | Player       |
| VoiceModuleBase | VoiceModule  |
| bool | IsTransmitting |
| bool | IsAllowed     |

### ChangingMoveState
A player is trying to change their Move State (Walking/Running/Sneaking)

| Type | Member       |
| --- |--------------|
| Player | Player       |
| PlayerMovementState | OldState |
| PlayerMovementState | NewState |
| bool | IsAllowed     |

### ChangingSpectatedPlayer
A spectator is changing the player they are spectating. Haven't had luck with preventing or changing this.

| Type | Member       |
| --- |--------------|
| Player | Player       |
| Player | OldTarget    |
| Player | NewTarget    |

### TogglingNoClip
A player has pressed the NoClip keybind. This triggers regardless of player permissions but is blocked if they don't have it. Input can be taken and used for other purposes.

| Type | Member       |
| --- |--------------|
| Player | Player       |
| bool | IsEnabled     |
| bool | IsAllowed     |

### TogglingOverwatch
An admin is changing to the Overwatch role to keep an eye on the game. 

| Type | Member       |
| --- |--------------|
| Player | Player       |
| bool | IsEnabled     |
| bool | IsAllowed     |

### TogglingRadio
A player is trying to turn on/off their Radio

| Type | Member       |
| --- |--------------|
| Player | Player       |
| Item | Item         |
| Radio | Radio       |
| bool | NewState     |
| bool | IsAllowed     |

### SearchingPickup
Called before a player searches a Pickup.

| Type | Member       |
| --- |--------------|
| Player | Player       |
| SearchSession | SearchSession |
| SearchCompletor | SearchCompletor |
| float | SearchTime   |
| bool | IsAllowed     |
| Pickup | Pickup       |


### SendingAdminChatMessage
A player is trying to send a message to the Admin Chat

| Type | Member       |
| --- |--------------|
| Player | Player       |
| string | Message     |
| bool | IsAllowed     |

### PlayerDamageWindow
A player is damaging a window

| Type | Member       |
| --- |--------------|
| Player | Player       |
| Window | Window       |
| DamageHandler | Handler |
| bool | IsAllowed     |
Damage done is stored in Handler.Damage (float)

### DamagingDoor
A player is damaging a door

| Type | Member       |
| --- |--------------|
| Player | Player       |
| Door | Door         |
| float | Damage       |
| bool | IsAllowed     |
| DoorDamageType | DamageType |

### ItemAdded
An item has been added to a player's inventory

| Type | Member       |
| --- |--------------|
| Player | Player       |
| Item | Item         |
| Pickup | Pickup       |

### ItemRemoved
An item has been removed from a player's inventory

| Type | Member       |
| --- |--------------|
| Player | Player       |
| Item | Item         |
| Pickup | Pickup       |

### KillingPlayer
Invoked before KillPlayer is called.

| Type | Member       |
| --- |--------------|
| Player | Player       |
| DamageHandlerBase | Handler      |

### EnteringEnvironmentalHazard
A player has entered an environmental hazard such as SCP244, amnesia cloud, 106 sinkhole, and 173 tantrum

| Type   | Member       |
|--------|--------------|
| Player | Player       |
| Hazard | Hazard      |
| bool   | IsAllowed    |

### StayingOnEnvironmentalHazard
A player is still inside an environmental hazard

| Type   | Member       |
|--------|--------------|
| Player | Player       |
| Hazard | Hazard      |

### ExitingEnvironmentalHazard
A player is leaving an environmental hazard

| Type   | Member       |
|--------|--------------|
| Player | Player       |
| Hazard | Hazard      |
| bool   | IsAllowed    |

### ChangingNickname
Trying to change the nickname of a player

| Type   | Member  |
|--------|---------|
| Player | Player  |
| string | OldName |
| string | NewName |

### ChangingDangerState
A player is changing their danger state, from SCP-1853

| Type   | Member  |
|--------|---------|
| Player | Player  |
| DangerStackBase | Danger |
| DangerType | Type |
| bool | IsActivating |
| bool | IsEnding |
| Player | EncounteredPlayer |
| bool | IsAllowed |

---

## Scp049 (Doctor)

`Events.Scp049.*` or `Events.Doctor.*`

### FinishingRecall
Doctor's Recall ability is finishing

| Type | Member   |
| --- |----------|
| Player | Player |
| Scp049Role | Scp049 |
| Player | Target |
| Ragdoll | Ragdoll |
| bool | IsAllowed |

### StartingRecall
Doctor's Recall ability is starting

| Type | Member   |
| --- |----------|
| Player | Player |
| Scp049Role | Scp049 |
| Player | Target |
| Ragdoll | Ragdoll |
| bool | IsAllowed |

### ActivatingSense
Doctor activating their sense ability

| Type | Member   |
| --- |----------|
| Player | Player |
| Scp049Role | Scp049 |
| Player | Target |
| bool | IsAllowed |
| float | FailedCooldown |
|  float | Cooldown |
| float | Duration |

### SendingCall
Doctor is using teh Call ability

| Type | Member    |
| --- |-----------|
| Player | Player    |
| Scp049Role | Scp049    |
| float | Duration  |
| bool | IsAllowed |

### Attacking
Doctor is attempting to attack

| Type | Member   |
| --- |----------|
| Player | Player |
| Scp049Role | Scp049 |
| Player | Target |
| bool | IsAllowed |

---

## Scp0492 (Zombie)

`Events.Scp0492.*` or `Events.Zombie.*`

### TriggeringBloodlust
The bloodlust ability is being activated for a zombie

| Type        | Member    |
|-------------|-----------|
| Player      | Player    |
| Scp0492Role | Scp0492   |
| Player      | Target    |
| bool        | IsAllowed |

### ConsumedCorpse
A zombie has consumed a corpse

| Type        | Member    |
|-------------|-----------|
| Player      | Player    |
| Scp0492Role | Scp0492   |
| Ragdoll     | Ragdoll    |
| float | ConsumeHealth |

### ConsumingCorpse
A zombe is trying to consume a corpse.

| Type        | Member    |
|-------------|-----------|
| Player      | Player    |
| Scp0492Role | Scp0492   |
| Ragdoll     | Ragdoll    |
| ConsumeError | ErrorCode |
| bool        | IsAllowed |

---

## Scp079 (Computer/Camera)

`Events.Scp079.*` or `Events.Computer.*` or `Events.Camera.*`

### ChangingCamera
Computer is trying to change camera

### GainingExperience
Computer is recieving experience point

### GainingLevel
Computer is leveling up

### InteractingTesla
Computer trying to activate a telsa gate

### TriggeringDoor
Computer is interating with a door

### ElevatorTeleporting
Computer is using an elevator to change floors

### LockingDown
Computer is locking down a room

### ChangingSpeakerStatus
Computer is trying to talk to local players

### Recontained
Computer has been recontained

### Pinging
Computer is trying to ping to other SCPs

### RoomBlackout
Computer has blacked out a room

### ZoneBlackout
Computer has blacked out a zone

---

## Scp096 (ShyGuy)

`Events.Scp096.*` or `Events.ShyGuy.*`

### Enraging
Shy Guy is trying to enrage

### CalmingDown
Shy Guy is trying to calm down after raging

### AddingTarget
Attemping to add a visible target for Shy Guy while he is enraged

### StartPryingGate
Shy Guy is trying to open a gate

### Charging
Shy Guy is trying to use the Charge ability while enraged

### TryingNotToCry
Shy Guy is trying to use the Try not to cry ability

---

## Scp106 (Larry/OldMan)

`Events.Scp106.*` or `Events.Larry.*` or `Events.OldMan.*`

### **Attacking**
Larry is attacking a player

### **Teleporting**
Larry is attempting to teleport using the hunter atlas

### **Stalking**
Larry is attempting to stalk (Go underground)

### **ExitStalking**
Larry is attempting to resurface from stalking

---

## Scp173 (Peanut)

`Events.Scp173 .*` or `Events.Peanut.*`

### **Blinking**
Forcing players near Peanut to blink

### **BlinkingRequest**
Peanut is attempting to teleport nearby

### **PlacingTantrum**
Peanut is trying to place tantrum on the ground

### **UsingBreakneckSpeeds**
Peanut is trying to speed up

---
## Scp244 (Vase)

`Events.Scp244 .*` or `Events.Vase.*`

### **UsingScp244**
Vase is being activated and placed on the ground

| Type | Member   |
| --- |----------|
| Player | Player |
| bool | IsAllowed |
| Scp244 | Scp244 |

### **DamagingScp244**
Vase is being damaged

| Type | Member   |
| --- |----------|
| Scp244Pickup | Pickup |
| DamageHandler | Handler |
| bool | IsAllowed |

### **OpeningScp244**
Vase is being opened on the ground and activated

| Type | Member   |
| --- |----------|
| Scp244Pickup | Pickup |
| bool | IsAllowed |

---

## Scp330 (Candy)

`Events.Scp330 .*` or `Events.Candy.*`

### **InteractingScp330**
A player interacting with the candy bowl in LCZ

| Type | Member   |
| --- |----------|
| Player | Player |
| CandyKindID | Candy |
| bool | IsAllowed |
| int | UsageCount |
| bool | ShouldSever |

### **DroppingScp330**
A player is dropping a candy bag with the selected candy

| Type | Member   |
| --- |----------|
| Player | Player |
| CandyKindID | Candy |
| bool | IsAllowed |
| Scp330 | Scp330 |

### **EatingScp330**
A player is eating a candy

| Type | Member   |
| --- |----------|
| Player | Player |
| ICandy | Candy |
| bool | IsAllowed |

### **EatenScp330**
A player has finished eating a candy and has received the effects

| Type | Member   |
| --- |----------|
| Player | Player |
| ICandy | Candy |

---

## Scp914

`Events.Scp914.*`

### **UpgradingPickup**
Attemping to upgrade a pickup inside 914

### **UpgradingInventoryItem**
Attempting to upgrade an item in a player's inventory inside 914

### **UpgradingPlayer**
Attempting to upgrade and move a player to the output inside 914

### **Activating**
A player is turning on 914

### ChangingKnobSetting
A player is chaning the setting of 914 (Coarse, Rough, 1to1, Fine, Very Fine)

---

## Scp939 (Dog)

`Events.Scp939.*` or `Events.Dog.*`

### **ChangingFocus**
Dog is crouching and trying to focus in

### **Lunging**
Dog is trying to pounce after crouching

### **PlacingAmnesticCloud**
Dog is trying to place her amnestic cloud

### **PlayingVoice**
Dog is playing a voiceclip from a player

### **SavingVoice**
Server is saving a voice clip of a consenting player for the dog

### **PlayingSound**
Dog is playing a generic sound

### **Clawed**
Dog has slashed at a player

### **ValidatingVisibility**
Confirming before dog is able to see a player

---

## Scp3114 (Skeleton)

`Events.Scp3114.*` or `Events.Skeleton.*`

### **Disguising**
Skeleton is consuming a corpse to change it's disguise

### **Disguised**
Skeleton has changed to a new appearance

### **TryUseBody**
Skeleton is trying to interact with a body to disguise

### **Revealed**
Skeleton has revealed its true form

### **Revealing**
Skeleton is trying to remove its disguise

### **VoiceLines**
A random voiceline is playing from the Skeleton

---

## Server

`Events.Server.*`

### **WaitingForPlayers**
The lobby has started and server is ready to accept players

| Type | Member   |
| --- |----------|

### **RoundStart**
Players have been given their roles and the round has begun

| Type | Member   |
| --- |----------|

### **EndingRound**
Confirming if round ending critera is met

| Type | Member       |
| --- |--------------|
| LeadingTeam | LeadingTeam  |
| SumInfo_ClassList | ClassList    |
| bool | IsRoundEnded |
| bool  | IsForceEnded |
| bool | IsAllowed    |

### **RoundEnded**
The round has ended, showing end of round card

| Type | Member       |
| --- |--------------|
| LeadingTeam | LeadingTeam  |
| SumInfo_ClassList | ClassList    |
| int | TimeToRestart |

### **RestartingRound**
Round is restarting, called after round end timer has completed or manually by admin

| Type | Member   |
| --- |----------|

### **ReportingCheater**
A player is reporting a cheater

| Type | Member   |
| --- |----------|
| Player | Player |
| Player | Target |
| int | ServerPort |
| string | Reason |
| bool | IsAllowed |

### **RespawningTeam**
The team with the most tickets is respawning as part of the 5 minute respawn timer.

| Type | Member               |
| --- |----------------------|
| List\<Player\> | Players              |
| int | MaximumRespawnAmount |
| SpawnableTeamType | nextKnownTeam        |
| SpawnableTeamHandlerBase | SpawnableTeam |
| Queue\<RoleTypeId\> | SpawnQueue           |
| bool | IsAllowed            |

### **AddingUnitName**
Choosing a unit name/number for an NTF group

| Type | Member   |
| --- |----------|
| UnitNamingRule | UnitNamingRule |
| bool | IsAllowed |

### **LocalReporting**
A player is sending a complaint about a player to the local server administrators.

| Type | Member   |
| --- |----------|
| Player | Player |
| Player | Target |
| string | Reason |
| bool | IsAllowed |

### **ChoosingStartTeamQueue**
Invoked before choosing the Team than player will get.

| Type | Member           |
| --- |------------------|
| List\<Team\> | TeamRespawnQueue |
| bool | IsAllowed        |

### SelectingRespawnTeam
Invoked before selecting the team that will respawn

| Type | Member   |
| --- |----------|
| SpawnableTeamType | Team |

### **ReloadedConfigs**
Invoked after the "reload configs" command is ran.

| Type | Member   |
| --- |----------|

### **ReloadedTranslations**
Invoked after the "reload translations" command is ran.

| Type | Member   |
| --- |----------|

### **ReloadedGameplay**
Invoked after the "reload gameplay" command is ran.

| Type | Member   |
| --- |----------|

### **ReloadedRA**
Invoked after the "reload remoteadminconfigs" command is ran.

| Type | Member   |
| --- |----------|

### **ReloadedPlugins**
Invoked after the "reload plugins" command is ran.

| Type | Member   |
| --- |----------|

### **ReloadedPermissions**
Invoked after the "reload permissions" command is ran.

| Type | Member   |
| --- |----------|

---

## Warhead

`Events.Warhead.*` or `Events.Nuke.*`

### **Stopping**
Invoked before stopping the warhead.

| Type   | Member  |
|--------|---------|
| Player | Player  |
| bool   | IsAllowed |

### **Starting**
Invoked before starting the warhead.

| Type   | Member  |
|--------|---------|
| Player | Player  |
| bool   | IsAllowed |
| bool | IsAuto |

### **ChangingLeverStatus**
Invoked before changing the warhead lever status.

| Type   | Member  |
|--------|---------|
| Player | Player  |
| bool   | IsAllowed |
| bool | CurrentState |

### **Detonated**
Invoked after the warhead has been detonated.

| Type   | Member  |
|--------|---------|

### **Detonating**
Invoked before detonating the warhead.

| Type   | Member  |
|--------|---------|
| bool   | IsAllowed |
