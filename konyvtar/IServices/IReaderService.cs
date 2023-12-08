using konyvtar.Contracts;

namespace konyvtar.IServices
{
    public interface IReaderService
    {
        Task Add(Reader reader);

        Task Delete(Reader reader);

        Task<Reader> Get(int id);

        Task<IEnumerable<Reader>> Get();

        Task Update(Reader newReader);
    }
}
