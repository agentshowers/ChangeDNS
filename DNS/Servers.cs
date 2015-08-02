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
            String url = Config.Get("api_url");
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

            Save();
        }

        public static bool TryLoad()
        {
            try
            {
                Load();
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void Load(){
            String filename = Config.Get("servers_file");
        }

        private static void Save()
        {
            string filename = Config.Get("servers_file");
            using (TextWriter writer = new StreamWriter(filename))
            {
                XmlSerializer x = new XmlSerializer(typeof(List<DNS>));
                x.Serialize(writer, ServerInstance.ServerList);
            }
        }
    }
}
