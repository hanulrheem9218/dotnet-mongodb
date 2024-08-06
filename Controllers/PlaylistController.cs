using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;

namespace MongoExample.Controllers
{
    [Controller, Route("api/[controller]")]
    public class PlayListController : Controller
    {
        private readonly MongoDBService _mongoDBService;
        public PlayListController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<PlayList>> Get()
        {
            return await _mongoDBService.GetAsync();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlayList playList)
        {
            await _mongoDBService.CreateAsync(playList);
            return CreatedAtAction(nameof(Get), new {id = playList.Id},playList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId)
        {
            await _mongoDBService.AddToPlayListAsync(id, movieId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsybc(id);
            return NoContent();
        }
    }
}