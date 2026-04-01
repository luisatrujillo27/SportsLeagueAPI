using Microsoft.EntityFrameworkCore;
using SportsLeague.Domain.Entities;

namespace SportsLeague.DataAccess.Context
{
    public class LeagueDbContext : DbContext
    {
        public LeagueDbContext(DbContextOptions<LeagueDbContext> options)
            : base(options)
        {
        }

        // 🔥 TABLAS
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Sponsor> Sponsors => Set<Sponsor>();
        public DbSet<Tournament> Tournaments => Set<Tournament>();
        public DbSet<TournamentSponsor> TournamentSponsors => Set<TournamentSponsor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 TEAM
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(t => t.City)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(t => t.Stadium)
                      .HasMaxLength(150);
                entity.Property(t => t.LogoUrl)
                      .HasMaxLength(500);
                entity.Property(t => t.CreatedAt)
                      .IsRequired();
                entity.Property(t => t.UpdatedAt)
                      .IsRequired(false);
                entity.HasIndex(t => t.Name)
                      .IsUnique();
            });

            // 🔹 SPONSOR
            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(s => s.ContactEmail)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(s => s.Phone)
                      .HasMaxLength(50);
                entity.Property(s => s.WebsiteUrl)
                      .HasMaxLength(250);
            });

            // 🔹 TOURNAMENT
            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name)
                      .IsRequired()
                      .HasMaxLength(150);
            });

            // 🔥 RELACIÓN N:M (LO MÁS IMPORTANTE)
            modelBuilder.Entity<TournamentSponsor>(entity =>
            {
                entity.HasKey(ts => ts.Id);

                entity.HasOne(ts => ts.Tournament)
                      .WithMany()
                      .HasForeignKey(ts => ts.TournamentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ts => ts.Sponsor)
                      .WithMany()
                      .HasForeignKey(ts => ts.SponsorId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(ts => ts.ContractAmount)
                      .IsRequired();

                entity.Property(ts => ts.JoinedAt)
                      .IsRequired();
            });
        }
    }
}