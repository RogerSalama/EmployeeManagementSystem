using System.Collections.Concurrent;

namespace EmployeeManagementSystem.API.Services
{
    public static class SessionService
    {
        private static readonly ConcurrentDictionary<string, string> _userTokens = new ConcurrentDictionary<string, string>();

        public static void StoreToken(string userId, string token)
        {
            _userTokens[userId] = token;
        }

        public static string GetToken(string userId)
        {
            _userTokens.TryGetValue(userId, out var token);
            return token;
        }

        public static void RemoveToken(string userId)
        {
            _userTokens.TryRemove(userId, out _);
        }
    }
}