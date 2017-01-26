using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class NacitajPremium : Udalost
    {
        public NacitajPremium(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.NacitajPremium;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("mainmenu").GetElementsByTagName("a");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                if (item.OuterText == "Prémium")
                {
                    item.InvokeMember("Click");
                }
            }
        }
    }
}