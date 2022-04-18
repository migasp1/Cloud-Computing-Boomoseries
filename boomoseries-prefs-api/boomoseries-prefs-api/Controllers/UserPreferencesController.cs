using AutoMapper;
using boomoseries_prefs_api.Entities;
using boomoseries_prefs_api.Models;
using boomoseries_prefs_api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace boomoseries_prefs_api.Controllers
{
    [Route("[controller]")]
    public class UserPreferencesController : Controller
    {
        //POST : PREF MOVIE OR SERIE
        //POST : PREF BOOK
        //GET : ALL ITEMS, let the framework serielize for me, I just need the lists and return an anonymous object
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
        public IActionResult AddUserFavoriteWatchable(UserWatchablePreferenceModel userWatchable)
        {
            var user = _mapper.Map<UserWatchablePreference>(userWatchable);
            try
            {
                _prefService.AddUserWatchbleFavorite(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Favorites/Book")]
        public IActionResult AddUserFavoriteBook(UserBookPreferenceModel userBook)
        {
            var user = _mapper.Map<UserBookPreference>(userBook);
            try
            {
                _prefService.AddUserBookFavorite(user);
                return Ok(user);
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
