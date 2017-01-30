using System;
using System.Windows.Forms;

namespace Glad
{
    public abstract class Udalost
    {
        public TimeSpan CasSimulacie { get; set; }
        protected WebBrowser wb;
        public TypAktivityEnum TypAktivity { get; set; }
        public BlokujucaUdalostEnum BlokujucaUdalost { get; set; }

        public abstract void Vykonaj();

        public override string ToString()
        {
            return TypAktivity.ToString();
        }
    }
}
