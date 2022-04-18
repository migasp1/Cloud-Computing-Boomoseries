using boomoseries_OrchAuth_api.Entities;
using boomoseries_OrchAuth_api.Helpers;
using boomoseries_OrchAuth_api.Models;
using boomoseries_OrchAuth_api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrchAuthController : Controller
    {
        private readonly IUsersCommunicationService _userService;
        private readonly IUserPreferencesService _userPreferencesService;
        private readonly AppSettings _appSettings;

        public OrchAuthController(
            IUsersCommunicationService userService,
            IUserPreferencesService userPreferencesService,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            _userPreferencesService = userPreferencesService;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var response = await _userService.RegisterUser(model);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
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

        [HttpPost("favorites")]
        public async Task<IActionResult> GetUserFavorites()
        {
            try
            {
                var userId = int.Parse(HttpContext.GetUserId());
                var response = await _userPreferencesService.GetFavoriteWatchables(userId);
                var deserialized = JsonConvert.DeserializeObject<FavoritesModel>(response);

                return Ok(deserialized);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Oops, something went wrong!" });
            }
        }

        //missing: search communication, post favorite watchable and post favorite book
    }
}
