I've done what I can to make most functions of a plugin accessible throuhgh Lua code. However, there are things I missed, like other plugins. Here I have included some instructions for users, and owner of those plugins to connect to the SCriPt system and allow their code to run in scripts.

## Getting Connected

There are mechanisms for other mods to add their class types and custom Global variables to scripts. These steps are meant for plugin creators.

There are 3 primary options for doing so:
1. Direct: Reference the SCriPt.dll library in your project, only do this if you know SCriPt will be installed on all servers running your plugin.
2. Reflection: Use reflection to call the SCriPt library at runtime, do this if you don't know if SCriPt will be installed on all servers running your plugin.
3. Use the TTCore library, this is the recommended method as it will work with or without SCriPt installed.

### Direct

1. Reference the SCriPt.dll library in your project. Don't need to reference MoonSharp, but it is helpful if you want more control over the script.
2. Make sure your plugin start priority is before `PluginPriority.Last`
3. In your `OnEnabled` method, add the following code:
```csharp
// Create the connector object
// Optional Second parameter for whether to attempt registering your whole assembly with MoonSharp.
// If you experience issues try setting the parameter to false.
PluginConnector connector = SCriPt.Instance.SetupConnector(this);

// Register your custom types, only really nessescary if you don't register your whole assembly
connector.AddType(typeof(MyCustomType));

// Register your global variables
// Make sure the name is unique
connector.AddGlobal(typeof(LuaGlobals), "MyGlobal");
```

Scripts can now access your custom types and global variables.

### Reflection

1. Make sure your plugin start priority is before `PluginPriority.Last`
2. In your `OnEnabled` method, add the following code:
```csharp

// Get a reference to the plugin
IPlugin<IConfig> SCriPtPlugin = Exiled.Loader.Loader.GetPlugin("SCriPt");

// Create the connector object
// Optional Second parameter for whether to attempt registering your whole assembly with MoonSharp.
// If you experience issues try setting the parameter to false.
var connector = SCriPtPlugin.GetType().GetMethod("SetupConnector").Invoke(SCriPtPlugin, new object[] { this });

// Register your custom types, only reall nessescary if you don't register your whole assembly
connector.GetType().GetMethod("AddType").Invoke(connector, new object[] { typeof(MyCustomType) });

// Register your global variables
// Make sure the name is unique
connector.GetType().GetMethod("AddGlobal").Invoke(connector, new object[] { typeof(LuaGlobals), "MyGlobal" });
```

Scripts you write should now have the same access as if you used the direct method.

### TTCore

1. Make sure your plugin start priority is before `PluginPriority.Last`
2. In your `OnEnabled` method, add the following code:
```csharp
// Create a TTCore OptionalPlugin
if(OptionalPlugin.GetReference("SCriPt", out OptionalPlugin SCriPtConnection))
{
    // Create the connector object
    // Optional Second parameter for whether to attempt registering your whole assembly with MoonSharp.
    // If you experience issues try setting the parameter to false.
    object connector = SCriPtConnection.CallMethod("SetupConnector", this);

    // Register your custom types, only reall nessescary if you don't register your whole assembly
    TTReflection.CallMethod(connector, "AddType", typeof(MyCustomType));

    // Register your global variables
    // Make sure the name is unique
    TTReflection.CallMethod(connector, "AddGlobal", typeof(LuaGlobals), "MyGlobal");
}
```

## Global Class

In the examples above, we reference a `LuaGlobals` class. This class just holds public static methods. Here is an example of what it could look like:
```csharp
public class LuaGlobals
{
    public static void MyGlobalFunction()
    {
        Log.Info("MyGlobalFunction was called!");
    }
}
```
If you want to control what exactly is available to the scripts, you can use the `MoonSharpUserData` attribute to mark the class as a userdata class. This will only expose the public methods and properties marked with the `MoonSharpHidden` attribute. You'll need to reference MoonSharp.Interpreter for this to work, and read the docs on how to use that.

## I want to access a plugin I didn't write

Since you didn't make the plugin, you won't be able to add code to it to register these methods. There's an easy way, and a hard way to do this. Let's start with the easy way.

### Easy Way
The easiest way to interact with a mod that doesn't have direct support is to use the Remote Admin commands it provides. This is limited to what they expose, but it's a good start.

```lua
Server:RACommand("command")
```

These commands will be executed as the Server player, so be careful as it will bypass most permissions

### Hard Way
There's technically 2 options here.
- If all you want to do is register the types inside the plugin and access them that way you can do this inside a Global Script file.
- If you want more control, or add a custom global variable, you'll need to use reflection to call the plugin's methods. This will require making your own EXILED plugin. If you already have one then great! If not, it's good coding practice to make one anyway. The EXILED team is building out their plugin dev guide.

#### Using a `Script`

Make a new `.lua` file in the `Scripts/Globals` folder. This will run before all Script objects are created, and can register the types you need.

```lua
-- This is an example accessing the Exiled API
-- The first parameter is the full namespace path to the class you want to register
-- The second parameter is the library/plugin it's in
RegisterType('Exiled.API.Features.Npc', 'Exiled.API')
```

This code will allow you to reference the `Npc` class in your scripts. Useful for class types I forgot to register. If there are some common ones you see missing be sure to reach out about it to have it added to the core plugin.

#### Using your own Plugin

- First off, it's recommended that you directly reference the library you want to access in your project. This will make your life easier. If you can't do that, you'll need to use reflection to call the plugin's methods.
- You also need to reference `MoonSharp`, you can find it in NuGet. Referencing this will mean you need it installed on your server, but if this plugin is purpose built for this it's easiest to include it.
- Make sure your plugin start priority is before `PluginPriority.Last`
- In your `OnEnabled` method, add the following code:
```csharp
// The following will register the Npc class from EXILED
UserData.RegisterType<Exiled.API.Features.Npc>();

// You can also register the full assembly
// If there are naming conflicts with some files you may need to register indevidual classes, or check out Custom Proxies
UserData.RegisterAssembly(Loader.GetPlugin("NameOfPlugin").Assembly);

// If you want to add a custom global variable
// This type will be from your own plugin, unless there is a handy class in their code that only contains static methods.
// This is added differently because "UserData" applies in a universal context. But Globals need to be added to the script directly at runtime, so they need to be queued up.
ScriptLoader.AddGlobal<OtherGlobals>("OtherGlobals");
```

Now for the `OtherGlobals` class.
```csharp
[MoonSharpUserData] // Do not include this if you didn't include MoonSharp
public class OtherGlobals
{
    public static int GlobalVariable => Other.Library.Class.Variable;
    
    public static void OtherGlobalFunction()
    {
        Other.Library.Class.Method();
        // If you didn't directly reference the library you're trying to include use reflection here to call it.
    }
}
```
Alternatively to this, you can also use the connector logic above, just reference the other class as necessary.

```csharp
// Create the connector object
// Optional Second parameter for whether to attempt registering your whole assembly with MoonSharp.
// If you experience issues try setting the parameter to false.
PluginConnector connector = SCriPt.Instance.SetupConnector(OtherPlugin.Instance);

// Register your custom types, only really nessescary if you don't register your whole assembly
connector.AddType(typeof(OtherType));

// Register your global variables
// Make sure the name is unique
connector.AddGlobal(typeof(OtherGlobals), "OtherGlobal");
```
