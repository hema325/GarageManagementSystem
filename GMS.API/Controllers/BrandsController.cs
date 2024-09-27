using GMS.Shared.Dtos.Requests.Brands;
using GMS.Shared.Dtos.Responses.Brands;

namespace GMS.API.Controllers
{
    public class BrandsController: BaseController
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;

        public BrandsController(IUnitOfWork ufw, IMapper mapper)
        {
            _ufw = ufw;
            _mapper = mapper;
        }

        [HttpPost(Router.Brands.Create)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> CreateAsync([FromForm] CreateOrUpdateBrandDto dto)
        {
            var brand = _mapper.Map<Brand>(dto);

            if(dto.Image != null)
                brand.ImageUrl = await FileHelpers.SaveFileAsync(dto.Image);

            _ufw.Brands.Create(brand);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok(brand.Id));
        }

        [HttpPut(Router.Brands.Update)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] CreateOrUpdateBrandDto dto)
        {
            var brand = await _ufw.Brands.GetByIdAsync(id);

            if (brand == null)
                return NotFound(ResponseFactory.NotFound());

            if (dto.Image != null)
            {
                if (brand.ImageUrl != null)
                    await FileHelpers.DeleteFileAsync(brand.ImageUrl);

                brand.ImageUrl = await FileHelpers.SaveFileAsync(dto.Image);
            }

            _mapper.Map(dto, brand);
            
            _ufw.Brands.Update(brand);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }

        [HttpDelete(Router.Brands.Delete)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var brand = await _ufw.Brands.GetByIdAsync(id);

            if (brand == null)
                return NotFound(ResponseFactory.NotFound());

            if (brand.ImageUrl != null)
                await FileHelpers.DeleteFileAsync(brand.ImageUrl);

            _ufw.Brands.Delete(brand);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }

        [HttpGet(Router.Brands.GetById)]
        [HaveRoles(Roles.Admin, Roles.Employee)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var brand = await _ufw.Brands.GetByIdAsync(id);

            if (brand == null)
                return NotFound(ResponseFactory.NotFound());

            return Ok(ResponseFactory.Ok(_mapper.Map<BrandDto>(brand)));
        }

        [HttpGet(Router.Brands.GetAll)]
        [HaveRoles(Roles.Admin, Roles.Employee)]
        public async Task<IActionResult> GetAllAsync()
        {
            var brand = await _ufw.Brands.GetAllAsync();

            return Ok(ResponseFactory.Ok(_mapper.Map<List<BrandDto>>(brand)));
        }
    }
}
