using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glad
{
    public class HodnotenieHracov
    {
        public string MenoHraca { get; set; }
        public int Hodnotenie { get; set; }

        public HodnotenieHracov(string meno)
        {
            MenoHraca = meno;
            Hodnotenie = 0;
        }
    }
}
