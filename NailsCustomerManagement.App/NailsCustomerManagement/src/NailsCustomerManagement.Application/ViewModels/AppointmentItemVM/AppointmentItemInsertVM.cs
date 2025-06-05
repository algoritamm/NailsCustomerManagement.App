using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.ViewModels.AppointmentItemVM
{
    public class AppointmentItemInsertVM
    {
        public int SelectedServiceTypeId { get; set; }
        public int SelectedPaymentTypeId { get; set; }
        public DateTime ItemDate { get; set; }
        public TimeSpan ItemTime { get; set; }
        public string AppointmentName { get; set; }
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
    }
}
