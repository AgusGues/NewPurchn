using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Domain;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;

namespace BusinessFacade
{
    public class Global
    {
        //public static string ConnectionString = "Initial Catalog=GRCBoard;Data Source=HARRY\\CPD;User ID=sa;Password=280225";
        //public static string ConnectionString = "Initial Catalog=grcboard;Data Source=IT3;User ID=sa;Password=grc05;Max Pool Size=1000000;MultipleActiveResultSets=True";
        public static string ConnectionString()
        {
            string returnValue = null;
            ConnectionStringSettings connSetting = ConfigurationManager.ConnectionStrings["GRCBoard"];
            if (connSetting != null)
                return connSetting.ConnectionString;
            //returnValue = "Initial Catalog=grcboard;Data Source=IT3;User ID=sa;Password=grc05;Max Pool Size=1000000;MultipleActiveResultSets=True";
            return returnValue;

            //= ConfigurationManager.ConnectionStrings["GRCBoard"].ToString();
        }
        public static string ConnectionKrwg()
        {
            string returnValue = null;
            ConnectionStringSettings connSetting = ConfigurationManager.ConnectionStrings["BPASKrwg"];
            if (connSetting != null)
                return connSetting.ConnectionString;
            return returnValue;
        }

        public static string ConnectionCtrp()
        {
            string returnValue = null;
            ConnectionStringSettings connSetting = ConfigurationManager.ConnectionStrings["BPASCtrp"];
            if (connSetting != null)
                return connSetting.ConnectionString;
            return returnValue;
        }

        public static Users UserLogin = new Users();        
        public static SettingNumber settingNumber = new SettingNumber();
        public static string link = string.Empty;
        


        public static string Status(int intStatus)
        {                  
            string strStatus = string.Empty;
            if(intStatus == 0)
                strStatus = "Open";
            else if(intStatus == 1)
                strStatus = "Shipment";
            else if(intStatus == 2)
                strStatus = "Received";
            else if(intStatus == 3)
                strStatus = "Invoice";
            else if (intStatus == -1)
                strStatus = "Cancel";            

            return strStatus;
        }

        public static string ConvertNumericToRomawi(int intBulan)
        {
            if (intBulan == 1)
                return "I";
            if (intBulan == 2)
                return "II";
            if (intBulan == 3)
                return "III";
            if (intBulan == 4)
                return "IV";
            if (intBulan == 5)
                return "V";
            if (intBulan == 6)
                return "VI";
            if (intBulan == 7)
                return "VII";
            if (intBulan == 8)
                return "VIII";
            if (intBulan == 9)
                return "IX";
            if (intBulan == 10)
                return "X";
            if (intBulan == 11)
                return "XI";
            if (intBulan == 12)
                return "XII";

            return string.Empty;
        }
        public static string BulanRomawi(int Bulan)
        {
            string[] romawi = ",I,II,III,IV,V,VI,VII,VIII,IX,X,XI,XII".Split(',');
            return romawi[Bulan];
        }

        public static string nBulan(int Bln)
        {
            string[] nBul = new string[] { "", "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };
            return nBul[Bln];
        }
        public static string nBulan(int Bln, bool Short)
        {
            string[] nBul = (Short == true) ?
                new string[] { "", "Jan", "Feb", "Mar", "Apr", "Mei", "Jun", "Jul", "Agu", "Sep", "Okt", "Nov", "Des" } :
                new string[] { "", "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };
            return nBul[Bln];

        }
        public static string GetConfig(string Key, string Section)
        {
            /**
             * Added on 29-09-2014
             * Mengambil data koneksi database dari file Config.ini
             * file config.ini di generate dengan program khusus
             */
            string hasil = string.Empty;
            string conn = (Section == string.Empty) ? "Connection" : Section;
            string path = HttpContext.Current.Server.MapPath("~/App_Data/Config.ini");
            EncryptPasswordFacade enc = new EncryptPasswordFacade();
            var cfg = new Inifiles(path);
            hasil = cfg.Read(enc.EncryptToString(Key), conn);
            return enc.DecryptString(hasil);
        }

        public static string GetActiveConnection()
        {
            /**
             * untuk merubah connection string yang digunakan
             * dengan merubah status koneksi di file
             */
            string path = HttpContext.Current.Server.MapPath("~/App_Data/ConConfig.ini");
            var cfg = new Inifiles(path);
            return cfg.Read("Aktif", "DbConnection");
        }
        public static string GetActiveConnection(string DatabaseName)
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/ConConfig.ini");
            var cfg = new Inifiles(path);
            return cfg.Read(DatabaseName, "DbConnection");
        }
        public static Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //if (!string.IsNullOrEmpty(ipAddress))
            //{
            //    string[] addresses = ipAddress.Split(',');
            //    if (addresses.Length != 0)
            //    {
            //        return addresses[0];
            //    }
            //}

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        public static void LoadBulan(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Pilih Bulan--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddl.Items.Add(new ListItem(nBulan(i).ToString(), i.ToString()));
            }
            ddl.SelectedValue = DateTime.Now.Month.ToString();
        }
        public static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
    }
}
