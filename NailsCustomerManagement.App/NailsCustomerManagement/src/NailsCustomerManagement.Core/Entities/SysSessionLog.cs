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
        public DateTime SessionStartedDate { get; set; }
        public DateTime? SessionEndedDate { get; set; }    
    }
}
