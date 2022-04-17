using boomoseries_OrchAuth_api.Entities;
using boomoseries_OrchAuth_api.Models;
using boomoseries_OrchAuth_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrchAuthController : Controller
    {
        private readonly IUsersCommunicationService _userService;
        private readonly AppSettings _appSettings;

        public OrchAuthController(
            IUsersCommunicationService userService,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateModel model)
        {
            var response = await _userService.AuthenticateUser(model);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = response.Content.ReadAsStringAsync().Result;

                return BadRequest(errorMessage);
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(responseString);
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info and authentication token
                return Ok(new
                {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = tokenString
                });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var response = await _userService.RegisterUser(model);

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {

                var errorMessage = response.Content.ReadAsStringAsync().Result;

                return BadRequest(errorMessage);
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();      
                var registerModel = JsonConvert.DeserializeObject<RegisterModel>(responseString);
                return Ok(registerModel);
            }
        }
    }
}
