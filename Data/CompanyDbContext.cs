using Microsoft.EntityFrameworkCore;

using CompanyApp.Models.Entity;

namespace CompanyApp.Data;

public class CompanyDbContext : DbContext
{
    public CompanyDbContext()
    {
    }

    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDetail> ProductDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the one-to-many relationship
        modelBuilder.Entity<ProductDetail>()
            .HasOne(_ => _.Products)                   // Each productdetail has one product
            .WithMany(_ => _.ProductDetails)           // Each product has many productdetails
            .HasForeignKey(_ => _.ProductId)           // The foreign key is ProductId
            .OnDelete(DeleteBehavior.Cascade);         // Configure cascade delete
            
    }
}
