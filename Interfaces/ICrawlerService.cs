using NovaVida.Models;

namespace NovaVida.Interfaces;

public interface ICrawlerService
{
    public List<Products> Pesquisar(string txtPesquisar);
}

