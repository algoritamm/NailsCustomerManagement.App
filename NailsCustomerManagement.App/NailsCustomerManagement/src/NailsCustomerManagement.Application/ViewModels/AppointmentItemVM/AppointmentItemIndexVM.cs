using NailsCustomerManagement.Core.DTOs;
using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.ViewModels.AppointmentItemVM
{
    public class AppointmentItemIndexVM
    {
        public int AppointmentId { get; set; }
        public string CustomerFullName { get; set; }
        public List<AppointmentItemDataTableVM> AppointmentItems { get; set; }
        public List<AppointmentItemStatusVM> FilterStatuses { get; set; }

        public static AppointmentItemIndexVM ToViewModel(List<AppointmentItemDataTableDto> items, IEnumerable<AlgAppointmentItemStatus> statuses, int? loggedUserId)
        {
            return new AppointmentItemIndexVM()
            {
                AppointmentId = (int)items.FirstOrDefault()?.AppointmentId,
                CustomerFullName = items.FirstOrDefault()?.CustomerFullName ?? string.Empty,
                AppointmentItems = items.Select(t => new AppointmentItemDataTableVM()
                {
                    AppointmentId = t.AppointmentId,
                    AppointmentItemId = t.AppointmentItemId,
                    AppointmentTitle = t.AppointmentTitle ?? string.Empty,
                    AppointmentItemDate = t.AppointmentItemDate,
                    AppointmentItemTime = t.AppointmentItemTime,
                    AmountPaid = t.AmountPaid ?? string.Empty,
                    AppointmentStatus = new AppointmentItemStatusVM()
                    {
                        StatusId = t.AppointmentStatusId,
                        StatusName = t.AppointmentStatus,
                        StatusColor = t.AppointmentStatusColor,
                    },
                    PayementType = t.PayementType ?? string.Empty,
                    ServiceName = t.ServiceName ?? string.Empty,
                    ServicePrice = t.ServicePrice,
                    AccountFullName = t.AccountFullName ?? string.Empty,
                    CanUpdateAppointmentItem = t.AccountId == loggedUserId,
                    CanDeleteAppointmentItem = t.AccountId == loggedUserId,
                }).ToList(),
                FilterStatuses = statuses.Where(x => x.AppointmentItemStatusParentId.HasValue).Select(s => new AppointmentItemStatusVM() { 
                    StatusId = s.AppointmentItemStatusId,
                    StatusName = s.AppointmentItemStatusNameEng,
                    StatusColor = s.AppointmentItemStatusClass,
                }).ToList()
            };
        }
    }
}
