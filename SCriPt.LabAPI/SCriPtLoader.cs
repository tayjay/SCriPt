using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using SCriPt.LabAPI.Handlers;

namespace SCriPt.LabAPI;

public class SCriPtLoader : Plugin
{
    public override void Enable()
    {
        ScriptLoader.LoadScripts();
    }

    public override void Disable()
    {
        ScriptLoader.UnloadAllScripts();
    }

    public override string Name { get; } = "SCriPt.Loader";
    public override string Description { get; } = "Loads scripts after SCriPt and all dependants have been loaded.";
    public override string Author { get; } = "TayTay";
    public override Version Version { get; } = new Version(1, 0, 0, 0);
    public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.Lowest;
}