using NailsCustomerManagement.Application.ViewModels.AppointmentItemVM;
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
        void DeleteAppointmentItem(int appointmentItemId);
        IEnumerable<IGrouping<DateTime, AppointmentSchedulerDto>> GetAppointmentForSchedulerForDataTable(byte statusId, int accountId);
        IEnumerable<AppointmentSchedulerDto> GetAppointmentForSchedulerForDataTableBydate(byte statusId, int accountId, DateTime selectedDate);
        AppointmentItemUpdateVM GetAppointmentItemForUpdateVM(int appointmentItemId);
        List<AppointmentItemDataTableDto> GetAppointmentItemsForDataTable(int appointmentId, int? accountId);
        IEnumerable<AlgAppointmentItemStatus> GetAppointmentItemStatuses();
        CustomerAppointmentDto? GetCustomersAppointmentsForDataTable(int? userId);
        InsertAppointmentVM GetInsertAppointmentViewModel(int appointmentId);
        InsertNewCustomerVM GetInsertNewCustomerForViewModel();
        UpdateAppointmentVM GetUpdateAppointmentStatusModel(int appointmentId);
        int InsertAppointmentItem(AppointmentItemInsertVM insertModel, int loggedUserId, string loggedUsername);
        void InsertNewCustomerAppointment(InsertNewCustomerVM model, string loggedUsername, int loggedUserId, string loggedFullName);
        void UpdateAppointmentItem(AppointmentItemUpdateVM updateModel, string loggedUsername);
        void UpdateAppointmentStatus(int appointmentId, int statusId, string loggedUser);
    }
}
