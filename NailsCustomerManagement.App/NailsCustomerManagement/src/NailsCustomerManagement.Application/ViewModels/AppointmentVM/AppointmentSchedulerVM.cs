using NailsCustomerManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.ViewModels.AppointmentVM
{
    public class AppointmentSchedulerVM
    {
        public DateTime AppointmentDate { get; set; }
        public List<AppointmentDetailsVM> Appointments { get; set; }

        public static AppointmentSchedulerVM ToViewModel(DateTime selectedDate, IEnumerable<AppointmentSchedulerDto> appointments)
        {
            return new AppointmentSchedulerVM
            {
                AppointmentDate = selectedDate.Date,
                Appointments = appointments.Select(a => new AppointmentDetailsVM
                {
                    AppointmentItemId = a.AppointmentItemId,
                    AppointmentTitle = a.AppointmentTitle,
                    AppointmentStatus = a.AppointmentStatus,
                    AppointmentStatusColor = a.AppointmentStatusColor,
                    AppointmentItemDate = a.AppointmentItemDate,
                    AppointmentItemTime = a.AppointmentItemTime,
                    ServiceName = a.ServiceName,
                    CustomerFullName = a.CustomerFullName
                }).ToList()
            };
        }
    }
}
