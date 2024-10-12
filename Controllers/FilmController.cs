using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using swBackend.Interfaces;
using swBackend.Models;


namespace swBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmRepository _FilmRepository;

        public FilmController(IFilmRepository filmRepository)
        {
            _FilmRepository = filmRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<FilmModel>>> GetAllFilmsAsync()
        {

            var films = await _FilmRepository.GetAllFilms();
            return Ok(films);
        }
    }
}
