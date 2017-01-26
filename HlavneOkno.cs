using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glad
{
    public partial class HlavneOkno : Form
    {
        private Jadro _jadro;

        [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
        private static extern int UrlMkSetSessionOption(
            int dwOption, string pBuffer, int dwBufferLength, int dwReserved);

        private const int URLMON_OPTION_USERAGENT = 0x10000001;
        private const int URLMON_OPTION_USERAGENT_REFRESH = 0x10000002;

        public void ChangeUserAgent()
        {
            const string ua = "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0\r\n";

            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT_REFRESH, null, 0, 0);
            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, ua, ua.Length, 0);
        }

        public HlavneOkno()
        {
            ChangeUserAgent();
            InitializeComponent();
            comboBox1.SelectedIndex = 0;

            _jadro = new Jadro(webBrowser1);
            _jadro.ZmenaKalendarUdalosti += AktualizujKalendarUdalosti;
            _jadro.ZmenaSimCasu += AktualizujSimCas;

            comboBoxLokacia.DataSource = _jadro.LoadLokacie();
            toolStripStatusLabel1.Text = "";
        }

        private void AktualizujKalendarUdalosti()
        {
            textBox1.Text = "";

            foreach (var udalost in _jadro.KalendarUdalosti.Values)
            {
                textBox1.AppendText(string.Format("{0} - {1}{2}", udalost.CasSimulacie, udalost.TypAktivity, Environment.NewLine));
            }
        }

        private void AktualizujSimCas()
        {
            toolStripStatusLabel1.Text = _jadro.SimCas.ToString("g");
        }

        // prihlasenie
        private void Login_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.GetElementById("login_username").SetAttribute("value", txtUsername.Text);
            webBrowser1.Document.GetElementById("login_password").SetAttribute("value", txtPassword.Text);   // loginsubmit
            
            var c = webBrowser1.Document.GetElementById("loginsubmit");
            c.InvokeMember("Click");

            Console.WriteLine("Login ... ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var c = webBrowser1.Document.GetElementById("submenu1").GetElementsByTagName("a");    // nacitacnie aukcnej budovy
            foreach (HtmlElement item in c)
            {
                Console.WriteLine(item.OuterText);
                if (item.OuterText == "Aukčná budova")
                {
                    item.InvokeMember("Click");
                }
            }
            
            while (true)           // cakanie na nacitanie
            {
                Console.WriteLine(webBrowser1.StatusText);
                Application.DoEvents();
                if (!webBrowser1.IsBusy && !string.IsNullOrEmpty(webBrowser1.StatusText) && !webBrowser1.StatusText.Contains("s4-sk.gladiatus.gameforge.com"))
                    break;
            }

            var o = webBrowser1.Document.GetElementsByTagName("select").GetElementsByName("itemType")[0].Children;

            foreach (HtmlElement option in o)        // vyber typu itemy z aukcie
            {
                var value = option.GetAttribute("value").ToString();
                Console.WriteLine(value);

                // prstene - 6
                // amulety - 9
                var index = comboBox1.SelectedIndex == 0 ? "9" : "6";

                if (value.Equals(index))
                    option.SetAttribute("selected", "selected");
            }

            var s = webBrowser1.Document.GetElementsByTagName("input");   // kliknutie na tlacitko FILTER v aukcii
            s[6].InvokeMember("Click");

            while (true)
            {
                Console.WriteLine(webBrowser1.StatusText);
                Application.DoEvents();
                if (!webBrowser1.IsBusy && !string.IsNullOrEmpty(webBrowser1.StatusText) && !webBrowser1.StatusText.Contains("s4-sk.gladiatus.gameforge.com"))
                    break;
            }

            var t = webBrowser1.Document.GetElementById("auction_table");
            _jadro.ParsujItemy(t.GetElementsByTagName("tr"));

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _jadro.listPonuk;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _jadro.listPonuk.Count; i++)
            {
                if (_jadro.listPonuk[i].NajnizsiaPonuka > _jadro.listPonuk[i].Cena && _jadro.listPonuk[i].Volny)
                {
                    var tlacPonukni = _jadro.listElementov[i];
                    tlacPonukni.InvokeMember("Click");
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            label3.Text = _jadro.ZistiStavZlata();
            label7.Text = _jadro.ZistiPocetExpBodov();
            label9.Text = _jadro.ZistiPocetZivotov();
            _jadro.UkladajZlato = checkBox1.Checked;
                       

            if (_jadro.SimulaciaBezi && _jadro.Naplanova)
            {
                _jadro.NaplanujDalsiuAktivitu();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _jadro.SpustSimulaciu(comboBoxLokacia.SelectedItem.ToString().Split('-'));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _jadro.SimulaciaBezi = false;
        }
    }
}