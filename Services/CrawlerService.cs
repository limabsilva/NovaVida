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
        Products products = new Products()
        {
            NameProduct = "Headset Gamer Redragon Zeus X, Chroma Mk.II, RGB, Surround 7.1, USB, Drivers 53MM, Preto/Vermelho - H510-RGB",
            IdProduct = 1,
            PriceProduct = (decimal)299.90,
            URLProduct = "https://www.kabum.com.br/produto/227818/headset-gamer-redragon-zeus-x-chroma-mk-ii-rgb-surround-7-1-usb-drivers-53mm-preto-vermelho-h510-rgb"
        };
        productsList.Add(products);

        Tracker(strSearch);

        return productsList;
    }

    public List<Products> Tracker(string strSearch)
    {

        Stream streamRet;
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
            var regex = new Regex(patternMain, RegexOptions.IgnoreCase);
            Match m = regex.Match(container.InnerHtml);
            //var productCard = regex.Matches(container.InnerHtml).OfType<Match>().FirstOrDefault().Groups[0];
            if (m.Success)
            {
                var productCardM = m.Groups[0];
                var nodes = container.SelectNodes(productCardM.ToString().Replace("<div", "//div [@").Replace(">", "]"));
                foreach (var node in nodes)
                {
                    var navigator = (HtmlAgilityPack.HtmlNodeNavigator)node.CreateNavigator();
                    //GetNameProduct
                    var regexName = new Regex(patternName, RegexOptions.IgnoreCase);
                    Match mName = regexName.Match(node.InnerHtml);
                    var nameProduct = node.SelectSingleNode(mName.Groups[0].ToString().Replace("<span height=\"54\"", "//span [@").Replace(">", "]")).InnerText;



                    //GetPriceProduct                    
                    var regexPrice = new Regex(patternPrice, RegexOptions.IgnoreCase);
                    Match mPrice = regexPrice.Match(node.InnerHtml);
                    var priceProduct = Convert.ToDecimal(node.SelectSingleNode(mPrice.Groups[0].ToString().Replace("<span", "//span [@").Replace(">", "]")).InnerText.Replace("R$","").Trim());



                    //GetURLProduct
                    var regexURL = new Regex(patternURL, RegexOptions.IgnoreCase);
                    Match mUrl = regexURL.Match(node.InnerHtml);
                    var urlProduct = mUrl.Groups[0];
                }
            }


        }

        return productsList;
    }

}