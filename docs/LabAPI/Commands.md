# Commands

The following commands are used to help manage the SCriPt.LabAPI plugin and its scripts.

## Lua
- Name: `lua`
- Aliases: `script`
- Description: Parent command for managing the SCriPt environment.
- Usage: `lua <subcommand> [args]`

### list
- Name: `list`
- Aliases: `ls`
- Description: Lists all Lua scripts currently running on the server.
- Usage: `lua list`
- Permission: `script.list`

### reload
- Name: `reload`
- Aliases: `rl`
- Description: Reloads all Lua scripts currently running on the server.
- Usage: `lua reload`
- Permission: `script.reload`

### exec
- Name: `exec`
- Aliases: `run`
- Description: Executes lua code directly on the server.
- Usage: `lua exec <code>`
- Permission: `script.exec`

### docs
- Name: `docs`
- Aliases: `documentation`
- Description: Generate documentation for the current version SCP:SL server. Saves to the SCriPt/docs directory.
- Usage: `lua docs`
- Permission: `script.docs`

