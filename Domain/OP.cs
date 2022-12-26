using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class OP : GRCBaseDomain
    {        
	    private string oPNo = string.Empty;
        private int id2 = 0;
        private string oPNo2 = string.Empty;
	    private DateTime shipmentDate = DateTime.Now.Date;
	    private int customerType = 0;
	    private int customerID = 0;
        private string customerCode = string.Empty;
        private string customerName = string.Empty;
        private string address = string.Empty;
	    private int salesID = 0;
	    private string alamatLain = string.Empty;
	    private int typeOP = 0;
	    private int proyek = 0;
	    private int depoID = 0;
        private int diambilSendiri = 0;
        private int status = 0;
        private string keterangan1 = string.Empty;
        private string keterangan2 = string.Empty;
        private decimal totalKubikasi = 0;
        private string area = string.Empty;
        private int kabupatenID = 0;
        private string noDO = string.Empty;
        private string noDO2 = string.Empty;
        private DateTime approveDistDate = DateTime.MinValue;
        private DateTime approveSCDate = DateTime.MaxValue;       
        private int distributorID = 0;
        private string keterangan = string.Empty;
        private int distSubID = 0;
        private int typeDistSub = 0;
        private string alasanCancel = string.Empty;
        private decimal harga = 0;
        private int agenID = 0;
        private int sjID = 0;
        private string sjNO = string.Empty;
        private string lastApproveBy = string.Empty;
        private string unApprove = string.Empty;
        private decimal creditLimit = 0;
        private int zonaIDforViewState = 0;
        private int viewBtnPayment = 0;
        private string opSession = string.Empty;
        private int deptID = 0;
        private int infoHBM = 0;
        private int hbmCityID = 0;
        private int barangPromosi = 0;
        private int qtyOrder = 0;
        private int qtyNew = 0;
        private int jumlah = 0;
        private int jenisCustomer = 0;
        private int lamaPembayaran = 0;
        private decimal nominalVoucher = 0;
        private decimal saldoVoucher = 0;
        private decimal nominalPotong = 0;
        private string kodeVoucher = string.Empty;
        private int pTID = 0;
        private decimal persen = 0;
        private int shipmentDateType = 0;
        private int paymentInfoType = 0; // 1 = customer project/mailan, 2 = toko retail/jatim
        private string tokoKode = string.Empty;
        private int priceTypeID = 0;
        private int opRetur = 0;
        private int subDistributorID = 0;
        private int tokoZonaID = 0;
        private string tokoPriceType = string.Empty;
        private int imsID = 0;
        private int umurOP = 0;
        private int caraPembayaran = 0;
        private string pIC = string.Empty;
        private string noPhone = string.Empty;

        public string NoPhone { get { return noPhone; } set { noPhone = value; } }
        public string PIC { get { return pIC; } set { pIC = value; } }
        public int CaraPembayaran { get { return caraPembayaran; } set { caraPembayaran = value; } }
        public int UmurOP { get { return umurOP; } set { umurOP = value; } }
        public int ImsID { get { return imsID; } set { imsID = value; } }
        public int TokoZonaID { get { return tokoZonaID; } set { tokoZonaID = value; } }
        public string TokoPriceType { get { return tokoPriceType; } set { tokoPriceType = value; } }
        public int SubDistributorID { get { return subDistributorID; } set { subDistributorID = value; } }
        public int OpRetur { get { return opRetur; } set { opRetur = value; } }
        public int PriceTypeID { get { return priceTypeID; } set { priceTypeID = value; } }
        public string TokoKode
        {
            get { return tokoKode; }
            set { tokoKode = value; }
        }
        public int PaymentInfoType
        {
            get { return paymentInfoType; }
            set { paymentInfoType = value; }
        }
        public int ShipmentDateType
        {
            get { return shipmentDateType; }
            set { shipmentDateType = value; }
        }
        public decimal Persen
        {
            get { return persen; }
            set { persen = value; }
        }
        public int PTID
        {
            get { return pTID; }
            set { pTID = value; }
        }
        public decimal NominalVoucher
        {
            get { return nominalVoucher; }
            set { nominalVoucher = value; }
        }
        public string KodeVoucher
        {
            get { return kodeVoucher; }
            set { kodeVoucher = value; }
        }
        public decimal SaldoVoucher
        {
            get { return saldoVoucher; }
            set { saldoVoucher = value; }
        }
        public decimal NominalPotong
        {
            get { return nominalPotong; }
            set { nominalPotong = value; }
        }
        public int LamaPembayaran
        {
            get { return lamaPembayaran; }
            set { lamaPembayaran = value; }
        }
        public int JenisCustomer
        {
            get { return jenisCustomer; }
            set { jenisCustomer = value; }
        }
        public int Jumlah
        {
            get { return jumlah; }
            set { jumlah = value; }
        }
        public int QtyOrder
        {
            get { return qtyOrder; }
            set { qtyOrder = value; }
        }
        public int QtyNew
        {
            get { return qtyNew; }
            set { qtyNew = value; }
        }
        public int BarangPromosi
        {
            get { return barangPromosi; }
            set { barangPromosi = value; }
        }
        public int InfoHBM
        {
            get { return infoHBM; }
            set { infoHBM = value; }
        }
        public int HbmCityID
        {
            get { return hbmCityID; }
            set { hbmCityID = value; }
        }
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public string OPSession
        {
            get { return opSession; }
            set { opSession = value; }
        }
        public int ViewBtnPayment
        {
            get { return viewBtnPayment; }
            set { viewBtnPayment = value; }
        }
        public int ZonaIDforViewState
        {
            get { return zonaIDforViewState; }
            set { zonaIDforViewState = value; }
        }
        public decimal CreditLimit
        {
            get { return creditLimit; }
            set { creditLimit = value; }
        }
        public string UnApprove
        {
            get
            {
                return unApprove;
            }
            set
            {
                unApprove = value;
            }
        }
        public string LastApproveBy
        {
            get
            {
                return lastApproveBy;
            }
            set
            {
                lastApproveBy = value;
            }
        }
        public string SjNO
        {
            get
            {
                return sjNO;
            }
            set
            {
                sjNO = value;
            }
        }
        public int SjID
        {
            get
            {
                return sjID;
            }
            set
            {
                sjID = value;
            }
        }

        public decimal Harga
        {
            get
            {
                return harga;
            }
            set
            {
                harga = value;
            }
        }
        public string OPNo
        {
            get
            {
                return oPNo;
            }
            set
            {
                oPNo = value;
            }
        }
        public int AgenID
        {
            get
            {
                return agenID;
            }
            set
            {
                agenID = value;
            }
        }
        public int ID2
        {
            get
            {
                return id2;
            }
            set
            {
                id2 = value;
            }
        }

        public string OPNo2
        {
            get
            {
                return oPNo2;
            }
            set
            {
                oPNo2 = value;
            }
        }


        public DateTime ShipmentDate
        {
            get
            {
                return shipmentDate;
            }
            set
            {
                shipmentDate = value;
            }
        }

        public int CustomerType
        {
            get
            {
                return customerType;
            }
            set
            {
                customerType = value;
            }        
        }

        public int CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }

        public string CustomerCode
        {
            get
            {
                return customerCode;
            }
            set
            {
                customerCode = value;
            }
        }

        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public int SalesID
        {
            get
            {
                return salesID;
            }
            set
            {
                salesID = value;
            }
        }

        public string AlamatLain
        {
            get
            {
                return alamatLain;
            }
            set
            {
                alamatLain = value;
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

        public int Proyek
        {
            get
            {
                return proyek;
            }
            set
            {
                proyek = value;
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

        public int DiambilSendiri
        {
            get
            {
                return diambilSendiri;
            }
            set
            {
                diambilSendiri = value;
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

        public string Keterangan1
        {
            get
            {
                return keterangan1;
            }
            set
            {
                keterangan1 = value;
            }
        }

        public string Keterangan2
        {
            get
            {
                return keterangan2;
            }
            set
            {
                keterangan2 = value;
            }
        }

        public decimal TotalKubikasi
        {
            get
            {
                return totalKubikasi;           
            }
            set
            {
                totalKubikasi = value;
            }
        }

        public string Area
        {
            get
            {
                return area;
            }
            set
            {
                area = value;
            }
        }

        public int KabupatenID
        {
            get
            {
                return kabupatenID;
            }
            set
            {
                kabupatenID = value;
            }
        }

        public string NoDO
        {
            get
            {
                return noDO;
            }
            set
            {
                noDO = value;
            }
        }

        public string NoDO2
        {
            get
            {
                return noDO2;
            }
            set
            {
                noDO2 = value;
            }
        }

        public int DistributorID
        {
            get
            {
                return distributorID;
            }
            set
            {
                distributorID = value;
            }
        }

        public DateTime ApproveDistDate
        {
            get
            {
                return approveDistDate;
            }
            set
            {
                approveDistDate = value;
            }
        }

        public DateTime ApproveSCDate
        {
            get
            {
                return approveSCDate;
            }
            set
            {
                approveSCDate = value;
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

        public int DistSubID
        {
            get
            {
                return distSubID;
            }
            set
            {
                distSubID = value;
            }
        }

        public int TypeDistSub
        {
            get
            {
                return typeDistSub;
            }
            set
            {
                typeDistSub = value;
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
