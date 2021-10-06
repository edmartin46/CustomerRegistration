using AF.CustomerRegistration.DB.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AF.CustomerRegistration.DB.Services
{
    public interface ICustomerRegistrationDBService
    {
        public Task<int> AddRegistration(RegisterCustomerModel registerCustomer);
    }
}
