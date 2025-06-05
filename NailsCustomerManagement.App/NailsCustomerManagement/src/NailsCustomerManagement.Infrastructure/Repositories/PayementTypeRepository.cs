using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Infrastructure.Repositories
{
    public class PayementTypeRepository : IPayementTypeRepository
    {
        private readonly NailsCustomerManagementDbContext _context;
        public PayementTypeRepository(NailsCustomerManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AlgPaymentType> GetPayementTypes()
        {
            return _context.AlgPaymentTypes;
        }
    }
}
