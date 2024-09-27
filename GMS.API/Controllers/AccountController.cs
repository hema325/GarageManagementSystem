using GMS.API.Helpers;
using GMS.API.Services.JWT;
using GMS.Shared.Dtos.Requests.Account;
using GMS.Shared.Dtos.Responses.Account;
using Microsoft.AspNetCore.Authorization;

namespace GMS.API.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jwtManager;

        public AccountController(IUnitOfWork ufw,
                                 IMapper mapper,
                                 IJWTManager jwtManager)
        {
            _ufw = ufw;
            _mapper = mapper;
            _jwtManager = jwtManager;
        }

        [HttpPost(Router.Account.Login)]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto dto)
        {
            var user = await _ufw.Users.GetByEmailAsync(dto.Email);

            if (user == null || user.Password != dto.Password)
                return Unauthorized(ResponseFactory.Unauthorized(ErrorMessages.InvalidEmailOrPassword));

            user.RefreshToken = KeyHelpers.GetRandomKey();

            _ufw.Users.Update(user);
            await _ufw.SaveChangesAsync();

            var response = _mapper.Map<UserSessionDto>(user);
            response.JWTToken = _jwtManager.GenerateToken(user);

            return Ok(ResponseFactory.Ok(response));
        }

        [HttpPost(Router.Account.RequestJwtToken)]
        [AllowAnonymous]
        public async Task<IActionResult> RequestJwtTokenAsync([FromBody] RequestJwtTokenDto dto)
        {
            var user = await _ufw.Users.GetByIdAsync(dto.UserId);

            if (user == null || user.RefreshToken != dto.RefreshToken)
                return Unauthorized(ResponseFactory.Unauthorized(ErrorMessages.InvalidRefreshToken));

            user.RefreshToken = KeyHelpers.GetRandomKey();

            _ufw.Users.Update(user);
            await _ufw.SaveChangesAsync();

            var response = _mapper.Map<UserSessionDto>(user);
            response.JWTToken = _jwtManager.GenerateToken(user);

            return Ok(ResponseFactory.Ok(response));
        }
        
        [HttpPost(Router.Account.RevokeRefreshToken)]
        public async Task<IActionResult> RevokeRefreshTokenAsync()
        {
            var user = await _ufw.Users.GetByIdAsync(User.Id()!.Value);

            user!.RefreshToken = null;

            _ufw.Users.Update(user);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }

        [HttpPut(Router.Account.ChangePassword)]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto dto)
        {
            var user = await _ufw.Users.GetByIdAsync(User.Id()!.Value);

            if (user!.Password != dto.OldPassword)
                return Unauthorized(ResponseFactory.Unauthorized(ErrorMessages.InvalidOldPassword));

            user.Password = dto.NewPassword;

            _ufw.Users.Update(user);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }
    }
}
