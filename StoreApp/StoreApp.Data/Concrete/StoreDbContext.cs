using Microsoft.EntityFrameworkCore;
namespace StoreApp.Data.Concrete;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasMany(x => x.Categories)
            .WithMany(x => x.Products)
            .UsingEntity<ProductCategory>();

        modelBuilder.Entity<Category>()
            .HasIndex(x => x.Url)
            .IsUnique();


        modelBuilder.Entity<Product>().HasData(
            new List<Product>()  {
                new() {Id = 1, Name = "Samsung S24", Price = 50000, Description = "Çok güzel Telefon"},
                new() {Id = 2, Name = "Samsung S25", Price = 60000,  Description = "Çok güzel Telefon"},
                new() {Id = 3, Name = "Samsung S26", Price = 70000,  Description = "Çok güzel Telefon"},
                new() {Id = 4, Name = "Samsung S27", Price = 80000,  Description = "Çok güzel Telefon"},
                new() {Id = 5, Name = "Samsung S28", Price = 90000,  Description = "Çok güzel Telefon"},
                new() {Id = 6, Name = "Samsung S29", Price = 100000, Description = "Çok güzel Telefon"},
                new() {Id = 7, Name = "Arçelik Çamaşır Makinesi", Price = 100000, Description = "Çok güzel Makinesi"},
            });

        modelBuilder.Entity<Category>().HasData(
            new List<Category>() {
                new() {Id = 1, Name = "Telefon", Url = "telefon"},
                new() {Id = 2, Name = "Elektronik", Url = "elektronik"},
                new() {Id = 3, Name = "Beyaz Eşya", Url = "beyaz-esya"}, // extansion method , slug dotnet 
            }
        );

        modelBuilder.Entity<ProductCategory>().HasData(
            new List<ProductCategory>() {
                new() {ProductId = 1, CategoryId = 1},
                new() {ProductId = 1, CategoryId = 2},
                new() {ProductId = 2, CategoryId = 1},
                new() {ProductId = 3, CategoryId = 1},
                new() {ProductId = 4, CategoryId = 1},
                new() {ProductId = 5, CategoryId = 2},
                new() {ProductId = 6, CategoryId = 2},
                new() {ProductId = 7, CategoryId = 3},
            }
        );
    }
}
