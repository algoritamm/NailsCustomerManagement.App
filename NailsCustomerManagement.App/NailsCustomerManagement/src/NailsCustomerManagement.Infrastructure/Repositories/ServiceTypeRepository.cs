using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Infrastructure.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly NailsCustomerManagementDbContext _context;
        public ServiceTypeRepository(NailsCustomerManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AlgServiceType> GetServiceTypes()
        {
            return _context.AlgServiceTypes;
        }
    }
}
