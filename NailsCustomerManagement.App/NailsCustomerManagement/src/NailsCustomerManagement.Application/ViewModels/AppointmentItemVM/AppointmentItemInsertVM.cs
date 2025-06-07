using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.ViewModels.AppointmentItemVM
{
    public class AppointmentItemInsertVM
    {
        [Required(ErrorMessage = "This field is required.")]
        public int SelectedServiceTypeId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int SelectedPaymentTypeId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public DateTime ItemDate { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public TimeSpan ItemTime { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string AppointmentName { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required.")]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int CustomerId { get; set; }
    }
}
