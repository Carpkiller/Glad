using System;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class NacitajInventar : Udalost
    {
        public NacitajInventar(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.NacitajInventar;
            BlokujucaUdalost = BlokujucaUdalostEnum.DoplnenieZivota;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("mainnav").GetElementsByTagName("a");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                if (item.OuterText == "Inventár")
                {
                    item.InvokeMember("Click");
                }
            }

            //wb.Refresh();
        }
    }
}