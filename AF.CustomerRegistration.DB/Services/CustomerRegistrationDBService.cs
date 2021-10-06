using AF.CustomerRegistration.DB.Context;
using AF.CustomerRegistration.DB.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AF.CustomerRegistration.DB.Services
{
    public class CustomerRegistrationDBService: ICustomerRegistrationDBService
    {
        private IRegisterCustomerContext _registerCustomerContext;
        private readonly ILogger<CustomerRegistrationDBService> _logger;
        public CustomerRegistrationDBService(
            IRegisterCustomerContext registerCustomerContext,
            ILogger<CustomerRegistrationDBService> logger)
        {
            _registerCustomerContext = registerCustomerContext;
            _logger = logger;
        }
        
        public async Task<int> AddRegistration(RegisterCustomerModel registerCustomer)
        {
            //Note would not normally use a localised error handler but would set up some centralised error handling.
            try
            {
                await _registerCustomerContext.RegisterCustomers.AddAsync(registerCustomer);
                var result = await _registerCustomerContext.SaveChangesAsync();
                return registerCustomer.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return 0;
        }
    }
}
