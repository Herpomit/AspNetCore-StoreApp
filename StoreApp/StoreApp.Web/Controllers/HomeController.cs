using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;

public class HomeController : Controller
{
    public int pageSize = 3;
    private readonly IStoreRepository _storeRepository;

    public HomeController(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    //  localhost:5000/?page=1
    public IActionResult Index(int page = 1)
    {
        var products = _storeRepository
        .Products
        .Skip((page - 1) * pageSize)  // 1 -1 => 0*3 => 0 // 2 -1 => 1 * 3 => 3 // 3 -1 => 2 * 3 => 6
        .Select(x =>
        new ProductViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
        }).Take(pageSize);
        return View(new ProductListViewModel
        {
            Products = products,
            PageInfo = new PageInfo
            {
                TotalItems = _storeRepository.Products.Count(),
                ItemsPerPage = pageSize,
                CurrentPage = page
            }
        });
    }
}
