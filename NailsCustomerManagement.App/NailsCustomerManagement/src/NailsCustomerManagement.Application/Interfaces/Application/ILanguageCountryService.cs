using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.Interfaces.Application
{
    public interface ILanguageCountryService
    {
        IEnumerable<AlgLanguageCountry> GetLanguageCountries();
        AlgLanguageCountry? GetLanguageCountryById(int languageId);
    }
}
