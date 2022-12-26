using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{

    public class Defect : GRCBaseDomain
    {
        private DateTime tgl = DateTime.Now.Date;
        private int jenisID = 0;
        private DateTime tglProduksi = DateTime.Now.Date;
        private int prodID = 0;
        private int groupCutID = 0;
        private int groupProdID = 0;
        private int groupJemurID = 0;
        private int ukuranID = 0;
        private int paletID = 0;
        private int status = 0;
        private int serahID= 0;
        private int destID = 0;
        private string defectNo = string.Empty;
        private string jenis = string.Empty;
        private string gProd = string.Empty;
        private string gJmr = string.Empty;
        private string gCut = string.Empty;
        private string ukuran = string.Empty;
        private string noPalet = string.Empty;
        private int qty = 0;
        private int tPot;

        public int TPot
        {
            get { return tPot ; }
            set { tPot = value; }
        }
        public string Jenis
        {
            get { return jenis; }
            set { jenis = value; }
        }
        public string GProd
        {
            get { return gProd; }
            set { gProd = value; }
        }
        public string GJmr
        {
            get { return gJmr; }
            set { gJmr = value; }
        }
        public string GCut
        {
            get { return gCut; }
            set { gCut = value; }
        }
        public string Ukuran
        {
            get { return ukuran; }
            set { ukuran = value; }
        }
        public string NoPalet
        {
            get { return noPalet; }
            set { noPalet = value; }
        }
        public int Qty
        {
            get { return qty ; }
            set { qty = value; }
        }
        public int DestID
        {
            get { return destID; }
            set { destID = value; }
        }
        public string DefectNo
        {
            get { return defectNo; }
            set { defectNo = value; }
        }
        public DateTime Tgl
        {
            get { return tgl; }
            set { tgl = value; }
        }

        public DateTime TglProduksi
        {
            get { return tglProduksi; }
            set { tglProduksi = value; }
        }
        public int JenisID
        {
            get { return jenisID; }
            set { jenisID = value; }
        }
        public int SerahID
        {
            get { return serahID; }
            set { serahID = value; }
        }
        public int ProdID
        {
            get { return prodID; }
            set { prodID = value; }
        }

        public int GroupCutID
        {
            get { return groupCutID; }
            set { groupCutID = value; }
        }

        public int GroupProdID
        {
            get { return groupProdID; }
            set { groupProdID = value; }
        }

        public int GroupJemurID
        {
            get { return groupJemurID; }
            set { groupJemurID = value; }
        }

        public int UkuranID
        {
            get { return ukuranID; }
            set { ukuranID = value; }
        }

        public int PaletID
        {
            get { return paletID; }
            set { paletID = value; }
        }

        
        public int Status
        {
            get {return status;}
            set {status = value;}
        }


        public static System.Collections.ArrayList RetrieveByTgl(string p)
        {
            throw new NotImplementedException();
        }
    }
}
