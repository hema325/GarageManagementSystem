using System.Text.Json;

namespace GMS.Client.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<TData?> ReadFromJsonWithStreamResetAsync<TData>(this HttpContent content)
        {
            var stream = await content.ReadAsStreamAsync();

            var data = JsonSerializer.Deserialize<TData>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            stream.Seek(0, SeekOrigin.Begin);

            return data;
        }
    }
}
