using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stocks.Data;
using StocksOperation.DTO.DTO_ACCOUNTS;
using StocksOperation.Interfaces;
using StocksOperation.Models;
using System.Diagnostics.Eventing.Reader;

namespace StocksOperation.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,ITokenService tokenService,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == dto.UserName.ToLower());
            if (user == null)
            {
                return Unauthorized("UserNotFound");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("UserNotFound");
            }

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email=user.Email,
                    Token=_tokenService.CreateToken(user)
                }
                );

        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.EmailAddress,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var RoleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (RoleResult.Succeeded)
                    {
                        return Ok("User Created");
                    }
                    else
                    {
                        return StatusCode(500, RoleResult.Errors);
                    }

                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }


    }
}
