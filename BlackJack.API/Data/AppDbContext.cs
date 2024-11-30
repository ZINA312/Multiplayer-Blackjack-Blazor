using Microsoft.EntityFrameworkCore;
using BlackJack.Domain.Entities;

namespace BlackJack.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<GameSession> game { get; set; }
        public DbSet<Player> player { get; set; }
        public DbSet<Deck> deck { get; set; }
        public DbSet<Dealer> dealer { get; set; }
        public DbSet<Card> card { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameSession>()
                .HasKey(gs => gs.GameId);

            modelBuilder.Entity<GameSession>()
                .HasMany(gs => gs.Players) 
                .WithOne(p => p.GameSession) 
                .HasForeignKey(p => p.GameId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameSession>()
                .HasOne<Deck>()
                .WithOne(d => d.GameSession)
                .HasForeignKey<Deck>(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameSession>()
                .HasOne<Dealer>()
                .WithOne(d => d.GameSession)
                .HasForeignKey<Dealer>(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Deck>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Dealer>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Card>()
                .HasKey(c => new { c.Suit, c.Value });

            base.OnModelCreating(modelBuilder);
        }
    }
}
