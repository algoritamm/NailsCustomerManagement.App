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
using NailsCustomerManagement.Application.ViewModels.AppointmentItemVM;
using NailsCustomerManagement.Core.Enums;
using NailsCustomerManagement.Core.Exceptions;
using System.Transactions;
using NailsCustomerManagement.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NailsCustomerManagement.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CustomerAppointmentDto? GetCustomersAppointmentsForDataTable(int? userId)
        {
            try
            {
                var appointmentDto = _unitOfWork.AppointmentRepo.GetCustomersAppointmentsForDataTable(userId);
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

        public int InsertAppointmentItem(AppointmentItemInsertVM insertModel, int loggedUserId, string loggedUsername)
        {
            try
            {
                var insertModelToDatabase = new AlgAppointmentItem()
                {
                    AppointmentItemName = insertModel.AppointmentName,
                    AppointmentItemStatusId = (byte)AppointmentItemStatusEnum.Active,
                    CustomerId = insertModel.CustomerId,
                    AppointmentItemCretedDate = DateTime.Now,
                    ServiceTypeId = insertModel.SelectedServiceTypeId,
                    AccountId = loggedUserId,
                    AppointmentItemDate = insertModel.ItemDate.Date,
                    AppointmentItemTime = insertModel.ItemTime,
                    AppointmentId = insertModel.AppointmentId,
                    PaymentTypeId = insertModel.SelectedPaymentTypeId,
                    InsertUser = loggedUsername,
                    InsertDate = DateTime.Now,
                };

                _unitOfWork.AppointmentRepo.InsertAppointmentItem(insertModelToDatabase);
                _unitOfWork.Complete();

                return insertModelToDatabase.AppointmentItemId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AppointmentItemUpdateVM GetAppointmentItemForUpdateVM(int appointmentItemId)
        {
            try
            {
                var appointmentItem = _unitOfWork.AppointmentRepo.GetAppointmentItem(appointmentItemId);
                var statuses = _unitOfWork.AppointmentRepo.GetAppointmentItemStatuses();
                var payments = _unitOfWork.PayementTypeRepo.GetPayementTypes();

                var updateModel = AppointmentItemUpdateVM.ToViewModel(appointmentItem, statuses, payments);
                return updateModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public InsertNewCustomerVM GetInsertNewCustomerForViewModel()
        {
            try
            {
                IEnumerable<AlgPaymentType> payements = _unitOfWork.PayementTypeRepo.GetPayementTypes();
                IEnumerable<AlgServiceType> services = _unitOfWork.ServiceTypeRepo.GetServiceTypes();

                var model = InsertNewCustomerVM.ToViewModel(payements, services);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertNewCustomerAppointment(InsertNewCustomerVM model, string loggedUsername, int loggedUserId, string loggedFullName)
        {
            
            using (var transaction = _unitOfWork.Context.Database.BeginTransaction())
            {
                AlgAppointment appointment = new AlgAppointment();
                AlgCustomer customer = new AlgCustomer();

                try
                {
                    // INSERT CUSTOMER
                    customer = new AlgCustomer()
                    {
                        CustomerFirstName = model.FirstName,
                        CustomerLastName = model.LastName,
                        CustomerEmail = model.Email,
                        CustomerTelephoneNo = model.TelephoneNo,
                        CustomerStatusId = (byte)CustomerStatusEnum.Active,
                        InsertDate = DateTime.Now,
                        InsertUser = loggedUsername,
                    };

                    _unitOfWork.CustomerRepo.InsertCustomer(customer);
                    _unitOfWork.Complete();

                    // INSERT APPOINTMENT
                    appointment = new AlgAppointment()
                    {
                        AppointmentName = loggedFullName,
                        CustomerId = customer.CustomerId,
                        AppointmentCreatedDate = DateTime.Now,
                        AppointmentStatusId = (byte)AppointmentStatusEnum.Active,
                        InsertDate = DateTime.Now,
                        InsertUser = loggedUsername,
                    };

                    _unitOfWork.AppointmentRepo.InsertAppointment(appointment);
                    _unitOfWork.Complete();

                    // INSERT APPOINTMENT ITEM
                    var appointmentItem = new AlgAppointmentItem()
                    {
                        AppointmentItemName = model.AppointmentItemName,
                        AppointmentItemStatusId = (byte)AppointmentItemStatusEnum.Active,
                        CustomerId = customer.CustomerId,
                        AppointmentItemCretedDate = DateTime.Now,
                        ServiceTypeId = model.SelectedServiceTypeId,
                        AccountId = loggedUserId,
                        AppointmentItemDate = model.ItemDate,
                        AppointmentItemTime = model.ItemTime,
                        AppointmentId = appointment.AppointmentId,
                        PaymentTypeId = model.SelectedPaymentTypeId,
                        InsertDate = DateTime.Now,
                        InsertUser = loggedUsername,
                    };

                    _unitOfWork.AppointmentRepo.InsertAppointmentItem(appointmentItem);
                    _unitOfWork.Complete();

                    transaction.Commit();

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public UpdateAppointmentVM GetUpdateAppointmentStatusModel(int appointmentId)
        {
            try
            {
                var appointment = _unitOfWork.AppointmentRepo.GetAppointment(appointmentId);
                var statuses = _unitOfWork.AppointmentRepo.GetAppointmentStatuses()?.Where(x => x.AppointmentStatusParentId != null) ?? Enumerable.Empty<AlgAppointmentStatus>();

                var updateModel = UpdateAppointmentVM.ToViewModel(statuses, appointment);
                return updateModel; 

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateAppointmentItem(AppointmentItemUpdateVM updateModel, string loggedUsername)
        {
            try
            {
                var modelForUpdate = _unitOfWork.AppointmentRepo.GetAppointmentItem(updateModel.AppointmentItemId);

                if (modelForUpdate == null)
                    throw new UpdateAppointmentItemException($"Appointment item with id {updateModel.AppointmentItemId} doesn't exists.");

                modelForUpdate.AppointmentItemName = updateModel.AppointmentName;
                modelForUpdate.AppointmentItemDate = updateModel.ItemDate;
                modelForUpdate.AppointmentItemTime = updateModel.ItemTime;
                modelForUpdate.AmountPaid = updateModel.AmountPaid;
                modelForUpdate.PaymentTypeId = updateModel.SelectedPaymentTypeId;
                modelForUpdate.AppointmentItemStatusId = updateModel.SelectedStatusId;
                modelForUpdate.UpdateDate = DateTime.Now;
                modelForUpdate.UpdateUser = loggedUsername;

                _unitOfWork.AppointmentRepo.UpdateAppointmentItem(modelForUpdate);
                _unitOfWork.Complete();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteAppointmentItem(int appointmentItemId)
        {
            try
            {
                _unitOfWork.AppointmentRepo.DeleteAppointmentItem(appointmentItemId);
                _unitOfWork.Complete();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateAppointmentStatus(int appointmentId, int statusId, string loggedUser)
        {
            try
            {
                var appointment = _unitOfWork.AppointmentRepo.GetAppointment(appointmentId);
                if (appointment == null)
                {
                    throw new UpdateAppointmentException($"Appointment with id {appointmentId} doesn't exists.");
                }
                appointment.AppointmentStatusId = statusId;
                appointment.UpdateDate = DateTime.Now;
                appointment.UpdateUser = loggedUser;

                _unitOfWork.AppointmentRepo.UpdateAppointment(appointment);
                _unitOfWork.Complete();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }

}
