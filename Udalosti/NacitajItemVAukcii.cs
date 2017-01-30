using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class NacitajItemVAukcii : Udalost
    {
        string Index;
        public NacitajItemVAukcii(TimeSpan cas, WebBrowser webBrowser, string index)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.NacitajItemVAukcii;
            Index = index;
            BlokujucaUdalost = BlokujucaUdalostEnum.Aukcia;
        }

        public override void Vykonaj()
        {
            var o = wb.Document.GetElementsByTagName("select").GetElementsByName("itemType")[0].Children;

            foreach (HtmlElement option in o)        // vyber typu itemy z aukcie
            {
                var value = option.GetAttribute("value").ToString();
                Console.WriteLine(value);

                // prstene - 6
                // amulety - 9
                //var index = comboBox1.SelectedIndex == 0 ? "9" : "6";
                var index = Index;

                if (value.Equals(index))
                    option.SetAttribute("selected", "selected");
            }

            var s = wb.Document.GetElementsByTagName("input");   // kliknutie na tlacitko FILTER v aukcii
            s[6].InvokeMember("Click");
        }
    }
}
