The following are global variables that can be accessed from anywhere in the code.

Globals can be accessed from anywhere in your code

### AdminToys
For spawning and interacting with Admin Toys. These are Unity Primitive objects that can be spawned in the game such as cubes, spheres, and lights.

| Type      | Function          | Arguments                                                                                           |
|-----------|-------------------|-----------------------------------------------------------------------------------------------------|
| [Primitive](https://exiled-team.github.io/EXILED/api/Exiled.API.Features.Toys.Primitive.html) | AdminToys:Spawn   | string name <br/>Vector3 position <br/>Quaternion rotation <br/>Color color                         |
| [Primitive](https://exiled-team.github.io/EXILED/api/Exiled.API.Features.Toys.Primitive.html) | AdminToys:Destroy | string name                                                                                         |
| [Primitive](https://exiled-team.github.io/EXILED/api/Exiled.API.Features.Toys.Primitive.html) | AdminToys:Create  | PrimitiveType type <br/>Vector3 position <br/>Vector3 rotation <br/> Vector3 scale <br/>Color color |

### Cassie
For interacting with the in-game announcer.

| Type | Variable          | Interaction |
| --- |-------------------|-------------|
 | bool | Cassie.IsSpeaking | Get |

| Type | Function               | Arguments                                                                                                               |
| --- |------------------------|-------------------------------------------------------------------------------------------------------------------------|
| void | Cassie:Message         | _string_ **words** <br/>_bool_ **makeHold** = true <br/>_bool_ **makeNoise** = true <br/>_bool_ **isSubtitles** = false |
| void | Cassie:GlitchyMessage  | string message <br/>float glitchChance <br/>float jamChance                                                             |
| bool | Cassie:IsValidWord     | string word                                                                                                             |
| bool | Cassie:IsValidSentence | string sentence                                                                                                         |

### Config
For interacting with script configs.

*See [Config Section](https://github.com/tayjay/SCriPt/wiki/Script-Configs) for more info*

| Type | Function       | Arguments                           |
|------|----------------|-------------------------------------|
| int | Config:Load    | string key <br/>int defaultValue    |
| float | Config:LoadInt | string key <br/>int defaultValue    |
| float | Config:Load    | string key <br/>float defaultValue  |
| float | Config:LoadFloat | string key <br/>float defaultValue  |
| string | Config:Load    | string key <br/>string defaultValue |
| string | Config:LoadString | string key <br/>string defaultValue |
| bool | Config:Load    | string key <br/>bool defaultValue   |
| bool | Config:LoadBool | string key <br/>bool defaultValue   |
| int | Config:Save    | string key <br/>int value           |
| float | Config:SaveInt | string key <br/>int value           |
| float | Config:Save    | string key <br/>float value         |
| float | Config:SaveFloat | string key <br/>float value         |
| string | Config:Save    | string key <br/>string value        |
| string | Config:SaveString | string key <br/>string value        |
| bool | Config:Save    | string key <br/>bool value          |
| bool | Config:SaveBool | string key <br/>bool value          |

The `Load` functions are used to create and retrieve a value from the config file. The `Save` functions are used to update the value in the config file.

### Timing
For creating and managing coroutines.

*See [Coroutines](https://github.com/tayjay/SCriPt/wiki/Coroutines) for more info*

| Type | Function             | Arguments                                         |
|------|----------------------|---------------------------------------------------|
| CoroutineHandle | Timing:CallDelayed   | float delay <br/>Function action                  |
| CoroutineHandle | Timing:CallDelayed   | float delay <br/>Function action <br/>object[] args |
| CoroutineHandle | Timing:CallCoroutine | Function action                                    |
| CoroutineHandle | Timing:CallCoroutine | Function action <br/>object[] args                 |
| void | Timing:KillCoroutine | CoroutineHandle handle |
| void | Timing:Kill | CoroutineHandle handle |

### Decon
For interacting with the LCZ decontamination sequence.

| Type  | Function      | Arguments |
|-------|---------------|-----------|
 | void  | Decon:Disable |          |

### Events
For subscribing to events.

*See [Events Section](https://github.com/tayjay/SCriPt/wiki/Events) for more info*

### Facility
For interacting with the Map/Facility.


### Lobby
For interacting with the lobby.

### Player
A helper for interacting with players.

### Role
For interacting with player roles.

### Round
For interacting with the current round.

### Server
For interacting with the server.

| Type         | Variable | Interaction |
|--------------|----------|-------------|
| int          | PlayerCount | Get         |
| int          | NpcCount | Get         |
| int          | ScpCount | Get         |
| int          | HumanCount | Get         |
| int          | DClassCount | Get         |
| int          | ScientistCount | Get         |
| int          | FoundationCount | Get         |
| int          | ChaosCount | Get         |
| bool         | FriendlyFire | Get/Set     |
| List(Player) | Players | Get         |
| List(Player) | RemoteAdmins | Get         |
| double       | Tps | Get         |
| double       | Frametime | Get         |
| string       | Ip | Get         |
| string       | Port | Get         |
| string       | Name | Get         |
| string       | Version | Get         |
| bool         | StreamingAllowed | Get |
| bool         | IsBeta | Get |
| bool         | IsIdleModeEnabled | Get/Set |


| Type | Function             | Arguments                        |
|------|----------------------|----------------------------------|
| string | Server:RACommand     | string command                   |
| void | Server:LACommand     | string command                   |
| void | Server:Restart       |                                  |
| void | Server:Shutdown      |                                  |
| void | Server:SendBroadcast | string message <br/>float duration |
| object | Server:GetSessionVariable | string key |
| void | Server:SetSessionVariable | string key <br/>object value |

### Store
For interacting with persistent storage.

*See [Persistence Section](https://github.com/tayjay/SCriPt/wiki/Persistence) for more info*

| Type  | Function       | Arguments                               |
|-------|----------------|-----------------------------------------|
| Table | Store:Load    | string key <br/>Table defaultValue = {} |
| void  | Store:Save    | string key <br/>Table value             |


### Warhead
For interacting with the warhead.

| Type | Variable | Interaction |
|------|----------|-------------|
| float | Warhead.DetonationTimer | Get/Set     |
| bool | Warhead.LeverStatus | Get/Set     |

| Type | Function       | Arguments |
|------|----------------|-----------|
| void | Warhead:Start  | |
| void | Warhead:Stop   | |
| void | Warhead:Lock   | |
| void | Warhead:Unlock | |
| void | Warhead:Shake  | |

