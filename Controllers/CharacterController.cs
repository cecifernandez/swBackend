using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using swBackend.Interfaces;
using swBackend.Models;

namespace swBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _CharacterRepository;

        public CharacterController(ICharacterRepository CharacterRepository)
        {
            _CharacterRepository = CharacterRepository;
        }

        [HttpGet("characters")]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetAllCharacters()
        {
            var characters = await _CharacterRepository.GetAllCharacters();
            return Ok(characters);
        }
    }
}
