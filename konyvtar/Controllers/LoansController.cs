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

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            var loans = await _loanService.Get();
            return Ok(loans);
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
        public async Task<IActionResult> AddLoan([FromBody] Loan loan)
        {
            var existingLoan = await _loanService.Get(loan.Id);
            if (existingLoan != null)
            {
                return Conflict();
            }

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
