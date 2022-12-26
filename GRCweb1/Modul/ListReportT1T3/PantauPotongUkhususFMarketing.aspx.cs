using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class PantauPotongUkhususFMarketing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                string[] costpj = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("costpj", "CostControl").Split(',');
                int post = Array.IndexOf(costpj, ((Users)Session["Users"]).UserName.ToString());
                btnSimpan.Enabled = true;
                LoadTahun();
                LoadBulan();
                Session["Nilai"] = null;
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadTahun()
        {
            PakaiFacade pd = new PakaiFacade();
            pd.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i < 13; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
            priviewrpt();

        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session["Nilai"] = null;
            btnSimpan.Enabled = true;
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                TextBox txt = (TextBox)lstMatrix.Items[i].FindControl("jmlDelivery");
                txt.Text = "";
            }
            Response.Redirect("BudgetingLogistik.aspx");
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            if (Session["Nilai"] != null)
            {
                ArrayList arrD = (ArrayList)Session["Nilai"];
                foreach (T3_PantauPUM pj in arrD)
                {
                    T3_PantauPUM pjj = new T3_PantauPUM();
                    pjj = pj;
                    if (pjj.Partno.Trim() != string.Empty)
                        InsertNilai(pjj);
                }
                priviewrpt();
            }
        }
        protected void InsertNilai(object objDomain)
        {
            string thnbln = ddlTahun.Text + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            T3_PantauPUM obj = new T3_PantauPUM();
            obj = (T3_PantauPUM)objDomain;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "if (select count(partno) from T3_PantauPotongUKhusus where rowstatus>-1 and partno='" + obj.Partno + "' and thnbln='" + thnbln + "')=0 begin " +
                "insert T3_PantauPotongUKhusus(Partno, Target,Pencapaian, Customer, SisaStock, Persen,thnbln,rowstatus) values ('" + obj.Partno +
                "'," + obj.target + "," + obj.Pencapaian + "," + obj.Customer + "," + obj.SisaStock + "," + obj.Persen + ",'" + obj.thnbln + "',0)end " +
                "else begin update T3_PantauPotongUKhusus set Target=" + obj.target + ",Pencapaian=" + obj.Pencapaian + ", Customer=" + obj.Customer +
                ", SisaStock=" + obj.SisaStock + ", Persen=" + obj.Persen + " where Partno='" + obj.Partno + "' and thnbln='" + thnbln + "'  end";
            SqlDataReader sdr = zl.Retrieve();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                TextBox jml = (TextBox)lstMatrix.Items[i].FindControl("jmlDelivery");
                Label txtjml = (Label)lstMatrix.Items[i].FindControl("txtjmlDelivery");
                HiddenField hd = (HiddenField)lstMatrix.Items[i].FindControl("txtNilaiID");
                if (hd != null) { hd.Visible = false; }
                if (jml != null) { jml.Visible = false; }
                if (txtjml != null) { txtjml.Visible = true; }
            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PantauUKhusus.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>PEMANTAUAN PEMOTONGAN UKURAN KHUSUS DARI MARKETING</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedItem.Text;
            Html += "";
            string HtmlEnd = "";
            Div5.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            priviewrpt();
        }
        protected void priviewrpt()
        {
            string thnbln = ddlTahun.Text + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select  ROW_NUMBER() OVER (ORDER BY Partno) Urutan, Partno, Target,isnull(sa,0)sa, " +
                "Pencapaian, Customer, SisaStock, Persen from T3_PantauPotongUKhusus where thnbln ='" + thnbln + "' " +
                "union all select (select count(partno) +1  from T3_PantauPotongUKhusus where thnbln ='" + thnbln + "') Urutan, " +
                "'' Partno, 0 Target, 0 sa,0 Pencapaian, 0 Customer, 0 SisaStock, 0 Persen ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObject(sdr));
                }
            }
            lstMatrix.DataSource = arrData;
            lstMatrix.DataBind();
            lstMatrix2.DataSource = arrData;
            lstMatrix2.DataBind();
            txtPartno_TextChanged(null, null);
            #region update sarmut Logistik finishgood
            //string sarmutPrs = "Efisiensi Budget Logistik ";
            //string strJmlLine = string.Empty;
            ////string strDept = "TRANS";
            //int deptid = getDeptID("LOGISTIK FINISHGOOD");
            //string actual = (Convert.ToDecimal(Session["actual"].ToString())).ToString("0.##").Replace(",", ".");

            //ZetroView zl1 = new ZetroView();
            //zl1.QueryType = Operation.CUSTOM;
            //zl1.CustomQuery = "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
            //    "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
            //    "update SPD_TransPrs set actual=" + actual + " where Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
            //    "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')";
            //SqlDataReader sdr1 = zl1.Retrieve();
            #endregion

        }
        private int GetTotalPotong(string thnbln, string partno)
        {
            int result = 0;

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from (select (select partno from fc_items where id=vw_KartuStockBJNew.itemid) partno,sum(qty) total " +
            "from vw_KartuStockBJNew where qty>-1 and left(convert(char,tanggal,112),6)='" + thnbln + "' group by itemid)A where partno='" + partno + "' ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["total"].ToString()); ;
                }
            }
            return result;
        }
        private int GetTotalKirim(string thnbln, string partno)
        {
            int result = 0;

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from (select (select partno from fc_items where id=vw_KartuStockBJNew.itemid)" +
                " partno,sum(qty * -1) total from vw_KartuStockBJNew where qty<0 and left(convert(char,tanggal,112),6)='" + thnbln
                + "'  group by itemid)A where partno='" + partno + "' ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["total"].ToString()); ;
                }
            }
            return result;
        }
        private int GetSA(string thnbln, string partno)
        {
            int result = 0;
            string strBln = string.Empty;
            int thn = 0;
            if (thnbln.Substring(4, 2) == "01") { strBln = "desqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)) - 1; }
            if (thnbln.Substring(4, 2) == "02") { strBln = "janqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            if (thnbln.Substring(4, 2) == "03") { strBln = "febqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            if (thnbln.Substring(4, 2) == "04") { strBln = "marqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            if (thnbln.Substring(4, 2) == "05") { strBln = "aprqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            if (thnbln.Substring(4, 2) == "06") { strBln = "meiqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            if (thnbln.Substring(4, 2) == "07") { strBln = "junqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            if (thnbln.Substring(4, 2) == "08") { strBln = "julqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            if (thnbln.Substring(4, 2) == "09") { strBln = "aguqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            if (thnbln.Substring(4, 2) == "10") { strBln = "sepqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            if (thnbln.Substring(4, 2) == "11") { strBln = "oktqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            if (thnbln.Substring(4, 2) == "12") { strBln = "novqty"; thn = Convert.ToInt32(thnbln.Substring(0, 4)); }
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from (select (select partno from fc_items where id=saldoinventoryBJ.itemid)" +
                " partno,sum(" + strBln + ") total from saldoinventoryBJ where yearperiod=" + thn
                + "  group by itemid)A where partno='" + partno + "' ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(decimal.Parse(sdr["total"].ToString()));
                }
            }
            return result;
        }
        private decimal TotalPakai = 0;
        private decimal TotalStd = 0;
        string nilai = string.Empty;
        decimal persena = 0;
        decimal persenb = 0;
        decimal persennn = 0;
        int perss = 0;
        decimal persena2 = 0;
        decimal persenb2 = 0;
        decimal persen2 = 0;
        int perss2 = 0;
        protected void lstMatrix_DataBound(object sender, RepeaterItemEventArgs e)
        {
            T3_PantauPUM pj = (T3_PantauPUM)e.Item.DataItem;
            TextBox jml = (TextBox)e.Item.FindControl("TxtTarget");
            TextBox txtPartno = (TextBox)e.Item.FindControl("txtPartno");
            Label lblketerangan = (Label)e.Item.FindControl("lblketerangan");
            jml.Text = pj.target.ToString();
            txtPartno.Text = pj.Partno;

        }
        protected void lstMatrix2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            T3_PantauPUM pj = (T3_PantauPUM)e.Item.DataItem;
            Label jml = (Label)e.Item.FindControl("TxtTarget");
            Label txtPartno = (Label)e.Item.FindControl("txtPartno");
            Label lblketerangan = (Label)e.Item.FindControl("lblketerangan");
            jml.Text = pj.target.ToString();
            txtPartno.Text = pj.Partno;
            lblketerangan.Text = pj.keterangan;

            

        }
        protected void txtPartno_TextChanged(object sender, EventArgs e)
        {
            decimal tPotong = 0; decimal tkirim = 0; int tTarget = 0; int tSA = 0; decimal tBEY = 0; decimal tKPL = 0; decimal tKKR = 0; decimal tPPE = 0;
            ArrayList arrData = /*(Session["Nilai"] != null) ? (ArrayList)Session["Nilai"] :*/ new ArrayList();
            string thnbln = ddlTahun.Text + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            CostCenterFacade cp = new CostCenterFacade();
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst");
                T3_PantauPUM pj = new T3_PantauPUM();
                TextBox jml = (TextBox)lstMatrix.Items[i].FindControl("TxtTarget");
                TextBox txtPartno = (TextBox)lstMatrix.Items[i].FindControl("txtPartno");
                HiddenField hd = (HiddenField)lstMatrix.Items[i].FindControl("txtNilaiID");
                if (jml.Text != string.Empty && txtPartno.Text != string.Empty)
                {
                    tSA = GetSA(thnbln, txtPartno.Text);
                    tPotong = GetTotalPotong(thnbln, txtPartno.Text);
                    tkirim = GetTotalKirim(thnbln, txtPartno.Text);
                    tTarget = tTarget + Int32.Parse(jml.Text);
                    if (txtPartno.Text != string.Empty)
                    {
                        tr.Cells[3].InnerHtml = (tSA).ToString("N0");
                        tr.Cells[4].InnerHtml = (tPotong).ToString("N0");
                        tr.Cells[5].InnerHtml = (tkirim).ToString("N0");
                        tr.Cells[6].InnerHtml = (tSA + tPotong - tkirim).ToString("N0");
                        tr.Cells[7].InnerHtml = ((tPotong + tSA) / Convert.ToDecimal(jml.Text) * 100).ToString("N0");
                        var a = tr.Cells[7].InnerHtml;
                        if (a == "100")
                        {
                            tr.Cells[8].InnerHtml = "TERCAPAI";
                        }
                        else
                        {
                            tr.Cells[8].InnerHtml = "TIDAK TERCAPAI";
                        }
                    }
                    pj.thnbln = thnbln;
                    pj.Partno = txtPartno.Text;
                    pj.target = int.Parse(jml.Text);
                    pj.sa = Convert.ToInt32(tSA);
                    pj.Pencapaian = Convert.ToInt32(tPotong);
                    pj.Customer = Convert.ToInt32(tkirim);
                    pj.SisaStock = Convert.ToInt32(tSA + tPotong - tkirim);
                    pj.Persen = decimal.Parse(tr.Cells[6].InnerHtml);
                    var bb = tr.Cells[7].InnerHtml;
                    if (bb == "100")
                    {
                        pj.keterangan = "TERCAPAI";
                    }
                    else
                    {
                        pj.keterangan = "TIDAK TERCAPAI";
                    }
                    pj.RowStatus = 0;
                    pj.CreatedBy = ((Users)Session["Users"]).UserName;

                    if (tr.Cells[8].InnerHtml == "TERCAPAI")
                    {
                        persena += 1;
                    }
                    if (pj.target != 0)
                    {
                        persenb += 1;
                    }
                    if (persenb != 0)
                    {
                        persennn = (persena / persenb) * 100;
                    }
                    if (persennn == 100)
                    {
                        perss = 100;
                    }
                    else
                    {
                        perss = 20;
                    }
                    //persenb = (IList((Repeater)Container.Parent).DataSource).Count;

                    lblpencapaian.Text = "Pencapaian KPI bulan  " + ddlBulan.SelectedItem + " " + ddlTahun.Text + ":";
                    lblpersen.Text = persennn.ToString("N0");
                    lblnilai.Text = "Nilai " + perss.ToString();
                }
                arrData.Add(pj);

                HtmlTableRow tr2 = (HtmlTableRow)lstMatrix2.Items[i].FindControl("lst2");
                T3_PantauPUM pj2 = new T3_PantauPUM();
                TextBox jml2 = (TextBox)lstMatrix.Items[i].FindControl("TxtTarget");
                TextBox txtPartno2 = (TextBox)lstMatrix.Items[i].FindControl("txtPartno");
                HiddenField hd2 = (HiddenField)lstMatrix.Items[i].FindControl("txtNilaiID");
                if (jml.Text != string.Empty && txtPartno.Text != string.Empty)
                {
                    tSA = GetSA(thnbln, txtPartno.Text);
                    tPotong = GetTotalPotong(thnbln, txtPartno.Text);
                    tkirim = GetTotalKirim(thnbln, txtPartno.Text);
                    tTarget = tTarget + Int32.Parse(jml.Text);
                    if (txtPartno.Text != string.Empty)
                    {
                        tr2.Cells[3].InnerHtml = (tSA).ToString("N0");
                        tr2.Cells[4].InnerHtml = (tPotong).ToString("N0");
                        tr2.Cells[5].InnerHtml = (tkirim).ToString("N0");
                        tr2.Cells[6].InnerHtml = (tSA + tPotong - tkirim).ToString("N0");
                        tr2.Cells[7].InnerHtml = ((tPotong + tSA) / Convert.ToDecimal(jml.Text) * 100).ToString("N0");
                        var a = tr.Cells[7].InnerHtml;
                        if (a == "100")
                        {
                            tr2.Cells[8].InnerHtml = "TERCAPAI";
                        }
                        else
                        {
                            tr2.Cells[8].InnerHtml = "TIDAK TERCAPAI";
                        }
                    }
                    pj.thnbln = thnbln;
                    pj.Partno = txtPartno.Text;
                    pj.target = int.Parse(jml.Text);
                    pj.sa = Convert.ToInt32(tSA);
                    pj.Pencapaian = Convert.ToInt32(tPotong);
                    pj.Customer = Convert.ToInt32(tkirim);
                    pj.SisaStock = Convert.ToInt32(tSA + tPotong - tkirim);
                    pj.Persen = decimal.Parse(tr.Cells[6].InnerHtml);
                    var bb = tr2.Cells[7].InnerHtml;
                    if (bb == "100")
                    {
                        pj.keterangan = "TERCAPAI";
                    }
                    else
                    {
                        pj.keterangan = "TIDAK TERCAPAI";
                    }
                    pj.RowStatus = 0;
                    pj.CreatedBy = ((Users)Session["Users"]).UserName;

                    if (tr2.Cells[8].InnerHtml == "TERCAPAI")
                    {
                        persena2 += 1;
                    }
                    if (pj.target != 0)
                    {
                        persenb2 += 1;
                    }
                    if (persenb2 != 0)
                    {
                        persen2 = (persena2 / persenb2) * 100;
                    }
                    if (persen2 == 100)
                    {
                        perss2 = 100;
                    }
                    else
                    {
                        perss2 = 20;
                    }
                    //persenb = (IList((Repeater)Container.Parent).DataSource).Count;

                    lblpencapaian2.Text = "Pencapaian KPI bulan  " + ddlBulan.SelectedItem + " " + ddlTahun.Text + ":";
                    lblpersen2.Text = persen2.ToString("N0");
                    lblnilai2.Text = "Nilai " + perss.ToString();
                }
                //arrData.Add(pj);
            }
            Session["Nilai"] = arrData;
            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");

            insertforpes(persennn.ToString("N0"));
        }
       
        public void insertforpes(string actual)
        {
            string thnbln = ddlTahun.Text + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " if (select  count(id) from tmppantaukhusus where thnbln = '" + thnbln + "')= 0 begin " +
                             " insert tmppantaukhusus(thnbln, actual)values('" + thnbln + "', '" + actual + "')end " +
                             " else begin update tmppantaukhusus set actual = '" + actual + "' where thnbln = '" + thnbln + "' end ";
            SqlDataReader sdr = zl.Retrieve();
        }
        protected int getDeptID(string deptName)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from spd_dept where dept like '%" + deptName + "%'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        private T3_PantauPUM GeneratObject(SqlDataReader sdr)
        {
            T3_PantauPUM cfn = new T3_PantauPUM();
            cfn.Urutan = int.Parse(sdr["Urutan"].ToString());
            cfn.Partno = sdr["Partno"].ToString();
            cfn.sa = int.Parse(sdr["sa"].ToString());
            cfn.target = int.Parse(sdr["target"].ToString());
            cfn.Pencapaian = int.Parse(sdr["Pencapaian"].ToString());
            cfn.Customer = int.Parse(sdr["Customer"].ToString());
            cfn.SisaStock = int.Parse(sdr["SisaStock"].ToString());
            cfn.Persen = decimal.Parse(sdr["Persen"].ToString());
            return cfn;
        }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            priviewrpt();
        }
        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            priviewrpt();
        }
    }
}

public class T3_PantauPUM : GRCBaseDomain
{
    public int Urutan { get; set; }
    public string Partno { get; set; }
    public int sa { get; set; }
    public int target { get; set; }
    public int Pencapaian { get; set; }
    public int Customer { get; set; }
    public int SisaStock { get; set; }
    public decimal Persen { get; set; }
    public string thnbln { get; set; }

    public string keterangan { get; set; }
}