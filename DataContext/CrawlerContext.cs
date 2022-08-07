using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using NovaVida.Models;

namespace NovaVida.DataContext;

public class CrawlerContext : DbContext
{    
    public CrawlerContext(DbContextOptions<CrawlerContext> options)
        : base(options)
    {
    }

    public DbSet<Products> Products { get; set; }
    public DbSet<ProductReviews> ProductReviews { get; set; }
}
