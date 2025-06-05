using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.Interfaces.Application
{
    public interface ISessionLogService
    {
        int InsertSessionLog(SysSessionLog session);
        int UpdateSessionLog(SysSessionLog session);
    }
}
