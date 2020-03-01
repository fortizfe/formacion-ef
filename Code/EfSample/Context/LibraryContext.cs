using EfSample.Models;
using Microsoft.EntityFrameworkCore;

namespace EfSample.Context
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Library;Integrated Security=true;");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).IsRequired().HasMaxLength(200);
                b.Property(p => p.LastName).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Title).IsRequired().HasMaxLength(200);
                b.Property(p => p.Isbn).IsRequired().HasMaxLength(200);
                b.Property(p => p.Bookcase).IsRequired().HasMaxLength(10);
                b.HasOne(p => p.Author).WithMany(a => a.Books).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}