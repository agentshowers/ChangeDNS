using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Servers.Initialize();
            Servers.Update();
            Servers.SaveServers();
            foreach (DNS dns in Servers.GetServers())
            {
                Console.WriteLine(dns.Name + "\t" + dns.PrimaryIP + "\t" + dns.SecondaryIP);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
