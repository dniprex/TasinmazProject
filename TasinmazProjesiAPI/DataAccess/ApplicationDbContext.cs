using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Il> iller { get; set; }
        public DbSet<Ilce> ilceler { get; set; }
        public DbSet<Mahalle> mahalleler { get; set; }
        public DbSet<Tasinmaz> tasinmazlar { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasinmaz>().ToTable("tasinmazlar");
            modelBuilder.Entity<Il>().ToTable("iller");
            modelBuilder.Entity<Ilce>().ToTable("ilceler");
            modelBuilder.Entity<Mahalle>().ToTable("mahalleler");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Log>().ToTable("Logs");
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Database=TasinmazProjesi;Username=postgres;Password=4434");
        //}
    }
}