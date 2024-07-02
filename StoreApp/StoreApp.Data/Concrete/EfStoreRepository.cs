using Microsoft.EntityFrameworkCore;
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

    public int GetProductCount(string category)
    {
        return category == null
                ? Products.Count() : Products.Include(x => x.Categories).Where(x => x.Categories.Any(c => c.Url == category)).Count();
    }

    public IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize)
    {
        var products = Products;

        if (!string.IsNullOrEmpty(category))
        {
            products = products.Include(x => x.Categories).Where(x => x.Categories.Any(c => c.Url == category));
        }

        return products.Skip((page - 1) * pageSize).Take(pageSize);
    }
}
