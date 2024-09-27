using GMS.Shared.Dtos.Requests.Users;
using GMS.Shared.Dtos.Responses.Users;

namespace GMS.API.Controllers
{
    [HaveRoles(Roles.Admin)]
    public class UsersController : BaseController
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork ufw, IMapper mapper)
        {
            _ufw = ufw;
            _mapper = mapper;
        }

        [HttpPost(Router.Users.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto dto)
        {
            if (await _ufw.Users.IsEmailExistsAsync(dto.Email))
                return Conflict(ResponseFactory.Conflict(ErrorMessages.DuplicateEmailAddress));

            var user = _mapper.Map<User>(dto);

            _ufw.Users.Create(user);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok(user.Id));
        }

        [HttpPut(Router.Users.Update)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserDto dto)
        {
            var user = await _ufw.Users.GetByIdAsync(id);

            if (user == null)
                return NotFound(ResponseFactory.NotFound());

            if (await _ufw.Users.IsEmailExistsExceptUserAsync(id, dto.Email))
                return Conflict(ResponseFactory.Conflict(ErrorMessages.DuplicateEmailAddress));

            _mapper.Map(dto, user);

            _ufw.Users.Update(user);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }

        [HttpDelete(Router.Users.Delete)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var user = await _ufw.Users.GetByIdAsync(id);

            if (user == null)
                return NotFound(ResponseFactory.NotFound());

            _ufw.Users.Delete(user);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }

        [HttpGet(Router.Users.GetById)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _ufw.Users.GetByIdAsync(id);

            if (user == null)
                return NotFound(ResponseFactory.NotFound());

            return Ok(ResponseFactory.Ok(_mapper.Map<UserDto>(user)));
        }

        [HttpGet(Router.Users.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _ufw.Users.GetAllAsync();

            return Ok(ResponseFactory.Ok(_mapper.Map<List<UserDto>>(users)));
        }
    }
}
