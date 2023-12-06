using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace konyvtar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        private readonly IReaderService _readerService;

        public ReadersController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reader>>> GetReaders()
        {
            var readers = await _readerService.Get();
            return Ok(readers);
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
        public async Task<ActionResult<Reader>> AddReader(Reader reader)
        {
            await _readerService.Add(reader);
            return CreatedAtAction(nameof(GetReader), new { id = reader.Id }, reader);
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
