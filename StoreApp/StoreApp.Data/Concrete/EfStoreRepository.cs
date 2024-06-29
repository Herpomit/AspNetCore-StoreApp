using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete;

public class EfStoreRepository : IStoreRepository
{
    private readonly StoreDbContext _context;

    public EfStoreRepository(StoreDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> Products => _context.Products;

    public IQueryable<Category> Categories => _context.Categories;
    public void CreateProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
}
