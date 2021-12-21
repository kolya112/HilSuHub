using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using INIManager;

namespace HilSuHub
{
    public partial class pBalance : Form
    {
        public pBalance()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nickname = textBox1.Text;
            HubManager settings = new HubManager(Directory.GetCurrentDirectory() + "\\settings.ini");
            if (settings.GetString("main", "login") == "null")
            {
                MessageBox.Show("Введите логин в settings.ini Basic авторизации к HilAPI, выданный Вам администратором", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string nick = settings.GetString("main", "login");
            if (settings.GetString("main", "password") == "null")
            {
                MessageBox.Show("Введите пароль в settings.ini Basic авторизации к HilAPI, выданный Вам администратором", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string password = settings.GetString("main", "password");
            byte[] all = Encoding.UTF8.GetBytes(nick + ":" + password);
            string header = "Basic " + Convert.ToBase64String(all);
            WebRequest auth = WebRequest.Create($"https://api.hil.su/v2/auth/application/token");
            auth.Headers["Authorization"] = header;
            auth.Method = "POST";
            WebResponse response = auth.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    JSON token = JsonConvert.DeserializeObject<JSON>(reader.ReadToEnd());
                    string access = token.accessToken;

                    HttpWebResponse balance = (HttpWebResponse)WebRequest.Create($"https://api.hil.su/v2/economy/balance?username={nickname}&accessToken={access}").GetResponse();

                    string value = JObject.Parse(new StreamReader(balance.GetResponseStream()).ReadToEnd()).SelectToken("response.balances.coins").ToString();

                    MessageBox.Show("Червонцев у игрока: " + value, "Баланс", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        class JSON
        {
            public string accessToken { get; set; }
        }
    }
}
