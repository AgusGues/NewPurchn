using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Produk
    {

        public class T3SerahT
        {
            public List<Serah> T3SerahTambah { get; set; }
        }

        public class RekapT
        {
            public List<Rekap> RekapTambah { get; set; }
        }

        public class PartnoStok
        {
            public string PartNo { get; set; }
            public int Tebal { get; set; }
            public int Panjang { get; set; }
            public int Lebar { get; set; }
            public string PartName { get; set; }
            public int Qty { get; set; }
            public float Volume { get; set; }
            public string Lokasi { get; set; }
            public int GroupID { get; set; }
            public int ID { get; set; }
            public int LokID { get; set; }
            public int ItemID { get; set; }
            public int HPP { get; set; }
            public int ItemTypeID { get; set; }
            public string Kode { get; set; }
            public string CreatedBy { get; set; }
        }


        public class Simetris
        {
            public int ID { get; set; }
            public int RekapID { get; set; }
            public int SerahID { get; set; }
            public int LokasiID { get; set; }
            public string TglSm { get; set; }
            public int ItemID { get; set; }
            public int QtyInSm { get; set; }
            public int QtyOutSm { get; set; }
            public int GroupID { get; set; }
            public string MCutter { get; set; }
            public string BS { get; set; }
            public int NCH { get; set; }
            public int NCSS { get; set; }
            public int NCSE { get; set; }
            public string Defect { get; set; }
            public string CreatedBy { get; set; }

            public string PartnoSer { get; set; }
            public string LokasiSer { get; set; }
            public string PartnoSm { get; set; }
            public string LokasiSm { get; set; }
            public string Groups { get; set; }

        }

        public class Rekap
        {
            public int DestID { get; set; }
            public int SerahID { get; set; }
            public int GroupID { get; set; }
            public int T1serahID { get; set; }
            public int LokasiID { get; set; }
            public int ItemIDSer { get; set; }
            public string TglTrm { get; set; }
            public int QtyInTrm { get; set; }
            public int QtyOutTrm { get; set; }
            public int HPP { get; set; }
            public int SA { get; set; }
            public string Keterangan { get; set; }
            public string Process { get; set; }
            public string CreatedBy { get; set; }
            public int CutID { get; set; }
            public int T1sItemID { get; set; }
            public int T1SLokID { get; set; }
            public int CutQty { get; set; }
            public int CutLevel { get; set; }

        }

        public class Serah
        {
            public string Flag { get; set; }
            public int ItemID { get; set; }
            public int GroupID { get; set; }
            public int ID { get; set; }
            public int LokID { get; set; }
            public int Qty { get; set; }
            public int HPP { get; set; }
            public string CreatedBy { get; set; }
        }

        public class GroupMarketing
        {
            public int ID { get; set; }
            public string Groups { get; set; }
        }


        public class Defect
        {
            public int ID { get; set; }
            public string JenisDefect { get; set; }
        }


        public class GetLokasi
        {
            public int ID { get; set; }
            public string Lokasi { get; set; }
        }

        public class Cutter
        {
            public int ID { get; set; }
            public string NamaMesin { get; set; }
        }

        public class NoPartnoListPlank
        {
            public int ID { get; set; }
            public int Lock { get; set; }
        }

        public class ClosingStatus
        {
            public int status { get; set; }
            public int clsStat { get; set; }
        }

        public class User
        {
            public int DeptId { get; set; }
            public string Username { get; set; }
            public int UnitKerja { get; set; }
        }
    }
}
