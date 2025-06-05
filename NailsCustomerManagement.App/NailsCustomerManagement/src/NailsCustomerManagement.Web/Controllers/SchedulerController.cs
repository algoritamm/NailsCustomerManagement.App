using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NailsCustomerManagement.Application.ViewModels.AppointmentVM;
using NailsCustomerManagement.Core.Enums;
using NailsCustomerManagement.Application.Interfaces.Application;
using NailsCustomerManagement.Helpers;
using NailsCustomerManagement.Web.Extensions;

namespace NailsCustomerManagement.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "SchedulerView")]
    public class SchedulerController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ILogger<AppointmentController> _logger;
        public SchedulerController(IAppointmentService appointmentService, ILogger<AppointmentController> logger, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _appointmentService = appointmentService;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
        }
        public IActionResult Index(DateTime? date = null)
        {
            try
            {
                DateTime selectedDate = date ?? DateTime.Today;
                var appointmentsForDataTable = _appointmentService.GetAppointmentForSchedulerForDataTableBydate((byte)AppointmentItemStatusEnum.Active, User.GetUserId(), selectedDate);
                var model = AppointmentSchedulerVM.ToViewModel(selectedDate, appointmentsForDataTable);
                
                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
