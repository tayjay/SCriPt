-- To be redone for LabAPI --

-- Start by creating a table to hold our functions
skeleton_command = {
    command_type='RemoteAdmin', -- This is a RemoteAdmin command
    command='skeleton', -- The command name
    description='Enable Scp-3114 to spawn at the start of the game.', -- A description of the command
    aliases={'scp3114'}, -- Additional command names
    permission='SCriPt.execute', -- The permission required to run the command
    -- Custom fields
    can_spawn= Config:Load('CanSkeletonSpawn', false), -- Whether the skeleton can spawn
    spawn_chance= Config:Load('BaseSkeletonSpawn', 25) -- The chance that the skeleton will spawn
}


-- Function called when the command is executed
function skeleton_command.execute(args, sender)
    if #args > 1 then
        if args[2] == 'help' then
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

-- Since the logic for this needs to run at the start of the game, we'll add it to the RoundStarted event

-- Load function, called when the script is loaded
function skeleton_command.load()
    Events.Server.RoundStart.add(skeleton_command.onRoundStarted)
    print('Skeleton Command Loaded')
end

-- Unload function, called when the script is unloaded
function skeleton_command.unload()
    Events.Server.RoundStart.remove(skeleton_command.onRoundStarted)
end

-- Function called when the round starts
function skeleton_command.onRoundStarted()
    if(skeleton_command.can_spawn) then -- Will only run this if the enable command has been run
        local chance = math.random(1, 100)
        if chance <= skeleton_command.spawn_chance then
            local player = Player.GetRandomByRole(RoleTypeId.ClassD) -- Get a random ClassD player
            player:SetRole(RoleTypeId.Scp3114) -- Change the player's role to Scp3114
        end
    end
end