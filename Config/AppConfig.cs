using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using System;

namespace STA_Coding_Challenge.Config
{
    public class AppConfig
    {
        // Gets or sets the environment the application is running in (ex. QA, UAT, Prod)
        public string Environment { get; set; }
        // Gets or sets the browser to be used for automation (ex. Chrome, Firefox
        public string Browser { get; set; }
    }
}
