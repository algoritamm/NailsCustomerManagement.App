using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Helpers
{
    public class ExceptionHelper
    {
        public static void LogException<T>(Exception ex, ILogger<T> _logger)
        {
            _logger.LogError("Exception message: " + ex.Message);
            _logger.LogError("Exception stack trace: " + ex.StackTrace);
            if (ex.InnerException != null)
            {
                _logger.LogError("Inner exception message: " + ex.InnerException.Message);
                _logger.LogError("Inner exception stack trace: " + ex.InnerException.StackTrace);
            }
        }
    }
}
