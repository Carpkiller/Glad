using System;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class RefreshStranky : Udalost
    {
        public RefreshStranky(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.Refresh;
            BlokujucaUdalost = BlokujucaUdalostEnum.Ziadna;
        }

        public override void Vykonaj()
        {
            wb.Refresh(WebBrowserRefreshOption.Completely);
        }
    }
}