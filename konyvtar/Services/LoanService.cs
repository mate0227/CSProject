using Konyvtar.Contexts;
using konyvtar.Contracts;
using Microsoft.EntityFrameworkCore;
using konyvtar.IServices;

namespace konyvtar.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILogger<LoanService> _logger;
        private readonly ServiceContexts _servicecontexts;

        public LoanService(ServiceContexts sContext, ILogger<LoanService> logger)
        {
            _servicecontexts = sContext;
            _logger = logger;
        }

        public async Task Add(Loan loan)
        {
            _servicecontexts.Loans.Add(loan);

            await _servicecontexts.SaveChangesAsync();
            _logger.LogInformation("Loan added. Loan: {@Loan}", loan);
        }

        public async Task Delete(Loan loan)
        {
            _servicecontexts.Loans.Remove(loan);
            await _servicecontexts.SaveChangesAsync();

            _logger.LogInformation("Loan deleted. Loan: {@Loan}", loan);
        }

        public async Task<Loan> Get(int id)
        {
            var loan = await _servicecontexts.Loans.FindAsync(id);

            _logger.LogInformation("Loan retrieved. Loan: {@Loan}", loan);
            return loan;
        }

        public async Task<IEnumerable<Loan>> Get()
        {
            _logger.LogInformation("Loans retrieved");

            var loans = await _servicecontexts.Loans.ToListAsync();
            return loans;
        }

        public async Task Update(Loan newLoan)
        {
            var existingLoan = await Get(newLoan.Id);
            existingLoan.BorrowDate = newLoan.BorrowDate;
            existingLoan.ReturnDeadline = newLoan.ReturnDeadline;

            _logger.LogInformation("Loan updated. Loan: {@Loan}", existingLoan);
            await _servicecontexts.SaveChangesAsync();
        }
    }

}
