using AutoMapper;
using boomoseries_OrchAuth_api.DTOs;
using boomoseries_OrchAuth_api.Entities;
using boomoseries_OrchAuth_api.Helpers;
using boomoseries_OrchAuth_api.Models;
using boomoseries_OrchAuth_api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
    [Route("api/v1/[controller]")]
    public class OrchAuthController : Controller
    {
        private readonly IUsersCommunicationService _userService;
        private readonly IUserPreferencesService _userPreferencesService;
        private readonly ISearchCommunicationServiceWatchables _commServiceSearchWatchables;
        private readonly ISearchCommunicationServiceBooks _commServiceSearchBooks;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private ILogger<OrchAuthController> logger; 

        public OrchAuthController(
            IUsersCommunicationService userService,
            IUserPreferencesService userPreferencesService,
            IOptions<AppSettings> appSettings,
            IMapper mapper,
            ISearchCommunicationServiceBooks commServiceSearchBooks,
            ISearchCommunicationServiceWatchables commServiceSearchWatchables,
            ILogger<OrchAuthController> logger)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            _userPreferencesService = userPreferencesService;
            _mapper = mapper;
            _commServiceSearchWatchables= commServiceSearchWatchables;
            _commServiceSearchBooks = commServiceSearchBooks;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
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
        [HttpPost("Register")]
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


        [HttpPost("Favorites/Book")]
        public async Task<IActionResult> AddFavoriteBook(UserBookPreferenceModel model)
        {
            try
            {
                var bookModel = _mapper.Map<UserBookPreferenceDTO>(model);
                var userId = int.Parse(HttpContext.GetUserId());
                bookModel.Userid = userId;    
                var response = await _userPreferencesService.AddFavoriteBook(bookModel);
                var deserialized = JsonConvert.DeserializeObject<FavoritesModel>(response);

                return Ok(deserialized);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Favorites/Watchable")]
        public async Task<IActionResult> AddFavoriteWatchable(UserWatchablePreferenceModel model)
        {
            try
            {
                var watchableModel = _mapper.Map<UserWatchablePreferenceDTO>(model);
                var userId = int.Parse(HttpContext.GetUserId());
                watchableModel.Userid = userId;
                var response = await _userPreferencesService.AddFavoriteWatchables(watchableModel);
                var deserialized = JsonConvert.DeserializeObject<FavoritesModel>(response);

                return Ok(deserialized);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.ToString());
                return BadRequest(new { message = "Oops, something went wrong!" });
            }
        }

        [HttpPost("Favorites")]
        public async Task<IActionResult> GetUserFavorites()
        {
            try
            {
                var userId = int.Parse(HttpContext.GetUserId());
                var response = await _userPreferencesService.GetFavoriteWatchables(userId);
                var deserialized = JsonConvert.DeserializeObject<FavoritesModel>(response);

                return Ok(deserialized);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.ToString());
                return BadRequest(new { message = "Oops, something went wrong!" });
            }
        }

        [AllowAnonymous]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchItemsByRating(string type, double minRating)
        {
            if(type == null)
                return BadRequest("Oops, something went wrong - Null type!");
            if (type.Equals("Movie"))
            {
                try
                {
                    var responseBody = await _commServiceSearchWatchables.GetWatchblesByRating(type, minRating);
                    if (responseBody is List<WatchableDTO>)
                    {
                        List<WatchableDTO> responseBodyTyped = (List<WatchableDTO>)responseBody;
                        return Ok(responseBodyTyped);
                    }
                    return BadRequest((string)responseBody);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.ToString());
                    return BadRequest("Oops, something went wrong! " + ex.ToString());
                }
            }
            else if (type.Equals("TV Show"))
            {
                try
                {
                    var responseBody = await _commServiceSearchWatchables.GetWatchblesByRating(type, minRating);
                    if (responseBody is List<WatchableDTO>)
                    {
                        List<WatchableDTO> responseBodyTyped = (List<WatchableDTO>)responseBody;
                        return Ok(responseBodyTyped);
                    }
                    return BadRequest((string)responseBody);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.ToString());
                    return BadRequest("Oops, something went wrong! " + ex.ToString());
                }
            }
            else if (type.Equals("Book"))
            {
                try
                {
                    var responseBody = await _commServiceSearchBooks.ObtainBooksByRating(type, minRating);
                    if (responseBody is List<BookDTO>)
                    {
                        List<BookDTO> responseBodyTyped = (List<BookDTO>)responseBody;
                        return Ok(responseBodyTyped);
                    }
                    return BadRequest((string)responseBody);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.ToString());
                    return BadRequest("Oops, something went wrong! error" + ex.ToString());
                }
            }
            return BadRequest("Oops, something went wrong! Type did not match");
        }

        [AllowAnonymous]
        [HttpGet("Search/{item_title}")]
        public async Task<IActionResult> GetSpecificItem(string type, string item_title)
        {
            if (type == null)
                return BadRequest("Oops, something went wrong! ");
            if (type.Equals("Movie"))
            {
                try
                {
                    var responseBody = await _commServiceSearchWatchables.ObtainSepcificWatchables(type, item_title);
                    if (responseBody is List<WatchableDTO>)
                    {
                        List<WatchableDTO> responseBodyTyped = (List<WatchableDTO>)responseBody;
                        return Ok(responseBodyTyped);
                    }
                    return BadRequest((string)responseBody);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.ToString());
                    return BadRequest("Oops, something went wrong! " + ex.ToString());
                }
            }
            else if (type.Equals("TV Show"))
            {
                try
                {
                    var responseBody = await _commServiceSearchWatchables.ObtainSepcificWatchables(type, item_title);
                    if (responseBody is List<WatchableDTO>)
                    {
                        List<WatchableDTO> responseBodyTyped = (List<WatchableDTO>)responseBody;
                        return Ok(responseBodyTyped);
                    }
                    return BadRequest((string)responseBody);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.ToString());
                    return BadRequest("Oops, something went wrong! " + ex.ToString());
                }
            }
            else if (type.Equals("Book"))
            {
                try
                {
                    var responseBody = await _commServiceSearchBooks.ObtainSpecificBook(type, item_title);
                    if (responseBody is List<BookDTO>)
                    {
                        List<BookDTO> responseBodyTyped = (List<BookDTO>)responseBody;
                        return Ok(responseBodyTyped);
                    }
                    return BadRequest((string)responseBody);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.ToString());
                    return BadRequest("Oops, something went wrong! " + ex.ToString());
                }
            }
            return BadRequest("Oops, something went wrong! ");
        }

        [AllowAnonymous]
        [HttpGet("Search/random")]
        public async Task<IActionResult> GetRandomItems(string type)
        {
            if (type == null)
                return BadRequest("Oops, something went wrong! ");
            if (type.Equals("Movie"))
            {
                try
                {
                    var responseBody = await _commServiceSearchWatchables.ObtainRandomWatchable(type);
                    if (responseBody is List<WatchableDTO>)
                    {
                        List<WatchableDTO> responseBodyTyped = (List<WatchableDTO>)responseBody;
                        return Ok(responseBodyTyped);
                    }
                    return BadRequest((string)responseBody);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.ToString());
                    return BadRequest("Oops, something went wrong! " + ex.ToString());
                }
            }
            else if (type.Equals("TV Show"))
            {
                try
                {
                    var responseBody = await _commServiceSearchWatchables.ObtainRandomWatchable(type);
                    if (responseBody is List<WatchableDTO>)
                    {
                        List<WatchableDTO> responseBodyTyped = (List<WatchableDTO>)responseBody;
                        return Ok(responseBodyTyped);
                    }
                    return BadRequest((string)responseBody);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.ToString());
                    return BadRequest("Oops, something went wrong! " + ex.ToString());
                }
            }
            else if (type.Equals("Book"))
            {
                try
                {
                    var responseBody = await _commServiceSearchBooks.ObtainRandomBooks(type);
                    if (responseBody is List<BookDTO>)
                    {
                        List<BookDTO> responseBodyTyped = (List<BookDTO>)responseBody;
                        return Ok(responseBodyTyped);
                    }
                    return BadRequest((string)responseBody);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.ToString());
                    return BadRequest("Oops, something went wrong! " + ex.ToString());
                }
            }
            return BadRequest("Oops, something went wrong! ");
        }
    }
}
