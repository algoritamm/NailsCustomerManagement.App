using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.Interfaces.Infrastructure
{
    public interface ILanguageCountryRepository
    {
        IEnumerable<AlgLanguageCountry> GetLanguageCountries();
        AlgLanguageCountry? GetLanguageCountryById(int languageId);
    }
}
