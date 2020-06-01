using Hahn.ApplicatonProcess.May2020.Domain.BusinessLogic.Interfaces;
using Hahn.ApplicatonProcess.May2020.Domain.Helpers;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Repositories.Interfaces;
using Hahn.ApplicatonProcess.May2020.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain.BusinessLogic.Implementation
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository applicantRepository;
        private readonly IConfigurationFile configurationFile;
        
        public ApplicantService(IApplicantRepository applicantRepository, IConfigurationFile configurationFile)
        {
            this.applicantRepository = applicantRepository ?? throw new ArgumentNullException(nameof(applicantRepository));
            this.configurationFile = configurationFile ?? throw new ArgumentNullException(nameof(configurationFile));

        }

        public async Task<Response<Applicant>> GetApplicant(int ID)
        {
            Applicant newApplicant = new Applicant();
            Response<Applicant> response = new Response<Applicant>();

            newApplicant = await this.applicantRepository.GetByID(ID);
            if (newApplicant != null && newApplicant.ID > 0)
            {
                response.ResponseCode = "00";
                response.Description = "Successful";
                response.Result = newApplicant;
                return response;
            }

            response.ResponseCode = "100";
            response.Description = "Could not retrieve applicant.";
            return response;
        }

        public async Task<Response<IEnumerable<Applicant>>> GetAllApplicants()
        {
            IEnumerable<Applicant> applicants = new List<Applicant>();
            Response<IEnumerable<Applicant>> response = new Response<IEnumerable<Applicant>>();

            applicants = await this.applicantRepository.GetAll();
            if (applicants != null && applicants.Any())
            {
                response.ResponseCode = "00";
                response.Description = "Successful";
                response.Result = applicants;
                return response;
            }

            response.ResponseCode = "100";
            response.Description = "Could not retrieve applicants.";
            return response;
        }

        public async Task<Response<Applicant>> CreateApplicant(Applicant applicant)
        {
            Applicant newApplicant = new Applicant();
            Response<Applicant> response = new Response<Applicant>();

            CountryResponse countryResponse = await ValidateCountry(applicant.CountryOfOrigin);

            if (countryResponse != null && countryResponse.Name != null && !string.IsNullOrEmpty(countryResponse.Name))
            {
                newApplicant = await this.applicantRepository.Create(applicant);
                await this.applicantRepository.Save();

                if (newApplicant != null && newApplicant.ID > 0)
                {
                    response.ResponseCode = "00";
                    response.Description = "Successful";
                    response.Result = newApplicant;
                    return response;
                }
            }

            response.ResponseCode = "100";
            response.Description = "Could not create applicant.";
            return response;
        }

        public async Task<Response<Applicant>> UpdateApplicant(Applicant applicant)
        {
            Applicant newApplicant = new Applicant();
            Response<Applicant> response = new Response<Applicant>();

            CountryResponse countryResponse = await ValidateCountry(applicant.CountryOfOrigin);

            if (countryResponse != null && countryResponse.Name != null && !string.IsNullOrEmpty(countryResponse.Name))
            {
                newApplicant = await this.applicantRepository.Update(applicant);
                await this.applicantRepository.Save();

                if (newApplicant != null && newApplicant.ID > 0)
                {
                    response.ResponseCode = "00";
                    response.Description = "Successful";
                    response.Result = newApplicant;
                    return response;
                }
            }
            else
            {
                response.ResponseCode = "100";
                response.Description = "The supplied country could not be validated.";
                return response;
            }

            response.ResponseCode = "100";
            response.Description = "Could not update applicant.";
            return response;
        }

        public async Task<Response<Applicant>> RemoveApplicant(int ID)
        {
            Applicant applicant = new Applicant();
            Response<Applicant> response = new Response<Applicant>();

            applicant = await this.applicantRepository.GetByID(ID);
            await this.applicantRepository.Delete(applicant);
            await this.applicantRepository.Save();

            response.ResponseCode = "00";
            response.Description = "Successful";
            return response;
        }

        public async Task<CountryResponse> ValidateCountry(string countryName)
        {
            CountryResponse response = new CountryResponse();
            string url = this.configurationFile.EuRestCountriesUrl.Replace("{{name}}", countryName);
            RestClientCall<CountryResponse> call = new RestClientCall<CountryResponse>();

            try
            {
                response = await call.Get(url);

                return response;
            }
            catch(Exception er) { }

            response.Name = string.Empty;
            return response;
        }
    }
}
