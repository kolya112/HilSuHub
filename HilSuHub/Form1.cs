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
            HttpWebResponse response = (HttpWebResponse)WebRequest.Create($"https://api.hil.su/v2/economy/top?limit=1&currency=coins").GetResponse();
            JSON coinstopone = JsonConvert.DeserializeObject<JSON>(new StreamReader(response.GetResponseStream()).ReadToEnd());
            response.Close();

            label4.Text = "Червонцев: " + coinstopone.balance;
        }

        class JSON
        {
            public decimal balance { get; set; }
        }
    }
}
