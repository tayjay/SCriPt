# Getting Started


## Getting Started with LabAPI
1. First off, if you don't have one, we need a game server to run this on. This can be your local computer if you want. Follow [NorthWood's Instructions](https://en.scpslgame.com/index.php?title=Guide:Hosting_a_server) to setup a local SCP:SL server.
2. Add your SteamID to the Owner permission on the server so you can run commands.
3. Get the latest packages for SCriPt from the [releases page]() for the SCriPt plugin and dependencies.
4. On first setup, copy the contents of the dependencies zip to your AppData (Windows) or Configs (Linux) folder `SCP Secret Laboratory\LabAPI\dependencies\global`
5. Place the SCriPt.LabAPI.dll in `SCP Secret Laboratory\LabAPI\plugins\7777` (or `global` if you want)
6. Run the server once, then check the `SCP Secret Laboratory\LabAPI` directory. If everything worked, you should see a new `SCriPt` folder. This is where you will place your Lua scripts.

## Your First Script

Let's start with a simple script that prints "Hello, World!" to the console. 
Create a new file in the `SCP Secret Laboratory\LabAPI\SCriPt` folder called `HelloWorld.lua` and put the following code in it:

```lua
print("Hello, World!")
```

When you launch the server, you should see `[Info] [Lua-HelloWorld] Hello, World!` in the console. If you see this, you've written your first script.

## Your *Actual* First Script

This is the most important note when getting started. SCriPt works on a similar paradigm as C#, using events to execute code when it is needed.
Let's create a basic script that greets players when they join the server. We'll go over what's happening in a moment.

Create a new file in the `SCP Secret Laboratory\LabAPI\SCriPt` folder called `HelloUser.lua` and put the following code in it:

```lua
hello_user = SCriPt.Module("hello_user")

function hello_user:load()
    Events.Player.Joined:add(hello_user.onPlayerJoined)
end

function hello_user:unload()
    Events.Player.Joined:remove(hello_user.onPlayerJoined)
end

function hello_user:onPlayerJoined(args)
    Server:Broadcast("Welcome to the server, " .. args.Player.Nickname .. "!")
    print("Player " .. args.Player.Nickname .. " has joined the server.")
end
```

### Explanation
- `hello_user = SCriPt.Module("hello_user")`: This creates a new module named `hello_user`. Modules are used to organize your code and can be loaded and unloaded independently.
- `function hello_user:load()`: This function is called when the script is loaded. Here, we add an event listener for when a player joins the server.
- `Events.Player.Joined:add(hello_user.onPlayerJoined)`: This line registers the `onPlayerJoined` function to be called whenever a player joins the server.
- `function hello_user:unload()`: This function is called when the script is unloaded. It removes the event listener to prevent memory leaks.
- `Events.Player.Joined:remove(hello_user.onPlayerJoined)`: This line unregisters the `onPlayerJoined` function when the script is unloaded.
- `function hello_user:onPlayerJoined(args)`: This function is called when a player joins the server. It takes an `args` parameter that contains information about the player.
- `Server:Broadcast("Welcome to the server, " .. args.Player.Nickname .. "!")`: This line sends a message to all players on the server welcoming the new player.
- `print("Player " .. args.Player.Nickname .. " has joined the server.")`: This line prints a message to the console indicating that a player has joined.

## Next Steps
Now that you have a basic understanding of how SCriPt works, you can start creating more complex scripts. The rest of the documentation will cover more advanced topics.

