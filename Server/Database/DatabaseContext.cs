using Microsoft.EntityFrameworkCore;
using UserSpying.Shared.Models;

namespace UserSpying.Server.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CustomField> CustomFields { get; set; }
        public DbSet<Gender> Genders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Database\\users-spying.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                entity.Property(u => u.GenderId)
                    .IsRequired();

                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("varchar(150)");

                entity.Property(u => u.DateOfBirth)
                    .IsRequired()
                    .HasColumnType("date");

                entity.HasOne(u => u.Gender)
                    .WithMany(g => g.Users)
                    .HasForeignKey(u => u.GenderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CustomField>(entity =>
            {
                entity.HasKey(cf => cf.Id);

                entity.Property(cf => cf.UserId)
                    .IsRequired();

                entity.Property(cf => cf.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("varchar(20)");

                entity.Property(cf => cf.Value)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");

                entity.HasOne<User>()
                    .WithMany(u => u.CustomFields)
                    .HasForeignKey(cf => cf.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(cf => new { cf.UserId, cf.Name }).IsUnique();
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(g => g.Id);

                entity.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("varchar(20)");

                entity.Property(g => g.Abbreviation)
                    .IsRequired()
                    .HasColumnType("char(1)");

                entity.Property(g => g.Honorific)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id = 1, Name = "Mężczyzna", Abbreviation = 'M', Honorific = "Pan" },
                new Gender { Id = 2, Name = "Kobieta", Abbreviation = 'K', Honorific = "Pani" }
            );
        }

    }
}
