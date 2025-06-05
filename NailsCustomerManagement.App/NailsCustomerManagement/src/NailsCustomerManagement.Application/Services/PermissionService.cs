using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Core.Enums;
using NailsCustomerManagement.Application.Interfaces.Application;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PermissionService(IUnitOfWork unitOfWork)
        {
             _unitOfWork = unitOfWork;
        }

        public IEnumerable<AdmPermissionRole> GetActivePermissionsForRoles(int[] roleIds)
        {
            return _unitOfWork.PermissionRepo.GetPermissionsByStatusAndRoleIds(roleIds, (byte)PermissionStatusEnum.Active) ?? Enumerable.Empty<AdmPermissionRole>();
        }
    }
}
