# Wrappers

## What is a Wrapper?
When setting up this plugin, there are 2 main ways to include C# Types into your Lua scripts:
- The first is by directly registering them. This has the benefit of both being easy to implement/update, and gives full access to the object in Lua. However, specific control on access to a type's members is lost.
- The second is through a Wrapper. This is needed when the compiler cannot decern a type based on duplicate names, or if more specific access control is required on a Type. The benefit is greater control and expandability on specific types. However, it adds a lot of development overhead, especially if the base type is updated.

In a perfect world, all types would be implemented as wrappers. This would be safer and give access to easier calls in Lua. However, it's a matter of time and effort to implement them all. As such, the plugin has a few wrappers implemented, and more will be added over time.

## How to use a Wrapper
If a wrapper is required, it will be applied automatically by the plugin. You will just need to be aware of the available methods and properties on the wrapper, as they may differ from the base type.

