using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Domain;
using Hahn.ApplicatonProcess.May2020.Domain.BusinessLogic.Interfaces;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicatonProcess.May2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {

        private readonly ILogger<ApplicantController> logger;
        private readonly IApplicantService applicantService;
        private readonly IConfigurationFile configurationFile;

        public ApplicantController(ILogger<ApplicantController> logger, IApplicantService applicantService, IConfigurationFile configurationFile)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.applicantService = applicantService ?? throw new ArgumentNullException(nameof(applicantService));
            this.configurationFile = configurationFile ?? throw new ArgumentNullException(nameof(configurationFile));
        }

        // GET: api/Applicant
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Response<IEnumerable<Applicant>> response = new Response<IEnumerable<Applicant>>();
            this.logger.LogInformation("Executing a get Applicants", null);

            try
            {
                response = await this.applicantService.GetAllApplicants();
                if (response.ResponseCode.Equals("00"))
                {
                    return Ok(response);
                }
            } 
            catch (Exception exception) { this.logger.LogError("An error occurred.", exception); }

            return BadRequest(response);
        }

        // GET: api/Applicant/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            Response<Applicant> response = new Response<Applicant>();
            this.logger.LogInformation("Executing an Applicant get", null);

            try
            {
                if (id > 0)
                {
                    response = await this.applicantService.GetApplicant(id);
                    if (response.ResponseCode.Equals("00"))
                    {
                        return Ok(response);
                    }
                }
                else
                {
                    response.ResponseCode = "100";
                    response.Description = "The id must be greater than zero(0).";
                }
            }
            catch (Exception exception) { this.logger.LogError("An error occurred.", exception); }

            return BadRequest(response);
        }

        // POST: api/Applicant
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Applicant applicant)
        {
            Response<Applicant> response = new Response<Applicant>();
            this.logger.LogInformation("Executing a create Applicant", applicant);

            try
            {
                response = await this.applicantService.CreateApplicant(applicant);
                if (response.ResponseCode.Equals("00"))
                {
                    return Created($"{ this.configurationFile.HostUrl }api/Applicant/{ response.Result.ID.ToString() }", response);
                }
               
            }
            catch (Exception exception) { this.logger.LogError("An error occurred.", exception); }

            return BadRequest(response);
        }

        // PUT: api/Applicant/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Applicant applicant)
        {
            Response<Applicant> response = new Response<Applicant>();
            this.logger.LogInformation($"Executing an Applicant update with ID : {id}", applicant);

            try
            {
                response = await this.applicantService.UpdateApplicant(applicant);
                if (response.ResponseCode.Equals("00"))
                {
                    return Ok(response);
                }
            }
            catch (Exception exception) { this.logger.LogError("An error occurred.", exception); }

            return BadRequest(response);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Response<Applicant> response = new Response<Applicant>();
            this.logger.LogInformation($"Executing a delete applicant with ID : {id}", null);

            try
            {
                response = await this.applicantService.RemoveApplicant(id);
                if (response.ResponseCode.Equals("00"))
                {
                    return Ok(response);
                }
            }
            catch (Exception exception) { this.logger.LogError("An error occurred.", exception); }

            return BadRequest(response);
        }

        // GET: api/Applicant/{countryName}
        [HttpGet]
        [Route("ValidateCountry/{countryName}")]
        public async Task<IActionResult> ValidateCountry(string countryName)
        {
            CountryResponse countryResponse = new CountryResponse();
            Response<string> response = new Response<string>();
            this.logger.LogInformation($"Executing a validate country. Country name is : {countryName}", null);

            try
            {
                if (!string.IsNullOrEmpty(countryName))
                {
                    countryResponse = await this.applicantService.ValidateCountry(countryName);
                    if (countryResponse != null && !string.IsNullOrEmpty(countryResponse.Name))
                    {
                        response.ResponseCode = "00";
                        response.Description = "Successful";
                        return Ok(response);
                    }
                    else
                    {
                        response.ResponseCode = "100";
                        response.Description = "The id must be greater than zero(0).";
                        return Ok(response);
                    }
                }
                else
                {
                    response.ResponseCode = "100";
                    response.Description = "The id must be greater than zero(0).";
                }
            }
            catch (Exception exception) { this.logger.LogError("An error occurred.", exception); }

            return BadRequest(response);
        }
    }
}
