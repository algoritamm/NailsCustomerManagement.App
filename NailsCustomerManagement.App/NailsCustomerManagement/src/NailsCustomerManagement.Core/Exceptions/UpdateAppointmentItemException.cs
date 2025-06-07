using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.Exceptions
{
    public class UpdateAppointmentItemException : Exception
    {
        public UpdateAppointmentItemException() { }

        public UpdateAppointmentItemException(string message) : base(message) { }

        public UpdateAppointmentItemException(string message, Exception inner) : base(message, inner) { }
    }
}
