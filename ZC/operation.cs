using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ZC
{
    class operation
    {
        ZC form1 = new ZC();

 
        public string GetIP()
        {
            string IPstring = "";
            IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            if (ipHostEntry.AddressList.Length > 0)
            {
                foreach (IPAddress ip in ipHostEntry.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)

                        IPstring = ip.ToString();
                }
            }
            return IPstring;
        }
    }
}
