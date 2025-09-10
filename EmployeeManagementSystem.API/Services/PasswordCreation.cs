using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagementSystem.API.Services
{
    public class PasswordService
    {
        // Password requirements (match those in Identity options)
        private const int RequiredLength = 8;
        private const int RequiredUniqueChars = 3;
        private const bool RequireDigit = true;
        private const bool RequireNonAlphanumeric = true;
        private const bool RequireUppercase = true;
        private const bool RequireLowercase = true;

        private static readonly char[] SpecialChars = "!@#$%^&*()-_=+[{]}\\|;:'\",<.>/?`~".ToCharArray();

        // Checks if a password meets the policy requirements
        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < RequiredLength)
                return false;

            if (RequireDigit && !password.Any(char.IsDigit))
                return false;

            if (RequireNonAlphanumeric && !password.Any(ch => SpecialChars.Contains(ch)))
                return false;

            if (RequireUppercase && !password.Any(char.IsUpper))
                return false;

            if (RequireLowercase && !password.Any(char.IsLower))
                return false;

            if (password.Distinct().Count() < RequiredUniqueChars)
                return false;

            return true;
        }


        // Optional, can generate password for npc employees or tests:
        // Generates a random password that satisfies all requirements
        public string GeneratePassword()
        {
            var random = new Random();
            string password;
            do
            {
                var sb = new StringBuilder();

                // Ensure each requirement is met at least once
                sb.Append((char)random.Next('A', 'Z' + 1)); // Uppercase
                sb.Append((char)random.Next('a', 'z' + 1)); // Lowercase
                sb.Append((char)random.Next('0', '9' + 1)); // Digit
                sb.Append(SpecialChars[random.Next(SpecialChars.Length)]); // Special char

                // Fill remaining length
                while (sb.Length < RequiredLength)
                {
                    int pick = random.Next(4);
                    switch (pick)
                    {
                        case 0: sb.Append((char)random.Next('A', 'Z' + 1)); break;
                        case 1: sb.Append((char)random.Next('a', 'z' + 1)); break;
                        case 2: sb.Append((char)random.Next('0', '9' + 1)); break;
                        case 3: sb.Append(SpecialChars[random.Next(SpecialChars.Length)]); break;
                    }
                }

                password = new string(sb.ToString().OrderBy(_ => random.Next()).ToArray()); // Shuffle
            }
            while (!ValidatePassword(password));

            return password;
        }

        // Hashes a password with SHA256 (for demonstration: use Identity's hasher in production)
        public string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}