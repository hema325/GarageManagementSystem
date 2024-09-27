
namespace GMS.API.Services.JWT
{
    public interface IJWTManager
    {
        string GenerateToken(User user);
    }
}
