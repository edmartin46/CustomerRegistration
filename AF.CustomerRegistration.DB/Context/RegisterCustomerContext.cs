using AF.CustomerRegistration.DB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AF.CustomerRegistration.DB.Context
{
    public class RegisterCustomerContext : DbContext, IRegisterCustomerContext
    {
        public DbSet<RegisterCustomerModel> RegisterCustomers { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            var result = await base.SaveChangesAsync();
            return result;
        }
        public string DbPath { get; private set; }
        public RegisterCustomerContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}CustomerRegistration.db";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
        
    }
}
