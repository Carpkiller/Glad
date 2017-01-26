using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class AktivujElixir : Udalost
    {
        public AktivujElixir(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.AktivujElixir;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("content").GetElementsByTagName("input");
            c[2].InvokeMember("Click");
        }
    }
}