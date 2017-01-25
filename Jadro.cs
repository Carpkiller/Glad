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

        private readonly WebBrowser wb;
        private TimeSpan simCas;
        public bool SimulaciaBezi;

        public delegate void ZmenaKalendarUdalostiHandler();
        public event ZmenaKalendarUdalostiHandler ZmenaKalendarUdalosti;


        public List<Ponuka> listPonuk;
        public List<System.Windows.Forms.HtmlElement> listElementov;
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

        public void SpustSimulaciu()
        {
            simCas = new TimeSpan(0,0,0,0,0);
            KalendarUdalosti = new SortedList<TimeSpan, Udalost>();
            Naplanova = true;

            var simCasUdalosti = simCas + new TimeSpan(0, 0, 5);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajLokaciu(simCasUdalosti, wb));

            SpustBehSimulacie();

            if (ZmenaKalendarUdalosti != null) 
                ZmenaKalendarUdalosti();
        }

        private async void SpustBehSimulacie()
        {
            SimulaciaBezi = true;

            while (SimulaciaBezi)
            {
                Console.WriteLine(simCas + "  --  " + KalendarUdalosti.Values.Count);
                if (simCas.Equals(KalendarUdalosti.First().Value.CasSimulacie))
                {
                    KalendarUdalosti.First().Value.Vykonaj();
                    Naplanova = true;
                }

                simCas = simCas.Add(new TimeSpan(0,0,0,0,100));
                await PutTaskDelay();
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
            }

            Naplanova = false;

            if (ZmenaKalendarUdalosti != null) //vyvolani udalosti
                ZmenaKalendarUdalosti();
        }

        private void NasledujucaPoNacitajItemVAukcii()
        {
            var simCasUdalosti = simCas + new TimeSpan(0, 0, 5);
            KalendarUdalosti.Add(simCasUdalosti, new PonukniNaAukcii(simCasUdalosti, wb));
        }

        private void NasledujucaPoNacitajAukcnuBudovu()
        {
            var simCasUdalosti = simCas + new TimeSpan(0, 0, 2);
            KalendarUdalosti.Add(simCasUdalosti, new NacitajItemVAukcii(simCasUdalosti, wb));
        }

        private void NasledujucaPoZautocNaExpedicii()
        {
            Random rand = new Random();

            var simCasUdalosti = simCas + new TimeSpan(0, 5, 10 + rand.Next(50));
            KalendarUdalosti.Add(simCasUdalosti, new NacitajLokaciu(simCasUdalosti, wb));

            //if (Zlato > 25800)
            //{
            //    simCasUdalosti = simCas + new TimeSpan(0, 0, 2);
            //    KalendarUdalosti.Add(simCasUdalosti, new NacitajAukcnuBudovu(simCasUdalosti, wb));
            //}
        }

        private void NasledujucaPoNacitajLokaciu()
        {
            var simCasUdalosti = simCas + new TimeSpan(0, 0, 5);
            KalendarUdalosti.Add(simCasUdalosti, new ZautocNaExpedicii(simCasUdalosti, wb));
        }

        internal string ZistiStavZlata()
        {
            Zlato = int.Parse(wb.Document.GetElementById("sstat_gold_val").InnerText.Replace(".", ""));
            return Zlato.ToString();
        }
    }
}
