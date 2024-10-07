using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using swBackend.Models;
using swBackend.Services;

namespace swBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SWAPIController : ControllerBase
    {
        private readonly SWAPIService _SWAPIService;

        public SWAPIController(SWAPIService SWAPIservice)
        {
            _SWAPIService = SWAPIservice;
        }

        [HttpGet("characters")]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetAllCharacters()
        {
            var characters = await _SWAPIService.GetAllCharactersAsync();
            return Ok(characters);
        }

        [HttpGet("films")]
        public async Task<ActionResult<IEnumerable<FilmModel>>> GetAllFilmsAsync()
        {

            var films = await _SWAPIService.GetAllFilmsAsync();
            return Ok(films);
        }
    }
}
