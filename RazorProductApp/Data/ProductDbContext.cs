using Microsoft.EntityFrameworkCore;
using RazorProductApp.Models;

namespace RazorProductApp.Data
{
    public class ProductDbContext : DbContext
    {
        //Create a new DbContext class
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {


        }
        //Create a new DbSet property
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
        }

    }
}

//Give me all the steps in this application with any code to add the pagination feature to the Index page of the RazorProductApp application.
// Path: RazorProductApp/Pages/Index.cshtml.cs
// Steps :
// 1. Add a new property to the IndexModel class
// 2. Add a new method to the IndexModel class
// 3. Update the OnGetAsync method
// 4. Update the Index.cshtml file
// 5. Update the Index.cshtml.cs file
// 6. Update the Index.cshtml file
// 7. Update the Index.cshtml.cs file
