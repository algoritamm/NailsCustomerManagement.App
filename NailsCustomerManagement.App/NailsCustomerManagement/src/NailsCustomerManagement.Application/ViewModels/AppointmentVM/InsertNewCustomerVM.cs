using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.ViewModels.AppointmentVM
{
    public class InsertNewCustomerVM
    {
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [RegularExpression("^\\+38\\d+$", ErrorMessage = "This format is not valid.")]
        public string TelephoneNo { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [RegularExpression("^[^\\s]+@[^\\s]+\\.[^\\s]+$", ErrorMessage = "This format is not valid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string AppointmentItemName { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required.")]
        public DateTime ItemDate { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public TimeSpan ItemTime { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int SelectedServiceTypeId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int SelectedPaymentTypeId { get; set; }
        public IEnumerable<AlgPaymentType>? Payments { get; set; } = new List<AlgPaymentType>();
        public IEnumerable<AlgServiceType>? ServiceTypes { get; set; } = new List<AlgServiceType>();

        public static InsertNewCustomerVM ToViewModel(IEnumerable<AlgPaymentType>? payments, IEnumerable<AlgServiceType>? serviceTypes)
        {
            return new InsertNewCustomerVM()
            {
                Payments = payments,
                ServiceTypes = serviceTypes
            };
        }
    }
}
