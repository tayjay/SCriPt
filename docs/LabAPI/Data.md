# Persistant Data

The `Data` module in SCriPt.LabAPI provides a way to store and retrieve data in a persistent manner. This is useful for saving player data, game state, or any other information that needs to be retained across server restarts.

## Accessing Data

`Data.Get(key)`

Key must be a unique string that identifies the data you want to retrieve. If you use a key that already exists, it will load data from that data file.

```lua
local playerData = Data.Get("player_data")
playerData[player.UserId] = {
    name = player.Nickname,
    score = 0,
    items = {}
}
```

This returns a table that can only contain static data types (strings, numbers, booleans, tables). If the key does not exist, it will return a nil value.
Changes to the table will be automatically saved.

Other scripts can access the same data by using the same key.

## Immutable Data

If you want to store some data that you don't want scripts to modify, there is an immutable tag that can be applied to accomplish this.
This is useful for storing configuration data or other information that should not be changed by scripts.
This data can be changed by editing the file directly.

```lua
local configData = Data.Get("config_data")
configData['.enabled'] = true
```

Starting a key with a `.` or `_` will make it immutable. If it does not exist, setting it will create the entry. Future attempts to modify it will be ignored.