using Microsoft.EntityFrameworkCore;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly NailsCustomerManagementDbContext _context;
        public IAccountRepository AccountRepo { get; private set; }
        public ILanguageCountryRepository LanguageCountryRepo { get; private set; } 
        public ISessionLogRepository SessionLogRepo { get; private set; }
        public IPermissionRepository PermissionRepo { get; private set; }
        public IAppointmentRepository AppointmentRepo { get; private set; } 
        public IPayementTypeRepository PayementTypeRepo { get; private set; }   
        public IServiceTypeRepository ServiceTypeRepo { get; private set; }

        public ICustomerRepository CustomerRepo { get; private set; }

        public UnitOfWork(NailsCustomerManagementDbContext context,
            IAccountRepository accountRepo,
            ILanguageCountryRepository languageCountryRepo,
            ISessionLogRepository sessionLogRepo,
            IPermissionRepository permissionRepo,
            IAppointmentRepository appointmentRepo,
            IPayementTypeRepository payementTypeRepo,
            IServiceTypeRepository serviceTypeRepo, 
            ICustomerRepository customerRepository)
        {
            _context = context;
            AccountRepo = accountRepo;
            LanguageCountryRepo = languageCountryRepo;
            SessionLogRepo = sessionLogRepo;
            PermissionRepo = permissionRepo;
            AppointmentRepo = appointmentRepo;
            PayementTypeRepo = payementTypeRepo;
            ServiceTypeRepo = serviceTypeRepo;   
            CustomerRepo = customerRepository;
        }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public DbContext Context => _context;
    }
}
