using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain
{
    public class ConfigurationFile : IConfigurationFile
    {
        private readonly IConfiguration configuration;
        public ConfigurationFile(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string EuRestCountriesUrl
        {
            get
            {
                return this.configuration.GetValue<string>("euRestCountriesUrl");
            }
        }

        public string HostUrl
        {
            get
            {
                return this.configuration.GetValue<string>("HostUrl");
            }
        }
    }
}
