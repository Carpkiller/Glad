using Glad.Databaza;
using Glad.Udalosti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad
{
    public class Jadro
    {
        public SortedList<TimeSpan, Udalost> KalendarUdalosti { get; set; }
        public bool Naplanova { get; set; }
        public int Zlato { get; set; }
        public string Hrac { get; set; }
        public int ExpBody { get; set; }
        public string Zivoty { get; set; }
        public string CasAreny { get; set; }
        public string CasTurmy { get; set; }
        public string CasExpedicie { get; set; }
        public string CasZalaru { get; set; }

        public string Monstrum { get; set; }
        public HracArena protivnik { get; set; }
        public string Lokacia { get; set; }

        private readonly WebBrowser wb;
        public TimeSpan SimCas;
        public bool SimulaciaBezi;

        public delegate void ZmenaKalendarUdalostiHandler();
        public event ZmenaKalendarUdalostiHandler ZmenaKalendarUdalosti;

        public delegate void ZmenaSimCasuHandler();
        public event ZmenaSimCasuHandler ZmenaSimCasu;


        public List<Ponuka> listPonuk;
        public List<System.Windows.Forms.HtmlElement> listElementov;

        public bool UkladajZlato { get; set; }
        public bool JePonuknute { get; set; }

        public bool KlikajExpedicie { get; set; }
        public bool KlikajArenu { get; set; }
        public bool KlikajTurmu { get; set; }
        public bool KlikajZalar { get; set; }
        public bool JeKostym { get; set; }
        public bool JeModPrieskum { get; set; }
        public bool JeModZarabanie { get; set; }

        public Jadro(WebBrowser wb)
        {
            listPonuk = new List<Ponuka>();
            listElementov = new List<System.Windows.Forms.HtmlElement>();
            KalendarUdalosti = new SortedList<TimeSpan, Udalost>();
            this.wb = wb;
            SimulaciaBezi = false;
        }

        internal void ParsujItemy(System.Windows.Forms.HtmlElementCollection inputHtml)
        {
            listPonuk = new List<Ponuka>();
            listElementov = new List<System.Windows.Forms.HtmlElement>();

            foreach (System.Windows.Forms.HtmlElement row in inputHtml)
            {
                var i = row.GetElementsByTagName("td");
                foreach (System.Windows.Forms.HtmlElement item in i)
                {
                    var tt = item.GetElementsByTagName("input");
                    var divs = item.GetElementsByTagName("div");
                    var y = item.OuterText.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    if (y.Length > 1)
                    {
                        var najnizsiaPonuka = y[10].Split(':')[1].Trim().Replace(".", "");
                        var cena = y[12].Split(' ')[1].Replace(".", "");

                        listPonuk.Add(new Ponuka("", najnizsiaPonuka, cena, divs[7].GetElementsByTagName("a").Count == 0 || divs[7].GetElementsByTagName("span").Count == 0));
                        listElementov.Add(tt[7]);
                    }
                }
            }
        }

        public void SpustSimulaciu(string[] lokacia)
        {
            SimCas = new TimeSpan(0, 0, 0, 0, 0);
            KalendarUdalosti = new SortedList<TimeSpan, Udalost>();
            Naplanova = true;
            Lokacia = lokacia[0].Trim();
            Monstrum = lokacia[1].Trim();

            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);

            KalendarUdalosti.Add(simCasUdalosti, new InicializacnaUdalost(simCasUdalosti, wb));

            SpustBehSimulacie();

            if (ZmenaKalendarUdalosti != null)
                ZmenaKalendarUdalosti();
        }

        private async void SpustBehSimulacie()
        {
            SimulaciaBezi = true;

            while (SimulaciaBezi)
            {
                Console.WriteLine(SimCas + "  --  " + KalendarUdalosti.Values.Count);
                if (SimCas.Equals(KalendarUdalosti.First().Value.CasSimulacie))
                {
                    KalendarUdalosti.First().Value.Vykonaj();
                    Naplanova = true;
                }

                SimCas = SimCas.Add(new TimeSpan(0, 0, 0, 0, 100));
                await PutTaskDelay();

                if (ZmenaSimCasu != null)
                    ZmenaSimCasu();

                if (KalendarUdalosti.Count == 0)
                {
                    SimulaciaBezi = false;
                }
            }

            SimulaciaBezi = false;
        }

        async Task PutTaskDelay()
        {
            await Task.Delay(100);
        }

        public void NaplanujDalsiuAktivitu()
        {
            var predchAktivita = KalendarUdalosti.First().Value.TypAktivity;
            KalendarUdalosti.RemoveAt(0);

            switch (predchAktivita)
            {
                case TypAktivityEnum.InicializacnaUdalost:
                    NasledujucaPoInicializacnaUdalost();
                    break;
                case TypAktivityEnum.NacitajLokaciu:
                    NasledujucaPoNacitajLokaciu();
                    break;
                case TypAktivityEnum.ZautocNaExpedicii:
                    NasledujucaPoZautocNaExpedicii();
                    break;
                case TypAktivityEnum.NacitajAukcnuBudovu:
                    NasledujucaPoNacitajAukcnuBudovu();
                    break;
                case TypAktivityEnum.NacitajItemVAukcii:
                    NasledujucaPoNacitajItemVAukcii();
                    break;
                case TypAktivityEnum.PonukniNaAukcii:
                    NasledujucaPoPonukniNaAukcii();
                    break;
                case TypAktivityEnum.NacitajPremium:
                    NasledujucaPoNacitajPremium();
                    break;
                case TypAktivityEnum.NacitajInventar:
                    NasledujucaPoNacitajInventar();
                    break;
                case TypAktivityEnum.NacitajArenu:
                    NasledujucaPoNacitajArenu();
                    break;
                case TypAktivityEnum.NacitajArenuProvinciarum:
                    NasledujucaPoNacitajArenuProvinciarum();
                    break;
                case TypAktivityEnum.ZautocVArene:
                    NasledujucaPoZautocVArene();
                    break;
            }
            Naplanova = false;

            if (ZmenaKalendarUdalosti != null) //vyvolani udalosti
                ZmenaKalendarUdalosti();
        }

        private void NasledujucaPoInicializacnaUdalost()
        {
            var profil = wb.Document.GetElementById("content").GetElementsByTagName("div");
            foreach (HtmlElement item in profil)
            {
                if (item.GetAttribute("className") == "player_name_bg pngfix")
                {
                    Hrac = item.OuterText.Trim();
                }
            }
            TimeSpan simCasUdalosti;

            if (KlikajExpedicie)
            {
                simCasUdalosti = SimCas + CasDostupnostiExpedicie();
                KalendarUdalosti.Add(simCasUdalosti, new NacitajLokaciu(simCasUdalosti, wb, Lokacia));
            }
            if (KlikajArenu)
            {
                simCasUdalosti = SimCas + CasDostupnostiAreny();
                KalendarUdalosti.Add(simCasUdalosti, new NacitajArenu(simCasUdalosti, wb));
            }


        }

        private void NasledujucaPoZautocVArene()
        {
            var simCasUdalosti = SimCas + CasDostupnostiAreny();
            KalendarUdalosti.Add(simCasUdalosti, new NacitajArenu(simCasUdalosti, wb));

            UlozStatistiku();
        }

        private TimeSpan CasDostupnostiAreny()
        {
            if (CasAreny == "Do arény")
            {
                CasAreny = "0:0:0";
            }
            var casyDostupnosti = CasAreny.Split(':');
            return new TimeSpan(int.Parse(casyDostupnosti[0]), int.Parse(casyDostupnosti[1]), int.Parse(casyDostupnosti[2])+4);
        }

        private TimeSpan CasDostupnostiExpedicie()
        {
            if (CasExpedicie == "Na výpravu")
            {
                CasExpedicie = "0:0:0";
            }
            var casyDostupnosti = CasExpedicie.Split(':');
            return new TimeSpan(int.Parse(casyDostupnosti[0]), int.Parse(casyDostupnosti[1]), int.Parse(casyDostupnosti[2]));
        }

        private void NasledujucaPoNacitajArenuProvinciarum()
        {
            var protivnici = ParsujHracovAreny();
            protivnik = protivnici[NajdiNajvhodnejsiehoProtivnika(protivnici)];

            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);            
            KalendarUdalosti.Add(simCasUdalosti, new ZautocVArene(simCasUdalosti, wb, protivnik));
        }

        private int NajdiNajvhodnejsiehoProtivnika(List<HracArena> protivnici)
        {
            return 1;
        }

        private List<HracArena> ParsujHracovAreny()
        {
            List<HracArena> res = new List<HracArena>();
            var loc = wb.Document.GetElementById("own2").GetElementsByTagName("tr");

            for (int i = 1; i < loc.Count; i++ )
            {
                var riadok = loc[i].GetElementsByTagName("td");
                var hracArena = new HracArena();
                hracArena.MenoHraca = riadok[0].OuterText.Trim();
                hracArena.Uroven = riadok[1].OuterText.Trim();
                hracArena.Server = riadok[2].OuterText.Trim();
                hracArena.ButtonUtok = riadok[3].GetElementsByTagName("span")[0];
                res.Add(hracArena);
            }

            return res;
        }

        private void NasledujucaPoNacitajArenu()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajArenuProvinciarum(simCasUdalosti, wb));
        }

        private void NasledujucaPoNacitajInventar()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
            KalendarUdalosti.Add(simCasUdalosti, new AktivujElixir(simCasUdalosti, wb));
        }

        private void NasledujucaPoNacitajPremium()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajInventar(simCasUdalosti, wb));
        }

        private void NasledujucaPoPonukniNaAukcii()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
            if (!JePonuknute)
            {
                KalendarUdalosti.Add(simCasUdalosti, new NacitajItemVAukcii(simCasUdalosti, wb, "6"));
            }
            else if (UkladajZlato && Zlato > 35800)
            {
                simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
                KalendarUdalosti.Add(simCasUdalosti, new PonukniNaAukcii(simCasUdalosti, wb, this));
            }

            else
            {
                if (int.Parse(Zivoty.Replace("%", "")) < 12)
                {
                    simCasUdalosti = SimCas + new TimeSpan(0, 0, 5);
                    KalendarUdalosti.Add(simCasUdalosti, new NacitajPremium(simCasUdalosti, wb));
                }                
            }
        }

        private void NasledujucaPoNacitajItemVAukcii()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
            KalendarUdalosti.Add(simCasUdalosti, new PonukniNaAukcii(simCasUdalosti, wb, this));
        }

        private void NasledujucaPoNacitajAukcnuBudovu()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajItemVAukcii(simCasUdalosti, wb, "9"));
        }

        private void NasledujucaPoZautocNaExpedicii()
        {
            Random rand = new Random();

            var simCasUdalosti = SimCas + new TimeSpan(0, 5, 10 + rand.Next(80));
            KalendarUdalosti.Add(simCasUdalosti, new NacitajLokaciu(simCasUdalosti, wb, Lokacia));

            if (ExpBody == 0)
            {
                KalendarUdalosti.Clear();
            }

            if (UkladajZlato && Zlato > 35800)
            {
                simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
                KalendarUdalosti.Add(simCasUdalosti, new NacitajAukcnuBudovu(simCasUdalosti, wb));
            }            
        }

        private void NasledujucaPoNacitajLokaciu()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
            KalendarUdalosti.Add(simCasUdalosti, new ZautocNaExpedicii(simCasUdalosti, wb, Monstrum));
        }

        public string[] LoadLokacie()
        {
            string[] lines = System.IO.File.ReadAllLines(@"Data\\Lokacie.txt");

            return lines;
        }

        private void UlozStatistiku()
        {        
            string mojeZasahy = "0";
            string superoveZasahy = "0";
            string server = "";
            string zlato = "0";
            string premia = "0"; 
            string zasahy = "";
            var vysledok = wb.Document.GetElementById("reportHeader").InnerText.Split(' ')[2];

            var report = wb.Document.GetElementById("content").GetElementsByTagName("div");

            foreach (HtmlElement item in report)
            {
                if (vysledok == "jalcisko" || vysledok == "Lukass")
                {
                    if (item.GetAttribute("className") == "title2_inner")
                    {
                        var riadky = item.GetElementsByTagName("p");

                        var slova = riadky[0].OuterText.Split(' ');

                        zlato = slova[2];
                        premia = slova[10];
                        break;
                    }
                }
            }

            var zranenia = wb.Document.GetElementById("content").GetElementsByTagName("fieldset");
            foreach (HtmlElement item in zranenia)
            {
                    if (item.GetAttribute("className") == "dungeon_report_statistic")
                    {
                        var riadky = item.GetElementsByTagName("tr");
                        mojeZasahy = riadky[1].GetElementsByTagName("td")[2].OuterText;
                        superoveZasahy = riadky[2].GetElementsByTagName("td")[2].OuterText;
                        break;
                    }
            }

            if (!JeKostym)
            {
                premia = (int.Parse(premia) * 10 + int.Parse(premia)).ToString();
            }

            var result = new InsertToDatabase().VlozVysledokSuboja("2", Hrac, protivnik.MenoHraca, protivnik.Server, zlato, premia, mojeZasahy + "/" + superoveZasahy, vysledok);

        }

        internal void AktualizujSystemovePremenne()
        {
            CasExpedicie = wb.Document.GetElementById("cooldown_bar_expedition").InnerText.Replace("\r\n", "");
            CasAreny = wb.Document.GetElementById("cooldown_bar_arena").InnerText.Replace("\r\n", "");
            CasZalaru = wb.Document.GetElementById("cooldown_bar_dungeon").InnerText.Replace("\r\n", "");
            CasTurmy = wb.Document.GetElementById("cooldown_bar_ct").InnerText.Replace("\r\n", "");
            Zivoty = wb.Document.GetElementById("header_values_hp_percent").InnerText;
            ExpBody = int.Parse(wb.Document.GetElementById("expeditionpoints_value_point").InnerText);
            Zlato = int.Parse(wb.Document.GetElementById("sstat_gold_val").InnerText.Replace(".", ""));           
        }
    }
}
