using NailsCustomerManagement.Core.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace NailsCustomerManagement.Web.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            Claim userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return Int32.Parse(userId?.Value);
        }
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            Claim userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            return userId?.Value;
        }
        public static string GetFirstName(this ClaimsPrincipal principal)
        {
            var firstName = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            return firstName?.Value;
        }
        public static string GetLastName(this ClaimsPrincipal principal)
        {
            var lastName = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
            return lastName?.Value;
        }
        public static string GetEmailAddress(this ClaimsPrincipal principal)
        {
            var emailAddress = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            return emailAddress?.Value;
        }
        public static string GetFirstNameLastName(this ClaimsPrincipal principal)
        {
            var firstName = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtended.FirstName);
            var lastName = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtended.LastName);
            return firstName?.Value + " " + lastName?.Value;
        }
        public static IEnumerable<string> GetPermissions(this ClaimsPrincipal principal)
        {
            var permissions = principal.Claims.Where(c => c.Type == ClaimTypesExtended.Permission).Select(c => c.Value);
            return permissions;
        }
        public static bool HasPermission(this ClaimsPrincipal principal, string claimValue)
        {
            var permissions = principal.Claims.Where(c => c.Type == ClaimTypesExtended.Permission & c.Value == claimValue).FirstOrDefault();
            return permissions != null ? true : false;
        }
    }
}
