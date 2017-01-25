using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad.Udalosti
{
    public class PonukniNaAukcii : Udalost
    {
        List<Ponuka> listPonuk;
        List<System.Windows.Forms.HtmlElement> listElementov;

        public PonukniNaAukcii(TimeSpan cas, WebBrowser webBrowser)
        {
            CasSimulacie = cas;
            wb = webBrowser;
            TypAktivity = TypAktivityEnum.PonukniNaAukcii;
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
                }
            }
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
    }
}
