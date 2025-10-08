using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using CommandSystem;
using LabApi.Features.Console;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;
using MEC;
using SCriPt.LabAPI.API.Lua.Objects;
using SCriPt.LabAPI.Handlers;
using SCriPt.LabAPI.Utils;

namespace SCriPt.LabAPI.Commands;

public class LuaDocsCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if(!sender.HasPermissions("script.docs"))
        {
            response = "You do not have permission to use this command.";
            Logger.Warn("Player attempted to generate documentation without permission.");
            return false;
        }
        Logger.Info("Starting Documentation Generation...");
        Timing.CallDelayed(Timing.WaitForOneFrame, () =>
        {
            ScriptLoader.ScriptPathParent.CreateSubdirectory("SCriPt/Docs");
            WriteDocumentation(typeof(Player).Assembly);
            WriteDocumentation(typeof(ReferenceHub).Assembly);
            WriteDocumentation(typeof(ScriptHandler).Assembly);
            sender.Respond("Documentation Generation Complete. Can be found in the SCriPt/Docs directory.");
            Logger.Info("Documentation Generation Complete.");
        });
        
        
        response = "Documentation Generation Started...";
        return true;
        
    }
    
    /// <summary>
    /// Generates a documentation file for the given assembly.
    /// </summary>
    /// <param name="assembly">The assembly to document.</param>
    private void WriteDocumentation(Assembly assembly)
    {
        // Define the output path for the documentation file.
        string outputFileName = ScriptLoader.ScriptPathParent.FullName + $"/SCriPt/Docs/{assembly.GetName().Name}.md";
        Directory.CreateDirectory(Path.GetDirectoryName(outputFileName)); // Ensure the directory exists.

        try
        {
            // --- 1. Load the Assembly ---
            Logger.Info($"Reflecting Assembly: {assembly.GetName().Name}...");

            using var fs = File.CreateText(outputFileName);
            MarkdownDocGen.PrintAssembly(assembly, fs);

            Logger.Info($"\n--- Success! ---");
            Logger.Info($"Documentation has been written to: {Path.GetFullPath(outputFileName)}");

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Logger.Info($"An error occurred: {ex.Message}");
            Console.ResetColor();
        }

        Logger.Info("\nPress any key to exit.");
    }


    /// <summary>
    /// Writes formatted information about a given Type to the specified TextWriter.
    /// This method inspects the type and lists its fields, properties, events, and methods.
    /// </summary>
    /// <param name="type">The type to inspect.</param>
    /// <param name="writer">The TextWriter (e.g., a StreamWriter) to write to.</param>
    public static void PrintTypeInfo(Type type, TextWriter writer)
    {
        writer.WriteLine($"TYPE: {type.FullName}");
        writer.WriteLine($"  Is Class: {type.IsClass}");
        writer.WriteLine($"  Is Interface: {type.IsInterface}");
        writer.WriteLine($"  Is Enum: {type.IsEnum}");
        writer.WriteLine();
        
        // --- BindingFlags are used to filter the members we want to retrieve ---
        // We look for public members, both on instances and static, but only those declared on the current type.
        BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

        // --- Find and Print Members (Fields, Properties, Events, Methods) ---
        
        // Fieldsr
        FieldInfo[] fields = type.GetFields(flags);
        if (fields.Length > 0)
        {
            writer.WriteLine("  Fields:");
            foreach (FieldInfo field in fields)
            {
                string staticPrefix = field.IsStatic ? "[Static] " : "";
                writer.WriteLine($"    -> {staticPrefix}{field.FieldType.Name} {field.Name}");
            }
            writer.WriteLine();
        }

        // Properties
        PropertyInfo[] properties = type.GetProperties(flags);
        if (properties.Length > 0)
        {
            writer.WriteLine("  Properties:");
            foreach (PropertyInfo prop in properties)
            {
                var accessors = new StringBuilder();
                if (prop.CanRead) accessors.Append("get; ");
                if (prop.CanWrite) accessors.Append("set; ");

                // Check if the property is static by inspecting its accessor methods.
                bool isStatic = (prop.GetGetMethod(true) ?? prop.GetSetMethod(true))?.IsStatic ?? false;
                string staticPrefix = isStatic ? "[Static] " : "";

                writer.WriteLine($"    -> {staticPrefix}{prop.PropertyType.Name} {prop.Name} {{ {accessors} }}");
            }
            writer.WriteLine();
        }
        
        // *** NEW: Events ***
        EventInfo[] events = type.GetEvents(flags);
        if (events.Length > 0)
        {
            writer.WriteLine("  Events:");
            foreach (EventInfo evt in events)
            {
                // Check if the event is static by inspecting its 'add' accessor method.
                bool isStatic = evt.GetAddMethod(true)?.IsStatic ?? false;
                string staticPrefix = isStatic ? "[Static] " : "";

                // The EventHandlerType is the delegate type for the event (e.g., EventHandler).
                writer.WriteLine($"    -> {staticPrefix}{evt.EventHandlerType.Name} {evt.Name}");
            }
            writer.WriteLine();
        }

        // Methods
        MethodInfo[] methods = type.GetMethods(flags);
        if (methods.Length > 0)
        {
            writer.WriteLine("  Methods:");
            foreach (MethodInfo method in methods)
            {
                // IsSpecialName is true for property getters/setters and event add/remove methods, which we want to skip.
                if (method.IsSpecialName) continue;
                
                string staticPrefix = method.IsStatic ? "[Static] " : "";
                
                // Build the parameter string (e.g., "string name, int count").
                var parameters = new StringBuilder();
                ParameterInfo[] paramInfos = method.GetParameters();
                for(int i = 0; i < paramInfos.Length; i++)
                {
                    parameters.Append($"{paramInfos[i].ParameterType.Name} {paramInfos[i].Name}");
                    if (i < paramInfos.Length - 1)
                    {
                        parameters.Append(", ");
                    }
                }
                writer.WriteLine($"    -> {staticPrefix}{method.ReturnType.Name} {method.Name}({parameters})");
            }
            writer.WriteLine();
        }
    }

    public string Command { get; } = "docs";
    public string[] Aliases { get; } = new string[] { "documentation", "docgen" };
    public string Description { get; } = "Generates documentation for the SCriPt API in the SCriPt/Docs directory. Requires script.docs permission.";
}