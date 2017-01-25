using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class NacitajLokaciu : Udalost
    {

        public NacitajLokaciu(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.NacitajLokaciu;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("submenu2").GetElementsByTagName("a");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                //if (item.OuterText == "Tábor Teutónov ")
                if (item.OuterText == "Baňa ")
                {
                    item.InvokeMember("Click");
                }
            }
        }
    }
}
