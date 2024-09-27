using GMS.Shared.Dtos.Responses.Brands;

namespace GMS.API.Mappings.Resolvers
{
    public class ImageUrlResolver : IValueResolver<Brand, BrandDto, string>
    {
        private readonly HttpContext _httpContext;
        public ImageUrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext!;
        }
        public string? Resolve(Brand source, BrandDto destination, string destMember, ResolutionContext context)
        {
            if(source.ImageUrl != null)
                return $"{_httpContext.Request.Scheme}://{_httpContext.Request.Host}/{source.ImageUrl}";

            return null;
        }
    }
}
