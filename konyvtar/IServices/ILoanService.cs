using konyvtar.Contracts;

namespace konyvtar.IServices
{
    public interface ILoanService
    {
        Task Add(Loan loan);

        Task Delete(Loan loan);

        Task<Loan> Get(int id);

        Task<IEnumerable<Loan>> Get();

        Task Update(Loan newLoan);
    }
}
