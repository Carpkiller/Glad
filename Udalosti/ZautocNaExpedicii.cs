using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class ZautocNaExpedicii : Udalost
    {
        public ZautocNaExpedicii(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.ZautocNaExpedicii;
        }

        public override void Vykonaj()
        {
            bool najdeny = false;
            var c = wb.Document.GetElementById("expedition_list").GetElementsByTagName("div");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                if (item.OuterText == "Praveký jašter ")
                {
                    najdeny = true;
                }
                if (najdeny && item.OuterText == "Zaútočiť ")
                {
                    var e = item.GetElementsByTagName("button");
                    e[0].InvokeMember("Click");
                    return;
                }
            }
        }
    }
}
