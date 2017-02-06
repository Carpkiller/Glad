using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glad.Databaza;
using Glad.Udalosti;

namespace Glad
{
    public class Jadro
    {
        public SortedList<TimeSpan, Udalost> KalendarUdalosti { get; set; }
        public List<Udalost> KalendarBlokovanychUdalosti { get; set; }
        private List<string> PoradieZalohovania { get; set; }
        private int _indexZalohovania;

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
        public HracArena Protivnik { get; set; }
        public string Lokacia { get; set; }

        private readonly WebBrowser wb;
        public TimeSpan SimCas;
        public bool SimulaciaBezi;
        public bool Inicializacia { get; set; }

        public delegate void ZmenaKalendarUdalostiHandler();

        public event ZmenaKalendarUdalostiHandler ZmenaKalendarUdalosti;

        public delegate void ZmenaSimCasuHandler();

        public event ZmenaSimCasuHandler ZmenaSimCasu;

        private BlokujucaUdalostEnum dovodBlokacieSimulacie;

        public List<Ponuka> ListPonuk;
        public List<HtmlElement> ListElementov;

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
            ListPonuk = new List<Ponuka>();
            ListElementov = new List<HtmlElement>();
            KalendarUdalosti = new SortedList<TimeSpan, Udalost>();
            KalendarBlokovanychUdalosti = new List<Udalost>();
            this.wb = wb;
            SimulaciaBezi = false;
            Inicializacia = false;
            JePonuknute = true;
            PoradieZalohovania = new List<string>()
            {
                "9",
                "6"
            };
            _indexZalohovania = 0;
        }

        internal void ParsujItemy(HtmlElementCollection inputHtml)
        {
            ListPonuk = new List<Ponuka>();
            ListElementov = new List<HtmlElement>();

            foreach (HtmlElement row in inputHtml)
            {
                var i = row.GetElementsByTagName("td");
                foreach (HtmlElement item in i)
                {
                    var tt = item.GetElementsByTagName("input");
                    var divs = item.GetElementsByTagName("div");
                    var y = item.OuterText.Split(new[] {"\r\n"}, StringSplitOptions.None);

                    if (y.Length > 1)
                    {
                        var najnizsiaPonuka = y[10].Split(':')[1].Trim().Replace(".", "");
                        var cena = y[12].Split(' ')[1].Replace(".", "");

                        ListPonuk.Add(new Ponuka("", najnizsiaPonuka, cena,
                            divs[7].GetElementsByTagName("a").Count == 0 ||
                            divs[7].GetElementsByTagName("span").Count == 0));
                        ListElementov.Add(tt[7]);
                    }
                }
            }
        }

        public void SpustSimulaciu(string[] lokacia)
        {
            dovodBlokacieSimulacie = BlokujucaUdalostEnum.Ziadna;
            SimCas = new TimeSpan(0, 0, 0, 0, 0);
            KalendarUdalosti = new SortedList<TimeSpan, Udalost>();
            Naplanova = true;
            Lokacia = lokacia[0].Trim();
            Monstrum = lokacia[1].Trim();

            if (!Inicializacia)
            {
                var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
                KalendarUdalosti.Add(simCasUdalosti, new InicializacnaUdalost(simCasUdalosti, wb));
            }
            else
            {
                NaplanujDalsiuAktivitu(TypAktivityEnum.NaplanujDalsieUdalosti);
            }

            SpustBehSimulacie();

            if (ZmenaKalendarUdalosti != null)
                ZmenaKalendarUdalosti();
        }

        private async void SpustBehSimulacie()
        {
            SimulaciaBezi = true;

            while (SimulaciaBezi)
            {
                Console.WriteLine(SimCas + @"  --  " + KalendarUdalosti.Values.Count);
                if (SimCas.Equals(KalendarUdalosti.First().Value.CasSimulacie) &&
                    (dovodBlokacieSimulacie == BlokujucaUdalostEnum.Ziadna ||
                     dovodBlokacieSimulacie == KalendarUdalosti.First().Value.BlokujucaUdalost))
                {
                    dovodBlokacieSimulacie = KalendarUdalosti.First().Value.BlokujucaUdalost;
                    KalendarUdalosti.First().Value.Vykonaj();
                    Naplanova = true;
                }
                else if (SimCas.Equals(KalendarUdalosti.First().Value.CasSimulacie) &&
                         (dovodBlokacieSimulacie != BlokujucaUdalostEnum.Ziadna &&
                          dovodBlokacieSimulacie != KalendarUdalosti.First().Value.BlokujucaUdalost))
                {
                    KalendarBlokovanychUdalosti.Add(KalendarUdalosti.First().Value);
                    KalendarUdalosti.RemoveAt(0);
                }

                if (dovodBlokacieSimulacie == BlokujucaUdalostEnum.Ziadna && KalendarBlokovanychUdalosti.Count > 0)
                {
                    NaplanujBlokovanuAktivitu();
                }

                if (!JePonuknute)
                {
                    NaplanujDalsiuAktivitu(TypAktivityEnum.NeboloPonuknuteVAukcii);
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

        private void NaplanujBlokovanuAktivitu()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);

            var udalost = KalendarBlokovanychUdalosti.First();
            KalendarBlokovanychUdalosti.RemoveAt(0);
            udalost.CasSimulacie = simCasUdalosti;

            KalendarUdalosti.Add(simCasUdalosti, udalost);
        }

        private async Task PutTaskDelay()
        {
            await Task.Delay(100);
        }

        public void NaplanujDalsiuAktivitu(TypAktivityEnum predchAktiv = TypAktivityEnum.Refresh)
        {
            if (KalendarUdalosti.Count > 0)
            {
                predchAktiv = KalendarUdalosti.First().Value.TypAktivity;
                KalendarUdalosti.RemoveAt(0);
            }

            switch (predchAktiv)
            {
                case TypAktivityEnum.InicializacnaUdalost:
                    NasledujucaPoInicializacnaUdalost();
                    break;
                case TypAktivityEnum.NaplanujDalsieUdalosti:
                    NasledujucaNaplanujDalsieUdalosti();
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
                case TypAktivityEnum.NeboloPonuknuteVAukcii:
                    NasledujucaPoNeboloPonuknuteVAukcii();
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
                case TypAktivityEnum.NacitajTurmuProvinciarum:
                    NasledujucaPoNacitajTurmuProvinciarum();
                    break;
                case TypAktivityEnum.ZautocVTurme:
                    NasledujucaPoZautocVTurme();
                    break;
                case TypAktivityEnum.NacitajTurmu:
                    NasledujucaPoNacitajTurmu();
                    break;
            }
            Naplanova = false;

            if (ZmenaKalendarUdalosti != null) //vyvolani udalosti
                ZmenaKalendarUdalosti();
        }

        private void NasledujucaPoNacitajTurmu()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajTurmuProvinciarum(simCasUdalosti, wb));
        }

        private void NasledujucaNaplanujDalsieUdalosti()
        {
            TimeSpan simCasUdalosti;
            if (KlikajArenu)
            {
                simCasUdalosti = SimCas + CasDostupnostiAreny();
                KalendarUdalosti.Add(simCasUdalosti, new NacitajArenu(simCasUdalosti, wb));
            }
            if (KlikajExpedicie)
            {
                simCasUdalosti = SimCas + CasDostupnostiExpedicie();
                KalendarUdalosti.Add(simCasUdalosti, new NacitajLokaciu(simCasUdalosti, wb, Lokacia));
            }

            if (KlikajTurmu)
            {
                simCasUdalosti = SimCas + CasDostupnostiTurmy();
                KalendarUdalosti.Add(simCasUdalosti, new NacitajTurmu(simCasUdalosti, wb));
            }
        }


        private void NasledujucaPoInicializacnaUdalost()
        {
            Hrac = ZistiMenoHraca();

            TimeSpan simCasUdalosti;
            if (KlikajArenu)
            {
                simCasUdalosti = SimCas + CasDostupnostiAreny();
                KalendarUdalosti.Add(simCasUdalosti, new NacitajArenu(simCasUdalosti, wb));
            }
            if (KlikajExpedicie)
            {
                simCasUdalosti = SimCas + CasDostupnostiExpedicie();
                KalendarUdalosti.Add(simCasUdalosti, new NacitajLokaciu(simCasUdalosti, wb, Lokacia));
            }

            if (KlikajTurmu)
            {
                simCasUdalosti = SimCas + CasDostupnostiTurmy();
                KalendarUdalosti.Add(simCasUdalosti, new NacitajTurmu(simCasUdalosti, wb));
            }

            Inicializacia = true;
        }

        private string ZistiMenoHraca()
        {
            var profil = wb.Document.GetElementById("content").GetElementsByTagName("div");
            foreach (HtmlElement item in profil)
            {
                if (item.GetAttribute("className") == "player_name_bg pngfix")
                {
                    return item.OuterText.Trim().Split(' ')[0];
                }
            }

            return String.Empty;
        }

        private void NasledujucaPoZautocVArene()
        {
            var simCasUdalosti = SimCas + CasDostupnostiAreny();
            KalendarUdalosti.Add(simCasUdalosti, new NacitajArenu(simCasUdalosti, wb));
            dovodBlokacieSimulacie = BlokujucaUdalostEnum.Ziadna;

            UlozStatistiku();

            if (UkladajZlato && Zlato > 30800)
            {
                simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
                KalendarUdalosti.Add(simCasUdalosti, new NacitajAukcnuBudovu(simCasUdalosti, wb));
            }
            if (int.Parse(Zivoty.Replace("%", "")) < 2)
            {
                simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
                KalendarUdalosti.Add(simCasUdalosti, new NacitajPremium(simCasUdalosti, wb));
            }

        }

        private void NasledujucaPoZautocVTurme()
        {
            var simCasUdalosti = SimCas + CasDostupnostiTurmy();
            KalendarUdalosti.Add(simCasUdalosti, new NacitajTurmu(simCasUdalosti, wb));
            dovodBlokacieSimulacie = BlokujucaUdalostEnum.Ziadna;

            UlozStatistikuTurma();

            if (UkladajZlato && Zlato > 30800)
            {
                simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
                KalendarUdalosti.Add(simCasUdalosti, new NacitajAukcnuBudovu(simCasUdalosti, wb));
            }
        }

        private TimeSpan CasDostupnostiAreny()
        {
            if (CasAreny.Trim() == "Do arény")
            {
                CasAreny = "0:0:0";
            }
            var casyDostupnosti = CasAreny.Split(':');
            return new TimeSpan(int.Parse(casyDostupnosti[0]), int.Parse(casyDostupnosti[1]),
                int.Parse(casyDostupnosti[2]) + 1);
        }

        private TimeSpan CasDostupnostiExpedicie()
        {
            if (CasExpedicie.Length != 8)
            {
                CasExpedicie = "0:0:0";
            }
            var casyDostupnosti = CasExpedicie.Split(':');
            return new TimeSpan(int.Parse(casyDostupnosti[0]), int.Parse(casyDostupnosti[1]),
                int.Parse(casyDostupnosti[2]) + 1);
        }

        private TimeSpan CasDostupnostiTurmy()
        {
            if (CasTurmy.Length != 8)
            {
                CasTurmy = "0:0:0";
            }
            var casyDostupnosti = CasTurmy.Split(':');
            return new TimeSpan(int.Parse(casyDostupnosti[0]), int.Parse(casyDostupnosti[1]),
                int.Parse(casyDostupnosti[2]) + 2);
        }

        private void NasledujucaPoNacitajArenuProvinciarum()
        {
            var protivnici = ParsujHracovAreny();
            Protivnik = NajdiNajvhodnejsiehoProtivnika(protivnici);

            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
            KalendarUdalosti.Add(simCasUdalosti, new ZautocVArene(simCasUdalosti, wb, Protivnik));
        }

        private void NasledujucaPoNacitajTurmuProvinciarum()
        {
            var protivnici = ParsujHracovTurmy();
            Protivnik = NajdiNajvhodnejsiehoProtivnikaTurma(protivnici);

            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
            KalendarUdalosti.Add(simCasUdalosti, new ZautocVTurme(simCasUdalosti, wb, Protivnik));
        }

        private HracArena NajdiNajvhodnejsiehoProtivnikaTurma(List<HracArena> protivnici)
        {
            try
            {
            Logovanie.ZalogujProtivnikov(protivnici);
            var db = new SelectFromDatabase().GetZaznamy(protivnici, "4");
            Logovanie.ZalogujDbResult(db);
            var pomList = new List<HracArena>(protivnici);

            if (db.Count == 0)
            {
                return NajblizsiLevel(protivnici);
            }
            else
            {
                var najlepsiHrac = NajviacZlataZHraca(db);
                if (int.Parse(najlepsiHrac.Premia) > 40000)
                {
                    Logovanie.LogujText(string.Format("Pripad 1. - {0} - {1}", najlepsiHrac.Protivnik, najlepsiHrac.Premia));
                    return protivnici.Find(x => x.MenoHraca == najlepsiHrac.Protivnik);
                }
            }

            if (JeModPrieskum)
            {
                foreach (var protivnik in protivnici)
                {
                    foreach (var zaznam in db)
                    {
                        if (zaznam.Protivnik == protivnik.MenoHraca)
                        {
                            Logovanie.LogujText(string.Format("Odstranovanie hraca - {0}", protivnik.MenoHraca));
                            pomList.Remove(protivnik);
                            break;
                        }
                    }
                }
                if (pomList.Count == 0)
                {
                    var najlepsiHrac = NajviacZlataZHraca(db);
                    Logovanie.LogujText(string.Format("Pripad 2. - {0} - {1}", najlepsiHrac.Protivnik, najlepsiHrac.Premia));
                    return protivnici.Find(x => x.MenoHraca == najlepsiHrac.Protivnik);
                }
                else
                {
                    Logovanie.LogujText("Pomocny list");
                    Logovanie.ZalogujProtivnikov(pomList);
                    return NajblizsiLevel(pomList);
                }
            }
            if (JeModZarabanie)
            {
                var najlepsiHrac = NajviacZlataZHraca(db);
                Logovanie.LogujText(string.Format("Pripad 3. - {0} - {1}", najlepsiHrac.Protivnik, najlepsiHrac.Premia));
                return protivnici.Find(x => x.MenoHraca == najlepsiHrac.Protivnik);
            }
            }
            catch (Exception e )
            {
                MessageBox.Show(e.Message);
            }
            return NajblizsiLevel(protivnici);
        }

        private HracArena NajblizsiLevel(List<HracArena> protivnici)
        {
            foreach (var prot in protivnici)
            {
                prot.Rating = Math.Abs(106 - int.Parse(prot.Uroven));
            }

            Logovanie.ZalogujSupera(protivnici.OrderBy(x => x.Rating).First());
            return protivnici.OrderBy(x => x.Rating).First();
        }

        private Zaznam NajviacZlataZHraca(List<Zaznam> protivnici)
        {
            int max = 0;
            Zaznam hladanyHrac = null;

            foreach (var protivnik in protivnici)
            {
                if (int.Parse(protivnik.Premia.Replace(".", "")) >= max && protivnici.Where(x => x.Datum == DateTime.Today).Count(x => x.Protivnik == protivnik.Protivnik) < 5)
                {
                    max = int.Parse(protivnik.Premia.Replace(".", ""));
                    hladanyHrac = protivnik;
                }
            }

            return hladanyHrac;
        }

        private HracArena NajdiNajvhodnejsiehoProtivnika(List<HracArena> protivnici)
        {
            var db = new SelectFromDatabase().GetZaznamy(protivnici, "2");
            var pomList = new List<HracArena>(protivnici);

            if (db.Count == 0)
            {
                return NajblizsiLevel(protivnici);
            }

            if (JeModPrieskum)
            {
                // odstranenie hracov co sa vyskytuju v DB
                foreach (var protivnik in protivnici)
                {
                    if (db.Any(x => x.Protivnik == protivnik.MenoHraca))
                    {
                        pomList.Remove(protivnik);
                    }
                }
                if (pomList.Count == 0)
                {
                    var najlepsiHrac = NajviacZlataZHraca(db);
                    return protivnici.Find(x => x.MenoHraca == najlepsiHrac.Protivnik);
                }
                else
                {
                    return NajblizsiLevel(pomList);
                }
            }
            if (JeModZarabanie)
            {
                var najlepsiHrac = NajviacZlataZHraca(db);
                return protivnici.Find(x => x.MenoHraca == najlepsiHrac.Protivnik);
            }

            return NajblizsiLevel(protivnici);
        }

        private List<HracArena> ParsujHracovAreny()
        {
            List<HracArena> res = new List<HracArena>();
            var loc = wb.Document.GetElementById("own2").GetElementsByTagName("tr");

            for (int i = 1; i < loc.Count; i++)
            {
                var riadok = loc[i].GetElementsByTagName("td");
                var hracArena = new HracArena
                {
                    MenoHraca = riadok[0].OuterText.Trim(),
                    Uroven = riadok[1].OuterText.Trim(),
                    Server = riadok[2].OuterText.Trim(),
                    ButtonUtok = riadok[3].GetElementsByTagName("span")[0]
                };
                res.Add(hracArena);
            }

            return res;
        }

        private List<HracArena> ParsujHracovTurmy()
        {
            List<HracArena> res = new List<HracArena>();
            var loc = wb.Document.GetElementById("own3").GetElementsByTagName("tr");

            for (int i = 1; i < loc.Count; i++)
            {
                var riadok = loc[i].GetElementsByTagName("td");
                var hracArena = new HracArena
                {
                    MenoHraca = riadok[0].OuterText.Trim(),
                    Uroven = riadok[1].OuterText.Trim(),
                    Server = riadok[2].OuterText.Trim(),
                    ButtonUtok = riadok[3].GetElementsByTagName("span")[0]
                };
                res.Add(hracArena);
            }

            return res;
        }

        private void NasledujucaPoNacitajArenu()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajArenuProvinciarum(simCasUdalosti, wb));
        }

        private void NasledujucaPoNacitajInventar()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
            KalendarUdalosti.Add(simCasUdalosti, new AktivujElixir(simCasUdalosti, wb));
        }

        private void NasledujucaPoNacitajPremium()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajInventar(simCasUdalosti, wb));
        }

        private void NasledujucaPoPonukniNaAukcii()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
            if (UkladajZlato && Zlato > 25800)
            {
                KalendarUdalosti.Add(simCasUdalosti, new PonukniNaAukcii(simCasUdalosti, wb, this, Zlato));
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

        private void NasledujucaPoNeboloPonuknuteVAukcii()
        {
            _indexZalohovania++;
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajItemVAukcii(simCasUdalosti, wb, PoradieZalohovania[_indexZalohovania]));
        }

        private void NasledujucaPoNacitajItemVAukcii()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
            KalendarUdalosti.Add(simCasUdalosti, new PonukniNaAukcii(simCasUdalosti, wb, this, Zlato));
        }

        private void NasledujucaPoNacitajAukcnuBudovu()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 2);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajItemVAukcii(simCasUdalosti, wb, PoradieZalohovania[_indexZalohovania]));
        }

        private void NasledujucaPoZautocNaExpedicii()
        {
            Random rand = new Random();

            var simCasUdalosti = SimCas + new TimeSpan(0, 5, 10 + rand.Next(60));

            if (ExpBody > 0)
            {
                KalendarUdalosti.Add(simCasUdalosti, new NacitajLokaciu(simCasUdalosti, wb, Lokacia));
            }

            if (UkladajZlato && Zlato > 30800)
            {
                simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
                KalendarUdalosti.Add(simCasUdalosti, new NacitajAukcnuBudovu(simCasUdalosti, wb));
            }

            dovodBlokacieSimulacie = BlokujucaUdalostEnum.Ziadna;
        }

        private void NasledujucaPoNacitajLokaciu()
        {
            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 1);
            KalendarUdalosti.Add(simCasUdalosti, new ZautocNaExpedicii(simCasUdalosti, wb, Monstrum));
        }

        public string[] LoadLokacie()
        {
            string[] lines = File.ReadAllLines(@"Data\\Lokacie.txt");

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
                    if (item.GetAttribute("className") == "report_reward")
                    {
                        var riadky = item.GetElementsByTagName("p");

                        var slova = riadky[0].OuterText.Split(' ');

                        zlato = slova[2].Replace(".", "");
                        premia = slova[10].Replace(".", "");
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
                premia = (int.Parse(premia)*10 + int.Parse(premia)).ToString();
            }

            var result = new InsertToDatabase().VlozVysledokSuboja("2", Hrac, Protivnik.MenoHraca, Protivnik.Server,
                zlato, premia, mojeZasahy + "/" + superoveZasahy, vysledok);

        }

        private void UlozStatistikuTurma()
        {
            string mojeZasahy = "0";
            string superoveZasahy = "0";
            string server = "";
            string zlato = "0";
            string premia = "0";
            string zasahy = "";

            string vysledok = "";

            vysledok = wb.Document.GetElementById("reportHeader").InnerText.Split(' ')[2];

            var report = wb.Document.GetElementById("content").GetElementsByTagName("div");


            foreach (HtmlElement item in report)
            {
                if (item.GetAttribute("className") == "title2_box")
                {
                    var slova = item.OuterText.Split(' ');

                    mojeZasahy = slova[2];
                    superoveZasahy = slova[6];
                    break;
                }
            }

            foreach (HtmlElement item in report)
            {
                if (vysledok == "jalcisko" || vysledok == "Lukass")
                {
                    if (item.GetAttribute("className") == "report_reward")
                    {
                        var podele = item.GetElementsByTagName("div");

                        foreach (HtmlElement poditem in podele)
                        {
                            if (poditem.GetAttribute("className") == "title2_inner")
                            {
                                var riadky = item.GetElementsByTagName("p");

                                var slova = riadky[0].OuterText.Split(' ');

                                zlato = slova[2].Replace(".", "");
                                premia = slova[10].Replace(".", "");
                                break;
                            }
                        }
                    }
                }
            }

            if (!JeKostym)
            {
                premia = (int.Parse(premia)*10 + int.Parse(premia)).ToString();
            }

            var result = new InsertToDatabase().VlozVysledokSuboja("4", Hrac, Protivnik.MenoHraca, Protivnik.Server,
                zlato, premia, mojeZasahy + "/" + superoveZasahy, vysledok);
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