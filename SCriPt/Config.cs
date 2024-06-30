using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SCriPt
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        
        [Description("What level should Lua print statement be made at? 0=none, 1=errors, 2=warnings, 3=info, 4=debug")]
        public int PrintLevel { get; set; } = 4;
        
        //todo: Configs to implement
        // ScriptPath Where to place the scripts, defaults to location of LocalAdmin
        [Description("Where to place the scripts, defaults to location of LocalAdmin. Use '/'s and be sure to include the trailing slash.")]
        public string ScriptPath { get; set; } = "";
        // CoroutineAutoYield How many operations can be done by a coroutine before it yields, default is 
        [Description("How many operations can be done by a coroutine before it yields, default is 1000000. If coroutines are dying early this can be set to 0, this can cause infinte loops if not accounted for.")]
        public int CoroutineAutoYield { get; set; } = 1000000;
        
        // AutoDownloadMoonSharp Should MoonSharp be downloaded automatically, default is true
        /*[Description("Should MoonSharp be downloaded automatically, default is true")]
        public bool AutoDownloadMoonSharp { get; set; } = true;*/
        
        // SandboxLevel What level of sandboxing should be used, default is Default
        [Description("What level of sandboxing should be used, default is Default. From least to most permissive the options are Hard, Soft, Default, Complete")]
        public string SandboxLevel { get; set; } = "Soft";
        
        // Access Level
        [Description("What level of access should be given to the system, default is Standard. Options are Limited, Standard")]
        public string SystemAccessLevel { get; set; } = "Limited";
    }
}