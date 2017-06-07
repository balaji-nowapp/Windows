using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pidilite
{


    public static class RegistryConfig
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
        IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        public static  PrivateFontCollection pfc = new PrivateFontCollection();
        public static Font myFont, myFontBold,myBCFont,myHeaderFont ;
        public static string UserName, Password;
        public static Bitmap userImage { get; set; }
        static RegistryConfig ()
            {
            
            byte[] fontData = Properties.Resources.OpenSans_Light;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            pfc.AddMemoryFont(fontPtr, Properties.Resources.OpenSans_Light.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.OpenSans_Light.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            myFont = new Font(pfc.Families[0], 9.0F);
            myHeaderFont = new Font(pfc.Families[0], 12.0F);

            byte[] fontData1 = Properties.Resources.OpenSans_Bold;
            IntPtr fontPtr1 = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData1.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData1, 0, fontPtr1, fontData1.Length);
            uint dummy1 = 0;
            pfc.AddMemoryFont(fontPtr1, Properties.Resources.OpenSans_Bold.Length);
            AddFontMemResourceEx(fontPtr1, (uint)Properties.Resources.OpenSans_Bold.Length, IntPtr.Zero, ref dummy1);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr1);
            myFontBold = new Font(pfc.Families[0], 9.0F);
            myBCFont = new Font(pfc.Families[0], 13.0F);

        
        }
        public static string registryName = @"SOFTWARE\Pidilite\PidiliteConfig";
        public static string serverName { get; set; }
        public static string serverUser { get; set; }
        public static string serverPwd { get; set; }
        public static string database { get; set; }
        public static Int64 userId { get; set; }
        public static Int64 OrgId { get; set; }
        public static bool isRegEmpty = true;
        public static bool LoadRegValues()
        {
            RegistryKey myregistry = Registry.CurrentUser.OpenSubKey(registryName);
            if (myregistry != null)
            {
                try
                {
                    serverName = (string)myregistry.GetValue("server_name");
                    serverUser = (string)myregistry.GetValue("server_user");
                    serverPwd = (string)myregistry.GetValue("server_Pwd");
                    database=(string)myregistry.GetValue("database");
                    isRegEmpty = false;
                }
                catch (Exception ex)
                {
                    isRegEmpty = true;
                    Log.LogData("Error in Loading Registry Values: " + ex.Message + ex.StackTrace, Log.Status.Error);
                }
            }
            else
            {
                isRegEmpty = true;
            }
            return isRegEmpty; 
        }
        public static string myConn;
        public static void opGetUserPhoto(string avatar)
        {
            try
            {

                Application.DoEvents();
                string strURL = ConfigurationManager.AppSettings["Url"].Replace("index.php/windowsapi", "");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL + "uploads/users/" + avatar);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = 300000;
                using (HttpWebResponse lxResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (BinaryReader reader = new BinaryReader(lxResponse.GetResponseStream()))
                    {
                        Byte[] lnByte = reader.ReadBytes(1 * 1024 * 1024 * 10);

                        userImage = ByteToImage(lnByte);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Getting Avatar: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
        }
        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

    }
}
