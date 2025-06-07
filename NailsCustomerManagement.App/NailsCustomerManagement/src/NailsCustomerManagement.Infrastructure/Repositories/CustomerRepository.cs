using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NailsCustomerManagementDbContext _context;

        public CustomerRepository(NailsCustomerManagementDbContext context)
        {
            _context = context;
        }

        public void InsertCustomer(AlgCustomer customer)
        {
            _context.AlgCustomers.Add(customer);
        }
    }
}
