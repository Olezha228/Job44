using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Job4
{
    public class Internet
    {
        public static bool IsConnected
        { 
            get { return IsConnectedToGoogle() && CanDownloadFile(); } 
            private set { IsConnected = value; }
        }

        private static bool IsConnectedToGoogle()
        {
            try
            {
                string myAddress = "www.google.com";
                IPAddress[] addresslist = Dns.GetHostAddresses(myAddress);

                if (addresslist[0].ToString().Length > 6)
                {
                    return true;
                }
                else
                    return false;

            }
            catch
            {
                return false;
            }

        }

        private static bool CanDownloadFile()
        {
            try
            {
                WebClient Client = new WebClient();
                string Response;
                Response = Client.DownloadString("http://www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }

         
    }
}
