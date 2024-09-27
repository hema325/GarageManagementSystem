using AutoMapper;
using GMS.API.Entities;
using GMS.Shared.Dtos.Requests.Users;
using GMS.Shared.Dtos.Responses.Users;

namespace GMS.API.Mappings.Profiles
{
    public class UsersProfile: Profile
    {
        public UsersProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserDto>();
        }

    }
}
