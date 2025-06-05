using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Infrastructure.Repositories
{
    public class SessionLogRepository : ISessionLogRepository
    {
        private readonly NailsCustomerManagementDbContext _context; 
        
        public SessionLogRepository(NailsCustomerManagementDbContext context)
        {
            _context = context;
        }

        public void InsertSessionLog(SysSessionLog sessionlog)
        {
            _context.SysSessionLogs.Add(sessionlog);
        }

        public void UpdateSessionLog(SysSessionLog sessionLog)
        {
            _context.SysSessionLogs.Update(sessionLog);
        }
    }
}
