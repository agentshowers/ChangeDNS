using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DNS
{
    public class Network
    {
        public static List<String> GetNICs()
        {
            using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (var networkConfigs = networkConfigMng.GetInstances())
                {
                    List<String> listNIC = new List<String>();
                    foreach (var managementObject in networkConfigs.Cast<ManagementObject>())
                    {
                        listNIC.Add((string)managementObject["Description"]);
                    }
                    return listNIC;
                }
            }
        }
    }
}
