using FluentValidation;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.ModelValidators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
	{
		public ApplicantValidator()
		{
			RuleFor(x => x.ID).NotNull();
			RuleFor(x => x.Name).Length(5, 50);
            RuleFor(x => x.FamilyName).Length(5, 50);
            RuleFor(x => x.Address).Length(10, 100);
            RuleFor(x => x.CountryOfOrigin).NotNull().NotEmpty();
            RuleFor(x => x.EMailAddress).EmailAddress();
			RuleFor(x => x.Age).InclusiveBetween(20, 60);
            RuleFor(x => x.Hired).NotNull();

        }
    }
}
