using System;
using System.Collections.Generic;

namespace STA_Coding_Challenge.Config
{
    public class EnvironmentData
    {// Gets or sets the data for a specific environment configuration.
        public string Name { get; set; }
        public string Url { get; set; }
        public string ConnectionString { get; set; }
        public string ResultUrl { get; set; }
    }

    public class EnvironmentConfig
    {// Represents the configuration for multiple environments.
        public List<EnvironmentData> Environments { get; set; }
    }
}
