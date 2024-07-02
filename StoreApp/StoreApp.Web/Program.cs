using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b =>
    {
        b.MigrationsAssembly("StoreApp.Web");
    });
});

builder.Services.AddScoped<IStoreRepository, EfStoreRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// products/telefon => kategori ürün listesi
app.MapControllerRoute("products_in_category", "products/{category?}", new { controller = "Home", action = "Index" });

// ürün detay sayfası samsung-s24 => urun detay
app.MapControllerRoute("product_detail", "{name}", new { controller = "Home", action = "Details" });



app.MapDefaultControllerRoute();

app.Run();
