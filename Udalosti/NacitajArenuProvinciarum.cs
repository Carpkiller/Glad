using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class NacitajArenuProvinciarum : Udalost
    {
        public NacitajArenuProvinciarum(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.NacitajArenuProvinciarum;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("mainnav").GetElementsByTagName("a");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                if (item.OuterText == "Aréna Provinciarum")
                {
                    item.InvokeMember("Click");
                }
            }
        }
    }
}
