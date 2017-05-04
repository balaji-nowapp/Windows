using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pidilite
{


    public static class RegistryConfig
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
        IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        public static  PrivateFontCollection pfc = new PrivateFontCollection();
        public static Font myFont, myFontBold,myBCFont ;

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
                }
            }
            else
            {
                isRegEmpty = true;
            }
            return isRegEmpty; 
        }
        public static string myConn;

       
    }
}
