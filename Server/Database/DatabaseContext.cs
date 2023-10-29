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

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Marek", LastName = "Mostowiak", DateOfBirth = new DateTime(1972, 2, 3), GenderId = 1},
                new User { Id = 2, FirstName = "Hanna", LastName = "Mostowiak", DateOfBirth = new DateTime(1976, 11, 15), GenderId = 2},
                new User { Id = 3, FirstName = "Seba", LastName = "Sebastian", DateOfBirth = new DateTime(1991, 5, 29), GenderId = 1}
            );

            modelBuilder.Entity<CustomField>().HasData(
                new CustomField { Id = 1, UserId = 1, Name = "Robi mu się niedobrze na myśl o", Value = "kartonach"},
                new CustomField { Id = 2, UserId = 1, Name = "Numer telefonu", Value = "745215335"},
                new CustomField { Id = 3, UserId = 2, Name = "Ulubiona kawa", Value = "Flat White"},
                new CustomField { Id = 4, UserId = 2, Name = "Rok wydania prawa jazdy", Value = "1995"},
                new CustomField { Id = 5, UserId = 2, Name = "Rozmiar buta", Value = "40"},
                new CustomField { Id = 6, UserId = 3, Name = "Ksywa na dzielni", Value = "TypowySeba"},
                new CustomField { Id = 7, UserId = 3, Name = "Ulubiony fast food", Value = "Wrap z mcdonald"}
            );
        }

    }
}
