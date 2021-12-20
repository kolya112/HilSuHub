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
            // Топ 1 по червонцам
            HttpWebResponse response1 = (HttpWebResponse)WebRequest.Create($"https://api.hil.su/v2/economy/top?limit=1&currency=coins").GetResponse(); // Первый запрос

            JSON apiAVAILABLE = JsonConvert.DeserializeObject<JSON>(new StreamReader(response1.GetResponseStream()).ReadToEnd());

            if (apiAVAILABLE.success != true)
            {
                top1csum.Text = "Ошибка подключения к HilAPI";
                response1.Close();
            }
            else
            {
                HttpWebResponse response2 = (HttpWebResponse)WebRequest.Create($"https://api.hil.su/v2/economy/top?limit=1&currency=coins").GetResponse(); // Второй запрос
                string serveranswer2 = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                string resultbalance = JObject.Parse(serveranswer2).SelectToken("response.users[0].balance").ToString();
                string resultnickname = JObject.Parse(serveranswer2).SelectToken("response.users[0].user.username").ToString();

                response1.Close();
                response2.Close();
                top1cnick.Text = resultnickname;
                top1csum.Text = resultbalance;
            }
        }

        class JSON
        {
            public bool success { get; set; }
        }
    }
}
