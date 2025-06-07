using NailsCustomerManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.ViewModels.AppointmentVM
{
    public class AppointmentIndexVM
    {
        public List<AppointmentVM> Appointments { get; set; }
        public List<AppointmentStatusVM> FilterStatuses { get; set; }

        public static AppointmentIndexVM ToViewModel(CustomerAppointmentDto appointmentDto, int loggedUserId)
        {
            return new AppointmentIndexVM()
            {
                Appointments = appointmentDto.AppointmentDetailsDtos.Select(x => new AppointmentVM()
                {
                    AppointmentId = x.AppointmentId,
                    CustomerId = x.CustomerId,
                    AppointmentName = x.AppointmentName,
                    CustomerFullName = x.CustomerFullName,
                    Status = new AppointmentStatusVM()
                    {
                        StatusId = x.Status.StatusId,
                        StatusName = x.Status.StatusName,
                        StatusClass = x.Status.StatusClass,
                    },
                    CanChangeAppointment = x.AccountId == loggedUserId,
                }).ToList(),
                FilterStatuses = appointmentDto.FilterStatuses.Select(s => new AppointmentStatusVM()
                {
                    StatusId = s.StatusId,
                    StatusName = s.StatusName,
                    StatusClass = s.StatusClass,
                }).ToList()
            }; 
        }
    }
    public class AppointmentVM
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public string AppointmentName { get; set; } = null!;
        public string CustomerFullName { get; set; } = null!;
        public AppointmentStatusVM Status { get; set; } = null!;
        public bool CanChangeAppointment {  get; set; }
    }

    public class AppointmentStatusVM
    {
        public int StatusId { get; set; }
        public string StatusClass { get; set; } = null!;
        public string StatusName { get; set; } = null!;
    }
}
