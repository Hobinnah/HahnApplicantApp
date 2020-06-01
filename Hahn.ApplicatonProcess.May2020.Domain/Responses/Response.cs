using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.Responses
{
    public class Response<T> where T : class
    {
        public String ResponseCode { get; set; }
        public String Description { get; set; }
        public string LoggerID { get; set; }
        public T Result { get; set; }
    }
}
