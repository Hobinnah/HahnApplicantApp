using Hahn.ApplicatonProcess.May2020.Domain;
using Hahn.ApplicatonProcess.May2020.Domain.BusinessLogic.Implementation;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Repositories.Interfaces;
using Hahn.ApplicatonProcess.May2020.Domain.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.UintTest.BusinessLogic.Implementations
{
    [TestClass]
    public class ApplicantServiceTest
    {
        private ApplicantService applicantService;
        private Applicant applicant = null;
        private string Url;
        private int _ID;

        [TestInitialize]
        public void TestInitializer()
        {
            Url = "https://restcountries.eu/rest/v2/name/{{name}}?fullText=true";

            var applicantRepository = new Mock<IApplicantRepository>();
            _ID = 1;
            applicant = new Applicant
            {
                Address = "Address",
                Age = 20,
                CountryOfOrigin = "Germany",
                EMailAddress = "oeze@nse.com.ng",
                FamilyName = "James",
                Hired = true,
                ID = _ID,
                Name = "Peter"
            };

            applicantRepository.Setup(x => x.GetByID(1)).ReturnsAsync(applicant);

            applicantRepository.Setup(x => x.Create(It.IsAny<Applicant>())).ReturnsAsync((Applicant a) => a);

            applicantRepository.Setup(x => x.Update(It.IsAny<Applicant>())).ReturnsAsync((Applicant a) => a);

            applicantRepository.Setup(x => x.Delete(It.IsAny<Applicant>()));

            var configurationFile = new Mock<IConfigurationFile>();
            configurationFile.Setup(x => x.EuRestCountriesUrl).Returns(Url);

            applicantService = new ApplicantService(applicantRepository.Object, configurationFile.Object);

            
        }

        [DataTestMethod]
        [DataRow("Nigeria")]
        [DataRow("Germany")]
        public async Task ShouldReturnTheFullCountryDetails(string countryName)
        {
            var result = await applicantService.ValidateCountry(countryName);

            Assert.IsNotNull(result);
            Assert.AreEqual(countryName, result.Name);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionIfCustomerIsNull()
        {
            Applicant applicant = null;

            var exception = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => applicantService.CreateApplicant(applicant));

            Assert.IsNotNull(exception);
            Assert.AreEqual("applicant", exception.ParamName);
        }
        
        [TestMethod]
        public async Task ShouldCreateApplicantInformation()
        {
            var applicant = new Applicant
            {
                Address = "Address",
                Age = 20,
                CountryOfOrigin = "Germany",
                EMailAddress = "oeze@nse.com.ng",
                FamilyName = "James",
                Hired = true,
                ID = _ID,
                Name = "Peter"
            };

            var applicantResult = await applicantService.CreateApplicant(applicant);

            Assert.IsNotNull(applicantResult);
            Assert.AreEqual(ResultCode.SUCCESS, applicantResult.ResponseCode);
            Assert.AreEqual(_ID, applicantResult.Result.ID);

        }
        
        [TestMethod]
        public async Task ShouldUpdateApplicantInformation()
        {
            var applicant = new Applicant
            {
                Address = "Address",
                Age = 20,
                CountryOfOrigin = "Germany",
                EMailAddress = "oeze@nse.com.ng",
                FamilyName = "James",
                Hired = true,
                ID = _ID,
                Name = "James"
            };

            var applicantResult = await applicantService.UpdateApplicant(applicant);

            Assert.IsNotNull(applicantResult);
            Assert.AreEqual(ResultCode.SUCCESS, applicantResult.ResponseCode);
            Assert.AreEqual("James", applicantResult.Result.Name);

        }
        
        [TestMethod]
        public async Task ShouldDeleteApplicantInformation()
        {
            
            var applicantResult = await applicantService.RemoveApplicant(_ID);

            Assert.IsNotNull(applicantResult);
            Assert.AreEqual(ResultCode.SUCCESS, applicantResult.ResponseCode);
        }
        
        [DataTestMethod]
        [DataRow(1)]
        public async Task ShouldProduceAnApplicantWIthData(int id)
        {
            var applicant = await applicantService.GetApplicant(id);

            Assert.IsNotNull(applicant);
            Assert.AreEqual(ResultCode.SUCCESS, applicant.ResponseCode);
            Assert.AreEqual(_ID, applicant.Result.ID);

        }

        [TestMethod]
        public async Task ShouldFetchAllApplicantInformation()
        {

            var result = await applicantService.GetAllApplicants();

            Assert.IsNotNull(result);
            Assert.AreEqual(ResultCode.SUCCESS, result.ResponseCode);
        }

    }
}
