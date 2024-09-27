namespace GMS.API.Settings
{
    public class JWTSettings
    {
        public const string SectionName = "JWT";

        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string SigningKey { get; init; }
        public double ExpirationInMinutes { get; init; } 
    }
}
