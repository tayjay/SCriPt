using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using SCriPt.LabAPI.Handlers;
using Logger = LabApi.Features.Console.Logger;

namespace SCriPt.LabAPI.Utils;

/// <summary>
/// Generates a Lua-focused API reference markdown file from the runtime-registered types,
/// globals, events, and enums.
/// </summary>
public static class LuaDocGenerator
{
    public static void Generate()
    {
        try
        {
            var docsDir = Path.Combine(ScriptLoader.ScriptPathParent.FullName, "SCriPt", "Docs");
            Directory.CreateDirectory(docsDir);
            var filePath = Path.Combine(docsDir, "LuaAPI.md");

            using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
            writer.WriteLine("# SCriPt Lua API Reference");
            writer.WriteLine();
            writer.WriteLine($"_Auto-generated on {DateTime.UtcNow:yyyy-MM-dd HH:mm} UTC_");
            writer.WriteLine();

            WriteGlobals(writer);
            WriteEvents(writer);
            WriteTypes(writer);
            WriteEnums(writer);

            Logger.Info($"[LuaDocGenerator] Generated API docs at {filePath}");
        }
        catch (Exception e)
        {
            Logger.Error($"[LuaDocGenerator] Failed to generate docs: {e.Message}");
        }
    }

    private static void WriteGlobals(StreamWriter writer)
    {
        writer.WriteLine("## Globals");
        writer.WriteLine();

        // Track which globals we've already written (skip aliases)
        var writtenTypes = new HashSet<Type>();
        var aliases = new Dictionary<Type, List<string>>();

        // Group globals by underlying type to detect aliases
        foreach (var kvp in ScriptLoader.Globals.OrderBy(g => g.Key))
        {
            var type = GetUnderlyingType(kvp.Value);
            if (type == null || type.IsEnum) continue;

            if (!aliases.ContainsKey(type))
                aliases[type] = new List<string>();
            aliases[type].Add(kvp.Key);
        }

        foreach (var kvp in aliases.OrderBy(a => a.Value[0]))
        {
            var type = kvp.Key;
            var names = kvp.Value;

            var heading = names[0];
            if (names.Count > 1)
                heading += " (" + string.Join(", ", names.Skip(1)) + ")";

            writer.WriteLine($"### {heading}");
            writer.WriteLine();

            WritePublicMembers(writer, type);
            writer.WriteLine();
        }
    }

    private static void WriteEvents(StreamWriter writer)
    {
        writer.WriteLine("## Events");
        writer.WriteLine();
        writer.WriteLine("Access via `Events.<Category>.<EventName>`. Use `.add(callback)` / `.remove(callback)` or `mod.on(event, callback)`.");
        writer.WriteLine();

        if (SCriPt.Instance?.DynamicEventHandlers == null) return;

        foreach (var kvp in SCriPt.Instance.DynamicEventHandlers.OrderBy(h => h.Key))
        {
            var categoryName = kvp.Key;
            var handler = kvp.Value;

            writer.WriteLine($"### {categoryName}");
            writer.WriteLine();

            foreach (var evtKvp in handler.Events.OrderBy(e => e.Key))
            {
                var eventName = evtKvp.Key;

                // Get the args type from the source event
                var sourceEvent = handler.SourceType
                    .GetEvent(eventName, BindingFlags.Public | BindingFlags.Static);
                var argsType = GetEventArgsType(sourceEvent);

                if (argsType != null)
                {
                    writer.WriteLine($"- **{eventName}** ({argsType.Name})");

                    // List event args properties
                    var props = argsType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => !IsHidden(p))
                        .OrderBy(p => p.Name);

                    foreach (var prop in props)
                    {
                        var access = prop.CanRead && prop.CanWrite ? "get/set" : prop.CanRead ? "get" : "set";
                        writer.WriteLine($"  - {prop.Name}: `{GetSimpleTypeName(prop.PropertyType)}` ({access})");
                    }
                }
                else
                {
                    writer.WriteLine($"- **{eventName}**");
                }
            }

            writer.WriteLine();
        }
    }

    private static void WriteTypes(StreamWriter writer)
    {
        writer.WriteLine("## Types");
        writer.WriteLine();
        writer.WriteLine("LabAPI wrapper types available in Lua scripts.");
        writer.WriteLine();

        var wrapperAssembly = typeof(LabApi.Features.Wrappers.Player).Assembly;
        var wrapperTypes = wrapperAssembly.GetTypes()
            .Where(t => t.Namespace == "LabApi.Features.Wrappers" && t.IsPublic && !t.IsEnum && !t.IsInterface)
            .OrderBy(t => t.Name);

        foreach (var type in wrapperTypes)
        {
            if (!UserData.IsTypeRegistered(type)) continue;

            writer.WriteLine($"### {type.Name}");
            writer.WriteLine();
            WritePublicMembers(writer, type);
            writer.WriteLine();
        }
    }

    private static void WriteEnums(StreamWriter writer)
    {
        writer.WriteLine("## Enums");
        writer.WriteLine();

        var writtenTypes = new HashSet<Type>();
        var aliases = new Dictionary<Type, List<string>>();

        foreach (var kvp in ScriptLoader.Globals.OrderBy(g => g.Key))
        {
            var type = GetUnderlyingType(kvp.Value);
            if (type == null || !type.IsEnum) continue;

            if (!aliases.ContainsKey(type))
                aliases[type] = new List<string>();
            aliases[type].Add(kvp.Key);
        }

        foreach (var kvp in aliases.OrderBy(a => a.Value[0]))
        {
            var type = kvp.Key;
            var names = kvp.Value;

            var heading = names[0];
            if (names.Count > 1)
                heading += " (" + string.Join(", ", names.Skip(1)) + ")";

            writer.WriteLine($"### {heading}");
            writer.WriteLine();

            var enumNames = Enum.GetNames(type);
            // Limit very large enums
            if (enumNames.Length > 100)
            {
                foreach (var name in enumNames.Take(100))
                    writer.WriteLine($"- {name}");
                writer.WriteLine($"- _...and {enumNames.Length - 100} more_");
            }
            else
            {
                foreach (var name in enumNames)
                    writer.WriteLine($"- {name}");
            }

            writer.WriteLine();
        }
    }

    private static void WritePublicMembers(StreamWriter writer, Type type)
    {
        var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

        // Properties
        var props = type.GetProperties(flags)
            .Where(p => !p.IsSpecialName && !IsHidden(p))
            .OrderByDescending(p => IsStatic(p))
            .ThenBy(p => p.Name)
            .ToArray();

        if (props.Length > 0)
        {
            writer.WriteLine("**Properties:**");
            foreach (var p in props)
            {
                var prefix = IsStatic(p) ? "(static) " : "";
                var access = p.CanRead && p.CanWrite ? "get/set" : p.CanRead ? "get" : "set";
                writer.WriteLine($"- {prefix}{p.Name}: `{GetSimpleTypeName(p.PropertyType)}` ({access})");
            }
            writer.WriteLine();
        }

        // Methods
        var methods = type.GetMethods(flags)
            .Where(m => !m.IsSpecialName && !IsHidden(m))
            .OrderByDescending(m => m.IsStatic)
            .ThenBy(m => m.Name)
            .ToArray();

        if (methods.Length > 0)
        {
            writer.WriteLine("**Methods:**");
            foreach (var m in methods)
            {
                var prefix = m.IsStatic ? "(static) " : "";
                var parameters = string.Join(", ", m.GetParameters().Select(p =>
                    $"{GetSimpleTypeName(p.ParameterType)} {p.Name}"));
                var ret = m.ReturnType == typeof(void) ? "" : $" -> `{GetSimpleTypeName(m.ReturnType)}`";
                writer.WriteLine($"- {prefix}{m.Name}({parameters}){ret}");
            }
        }
    }

    private static Type GetUnderlyingType(DynValue dynValue)
    {
        try
        {
            if (dynValue.Type == DataType.UserData && dynValue.UserData?.Descriptor != null)
                return dynValue.UserData.Descriptor.Type;
        }
        catch
        {
            // ignore
        }

        return null;
    }

    private static Type GetEventArgsType(EventInfo eventInfo)
    {
        if (eventInfo == null) return null;
        var handlerType = eventInfo.EventHandlerType;
        if (handlerType.IsGenericType)
            return handlerType.GetGenericArguments()[0];
        return null;
    }

    private static bool IsHidden(MemberInfo member)
    {
        return member.GetCustomAttributes(true)
            .Any(a =>
            {
                var name = a.GetType().Name;
                return name == "MoonSharpHiddenAttribute" || name == "MoonSharpHideMemberAttribute";
            });
    }

    private static bool IsStatic(PropertyInfo p)
    {
        var acc = p.GetGetMethod(true) ?? p.GetSetMethod(true);
        return acc?.IsStatic ?? false;
    }

    private static string GetSimpleTypeName(Type t)
    {
        if (t == typeof(void)) return "void";
        if (t == typeof(bool)) return "bool";
        if (t == typeof(int)) return "int";
        if (t == typeof(float)) return "float";
        if (t == typeof(double)) return "double";
        if (t == typeof(string)) return "string";
        if (t == typeof(long)) return "long";
        if (t == typeof(byte)) return "byte";
        if (t == typeof(short)) return "short";
        if (t == typeof(uint)) return "uint";
        if (t == typeof(ulong)) return "ulong";
        if (t == typeof(object)) return "object";

        if (t.IsArray)
            return GetSimpleTypeName(t.GetElementType()) + "[]";

        if (t.IsByRef)
            return GetSimpleTypeName(t.GetElementType());

        if (t.IsGenericType)
        {
            var name = t.Name;
            var tick = name.IndexOf('`');
            if (tick >= 0) name = name.Substring(0, tick);
            var args = string.Join(", ", t.GetGenericArguments().Select(GetSimpleTypeName));
            return $"{name}<{args}>";
        }

        return t.Name;
    }
}
