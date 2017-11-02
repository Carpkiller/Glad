using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class ZautocVZalari : Udalost
    {
        public ZautocVZalari(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.ZautocVZalari;
            BlokujucaUdalost = BlokujucaUdalostEnum.Zalar;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("content").GetElementsByTagName("div");

            foreach (HtmlElement item in c)
            {
                string value = item.GetAttribute("class").ToString();

                if (item.InnerText.StartsWith("Vstúp do žalára"))
                {
                    var q = item.GetElementsByTagName("input");

                    q[0].InvokeMember("Click");                    
                }
            }  


            c = wb.Document.GetElementById("content").GetElementsByTagName("img");    // onclick

            foreach (HtmlElement item in c)
            {
                string value = item.GetAttribute("onclick").ToString();

                if (value != null && value.Length > 5)
                {
                    item.InvokeMember("Click");
                    return;
                }
            }            
        }
    }
}
