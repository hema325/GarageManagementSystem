using GMS.Shared.Dtos.Requests.Owners;
using GMS.Shared.Dtos.Responses.Owners;

namespace GMS.API.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;

        public OwnersController(IUnitOfWork ufw, IMapper mapper)
        {
            _ufw = ufw;
            _mapper = mapper;
        }

        [HttpPost(Router.Owners.Create)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateOwnerDto dto)
        {
            var owner = _mapper.Map<Owner>(dto);    

            _ufw.Owners.Create(owner);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok(owner.Id));
        }

        [HttpPut(Router.Owners.Update)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CreateOrUpdateOwnerDto dto)
        {
            var owner = await _ufw.Owners.GetByIdAsync(id);

            if (owner == null)
                return NotFound(ResponseFactory.NotFound());

            _mapper.Map(dto, owner);

            _ufw.Owners.Update(owner);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }

        [HttpDelete(Router.Owners.Delete)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var owner = await _ufw.Owners.GetByIdAsync(id);

            if (owner == null)
                return NotFound(ResponseFactory.NotFound());


            _ufw.Owners.Delete(owner);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }

        [HttpGet(Router.Owners.GetById)]
        [HaveRoles(Roles.Admin, Roles.Employee)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var owner = await _ufw.Owners.GetByIdAsync(id);

            if (owner == null)
                return NotFound(ResponseFactory.NotFound());

            return Ok(ResponseFactory.Ok(_mapper.Map<OwnerDto>(owner)));
        }

        [HttpGet(Router.Owners.GetAll)]
        [HaveRoles(Roles.Admin, Roles.Employee)]
        public async Task<IActionResult> GetAllAsync()
        {
            var owners = await _ufw.Owners.GetAllAsync();

            return Ok(ResponseFactory.Ok(_mapper.Map<List<OwnerDto>>(owners)));
        }
    }
}
