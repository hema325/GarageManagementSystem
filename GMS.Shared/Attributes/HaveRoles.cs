using GMS.Shared.Enums;
using Microsoft.AspNetCore.Authorization;

namespace GMS.Shared.Attributes
{
    public class HaveRoles: AuthorizeAttribute
    {
        public HaveRoles(params Roles[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
