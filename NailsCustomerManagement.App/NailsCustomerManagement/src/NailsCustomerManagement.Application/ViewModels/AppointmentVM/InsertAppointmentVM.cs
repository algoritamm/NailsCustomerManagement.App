using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NailsCustomerManagement.Application.ViewModels.AppointmentItemVM;


namespace NailsCustomerManagement.Application.ViewModels.AppointmentVM
{
    public class InsertAppointmentVM
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public string DateToday { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string TimeNow { get; set; } = DateTime.Now.ToString(@"HH\:mm");
        public IEnumerable<SelectListItem>? Accounts { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? Payments { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? ServiceTypes { get; set; } = new List<SelectListItem>();
        public AppointmentItemInsertVM InsertAppointment { get; set; } = new();

        public static InsertAppointmentVM ToViewModel(AlgAppointment appountment, IEnumerable<AdmAccount> accounts, IEnumerable<AlgPaymentType> payements, IEnumerable<AlgServiceType> services)
        {
            return new InsertAppointmentVM()
            {
                AppointmentId = appountment.AppointmentId,
                CustomerId = appountment.CustomerId,
                Accounts = accounts.Select(x => new SelectListItem()
                {
                    Value = x.AccountId.ToString(),
                    Text = $"{x.FirstName} {x.LastName}"
                }),
                Payments = payements.Select(x => new SelectListItem()
                {
                    Value = x.PaymnetTypeId.ToString(),
                    Text = x.PaymnetTypeNameEng,
                }),
                ServiceTypes = services.Select(x => new SelectListItem()
                {
                    Value = x.ServiceTypeId.ToString(),
                    Text = x.ServiceTypeNameEng,
                })
            }; 
        }
    }
}
