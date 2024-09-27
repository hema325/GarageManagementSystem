using System.Security.Cryptography;

namespace GMS.API.Helpers
{
    public static class KeyHelpers
    {
        public static string GetRandomKey(int length = 40)
        {
            var bytes = RandomNumberGenerator.GetBytes(length);
            return Convert.ToBase64String(bytes);
        }
    }
}
