## What is a Proxy object?
Either due to necessity, convenience, or security, some object types are contained inside a wrapper class. In MoonSharp this is known as a proxy.
Proxy objects found here overwrite the available functions and variables referenced in the EXILED documentation. Check this reference first when you want information on an object type.

## Proxies

### Door

| Type | Variable      | Interaction |
| --- |---------------|-------------|
| string | Name          | Get         |
| Vector3 | Position      | Get         |
| Quaternion | Rotation      | Get         |
| Vector3 | Scale         | Get         |
| DoorType | Type          | Get         |
| Room | Room          | Get         |
| ZoneType | Zone          | Get         |
| bool | IsFullyOpen   | Get         |
| bool | IsFullyClosed | Get         |
| bool | IsMoving      | Get         |
| bool | IsGate        | Get         |
| bool | IsCheckpoint  | Get         |
| bool | IsElevator    | Get         |
| bool | IsBreakable   | Get         |
| KeycardPermissions | KeycardPermissions | Get         |
| bool | IsOpen        | Get/Set     |
| bool | IsLocked      | Get/Set     |
| bool | AllowsScp106  | Get/Set     |

| Type | Function      | Arguments                                      |
| --- |---------------|------------------------------------------------|
| void | Open          |                                                |
| void | Close         |                                                |
| void | Lock          | float duration <br> [ DoorLockType lockType ] |
| void | Unlock        |                                                |
| bool | Damage        | float amount                                   |
| void | Break         |                                 |

### Item

### Npc

### Pickup

### Player

### Room

### TeslaGate

### CommandSender

