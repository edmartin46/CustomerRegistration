using DB = AF.CustomerRegistration.DB.Model;
using AF.CustomerRegistration.DB.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
/* Normally I would use say NNIT and moq injected objects to test business logic,
* but in this case have use the microsoft test classes*/
using RegisterCustomer.Model;
using System;
using System.Threading.Tasks;
using CustomerRegistration.Controllers;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace CustomerRegistration.Test
{
    [TestClass]
    public class TestValidationRules
    {
        Mock<ICustomerRegistrationDBService> _customerRegistrationDBService;
        Mock<ILogger<CustomerController>> _logger;
        CustomerController _customerController;

        public TestValidationRules()
        {
            var dbService = new Mock<ICustomerRegistrationDBService>();
            dbService.Setup(x => x.AddRegistration(It.IsAny<DB.RegisterCustomerModel>())).Returns(Task.FromResult(1));
            _customerRegistrationDBService = dbService;
            _logger = new Mock<ILogger<CustomerController>>();
            _customerController = new CustomerController(_logger.Object, _customerRegistrationDBService.Object);
        }
        [TestMethod]
        public void Check_A_Good_Request_Passes()
        {
            var request = new RegisterCustomerModel()
            {
                FirstName = "Edw",
                LastName = "Martin",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Email = "abc@def.com",
                ReferenceNumber = "AA-123456"
            };
            SimulateValidation(request);
            Assert.AreEqual(_customerController.ModelState.IsValid, true);
        }
        [TestMethod]
        public void Check_A_Short_First_Name_Fails()
        {        
            var request = new RegisterCustomerModel()
            {
                FirstName = "Ed",
                LastName = "Martin",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Email = "abc@def.com",
                ReferenceNumber = "AA-123456"
            };
            SimulateValidation(request);
            Assert.AreEqual(_customerController.ModelState.IsValid, false);
        }


        private void SimulateValidation(object model)
        {
            // mimic the behaviour of the model binder which is responsible for Validating the Model
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _customerController.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }
    }
}
