# Coroutines

Coroutines are a way to run code in parallel with the main thread.
This can be useful for:
- running code that takes a long time to complete
- running code that needs to wait for a certain condition to be met
- running code that needs to be executed at regular intervals

The `Timing` global variable provides functions for creating and managing coroutines.

## CallDelayed
Call a function after a delay.

```lua
Timing:CallDelayed(5, function()
    print('This will be printed after 5 seconds')
end)
```

## CallPeriodically
Call a function at regular intervals for a certain duration.

```lua
--duration, interval, action
Timing:CallPeriodically(60, 1, function()
    print('This will be printed every second')
end)

```

## CallCoroutine

Call a coroutine.

Note that Lua Coroutine logic applies here. You can use `coroutine.yield(x)` to pause the coroutine for x seconds, 0.1 = 10 loops/second.

```lua
Timing:CallCoroutine(function()
    print('This will be printed immediately')
    while true do
        print('This will be printed every second')
        coroutine.yield(1)
    end
end)
```

`coroutine.yield` can currently only be used in CallCoroutine, if you want to use `CallDelayed` and have a break in it use `CallCoroutine` and start function with `coroutine.yield(delay)`

## Using Arguments
These functions can also be called with arguments. The arguments need to be passed along in an object[].

```lua
Timing:CallDelayed(5, function(word1, word2)
    print('This will be printed after 5 seconds')
    print('The arguments are: ' .. word1 .. ' ' .. word2)
end, {'Hello',' World!'})
```

## KillCoroutine
Executing these functions will return a CoroutineHandle object. If you store this object in your script it can be used later to kill the coroutine early.

Alias `Timing:Kill`
```lua
local tag = Timing:CallCoroutine(function()
    print('This will be printed immediately')
    while true do
        print('This will be printed every second')
        coroutine.yield(Timing.WaitForSeconds(1))
    end
end)
...
Timing:KillCoroutine(tag)
```
Note: Coroutines are stored per script, and will also be killed when the script is unloaded.