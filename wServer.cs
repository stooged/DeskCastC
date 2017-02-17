using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace DeskCastC
{
    class wServer
    {
        public event EventHandler<wServer> StatusMessage;

        public string TheMessage { get; internal set; }

        Utils UTL1 = new Utils();

        string FilePath;
        long fileLength;
        int ServerPort;
        string LocalIP;



        public void WebServerThread()
        {
            wServer ServerEvent = new wServer();
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                soc.Bind(new IPEndPoint(IPAddress.Any, ServerPort));
                soc.Listen(10);

                ServerEvent.TheMessage = "Stream Server Started: " + LocalIP + ":" + ServerPort;
                StatusMessage(this, ServerEvent);

                while (true)
                {
                    AsyncCallback ScallBack;
                    AsyncCallback HcallBack;
                    Socket client = soc.Accept();
                    Byte[] RecBuffer = new byte[client.ReceiveBufferSize];
                    string PlayerData = string.Empty;
                    client.Receive(RecBuffer);
                    string ReceiveData = Encoding.ASCII.GetString(RecBuffer);

                    ServerEvent.TheMessage = ReceiveData;
                    StatusMessage(this, ServerEvent);

                    if (ReceiveData.Contains("video.mp4") == true)
                    {
                        int StartPosition = 0;


                        if (ReceiveData.Contains("Range: bytes=") == true)
                        {
                            string[] Spl1, Spl2;
                            Spl1 = Regex.Split(ReceiveData, "Range: bytes=");
                            Spl2 = Regex.Split(Spl1[1], "-");
                            StartPosition = Convert.ToInt32(Spl2[0]);
                        }

                        if (FilePath.Length == 0) { NoFile(client); return; }
                        if (File.Exists(FilePath) == false)
                        {

                            ServerEvent.TheMessage = FilePath + " Not Found";
                            StatusMessage(this, ServerEvent);
                            NoFile(client);
                            return;
                        }

   
                        fileLength = new FileInfo(FilePath).Length;
                        ServerEvent.TheMessage = "Streaming: " + FilePath + " - " + client.RemoteEndPoint.ToString();
                        StatusMessage(this, ServerEvent);

                        string StreamHeader = "HTTP/1.1 206 Partial Content" + Environment.NewLine +
                                                   "Server: DeskCast" + Environment.NewLine +
                                                   "Accept-Ranges: bytes" + Environment.NewLine +
                                                   "Content-Length: " + (fileLength - StartPosition) + Environment.NewLine +
                                                   "Content-Range: bytes " + StartPosition + "-" + (fileLength - 1) + "/" + fileLength + Environment.NewLine +
                                                   "Date: " + DateTime.Now.ToString("R") + Environment.NewLine +
                                                   "Connection: Close" + Environment.NewLine +
                                                   "Content-Type: video/mp4" + Environment.NewLine + Environment.NewLine;


                        client.Send(Encoding.ASCII.GetBytes(StreamHeader), Encoding.ASCII.GetBytes(StreamHeader).Length, SocketFlags.None);
                        ScallBack = new AsyncCallback(FileSent);

                        int bytesRead = int.MaxValue;
                        Byte[] buffer = new byte[fileLength - StartPosition];
                        FileStream inFile = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                        inFile.Seek(StartPosition, SeekOrigin.Begin);
                        bytesRead = inFile.Read(buffer, 0, buffer.Length);
                        string TmpFile = Application.UserAppDataPath + @"\" + UTL1.MD5Hash(UTL1.GetUnixTimestamp().ToString()) + ".tmp";
                        FileStream outFile = new FileStream(TmpFile, FileMode.Create, FileAccess.Write);
                        outFile.Write(buffer, 0, bytesRead);
                        Thread.Sleep(100);
                        outFile.Close();
                        inFile.Close();
                        client.BeginSendFile(TmpFile, ScallBack, null);
                        buffer = null;
                    }
                    else
                    {

                        if (ReceiveData.Substring(0, 4) == "GET " || ReceiveData.Substring(0, 5) == "POST ")
                        {
                            string[] Spl1, Spl2;
                            string FPath, FType = "";
                            long FLength;

                            ReceiveData = ReceiveData.Replace("POST ", "GET ");

                            Spl1 = Regex.Split(ReceiveData, "GET ");
                            Spl2 = Regex.Split(Spl1[1], " HTTP");
                            Spl1 = Regex.Split(Spl2[0], "/");


                            string StrReqFile = Spl1[Spl1.Length - 1];

                            if (StrReqFile.Length == 0) { NoFile(client); return; }

                            FPath = Application.StartupPath + @"\data\" + StrReqFile;

                            if (FPath.Length == 0) { NoFile(client); return; }


                            if (File.Exists(FPath) == false)
                            {
                                ServerEvent.TheMessage = FPath + " Not Found";
                                StatusMessage(this, ServerEvent);
                                NoFile(client);
                                return;
                            }

                            Spl1 = Regex.Split(StrReqFile, @"\.");
                            switch (Spl1[1])
                            {
                                case "js":
                                    FType = "application/javascript";
                                    break;
                                case "html":
                                    FType = "text/html";
                                    break;
                                case "css":
                                    FType = "text/css";
                                    break;
                                case "png":
                                    FType = "image/png";
                                    break;
                                case "gif":
                                    FType = "image/gif";
                                    break;
                            }


                            if (StrReqFile == "player.js")
                            {
                                PlayerData = UTL1.GetFile(FPath);
                                PlayerData = PlayerData.Replace("$RECEIVER$NAME$", UTL1.CC_Receiver());
                                FLength = PlayerData.Length;
                            }
                            else
                            {
                                FLength = new FileInfo(FPath).Length;
                            }

                            string htmlHeader = "HTTP/1.1 200 OK" + Environment.NewLine +
                                                     "Server: DeskCast" + Environment.NewLine +
                                                     "Date: " + DateTime.Now.ToString("R") + Environment.NewLine +
                                                     "Connection: close" + Environment.NewLine +
                                                    "Content-Length: " + FLength + Environment.NewLine +
                                                    "Content-Type: " + FType + Environment.NewLine + Environment.NewLine;

                            if (StrReqFile == "player.js")
                            {
                                client.Send(Encoding.ASCII.GetBytes(htmlHeader + PlayerData), Encoding.ASCII.GetBytes(htmlHeader + PlayerData).Length, SocketFlags.None);
                            }
                            else
                            {
                                client.Send(Encoding.ASCII.GetBytes(htmlHeader), Encoding.ASCII.GetBytes(htmlHeader).Length, SocketFlags.None);
                                HcallBack = new AsyncCallback(HtmlSent);
                                client.BeginSendFile(FPath, HcallBack, null);
                            }

                            goto FoundFile;

                        }
                        FoundFile:;
                    }
                }
            }
            catch (Exception ex)
            {
                ServerEvent.TheMessage = "ERROR: " + ex.Message;
                StatusMessage(this, ServerEvent);
            }
        }




        void NoFile(Socket cli)
        {
            wServer ServerEvent = new wServer();
            string htmlBody = "<html><head><title>HTTP 404 Not Found</title></head><body><table width='400' cellpadding='3' cellspacing='5'><tr><td id='tableProps' valign='top' align='left'></td><td id='tableProps2' align='left' valign='middle' width='360'><h1 id='errortype' style='COLOR: black; FONT: 13pt/15pt verdana'><span id='errorText'>The page cannot be found</span></h1> </td></tr><tr><td id='tablePropsWidth' width='400' colspan='2'><font style='COLOR: black; FONT: 8pt/11pt verdana'>The page you are looking for might have been removed, had its name changed, or is temporarily unavailable.</font></td></tr></table></body></html>";
            string ErrorHeader = "HTTP/1.1 404 Not Found" + Environment.NewLine +
                   "Server: DeskCast" + Environment.NewLine +
                   "Date: " + DateTime.Now.ToString("R") + Environment.NewLine +
                   "Connection: Close" + Environment.NewLine +
                   "Content-Length: " + htmlBody.Length.ToString() + Environment.NewLine +
                   "Content-Type: text/html" + Environment.NewLine + Environment.NewLine + htmlBody;
            ServerEvent.TheMessage = "404 Not Found - " + cli.RemoteEndPoint.ToString();
            StatusMessage(this, ServerEvent);
            cli.Send(Encoding.ASCII.GetBytes(ErrorHeader), Encoding.ASCII.GetBytes(ErrorHeader).Length, SocketFlags.None);
        }




        public void StartServer(string ListenIP, int ListenPort)
        {
            LocalIP = ListenIP;
            Thread WebServer = new Thread(WebServerThread);
            if (WebServer.ThreadState == ThreadState.Unstarted || WebServer.ThreadState == ThreadState.Stopped)
            {
                ServerPort = ListenPort;
                WebServer.Start();
            }
        }




        public void VideoFile(string StrFilePath)
        {
            FilePath = StrFilePath;
        }




        void FileSent(IAsyncResult result)
        {
            wServer ServerEvent = new wServer();
            CleanTempFiles();
            ServerEvent.TheMessage = "Stream Finished";
            StatusMessage(this, ServerEvent);
        }




        void HtmlSent(IAsyncResult result)
        {
            wServer ServerEvent = new wServer();
            ServerEvent.TheMessage = "HTML Object Sent";
            StatusMessage(this, ServerEvent);
        }




        public void CleanTempFiles()
        {
            foreach (string filename in Directory.GetFiles(Application.UserAppDataPath, "*.tmp"))
            {
                if (UTL1.FileInUse(filename) == false)
                {
                    File.Delete(filename);
                }
            }
        }


    }
}
