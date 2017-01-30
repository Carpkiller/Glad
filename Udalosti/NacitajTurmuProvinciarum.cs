using System;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class NacitajTurmuProvinciarum : Udalost
    {
        public NacitajTurmuProvinciarum(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.NacitajTurmuProvinciarum;
            BlokujucaUdalost = BlokujucaUdalostEnum.Turma;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("mainnav").GetElementsByTagName("a");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                if (item.OuterText == "Circus Provinciarum")
                {
                    item.InvokeMember("Click");
                }
            }
        }
    }
}
