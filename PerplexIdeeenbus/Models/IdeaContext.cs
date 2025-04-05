using Microsoft.EntityFrameworkCore;

namespace PerplexIdeeenbus.Models
{
    public class IdeaContext : DbContext
    {
        public IdeaContext(DbContextOptions<IdeaContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Idea>().HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Idea> Ideas { get; set; } = null!;
    }
}
