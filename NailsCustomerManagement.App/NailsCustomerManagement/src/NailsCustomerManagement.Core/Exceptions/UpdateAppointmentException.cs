using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.Exceptions
{
    public class UpdateAppointmentException : Exception
    {
        public UpdateAppointmentException() { }

        public UpdateAppointmentException(string message) : base(message) { }

        public UpdateAppointmentException(string message, Exception inner) : base(message, inner) { }
    }
}
