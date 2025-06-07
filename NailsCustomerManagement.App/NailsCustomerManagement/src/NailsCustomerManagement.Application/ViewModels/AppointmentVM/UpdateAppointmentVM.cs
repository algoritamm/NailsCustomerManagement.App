using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.ViewModels.AppointmentVM
{
    public class UpdateAppointmentVM
    {
        [Required(ErrorMessage = "This field is required.")]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int SelectedStatusId { get; set; }
        public IEnumerable<AlgAppointmentStatus> Statuses { get; set; } = new List<AlgAppointmentStatus>();
        public int AppointmentStatusId { get; set; }

        public static UpdateAppointmentVM ToViewModel(IEnumerable<AlgAppointmentStatus> statuses, AlgAppointment appointment)
        {
            return new UpdateAppointmentVM ()
            {
                Statuses = statuses,
                AppointmentId = appointment.AppointmentId,
                AppointmentStatusId = appointment.AppointmentStatusId,
            };
        }
    }
}
