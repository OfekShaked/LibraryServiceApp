using LibraryRenewal.DAL.Interfaces;
using LibraryRenewal.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryRenewal.DAL
{
    public partial class LibraryDbContext : DbContext, ILibraryDbContext
    {
        public LibraryDbContext()
        {
        }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AbstractItem> AbstractItems { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public void DetachEntity<T>(T entity)
        {
            Entry(entity).State = EntityState.Detached;
        }

        public Task<int> SaveChangesAsyncInherited()
        {
            return SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=C:\\Users\\ofeks\\AppData\\Local\\Packages\\dd5422e6-8678-4ba5-a971-ebc6a287926d_mx5k543xky706\\LocalState\\LibraryDb.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbstractItem>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.HasIndex(e => e.Isbn)
                    .IsUnique();

                entity.Property(e => e.ItemId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ItemID");

                entity.Property(e => e.Edition).HasColumnType("NVARCHAR(255)");

                entity.Property(e => e.IdofGenre).HasColumnName("IDOfGenre");

                entity.Property(e => e.Isbn)
                    .HasColumnType("NVARCHAR(255)")
                    .HasColumnName("ISBN");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(255)");

                entity.Property(e => e.PrintDate)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(255)");

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(255)");

                entity.Property(e => e.Subject).HasColumnType("NVARCHAR(255)");

                entity.Property(e => e.Summary).HasColumnType("NVARCHAR(255)");

                entity.Property(e => e.Writer)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(255)");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(e => e.GenreName)
                    .IsUnique();

                entity.Property(e => e.GenreId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("GenreID");

                entity.Property(e => e.GenreName)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(255)");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.SaleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SaleID");

                entity.Property(e => e.ItemSoldId).HasColumnName("ItemSoldID");

                entity.Property(e => e.SaleDate)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(255)");

                entity.Property(e => e.SalesManUsername)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(255)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UserID");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(255)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(255)");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(10)");

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(20)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(255)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
    }
}

