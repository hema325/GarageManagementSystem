using GMS.Shared.Dtos.Requests.Owners;
using GMS.Shared.Dtos.Responses.Owners;

namespace GMS.API.Mappings.Profiles
{
    public class OwnersProfile: Profile
    {
        public OwnersProfile()
        {
            CreateMap<CreateOrUpdateOwnerDto, Owner>();
            CreateMap<Owner, OwnerDto>();
        }
    }
}
