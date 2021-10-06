using AF.CustomerRegistration.DB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AF.CustomerRegistration.DB.Context
{
    public interface IRegisterCustomerContext
    {
        public DbSet<RegisterCustomerModel> RegisterCustomers { get; set; }
        public Task<int> SaveChangesAsync();
    }
}
