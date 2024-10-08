﻿using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetAllCharacters(/*[FromQuery] string name*/)
        {
            var characters = await _CharacterRepository.GetAllCharacters();

            //if (!string.IsNullOrEmpty(name))
            //{
            //    characters = characters.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            //}

            return Ok(characters);
        }

        [HttpGet("characters/search")]
        public async Task<ActionResult<IEnumerable<CharacterModel>>> GetCharacter([FromQuery] string name)
        {
            var characters = await _CharacterRepository.GetAllCharacters();

            if (!string.IsNullOrEmpty(name))
            {
                characters = characters.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Ok(characters);
        }
    }
}
