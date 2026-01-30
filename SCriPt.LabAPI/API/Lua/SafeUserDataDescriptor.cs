using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using Logger = LabApi.Features.Console.Logger;

namespace SCriPt.LabAPI.API.Lua;

/// <summary>
/// A UserData descriptor that gracefully handles types with duplicate/conflicting members
/// (e.g. LabAPI wrapper classes that shadow base class members with 'new').
/// Subclasses DispatchingUserDataDescriptor directly to avoid StandardUserDataDescriptor's
/// private FillMemberList which throws on conflicts.
/// </summary>
public class SafeUserDataDescriptor : DispatchingUserDataDescriptor
{
    public InteropAccessMode AccessMode { get; }

    public SafeUserDataDescriptor(Type type, InteropAccessMode accessMode, string friendlyName = null)
        : base(type, friendlyName)
    {
        if (accessMode == InteropAccessMode.Default)
            accessMode = UserData.DefaultAccessMode;

        AccessMode = accessMode;

        FillMemberList(type);
    }

    private void FillMemberList(Type type)
    {
        // Build ignore list from MoonSharpHideMemberAttribute
        var hideMembers = type.GetCustomAttributes(typeof(MoonSharpHideMemberAttribute), true)
            .Cast<MoonSharpHideMemberAttribute>()
            .Select(a => a.MemberName)
            .ToHashSet();

        if (AccessMode == InteropAccessMode.HideMembers)
            return;

        var bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy;

        // Constructors
        foreach (var ci in type.GetConstructors(BindingFlags.Public | BindingFlags.Instance))
        {
            if (hideMembers.Contains("__new")) continue;
            var desc = MethodMemberDescriptor.TryCreateIfVisible(ci, AccessMode);
            TryAddMember("__new", desc);
        }

        // Methods
        foreach (var mi in type.GetMethods(bindingFlags).Where(m => !m.IsSpecialName))
        {
            if (hideMembers.Contains(mi.Name)) continue;
            var desc = MethodMemberDescriptor.TryCreateIfVisible(mi, AccessMode);
            TryAddMember(mi.Name, desc);
        }

        // Properties
        foreach (var pi in type.GetProperties(bindingFlags))
        {
            if (hideMembers.Contains(pi.Name)) continue;
            var desc = PropertyMemberDescriptor.TryCreateIfVisible(pi, AccessMode);
            TryAddMember(pi.Name, desc);
        }

        // Fields
        foreach (var fi in type.GetFields(bindingFlags))
        {
            if (hideMembers.Contains(fi.Name)) continue;
            var desc = FieldMemberDescriptor.TryCreateIfVisible(fi, AccessMode);
            TryAddMember(fi.Name, desc);
        }

        // Events
        foreach (var ei in type.GetEvents(bindingFlags))
        {
            if (hideMembers.Contains(ei.Name)) continue;
            var desc = EventMemberDescriptor.TryCreateIfVisible(ei, AccessMode);
            TryAddMember(ei.Name, desc);
        }

        // Nested types
        foreach (var nestedType in type.GetNestedTypes(BindingFlags.Public))
        {
            if (hideMembers.Contains(nestedType.Name)) continue;
            if (!UserData.IsTypeRegistered(nestedType))
                UserData.RegisterType(nestedType, AccessMode);

            var dynVal = UserData.CreateStatic(nestedType);
            if (dynVal != null)
                AddDynValue(nestedType.Name, dynVal);
        }
    }

    private void TryAddMember(string name, IMemberDescriptor desc)
    {
        if (desc == null) return;

        try
        {
            AddMember(name, desc);
        }
        catch (ArgumentException)
        {
            //Logger.Warn($"[SafeUserDataDescriptor] Skipped conflicting member '{name}' on type '{Type.Name}'");
        }
    }
}
