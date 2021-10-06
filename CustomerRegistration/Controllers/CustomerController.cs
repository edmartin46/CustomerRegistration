using AF.CustomerRegistration.DB.Services;
using DB = AF.CustomerRegistration.DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegisterCustomer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerRegistration.Filters;

namespace CustomerRegistration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
       
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerRegistrationDBService _customerRegistrationDBService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerRegistrationDBService customerRegistrationDBService)
        {
            _logger = logger;
            _customerRegistrationDBService = customerRegistrationDBService;
        }
        [Route("Register")]
        [HttpPost]
        [ValidateModel]

        public async Task<IActionResult> Register([FromBody] RegisterCustomerModel registerCustomerModel)
        {
            var dbModel = new DB.RegisterCustomerModel
            {
                DateOfBirth = registerCustomerModel.DateOfBirth,
                Email = registerCustomerModel.Email,
                FirstName = registerCustomerModel.FirstName,
                LastName = registerCustomerModel.LastName,
                ReferenceNumber = registerCustomerModel.ReferenceNumber
            };
            var result = await _customerRegistrationDBService.AddRegistration(dbModel);
            return Ok(result);
           
        }
    }
}
