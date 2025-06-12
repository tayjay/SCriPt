Welcome to the SCriPt wiki!

This will be the home for all documentation for the SCriPt plugin. Please check out the starting tutorials, and API references for writing your own Lua Scripts.

## What EXILED Version is this for?
Currently this plugin is written for the Original EXILED 8 framework. Most functionality should work with the EXILED Reboot 8 branch, but will break when EXILED Reboot 9 is released.
I will be keeping an eye on the development of EXILED Reboot and will update the plugin if needed.


## Introduction

This is the definitive guide to using the SCriPt "Lua -> EXILED" plugin for SCP:SL. This guide will cover all the types, events, and global variables that are available to you when writing scripts for the plugin.
Scripts ran by this plugin do not require any kind of compilation, 

This plugin is meant to be a middle ground between plugins in "ScriptedEvents" and making an EXILED plugin. 
ScriptedEvents is an amazing plugin if you want to make a quick game mode as long as you learn their language. I needed something with a little more control.

To start, a solid understanding of Lua is beneficial. Programming knowledge in general will also let you get started.
You will also want to understand Event Based systems, as it's the core of the plugin.
For bonus points, knowing how MoonSharp works for its translation of Lua to C# will also be helpful.

The target audience of this plugin is between a few groups:
 - Plugin Developers who want to automate certain tasks on their testing server and not need to change their own code
 - Server Owners who want to add custom functionality to their server without needing to create, commission, or install a plugin
 - Server Owners that have a repetitive task that they believe could be automated

If you are running a verified server, be sure to check <https://scpslgame.com/Verified_server_rules.pdf> to make sure your scripts are not breaking any rules.

Please review the reminders below for more information on safety.

Once you're ready, you can go to [Getting Started](https://github.com/tayjay/SCriPt/wiki/Getting-Started)

## Important Reminders

- Back up important data before executing any new or unknown scripts.
- Research Lua scripting principles and best practices for secure coding.
- Never run scripts from untrusted sources.
- By using this plugin, you acknowledge and agree to the terms of the Disclaimer

## Disclaimer
This plugin enables the ability to use Lua scripts for task automation. Please be advised of the following:

- Potential for Abuse: While Lua scripts offer valuable automation tools, they can also be misused. Misconfigured or intentionally malicious scripts may cause disruptions, in-game issues, or stability problems within the server environment.
- Responsibility: Users are fully responsible for the Lua scripts they write, execute, or share. Neither the server owners nor the plugin creators are liable for any damages or negative consequences arising from the use of these scripts.
- Use at Your Own Risk: You agree to use this modification and any associated scripting features at your own risk. Always exercise caution and thoroughly test scripts in a safe environment before deployment.
- No Guarantees: The server owners and plugin creators make no guarantees regarding the safety, reliability, or error-free performance of user-created scripts.
