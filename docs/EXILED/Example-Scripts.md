## Readding Skeleton

I miss the Skeleton from Halloween 2023, so I want to add a chance that a player can spawn as Scp3114 at the start of the game. There are plugins that do this already, see my "TTAddons". But let's try it ourselves.

```lua 
-- Start by creating a table to hold our functions
scp3114 = {}

-- Load function, called when the script is loaded
function scp3114:load()
    -- Add the function to the event
    Events.Server.RoundStarted:add(scp3114.onRoundStarted)
end

-- Unload function, called when the script is unloaded
function scp3114:unload()
    -- Remove the function from the event
    Events.Server.RoundStarted:remove(scp3114.onRoundStarted)
end

-- Function called when the round starts
function scp3114:onRoundStarted()
    -- Get a random number between 1 and 100
    local chance = math.random(1, 100)
    -- If the number is less than or equal to 25
    if chance <= 25 then
        -- Get a random player
        local player = Player.GetRandom()
        -- Change the player's role to Scp3114
        player:ChangeRole('Scp3114')
    end
end
```

Put this in a file ```Scp3114.lua```. 
Place the file in ```Scripts``` and it will run when the server starts.

## Custom Commands

By specifying some key variables into your script table you can tell SCriPt that it should be loaded as a command. These commands are registered to RemoteAdmin or the client `~` and will have a help entry and auto-complete like any plugin command.

Here is an example of a command that will list all the words CASSIE can say.

```lua
cassiewords_command = {
    command_type='Client',
    aliases={'cwords'},
    command='cassiewords',
    description='Get a list of words CASSIE can say.'
}

function cassie_words_command.execute(args,sender)
    let words = Cassie:GetAllWords()
    let output = ''
    for i=1, #words do
        output = output .. ', ' .. words[i]
    end
    return {
        response=output,
        result=true
    }
end
```

Note that there are some strict requirements when making a command object.
- First, like any other script it will be contained in a global table.
- The table should contain a `command_type` field, which can be either `Client` or `RemoteAdmin`. If not specified, `RemoteAdmin` will be selected.
- The table must contain a `command` field, which is the command name.
- An optional table `aliases` can be added to provide additional command names.
- An optional string `description` can be added to provide a description of the command for the help display.
- An optional string `permissions` can be added which will check the EXILED permission list for whether an RemoteAdmin command be be ran.
  - Client commands are always allowed.
  - If no permissions are supplied, they will be ignored.
- The function `execute` must be defined.
  - The function must take two arguments, `args`(string[]) and `sender`(CommandSender).
  - The function must return a table with a `response` and a `result` field.

Here's an example of a RemoteAdmin command that can be made

```lua
-- Start by creating a table to hold our functions
skeleton_command = {
  command_type='RemoteAdmin', -- This is a RemoteAdmin command
  command='skeleton', -- The command name
  description='Enable Scp-3114 to spawn at the start of the game.', -- A description of the command
  aliases={'scp3114'}, -- Additional command names
  -- Custom fields
  can_spawn=false, -- Whether the skeleton can spawn
  spawn_chance=25 -- The chance that the skeleton will spawn
}

-- Function called when the command is executed
function skeleton_command.execute(args, sender)
  if #args > 1 then -- if number of arguments is greater than 1
    if args[2] == 'help' then -- If the user wants help
      return {response='Usage: skeleton <enable|disable|chance [number]>', result=true}
    end
    if args[2] == 'enable' then
      skeleton_command.can_spawn = true -- Enable the skeleton
      return {response='Scp-3114 can now spawn at the start of the game.', result=true}
    elseif args[2] == 'disable' then
      skeleton_command.can_spawn = false -- Disable the skeleton
      return {response='Scp-3114 will no longer spawn at the start of the game.', result=true}
    elseif args[2] == 'chance' then
      if #args > 2 then
        local chance = tonumber(args[3])
        if chance then
          skeleton_command.spawn_chance = chance -- Set the spawn chance
          return {response='Scp-3114 spawn chance set to ' .. chance .. '%.', result=true}
        else
          return {response='Invalid chance.', result=false}
        end
      else
        return {response='Current spawn chance is ' .. skeleton_command.spawn_chance .. '%.', result=true}
      end
    end
  end
  return {response='Usage: skeleton <enable|disable|chance [number]>', result=false}
end

-- Since the logic for this needs to run at the start of the game, we'll add it to the RoundStart event

-- Load function, called when the script is loaded
function skeleton_command.load()
  Events.Server.RoundStart.add(skeleton_command.onRoundStart)
end

-- Unload function, called when the script is unloaded
function skeleton_command.unload()
  Events.Server.RoundStart.remove(skeleton_command.onRoundStart)
end

-- Function called when the round starts
function skeleton_command.onRoundStart()
  if(skeleton_command.can_spawn) then -- Will only run this if the enable command has been run
    local chance = math.random(1, 100)
    if chance <= skeleton_command.spawn_chance then
      local player = Player.GetRandomByRole('ClassD') -- Get a random ClassD player
      player:SetRole('Scp3114') -- Change the player's role to Scp3114
    end
  end
end
```

For the event logic, I recommend using the built-in load and unload functions to handle registering the events rather than adding them through the command directly. In the case that the command is executed, but the script is disabled before the event is cleared it can cause issues. This way, the event is always cleared when the script is unloaded.

