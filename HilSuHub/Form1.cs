using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using INIManager;

namespace HilSuHub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\settings.ini"))
            {
                WebClient web = new WebClient();
                try
                {
                    web.DownloadFile("http://nsp.mygamesonline.org/api/hilsu/settings.ini", "settings.ini");
                }
                catch (WebException)
                {
                    MessageBox.Show("Oops! Try later again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            HttpWebResponse response = (HttpWebResponse)WebRequest.Create($"https://api.hil.su/v2/economy/top?limit=5&currency=coins").GetResponse(); // Запрос для получения топ 5 по червонцам
            string serveranswer = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();
            string[] users = { JObject.Parse(serveranswer).SelectToken("response.users[0].user.username").ToString(), 
                JObject.Parse(serveranswer).SelectToken("response.users[1].user.username").ToString(), 
                JObject.Parse(serveranswer).SelectToken("response.users[2].user.username").ToString(), 
                JObject.Parse(serveranswer).SelectToken("response.users[3].user.username").ToString(), 
                JObject.Parse(serveranswer).SelectToken("response.users[4].user.username").ToString() };
            decimal[] balances = { Math.Round(Convert.ToDecimal(JObject.Parse(serveranswer).SelectToken("response.users[0].balance").ToString())),
                Math.Round(Convert.ToDecimal(JObject.Parse(serveranswer).SelectToken("response.users[1].balance").ToString())),
                Math.Round(Convert.ToDecimal(JObject.Parse(serveranswer).SelectToken("response.users[2].balance").ToString())),
                Math.Round(Convert.ToDecimal(JObject.Parse(serveranswer).SelectToken("response.users[3].balance").ToString())),
                Math.Round(Convert.ToDecimal(JObject.Parse(serveranswer).SelectToken("response.users[4].balance").ToString())) };
            string[] arrayusers1 = { 
            users[0], balances[0].ToString()
            };
            string[] arrayusers2 = {
            users[1], balances[1].ToString()
            };
            string[] arrayusers3 = {
            users[2], balances[2].ToString()
            };
            string[] arrayusers4 = {
            users[3], balances[3].ToString()
            };
            string[] arrayusers5 = {
            users[4], balances[4].ToString()
            };
            ListViewItem itm1 = new ListViewItem(arrayusers1);
            ListViewItem itm2 = new ListViewItem(arrayusers2);
            ListViewItem itm3 = new ListViewItem(arrayusers3);
            ListViewItem itm4 = new ListViewItem(arrayusers4);
            ListViewItem itm5 = new ListViewItem(arrayusers5);
            coinstop.Items.Add(itm1);
            coinstop.Items.Add(itm2);
            coinstop.Items.Add(itm3);
            coinstop.Items.Add(itm4);
            coinstop.Items.Add(itm5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pBalance balance = new pBalance();
            balance.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
