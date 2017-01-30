using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class NacitajArenu : Udalost
    {
        public NacitajArenu(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.NacitajArenu;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("submenu1").GetElementsByTagName("a");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                if (item.OuterText == "Aréna")
                {
                    item.InvokeMember("Click");
                }
            }
        }
    }
}
