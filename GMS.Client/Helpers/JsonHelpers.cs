using System.Text.Json;

namespace GMS.Client.Helpers
{
    public static class JsonHelpers
    {
        public static TData? Deserialize<TData>(string json)
        {
            if (json != null)
                return JsonSerializer.Deserialize<TData>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return default;
        }

        public static string Serialize<TData>(TData data)
            => JsonSerializer.Serialize(data);
    }
}
