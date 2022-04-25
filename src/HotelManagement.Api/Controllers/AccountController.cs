using HotelManagement.Api.Requests;
using HotelManagement.Core.Configuration;
using HotelManagement.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly HotelManagementSettings _hotelManagementSettings;
        private readonly SignInManager<HotelManagementUser> _signInManager;
        private readonly UserManager<HotelManagementUser> _userManager;

        public AccountController(SignInManager<HotelManagementUser> signInManager,
          UserManager<HotelManagementUser> userManager,
          IOptions<HotelManagementSettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _hotelManagementSettings = options.Value;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    // Create the token
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                    };

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_hotelManagementSettings.Tokens.Key));
                    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                      _hotelManagementSettings.Tokens.Issuer,
                      _hotelManagementSettings.Tokens.Audience,
                      claims,
                      expires: DateTime.Now.AddMinutes(30),
                      signingCredentials: signingCredentials);

                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };

                    return Created("", results);
                }
            }

            return Unauthorized();
        }
    }
}