using Konyvtar.Contexts;
using konyvtar.Contracts;
using Microsoft.EntityFrameworkCore;
using konyvtar.IServices;

namespace konyvtar.Services
{
    public class ReaderService : IReaderService
    {
        private readonly ILogger<ReaderService> _logger;
        private readonly ServiceContexts _servicecontexts;

        public ReaderService(ServiceContexts sContext, ILogger<ReaderService> logger)
        {
            _servicecontexts = sContext;
            _logger = logger;
        }

        public async Task Add(Reader reader)
        {
            _servicecontexts.Readers.Add(reader);

            await _servicecontexts.SaveChangesAsync();
            _logger.LogInformation("Reader added. Reader: {@Reader}", reader);
        }

        public async Task Delete(Reader reader)
        {
            _servicecontexts.Readers.Remove(reader);
            await _servicecontexts.SaveChangesAsync();

            _logger.LogInformation("Reader deleted. Reader: {@Reader}", reader);
        }

        public async Task<Reader> Get(int id)
        {
            var reader = await _servicecontexts.Readers.FindAsync(id);

            _logger.LogInformation("Reader retrieved. Reader: {@Reader}", reader);
            return reader;
        }

        public async Task<IEnumerable<Reader>> Get()
        {
            _logger.LogInformation("Readers retrieved");

            var readers = await _servicecontexts.Readers.ToListAsync();
            return readers;
        }

        public async Task Update(Reader newReader)
        {
            var existingReader = await Get(newReader.Id);
            existingReader.BirthDate = newReader.BirthDate;
            existingReader.Address = newReader.Address;
            existingReader.Name = newReader.Name;

            _logger.LogInformation("Reader updated. Reader: {@Reader}", existingReader);
            await _servicecontexts.SaveChangesAsync();
        }
    }
}
