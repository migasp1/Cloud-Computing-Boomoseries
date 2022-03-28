﻿using boomoseries_Movies_api.DTOs;
using boomoseries_Movies_api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace boomoseries_Movies_api.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ICommunicationService commService;
        public MoviesController(ICommunicationService commService)
        {
            this.commService = commService;
        }

        [HttpGet("/api/v1/Movies/{movie_title}")]
        public async Task<IActionResult> GetSpecificMovie(string movie_title)
        {
            try
            {
                var responseBody = await commService.ObtainSepcificMovie(movie_title);
                MovieDTO deserializedMovie = JsonConvert.DeserializeObject<MovieDTO>(responseBody);
                return Ok(deserializedMovie);    
            }
            catch (Exception ex)
            {
               return BadRequest("Oops, something went wrong! " + ex.Message);
            }
            
        }
    }
}