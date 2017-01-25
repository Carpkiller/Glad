using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glad
{
    public class Ponuka
    {
        
        public int NajnizsiaPonuka { get; set; }
        public int Cena { get; set; }
        public bool Volny { get; set; }

        public string NazovItemu { get; set; }

        public Ponuka()
        {

        }

        public Ponuka(string p, string najnizsiaPonuka, string cena, bool volny)
        {
            // TODO: Complete member initialization
            NazovItemu = p;
            NajnizsiaPonuka = int.Parse(najnizsiaPonuka);
            Cena = int.Parse(cena);
            Volny = volny;
        }
    }
}
