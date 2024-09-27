
namespace GMS.Client.Helpers
{
    public static class QueryStringHelpers
    {
        public static string QueryFromObject<TObject>(TObject obj)
        {
            var props = obj!
                .GetType()
                .GetProperties();

            var queries = props.Select(prop =>
            {
                var key = prop.Name;
                var value = prop.GetValue(obj);

                return value == null ? string.Empty : $"{key}={value}";
            });

            return "?" + string.Join("&", queries);
        }
    }
}
