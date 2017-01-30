using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class PonukniNaAukcii : Udalost
    {
        List<Ponuka> listPonuk;
        List<HtmlElement> listElementov;
        Jadro _jadro;

        public PonukniNaAukcii(TimeSpan cas, WebBrowser webBrowser, Jadro jadro)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.PonukniNaAukcii;
            _jadro = jadro;
            BlokujucaUdalost = BlokujucaUdalostEnum.Aukcia;
        }

        public override void Vykonaj()
        {
            var t = wb.Document.GetElementById("auction_table");
            ParsujItemy(t.GetElementsByTagName("tr"));

            for (int i = 0; i < listPonuk.Count; i++)
            {
                if (listPonuk[i].NajnizsiaPonuka > listPonuk[i].Cena && listPonuk[i].Volny)
                {
                    var tlacPonukni = listElementov[i];
                    tlacPonukni.InvokeMember("Click");
                    _jadro.JePonuknute = true;
                    return;
                }
            }
            _jadro.JePonuknute = false;
        }

        internal void ParsujItemy(HtmlElementCollection inputHtml)
        {
            listPonuk = new List<Ponuka>();
            listElementov = new List<HtmlElement>();

            foreach (HtmlElement row in inputHtml)
            {
                var i = row.GetElementsByTagName("td");
                foreach (HtmlElement item in i)
                {
                    var tt = item.GetElementsByTagName("input");
                    var divs = item.GetElementsByTagName("div");
                    var y = item.OuterText.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    if (y.Length > 1)
                    {
                        var najnizsiaPonuka = y[10].Split(':')[1].Trim().Replace(".", "");
                        var cena = y[12].Split(' ')[1].Replace(".", "");

                        listPonuk.Add(new Ponuka(cena, najnizsiaPonuka, cena, divs[7].GetElementsByTagName("a").Count == 0 && divs[7].GetElementsByTagName("span").Count == 0));
                        listElementov.Add(tt[7]);
                    }
                }
            }
        }
    }
}
