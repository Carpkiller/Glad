using System;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class ZautocVTurme : Udalost
    {
        private HracArena _super;

        public ZautocVTurme(TimeSpan cas, WebBrowser webBrowser, HracArena super)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.ZautocVTurme;
            _super = super;
            BlokujucaUdalost = BlokujucaUdalostEnum.Turma;
        }

        public override void Vykonaj()
        {
            _super.ButtonUtok.InvokeMember("Click");
        }
    }
}
