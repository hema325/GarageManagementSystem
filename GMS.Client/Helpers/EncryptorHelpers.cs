using System.Text;

namespace GMS.Client.Helpers
{
    public static class EncryptorHelpers
    {
        public static string Encrypt(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static string Decrypt(string ecryptedText)
        {
            var bytes = Convert.FromBase64String(ecryptedText);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
