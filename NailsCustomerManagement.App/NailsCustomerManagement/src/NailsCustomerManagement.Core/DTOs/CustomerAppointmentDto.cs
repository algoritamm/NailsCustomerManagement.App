using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.DTOs
{
    public class CustomerAppointmentDto
    {
        public List<AppointmentDetailsDto> AppointmentDetailsDtos { get; set; } = new ();
        public List<AppointmentStatusDto> FilterStatuses { get; set; } = new ();

    }

    public class AppointmentDetailsDto
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public string AppointmentName { get; set; } = null!;
        public string CustomerFullName { get; set; } = null!;
        public AppointmentStatusDto Status { get; set; } = null!;
        public int AccountId { get; set; }
    }

    public class AppointmentStatusDto
    {
        public int StatusId { get; set; }
        public string StatusClass { get; set; } = null!;
        public string StatusName { get; set; } = null!;
    }
}
