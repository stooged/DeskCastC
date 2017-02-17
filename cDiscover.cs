using System;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace DeskCastC
{
    class cDiscover
    {

        public event EventHandler<cDiscover> DiscoveredCC;
        public event EventHandler<cDiscover> DiscoverError;

        public string ChromeCast_Name { get; internal set; }
        public string ChromeCast_IP { get; internal set; }
        public string ChromeCast_BaseURL { get; internal set; }
        public string ChromeCast_UID { get; internal set; }
        public string ErrorMessage { get; internal set; }


        int listenPort = 0;
        Thread SsDPThreads;
        UdpClient Dlisteners;
        bool CancelListen = false;


        int RandomPort()
        {
         Random RndPort = new Random();
         return RndPort.Next(50000, 58000);
        }



        void SSDPReceive()
        {
            listenPort = RandomPort();
            cDiscover CCEvent = new cDiscover();
            UdpClient Dlistener = new UdpClient(listenPort);
            IPEndPoint DEP = new IPEndPoint(IPAddress.Any, listenPort);
            Dlisteners = Dlistener;
            Byte[] sBytes = Encoding.ASCII.GetBytes("M-SEARCH * HTTP/1.1" + Environment.NewLine +
                                              "HOST: 239.255.255.250:1900" + Environment.NewLine +
                                              "MAN: \"ssdp:discover\"" + Environment.NewLine +
                                              "MX: 5" + Environment.NewLine +
                                              "ST: urn:dial-multiscreen-org:service:dial:1" + Environment.NewLine + Environment.NewLine);
            Dlistener.Send(sBytes, sBytes.Length, "239.255.255.250", 1900);
            try
            {
                while (CancelListen != true)
                {
                    Byte[] lBytes = Dlistener.Receive(ref DEP);
                    string Result = Encoding.ASCII.GetString(lBytes, 0, lBytes.Length);
                    string[] Spl1, Spl2;
                    if (Result.Contains("LOCATION: ") == true)
                        {
                        Spl1 = Regex.Split(Result, "LOCATION: ");
                        Spl2 = Regex.Split(Spl1[1], Environment.NewLine);
                        GetCCInfo(Spl2[0], DEP.Address.ToString());
                    }
                }
            }
            catch (Exception e)
            {
               CCEvent.ErrorMessage = e.Message;
               DiscoverError(this, CCEvent);
            }
            finally
            {
                Dlistener.Close();
            }
        }




        void GetCCInfo(string StrLocation, string StrIP)
        {
            cDiscover CCEvent = new cDiscover();
            try
            {
                WebClient oWeb = new WebClient();
                string Result = oWeb.DownloadString(StrLocation);
                string CC_Base = "";
                if (Result.Contains("<friendlyName>") == true)
                {
                    string[] Spl1, Spl2;
                    Spl1 = Regex.Split(Result, "<friendlyName>");
                    Spl2 = Regex.Split(Spl1[1], "</friendlyName>");
                    string CC_Name = Spl2[0];
                    Spl1 = Regex.Split(Result, "<modelName>");
                    Spl2 = Regex.Split(Spl1[1], "</modelName>");
                    string CC_Model = Spl2[0];
                    if (CC_Model != "Eureka Dongle") { goto NotCC; }
                    if (Result.Contains("<URLBase>") == true)
                    {
                        Spl1 = Regex.Split(Result, "<URLBase>");
                        Spl2 = Regex.Split(Spl1[1], "</URLBase>");
                        CC_Base = Spl2[0];
                    }
                    Spl1 = Regex.Split(Result, "<UDN>");
                    Spl2 = Regex.Split(Spl1[1], "</UDN>");
                    string CC_UID = Spl2[0].Replace("uuid:", "");

                    CCEvent.ChromeCast_BaseURL = CC_Base;
                    CCEvent.ChromeCast_IP = StrIP;
                    CCEvent.ChromeCast_Name = CC_Name;
                    CCEvent.ChromeCast_UID = CC_UID;
                    DiscoveredCC(this, CCEvent);


                    NotCC:;
                }
            }
            catch (Exception Ex)
            {
            CCEvent.ErrorMessage = Ex.Message;
            DiscoverError(this, CCEvent);
            }
        }




       public string GetLocalIP()
        {
            string strRet = String.Empty;
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    strRet = ip.ToString();
                    break;
                }
            return strRet;
        }



        public void DiscoverChromeCast()
        {
            if (SsDPThreads != null)
            {
                if (SsDPThreads.IsAlive == true)
                {
                    Dlisteners.Close();
                    CancelListen = true;
                    Thread.Sleep(500);
                    SsDPThreads.Abort();
                }
         }
            Thread SsDPThread = new Thread(SSDPReceive);
            SsDPThreads = SsDPThread;
            CancelListen = false;
            SsDPThread.Start();
        }



        public void StopDiscovery()
        {
            if (SsDPThreads != null)
            {
                if (SsDPThreads.IsAlive == true)
                {
                    Dlisteners.Close();
                    CancelListen = true;
                    Thread.Sleep(10);
                    SsDPThreads.Abort();
                }
            }
        }











    }
}
