using System;
using System.Net;

namespace DeskCastC
{
    class wClient
    {

        public event EventHandler<wClient> GotInit;
        public event EventHandler<wClient> InitError;
        public event EventHandler<wClient> GotChannel;
        public event EventHandler<wClient> ChannelError;
        public event EventHandler<wClient> GotDelReceiver;
        public event EventHandler<wClient> DelReceiverError;

        public string PlData { get; internal set; }
        public string ErrorMessage { get; internal set; }

        /*
        string encReceiver = "PCFET0NUWVBFIGh0bWw+DQo8aHRtbD4NCiAgPHNjcmlwdCBzcmM9Imh0dHBzOi8vd3d3LmdzdGF0aWMuY29tL2Nhc3QvanMvcmVjZWl2ZXIvMS4wL2Nhc3RfcmVjZWl2ZXIuanMiPjwvc2NyaXB0Pg0KICA8c2NyaXB0IHR5cGU9InRleHQvamF2YXNjcmlwdCI+DQoNCiAgICBjYXN0LnJlY2VpdmVyLmxvZ2dlci5zZXRMZXZlbFZhbHVlKDApOw0KICAgIHZhciByZWNlaXZlciA9IG5ldyBjYXN0LnJlY2VpdmVyLlJlY2VpdmVyKA0KICAgICAgICAnRmxpbmcnLCANCiAgICAgICAgW2Nhc3QucmVjZWl2ZXIuUmVtb3RlTWVkaWEuTkFNRVNQQUNFXSwNCiAgICAgICAgIiIsDQogICAgICAgIDUpOw0KICAgIHZhciByZW1vdGVNZWRpYSA9IG5ldyBjYXN0LnJlY2VpdmVyLlJlbW90ZU" +
        "1lZGlhKCk7DQogICAgcmVtb3RlTWVkaWEuYWRkQ2hhbm5lbEZhY3RvcnkoDQogICAgICAgIHJlY2VpdmVyLmNyZWF0ZUNoYW5uZWxGYWN0b3J5KGNhc3QucmVjZWl2ZXIuUmVtb3RlTWVkaWEuTkFNRVNQQUNFKSk7DQoNCiAgICByZWNlaXZlci5zdGFydCgpOw0KDQogICAgd2luZG93LmFkZEV2ZW50TGlzdGVuZXIoJ2xvYWQnLCBmdW5jdGlvbigpIHsNCiAgICAgIHZhciBlbGVtID0gZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoJ3ZpZCcpOw0KICAgICAgcmVtb3RlTWVkaWEuc2V0TWVkaWFFbGVtZW50KGVsZW0pOw0KDQogICAgICB2YXIgY2hlY2tTdGF0dXMgPSBmdW5jdGlvbigpIHsNCiAgICAgICAgdmFyIHN0YXR1cyA9IGRvY3VtZW50LmdldEVsZW1lbnRCeUlkKCdzdGF0dXMnKTsNCiAgICAgICAgdmFyIHN0ID0gcmVtb" +
        "3RlTWVkaWEuZ2V0U3RhdHVzKClbJ3N0YXRlJ107DQogICAgICAgIA0KICAgICAgICBpZiggc3QgPT0gMCB8fCByZW1vdGVNZWRpYS5nZXRTdGF0dXMoKVsnY3VycmVudF90aW1lJ10gPT0gMCApIHsNCiAgICAgICAgICAgIHN0YXR1cy5zdHlsZS5kaXNwbGF5ID0gJ2Jsb2NrJzsNCiAgICAgICAgfQ0KICAgICAgICBlbHNlIHsNCiAgICAgICAgICAgIGlmKCBzdCA9PSAxICYmIHJlbW90ZU1lZGlhLmdldFN0YXR1cygpWydjdXJyZW50X3RpbWUnXSA+IDAgKSB7DQogICAgICAgICAgICAgICAgc3RhdHVzLmlubmVySFRNTCA9ICdQYXVzZWQuLi4nOw0KICAgICAgICAgICAgICAgIHN0YXR1cy5zdHlsZS5kaXNwbGF5ID0gJ2Jsb2NrJzsNCiAgICAgICAgICAgIH0NCiAgICAgICAgICAgIGVsc2Ugew0KICAgICAgICAgICAgICAgIHN0YXR1" +
        "cy5pbm5lckhUTUwgPSByZW1vdGVNZWRpYS5nZXRTdGF0dXMoKVsnY3VycmVudF90aW1lJ107DQogICAgICAgICAgICAgICAgc3RhdHVzLnN0eWxlLmRpc3BsYXkgPSAnbm9uZSc7DQogICAgICAgICAgICAgICAgZWxlbS5zdHlsZS5kaXNwbGF5ID0gJ2Jsb2NrJzsNCiAgICAgICAgICAgIH0NCiAgICAgICAgfQ0KICAgICAgfQ0KICAgICAgc2V0SW50ZXJ2YWwoY2hlY2tTdGF0dXMsIDEwMDApOw0KICAgICAgDQogICAgfSk7DQogIDwvc2NyaXB0Pg0KICA8dGl0bGU+TWVkaWEgUGxheWVyIEFwcDwvdGl0bGU+DQogIDxib2R5Pg0KICAgIDx2aWRlbyBpZD0idmlkIg0KICAgICAgICAgICBzdHlsZT0icG9zaXRpb246YWJzb2x1dGU7dG9wOjA7bGVmdDowO2hlaWdodDoxMDAlO3dpZHRoOjEwMCUiPg0KICAgIDxkaXYgaWQ9InN0YXR1cyIgc3R5bG" +
        "U9ImRpc3BsYXk6bm9uZTsgZm9udC1zaXplOjMwMCU7IHBvc2l0aW9uOmFic29sdXRlO3RvcDo0MCU7bGVmdDo0MCU7Ij4NCiAgICA8L2Rpdj4NCiAgPC9ib2R5Pg0KPC9odG1sPg==";
        */

        public void PostInit(string LocalIP, string ChromeCastIP, string CC_Receiver, string LocalPort)
        {
            wClient ClientEvent = new wClient();
            try
            {
                WebClient oWeb = new WebClient();
                oWeb.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                oWeb.Headers.Add("Origin", "http://" + LocalIP);
                string Result = oWeb.UploadString("http://" + ChromeCastIP + ":8008/apps/" + CC_Receiver, "POST", "http://" + LocalIP + ":" + LocalPort + "/receiver.html");
                //  string Result = oWeb.UploadString("http://" + ChromeCastIP + ":8008/apps/" + CC_Receiver, "POST", "data:text/html;base64," + encReceiver);
                ClientEvent.PlData = Result;
                GotInit(this, ClientEvent);
            }
            catch (Exception ex)
            {
                ClientEvent.ErrorMessage = ex.Message;
                InitError(this, ClientEvent);
            }
        }




        public void PostChannel(string LocalIP, string ChromeCastIP, string CC_Receiver, string ChromeCastUID)
        {
            wClient ClientEvent = new wClient();
            try
            {
                WebClient oWeb = new WebClient();
                oWeb.Headers.Add("Content-Type", "application/json");
                oWeb.Headers.Add("Origin", "http://" + LocalIP);
                string Result = oWeb.UploadString("http://" + ChromeCastIP + ":8008/connection/" + CC_Receiver, "POST", "{\"channel\":0,\"senderId\":{\"appName\":\"" + CC_Receiver + "\", \"senderId\":\"" + ChromeCastUID + "\"}}");
                ClientEvent.PlData = Result;
                GotChannel(this, ClientEvent);
            }
            catch (Exception ex)
            {
                ClientEvent.ErrorMessage = ex.Message;
                ChannelError(this, ClientEvent);
            }
        }



        public void DelReceiver(string LocalIP, string ChromeCastIP, string CC_Receiver)
        {
            wClient ClientEvent = new wClient();
            try
            {
                WebClient oWeb = new WebClient();
                oWeb.Headers.Add("Origin", "http://" + LocalIP);
                string Result = oWeb.UploadString("http://" + ChromeCastIP + ":8008/apps/" + CC_Receiver, "DELETE", "");
                ClientEvent.PlData = Result;
                GotDelReceiver(this, ClientEvent);
            }
            catch (Exception ex)
            {
                ClientEvent.ErrorMessage = ex.Message;
                DelReceiverError(this, ClientEvent);
            }
        }
        

    }
}
