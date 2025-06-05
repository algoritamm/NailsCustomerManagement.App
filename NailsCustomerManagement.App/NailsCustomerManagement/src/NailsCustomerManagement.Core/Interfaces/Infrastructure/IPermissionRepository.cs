using NailsCustomerManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Core.Interfaces.Infrastructure
{
    public interface IPermissionRepository
    {
        IEnumerable<AdmPermissionRole>? GetPermissionsByStatusAndRoleIds(int[] roleIds, byte statusId);
    }
}
