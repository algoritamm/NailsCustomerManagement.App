using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Application.Interfaces.Application;
using Microsoft.AspNetCore.Authentication.Cookies;
using NailsCustomerManagement.Helpers;
using NailsCustomerManagement.Core.Statics;
using NailsCustomerManagement.Core.Enums;

namespace NailsCustomerManagement.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AdmAccount? GetAccountByUsernameWithRoles(string username)
        {
            return _unitOfWork.AccountRepo.GetAccountByUsernameWithRoles(username);
        }

        public bool CheckAccountPassword(AdmAccount account, string password, out string message)
        {
            try
            {
                message = string.Empty;

                if (string.IsNullOrEmpty(account.Password))
                {
                    message = $"The user doesn't have the password.";
                    return false;
                }
                if (string.IsNullOrEmpty(password))
                {
                    message = $"Error with entered password.";
                    return false;
                }

                bool isPasswordCorrect = PasswordHasherHelper.VerifyPassword(password, account.Password);
                message = isPasswordCorrect ? message : $"Login failed. Wrong password.";

                return isPasswordCorrect;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CreateSessionId(int length)
        {
            Random random = new Random();
            var randomLength = random.Next(0, length+1);

            var code = GenerateRandomCode(randomLength, random);

            if(!int.TryParse(code, out int sessionId))
                return int.MinValue;

            return sessionId;
        }

        public ClaimsIdentity GenerateClaimsIdentityForUser(AdmAccount userwithRoles)
        {
            IEnumerable<AdmRole> userRoles = userwithRoles.AdmAccountRoles.Select(ar => ar.Role);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, userwithRoles.UserName.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userwithRoles.AccountId.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, userwithRoles.FirstName.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Surname, userwithRoles.LastName.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Email, userwithRoles.EmailAddress?.ToString() ?? string.Empty));
            identity.AddClaim(new Claim(ClaimTypesExtended.UserCode, userwithRoles.UserCode.ToString().TrimEnd()));

            string roleMerged = "";
            foreach (AdmRole role in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleNameEng));
                roleMerged += role.RoleNameEng + ", ";
            }

            identity.AddClaim(new Claim(ClaimTypes.GroupSid, roleMerged.Remove(roleMerged.Length - 2)));

            int[] userRolesIds = userwithRoles.AdmAccountRoles.Select(ar => ar.RoleId).ToArray();
            IEnumerable<AdmPermissionRole> userPermissions = _unitOfWork.PermissionRepo.GetPermissionsByStatusAndRoleIds(userRolesIds, (byte)PermissionStatusEnum.Active) ?? Enumerable.Empty<AdmPermissionRole>();
            foreach (AdmPermissionRole permission in userPermissions)
            {
                identity.AddClaim(new Claim(ClaimTypesExtended.Permission, permission.Permission.PermissionKey));
            }

            return identity;
        }

        #region Private methods

        private string GenerateRandomCode(int length, Random random)
        {
            var code = string.Empty;    
            while (length > 0)
            {
                code += random.Next(0,9);
                length--;
            }
            return code;
        }
        #endregion
    }
}
