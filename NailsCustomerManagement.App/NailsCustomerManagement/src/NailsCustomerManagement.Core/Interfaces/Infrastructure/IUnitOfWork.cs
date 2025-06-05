using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.Interfaces.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepo { get; }
        ILanguageCountryRepository LanguageCountryRepo { get; }
        ISessionLogRepository SessionLogRepo { get; }
        IPermissionRepository PermissionRepo { get; }
        IAppointmentRepository AppointmentRepo { get; }
        IPayementTypeRepository PayementTypeRepo { get; }
        IServiceTypeRepository ServiceTypeRepo { get; }

        int Complete();
        void Dispose();
    }
}
