using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNS
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            Servers.Initialize();
            if (!Servers.TryLoad())
                Servers.Update();

            UpdateServerBox();

            foreach (string nic in Network.GetNICs())
                boxNICs.Items.Add(nic);
            boxNICs.SelectedIndex = 0;
        }

        private void UpdateServerBox(){
            foreach (DNS dns in Servers.GetServers())
                boxServers.Items.Add(dns.Name);
            boxServers.SelectedItem = Config.Get("default_dns");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Servers.Update();
                UpdateServerBox();
                MessageBox.Show(this,"DNS Servers updated","Update",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch(Exception){
                MessageBox.Show(this, "DNS Servers could not be updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
