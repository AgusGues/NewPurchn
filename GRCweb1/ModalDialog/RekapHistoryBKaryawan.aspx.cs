using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using Domain;
using System.IO;
using DataAccessLayer;

namespace GRCweb1.ModalDialog
{
    public partial class RekapHistoryBKaryawan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                string str = (Request.QueryString["d"] != null) ? Request.QueryString["d"].ToString() : "";
                if (str != "")
                {
                    string[] data = str.Split(':');
                    //string Bagian = data[2].ToString();
                    string Nama = data[0].ToString();
                    string Department = data[2].ToString();
                    string NIP = data[1].ToString();
                    txtPIC1.Text = Nama.Trim();
                    //txtJabatan1.Text = Bagian.Trim();
                    txtDept1.Text = Department.Trim();
                    GetJabatan(NIP);

                    LoadDataHistory(Nama, NIP);
                }
            }

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Buffer = true;
            //Response.BufferOutput = true;
            //Response.AddHeader("content-disposition", "attachment;filename=RekapPesDetail.xls");
            //Response.Charset = "utf-8";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //string Html = "<b>" + txtJudul.Text.ToUpper() + "</b>";
            //string HtmlEnd = "";
            //lstr.RenderControl(hw);
            //string Contents = sw.ToString();
            //Contents = Contents.Replace("border=\"0", "border=\"1");
            //Response.Write(Html + Contents + HtmlEnd);
            //Response.Flush();
            //Response.End();
        }
        private void GetJabatan(string NIP)
        {
            MasterPES2 ip2 = new MasterPES2();
            PESM2 d = new PESM2();
            d = ip2.RetrieveJabatan(NIP);
            //txtDept1.Text = d.DeptName.ToUpper();
            if (d.BagianName == null)
            {
                txtJabatan1.Text = "DATA BELUM ADA !!!! ";
            }
            else
            {
                txtJabatan1.Text = d.BagianName.Trim().ToUpper();
            }


        }
        //private int adarebobot = 0;
        private void LoadDataHistory(string Nama, string NIP)
        {
            ArrayList arrData = new ArrayList();
            MasterPES2 ip = new MasterPES2();
            arrData = ip.LoadHistory(Nama, NIP);
            lstPES.DataSource = arrData;
            lstPES.DataBind();
        }
        protected void lstPES_DataBound(object sender, RepeaterItemEventArgs e)
        {
            //string str = (Request.QueryString["d"] != null) ? Request.QueryString["d"].ToString() : "";
            //string[] data=str.Split(':');
            //ISO_PES isp=new ISO_PES();
            //#region ItemTemplate
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{

            //}
            //#endregion

        }


    }

    public class MasterPES2
    {
        private ArrayList arrData = new ArrayList();
        private PESM2 pm = new PESM2();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }
        protected string strError = string.Empty;
        private PESM2 objPES = new PESM2();

        public ArrayList LoadHistory(string Nama, string NIP)
        {
            arrData = new ArrayList();
            string strSQL =
            //" select BagianName,MulaiBulan,MulaiTahun,SampaiBulan,SampaiTahun,case when Data2.DeptID=25 then REPLACE(BagianName,'manager ','') when Data2.DeptID=27 and Data2.BagianName like'%ISO%' " +
            //" then 'ISO' when Data2.DeptID=27 and Data2.BagianName not like'%ISO%'  then REPLACE(BagianName,'manager ','') else (select top 1 DeptName from ISO_Dept AA where AA.DeptID=Data2.DeptID and AA.RowStatus>-1)  end DeptName " +
            //" from (select Data1.DeptID,Data1.BagianID,Data1.PIC,(select A.BagianName from ISO_Bagian A where A.ID=Data1.BagianID)BagianName, " +
            //" (select top 1 DATENAME(MONTH,sop.TglMulai) from ISO_SOP sop where sop.PIC=Data1.PIC and sop.BagianID=Data1.BagianID and " +
            //" sop.DeptID=Data1.DeptID and sop.RowStatus>-1 order by TglMulai asc)MulaiBulan, " +
            //" (select top 1 YEAR(sop.TglMulai) from ISO_SOP sop where sop.PIC=Data1.PIC and sop.BagianID=Data1.BagianID and sop.DeptID=Data1.DeptID " +
            //" and sop.RowStatus>-1 order by TglMulai asc)MulaiTahun, " +
            //" (select top 1 DATENAME(MONTH,sop.TglMulai) from ISO_SOP sop where sop.PIC=Data1.PIC and sop.BagianID=Data1.BagianID and " +
            //" sop.DeptID=Data1.DeptID and sop.RowStatus>-1 order by TglMulai desc)SampaiBulan, " +
            //" (select top 1 YEAR(sop.TglMulai) from ISO_SOP sop where sop.PIC=Data1.PIC and sop.BagianID=Data1.BagianID and sop.DeptID=Data1.DeptID " +
            //" and sop.RowStatus>-1 order by TglMulai desc)SampaiTahun " +
            //" from (select DeptID,BagianID,PIC from ISO_SOP where PIC in (select UserName from ISO_Users where ID in (select UserID from UserAccount " +
            //" where NIK='"+NIP+"') group by UserName) and RowStatus>-1 group by DeptID,BagianID,PIC) as Data1 ) as Data2 order by MulaiTahun,SampaiTahun ";
            " select *,case when StatusJabatan='Now' then 'SEKARANG' when StatusJabatan='Past' then Keterangan2 end KeteranganStatus from (select *,(UPPER(MulaiBulan) + ' - ' + MulaiTahun)Keterangan,(UPPER(SampaiBulan) + ' - ' + SampaiTahun)Keterangan2  from ( " +
            " select UPPER(BagianName)BagianName,MulaiBulan,SampaiBulan,CONVERT(varchar(10),MulaiTahun) as MulaiTahun,CONVERT(varchar(10),SampaiTahun) as SampaiTahun,case when Data2.DeptID=25 then UPPER(REPLACE(BagianName,'manager ','')) when Data2.DeptID=27 and Data2.BagianName like'%ISO%' " +
            " then 'ISO' when Data2.DeptID=27 and Data2.BagianName not like'%ISO%'  then UPPER(REPLACE(BagianName,'manager ','')) else (select top 1 UPPER(DeptName) from ISO_Dept AA where AA.DeptID=Data2.DeptID and AA.RowStatus>-1)  end DeptName,case when Data2.BagianID=(select top 1 BagianID from UserAccount  where NIK='" + NIP + "' order by ID desc)  then 'Now' else 'Past' end StatusJabatan " +
            " from (select Data1.DeptID,Data1.BagianID,Data1.PIC,(select A.BagianName from ISO_Bagian A where A.ID=Data1.BagianID)BagianName, " +
            " (select top 1 DATENAME(MONTH,sop.TglMulai) from ISO_SOP sop where sop.PIC=Data1.PIC and sop.BagianID=Data1.BagianID and " +
            " sop.DeptID=Data1.DeptID and sop.RowStatus>-1 order by TglMulai asc)MulaiBulan, " +
            " (select top 1 YEAR(sop.TglMulai) from ISO_SOP sop where sop.PIC=Data1.PIC and sop.BagianID=Data1.BagianID and sop.DeptID=Data1.DeptID " +
            " and sop.RowStatus>-1 order by TglMulai asc)MulaiTahun, " +
            " (select top 1 DATENAME(MONTH,sop.TglMulai) from ISO_SOP sop where sop.PIC=Data1.PIC and sop.BagianID=Data1.BagianID and " +
            " sop.DeptID=Data1.DeptID and sop.RowStatus>-1 order by TglMulai desc)SampaiBulan, " +
            " (select top 1 YEAR(sop.TglMulai) from ISO_SOP sop where sop.PIC=Data1.PIC and sop.BagianID=Data1.BagianID and sop.DeptID=Data1.DeptID " +
            " and sop.RowStatus>-1 order by TglMulai desc)SampaiTahun " +
            " from (select DeptID,BagianID,PIC from ISO_SOP where PIC in (select UserName from ISO_Users where ID in (select UserID from UserAccount " +
            " where NIK='" + NIP + "') group by UserName) and RowStatus>-1 group by DeptID,BagianID,PIC) as Data1 ) as Data2 ) as Data3 ) as Data4 order by MulaiTahun,SampaiTahun ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new PESM2
                    {
                        BagianName = sdr["BagianName"].ToString(),
                        MulaiBulan = sdr["MulaiBulan"].ToString(),
                        SampaiBulan = sdr["SampaiBulan"].ToString(),
                        MulaiTahun = sdr["MulaiTahun"].ToString(),
                        SampaiTahun = sdr["SampaiTahun"].ToString(),
                        DeptName = sdr["DeptName"].ToString(),
                        StatusJabatan = sdr["StatusJabatan"].ToString(),
                        KETERANGAN = sdr["KETERANGAN"].ToString(),
                        KeteranganStatus = sdr["KeteranganStatus"].ToString(),
                        KETERANGAN2 = sdr["KETERANGAN2"].ToString(),
                    });
                }
            }
            return arrData;
        }

        public PESM2 RetrieveJabatan(string NIP)
        {
            string strSQL = string.Empty;

            strSQL =
                     //" select A.BagianName from (select top 1 BagianID,ThnBln,DeptID from (select BagianID,LEFT(convert(char,tglmulai,112),6)ThnBln,DeptID from ISO_SOP " +
                     //     " where PIC in (select UserName from ISO_Users where ID in (select UserID from UserAccount where NIK='" + NIP + "')) " +
                     //     " and RowStatus>-1) as Data1 order by ThnBln desc) as Data2 INNER JOIN ISO_Bagian A ON Data2.BagianID=A.ID and A.DeptID=Data2.DeptID " +
                     //     " union " +
                     " select top 1 A1.ID,BagianName from UserAccount A1 INNER JOIN ISO_Bagian B1 ON A1.BagianID=B1.ID and A1.DeptID=B1.DeptID where A1.NIK='" + NIP + "' order by ID desc ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_Jabatan(sqlDataReader);
                }
            }

            return new PESM2();
        }

        public PESM2 GenerateObject_Jabatan(SqlDataReader sqlDataReader)
        {
            objPES = new PESM2();
            objPES.BagianName = sqlDataReader["BagianName"].ToString();
            return objPES;
        }
    }

    public class PESM2
    {
        public string NO { get; set; }
        public string NIP { get; set; }
        public string NAMA { get; set; }
        public string DEPARTMENT { get; set; }
        public string TGLMASUK { get; set; }
        public string BAGIAN { get; set; }
        public string KETERANGAN { get; set; }
        public string MulaiBulan { get; set; }
        public string SampaiBulan { get; set; }
        public string MulaiTahun { get; set; }
        public string SampaiTahun { get; set; }
        public string StatusJabatan { get; set; }
        public string KeteranganStatus { get; set; }
        public string KETERANGAN2 { get; set; }


        public int ID { get; set; }
        public string Desk { get; set; }
        public string TypeBobot { get; set; }
        public int TypePes { get; set; }
        public int ISOUserID { get; set; }
        public string Target { get; set; }
        public string SOPNo { get; set; }
        public string SOPName { get; set; }
        public decimal BobotNilai { get; set; }
        public string PIC { get; set; }
        public string Tahun { get; set; }
        public decimal Score { get; set; }
        public decimal TotalBobot { get; set; }
        public decimal TotalNilai { get; set; }
        public string Pencapaian { get; set; }
        public decimal Nilai { get; set; }
        public int DeptID { get; set; }
        public int BagianID { get; set; }
        public string DeptName { get; set; }
        public string BagianName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PESName { get; set; }
        public string Checking { get; set; }
        public string NIK { get; set; }
    }
}
