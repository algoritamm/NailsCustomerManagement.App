using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.ViewModels.AppointmentVM
{
    public class AppointmentDetailsVM
    {
        public int AppointmentItemId { get; set; }
        public string AppointmentTitle { get; set; } = null!;
        public string AppointmentStatus { get; set; } = null!;
        public string AppointmentStatusColor { get; set; } = null!;
        public DateTime AppointmentItemDate { get; set; }
        public TimeSpan AppointmentItemTime { get; set; }
        public string ServiceName { get; set; } = null!;
        public string CustomerFullName { get; set; } = null!;
    }
}
