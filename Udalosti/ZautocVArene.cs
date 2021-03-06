﻿using System;
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
            BlokujucaUdalost = BlokujucaUdalostEnum.Arena;
        }

        public override void Vykonaj()
        {
            _super.ButtonUtok.InvokeMember("Click");
        }
    }
}
