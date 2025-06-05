using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NailsCustomerManagement.Application.ViewModels.AppointmentItemVM;
using NailsCustomerManagement.Application.ViewModels.AppointmentVM;
using NailsCustomerManagement.Application.Interfaces.Application;
using NailsCustomerManagement.Core.Statics;
using NailsCustomerManagement.Helpers;
using NailsCustomerManagement.Web.Extensions;
using NToastNotify;

namespace NailsCustomerManagement.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "AppointmentView")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ILogger<AppointmentController> _logger;
        private readonly IToastNotification _toastNotification;
        public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger, IStringLocalizer<SharedResource> sharedLocalizer, IToastNotification toastNotification)
        {
            _appointmentService = appointmentService;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
            _toastNotification = toastNotification;
        }


        public IActionResult Index()
        {
            try
            {
                var appointmentDto = _appointmentService.GetCustomersAppointmentsForDataTable();
                var model = AppointmentIndexVM.ToViewModel(appointmentDto);

                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult ViewAppointment(int appointmentId)
        {
            try
            {
                int? loggedUserId = User.HasPermission(Permissions.Home.AdministratorRolePermission) ? null : User.GetUserId();
                var appointmentItemsDetailsDto = _appointmentService.GetAppointmentItemsForDataTable(appointmentId, loggedUserId);
                var itemStatuses = _appointmentService.GetAppointmentItemStatuses();
                var model = AppointmentItemIndexVM.ToViewModel(appointmentItemsDetailsDto, itemStatuses);

                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        public ActionResult _InsertAppointment(InsertAppointmentVM model)
        {
            try
            {
                var insertModel = _appointmentService.GetInsertAppointmentViewModel(model.AppointmentId);
                return PartialView(insertModel);

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertAppointmentItem(AppointmentItemInsertVM model)
        {
            if (!ModelState.IsValid)
                return NotFound();

            try
            {
                //Insert
                _toastNotification.AddSuccessToastMessage(_sharedLocalizer["Successfully saved data."]);
                return Ok();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                _toastNotification.AddErrorToastMessage(_sharedLocalizer["An unexpected error occurred. Please try again later."]);
                return BadRequest();
            }
        }

    }
}
