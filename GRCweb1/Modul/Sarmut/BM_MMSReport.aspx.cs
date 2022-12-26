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
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Globalization;

namespace GRCweb1.Modul.SarMut
{
    public partial class BM_MMSReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = (Users)Session["Users"];

                if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                {
                    PanelCiteureup.Visible = true;
                    PanelKarawang.Visible = false;
                }
                else
                {
                    PanelKarawang.Visible = true;
                    PanelCiteureup.Visible = false;
                }

                btnSimpan.Visible = false; btnCancel.Visible = false;
                LoadBulan();
                LoadTahun();
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
            ddlBulan.Items.Add(new ListItem("--Pilih Bulan--", "0"));
            for (int i = 1; i < 13; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            Session["Nilai"] = null;
            btnSimpan.Enabled = true;
            //if (user.UnitKerjaID == 1)
            //{
            //    for (int i = 0; i < lstMatrix.Items.Count; i++)
            //    {
            //        TextBox txt = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL1");
            //        TextBox txt2 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL2");
            //        TextBox txt3 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL3");
            //        TextBox txt4 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL4");

            //        txt.Text = ""; txt2.Text = ""; txt3.Text = ""; txt4.Text = "";
            //    }
            //}
            //else if (user.UnitKerjaID == 7)
            //{
            //    for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            //    {
            //        TextBox txt = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1");
            //        TextBox txt2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2");
            //        TextBox txt3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3");
            //        TextBox txt4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4");
            //        TextBox txt5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5");
            //        TextBox txt6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6");

            //        txt.Text = ""; txt2.Text = ""; txt3.Text = ""; txt4.Text = ""; txt5.Text = ""; txt6.Text = "";
            //    }
            //}

            Response.Redirect("BM_MMSReport.aspx");

        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            btnSimpan.Enabled = false;

            Users user = (Users)Session["Users"];
            #region Citeureup
            if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
            {
                for (int i = 0; i < lstMatrix.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                    if (tr.Cells[0].InnerHtml != "")
                    {
                        FacadeBMReportMMS fbm = new FacadeBMReportMMS();
                        DomainBMReportMMS dbm = new DomainBMReportMMS();
                        TextBox Keterangan = (TextBox)lstMatrix.Items[i].FindControl("Keterangan");
                        dbm.UnitKerjaID = user.UnitKerjaID;
                        dbm.Tanggal2 = Convert.ToDateTime(tr.Cells[0].InnerHtml);
                        dbm.Tanggal = tr.Cells[0].InnerHtml;
                        dbm.Keterangan = Keterangan.Text.ToString() != "" ? Keterangan.Text.ToString() : "-";

                        #region Line 1 
                        string OutM3L1 = tr.Cells[1].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[1].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L1 = Convert.ToDecimal(OutM3L1);
                        dbm.QtySPBL1 = decimal.Parse(tr.Cells[2].InnerHtml);
                        dbm.EfesiensiL1 = Math.Round(decimal.Parse(tr.Cells[3].InnerHtml), 2);
                        #endregion

                        #region Line 2
                        string OutM3L2 = tr.Cells[4].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[4].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L2 = Convert.ToDecimal(OutM3L2);
                        dbm.QtySPBL2 = decimal.Parse(tr.Cells[5].InnerHtml);
                        dbm.EfesiensiL2 = Math.Round(decimal.Parse(tr.Cells[6].InnerHtml), 2);
                        #endregion

                        #region Line 3
                        string OutM3L3 = tr.Cells[7].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[7].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L3 = Convert.ToDecimal(OutM3L3);
                        dbm.QtySPBL3 = decimal.Parse(tr.Cells[8].InnerHtml);
                        dbm.EfesiensiL3 = Math.Round(decimal.Parse(tr.Cells[9].InnerHtml), 2);
                        #endregion

                        #region Line 4
                        string OutM3L4 = tr.Cells[10].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[10].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L4 = Convert.ToDecimal(OutM3L4);
                        dbm.QtySPBL4 = decimal.Parse(tr.Cells[11].InnerHtml);
                        dbm.EfesiensiL4 = Math.Round(decimal.Parse(tr.Cells[12].InnerHtml), 2);
                        #endregion

                        #region Line 5                   
                        dbm.OutM3L5 = 0;
                        dbm.QtySPBL5 = 0;
                        dbm.EfesiensiL5 = 0;
                        #endregion

                        #region Line 6                                   
                        dbm.OutM3L6 = 0;
                        dbm.QtySPBL6 = 0;
                        dbm.EfesiensiL6 = 0;
                        #endregion

                        int rst = 0;

                        rst = fbm.insertPMMS(dbm);

                        if (rst > 0)
                        {
                            btnSimpan.Enabled = false;
                        }
                    }
                }
            }
            #endregion
            #region Karawang
            else if (user.UnitKerjaID == 7)
            {
                for (int i = 0; i < lstMatrixK2.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                    if (tr.Cells[0].InnerHtml != "")
                    {
                        FacadeBMReportMMS fbm = new FacadeBMReportMMS();
                        DomainBMReportMMS dbm = new DomainBMReportMMS();
                        TextBox Keterangan = (TextBox)lstMatrixK2.Items[i].FindControl("Keterangan");
                        dbm.UnitKerjaID = user.UnitKerjaID;
                        dbm.Tanggal2 = Convert.ToDateTime(tr.Cells[0].InnerHtml);
                        dbm.Tanggal = tr.Cells[0].InnerHtml;
                        dbm.Keterangan = Keterangan.Text.ToString() != "" ? Keterangan.Text.ToString() : "-";

                        #region Line 1
                        string OutM3L1 = tr.Cells[1].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[1].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L1 = Convert.ToDecimal(OutM3L1);
                        dbm.QtySPBL1 = decimal.Parse(tr.Cells[2].InnerHtml);
                        dbm.EfesiensiL1 = Math.Round(decimal.Parse(tr.Cells[3].InnerHtml), 2);
                        #endregion

                        #region Line 2
                        string OutM3L2 = tr.Cells[4].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[4].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L2 = Convert.ToDecimal(OutM3L2);
                        dbm.QtySPBL2 = decimal.Parse(tr.Cells[5].InnerHtml);
                        dbm.EfesiensiL2 = Math.Round(decimal.Parse(tr.Cells[6].InnerHtml), 2);
                        #endregion

                        #region Line 3
                        string OutM3L3 = tr.Cells[7].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[7].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L3 = Convert.ToDecimal(OutM3L3);
                        dbm.QtySPBL3 = decimal.Parse(tr.Cells[8].InnerHtml);
                        dbm.EfesiensiL3 = Math.Round(decimal.Parse(tr.Cells[9].InnerHtml), 2);
                        #endregion

                        #region Line 4
                        string OutM3L4 = tr.Cells[10].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[10].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L4 = Convert.ToDecimal(OutM3L4);
                        dbm.QtySPBL4 = decimal.Parse(tr.Cells[11].InnerHtml);
                        dbm.EfesiensiL4 = Math.Round(decimal.Parse(tr.Cells[12].InnerHtml), 2);
                        #endregion

                        #region Line 5
                        string OutM3L5 = tr.Cells[13].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[13].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L5 = Convert.ToDecimal(OutM3L5);
                        dbm.QtySPBL5 = decimal.Parse(tr.Cells[14].InnerHtml);
                        dbm.EfesiensiL5 = Math.Round(decimal.Parse(tr.Cells[15].InnerHtml), 2);
                        #endregion

                        #region Line 6
                        string OutM3L6 = tr.Cells[16].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[16].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L6 = Convert.ToDecimal(OutM3L6);
                        dbm.QtySPBL6 = decimal.Parse(tr.Cells[17].InnerHtml);
                        dbm.EfesiensiL6 = Math.Round(decimal.Parse(tr.Cells[18].InnerHtml), 2);
                        #endregion

                        int rst = 0;

                        rst = fbm.insertPMMS(dbm);

                        //if (rst > 0)
                        //{
                        //    btnSimpan.Enabled = false;
                        //}
                    }
                }
            }
            #endregion
            { DisplayAJAXMessage(this, " Data berhasil disimpan !! "); return; }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string Periode = Session["PeriodeBulanTahun"].ToString();
            FacadeBMReportMMS fbm2 = new FacadeBMReportMMS();
            DomainBMReportMMS dbm2 = new DomainBMReportMMS();
            Users user = (Users)Session["Users"];

            int intResult = 0;

            dbm2.Periode = Periode;
            dbm2.LastModifiedBy = user.UserName;

            intResult = fbm2.Cancel(dbm2);

            if (intResult > -1)
            {
                btnCancel.Enabled = false;

                LoadPreview();

                { DisplayAJAXMessage(this, " Data berhasil di cancel !! "); return; }
            }

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            #region Data Citeureup
            if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
            {
                for (int i = 0; i < lstMatrix.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                    TextBox Ket = (TextBox)lstMatrix.Items[i].FindControl("Keterangan");
                    Label Keterangan = (Label)lstMatrix.Items[i].FindControl("txtKeterangan");

                    if (Ket != null) { Ket.Visible = false; }
                    Keterangan.Text = Ket.Text;
                    if (Keterangan != null) { Keterangan.Visible = true; }

                    tr.Cells[13].Attributes.Add("style", "background-color:White;");

                }
            }
            #endregion
            #region Data Karawang
            else if (user.UnitKerjaID == 7)
            {
                for (int i = 0; i < lstMatrixK2.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                    TextBox Ket = (TextBox)lstMatrixK2.Items[i].FindControl("Keterangan");
                    Label Keterangan = (Label)lstMatrixK2.Items[i].FindControl("txtKeterangan");

                    if (Ket != null) { Ket.Visible = false; }
                    Keterangan.Text = Ket.Text;
                    if (Keterangan != null) { Keterangan.Visible = true; }

                    tr.Cells[19].Attributes.Add("style", "background-color:White;");
                }
                #endregion
            }

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanAntiFoam.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            string Html = "PT BANGUNPERKASA ADHITAMASENTRA";
            Html += " <br> ";
            Html += "<center><b>PEMANTAUAN EFFISIENSI ANTI FOAM</b></center>";
            Html += "<center>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedItem.Text + "<center>";
            Html += "";
            Html += " <br> ";
            Html += " <br> ";

            if (user.UnitKerjaID == 1)
            {
                lst.RenderControl(hw); string PlantNama = "Citeureup"; Session["PlantNama"] = PlantNama;
            }
            else if (user.UnitKerjaID == 13)
            {
                lst.RenderControl(hw); string PlantNama = "Jombang"; Session["PlantNama"] = PlantNama;
            }
            else if (user.UnitKerjaID == 7)
            {
                lstK.RenderControl(hw); string PlantNama = "Karawang"; Session["PlantNama"] = PlantNama;
            }
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"\"", "border=\"1\"");
            string Tanggal = Session["Tgl"].ToString();
            string Admin = Session["admin"].ToString();
            string Manager = Session["manager"].ToString();
            string Plant = Session["PlantNama"].ToString();

            if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
            {
                string HtmlEnd = " <br> ";
                HtmlEnd += " <br> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp; " + Plant + ", " + Tanggal + " ";
                HtmlEnd += "<br> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Mengetahui : ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Disetujui : ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Dibuat : ";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " <u>( Plant Manager )</u> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " <u>( " + Manager + " )</u> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " <u>( " + Admin + " )</u> ";

                HtmlEnd += "<br>";

                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Coprt Plant Manager ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; &emsp; ";
                HtmlEnd += " Manager Board Mill  ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;";
                HtmlEnd += " Staff Adm BM ";
                HtmlEnd += " &emsp;";

                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }
            else if (user.UnitKerjaID == 7)
            {
                string HtmlEnd = " <br> ";
                HtmlEnd += " <br> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp; " + Plant + ", " + Tanggal + " ";
                HtmlEnd += "<br> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Mengetahui : ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Disetujui : ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Dibuat : ";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";

                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " <u>( Plant Manager )</u> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " <u>( " + Manager + " )</u> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp; ";
                HtmlEnd += " <u>(" + Admin + ")</u> ";

                HtmlEnd += "<br>";

                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Coprt Plant Manager ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; &emsp; ";
                HtmlEnd += " Manager Board Mill  ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;";
                HtmlEnd += " Staff Adm BM ";
                HtmlEnd += " &emsp;";


                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }



        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            // priview();   
            LoadPreview();

            //agus 09-02-2022
            string thnbln = ddlTahun.SelectedValue.Trim() + ddlBulan.SelectedValue.Trim().PadLeft(2, '0');
            Users user = (Users)Session["Users"];
            if (user.UnitKerjaID == 7)
            {
                HasilKarawang(thnbln);
            }
            else if (user.UnitKerjaID == 1)
            {
                HasilCitereup(thnbln);
            }
            else if (user.UnitKerjaID == 13)
            {
                HasilHasilJombang(thnbln);
            }
            //agus 09-02-2022
        }


        protected void HasilKarawang(string thnbln)
        {
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery = "SELECT isnull(QueryAutoPES,'')QueryAutoPES from iso_usercategory where id=7964";
            SqlDataReader dr = zv.Retrieve();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strSQL = dr["QueryAutoPES"].ToString().Trim();
                }
            }
            if (strSQL != string.Empty)
            {
                string strparameter = "declare @thnbln int set @thnbln='" + thnbln + " '";
                ZetroView zv1 = new ZetroView();
                zv1.QueryType = Operation.CUSTOM;
                zv1.CustomQuery = strparameter + strSQL;
                SqlDataReader dr1 = zv1.Retrieve();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        txtakumulasioutput.Text = dr1["AkumulasiOutPut"].ToString().Trim();
                        txtakumulasipemakaian.Text = dr1["AkumulasiPemakaian"].ToString().Trim();
                        txttotaleffisiensi.Text = dr1["actual"].ToString().Trim();
                    }
                }

            }


        }

        protected void HasilCitereup(string thnbln)
        {
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery = "SELECT isnull(QueryAutoPES,'')QueryAutoPES from iso_usercategory where id=8373";
            SqlDataReader dr = zv.Retrieve();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strSQL = dr["QueryAutoPES"].ToString().Trim();
                }
            }
            if (strSQL != string.Empty)
            {
                string strparameter = "declare @thnbln int set @thnbln='" + thnbln + " '";
                ZetroView zv1 = new ZetroView();
                zv1.QueryType = Operation.CUSTOM;
                zv1.CustomQuery = strparameter + strSQL;
                SqlDataReader dr1 = zv1.Retrieve();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        txtakumulasioutput.Text = dr1["AkumulasiOutPut"].ToString().Trim();
                        txtakumulasipemakaian.Text = dr1["AkumulasiPemakaian"].ToString().Trim();
                        txttotaleffisiensi.Text = dr1["actual"].ToString().Trim();
                    }
                }

            }


        }

        protected void HasilHasilJombang(string thnbln)
        {
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery = "SELECT isnull(QueryAutoPES,'')QueryAutoPES from iso_usercategory where id=9667";
            SqlDataReader dr = zv.Retrieve();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strSQL = dr["QueryAutoPES"].ToString().Trim();
                }
            }
            if (strSQL != string.Empty)
            {
                string strparameter = "declare @thnbln int set @thnbln='" + thnbln + " '";
                ZetroView zv1 = new ZetroView();
                zv1.QueryType = Operation.CUSTOM;
                zv1.CustomQuery = strparameter + strSQL;
                SqlDataReader dr1 = zv1.Retrieve();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        txtakumulasioutput.Text = dr1["AkumulasiOutPut"].ToString().Trim();
                        txtakumulasipemakaian.Text = dr1["AkumulasiPemakaian"].ToString().Trim();
                        txttotaleffisiensi.Text = dr1["actual"].ToString().Trim();
                    }
                }

            }


        }

        protected void SarmutOnSystem1()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-1
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS Anti FOam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL1/nullif(OutM3L1,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L1),0)as decimal(10,2))OutM3L1,round(cast(isnull(sum(QtySPBL1),0)as decimal(10,2)),0) QtySPBL1 from  TempMSFoamx " +
                            " ) as xx ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 1' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }

        protected void SarmutOnSystem2()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-2
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS Anti Foam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL2/nullif(OutM3L2,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L2),0)as decimal(10,2))OutM3L2,round(cast(isnull(sum(QtySPBL2),0)as decimal(10,2)),0) QtySPBL2 from TempMSFoamx " +
                            " ) as xx ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 2' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }

        protected void SarmutOnSystem3()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-3
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS Anti Foam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL3/nullif(OutM3L3,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L3),0)as decimal(10,2))OutM3L3,round(cast(isnull(sum(QtySPBL3),0)as decimal(10,2)),0) QtySPBL3 from TempMSFoamx " +
                            " ) as xx ";


            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 3' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }

        protected void SarmutOnSystem4()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-4
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS Anti Foam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL4/nullif(OutM3L4,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L4),0)as decimal(10,2))OutM3L4,round(cast(isnull(sum(QtySPBL4),0)as decimal(10,2)),0) QtySPBL4 " +
                            " from TempMSFoamx  " +
                            " ) as xx ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 4' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }

        protected void SarmutOnSystem5()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-5
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS ANti FOam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL5/nullif(OutM3L5,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L5),0)as decimal(10,2))OutM3L5,round(cast(isnull(sum(QtySPBL5),0)as decimal(10,2)),0) QtySPBL5 " +
                            " from TempMSFoamx  " +
                            " ) as xx ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 5' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }

        protected void SarmutOnSystem6()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-6
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS Anti Foam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL6/nullif(OutM3L6,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L6),0)as decimal(10,2))OutM3L6,round(cast(isnull(sum(QtySPBL6),0)as decimal(10,2)),0) QtySPBL6 " +
                            " from TempMSFoamx  " +
                            " ) as xx ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 6' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
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

        protected void LoadPreview()
        {
            string TahunR = DateTime.Now.Year.ToString();
            string BulanR = DateTime.Now.Month.ToString();
            int HariR = DateTime.Now.Day;
            if (HariR < 10)
            { string HariR0 = "0" + HariR.ToString(); Session["HariR"] = HariR0; }
            else
            { string HariR0 = HariR.ToString(); Session["HariR"] = HariR0; }

            if (BulanR == "1") { string BulanString = "Januari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "2") { string BulanString = "Februari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "3") { string BulanString = "Maret"; Session["BulanS"] = BulanString; }
            else if (BulanR == "4") { string BulanString = "April"; Session["BulanS"] = BulanString; }
            else if (BulanR == "5") { string BulanString = "Mei"; Session["BulanS"] = BulanString; }
            else if (BulanR == "6") { string BulanString = "Juni"; Session["BulanS"] = BulanString; }
            else if (BulanR == "7") { string BulanString = "Juli"; Session["BulanS"] = BulanString; }
            else if (BulanR == "8") { string BulanString = "Agustus"; Session["BulanS"] = BulanString; }
            else if (BulanR == "9") { string BulanString = "September"; Session["BulanS"] = BulanString; }
            else if (BulanR == "10") { string BulanString = "Oktober"; Session["BulanS"] = BulanString; }
            else if (BulanR == "11") { string BulanString = "November"; Session["BulanS"] = BulanString; }
            else if (BulanR == "12") { string BulanString = "Desember"; Session["BulanS"] = BulanString; }
            string BulanAlias = Session["BulanS"].ToString();
            string HariAlias = Session["HariR"].ToString();
            string TglAliasR = HariAlias + "-" + BulanAlias + "-" + TahunR; Session["Tgl"] = TglAliasR;

            Users user = (Users)Session["Users"];
            int Bulan1 = int.Parse(ddlBulan.SelectedValue);
            string Tahun = ddlTahun.SelectedValue.ToString();
            if (Bulan1 < 10)
            {
                string Bulan2 = "0" + Bulan1.ToString(); Session["Bulan2"] = Bulan2;
            }
            else
            {
                string Bulan2 = Bulan1.ToString(); Session["Bulan2"] = Bulan2;
            }

            string Periode = Tahun + Session["Bulan2"].ToString() + "01";
            string PeriodeBulanTahun = Tahun + Session["Bulan2"].ToString();
            Session["PeriodeBulanTahun"] = PeriodeBulanTahun;

            DomainBMReportMMS DBM = new DomainBMReportMMS();
            FacadeBMReportMMS FBM = new FacadeBMReportMMS();
            int CekInput = FBM.RetrieveInputan(PeriodeBulanTahun);
            Session["Cek"] = CekInput;
            #region Ada inputan
            if (CekInput > 0)
            {
                btnSimpan.Visible = false; btnCancel.Visible = true;
                ArrayList arrData2 = new ArrayList();
                FacadeBMReportMMS Fbm2 = new FacadeBMReportMMS();

                if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                {
                    arrData2 = Fbm2.RetrieveReportMMS2(PeriodeBulanTahun); Session["Data2"] = arrData2;
                    lstMatrix.DataSource = arrData2;
                    lstMatrix.DataBind();
                }
                else if (user.UnitKerjaID == 7)
                {
                    arrData2 = Fbm2.RetrieveReportMMS2K(PeriodeBulanTahun); Session["Data2"] = arrData2;
                    lstMatrixK2.DataSource = arrData2;
                    lstMatrixK2.DataBind();
                }
            }
            #endregion
            #region Belum ada inputan
            else if (CekInput == 0)
            {
                btnSimpan.Visible = true; btnCancel.Visible = false;

                ArrayList arrData = new ArrayList();
                FacadeBMReportMMS Fbm = new FacadeBMReportMMS();

                if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                {
                    arrData = Fbm.RetrieveReportFlo(Periode, PeriodeBulanTahun); Session["Data"] = arrData;
                    lstMatrix.DataSource = arrData;
                    lstMatrix.DataBind();
                    if (user.UnitKerjaID == 1)
                    {
                        #region update sarmut
                        SarmutOnSystem1();
                        SarmutOnSystem2();
                        SarmutOnSystem3();
                        SarmutOnSystem4();
                    }
                    #endregion
                }
                else if (user.UnitKerjaID == 7)
                {
                    arrData = Fbm.RetrieveReportFloKarawang(Periode, PeriodeBulanTahun); Session["Data"] = arrData;
                    lstMatrixK2.DataSource = arrData;
                    lstMatrixK2.DataBind();

                    SarmutOnSystem1();
                    SarmutOnSystem2();
                    SarmutOnSystem3();
                    SarmutOnSystem4();
                    SarmutOnSystem5();
                    SarmutOnSystem6();
                }
            }
            #endregion


        }
        private decimal TotalPakai = 0;
        private decimal TotalStd = 0;

        protected void lstMatrix_DataBound(object sender, RepeaterItemEventArgs e)
        {
            decimal totalOutL1 = 0; decimal totalSPBL1 = 0; decimal totalEfesiensiL1 = 0;
            decimal totalOutL2 = 0; decimal totalSPBL2 = 0; decimal totalEfesiensiL2 = 0;
            decimal totalOutL3 = 0; decimal totalSPBL3 = 0; decimal totalEfesiensiL3 = 0;
            decimal totalOutL4 = 0; decimal totalSPBL4 = 0; decimal totalEfesiensiL4 = 0;

            Users user = (Users)Session["Users"];

            #region Citeureup
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                string tgl = tr.Cells[0].InnerHtml;
                DomainBMReportMMS bm = new DomainBMReportMMS();
                FacadeBMReportMMS fbm = new FacadeBMReportMMS();
                bm = fbm.Retrievetgl(tgl);

                #region Sudah ada inputan
                if (bm.ID > 0)
                {

                    TextBox ket = (TextBox)lstMatrix.Items[i].FindControl("Keterangan");
                    ket.Text = bm.Keterangan.Trim();
                    LabelStatus.Visible = true; LabelStatus.Attributes["style"] = "color:blue; font-weight:bold;"; LabelStatus.Font.Name = "calibri";
                    LabelStatus.Text = "Status Laporan : Release";

                    // tr.Cells[2].Attributes.Add("style", "background-color:White;");

                }
                #endregion
                #region Belum ada inputan
                else if (bm.ID == 0)
                {
                    string PakaiL1 = tr.Cells[2].InnerHtml != "" ? tr.Cells[2].InnerHtml : "0"; decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
                    string PakaiL2 = tr.Cells[5].InnerHtml != "" ? tr.Cells[5].InnerHtml : "0"; decimal DPakaiL2 = Convert.ToDecimal(PakaiL2);
                    string PakaiL3 = tr.Cells[8].InnerHtml != "" ? tr.Cells[8].InnerHtml : "0"; decimal DPakaiL3 = Convert.ToDecimal(PakaiL3);
                    string PakaiL4 = tr.Cells[11].InnerHtml != "" ? tr.Cells[11].InnerHtml : "0"; decimal DPakaiL4 = Convert.ToDecimal(PakaiL4);

                    string OutProdL1 = tr.Cells[1].InnerHtml != "" ? tr.Cells[1].InnerHtml : "0";
                    string OutProdL2 = tr.Cells[4].InnerHtml != "" ? tr.Cells[4].InnerHtml : "0";
                    string OutProdL3 = tr.Cells[7].InnerHtml != "" ? tr.Cells[7].InnerHtml : "0";
                    string OutProdL4 = tr.Cells[10].InnerHtml != "" ? tr.Cells[10].InnerHtml : "0";

                    //tr.Cells[1].InnerHtml = Convert.ToDecimal(OutProdL1).ToString("N2");

                    if (DPakaiL1 >= 0 && OutProdL1 != "0,00")
                    {
                        tr.Cells[3].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(OutProdL1))).ToString("N2");
                    }
                    else if (DPakaiL1 == 0 && OutProdL1 == "0,00" || DPakaiL1 >= 0 && OutProdL1 == "0,00")
                    {
                        tr.Cells[3].InnerHtml = "0.00";
                    }

                    if (DPakaiL2 >= 0 && OutProdL2 != "0,00")
                    {
                        tr.Cells[6].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(OutProdL2))).ToString("N2");
                    }
                    else if (DPakaiL2 == 0 || OutProdL2 == "0,00")
                    {
                        tr.Cells[6].InnerHtml = "0.00";
                    }

                    if (DPakaiL3 >= 0 && OutProdL3 != "0,00")
                    { tr.Cells[9].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(OutProdL3))).ToString("N2"); }
                    else if (DPakaiL3 == 0 && OutProdL3 == "0,00" || DPakaiL3 >= 0 && OutProdL3 == "0,00")
                    { tr.Cells[9].InnerHtml = "0.00"; }

                    if (DPakaiL4 >= 0 && OutProdL4 != "0,00")
                    { tr.Cells[12].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(OutProdL4))).ToString("N2"); }
                    else if (DPakaiL4 == 0 && OutProdL4 == "0,00" || DPakaiL4 >= 0 && OutProdL4 == "0,00")
                    { tr.Cells[12].InnerHtml = "0.00"; }

                    LabelStatus.Visible = true;
                    LabelStatus.Text = "Status Laporan : Open";
                    LabelStatus.Attributes["style"] = "color:green; font-weight:bold;";
                    LabelStatus.Font.Name = "calibri";
                }

                #endregion

                totalOutL1 += tr.Cells[1].InnerHtml != "" ? decimal.Parse(tr.Cells[1].InnerHtml) : 0;
                totalSPBL1 += tr.Cells[2].InnerHtml != "" ? decimal.Parse(tr.Cells[2].InnerHtml) : 0;
                totalEfesiensiL1 += tr.Cells[3].InnerHtml != "&nbsp;&nbsp;0,00" ? decimal.Parse(tr.Cells[3].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                totalOutL2 += tr.Cells[4].InnerHtml != "" ? decimal.Parse(tr.Cells[4].InnerHtml) : 0;
                totalSPBL2 += tr.Cells[5].InnerHtml != "" ? decimal.Parse(tr.Cells[5].InnerHtml) : 0;
                totalEfesiensiL2 += tr.Cells[6].InnerHtml != "&nbsp;&nbsp;0,00" ? decimal.Parse(tr.Cells[6].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                totalOutL3 += tr.Cells[7].InnerHtml != "" ? decimal.Parse(tr.Cells[7].InnerHtml) : 0;
                totalSPBL3 += tr.Cells[8].InnerHtml != "" ? decimal.Parse(tr.Cells[8].InnerHtml) : 0;
                totalEfesiensiL3 += tr.Cells[9].InnerHtml != "&nbsp;&nbsp;0,00" ? decimal.Parse(tr.Cells[9].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                totalOutL4 += tr.Cells[10].InnerHtml != "" ? decimal.Parse(tr.Cells[10].InnerHtml) : 0;
                totalSPBL4 += tr.Cells[11].InnerHtml != "" ? decimal.Parse(tr.Cells[11].InnerHtml) : 0;
                totalEfesiensiL4 += tr.Cells[12].InnerHtml != "&nbsp;&nbsp;0,00" ? decimal.Parse(tr.Cells[12].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
                trfooter.Cells[0].InnerHtml = "&nbsp;&nbsp;" + "<b> TOTAL </b>";

                trfooter.Cells[1].InnerHtml = totalOutL1.ToString("N2");
                trfooter.Cells[2].InnerHtml = totalSPBL1.ToString("N2");
                //trfooter.Cells[3].InnerHtml = ((Convert.ToDecimal(totalSPBL1).ToString("N2") != "0,00") && (Convert.ToDecimal(totalOutL1).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBL1) / Convert.ToDecimal(totalOutL1)).ToString("N2") : "0.00";
                if (totalSPBL1 == 0 || totalSPBL1 > 0 && totalOutL1 == 0)
                {
                    trfooter.Cells[3].InnerHtml = "0.00";
                }
                else if (totalSPBL1 == 0 && totalOutL1 > 0 || totalSPBL1 > 0 && totalOutL1 > 0)
                {
                    trfooter.Cells[3].InnerHtml = (Convert.ToDecimal(totalSPBL1) / Convert.ToDecimal(totalOutL1)).ToString("N2");
                }

                trfooter.Cells[4].InnerHtml = totalOutL2.ToString("N2");
                trfooter.Cells[5].InnerHtml = totalSPBL2.ToString("N2");
                if (totalSPBL2 == 0 || totalSPBL2 > 0 && totalOutL2 == 0)
                {
                    trfooter.Cells[6].InnerHtml = "0.00";
                }
                else if (totalSPBL2 == 0 && totalOutL2 > 0 || totalSPBL2 > 0 && totalOutL2 > 0)
                {
                    trfooter.Cells[6].InnerHtml = (Convert.ToDecimal(totalSPBL2) / Convert.ToDecimal(totalOutL2)).ToString("N2");
                }



                trfooter.Cells[7].InnerHtml = totalOutL3.ToString("N2");
                trfooter.Cells[8].InnerHtml = totalSPBL3.ToString("N2");
                //trfooter.Cells[9].InnerHtml = ((Convert.ToDecimal(totalSPBL3).ToString("N2") != "0,00") && (Convert.ToDecimal(totalOutL3).ToString("N2") != "0.00")) ? (Convert.ToDecimal(totalSPBL3) / Convert.ToDecimal(totalOutL3)).ToString("N2") : "0.00";
                if (totalSPBL3 == 0 || totalSPBL3 > 0 && totalOutL3 == 0)
                {
                    trfooter.Cells[9].InnerHtml = "0.00";
                }
                else if (totalSPBL3 == 0 && totalOutL3 > 0 || totalSPBL3 > 0 && totalOutL3 > 0)
                {
                    trfooter.Cells[9].InnerHtml = (Convert.ToDecimal(totalSPBL3) / Convert.ToDecimal(totalOutL3)).ToString("N2");
                }

                trfooter.Cells[10].InnerHtml = totalOutL4.ToString("N2");
                trfooter.Cells[11].InnerHtml = totalSPBL4.ToString("N2");
                //trfooter.Cells[12].InnerHtml = ((Convert.ToDecimal(totalSPBL4).ToString("N2") != "0,00") && (Convert.ToDecimal(totalOutL4).ToString("N2") != "0.00")) ? (Convert.ToDecimal(totalSPBL4) / Convert.ToDecimal(totalOutL4)).ToString("N2") : "0.00";
                if (totalSPBL4 == 0 || totalSPBL4 > 0 && totalOutL4 == 0)
                {
                    trfooter.Cells[12].InnerHtml = "0.00";
                }
                else if (totalSPBL4 == 0 && totalOutL4 > 0 || totalSPBL4 > 0 && totalOutL4 > 0)
                {
                    trfooter.Cells[12].InnerHtml = (Convert.ToDecimal(totalSPBL4) / Convert.ToDecimal(totalOutL4)).ToString("N2");
                }
            }
            #endregion

            string TglAlias = Session["Tgl"].ToString();
            DomainBMReportMMS DS = new DomainBMReportMMS();
            FacadeBMReportMMS FS = new FacadeBMReportMMS();
            DS = FS.RetrieveSign(user.UnitKerjaID);
            Session["admin"] = DS.AdminSign;
            Session["manager"] = DS.MgrSign;
        }
        public decimal totalOutLA1 = 0;
        public decimal totalOutLA2 = 0;
        public decimal totalOutLA3 = 0;
        public decimal totalOutLA4 = 0;
        public decimal totalOutLA5 = 0;
        public decimal totalOutLA6 = 0;
        public decimal totalSPBLA1 = 0;
        public decimal totalSPBLA2 = 0;
        public decimal totalSPBLA3 = 0;
        public decimal totalSPBLA4 = 0;
        public decimal totalSPBLA5 = 0;
        public decimal totalSPBLA6 = 0;

        protected void lstMatrixK2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            decimal totalOutL1 = 0; decimal totalSPBL1 = 0; decimal totalEfesiensiL1 = 0;
            decimal totalOutL2 = 0; decimal totalSPBL2 = 0; decimal totalEfesiensiL2 = 0;
            decimal totalOutL3 = 0; decimal totalSPBL3 = 0; decimal totalEfesiensiL3 = 0;
            decimal totalOutL4 = 0; decimal totalSPBL4 = 0; decimal totalEfesiensiL4 = 0;
            decimal totalOutL5 = 0; decimal totalSPBL5 = 0; decimal totalEfesiensiL5 = 0;
            decimal totalOutL6 = 0; decimal totalSPBL6 = 0; decimal totalEfesiensiL6 = 0;
            Users user = (Users)Session["Users"];

            //for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            //{           
            Label txtKet = (Label)e.Item.FindControl("txtKeterangan");
            TextBox Ket = (TextBox)e.Item.FindControl("Keterangan");

            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lstK2");
            string tgl = tr.Cells[0].InnerHtml;
            DomainBMReportMMS bm = new DomainBMReportMMS();
            FacadeBMReportMMS fbm = new FacadeBMReportMMS();
            bm = fbm.Retrievetgl(tgl);

            if (bm.ID > 0)
            {
                Ket.Text = bm.Keterangan.Trim();
                txtKet.Text = Ket.Text;
                LabelStatus.Visible = true; LabelStatus.Attributes["style"] = "color:blue; font-weight:bold;"; LabelStatus.Font.Name = "calibri";
                LabelStatus.Text = "Status Laporan : Release";

                //tr.Cells[21].Attributes.Add("style", "background-color:White;");
            }
            #region Belum ada inputan
            else if (bm.ID == 0)
            {
                string PakaiL1 = tr.Cells[2].InnerHtml != "" ? tr.Cells[2].InnerHtml : "0"; decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
                string PakaiL2 = tr.Cells[5].InnerHtml != "" ? tr.Cells[5].InnerHtml : "0"; decimal DPakaiL2 = Convert.ToDecimal(PakaiL2);
                string PakaiL3 = tr.Cells[8].InnerHtml != "" ? tr.Cells[8].InnerHtml : "0"; decimal DPakaiL3 = Convert.ToDecimal(PakaiL3);
                string PakaiL4 = tr.Cells[11].InnerHtml != "" ? tr.Cells[11].InnerHtml : "0"; decimal DPakaiL4 = Convert.ToDecimal(PakaiL4);
                string PakaiL5 = tr.Cells[14].InnerHtml != "" ? tr.Cells[14].InnerHtml : "0"; decimal DPakaiL5 = Convert.ToDecimal(PakaiL5);
                string PakaiL6 = tr.Cells[17].InnerHtml != "" ? tr.Cells[17].InnerHtml : "0"; decimal DPakaiL6 = Convert.ToDecimal(PakaiL6);

                string OutProdL1 = tr.Cells[1].InnerHtml != "" ? tr.Cells[1].InnerHtml : "0";
                string OutProdL2 = tr.Cells[4].InnerHtml != "" ? tr.Cells[4].InnerHtml : "0";
                string OutProdL3 = tr.Cells[7].InnerHtml != "" ? tr.Cells[7].InnerHtml : "0";
                string OutProdL4 = tr.Cells[10].InnerHtml != "" ? tr.Cells[10].InnerHtml : "0";
                string OutProdL5 = tr.Cells[13].InnerHtml != "" ? tr.Cells[13].InnerHtml : "0";
                string OutProdL6 = tr.Cells[16].InnerHtml != "" ? tr.Cells[16].InnerHtml : "0";

                if (DPakaiL1 >= 0 && Convert.ToDecimal(OutProdL1) != 0)
                { tr.Cells[3].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(OutProdL1))).ToString("N2"); }
                else if (DPakaiL1 == 0 && OutProdL1 == "0" || DPakaiL1 >= 0 && OutProdL1 == "0")
                { tr.Cells[3].InnerHtml = "0.00"; }

                if (DPakaiL2 >= 0 && Convert.ToDecimal(OutProdL2) != 0)
                { tr.Cells[6].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(OutProdL2))).ToString("N2"); }
                else if (DPakaiL2 == 0 && OutProdL2 == "0" || DPakaiL2 >= 0 && OutProdL2 == "0")
                { tr.Cells[6].InnerHtml = "0.00"; }

                if (DPakaiL3 >= 0 && Convert.ToDecimal(OutProdL3) != 0)
                { tr.Cells[9].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(OutProdL3))).ToString("N2"); }
                else if (DPakaiL3 == 0 && OutProdL3 == "0" || DPakaiL3 >= 0 && OutProdL3 == "0")
                { tr.Cells[9].InnerHtml = "0.00"; }

                if (DPakaiL4 >= 0 && Convert.ToDecimal(OutProdL4) != 0)
                { tr.Cells[12].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(OutProdL4))).ToString("N2"); }
                else if (DPakaiL4 == 0 && OutProdL4 == "0" || DPakaiL4 >= 0 && OutProdL4 == "0")
                { tr.Cells[12].InnerHtml = "0.00"; }

                if (DPakaiL5 >= 0 && Convert.ToDecimal(OutProdL5) != 0)
                { tr.Cells[15].InnerHtml = ((Convert.ToDecimal(DPakaiL5) / decimal.Parse(OutProdL5))).ToString("N2"); }
                else if (DPakaiL5 == 0 && OutProdL5 == "0" || DPakaiL5 >= 0 && OutProdL5 == "0")
                { tr.Cells[15].InnerHtml = "0.00"; }

                if (DPakaiL6 >= 0 && Convert.ToDecimal(OutProdL6) != 0)
                { tr.Cells[18].InnerHtml = ((Convert.ToDecimal(DPakaiL6) / decimal.Parse(OutProdL6))).ToString("N2"); }
                else if (DPakaiL6 == 0 && OutProdL6 == "0" || DPakaiL6 >= 0 && OutProdL6 == "0")
                { tr.Cells[18].InnerHtml = "0.00"; }

                LabelStatus.Visible = true;
                LabelStatus.Text = "Status Laporan : Open";
                LabelStatus.Attributes["style"] = "color:green; font-weight:bold;";
                LabelStatus.Font.Name = "calibri";
            }
            #endregion

            totalOutL1 += tr.Cells[1].InnerHtml != "" ? decimal.Parse(tr.Cells[1].InnerHtml) : 0;
            totalSPBL1 += tr.Cells[2].InnerHtml != "" ? decimal.Parse(tr.Cells[2].InnerHtml) : 0;
            totalEfesiensiL1 += tr.Cells[3].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[3].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;
            totalOutLA1 = totalOutLA1 + totalOutL1;
            totalSPBLA1 = totalSPBLA1 + totalSPBL1;

            totalOutL2 += tr.Cells[4].InnerHtml != "" ? decimal.Parse(tr.Cells[4].InnerHtml) : 0;
            totalSPBL2 += tr.Cells[5].InnerHtml != "" ? decimal.Parse(tr.Cells[5].InnerHtml) : 0;
            totalEfesiensiL2 += tr.Cells[6].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[6].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;
            totalOutLA2 = totalOutLA2 + totalOutL2;
            totalSPBLA2 = totalSPBLA2 + totalSPBL2;


            totalOutL3 += tr.Cells[7].InnerHtml != "" ? decimal.Parse(tr.Cells[7].InnerHtml) : 0;
            totalSPBL3 += tr.Cells[8].InnerHtml != "" ? decimal.Parse(tr.Cells[8].InnerHtml) : 0;
            totalEfesiensiL3 += tr.Cells[9].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[9].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;
            totalOutLA3 = totalOutLA3 + totalOutL3;
            totalSPBLA3 = totalSPBLA3 + totalSPBL3;

            totalOutL4 += tr.Cells[10].InnerHtml != "" ? decimal.Parse(tr.Cells[10].InnerHtml) : 0;
            totalSPBL4 += tr.Cells[11].InnerHtml != "" ? decimal.Parse(tr.Cells[11].InnerHtml) : 0;
            totalEfesiensiL4 += tr.Cells[12].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[12].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;
            totalOutLA4 = totalOutLA4 + totalOutL4;
            totalSPBLA4 = totalSPBLA4 + totalSPBL4;

            totalOutL5 += tr.Cells[13].InnerHtml != "" ? decimal.Parse(tr.Cells[13].InnerHtml) : 0;
            totalSPBL5 += tr.Cells[14].InnerHtml != "" ? decimal.Parse(tr.Cells[14].InnerHtml) : 0;
            totalEfesiensiL5 += tr.Cells[15].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[15].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;
            totalOutLA5 = totalOutLA5 + totalOutL5;
            totalSPBLA5 = totalSPBLA5 + totalSPBL5;

            totalOutL6 += tr.Cells[16].InnerHtml != "" ? decimal.Parse(tr.Cells[16].InnerHtml) : 0;
            totalSPBL6 += tr.Cells[17].InnerHtml != "" ? decimal.Parse(tr.Cells[17].InnerHtml) : 0;
            totalEfesiensiL6 += tr.Cells[18].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[18].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;
            totalOutLA6 = totalOutLA6 + totalOutL6;
            totalSPBLA6 = totalSPBLA6 + totalSPBL6;

            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
            trfooter.Cells[0].InnerHtml = "&nbsp;&nbsp;" + "<b> TOTAL </b>";

            trfooter.Cells[1].InnerHtml = totalOutLA1.ToString("N2");
            trfooter.Cells[2].InnerHtml = totalSPBLA1.ToString("N0");
            if (totalOutLA1 > 0)
                trfooter.Cells[3].InnerHtml = ((Convert.ToDecimal(totalSPBLA1).ToString("N2") != "0,00") &&
                    (Convert.ToDecimal(totalOutLA1).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBLA1) /
                    Convert.ToDecimal(totalOutLA1)).ToString("N2") : "0.00";
            else
                trfooter.Cells[3].InnerHtml = "0.00";

            trfooter.Cells[4].InnerHtml = totalOutLA2.ToString("N2");
            trfooter.Cells[5].InnerHtml = totalSPBLA2.ToString("N0");
            if (totalOutLA2 > 0)
                trfooter.Cells[6].InnerHtml = ((Convert.ToDecimal(totalSPBLA2).ToString("N2") != "0,00") &&
                    (Convert.ToDecimal(totalOutLA2).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBLA2) /
                    Convert.ToDecimal(totalOutLA2)).ToString("N2") : "0.00";
            else
                trfooter.Cells[6].InnerHtml = "0.00";
            trfooter.Cells[7].InnerHtml = totalOutLA3.ToString("N2");
            trfooter.Cells[8].InnerHtml = totalSPBLA3.ToString("N0");
            if (totalOutLA3 > 0)
                trfooter.Cells[9].InnerHtml = ((Convert.ToDecimal(totalSPBLA3).ToString("N2") != "0,00") &&
                    (Convert.ToDecimal(totalOutLA3).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBLA3) /
                    Convert.ToDecimal(totalOutLA3)).ToString("N2") : "0.00";
            else
                trfooter.Cells[9].InnerHtml = "0.00";
            trfooter.Cells[10].InnerHtml = totalOutLA4.ToString("N2");
            trfooter.Cells[11].InnerHtml = totalSPBLA4.ToString("N0");
            if (totalOutLA4 > 0)
                trfooter.Cells[12].InnerHtml = ((Convert.ToDecimal(totalSPBLA4).ToString("N2") != "0,00") &&
                    (Convert.ToDecimal(totalOutLA4).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBLA4) /
                    Convert.ToDecimal(totalOutLA4)).ToString("N2") : "0.00";
            else
                trfooter.Cells[12].InnerHtml = "0.00";
            trfooter.Cells[13].InnerHtml = totalOutLA5.ToString("N2");
            trfooter.Cells[14].InnerHtml = totalSPBLA5.ToString("N0");
            if (totalOutLA5 > 0)
                trfooter.Cells[15].InnerHtml = ((Convert.ToDecimal(totalSPBLA5).ToString("N2") != "0,00") &&
                    (Convert.ToDecimal(totalOutLA5).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBLA5) /
                    Convert.ToDecimal(totalOutLA5)).ToString("N2") : "0.00";
            else
                trfooter.Cells[15].InnerHtml = "0.00";
            trfooter.Cells[16].InnerHtml = totalOutLA6.ToString("N2");
            trfooter.Cells[17].InnerHtml = totalSPBLA6.ToString("N0");
            if (totalOutLA6 > 0)
                trfooter.Cells[18].InnerHtml = ((Convert.ToDecimal(totalSPBLA6).ToString("N2") != "0,00") &&
                    (Convert.ToDecimal(totalOutLA6).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBLA6) /
                    Convert.ToDecimal(totalOutLA6)).ToString("N2") : "0.00";
            else
                trfooter.Cells[18].InnerHtml = "0.00";

            //}


            string TglAlias = Session["Tgl"].ToString();
            DomainBMReportMMS DS = new DomainBMReportMMS();
            FacadeBMReportMMS FS = new FacadeBMReportMMS();
            DS = FS.RetrieveSign(user.UnitKerjaID);
            Session["admin"] = DS.AdminSign;
            Session["manager"] = DS.MgrSign;
        }

        //protected void QtyPakaiLK1_Change(object sender, EventArgs e)
        //{
        //    decimal totalPakaiBBL1 = 0; decimal totalPPML1 = 0;
        //    for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //    {
        //        HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //        DomainBMReportMMS pj = new DomainBMReportMMS();
        //        TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1");
        //        if (jml.Text != string.Empty)
        //        {
        //            string PakaiL1 = tr.Cells[3].InnerHtml != "" ? tr.Cells[3].InnerHtml : "0";
        //            decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
        //            tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jml.Text)) * 1000000).ToString("N0");
        //        }

        //        totalPakaiBBL1 += jml.Text != "" ? decimal.Parse(jml.Text) : 0;
        //        totalPPML1 += tr.Cells[5].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[5].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //        HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
        //        trfooter.Cells[2].InnerHtml = totalPakaiBBL1.ToString("N0");
        //        trfooter.Cells[5].InnerHtml = totalPPML1.ToString("N0");
        //    }
        //}

        //protected void QtyPakaiL1_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL1 = 0; decimal totalPPML1 = 0;

        //    if (user.UnitKerjaID == 1)
        //    {
        //        for (int i = 0; i < lstMatrix.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
        //            DomainBMReportMMS pj = new DomainBMReportMMS();
        //            TextBox jml = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL1");
        //            if (jml.Text != string.Empty)
        //            {
        //                string PakaiL1 = tr.Cells[3].InnerHtml != "" ? tr.Cells[3].InnerHtml : "0";
        //                decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
        //                tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jml.Text)) * 1000000).ToString("N0");
        //            }

        //            totalPakaiBBL1 += jml.Text != "" ? decimal.Parse(jml.Text) : 0;
        //            totalPPML1 += tr.Cells[5].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[5].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
        //            trfooter.Cells[2].InnerHtml = totalPakaiBBL1.ToString("N0");
        //            trfooter.Cells[5].InnerHtml = totalPPML1.ToString("N0");
        //        }
        //    }
        //    else if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            DomainBMReportMMS pj = new DomainBMReportMMS();
        //            TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1");
        //            if (jml.Text != string.Empty)
        //            {
        //                string PakaiL1 = tr.Cells[3].InnerHtml != "" ? tr.Cells[3].InnerHtml : "0";
        //                decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
        //                tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jml.Text)) * 1000000).ToString("N0");
        //            }

        //            totalPakaiBBL1 += jml.Text != "" ? decimal.Parse(jml.Text) : 0;
        //            totalPPML1 += tr.Cells[5].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[5].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[2].InnerHtml = totalPakaiBBL1.ToString("N0");
        //            trfooter.Cells[5].InnerHtml = totalPPML1.ToString("N0");
        //        }
        //    }
        //}
        //protected void QtyPakaiL2_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL2 = 0; decimal totalPPML2 = 0;

        //    if (user.UnitKerjaID == 1)
        //    {
        //        for (int i = 0; i < lstMatrix.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
        //            TextBox jmlL2 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL2");
        //            if (jmlL2.Text != string.Empty)
        //            {
        //                string PakaiL2 = tr.Cells[8].InnerHtml != "" ? tr.Cells[8].InnerHtml : "0";
        //                decimal DPakaiL2 = Convert.ToDecimal(PakaiL2);
        //                tr.Cells[10].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(jmlL2.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL2 += jmlL2.Text != "" ? decimal.Parse(jmlL2.Text) : 0;
        //            totalPPML2 += tr.Cells[10].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[10].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
        //            trfooter.Cells[7].InnerHtml = totalPakaiBBL2.ToString("N0");
        //            trfooter.Cells[10].InnerHtml = totalPPML2.ToString("N0");
        //        }
        //    }
        //    else if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            TextBox jmlL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2");
        //            if (jmlL2.Text != string.Empty)
        //            {
        //                string PakaiL2 = tr.Cells[8].InnerHtml != "" ? tr.Cells[8].InnerHtml : "0";
        //                decimal DPakaiL2 = Convert.ToDecimal(PakaiL2);
        //                tr.Cells[10].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(jmlL2.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL2 += jmlL2.Text != "" ? decimal.Parse(jmlL2.Text) : 0;
        //            totalPPML2 += tr.Cells[10].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[10].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[7].InnerHtml = totalPakaiBBL2.ToString("N0");
        //            trfooter.Cells[10].InnerHtml = totalPPML2.ToString("N0");
        //        }
        //    }

        //}
        //protected void QtyPakaiL3_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL3 = 0; decimal totalPPML3 = 0;

        //    if (user.UnitKerjaID == 1)
        //    {
        //        for (int i = 0; i < lstMatrix.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
        //            TextBox jmlL3 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL3");
        //            if (jmlL3.Text != string.Empty)
        //            {
        //                string PakaiL3 = tr.Cells[13].InnerHtml != "" ? tr.Cells[13].InnerHtml : "0";
        //                decimal DPakaiL3 = Convert.ToDecimal(PakaiL3);
        //                tr.Cells[15].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(jmlL3.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL3 += jmlL3.Text != "" ? decimal.Parse(jmlL3.Text) : 0;
        //            totalPPML3 += tr.Cells[15].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[15].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
        //            trfooter.Cells[12].InnerHtml = totalPakaiBBL3.ToString("N0");
        //            trfooter.Cells[15].InnerHtml = totalPPML3.ToString("N0");
        //        }
        //    }
        //    else if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            TextBox jmlL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3");
        //            if (jmlL3.Text != string.Empty)
        //            {
        //                string PakaiL3 = tr.Cells[13].InnerHtml != "" ? tr.Cells[13].InnerHtml : "0";
        //                decimal DPakaiL3 = Convert.ToDecimal(PakaiL3);
        //                tr.Cells[15].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(jmlL3.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL3 += jmlL3.Text != "" ? decimal.Parse(jmlL3.Text) : 0;
        //            totalPPML3 += tr.Cells[15].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[15].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[12].InnerHtml = totalPakaiBBL3.ToString("N0");
        //            trfooter.Cells[15].InnerHtml = totalPPML3.ToString("N0");
        //        }
        //    }
        //}
        //protected void QtyPakaiL4_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL4 = 0; decimal totalPPML4 = 0;

        //    if (user.UnitKerjaID == 1)
        //    {
        //        for (int i = 0; i < lstMatrix.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
        //            TextBox jmlL4 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL4");
        //            if (jmlL4.Text != string.Empty)
        //            {
        //                string PakaiL4 = tr.Cells[18].InnerHtml != "" ? tr.Cells[18].InnerHtml : "0";
        //                decimal DPakaiL4 = Convert.ToDecimal(PakaiL4);
        //                tr.Cells[20].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(jmlL4.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL4 += jmlL4.Text != "" ? decimal.Parse(jmlL4.Text) : 0;
        //            totalPPML4 += tr.Cells[20].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[20].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
        //            trfooter.Cells[17].InnerHtml = totalPakaiBBL4.ToString("N0");
        //            trfooter.Cells[20].InnerHtml = totalPPML4.ToString("N0");
        //        }
        //    }
        //    else if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            TextBox jmlL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4");
        //            if (jmlL4.Text != string.Empty)
        //            {
        //                string PakaiL4 = tr.Cells[18].InnerHtml != "" ? tr.Cells[18].InnerHtml : "0";
        //                decimal DPakaiL4 = Convert.ToDecimal(PakaiL4);
        //                tr.Cells[20].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(jmlL4.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL4 += jmlL4.Text != "" ? decimal.Parse(jmlL4.Text) : 0;
        //            totalPPML4 += tr.Cells[20].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[20].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[17].InnerHtml = totalPakaiBBL4.ToString("N0");
        //            trfooter.Cells[20].InnerHtml = totalPPML4.ToString("N0");
        //        }
        //    }
        //}
        //protected void QtyPakaiL5_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL5 = 0; decimal totalPPML5 = 0;

        //    if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            TextBox jmlL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5");
        //            if (jmlL5.Text != string.Empty)
        //            {
        //                string PakaiL5 = tr.Cells[23].InnerHtml != "" ? tr.Cells[23].InnerHtml : "0";
        //                decimal DPakaiL5 = Convert.ToDecimal(PakaiL5);
        //                tr.Cells[25].InnerHtml = ((Convert.ToDecimal(DPakaiL5) / decimal.Parse(jmlL5.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL5 += jmlL5.Text != "" ? decimal.Parse(jmlL5.Text) : 0;
        //            totalPPML5 += tr.Cells[25].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[25].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[22].InnerHtml = totalPakaiBBL5.ToString("N0");
        //            trfooter.Cells[25].InnerHtml = totalPPML5.ToString("N0");
        //        }
        //    }        
        //}
        //protected void QtyPakaiL6_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL6 = 0; decimal totalPPML6 = 0;

        //    if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            TextBox jmlL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6");
        //            if (jmlL6.Text != string.Empty)
        //            {
        //                string PakaiL6 = tr.Cells[28].InnerHtml != "" ? tr.Cells[28].InnerHtml : "0";
        //                decimal DPakaiL6 = Convert.ToDecimal(PakaiL6);
        //                tr.Cells[30].InnerHtml = ((Convert.ToDecimal(DPakaiL6) / decimal.Parse(jmlL6.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL6 += jmlL6.Text != "" ? decimal.Parse(jmlL6.Text) : 0;
        //            totalPPML6 += tr.Cells[30].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[30].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[27].InnerHtml = totalPakaiBBL6.ToString("N0");
        //            trfooter.Cells[30].InnerHtml = totalPPML6.ToString("N0");
        //        }
        //    }
        //}

        //protected void Keterangan_Change(object sender, EventArgs e)
        //{ }
        protected void lstMatrix_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void lstMatrixK2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }

    public class FacadeBMReportMMS
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private DomainBMReportMMS objBM = new DomainBMReportMMS();

        public FacadeBMReportMMS()
                : base()
        {

        }
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int insertPMMS(object objDomain)
        {
            try
            {
                objBM = (DomainBMReportMMS)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@Tanggal", objBM.Tanggal));
                sqlListParam.Add(new SqlParameter("@Tanggal2", objBM.Tanggal2));
                sqlListParam.Add(new SqlParameter("@Keterangan", objBM.Keterangan));

                sqlListParam.Add(new SqlParameter("@OutM3L1", objBM.OutM3L1));
                sqlListParam.Add(new SqlParameter("@QtySPBL1", objBM.QtySPBL1));
                sqlListParam.Add(new SqlParameter("@EfesiensiL1", objBM.EfesiensiL1));

                sqlListParam.Add(new SqlParameter("@OutM3L2", objBM.OutM3L2));
                sqlListParam.Add(new SqlParameter("@QtySPBL2", objBM.QtySPBL2));
                sqlListParam.Add(new SqlParameter("@EfesiensiL2", objBM.EfesiensiL2));

                sqlListParam.Add(new SqlParameter("@OutM3L3", objBM.OutM3L3));
                sqlListParam.Add(new SqlParameter("@QtySPBL3", objBM.QtySPBL3));
                sqlListParam.Add(new SqlParameter("@EfesiensiL3", objBM.EfesiensiL3));

                sqlListParam.Add(new SqlParameter("@OutM3L4", objBM.OutM3L4));
                sqlListParam.Add(new SqlParameter("@QtySPBL4", objBM.QtySPBL4));
                sqlListParam.Add(new SqlParameter("@EfesiensiL4", objBM.EfesiensiL4));

                sqlListParam.Add(new SqlParameter("@OutM3L5", objBM.OutM3L5));
                sqlListParam.Add(new SqlParameter("@QtySPBL5", objBM.QtySPBL5));
                sqlListParam.Add(new SqlParameter("@EfesiensiL5", objBM.EfesiensiL5));

                sqlListParam.Add(new SqlParameter("@OutM3L6", objBM.OutM3L6));
                sqlListParam.Add(new SqlParameter("@QtySPBL6", objBM.QtySPBL6));
                sqlListParam.Add(new SqlParameter("@EfesiensiL6", objBM.EfesiensiL6));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "BMAntiFoam_Insert");
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
                objBM = (DomainBMReportMMS)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Periode", objBM.Periode));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBM.LastModifiedBy));           

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "BMAntiFoam_Cancel");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        private string MatrixQuery(string Periode, string Periode2)
        {
            string result =
            #region Lama
        /**
        " declare @date datetime " +
        " set @date = '" + Periode + "'; " +

        " with DaysInMonth as (select @date as Date " +
        " union all " +
        " select dateadd(dd,1,Date) " +
        " from DaysInMonth " +
        " where month(date) = month(@Date)), " +

        " DestackingL1 AS ( select A1.Qty,A1.TglProduksi,((A2.Tebal*A2.Lebar*A2.Panjang)/1000000000)Volume from BM_Destacking A1  " +
        "		            INNER JOIN fc_items A2 ON A1.ItemID=A2.ID " +
        "		            INNER JOIN BM_PlantGroup A3 ON A1.PlantGroupID=A3.ID " +
        "		            where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and A3.PlantID=1 and A1.rowstatus>-1), " +

        " DestackingL2 AS ( select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        "		            INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        "	    	        INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID " +
        "   		        where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=2 and AL1.rowstatus>-1), " +

        " DestackingL3 AS ( select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        "		            INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        "	    	        INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID " +
        "   		        where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=3 and AL1.rowstatus>-1), " +

        " DestackingL4 AS ( select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        "		            INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        "	    	        INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID " +
        "   		        where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=4 and AL1.rowstatus>-1), " +

        " PemakaianL1 AS  ( select B2.Quantity,B1.PakaiDate from Pakai B1 " +
        "                   INNER JOIN PakaiDetail B2 ON B1.ID=B2.PakaiID " +
        "                   where LEFT(convert(char,B1.pakaidate,112),6)='" + Periode2 + "' and B2.ItemID " +
        "                   in (select ID from Inventory where ItemCode='021396001000000') " +
        "                   and B1.Status>1 and B2.ProdLine=1 and B2.rowstatus>-1), " +

        " PemakaianL2 AS  ( select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
        "                   INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
        "                   where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID " +
        "                   in (select ID from Inventory where ItemCode='021396001000000') " +
        "                   and BL1.Status>1 and BL2.ProdLine=2 and BL2.rowstatus>-1), " +

        " PemakaianL3 AS  ( select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
        "                   INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
        "                   where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID " +
        "                   in (select ID from Inventory where ItemCode='021396001000000') " +
        "                   and BL1.Status>1 and BL2.ProdLine=3 and BL2.rowstatus>-1), " +

        " PemakaianL4 AS  ( select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
        "                   INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
        "                   where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID " +
        "                   in (select ID from Inventory where ItemCode='021396001000000') " +
        "                   and BL1.Status>1 and BL2.ProdLine=4 and BL2.rowstatus>-1) " +

        " select " +
        " Tanggal " +
        " ,cast(SUM(OutM3L1) as decimal(10,0)) OutM3L1,SUM(QtySPBL1)QtySPBL1,SUM(EfesiensiL1)EfesiensiL1 " +
        " ,cast(SUM(OutM3L2) as decimal(10,0)) OutM3L2,SUM(QtySPBL2)QtySPBL2,SUM(EfesiensiL2)EfesiensiL2 " +
        " ,cast(SUM(OutM3L3) as decimal(10,0)) OutM3L3,SUM(QtySPBL3)QtySPBL3,SUM(EfesiensiL3)EfesiensiL3 " +
        " ,cast(SUM(OutM3L4) as decimal(10,0)) OutM3L4,SUM(QtySPBL4)QtySPBL4,SUM(EfesiensiL4)EfesiensiL4 " +

        " from   " +

        " (select " +
        " Tanggal " +
        " ,(M3L1)OutM3L1,SUM(QtyPL1) QtySPBL1,SUM(EfesiensiL1)EfesiensiL1 " +
        " ,(M3L2)OutM3L2,SUM(QtyPL2) QtySPBL2,SUM(EfesiensiL2)EfesiensiL2 " +
        " ,(M3L3)OutM3L3,SUM(QtyPL3) QtySPBL3,SUM(EfesiensiL3)EfesiensiL3 " +
        " ,(M3L4)OutM3L4,SUM(QtyPL4) QtySPBL4,SUM(EfesiensiL4)EfesiensiL4 " +

        " from  " +

        " (select " +
        " tanggal " +
        " ,M3L1,QtyPL1,ISNULL(ROUND((QtyPL1/NULLIF(M3L1,0)),2),0)EfesiensiL1 " +
        " ,M3L2,QtyPL2,ISNULL(ROUND((QtyPL2/NULLIF(M3L2,0)),2),0)EfesiensiL2 " +
        " ,M3L3,QtyPL3,ISNULL(ROUND((QtyPL3/NULLIF(M3L3,0)),2),0)EfesiensiL3 " +
        " ,M3L4,QtyPL4,ISNULL(ROUND((QtyPL4/NULLIF(M3L4,0)),2),0)EfesiensiL4 " +

        " from " +

        " (select " +
        " LEFT(convert(char,A.date,106),11)Tanggal " +
        " ,ROUND(ISNULL(sum(B.Qty),0)* ISNULL(B.Volume,0),0) 'M3L1',ISNULL(C.Quantity,0)'QtyPL1' " +
        " ,0'M3L2',0'QtyPL2' " +
        " ,0'M3L3',0'QtyPL3' " +
        " ,0'M3L4',0'QtyPL4' " +
        " from DaysInMonth A  " +
        " LEFT JOIN DestackingL1 B ON A.[Date]=B.TglProduksi " +
        " LEFT JOIN Pemakaianl1 C ON C.PakaiDate=B.TglProduksi " +
        " where month(date) = month(@Date)  group by A.Date,B.Volume,C.Quantity " +

        " UNION ALL " +

        " select " +
        " LEFT(convert(char,A2.date,106),11)Tanggal " +
        " ,0'M3L1',0'QtyPL1' " +
        " ,ROUND(ISNULL(sum(B2.Qty),0)* ISNULL(B2.Volume,0),0) 'M3L2',ISNULL(C2.Quantity,0)'QtyPL2' " +
        " ,0'M3L3',0'QtyPL3' " +
        " ,0'M3L4',0'QtyPL4' " +
        " from DaysInMonth A2 " +
        " LEFT JOIN DestackingL2 B2 ON A2.[Date]=B2.TglProduksi " +
        " LEFT JOIN PemakaianL2 C2 ON C2.PakaiDate=B2.TglProduksi  " +
        " where month(date) = month(@Date)  group by A2.Date,B2.Volume,C2.Quantity " +

        " UNION ALL " +

        " select " +
        " LEFT(convert(char,A3.date,106),11)Tanggal " +
        " ,0'M3L1',0'QtyPL1' " +
        " ,0'M3L2',0'QtyPL2' " +
        " ,ROUND(ISNULL(sum(B3.Qty),0)* ISNULL(B3.Volume,0),0) 'M3L3',ISNULL(C3.Quantity,0)'QtyPL3' " +
        " ,0'M3L4',0'QtyPL4' " +
        " from DaysInMonth A3 " +
        " LEFT JOIN DestackingL3 B3 ON A3.[Date]=B3.TglProduksi " +
        " LEFT JOIN PemakaianL3 C3 ON C3.PakaiDate=B3.TglProduksi  " +
        " where month(date) = month(@Date)  group by A3.Date,B3.Volume,C3.Quantity " +

         " UNION ALL " +

        " select " +
        " LEFT(convert(char,A4.date,106),11)Tanggal " +
        " ,0'M3L1',0'QtyPL1' " +
        " ,0'M3L2',0'QtyPL2' " +
        " ,0'M3L3',0'QtyPL3' " +
        " ,ROUND(ISNULL(sum(B4.Qty),0)* ISNULL(B4.Volume,0),0)'M3L4',ISNULL(C4.Quantity,0)'QtyPL4' " +
        " from DaysInMonth A4 " +
        " LEFT JOIN DestackingL4 B4 ON A4.[Date]=B4.TglProduksi " +
        " LEFT JOIN PemakaianL4 C4 ON C4.PakaiDate=B4.TglProduksi " +
        " where month(date) = month(@Date)  group by A4.Date,B4.Volume,C4.Quantity " +
        " ) as xx group by Tanggal,M3L1,M3L2,M3L3,M3L4,QtyPL1,QtyPL2,QtyPL3,QtyPL4) " +
        " as xx1 group by tanggal,M3L1,M3L2,M3L3,M3L4) as xx2 group by Tanggal " +

        " union all " +

        " select " +
        " ''Tanggal " +
        " ,'0'OutM3L1,'0'QtySPBL1,'0'EfesiensiL1 " +
        " ,'0'OutM3L2,'0'QtySPBL2,'0'EfesiensiL2 " +
        " ,'0'OutM3L3,'0'QtySPBL3,'0'EfesiensiL3 " +
        " ,'0'OutM3L4,'0'QtySPBL4,'0'EfesiensiL4 ";
        **/
            #endregion
            #region Baru
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempMSFoamx]') AND type in (N'U')) DROP TABLE [dbo].[TempMSFoamx] ; " +
            " declare @date datetime " +
            " set @date = '" + Periode + "'; " +

            " with DaysInMonth as (select @date as Date " +
            " union all " +
            " select dateadd(dd,1,Date) " +
            " from DaysInMonth " +
            " where month(date) = month(@Date)), " +

            " DestackingL1 AS ( select A1.Qty,A1.TglProduksi,((A2.Tebal*A2.Lebar*A2.Panjang)/1000000000)Volume from BM_Destacking A1  " +
            "		            INNER JOIN fc_items A2 ON A1.ItemID=A2.ID " +
            "		            INNER JOIN BM_PlantGroup A3 ON A1.PlantGroupID=A3.ID " +
            "		            where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and A3.PlantID=1 and A1.rowstatus>-1), " +

            " DestackingL2 AS ( select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
            "		            INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
            "	    	        INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID " +
            "   		        where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=2 and AL1.rowstatus>-1), " +

            " DestackingL3 AS ( select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
            "		            INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
            "	    	        INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID " +
            "   		        where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=3 and AL1.rowstatus>-1), " +

            " DestackingL4 AS ( select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
            "		            INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
            "	    	        INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID " +
            "   		        where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=4 and AL1.rowstatus>-1), " +

            " PemakaianL1 AS  ( select B2.Quantity,B1.PakaiDate from Pakai B1 " +
            "                   INNER JOIN PakaiDetail B2 ON B1.ID=B2.PakaiID " +
            "                   where LEFT(convert(char,B1.pakaidate,112),6)='" + Periode2 + "' and B2.ItemID " +
            "                   in (select ItemID from BM_PFloculantItem where Keterangan='MMS') " +
            "                   and B1.Status>1 and B2.ProdLine=1 and B2.rowstatus>-1), " +

            " PemakaianL2 AS  ( select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
            "                   INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
            "                   where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID " +
            "                   in (select ItemID from BM_PFloculantItem where Keterangan='MMS') " +
            "                   and BL1.Status>1 and BL2.ProdLine=2 and BL2.rowstatus>-1), " +

            " PemakaianL3 AS  ( select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
            "                   INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
            "                   where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID " +
            "                   in (select ItemID from BM_PFloculantItem where Keterangan='MMS') " +
            "                   and BL1.Status>1 and BL2.ProdLine=3 and BL2.rowstatus>-1), " +

            " PemakaianL4 AS  ( select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
            "                   INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
            "                   where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID " +
            "                   in (select ItemID from BM_PFloculantItem where Keterangan='MMS') " +
            "                   and BL1.Status>1 and BL2.ProdLine=4 and BL2.rowstatus>-1) " +

            " select * into TempMSFoamx from ( " +
            " select " +
            " Tanggal, " +
            " case when M3L1_Dec='.5' then cast(FLOOR(OutM3L1) as decimal(10,2)) else cast(ROUND(OutM3L1,2) as decimal(10,2)) end OutM3L1,QtySPBL1,EfesiensiL1, " +
            " case when M3L2_Dec='.5' then cast(FLOOR(OutM3L2) as decimal(10,2)) else cast(ROUND(OutM3L2,2) as decimal(10,2)) end OutM3L2,QtySPBL2,EfesiensiL2, " +
            " case when M3L3_Dec='.5' then cast(FLOOR(OutM3L3) as decimal(10,2)) else cast(ROUND(OutM3L3,2) as decimal(10,2)) end OutM3L3,QtySPBL3,EfesiensiL3, " +
            " case when M3L4_Dec='.5' then cast(FLOOR(OutM3L4) as decimal(10,2)) else cast(ROUND(OutM3L4,2) as decimal(10,2)) end OutM3L4,QtySPBL4,EfesiensiL4 " +

            " from   " +

            " (select " +
            " Tanggal, " +
            " OutM3L1,cast(QtySPBL1 as decimal(10,1))QtySPBL1,EfesiensiL1,RIGHT(OutM3L1,2)M3L1_Dec, " +
            " OutM3L2,cast(QtySPBL2 as decimal(10,1))QtySPBL2,EfesiensiL2,RIGHT(OutM3L2,2)M3L2_Dec, " +
            " OutM3L3,cast(QtySPBL3 as decimal(10,1))QtySPBL3,EfesiensiL3,RIGHT(OutM3L3,2)M3L3_Dec, " +
            " OutM3L4,cast(QtySPBL4 as decimal(10,1))QtySPBL4,EfesiensiL4,RIGHT(OutM3L4,2)M3L4_Dec " +

            " from  " +

            " (select " +
            " tanggal, " +
            " cast(M3L1 as decimal(10,2))OutM3L1,QtyPL1 QtySPBL1,case when M3L1 = 0 then 0 when QtyPL1 = 0 then 0 else cast((QtyPL1/M3L1) as decimal(10,2)) end EfesiensiL1, " +
            " cast(M3L2 as decimal(10,2))OutM3L2,QtyPL2 QtySPBL2,case when M3L2 = 0 then 0 when QtyPL2 = 0 then 0 else cast((QtyPL2/M3L2) as decimal(10,2)) end EfesiensiL2, " +
            " cast(M3L3 as decimal(10,2))OutM3L3,QtyPL3 QtySPBL3,case when M3L3 = 0 then 0 when QtyPL3 = 0 then 0 else cast((QtyPL3/M3L3) as decimal(10,2)) end EfesiensiL3, " +
            " cast(M3L4 as decimal(10,2))OutM3L4,QtyPL4 QtySPBL4,case when M3L4 = 0 then 0 when QtyPL4 = 0 then 0 else cast((QtyPL4/M3L4) as decimal(10,2)) end EfesiensiL4 " +

            " from " +
            "(select " +
            " Tanggal, " +
            " isnull(sum(M3L1),2)M3L1,isnull(sum(QtyPL1),0)QtyPL1, " +
            " isnull(sum(M3L2),2)M3L2,isnull(sum(QtyPL2),0)QtyPL2, " +
            " isnull(sum(M3L3),2)M3L3,isnull(sum(QtyPL3),0)QtyPL3, " +
            " isnull(sum(M3L4),2)M3L4,isnull(sum(QtyPL4),0)QtyPL4 " +

            " from  " +
            " (select Tanggal, " +
            " cast(sum(M3L1) as decimal(10,2))M3L1,QtyPL1, " +
            " cast(sum(M3L2) as decimal(10,2))M3L2,QtyPL2, " +
            " cast(sum(M3L3) as decimal(10,2))M3L3,QtyPL3, " +
            " cast(sum(M3L4) as decimal(10,2))M3L4,QtyPL4 " +

            " from  " +
            " (select  tanggal, " +
            " M3L1,sum(QtyPL1)QtyPL1, " +
            " M3L2,sum(QtyPL2)QtyPL2, " +
            " M3L3,sum(QtyPL3)QtyPL3, " +
            " M3L4,sum(QtyPL4)QtyPL4 " +


            " from " +

            " (select " +
            " LEFT(convert(char,A.date,106),11)Tanggal " +
            " ,ISNULL(sum(B.Qty)* ISNULL(B.Volume,0),0) 'M3L1',ISNULL(C.Quantity,0)'QtyPL1' " +
            " ,0'M3L2',0'QtyPL2' " +
            " ,0'M3L3',0'QtyPL3' " +
            " ,0'M3L4',0'QtyPL4' " +
            " from DaysInMonth A  " +
            " LEFT JOIN DestackingL1 B ON A.[Date]=B.TglProduksi " +
            " LEFT JOIN Pemakaianl1 C ON C.PakaiDate=A.Date " +
            " where month(date) = month(@Date)  group by A.Date,B.Volume,C.Quantity " +

            " UNION ALL " +

            " select " +
            " LEFT(convert(char,A2.date,106),11)Tanggal " +
            " ,0'M3L1',0'QtyPL1' " +
            " ,ISNULL(sum(B2.Qty)* ISNULL(B2.Volume,0),0) 'M3L2',ISNULL(C2.Quantity,0)'QtyPL2' " +
            " ,0'M3L3',0'QtyPL3' " +
            " ,0'M3L4',0'QtyPL4' " +
            " from DaysInMonth A2 " +
            " LEFT JOIN DestackingL2 B2 ON A2.[Date]=B2.TglProduksi " +
            " LEFT JOIN PemakaianL2 C2 ON C2.PakaiDate=A2.Date  " +
            " where month(date) = month(@Date)  group by A2.Date,B2.Volume,C2.Quantity " +

            " UNION ALL " +

            " select " +
            " LEFT(convert(char,A3.date,106),11)Tanggal " +
            " ,0'M3L1',0'QtyPL1' " +
            " ,0'M3L2',0'QtyPL2' " +
            " ,ISNULL(sum(B3.Qty)* ISNULL(B3.Volume,0),0) 'M3L3',ISNULL(C3.Quantity,0)'QtyPL3' " +
            " ,0'M3L4',0'QtyPL4' " +
            " from DaysInMonth A3 " +
            " LEFT JOIN DestackingL3 B3 ON A3.[Date]=B3.TglProduksi " +
            " LEFT JOIN PemakaianL3 C3 ON C3.PakaiDate=A3.Date  " +
            " where month(date) = month(@Date)  group by A3.Date,B3.Volume,C3.Quantity " +

             " UNION ALL " +

            " select " +
            " LEFT(convert(char,A4.date,106),11)Tanggal " +
            " ,0'M3L1',0'QtyPL1' " +
            " ,0'M3L2',0'QtyPL2' " +
            " ,0'M3L3',0'QtyPL3' " +
            " ,ISNULL(sum(B4.Qty)* ISNULL(B4.Volume,0),0)'M3L4',ISNULL(C4.Quantity,0)'QtyPL4' " +
            " from DaysInMonth A4 " +
            " LEFT JOIN DestackingL4 B4 ON A4.[Date]=B4.TglProduksi " +
            " LEFT JOIN PemakaianL4 C4 ON C4.PakaiDate=A4.Date " +
            " where month(date) = month(@Date)  group by A4.Date,B4.Volume,C4.Quantity " +
            " ) as xx group by Tanggal,M3L1,M3L2,M3L3,M3L4) " +
            " as xx1 group by Tanggal,QtyPL1,QtyPL2,QtyPL3,QtyPL4 ) as xx2 group by Tanggal ) as xx3) as xx4) as xx5 )xx7 order by  Tanggal   " +

            //" union all " +

            //" select " +
            //" ''Tanggal " +
            //" ,'0'OutM3L1,'0'QtySPBL1,'0'EfesiensiL1 " +
            //" ,'0'OutM3L2,'0'QtySPBL2,'0'EfesiensiL2 " +
            //" ,'0'OutM3L3,'0'QtySPBL3,'0'EfesiensiL3 " +
            //" ,'0'OutM3L4,'0'QtySPBL4,'0'EfesiensiL4 ";
            " select * from  ( " +
            " select  'A' urt, * from TempMSFoamx " +
            " union all " +
            " select 'B'urt,''Tanggal, " +
            " 0'OutM3L1' ,0'QtySPBL1' ,0'EfesiensiL1' , " +
            " 0'OutM3L2' ,0'QtySPBL2' ,0'EfesiensiL2' ,  " +
            " 0'OutM3L3' ,0'QtySPBL3' ,0'EfesiensiL3' , " +
            " 0'OutM3L4' ,0'QtySPBL4' ,0'EfesiensiL4' " +
            " ) xx8 order by urt ";

            #endregion
            return result;
        }
        private string MatrixQueryKarawang(string Periode, string Periode2)
        {
            string result =
            #region remark
            /*
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempMSFoamx]') AND type in (N'U')) DROP TABLE [dbo].[TempMSFoamx]; " +

        " declare @date datetime  set @date = '" + Periode + "'; " +
        " with DaysInMonth as ( " +
        " select @date as Date " +
        " union all " +
        "  select dateadd(dd,1,Date)  from DaysInMonth  where month(date) = month(@Date)), " +

        " DestackingL1 AS ( " +
        " select A1.Qty,A1.TglProduksi,((A2.Tebal*A2.Lebar*A2.Panjang)/1000000000)Volume from BM_Destacking A1 " +
        " INNER JOIN fc_items A2 ON A1.ItemID=A2.ID " +
        " INNER JOIN BM_PlantGroup A3 ON A1.PlantGroupID=A3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and A3.PlantID=1 and A1.rowstatus>-1), " +

        " DestackingL2 AS ( " +
        " select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        " INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        " INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=2 and AL1.rowstatus>-1), " +

        " DestackingL3 AS ( " +
        " select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        " INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        " INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=3 and AL1.rowstatus>-1), " +

        " DestackingL4 AS ( " +
        " select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        " INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        " INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=4 and AL1.rowstatus>-1), " +

        " DestackingL5 AS ( " +
        " select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        " INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        " INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=5 and AL1.rowstatus>-1), " +

        " DestackingL6 AS ( " +
        " select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        " INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        " INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID " +
        " where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=6 and AL1.rowstatus>-1), " +

        " PemakaianL1 AS  ( " +
        " select B2.Quantity,B1.PakaiDate from Pakai B1 " +
        " INNER JOIN PakaiDetail B2 ON B1.ID=B2.PakaiID where " +
        " LEFT(convert(char,B1.pakaidate,112),6)='" + Periode2 + "' and B2.ItemID in " +
        " (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and B2.rowstatus>-1) " +
        " and B1.Status>1 and B2.ProdLine=1), " +

        " PemakaianL2 AS  ( " +
        " select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
        " INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
        " (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) " +
        " and BL1.Status>1 and BL2.ProdLine=2), " +

        " PemakaianL3 AS ( " +
        " select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
        " INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
        " (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) " +
        " and BL1.Status>1 and BL2.ProdLine=3), " +

        " PemakaianL4 AS  ( " +
        " select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
        " INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
        " (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) and BL1.Status>1 and BL2.ProdLine=4), " +

        " PemakaianL5 AS  ( " +
        " select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
        " INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
        " (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) and BL1.Status>1 and BL2.ProdLine=5), " +

        " PemakaianL6 AS  ( " +
        " select BL2.Quantity,BL1.PakaiDate from Pakai BL1 " +
        " INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
        " (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) and BL1.Status>1 and BL2.ProdLine=6) " +

        " select * into TempMSFoamx " +

        " from ( " +

        " select Tanggal, " +
        " OutM3L1,QtySPBL1,EfesiensiL1, " +
        " OutM3L2,QtySPBL2,EfesiensiL2, " +
        " OutM3L3,QtySPBL3,EfesiensiL3, " +
        " OutM3L4,QtySPBL4,EfesiensiL4, " +
        " OutM3L5,QtySPBL5,EfesiensiL5, " +
        " OutM3L6,QtySPBL6,EfesiensiL6 " +

        " from ( " +

        " select Tanggal, " +
        " SUM(M3L1)OutM3L1,SUM(QtyPL1)QtySPBL1,SUM(EfesiensiL1)EfesiensiL1 , " +
        " SUM(M3L2)OutM3L2,SUM(QtyPL2)QtySPBL2,SUM(EfesiensiL2)EfesiensiL2 , " +
        " SUM(M3L3)OutM3L3,SUM(QtyPL3)QtySPBL3,SUM(EfesiensiL3)EfesiensiL3 , " +
        " SUM(M3L4)OutM3L4,SUM(QtyPL4)QtySPBL4,SUM(EfesiensiL4)EfesiensiL4 , " +
        " SUM(M3L5)OutM3L5,SUM(QtyPL5)QtySPBL5,SUM(EfesiensiL5)EfesiensiL5 , " +
        " SUM(M3L6)OutM3L6,SUM(QtyPL6)QtySPBL6,SUM(EfesiensiL6)EfesiensiL6 " +

        " from ( " +
        " select tanggal, " +
        " M3L1,QtyPL1,ISNULL(ROUND((QtyPL1/NULLIF(M3L1,0)),2),0)EfesiensiL1 , " +
        " M3L2,QtyPL2,ISNULL(ROUND((QtyPL2/NULLIF(M3L2,0)),2),0)EfesiensiL2 , " +
        " M3L3,QtyPL3,ISNULL(ROUND((QtyPL3/NULLIF(M3L3,0)),2),0)EfesiensiL3 , " +
        " M3L4,QtyPL4,ISNULL(ROUND((QtyPL4/NULLIF(M3L4,0)),2),0)EfesiensiL4 , " +
        " M3L5,QtyPL5,ISNULL(ROUND((QtyPL5/NULLIF(M3L5,0)),2),0)EfesiensiL5 ,	" +
        " M3L6,QtyPL6,ISNULL(ROUND((QtyPL6/NULLIF(M3L6,0)),2),0)EfesiensiL6 " +

        " from ( " +
        " select Tanggal, " +
        " sum(M3L1)M3L1,avg(QtyPL1)QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L4,QtyPL4,M3L5,QtyPL5,M3L6,QtyPL6 " +

        " from ( " +

        " select LEFT(convert(char,A.date,106),11)Tanggal, " +
        " ROUND(ISNULL(sum(B.Qty),0)* ISNULL(B.Volume,0),5) 'M3L1',ISNULL(C.Quantity,0)'QtyPL1', " +
        " 0'M3L2',0'QtyPL2' ,0'M3L3',0'QtyPL3' ,0'M3L4',0'QtyPL4' ,0'M3L5',0'QtyPL5' ,0'M3L6',0'QtyPL6' " +

        " from DaysInMonth A " +
        " LEFT JOIN DestackingL1 B ON convert(char,A.[Date],112)=convert(char,B.TglProduksi,112) " +
        " LEFT JOIN Pemakaianl1 C ON convert(char,C.PakaiDate,112)=convert(char,B.TglProduksi,112) where month(date) = month(@Date) " +
        " group by A.Date,B.Volume,C.Quantity)ds group by Tanggal,M3L4,QtyPL4,M3L2,QtyPL2,M3L3,QtyPL3,M3L5,QtyPL5,M3L6,QtyPL6 " +

        " UNION ALL " +

        " select Tanggal, " +
        " M3L1,QtyPL1,sum(M3L2)M3L2,avg(QtyPL2)QtyPL2,M3L3,QtyPL3,M3L4,QtyPL4,M3L5,QtyPL5,M3L6,QtyPL6 " +

        " from ( " +

        " select LEFT(convert(char,A2.date,106),11)Tanggal, " +
        " 0'M3L1',0'QtyPL1', " +
        " ROUND(ISNULL(sum(B2.Qty),0)* ISNULL(B2.Volume,0),5) 'M3L2',ISNULL(C2.Quantity,0)'QtyPL2', " +
        " 0'M3L3',0'QtyPL3' ,0'M3L4',0'QtyPL4' ,0'M3L5',0'QtyPL5' ,0'M3L6',0'QtyPL6' " +

        " from DaysInMonth A2 " +
        " LEFT JOIN DestackingL2 B2 ON convert(char,A2.[Date],112)=convert(char,B2.TglProduksi,112) " +
        " LEFT JOIN PemakaianL2 C2 ON convert(char,C2.PakaiDate,112)=convert(char,B2.TglProduksi,112) " +
        " where month(date) = month(@Date)  group by A2.Date,B2.Volume,C2.Quantity )ds group by Tanggal, " +
        " M3L4,QtyPL4,M3L1,QtyPL1,M3L3,QtyPL3,M3L5,QtyPL5,M3L6,QtyPL6 " +

        " UNION ALL " +

        " select Tanggal,M3L1,QtyPL1,M3L2,QtyPL2,sum(M3L3)M3L3,avg(QtyPL3)QtyPL3,M3L4,QtyPL4,M3L5,QtyPL5,M3L6,QtyPL6 " +

         " from ( " +
         " select LEFT(convert(char,A3.date,106),11)Tanggal, " +
         " 0'M3L1',0'QtyPL1' ,0'M3L2',0'QtyPL2', " +
         " ROUND(ISNULL(sum(B3.Qty),0)* ISNULL(B3.Volume,0),5) 'M3L3',ISNULL(C3.Quantity,0)'QtyPL3', " +
         " 0'M3L4',0'QtyPL4' ,0'M3L5',0'QtyPL5' ,0'M3L6',0'QtyPL6' " +

         " from DaysInMonth A3 " +
         " LEFT JOIN DestackingL3 B3 ON convert(char,A3.[Date],112)=convert(char,B3.TglProduksi,112) " +
         " LEFT JOIN PemakaianL3 C3 ON convert(char,C3.PakaiDate,112)=convert(char,B3.TglProduksi,112) " +
         " where month(date) = month(@Date)  group by A3.Date,B3.Volume,C3.Quantity)ds group by Tanggal, " +
         " M3L4,QtyPL4,M3L1,QtyPL1,M3L2,QtyPL2,M3L5,QtyPL5,M3L6,QtyPL6 " +

         " UNION ALL " +

         " select Tanggal, " +
         " M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,sum(M3L4)M3L4,avg(QtyPL4)QtyPL4,M3L5,QtyPL5,M3L6,QtyPL6 " +

         " from ( " +

         " select LEFT(convert(char,A4.date,106),11)Tanggal, " +
         " 0'M3L1',0'QtyPL1' ,0'M3L2',0'QtyPL2' ,0'M3L3',0'QtyPL3', " +
         " ROUND(ISNULL(sum(B4.Qty),0)* ISNULL(B4.Volume,0),5)'M3L4',ISNULL(C4.Quantity,0)'QtyPL4', " +
         " 0'M3L5',0'QtyPL5' ,0'M3L6',0'QtyPL6' " +

         " from DaysInMonth A4 " +

         " LEFT JOIN DestackingL4 B4 ON convert(char,A4.[Date],112)=convert(char,B4.TglProduksi,112) " +
         " LEFT JOIN PemakaianL4 C4 ON convert(char,C4.PakaiDate,112)=convert(char,B4.TglProduksi,112) " +
         " where month(date) = month(@Date)  group by A4.Date,B4.Volume,C4.Quantity)ds group by Tanggal, " +
         " M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L5,QtyPL5,M3L6,QtyPL6 " +

         " UNION ALL " +
         " select Tanggal, " +
         " M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L4,QtyPL4,sum(M3L5)M3L5,avg(QtyPL5)QtyPL5,M3L6,QtyPL6 " +

         " from ( " +
         " select LEFT(convert(char,A5.date,106),11)Tanggal, " +
         " 0'M3L1',0'QtyPL1' ,0'M3L2',0'QtyPL2' ,0'M3L3',0'QtyPL3' ,0'M3L4',0'QtyPL4', " +
         " ROUND(ISNULL(sum(B5.Qty),0)* ISNULL(B5.Volume,0),5)'M3L5',ISNULL(C5.Quantity,0)'QtyPL5', " +
         " 0'M3L6',0'QtyPL6' " +

         " from DaysInMonth A5 " +

         " LEFT JOIN DestackingL5 B5 ON convert(char,A5.[Date],112)=convert(char,B5.TglProduksi,112) " +
         " LEFT JOIN PemakaianL5 C5 ON convert(char,C5.PakaiDate,112)=convert(char,B5.TglProduksi,112) where month(date) = month(@Date)  group by A5.Date,B5.Volume,C5.Quantity)ds group by Tanggal, " +
         " M3L4,QtyPL4,M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L6,QtyPL6 " +

         " UNION ALL " +
         " select Tanggal, " +
         " M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L4,QtyPL4,M3L5,QtyPL5,sum(M3L6)M3L6,avg(QtyPL6)QtyPL6 " +

         " from ( " +
         " select LEFT(convert(char,A6.date,106),11)Tanggal, " +
         " 0'M3L1',0'QtyPL1' ,0'M3L2',0'QtyPL2' ,0'M3L3',0'QtyPL3' ,0'M3L4',0'QtyPL4' ,0'M3L5',0'QtyPL5', " +
         " ROUND(ISNULL(sum(B6.Qty),0)* ISNULL(B6.Volume,0),5)'M3L6',ISNULL(C6.Quantity,0)'QtyPL6' " +

         " from DaysInMonth A6 " +
         " LEFT JOIN DestackingL6 B6 ON convert(char,A6.[Date],112)=convert(char,B6.TglProduksi,112) " +
         " LEFT JOIN PemakaianL6 C6 ON convert(char,C6.PakaiDate,112)=convert(char,B6.TglProduksi,112) " +
         " where month(date) = month(@Date)  group by A6.Date,B6.Volume,C6.Quantity )ds group by Tanggal, " +
         " M3L4,QtyPL4,M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L5,QtyPL5) as xx group by " +
         " Tanggal,M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L4,QtyPL4,M3L5,QtyPL5,M3L6,QtyPL6 " +
         " ) as xx1 group by tanggal " +
         " ) as xx2 " +

         " union all " +

         " select ''Tanggal, " +
         " '0'OutM3L1,'0'QtySPBL1,'0'EfesiensiL1, " +
         " '0'OutM3L2,'0'QtySPBL2,'0'EfesiensiL2, " +
         " '0'OutM3L3,'0'QtySPBL3,'0'EfesiensiL3, " +
         " '0'OutM3L4,'0'QtySPBL4,'0'EfesiensiL4, " +
         " '0'OutM3L5,'0'QtySPBL5,'0'EfesiensiL5, " +
         " '0'OutM3L6,'0'QtySPBL6,'0'EfesiensiL6 ) as q " +

        " select * from TempMSFoamx order by Tanggal Asc ";*/
            #endregion
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempMSFoamx]') AND type in (N'U')) DROP TABLE [dbo].[TempMSFoamx]; " +
            "							 " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL1]') AND type in (N'U')) DROP TABLE [dbo].DestackingL1; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL2]') AND type in (N'U')) DROP TABLE [dbo].DestackingL2; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL3]') AND type in (N'U')) DROP TABLE [dbo].DestackingL3; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL4]') AND type in (N'U')) DROP TABLE [dbo].DestackingL4; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL5]') AND type in (N'U')) DROP TABLE [dbo].DestackingL5; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL6]') AND type in (N'U')) DROP TABLE [dbo].DestackingL6; " +
            "							 " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL1]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL1; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL2]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL2; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL3]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL3; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL4]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL4; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL5]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL5; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL6]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL6;						 " +
            "							 " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DaysInMonth]') AND type in (N'U')) DROP TABLE [dbo].DaysInMonth;						 " +
            "							 " +
            "	 declare @date datetime  set @date = '" + Periode + "'; " +
            "	 with DaysInMonth as ( select @date as [Date] " +
            "	 union all " +
            "	select dateadd(dd,1,Date)  from DaysInMonth  where month(date) = month(@Date))						 " +
            "	  select * into DaysInMonth from DaysInMonth						 " +
            "							 " +
            "	 select A1.Qty,A1.TglProduksi,((A2.Tebal*A2.Lebar*A2.Panjang)/1000000000)Volume into DestackingL1 from BM_Destacking A1 INNER JOIN fc_items A2 ON A1.ItemID=A2.ID " +
            "	 INNER JOIN BM_PlantGroup A3 ON A1.PlantGroupID=A3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and A3.PlantID=1 and A1.rowstatus>-1 " +
            "							 " +
            "	 select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume into DestackingL2 from BM_Destacking AL1 INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
            "	 INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=2 and AL1.rowstatus>-1						 " +
            "							 " +
            "	 select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume into DestackingL3 from BM_Destacking AL1 INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
            "	 INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=3 and AL1.rowstatus>-1						 " +
            "							 " +
            "	 select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume into DestackingL4 from BM_Destacking AL1 INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
            "	 INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=4 and AL1.rowstatus>-1 " +
            "							 " +
            "	 select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume into DestackingL5 from BM_Destacking AL1 INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
            "	 INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=5 and AL1.rowstatus>-1						 " +
            "	 " +
            "	 select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume into DestackingL6 from BM_Destacking AL1 INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
            "	 INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=6 and AL1.rowstatus>-1						 " +
            "	 " +
            "	 select sum(BL2.Quantity)Quantity,BL1.PakaiDate into PemakaianL1 from Pakai BL1 INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where  LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
            "	 (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) and BL1.Status>1 and BL2.ProdLine=1 group by BL1.PakaiDate						 " +
            "							 " +
            "	 select sum(BL2.Quantity)Quantity,BL1.PakaiDate into PemakaianL2 from Pakai BL1 INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
            "	 (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) and BL1.Status>1 and BL2.ProdLine=2 group by BL1.PakaiDate						 " +
            "							 " +
            "	 select sum(BL2.Quantity)Quantity,BL1.PakaiDate into PemakaianL3 from Pakai BL1 INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
            "	 (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) and BL1.Status>1 and BL2.ProdLine=3  group by BL1.PakaiDate						 " +
            "							 " +
            "	 select sum(BL2.Quantity)Quantity,BL1.PakaiDate into PemakaianL4 from Pakai BL1 INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
            "	 (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) and BL1.Status>1 and BL2.ProdLine=4 group by BL1.PakaiDate						 " +
            "							 " +
            "	 select sum(BL2.Quantity)Quantity,BL1.PakaiDate into PemakaianL5 from Pakai BL1 INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
            "	 (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) and BL1.Status>1 and BL2.ProdLine=5 group by BL1.PakaiDate						 " +
            "							 " +
            "	 select sum(BL2.Quantity)Quantity,BL1.PakaiDate into PemakaianL6 from Pakai BL1 INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID in " +
            "	 (select ID from Inventory where ItemCode='021396001000000' and aktif=1 and BL2.rowstatus>-1) and BL1.Status>1 and BL2.ProdLine=6  group by BL1.PakaiDate						 " +
            "							 " +
            "	 select * into TempMSFoamx " +
            "	 from ( " +
            "		 select Tanggal, 					 " +
            "		 OutM3L1,QtySPBL1,EfesiensiL1,  OutM3L2,QtySPBL2,EfesiensiL2,  OutM3L3,QtySPBL3,EfesiensiL3,  OutM3L4,QtySPBL4,EfesiensiL4,  OutM3L5,QtySPBL5,EfesiensiL5,  OutM3L6,QtySPBL6,EfesiensiL6 					 " +
            "		 from ( 					 " +
            "			 select Tanggal, 				 " +
            "			 SUM(M3L1)OutM3L1,SUM(QtyPL1)QtySPBL1,SUM(EfesiensiL1)EfesiensiL1 ,  SUM(M3L2)OutM3L2,SUM(QtyPL2)QtySPBL2,SUM(EfesiensiL2)EfesiensiL2 ,  SUM(M3L3)OutM3L3,SUM(QtyPL3)QtySPBL3,SUM(EfesiensiL3)EfesiensiL3 , 				 " +
            "			 SUM(M3L4)OutM3L4,SUM(QtyPL4)QtySPBL4,SUM(EfesiensiL4)EfesiensiL4 ,  SUM(M3L5)OutM3L5,SUM(QtyPL5)QtySPBL5,SUM(EfesiensiL5)EfesiensiL5 ,  SUM(M3L6)OutM3L6,SUM(QtyPL6)QtySPBL6,SUM(EfesiensiL6)EfesiensiL6 				 " +
            "			 from ( 				 " +
            "				 select tanggal, M3L1,QtyPL1,ISNULL(ROUND((QtyPL1/NULLIF(M3L1,0)),2),0)EfesiensiL1 , M3L2,QtyPL2,ISNULL(ROUND((QtyPL2/NULLIF(M3L2,0)),2),0)EfesiensiL2 ,  M3L3,QtyPL3,ISNULL(ROUND((QtyPL3/NULLIF(M3L3,0)),2),0)EfesiensiL3 , 			 " +
            "				 M3L4,QtyPL4,ISNULL(ROUND((QtyPL4/NULLIF(M3L4,0)),2),0)EfesiensiL4 , M3L5,QtyPL5,ISNULL(ROUND((QtyPL5/NULLIF(M3L5,0)),2),0)EfesiensiL5 , M3L6,QtyPL6,ISNULL(ROUND((QtyPL6/NULLIF(M3L6,0)),2),0)EfesiensiL6 			 " +
            "				 from ( 			 " +
            "					 select Tanggal, sum(M3L1)M3L1,avg(QtyPL1)QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L4,QtyPL4,M3L5,QtyPL5,M3L6,QtyPL6 from ( 		 " +
            "						 select LEFT(convert(char,A.date,106),11)Tanggal, ROUND(ISNULL(sum(B.Qty),0)* ISNULL(B.Volume,0),5) 'M3L1',ISNULL(C.Quantity,0)'QtyPL1', 	 " +
            "						 0'M3L2',0'QtyPL2' ,0'M3L3',0'QtyPL3' ,0'M3L4',0'QtyPL4' ,0'M3L5',0'QtyPL5' ,0'M3L6',0'QtyPL6' 	 " +
            "						 from DaysInMonth A  LEFT JOIN DestackingL1 B ON convert(char,A.[Date],112)=convert(char,B.TglProduksi,112) LEFT JOIN Pemakaianl1 C ON convert(char,C.PakaiDate,112)=convert(char,B.TglProduksi,112) 	 " +
            "						 where month(date) = month(@Date) group by A.Date,B.Volume,C.Quantity)	 " +
            "					 ds group by Tanggal,M3L4,QtyPL4,M3L2,QtyPL2,M3L3,QtyPL3,M3L5,QtyPL5,M3L6,QtyPL6 		 " +
            "					 UNION ALL 		 " +
            "					 select Tanggal, M3L1,QtyPL1,sum(M3L2)M3L2,avg(QtyPL2)QtyPL2,M3L3,QtyPL3,M3L4,QtyPL4,M3L5,QtyPL5,M3L6,QtyPL6 from ( 		 " +
            "						 select LEFT(convert(char,A2.date,106),11)Tanggal, 	 " +
            "						 0'M3L1',0'QtyPL1', 	 " +
            "						 ROUND(ISNULL(sum(B2.Qty),0)* ISNULL(B2.Volume,0),5) 'M3L2',ISNULL(C2.Quantity,0)'QtyPL2', 	 " +
            "						 0'M3L3',0'QtyPL3' ,0'M3L4',0'QtyPL4' ,0'M3L5',0'QtyPL5' ,0'M3L6',0'QtyPL6' 	 " +
            "						 from DaysInMonth A2 	 " +
            "						 LEFT JOIN DestackingL2 B2 ON convert(char,A2.[Date],112)=convert(char,B2.TglProduksi,112) LEFT JOIN PemakaianL2 C2 ON convert(char,C2.PakaiDate,112)=convert(char,B2.TglProduksi,112) 	 " +
            "						 where month(date) = month(@Date)  group by A2.Date,B2.Volume,C2.Quantity )	 " +
            "					 ds group by Tanggal, M3L4,QtyPL4,M3L1,QtyPL1,M3L3,QtyPL3,M3L5,QtyPL5,M3L6,QtyPL6 		 " +
            "					 UNION ALL 		 " +
            "					 select Tanggal,M3L1,QtyPL1,M3L2,QtyPL2,sum(M3L3)M3L3,avg(QtyPL3)QtyPL3,M3L4,QtyPL4,M3L5,QtyPL5,M3L6,QtyPL6 from ( 		 " +
            "						  select LEFT(convert(char,A3.date,106),11)Tanggal, 	 " +
            "						  0'M3L1',0'QtyPL1' ,0'M3L2',0'QtyPL2', 	 " +
            "						  ROUND(ISNULL(sum(B3.Qty),0)* ISNULL(B3.Volume,0),5) 'M3L3',ISNULL(C3.Quantity,0)'QtyPL3', 	 " +
            "						  0'M3L4',0'QtyPL4' ,0'M3L5',0'QtyPL5' ,0'M3L6',0'QtyPL6' 	 " +
            "						  from DaysInMonth A3 	 " +
            "						  LEFT JOIN DestackingL3 B3 ON convert(char,A3.[Date],112)=convert(char,B3.TglProduksi,112) LEFT JOIN PemakaianL3 C3 ON convert(char,C3.PakaiDate,112)=convert(char,B3.TglProduksi,112) 	 " +
            "						  where month(date) = month(@Date)  group by A3.Date,B3.Volume,C3.Quantity)	 " +
            "					  ds group by Tanggal, M3L4,QtyPL4,M3L1,QtyPL1,M3L2,QtyPL2,M3L5,QtyPL5,M3L6,QtyPL6 		 " +
            "					  UNION ALL 		 " +
            "					  select Tanggal, M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,sum(M3L4)M3L4,avg(QtyPL4)QtyPL4,M3L5,QtyPL5,M3L6,QtyPL6 from ( 		 " +
            "						  select LEFT(convert(char,A4.date,106),11)Tanggal,0'M3L1',0'QtyPL1' ,0'M3L2',0'QtyPL2' ,0'M3L3',0'QtyPL3', ROUND(ISNULL(sum(B4.Qty),0)* ISNULL(B4.Volume,0),5)'M3L4',ISNULL(C4.Quantity,0)'QtyPL4', 	 " +
            "						  0'M3L5',0'QtyPL5' ,0'M3L6',0'QtyPL6' 	 " +
            "						  from DaysInMonth A4 LEFT JOIN DestackingL4 B4 ON convert(char,A4.[Date],112)=convert(char,B4.TglProduksi,112) LEFT JOIN PemakaianL4 C4 ON convert(char,C4.PakaiDate,112)=convert(char,B4.TglProduksi,112) 	 " +
            "						  where month(date) = month(@Date)  group by A4.Date,B4.Volume,C4.Quantity)	 " +
            "					  ds group by Tanggal, M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L5,QtyPL5,M3L6,QtyPL6 		 " +
            "					  UNION ALL 		 " +
            "					  select Tanggal, M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L4,QtyPL4,sum(M3L5)M3L5,avg(QtyPL5)QtyPL5,M3L6,QtyPL6 from ( 		 " +
            "						  select LEFT(convert(char,A5.date,106),11)Tanggal, 0'M3L1',0'QtyPL1' ,0'M3L2',0'QtyPL2' ,0'M3L3',0'QtyPL3' ,0'M3L4',0'QtyPL4', 	 " +
            "						  ROUND(ISNULL(sum(B5.Qty),0)* ISNULL(B5.Volume,0),5)'M3L5',ISNULL(C5.Quantity,0)'QtyPL5', 0'M3L6',0'QtyPL6' 	 " +
            "						  from DaysInMonth A5 LEFT JOIN DestackingL5 B5 ON convert(char,A5.[Date],112)=convert(char,B5.TglProduksi,112) lEFT JOIN PemakaianL5 C5 ON convert(char,C5.PakaiDate,112)=convert(char,B5.TglProduksi,112) 	 " +
            "						  where month(date) = month(@Date)  group by A5.Date,B5.Volume,C5.Quantity)	 " +
            "					  ds group by Tanggal, M3L4,QtyPL4,M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L6,QtyPL6 		 " +
            "					  UNION ALL 		 " +
            "					  select Tanggal, M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L4,QtyPL4,M3L5,QtyPL5,sum(M3L6)M3L6,avg(QtyPL6)QtyPL6 from ( 		 " +
            "						  select LEFT(convert(char,A6.date,106),11)Tanggal, 0'M3L1',0'QtyPL1' ,0'M3L2',0'QtyPL2' ,0'M3L3',0'QtyPL3' ,0'M3L4',0'QtyPL4' ,0'M3L5',0'QtyPL5', 	 " +
            "						  ROUND(ISNULL(sum(B6.Qty),0)* ISNULL(B6.Volume,0),5)'M3L6',ISNULL(C6.Quantity,0)'QtyPL6' 	 " +
            "						  from DaysInMonth A6 LEFT JOIN DestackingL6 B6 ON convert(char,A6.[Date],112)=convert(char,B6.TglProduksi,112) LEFT JOIN PemakaianL6 C6 ON convert(char,C6.PakaiDate,112)=convert(char,B6.TglProduksi,112) 	 " +
            "						  where month(date) = month(@Date)  group by A6.Date,B6.Volume,C6.Quantity )	 " +
            "					  ds group by Tanggal, M3L4,QtyPL4,M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L5,QtyPL5		 " +
            "				  ) as xx 			 " +
            "				  group by  Tanggal,M3L1,QtyPL1,M3L2,QtyPL2,M3L3,QtyPL3,M3L4,QtyPL4,M3L5,QtyPL5,M3L6,QtyPL6 			 " +
            "			  ) as xx1 group by tanggal 				 " +
            "		  ) as xx2 					 " +
            "		  union all 					 " +
            "		  select ''Tanggal, 					 " +
            "		  '0'OutM3L1,'0'QtySPBL1,'0'EfesiensiL1, 					 " +
            "		  '0'OutM3L2,'0'QtySPBL2,'0'EfesiensiL2, 					 " +
            "		  '0'OutM3L3,'0'QtySPBL3,'0'EfesiensiL3, 					 " +
            "		  '0'OutM3L4,'0'QtySPBL4,'0'EfesiensiL4, 					 " +
            "		  '0'OutM3L5,'0'QtySPBL5,'0'EfesiensiL5, 					 " +
            "		  '0'OutM3L6,'0'QtySPBL6,'0'EfesiensiL6 					 " +
            "	) as q " +
            "	 select * from TempMSFoamx order by Tanggal Asc " +
            "							 " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL1]') AND type in (N'U')) DROP TABLE [dbo].DestackingL1; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL2]') AND type in (N'U')) DROP TABLE [dbo].DestackingL2; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL3]') AND type in (N'U')) DROP TABLE [dbo].DestackingL3; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL4]') AND type in (N'U')) DROP TABLE [dbo].DestackingL4; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL5]') AND type in (N'U')) DROP TABLE [dbo].DestackingL5; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DestackingL6]') AND type in (N'U')) DROP TABLE [dbo].DestackingL6; " +
            "							 " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL1]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL1; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL2]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL2; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL3]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL3; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL4]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL4; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL5]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL5; " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PemakaianL6]') AND type in (N'U')) DROP TABLE [dbo].PemakaianL6;						 " +
            "							 " +
            "	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DaysInMonth]') AND type in (N'U')) DROP TABLE [dbo].DaysInMonth;						 ";

            return result;
        }
        public ArrayList RetrieveReportFlo(string Periode, string Periode2)
        {
            arrData = new ArrayList();
            string strsql = this.MatrixQuery(Periode, Periode2);
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQuery(Periode, Periode2));
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList RetrieveReportFloKarawang(string Periode, string Periode2)
        {
            arrData = new ArrayList();
            string strsql = this.MatrixQueryKarawang(Periode, Periode2);
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQueryKarawang(Periode, Periode2));
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectKarawang(sdr));
                }
            }
            return arrData;
        }
        private string MatrixQuery2(string Periode)
        {
            string result =
            " select tanggal," +
            " OutM3L1, QtySPBL1, EfesiensiL1," +
            " OutM3L2, QtySPBL2, EfesiensiL2, " +
            " OutM3L3, QtySPBL3, EfesiensiL3," +
            " OutM3L4, QtySPBL4, EfesiensiL4 " +
            " from ( " +
            " select 'A'Urutan,Tanggal'Tanggal', " +
            " OutM3_L1 'OutM3L1' ,PakaiMMS_L1 'QtySPBL1' ,E_L1 'EfesiensiL1' , " +
            " OutM3_L2 'OutM3L2' ,PakaiMMS_L2 'QtySPBL2' ,E_L2 'EfesiensiL2' , " +
            " OutM3_L3 'OutM3L3' ,PakaiMMS_L3 'QtySPBL3' ,E_L3 'EfesiensiL3' , " +
            " OutM3_L4 'OutM3L4' ,PakaiMMS_L4 'QtySPBL4' ,E_L4 'EfesiensiL4' ,Keterangan  " +
            " from BM_PAntiFoam where Rowstatus>-1 and LEFT(convert(char,tanggal2,112),6)='" + Periode + "' " +
            " union all " +
            " select 'B'Urutan,''Tanggal, " +
            " 0'OutM3L1' ,0'QtySPBL1' ,0'EfesiensiL1' ,  " +
            " 0'OutM3L2' ,0'QtySPBL2' ,0'EfesiensiL2' ,  " +
            " 0'OutM3L3' ,0'QtySPBL3' ,0'EfesiensiL3' ,  " +
            " 0'OutM3L4' ,0'QtySPBL4' ,0'EfesiensiL4' ,0'Keterangan' " +
            " ) as xx  ORDER BY urutan,Tanggal asc ";


            return result;
        }
        private string MatrixQuery2Krwg(string Periode)
        {
            string result =
            " select tanggal," +
            " OutM3L1, QtySPBL1, EfesiensiL1, " +
            " OutM3L2, QtySPBL2, EfesiensiL2, " +
            " OutM3L3, QtySPBL3, EfesiensiL3, " +
            " OutM3L4, QtySPBL4, EfesiensiL4, " +
            " OutM3L5, QtySPBL5, EfesiensiL5, " +
            " OutM3L6, QtySPBL6, EfesiensiL6  " +
            " from ( " +
            " select 'A'Urutan,Tanggal'Tanggal', " +
            " OutM3_L1 'OutM3L1' ,PakaiMMS_L1 'QtySPBL1' ,E_L1 'EfesiensiL1' , " +
            " OutM3_L2 'OutM3L2' ,PakaiMMS_L2 'QtySPBL2' ,E_L2 'EfesiensiL2' , " +
            " OutM3_L3 'OutM3L3' ,PakaiMMS_L3 'QtySPBL3' ,E_L3 'EfesiensiL3' , " +
            " OutM3_L4 'OutM3L4' ,PakaiMMS_L4 'QtySPBL4' ,E_L4 'EfesiensiL4' , " +
            " OutM3_L5 'OutM3L5' ,PakaiMMS_L5 'QtySPBL5' ,E_L5 'EfesiensiL5' , " +
            " OutM3_L6 'OutM3L6' ,PakaiMMS_L6 'QtySPBL6' ,E_L6 'EfesiensiL6',Keterangan  " +
            " from BM_PAntiFoam where Rowstatus>-1 and LEFT(convert(char,tanggal2,112),6)='" + Periode + "' " +
            " union all " +
            " select 'B'Urutan,''Tanggal, " +
            " 0'OutM3L1' ,0'QtySPBL1' ,0'EfesiensiL1' ,  " +
            " 0'OutM3L2' ,0'QtySPBL2' ,0'EfesiensiL2' ,  " +
            " 0'OutM3L3' ,0'QtySPBL3' ,0'EfesiensiL3' ,  " +
            " 0'OutM3L4' ,0'QtySPBL4' ,0'EfesiensiL4' ,  " +
            " 0'OutM3L5' ,0'QtySPBL5' ,0'EfesiensiL5' ,  " +
            " 0'OutM3L6' ,0'QtySPBL6' ,0'EfesiensiL6'   " +
            " ,'-'Keterangan " +
            " ) as xx  ORDER BY urutan,Tanggal asc ";


            return result;
        }
        private string MatrixQuery2K(string Periode)
        {
            string result =

            " select Tanggal, " +
            " OutM3_L1 'OutM3L1' ,PakaiBB_L1 'QtyPakaiL1' ,PakaiFlo_L1 'QtySPBL1' ,E_L1 'EfesiensiL1' ,PPM_L1 'PPM1', " +
            " OutM3_L2 'OutM3L2' ,PakaiBB_L2 'QtyPakaiL2' ,PakaiFlo_L2 'QtySPBL2' ,E_L2 'EfesiensiL2' ,PPM_L2 'PPM2', " +
            " OutM3_L3 'OutM3L3' ,PakaiBB_L3 'QtyPakaiL3' ,PakaiFlo_L3 'QtySPBL3' ,E_L3 'EfesiensiL3' ,PPM_L3 'PPM3', " +
            " OutM3_L4 'OutM3L4' ,PakaiBB_L4 'QtyPakaiL4' ,PakaiFlo_L4 'QtySPBL4' ,E_L4 'EfesiensiL4' ,PPM_L4 'PPM4', " +
            " OutM3_L5 'OutM3L5' ,PakaiBB_L5 'QtyPakaiL5' ,PakaiFlo_L5 'QtySPBL5' ,E_L5 'EfesiensiL5' ,PPM_L5 'PPM5',  " +
            " OutM3_L6 'OutM3L6' ,PakaiBB_L6 'QtyPakaiL6' ,PakaiFlo_L6 'QtySPBL6' ,E_L6 'EfesiensiL6' ,PPM_L6 'PPM6'  " +
            " from BM_PFloculant where Rowstatus>-1 and LEFT(convert(char,tanggal2,112),6)='" + Periode + "' order by tanggal2";

            return result;
        }
        public ArrayList RetrieveReportMMS2(string Periode)
        {
            arrData = new ArrayList();
            string strsql = this.MatrixQuery2(Periode);
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQuery2(Periode));
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList RetrieveReportMMS2K(string Periode)
        {
            arrData = new ArrayList();
            string strsql = this.MatrixQuery2Krwg(Periode);
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQuery2Krwg(Periode));
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectKrwg(sdr));
                }
            }
            return arrData;
        }
        public ArrayList RetrieveReportFlo2Karawang(string Periode)
        {
            arrData = new ArrayList();
            string strsql = this.MatrixQuery2K(Periode);
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQuery2K(Periode));
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectK(sdr));
                }
            }
            return arrData;
        }
        public int RetrieveInputan(string Periode)
        {
            string StrSql =
            " select SUM(Total)Total from (select COUNT(ID)Total from BM_PAntiFoam " +
            " where LEFT(convert(char,tanggal2,112),6)='" + Periode + "' and Rowstatus>-1 union all select '0'Total ) as xx ";
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
        public DomainBMReportMMS Retrievetgl(string Tgl)
        {
            string StrSql =
            " select ID,Keterangan from BM_PAntiFoam where Tanggal='" + Tgl + "' and rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveTgl(sqlDataReader);
                }
            }

            return new DomainBMReportMMS();
        }
        public DomainBMReportMMS RetrieveSign(int ID)
        {
            string StrSql = " select * from BM_PSign where rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveSign(sqlDataReader);
                }
            }

            return new DomainBMReportMMS();
        }

        private DomainBMReportMMS GenerateObject_RetrieveSign(SqlDataReader sdr)
        {
            DomainBMReportMMS cpj = new DomainBMReportMMS();
            cpj.AdminSign = sdr["AdminSign"].ToString();
            cpj.MgrSign = sdr["MgrSign"].ToString();
            cpj.PMSign = sdr["PMSign"].ToString();
            return cpj;
        }

        private DomainBMReportMMS GenerateObject(SqlDataReader sdr)
        {
            DomainBMReportMMS cpj = new DomainBMReportMMS();

            cpj.Tanggal = sdr["Tanggal"].ToString();

            //cpj.OutM3L1 = Convert.ToInt32(sdr["OutM3L1"]);
            cpj.OutM3L1 = Convert.ToDecimal(sdr["OutM3L1"]);
            cpj.QtySPBL1 = Convert.ToDecimal(sdr["QtySPBL1"]);
            cpj.EfesiensiL1 = Convert.ToDecimal(sdr["EfesiensiL1"]);


            cpj.OutM3L2 = Convert.ToDecimal(sdr["OutM3L2"]);
            cpj.QtySPBL2 = Convert.ToDecimal(sdr["QtySPBL2"]);
            cpj.EfesiensiL2 = Convert.ToDecimal(sdr["EfesiensiL2"]);

            cpj.OutM3L3 = Convert.ToDecimal(sdr["OutM3L3"]);
            cpj.QtySPBL3 = Convert.ToDecimal(sdr["QtySPBL3"]);
            cpj.EfesiensiL3 = Convert.ToDecimal(sdr["EfesiensiL3"]);

            cpj.OutM3L4 = Convert.ToDecimal(sdr["OutM3L4"]);
            cpj.QtySPBL4 = Convert.ToDecimal(sdr["QtySPBL4"]);
            cpj.EfesiensiL4 = Convert.ToDecimal(sdr["EfesiensiL4"]);

            return cpj;
        }
        private DomainBMReportMMS GenerateObjectKrwg(SqlDataReader sdr)
        {
            DomainBMReportMMS cpj = new DomainBMReportMMS();

            cpj.Tanggal = sdr["Tanggal"].ToString();

            cpj.OutM3L1 = Convert.ToInt32(sdr["OutM3L1"]);
            cpj.QtySPBL1 = Convert.ToInt32(sdr["QtySPBL1"]);
            cpj.EfesiensiL1 = Convert.ToDecimal(sdr["EfesiensiL1"]);


            cpj.OutM3L2 = Convert.ToInt32(sdr["OutM3L2"]);
            cpj.QtySPBL2 = Convert.ToInt32(sdr["QtySPBL2"]);
            cpj.EfesiensiL2 = Convert.ToDecimal(sdr["EfesiensiL2"]);

            cpj.OutM3L3 = Convert.ToInt32(sdr["OutM3L3"]);
            cpj.QtySPBL3 = Convert.ToInt32(sdr["QtySPBL3"]);
            cpj.EfesiensiL3 = Convert.ToDecimal(sdr["EfesiensiL3"]);

            cpj.OutM3L4 = Convert.ToInt32(sdr["OutM3L4"]);
            cpj.QtySPBL4 = Convert.ToInt32(sdr["QtySPBL4"]);
            cpj.EfesiensiL4 = Convert.ToDecimal(sdr["EfesiensiL4"]);

            cpj.OutM3L5 = Convert.ToInt32(sdr["OutM3L5"]);
            cpj.QtySPBL5 = Convert.ToInt32(sdr["QtySPBL5"]);
            cpj.EfesiensiL5 = Convert.ToDecimal(sdr["EfesiensiL5"]);

            cpj.OutM3L6 = Convert.ToInt32(sdr["OutM3L6"]);
            cpj.QtySPBL6 = Convert.ToInt32(sdr["QtySPBL6"]);
            cpj.EfesiensiL6 = Convert.ToDecimal(sdr["EfesiensiL6"]);

            return cpj;
        }
        private DomainBMReportMMS GenerateObjectK(SqlDataReader sdr)
        {
            DomainBMReportMMS cpj = new DomainBMReportMMS();

            cpj.Tanggal = sdr["Tanggal"].ToString();

            cpj.OutM3L1 = Convert.ToInt32(sdr["OutM3L1"]);
            cpj.QtySPBL1 = Convert.ToInt32(sdr["QtySPBL1"]);
            cpj.EfesiensiL1 = Convert.ToDecimal(sdr["EfesiensiL1"]);

            cpj.OutM3L2 = Convert.ToInt32(sdr["OutM3L2"]);
            cpj.QtySPBL2 = Convert.ToInt32(sdr["QtySPBL2"]);
            cpj.EfesiensiL2 = Convert.ToDecimal(sdr["EfesiensiL2"]);

            cpj.OutM3L3 = Convert.ToInt32(sdr["OutM3L3"]);
            cpj.QtySPBL3 = Convert.ToInt32(sdr["QtySPBL3"]);
            cpj.EfesiensiL3 = Convert.ToDecimal(sdr["EfesiensiL3"]);

            cpj.OutM3L4 = Convert.ToInt32(sdr["OutM3L4"]);
            cpj.QtySPBL4 = Convert.ToInt32(sdr["QtySPBL4"]);
            cpj.EfesiensiL4 = Convert.ToDecimal(sdr["EfesiensiL4"]);

            cpj.OutM3L5 = Convert.ToInt32(sdr["OutM3L5"]);
            cpj.QtySPBL5 = Convert.ToInt32(sdr["QtySPBL5"]);
            cpj.EfesiensiL5 = Convert.ToDecimal(sdr["EfesiensiL5"]);

            cpj.OutM3L6 = Convert.ToInt32(sdr["OutM3L6"]);
            cpj.QtySPBL6 = Convert.ToInt32(sdr["QtySPBL6"]);
            cpj.EfesiensiL6 = Convert.ToDecimal(sdr["EfesiensiL6"]);

            return cpj;
        }
        private DomainBMReportMMS GenerateObjectKarawang(SqlDataReader sdr)
        {
            DomainBMReportMMS cpj = new DomainBMReportMMS();

            cpj.Tanggal = sdr["Tanggal"].ToString();

            cpj.OutM3L1 = Convert.ToDecimal(sdr["OutM3L1"]);
            cpj.QtySPBL1 = Convert.ToInt32(sdr["QtySPBL1"]);
            cpj.EfesiensiL1 = Convert.ToDecimal(sdr["EfesiensiL1"]);

            cpj.OutM3L2 = Convert.ToDecimal(sdr["OutM3L2"]);
            cpj.QtySPBL2 = Convert.ToInt32(sdr["QtySPBL2"]);
            cpj.EfesiensiL2 = Convert.ToDecimal(sdr["EfesiensiL2"]);

            cpj.OutM3L3 = Convert.ToDecimal(sdr["OutM3L3"]);
            cpj.QtySPBL3 = Convert.ToInt32(sdr["QtySPBL3"]);
            cpj.EfesiensiL3 = Convert.ToDecimal(sdr["EfesiensiL3"]);

            cpj.OutM3L4 = Convert.ToDecimal(sdr["OutM3L4"]);
            cpj.QtySPBL4 = Convert.ToInt32(sdr["QtySPBL4"]);
            cpj.EfesiensiL4 = Convert.ToDecimal(sdr["EfesiensiL4"]);

            cpj.OutM3L5 = Convert.ToDecimal(sdr["OutM3L5"]);
            cpj.QtySPBL5 = Convert.ToInt32(sdr["QtySPBL5"]);
            cpj.EfesiensiL5 = Convert.ToDecimal(sdr["EfesiensiL5"]);

            cpj.OutM3L6 = Convert.ToDecimal(sdr["OutM3L6"]);
            cpj.QtySPBL6 = Convert.ToInt32(sdr["QtySPBL6"]);
            cpj.EfesiensiL6 = Convert.ToDecimal(sdr["EfesiensiL6"]);

            return cpj;
        }
        private DomainBMReportMMS GenerateObject_RetrieveTgl(SqlDataReader sdr)
        {
            DomainBMReportMMS cpj = new DomainBMReportMMS();

            cpj.ID = Convert.ToInt32(sdr["ID"]);
            cpj.Keterangan = sdr["Keterangan"].ToString();

            return cpj;
        }


    }

    public class DomainBMReportMMS
    {
        public int UnitKerjaID { get; set; }
        public int ID { get; set; }

        public DateTime LastModifiedTime { get; set; }
        public DateTime Tanggal2 { get; set; }

        public string AdminSign { get; set; }
        public string MgrSign { get; set; }
        public string PMSign { get; set; }
        public string LastModifiedBy { get; set; }
        public string Periode { get; set; }
        public string Tanggal { get; set; }
        public string Bulan { get; set; }
        public string Tahun { get; set; }
        public string Keterangan { get; set; }

        public Decimal OutM3L1 { get; set; }
        public Decimal OutM3L2 { get; set; }
        public Decimal OutM3L3 { get; set; }
        public Decimal OutM3L4 { get; set; }
        public Decimal OutM3L5 { get; set; }
        public Decimal OutM3L6 { get; set; }

        public Decimal QtySPBL1 { get; set; }
        public Decimal QtySPBL2 { get; set; }
        public Decimal QtySPBL3 { get; set; }
        public Decimal QtySPBL4 { get; set; }
        public Decimal QtySPBL5 { get; set; }
        public Decimal QtySPBL6 { get; set; }

        public Decimal EfesiensiL1 { get; set; }
        public Decimal EfesiensiL2 { get; set; }
        public Decimal EfesiensiL3 { get; set; }
        public Decimal EfesiensiL4 { get; set; }
        public Decimal EfesiensiL5 { get; set; }
        public Decimal EfesiensiL6 { get; set; }


    }


}
