using Hahn.ApplicatonProcess.May2020.Domain;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDBContext(serviceProvider.GetRequiredService<DbContextOptions<AppDBContext>>()))
            {
                // Look for any applicant.
                if (context.Applicants.Any())
                {
                    return;   // Data was already seeded
                }

                context.Applicants.AddRange(
                    new Applicant
                    {
                        ID = 1,
                        Address = "Suite 25, Osborne Drive",
                        Age = 35,
                        CountryOfOrigin ="Germany",
                        EMailAddress = "hobinnah@yahoo.com",
                        FamilyName = "Eze",
                        Name = "Obinna",
                        Hired = true
                    },
                    new Applicant
                    {
                        ID = 2,
                        Address = "Plot 235 Logan Avenue",
                        Age = 45,
                        CountryOfOrigin = "Aruba",
                        EMailAddress = "travis@yahoo.com",
                        FamilyName = "Travis",
                        Name = "Johnson",
                        Hired = false
                    },
                    new Applicant
                    {
                        ID = 3,
                        Address = "White House DC, Coker Parkview",
                        Age = 29,
                        CountryOfOrigin = "Finland",
                        EMailAddress = "janet@yahoo.com",
                        FamilyName = "Flower",
                        Name = "Janet",
                        Hired = true
                    });

                context.SaveChanges();
            }
        }
    }
}
