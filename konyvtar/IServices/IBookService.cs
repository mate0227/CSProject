using konyvtar.Contracts;

namespace konyvtar.IServices
{
    public interface IBookService
    {
        Task Add(Book book);

        Task Delete(Book book);

        Task<Book> Get(int id);

        Task<IEnumerable<Book>> Get();

        Task Update(Book newBook);
    }
}
