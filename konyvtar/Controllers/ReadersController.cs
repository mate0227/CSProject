using konyvtar.Contracts;
using konyvtar.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace konyvtar.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        private readonly IReaderService _readerService;

        public ReadersController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reader>>> GetReaders()
        {
            var readers = await _readerService.Get();
            return Ok(readers.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reader>> GetReader(int id)
        {
            var reader = await _readerService.Get(id);
            if (reader == null)
            {
                return NotFound();
            }
            return Ok(reader);
        }

        [HttpPost]
        public async Task<IActionResult> AddReader([FromBody] Reader reader)
        {
            var existingReader = await _readerService.Get(reader.Id);
            if (existingReader is not null)
            {
                return Conflict();
            }

            await _readerService.Add(reader);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Reader>> UpdateReader(int id, Reader reader)
        {
            if (id != reader.Id)
            {
                return BadRequest();
            }

            await _readerService.Update(reader);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reader>> DeleteReader(int id)
        {
            var reader = await _readerService.Get(id);
            if (reader == null)
            {
                return NotFound();
            }

            await _readerService.Delete(reader);
            return Ok(reader);
        }
    }
}
