using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain
{
    public interface IConfigurationFile
    {
        string EuRestCountriesUrl { get; }
        string HostUrl { get; }
        
    }
}
