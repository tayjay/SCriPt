using LightContainmentZoneDecontamination;

namespace SCriPt.EXILED.Extensions
{
    public static class DeconExtensions
    {
        public static void Disable(this DecontaminationController decon)
        {
            decon.NetworkDecontaminationOverride = DecontaminationController.DecontaminationStatus.Disabled;
        }
    }
}