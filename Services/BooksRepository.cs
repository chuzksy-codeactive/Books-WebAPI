using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Books.Api.Contexts;
using Books.Api.Entities;

using Microsoft.EntityFrameworkCore;

namespace Books.Api.Services
{
    public class BooksRepository : IBooksRepository, IDisposable
    {
        private BooksContext _booksContext;

        public BooksRepository (BooksContext booksContext)
        {
            _booksContext = booksContext;
        }
        public async Task<Book> GetBookAsync (Guid id)
        {
            return await _booksContext.Books.Include (b => b.Author).FirstOrDefaultAsync (a => a.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync ()
        {
            return await _booksContext.Books.Include (book => book.Author).ToListAsync ();
        }

        public void Dispose ()
        {
            Dispose (true);
            GC.SuppressFinalize (this);
        }

        protected virtual void Dispose (bool disposing)
        {
            if (disposing)
            {
                if (_booksContext != null)
                {
                    _booksContext.Dispose ();
                    _booksContext = null;
                }
            }
        }
    }
}
