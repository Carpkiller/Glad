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
            BlokujucaUdalost = BlokujucaUdalostEnum.Expedicia;
        }

        public override void Vykonaj()
        {
            if (_lokacia == string.Empty)
            {
                var c = wb.Document.GetElementById("cooldown_bar_expedition").GetElementsByTagName("a");
                c[0].InvokeMember("Click");
            }
            else
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
}
