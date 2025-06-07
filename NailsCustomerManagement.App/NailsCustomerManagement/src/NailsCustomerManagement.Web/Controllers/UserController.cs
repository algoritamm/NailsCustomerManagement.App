using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NailsCustomerManagement.Application.ViewModels.UserVM;
using System.Security.Claims;
using NailsCustomerManagement.Application.Interfaces.Application;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NailsCustomerManagement.Application.Services;
using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Helpers;

namespace NailsCustomerManagement.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ISessionLogService _sessionService;
        private readonly ILanguageCountryService _languageCountryService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ILogger<UserController> _logger;
        public UserController(IAccountService accountService, ILanguageCountryService languageCountryService, IStringLocalizer<SharedResource> sharedLocalizer, ILogger<UserController> logger, ISessionLogService sessionService)
        {
            _accountService = accountService;
            _languageCountryService = languageCountryService;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
            _sessionService = sessionService;   
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            var languageCountries = _languageCountryService.GetLanguageCountries();
            var loginVM = LoginVM.ToViewModel(languageCountries);
            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            // End session
            var sessionId = HttpContext.Session.GetInt32("SessionID");
            if (sessionId != null)
            {
                _sessionService.UpdateSessionLogById(sessionId ?? 0);
                HttpContext.Session.Clear();
            }

            await HttpContext.SignOutAsync();
            _logger.LogInformation($"Session successfully ended. Session id {sessionId}.");

            return Redirect(nameof(Login));
        }

        /// <summary>
        /// Changes the current culture(language date format etc) from a predefined 
        /// collection of supported cultures
        /// </summary>
        /// <param name="cultureId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetLanguage(int cultureId)
        {
            var language = _languageCountryService.GetLanguageCountryById(cultureId);

            Response.Cookies.Append("CultureValues", language?.LanguageSuffix ?? "Eng");
            string culture = language?.CountryCode ?? "en-US";
            Response.Cookies.Append(
                 CookieRequestCultureProvider.DefaultCookieName,
                 CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                 new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
             );

            return Json(new { Culture = culture });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin([FromBody] LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }
            
            try
            {
                var account = _accountService.GetAccountByUsernameWithRoles(model.Username);
                if (account is null)
                {
                    return Ok(_sharedLocalizer["Login faild. Username isn't valid."].Value);
                }
                bool passwordCorrect = _accountService.CheckAccountPassword(account, model.Password, out string message);
                if (!passwordCorrect)
                {
                    return Ok(_sharedLocalizer[message].Value);
                }
                else if (account.AdmAccountRoles.Count() == 0)
                {
                    return Ok(_sharedLocalizer["User account doesn't have login role."].Value);
                }
                /*ADD CLAIMS*/
                var claimsIdentity = _accountService.GenerateClaimsIdentityForUser(account);
                /*IDENTITY LOGIN*/
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = true });

                // Set language info in cookie
                var language = _languageCountryService.GetLanguageCountryById(model.SelectedLanguageCountryId);
                Response.Cookies.Append("CultureValues", language?.LanguageSuffix ?? "Eng");
                Response.Cookies.Append(
                     CookieRequestCultureProvider.DefaultCookieName,
                     CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language?.CountryCode ?? "en-US")),
                     new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                 );
                // Create session
                int? existingSessionId = HttpContext.Session.GetInt32("SessionID");

                if (existingSessionId.HasValue)
                {
                    HttpContext.Session.Clear();
                }

                int sessionId = _sessionService.InsertSessionLog(new SysSessionLog()
                {
                    SessionUser = account.UserName,
                    SessionStartedDate = DateTime.UtcNow
                });

                HttpContext.Session.SetInt32("SessionID", sessionId);
                _logger.LogInformation($"Session successfully started. Session id {sessionId}.");
                return Ok();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogException(ex, _logger);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
