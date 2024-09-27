using GMS.Shared.Enums;

namespace GMS.API.Entities
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
    }
}
