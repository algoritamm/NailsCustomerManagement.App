using Microsoft.EntityFrameworkCore;
using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly NailsCustomerManagementDbContext _context;
        public PermissionRepository(NailsCustomerManagementDbContext contect)
        {
            _context = contect;
        }

        public IEnumerable<AdmPermissionRole>? GetPermissionsByStatusAndRoleIds(int[] roleIds, byte statusId)
        {
            var result = _context.AdmPermissionRoles.Include(p => p.Permission)
                                        .Include(p => p.Role)
                                        .Where(r => roleIds.Contains(r.RoleId) && r.PermissionRoleStatusId == statusId);
            return result;
        }
    }
}
