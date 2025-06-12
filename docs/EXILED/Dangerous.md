## Warning

The docs you are about to read can be considered dangerous to your system. Only ever run code if you trust that the source is safe.
This code will not be executed if the required config options are not set to allow it. The default state has them disabled for your safety.

## So why is this so danagerous?

Enabling the features below will allow a legitimate administrator to access system libraries in Lua.
This means they can read and write files on the server, and execute system commands. This is a very powerful feature that should only be enabled if you trust the people who wrote the scripts you are running.

Legitimate uses could include:
- Executing system commands upon certain events in-game
- Saving/Loading persistent data in system files
- Dynamically updating configuration files
- Dynamically add missed class references to Scripts

Each of these can easily be abused.

**You have been warned!**

## Enabling the features

| Config Option | Description                                                                                  | Default   | Options |
|---------------|----------------------------------------------------------------------------------------------|-----------| ------- |
| `SandboxLevel`    | Determines what built-in lua functions are available. https://www.moonsharp.org/sandbox.html | `Soft`    | `Hard`, `Soft`, `Default`, `Complete` |
| `SystemAccessLevel`     | Determintes whether the `io`, `file`, and `os` modules can be used. https://www.moonsharp.org/platforms.html                         | `Limited` | `Limited`, `Standard` |

## SandboxLevel

### Hard

A sort of "hard" sandbox preset, including string, math, table, bit32 packages, constants and table iterators.

### Soft

A softer sandbox preset, adding metatables support, error handling, coroutine, time functions and dynamic evaluations.

### Default

Includes everything except "debug" as now. Beware that using this preset allows scripts unlimited access to the system.

### Complete

The complete package. Beware that using this preset allows scripts unlimited access to the system.

## SystemAccessLevel

### Limited

Very limited support. Disables the `io`, `file` and parts of the `os` modules.

### Standard

Implements all the needed methods, going to files, environment variables, etc.

## Examples

If you have the `SystemAccessLevel` set to `Standard` and the `SandboxLevel` set to `Default`, these scripts will work. If you have the `SystemAccessLevel` set to `Limited` and the `SandboxLevel` set to `Soft`, these scripts will not work.

```lua
-- This script will write a file to the server's filesystem
local file = io.open("test.txt", "w")
file:write("Hello, World!")
file:close()
```


```lua
-- This script will execute a system command
os.execute("echo Hello, World!")
```


```lua
-- This script will read a file from the server's filesystem
local file = io.open("test.txt", "r")
local contents = file:read("*a")
file:close()
print(contents)
```


```lua
-- This script will read an environment variable
local env = os.getenv("PATH")
print(env)
```

## Conclusion

If you are going to enable these features, make sure you trust the people who are writing the scripts you are running. If you don't trust them, don't enable these features. If you do trust them, make sure you have a good backup of your server before enabling these features. If you don't, you could lose everything.

This plugin was built on the idea to give admins more control over how their game server runs. 

_With great power comes great responsibility._