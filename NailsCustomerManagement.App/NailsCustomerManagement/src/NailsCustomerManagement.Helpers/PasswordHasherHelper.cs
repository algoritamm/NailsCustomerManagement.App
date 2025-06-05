using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Helpers
{
    public class PasswordHasherHelper
    {
        private static readonly PasswordHasher<object> _hasher = new();

        /// <summary>
        /// Hashing the text into a hashed value.
        /// </summary>
        public static string HashPassword(string plainPassword)
        {
            return _hasher.HashPassword(null, plainPassword);
        }

        /// <summary>
        /// Compares text and a hashed value.
        /// </summary>
        public static bool VerifyPassword(string plainPassword, string hashedPasswordFromDb)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPasswordFromDb, plainPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
