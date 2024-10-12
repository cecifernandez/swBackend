using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using swBackend.Interfaces;
using swBackend.Models;
using swBackend.Repositories;
using System.Net.Http;

namespace swBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly InfoRepository _infoRepository;

        public CharacterController(ICharacterRepository CharacterRepository, InfoRepository infoRepository)
        {
            _characterRepository = CharacterRepository;
            _infoRepository = infoRepository;
        }


        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetAllCharacters()
        {
            var characters = await _characterRepository.GetAllCharacters();

            return Ok(characters);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetCharacter([FromQuery] string name)
        {
            var characters = await _characterRepository.GetAllCharacters();

            if (!string.IsNullOrWhiteSpace(name))
            {
                characters = characters
                    .Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList(); 
            }

            return Ok(characters);
        }
    }
}
