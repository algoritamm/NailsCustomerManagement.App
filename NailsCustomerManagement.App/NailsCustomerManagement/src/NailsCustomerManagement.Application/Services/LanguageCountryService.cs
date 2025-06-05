using NailsCustomerManagement.Application.Interfaces.Application;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.Services
{
    public class LanguageCountryService : ILanguageCountryService
    {
        private IUnitOfWork _unitOfWork;
        public LanguageCountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AlgLanguageCountry> GetLanguageCountries()
        {
            return _unitOfWork.LanguageCountryRepo.GetLanguageCountries();
        }

        public AlgLanguageCountry? GetLanguageCountryById(int languageId)
        {
            return _unitOfWork.LanguageCountryRepo.GetLanguageCountryById(languageId);
        }
    }
}
