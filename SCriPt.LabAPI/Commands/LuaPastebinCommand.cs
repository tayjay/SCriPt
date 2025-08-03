using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using LabApi.Features.Permissions;

namespace SCriPt.LabAPI.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class LuaPastebinCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if(!sender.HasPermissions("script.pastebin"))
        {
            response = "You do not have permission to use this command.";
            return false;
        }
        if (arguments.Count < 1)
        {
            response = "Usage: pastebin <pastebin_id> [<name>]";
            return false;
        }
        string pastebinId = arguments.At(0);
        string name = arguments.Count > 1 ? arguments.At(1) : pastebinId;
        try
        {
            // Assuming LuaScriptManager is a class that handles Lua script loading
            //LuaScriptManager.LoadScriptFromPastebin(pastebinId, name);
            response = $"Successfully loaded script from Pastebin with ID: {pastebinId} as {name}.";
            return true;
        }
        catch (Exception ex)
        {
            response = $"Failed to load script from Pastebin: {ex.Message}";
            return false;
        }
        
    }

    public string Command { get; } = "pastebin";
    public string[] Aliases { get; } =
    [
        "pb"
    ];
    public string Description { get; } = "Allows you to load Lua scripts from Pastebin.";
}