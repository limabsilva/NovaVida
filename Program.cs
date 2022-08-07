using NovaVida.DataContext;
using NovaVida.Interfaces;
using NovaVida.Services;
using NovaVida.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI EntityFramework
builder.Services.AddDbContext<CrawlerContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<CrawlerContext, CrawlerContext>();
//DI EntityFramework

//DI Repository
builder.Services.AddScoped<IProductsRepository, ProductRepository>();
builder.Services.AddScoped<IProductReviewsRepository, ProductReviewsRepository>();
//DI Repository

//DI Services
builder.Services.AddScoped<ICrawlerService, CrawlerService>();
//DI Services

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
