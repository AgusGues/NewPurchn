using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SuratJalan : GRCBaseDomain
    {
        private string suratJalanNo = string.Empty;
        private int opID = 0;
        private string opNo = string.Empty;
        private int scheduleID = 0;
        private string scheduleNo = string.Empty;
        private string policeCarNo = string.Empty;
        private string driverName = string.Empty;
        private int status = 0;
        private int typeOP = 0;
        private int depoID = 0;
        private int cetak = 0;
        private int toDepoID = 0;
        private string keterangan = string.Empty;
        private DateTime scheduleDate = DateTime.Now.Date;
        private int countPrint = 0;
        private DateTime receiveDate = DateTime.Now.Date;
        private DateTime postingShipmentDate = DateTime.MinValue;
        private DateTime postingReceiveDate = DateTime.MinValue;
        private DateTime tglKirimActual = DateTime.Now;
        private string alasanCancel = string.Empty;
        // For ProformaINvoice
        private string kodeToko = string.Empty;
        private string namaToko = string.Empty;
        private string kenekName = string.Empty;
        private string kabupatenName = string.Empty;
        private int id = 0;
        private int refOPID = 0;
        private string refOPNo = string.Empty;
        private int refSJID = 0;
        private string refSJNo = string.Empty;
        private int refSJDetailID = 0;
        private int statusPengajuan = 0;
        private int statusInvoice = 0;
        private string cnNo = string.Empty;
        private int cnID = 0;
        private int subCompanyID = 0;
        private int expedisiDetailID = 0;
        private DateTime opCreatedTime = DateTime.MinValue;
        private string namaExpedisi = string.Empty;
        private int opRetur = 0;
        private int cetakKwitansi = 0;
        private int countCetakKwitansi = 0;
        private int kabupatenID = 0;
        private decimal cost = 0;
        private int distID = 0;
        private int subDistID = 0;
        private string emailAddress = string.Empty;
        private string pic = string.Empty;
        private string alasanTurunStatus = string.Empty;

        public string AlasanTurunStatus
        {
            get { return alasanTurunStatus; }
            set { alasanTurunStatus = value; }
        }
        public string Pic
        {
            get { return pic; }
            set { pic = value; }
        }
        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }
        public int DistID
        {
            get { return distID; }
            set { distID = value; }
        }
        public int SubDistID
        {
            get { return subDistID; }
            set { subDistID = value; }
        }
        public decimal Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public int KabupatenID
        {
            get { return kabupatenID; }
            set { kabupatenID = value; }
        }
        public int CountCetakKwitansi
        {
            get { return countCetakKwitansi; }
            set { countCetakKwitansi = value; }
        }
        public int CetakKwitansi
        {
            get { return cetakKwitansi; }
            set { cetakKwitansi = value; }
        }
        public int OpRetur
        {
            get { return opRetur; }
            set { opRetur = value; }
        }
        public DateTime OpCreatedTime
        {
            get { return opCreatedTime; }
            set { opCreatedTime = value; }
        }
        public string NamaExpedisi
        { get { return namaExpedisi; }
          set { namaExpedisi = value; }
        }

        public int ExpedisiDetailID
        {
            get
            {
                return expedisiDetailID;
            }
            set
            {
                expedisiDetailID = value;
            }
        }
        public int SubCompanyID
        {
            get
            {
                return subCompanyID;
            }
            set
            {
                subCompanyID = value;
            }
        }
        public string CnNo
        {
            get
            {
                return cnNo;
            }
            set
            {
                cnNo = value;
            }
        }
        public int CnID
        {
            get
            {
                return cnID;
            }
            set
            {
                cnID = value;
            }
        }
        public int StatusInvoice
        {
            get{return statusInvoice;}
            set{statusInvoice = value;}
        }
        public int StatusPengajuan
        {
            get
            {
                return statusPengajuan;
            }
            set
            {
                statusPengajuan = value;
            }
        }
        public int RefOPID
        {
            get
            {
                return refOPID;
            }
            set
            {
                refOPID = value;
            }
        }

        public string RefOPNo
        {
            get
            {
                return refOPNo;
            }
            set
            {
                refOPNo = value;
            }
        }

        public int RefSJID
        {
            get
            {
                return refSJID;
            }
            set
            {
                refSJID = value;
            }
        }

        public string RefSJNo
        {
            get
            {
                return refSJNo;
            }
            set
            {
                refSJNo = value;
            }
        }

        public int RefSJDetailID
        {
            get
            {
                return refSJDetailID;
            }
            set
            {
                refSJDetailID = value;
            }
        }

        public string KabupatenName
        {
            get
            {
                return kabupatenName;
            }
            set
            {
                kabupatenName = value;
            }
        }
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string KenekName
        {
            get
            {
                return kenekName;
            }
            set
            {
                kenekName = value;
            }
        }
        public string KodeToko
        {
            get
            {
                return kodeToko;
            }
            set
            {
                kodeToko = value;
            }
        }

        public string NamaToko
        {
            get
            {
                return namaToko;
            }
            set
            {
                namaToko = value;
            }
        }

        public DateTime TglKirimActual
        {
            get
            {
                return tglKirimActual;
            }
            set
            {
                tglKirimActual = value;
            }
        }
        public string SuratJalanNo
        {
            get
            {
                return suratJalanNo;
            }
            set
            {
                suratJalanNo = value;
            }
        }

        public int OPID
        {
            get
            {
                return opID;
            }
            set
            {
                opID = value;
            }
        }

        public string OPNo
        {
            get
            {
                return opNo;
            }
            set
            {
                opNo = value;
            }
        }

        public int ScheduleID
        {
            get
            {
                return scheduleID;
            }
            set
            {
                scheduleID = value;
            }
        }

        public string ScheduleNo
        {
            get
            {
                return scheduleNo;
            }
            set
            {
                scheduleNo = value;
            }
        }

        public string PoliceCarNo
        {
            get
            {
                return policeCarNo;
            }
            set
            {
                policeCarNo = value;
            }
        }

        public string DriverName
        {
            get
            {
                return driverName;
            }
            set
            {
                driverName = value;
            }
        }

        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public int TypeOP
        {
            get
            {
                return typeOP;
            }
            set
            {
                typeOP = value;
            }
        }

        public int DepoID
        {
            get
            {
                return depoID;
            }
            set
            {
                depoID = value;
            }
        }

        public int ToDepoID
        {
            get
            {
                return toDepoID;
            }
            set
            {
                toDepoID = value;
            }
        }

        public int Cetak
        {
            get
            {
                return cetak;
            }
            set
            {
                cetak = value;
            }
        }

        public string Keterangan
        {
            get
            {
                return keterangan;
            }
            set
            {
                keterangan = value;
            }
        }

        public DateTime ScheduleDate
        {
            get
            {
                return scheduleDate;
            }
            set
            {
                scheduleDate = value;
            }
        }

        public int CountPrint
        {
            get
            {
                return countPrint;
            }
            set
            {
                countPrint = value;
            }
        }

        public DateTime ReceiveDate
        {
            get
            {
                return receiveDate;
            }
            set
            {
                receiveDate = value;
            }
        }

        public DateTime PostingShipmentDate
        {
            get
            {
                return postingShipmentDate;
            }
            set
            {
                postingShipmentDate = value;
            }
        }

        public DateTime PostingReceiveDate
        {
            get
            {
                return postingReceiveDate;
            }
            set
            {
                postingReceiveDate = value;
            }
        }

        public string AlasanCancel
        {
            get
            {
                return alasanCancel;
            }
            set
            {
                alasanCancel = value;
            }
        }
    }
}
