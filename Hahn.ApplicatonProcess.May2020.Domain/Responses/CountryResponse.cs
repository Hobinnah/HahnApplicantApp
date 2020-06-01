using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain
{

    public class CountryResponse
    {
        public List<Currency> Currencies { get; set; }

        public string Name { get; set; }

        public string Capital { get; set; }
    }

    public class Currency
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }
    }

}
