using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DNS
{
    public class DNS
    {
        public string Name { get; set; }
        public string PrimaryIP { get; set; }
        public string SecondaryIP { get; set; }
        public DNS() { }
        public DNS(string name, string primaryIP, string secondaryIP)
        {
            Name = name;
            PrimaryIP = primaryIP;
            SecondaryIP = secondaryIP;
        }
    }

    public class Servers
    {
        private List<DNS> ServerList;

        private static Servers ServerInstance;

        private Servers()
        {
            ServerList = new List<DNS>();
        }

        public static void Initialize()
        {
            if (ServerInstance == null)
                ServerInstance = new Servers();
        }

        public static List<DNS> GetServers()
        {
            return ServerInstance.ServerList;
        }

        public static void Update()
        {
            String url = ConfigurationManager.AppSettings["api_url"];
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";

            using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objText = reader.ReadToEnd();
                    ServerInstance.ServerList = (List<DNS>)js.Deserialize(objText, typeof(List<DNS>));
                }
            }
        }

        public static bool TryLoadServers()
        {
            String file = ConfigurationManager.AppSettings["servers_file"];
            return false;
        }

        public static void SaveServers()
        {
            string filename = ConfigurationManager.AppSettings["servers_file"];
            XmlSerializer x = new XmlSerializer(typeof(List<DNS>));
            TextWriter writer = new StreamWriter(filename);
            x.Serialize(writer, ServerInstance.ServerList);
        }
    }
}
