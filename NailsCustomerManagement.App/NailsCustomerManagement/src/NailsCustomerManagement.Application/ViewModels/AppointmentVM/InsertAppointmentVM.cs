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
        public IEnumerable<SelectListItem> Accounts { get; set; }
        public IEnumerable<SelectListItem> Payments { get; set; }
        public IEnumerable<SelectListItem> ServiceTypes { get; set; }
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
