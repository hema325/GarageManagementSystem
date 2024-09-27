using Blazored.LocalStorage;

namespace GMS.Client.Extensions
{
    public static class LocalStorageServiceExtensions
    {
        public static async Task SetItemEncryptedAsync<TItem>(this ILocalStorageService localStorage, string key, TItem item)
        {
            var itemStr = JsonHelpers.Serialize(item);
            var encryptedItem = EncryptorHelpers.Encrypt(itemStr);
            await localStorage.SetItemAsStringAsync(key, encryptedItem);
        }
        
        public static async Task<TItem?> GetItemDecryptedAsync<TItem>(this ILocalStorageService localStorage, string key)
        { 
            var encryptedItem = await localStorage.GetItemAsStringAsync(key);
            
            if (encryptedItem != null)
            {
                var decryptedItem = EncryptorHelpers.Decrypt(encryptedItem);
                return JsonHelpers.Deserialize<TItem>(decryptedItem);
            }

            return default;
        }
    }
}
