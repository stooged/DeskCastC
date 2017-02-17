using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Win32;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DeskCastC
{
    public partial class Form1 : Form
    {
        Process gProcess;
        wClient WC1 = new wClient();
        wSocket WS1 = new wSocket();
        wServer WebSvr1 = new wServer();
        cDiscover DSC1 = new cDiscover();
        Utils UTL1 = new Utils();

        bool PauseTimeTrack, PauseVolTrack, IsRooted, IsTranscoding, IsLocalStream;
        string LocalIP, TrancodeFilePath, VideoTitle, TempFilePath, ChromeCastUID, ChromeCastIP, ResumePosition;
        int LocalPort, TranscodePort, TransCodeDuration, pDuration;



        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            DSC1.DiscoveredCC += DSC1_DiscoveredCC;
            DSC1.DiscoverError += DSC1_DiscoverError;
            WebSvr1.StatusMessage += WebSvr1_StatusMessage;

            WC1.GotInit += WC1_GotInit;
            WC1.InitError += WC1_InitError;
            WC1.GotChannel += WC1_GotChannel;
            WC1.ChannelError += WC1_ChannelError;
            WC1.GotDelReceiver += WC1_GotDelReceiver;
            WC1.DelReceiverError += WC1_DelReceiverError;

            WS1.WsConnected += WS1_WsConnected;
            WS1.WsClosed += WS1_WsClosed;
            WS1.WsError += WS1_WsError;
            WS1.WsMessageReceived += WS1_WsMessageReceived;
            WS1.WsDataReceived += WS1_WsDataReceived;

            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            string result = "False";
            if (Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\DeskCastC", "ROOTED", "False") != null)
            {
                result = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\DeskCastC", "ROOTED", "False").ToString();
            }
            else
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\DeskCastC", "ROOTED", "False");
            }
            IsRooted = Convert.ToBoolean(result);

            if (IsRooted == false)
            {
                RadioButton1.Checked = true;
                RadioButton2.Checked = false;
            }
            else
            {
                RadioButton2.Checked = true;
                RadioButton1.Checked = false;
            }

            this.Show();

            LocalIP = DSC1.GetLocalIP();

            Random RPort = new Random();
            LocalPort = RPort.Next(9000, 9500);
            TranscodePort = RPort.Next(9600, 10100);
            AppLog("Local IP: " + LocalIP);
            WebSvr1.StartServer(LocalIP, LocalPort);

            DSC1.DiscoverChromeCast();
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\DeskCastC", "ROOTED", IsRooted.ToString());
                DSC1.StopDiscovery();
                WebSvr1.CleanTempFiles();
                if (BackgroundWorker1.IsBusy == true) { BackgroundWorker1.CancelAsync(); }
                gProcess.Kill();
                Environment.Exit(0);
            }
            catch
            {
                Environment.Exit(0);
            }
        }



        void SetState(int cState)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(SetState), new object[] { cState });
                return;
            }
            TxtState.Text = cState.ToString();
            if (cState == 1)
            {
                Timer2.Start();
            }
            else
            {
                Timer2.Stop();
            }
        }




        void AppLog(string strText)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppLog), new object[] { strText });
                return;
            }
            if (CheckBox1.Checked == true)
            {
                TextBox1.AppendText(strText + Environment.NewLine);
            }
        }


        void TranscodeLog(string strText)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(TranscodeLog), new object[] { strText });
                return;
            }
            if (checkBox2.Checked == true)
            {
                TextBox1.AppendText(strText + Environment.NewLine);
            }
        }


        void AddItem(string cItem)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AddItem), new object[] { cItem });
                return;
            }
            for (int i = 0; i < ComboBox1.Items.Count - 1; i++)
            {
                if (ComboBox1.Items[0].ToString() == cItem)
                {
                    goto IsListed;
                }
            }
            ComboBox1.Items.Add(cItem);
            IsListed:
            if (ComboBox1.Text.Length == 0) { ComboBox1.Text = ComboBox1.Items[0].ToString(); }
        }



        void TrackTime(int tMax, int tValue)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int,int>(TrackTime), new object[] { tMax, tValue } );
                return;
            }
            if (PauseTimeTrack == false)
            {
                if (IsTranscoding == true)
                {
                    tMax = TransCodeDuration;
                }
                if (tMax < tValue) { tMax = tValue; }
                if (tMax > 0) { TrackBar1.Maximum = tMax; }
                if (tValue > 0) { TrackBar1.Value = tValue; }
            }
        }



        void TrackVolume(int tLevel)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(TrackVolume), new object[] { tLevel });
                return;
            }
            if (PauseVolTrack == false)
            {
                TrackBar2.Value = tLevel;
            }
        }

        

        void DSC1_DiscoveredCC(object sender, cDiscover e)
        {
            AppLog("Found: " + e.ChromeCast_Name + " / " + e.ChromeCast_IP + " / " + e.ChromeCast_UID);
            AddItem(e.ChromeCast_Name + " / " + e.ChromeCast_IP + " / " + e.ChromeCast_UID);
        }

        void DSC1_DiscoverError(object sender, cDiscover e)
        {
            AppLog("error: " + e.ErrorMessage);
        }



        void WebSvr1_StatusMessage(object sender, wServer e)
        {
            AppLog(e.TheMessage);
        }


        void WC1_GotInit(object sender, wClient e)
        {
            AppLog(e.PlData);
            Timer1.Start();
        }


        void WC1_InitError(object sender, wClient e)
        {
            AppLog(e.ErrorMessage);
        }

        void WC1_GotChannel(object sender, wClient e)
        {
            string[] Spl1, Spl2;
            if (e.PlData.Contains("\"URL\":\"ws") == true)
            {
                Timer1.Enabled = false;
                AppLog(e.PlData);
                Spl1 = Regex.Split(e.PlData, "\"URL\":\"");
                Spl2 = Regex.Split(Spl1[1], "\"");
                UTL1.CmdID = 0;
                WS1.ConnectWs(Spl2[0]);
            }
        }

        void WC1_ChannelError(object sender, wClient e)
        {
        }
        void WC1_GotDelReceiver(object sender, wClient e)
        {
        }
        void WC1_DelReceiverError(object sender, wClient e)
        {
        }




        void WS1_WsConnected(object sender, wSocket e)
        {
            AppLog("Connected");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            WC1.PostChannel(LocalIP,ChromeCastIP, UTL1.CC_Receiver() ,ChromeCastUID );
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (WS1.IsSocketOpen() == false) { SetState(0); }
            WS1.SendMessage(UTL1.Status());
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            if (BackgroundWorker1.CancellationPending == false)
            {
                Timer3.Stop();
                BackgroundWorker1.WorkerSupportsCancellation = true;
                BackgroundWorker1.RunWorkerAsync();
         }
        }

        private void Timer4_Tick(object sender, EventArgs e)
        {
            Timer4.Stop();
            if (IsLocalStream == true)
            {
                LoadLocalStream(TempFilePath);
            }
            else
            {
                LoadNetWorkStream(TempFilePath);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            WS1.SendMessage(UTL1.Pause());
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            WS1.SendMessage(UTL1.ResumePlay(ResumePosition));
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            Label1.Text = TimeSpan.FromSeconds(TrackBar1.Value).ToString();
        }

        private void TrackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            PauseTimeTrack = true;
        }

        private void TrackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            WS1.SendMessage(UTL1.ResumePlay(ResumePosition));
            PauseTimeTrack = false;
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            Label1.Text = TimeSpan.FromSeconds(TrackBar1.Value).ToString();
            Label2.Text = TimeSpan.FromSeconds(TrackBar1.Maximum).ToString();
            ResumePosition = TrackBar1.Value.ToString();
        }

        private void TrackBar2_MouseDown(object sender, MouseEventArgs e)
        {
            PauseVolTrack = true;
        }

        private void LoadFileFromURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string StrDef = Clipboard.GetText();
            if (StrDef.Substring(0, 4) != "http") { StrDef = ""; }
            Form2 InputBox = new Form2();
            if (InputBox.ShowDialog(this) == DialogResult.OK)
            {
                string Result = InputBox.textBox1.Text;
                if (Result.Contains("http") == true)
                {
                    LoadNetWorkStream(Result);
                }
            }
            InputBox.Dispose();
        }

        private void SelectFileFromComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.FileName = "";
            OpenFileDialog1.Filter = "Video Files|*.*";
            OpenFileDialog1.Multiselect = false;
            OpenFileDialog1.Title = "Select Video File To Cast";
            OpenFileDialog1.ShowDialog();
            LoadLocalStream(OpenFileDialog1.FileName);
        }

        private void RadioButton1_Click(object sender, EventArgs e)
        {
            IsRooted = false;
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\DeskCastC", "ROOTED", IsRooted.ToString());
        }

        private void RadioButton2_Click(object sender, EventArgs e)
        {
            IsRooted = true;
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\DeskCastC", "ROOTED", IsRooted.ToString());
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] theFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string theFile in theFiles)
                {
                    LoadLocalStream(theFile);
                    break;
                }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (Button5.Text == "Mute")
            {
                WS1.SendMessage(UTL1.Mute());
            }
            else
            {
                WS1.SendMessage(UTL1.UnMute());
            }
        }

        private void TrackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            if (TrackBar2.Value == 10)
            {
                WS1.SendMessage(UTL1.Volume("1"));
                    }
            else
            {
                WS1.SendMessage(UTL1.Volume("0." + TrackBar2.Value));
            }
            PauseVolTrack = false;
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            ComboBox1.Items.Clear();
            ComboBox1.Text = String.Empty;
            DSC1.DiscoverChromeCast();
        }

        void WS1_WsClosed(object sender, wSocket e)
        {
            AppLog("Closed");
            Label3.Text = "";
            SetState(0);
        }
        void WS1_WsError(object sender, wSocket e)
        {
            AppLog("Error");
            Label3.Text = "";
        }
        void WS1_WsMessageReceived(object sender, wSocket e)
        {
            AppLog(e.StrMessage);

            if (e.StrMessage.Contains("\"type\":\"STATUS\"") == true || e.StrMessage.Contains("\"type\":\"RESPONSE\"") == true)
            {
                string[] Spl1, Spl2;
                int pTime;
                if (e.StrMessage.Contains("\"state\":") == true)
                {
                    Spl1 = Regex.Split(e.StrMessage, "\"state\":");
                    int pState = Convert.ToInt32(Spl1[1].Substring(0, 1));
                    if (pState == 2)
                    {
                           SetState(1);
                    }
                    else
                    {
                           SetState(0);
                    }
                }



                if (e.StrMessage.Contains("\"duration\":") == true)
                {
                    Spl1 = Regex.Split(e.StrMessage, "\"duration\":");
                    Spl2 = Regex.Split(Spl1[1], ",");
                    if (Spl2[0] == "null") { Spl2[0] = "0"; }
                    pDuration = Convert.ToInt32(Convert.ToDouble(Spl2[0]));
                }



                if (e.StrMessage.Contains("\"current_time\":") == true)
                {
                    Spl1 = Regex.Split(e.StrMessage, "\"current_time\":");
                    Spl2 = Regex.Split(Spl1[1], ",");
                    if (Spl2[0] == "null") { Spl2[0] = "0"; }
                    pTime = Convert.ToInt32(Convert.ToDouble(Spl2[0]));
                    TrackTime(pDuration, pTime);
                }



                if (e.StrMessage.Contains("\"volume\":") == true)
                {
                    Spl1 = Regex.Split(e.StrMessage, "\"volume\":");
                    Spl2 = Regex.Split(Spl1[1], ",");
                    if (Spl2[0] == "null") { Spl2[0] = "0"; }
                    if (Spl2[0].Length >= 3)
                    {
                        Spl2[0] = Spl2[0].Substring(2, 1);
                    }
                    else
                    {
                        if (Spl2[0] == "1") { Spl2[0] = "10"; }
                    }
                    int pVolume = Convert.ToInt32(Spl2[0]);
                    TrackVolume(pVolume);
                }



                if (e.StrMessage.Contains("\"muted\":") == true)
                {
                    Spl1 = Regex.Split(e.StrMessage, "\"muted\":");
                    Spl2 = Regex.Split(Spl1[1], ",");
                    bool pMuteState = Convert.ToBoolean(Spl2[0]);
                    if (pMuteState == false)
                    {
                        Button5.Text = "Mute";
                    }
                    else
                    {
                        Button5.Text = "Unmute";
                    }
                }
            }
        }


        void WS1_WsDataReceived(object sender, wSocket e)
        {
        }


        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            StartTransCodeServer();
        }


        private void StartTransCodeServer()
        {
            try
            {
                Process tProcess = new Process();
                ProcessStartInfo startinfo = new ProcessStartInfo();
                gProcess = tProcess;
                startinfo.FileName = UTL1.Transcoder;
                startinfo.Arguments = " -i \"" + TrancodeFilePath + "\" -vcodec libx264 -r 25 -b:v 3000k -s 1280x720 -ar 44100 -ac 2 -f matroska tcp://" + LocalIP + ":" + TranscodePort + "?listen";
                startinfo.UseShellExecute = false;
                startinfo.RedirectStandardError = true;
                startinfo.RedirectStandardOutput = true;
                startinfo.CreateNoWindow = true;
                tProcess.StartInfo = startinfo;
                tProcess.ErrorDataReceived += Proc_DataReceived;
                tProcess.OutputDataReceived += Proc_DataReceived;
                tProcess.EnableRaisingEvents = true;
                tProcess.Start();
                AppLog("Transcode Server Enabled");
                tProcess.BeginOutputReadLine();
                tProcess.BeginErrorReadLine();
                tProcess.WaitForExit();
                AppLog("Transcode Server Disabled");
            }
            catch { }
        }



        void Proc_DataReceived(object sender, DataReceivedEventArgs e)
        {        
            string ProcData = e.Data;
            try
            {
                TranscodeLog(ProcData);
                if (ProcData.Contains("Duration: ") == true)
                {
                    string[] Spl1, Spl2;
                    Spl1 = Regex.Split(e.Data, "Duration: ");
                    Spl2 = Regex.Split(Spl1[1], ",");
                    TransCodeDuration = Convert.ToInt32(TimeSpan.Parse(Spl2[0].Trim()).TotalSeconds);
                    Thread.Sleep(300);
                    WS1.SendMessage(UTL1.LoadPlayURL(VideoTitle, "http://" + LocalIP + ":" + TranscodePort));
                }
            }
            catch { }
        }



        void LoadLocalStream(string LocalFile)
        {
            if (LocalFile.Contains(".mp4") == true || LocalFile.Contains(".avi") == true || LocalFile.Contains(".mkv") == true || LocalFile.Contains(".flv") == true || LocalFile.Contains(".mpg") == true || LocalFile.Contains(".webm") == true || LocalFile.Contains(".divx") == true || LocalFile.Contains(".mov") == true || LocalFile.Contains(".m4v") == true || LocalFile.Contains(".wmv") == true)
            {
                if (WS1.IsSocketOpen() == false)
                {
                    IsLocalStream = true;
                    TempFilePath = LocalFile;
                    ConnectChromecast();
                    Timer4.Start();
                    return;
                }

                WebSvr1.CleanTempFiles();
                if (IsTranscoding == true)
                {
                     if (BackgroundWorker1.IsBusy == true) {BackgroundWorker1.CancelAsync();}
                     if (gProcess.HasExited == false) { gProcess.Kill();}
                }
                VideoTitle = LocalFile;
                VideoTitle = Regex.Split(LocalFile, @"\\")[Regex.Split(LocalFile, @"\\").Length - 1];

                Label3.Text = VideoTitle;

                if (LocalFile.Contains(".mp4") == true)
                {
                    IsTranscoding = false;
                    AppLog("Streaming: " + LocalFile);
                    WebSvr1.VideoFile(LocalFile);
                    WS1.SendMessage(UTL1.LoadPlayURL(VideoTitle, "http://" + LocalIP + ":" + LocalPort + "/video.mp4"));
                }
                else
                {
                    Thread.Sleep(1000);
                    AppLog("Transcoding: " + LocalFile);
                    TrancodeFilePath = LocalFile;
                    if (IsTranscoding == true && BackgroundWorker1.IsBusy == true)
                    {
                        if (BackgroundWorker1.CancellationPending == true)
                        {
                            Timer3.Start();
                        }
                        return;
                    }
                    IsTranscoding = true;
                    BackgroundWorker1.WorkerSupportsCancellation = true;
                    BackgroundWorker1.RunWorkerAsync();
                }
            }
        }




        void LoadNetWorkStream(string Networkfile)
        {
            if (Networkfile.Contains(".mp4") == true || Networkfile.Contains(".avi") == true || Networkfile.Contains(".mkv") == true || Networkfile.Contains(".flv") == true || Networkfile.Contains(".mpg") == true || Networkfile.Contains(".webm") == true || Networkfile.Contains(".divx") == true || Networkfile.Contains(".mov") == true || Networkfile.Contains(".m4v") == true || Networkfile.Contains(".wmv") == true)      {
                if (WS1.IsSocketOpen() == false)
                {
                    IsLocalStream = false;
                    TempFilePath = Networkfile;
                    ConnectChromecast();
                    Timer4.Start();
                    return;
                }
                WebSvr1.CleanTempFiles();
                if (IsTranscoding == true)
                {
                    if (BackgroundWorker1.IsBusy == true) { BackgroundWorker1.CancelAsync(); }
                    if (gProcess.HasExited == false) { gProcess.Kill(); }
                }
                string VideoURL = Networkfile;
                string Videofile = Regex.Split(VideoURL, "/")[Regex.Split(VideoURL, "/").Length - 1];
                Label3.Text = Videofile;
                if (Videofile.Substring(Videofile.Length - 4, 4).ToLower() == ".mp4")
                {
                    IsTranscoding = false;
                    VideoTitle = Videofile;
                    WS1.SendMessage(UTL1.LoadPlayURL(VideoTitle, VideoURL));
                }
                else
                {
                    Thread.Sleep(1000);
                    TrancodeFilePath = VideoURL;
                    VideoTitle = Videofile;
                    AppLog("Transcoding: " + VideoURL);
                    if (IsTranscoding == true && BackgroundWorker1.IsBusy == true)
                    {
                        if (BackgroundWorker1.CancellationPending == true)
                        {
                            Timer3.Start();
                        }
                        return;
                    }
                    IsTranscoding = true;
                    BackgroundWorker1.WorkerSupportsCancellation = true;
                    BackgroundWorker1.RunWorkerAsync();
                }
            }
        }



        void ConnectChromecast()
        {
            if (ComboBox1.Text == String.Empty) {return;}
            if (WS1.IsSocketOpen() == false)
            {
                ChromeCastIP = Regex.Split(ComboBox1.Text, " / ")[1];
                ChromeCastUID = Regex.Split(ComboBox1.Text, " / ")[2];
                WC1.PostInit(LocalIP, ChromeCastIP, UTL1.CC_Receiver(), LocalPort.ToString());
            }
        }


        void DisConnectChromecast()
        {
            Timer1.Stop();
            WC1.DelReceiver(LocalIP, ChromeCastIP, UTL1.CC_Receiver());
        }



        private void Button1_Click(object sender, EventArgs e)
        {
            ConnectChromecast();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DisConnectChromecast();
        }


    }
}
