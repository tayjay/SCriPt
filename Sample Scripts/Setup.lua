-- LabAPI
-- I use this script to setup my dev server environment.
setup = SCriPt.Mod('Setup')
setup.enabled = true

function setup.load()
    Events.Player.Spawned.add(setup.OnSpawned)
end 

function setup.unload()
    Events.Player.Spawned.remove(setup.OnSpawned)
end 

function setup.OnSpawned(args)
    if setup.enabled then
        -- Setup player with certain items and permissions
    end
end 

function setup.OnWaitingForPlayers()
    if setup.enabled then
        Decon:Disable()
        print('Decon disabled')
        Lobby:Lock()
        print('Lobby locked')
        Round:Lock()
        print('Round locked')
    end
end 


--SCriPt.Command(CommandType type, string name, string[] aliases, string description, string permission, Closure execute)

setup_command = SCriPt.Command(
        CommandType.RemoteAdmin, -- This is a RemoteAdmin command
        'setup', -- The command name
        { 'setup' }, -- Additional command names
        'Enable or disable the setup script for the server.', -- A description of the command
        'SCriPt.setup', -- The permission required to run the command
        setup_command.execute -- The function to call when the command is executed
)

function setup_command.execute(args,sender)
    if setup.enabled then
        setup.enabled = false
    else
        setup.enabled = true
    end
end 