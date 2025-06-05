using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NailsCustomerManagement.Application.ViewModels.UserVM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "This field is required.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? Password { get; set; }
        public string? LoginResult { get; set; }
        public int SelectedLanguageCountryId { get; set; }
        public List<SelectListItem> LanguageCountries { get; set; } = new List<SelectListItem>();

        public static LoginVM ToViewModel(IEnumerable<AlgLanguageCountry> languageCountries)
        {
            return new LoginVM()
            {
                LanguageCountries = languageCountries.Select(language => new SelectListItem()
                {
                    Text = language.DisplayLanguage,
                    Value = language.LanguageCountryId.ToString(),
                }).ToList(),
            };
        }
    }
}
