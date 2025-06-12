# Staying in compliance
In order to have your server hosted publicly you must remain in compliance with the VSR. This page will help you understand what you need to do to stay in compliance.

The full rules can be found [here](https://scpslgame.com/Verified_server_rules.pdf), but let's look at the key points that pertain to this plugin.


- VSR 3.6 do not present racist content, personal data, and/or anything illegal in your scripts. 
- VSR 3.7 requires a certain level of performance and stability. Avoid running excessively complex loops, or heavy workloads on frequent events. Consider using Coroutines and breaking the work up over time. For example:
    ```lua
    function heavyWorkload()
        for i=1, 1000 do
            -- Do something
            coroutine.yield(Timing.WaitForOneFrame) -- Separate the work over time.
        end
    end
    ```
- VSR 3.8 do not apply coloured badged that are not approved.
- VSR 3.12 do not use this plugin to state false information such as impersonating NorthWood staff.
- VSR 3.14 custom commands cannot be used to provide non server-staff with excessive permission such as modifying the roles or permissions of other players. See list in VSR for more info.
- VSR 3.18 do not use this plugin to excessively spam a player's GameConsole or the RemoteAdmin console. Be careful of how often you are sending responses to players.
- VSR 6.1 do not modify or block the Global Badge of a player.
- VSR 8.3.1 do not use this plugin to cause physical harm to a player such as loud noses or flashing lights.
- VSR 8.3.2 you must report your server as modded (This is a requirement of EXILED as well)
- VSR 8.3.4 Automated kicks and bans must be clearly described to the player.
- VSR 8.4 do not share the secret part of a player's auth token or IP with anyone.
- VSR 8.6 Do not use this plugin to initiate a DoS attack against players or other servers.
- VSR 8.7 do not use this plugin to give all players any admin permissions.
- VSR 8.9 you cannot kick a player for enabling DoNotTrack
- VSR 8.11 You must honor the privacy settings a player has sent to the server. Be mindful of what data you store. This can be found under the `Player#DoNotTrack` variable. If it is true you cannot store information about a player such as their Name/ID/IP attached to a currency. Always have a fallback if a player does not wish to be tracked.
    ```lua
    function PlayerPoints.Give(player, amount)
        if player.DoNotTrack then -- If the player has opted out of tracking
            print("Player " .. player.Nickname .. " has opted out of tracking.")
            player.ShowHint("Due to your privacy settings, we cannot track your points.")
            return
        end
        -- Your code here
    end
    ```
- VSR 8.13 do not use this plugin to grant achievements to players.
- VSR 12.1 if you use pretty much any part of this plugin that makes a change to gameplay, your server must be flagged "heavily_modded_server = true"

Be sure to read the VSR in full to ensure you are in compliance. If you have any questions about the VSR, please reach out to the NorthWood team.

If you aren't planning on hosting your server publicly, these are still a good guideline to follow to create an enjoyable server for your players.