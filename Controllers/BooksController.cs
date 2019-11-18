using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Books.Api.Models;
using Books.Api.Services;

using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route ("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController (IBooksRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ??
                throw new ArgumentNullException (nameof (bookRepository));
            _mapper = mapper ?? 
                throw new ArgumentNullException (nameof (mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks ()
        {
            var bookEntities = await _bookRepository.GetBooksAsync ();

            return Ok (_mapper.Map<IEnumerable<BookDto>>(bookEntities));
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetBook (Guid id)
        {
            var bookEntity = await _bookRepository.GetBookAsync (id);

            if (bookEntity == null)
            {
                return NotFound ();
            }

            return Ok (_mapper.Map<BookDto>(bookEntity));
        }
    }
}
