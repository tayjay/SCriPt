using System.ComponentModel;

namespace SCriPt.LabAPI;

public class Config
{
    // CoroutineAutoYield How many operations can be done by a coroutine before it yields, default is 1,000,000.
    [Description("How many operations can be done by a coroutine before it yields, default is 1000000. If coroutines are dying early this can be set to 0, this can cause infinte loops if not accounted for.")]
    public int CoroutineAutoYield { get; set; } = 1000000;
    
    // "Should scripts have full permissions? If false, default permissions will be applied to scripts, limiting their ability to interact with dangerous systems. If True, scripts will have full access to the server. Default is false."
    [Description("Should scripts have full permissions? If false, default permissions will be applied to scripts, limiting their ability to interact with dangerous systems. If True, scripts will have full access to the server. Default is false.")]
    public bool FullAccess { get; set; } = false;
}