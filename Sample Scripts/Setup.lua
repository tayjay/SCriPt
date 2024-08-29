setup = {
    enabled = true,
}

function setup.load()
    Events.Player.Spawned.add(setup.OnSpawned)
end 

function setup.unload()
    Events.Player.Spawned.remove(setup.OnSpawned)
end 

function setup.OnSpawned(args)
    if enabled then
        -- Setup player with certain items and permissions
    end
end 

function setup.OnWaitingForPlayers()
    if enabled then
        Decon:Disable()
        print('Decon disabled')
        Lobby:Lock()
        print('Lobby locked')
        Round:Lock()
        print('Round locked')
    end
end 


setup_command ={
    command_type="RemoteAdmin",
    command="setup"
}

function setup_command.execute(args,sender)
    if setup.enabled then
        setup.enabled = false
    else
        setup.enabled = true
    end
end 