using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.Interfaces.Application
{
    public interface IAccountService
    {
        bool CheckAccountPassword(AdmAccount account, string password, out string message);
        int CreateSessionId(int length);
        ClaimsIdentity GenerateClaimsIdentityForUser(AdmAccount userwithRoles);
        AdmAccount? GetAccountByUsernameWithRoles(string username);
    }
}
