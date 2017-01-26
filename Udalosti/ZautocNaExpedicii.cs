using System;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class ZautocNaExpedicii : Udalost
    {
        private readonly string _monstrum;

        public ZautocNaExpedicii(TimeSpan cas, WebBrowser webBrowser, string monstrum)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.ZautocNaExpedicii;
            _monstrum = monstrum;
        }

        public override void Vykonaj()
        {
            bool najdeny = false;
            var c = wb.Document.GetElementById("expedition_list").GetElementsByTagName("div");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                //if (item.OuterText == "Praveký jašter ")
                if (item.OuterText != null && item.OuterText.Contains(_monstrum))
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
