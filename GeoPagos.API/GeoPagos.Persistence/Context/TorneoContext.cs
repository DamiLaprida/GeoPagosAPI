using GeoPagos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable
namespace GeoPagos.Persistence.Context
{
    public class TorneoContext : DbContext
    {
        public TorneoContext()
        {
        }

        public TorneoContext(DbContextOptions<TorneoContext> options)
            : base(options)
        {
        }

        public DbSet<Torneo> Torneos { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<Ronda> Rondas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("TorneoDb");
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Partido>();

            modelBuilder.Entity<Jugador>()
              .HasOne(j => j.TorneoNavigation)
              .WithMany(t => t.Jugadores)
              .HasForeignKey(j => j.IdTorneo);
        }
    }
}
