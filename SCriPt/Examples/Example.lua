example = {}
    
    
function example:load()
    print("Example mod loaded")
    Events.Server.WaitingForPlayers.add(example.onWaitingForPlayers)
end

function example:unload()
    print("Example mod unloaded")
    Events.Server.WaitingForPlayers.remove(example.onWaitingForPlayers)
end

function example:onWaitingForPlayers()
    print("Example mod waiting for players")
end