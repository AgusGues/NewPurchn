using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DomainReportT1T3
    {
        public string Partno { get; set; }
        public string Partno2 { get; set; }
        public string Partno3 { get; set; }
        public string Lokasi { get; set; }
        public string Lokasi2 { get; set; }
        public string Lokasi3 { get; set; }
        public int Saldo { get; set; }
        public int P99 { get; set; }
        public int QtyTest { get; set; }
        public DateTime Tglproduksi { get; set; }
        public string TglProduksi
        {
            get { return Tglproduksi.ToShortDateString(); }
            set { Tglproduksi.ToShortDateString(); }
        }
        public DateTime Tglserah { get; set; }
        public string TglSerah
        {
            get { return Tglserah.ToShortDateString(); }
            set { Tglserah.ToShortDateString(); }
        }
        public DateTime Tgljemur { get; set; }
        public string TglJemur
        {
            get { return Tgljemur.ToShortDateString(); }
            set { Tgljemur.ToShortDateString(); }
        }
        public DateTime CreatedTime { get; set; }
        public string TglTransaksi
        {
            get { return CreatedTime.ToShortDateString(); }
            set { CreatedTime.ToShortDateString(); }
        }
        public int SisaJemur { get; set; }
        public int Awal { get; set; }
        public int AdjustIn { get; set; }
        public int AdjustOut { get; set; }
        public int Penerimaan { get; set; }
        public int Pengeluaran { get; set; }
        public string Periode { get; set; }
        public string Ukuran { get; set; }
        public int Qty { get; set; }
        public int Qty2 { get; set; }
        public int Qty3 { get; set; }
        public string Oven { get; set; }
        public float M3 { get; set; }
        public float M3_2 { get; set; }
        public float M3_3 { get; set; }
        public decimal Sisa { get; set; }
        public string Line { get; set; }
        public string Group { get; set; }
    }
    public class ReportT1T3Simetris
    {
        public string PartnoSer { get; set; }
        public string LokasiSer { get; set; }
        public string PartnoSm { get; set; }
        public string LokasiSm { get; set; }
        public string PartnoBP { get; set; }
        public string LokasiBP { get; set; }
        public string PartnoBS { get; set; }
        public string LokasiBS { get; set; }
        public string Groups { get; set; }
        public string MCutter { get; set; }
        public int QtyInSm { get; set; }
        public int QtyOutSm { get; set; }
        public int QtyOutBP { get; set; }
        public int QtyOutBS { get; set; }
        public decimal V1 { get; set; }
        public decimal V2 { get; set; }
        public DateTime tglinput { get; set; }
        public string TglInput
        {
            get { return tglinput.ToShortDateString(); }
            set { tglinput.ToShortDateString(); }
        }
        public DateTime Tanggal { get; set; }
        public string TglTransaksi
        {
            get { return Tanggal.ToShortDateString(); }
            set { Tanggal.ToShortDateString(); }
        }
    }

    public class ReportT1T3Pengiriman
    {
        public string Partno { get; set; }
        public string SJNo { get; set; }
        public string Customer { get; set; }
        public string Cust { get; set; }
        public string Pengiriman { get; set; }
        public string JenisPalet { get; set; }
        public string Keterangan { get; set; }
        public string Groups { get; set; }
        public string ScheduleNo { get; set; }
        public string Alamat { get; set; }
        public int Qty { get; set; }
        public int JmlPalet { get; set; }
        public int Tebal { get; set; }
        public int Panjang { get; set; }
        public int Lebar { get; set; }
        public decimal Meter { get; set; }
        public decimal M3 { get; set; }
        public decimal Konversi { get; set; }
        public DateTime CreatedTime { get; set; }
        public string TglInput
        {
            get { return CreatedTime.ToShortDateString(); }
            set { CreatedTime.ToShortDateString(); }
        }
        public DateTime TglKirim { get; set; }
        public string TanggalKirim
        {
            get { return TglKirim.ToShortDateString(); }
            set { TglKirim.ToShortDateString(); }
        }
        public DateTime tglSJ { get; set; }
        public string TglSJ
        {
            get { return tglSJ.ToShortDateString(); }
            set { tglSJ.ToShortDateString(); }
        }
    }
}
