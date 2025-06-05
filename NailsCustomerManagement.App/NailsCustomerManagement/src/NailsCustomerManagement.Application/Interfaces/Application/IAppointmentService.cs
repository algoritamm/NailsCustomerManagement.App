using NailsCustomerManagement.Application.ViewModels.AppointmentVM;
using NailsCustomerManagement.Core.DTOs;
using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.Interfaces.Application
{
    public interface IAppointmentService
    {
        IEnumerable<IGrouping<DateTime, AppointmentSchedulerDto>> GetAppointmentForSchedulerForDataTable(byte statusId, int accountId);
        IEnumerable<AppointmentSchedulerDto> GetAppointmentForSchedulerForDataTableBydate(byte statusId, int accountId, DateTime selectedDate);
        List<AppointmentItemDataTableDto> GetAppointmentItemsForDataTable(int appointmentId, int? accountId);
        IEnumerable<AlgAppointmentItemStatus> GetAppointmentItemStatuses();
        CustomerAppointmentDto? GetCustomersAppointmentsForDataTable();
        InsertAppointmentVM GetInsertAppointmentViewModel(int appointmentId);
     }
}
