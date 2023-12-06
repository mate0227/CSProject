using konyvtar.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Konyvtar.Contexts
{
    public class ServiceContexts : DbContext
    {
        public ServiceContexts(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Reader>Readers { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
