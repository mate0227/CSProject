using konyvtar.Contracts;
using System.Net.Http.Json;


namespace konyvtar_ui.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<IEnumerable<Book>?> GetBooksAsync() =>
            _httpClient.GetFromJsonAsync<IEnumerable<Book>>("Books");
        public Task<Book?> GetBookAsync(int id) =>
            _httpClient.GetFromJsonAsync<Book>($"Books/{id}");
        public Task AddBookAsync(Book book) =>
            _httpClient.PostAsJsonAsync("Books", book);

        public Task UpdateBookAsync(int id, Book book) =>
            _httpClient.PutAsJsonAsync($"Books/{id}", book);
        public Task DeleteBookAsync(int id) =>
            _httpClient.DeleteAsync($"Books/{id}");



    }
}
