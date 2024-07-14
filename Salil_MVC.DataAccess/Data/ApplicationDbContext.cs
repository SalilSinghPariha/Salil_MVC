using Microsoft.EntityFrameworkCore;
using Salil_MVC.Models;

namespace Salil_MVC.DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1,Name="Action",DisplayOrder=1},
                 new Category { Id = 2, Name = "SciFi", DisplayOrder = 2},
                  new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );

        }

        public DbSet<Category> categories { get; set; }
    }
}
