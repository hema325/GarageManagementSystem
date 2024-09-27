using System.Security.Claims;

namespace GMS.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? Id(this ClaimsPrincipal user)
        {
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);

            return id == null ? null : int.Parse(id);
        }

        public static string? Email(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.Email);
    }
}
