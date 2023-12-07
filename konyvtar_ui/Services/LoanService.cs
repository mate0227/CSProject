using konyvtar.Contracts;
using System.Net.Http.Json;

namespace konyvtar_ui.Services
{
    public class LoanService : ILoanService
    {
        private readonly HttpClient _httpClient;

        public LoanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<IEnumerable<Loan>?> GetLoansAsync() =>
            _httpClient.GetFromJsonAsync<IEnumerable<Loan>>("Loans");
        public Task<Loan?> GetLoanAsync(int id) =>
            _httpClient.GetFromJsonAsync<Loan>($"Loans/{id}");
        public Task AddLoanAsync(Loan loan) =>
            _httpClient.PostAsJsonAsync("Loans", loan);

        public Task UpdateLoanAsync(int id, Loan loan) =>
            _httpClient.PutAsJsonAsync($"Loans/{id}", loan);
        public Task DeleteLoanAsync(int id) =>
            _httpClient.DeleteAsync($"Loans/{id}");



    }
}
