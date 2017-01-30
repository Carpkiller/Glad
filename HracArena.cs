using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad
{
    public class HracArena
    {
        public string MenoHraca { get; set; }
        public string Uroven { get; set; }
        public string Server { get; set; }
        public HtmlElement ButtonUtok { get; set; }

        public HracArena(string meno, string uroven, string server, HtmlElement htmlElem)
        {
            MenoHraca = meno;
            Uroven = uroven;
            Server = server;
            ButtonUtok = htmlElem;
        }

        public HracArena()
        {
            // TODO: Complete member initialization
        }
    }
}
