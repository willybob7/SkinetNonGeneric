using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.DTOs;
using API.Errors;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                //Token = "This will be a token",
                Token = _tokenService.CreateToken(user),
                Username = user.DisplayName
            };
        }


        [HttpPost("logout")]
        public async Task<ActionResult<UserDto>> Logout()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            await _signInManager.SignOutAsync();

            return new UserDto
            {
                Email = user.Email,
                Token = $"{user.DisplayName} has been signed out",
                Username = user.DisplayName
            };
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                Username = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //does the same thing
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Username = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.Users.AsNoTracking().Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
            return new AddressDTO
            {
                FirstName = user.Address.FirstName,
                LastName = user.Address.LastName,
                State = user.Address.State,
                Street = user.Address.Street,
                City = user.Address.City,
                ZipCode = user.Address.ZipCode
            };
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);

            if (user.Address is null)
            {
                user.Address = new Address
                {
                    AppUser = user,
                    AppUserId = user.Id
                };
            }

            user.Address.FirstName = address.FirstName;
            user.Address.LastName = address.LastName;
            user.Address.Street = address.Street;
            user.Address.City = address.City;
            user.Address.State = address.State;
            user.Address.ZipCode = address.ZipCode;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok(new AddressDTO
                {
                    FirstName = user.Address.FirstName,
                    LastName = user.Address.LastName,
                    State = user.Address.State,
                    Street = user.Address.Street,
                    City = user.Address.City,
                    ZipCode = user.Address.ZipCode
                });

            return BadRequest("Problem updating the user");
        }
    }
}
