using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.Models
{
    public class Applicant
    {
        [Key]
        public int ID { get; set; }

        //[Required(ErrorMessage = "The name field is compulsory and must be supplied")]
        //[MinLength(5)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "The family name field is compulsory and must be supplied")]
        //[MinLength(5)]
        public string FamilyName { get; set; }

        //[MinLength(10)]
        //[Required(ErrorMessage = "The address field is compulsory and must be supplied")]
        public string Address { get; set; }
        
        //[Required(ErrorMessage = "The country field is compulsory and must be supplied")]
        public string CountryOfOrigin { get; set; }

        //[DataType(DataType.EmailAddress)]
        //[Required(ErrorMessage = "The email field is compulsory and must be supplied")]
        public string EMailAddress { get; set; }

        //[Required(ErrorMessage = "The age field is compulsory and must be supplied")]
        //[Range(20, 60, ErrorMessage = "Age Must be between 20 to 60")]
        public int Age { get; set; }
        public bool Hired { get; set; } = false;
    }
}
