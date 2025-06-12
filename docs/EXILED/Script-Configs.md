To make customization of scripts easier, a custom config file is generated when the plugin starts. This config file is stored in JSON format and will save and load scripts that call the `Config` global object.

## Config File
The config file is stored in `Scripts/Data/config.json`. This file contains a dictionary which stores any variable name a script references with the `Config` global. A script must run once to generate the config vaules it is looking for.

```json
{
  "Config":{
    "BaseValue":0
  }
}
```
## Examples

### Load
```lua
my_script = {
    value = Config:Load("BaseValue", 0) -- (configName, defaultValue)
}

function my_script:load()
    print(my_script.value)
end
```

```
[DEBUG] [Lua] 0
```

Editing the config file entry for "BaseValue" to any other number will reflect the change in the load function.

The `Load` function's second parameter is overloaded, the type of value it returns depends on the type provided as a default.

For example:
- `Config:Load("BaseInt", 0)` will return a integer number.
- `Config:Load("BaseFloat", 0.0)` will return a decimal number.
- `Config:Load("BaseString", "")` will return a string.
- `Config:Load("BaseBool", false)` will return a boolean.

Explicit calls for `LoadInt`, `LoadFloat`, `LoadString`, and `LoadBool` are also available if you would prefer to be specific.

The `Load` function will not modify the value saved after it is created. For that you'll need the next function.
