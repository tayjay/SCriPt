using LabApi.Features.Wrappers;
using MoonSharp.Interpreter;

namespace SCriPt.LabAPI.API.Lua.Proxies;

public class ProxyCapybaraToy : ProxyAdminToy
{
    CapybaraToy CapybaraToy { get; }
        
    [MoonSharpHidden]
    public ProxyCapybaraToy(CapybaraToy capybara) : base(capybara)
    {
        CapybaraToy = capybara;
    }
    
    public bool CollidersEnabled
    {
        get => CapybaraToy.CollidersEnabled;
        set => CapybaraToy.CollidersEnabled = value;
    }
    
    
    
}