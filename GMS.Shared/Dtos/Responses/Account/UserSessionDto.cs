namespace GMS.Shared.Dtos.Responses.Account
{
    public class UserSessionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string JWTToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
