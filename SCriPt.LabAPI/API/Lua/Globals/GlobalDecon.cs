using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.API.Lua.Globals;

[MoonSharpUserData]
public class GlobalDecon
{
    public static void Disable()
    {
        LightContainmentZoneDecontamination.DecontaminationController.Singleton.NetworkDecontaminationOverride =
            LightContainmentZoneDecontamination.DecontaminationController.DecontaminationStatus.Disabled;
    }
    
    public static void Force()
    {
        LightContainmentZoneDecontamination.DecontaminationController.Singleton.ForceDecontamination();
    }
}