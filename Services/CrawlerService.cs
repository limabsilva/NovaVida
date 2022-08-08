using NovaVida.Models;
using NovaVida.Interfaces;
using HtmlAgilityPack;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace NovaVida.Services;

public class CrawlerService : ICrawlerService
{
    private readonly string _crawlerUrl = "https://www.kabum.com.br/busca/";

    public List<Products> SearchProduct(string strSearch)
    {

        List<Products> productsList = new List<Products>();
        //Products products = new Products()
        //{
        //    NameProduct = "Headset Gamer Redragon Zeus X, Chroma Mk.II, RGB, Surround 7.1, USB, Drivers 53MM, Preto/Vermelho - H510-RGB",
        //    IdProduct = 1,
        //    PriceProduct = "",
        //    URLProduct = "https://www.kabum.com.br/produto/227818/headset-gamer-redragon-zeus-x-chroma-mk-ii-rgb-surround-7-1-usb-drivers-53mm-preto-vermelho-h510-rgb"
        //};
        //productsList.Add(products);

        productsList = Tracker(strSearch);

        return productsList;
    }

    public List<Products> Tracker(string strSearch)
    {

        List<Products> productsList = new List<Products>();
        string strURI = _crawlerUrl + strSearch;


        using (var client = new HttpClient())
        {
            Uri myUri = new Uri(strURI);
            var request = new HttpRequestMessage(HttpMethod.Get, myUri);
            //request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("authority", "www.kabum.com.br");
            request.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.Headers.Add("accept-language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
            request.Headers.Add("referer", "https://www.kabum.com.br/");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36");


            client.Timeout = System.TimeSpan.FromSeconds(60);

            HttpResponseMessage? response = client.SendAsync(request).Result;

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response.Content.ReadAsStringAsync().Result);
            var patternMain = @"(<div class=""[^""]*productCard"">)";
            var patternName = @"(<span height=""54"" class=""[^""]*nameCard"">)";
            var patternPrice = @"(<span class=""[^""]* priceCard"">)";
            var patternURL = @"(<a [^>]*href=(?:'(?<href>.*?)')|(?:\""(?<href>.*?)\""))";

            var container = htmlDoc.DocumentNode.SelectSingleNode("//main[@id='listing']");
            if (container != null)
            {
                var regex = new Regex(patternMain, RegexOptions.IgnoreCase);
                Match m = regex.Match(container.InnerHtml);
                if (m.Success)
                {
                    var productCardM = m.Groups[0];
                    var nodes = container.SelectNodes(productCardM.ToString().Replace("<div", "//div [@").Replace(">", "]"));
                    string nameProduct, priceProduct, urlProduct;

                    foreach (var node in nodes)
                    {
                        var navigator = (HtmlAgilityPack.HtmlNodeNavigator)node.CreateNavigator();

                        //GetNameProduct
                        //var regexName = new Regex(patternName, RegexOptions.IgnoreCase);
                        //Match mName = regexName.Match(node.InnerHtml);
                        //nameProduct = node.SelectSingleNode(mName.Groups[0].ToString().Replace("<span height=\"54\"", "//span [@").Replace(">", "]")).InnerText;
                        nameProduct = node.InnerText;


                        //GetPriceProduct                    
                        //var regexPrice = new Regex(patternPrice, RegexOptions.IgnoreCase);
                        //Match mPrice = regexPrice.Match(node.InnerHtml);
                        //priceProduct = node.SelectSingleNode(mPrice.Groups[0].ToString().Replace("<span", "//span [@").Replace(">", "]")).InnerText;
                        priceProduct = "";


                        //GetURLProduct
                        //var regexURL = new Regex(patternURL, RegexOptions.IgnoreCase);
                        //Match mUrl = regexURL.Match(node.InnerHtml);
                        //urlProduct = mUrl.Groups[0].ToString();
                        urlProduct = "";


                        productsList.Add(
                            new Products()
                            {
                                NameProduct = nameProduct,
                                PriceProduct = priceProduct,
                                URLProduct = urlProduct
                            });

                    }
                }
            }
            else
            {
                productsList.Add(
                    new Products()
                    {
                        NameProduct = "Lista de produtos não disponível.",
                        PriceProduct = null,
                        URLProduct = null
                    });
            }
        }

        return productsList;
    }

}