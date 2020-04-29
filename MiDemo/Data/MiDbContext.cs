namespace MiDemo.Data
{
    using Microsoft.EntityFrameworkCore;
    using MiDemo.Model;

    public class MiDbContext : DbContext
    {
        public MiDbContext(DbContextOptions<MiDbContext> options)
            : base(options)
        {
        }

        public DbSet<SampleTable> SampleTable { get; set; }
    }
}