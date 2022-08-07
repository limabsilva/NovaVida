using NovaVida.Interfaces;
using NovaVida.DataContext;
using NovaVida.Models;

namespace NovaVida.Repositories;
public class ProductRepository : IProductsRepository
{
    private readonly CrawlerContext _crawlerContext ;
    public ProductRepository(CrawlerContext crawlerContext)
    {
        _crawlerContext = crawlerContext;
    }

    public long SaveProduct(Products products)
    {
        _crawlerContext.Products.Add(products);
        return _crawlerContext.SaveChanges();
    }
}
