using konyvtar.Contracts;

namespace konyvtar_ui.Services
{
    public interface IReaderService
    {
        Task<IEnumerable<Reader>?> GetReadersAsync();
        Task<Reader?> GetReaderAsync(int id);
        Task AddReaderAsync(Reader reader);
        Task UpdateReaderAsync(int id, Reader reader);
        Task DeleteReaderAsync(int id);
    }
}
