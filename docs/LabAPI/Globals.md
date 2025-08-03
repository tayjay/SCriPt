# Globals

The following is a reference of all custom Global variables that are available in SCriPt.LabAPI. These variables are automatically registered and can be used in your Lua scripts without any additional setup.

## AdminToys
For adding the various admin toys provided by SCP:SL. This includes Text, Lights, Cameras, Shapes, and more.

## Data
For storing and retrieving data in a persistent manner. This is useful for saving player data, game state, or any other information that needs to be retained across server restarts.

## DMS
For interacting with the Deadman Switch.

## Decon
For interacting with the LCZ Decontamination Sequence.

## Events
For subscribing to the events of the game. This is a key part of SCriPt, as it allows you to run code when certain events occur, such as a player joining the server or an item being picked up.

## Lobby
For interacting with the Lobby.

## New
For creating new instances of various types. Such as Vector3, Quaternion, and other types that are commonly used in SCP:SL.

## Players
For accessing player-related information and functionality. This includes getting a list of all players, finding a player by their ID or name, and other player-related operations.

## Round
For interacting with the current round. This includes starting and ending rounds, checking the current round state, and other round-related operations.

## SCriPt
For accessing the SCriPt module itself. This is used for creating modules, custom objects, and other interactions with the SCriPt plugin. 

## Server
For interacting with the server itself. This includes broadcasting messages, sending commands, and other server-related operations.

## Timing
For handling Coroutines. Use this for calling functions after a delay or for running code in a separate thread.