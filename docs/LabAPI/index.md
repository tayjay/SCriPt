# SCriPt.LabAPI - SCP:SL Lua Plugin Library


## What is SCriPt?
SCriPt is an SCP:SL plugin that allows server owners to write their own pseudo-plugins in Lua.
This project began as a way for me to automatically configure my dev server on server startup, but has since evolved into a full-fledged plugin library.
This project has been in development since early 2024.

## What the heck is that name?
The name SCriPt is a play on words, combining "SCP" and "Script". It is pronounced like "script", but with the "SCP" prefix to indicate its connection to SCP:SL.


## Why LabApi?
Now that the SCP:SL dev team has done amazing work on developing the LabApi plugin library. Many of its features mirror those of EXILED.
In recent months, the EXILED modding community has been in a state of uncertainty.
To ensure a smooth development experience and to broaden the reach of my plugin, I have decided to develop SCriPt on the LabApi library.

## How does it work?
SCriPt.LabAPI is a Lua plugin library designed to provide a bridge between Lua scripts and the LabAPI plugin library for SCP:SL.
A server owner or developer can write Lua scripts that hook into the LabApi events and objects the same as they would with C# plugins.
There are some liberties taken with the structure of Lua scripts so the may not follow official Lua style guides. However, this is meant to make the code more resemble C# plugins.

On the backend, Lua is handled using the MoonSharp library, which provides a powerful Lua interpreter for .NET applications. This library is a required dependency for SCriPt, and must be placed in the SCP:SL dependency folder.

## Documentation
The following pages on this site provide documentation for the SCriPt.LabAPI library. Before getting into this, an understanding of programming is needed. Knowledge of Lua specifically is highly recommended as well, but knowing the basics is enough to get started. 

If you want to get into SCriPt, [Click Here](https://tayjay.github.io/SCriPt/LabAPI/Getting-Started.html) to get started.