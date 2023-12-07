using konyvtar.Contracts;

namespace konyvtar_ui.Services
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>?> GetLoansAsync();
        Task<Loan?> GetLoanAsync(int id);
        Task AddLoanAsync(LoanDTO loan);
        Task UpdateLoanAsync(int id, Loan loan);
        Task DeleteLoanAsync(int id);
    }
}
