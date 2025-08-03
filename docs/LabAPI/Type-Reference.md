# Type Reference

This is a reference for the types used in the Lab API. Each type is defined with its properties and their types.

If a property or method either doesn't work you think is missing, you can use the `lua docs` command to generate the raw docs from LabApi directly. This is what I use to generate this documentation.

## `LabApi.Features.Wrappers.Player`
### Properties
| Name                  | Type                                              | Description |
|-----------------------|---------------------------------------------------|-------------|
| `GameObject`          | `UnityEngine.GameObject`                          | Underlying Unity object |
| `Position`            | `UnityEngine.Vector3`                             | World position |
| `Rotation`            | `UnityEngine.Quaternion`                          | World rotation |
| `Nickname`            | `string`                                          | Player’s display name |
| `UserId`              | `string`                                          | User ID (e.g., Steam ID) |
| `Role`                | `LabApi.Features.Roles.RoleType`                  | Player’s current role |
| `IsLocalPlayer`       | `bool`                                            | True if local player |
| `IsConnected`         | `bool`                                            | True if player is connected |
| `Inventory`           | `LabApi.Features.Inventory.Inventory`             | Inventory wrapper |
| `CameraPosition`      | `Vector3`                                         | Player's camera position |
| `CameraRotation`      | `Quaternion`                                      | Player's camera rotation |
| `Health`              | `float`                                           | Current HP |
| `MaxHealth`           | `float`                                           | Maximum HP |
| `IsCuffed`            | `bool`                                            | True if player is handcuffed |
| `IsMuted`             | `bool`                                            | True if voice chat is muted |
| `GodMode`             | `bool`                                            | Whether god mode is enabled |
| `BypassMode`          | `bool`                                            | Whether bypass mode is active |
| `PlayerId`            | `ushort`                                          | Internal numeric ID |
| `Ammo`                | `Dictionary<AmmoType, ushort>`                    | Ammo by type |
| `IsHost`              | `bool`                                            | True if this player is host |
| `CurrentItem`         | `ItemBase`                                        | Currently held item |
| `Items`               | `IReadOnlyList<ItemBase>`                         | Inventory items |
| `StaminaRemaining`    | `float`                                           | Stamina %
| `IsNoclipEnabled`     | `bool`                                            | True if noclip is active |
| `IsSCP`               | `bool`                                            | True if player is SCP |
| `IsHuman`             | `bool`                                            | True if player is human |
| `Team`                | `Team`                                            | Player’s team enum |
| `IsInOverwatch`       | `bool`                                            | Whether the player is in overwatch mode |
| `IsInvisible`         | `bool`                                            | Whether the player is invisible |
| `IsLocked`            | `bool`                                            | Whether the player's state is locked |
| `Username`            | `string`                                          | Networked username |
| `DisplayNickname`     | `string`                                          | Displayed nickname (may differ from actual) |
### Methods
| Signature | Description |
|----------|-------------|
| `void Teleport(Vector3 position)` | Move the player to the specified world position |
| `void Teleport(Vector3 position, Quaternion rotation)` | Move the player and set rotation |
| `void ChangeRole(RoleTypeId role)` | Change the player’s role |
| `void Kill()` | Instantly kill the player |
| `void Damage(float amount)` | Damage the player for a certain amount |
| `void Heal(float amount)` | Heal the player by a certain amount |
| `void ClearInventory()` | Remove all items |
| `void AddItem(ItemType item)` | Give the player a specific item |
| `void DropAllItems()` | Drop all carried items |
| `void DropItem(ItemBase item)` | Drop a specific item |
| `void ClearAmmo()` | Reset all ammo |
| `void AddAmmo(AmmoType type, ushort amount)` | Add ammo of a specific type |
| `void RemoveAmmo(AmmoType type, ushort amount)` | Remove ammo of a specific type |
| `void SetAmmo(AmmoType type, ushort amount)` | Set ammo to a specific value |
| `void SendHint(string message, float duration)` | Display a hint message |
| `void Mute(bool muted)` | Mute or unmute player |
| `void SetGodMode(bool enabled)` | Enable/disable god mode |
| `void SetBypassMode(bool enabled)` | Enable/disable bypass mode |
| `void SetNoclip(bool enabled)` | Enable/disable noclip |
| `void SetInvisible(bool invisible)` | Toggle player invisibility |
| `void Lock(bool locked)` | Lock the player's state |
| `void SetOverwatch(bool enabled)` | Toggle overwatch mode |
| `void ShowHint(string message, float duration)` | Show a hint (same as SendHint) |
| `void Move(Vector3 position)` | Move player to a position |
| `void Rotate(Quaternion rotation)` | Rotate player to face a direction |
| `void SetRole(RoleTypeId role)` | Alternative to ChangeRole |
| `void ResetInventory()` | Clear and reset inventory |
| `bool HasItem(ItemType item)` | Check if player has a specific item |
| `ItemBase FindItem(ItemType item)` | Find item in inventory |
| `bool IsInRoom(RoomType room)` | Check if player is in a certain room |
| `void Scale(float multiplier)` | Scale player's size (e.g., shrink or grow) |
| `void SetNickname(string name)` | Change player’s nickname |
| `void SetHealth(float amount)` | Set HP to exact value |
| `void SetMaxHealth(float amount)` | Set max HP |
| `void ClearStatusEffects()` | Remove all active status effects |
| `void ApplyStatusEffect(StatusEffectType effect, float duration)` | Apply a status effect |


## `LabApi.Features.Inventory.Inventory`
### Properties

| Name              | Type                           | Description |
|-------------------|--------------------------------|-------------|
| `Items`           | `IReadOnlyList<ItemBase>`      | Items currently in the player's inventory |
| `Ammo`            | `Dictionary<AmmoType, ushort>` | Current ammo amounts by type |
| `HeldItem`        | `ItemBase`                     | The item currently held |
| `Capacity`        | `int`                          | Max number of items the inventory can hold |
### Methods

| Signature | Description |
|-----------|-------------|
| `void AddItem(ItemType type)` | Adds an item of the specified type |
| `void RemoveItem(ItemBase item)` | Removes the given item |
| `bool HasItem(ItemType type)` | Checks whether the inventory contains an item of the specified type |
| `ItemBase GetItem(ItemType type)` | Returns the first item of the specified type |
| `void Clear()` | Removes all items from the inventory |
| `void DropItem(ItemBase item)` | Drops a specific item from the inventory |
| `void DropAllItems()` | Drops all items in the inventory |
| `void SetHeldItem(ItemBase item)` | Sets the currently held item |
| `void SetAmmo(AmmoType type, ushort amount)` | Sets the ammo count for a given type |
| `void AddAmmo(AmmoType type, ushort amount)` | Adds ammo of the specified type |
| `void RemoveAmmo(AmmoType type, ushort amount)` | Removes ammo of the specified type |
| `ushort GetAmmo(AmmoType type)` | Gets the ammo amount for the specified type |
| `void ClearAmmo()` | Removes all stored ammo |

## Enum: `RoleType/RoleTypeId`

| Name                | Value |
|---------------------|-------|
| `None`              | `0`   |
| `ClassD`            | `1`   |
| `Scientist`         | `2`   |
| `FacilityGuard`     | `3`   |
| `NtfPrivate`        | `4`   |
| `NtfSergeant`       | `5`   |
| `NtfSpecialist`     | `6`   |
| `NtfCaptain`        | `7`   |
| `ChaosConscript`    | `8`   |
| `ChaosRifleman`     | `9`   |
| `ChaosRepressor`    | `10`  |
| `ChaosMarauder`     | `11`  |
| `Scp173`            | `12`  |
| `Scp106`            | `13`  |
| `Scp049`            | `14`  |
| `Scp0492`           | `15`  |
| `Scp096`            | `16`  |
| `Scp939`            | `17`  |
| `Tutorial`          | `18`  |
| `Spectator`         | `19`  |

## Enum: `StatusEffectType`

| Name                | Value |
|---------------------|-------|
| `None`              | `0`   |
| `Amnesia`           | `1`   |
| `Bleeding`          | `2`   |
| `Blinded`           | `3`   |
| `Burned`            | `4`   |
| `Concussed`         | `5`   |
| `Deafened`          | `6`   |
| `Decontaminating`   | `7`   |
| `Disabled`          | `8`   |
| `Exhausted`         | `9`   |
| `Flashed`           | `10`  |
| `Hemorrhage`        | `11`  |
| `Poisoned`          | `12`  |
| `SeveredHands`      | `13`  |
| `Sinkhole`          | `14`  |
| `SCP207`            | `15`  |
| `Stained`           | `16`  |
| `Stuck`             | `17`  |
| `Ensnared`          | `18`  |
| `Invigorated`       | `19`  |


## Enum: `AmmoType`

| Name      | Value |
|-----------|-------|
| `None`    | `0`   |
| `Ammotype9mm`      | `1`   |
| `Ammotype556x45mm` | `2`   |
| `Ammotype762x39mm` | `3`   |

## Enum: `ItemType`

| Name                    | Value |
|-------------------------|-------|
| `None`                  | `0`   |
| `KeycardJanitor`        | `1`   |
| `KeycardScientist`      | `2`   |
| `KeycardResearchCoordinator` | `3` |
| `KeycardZoneManager`    | `4`   |
| `KeycardGuard`          | `5`   |
| `KeycardMTFPrivate`     | `6`   |
| `KeycardContainmentEngineer` | `7` |
| `KeycardNTFLieutenant`  | `8`   |
| `KeycardNTFCommander`   | `9`   |
| `KeycardFacilityManager`| `10`  |
| `KeycardChaosInsurgency`| `11`  |
| `KeycardO5`             | `12`  |
| `Radio`                 | `13`  |
| `COM15`                 | `14`  |
| `COM18`                 | `15`  |
| `FSP9`                  | `16`  |
| `Crossvec`              | `17`  |
| `Logicer`               | `18`  |
| `Shotgun`               | `19`  |
| `AK`                    | `20`  |
| `MicroHID`              | `21`  |
| `GrenadeHE`             | `22`  |
| `GrenadeFlash`          | `23`  |
| `SCP018`                | `24`  |
| `SCP207`                | `25`  |
| `Adrenaline`            | `26`  |
| `Painkillers`           | `27`  |
| `Coin`                  | `28`  |
| `ArmourLight`           | `29`  |
| `ArmourCombat`          | `30`  |
| `ArmourHeavy`           | `31`  |
| `Disarmer`              | `32`  |
| `Medkit`                | `33`  |
| `RadioModular`          | `34`  |
| `Jailbird`              | `35`  |
| `ParticleDisruptor`     | `36`  |
| `SCP330`                | `37`  |
| `SCP2176`               | `38`  |
| `SCP244a`               | `39`  |
| `SCP244b`               | `40`  |
| `GunCOM15`              | `41`  |
| `GunCOM18`              | `42`  |
| `GunFSP9`               | `43`  |
| `GunCrossvec`           | `44`  |
| `GunLogicer`            | `45`  |
| `GunShotgun`            | `46`  |
| `GunAK`                 | `47`  |


