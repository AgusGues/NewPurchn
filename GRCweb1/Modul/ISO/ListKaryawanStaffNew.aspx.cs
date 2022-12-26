using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.ISO
{
    public partial class ListKaryawanStaffNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = Request.QueryString["path"];
            if (path != null)
            {
                System.Drawing.Bitmap img = new System.Drawing.Bitmap(path);
                img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadKaryawanStaff();
            }

            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            LoadKaryawanStaff();
        }

        protected void btnExport_ServerClick(object sender, EventArgs e)
        {
            foreach (RepeaterItem img in lstKaryawanStaff.Items)
            {


                Repeater rpt = (Repeater)img.FindControl("attachPrs");
                foreach (RepeaterItem rm in rpt.Items)
                {

                    Image im = (Image)rm.FindControl("lihatprs");
                    Image im1 = (Image)rm.FindControl("hapusprs");

                    im.Visible = false;
                    im1.Visible = false;

                }

                Repeater rpt1 = (Repeater)img.FindControl("attachOJD"); //2
                foreach (RepeaterItem rm in rpt1.Items)
                {

                    Image im = (Image)rm.FindControl("lihatprs");
                    Image im1 = (Image)rm.FindControl("hapusprs");
                    im.Visible = false;
                    im1.Visible = false;

                }

                Repeater rpt2 = (Repeater)img.FindControl("attachjuri");
                foreach (RepeaterItem rm in rpt2.Items)
                {

                    Image im = (Image)rm.FindControl("lihatprs");
                    Image im1 = (Image)rm.FindControl("hapusprs");
                    im.Visible = false;
                    im1.Visible = false;

                }

                ImageButton imx = img.FindControl("attprs") as ImageButton;
                ImageButton imx1 = img.FindControl("attojd") as ImageButton;
                ImageButton imx2 = img.FindControl("attjuri") as ImageButton;

                HtmlTableCell tc = img.FindControl("grid1") as HtmlTableCell;
                HtmlTableCell tc1 = img.FindControl("grid2") as HtmlTableCell;
                HtmlTableCell tc2 = img.FindControl("grid3") as HtmlTableCell;

                //TableHeaderCell t = img.FindControl("t1") as TableHeaderCell;
                //TableHeaderCell t1 = img.FindControl("t2") as TableHeaderCell;
                //TableHeaderCell t2 = img.FindControl("t3") as TableHeaderCell;

                imx.Visible = false;
                imx1.Visible = false;
                imx2.Visible = false;

                tc.Visible = false;
                tc1.Visible = false;
                tc2.Visible = false;

                //t.Visible = false;
                //t1.Visible = false;
                //t2.Visible = false;

            }

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListKaryawanStaff.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<H2>LIST KARYAWAN STAFF</H2>";
            //Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + " &nbsp; " + ddlTahun.SelectedValue.ToString();
            Html += "";
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();


        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET server 
            control at run time. Used to avoid issue using RenderControl above */
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            LoadKaryawanStaff();
        }
        protected void lstKaryawanStaff_DataBound(object sender, RepeaterItemEventArgs e)
        {
            PESM2 pm = (PESM2)e.Item.DataItem;

            //Repeater lstKaryawanStaff = (Repeater)e.Item.FindControl("lstKaryawanStaff");
            //string data = pm.NAMA + ":" + pm.NIP + ":" + pm.DEPARTMENT;

            if (pm.NIP != null)
            {
                PESM2 pes1 = new PESM2();
                MasterPES2 pesf = new MasterPES2();
                int ttl = pesf.TotalUser(pm.NIP);
                if (ttl > 1)
                {
                    PESM2 pes2 = new PESM2();
                    MasterPES2 pesf2 = new MasterPES2();
                    int ttl2 = pesf2.TotalUser_Alias(pm.NIP);
                    if (ttl2 == 0)
                    {
                        PESM2 pes10 = new PESM2();
                        MasterPES2 pesf10 = new MasterPES2();

                        int result = 0;
                        pes10.NIP = pm.NIP;
                        pes10.NAMA = pesf10.CekNama(pm.NIP);
                        result = pesf10.InsertData(pes10);
                        if (result > 0)
                        {
                            LoadKaryawanStaff();
                        }
                    }
                }

                //if (ttl == 0)
                //{
                //    //PESM2 pes10 = new PESM2();
                //    //MasterPES2 pesf10 = new MasterPES2();

                //    //int result = 0;
                //    //pes10.NIP = pm.NIP;
                //    //pes10.NAMA = pm.NAMA;
                //    //result = pesf10.InsertData(pes10);

                //}
            }

            Repeater lstKaryawanStaff = (Repeater)e.Item.FindControl("lstKaryawanStaff");
            string data = pm.NAMA + ":" + pm.NIP + ":" + pm.DEPARTMENT;


            HtmlTableRow tr0 = (HtmlTableRow)e.Item.FindControl("ps1");
            tr0.Cells[2].Attributes.Add("onclick", "return loadHistory('" + data.ToString() + "')");

            tr0.Cells[2].Attributes.Add("title", "Click for detail history bagian");

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps1");
                for (int i = 1; i < tr.Cells.Count; i++)
                {
                    if (pm.KETERANGAN == "BELUM TERDAFTAR")
                    {
                        tr.Cells[i].Attributes.Add("style", "background-color:Red; color:White;");
                    }
                    else if (pm.BAGIAN.Contains("IN ACTING"))
                    {
                        tr.Cells[i].Attributes.Add("style", "background-color:Green; color:White;");
                    }
                }
            }
            if (pm.Tgl_Sosialisasi.ToString("dd/MM/yyyy") == "01/01/1900")
                tr0.Cells[7].InnerHtml = "";
            else
                tr0.Cells[7].InnerHtml = pm.Tgl_Sosialisasi.ToString("dd MMM yyyy");

            if (pm.Tgl_OJD.ToString("dd/MM/yyyy") == "01/01/1900")
                tr0.Cells[10].InnerHtml = "";
            else
                tr0.Cells[10].InnerHtml = pm.Tgl_OJD.ToString("dd MMM yyyy");
            if (pm.Tgl_Penjurian.ToString("dd/MM/yyyy") == "01/01/1900")
                tr0.Cells[13].InnerHtml = "";
            else
                tr0.Cells[13].InnerHtml = pm.Tgl_Penjurian.ToString("dd MMM yyyy");
            Repeater rpts = (Repeater)e.Item.FindControl("attachPrs");
            ImageButton imgs = (ImageButton)e.Item.FindControl("attPrs");
            LoadListAttachmentPrs(pm.NIP, rpts);
            Image att = (Image)e.Item.FindControl("attPrs");
            att.Visible = true;
            att.Attributes.Add("onclick", "OpenDialog2('" + pm.NIP + "&tp=1')");

            Repeater rptso = (Repeater)e.Item.FindControl("attachOJD");
            ImageButton imgso = (ImageButton)e.Item.FindControl("attOJD");
            LoadListAttachmentOJD(pm.NIP, rptso);
            Image atto = (Image)e.Item.FindControl("attOJD");
            atto.Visible = true;
            atto.Attributes.Add("onclick", "OpenDialog3('" + pm.NIP + "&tp=1')");

            Repeater rptsp = (Repeater)e.Item.FindControl("attachjuri");
            ImageButton imgsp = (ImageButton)e.Item.FindControl("attjuri");
            LoadListAttachmentPenjurian(pm.NIP, rptsp);
            Image attp = (Image)e.Item.FindControl("attjuri");
            attp.Visible = true;
            attp.Attributes.Add("onclick", "OpenDialog4('" + pm.NIP + "&tp=1')");
        }

        private void LoadListAttachmentPrs(string BAID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            //if (Convert.ToInt32(BAID) == 0)
            //    return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from iso_sosialisasipes where rowstatus>-1 and NIP='" + BAID + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_SosialisasiPES
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            Attachment = sdr["Attachment"].ToString(),
                        });
                    }
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }
        private void LoadListAttachmentOJD(string BAID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            //if (Convert.ToInt32(BAID) == 0)
            //    return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from iso_OJDpes where rowstatus>-1 and NIP='" + BAID + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_SosialisasiPES
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            Attachment = sdr["Attachment"].ToString(),
                        });
                    }
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }
        private void LoadListAttachmentPenjurian(string BAID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            //if (Convert.ToInt32(BAID) == 0)
            //    return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from iso_Penjurianpes where rowstatus>-1 and NIP='" + BAID + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_SosialisasiPES
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            Attachment = sdr["Attachment"].ToString(),
                        });
                    }
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }
        protected void attachPrs_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "preprs":
                        string Nama = e.CommandArgument.ToString();
                        string Nama2 = @"\" + Nama;
                        string dirPath = @"D:\DATA LAMPIRAN PURCHN\Sosialisasi pes\";
                        string ext = Path.GetExtension(Nama);

                        Response.Clear();
                        string excelFilePath = dirPath + Nama;
                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                        if (file.Exists)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            Response.AddHeader("Content-Length", file.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(file.FullName);
                            Response.End();
                        }

                        break;
                    case "hpsprs":
                        Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapusprs");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "Update iso_sosialisasipes set RowStatus=-1 where ID=" + hps.CssClass;
                        SqlDataReader sdr = zl.Retrieve();
                        break;
                }
                LoadKaryawanStaff();
            }
            catch
            {
                DisplayAJAXMessage(this, "Data Error");
                return;
            }
        }
        protected void attachPrs_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatprs") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihatprs");
                Image hps = (Image)e.Item.FindControl("hapusprs");
                ISO_SosialisasiPES att = (ISO_SosialisasiPES)e.Item.DataItem;
                //hps.Visible =  true ;
                //hps.Visible = true ;

                if (users.Flag == 4)
                {
                    hps.Visible = true;
                }
                else
                {
                    hps.Visible = false;
                }
            }
        }
        protected void attachjuri_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            //try
            //{
            switch (e.CommandName)
            {
                case "preprs":
                    string Nama = e.CommandArgument.ToString();
                    string Nama2 = @"\" + Nama;
                    string dirPath = @"D:\DATA LAMPIRAN PURCHN\OJD pes\";
                    string ext = Path.GetExtension(Nama);

                    Response.Clear();
                    string excelFilePath = dirPath + Nama;
                    System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "application/octet-stream";
                        Response.WriteFile(file.FullName);
                        Response.End();
                    }

                    break;
                case "hpsprs":
                    Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapusprs");
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "Update iso_ojdpes set RowStatus=-1 where ID=" + hps.CssClass;
                    SqlDataReader sdr = zl.Retrieve();
                    break;
            }
            LoadKaryawanStaff();
            //}
            //catch
            //{
            //    DisplayAJAXMessage(this, "Data Error");
            //    return;
            //}
        }
        protected void attachOJD_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            //try
            //{
            switch (e.CommandName)
            {
                case "preprs":
                    string Nama = e.CommandArgument.ToString();
                    string Nama2 = @"\" + Nama;
                    string dirPath = @"D:\DATA LAMPIRAN PURCHN\OJD pes\";
                    string ext = Path.GetExtension(Nama);

                    Response.Clear();
                    string excelFilePath = dirPath + Nama;
                    System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "application/octet-stream";
                        Response.WriteFile(file.FullName);
                        Response.End();
                    }

                    break;
                case "hpsprs":
                    Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapusprs");
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "Update iso_ojdpes set RowStatus=-1 where ID=" + hps.CssClass;
                    SqlDataReader sdr = zl.Retrieve();
                    break;
            }
            LoadKaryawanStaff();
            //}
            //catch
            //{
            //    DisplayAJAXMessage(this, "Data Error");
            //    return;
            //}
        }
        protected void attachOJD_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatprs") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihatprs");
                Image hps = (Image)e.Item.FindControl("hapusprs");
                ISO_SosialisasiPES att = (ISO_SosialisasiPES)e.Item.DataItem;
                //hps.Visible =  true ;
                //hps.Visible = true ;

                if (users.Flag == 4)
                {
                    hps.Visible = true;
                }
                else
                {
                    hps.Visible = false;
                }
            }
        }
        protected void attachjuri_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatprs") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihatprs");
                Image hps = (Image)e.Item.FindControl("hapusprs");
                ISO_SosialisasiPES att = (ISO_SosialisasiPES)e.Item.DataItem;
                //hps.Visible =  true ;
                //hps.Visible = true ;

                if (users.Flag == 4)
                {
                    hps.Visible = true;
                }
                else
                {
                    hps.Visible = false;
                }
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private void LoadKaryawanStaff()
        {
            MasterPES2 rps = new MasterPES2();
            ArrayList arrListKaryS = new ArrayList();

            arrListKaryS = rps.RetrieveListKaryawanStaff();
            lstKaryawanStaff.DataSource = arrListKaryS;
            lstKaryawanStaff.DataBind();
        }

        protected void lstKaryawanStaff_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }

    public class MasterPES2
    {
        protected string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private PESM2 pm = new PESM2();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }
        private List<SqlParameter> sqlListParam;

        public int InsertData(object objDomain)
        {
            try
            {
                pm = (PESM2)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NIP", pm.NIP));
                sqlListParam.Add(new SqlParameter("@NAMA", pm.NAMA));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "Insert_UserAccountAlias");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public ArrayList RetrieveListKaryawanStaff()
        {
            arrData = new ArrayList();
            string strSQL =
            " select ROW_NUMBER() over (order by data3.ID asc ) as NO,data3.NIP,NAMA,TGLMASUK,BAGIAN,DEPARTMENT, KETERANGAN," +
            "isnull(tgl_sosialisasi,'1/1/1900')tgl_sosialisasi,isnull(tgl_OJD,'1/1/1900')tgl_OJD,isnull(tgl_Penjurian,'1/1/1900')tgl_Penjurian   from " +
            " (select 'A'ID,NIP,NAMA,UPPER(TGLMASUK)TGLMASUK,UPPER(BAGIAN)BAGIAN," +
            " case " +
            " when Data2.DEPTID=25 and BAGIAN like'%manager%' then (UPPER(REPLACE(BAGIAN,'Manager ',''))) " +
            " when Data2.DEPTID=27 and BAGIAN like'%ISO%' then 'ISO' " +
            " when Data2.DEPTID=27 and BAGIAN like'%manager %' then (UPPER(REPLACE(BAGIAN,'Manager ',''))) else UPPER(DEPARTMENT) end DEPARTMENT, " +
            " SYSTEMPES KETERANGAN from (" +
            " select NIP,DeptID,Data1.UserName NAMA," +
            " case " +
            " when Flag in (1,4) then (select top 1 BagianName from iso_bagian AA2 where AA2.ID=Data1.BagianID and AA2.RowStatus>-1) " +
            " when Flag=2 then (select top 1 Jabatan from hrd.dbo.KARYAWAN_STAFF AA3 where AA3.NIP=Data1.NIP) " +
            " when Flag=3 then (select top 1 Jabatan from hrd.dbo.DEPJABATAN AA3 where  AA3.JABID=Data1.JabID and AA3.DEPTID=Data1.DEPTID2) end BAGIAN," +
            " case " +
            " when Flag in (1,3,4) then (select top 1 DeptName from ISO_Dept AA where AA.DeptID=Data1.DEPTID and AA.RowStatus>-1) " +
            " when Flag=2 then (select Departement_Staff from hrd.dbo.Staff_Departement AA1 where AA1.DeptId_Staff=Data1.DEPTID ) end DEPARTMENT," +

            " LEFT(CONVERT(CHAR,Data1.TGLMASUK,106),11)TGLMASUK," +
            " case when Flag in (1,3) then 'TERDAFTAR' when Flag in (2,4) then 'BELUM TERDAFTAR' end 'SYSTEMPES'  from (" +


            " select NIP,case when TotalUserName>1 then (select A.UserNameAlias from UserAccount_Alias A where A.NIK=NIP and A.RowStatus>-1) else " +
            "(select top 1 B2.UserName from UserAccount B2 where B2.NIK=xx2.NIP and xx2.BagianID=B2.BagianID and RowStatus>-1) end 'UserName',DEPTID,DeptID2,BagianID,JabID,Flag,TGLMASUK " +
            " from (select (COUNT(UserName))TotalUserName,NIP,DEPTID,DeptID2,BagianID,JabID,Flag,TGLMASUK from (select B.UserName,A.NIP,(select top 1 DeptID from UserAccount uc0 where uc0.NIK=B.NIK and uc0.RowStatus>-1 order by ID desc)DEPTID,''DeptID2, " +
            "(select top 1 BagianID from UserAccount uc where uc.NIK=B.NIK and uc.RowStatus>-1 order by uc.ID desc)BagianID,''JabID,'1'Flag,A.TGLMASUK " +
            " from  [hrd].dbo.KARYAWAN_STAFF A " +
            " INNER JOIN (select NIk,UserName from UserAccount where RowStatus>-1 and UserID in (select UserID from ISO_UserCategory where RowStatus>-1) group by NIK,UserName) B ON A.NIP=B.NIK   " +
            " where NIP in (select nik from UserAccount where RowStatus>-1) and A.RowStatus>-1 ) as xx1 group by xx1.NIP,DEPTID,DeptID2,BagianID,JabID,Flag,TGLMASUK ) as xx2 " +

            " union all " +

            " select A.NIP,B.UserName,(select top 1 DeptID from UserAccount uc0 where uc0.NIK=B.NIK and uc0.RowStatus>-1 order by ID desc)DEPTID,''DeptID2," +
            " (select top 1 BagianID from UserAccount uc where uc.NIK=B.NIK and uc.RowStatus>-1 order by uc.ID desc)BagianID,''JabID,'4'Flag,A.TGLMASUK " +
            " from  [hrd].dbo.KARYAWAN_STAFF A " +
            " INNER JOIN (select NIk,UserName from UserAccount where RowStatus>-1 and UserID not in (select UserID from ISO_UserCategory where RowStatus>-1) group by NIK,UserName) B ON A.NIP=B.NIK " +
            " where NIP in (select nik from UserAccount where RowStatus>-1) " +

            " union all " +

            " select A.NIP,A.NAMA,A.DEPTID,''DeptID2,A.JabatanID BagianID,''JabID,'2'Flag,A.TGLMASUK  from  [hrd].dbo.KARYAWAN_STAFF A LEFT JOIN UserAccount B " +
            " ON A.NIP=B.NIK where (A.TGLKELUAR is null and B.ID is NULL)" +

            " union all " +

            " select NIK,UserName,DeptID,DeptID2,BagianID,JabID,Flag,TGLMASUK from (select A.NIK,A.UserName,A.DEPTID,B.DEPTID DeptID2,''BagianID," +
            " B.JabatanID JabID,'3'Flag,B.TGLMASUK from UserAccount A " +
            " LEFT JOIN [hrd].dbo.KARYAWAN B  ON A.NIK=B.NIP  " +
            " where (B.TGLKELUAR is null and B.ID is not NULL and A.RowStatus>-1 and A.Lock is null)) as Data1 " +
            " where Data1.NIK not in (select A.NIP from [hrd].dbo.KARYAWAN_STAFF A where A.TGLKELUAR is null) " +

            " ) as Data1 ) as Data2 ) as Data3 left join (select distinct NIP,isnull(tgl_sosialisasi,'1/1/1900')tgl_sosialisasi " +
            "from iso_sosialisasipes where rowstatus>-1 )so on Data3.NIP=so.NIP " +
            "left join (select distinct NIP,isnull(tgl_OJD,'1/1/1900')tgl_OJD from iso_OJDPES where rowstatus>-1 )soj on Data3.NIP=soj.NIP " +
            "left join (select distinct NIP,isnull(tgl_Penjurian,'1/1/1900')tgl_Penjurian from iso_PenjurianPES where rowstatus>-1 )sopj on Data3.NIP=sopj.NIP order by KETERANGAN,DEPARTMENT,NIP ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new PESM2
                    {
                        NO = sdr["NO"].ToString(),
                        NIP = sdr["NIP"].ToString(),
                        NAMA = sdr["NAMA"].ToString(),
                        DEPARTMENT = sdr["DEPARTMENT"].ToString(),
                        TGLMASUK = sdr["TGLMASUK"].ToString(),
                        BAGIAN = sdr["BAGIAN"].ToString(),
                        KETERANGAN = sdr["KETERANGAN"].ToString(),
                        Tgl_Sosialisasi = DateTime.Parse(sdr["Tgl_Sosialisasi"].ToString()),
                        Tgl_OJD = DateTime.Parse(sdr["Tgl_OJD"].ToString()),
                        Tgl_Penjurian = DateTime.Parse(sdr["Tgl_Penjurian"].ToString())
                    });
                }
            }
            return arrData;
        }

        public int TotalUser(string NIK)
        {

            string StrSql =
            " select count(ID)ttl from UserAccount where NIK='" + NIK + "' and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ttl"]);
                }
            }

            return 0;
        }

        public int TotalUser_Alias(string NIK)
        {

            string StrSql =
            " select count(ID)ttl from UserAccount_Alias where NIK='" + NIK + "' and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ttl"]);
                }
            }

            return 0;
        }

        public string CekNama(string NIK)
        {
            string result = "0";
            string StrSql = " select top 1 NAMA from hrd.dbo.KARYAWAN_STAFF where NIP='" + NIK + "' and TGLKELUAR is null order by id desc ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["NAMA"].ToString();
                }
            }

            return result;
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
        public DateTime Tgl_Sosialisasi { get; set; }
        public string Attachment { get; set; }
        public DateTime Tgl_OJD { get; set; }
        public string AttachOJD { get; set; }
        public DateTime Tgl_Penjurian { get; set; }
        public string AttachPenjurian { get; set; }

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

    public class ISO_SosialisasiPES : GRCBaseDomain
    {
        public string Attachment { get; set; }
    }

}