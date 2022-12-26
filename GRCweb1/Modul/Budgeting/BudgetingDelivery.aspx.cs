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

namespace GRCweb1.Modul.Budgeting
{
    public partial class BudgetingDelivery : System.Web.UI.Page
    {
        decimal TotalQty = 0; decimal TotalPakai = 0; decimal TotalPersen = 0; decimal TotalQty2 = 0; decimal TotalPakai2 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Session["ada"] = "-";

                RB2.Visible = false;
                string NamaBulanNow = DateTime.Now.Month.ToString();
                string NamaBulanTemp = string.Empty;
                if (NamaBulanNow == "1") { NamaBulanTemp = "JANUARI"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "2") { NamaBulanTemp = "FEBRUARI"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "3") { NamaBulanTemp = "MARET"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "4") { NamaBulanTemp = "APRIL"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "5") { NamaBulanTemp = "MEI"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "6") { NamaBulanTemp = "JUNI"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "7") { NamaBulanTemp = "JULI"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "8") { NamaBulanTemp = "AGUSTUS"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "9") { NamaBulanTemp = "SEPTEMBER"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "10") { NamaBulanTemp = "OKTOBER"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "11") { NamaBulanTemp = "NOVEMBER"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "12") { NamaBulanTemp = "DESEMBER"; Session["NamaBulanTemp"] = NamaBulanTemp; }

                LoadBudgetPerBulan();
                RB1.Visible = false; PanelPBulan.Visible = true;
                LabelHeader.Visible = true; LabelHeader.Text = "PERIODE BUDGET :" + " " + Session["NamaBulanTemp"].ToString();

                Users user = (Users)Session["Users"];
                if (user.UnitKerjaID == 1)
                { btnSave.Enabled = true; }
                else
                { btnSave.Enabled = false; }

                int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahunBulan.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahunBulan.SelectedValue.ToString();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport2);

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 400, 100 , 30 ,false); </script>", false);
            }
            catch { }
        }

        protected void btnPreview2_Click(object sender, EventArgs e)
        {
            LoadBudgetTahunan();
            PanelRBulanan.Visible = false;
            PanelRTahunan.Visible = true;
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            //Bulanan
            if (RB1.Checked)
            {
                LoadBudget();
                PanelRBulanan.Visible = true;
                PanelRTahunan.Visible = false;
            }
            //Tahunan di non aktifkan
            else if (RB2.Checked)
            {
                LoadBudgetTahunan();
                PanelRBulanan.Visible = false;
                PanelRTahunan.Visible = true;
            }

            LoadBudget();
            //PanelRBulanan.Visible = false; PanelBulanan_Baru.Visible = true;
            PanelRTahunan.Visible = false;
            LabelHeader.Visible = true; LabelHeader.Text = "PERIODE BUDGET :" + " " + ddlBulan.SelectedItem.ToString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            FacadeBudgetDelivery FacadeTotalC = new FacadeBudgetDelivery();
            string bln01 = string.Empty;
            string bln = ddlBulan.SelectedValue.ToString();
            if (bln.Length == 1)
            {
                bln01 = "0" + bln; ;
            }
            else
            {
                bln01 = bln; ;
            }
            BudgetDelivery DomainTotalC = new BudgetDelivery();
            //string PeriodeCC = ddlTahunBulan.SelectedItem.ToString() + ddlBulan.SelectedValue.ToString().Substring(0, 2);
            string PeriodeCC = ddlTahunBulan.SelectedItem.ToString() + bln01;

            int intResult = 0;
            DomainTotalC.PeriodeCostControl = PeriodeCC;
            intResult = FacadeTotalC.Cancel(DomainTotalC);
            if (intResult > 0)
            {
                btnSave.Enabled = true; btnCancel.Enabled = false; LabelResult.Visible = true;
                LabelResult.Attributes["style"] = "color:blue; font-weight:bold; font-size:16; font-size:Courier New";
                LabelResult.Text = "Cancel OK !!";

                LabelStatus.Text = "Status : Open";
            }
            else
            {
                btnSave.Enabled = false; btnCancel.Enabled = true; LabelResult.Visible = true;
                LabelResult.Text = "Upss Error !!";
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            FacadeBudgetDelivery FacadeTotalS = new FacadeBudgetDelivery();
            BudgetDelivery DomainTotalS = new BudgetDelivery();
            //string PeriodeCC = ddlTahunBulan.SelectedItem.ToString() + ddlBulan.SelectedValue.ToString().Substring(0,2);

            string bulan = ddlBulan.SelectedValue.ToString(); string bln = string.Empty;
            if (bulan.ToString().Length == 1)
            {
                bln = "0" + bulan;
            }
            else
            {
                bln = bulan;
            }
            string Data = string.Empty;


            string PeriodeCC = ddlTahunBulan.SelectedItem.ToString() + bln;
            int intResult = 0;
            if (Convert.ToInt32(PeriodeCC) < 202006)
            {
                for (int i = 0; i < DataBudget.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)DataBudget.Items[i].FindControl("ps1");
                    if (tr.Cells[1].InnerHtml != "")
                    {
                        DomainTotalS.PeriodeCostControl = PeriodeCC;
                        DomainTotalS.Urutan = Convert.ToInt32(tr.Cells[0].InnerHtml);
                        DomainTotalS.NamaPlant = Convert.ToString(tr.Cells[1].InnerHtml).Trim();
                        DomainTotalS.JenisKendaraan = Convert.ToString(tr.Cells[2].InnerHtml).Trim().Remove(0, 10).Trim();
                        DomainTotalS.NoPol = Convert.ToString(tr.Cells[3].InnerHtml).Trim().Remove(0, 10).Trim();
                        DomainTotalS.MaxBudget = Convert.ToDecimal(tr.Cells[4].InnerHtml);
                        DomainTotalS.Actual = Convert.ToDecimal(tr.Cells[5].InnerHtml);
                        DomainTotalS.Persen = Math.Round(Convert.ToDecimal(tr.Cells[7].InnerHtml) / Convert.ToDecimal(tr.Cells[6].InnerHtml), 3) * 100;
                        DomainTotalS.CreatedBy = user.UserName;
                        DomainTotalS.Km = 0;
                        DomainTotalS.TtlBudget = 0;

                        intResult = FacadeTotalS.Simpan(DomainTotalS);

                    }
                }
            }
            else
            {
                for (int i = 0; i < DataBudgetNew.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)DataBudgetNew.Items[i].FindControl("ps1");
                    if (tr.Cells[1].InnerHtml != "")
                    {
                        DomainTotalS.PeriodeCostControl = PeriodeCC;
                        DomainTotalS.Urutan = Convert.ToInt32(tr.Cells[0].InnerHtml);
                        DomainTotalS.NamaPlant = Convert.ToString(tr.Cells[1].InnerHtml).Trim();
                        DomainTotalS.JenisKendaraan = Convert.ToString(tr.Cells[2].InnerHtml).Trim().Remove(0, 10).Trim();
                        DomainTotalS.NoPol = Convert.ToString(tr.Cells[3].InnerHtml).Trim().Remove(0, 10).Trim();
                        DomainTotalS.MaxBudget = Convert.ToDecimal(tr.Cells[4].InnerHtml);
                        DomainTotalS.Actual = Convert.ToDecimal(tr.Cells[7].InnerHtml);

                        if (tr.Cells[6].InnerHtml.Trim() == "0,00")
                        {
                            DomainTotalS.Persen = 0;
                        }
                        else
                        {
                            DomainTotalS.Persen = Math.Round(Convert.ToDecimal(tr.Cells[7].InnerHtml) / Convert.ToDecimal(tr.Cells[6].InnerHtml), 3) * 100;
                        }
                        DomainTotalS.CreatedBy = user.UserName;
                        string KM = tr.Cells[5].InnerHtml.ToString().Replace(".", "");
                        DomainTotalS.Km = Convert.ToInt32(KM);
                        DomainTotalS.TtlBudget = Convert.ToDecimal(tr.Cells[6].InnerHtml);

                        intResult = FacadeTotalS.Simpan(DomainTotalS);

                    }
                }
            }
            //ZetroView zl = new ZetroView();
            //zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "exec [sqlkrwg.grcboard.com].bpaskrwg.dbo.CostControlDelivery_Simpan_from_ctrp '" + PeriodeCC + "'" +
            //    "exec [sqlJombang.grcboard.com].bpasjombang.dbo.CostControlDelivery_Simpan_from_ctrp '" + PeriodeCC + "' ";
            //SqlDataReader sdr = zl.Retrieve();
            if (user.UnitKerjaID == 1)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery =
                    "exec [sqlkrwg.grcboard.com].bpaskrwg.dbo.CostControlDelivery_Simpan_from_ctrp '" + PeriodeCC + "' " +
                    "exec [sqlJombang.grcboard.com].bpasjombang.dbo.CostControlDelivery_Simpan_from_ctrp '" + PeriodeCC + "'";
                SqlDataReader sdr = zl.Retrieve();
            }

            if (intResult > 0)
            {
                btnSave.Enabled = false; btnCancel.Enabled = true; LabelResult.Visible = true;
                LabelResult.Attributes["style"] = "color:Blue; font-size:16; font-weight:bold;";
                LabelResult.Text = "Save OK !!!";

                LabelStatus.Attributes["style"] = "color:green; font-weight:bold; font-size:32; font-size:Courier New";
                LabelStatus.Text = "Status : Release";
            }
            else
            {
                LabelResult.Visible = true; LabelResult.Text = "Upss Error !!";
                LabelStatus.Text = "Status : Open";
            }

        }

        protected void DataBudget_DataBound(object sender, RepeaterItemEventArgs e)
        { }

        protected void DataBudget2_DataBound(object sender, RepeaterItemEventArgs e)
        { }

        protected void DataBudgetNew_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string Flag = Session["ada"].ToString();

            BudgetDelivery p = (BudgetDelivery)e.Item.DataItem;
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps1");
                    if (tr != null)
                    {
                        if (Flag == "0" || Flag == "-")
                        {
                            TotalQty += p.TtlBudget; TotalPakai += p.Actual;
                        }
                        else if (Flag == "1")
                        {
                            tr.Cells[6].InnerText = p.TtlBudget.ToString("N1");
                            tr.Cells[7].InnerText = p.Actual.ToString("N0");

                            TotalQty += p.TtlBudget; TotalPakai += p.Actual;
                        }

                        /** new **/
                        //if (tr.Cells[1].InnerText.Trim() == "KARAWANG")
                        //{
                        //    TotalQty2 += p.TtlBudget;
                        //    TotalPakai2 += p.Actual;
                        //}
                        /** end new **/

                        ZetroView zSave = new ZetroView();
                        zSave.QueryType = Operation.CUSTOM;

                        zSave.CustomQuery =
                        " insert into MaterialBudgetArmada_Nilai_Temp (Periode,Budget,Actual,NamaPlant) " +
                        " values " +
                        " ('" + Session["PeriodePES"].ToString() + "','" + p.TtlBudget.ToString("N2").Replace(".", "").Replace(",", ".") + "','" + p.Actual.ToString("N2").Replace(".", "").Replace(",", ".") + "','" + tr.Cells[1].InnerText.Trim() + "')";

                        SqlDataReader sdSave = zSave.Retrieve();
                    }
                }
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow tr2 = (HtmlTableRow)e.Item.FindControl("ftr");
                    tr2.Cells[1].InnerText = TotalQty.ToString("N0");
                    tr2.Cells[2].InnerText = TotalPakai.ToString("N0");
                    tr2.Cells[3].InnerText = ((TotalPakai / TotalQty) * 100).ToString("N0") + " %";
                    if (Session["totalatas"] == null)
                        Session["totalatas"] = TotalQty;
                }
            }
            catch { }


        }

        private void LoadBudget()
        {
            Session["totalatas"] = null;
            ZetroView zSave = new ZetroView();
            zSave.QueryType = Operation.CUSTOM;
            zSave.CustomQuery =
            " truncate table MaterialBudgetArmada_Nilai_Temp ";
            SqlDataReader sdSave = zSave.Retrieve();

            LabelResult.Visible = false;
            Users users = (Users)Session["Users"];
            FacadeBudgetDelivery rps = new FacadeBudgetDelivery();
            ArrayList arrBudget = new ArrayList();
            if (ddlBulan.SelectedValue == "" && ddlTahunBulan.SelectedValue == "")
            {
                int BulanTemp1 = DateTime.Now.Month; Session["BulanTemp"] = BulanTemp1;
                string Tahun1 = DateTime.Now.Year.ToString(); Session["TahunTemp"] = Tahun1; PanelRBulanan.Visible = true;
            }
            else
            {
                int BulanTemp1 = Convert.ToInt32(ddlBulan.SelectedValue); Session["BulanTemp"] = BulanTemp1;
                string Tahun1 = ddlTahunBulan.SelectedValue; Session["TahunTemp"] = Tahun1;
            }

            int BulanTemp = Convert.ToInt32(Session["BulanTemp"]);
            string Tahun = Session["TahunTemp"].ToString();

            if (BulanTemp < 10)
            {
                string Bulan = "0" + BulanTemp.ToString(); Session["Bulan"] = Bulan;
            }
            else
            {
                string Bulan = BulanTemp.ToString(); Session["Bulan"] = Bulan;
            }
            string Periode = Tahun + Session["Bulan"]; Session["PeriodePES"] = Periode;
            FacadeBudgetDelivery rps_1 = new FacadeBudgetDelivery();
            int CekData = rps_1.RetrieveDataBudgetDelivery(Periode);


            if (users.UnitKerjaID == 1 && users.Flag != 1)
            {
                string Query1 = "";
                string Query2 = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                string Query3 = "[sql1.grcboard.com].bpasHO.dbo.";
                string Query4 = "[sqljombang.grcboard.com].bpasjombang.dbo.";
                string Query5 = "[sqlkrwg.grcboard.com].bpascrb.dbo.";
                string Query6 = "[sqlkrwg.grcboard.com].bpascrb.dbo.";

                Session["Query1"] = Query1;
                Session["Query2"] = Query2;
                Session["Query3"] = Query3;
                Session["Query4"] = Query4;
                Session["Query5"] = Query5;
                Session["Query6"] = Query6;
            }
            else if (users.UnitKerjaID == 7 && users.Flag != 1)
            {
                string Query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                string Query2 = "";
                string Query3 = "[sql1.grcboard.com].bpasHO.dbo.";
                string Query4 = "[sqljombang.grcboard.com].bpasjombang.dbo.";
                string Query5 = "";
                string Query6 = "Bpascrb.dbo.";

                Session["Query1"] = Query1;
                Session["Query2"] = Query2;
                Session["Query3"] = Query3;
                Session["Query4"] = Query4;
                Session["Query5"] = Query5;
                Session["Query6"] = Query6;
            }
            else if (users.UnitKerjaID == 13 && users.Flag != 1)
            {
                string Query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                string Query2 = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                string Query3 = "[sql1.grcboard.com].bpasHO.dbo.";
                string Query4 = " ";
                string Query5 = "[sqlkrwg.grcboard.com].bpascrb.dbo.";
                string Query6 = "[sqlkrwg.grcboard.com].bpascrb.dbo.";

                Session["Query1"] = Query1;
                Session["Query2"] = Query2;
                Session["Query3"] = Query3;
                Session["Query4"] = Query4;
                Session["Query5"] = Query5;
                Session["Query6"] = Query6;
            }

            else if (users.Flag == 1)
            {
                string Query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                string Query2 = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                string Query3 = "";
                string Query4 = "[sqljombang.grcboard.com].bpasjombang.dbo.";
                string Query5 = "[sqlkrwg.grcboard.com].bpascrb.dbo.";

                Session["Query1"] = Query1;
                Session["Query2"] = Query2;
                Session["Query3"] = Query3;
                Session["Query4"] = Query4;
                Session["Query5"] = Query5;
            }

            string Query01 = Session["Query1"].ToString();
            string Query02 = Session["Query2"].ToString();
            string Query03 = Session["Query3"].ToString();
            string Query04 = Session["Query4"].ToString();
            string Query05 = Session["Query5"].ToString();
            string Query06 = Session["Query6"].ToString();

            if (CekData == 0)
            {
                Session["ada"] = 0;
                if (Convert.ToInt32(Periode) >= 202006)
                {
                    PanelBulanan_Baru.Visible = true; PanelRBulanan.Visible = false;
                    arrBudget = rps.RetrieveBudgetDeliveryNew(Periode, Query01, Query02, Query03, Query04, Query05, Query06);

                    DataBudgetNew.DataSource = arrBudget;
                    DataBudgetNew.DataBind();
                }
                else
                {
                    PanelBulanan_Baru.Visible = false; PanelRBulanan.Visible = true;
                    arrBudget = rps.RetrieveBudgetDelivery(Periode, Query01, Query02, Query03, Query04);

                    DataBudget.DataSource = arrBudget;
                    DataBudget.DataBind();
                }

                btnSave.Enabled = true; btnCancel.Enabled = false;
                LabelStatus.Visible = true;
                LabelStatus.Attributes["style"] = "color:green; font-weight:bold; font-size:32; font-size:Calibri";
                LabelStatus.Text = "Status : Open";
            }
            else
            {
                Session["ada"] = 1;

                if (Convert.ToInt32(Periode) >= 202006)
                {
                    PanelBulanan_Baru.Visible = true; PanelRBulanan.Visible = false;
                    arrBudget = rps.RetrieveBudgetDeliveryAdaNew(Periode);
                    DataBudgetNew.DataSource = arrBudget;
                    DataBudgetNew.DataBind();
                }
                else
                {
                    PanelBulanan_Baru.Visible = false; PanelRBulanan.Visible = true;
                    arrBudget = rps.RetrieveBudgetDeliveryAda(Periode);
                    DataBudget.DataSource = arrBudget;
                    DataBudget.DataBind();
                }

                btnSave.Enabled = false; btnCancel.Enabled = true;
                LabelStatus.Visible = true;
                LabelStatus.Attributes["style"] = "color:green; font-weight:bold; font-size:32; font-size:Calibri";
                LabelStatus.Text = "Status : Release";
            }

            if (arrBudget.Count > 0)
            {
                if (CekData == 0)
                {
                    FacadeBudgetDelivery FacadeTotal = new FacadeBudgetDelivery();
                    BudgetDelivery DomainTotal = new BudgetDelivery();
                    DomainTotal = FacadeTotal.RetrieveTotalBudget(Periode, Query01, Query02, Query03, Query04, Query05);
                    Session["DomainTotal"] = DomainTotal.TotalPersen;
                    Session["TotalActual"] = DomainTotal.TotalActual;

                    PanelKeterangan.Visible = true; PanelRBulanan.Visible = false; PanelBulanan_Baru.Visible = true;
                    LabelTotalStd.Visible = true; txtTotalStd.Visible = true;
                    LabelTotalStd.Text = "Total Budget Standar "; txtTotalStd.Text = DomainTotal.TotalStd.ToString("N0");

                    LabelTotalPakai.Visible = true; txtTotalPakai.Visible = true;
                    LabelTotalPakai.Text = "Total Actual Pemakaian "; txtTotalPakai.Text = DomainTotal.TotalActual.ToString("N0");

                    LabelTotalPersen.Visible = true; txtTotalPersen.Visible = true;
                    LabelTotalPersen.Text = "Total Persentase "; txtTotalPersen.Text = DomainTotal.TotalPersen.ToString("N0") + " %";
                }
                else
                {
                    FacadeBudgetDelivery FacadeTotalS = new FacadeBudgetDelivery();
                    BudgetDelivery DomainTotalS = new BudgetDelivery();
                    string Period = string.Empty;
                    if (Convert.ToInt32(Periode) >= 202006)
                    {
                        Period = "2";
                    }
                    else
                    {
                        Period = "1";
                    }


                    DomainTotalS = FacadeTotalS.RetrieveTotalBudgetS(Periode, Period);


                    Session["DomainTotal"] = DomainTotalS.TotalPersen;
                    Session["TotalStandar"] = DomainTotalS.TotalStd;
                    Session["TotalActual"] = DomainTotalS.TotalActual;

                    PanelKeterangan.Visible = true;
                    LabelTotalStd.Visible = true; txtTotalStd.Visible = true;
                    LabelTotalStd.Text = "Total Budget Standar ";
                    //txtTotalStd.Text = DomainTotalS.TotalStd.ToString("N0");

                    LabelTotalPakai.Visible = true; txtTotalPakai.Visible = true;
                    LabelTotalPakai.Text = "Total Actual Pemakaian "; txtTotalPakai.Text = DomainTotalS.TotalActual.ToString("N0");

                    LabelTotalPersen.Visible = true; txtTotalPersen.Visible = true;
                    LabelTotalPersen.Text = "Total Persentase "; txtTotalPersen.Text = DomainTotalS.TotalPersen.ToString("N0") + " %";
                }

                decimal TotalPersen = Convert.ToDecimal(Session["DomainTotal"]);
                decimal TotalStandar = Convert.ToDecimal(Session["TotalStandar"]);
                decimal TotalActual = Convert.ToDecimal(Session["TotalActual"]);

                #region update sarmut Armada
                string sarmutPrs = "Effesiensi Budget Transportasi";
                string strJmlLine = string.Empty;
                //string strDept = "TRANS";
                int deptid = getDeptID("TRANS");
                //int actual = Convert.ToInt32(DomainTotal.TotalPersen);
                int actual = Convert.ToInt32(TotalPersen);

                #endregion
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahunBulan.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
                    "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                    "update SPD_TransPrs set actual=" + actual + " where Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
                    "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')";
                SqlDataReader sdr1 = zl1.Retrieve();

                #region Yoga SarmutToPES
                string actualTotal = (Convert.ToDecimal(Session["TotalActual"].ToString())).ToString("N0");
                ArrayList arrData = new ArrayList();
                ZetroView zs = new ZetroView();
                zs.QueryType = Operation.CUSTOM;
                decimal targetSarmutArmada = 0;
                int bulan = Convert.ToInt32(ddlBulan.SelectedValue) - 1;
                zs.CustomQuery = "SELECT top 1 * FROM SPD_TransPrs WHERE SarmutPID IN (SELECT ID from SPD_Perusahaan where Rowstatus>-1 and " +
                                 "SarMutPerusahaan='" + sarmutPrs + "') and Approval>1 order by id desc ";
                SqlDataReader adr = zs.Retrieve();
                if (adr.HasRows)
                {
                    while (adr.Read())
                    {
                        targetSarmutArmada = (adr["Target"] == DBNull.Value) ? 0 : Convert.ToDecimal(adr["Target"]);
                    }
                }
                if (actual > 0)
                {
                    string strError = "";
                    //decimal newActual = (TotalActual / TotalStandar) * 100;
                    string ketPes = (actual > targetSarmutArmada) ? "Tidak Tercapai (> 100%)" : "Tercapai(100% Maks)";
                    string valueActual = ((actual / targetSarmutArmada) * 100).ToString();
                    double percentActual = Convert.ToDouble(TotalPersen);
                    ZetroView zv = new ZetroView();
                    zv.QueryType = Operation.CUSTOM;
                    int IdKPI = 0;
                    string kpiName = string.Empty;
                    zv.CustomQuery = "SELECT * FROM ISO_KPI WHERE DeptID=26 and month(TglMulai)=" + ddlBulan.SelectedValue + " AND year(TglMulai)=" + ddlTahunBulan.SelectedValue + " ORDER BY ID desc";
                    SqlDataReader dr = zv.Retrieve();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            IdKPI = Int32.Parse(dr["ID"].ToString());
                            kpiName = dr["KPIName"].ToString();
                        }
                    }
                    if (kpiName == string.Empty)
                    {
                        int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahunBulan.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                        ArrayList arrSarPes = new ArrayList();
                        SarmutPESFacade sarPesFacade = new SarmutPESFacade();
                        arrSarPes = sarPesFacade.RetrieveUserCategory(sarmutPrs);
                        foreach (SarmutPes uc in arrSarPes)
                        {
                            int idUserCategory = uc.IDUserCategory;
                            int userID = uc.UserID;
                            int bagianID = uc.BagianID;
                            decimal bobotNilai = uc.BobotNilai;
                            string pic = uc.Pic;
                            int deptID = uc.DeptID;
                            string description = uc.Description;
                            int pesType = uc.PesType;
                            int categoryID = uc.CategoryID;
                            //uc.Actual = string.Concat(actual.ToString(), valueActual.ToString()) ;
                            uc.Ket = string.Concat(TotalActual.ToString(), " (" + Math.Round(percentActual) + "%)");
                            //uc.Percent = valueActual.ToString();
                            //uc.TglMulai = Convert.ToDateTime(txtTglMulai.Text);
                            int pjgDept = ((Users)Session["Users"]).DeptID;
                            string ddlDept = "ARMADA";
                            uc.DeptName = (pjgDept >= 4) ? ddlDept.Substring(0, 3) : ddlDept.Substring(0, pjgDept);
                            txtTglMulai.Text = Convert.ToDateTime(uc.TglMulai).ToString();
                            uc.TglMulai = Convert.ToDateTime(LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahunBulan.SelectedValue.ToString());

                            ZetroView zx = new ZetroView();
                            zx.QueryType = Operation.CUSTOM;
                            int idSopScore = 0;
                            string targetKe = string.Empty;
                            decimal pointNilai = 0;
                            string ketActual = string.Empty;
                            zx.CustomQuery = "select ID,CategoryID,TargetKe,PointNilai from ISO_SOPScore where CategoryID=" + categoryID + " " +
                                             "and RowStatus>-1 and TargetKe='" + ketPes + "' ";
                            SqlDataReader xdr = zx.Retrieve();
                            if (xdr.HasRows)
                            {
                                while (xdr.Read())
                                {
                                    idSopScore = Int32.Parse(xdr["ID"].ToString());
                                    targetKe = xdr["TargetKe"].ToString();
                                    pointNilai = Convert.ToDecimal(xdr["PointNilai"].ToString());
                                    if (idSopScore > 0)
                                    {
                                        ketActual = string.Concat(actualTotal, " (" + Math.Round(percentActual) + "%)");
                                    }
                                }
                            }
                            uc.SopScoreID = idSopScore;
                            uc.KetTargetKe = targetKe;
                            uc.PointNilai = pointNilai;
                            uc.Actual = ketActual;

                            arrData.Add(uc);
                            strError = SimpanKPI(uc);
                        }
                    }
                    else
                    {
                        ArrayList arrSarPesUPdate = new ArrayList();
                        ArrayList arrSarPesUPdate2 = new ArrayList();
                        SarmutPESFacade updatesarPesFacade = new SarmutPESFacade();
                        arrSarPesUPdate = updatesarPesFacade.RetrieveUserCategory2(sarmutPrs);
                        foreach (SarmutPes up in arrSarPesUPdate)
                        {
                            int categoryID = up.CategoryID;
                            int deptID = up.DeptID;

                            arrSarPesUPdate2 = updatesarPesFacade.RetrieveID(deptID, ddlBulan.SelectedValue, ddlTahunBulan.SelectedValue, categoryID);
                            foreach (SarmutPes tp in arrSarPesUPdate2)
                            {
                                //SarmutPes updateSarPes = updatesarPesFacade.RetrieveID(deptID, ddlBulan.SelectedValue, ddlTahun.SelectedValue, categoryID);
                                int id = tp.ID;

                                SarmutPes updateSarPesScore = updatesarPesFacade.RetrieveUpdateScore(categoryID, ketPes);
                                int IDScore = updateSarPesScore.IDScore;
                                int CategoryID = updateSarPesScore.CategoryID;
                                string TargetKe = updateSarPesScore.KetTargetKe;
                                decimal PointNilai = updateSarPesScore.PointNilai;

                                up.KPIID = id;
                                up.Ket = string.Concat(actualTotal, " (" + Math.Round(percentActual) + "%)");
                                up.SopScoreID = IDScore;
                                up.KetTargetKe = TargetKe;
                                up.PointNilai = PointNilai;
                                arrData.Add(up);
                                strError = UpdateKPI(up);
                            }
                        }
                    }
                }
                #endregion

                #region Update ke Bagan Besar
                /** Start 18 Februari 2021, Created by: Beny 
                 *  Update Nilai ke DataBase sebelum Update ke Buku Besar ISO, 
                 *  Update ke Buku Besar ketika Apv Manager di Apv SarMut **/

                decimal TotalBudgetPerBulan = 0;
                decimal ActualSPBPerBulan = 0;
                string NilaiBulan = string.Empty;
                string NamaBulan = string.Empty;
                string Value = string.Empty;

                /** Jika Dept. Transportation dan Plant Citeureup , maka update ke bagan besar **/
                if (users.DeptID == 26 && users.UnitKerjaID == 1)
                {
                    NilaiBulan = ddlBulan.SelectedValue;
                    Tahun = ddlTahunBulan.SelectedValue;
                    if (NilaiBulan == "1") { NamaBulan = "Jan"; }
                    else if (NilaiBulan == "2") { NamaBulan = "Feb"; }
                    else if (NilaiBulan == "3") { NamaBulan = "Mar"; }
                    else if (NilaiBulan == "4") { NamaBulan = "Apr"; }
                    else if (NilaiBulan == "5") { NamaBulan = "Mei"; }
                    else if (NilaiBulan == "6") { NamaBulan = "Jun"; }
                    else if (NilaiBulan == "7") { NamaBulan = "Jul"; }
                    else if (NilaiBulan == "8") { NamaBulan = "Agu"; }
                    else if (NilaiBulan == "9") { NamaBulan = "Sep"; }
                    else if (NilaiBulan == "10") { NamaBulan = "Okt"; }
                    else if (NilaiBulan == "11") { NamaBulan = "Nov"; }
                    else if (NilaiBulan == "12") { NamaBulan = "Des"; }

                    TotalBudgetPerBulan = Convert.ToDecimal(TotalStandar.ToString("N0"));
                    ActualSPBPerBulan = Convert.ToDecimal(TotalActual.ToString("N0"));

                    FacadeBudgetTrans f_del = new FacadeBudgetTrans();
                    int ttl = f_del.RetrieveApvSarmut(NilaiBulan, Tahun);

                    BudgetTrans fin0 = new BudgetTrans();
                    FacadeBudgetTrans fin1 = new FacadeBudgetTrans();
                    int Nilai1 = fin1.RetrieveTableBB(NilaiBulan, Tahun);
                    //if (Nilai1 == 0)
                    //{
                    ArrayList arrBB = new ArrayList();
                    FacadeBudgetTrans F = new FacadeBudgetTrans();
                    arrBB = F.RetrieveMapping("Effesiensi Budget Transportasi");
                    if (arrBB.Count > 0)
                    {
                        int i = 0;
                        foreach (BudgetTrans List in arrBB)
                        {
                            if (List.Flag == "TotalBudget")
                            {
                                Value = TotalBudgetPerBulan.ToString();
                            }
                            else if (List.Flag == "TotalPakai")
                            {
                                Value = ActualSPBPerBulan.ToString();
                            }
                            else if (List.Flag == "Persen")
                            {
                                string A = TotalPersen.ToString();
                                Value = A.Replace(",", ".");
                            }

                            ZetroView z0 = new ZetroView();
                            z0.QueryType = Operation.CUSTOM;
                            if (Nilai1 == 0)
                            {
                                z0.CustomQuery =
                                "insert into BukuBesar_Mapping (Bulan,Tahun,Nilai,ParameterTerukur,InitialBulan,CreatedBy,CreatedTime,RowStatus,ItemSarmut) " +
                                "values " +
                                "('" + ddlBulan.SelectedValue + "','" + ddlTahunBulan.SelectedValue + "','" + Value + "','" + List.ParameterTerukur + "','" + NamaBulan +
                                     "','" + users.UserName + "'," + "GetDate()" + "," + "0" + ",'" + List.ItemSarmut + "')";
                            }
                            else if (Nilai1 > 0 && ttl < 2)
                            {
                                z0.CustomQuery =
                                "update BukuBesar_Mapping set Nilai='" + Value + "',CreatedBy='" + users.UserName + "' " +
                                "where ItemSarmut='" + List.ItemSarmut + "' and ParameterTerukur='" + List.ParameterTerukur + "' and Bulan='" + ddlBulan.SelectedValue + "' " +
                                "and Tahun='" + ddlTahunBulan.SelectedValue + "' and RowStatus>-1 ";
                            }
                            SqlDataReader sd0 = z0.Retrieve();
                        }
                        i = i + 1;
                    }
                    //}                
                }
                #endregion

                Link2PES();
                txtTotalStd.Text = Decimal.Parse(Session["totalatas"].ToString()).ToString("N0");
            }
        }

        protected void Link2PES()
        {
            string strError = ""; int Approval = -1; int ada = 0; decimal actual = 0;
            string actual_persen = string.Empty;
            string actual_nilai = string.Empty;

            Users user = (Users)Session["Users"];

            string PeriodePES = Session["PeriodePES"].ToString();

            ArrayList arrDefect = new ArrayList();
            FacadeBudgetDelivery sarPesDefect = new FacadeBudgetDelivery();
            arrDefect = sarPesDefect.RetrieveUser_CC();
            foreach (BudgetDelivery uc in arrDefect)
            {
                ZetroView zv0 = new ZetroView();
                zv0.QueryType = Operation.CUSTOM;
                zv0.CustomQuery =
                " select sum(ada)ada from ( " +
                " select ID ada from PES_Nilai where UserID_ISO='" + uc.UserID + "' and Periode='" + PeriodePES + "' " +
                " and ItemSarMut='" + uc.ItemSarMut + "' and RowStatus>-1 " +
                " union all " +
                " select 0 ) as x ";
                SqlDataReader dr0 = zv0.Retrieve();
                if (dr0.HasRows)
                {
                    while (dr0.Read())
                    {
                        ada = Convert.ToInt32(dr0["ada"]);
                    }
                }

                ZetroView zv = new ZetroView();
                zv.QueryType = Operation.CUSTOM;
                zv.CustomQuery =
                " select sum(Approval)Approval from ( " +
                " select Approval from ISO_KPIDetail where KPIID in (select ID from ISO_KPI where left(convert(char,tglmulai,112),6)='" + PeriodePES + "' " +
                " and DeptID in (26,27) and ISO_UserID='" + uc.UserID + "' and RowStatus>-1 and CategoryID in (select ID from ISO_UserCategory  " +
                " where Sarmut='" + uc.ItemSarMut + "')) and RowStatus>-1 " +
                "  union all " +
                " select 0 ) as x ";
                SqlDataReader dr = zv.Retrieve();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Approval = Convert.ToInt32(dr["Approval"]);
                    }
                }

                if (Approval == 0 && ada == 0)
                {
                    #region Insert ke table PES_Nilai
                    //ZetroView zSave = new ZetroView();
                    //zSave.QueryType = Operation.CUSTOM;
                    //zSave.CustomQuery =

                    //" insert into PES_Nilai (ItemSarmut,DeptID,UserID_ISO,Periode,Actual_Value,RowStatus,CreatedTime,CreatedBy,Persen) " +
                    //" values " +
                    //" ('" + uc.ItemSarMut + "','" + uc.DeptID + "','" + uc.UserID + "','" + PeriodePES + "','" + actual_nilai + "','0',GETDATE(),'" + user.UserName + "','" + actual_persen + "')";

                    //SqlDataReader sdSave = zSave.Retrieve();

                    ZetroView zInsert2 = new ZetroView();
                    zInsert2.QueryType = Operation.CUSTOM;
                    zInsert2.CustomQuery =
                        "exec sp_InsertNilaiPES '" + uc.UserID + "', '" + PeriodePES + "','" + uc.ItemSarMut + "','0' ";
                    SqlDataReader sdrInsert2 = zInsert2.Retrieve();

                    #endregion
                }
                //else if (Approval == 0 && ada > 0)
                //{
                //    ZetroView zInsert2 = new ZetroView();
                //    zInsert2.QueryType = Operation.CUSTOM;
                //    zInsert2.CustomQuery =
                //        "exec sp_InsertNilaiPES '" + uc.UserID + "', '" + PeriodePES + "','" + uc.ItemSarMut + "','" + ada + "' ";
                //    SqlDataReader sdrInsert2 = zInsert2.Retrieve();
                //}
            }
        }

        private string SimpanKPI(SarmutPes sop)
        {
            string strEvent = "Insert";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(3, 3, Convert.ToDateTime(txtTglMulai.Text).Year);
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 2;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 2;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                //HO ikut C dulu
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }

            SarmutPESProcessFacade sarpesProcessFacade = new SarmutPESProcessFacade(sop, docNo);
            string strError = string.Empty;
            strError = sarpesProcessFacade.Insert();
            if (strError == string.Empty)
            {
                //InsertLog(strEvent);
                //txtTaskNo.Text = "Doc No. : " + sarpesProcessFacade.sopNonya;
            }
            return strError;
        }
        private string UpdateKPI(SarmutPes sop)
        {
            string strEvent = "Update";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(3, 3, Convert.ToInt32(ddlTahunBulan.SelectedValue));
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 2;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 2;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                //HO ikut C dulu
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }

            SarmutPESProcessFacade sarpesProcessFacade = new SarmutPESProcessFacade(sop, docNo);
            string strError = string.Empty;
            strError = sarpesProcessFacade.UpdateKpi();
            if (strError == string.Empty)
            {
                //InsertLog(strEvent);
                //txtTaskNo.Text = "Doc No. : " + sarpesProcessFacade.sopNonya;
            }
            return strError;
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
        private void LoadBudgetTahunan()
        {
            string NamaBulan1 = Session["NamaBulan"].ToString();
            string Periode1 = Session["Periode"].ToString();
            string PengKali1 = Session["PengKali"].ToString();

            PanelRTahunan.Visible = true;
            string Tahun = ddlTahun.SelectedItem.ToString();
            FacadeBudgetDelivery rpsT = new FacadeBudgetDelivery();
            ArrayList arrBudget2 = new ArrayList();
            arrBudget2 = rpsT.RetrieveBudgetDeliveryTahunan(Tahun, Periode1, PengKali1, NamaBulan1);
            DataBudget2.DataSource = arrBudget2;
            DataBudget2.DataBind();
        }

        protected void LoadBulan()
        {
            ArrayList arrBulan = new ArrayList();
            FacadeBudgetDelivery data = new FacadeBudgetDelivery();
            arrBulan = data.RetrieveBulan();
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem(Session["NamaBulanTemp"].ToString(), DateTime.Now.Month.ToString()));
            foreach (BudgetDelivery bulan in arrBulan)
            {
                ddlBulan.Items.Add(new ListItem(bulan.BulanNama, bulan.Bulan));
            }
        }
        protected void LoadTahun()
        {
            ArrayList arrTahun = new ArrayList();
            FacadeBudgetDelivery data = new FacadeBudgetDelivery();
            arrTahun = data.RetrieveTahun();
            ddlTahunBulan.Items.Clear();
            ddlTahunBulan.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            foreach (BudgetDelivery tahun in arrTahun)
            {
                ddlTahunBulan.Items.Add(new ListItem(tahun.Tahun, tahun.Tahun));
            }
        }

        protected void LoadTahunAja()
        {
            ArrayList arrTahun = new ArrayList();
            FacadeBudgetDelivery data = new FacadeBudgetDelivery();
            arrTahun = data.RetrieveTahun();
            ddlTahun.Items.Clear();
            //ddlTahun.Items.Add(new ListItem("-- Pilih --", "0"));
            ddlTahunBulan.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            foreach (BudgetDelivery tahun in arrTahun)
            {
                ddlTahun.Items.Add(new ListItem(tahun.Tahun, tahun.Tahun));
            }
        }

        protected void ddlBulan_SelectedChange(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahunBulan.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahunBulan.SelectedValue.ToString();
        }
        protected void ddlTahunBulan_SelectedChange(object sender, EventArgs e)
        { }

        protected void ddlTahun_SelectedChange(object sender, EventArgs e)
        { }

        protected void RB1_CheckedChanged(object sender, EventArgs e)
        {
            if (RB1.Checked)
            {
                PanelPeriodeTahun.Visible = false;
                PanelUtama.Visible = true;
                PanelPeriodeBulan.Visible = true;
                PanelRTahunan.Visible = false;
                LoadBudget();
                LoadBulan();
                LoadTahun();
            }
        }

        protected void LoadBudgetPerBulan()
        {
            PanelPeriodeTahun.Visible = false;
            PanelUtama.Visible = true;
            PanelPeriodeBulan.Visible = true;
            PanelRTahunan.Visible = false;
            //LoadBudget();
            LoadBulan();
            LoadTahun();
        }

        protected void RB2_CheckedChanged(object sender, EventArgs e)
        {
            if (RB2.Checked)
            {
                string Bulan1 = DateTime.Now.Month.ToString();
                string Tahun1 = DateTime.Now.Year.ToString();

                if (Tahun1 == "2018")
                {
                    if (Bulan1 == "10")
                    {
                        string NamaBulan = "Okt"; Session["NamaBulan"] = NamaBulan;
                        string Periode = "(Okt)"; Session["Periode"] = Periode;
                        string PengKali = "1"; Session["PengKali"] = PengKali;
                    }
                    else if (Bulan1 == "11")
                    {
                        string NamaBulan = "Nov"; Session["NamaBulan"] = NamaBulan;
                        string Periode = "(Okt,Nov)"; Session["Periode"] = Periode;
                        string PengKali = "2"; Session["PengKali"] = PengKali;
                    }
                    else if (Bulan1 == "12")
                    {
                        string NamaBulan = "Des"; Session["NamaBulan"] = NamaBulan;
                        string Periode = "(Okt,Nov,Des)"; Session["Periode"] = Periode;
                        string PengKali = "3"; Session["PengKali"] = PengKali;
                    }
                }
                else if (Tahun1 != "2018")
                {
                    if (Bulan1 == "1")
                    {
                        string NamaBulan = "Jan"; Session["NamaBulan"] = NamaBulan;
                        string Periode = "(Jan)"; Session["Periode"] = Periode;
                        string PengKali = "1"; Session["PengKali"] = PengKali;
                    }
                    else if (Bulan1 == "2")
                    {
                        string NamaBulan = "Feb"; Session["NamaBulan"] = NamaBulan;
                        string Periode = "(Jan,Feb)"; Session["Periode"] = Periode;
                        string PengKali = "2"; Session["PengKali"] = PengKali;
                    }
                    else if (Bulan1 == "3")
                    {
                        string NamaBulan = "Mrt"; Session["NamaBulan"] = NamaBulan;
                        string Periode = "(Jan,Feb,Mrt)"; Session["Periode"] = Periode;
                        string PengKali = "3"; Session["PengKali"] = PengKali;
                    }
                    else if (Bulan1 == "4")
                    {
                        string NamaBulan = "Apr"; Session["NamaBulan"] = NamaBulan;
                        string Periode = "(Jan,Feb,Mrt,Apr)"; Session["Periode"] = Periode;
                        string PengKali = "4"; Session["PengKali"] = PengKali;
                    }
                    else if (Bulan1 == "5")
                    {
                        string NamaBulan = "Mei"; Session["NamaBulan"] = NamaBulan;
                        string Periode = "(Jan,Feb,Mrt,Apr,Mei)"; Session["Periode"] = Periode;
                        string PengKali = "5"; Session["PengKali"] = PengKali;
                    }
                    else if (Bulan1 == "6")
                    {
                        string NamaBulan = "Jun"; Session["NamaBulan"] = NamaBulan;
                        string Periode = "(Jan,Feb,Mrt,Apr,Mei,Jun)"; Session["Periode"] = Periode;
                        string PengKali = "6"; Session["PengKali"] = PengKali;
                    }
                }
                else if (Bulan1 == "7")
                {
                    string NamaBulan = "Jul"; Session["NamaBulan"] = NamaBulan;
                    string Periode = "(Jan,Feb,Mrt,Apr,Mei,Jun,Jul)"; Session["Periode"] = Periode;
                    string PengKali = "7"; Session["PengKali"] = PengKali;
                }
                else if (Bulan1 == "8")
                {
                    string NamaBulan = "Agst"; Session["NamaBulan"] = NamaBulan;
                    string Periode = "(Jan,Feb,Mrt,Apr,Mei,Jun,Jul,Agst)"; Session["Periode"] = Periode;
                    string PengKali = "8"; Session["PengKali"] = PengKali;
                }
                else if (Bulan1 == "9")
                {
                    string NamaBulan = "Sep"; Session["NamaBulan"] = NamaBulan;
                    string Periode = "(Jan,Feb,Mrt,Apr,Mei,Jun,Jul,Agst,Sept)"; Session["Periode"] = Periode;
                    string PengKali = "9"; Session["PengKali"] = PengKali;
                }
                else if (Bulan1 == "10")
                {
                    string NamaBulan = "Okt"; Session["NamaBulan"] = NamaBulan;
                    string Periode = "(Jan,Feb,Mrt,Apr,Mei,Jun,Jul,Agst,Sept,Okt)"; Session["Periode"] = Periode;
                    string PengKali = "10"; Session["PengKali"] = PengKali;
                }
                else if (Bulan1 == "11")
                {
                    string NamaBulan = "Nov"; Session["NamaBulan"] = NamaBulan;
                    string Periode = "(Jan,Feb,Mrt,Apr,Mei,Jun,Jul,Agst,Sept,Okt,Nov)"; Session["Periode"] = Periode;
                    string PengKali = "11"; Session["PengKali"] = PengKali;
                }
                else if (Bulan1 == "12")
                {
                    string NamaBulan = "Des"; Session["NamaBulan"] = NamaBulan;
                    string Periode = "(Jan,Feb,Mrt,Apr,Mei,Jun,Jul,Agst,Sept,Okt,Nov,Des)"; Session["Periode"] = Periode;
                    string PengKali = "12"; Session["PengKali"] = PengKali;
                }
            }

            PanelPeriodeTahun.Visible = true;
            PanelUtama.Visible = true;
            PanelPeriodeBulan.Visible = false;
            PanelRBulanan.Visible = false;
            LoadTahunAja();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlBulan.SelectedValue) < 6 && Convert.ToInt32(ddlTahun.SelectedItem) <= 2020)
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.BufferOutput = true;
                //Response.AddHeader("content-disposition", "attachment;filename=PemantauanBudgetDelivery.xls");
                Response.AddHeader("content-disposition", "attachment;filename=PemantauanBudgetDelivery" + "_" + ddlBulan.SelectedValue + ddlTahunBulan.SelectedItem.ToString().Trim() + ".xls");
                Response.Charset = "utf-8";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                string Html = "PEMANTAUAN BUDGET TRANSPORTATION";
                Html += "<br>Periode Bulan : " + " " + ddlBulan.SelectedItem.ToString().Trim() + " " + ddlTahunBulan.SelectedItem.ToString().Trim();
                string HtmlEnd = "";
                lstNew.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.BufferOutput = true;
                //Response.AddHeader("content-disposition", "attachment;filename=PemantauanBudgetDelivery.xls");
                Response.AddHeader("content-disposition", "attachment;filename=PemantauanBudgetDelivery" + "_" + ddlBulan.SelectedValue + ddlTahunBulan.SelectedItem.ToString().Trim() + ".xls");
                Response.Charset = "utf-8";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                string Html = "PEMANTAUAN BUDGET TRANSPORTATION";
                Html += "<br>Periode Bulan : " + " " + ddlBulan.SelectedItem.ToString().Trim() + " " + ddlTahunBulan.SelectedItem.ToString().Trim();
                string HtmlEnd = "";
                lstNew.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }
        }

        protected void btnExport2_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanBudgetDelivery.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<br>PEMANTAUAN BUDGET TRANSPORTATION";
            Html += "<br>Periode Tahunan : " + " " + ddlTahun.SelectedItem.ToString().Trim();
            string HtmlEnd = "";
            lstNew.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
    public class FacadeBudgetDelivery
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private BudgetDelivery pm = new BudgetDelivery();
        private List<SqlParameter> sqlListParam;

        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int Simpan(object objDomain)
        {
            try
            {
                pm = (BudgetDelivery)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Urutan", pm.Urutan));
                sqlListParam.Add(new SqlParameter("@PeriodeCostControl", pm.PeriodeCostControl));
                sqlListParam.Add(new SqlParameter("@NamaPlant", pm.NamaPlant));
                sqlListParam.Add(new SqlParameter("@NoPol", pm.NoPol));
                sqlListParam.Add(new SqlParameter("@JenisKendaraan", pm.JenisKendaraan));
                sqlListParam.Add(new SqlParameter("@MaxBudget", pm.MaxBudget));
                sqlListParam.Add(new SqlParameter("@Actual", pm.Actual));
                sqlListParam.Add(new SqlParameter("@Persen", pm.Persen));
                sqlListParam.Add(new SqlParameter("@CreatedBy", pm.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Km", pm.Km));
                sqlListParam.Add(new SqlParameter("@TtlBudget", pm.TtlBudget));
                //sqlListParam.Add(new SqlParameter("@Persen", pm.Persen));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "CostControlDelivery_Simpan");
                strError = da.Error;
                return result;
            }

            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int Cancel(object objDomain)
        {
            try
            {
                pm = (BudgetDelivery)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PeriodeCostControl", pm.PeriodeCostControl));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "CostControlDelivery_Cancel");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public ArrayList RetrieveBudgetDelivery(string Periode, string Query1, string Query2, string Query3, string Query4)
        {
            arrData = new ArrayList();

            string strSQL =
            " select ROW_NUMBER() over (order by UrutanIndex,Ngurut asc ) as No,* from (select Ngurut,Plant,JenisUnit,NoPol,MaxBudget,actual,Persen,Urutan,UrutanIndex " +
            " from ( select *,actual1 as actual,((Actual1/MaxBudget)*100)Persen  from (select ROW_NUMBER() over (order by urutan asc ) as Ngurut,Plant,JenisUnit,NoPol,MaxBudget," +
            " CAST(ISNULL(SUM(actual),0) AS int)Actual1,urutan,UrutanIndex from (select ID,case when UnitKerja=1 then 'CITEUREUP'  when UnitKerja=7 " +
            " then 'KARAWANG' when UnitKerja=3 then 'DEPO SEMARANG' when UnitKerja=4 then 'DEPO JOGJA' when UnitKerja=8 then 'DEPO SOLO' " +
            " when UnitKerja=6 then 'DEPO SURABAYA' when UnitKerja=14 then 'DEPO PURWOKERTO' end Plant ,JenisUnit,NoPol,MaxBudget,(select sum(Quantity*AvgPrice) " +
            " HargaActual from " + Query1 + "PakaiDetail pd  " +
            " where pd.NoPol=mta.NoPol and pd.ItemID not in (select ID from " + Query1 + "Inventory where ItemName='solar') and RowStatus>-1 and " +
            " PakaiID in (select ID from " + Query1 + "Pakai where LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "'  and Status=3 and DeptID=26) and pd.groupid in (8,9,5) )Actual,urutan,UrutanIndex " +
            " from " + Query1 + "MaterialBudgetArmada mta where RowStatus>-1 and mta.aktif=1) as Data1  " +
            " group by NoPol,plant,JenisUnit,MaxBudget,urutan,ID,UrutanIndex   ) as Data2) as Data3 " +

            " union all " +

            " select ROW_NUMBER() over (order by urutan asc ) as Ngurut,Plant,JenisUnit,NoPol,MaxBudget,actual,Persen,Urutan,UrutanIndex from " +
            " ( select *,actual1 as actual,((Actual1/MaxBudget)*100)Persen  from (select Plant,JenisUnit,NoPol,MaxBudget," +
            " CAST(ISNULL(SUM(actual),0) AS int)Actual1,urutan,UrutanIndex from (select ID,case when UnitKerja=1 then 'CITEUREUP'  when UnitKerja=7 " +
            " then 'KARAWANG' when UnitKerja=3 then 'DEPO SEMARANG' when UnitKerja=4 then 'DEPO JOGJA' when UnitKerja=8 then 'DEPO SOLO' " +
            " when UnitKerja=6 then 'DEPO SURABAYA' when UnitKerja=14 then 'DEPO PURWOKERTO' end Plant ,JenisUnit,NoPol,MaxBudget,(select sum(Quantity*AvgPrice) " +
            " HargaActual from " + Query2 + "PakaiDetail pd " +
            " where pd.NoPol=mta.NoPol and pd.ItemID not in (select ID from " + Query2 + "Inventory where ItemName='solar') and RowStatus>-1 and " +
            " PakaiID in (select ID from " + Query2 + "Pakai where LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "'  and Status=3 and DeptID=26) and pd.groupid in (8,9,5) )Actual," +
            " urutan,UrutanIndex from " + Query2 + "MaterialBudgetArmada mta where RowStatus>-1 and mta.aktif=1) as Data1  " +
            " group by NoPol,plant,JenisUnit,MaxBudget,urutan,UrutanIndex   ) as Data2) as Data3 " +

            " union all " +

            " select ROW_NUMBER() over (order by urutan asc ) as Ngurut,Plant,JenisUnit,NoPol,MaxBudget,actual,Persen,Urutan,UrutanIndex from  ( select *,actual1 as actual,((Actual1/MaxBudget)*100)Persen" +
            " from (select Plant,JenisUnit,NoPol,MaxBudget, CAST(ISNULL(SUM(actual),0) AS int)Actual1,urutan,UrutanIndex from (select mta.ID, " +
            " case " +
            " when UnitKerja=1 then 'CITEUREUP'  " +
            " when UnitKerja=7  then 'KARAWANG' " +
            " when UnitKerja=3 then 'DEPO SEMARANG' " +
            " when UnitKerja=4 then 'DEPO JOGJA' " +
            " when UnitKerja=8 then 'DEPO SOLO' " +
            " when UnitKerja=6 then 'DEPO SURABAYA' " +
            " when UnitKerja=0 then 'DEPO JEMBER' " +
            " when UnitKerja=9 then 'DEPO PURWOKERTO' " +
            " when UnitKerja=100 then 'DEPO PALU'  end Plant, " +
            " JenisUnit,NoPol,MaxBudget,(select sum(Quantity*AvgPrice)" +
            " HargaActual from " + Query3 + "PakaiDetail pd  where pd.NoPol=mta.NoPol and pd.ItemID not in " +
            "(select ID from " + Query3 + "Inventory where ItemName='solar') and RowStatus>-1 and  " +
            " PakaiID in (select ID from " + Query3 + "Pakai where LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "'  " +
            " and Status>=2 and DeptID in (23,24,25,26,27,29,31)) and pd.groupid in (8,9,5) )Actual, urutan,'3'UrutanIndex " +
            " from " + Query3 + "MaterialBudgetArmada mta " +
            " where mta.RowStatus>-1 and mta.aktif=1 ) as Data1  " +
            " group by NoPol,plant,JenisUnit,MaxBudget,urutan,UrutanIndex  ) as Data2) as Data3 " +

            " union all " +

            " select ROW_NUMBER() over (order by urutan asc ) as Ngurut,Plant,JenisUnit,NoPol,MaxBudget,actual,Persen,Urutan,UrutanIndex from  ( select *,actual1 as actual,((Actual1/MaxBudget)*100)Persen" +
            " from (select Plant,JenisUnit,NoPol,MaxBudget, CAST(ISNULL(SUM(actual),0) AS int)Actual1,urutan,UrutanIndex from (select mta.ID, " +
            " case " +
            " when UnitKerja=1 then 'CITEUREUP'  " +
            " when UnitKerja=2 then 'DEPO CIREBON'  " +
            " when UnitKerja=7  then 'KARAWANG' " +
            " when UnitKerja=3 then 'DEPO SEMARANG' " +
            " when UnitKerja=4 then 'DEPO JOGJA' " +
            " when UnitKerja=8 then 'DEPO SOLO' " +
            " when UnitKerja=6 then 'DEPO SURABAYA' " +
            " when UnitKerja=0 then 'DEPO JEMBER' " +
            " when UnitKerja=9 then 'DEPO PURWOKERTO' " +
            " when UnitKerja=100 then 'DEPO PALU'  end Plant, " +
            " JenisUnit,NoPol,MaxBudget,(select sum(Quantity*AvgPrice)" +
            " HargaActual from " + Query4 + "PakaiDetail pd  where pd.NoPol=mta.NoPol and pd.ItemID not in " +
            "(select ID from " + Query4 + "Inventory where ItemName='solar') and RowStatus>-1 and  " +
            " PakaiID in (select ID from " + Query4 + "Pakai where LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "'  " +
            " and Status>=2 and DeptID in (26)) and pd.groupid in (8,9,5) )Actual, urutan,'3'UrutanIndex " +
            " from " + Query4 + "MaterialBudgetArmada mta " +
            " where mta.RowStatus>-1 and mta.aktif=1 ) as Data1  " +
            " group by NoPol,plant,JenisUnit,MaxBudget,urutan,UrutanIndex  ) as Data2) as Data3 " +


            ") as Data4 order by UrutanIndex,Urutan ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BudgetDelivery
                    {
                        No = sdr["No"].ToString(),
                        Plant = sdr["Plant"].ToString(),
                        JenisUnit = sdr["JenisUnit"].ToString(),
                        NoPol = sdr["NoPol"].ToString(),
                        MaxBudget = decimal.Parse(sdr["MaxBudget"].ToString()),
                        Actual = decimal.Parse(sdr["Actual"].ToString()),
                        Persen = decimal.Parse(sdr["Persen"].ToString())
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveBudgetDeliveryNew(string Periode, string Query1, string Query2, string Query3, string Query4, string Query5, string Query6)
        {
            arrData = new ArrayList();
            string strSQL =
            /** Data Citeureup **/
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv01C]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv01C] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv02C]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv02C] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv03C]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv03C] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv04C]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv04C] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv05C]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv05C] " +
            " select mta.ID, /**case " +
            " when UnitKerja=1 then 'CITEUREUP'  " +
            " when UnitKerja=7 then 'KARAWANG' " +
            " when UnitKerja=13 then 'JOMBANG' " +
            " when UnitKerja=3 then 'DEPO SEMARANG' " +
            " when UnitKerja=4 then 'DEPO JOGJA' " +
            " when UnitKerja=8 then 'DEPO SOLO'  " +
            " when UnitKerja=6 then 'DEPO SURABAYA' " +
            " when UnitKerja=14 then 'DEPO PURWOKERTO' end **/ 'CITEUREUP'Plant,JenisUnit,NoPol,MaxBudget, " +
            " (select sum(Quantity*AvgPrice)HargaActual from " + Query1 + "PakaiDetail pd where /**pd.NoPol**/REPLACE(pd.NoPol,'-',' ')=mta.NoPol and pd.ItemID " +
            " not in (select ID from " + Query1 + "Inventory where ItemName='solar') and RowStatus>-1 and  " +
            " PakaiID in (select ID from " + Query1 + "Pakai where LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "' and Status=3 and DeptID=26) " +
            " and pd.groupid in (8,9,5) )Actual,urutan,UrutanIndex,'" + Periode + "'tgl,Km Km2,lock kunci into tempDataDlv01C " +
            " from " + Query1 + "MaterialBudgetArmada mta " +
            " where mta. RowStatus>-1 and mta.aktif=1 " +
            " select REPLACE(KendaraanNo,'-',' ')KendaraanNo,TglSch,sum(Kilometer)KM into tempDataDlv02C from ( " +
            " select B.KendaraanNo,left(convert(char,TglSch,112),6)TglSch,Kilometer " +
            " from [sql1.grcboard.com].GrcBoard.dbo.ex_transaksiroute A " +
            " left join [sql1.grcboard.com].GrcBoard.dbo.ex_MasterKendaraan B ON A.KendaraanID=B.ID " +
            " where LEFT(CONVERT(char,TglSch,112),6)='" + Periode + "' and A.rowstatus>-1 and B.rowstatus>-1) as x group by KendaraanNo,TglSch " +
            " select  ID,Plant,JenisUnit,NoPol,MaxBudget,isnull(Actual,0)Actual,urutan,UrutanIndex,isnull(KM,0)KM,isnull(A.Km2,0)Km2,A.Kunci " +
            " into tempDataDlv03C from tempDataDlv01C A left join tempDataDlv02C B ON A.NoPol=B.KendaraanNo and A.tgl=B.TglSch " +
            " select Plant,JenisUnit,NoPol,MaxBudget,Actual,Urutan,UrutanIndex,case when kunci=1 then KM else KM2 end Km " +
            " into tempDataDlv04C from tempDataDlv03C " +
            " select *,(MaxBudget*Km)TtlBudget into tempDataDlv05C from tempDataDlv04C " +
            /** End Data Citeureup **/

            /** Data Karawang **/
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv01K]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv01K] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv02K]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv02K] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv03K]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv03K] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv04K]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv04K] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv05K]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv05K] " +
            " select mta.ID, /**case " +
            " when UnitKerja=1 then 'CITEUREUP'  " +
            " when UnitKerja=7 then 'KARAWANG' " +
            " when UnitKerja=13 then 'JOMBANG' " +
            " when UnitKerja=3 then 'DEPO SEMARANG' " +
            " when UnitKerja=4 then 'DEPO JOGJA' " +
            " when UnitKerja=8 then 'DEPO SOLO'  " +
            " when UnitKerja=6 then 'DEPO SURABAYA' " +
            " when UnitKerja=14 then 'DEPO PURWOKERTO' end**/ 'KARAWANG'Plant,JenisUnit,NoPol,MaxBudget, " +
            " (select sum(Quantity*AvgPrice)HargaActual from " + Query2 + "PakaiDetail pd where /**pd.NoPol**/REPLACE(pd.NoPol,'-',' ')=mta.NoPol and pd.ItemID " +
            " not in (select ID from " + Query2 + "Inventory where ItemName='solar') and RowStatus>-1 and  " +
            " PakaiID in (select ID from " + Query2 + "Pakai where LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "' and Status=3 and DeptID=26) " +
            " and pd.groupid in (8,9,5) )Actual,urutan,UrutanIndex,'" + Periode + "'tgl,Km Km2,lock kunci into tempDataDlv01K " +
            " from " + Query2 + "MaterialBudgetArmada mta " +
            " where mta. RowStatus>-1 and mta.aktif=1 " +
            " select REPLACE(KendaraanNo,'-',' ')KendaraanNo,TglSch,sum(Kilometer)KM into tempDataDlv02K from ( " +
            " select B.KendaraanNo,left(convert(char,TglSch,112),6)TglSch,Kilometer " +
            " from [sql1.grcboard.com].GrcBoard.dbo.ex_transaksiroute A " +
            " left join [sql1.grcboard.com].GrcBoard.dbo.ex_MasterKendaraan B ON A.KendaraanID=B.ID " +
            " where LEFT(CONVERT(char,TglSch,112),6)='" + Periode + "' and A.rowstatus>-1 and B.rowstatus>-1) as x group by KendaraanNo,TglSch " +
            " select  ID,Plant,JenisUnit,NoPol,MaxBudget,isnull(Actual,0)Actual,urutan,UrutanIndex,isnull(KM,0)KM,isnull(A.Km2,0)Km2,A.Kunci " +
            " into tempDataDlv03K from tempDataDlv01K A left join tempDataDlv02K B ON A.NoPol=B.KendaraanNo and A.tgl=B.TglSch " +
            " select Plant,JenisUnit,NoPol,MaxBudget,Actual,Urutan,UrutanIndex,case when kunci=1 then KM else KM2 end Km " +
            " into tempDataDlv04K from tempDataDlv03K " +
            " select *,(MaxBudget*Km)TtlBudget into tempDataDlv05K from tempDataDlv04K " +
            /** End Data Karawang **/

            /** Data Jombang **/
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv01J]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv01J] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv02J]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv02J] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv03J]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv03J] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv04J]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv04J] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv05J]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv05J] " +
            " select mta.ID, /**case " +
            " when UnitKerja=1 then 'CITEUREUP'  " +
            " when UnitKerja=7 then 'KARAWANG' " +
            " when UnitKerja=13 then 'JOMBANG' " +
            " when UnitKerja=3 then 'DEPO SEMARANG' " +
            " when UnitKerja=4 then 'DEPO JOGJA' " +
            " when UnitKerja=8 then 'DEPO SOLO'  " +
            " when UnitKerja=6 then 'DEPO SURABAYA' " +
            " when UnitKerja=14 then 'DEPO PURWOKERTO' end **/ 'JOMBANG'Plant,JenisUnit,NoPol,MaxBudget, " +
            " (select sum(Quantity*AvgPrice)HargaActual from " + Query4 + "PakaiDetail pd where /**pd.NoPol**/REPLACE(pd.NoPol,'-',' ')=mta.NoPol and pd.ItemID " +
            " not in (select ID from " + Query4 + "Inventory where ItemName='solar') and RowStatus>-1 and  " +
            " PakaiID in (select ID from " + Query4 + "Pakai where LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "' and Status=3 and DeptID=26) " +
            " and pd.groupid in (8,9,5) )Actual,urutan,UrutanIndex,'" + Periode + "'tgl,Km Km2,lock kunci into tempDataDlv01J " +
            " from " + Query4 + "MaterialBudgetArmada mta " +
            " where mta. RowStatus>-1 and mta.aktif=1 " +
            " select REPLACE(KendaraanNo,'-',' ')KendaraanNo,TglSch,sum(Kilometer)KM into tempDataDlv02J from ( " +
            " select B.KendaraanNo,left(convert(char,TglSch,112),6)TglSch,Kilometer " +
            " from [sql1.grcboard.com].GrcBoard.dbo.ex_transaksiroute A " +
            " left join [sql1.grcboard.com].GrcBoard.dbo.ex_MasterKendaraan B ON A.KendaraanID=B.ID " +
            " where LEFT(CONVERT(char,TglSch,112),6)='" + Periode + "' and A.rowstatus>-1 and B.rowstatus>-1) as x group by KendaraanNo,TglSch " +
            " select  ID,Plant,JenisUnit,NoPol,MaxBudget,isnull(Actual,0)Actual,urutan,UrutanIndex,isnull(KM,0)KM,isnull(A.Km2,0)Km2,A.Kunci " +
            " into tempDataDlv03J from tempDataDlv01J A left join tempDataDlv02J B ON A.NoPol=B.KendaraanNo and A.tgl=B.TglSch " +
            " select Plant,JenisUnit,NoPol,MaxBudget,Actual,Urutan,UrutanIndex,case when kunci=1 then KM else KM2 end Km " +
            " into tempDataDlv04J from tempDataDlv03J " +
            " select *,(MaxBudget*Km)TtlBudget into tempDataDlv05J from tempDataDlv04J " +
            /** End Data Jombang **/

            /** Data Cirebon **/
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv01CR]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv01CR] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv02CR]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv02CR] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv03CR]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv03CR] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv04CR]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv04CR] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv05CR]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv05CR] " +
            " select mta.ID, /**case " +
            " when UnitKerja=1 then 'CITEUREUP'  " +
            " when UnitKerja=2 then 'DEPO CIREBON' " +
            " when UnitKerja=7 then 'KARAWANG' " +
            " when UnitKerja=13 then 'JOMBANG' " +
            " when UnitKerja=3 then 'DEPO SEMARANG' " +
            " when UnitKerja=4 then 'DEPO JOGJA' " +
            " when UnitKerja=8 then 'DEPO SOLO'  " +
            " when UnitKerja=6 then 'DEPO SURABAYA' " +
            " when UnitKerja=14 then 'DEPO PURWOKERTO' end **/'DEPO CIREBON'Plant,JenisUnit,NoPol,MaxBudget, " +
            " (select sum(Quantity*AvgPrice)HargaActual from " + Query6 + "PakaiDetail pd where /**pd.NoPol**/REPLACE(pd.NoPol,'-',' ')=mta.NoPol and pd.ItemID " +
            " not in (select ID from " + Query6 + "Inventory where ItemName='solar') and RowStatus>-1 and  " +
            " PakaiID in (select ID from " + Query6 + "Pakai where LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "' and Status=3 and DeptID=26) " +
            " and pd.groupid in (8,9,5) )Actual,urutan,UrutanIndex,'" + Periode + "'tgl,Km Km2,lock kunci into tempDataDlv01CR " +
            " from " + Query6 + "MaterialBudgetArmada mta " +
            " where mta. RowStatus>-1 and mta.aktif=1 " +
            " select REPLACE(KendaraanNo,'-',' ')KendaraanNo,TglSch,sum(Kilometer)KM into tempDataDlv02CR from ( " +
            " select B.KendaraanNo,left(convert(char,TglSch,112),6)TglSch,Kilometer " +
            " from [sql1.grcboard.com].GrcBoard.dbo.ex_transaksiroute A " +
            " left join [sql1.grcboard.com].GrcBoard.dbo.ex_MasterKendaraan B ON A.KendaraanID=B.ID " +
            " where LEFT(CONVERT(char,TglSch,112),6)='" + Periode + "' and A.rowstatus>-1 and B.rowstatus>-1) as x group by KendaraanNo,TglSch " +
            " select  ID,Plant,JenisUnit,NoPol,MaxBudget,isnull(Actual,0)Actual,urutan,UrutanIndex,isnull(KM,0)KM,isnull(A.Km2,0)Km2,A.Kunci " +
            " into tempDataDlv03CR from tempDataDlv01CR A left join tempDataDlv02CR B ON A.NoPol=B.KendaraanNo and A.tgl=B.TglSch " +
            " select Plant,JenisUnit,NoPol,MaxBudget,Actual,Urutan,UrutanIndex,case when kunci=1 then KM else KM2 end Km " +
            " into tempDataDlv04CR from tempDataDlv03CR " +
            " select *,(MaxBudget*Km)TtlBudget into tempDataDlv05CR from tempDataDlv04CR " +
            /** End Data Cirebon **/

            /** Data Depo ( Data ada di DB HO )**/
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv01H]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv01H] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv02H]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv02H] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv03H]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv03H] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv04H]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv04H] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv05H]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv05H] " +
            " select mta.ID, case " +
            " when UnitKerja=1 then 'CITEUREUP'  " +
            " when UnitKerja=7 then 'KARAWANG' " +
            " when UnitKerja=3 then 'DEPO SEMARANG' " +
            " when UnitKerja=4 then 'DEPO JOGJA' " +
            " when UnitKerja=8 then 'DEPO SOLO'  " +
            " when UnitKerja=6 then 'DEPO SURABAYA' " +
            " when UnitKerja=9 then 'DEPO PURWOKERTO' " +
            " when UnitKerja=0 then 'DEPO JEMBER' " +
            " end Plant,JenisUnit,NoPol,MaxBudget, " +
            " (select sum(Quantity*AvgPrice)HargaActual from " + Query3 + "PakaiDetail pd where /**pd.NoPol**/REPLACE(pd.NoPol,'-',' ')=mta.NoPol and pd.ItemID " +
            " not in (select ID from " + Query3 + "Inventory where ItemName='solar') and RowStatus>-1 and  " +
            " PakaiID in (select ID from " + Query3 + "Pakai where LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "' and Status=3 " +
            " and DeptID in (23,24,25,26,27,29,31)) " +
            " and pd.groupid in (8,9,5) )Actual,urutan,UrutanIndex,'" + Periode + "'tgl,Km Km2,lock kunci into tempDataDlv01H " +
            " from " + Query3 + "MaterialBudgetArmada mta " +
            " where mta. RowStatus>-1 and mta.aktif=1 " +
            " select REPLACE(KendaraanNo,'-',' ')KendaraanNo,TglSch,sum(Kilometer)KM into tempDataDlv02H from ( " +
            " select B.KendaraanNo,left(convert(char,TglSch,112),6)TglSch,Kilometer " +
            " from [sql1.grcboard.com].GrcBoard.dbo.ex_transaksiroute A " +
            " left join [sql1.grcboard.com].GrcBoard.dbo.ex_MasterKendaraan B ON A.KendaraanID=B.ID " +
            " where LEFT(CONVERT(char,TglSch,112),6)='" + Periode + "' and A.rowstatus>-1 and B.rowstatus>-1) as x group by KendaraanNo,TglSch " +
            " select  ID,Plant,JenisUnit,NoPol,MaxBudget,isnull(Actual,0)Actual,urutan,UrutanIndex,isnull(KM,0)KM,isnull(A.Km2,0)Km2,A.Kunci " +
            " into tempDataDlv03H from tempDataDlv01H A left join tempDataDlv02H B ON A.NoPol=B.KendaraanNo and A.tgl=B.TglSch " +
            " select Plant,JenisUnit,NoPol,MaxBudget,Actual,Urutan,UrutanIndex,case when kunci=1 then KM else KM2 end Km " +
            " into tempDataDlv04H from tempDataDlv03H " +
            " select *,(MaxBudget*Km)TtlBudget into tempDataDlv05H from tempDataDlv04H " +
            /** End Data Depo **/

            /** Kumpulin jadi satu laporan ***/
            " select ROW_NUMBER() over (order by urutanindex,urutan asc ) as No,Plant,JenisUnit,NoPol,MaxBudget,cast(Actual as decimal(18,2))Actual,Persen,Km,cast(TtlBudget as decimal(18,2))TtlBudget from ( " +
            " select *,case when TtlBudget=0 then 0 else cast((Actual/TtlBudget)*100 as decimal(10,2)) end Persen from tempDataDlv05C " +
            " union all " +
            " select *,case when TtlBudget=0 then 0 else cast((Actual/TtlBudget)*100 as decimal(10,2)) end Persen from tempDataDlv05K " +
            " union all " +
            " select *,case when TtlBudget=0 then 0 else cast((Actual/TtlBudget)*100 as decimal(10,2)) end Persen from tempDataDlv05J " +
            " union all " +
            " select *,case when TtlBudget=0 then 0 else cast((Actual/TtlBudget)*100 as decimal(10,2)) end Persen from tempDataDlv05H " +
            " union all " +
            " select *,case when TtlBudget=0 then 0 else cast((Actual/TtlBudget)*100 as decimal(10,2)) end Persen from tempDataDlv05CR " +
            "  ) as x order by UrutanIndex,Urutan ";




            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BudgetDelivery
                    {
                        No = sdr["No"].ToString(),
                        Plant = sdr["Plant"].ToString(),
                        JenisUnit = sdr["JenisUnit"].ToString(),
                        NoPol = sdr["NoPol"].ToString(),
                        MaxBudget = decimal.Parse(sdr["MaxBudget"].ToString()),
                        Actual = decimal.Parse(sdr["Actual"].ToString()),
                        Persen = decimal.Parse(sdr["Persen"].ToString()),
                        Km = Convert.ToInt32(sdr["Km"].ToString()),
                        TtlBudget = decimal.Parse(sdr["TtlBudget"].ToString())
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveBudgetDeliveryTahunan(string Tahun, string Periode, string PengKali, string NamaBulan)
        {
            arrData = new ArrayList();
            if (Tahun == "2018")
            {
                if (NamaBulan == "Okt")
                {
                    string PeriodeBulan = "(Okt)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Nov")
                {
                    string PeriodeBulan = "(Okt+Nov)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Des")
                {
                    string PeriodeBulan = "(Okt+Nov+Des)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
            }
            else if (Tahun != "2018")
            {
                if (NamaBulan == "Jan")
                {
                    string PeriodeBulan = "(Jan)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Feb")
                {
                    string PeriodeBulan = "(Jan+Feb)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Mrt")
                {
                    string PeriodeBulan = "(Jan+Feb+Mrt)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Apr")
                {
                    string PeriodeBulan = "(Jan+Feb+Mrt+Apr)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Mei")
                {
                    string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Jun")
                {
                    string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Jul")
                {
                    string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Agst")
                {
                    string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul+Agst)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Sept")
                {
                    string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul+Agst+Sept)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Okt")
                {
                    string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul+Agst+Sept+Okt)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Nov")
                {
                    string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul+Agst+Sept+Okt+Nov)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
                else if (NamaBulan == "Des")
                {
                    string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul+Agst+Sept+Okt+Nov+Des)";
                    HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
                }
            }

            HttpContext context1 = HttpContext.Current;
            string PeriodeBulan2 = (string)(context1.Session["Bulan"]);

            string Query1 = string.Empty;
            Query1 = "ISNULL(((NULLIF(" + PeriodeBulan2 + ",0)/(MaxBudget*" + PengKali + "))*100),0)Persen";
            string strSQL =
            "  select *," + PeriodeBulan2 + "Total, " +
            " " + Query1 + " from ( " +
            " select ROW_NUMBER() over (order by data1.urutan asc ) as NO,Data1.JenisUnit,Data1.NoPol,data1.MaxBudget, " +
            " case when Data1.UnitKerja=1 then 'CITEUREUP' when Data1.UnitKerja=7 then 'KARAWANG' end Plant, " +
            " ISNULL(A01,0)Jan,ISNULL(A02,0)Feb,ISNULL(A03,0)Mrt,ISNULL(A04,0)Apr, " +
            " ISNULL(A05,0)Mei,ISNULL(A06,0)Jun,ISNULL(A07,0)Jul,ISNULL(A08,0)Agst, " +
            " ISNULL(A09,0)Sept,ISNULL(A10,0)Okt,ISNULL(A11,0)Nov,ISNULL(A12,0)Des,Data1.urutan " +
            " from (select A.JenisUnit,A.NoPol,A.Urutan,A.MaxBudget,A.UnitKerja, " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "01' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A01', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "02' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A02', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "03' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A03', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "04' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A04', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "05' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A05', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "06' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A06', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "07' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A07', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "08' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A08', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "09' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A09', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "10' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A10', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "11' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A11', " +
            " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "12' and pk.ItemID not in (select ID from Inventory where ItemName='solar' and GroupID=2) and Status=2))'A12' " +
            " from MaterialBudgetArmada A where A.RowStatus > -1 ) as Data1 " +
            " ) as Data2 group by NoPol,NO,JenisUnit,MaxBudget,Jan,feb,Mrt,Apr,Mei,Jun,Jul,Agst,Sept,Okt,Nov,Des,urutan,plant order by Data2.urutan ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BudgetDelivery
                    {
                        No = sdr["No"].ToString(),
                        Plant = sdr["Plant"].ToString(),
                        JenisUnit = sdr["JenisUnit"].ToString(),
                        NoPol = sdr["NoPol"].ToString(),
                        MaxBudget = Convert.ToDecimal(sdr["MaxBudget"]),
                        Jan = Convert.ToDecimal(sdr["Jan"]),
                        Feb = Convert.ToDecimal(sdr["Feb"]),
                        Mrt = Convert.ToDecimal(sdr["Mrt"]),
                        Apr = Convert.ToDecimal(sdr["Apr"]),
                        Mei = Convert.ToDecimal(sdr["Mei"]),
                        Jun = Convert.ToDecimal(sdr["Jun"]),
                        Jul = Convert.ToDecimal(sdr["Jul"]),
                        Agst = Convert.ToDecimal(sdr["Agst"]),
                        Sept = Convert.ToDecimal(sdr["Sept"]),
                        Okt = Convert.ToDecimal(sdr["Okt"]),
                        Nov = Convert.ToDecimal(sdr["Nov"]),
                        Des = Convert.ToDecimal(sdr["Des"]),
                        Total = Convert.ToDecimal(sdr["Total"]),
                        Persen = Convert.ToDecimal(sdr["Persen"])
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveBulan()
        {
            arrData = new ArrayList();
            string strSQL =
            " select Bulan,case " +
            " when Bulan=1 then 'JANUARI' " +
            " when Bulan=2 then 'FEBRUARI' " +
            " when Bulan=3 then 'MARET' " +
            " when Bulan=4 then 'APRIL' " +
            " when Bulan=5 then 'MEI' " +
            " when Bulan=6 then 'JUNI' " +
            " when Bulan=7 then 'JULI' " +
            " when Bulan=8 then 'AGUSTUS' " +
            " when Bulan=9 then 'SEPTEMBER' " +
            " when Bulan=10 then 'OKTOBER' " +
            " when Bulan=11 then 'NOVEMBER' " +
            " when Bulan=12 then 'DESEMBER' " +
            " end BulanNama from " +
            " (select DISTINCT(MONTH(CreatedTime))Bulan from SPP where LEFT(convert(char,createdtime,112),6)>='201810') as Data1 order by Bulan";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BudgetDelivery
                    {
                        Bulan = sdr["Bulan"].ToString(),
                        BulanNama = sdr["BulanNama"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveTahun()
        {
            arrData = new ArrayList();
            string strSQL =
            " select DISTINCT(YEAR(CreatedTime))Tahun from SPP where LEFT(convert(char,createdtime,112),6)>='201810' ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BudgetDelivery
                    {
                        Tahun = sdr["Tahun"].ToString()
                    });
                }
            }
            return arrData;
        }

        public BudgetDelivery RetrieveTotalBudget(string Periode, string Query1, string Query2, string Query3, string Query4, string Query5)
        {
            string strSQL = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            if (Convert.ToInt32(Periode) < 202101)
            {
                #region Query Lama
                strSQL =
                "select SUM(TotalStd)TotalStd,SUM(TotalActual)TotalActual,cast(((SUM(TotalActual)/SUM(TotalStd))*100) as decimal(10,2))TotalPersen from (select SUM(MaxBudget)TotalStd,SUM(Actual1)TotalActual," +
                "SUM(Persen)TotalPersen from ( select *,actual1 as actual,((Actual1/MaxBudget)*100)Persen  from (select MaxBudget,CAST(ISNULL(SUM(actual),0) " +
                "AS int)Actual1,urutan from (select MaxBudget,(select sum(Quantity*AvgPrice) HargaActual  from " + Query1 + "PakaiDetail pd  where pd.NoPol=mta.NoPol " +
                "and RowStatus>-1 and PakaiID in (select ID from " + Query1 + "Pakai where  LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "'  and Status=3 and DeptID=26) and pd.groupid in (8,9,5)  and ItemID not in (select ID from " + Query1 + "Inventory where ItemName='solar' and Aktif=1))" +
                "Actual,urutan from " + Query1 + "MaterialBudgetArmada mta  where RowStatus>-1 and aktif=1) as Data1  group by MaxBudget,urutan  ) as Data2 ) as Data3 " +

                "union " +

                "select SUM(MaxBudget)TotalStd,SUM(Actual1)TotalActual,SUM(Persen)TotalPersen from ( select *,actual1 as actual,((Actual1/MaxBudget)*100)Persen " +
                "from (select MaxBudget,CAST(ISNULL(SUM(actual),0) AS int)Actual1,urutan from (select MaxBudget,(select sum(Quantity*AvgPrice) HargaActual  " +
                "from " + Query2 + "PakaiDetail pd  where pd.NoPol=mta.NoPol and RowStatus>-1 and PakaiID in (select ID from " +
                "" + Query2 + "Pakai where  LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "'  and Status=3 and DeptID=26) and pd.groupid in (8,9,5)  and ItemID not in (select ID from " + Query1 + "Inventory where ItemName='solar' and Aktif=1) )Actual,urutan " +
                "from " + Query2 + "MaterialBudgetArmada mta  where RowStatus>-1 and aktif=1) as Data1  group by MaxBudget,urutan  ) as Data2 ) as Data3 " +

                "union " +

                "select SUM(MaxBudget)TotalStd,SUM(Actual1)TotalActual,SUM(Persen)TotalPersen from ( select *,actual1 as actual,((Actual1/MaxBudget)*100)Persen " +
                "from (select MaxBudget,CAST(ISNULL(SUM(actual),0) AS int)Actual1,urutan from (select MaxBudget,(select sum(Quantity*AvgPrice) HargaActual  " +
                "from " + Query4 + "PakaiDetail pd  where pd.NoPol=mta.NoPol and RowStatus>-1 and PakaiID in (select ID from " +
                "" + Query4 + "Pakai where  LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "'  and Status=3 and DeptID=26) and pd.groupid in (8,9,5)  and ItemID not in (select ID from " + Query1 + "Inventory where ItemName='solar' and Aktif=1) )Actual,urutan " +
                "from " + Query4 + "MaterialBudgetArmada mta  where RowStatus>-1 and aktif=1) as Data1  group by MaxBudget,urutan  ) as Data2 ) as Data3 " +

                "union " +

                "select SUM(MaxBudget)TotalStd,SUM(Actual1)TotalActual,SUM(Persen)TotalPersen from ( select *,actual1 as actual,((Actual1/MaxBudget)*100)" +
                "Persen  from (select MaxBudget,CAST(ISNULL(SUM(actual),0) AS int)Actual1,urutan from (select MaxBudget,(select sum(Quantity*AvgPrice) " +
                "HargaActual  from " + Query3 + "PakaiDetail pd  where pd.NoPol=mta.NoPol and RowStatus>-1 and PakaiID in (select ID from " +
                "" + Query3 + "Pakai where  LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "'  and Status>=2 and DeptID in (12,23,24,25,26,27,29,31)) and pd.groupid in (8,9,5) and ItemID not in (select ID from " + Query1 + "Inventory where ItemName='solar' and Aktif=1))Actual,urutan " +
                "from " + Query3 + "MaterialBudgetArmada mta  where RowStatus>-1 and aktif=1) as Data1  group by MaxBudget,urutan  ) as Data2 ) " +
                "as Data3 " +

                 "union " +

                "select SUM(MaxBudget)TotalStd,SUM(Actual1)TotalActual,SUM(Persen)TotalPersen from ( select *,actual1 as actual,((Actual1/MaxBudget)*100)" +
                "Persen  from (select MaxBudget,CAST(ISNULL(SUM(actual),0) AS int)Actual1,urutan from (select MaxBudget,(select sum(Quantity*AvgPrice) " +
                "HargaActual  from " + Query5 + "PakaiDetail pd  where pd.NoPol=mta.NoPol and RowStatus>-1 and PakaiID in (select ID from " +
                "" + Query5 + "Pakai where  LEFT(CONVERT(char,pakaidate,112),6)='" + Periode + "'  and Status>=2 and DeptID in (26)) and pd.groupid in (8,9,5) and ItemID not in (select ID from " + Query1 + "Inventory where ItemName='solar' and Aktif=1))Actual,urutan " +
                "from " + Query5 + "MaterialBudgetArmada mta  where RowStatus>-1 and aktif=1) as Data1  group by MaxBudget,urutan  ) as Data2 ) " +
                "as Data3 " +

                ") as DataFinal ";
                #endregion
            }
            else
            {

                strSQL =
                " select sum(TtlBudget)TotalStd,sum(Actual)TotalActual,((sum(Actual)/sum(TtlBudget))*100)TotalPersen from ( " +
                " select sum(TtlBudget)TtlBudget,sum(Actual)Actual from tempDataDlv05H " +
                " union all " +
                " select sum(TtlBudget)TtlBudget,sum(Actual)Actual  from tempDataDlv05K " +
                " union all " +
                " select sum(TtlBudget)TtlBudget,sum(Actual)Actual  from tempDataDlv05J " +
                " union all " +
                " select sum(TtlBudget)TtlBudget,sum(Actual)Actual  from tempDataDlv05C " +
                " union all " +
                " select sum(TtlBudget)TtlBudget,sum(Actual)Actual  from tempDataDlv05CR " +
                " ) as x " +

                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv01C]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv01C] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv02C]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv02C] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv03C]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv03C] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv04C]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv04C] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv05C]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv05C] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv01K]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv01K] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv02K]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv02K] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv03K]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv03K] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv04K]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv04K] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv05K]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv05K] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv01J]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv01J] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv02J]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv02J] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv03J]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv03J] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv04J]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv04J] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv05J]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv05J] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv01CR]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv01CR] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv02CR]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv02CR] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv03CR]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv03CR] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv04CR]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv04CR] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv05CR]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv05CR] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv01H]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv01H] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv02H]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv02H] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv03H]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv03H] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv04H]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv04H] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataDlv05H]') AND type in (N'U')) DROP TABLE [dbo].[tempDataDlv05H] ";

            }


            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return RetrieveTotalBudget(sqlDataReader);
                }
            }

            return new BudgetDelivery();
        }

        public BudgetDelivery RetrieveTotalBudget(SqlDataReader sqlDataReader)
        {
            pm = new BudgetDelivery();
            pm.TotalStd = Convert.ToDecimal(sqlDataReader["TotalStd"]);
            pm.TotalPersen = Convert.ToDecimal(sqlDataReader["TotalPersen"]);
            pm.TotalActual = Convert.ToDecimal(sqlDataReader["TotalActual"]);
            return pm;
        }

        public BudgetDelivery RetrieveTotalBudgetS(string Periode, string Period)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string query = string.Empty;
            if (Period == "1")
            {
                query = " select sum(MaxBudget)TotalStd,sum(Actual)TotalActual,(sum(Actual)/sum(MaxBudget))*100 TotalPersen from MaterialBudgetArmada_nilai " +
                        " where Rowstatus>-1 and PeriodeCostControl='" + Periode + "' and Rowstatus>-1  ";
            }
            else
            {
                query = " select sum(TotalBudget)TotalStd,sum(Actual)TotalActual,(sum(Actual)/sum(TotalBudget))*100 TotalPersen " +
                        " from MaterialBudgetArmada_nilai  where Rowstatus>-1 and PeriodeCostControl='" + Periode + "' and Rowstatus>-1  ";
            }
            string strSQL = query;
            //" select sum(MaxBudget)TotalStd,sum(Actual)TotalActual,(sum(Actual)/sum(MaxBudget))*100 TotalPersen from MaterialBudgetArmada_nilai " +
            //" where Rowstatus>-1 and PeriodeCostControl='"+Periode+"' and Rowstatus>-1  ";

            //" select sum(TotalBudget)TotalStd,sum(Actual)TotalActual,(sum(Actual)/sum(TotalBudget))*100 TotalPersen "+
            //" from MaterialBudgetArmada_nilai  where Rowstatus>-1 and PeriodeCostControl='" + Periode + "' and Rowstatus>-1  ";



            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return RetrieveTotalBudget(sqlDataReader);
                }
            }

            return new BudgetDelivery();
        }

        public ArrayList RetrieveBudgetDeliveryAda(string Periode)
        {
            arrData = new ArrayList();

            string strSQL =
            " select Urutan No,NamaPlant Plant,JenisKendaraan JenisUnit,NoPol,MaxBudget,Actual,Persen from MaterialBudgetArmada_nilai " +
            " where Rowstatus>-1 and PeriodeCostControl='" + Periode + "' order by urutan ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BudgetDelivery
                    {
                        No = sdr["No"].ToString(),
                        Plant = sdr["Plant"].ToString(),
                        JenisUnit = sdr["JenisUnit"].ToString(),
                        NoPol = sdr["NoPol"].ToString(),
                        MaxBudget = decimal.Parse(sdr["MaxBudget"].ToString()),
                        Actual = decimal.Parse(sdr["Actual"].ToString()),
                        Persen = decimal.Parse(sdr["Persen"].ToString())
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveBudgetDeliveryAdaNew(string Periode)
        {
            arrData = new ArrayList();

            string strSQL =
            " select Urutan No,NamaPlant Plant,JenisKendaraan JenisUnit,NoPol,MaxBudget,Actual,Persen,Km,TotalBudget TtlBudget from MaterialBudgetArmada_nilai " +
            " where Rowstatus>-1 and PeriodeCostControl='" + Periode + "' order by urutan ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BudgetDelivery
                    {
                        No = sdr["No"].ToString(),
                        Plant = sdr["Plant"].ToString(),
                        JenisUnit = sdr["JenisUnit"].ToString(),
                        NoPol = sdr["NoPol"].ToString(),
                        MaxBudget = decimal.Parse(sdr["MaxBudget"].ToString()),
                        Actual = decimal.Parse(sdr["Actual"].ToString()),
                        Persen = decimal.Parse(sdr["Persen"].ToString()),
                        Km = Convert.ToInt32(sdr["Km"].ToString()),
                        TtlBudget = decimal.Parse(sdr["TtlBudget"].ToString())
                    });
                }
            }
            return arrData;
        }

        public int RetrieveDataBudgetDelivery(string PeriodeCC)
        {
            string StrSql =
            " select sum(Total)Total from(select count(ID)Total from MaterialBudgetArmada_nilai where PeriodeCostControl='" + PeriodeCC + "' and Rowstatus>-1 " +
            " union all " +
            " select 0 ) as xx ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Total"]);
                }
            }

            return 0;
        }

        public ArrayList RetrieveUser_CC()
        {
            arrData = new ArrayList();
            string strSQL = "  select distinct A.UserID,A.Sarmut ItemSarMut,B.DeptID from ISO_UserCategory A inner join ISO_Users B ON A.UserID=B.ID where sarmut like'%Budget Transportasi%' and A.RowStatus>-1  and B.RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            //arrUsers = new ArrayList();

            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectUserDefect(sqlDataReader));
                }
            }
            return arrData;
        }

        public BudgetDelivery GenerateObjectUserDefect(SqlDataReader sqlDataReader)
        {
            pm = new BudgetDelivery();
            pm.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            pm.ItemSarMut = sqlDataReader["ItemSarMut"].ToString();
            pm.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            return pm;
        }
    }

    public class BudgetDelivery
    {
        public string PeriodeCostControl { get; set; }
        public string NamaPlant { get; set; }
        public string JenisKendaraan { get; set; }
        public int Rowstatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Createdtime { get; set; }
        public int Urutan { get; set; }
        public int Km { get; set; }
        public decimal TtlBudget { get; set; }
        public int UserID { get; set; }
        public string ItemSarMut { get; set; }
        public int DeptID { get; set; }

        public string No { get; set; }
        public string Plant { get; set; }
        public string NoPol { get; set; }
        public decimal MaxBudget { get; set; }
        public decimal Actual { get; set; }
        public decimal Persen { get; set; }
        public string JenisUnit { get; set; }
        public decimal TotalStd { get; set; }
        public decimal TotalPersen { get; set; }
        public decimal TotalActual { get; set; }

        public string Bulan { get; set; }
        public string Tahun { get; set; }
        public string BulanNama { get; set; }

        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mrt { get; set; }
        public decimal Apr { get; set; }
        public decimal Mei { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Agst { get; set; }
        public decimal Sept { get; set; }
        public decimal Okt { get; set; }
        public decimal Nov { get; set; }
        public decimal Des { get; set; }
        public decimal Total { get; set; }
    }

    public class BudgetTrans
    {
        public string ItemSarmut { get; set; }
        public string ParameterTerukur { get; set; }
        public string Persen { get; set; }
        public string Flag { get; set; }
        public int Rowstatus { get; set; }
        //public int UserID { get; set; }
        //public string ItemSarMut { get; set; }
    }

    public class FacadeBudgetTrans
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private BudgetTrans objTrans = new BudgetTrans();
        private List<SqlParameter> sqlListParam;

        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int RetrieveApv(string Bulan, string Tahun)
        {
            string StrSql =
            " select Approval from SPD_TransPrs where SarmutPID in (select ID from SPD_Perusahaan where DeptID=11 and  " +
            " SarMutPerusahaan='Effesiensi Budget Transportasi' and RowStatus>-1) and RowStatus>-1 and bulan=" + Bulan + " and tahun=" + Tahun + " ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Approval"]);
                }
            }

            return 0;
        }

        public int RetrieveTableBB(string Bulan, string Tahun)
        {
            string StrSql =
            " select count(ID) Nilai from BukuBesar_Mapping where rowstatus>-1 and bulan=" + Bulan + " and tahun=" + Tahun + " " +
            " and ItemSarMut='Effesiensi Budget Transportasi' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Nilai"]);
                }
            }

            return 0;
        }

        public int RetrieveApvSarmut(string Bulan, string Tahun)
        {
            string StrSql =
            " select sum(Approval)ttl from ( " +
            " select Approval from SPD_TransPrs where SarmutPID in (select ID from SPD_Perusahaan where DeptID=11 and " +
            " SarMutPerusahaan='Effesiensi Budget Transportasi') and Tahun='" + Tahun + "' and Bulan='" + Bulan + "' and RowStatus>-1 " +
            " union all " +
            " select 0 ) as x ";

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

        public ArrayList RetrieveMapping(string A)
        {
            arrData = new ArrayList();
            string strSQL =
            " select * from BukuBesar_Flaging where ItemSarmut='" + A + "' and RowStatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BudgetTrans
                    {
                        ItemSarmut = sdr["ItemSarmut"].ToString(),
                        ParameterTerukur = sdr["ParameterTerukur"].ToString(),
                        Flag = sdr["Flag"].ToString()
                    });
                }
            }
            return arrData;
        }
    }
}