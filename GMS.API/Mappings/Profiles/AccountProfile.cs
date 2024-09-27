using GMS.Shared.Dtos.Responses.Account;

namespace GMS.API.Mappings.Profiles
{
    public class AccountProfile: Profile
    {
        public AccountProfile()
        {
            CreateMap<User, UserSessionDto>();
        }
    }
}
