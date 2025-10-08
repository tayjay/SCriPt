using System.Collections.Generic;

namespace SCriPt.LabAPI.Utils;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

public static class MarkdownDocGen
{
    // ----- NEW: Assembly printer -----

    /// <summary>
    /// Writes Markdown documentation for all (or filtered) public types in the assembly.
    /// Groups by namespace and includes a table of contents with anchors.
    /// </summary>
    /// <param name="assembly">The assembly to document.</param>
    /// <param name="writer">Destination writer.</param>
    /// <param name="typeFilter">Optional filter; return true to include the type.</param>
    public static void PrintAssembly(Assembly assembly, TextWriter writer, Func<Type, bool>? typeFilter = null)
    {
        var asmName = assembly.GetName().Name ?? "Assembly";
        writer.WriteLine($"# {EscapeMd(asmName)} API Reference");
        writer.WriteLine();
        writer.WriteLine($"_Generated on {DateTime.UtcNow:yyyy-MM-dd} (UTC)_");
        writer.WriteLine();

        // Collect types (public + nested public) and apply optional filter
        // You can switch to assembly.GetExportedTypes() to only include publicly exported ones.
        var types = assembly.GetTypes()
                            .Where(t => (t.IsPublic || t.IsNestedPublic))
                            .Where(t => typeFilter?.Invoke(t) ?? true)
                            .ToList();

        // Group by namespace (null -> empty "(global)")
        var byNs = types.GroupBy(t => t.Namespace ?? "(global)")
                        .OrderBy(g => g.Key, StringComparer.Ordinal)
                        .ToList();

        // ---------- Table of Contents ----------
        writer.WriteLine("## Table of Contents");
        writer.WriteLine();

        foreach (var nsGroup in byNs)
        {
            var nsAnchor = ToAnchor(nsGroup.Key);
            writer.WriteLine($"- [{EscapeMd(nsGroup.Key)}](#{nsAnchor})");

            foreach (var t in OrderTypes(nsGroup))
            {
                writer.WriteLine($"  - [{EscapeMd(GetTypeDisplayName(t))}](#{ToAnchor(GetFullHeadingForType(t))})");
            }
        }

        writer.WriteLine();

        // ---------- Namespaces + Types ----------
        foreach (var nsGroup in byNs)
        {
            // Namespace heading
            writer.WriteLine($"## {EscapeMd(nsGroup.Key)}");
            writer.WriteLine();

            foreach (var t in OrderTypes(nsGroup))
            {
                // Type heading (consistent with PrintTypeInfo header)
                writer.WriteLine($"### {EscapeMd(GetFullHeadingForType(t))}");
                writer.WriteLine();
                // Delegate to the per-type printer. It emits its own H1; we’ll adapt it to H3 here.
                // Simple trick: capture into a StringWriter and downshift the first heading level.
                using var sw = new StringWriter();
                PrintTypeInfo(t, sw);

                var typeDoc = sw.ToString();

                // Convert leading "# " to "#### " so we don't break the namespace H3 structure
                // and preserve the rest. We only adjust the very first heading line.
                var adjusted = DowngradeFirstH1ToH4(typeDoc);

                writer.Write(adjusted);
                writer.WriteLine();
            }
        }
    }

    // Order: Interfaces, Classes, Structs, Enums, Delegates, then everything else by name
    private static IEnumerable<Type> OrderTypes(IEnumerable<Type> types) =>
        types.OrderBy(t => GetTypeSortKey(t))
             .ThenBy(t => t.Name, StringComparer.Ordinal);

    private static int GetTypeSortKey(Type t) =>
        t.IsInterface ? 0 :
        (t.IsClass && !typeof(MulticastDelegate).IsAssignableFrom(t.BaseType ?? typeof(object))) ? 1 :
        (t.IsValueType && !t.IsEnum) ? 2 :
        t.IsEnum ? 3 :
        typeof(MulticastDelegate).IsAssignableFrom(t.BaseType ?? typeof(object)) ? 4 :
        5;

    private static string GetTypeDisplayName(Type t)
    {
        // Short readable name (no namespace) with generic params cleaned up
        if (t.IsGenericType)
        {
            var name = t.Name;
            var tick = name.IndexOf('`');
            if (tick >= 0) name = name[..tick];
            var args = t.GetGenericArguments().Select(a => a.Name);
            return $"{name}<{string.Join(", ", args)}>";
        }
        return t.Name;
    }

    private static string GetFullHeadingForType(Type t)
    {
        // Match the heading that PrintTypeInfo uses at top (“FullName”)
        // But normalize nested types with '.' instead of '+' for nicer anchors
        var full = t.FullName ?? t.Name;
        return full.Replace('+', '.');
    }

    private static string DowngradeFirstH1ToH4(string md)
    {
        // Replace only the first line starting with "# " with "#### "
        using var reader = new StringReader(md);
        var sb = new StringBuilder();
        var first = true;
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            if (first && line.StartsWith("# "))
            {
                sb.AppendLine("####" + line[1..]); // "# " -> "#### "
                first = false;
                continue;
            }
            sb.AppendLine(line);
            first = false;
        }
        return sb.ToString();
    }

    private static string ToAnchor(string text)
    {
        // GitHub-like anchor: lower, trim, remove backticks, collapse spaces, strip punctuation except - _
        var s = text.Trim().ToLowerInvariant();
        s = s.Replace("`", "");
        var sb = new StringBuilder();
        foreach (var ch in s)
        {
            if (char.IsLetterOrDigit(ch) || ch is '-' or '_' or ' ')
                sb.Append(ch);
        }
        return sb.ToString().Replace(' ', '-');
    }
    
    
    
    /// <summary>
    /// Writes Markdown documentation for the provided Type to the given writer.
    /// Generates tables for fields, properties, events, and methods.
    /// </summary>
    public static void PrintTypeInfo(Type type, TextWriter writer)
    {
        // Header
        writer.WriteLine($"# {EscapeMd(type.FullName)}");
        writer.WriteLine();
        writer.WriteLine($"- **Kind:** {(type.IsClass ? "Class" : type.IsInterface ? "Interface" : type.IsEnum ? "Enum" : type.IsValueType ? "Struct" : "Type")}");
        if (type.BaseType != null && type.BaseType != typeof(object))
            writer.WriteLine($"- **Base Type:** `{GetFriendlyTypeName(type.BaseType)}`");
        if (type.IsGenericTypeDefinition)
            writer.WriteLine($"- **Generic Parameters:** {string.Join(", ", type.GetGenericArguments().Select(a => $"`{a.Name}`"))}");
        writer.WriteLine();

        // Binding flags (match your original intent)
        var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

        // Enums: show values and exit early (enums rarely have other members of interest)
        if (type.IsEnum)
        {
            var underlying = Enum.GetUnderlyingType(type);
            writer.WriteLine($"## Enum Values ({underlying.Name})");
            writer.WriteLine();
            writer.WriteLine("| Name | Value |");
            writer.WriteLine("|---|---|");
            foreach (var name in Enum.GetNames(type))
            {
                var value = Convert.ChangeType(Enum.Parse(type, name), underlying);
                writer.WriteLine($"| `{EscapePipes(name)}` | `{value}` |");
            }
            writer.WriteLine();
            return;
        }

        // Fields
        var fields = type.GetFields(flags)
                         .OrderByDescending(f => f.IsStatic)
                         .ThenBy(f => f.Name)
                         .ToArray();
        if (fields.Length > 0)
        {
            writer.WriteLine("## Fields");
            writer.WriteLine();
            writer.WriteLine("| Name | Type | Modifiers |");
            writer.WriteLine("|---|---|---|");
            foreach (var f in fields)
            {
                var mods = new StringBuilder();
                if (f.IsLiteral && !f.IsInitOnly) mods.Append("const ");
                if (f.IsInitOnly) mods.Append("readonly ");
                if (f.IsStatic && !(f.IsLiteral && !f.IsInitOnly)) mods.Append("static ");
                mods.Append(GetVisibility(f));
                writer.WriteLine($"| `{EscapePipes(f.Name)}` | `{GetFriendlyTypeName(f.FieldType)}` | `{mods}` |");
            }
            writer.WriteLine();
        }

        // Properties
        var props = type.GetProperties(flags)
                        .OrderByDescending(p => IsStatic(p))
                        .ThenBy(p => p.Name)
                        .ToArray();
        if (props.Length > 0)
        {
            writer.WriteLine("## Properties");
            writer.WriteLine();
            writer.WriteLine("| Name | Type | Get | Set | Static |");
            writer.WriteLine("|---|---|---|---|---|");
            foreach (var p in props)
            {
                var getM = p.GetGetMethod(true);
                var setM = p.GetSetMethod(true);
                var getStr = getM != null ? $"`{GetVisibility(getM)}`" : "";
                var setStr = setM != null ? $"`{GetVisibility(setM)}`" : "";
                writer.WriteLine($"| `{EscapePipes(p.Name)}` | `{GetFriendlyTypeName(p.PropertyType)}` | {getStr} | {setStr} | {(IsStatic(p) ? "`yes`" : "")} |");
            }
            writer.WriteLine();
        }

        // Events
        var evts = type.GetEvents(flags)
                       .OrderByDescending(e => IsStatic(e))
                       .ThenBy(e => e.Name)
                       .ToArray();
        if (evts.Length > 0)
        {
            writer.WriteLine("## Events");
            writer.WriteLine();
            writer.WriteLine("| Name | Handler Type | Static |");
            writer.WriteLine("|---|---|---|");
            foreach (var e in evts)
            {
                writer.WriteLine($"| `{EscapePipes(e.Name)}` | `{GetFriendlyTypeName(e.EventHandlerType)}` | {(IsStatic(e) ? "`yes`" : "")} |");
            }
            writer.WriteLine();
        }

        // Methods
        var methods = type.GetMethods(flags)
                          .Where(m => !m.IsSpecialName) // skip property/event accessors, operators' special names etc.
                          .OrderByDescending(m => m.IsStatic)
                          .ThenBy(m => m.Name)
                          .ToArray();
        if (methods.Length > 0)
        {
            writer.WriteLine("## Methods");
            writer.WriteLine();
            writer.WriteLine("| Name | Signature | Returns | Modifiers |");
            writer.WriteLine("|---|---|---|---|");
            foreach (var m in methods)
            {
                var mods = $"{(m.IsStatic ? "static " : "")}{GetVisibility(m)}";
                var sig = new StringBuilder();
                if (m.IsGenericMethodDefinition)
                {
                    sig.Append(m.Name);
                    sig.Append('<');
                    sig.Append(string.Join(", ", m.GetGenericArguments().Select(a => a.Name)));
                    sig.Append('>');
                }
                else
                {
                    sig.Append(m.Name);
                }
                sig.Append('(');
                sig.Append(string.Join(", ", m.GetParameters().Select(FormatParameter)));
                sig.Append(')');

                writer.WriteLine($"| `{EscapePipes(m.Name)}` | `{EscapePipes(sig.ToString())}` | `{GetFriendlyTypeName(m.ReturnType)}` | `{mods}` |");
            }
            writer.WriteLine();
        }
    }

    // -------- Helpers --------

    private static string GetVisibility(FieldInfo f) =>
        f.IsPublic ? "public" :
        f.IsFamily ? "protected" :
        f.IsAssembly ? "internal" :
        f.IsFamilyOrAssembly ? "protected internal" :
        f.IsPrivate ? "private" : "non-public";

    private static string GetVisibility(MethodBase m) =>
        m.IsPublic ? "public" :
        m.IsFamily ? "protected" :
        m.IsAssembly ? "internal" :
        m.IsFamilyOrAssembly ? "protected internal" :
        m.IsPrivate ? "private" : "non-public";

    private static bool IsStatic(PropertyInfo p)
    {
        var acc = p.GetGetMethod(true) ?? p.GetSetMethod(true);
        return acc?.IsStatic ?? false;
    }

    private static bool IsStatic(EventInfo e) =>
        e.GetAddMethod(true)?.IsStatic ?? false;

    private static string FormatParameter(ParameterInfo p)
    {
        var prefix =
            p.IsOut ? "out " :
            p.ParameterType.IsByRef && !p.IsOut ? "ref " :
            p.GetCustomAttributes(typeof(ParamArrayAttribute), false).Any() ? "params " : "";

        var type = p.ParameterType;
        if (type.IsByRef) type = type.GetElementType()!;

        var defaultSuffix = p.HasDefaultValue
            ? $" = {FormatDefaultValue(p.DefaultValue)}"
            : "";

        return $"{prefix}{GetFriendlyTypeName(type)} {p.Name}{defaultSuffix}";
    }

    private static string FormatDefaultValue(object? v)
    {
        if (v == null) return "null";
        return v is string s ? "\"" + s.Replace("\"", "\\\"") + "\"" :
               v is char c ? $"'{c}'" :
               v is bool b ? (b ? "true" : "false") :
               Convert.ToString(v, System.Globalization.CultureInfo.InvariantCulture) ?? "null";
    }

    private static string GetFriendlyTypeName(Type t)
    {
        if (t.IsGenericParameter) return t.Name;

        if (t.IsArray)
            return GetFriendlyTypeName(t.GetElementType()!) + "[]";

        if (t.IsPointer)
            return GetFriendlyTypeName(t.GetElementType()!) + "*";

        if (t.IsByRef)
            return GetFriendlyTypeName(t.GetElementType()!) + "&";

        if (t.IsGenericType)
        {
            var name = t.Name;
            var tick = name.IndexOf('`');
            if (tick >= 0) name = name.Substring(0, tick);
            var args = t.GetGenericArguments().Select(GetFriendlyTypeName);
            return $"{(t.IsNested ? t.DeclaringType!.FullName + "+" : t.Namespace + ".")}{name}<{string.Join(", ", args)}>";
        }

        // Map common CLR names to C# aliases
        return t switch
        {
            _ when t == typeof(void) => "void",
            _ when t == typeof(bool) => "bool",
            _ when t == typeof(byte) => "byte",
            _ when t == typeof(sbyte) => "sbyte",
            _ when t == typeof(short) => "short",
            _ when t == typeof(ushort) => "ushort",
            _ when t == typeof(int) => "int",
            _ when t == typeof(uint) => "uint",
            _ when t == typeof(long) => "long",
            _ when t == typeof(ulong) => "ulong",
            _ when t == typeof(float) => "float",
            _ when t == typeof(double) => "double",
            _ when t == typeof(decimal) => "decimal",
            _ when t == typeof(string) => "string",
            _ when t == typeof(object) => "object",
            _ => t.FullName ?? t.Name
        };
    }

    private static string EscapeMd(string s) =>
        s.Replace("_", "\\_").Replace("*", "\\*").Replace("`", "\\`");

    private static string EscapePipes(string s) =>
        s.Replace("|", "\\|");
}
