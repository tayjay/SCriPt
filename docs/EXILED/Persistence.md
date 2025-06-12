Everything that your script has done so far is lost when the server restarts. If you want to keep data around between restarts, you need to save it to a file. This is called persistence. Here is an example of how you can save data to a file and load it back up when the server starts.

## Example

```lua
-- Create a table to hold our script functions
storage_example = {
    -- Get the data from Store or initialize it to 0
    data = Store:Load("storage_example", ${count=0}) -- (tableName, defaultValue)
}

function storage_example:load()
    -- Add the function to the event
    Events.Server.WaitingForPlayers:add(storage_example.onWaitingForPlayers)
end

function storage_example:unload()
    -- Cleanup the event
    Events.Server.WaitingForPlayers:remove(storage_example.onWaitingForPlayers)
end

function storage_example:onWaitingForPlayers()
    -- Increment the count whenever the server starts up
    storage_example.data.count = storage_example.data.count + 1
    Store:Save("storage_example", storage_example.data)
    print("The server has been started " .. storage_example.data.count .. " times.")
    
end
```

## Rules
There are some limitations on what you can store in the Store. Here are the rules:
- The key must be a unique string
- The value is always a "Prime Table" denoted with the `$` prefix 
- A Prime Table can only contain the following types:
  - String
  - Number (int/float/double)
  - Boolean
  - Prime Table (with the same rules)
  - Nil
- You should call `Store:Load` with the creation of your script object to ensure the data is loaded when the script is loaded and is easily accessible.
  - Running `Store:Load` multiple times is fine as it always returns the same table.
  - Changes made to the local table are reflected in storage
- The `Store:Save` function must be ran to save the data to disk
  - If this is not ran it is possible your changes will be lost if the server stops unexpectedly


