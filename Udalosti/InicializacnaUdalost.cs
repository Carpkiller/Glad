﻿using System;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class InicializacnaUdalost : Udalost
    {
        public InicializacnaUdalost(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.InicializacnaUdalost;
            BlokujucaUdalost = BlokujucaUdalostEnum.Ziadna;
        }

        public override void Vykonaj()
        {
            var c = wb.Document.GetElementById("mainmenu").GetElementsByTagName("a");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                if (item.OuterText == "Prehľad")
                {
                    item.InvokeMember("Click");
                }
            }
        }
    }
}
