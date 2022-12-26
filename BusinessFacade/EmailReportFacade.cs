using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessFacade
{
    public class EmailReportFacade
    {
        public string mailSubject(string str1 )
        {
            string mailSubject = "Permohonan Approval Improvement Nama Barang " + "(Plant : " + str1 + ")";
            return mailSubject;
        }

        public string mailBody1()
        {
            string mailBody1 = string.Empty;
            
                mailBody1 = "<font size=-1><b>Mohon Approval untuk Nama Barang di bawah ini :</b></font><br/>" ;//+
           
            return mailBody1;
        }

        public string mailBody2(string str)
        {
            string mailBody2 = string.Empty;
            
                mailBody2 = "<br/> " +
                           "<table style=font:11px;> " +
                           "<tr> " +
                            "<td>Dibuat Oleh : </td> " +
                           "</tr> " +
                           "<tr> " +
                            "<td>"+ str+"</td> " +
                           "</tr>" +
                          "</table>";
            
            return mailBody2;
        }
        //Add By Razib WO : WO-IT-K0030621 : 27-08-2021
        public string mailSubjectWo(string str1)
        {
            string mailSubject = "Informasi Naik Target WorkOrder " + "(Plant : " + str1 + ") (No-Reply)";
            return mailSubject;
        }
        //Add By Razib WO : WO-IT-K0030621 : 27-08-2021
        public string mailBodyWO()
        {
            string mailBody1 = string.Empty;

            mailBody1 = " Informasi Naik Target WorkOrder : ";//+

            return mailBody1;
        }

        public string mailBody1Task()
        {
            string mailBody1 = string.Empty;

            mailBody1 = "Mohon Approval untuk Task di bawah ini :";//+

            return mailBody1;
        }

        public string mailSubjectTask(string str1)
        {
            string mailSubject = "Permohonan Approval Task " + "(Plant : " + str1 + ") (No-Reply)";
            return mailSubject;
        }


        public string mailBody2Task(string str)
        {
            string mailBody2 = string.Empty;

            mailBody2 =   str ;

            return mailBody2;
        }

        public string mailFooter()
        {
            string mailFooter = "<hr/> " +
             "<table> " +
              "<tr valign='top'> " +
               "<td> " +
                "<font size=-1><b>PT. BANGUNPERKASA ADHITAMASENTRA</b> " +
                "<table style=font-size:xx-small; cellpadding=3 cellspacing=3> " +
                 "<tr valign='top'> " +
                  "<td> " +
                    "<b>Office :</b><br/>Graha GRC board Lt. III Jl. S. Parman Kav. 64, Slipi Palmerah<br/> " +
                    "Jakarta Barat, 11410–Indonesia<br/> " +
                    "Telp (62-21) 53666800 (Hunting)<br/> " +
                    "Fax (62-21) 53666720 " +
                  "</td> " +
                  "<td> " +
                    "<b>Factory :</b><br/><b>PLANT CITEUREUP</b><br/> " +
                    "Komp. Industri Branta Mulia	Kp. Sabur, Ds.TariKolot, Citeureup<br/> " +
                    "Bogor, 16810 - Indonesia<br/> " +
                    "Telp (62-21) 875-6773, 87944118-9<br/> " +
                    "Fax (62-21) 875-6774 " +
                  "</td> " +
                  "<td> " +
                    "<br/><b>PLANT KARAWANG</b><br/> " +
                    "Jl. Raya Kosambi Curug, Desa Curug, Klari<br/> " +
                    "Karawang, 41371 - Indonesia<br/> " +
                    "Telp (62-267) 861-5519<br/> " +
                    "Fax (62-267) 861-5523 " +
                  "</td> " +
                 "</tr> " +
                "</table> " +
               "</td> " +
              "</tr> " +
             "</table>";
            return mailFooter;
        }

        public string mailSmtp()
        {
            string mailSmtp = "mail.grcboard.com";
            return mailSmtp;
        }

        public int mailPort()
        {
            int mailPort = 587;//via speedy
            //int mailPort = 465;//via grcboard
            return mailPort;
        }

    }
}
