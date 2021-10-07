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
using Microsoft.AspNetCore.Http;

namespace CustomerRegistration.Controllers
{
    [Produces("application/json")]
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
        /// <summary>
        /// Creates a Customer request.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Register
        ///     {
        ///         "FirstName": "Edm",
        ///         "LastName": "Martin",
        ///         "ReferenceNumber": "AA-123456",
        ///         "DateOfBirth": "1961-03-01",
        ///         "Email":"abc@def.com"
        ///     }
        ///
        /// </remarks>
        /// <param name="registerCustomerModel"></param>
        /// <returns>The newly created RegistrationId</returns>
        /// <response code="200">Returns the newly created RegistrationId</response>
        /// <response code="400">If the item fails validation. NOTE: You must provide either an Email or a Date of Birth</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Register")]
        
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
            //The error handling and logging takes place in the AddRegistration method
            if (result == 0)
            {
                return StatusCode(500);
            }
            return Ok(result);
           
        }
    }
}
