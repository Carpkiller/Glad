using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class ZautocVArene : Udalost
    {
        private HracArena _super;

        public ZautocVArene(TimeSpan cas, WebBrowser webBrowser, HracArena super)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.ZautocVArene;
            _super = super;
        }

        public override void Vykonaj()
        {
            //bool najdeny = false;
            //var c = wb.Document.GetElementById("own2").GetElementsByTagName("tr");    // nacitacnie aukcnej budovy
            //foreach (HtmlElement itemRiadok in c)
            //{
            //    var bunky = itemRiadok.GetElementsByTagName("td");
            //    foreach (HtmlElement itembunka in bunky)
            //    {
            //        if (itembunka.OuterText.Trim() == _super.MenoHraca)
            //        {
            //            var e = bunky[3].GetElementsByTagName("span");
            //            e[0].InvokeMember("Click");
            //            return;
            //        }
            //    }
            //}
            _super.ButtonUtok.InvokeMember("Click");
        }
    }
}
