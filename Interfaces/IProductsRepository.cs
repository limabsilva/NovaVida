using NovaVida.Models;

namespace NovaVida.Interfaces;
public interface IProductsRepository
{
    public long SaveProduct(Products products);
}

