using Microsoft.EntityFrameworkCore;

namespace Example.Data
{
    public class ExampleContext: DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options) : base(options)
        {
        }
        public DbSet<Example.Models.Examples> Examples { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Example.db");
            }
        }
    }
}
