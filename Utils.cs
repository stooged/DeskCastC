using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace DeskCastC
{
    class Utils
    {

        /*##############private whitelist server sql entry################

        INSERT INTO `pwl-custom_apps` (`ID`, `name`, `v2app`, `test_app`, `content`) VALUES(4, 'DeskCast', 0, 0, '{"use_channel":true,"allow_empty_post_data":true,"app_id":"DeskCast","url":"${POST_DATA}","dial_enabled":true}');

        ################################################################*/

        public int CmdID;
        public string Transcoder = "ffmpeg.dll";// .exe renamed to .dll
        // http://ffmpeg.zeranoe.com/builds/win32/static/ffmpeg-20140919-git-33c752b-win32-static.7z

       
        public string CC_Receiver()
        {
            bool IsRooted = Convert.ToBoolean(Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\DeskCastC", "ROOTED", "false").ToString());
            if (IsRooted == false)
            {
                return "18a8aeaa-8e3d-4c24-b05d-da68394a3476_1"; // should work with non rooted chromecasts(does not use local receiver files in data directory)
            }
            else
            {
                return "Fling"; // should work with rooted chromecasts and team eureka custom whitelist
               // return = "DeskCast"; // private whitelist server
            }
        }


        public string LoadPlayURL(string VideoTitle , string VideoURL )
        {
            CmdID = CmdID + 1;
            return "[\"ramp\",{\"title\":\"" + VideoTitle +  "\",\"src\":\"" + VideoURL + "\",\"type\":\"LOAD\",\"cmd_id\":" + CmdID + ",\"autoplay\":true}]";
            
        }

        public string Status()
        {
            CmdID = CmdID + 1;
            return "[\"ramp\",{\"type\":\"INFO\",\"cmd_id\":" + CmdID + "}]";
        }

        public string Pause()
        {
            CmdID = CmdID + 1;
            return "[\"ramp\",{\"type\":\"STOP\",\"cmd_id\":" + CmdID + "}]";
        }

        public string ResumePlay(string VideoPosition)
        {
            CmdID = CmdID + 1;
            return "[\"ramp\",{\"position\":" + VideoPosition + ",\"type\":\"PLAY\",\"cmd_id\":" + CmdID + "}]";
        }

        public string Play()
        {
            CmdID = CmdID + 1;
            return "[\"ramp\",{\"type\":\"PLAY\",\"cmd_id\":" + CmdID + "}]";
        }

        public string Mute()
        {
            CmdID = CmdID + 1;
            return "[\"ramp\",{\"type\":\"VOLUME\",\"cmd_id\":" + CmdID + ",\"muted\":true}]";
        }

        public string UnMute()
        {
            CmdID = CmdID + 1;
            return "[\"ramp\",{\"type\":\"VOLUME\",\"cmd_id\":" + CmdID + ",\"muted\":false}]";
        }

        public string Volume(string VolumeLevel)
        {
            CmdID = CmdID + 1;
            return "[\"ramp\",{\"volume\":" + VolumeLevel + ",\"type\":\"VOLUME\",\"cmd_id\":" + CmdID + "}]";
        }



        public bool FileInUse(string StrFileName)
        {
            if (File.Exists(StrFileName) == false)
            {
                return false;
            }
            Stream stream = null;
            try
            {
                stream = new FileStream(StrFileName, FileMode.Open);
            }
            catch
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }



        public string GetFile(string StrFileName)
        {
            return (File.ReadAllText(StrFileName));
        }



        public int GetUnixTimestamp()
        {
            Int32 Utx = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return Utx;
        }



        public string MD5Hash(string StrToHash)
        {
            MD5 md5h = MD5.Create();
            byte[] data = md5h.ComputeHash(Encoding.UTF8.GetBytes(StrToHash));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        
    }
}
