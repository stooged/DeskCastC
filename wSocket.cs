using System;
using System.Text;
using WebSocket4Net;

namespace DeskCastC
{
    class wSocket
    {

        public event EventHandler<wSocket> WsConnected;
        public event EventHandler<wSocket> WsClosed;
        public event EventHandler<wSocket> WsError;
        public event EventHandler<wSocket> WsMessageReceived;
        public event EventHandler<wSocket> WsDataReceived;

        public string StrMessage { get; internal set; }
        public string StrData { get; internal set; }

        WebSocket wSock; // uses WebSocket4Net.dll
        //https://websocket4net.codeplex.com/


        public void ConnectWs(string TheWsUrl, string TheProtocol = "")
        {
            WebSocket wSckt = new WebSocket(TheWsUrl, TheProtocol, WebSocketVersion.Rfc6455);
            wSock = wSckt;
            wSock.EnableAutoSendPing = false;
            wSock.Opened += new EventHandler(socketOpened);
            wSock.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(socketError);           
            wSock.Closed += new EventHandler(socketClosed);
            wSock.MessageReceived += new EventHandler<MessageReceivedEventArgs>(socketMessage);
            wSock.DataReceived += new EventHandler<DataReceivedEventArgs>(socketDataReceived);
            wSock.Open();
        }


        public void CloseWs()
        {
            wSock.Close();
            wSock = null;
        }


        public void SendMessage(string TheMessage)
        {
            if (IsSocketOpen() == true)
            {
                wSock.Send(TheMessage);
            }
        }


        public bool IsSocketOpen()
        {
           if (wSock == null){return false;}
            if (wSock.State == WebSocketState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        void socketOpened(object sender, EventArgs e)
        {
            wSocket SocketEvent = new wSocket();
            WsConnected(this, SocketEvent);
        }


        void socketClosed(object sender, EventArgs e)
        {
            wSocket SocketEvent = new wSocket();
            WsClosed(this, SocketEvent);
        }



        void socketError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            wSocket SocketEvent = new wSocket();
            WsError(this, SocketEvent);
        }


        void socketMessage(object sender, MessageReceivedEventArgs e)
        {
            if (e.Message == "[\"cm\",{\"type\":\"ping\"}]")
            {
                SendMessage("[\"cm\",{\"type\":\"pong\"}]");
            }
            else
            {
                wSocket SocketEvent = new wSocket();
                SocketEvent.StrMessage = e.Message;
                WsMessageReceived(this, SocketEvent);
            }
        }


        void socketDataReceived(object sender, DataReceivedEventArgs e)
        {
            wSocket SocketEvent = new wSocket();
            SocketEvent.StrData = Encoding.Default.GetString(e.Data);
            WsDataReceived(this, SocketEvent);
        }

        
    }
}
