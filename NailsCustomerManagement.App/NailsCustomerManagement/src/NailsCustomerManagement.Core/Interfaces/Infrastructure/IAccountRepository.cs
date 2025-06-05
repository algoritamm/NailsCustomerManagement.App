using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.Interfaces.Infrastructure
{
    public interface IAccountRepository
    {
        AdmAccount GetAccount(int accountId);
        AdmAccount? GetAccountByUsernameWithRoles(string username);
        IEnumerable<AdmAccount> GetAccounts();
    }
}
