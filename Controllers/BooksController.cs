using System;
using System.Threading.Tasks;

using Books.Api.Services;

using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route ("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _bookRepository;

        public BooksController (IBooksRepository bookRepository)
        {
            _bookRepository = bookRepository ??
                throw new ArgumentNullException (nameof (bookRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks ()
        {
            var bookEntities = await _bookRepository.GetBooksAsync ();

            return Ok (bookEntities);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetBook (Guid id)
        {
            var bookEntity = await _bookRepository.GetBookAsync (id);

            if (bookEntity == null)
            {
                return NotFound ();
            }

            return Ok (bookEntity);
        }
    }
}
