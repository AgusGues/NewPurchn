using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class PemantauanPacking : System.Web.UI.Page
    {
        decimal tactuala1 = 0;
        decimal tactualb1 = 0;
        decimal tactualc1 = 0;
        decimal tactuald1 = 0;
        decimal tactuale1 = 0;
        decimal ttargeta1 = 0;
        decimal ttargetb1 = 0;
        decimal ttargetc1 = 0;
        decimal ttargetd1 = 0;
        decimal ttargete1 = 0;
        decimal tjama1 = 0;
        decimal tjamb1 = 0;
        decimal tjamc1 = 0;
        decimal tjamd1 = 0;
        decimal tjame1 = 0;
        decimal totaltarget1 = 0;
        decimal totalactual1 = 0;


        decimal rtactuala1 = 0;
        decimal rtactualb1 = 0;
        decimal rtactualc1 = 0;
        decimal rtactuald1 = 0;
        decimal rtactuale1 = 0;
        decimal rttargeta1 = 0;
        decimal rttargetb1 = 0;
        decimal rttargetc1 = 0;
        decimal rttargetd1 = 0;
        decimal rttargete1 = 0;
        decimal rtjama1 = 0;
        decimal rtjamb1 = 0;
        decimal rtjamc1 = 0;
        decimal rtjamd1 = 0;
        decimal rtjame1 = 0;
        decimal rtotaltarget1 = 0;
        decimal rtotalactual1 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            if (!Page.IsPostBack)
            {
                txttanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadBulan();
                LoadTahun();
                aprove.Visible = false;
                btnUnApprove.Visible = false;
                btnApprove.Visible = false;
                btnExport.Visible = false;
                cekuser();
                btnUnApprove.Enabled = false;
                btnApprove.Enabled = false;
                btnSimpan.Enabled = false;
            }
        }

        public void cekuser()
        {
            int userapv = 0;
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select count(id)hasil from PantauPackingApvUser where UserID=" + users.ID + " and rowstatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    userapv = Int32.Parse(sdr["hasil"].ToString());
                }
            }
            if (userapv > 0)
            {
                input.Visible = false;
                btnSimpan.Visible = false;
                aprove.Visible = true;
                btnApprove.Visible = true;
                btnUnApprove.Visible = true;
                btnExport.Visible = true;
            }
            //Tambahan agar header jumlah orng bisa dinamis tiap plant
            if (users.UnitKerjaID == 1)
            {
                targetjamctrpa.Visible = true;
                targetshiftctrpa.Visible = true;
                targetjamctrpb.Visible = true;
                targetshiftctrpb.Visible = true;
            }
            else if (users.UnitKerjaID == 7)
            {
                targetjamkrwga.Visible = true;
                targetshiftkrwga.Visible = true;
                targetjamkrwgb.Visible = true;
                targetshiftkrwgb.Visible = true;
            }
            else if (users.UnitKerjaID == 13)
            {
                targetjamjmbga.Visible = true;
                targetshiftjmbga.Visible = true;
                targetjamjmbgb.Visible = true;
                targetshiftjmbgb.Visible = true;
            }
        }

        public int GetAproval()
        {
            int levelapv = 0;

            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select levelapv from PantauPackingApvUser where UserID=" + users.ID + " and rowstatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    levelapv = Int32.Parse(sdr["levelapv"].ToString());
                }
            }
            return levelapv;
        }

        public int CekAproval()
        {
            int bulan = Convert.ToInt32(ddlBulan.SelectedValue);

            int statusapv = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select statusapv from PantauPackingAproval where bulan=" + bulan +
                             " and tahun= " + Convert.ToInt32(ddlTahun.SelectedValue) + " and rowstatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    statusapv = Int32.Parse(sdr["statusapv"].ToString());
                }
            }
            return statusapv;
        }
        public int CekAprovalsimpan()
        {
            string tanggal = (Convert.ToDateTime(txttanggal.Text)).ToString("yyyyMMdd");

            int statusapv = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select statusapv from PantauPackingAproval where bulan=(select month ('" + tanggal + "')bulan) " +
                             " and tahun= (select year ('" + tanggal + "')tahun) and rowstatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    statusapv = Int32.Parse(sdr["statusapv"].ToString());
                }
            }
            return statusapv;
        }

        public int CekUserApv()
        {
            int userapv = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select UserID from PantauPackingApvUser where and rowstatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    userapv = Int32.Parse(sdr["UserID"].ToString());
                }
            }
            return userapv;
        }

        public void LoadTahun()
        {
            ddlTahun.Items.Clear();
            var currentYear = DateTime.Today.Year;
            for (int i = 2; i >= 0; i--)
            {
                ddlTahun.Items.Add((currentYear - i).ToString());
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString().PadLeft(2, '0')));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }

        protected void lstpacking_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Users users = (Users)Session["Users"];
            Packing p = (Packing)e.Item.DataItem;
            Label lbl = (Label)e.Item.FindControl("lblid");

            if (p.PID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " insert pantaupacking(P_ID,Tanggal,targeta,actuala,targetb,actualb,targetc,actualc,targetd,actuald,targete,actuale,RowStatus,CreatedBy,CreatedTime) " +
                                " values(" + p.PMID + ", '" + Convert.ToDateTime(txttanggal.Text).ToString("yyyyMMdd") + "' ,'0','0','0','0','0','0','0','0','0','0','0','" + users.UserName + "',getdate())";

                SqlDataReader sdr = zl.Retrieve();
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = "select top 1 id from pantaupacking order by id desc";
                SqlDataReader sdr2 = zl1.Retrieve();
                if (sdr2.HasRows)
                {
                    while (sdr2.Read())
                    {
                        lbl.ToolTip = sdr2["id"].ToString();
                    }
                }
            }
            else
            {
                lbl.ToolTip = p.PID.ToString();
            }
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps1");
                    if (tr != null)
                    {
                        tactuala1 += p.ActualA;
                        tactualb1 += p.ActualB;
                        tactualc1 += p.ActualC;
                        tactuald1 += p.ActualD;
                        tactuale1 += p.ActualE;
                        ttargeta1 += p.TargetA;
                        ttargetb1 += p.TargetB;
                        ttargetc1 += p.TargetC;
                        ttargetd1 += p.TargetD;
                        ttargete1 += p.TargetE;
                        tjama1 += (p.TargetA / p.TargetJam1);
                        tjamb1 += (p.TargetB / p.TargetJam1);
                        tjamc1 += (p.TargetC / p.TargetJam1);
                        tjamd1 += (p.TargetD / p.TargetJam1);
                        tjame1 += (p.TargetE / p.TargetJam2);

                    }
                }
                if (tactuala1 < 1)
                {
                    lblprsenA.Text = "0%";
                }
                else if (tactuala1 > 0)
                {
                    lblprsenA.Text = ((tactuala1 / ttargeta1) * 100).ToString("N0") + "%";
                }
                if (tactualb1 < 1)
                {
                    lblprsenB.Text = "0%";
                }
                else if (tactualb1 > 0)
                {
                    lblprsenB.Text = ((tactualb1 / ttargetb1) * 100).ToString("N0") + "%";
                }
                if (tactualc1 < 1)
                {
                    lblprsenC.Text = "0%";
                }
                else if (tactualc1 > 0)
                {
                    lblprsenC.Text = ((tactualc1 / ttargetc1) * 100).ToString("N0") + "%";
                }
                if (tactuald1 < 1)
                {
                    lblprsenD.Text = "0%";
                }
                else if (tactuald1 > 0)
                {
                    lblprsenD.Text = ((tactuald1 / ttargetd1) * 100).ToString("N0") + "%";
                }
                if (tactuale1 < 1)
                {
                    lblprsenE.Text = "0%";
                }
                else if (tactuale1 > 0)
                {
                    lblprsenE.Text = ((tactuale1 / ttargete1) * 100).ToString("N0") + "%";
                }

                totalactual1 = tactuala1 + tactualb1 + tactualc1 + tactuald1 + tactuale1;
                totaltarget1 = ttargeta1 + ttargetb1 + ttargetc1 + ttargetd1 + ttargete1;

                if (totaltarget1 != 0)
                {
                    TotalAll.Text = ((totalactual1 / totaltarget1) * 100).ToString("N0") + "%";
                }
                else
                {
                    TotalAll.Text = "0";
                }

                lblTotalAcutalA.Text = tactuala1.ToString("N0");
                lblTotalAcutalB.Text = tactualb1.ToString("N0");
                lblTotalAcutalC.Text = tactualc1.ToString("N0");
                lblTotalAcutalD.Text = tactuald1.ToString("N0");
                lblTotalAcutalE.Text = tactuale1.ToString("N0");
                lblTotalTargetA.Text = ttargeta1.ToString("N0");
                lblTotalTargetB.Text = ttargetb1.ToString("N0");
                lblTotalTargetC.Text = ttargetc1.ToString("N0");
                lblTotalTargetD.Text = ttargetd1.ToString("N0");
                lblTotalTargetE.Text = ttargete1.ToString("N0");
                lblTotalJamA.Text = tjama1.ToString("N0");
                lblTotalJamB.Text = tjamb1.ToString("N0");
                lblTotalJamC.Text = tjamc1.ToString("N0");
                lblTotalJamD.Text = tjamd1.ToString("N0");
                lblTotalJamE.Text = tjame1.ToString("N0");
                TotalActual.Text = totalactual1.ToString("N0");
                TotalTarget.Text = totaltarget1.ToString("N0");

            }
            catch
            {

            }
        }

        public void LoadList()
        {
            ArrayList arrData = new ArrayList();

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select distinct Jenis_Packing from pantaupackingmaster where rowstatus>-1 ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new ListType
                    {
                        JenisPacking = sdr["Jenis_Packing"].ToString()
                    });
                }
            }
            lstType.DataSource = arrData;
            lstType.DataBind();
        }

        public void LoadapvList()
        {
            ArrayList arrData = new ArrayList();

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select distinct Jenis_Packing from pantaupackingmaster where rowstatus>-1 ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new ListType
                    {
                        JenisPacking = sdr["Jenis_Packing"].ToString()
                    });
                }
            }
            lstType2.DataSource = arrData;
            lstType2.DataBind();
        }

        public void LoadPacking(string JenisPacking, Repeater rpt)
        {
            int ada = 0;
            string queryadd;
            ArrayList arrData = new ArrayList();
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery = " select count (p.id)hasil from PantauPacking p, pantaupackingmaster pm  where p.Tanggal='" + Convert.ToDateTime(txttanggal.Text).ToString("yyyyMMdd") + "' " +
                              " and pm.Rowstatus>-1 and p.p_id=pm.ID and pm.jenis_packing='" + JenisPacking + "'";
            SqlDataReader sdr2 = zl1.Retrieve();
            if (sdr2.HasRows)
            {
                while (sdr2.Read())
                {
                    ada = Convert.ToInt32(sdr2["hasil"].ToString());
                }
            }
            if (ada == 0)
            {
                queryadd = " select pm.*, " +
                            " (select isnull((select id from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))packingid, " +
                            " (select isnull((select ActualA from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))actualA, " +
                            " (select isnull((select ActualB from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))actualB, " +
                            " (select isnull((select ActualC from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))actualC, " +
                            " (select isnull((select ActualD from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))actualD, " +
                            " (select isnull((select ActualE from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))actualE, " +
                            " (select isnull((select TargetA from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))targetA, " +
                            " (select isnull((select TargetB from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))targetB, " +
                            " (select isnull((select TargetC from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))targetC, " +
                            " (select isnull((select TargetD from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))targetD, " +
                            " (select isnull((select TargetE from PantauPacking where Tanggal=@tgl and P_ID=pm.id),0))targetE " +
                            " from pantaupackingmaster pm where pm.jenis_packing='" + JenisPacking + "'order by pm.id ";
            }
            else
            {
                queryadd = " select pm.*,(p.ID)packingid,p.ActualA,p.ActualB,p.ActualC,p.ActualD,p.ActualE,p.TargetA,p.TargetB,p.TargetC,p.TargetD,p.TargetE " +
                           " from pantaupackingmaster pm, PantauPacking p where pm.id=p.P_ID and pm.jenis_packing='" + JenisPacking + "' and pm.rowstatus>-1 " +
                           " and p.tanggal=@tgl order by pm.id ";
            }

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "select * from pantaupackingmaster pm left join pantaupacking p on pm.id=p.p_id where pm.jenis_packing='" + JenisPacking + "' and and p.rowstatus>0 and pm.rowstatus>-1 order by id";
            zl.CustomQuery = " declare  @tgl datetime; set @tgl='" + Convert.ToDateTime(txttanggal.Text).ToString("yyyyMMdd") + "' " + queryadd;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new Packing
                    {
                        PMID = Convert.ToInt32(sdr["ID"].ToString()),
                        JenisUkuran1 = sdr["J_Ukuran1"].ToString(),
                        JenisUkuranNo1 = sdr["J_UkuranNo1"].ToString(),
                        TargetJam1 = Convert.ToDecimal(sdr["T_Jam1"].ToString()),
                        TargetShift1 = Convert.ToDecimal(sdr["T_Shift1"].ToString()),
                        JenisUkuran2 = sdr["J_Ukuran2"].ToString(),
                        JenisUkuranNo2 = sdr["J_UkuranNo2"].ToString(),
                        TargetJam2 = Convert.ToDecimal(sdr["T_Jam2"].ToString()),
                        TargetShift2 = Convert.ToDecimal(sdr["T_Shift2"].ToString()),
                        PID = Convert.ToInt32(sdr["packingid"].ToString()),
                        ActualA = Convert.ToDecimal(sdr["ActualA"].ToString()),
                        ActualB = Convert.ToDecimal(sdr["ActualB"].ToString()),
                        ActualC = Convert.ToDecimal(sdr["ActualC"].ToString()),
                        ActualD = Convert.ToDecimal(sdr["ActualD"].ToString()),
                        ActualE = Convert.ToDecimal(sdr["ActualE"].ToString()),
                        TargetA = Convert.ToDecimal(sdr["TargetA"].ToString()),
                        TargetB = Convert.ToDecimal(sdr["TargetB"].ToString()),
                        TargetC = Convert.ToDecimal(sdr["TargetC"].ToString()),
                        TargetD = Convert.ToDecimal(sdr["TargetD"].ToString()),
                        TargetE = Convert.ToDecimal(sdr["TargetE"].ToString()),
                    });
                }
            }
            rpt.DataSource = arrData;
            rpt.DataBind();

        }

        public void LoadApvPacking(string JenisPacking, Repeater rpt2)
        {
            ArrayList arrData = new ArrayList();
            string tgl = ((ddlTahun.SelectedValue) + (ddlBulan.SelectedValue)).ToString();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " declare @tgl varchar(10) set @tgl='" + tgl + "' " +
                            " select id, Jenis_Packing,J_Ukuran1,J_UkuranNo1,T_Jam1,T_Shift1,J_Ukuran2,J_UkuranNo2,T_Jam2,T_Shift2, " +
                            " (select isnull(sum(actualA),0)ActualA from PantauPacking where P_ID=pm.id and  left(convert(char,Tanggal,112),6)=@tgl)ActualA, " +
                            " (select isnull(sum(ActualB),0)ActualB from PantauPacking where P_ID=pm.id and  left(convert(char,Tanggal,112),6)=@tgl)ActualB, " +
                            " (select isnull(sum(ActualC),0)ActualC from PantauPacking where P_ID=pm.id and  left(convert(char,Tanggal,112),6)=@tgl)ActualC, " +
                            " (select isnull(sum(ActualD),0)ActualD from PantauPacking where P_ID=pm.id and  left(convert(char,Tanggal,112),6)=@tgl)ActualD, " +
                            " (select isnull(sum(ActualE),0)ActualE from PantauPacking where P_ID=pm.id and  left(convert(char,Tanggal,112),6)=@tgl)ActualE, " +
                            " (select isnull(sum(TargetA),0)TargetA from pantaupacking where P_ID=pm.id and  left(convert(char,Tanggal,112),6)=@tgl)TargetA, " +
                            " (select isnull(sum(TargetB),0)TargetB from pantaupacking where P_ID=pm.id and  left(convert(char,Tanggal,112),6)=@tgl)TargetB, " +
                            " (select isnull(sum(TargetC),0)TargetC from pantaupacking where P_ID=pm.id and  left(convert(char,Tanggal,112),6)=@tgl)TargetC, " +
                            " (select isnull(sum(TargetD),0)TargetD from pantaupacking where P_ID=pm.id and  left(convert(char,Tanggal,112),6)=@tgl)TargetD, " +
                            " (select isnull(sum(TargetE),0)TargetE from pantaupacking where P_ID=pm.id and  left(convert(char,Tanggal,112),6)=@tgl)TargetE " +
                            " from pantaupackingmaster pm WHERE pm.jenis_packing='" + JenisPacking + "' and pm.rowstatus>-1  ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new Packing
                    {
                        //PMID            = Convert.ToInt32(sdr["ID"].ToString()),
                        JenisUkuran1 = sdr["J_Ukuran1"].ToString(),
                        JenisUkuranNo1 = sdr["J_UkuranNo1"].ToString(),
                        TargetJam1 = Convert.ToDecimal(sdr["T_Jam1"].ToString()),
                        TargetShift1 = Convert.ToDecimal(sdr["T_Shift1"].ToString()),
                        JenisUkuran2 = sdr["J_Ukuran2"].ToString(),
                        JenisUkuranNo2 = sdr["J_UkuranNo2"].ToString(),
                        TargetJam2 = Convert.ToDecimal(sdr["T_Jam2"].ToString()),
                        TargetShift2 = Convert.ToDecimal(sdr["T_Shift2"].ToString()),
                        //PID             = Convert.ToInt32(sdr["packingid"].ToString()),
                        ActualA = Convert.ToDecimal(sdr["ActualA"].ToString()),
                        ActualB = Convert.ToDecimal(sdr["ActualB"].ToString()),
                        ActualC = Convert.ToDecimal(sdr["ActualC"].ToString()),
                        ActualD = Convert.ToDecimal(sdr["ActualD"].ToString()),
                        ActualE = Convert.ToDecimal(sdr["ActualE"].ToString()),
                        TargetA = Convert.ToDecimal(sdr["TargetA"].ToString()),
                        TargetB = Convert.ToDecimal(sdr["TargetB"].ToString()),
                        TargetC = Convert.ToDecimal(sdr["TargetC"].ToString()),
                        TargetD = Convert.ToDecimal(sdr["TargetD"].ToString()),
                        TargetE = Convert.ToDecimal(sdr["TargetE"].ToString()),
                    });
                }
            }
            rpt2.DataSource = arrData;
            rpt2.DataBind();


        }

        protected void lstType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            ListType lt = (ListType)e.Item.DataItem;
            Repeater rpt = (Repeater)e.Item.FindControl("lstpacking");
            LoadPacking(lt.JenisPacking, rpt);
        }

        protected void txttanggal_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            Repeater lstpacking;
            Regex pattern = new Regex("[,]");
            Regex pattern1 = new Regex("[.]");
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstpacking = ((Repeater)(objItem0.FindControl("lstpacking")));
                int i = 0;
                foreach (RepeaterItem objItem in lstpacking.Items)
                {
                    Label lbl = (Label)lstpacking.Items[i].FindControl("lblid");
                    TextBox txtTargetA = (TextBox)lstpacking.Items[i].FindControl("txtTargetA");
                    TextBox txtActualA = (TextBox)lstpacking.Items[i].FindControl("txtActualA");
                    TextBox txtTargetB = (TextBox)lstpacking.Items[i].FindControl("txtTargetB");
                    TextBox txtActualB = (TextBox)lstpacking.Items[i].FindControl("txtActualB");
                    TextBox txtTargetC = (TextBox)lstpacking.Items[i].FindControl("txtTargetC");
                    TextBox txtActualC = (TextBox)lstpacking.Items[i].FindControl("txtActualC");
                    TextBox txtTargetD = (TextBox)lstpacking.Items[i].FindControl("txtTargetD");
                    TextBox txtActualD = (TextBox)lstpacking.Items[i].FindControl("txtActualD");
                    TextBox txtTargetE = (TextBox)lstpacking.Items[i].FindControl("txtTargetE");
                    TextBox txtActualE = (TextBox)lstpacking.Items[i].FindControl("txtActualE");
                    txtActualA.Text = pattern1.Replace(decimal.Parse(txtActualA.Text).ToString(), "");
                    txtActualB.Text = pattern1.Replace(decimal.Parse(txtActualB.Text).ToString(), "");
                    txtActualC.Text = pattern1.Replace(decimal.Parse(txtActualC.Text).ToString(), "");
                    txtActualD.Text = pattern1.Replace(decimal.Parse(txtActualD.Text).ToString(), "");
                    txtActualE.Text = pattern1.Replace(decimal.Parse(txtActualE.Text).ToString(), "");
                    txtTargetA.Text = pattern1.Replace(decimal.Parse(txtTargetA.Text).ToString(), "");
                    txtTargetB.Text = pattern1.Replace(decimal.Parse(txtTargetB.Text).ToString(), "");
                    txtTargetC.Text = pattern1.Replace(decimal.Parse(txtTargetC.Text).ToString(), "");
                    txtTargetD.Text = pattern1.Replace(decimal.Parse(txtTargetD.Text).ToString(), "");
                    txtTargetE.Text = pattern1.Replace(decimal.Parse(txtTargetE.Text).ToString(), "");

                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "update pantaupacking " +
                        " set TargetA =" + pattern.Replace(decimal.Parse(txtTargetA.Text).ToString(), ".") +
                        ", ActualA =" + pattern.Replace(decimal.Parse(txtActualA.Text).ToString(), ".") +
                        ", TargetB =" + pattern.Replace(decimal.Parse(txtTargetB.Text).ToString(), ".") +
                        ", ActualB =" + pattern.Replace(decimal.Parse(txtActualB.Text).ToString(), ".") +
                        ", TargetC =" + pattern.Replace(decimal.Parse(txtTargetC.Text).ToString(), ".") +
                        ", ActualC =" + pattern.Replace(decimal.Parse(txtActualC.Text).ToString(), ".") +
                        ", TargetD =" + pattern.Replace(decimal.Parse(txtTargetD.Text).ToString(), ".") +
                        ", ActualD =" + pattern.Replace(decimal.Parse(txtActualD.Text).ToString(), ".") +
                        ", TargetE =" + pattern.Replace(decimal.Parse(txtTargetE.Text).ToString(), ".") +
                        ", ActualE =" + pattern.Replace(decimal.Parse(txtActualE.Text).ToString(), ".") +
                        " where ID=" + lbl.ToolTip;
                    SqlDataReader sdr = zl.Retrieve();
                    i++;
                }
            }
            btnSimpan.Enabled = false;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string Periode = ddlBulan.SelectedItem.ToString() + " " + ddlTahun.SelectedItem.ToString();

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=Pemantauanpacking.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = " PEMANTAUAN PACKING";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            string HtmlEnd = "";

            div3.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();



        }

        protected void lstType2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListType lt2 = (ListType)e.Item.DataItem;
            Repeater rpt2 = (Repeater)e.Item.FindControl("lstpacking2");
            LoadApvPacking(lt2.JenisPacking, rpt2);
        }

        protected void lstpacking2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Packing p = (Packing)e.Item.DataItem;
            Label lbl2 = (Label)e.Item.FindControl("lblid2");
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps2");
                    if (tr != null)
                    {
                        rtactuala1 += p.ActualA;
                        rtactualb1 += p.ActualB;
                        rtactualc1 += p.ActualC;
                        rtactuald1 += p.ActualD;
                        rtactuale1 += p.ActualE;
                        rttargeta1 += p.TargetA;
                        rttargetb1 += p.TargetB;
                        rttargetc1 += p.TargetC;
                        rttargetd1 += p.TargetD;
                        rttargete1 += p.TargetE;
                        rtjama1 += (p.TargetA / p.TargetJam1);
                        rtjamb1 += (p.TargetB / p.TargetJam1);
                        rtjamc1 += (p.TargetC / p.TargetJam1);
                        rtjamd1 += (p.TargetD / p.TargetJam1);
                        rtjame1 += (p.TargetE / p.TargetJam2);
                    }
                }
                if (rtactuala1 < 1)
                {
                    lblprsenAr.Text = "0%";
                }
                else if (rtactuala1 > 0)
                {
                    lblprsenAr.Text = ((rtactuala1 / rttargeta1) * 100).ToString("N0") + "%";
                }
                if (rtactualb1 < 1)
                {
                    lblprsenBr.Text = "0%";
                }
                else if (rtactualb1 > 0)
                {
                    lblprsenBr.Text = ((rtactualb1 / rttargetb1) * 100).ToString("N0") + "%";
                }
                if (rtactualc1 < 1)
                {
                    lblprsenCr.Text = "0%";
                }
                else if (rtactualc1 > 0)
                {
                    lblprsenCr.Text = ((rtactualc1 / rttargetc1) * 100).ToString("N0") + "%";
                }
                if (rtactuald1 < 1)
                {
                    lblprsenDr.Text = "0%";
                }
                else if (rtactuald1 > 0)
                {
                    lblprsenDr.Text = ((rtactuald1 / rttargetd1) * 100).ToString("N0") + "%";
                }
                if (rtactuale1 < 1)
                {
                    lblprsenEr.Text = "0%";
                }
                else if (rtactuale1 > 0)
                {
                    lblprsenEr.Text = ((rtactuale1 / rttargete1) * 100).ToString("N0") + "%";
                }

                rtotalactual1 = rtactuala1 + rtactualb1 + rtactualc1 + rtactuald1 + rtactuale1;
                rtotaltarget1 = rttargeta1 + rttargetb1 + rttargetc1 + rttargetd1 + rttargete1;

                if (rtotaltarget1 != 0)
                {
                    TotalAll2.Text = ((rtotalactual1 / rtotaltarget1) * 100).ToString("N0") + "%";
                }
                else
                {
                    TotalAll2.Text = "0";
                }

                lblTotalAcutalAr.Text = rtactuala1.ToString("N0");
                lblTotalAcutalBr.Text = rtactualb1.ToString("N0");
                lblTotalAcutalCr.Text = rtactualc1.ToString("N0");
                lblTotalAcutalDr.Text = rtactuald1.ToString("N0");
                lblTotalAcutalEr.Text = rtactuale1.ToString("N0");
                lblTotalTargetAr.Text = rttargeta1.ToString("N0");
                lblTotalTargetBr.Text = rttargetb1.ToString("N0");
                lblTotalTargetCr.Text = rttargetc1.ToString("N0");
                lblTotalTargetDr.Text = rttargetd1.ToString("N0");
                lblTotalTargetEr.Text = rttargete1.ToString("N0");
                lblTotalJamAr.Text = rtjama1.ToString("N0");
                lblTotalJamBr.Text = rtjamb1.ToString("N0");
                lblTotalJamCr.Text = rtjamc1.ToString("N0");
                lblTotalJamDr.Text = rtjamd1.ToString("N0");
                lblTotalJamEr.Text = rtjame1.ToString("N0");

                TotalActual2.Text = rtotalactual1.ToString("N0");
                TotalTarget2.Text = rtotaltarget1.ToString("N0");

            }
            catch
            {

            }
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            int levelapv = this.GetAproval();
            int statusapv = this.CekAproval();

            Regex pattern = new Regex("[,]");
            string str = (TotalAll2.Text);
            string ext = str.Remove(str.LastIndexOf('%'));
            string query;

            if (levelapv == 1 && statusapv == 0)
            {
                query = " Insert into pantaupackingaproval(bulan,tahun,statusapv,tgl_apv1,rowstatus) " +
                          " values( " + Convert.ToInt32(ddlBulan.SelectedValue) + "," + Convert.ToInt32(ddlTahun.SelectedValue) + ",1," +
                          " getdate(),0)";
            }
            else if (levelapv == 2 && statusapv == 1)
            {
                query = " update pantaupackingaproval set statusapv=2,tgl_apv2=getdate() where bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " and  " +
                        "tahun= " + Convert.ToInt32(ddlTahun.SelectedValue) + " and rowstatus>-1";
            }
            else if (levelapv == 3 && statusapv == 2)
            {
                query = " update pantaupackingaproval set statusapv=3,tgl_apv3=getdate(),pencapaian=" + Convert.ToDecimal(ext) + " where " +
                        " bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " and  tahun= " + Convert.ToInt32(ddlTahun.SelectedValue) + " and rowstatus>-1";
            }
            else
            {
                query = " ";
            }

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = query;

            SqlDataReader sdr = zl.Retrieve();
            btnApprove.Enabled = false;
            btnUnApprove.Enabled = false;
        }

        protected void Preview_Click(object sender, EventArgs e)
        {
            btnSimpan.Enabled = false;
            LoadList();
            int statusapv = this.CekAprovalsimpan();
            if (statusapv != 3)
            {
                btnSimpan.Enabled = true;
            }
        }

        protected void Preview2_Click(object sender, EventArgs e)
        {
            int levelapv = this.GetAproval();
            int statusapv = this.CekAproval();
            if (levelapv - 1 == statusapv)
            {
                btnApprove.Enabled = true;
                btnUnApprove.Enabled = true;
            }
            LoadapvList();
        }
    }
}
public class Packing : GRCBaseDomain
{
    public int PMID { get; set; }
    public string JenisUkuran1 { get; set; }
    public string JenisUkuranNo1 { get; set; }
    public decimal TargetJam1 { get; set; }
    public decimal TargetShift1 { get; set; }
    public string JenisUkuran2 { get; set; }
    public string JenisUkuranNo2 { get; set; }
    public decimal TargetJam2 { get; set; }
    public decimal TargetShift2 { get; set; }
    public int PID { get; set; }
    public decimal TargetA { get; set; }
    public decimal ActualA { get; set; }
    public decimal TargetB { get; set; }
    public decimal ActualB { get; set; }
    public decimal TargetC { get; set; }
    public decimal ActualC { get; set; }
    public decimal TargetD { get; set; }
    public decimal ActualD { get; set; }
    public decimal TargetE { get; set; }
    public decimal ActualE { get; set; }
}

public class ListType : GRCBaseDomain
{
    public string JenisPacking { get; set; }
}