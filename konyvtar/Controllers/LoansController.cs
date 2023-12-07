using konyvtar.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace konyvtar.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;
        private readonly IReaderService _readerService;

        public LoansController(ILoanService loanService, IBookService bookService, IReaderService readerService)
        {
            _loanService = loanService;
            _bookService = bookService;
            _readerService = readerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetLoans()
        {
            var loans = await _loanService.Get();
            return Ok(loans.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _loanService.Get(id);
            if (loan == null)
            {
                return NotFound();
            }
            return Ok(loan);
        }

        [HttpPost]
        public async Task<IActionResult> AddLoan([FromBody] LoanDTO loanDTO)
        {
            var existingLoan = await _loanService.Get(loanDTO.Id);
            if (existingLoan != null)
            {
                return Conflict();
            }

            Loan loan = new Loan()
            {
                BookId = loanDTO.BookId,
                ReaderId = loanDTO.ReaderId,
                BorrowDate = loanDTO.BorrowDate,
                ReturnDeadline = loanDTO.ReturnDeadline,
                Book = await _bookService.Get(loanDTO.BookId),
                Reader = await _readerService.Get(loanDTO.ReaderId)
            };

            await _loanService.Add(loan);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Loan>> UpdateLoan(int id, Loan loan)
        {
            if (id != loan.Id)
            {
                return BadRequest();
            }

            await _loanService.Update(loan);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Loan>> DeleteLoan(int id)
        {
            var loan = await _loanService.Get(id);
            if (loan == null)
            {
                return NotFound();
            }

            await _loanService.Delete(loan);
            return Ok(loan);
        }
    }
}
