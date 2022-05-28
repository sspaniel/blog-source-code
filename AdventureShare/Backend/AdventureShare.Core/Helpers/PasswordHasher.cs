using System;
using System.Security.Cryptography;
using System.Text;

namespace AdventureShare.Core.Helpers
{
    public static class PasswordHasher
    {
        public static string Hash(string password, string salt)
        {
            var bytesToHash = Encoding.UTF8.GetBytes($"{password}{salt}");
            var hashBytes = new SHA256Managed().ComputeHash(bytesToHash);
            var hashString = Convert.ToBase64String(hashBytes);
            return hashString;
        }
    }
}
