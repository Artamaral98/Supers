using Microsoft.EntityFrameworkCore;
using Supers.Domain.Entidades;

namespace Supers.Infrastructure.Dados
{
    public class SupersDbContext : DbContext
    {

        public SupersDbContext(DbContextOptions options) : base(options) { }
 
        public DbSet<HeroiSuperPoder> HeroisSuperPoderes { get; set; }

        public DbSet<SuperHeroi> SuperHerois { get; set; }

        public DbSet<SuperPoderes> SuperPoderes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SupersDbContext).Assembly);
        }
    }
}
