using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

public enum State
{
    P,
    F
}

namespace Line_Production
{
    public partial class Common
    {
        public static void ActiveProcess(string title)
        {
            Process[] processes = Process.GetProcesses();
            int windowHandle = 0;
            foreach (Process p in processes)
            {
                if (p.MainWindowTitle.Contains(title))
                {
                    windowHandle = (int)p.MainWindowHandle;
                    break;
                }

            }
            NativeWin32.SetForegroundWindow(windowHandle);
        }
        public static void WriteRegistry(string path, string keyName, string content)
        {
            var key = Registry.CurrentUser.CreateSubKey(path);
            if (!string.IsNullOrEmpty(keyName) && !string.IsNullOrEmpty(content))
            {
                key.SetValue(keyName, content);
                key.Close();
            }
        }

        public static string GetValueRegistryKey(string path, string keyName)
        {
            var key = Registry.CurrentUser.CreateSubKey(path);
            string value = null;
            if (key is object)
            {
                if (key.GetValue(keyName) is object)
                {
                    value = key.GetValue(keyName).ToString();
                    key.Close();
                    return value;
                }
            }

            return null;
        }

        public static bool Validate_qty(string qty)
        {
            var objRegExp = new System.Text.RegularExpressions.Regex(@"^\d+$");
            return objRegExp.Match(qty).Success;
        }

        public static string FindApplication(string NameSoft)
        {
            string astring = null;
            foreach (Process p in Process.GetProcesses())
            {
                string h = p.MainWindowTitle.ToString();
                if (h.Length > 0)
                {
                    if (h.Contains(NameSoft))
                    {
                        astring = h;
                    }
                }
            }

            return astring;
        }

        public static bool IsRunning(string nameSoft)
        {
            if (nameSoft is null)
            {
                return false;
            }

            Process[] p;
            p = Process.GetProcesses();
            foreach (var pro in p)
            {
                if (pro.MainWindowTitle.Contains(nameSoft))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Ghi log
        /// </summary>
        /// <param name="path">Đường dẫn</param>
        /// <param name="content">Nội dung cần ghi log</param>
        public static void WriteLog(string path, StringBuilder content)
        {
            using (var writer = new StreamWriter(path, true))
            {
                writer.Write(content.ToString());
            }
        }
        public static bool SendToComport(string data, Action<string> result)
        {
            try
            {
                SerialPort com = new SerialPort() { PortName = GetValueRegistryKey(Control.PathConfig, "COM") };
                if (!com.IsOpen) com.Open();
                if (com.IsOpen == true)
                {
                    com.Write(data);
                }
                com.Close();
                result(ConstantsText.OK);
                return true;
            }
            catch
            {
                result(ConstantsText.NG);
                return false;
            }

        }
    }

    public static class FileShare
    {
        public static string PASSWORD = "umcvn";
        public static string USER = @"VN\Scanner";
        public static string PATH = @"\\172.28.10.12";

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource,
        string password, string username, int flags);

        [StructLayout(LayoutKind.Sequential)]
        public class NetResource
        {
            public ResourceScope Scope;
            public ResourceType ResourceType;
            public ResourceDisplaytype DisplayType;
            public int Usage;
            public string LocalName;
            public string RemoteName;
            public string Comment;
            public string Provider;
        }

        public enum ResourceScope : int
        {
            Connected = 1,
            GlobalNetwork,
            Remembered,
            Recent,
            Context
        };

        public enum ResourceType : int
        {
            Any = 0,
            Disk = 1,
            Print = 2,
            Reserved = 8,
        }

        public enum ResourceDisplaytype : int
        {
            Generic = 0x0,
            Domain = 0x01,
            Server = 0x02,
            Share = 0x03,
            File = 0x04,
            Group = 0x05,
            Network = 0x06,
            Root = 0x07,
            Shareadmin = 0x08,
            Directory = 0x09,
            Tree = 0x0a,
            Ndscontainer = 0x0b
        }
        public static bool Connect(string networkName, NetworkCredential credentials)
        {
            try
            {
                var netResource = new NetResource
                {
                    Scope = ResourceScope.GlobalNetwork,
                    ResourceType = ResourceType.Disk,
                    DisplayType = ResourceDisplaytype.Share,
                    RemoteName = networkName
                };

                var userName = string.IsNullOrEmpty(credentials.Domain)
                    ? credentials.UserName
                    : string.Format(@"{0}\{1}", credentials.Domain, credentials.UserName);

                var result = WNetAddConnection2(
                    netResource,
                    credentials.Password,
                    userName,
                    0);

                return result == 0;
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                return false;
            }
        }
    }
}