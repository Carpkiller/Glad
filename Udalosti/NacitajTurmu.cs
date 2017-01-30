using System;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class NacitajTurmu : Udalost 
    {
        public NacitajTurmu(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.NacitajTurmu;
            BlokujucaUdalost = BlokujucaUdalostEnum.Turma;
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
