using konyvtar.Contracts;

namespace konyvtar_ui.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>?> GetBooksAsync();
        Task<Book?> GetBookAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(int id, Book book);
        Task DeleteBookAsync(int id);
    }
}
