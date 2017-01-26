using System;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class NacitajLokaciu : Udalost
    {
        private readonly String _lokacia;

        public NacitajLokaciu(TimeSpan cas, WebBrowser webBrowser, string lokacia)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.NacitajLokaciu;
            _lokacia = lokacia;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("submenu2").GetElementsByTagName("a");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                if (item.OuterText != null && item.OuterText.Contains(_lokacia))
                {
                    item.InvokeMember("Click");
                }
            }
        }
    }
}
