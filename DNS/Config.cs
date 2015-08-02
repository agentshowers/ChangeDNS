using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNS
{
    public class Config
    {
        public static String Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void Set(string key, string value)
        {

        }

        public static void Add(string key, string value)
        {

        }

        public static void Delete(string key)
        {

        }
    }
}
