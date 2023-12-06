using konyvtar.Contracts;
using System.Net.Http.Json;


namespace konyvtar_ui.Services
{
    public class ReaderService : IReaderService
    {
        private readonly HttpClient _httpClient;

        public ReaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<IEnumerable<Reader>?> GetReadersAsync() =>
            _httpClient.GetFromJsonAsync<IEnumerable<Reader>>("Readers");
        public Task<Reader?> GetReaderAsync(int id) =>
            _httpClient.GetFromJsonAsync<Reader>($"Readers/{id}");
        public Task AddReaderAsync(Reader reader) =>
            _httpClient.PostAsJsonAsync("Readers", reader);

        public Task UpdateReaderAsync(int id, Reader reader) =>
            _httpClient.PutAsJsonAsync($"Readers/{id}", reader);
        public Task DeleteReaderAsync(int id) =>
            _httpClient.DeleteAsync($"Readers/{id}");



    }
}
