using konyvtar.Contracts;


namespace konyvtar
{
    public interface IBookService
    {
        Task Add(Book book);

        Task Delete(Book book);

        Task<Book> Get(int id);

        Task<IEnumerable<Book>> Get();

        Task Update(Book newBook);
    }

    public interface IReaderService
    {
        Task Add(Reader reader);

        Task Delete(Reader reader);

        Task<Reader> Get(int id);

        Task<IEnumerable<Reader>> Get();

        Task Update(Reader newReader);
    }
    public interface ILoanService
    {
        Task Add(Loan loan);

        Task Delete(Loan loan);

        Task<Loan> Get(int id);

        Task<IEnumerable<Loan>> Get();

        Task Update(Loan newLoan);
    }
}
