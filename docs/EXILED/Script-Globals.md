Scripts saved to the `Scripts/Globals` directory are loaded before every script is loaded, and can be called from anywhere in the script. A limitation however, is that any changes made to the global are not shared with other scripts using the same global. This is to reduce the risk of shared data corruption. We will look at how to handle this later.

## Ground Rules

There are some considerations to be made when making a Global script.
- The script must be in the `Scripts/Globals` directory.
- All data and functions must be contained in its own data table
- Data in globals should not be manipulated by the script, unless you intend for it to only contain local, temporary data.
- Changes to the game should not be made in a global script, as it will be loaded every time a new script is made and can become noisy.

## Example
Let's look at an example of a global script that will keep track of a currency for players. This will require a persistent data storage so we'll include this in the example as well.
    
```lua
-- Create a table to hold our script functions
PlayerPoints = {
    -- Get the data for the player points from Store or initialize it
    players = Store:Load('player_points', ${ -- $ denotes a Prime Table
        player_steam = ${ -- This is just a placeholder for visualization
            points = 0 -- Using a table here will allow us to store more data later
        }
    }),
}

-- Function to give a player points
function PlayerPoints.Give(player, amount) -- (Player, number)
    if player.DoNotTrack then -- If the player has opted out of tracking
        print("Player " .. player.Nickname .. " has opted out of tracking.")
        return
    end
    local user_id = player.UserId:gsub('@', '_') -- Get the key for the user in "players", replacing the @ as it causing issues being a key
    if not PlayerPoints.players[user_id] then -- If they don't exist, create them
        PlayerPoints.players[user_id] = ${
            points = 0
        }
    end
    PlayerPoints.players[user_id].points = PlayerPoints.players[user_id].points + amount -- Give the player points
    Store:Save('player_points', PlayerPoints.players) -- Perform a manual save to ensure the data is saved, this isn't needed if you want to save disk write times.
end

-- Function to take points from a player
function PlayerPoints.Take(player, amount) -- (Player, number)
    if player.DoNotTrack then -- If the player has opted out of tracking
        print("Player " .. player.Nickname .. " has opted out of tracking.")
        return
    end
    local user_id = player.UserId:gsub('@', '_')
    if not PlayerPoints.players[user_id] then
        PlayerPoints.players[user_id] = ${
            points = 0
        }
    end
    PlayerPoints.players[user_id].points = PlayerPoints.players[user_id].points - amount
    Store:Save('player_points', PlayerPoints.players)
end

-- Function to get a player's points
function PlayerPoints.Get(player) -- (Player)
    if player.DoNotTrack then -- If the player has opted out of tracking
        print("Player " .. player.Nickname .. " has opted out of tracking.")
        return
    end
    local user_id = player.UserId:gsub('@', '_')
    if not PlayerPoints.players[user_id] then
        PlayerPoints.players[user_id] = ${
            points = 0
        }
    end
    return PlayerPoints.players[user_id].points
end
```

Any script can now call `PlayerPoints:Give(player, amount)`, `PlayerPoints:Take(player, amount)`, and `PlayerPoints:Get(player)` to interact with the player's points. 
```lua
function my_script:OnPlayerJoin(args)
    if not player.DoNotTrack then -- If the player has opted out of tracking
        print("Player " .. player.Nickname .. " has opted out of tracking.")
        return
    end
    local points = PlayerPoints.Get(args.Player)
    PlayerPoints.Give(args.Player, 100)
    print(points)
end 
```
Because we used `Store:Load` in our global to store the points, SCriPt handles sharing the data between the scripts. If you were to use a table directly, the changes would not reflect by other scripts trying to make changes.
