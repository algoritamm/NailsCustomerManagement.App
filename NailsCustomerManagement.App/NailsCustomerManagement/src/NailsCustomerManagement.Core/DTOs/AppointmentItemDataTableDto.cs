using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.DTOs
{
    public class AppointmentItemDataTableDto
    {
        public int  AppointmentId { get; set; }
        public int AppointmentItemId { get; set; }
        public string AppointmentTitle { get; set; } = null!;
        public int AppointmentStatusId { get; set; }    
        public string AppointmentStatus { get; set; } = null!;
        public string AppointmentStatusColor { get; set; } = null!;
        public DateTime AppointmentItemDate { get; set; }
        public TimeSpan AppointmentItemTime { get; set; }
        public string PayementType { get; set; } = null!;   
        public string AmountPaid { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public int ServicePrice { get; set; }
        public string CustomerFullName { get; set; } = null!;
        public string AccountFullName { get; set; } = null!;
    }
}
