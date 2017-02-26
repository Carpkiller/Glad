using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class NacitajZalar : Udalost
    {
        public NacitajZalar(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.NacitajZalar;
            BlokujucaUdalost = BlokujucaUdalostEnum.Zalar;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("cooldown_bar_dungeon").GetElementsByTagName("a");
            c[0].InvokeMember("Click");
        }
    }
}
