using Konyvtar.Contexts;
using konyvtar.Contracts;
using Microsoft.EntityFrameworkCore;
using konyvtar.IServices;

namespace konyvtar.Services
{
    public class BookService : IBookService
    {
        private readonly ILogger<BookService> _logger;
        private readonly ServiceContexts _servicecontexts;

        public BookService(ServiceContexts sContext, ILogger<BookService> logger)
        {
            _servicecontexts = sContext;
            _logger = logger;
        }

        public async Task Add(Book book)
        {
            _servicecontexts.Books.Add(book);

            await _servicecontexts.SaveChangesAsync();
            _logger.LogInformation("Book added. Book: {@Book}", book);
        }

        public async Task Delete(Book book)
        {
            _servicecontexts.Books.Remove(book);
            await _servicecontexts.SaveChangesAsync();

            _logger.LogInformation("Book deleted. Book: {@Book}", book);
        }

        public async Task<Book> Get(int id)
        {
            var book = await _servicecontexts.Books.FindAsync(id);

            _logger.LogInformation("Book retrieved. Book: {@Book}", book);
            return book;
        }

        public async Task<IEnumerable<Book>> Get()
        {
            _logger.LogInformation("Books retrieved");

            var books = await _servicecontexts.Books.ToListAsync();
            return books;
        }

        public async Task Update(Book newBook)
        {
            var existingBook = await Get(newBook.Id);
            existingBook.Title = newBook.Title;
            existingBook.Author = newBook.Author;
            existingBook.Publisher = newBook.Publisher;
            existingBook.PublicationYear = newBook.PublicationYear;

            _logger.LogInformation("Book updated. Book: {@Book}", existingBook);
            await _servicecontexts.SaveChangesAsync();
        }
    }
}
