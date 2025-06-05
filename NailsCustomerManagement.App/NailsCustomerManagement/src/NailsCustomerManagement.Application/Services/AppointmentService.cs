using NailsCustomerManagement.Application.ViewModels.AppointmentVM;
using NailsCustomerManagement.Core.DTOs;
using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Application.Interfaces.Application;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using NailsCustomerManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NailsCustomerManagement.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CustomerAppointmentDto? GetCustomersAppointmentsForDataTable()
        {
            try
            {
                var appointmentDto = _unitOfWork.AppointmentRepo.GetCustomersAppointmentsForDataTable();
                return appointmentDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AppointmentItemDataTableDto> GetAppointmentItemsForDataTable(int appointmentId, int? accountId)
        {
            try
            {
                var itemDtos = _unitOfWork.AppointmentRepo.GetAppointmentItemsForDataTable(appointmentId, accountId);
                return itemDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AlgAppointmentItemStatus> GetAppointmentItemStatuses()
        {
            try
            {
                var statuses = _unitOfWork.AppointmentRepo.GetAppointmentItemStatuses();
                return statuses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<IGrouping<DateTime, AppointmentSchedulerDto>> GetAppointmentForSchedulerForDataTable(byte statusId, int accountId)
        {
            try
            {
                var appointments = _unitOfWork.AppointmentRepo.GetAppointmentForScheduler(statusId, accountId);
                return appointments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AppointmentSchedulerDto> GetAppointmentForSchedulerForDataTableBydate(byte statusId, int accountId, DateTime selectedDate)
        {
            try
            {
                var appointments = _unitOfWork.AppointmentRepo.GetAppointmentForSchedulerByDate(statusId, accountId, selectedDate);
                return appointments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public InsertAppointmentVM GetInsertAppointmentViewModel(int appointmentId)
        {
            try
            {
                var _appointment = _unitOfWork.AppointmentRepo.GetAppointment(appointmentId);

                IEnumerable<AdmAccount> accounts = _unitOfWork.AccountRepo.GetAccounts();
                IEnumerable<AlgPaymentType> payements = _unitOfWork.PayementTypeRepo.GetPayementTypes();
                IEnumerable<AlgServiceType> services = _unitOfWork.ServiceTypeRepo.GetServiceTypes();

                InsertAppointmentVM insertModel = InsertAppointmentVM.ToViewModel(_appointment, accounts, payements, services);

                return insertModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
