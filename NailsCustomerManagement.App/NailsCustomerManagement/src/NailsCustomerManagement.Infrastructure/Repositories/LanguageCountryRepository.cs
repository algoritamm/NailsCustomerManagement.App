using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Infrastructure.Repositories
{
    public class LanguageCountryRepository : ILanguageCountryRepository
    {
        private readonly NailsCustomerManagementDbContext _context;
        public LanguageCountryRepository(NailsCustomerManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AlgLanguageCountry> GetLanguageCountries()
        {
            return _context.AlgLanguageCountries;
        }

        public AlgLanguageCountry? GetLanguageCountryById(int languageId)
        {
            return _context.AlgLanguageCountries.Where(l => l.LanguageCountryId == languageId).FirstOrDefault();
        }
    }
}
