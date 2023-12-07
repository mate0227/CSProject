using konyvtar.Contracts;
using Konyvtar.Contexts;
using Microsoft.EntityFrameworkCore;

namespace konyvtar
{
    public class ReaderService : IReaderService
    {
        private readonly ILogger<ReaderService> _logger;
        private readonly ServiceContexts _servicecontexts;

        public ReaderService(ServiceContexts sContext, ILogger<ReaderService> logger)
        {
            _servicecontexts = sContext;
            _logger = logger;
        }

        public async Task Add(Reader reader)
        {
            _servicecontexts.Readers.Add(reader);

            await _servicecontexts.SaveChangesAsync();
            _logger.LogInformation("Reader added. Reader: {@Reader}", reader);
        }

        public async Task Delete(Reader reader)
        {
            _servicecontexts.Readers.Remove(reader);
            await _servicecontexts.SaveChangesAsync();

            _logger.LogInformation("Reader deleted. Reader: {@Reader}", reader);
        }

        public async Task<Reader> Get(int id)
        {
            var reader = await _servicecontexts.Readers.FindAsync(id);

            _logger.LogInformation("Reader retrieved. Reader: {@Reader}", reader);
            return reader;
        }

        public async Task<IEnumerable<Reader>> Get()
        {
            _logger.LogInformation("Readers retrieved");

            var readers = await _servicecontexts.Readers.ToListAsync();
            return readers;
        }

        public async Task Update(Reader newReader)
        {
            var existingReader = await Get(newReader.Id);
            existingReader.BirthDate = newReader.BirthDate;
            existingReader.Address = newReader.Address;
            existingReader.Name = newReader.Name;

            _logger.LogInformation("Reader updated. Reader: {@Reader}", existingReader);
            await _servicecontexts.SaveChangesAsync();
        }
    }
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
    public class LoanService : ILoanService
    {
        private readonly ILogger<LoanService> _logger;
        private readonly ServiceContexts _servicecontexts;

        public LoanService(ServiceContexts sContext, ILogger<LoanService> logger)
        {
            _servicecontexts = sContext;
            _logger = logger;
        }

        public async Task Add(Loan loan)
        {
            _servicecontexts.Loans.Add(loan);

            await _servicecontexts.SaveChangesAsync();
            _logger.LogInformation("Loan added. Loan: {@Loan}", loan);
        }

        public async Task Delete(Loan loan)
        {
            _servicecontexts.Loans.Remove(loan);
            await _servicecontexts.SaveChangesAsync();

            _logger.LogInformation("Loan deleted. Loan: {@Loan}", loan);
        }

        public async Task<Loan> Get(int id)
        {
            var loan = await _servicecontexts.Loans.FindAsync(id);

            _logger.LogInformation("Loan retrieved. Loan: {@Loan}", loan);
            return loan;
        }

        public async Task<IEnumerable<Loan>> Get()
        {
            _logger.LogInformation("Loans retrieved");

            var loans = await _servicecontexts.Loans.ToListAsync();
            return loans;
        }

        public async Task Update(Loan newLoan)
        {
            var existingLoan = await Get(newLoan.Id);
            existingLoan.BorrowDate = newLoan.BorrowDate;
            existingLoan.ReturnDeadline = newLoan.ReturnDeadline;

            _logger.LogInformation("Loan updated. Loan: {@Loan}", existingLoan);
            await _servicecontexts.SaveChangesAsync();
        }
    }

}
