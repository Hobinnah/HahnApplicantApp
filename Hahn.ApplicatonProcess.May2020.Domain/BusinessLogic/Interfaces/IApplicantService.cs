using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain.BusinessLogic.Interfaces
{
    public interface IApplicantService
    {
        Task<Response<Applicant>> GetApplicant(int ID);

        Task<Response<IEnumerable<Applicant>>> GetAllApplicants();

        Task<Response<Applicant>> CreateApplicant(Applicant applicant);

        Task<Response<Applicant>> UpdateApplicant(Applicant applicant);

        Task<Response<Applicant>> RemoveApplicant(int ID);

        Task<CountryResponse> ValidateCountry(string countryName);
    }
}
