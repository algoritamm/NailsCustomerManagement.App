using Microsoft.EntityFrameworkCore;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly NailsCustomerManagementDbContext _context;

        public AccountRepository(NailsCustomerManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AdmAccount> GetAccounts()
        {
            return _context.AdmAccounts;
        }

        public AdmAccount? GetAccountByUsernameWithRoles(string username)
        {
            return _context.AdmAccounts.Include(ac => ac.AdmAccountRoles)
                                        .ThenInclude(r => r.Role)
                                        .Where(a => a.UserName.Equals(username))
                                        .FirstOrDefault();
        }

        public AdmAccount GetAccount(int accountId)
        {
            return _context.AdmAccounts.Find(accountId) ?? new (); 
        }

    }
}
