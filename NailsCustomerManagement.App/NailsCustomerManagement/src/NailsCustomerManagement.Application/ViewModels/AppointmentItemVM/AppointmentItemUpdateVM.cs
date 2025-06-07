using Microsoft.AspNetCore.Mvc.Rendering;
using NailsCustomerManagement.Application.ViewModels.AppointmentVM;
using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.ViewModels.AppointmentItemVM
{
    public class AppointmentItemUpdateVM
    {
        [Required(ErrorMessage = "This field is required.")]
        public int SelectedPaymentTypeId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public DateTime ItemDate { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public TimeSpan ItemTime { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string AppointmentName { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required.")]
        public int SelectedStatusId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int AppointmentItemId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string AmountPaid { get; set; }

        public string AppointmentItemDate { get; set; } = string.Empty;
        public string AppointmentItemTime { get; set; } = string.Empty;        
        public int AppointmentItemStatusId { get; set; }  
        public int PaymentTypeId { get; set; }
        public IEnumerable<SelectListItem>? Payments { get; set; } = new List<SelectListItem>();  
        public IEnumerable<SelectListItem>? Statuses { get; set; } = new List<SelectListItem>();

        public static AppointmentItemUpdateVM ToViewModel(AlgAppointmentItem appountment, IEnumerable<AlgAppointmentItemStatus> statuses, IEnumerable<AlgPaymentType> payements)
        {
            return new AppointmentItemUpdateVM()
            {
                PaymentTypeId = appountment.PaymentTypeId ?? 0,
                AppointmentItemStatusId = appountment.AppointmentItemStatusId,
                Statuses = statuses.Where(x => x.AppointmentItemStatusParentId != null).Select(x => new SelectListItem()
                {
                    Value = x.AppointmentItemStatusId.ToString(),
                    Text = x.AppointmentItemStatusNameEng,
                }),
                Payments = payements.Select(x => new SelectListItem()
                {
                    Value = x.PaymnetTypeId.ToString(),
                    Text = x.PaymnetTypeNameEng,
                }),
                AppointmentItemDate = appountment.AppointmentItemDate.ToString("yyyy-MM-dd"),
                AppointmentItemTime = appountment.AppointmentItemTime.ToString(),
                AppointmentName = appountment.AppointmentItemName,
                AppointmentItemId = appountment.AppointmentItemId,
                AmountPaid = appountment.AmountPaid ?? string.Empty,
            };
        }
    }
}
