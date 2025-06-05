using NailsCustomerManagement.Core.DTOs;
using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.ViewModels.AppointmentItemVM
{
    public class AppointmentItemDataTableVM
    {
        public int AppointmentId { get; set; }
        public int AppointmentItemId { get; set; }
        public string AppointmentTitle { get; set; }
        public DateTime AppointmentItemDate { get; set; }
        public TimeSpan AppointmentItemTime { get; set; }
        public string PayementType { get; set; }
        public string AmountPaid { get; set; }
        public string ServiceName { get; set; }
        public int ServicePrice { get; set; }
        public string AccountFullName { get; set; }
        public AppointmentItemStatusVM AppointmentStatus {  get; set; }

    }
}
