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

namespace Hahn.ApplicatonProcess.May2020.Test.BusinessLogic.Implementations
{
    [TestClass]
    public class ApplicantServiceTest
    {

        private ApplicantService applicantService;
        private int _ID;

        [TestInitialize]
        public void TestInitializer()
        {
            var applicantRepository = new Mock<IApplicantRepository>();
            applicantRepository.Setup(x => x.GetByID(It.IsAny<Int16>())).ReturnsAsync((Applicant a) => a);

            applicantRepository.Setup(x => x.GetAll()).ReturnsAsync((IEnumerable<Applicant> a) => a);

            applicantRepository.Setup(x => x.Create(It.IsAny<Applicant>())).ReturnsAsync((Applicant a) => a);

            applicantRepository.Setup(x => x.Update(It.IsAny<Applicant>())).ReturnsAsync((Applicant a) => a);

            applicantRepository.Setup(x => x.Delete(It.IsAny<Applicant>()));

            applicantRepository.Setup(x => x.Save());

            var configurationFile = new Mock<IConfigurationFile>();

            applicantService = new ApplicantService(applicantRepository.Object, configurationFile.Object);

            _ID = 1;
        }

        [TestMethod]
        public async Task ShouldSaveApplicantInformation()
        {
            var applicant = new Applicant 
            { 
                Address ="Address",
                Age = 20,
                CountryOfOrigin = "NGN",
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

        [DataTestMethod]
        [DataRow(1)]
        public async Task ShouldProduceAnApplicantOrderWIthData(int id)
        {
            var applicant = await applicantService.GetApplicant(id);

            Assert.IsNotNull(applicant);
            Assert.AreEqual(ResultCode.SUCCESS, applicant.ResponseCode);
            Assert.AreEqual(_ID, applicant.Result.ID);

        }
    }
}
