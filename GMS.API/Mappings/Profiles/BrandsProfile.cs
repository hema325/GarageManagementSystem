using GMS.API.Mappings.Resolvers;
using GMS.Shared.Dtos.Requests.Brands;
using GMS.Shared.Dtos.Responses.Brands;

namespace GMS.API.Mappings.Profiles
{
    public class BrandsProfile: Profile
    {
        public BrandsProfile()
        {
            CreateMap<CreateOrUpdateBrandDto, Brand>()
                .ForMember(des => des.ImageUrl, opt => opt.Ignore());

            CreateMap<Brand, BrandDto>()
                .ForMember(des => des.ImageUrl, opt => opt.MapFrom<ImageUrlResolver>());
        }
    }
}
