using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.Entities
{
    public class SysSessionLog
    {
        public int SessionId { get; set; }
        public string SessionUser { get; set; } = null!;
        public string SessionStartedDate { get; set; } = null!;
        public string? SessionEndedDate { get; set; }    
    }
}
