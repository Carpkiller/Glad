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
        public int ExpBody { get; set; }
        public string Zivoty { get; set; }

        public string Monstrum { get; set; }
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

            var simCasUdalosti = SimCas + new TimeSpan(0, 0, 5);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajLokaciu(simCasUdalosti, wb, Lokacia));
            //KalendarUdalosti.Add(simCasUdalosti, new NacitajPremium(simCasUdalosti, wb));

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
            }

            Naplanova = false;

            if (ZmenaKalendarUdalosti != null) //vyvolani udalosti
                ZmenaKalendarUdalosti();
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

        internal string ZistiStavZlata()
        {
            Zlato = int.Parse(wb.Document.GetElementById("sstat_gold_val").InnerText.Replace(".", ""));
            return Zlato.ToString();
        }

        internal string ZistiPocetExpBodov()
        {
            ExpBody = int.Parse(wb.Document.GetElementById("expeditionpoints_value_point").InnerText);
            return ExpBody.ToString();
        }

        internal string ZistiPocetZivotov()
        {
            Zivoty = wb.Document.GetElementById("header_values_hp_percent").InnerText;
            return Zivoty.ToString();
        }

        public string[] LoadLokacie()
        {
            string[] lines = System.IO.File.ReadAllLines(@"Data\\Lokacie.txt");

            return lines;
        }


    }
}
