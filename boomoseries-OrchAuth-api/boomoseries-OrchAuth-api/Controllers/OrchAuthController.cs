using AutoMapper;
using boomoseries_OrchAuth_api.Models;
using boomoseries_OrchAuth_api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boomoseries_OrchAuth_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrchAuthController : Controller
    {
        private readonly IUsersCommunicationService _userService;

        public OrchAuthController(
            IUsersCommunicationService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateModel model)
        {
            return Ok();
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
