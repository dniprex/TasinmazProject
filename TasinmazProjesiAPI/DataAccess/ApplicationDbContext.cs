using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Il> iller { get; set; }
        public DbSet<Ilce> ilceler { get; set; }
        public DbSet<Mahalle> mahalleler { get; set; }
        public DbSet<Tasinmaz> tasinmazlar { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Database=TasinmazProjesi;Username=postgres;Password=4434");
        //}
    }
}