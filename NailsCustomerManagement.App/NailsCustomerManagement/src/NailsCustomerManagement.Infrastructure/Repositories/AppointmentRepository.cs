using Microsoft.EntityFrameworkCore;
using NailsCustomerManagement.Core.DTOs;
using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly NailsCustomerManagementDbContext _context;
        public AppointmentRepository(NailsCustomerManagementDbContext context)
        {
            _context = context;
        }

        public CustomerAppointmentDto GetCustomersAppointmentsForDataTable()
        {
            CustomerAppointmentDto dataTableDto = new CustomerAppointmentDto();

            dataTableDto.FilterStatuses = _context.AlgAppointmentStatuses
                .Where(s => s.AppointmentStatusParentId != null).Select(x => new AppointmentStatusDto()
            {
                StatusId = x.AppointmentStatusId,
                StatusName = x.AppointmentStatusNameEng,
                StatusClass = x.AppointmentStatusClass,
            }).ToList();

            dataTableDto.AppointmentDetailsDtos = _context.AlgAppointments.Include(x => x.Customer)
                                                                            .Include(y => y.AppointmentStatus)
                                                                            .Select(d => new AppointmentDetailsDto()
                                                                            {
                                                                                AppointmentId = d.AppointmentId,
                                                                                CustomerId = d.CustomerId,
                                                                                AppointmentName = d.AppointmentName,
                                                                                CustomerFullName = $"{d.Customer.CustomerFirstName} {d.Customer.CustomerLastName}",
                                                                                Status = new AppointmentStatusDto() {
                                                                                    StatusId = d.AppointmentStatus.AppointmentStatusId,
                                                                                    StatusName = d.AppointmentStatus.AppointmentStatusNameEng,
                                                                                    StatusClass = d.AppointmentStatus.AppointmentStatusClass
                                                                                },
                                                                            }).ToList();
            return dataTableDto;
        }

        public List<AppointmentItemDataTableDto> GetAppointmentItemsForDataTable(int appointmentId, int? accountId)
        {
            return _context.AlgAppointmentItems
                .Include(c => c.Customer)
                .Include(a => a.Account)
                .Include(s => s.ServiceType)
                .Include(status => status.AppointmentItemStatus)
                .Include(p => p.PaymentType)
                .Where(z => z.AppointmentId == appointmentId && (!accountId.HasValue || z.AccountId == accountId))
                .Select(x => new AppointmentItemDataTableDto()
                {
                    AppointmentId = x.AppointmentId,
                    AppointmentItemId = x.AppointmentItemId,
                    AppointmentItemDate = x.AppointmentItemDate,
                    AppointmentItemTime = x.AppointmentItemTime,
                    AppointmentStatus = x.AppointmentItemStatus.AppointmentItemStatusNameEng,
                    AppointmentStatusColor = x.AppointmentItemStatus.AppointmentItemStatusClass,
                    AppointmentTitle = x.AppointmentItemName,
                    PayementType = x.PaymentType.PaymnetTypeNameEng,
                    AmountPaid = x.AmountPaid,
                    ServiceName = x.ServiceType.ServiceTypeNameEng,
                    ServicePrice = x.ServiceType.ServicePrice,
                    CustomerFullName = $"{x.Customer.CustomerFirstName} {x.Customer.CustomerLastName}",
                    AccountFullName = $"{x.Account.FirstName} {x.Account.LastName}",
                    AppointmentStatusId = x.AppointmentItemStatusId,

                }).ToList();
        }

        public IEnumerable<AlgAppointmentItemStatus> GetAppointmentItemStatuses()
        {
            return _context.AlgAppointmentItemStatuses;
        }

        public IEnumerable<IGrouping<DateTime, AppointmentSchedulerDto>> GetAppointmentForScheduler(byte statusId, int accountId)
        {
            var appointments = _context.AlgAppointmentItems
                .Include(c => c.Customer)
                .Include(a => a.Account)
                .Include(s => s.ServiceType)
                .Include(status => status.AppointmentItemStatus)
                .Include(p => p.PaymentType)
                .Where(z => z.AppointmentItemStatus.AppointmentItemStatusParentId == statusId && z.AccountId == accountId)
                .Select(x => new AppointmentSchedulerDto()
                {
                    AppointmentItemId = x.AppointmentItemId,
                    AppointmentItemDate = x.AppointmentItemDate,
                    AppointmentItemTime = x.AppointmentItemTime,
                    AppointmentStatus = x.AppointmentItemStatus.AppointmentItemStatusNameEng,
                    AppointmentStatusColor = x.AppointmentItemStatus.AppointmentItemStatusClass,
                    AppointmentTitle = x.AppointmentItemName,
                    ServiceName = x.ServiceType.ServiceTypeNameEng,
                    CustomerFullName = $"{x.Customer.CustomerFirstName} {x.Customer.CustomerLastName}",
                })
                .OrderBy(order => order.AppointmentItemTime)
                .GroupBy(x => x.AppointmentItemDate) ?? Enumerable.Empty<IGrouping<DateTime, AppointmentSchedulerDto>>();
            return appointments;
        }

        public IEnumerable<AppointmentSchedulerDto> GetAppointmentForSchedulerByDate(byte statusId, int accountId, DateTime selectedDate)
        {
            var appointments = _context.AlgAppointmentItems
                .Include(c => c.Customer)
                .Include(a => a.Account)
                .Include(s => s.ServiceType)
                .Include(status => status.AppointmentItemStatus)
                .Include(p => p.PaymentType)
                .Where(z => z.AppointmentItemStatus.AppointmentItemStatusParentId == statusId && z.AccountId == accountId && z.AppointmentItemDate.Date == selectedDate.Date)
                .Select(x => new AppointmentSchedulerDto()
                {
                    AppointmentItemId = x.AppointmentItemId,
                    AppointmentItemDate = x.AppointmentItemDate,
                    AppointmentItemTime = x.AppointmentItemTime,
                    AppointmentStatus = x.AppointmentItemStatus.AppointmentItemStatusNameEng,
                    AppointmentStatusColor = x.AppointmentItemStatus.AppointmentItemStatusClass,
                    AppointmentTitle = x.AppointmentItemName,
                    ServiceName = x.ServiceType.ServiceTypeNameEng,
                    CustomerFullName = $"{x.Customer.CustomerFirstName} {x.Customer.CustomerLastName}",
                })
                .OrderBy(order => order.AppointmentItemTime.ToString())
                .AsEnumerable() ?? Enumerable.Empty<AppointmentSchedulerDto> ();

            return appointments;
        }

        public AlgAppointment GetAppointment(int appointmentId)
        {
            return _context.AlgAppointments.Find(appointmentId) ?? new ();
        }

        public IEnumerable<AlgAppointmentStatus> GetAppointmentStatuses()
        {
            return _context.AlgAppointmentStatuses;
        }
    }
}
