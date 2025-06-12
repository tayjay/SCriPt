## Setting up your environment

These instructions will guide you through setting up a new SCP:SL server locally for testing. If you already have a server with EXILED installed jump to Step 4 to the plugin specific instructions.

1. First off, if you don't have one, we need a game server to run this on. This can be your local computer if you want. Follow [NorthWood's Instructions](https://en.scpslgame.com/index.php?title=Guide:Hosting_a_server) to setup a local SCP:SL server. 
2. Then install the latest [EXILED](https://github.com/Exiled-Team/EXILED) version.
3. Add your SteamID to the Owner permission on the server so you can run commands.
4. Get the latest version of the SCriPt.dll plugin from the releases page and place it in your EXILED plugins folder.
5. Get the latest release of [MoonSharp.Interpreter.dll](https://github.com/moonsharp-devs/moonsharp/releases/latest) (Currently 2.0.0.0) 
   1. Download `moonsharp_release_2.0.0.0.zip` from their releases page
   2. Extract it and navigate to the `\interpreter\net40` subfolder and find `MoonSharp.Interpreter.dll`
   3. Place it in your EXILED plugins "dependencies" subfolder.
6. Run the server once, then check the directory where you ran the `LocalAdmin` file. You should see a `Scripts` folder. This is where you will place your lua scripts.
7. Make sure you can connect to the server. Direct Connect to ```127.0.0.1``` if it's running on your local computer.


## Scripts Structure

If you check the "Scripts" folder, you will see a few folders. Here's how they work:

- **Data**: This folder holds persistent data for your scripts. See the `Script-Configs` and `Persistence` pages for more information.
- **Global**: Scripts in here can be loaded into other scripts as globals using a custom `require` implenentation for shared functionality. See the `Script-Globals` page for more information.
- All scripts directly inside the root "Scripts" folder are loaded when the server starts.
  - These scripts are loaded into their own "Script" objects, meaning they cannot talk or manipulate data between them.
  - This is a security feature to prevent scripts from interfering with each other.

## Hello World

Like any good programming tutorial, let's start with a "Hello World" script. Create a new file in the "Scripts/AutoLoad" folder called "HelloWorld.lua" and put the following code in it:

```lua
print('Hello, World!')
```

When you launch the server, you will see ```[Info] [Lua] Hello, World!``` in the console. It doesn't do anything fancy, but if you see this then you're ready to proceed.

## Writing your first script

The start of any good project is a plan. What do you want to do? In this example I want the server to automatically do the following:
- When a player joins, they are greeted with a message

Simple enough, a good way to structure your idea is to keep the event based nature of this plugin in mind, for example:
- When X happens, do Y

Let's look at the code that will do this and break it down. Create a new file in the "Scripts" folder called "HelloUser.lua" and put the following code in it:

```lua
hello_user = {}

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
Most of this code is boilerplate, but let's break it down:
- ```hello_user = {}``` creates a table to hold our functions. With how scripts load it is best to keep them in their own tables, and to not execute code directly, instead relying on functions and events to trigger your code at the right time.
- ```hello_user:load()...``` and ```hello_user:unload()``` are special functions that are called when the script is loaded and unloaded. This is where you should add and remove your event listeners. They are **automatically** called when the scripts loads and unloads.
- ```Events.Player.Joined:add(hello_user.onPlayerJoined)``` adds the ```onPlayerJoined``` function to the Player.Joined event. This means that when a player joins the server, the onPlayerJoined function will be called.
- ```function hello_user:onPlayerJoined(args)``` is the function that will be called when a player joins the server. It takes a single argument, args, which contains information about the player that joined.
- ```Server:Broadcast("Welcome to the server, " .. args.Player.Nickname .. "!")``` sends a message to all players on the server welcoming the new player.

Now, when a player joins the server, a broadcast message will be sent welcoming them. The print function is used so you can see it happen in the console too.

For more real examples of scripts check out the [Example Scripts Page](https://github.com/tayjay/SCriPt/wiki/Example-Scripts)

## Using other Plugins

See the [Extensions](https://github.com/tayjay/SCriPt/wiki/Extensions) page for information on how to use other plugins in your scripts.

You can always call RemoteAdmin commands using the `Server:RACommand("command")` function. This is limited to what RemoteAdmin commands a plugin exposes, but it's a good start.
