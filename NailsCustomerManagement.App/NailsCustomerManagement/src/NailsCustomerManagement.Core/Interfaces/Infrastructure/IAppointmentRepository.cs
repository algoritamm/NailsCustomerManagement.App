using NailsCustomerManagement.Core.DTOs;
using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.Interfaces.Infrastructure
{
    public interface IAppointmentRepository
    {
        void DeleteAppointmentItem(int appointmentItemId);
        AlgAppointment GetAppointment(int appointmentId);
        IEnumerable<IGrouping<DateTime, AppointmentSchedulerDto>> GetAppointmentForScheduler(byte statusId, int accountId);
        IEnumerable<AppointmentSchedulerDto> GetAppointmentForSchedulerByDate(byte statusId, int accountId, DateTime selectedDate);
        AlgAppointmentItem GetAppointmentItem(int appointmentItemId);
        List<AppointmentItemDataTableDto> GetAppointmentItemsForDataTable(int appointmentId, int? accountId);
        IEnumerable<AlgAppointmentItemStatus> GetAppointmentItemStatuses();
        IEnumerable<AlgAppointmentStatus> GetAppointmentStatuses();
        CustomerAppointmentDto GetCustomersAppointmentsForDataTable(int? userId);
        void InsertAppointment(AlgAppointment appointment);
        void InsertAppointmentItem(AlgAppointmentItem appointmentItem);
        void UpdateAppointment(AlgAppointment appointment);
        void UpdateAppointmentItem(AlgAppointmentItem appointmentItem);
    }
}
