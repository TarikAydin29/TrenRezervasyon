using Microsoft.EntityFrameworkCore;

namespace TrenRezervasyonAPI.Model
{
    public class RezervasyonDBContext : DbContext
    {
        public RezervasyonDBContext(DbContextOptions<RezervasyonDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tren>()
                .HasData(
                new Tren() { Id = 1, Name = "Başkent" },
                new Tren() { Id = 2, Name = "İzmir" }
                );
            modelBuilder.Entity<Vagon>()
                .HasData(
                new Vagon() { Id=1,Name="BaşkentV1",Kapasite=80,DoluKoltukAdet=35,TrenId=1},
                new Vagon() { Id=2,Name="BaşkentV2",Kapasite=100,DoluKoltukAdet=40,TrenId=1},
                new Vagon() { Id=3,Name="BaşkentV3",Kapasite=60,DoluKoltukAdet=55,TrenId=1},
                new Vagon() { Id=4,Name="İzmirV1",Kapasite=70,DoluKoltukAdet=30,TrenId=2},
                new Vagon() { Id=5,Name="İzmirV2",Kapasite=60,DoluKoltukAdet=25,TrenId=2},
                new Vagon() { Id=6,Name="İzmirV3",Kapasite=90,DoluKoltukAdet=50,TrenId=2}
                );
        }
        public DbSet<Tren> Tren { get; set; }
        public DbSet<Vagon> Vagonlar { get; set; }
        public DbSet<OturmaPlani> Oturmaplanlari { get; set; }
        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }
        public DbSet<RezervasyonCevabi> RezervasyonCevaplari { get; set; }
    }
}
