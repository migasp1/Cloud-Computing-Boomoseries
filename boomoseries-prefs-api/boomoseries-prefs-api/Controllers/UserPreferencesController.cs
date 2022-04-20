using AutoMapper;
using boomoseries_prefs_api.Entities;
using boomoseries_prefs_api.Models;
using boomoseries_prefs_api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace boomoseries_prefs_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserPreferencesController : Controller
    {
        private IUserPreferenceService _prefService;
        private readonly IMapper _mapper;

        public UserPreferencesController(
            IUserPreferenceService prefService,
            IMapper mapper)
        {
            _prefService = prefService; 
            _mapper = mapper;           
        }

        [HttpPost("Favorites/Watchable")]
        public IActionResult AddUserFavoriteWatchable(UserWatchablePreferenceDTO watchableModel)
        {
            try
            {
                var entity = _mapper.Map<UserWatchablePreference>(watchableModel);
                _prefService.AddUserWatchbleFavorite(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Favorites/Book")]
        public IActionResult AddUserFavoriteBook(UserBookPreferenceDTO bookModel)
        {
            var entity = _mapper.Map<UserBookPreference>(bookModel);
            try
            {
                _prefService.AddUserBookFavorite(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("Favorites")]
        public IActionResult GetFavoritesList(int userId)
        {
            //var user = _mapper.Map<UserBookPreference>(userBook);
            try
            {
                var userBooks = _prefService.GetUserFavoriteBooks(userId);
                var userWatchables = _prefService.GetUserFavoriteWatchables(userId);

                return Ok(new FavoritesModel
                {
                    Books = userBooks,
                    Watchables = userWatchables
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
