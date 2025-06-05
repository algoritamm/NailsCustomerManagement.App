using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NToastNotify;

namespace NailsCustomerManagement.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IToastNotification _toastNotification;

        public ErrorController(ILogger<ErrorController> logger, IStringLocalizer<SharedResource> localizer, IToastNotification toastNotification)
        {
            _logger = logger;
            _localizer = localizer;
            _toastNotification = toastNotification;
        }

        [Route("Error/{statusCode}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult HttpStatusCodeHandler(int statusCode, string? originalUrl = null)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 400:
                    ViewBag.ErrorCode = "400";
                    ViewBag.ErrorTitle = _localizer["Bad request"].Value;
                    ViewBag.ErrorDetails = _localizer["You made a request that server could not understand."].Value;

                    _logger.LogWarning($"400 Error Occured. Path = {statusCodeResult?.OriginalPath ?? originalUrl}" +
                        $" and QueryString = {statusCodeResult?.OriginalQueryString ?? originalUrl}");
                    break;
                case 401:
                    ViewBag.ErrorCode = "401";
                    ViewBag.ErrorTitle = _localizer["Authentication error"].Value;
                    ViewBag.ErrorDetails = _localizer["You are not authenticated. To view this page, you must be authenticated. To access home page click <a href='/'>here</a>"].Value;

                    _logger.LogWarning($"401 Error Occured. Path = {statusCodeResult?.OriginalPath ?? originalUrl}" +
                        $" and QueryString = {statusCodeResult?.OriginalQueryString ?? originalUrl}");
                    break;
                case 403:
                    ViewBag.ErrorCode = "403";
                    ViewBag.ErrorTitle = _localizer["Access denied"].Value;
                    ViewBag.ErrorDetails = _localizer["You are not allowed to access this page."].Value;

                    _logger.LogWarning($"403 Error Occured. Path = {statusCodeResult?.OriginalPath ?? originalUrl}" +
                        $" and QueryString = {statusCodeResult?.OriginalQueryString ?? originalUrl}");
                    break;
                case 404:
                    ViewBag.ErrorCode = "404";
                    ViewBag.ErrorTitle = _localizer["Error while trying to access the requested page"].Value;
                    ViewBag.ErrorDetails = _localizer["The page you have requested can not be found."].Value;

                    _logger.LogWarning($"404 Error Occured. Path = {statusCodeResult?.OriginalPath ?? originalUrl}" +
                        $" and QueryString = {statusCodeResult?.OriginalQueryString ?? originalUrl}");
                    break;
                case 410:
                    ViewBag.ErrorCode = "410";
                    ViewBag.ErrorTitle = _localizer["The link you accessed is no longer available."].Value;
                    ViewBag.ErrorDetails = _localizer["The link you accessed is no longer available because its validity period has expired."].Value;

                    _logger.LogWarning($"410 Error Occured. Path = {statusCodeResult?.OriginalPath ?? originalUrl}" +
                        $" and QueryString = {statusCodeResult?.OriginalQueryString ?? originalUrl}");
                    break;
                case 500:
                    ViewBag.ErrorCode = "500";
                    ViewBag.ErrorCodeColor = "text-danger";
                    ViewBag.ErrorTitle = _localizer["Internal server error"].Value;
                    ViewBag.ErrorDetails = _localizer["Please contact tech support"].Value;

                    _logger.LogWarning($"500 Error Occured. Path = {statusCodeResult?.OriginalPath ?? originalUrl}" +
                        $" and QueryString = {statusCodeResult?.OriginalQueryString ?? originalUrl}");
                    break;
            }

            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            _logger.LogError($"The path {exceptionDetails?.Path} threw an exception {exceptionDetails?.Error}");
            _logger.LogError("Exception message: " + exceptionDetails?.Error?.Message);
            _logger.LogError("Exception stack trace: " + exceptionDetails?.Error?.StackTrace);
            if (exceptionDetails?.Error?.InnerException != null)
            {
                _logger.LogError("Inner exception message: " + exceptionDetails?.Error?.InnerException?.Message);
                _logger.LogError("Inner exception stack trace: " + exceptionDetails?.Error?.InnerException?.StackTrace);
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                _toastNotification.AddErrorToastMessage(_localizer["Error while executing the action."].Value);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            ViewBag.ErrorCode = "404";
            ViewBag.ErrorTitle = _localizer["Not Found"].Value;
            ViewBag.ErrorDetails += _localizer["The resource requested could not be found on this server."].Value;
            return View("Error");
        }
    }
}
