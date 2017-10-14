using System;
using System.Collections.Generic;
using System.IO;

namespace Glad
{
    public static class Logovanie
    {
        private const string FileName = @"Data\\Logy.txt";

        public static void LogujText(string text)
        {
            using (StreamWriter file =
            new StreamWriter(FileName, true))
            {
                file.WriteLine(text);
            }
        }

        public static void ZalogujDbResult(List<Zaznam> db)
        {
            LogujText("=== Vystup z DB ====");
            foreach (var zaznam in db)
            {
                LogujText(string.Format("{0} - {1} , {2} - {3} - {4}", zaznam.Protivnik, zaznam.Premia, zaznam.Datum,
                    zaznam.Datum.Date == DateTime.Today, zaznam.Vitaz));
            }
        }

        internal static void ZalogujProtivnikov(List<HracArena> protivnici)
        {
            LogujText("=== Protivnici ====");
            foreach (var zaznam in protivnici)
            {
                LogujText(string.Format("{0} - {1}", zaznam.MenoHraca, zaznam.Uroven));
            }
        }

        public static void ZalogujSupera(HracArena super)
        {
            LogujText(string.Format("Vybraty protivnik(najblizsi level) => {0} - {1}", super.MenoHraca, super.Uroven));
        }
    }
}
