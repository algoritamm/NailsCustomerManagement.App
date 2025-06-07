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
                int? loggedUserId = User.HasPermission(Permissions.Home.AdministratorRolePermission) ? null : User.GetUserId();
                var appointmentDto = _appointmentService.GetCustomersAppointmentsForDataTable(loggedUserId);
                var model = AppointmentIndexVM.ToViewModel(appointmentDto, User.GetUserId());

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
                var model = AppointmentItemIndexVM.ToViewModel(appointmentItemsDetailsDto, itemStatuses, loggedUserId);

                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet]
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

        [HttpGet]
        public IActionResult _UpdateAppointment(AppointmentItemUpdateVM model)
        {
            var updateModel = _appointmentService.GetAppointmentItemForUpdateVM(model.AppointmentItemId);
            return PartialView(updateModel);
        }

        [HttpGet]
        public IActionResult _DeleteModal(int? id = null)
        {
            ViewData.Add("Id", id);
            return PartialView();
        }

        [HttpGet]
        public IActionResult _InsertNewCustomerAppointment()
        {
            try
            {
                var model = _appointmentService.GetInsertNewCustomerForViewModel();
                return PartialView(model);

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]   
        public IActionResult _UpdateAppointmentStatus(int appointmentId)
        {
            try
            {
                var updateModal = _appointmentService.GetUpdateAppointmentStatusModel(appointmentId);
                return PartialView(updateModal);

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAppointmentStatus(int appointmentId, int statusId)
        {

            try
            {
                _appointmentService.UpdateAppointmentStatus(appointmentId, statusId, User.GetUserName());
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertAppointmentItem(AppointmentItemInsertVM model)
        {
            if (!ModelState.IsValid)
                return NotFound();

            try
            {
                var appointmentItemId = _appointmentService.InsertAppointmentItem(model, User.GetUserId(), User.GetUserName());

                if (appointmentItemId == 0)
                {
                    _logger.LogError($"Insert appointment item was successfull but appointment item id is {appointmentItemId}.");
                    _toastNotification.AddAlertToastMessage(_sharedLocalizer["An error occurred while executing the action."]);
                    return BadRequest($"Insert appointment item was successfull but appointment item id is {appointmentItemId}.");
                }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAppointmentItem(AppointmentItemUpdateVM model)
        {
            try
            {
                _appointmentService.UpdateAppointmentItem(model, User.GetUserName());
                _toastNotification.AddSuccessToastMessage(_sharedLocalizer["Successfully update item."]);
                return Ok();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                _toastNotification.AddErrorToastMessage(_sharedLocalizer["An unexpected error occurred. Please try again later."]);
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAppointmantItem(int id)
        {
            try
            {
                _appointmentService.DeleteAppointmentItem(id);
                _toastNotification.AddSuccessToastMessage(_sharedLocalizer["Successfully deleted item."]);
                return Ok();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                _toastNotification.AddErrorToastMessage(_sharedLocalizer["An unexpected error occurred. Please try again later."]);
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertNewCustomerAppointment(InsertNewCustomerVM model)
        {
            try
            {
                _appointmentService.InsertNewCustomerAppointment(model, User.GetUserName(), User.GetUserId(), $"{User.GetFirstName()} {User.GetLastName()}");
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
