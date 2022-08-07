using NovaVida.Models;
using NovaVida.Interfaces;
namespace NovaVida.Services;

public class CrawlerService : ICrawlerService
{

    public List<Products> Pesquisar(string txtPesquisar)
    {

        List<Products> productsList = new List<Products>();
        Products products = new Products()
        {
            NameProduct = "Headset Gamer Redragon Zeus X, Chroma Mk.II, RGB, Surround 7.1, USB, Drivers 53MM, Preto/Vermelho - H510-RGB",
            IdProduct = 1,
            PriceProduct = (decimal)299.90,
            URLProduct = "https://www.kabum.com.br/produto/227818/headset-gamer-redragon-zeus-x-chroma-mk-ii-rgb-surround-7-1-usb-drivers-53mm-preto-vermelho-h510-rgb"
        };
        productsList.Add(products);  
        

        return productsList;
    }

}