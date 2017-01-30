using System;
using System.Runtime.InteropServices;
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

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _jadro.AktualizujSystemovePremenne();
            label3.Text = _jadro.Zlato.ToString();
            label7.Text = _jadro.ExpBody.ToString();
            label9.Text = _jadro.Zivoty;
            label11.Text = _jadro.CasAreny.ToString();
            label13.Text = _jadro.CasExpedicie.ToString();
            _jadro.UkladajZlato = checkBox1.Checked;
                       

            if (_jadro.SimulaciaBezi && _jadro.Naplanova)
            {
                _jadro.NaplanujDalsiuAktivitu();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _jadro.SpustSimulaciu(comboBoxLokacia.SelectedItem.ToString().Split('-'));

            _jadro.KlikajArenu = checkBoxKlikajArenu.Checked;
            _jadro.KlikajTurmu = checkBoxKlikajTurmu.Checked;
            _jadro.KlikajExpedicie = checkBoxKlikajExpedicie.Checked;
            _jadro.KlikajZalar = checkBoxKlikajZalar.Checked;

            _jadro.JeKostym = checkBoxJeKostym.Checked;
            _jadro.JeModPrieskum = checkBoxJeModPrieskum.Checked;
            _jadro.JeModZarabanie = checkBoxJeModZarabaci.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _jadro.SimulaciaBezi = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //_jadro.NajdiNajvhodnejsiehoProtivnikaTurma(null);
        }
    }
}