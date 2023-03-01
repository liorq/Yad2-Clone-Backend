using asp.net_workshop_real_app_public.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_workshop_real_app_public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var res = await _booksRepository.GetAllBooksAsync();
            if(res?.Count > 0)
            {
                return Ok(res);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var res = await _booksRepository.GetBookById(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
