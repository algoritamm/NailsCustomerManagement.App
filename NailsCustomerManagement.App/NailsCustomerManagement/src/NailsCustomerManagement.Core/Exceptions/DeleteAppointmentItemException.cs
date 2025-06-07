using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.Exceptions
{
    public class DeleteAppointmentItemException : Exception
    {
        public DeleteAppointmentItemException() { }

        public DeleteAppointmentItemException(string message) : base(message) { }

        public DeleteAppointmentItemException(string message, Exception inner) : base(message, inner) { }
    }
}
