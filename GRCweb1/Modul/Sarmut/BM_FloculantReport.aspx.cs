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
//using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.SarMut
{
    //public partial class BM_FloculantReport : System.Web.UI.Page
    public partial class BM_FloculantReport : System.Web.UI.Page
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

                btnLihat.Visible = false; btnNew.Visible = false; BtnPreview.Visible = true;
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
            //ddlBulan.Items.Add(new ListItem("--Pilih Bulan--", "0"));
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

            if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
            {
                btnSimpan.Visible = true; btnLihat.Visible = false; btnCancel.Visible = true; btnExport.Visible = false;

                //for (int i = 0; i < lstMatrix.Items.Count; i++)
                //{
                //    TextBox txt = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL1");
                //    TextBox txt2 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL2");
                //    TextBox txt3 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL3");
                //    TextBox txt4 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL4");

                //    txt.Text = ""; txt2.Text = ""; txt3.Text = ""; txt4.Text = "";
                //}
            }
            else if (user.UnitKerjaID == 7)
            {
                btnSimpan.Visible = true; btnLihat.Visible = false; btnCancel.Visible = true; btnExport.Visible = false;

                for (int i = 0; i < lstMatrixK2.Items.Count; i++)
                {
                    TextBox txt = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1K");
                    TextBox txt2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2K");
                    TextBox txt3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3K");
                    TextBox txt4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4K");
                    TextBox txt5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5K");
                    TextBox txt6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6K");

                    txt.Text = ""; txt2.Text = ""; txt3.Text = ""; txt4.Text = ""; txt5.Text = ""; txt6.Text = "";
                }
            }

            Response.Redirect("BM_FloculantReport.aspx");

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
                        FacadeBMReport2 fbm = new FacadeBMReport2();
                        DomainBMReport2 dbm = new DomainBMReport2();
                        TextBox Keterangan = (TextBox)lstMatrix.Items[i].FindControl("Keterangan");
                        dbm.UnitKerjaID = user.UnitKerjaID;
                        dbm.Tanggal2 = Convert.ToDateTime(tr.Cells[0].InnerHtml);
                        dbm.Tanggal = tr.Cells[0].InnerHtml;
                        dbm.Keterangan = Keterangan.Text.ToString() != "" ? Keterangan.Text.ToString() : "-";

                        #region Line 1

                        //TextBox jml = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL1");
                        //string jmlreplace = jml.Text.Replace(".", ",");
                        //string jmlreplace2 = jmlreplace != "" ? jmlreplace : "0";
                        //dbm.PakaiBBL1 = Convert.ToDecimal(jmlreplace2);
                        //dbm.PakaiBBL1 = Convert.ToDecimal(jmlreplace2);

                        dbm.PakaiBBL1 = Math.Round(decimal.Parse(tr.Cells[2].InnerHtml), 2);
                        string OutM3L1 = tr.Cells[1].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[1].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L1 = Convert.ToDecimal(OutM3L1);
                        dbm.QtySPBL1 = Math.Round(decimal.Parse(tr.Cells[3].InnerHtml), 3);
                        dbm.EfesiensiL1 = Math.Round(decimal.Parse(tr.Cells[4].InnerHtml), 2);
                        //dbm.PPML1 = decimal.Parse(jmlreplace2) > 0 ? (decimal.Parse(tr.Cells[3].InnerHtml) / decimal.Parse(jmlreplace2)) * 1000000 : 0;
                        dbm.PPML1 = dbm.PakaiBBL1 > 0 ? (decimal.Parse(tr.Cells[3].InnerHtml) / dbm.PakaiBBL1) * 1000000 : 0;

                        #endregion

                        #region Line 2

                        //TextBox jml2 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL2");
                        //string jml2replace = jml2.Text.Replace(".", ",");
                        //string jml2replace2 = jml2replace != "" ? jml2replace : "0";
                        //dbm.PakaiBBL2 = Convert.ToDecimal(jml2replace2);  

                        dbm.PakaiBBL2 = Math.Round(decimal.Parse(tr.Cells[7].InnerHtml), 2);
                        string OutM3L2 = tr.Cells[6].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[6].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L2 = Convert.ToDecimal(OutM3L2);
                        dbm.QtySPBL2 = decimal.Parse(tr.Cells[8].InnerHtml);
                        dbm.EfesiensiL2 = Math.Round(decimal.Parse(tr.Cells[9].InnerHtml), 2);
                        //dbm.PPML2 = decimal.Parse(jml2replace2) > 0 ? (decimal.Parse(tr.Cells[8].InnerHtml) / decimal.Parse(jml2replace2)) * 1000000 : 0;
                        dbm.PPML2 = dbm.PakaiBBL2 > 0 ? (decimal.Parse(tr.Cells[8].InnerHtml) / dbm.PakaiBBL2) * 1000000 : 0;

                        #endregion

                        #region Line 3

                        //TextBox jml3 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL3");
                        //string jml3replace = jml3.Text.Replace(".", ",");
                        //string jml3replace2 = jml3replace != "" ? jml3replace : "0";
                        //dbm.PakaiBBL3 = Convert.ToDecimal(jml3replace2);    

                        dbm.PakaiBBL3 = Math.Round(decimal.Parse(tr.Cells[12].InnerHtml), 2);
                        string OutM3L3 = tr.Cells[11].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[11].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L3 = Convert.ToDecimal(OutM3L3);
                        dbm.QtySPBL3 = decimal.Parse(tr.Cells[13].InnerHtml);
                        dbm.EfesiensiL3 = Math.Round(decimal.Parse(tr.Cells[14].InnerHtml), 2);
                        //dbm.PPML3 = decimal.Parse(jml3replace2) > 0 ? (decimal.Parse(tr.Cells[13].InnerHtml) / decimal.Parse(jml3replace2)) * 1000000 : 0;
                        dbm.PPML3 = dbm.PakaiBBL3 > 0 ? (decimal.Parse(tr.Cells[13].InnerHtml) / dbm.PakaiBBL3) * 1000000 : 0;

                        #endregion

                        #region Line 4

                        //TextBox jml4 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL4");
                        //string jml4replace = jml4.Text.Replace(".", ",");
                        //string jml4replace2 = jml4replace != "" ? jml4replace : "0";
                        //dbm.PakaiBBL4 = Convert.ToDecimal(jml4replace2);

                        dbm.PakaiBBL4 = Math.Round(decimal.Parse(tr.Cells[17].InnerHtml), 2);
                        string OutM3L4 = tr.Cells[16].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[16].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";
                        dbm.OutM3L4 = Convert.ToDecimal(OutM3L4);
                        dbm.QtySPBL4 = decimal.Parse(tr.Cells[18].InnerHtml);
                        dbm.EfesiensiL4 = Math.Round(decimal.Parse(tr.Cells[19].InnerHtml), 2);
                        //dbm.PPML4 = decimal.Parse(jml4replace2) > 0 ? (decimal.Parse(tr.Cells[18].InnerHtml) / decimal.Parse(jml4replace2)) * 1000000 : 0;
                        dbm.PPML4 = dbm.PakaiBBL4 > 0 ? (decimal.Parse(tr.Cells[18].InnerHtml) / dbm.PakaiBBL4) * 1000000 : 0;
                        //dbm.PakaiBBL4 = QtyPakaiBBD4;
                        #endregion

                        #region Optional Line 5 - 6
                        TextBox jml5 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL5");
                        decimal QtyPakaiBBD5 = 0; dbm.OutM3L5 = 0; dbm.QtySPBL5 = 0; dbm.EfesiensiL5 = 0; dbm.PPML5 = 0; dbm.PakaiBBL5 = QtyPakaiBBD5;
                        TextBox jml6 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL6");
                        decimal QtyPakaiBBD6 = 0; dbm.OutM3L6 = 0; dbm.QtySPBL6 = 0; dbm.EfesiensiL6 = 0; dbm.PPML6 = 0; dbm.PakaiBBL6 = QtyPakaiBBD6;
                        #endregion

                        int rst = 0;
                        rst = fbm.insertPFloculant(dbm);

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
                        FacadeBMReport2 fbm = new FacadeBMReport2();
                        DomainBMReport2 dbm = new DomainBMReport2();
                        TextBox Keterangan = (TextBox)lstMatrixK2.Items[i].FindControl("Keterangan");
                        dbm.UnitKerjaID = user.UnitKerjaID;
                        dbm.Tanggal2 = Convert.ToDateTime(tr.Cells[0].InnerHtml);
                        dbm.Tanggal = tr.Cells[0].InnerHtml;
                        dbm.Keterangan = Keterangan.Text.ToString() != "" ? Keterangan.Text.ToString() : "-";

                        #region Line 1

                        TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1K");
                        string jmlreplace = jml.Text.Replace(".", ",");
                        string jmlreplace2K = jmlreplace != "" ? jmlreplace : "0";
                        string OutM3L1 = tr.Cells[1].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[1].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";

                        dbm.PakaiBBL1 = Math.Round(decimal.Parse(tr.Cells[2].InnerHtml), 3);
                        dbm.OutM3L1 = Convert.ToDecimal(OutM3L1);
                        dbm.QtySPBL1 = Convert.ToDecimal(jmlreplace2K);
                        dbm.EfesiensiL1 = Math.Round(decimal.Parse(tr.Cells[4].InnerHtml), 2);
                        dbm.PPML1 = dbm.QtySPBL1 > 0 ? decimal.Parse(jmlreplace2K) / (decimal.Parse(tr.Cells[2].InnerHtml)) * 1000000 : 0;

                        #endregion

                        #region Line 2

                        TextBox jmlL2K = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2K");
                        string jmlreplaceL2K = jmlL2K.Text.Replace(".", ",");
                        string jmlreplace2L2K = jmlreplaceL2K != "" ? jmlreplaceL2K : "0";
                        string OutM3L2 = tr.Cells[6].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[6].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";

                        dbm.PakaiBBL2 = Math.Round(decimal.Parse(tr.Cells[7].InnerHtml), 3);
                        dbm.OutM3L2 = Convert.ToDecimal(OutM3L2);
                        dbm.QtySPBL2 = Convert.ToDecimal(jmlreplace2L2K);
                        dbm.EfesiensiL2 = Math.Round(decimal.Parse(tr.Cells[9].InnerHtml), 2);
                        dbm.PPML2 = dbm.QtySPBL2 > 0 ? decimal.Parse(jmlreplace2L2K) / (decimal.Parse(tr.Cells[7].InnerHtml)) * 1000000 : 0;

                        #endregion

                        #region Line 3

                        TextBox jmlL3K = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3K");
                        string jmlreplaceL3K = jmlL3K.Text.Replace(".", ",");
                        string jmlreplace2L3K = jmlreplaceL3K != "" ? jmlreplaceL3K : "0";
                        string OutM3L3 = tr.Cells[11].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[11].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";


                        dbm.PakaiBBL3 = Math.Round(decimal.Parse(tr.Cells[12].InnerHtml), 3);
                        dbm.OutM3L3 = Convert.ToDecimal(OutM3L3);
                        dbm.QtySPBL3 = Convert.ToDecimal(jmlreplace2L3K);
                        dbm.EfesiensiL3 = Math.Round(decimal.Parse(tr.Cells[14].InnerHtml), 2);
                        dbm.PPML3 = dbm.QtySPBL3 > 0 ? decimal.Parse(jmlreplace2L3K) / (decimal.Parse(tr.Cells[12].InnerHtml)) * 1000000 : 0;

                        #endregion

                        #region Line 4

                        TextBox jmlL4K = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4K");
                        string jmlreplaceL4K = jmlL4K.Text.Replace(".", ",");
                        string jmlreplace2L4K = jmlreplaceL4K != "" ? jmlreplaceL4K : "0";
                        string OutM3L4 = tr.Cells[16].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[16].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";

                        //dbm.PakaiBBL4 = Convert.ToDecimal(jmlreplace2L4K);
                        dbm.PakaiBBL4 = Math.Round(decimal.Parse(tr.Cells[17].InnerHtml), 3);
                        dbm.OutM3L4 = Convert.ToDecimal(OutM3L4);
                        //dbm.QtySPBL4 = Math.Round(decimal.Parse(tr.Cells[17].InnerHtml), 3);
                        dbm.QtySPBL4 = Convert.ToDecimal(jmlreplace2L4K);
                        dbm.EfesiensiL4 = Math.Round(decimal.Parse(tr.Cells[19].InnerHtml), 2);
                        //dbm.PPML4 = decimal.Parse(jmlreplace2L4K) > 0 ? (decimal.Parse(tr.Cells[18].InnerHtml) / decimal.Parse(jmlreplace2L4K)) * 1000000 : 0;
                        dbm.PPML4 = dbm.QtySPBL4 > 0 ? decimal.Parse(jmlreplace2L4K) / (decimal.Parse(tr.Cells[17].InnerHtml)) * 1000000 : 0;

                        #endregion

                        #region Line 5

                        TextBox jmlL5K = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5K");
                        string jmlreplaceL5K = jmlL5K.Text.Replace(".", ",");
                        string jmlreplace2L5K = jmlreplaceL5K != "" ? jmlreplaceL5K : "0";
                        string OutM3L5 = tr.Cells[21].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[21].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";

                        //dbm.PakaiBBL5 = Convert.ToDecimal(jmlreplace2L5K);
                        dbm.PakaiBBL5 = Math.Round(decimal.Parse(tr.Cells[22].InnerHtml), 3);
                        dbm.OutM3L5 = Convert.ToDecimal(OutM3L5);
                        //dbm.QtySPBL5 = Math.Round(decimal.Parse(tr.Cells[22].InnerHtml), 3);
                        dbm.QtySPBL5 = Convert.ToDecimal(jmlreplace2L5K);
                        dbm.EfesiensiL5 = Math.Round(decimal.Parse(tr.Cells[24].InnerHtml), 2);
                        //dbm.PPML5 = decimal.Parse(jmlreplace2L5K) > 0 ? (decimal.Parse(tr.Cells[23].InnerHtml) / decimal.Parse(jmlreplace2L5K)) * 1000000 : 0;
                        dbm.PPML5 = dbm.QtySPBL5 > 0 ? decimal.Parse(jmlreplace2L5K) / (decimal.Parse(tr.Cells[22].InnerHtml)) * 1000000 : 0;

                        #endregion

                        #region Line 6

                        TextBox jmlL6K = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6K");
                        string jmlreplaceL6K = jmlL6K.Text.Replace(".", ",");
                        string jmlreplace2L6K = jmlreplaceL6K != "" ? jmlreplaceL6K : "0";
                        string OutM3L6 = tr.Cells[26].InnerHtml != "&nbsp;&nbsp;0" ? tr.Cells[26].InnerHtml.Replace("&nbsp;&nbsp;", "") : "0";

                        dbm.PakaiBBL6 = Math.Round(decimal.Parse(tr.Cells[27].InnerHtml), 3);
                        dbm.OutM3L6 = Convert.ToDecimal(OutM3L6);
                        dbm.QtySPBL6 = Convert.ToDecimal(jmlreplace2L6K);
                        dbm.EfesiensiL6 = Math.Round(decimal.Parse(tr.Cells[29].InnerHtml), 2);
                        //dbm.PPML6 = decimal.Parse(jmlreplace2L6K) > 0 ? (decimal.Parse(tr.Cells[28].InnerHtml) / decimal.Parse(jmlreplace2L6K)) * 1000000 : 0;
                        dbm.PPML6 = dbm.QtySPBL6 > 0 ? decimal.Parse(jmlreplace2L6K) / (decimal.Parse(tr.Cells[27].InnerHtml)) * 1000000 : 0;

                        #endregion

                        int rst = 0;
                        rst = fbm.insertPFloculant(dbm);

                        if (rst > 0)
                        {
                            btnSimpan.Enabled = false;
                        }
                    }
                }
            }
            #endregion
            { DisplayAJAXMessage(this, " Data berhasil disimpan !! "); return; }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string Periode = Session["PeriodeBulanTahun"].ToString();
            FacadeBMReport2 fbm2 = new FacadeBMReport2();
            DomainBMReport2 dbm2 = new DomainBMReport2();
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
        protected void btnLihat_Click(object sender, EventArgs e)
        {
            Session["FlagLap"] = 5;
            LoadPreview();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=LaporanFlooculant" + "_" + ddlBulan.SelectedItem.Text + "_" + ddlTahun.SelectedItem.Text + ".xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            string Html = "PT BANGUNPERKASA ADHITAMASENTRA";
            Html += " <br> ";
            Html += "<center><b>PEMANTAUAN EFISIENSI FLOCULANT</b></center>";
            Html += "<center><b>Periode Flooculant : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedItem.Text + "<center><b>";
            Html += "";
            Html += " <br> ";
            Html += " <br> ";

            if (user.UnitKerjaID == 1)
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

                lst.RenderControl(hw); string PlantNama = "Citeureup"; Session["PlantNama"] = PlantNama;


            }
            else if (user.UnitKerjaID == 13)
            {
                lstLaporanK.RenderControl(hw); string PlantNama = "Jombang"; Session["PlantNama"] = PlantNama;
            }
            else if (user.UnitKerjaID == 7)
            {
                for (int i = 0; i < lstMatrixK2.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK");
                    TextBox Ket = (TextBox)lstMatrixK2.Items[i].FindControl("Keterangan");
                    Label Keterangan = (Label)lstMatrixK2.Items[i].FindControl("txtKeterangan");

                    if (Ket != null) { Ket.Visible = false; }
                    Keterangan.Text = Ket.Text;
                    if (Keterangan != null) { Keterangan.Visible = true; }
                    //tr.Cells[13].Attributes.Add("style", "background-color:White;");
                }

                lstK.RenderControl(hw); string PlantNama = "Karawang"; Session["PlantNama"] = PlantNama;
            }

            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            string Tanggal = Session["Tgl"].ToString();
            string Admin = Session["admin"].ToString();
            string Manager = Session["manager"].ToString();
            string Plant = Session["PlantNama"].ToString();

            if (user.UnitKerjaID == 7)
            {
                string HtmlEnd = " <br> ";
                HtmlEnd += " <br> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; " + Plant + ", " + Tanggal + " ";

                HtmlEnd += "<br> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
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
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp; ";
                HtmlEnd += " ( Zuhri ) ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " ( " + Manager + " ) ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " ( " + Admin + " ) ";

                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }
            else if (user.UnitKerjaID == 1)
            {
                string HtmlEnd = " <br> ";
                HtmlEnd += " <br> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; " + Plant + ", " + Tanggal + " ";

                HtmlEnd += "<br> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp; ";
                HtmlEnd += " Mengetahui : ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Disetujui : ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Dibuat : ";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp; ";
                HtmlEnd += " ( Justin ) ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " ( " + Manager + " ) ";
                HtmlEnd += " &emsp;&emsp;&emsp; ";
                HtmlEnd += " ( " + Admin + " ) ";

                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }
            else if (user.UnitKerjaID == 13)
            {
                string HtmlEnd = " <br> ";
                HtmlEnd += " <br> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; " + Plant + ", " + Tanggal + " ";

                HtmlEnd += "<br> ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp; ";
                HtmlEnd += " Mengetahui : ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Disetujui : ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " Dibuat : ";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += "<br>";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp; ";
                HtmlEnd += " ( Tresna ) ";
                HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp; ";
                HtmlEnd += " ( " + Manager + " ) ";
                HtmlEnd += " &emsp;&emsp;&emsp; ";
                HtmlEnd += " ( " + Admin + " ) ";

                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }

        }



        protected void BtnPreview_Click(object sender, EventArgs e)
        {
            // priview();  
            //btnSimpan.Enabled = true;
            Session["FlagLap"] = 0;
            LoadPreview();
        }

        protected void SarmutOnSystem1()
        {
            #region Update Sarmut Effisiensi Pemakaian Flooculant add by Razib  Line-1
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - Flooculant
            string sarmutPrs = "Effisiensi Pemakaian Flooculant";
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
                            " select cast(isnull(sum(QtySPBL1),0)as decimal(10,2))QtySPBL1,round(cast(isnull(sum(OutM3L1),0)as decimal(10,2)),0)OutM3L1 from TempFlo " +
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
            #region Update Sarmut Effisiensi Pemakaian Flooculant add by Razib  Line-2
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - Flooculant
            string sarmutPrs = "Effisiensi Pemakaian Flooculant";
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
                            "select isnull(round(QtySPBL2/nullif(OutM3L2,0),2),0)actual from ( " +
                            "select cast(isnull(sum(QtySPBL2),0)as decimal(10,2))QtySPBL2,round(cast(isnull(sum(OutM3L2),0)as decimal(10,2)),0)OutM3L2 from TempFlo " +
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
            #region Update Sarmut Effisiensi Pemakaian Flooculant add by Razib  Line-3
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - Flooculant
            string sarmutPrs = "Effisiensi Pemakaian Flooculant";
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
                            " select cast(isnull(sum(QtySPBL3),0)as decimal(10,2))QtySPBL3,round(cast(isnull(sum(OutM3L3),0)as decimal(10,2)),0)OutM3L3 from TempFlo " +
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
            #region Update Sarmut Effisiensi Pemakaian Flooculant add by Razib  Line-4
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - Flooculant
            string sarmutPrs = "Effisiensi Pemakaian Flooculant";
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
                            " select cast(isnull(sum(QtySPBL4),0)as decimal(10,2))QtySPBL4,round(cast(isnull(sum(OutM3L4),0)as decimal(10,2)),0)OutM3L4 from TempFlo " +
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
            #region Update Sarmut Effisiensi Pemakaian Flooculant add by Razib  Line-5
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - Flooculant
            string sarmutPrs = "Effisiensi Pemakaian Flooculant";
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
                            " select cast(isnull(sum(QtySPBL5),0)as decimal(10,2))QtySPBL5,round(cast(isnull(sum(OutM3L5),0)as decimal(10,2)),0)OutM3L5 from TempFlo " +
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
            #region Update Sarmut Effisiensi Pemakaian Flooculant add by Razib  Line-6
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - Flooculant
            string sarmutPrs = "Effisiensi Pemakaian Flooculant";
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
                            " select cast(isnull(sum(QtySPBL6),0)as decimal(10,2))QtySPBL6,round(cast(isnull(sum(OutM3L6),0)as decimal(10,2)),0)OutM3L6 from TempFlo " +
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
            string FLagLap = Session["FlagLap"].ToString();

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

            DomainBMReport2 DBM = new DomainBMReport2();
            FacadeBMReport2 FBM = new FacadeBMReport2();
            int CekInput = FBM.RetrieveInputan(PeriodeBulanTahun);
            Session["Cek"] = CekInput;
            #region sudah input
            if (CekInput > 0)
            {
                btnSimpan.Visible = false; btnCancel.Visible = true; btnNew.Visible = true;
                btnExport.Visible = false; btnLihat.Visible = true;

                ArrayList arrData2 = new ArrayList();
                FacadeBMReport2 Fbm2 = new FacadeBMReport2();

                if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                {
                    if (FLagLap == "5")
                    {
                        btnExport.Visible = true; BtnPreview.Visible = false; btnLihat.Visible = false; btnCancel.Visible = true;
                        PanelLaporanCiteureup.Visible = true; 
                        PanelCiteureup.Visible = false; 
                        PanelKarawang.Visible = false;
                        string Query = string.Empty;
                        Query =
                        " union all " +
                        " select '2'Urutan,''Tanggal, " +
                        " '0'OutM3L1,'0'QtyPakaiL1,'0'QtySPBL1,'0'EfesiensiL1,0'PPML1', " +
                        " '0'OutM3L2,'0'QtyPakaiL2,'0'QtySPBL2,'0'EfesiensiL2,0'PPML2', " +
                        " '0'OutM3L3,'0'QtyPakaiL3,'0'QtySPBL3,'0'EfesiensiL3,0'PPML3', " +
                        " '0'OutM3L4,'0'QtyPakaiL4,'0'QtySPBL4,'0'EfesiensiL4,0'PPML4', " +
                        " '0'OutM3L5,'0'QtyPakaiL5,'0'QtySPBL5,'0'EfesiensiL5,0'PPML5', " +
                        " '0'OutM3L6,'0'QtyPakaiL6,'0'QtySPBL6,'0'EfesiensiL6,0'PPML6', " +
                        " '-'Keterangan ";
                        ArrayList arrDataLC = new ArrayList();
                        //arrDataLC = Fbm2.RetrieveReportFlo2(PeriodeBulanTahun, Periode, Query); Session["Data2"] = arrDataLC;
                        arrDataLC = Fbm2.RetrieveReportFloReport(PeriodeBulanTahun, Periode, Query); Session["Data2"] = arrDataLC;
                        lstMatrixLapC.DataSource = arrDataLC;
                        lstMatrixLapC.DataBind();
                    }
                    else
                    {
                        //arrData2 = Fbm2.RetrieveReportFlo2(PeriodeBulanTahun, Periode); Session["Data2"] = arrData2;
                        //lstMatrix.DataSource = arrData2; Session["DataGrid"] = arrData2;
                        //lstMatrix.DataBind();
                        PanelLaporanKarawang.Visible = false;
                        PanelCiteureup.Visible = true; 
                        PanelKarawang.Visible = false;
                        string Query = string.Empty;
                        Query =
                        " union all " +
                        " select '2'Urutan,''Tanggal, " +
                        " '0'OutM3L1,'0'QtySPB_BBL1,'0'QtySPBL1,'0'EfesiensiL1,0'PPM1', " +
                        " '0'OutM3L2,'0'QtySPB_BBL2,'0'QtySPBL2,'0'EfesiensiL2,0'PPM2', " +
                        " '0'OutM3L3,'0'QtySPB_BBL3,'0'QtySPBL3,'0'EfesiensiL3,0'PPM3', " +
                        " '0'OutM3L4,'0'QtySPB_BBL4,'0'QtySPBL4,'0'EfesiensiL4,0'PPM4', " +
                        " '0'OutM3L5,'0'QtySPB_BBL5,'0'QtySPBL5,'0'EfesiensiL5,0'PPM5', " +
                        " '0'OutM3L6,'0'QtySPB_BBL6,'0'QtySPBL6,'0'EfesiensiL6,0'PPM6', " +
                        " '-'Keterangan ";
                        //ArrayList arrData2 = new ArrayList();
                        arrData2 = Fbm2.RetrieveReportFlo2(PeriodeBulanTahun, Periode, Query); Session["arrData2"] = arrData2;
                        lstMatrix.DataSource = arrData2;
                        lstMatrix.DataBind();
                    }
                }
                else if (user.UnitKerjaID == 7)
                {
                    if (FLagLap == "5")
                    {
                        btnExport.Visible = true; BtnPreview.Visible = false; btnLihat.Visible = false; btnCancel.Visible = true;
                        PanelLaporanKarawang.Visible = true; 
                        PanelCiteureup.Visible = false; 
                        PanelKarawang.Visible = false;
                        string Query = string.Empty;
                        Query =
                        " union all " +
                        " select '2'Urutan,''Tanggal, " +
                        " '0'OutM3L1,'0'QtyPakaiL1,'0'QtySPBL1,'0'EfesiensiL1,0'PPM1', " +
                        " '0'OutM3L2,'0'QtyPakaiL2,'0'QtySPBL2,'0'EfesiensiL2,0'PPM2', " +
                        " '0'OutM3L3,'0'QtyPakaiL3,'0'QtySPBL3,'0'EfesiensiL3,0'PPM3', " +
                        " '0'OutM3L4,'0'QtyPakaiL4,'0'QtySPBL4,'0'EfesiensiL4,0'PPM4', " +
                        " '0'OutM3L5,'0'QtyPakaiL5,'0'QtySPBL5,'0'EfesiensiL5,0'PPM5', " +
                        " '0'OutM3L6,'0'QtyPakaiL6,'0'QtySPBL6,'0'EfesiensiL6,0'PPM6','-'Keterangan ";
                        ArrayList arrDataLK = new ArrayList();
                        //arrDataLK = Fbm2.RetrieveReportFlo2Karawang(PeriodeBulanTahun, Periode, Query); Session["Data2"] = arrDataLK;
                        arrDataLK = Fbm2.RetrieveReportFloKarawangReport(PeriodeBulanTahun, Periode, Query); Session["Data2"] = arrDataLK;
                        lstMatrixLapK.DataSource = arrDataLK;
                        lstMatrixLapK.DataBind();
                    }
                    else
                    {
                        PanelLaporanKarawang.Visible = false; 
                        PanelCiteureup.Visible = false; 
                        PanelKarawang.Visible = true;
                        string Query = string.Empty;
                        Query =
                        " union all " +
                        " select '2'Urutan,''Tanggal, " +
                        " '0'OutM3L1,'0'QtyPakaiL1,'0'QtySPBL1,'0'EfesiensiL1,0'PPM1', " +
                        " '0'OutM3L2,'0'QtyPakaiL2,'0'QtySPBL2,'0'EfesiensiL2,0'PPM2', " +
                        " '0'OutM3L3,'0'QtyPakaiL3,'0'QtySPBL3,'0'EfesiensiL3,0'PPM3', " +
                        " '0'OutM3L4,'0'QtyPakaiL4,'0'QtySPBL4,'0'EfesiensiL4,0'PPM4', " +
                        " '0'OutM3L5,'0'QtyPakaiL5,'0'QtySPBL5,'0'EfesiensiL5,0'PPM5', " +
                        " '0'OutM3L6,'0'QtyPakaiL6,'0'QtySPBL6,'0'EfesiensiL6,0'PPM6','-'Keterangan ";

                        ArrayList arrDataK = new ArrayList();
                        arrDataK = Fbm2.RetrieveReportFlo2Karawang(PeriodeBulanTahun, Periode, Query); Session["DataK"] = arrDataK;
                        lstMatrixK2.DataSource = arrDataK;
                        lstMatrixK2.DataBind();
                    }
                }
            }
            #endregion
            #region belum input
            else if (CekInput == 0)
            {
                if (FLagLap == "5")
                {
                    DisplayAJAXMessage(this, "Silahkan input terlebih dahulu !!");
                    return;
                }


                btnSimpan.Visible = true; btnCancel.Visible = false; btnSimpan.Enabled = true;

                ArrayList arrData = new ArrayList();
                FacadeBMReport2 Fbm = new FacadeBMReport2();

                if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                {
                    arrData = Fbm.RetrieveReportFlo(Periode, PeriodeBulanTahun); 
                    
                    Session["Data"] = arrData;
                    lstMatrix.DataSource = arrData;
                    lstMatrix.DataBind();

                    if (arrData.Count > 0)
                    {
                        PanelCiteureup.Visible = true;
                    }

                    #region update sarmut
                    if (user.UnitKerjaID == 1)
                    {
                        SarmutOnSystem1();
                        SarmutOnSystem2();
                        SarmutOnSystem3();
                        SarmutOnSystem4();
                    }
                    #endregion

                }
                else if (user.UnitKerjaID == 7)
                {
                    arrData = Fbm.RetrieveReportFloKarawang(Periode, PeriodeBulanTahun); 
                    Session["Data"] = arrData;
                    lstMatrixK2.DataSource = arrData;
                    lstMatrixK2.DataBind();
                }
            }
            #endregion
        }
        private decimal TotalPakai = 0;
        private decimal TotalStd = 0;

        protected void lstMatrix_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users user = (Users)Session["Users"];

            #region Citeureup
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                for (int i = 0; i < lstMatrix.Items.Count; i++)
                {

                    HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                    //string tgl = tr.Cells[0].InnerHtml;
                    //DomainBMReport2 bm = new DomainBMReport2();
                    //FacadeBMReport2 fbm = new FacadeBMReport2();
                    //bm = fbm.Retrievetgl(tgl);              


                    LabelStatus.Visible = true;
                    LabelStatus.Text = "Status Laporan : Open";
                    LabelStatus.Attributes["style"] = "color:green; font-weight:bold;";
                    LabelStatus.Font.Name = "calibri";

                    if (tr.Cells[0].InnerHtml.Trim().Contains("Pemakaian"))
                    {
                        tr.Cells[0].Attributes["style"] = "text-align:right;";
                    }

                    if (tr.Cells[0].InnerHtml == "Total")
                    {
                        tr.Cells[0].Attributes["style"] = "font-weight:bold;";
                        tr.Cells[0].Attributes.Add("style", "background-color:grey;");
                        tr.Cells[0].InnerHtml = "TOTAL";
                    }
                }
                #endregion

                string TglAlias = Session["Tgl"].ToString();
                DomainBMReport2 DS = new DomainBMReport2();
                FacadeBMReport2 FS = new FacadeBMReport2();
                DS = FS.RetrieveSign(user.UnitKerjaID);
                Session["admin"] = DS.AdminSign;
                Session["manager"] = DS.MgrSign;
            }
        }
        protected void lstMatrixLapC_DataBound(object sender, RepeaterItemEventArgs e)
        {
            decimal totalOutL1 = 0; decimal totalSPBL1 = 0; decimal totalEfesiensiL1 = 0; decimal pakaiBBL1 = 0; decimal PPML1 = 0;
            decimal totalOutL2 = 0; decimal totalSPBL2 = 0; decimal totalEfesiensiL2 = 0; decimal pakaiBBL2 = 0; decimal PPML2 = 0;
            decimal totalOutL3 = 0; decimal totalSPBL3 = 0; decimal totalEfesiensiL3 = 0; decimal pakaiBBL3 = 0; decimal PPML3 = 0;
            decimal totalOutL4 = 0; decimal totalSPBL4 = 0; decimal totalEfesiensiL4 = 0; decimal pakaiBBL4 = 0; decimal PPML4 = 0;
            decimal totalPakaiL1 = 0; decimal totalPakaiL2 = 0; decimal totalPakaiL3 = 0; decimal totalPakaiL4 = 0;


            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                for (int i = 0; i < lstMatrixLapC.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrixLapC.Items[i].FindControl("lstLC");
                    //HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                    //string tgl = tr.Cells[0].InnerHtml;
                    //DomainBMReport2 bm = new DomainBMReport2();
                    //FacadeBMReport2 fbm = new FacadeBMReport2();
                    //bm = fbm.Retrievetgl(tgl);


                    PPML1 += Convert.ToDecimal(tr.Cells[5].InnerHtml);
                    PPML2 += Convert.ToDecimal(tr.Cells[10].InnerHtml);
                    PPML3 += Convert.ToDecimal(tr.Cells[15].InnerHtml);
                    PPML4 += Convert.ToDecimal(tr.Cells[20].InnerHtml);


                    LabelStatus.Visible = true; LabelStatus.Attributes["style"] = "color:blue; font-weight:bold;"; LabelStatus.Font.Name = "calibri";
                    LabelStatus.Text = "Status Laporan : Siap Cetak";

                    totalOutL1 += tr.Cells[1].InnerHtml != "" ? decimal.Parse(tr.Cells[1].InnerHtml) : 0;
                    totalPakaiL1 += tr.Cells[2].InnerHtml != "" ? decimal.Parse(tr.Cells[2].InnerHtml) : 0;
                    totalSPBL1 += tr.Cells[3].InnerHtml != "" ? decimal.Parse(tr.Cells[3].InnerHtml) : 0;
                    totalEfesiensiL1 += tr.Cells[4].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[4].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL2 += tr.Cells[6].InnerHtml != "" ? decimal.Parse(tr.Cells[6].InnerHtml) : 0;
                    totalPakaiL2 += tr.Cells[7].InnerHtml != "" ? decimal.Parse(tr.Cells[7].InnerHtml) : 0;
                    totalSPBL2 += tr.Cells[8].InnerHtml != "" ? decimal.Parse(tr.Cells[8].InnerHtml) : 0;
                    totalEfesiensiL2 += tr.Cells[9].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[9].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL3 += tr.Cells[11].InnerHtml != "" ? decimal.Parse(tr.Cells[11].InnerHtml) : 0;
                    totalPakaiL3 += tr.Cells[12].InnerHtml != "" ? decimal.Parse(tr.Cells[12].InnerHtml) : 0;
                    totalSPBL3 += tr.Cells[13].InnerHtml != "" ? decimal.Parse(tr.Cells[13].InnerHtml) : 0;
                    totalEfesiensiL3 += tr.Cells[14].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[14].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL4 += tr.Cells[16].InnerHtml != "" ? decimal.Parse(tr.Cells[16].InnerHtml) : 0;
                    totalPakaiL4 += tr.Cells[17].InnerHtml != "" ? decimal.Parse(tr.Cells[17].InnerHtml) : 0;
                    totalSPBL4 += tr.Cells[18].InnerHtml != "" ? decimal.Parse(tr.Cells[18].InnerHtml) : 0;
                    totalEfesiensiL4 += tr.Cells[19].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[19].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;


                }

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstFLC");
                trfooter.Cells[0].InnerHtml = "&nbsp;&nbsp;" + "<b> TOTAL </b>";

                trfooter.Cells[1].InnerHtml = totalOutL1.ToString("N0");
                trfooter.Cells[2].InnerHtml = totalPakaiL1.ToString("N0");
                trfooter.Cells[3].InnerHtml = totalSPBL1.ToString("N0");
                trfooter.Cells[4].InnerHtml = totalEfesiensiL1.ToString("N2");
                trfooter.Cells[5].InnerHtml = PPML1.ToString("N0");

                trfooter.Cells[6].InnerHtml = totalOutL2.ToString("N0");
                trfooter.Cells[7].InnerHtml = totalPakaiL2.ToString("N0");
                trfooter.Cells[8].InnerHtml = totalSPBL2.ToString("N0");
                trfooter.Cells[9].InnerHtml = totalEfesiensiL2.ToString("N2");
                trfooter.Cells[10].InnerHtml = PPML2.ToString("N0");

                trfooter.Cells[11].InnerHtml = totalOutL3.ToString("N0");
                trfooter.Cells[12].InnerHtml = totalPakaiL3.ToString("N0");
                trfooter.Cells[13].InnerHtml = totalSPBL3.ToString("N0");
                trfooter.Cells[14].InnerHtml = totalEfesiensiL3.ToString("N2");
                trfooter.Cells[15].InnerHtml = PPML3.ToString("N0");

                trfooter.Cells[16].InnerHtml = totalOutL4.ToString("N0");
                trfooter.Cells[17].InnerHtml = totalPakaiL4.ToString("N0");
                trfooter.Cells[18].InnerHtml = totalSPBL4.ToString("N0");
                trfooter.Cells[19].InnerHtml = totalEfesiensiL4.ToString("N2");
                trfooter.Cells[20].InnerHtml = PPML4.ToString("N0");

                Users user = (Users)Session["Users"];
                string TglAlias = Session["Tgl"].ToString();
                DomainBMReport2 DS = new DomainBMReport2();
                FacadeBMReport2 FS = new FacadeBMReport2();
                DS = FS.RetrieveSign(user.UnitKerjaID);
                Session["admin"] = DS.AdminSign;
                Session["manager"] = DS.MgrSign;

            }
        }
        protected void lstMatrixLapK_DataBound(object sender, RepeaterItemEventArgs e)
        {
            decimal totalOutL1 = 0; decimal totalSPBL1 = 0; decimal totalEfesiensiL1 = 0; decimal pakaiBBL1 = 0; decimal PPML1 = 0; decimal totalSPB_BBL1 = 0;
            decimal totalOutL2 = 0; decimal totalSPBL2 = 0; decimal totalEfesiensiL2 = 0; decimal pakaiBBL2 = 0; decimal PPML2 = 0; decimal totalSPB_BBL2 = 0;
            decimal totalOutL3 = 0; decimal totalSPBL3 = 0; decimal totalEfesiensiL3 = 0; decimal pakaiBBL3 = 0; decimal PPML3 = 0; decimal totalSPB_BBL3 = 0;
            decimal totalOutL4 = 0; decimal totalSPBL4 = 0; decimal totalEfesiensiL4 = 0; decimal pakaiBBL4 = 0; decimal PPML4 = 0; decimal totalSPB_BBL4 = 0;
            decimal totalOutL5 = 0; decimal totalSPBL5 = 0; decimal totalEfesiensiL5 = 0; decimal pakaiBBL5 = 0; decimal PPML5 = 0; decimal totalSPB_BBL5 = 0;
            decimal totalOutL6 = 0; decimal totalSPBL6 = 0; decimal totalEfesiensiL6 = 0; decimal pakaiBBL6 = 0; decimal PPML6 = 0; decimal totalSPB_BBL6 = 0;


            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                for (int i = 0; i < lstMatrixLapK.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrixLapK.Items[i].FindControl("lstLK");
                    //string tgl = tr.Cells[0].InnerHtml;
                    //DomainBMReport2 bm = new DomainBMReport2();
                    //FacadeBMReport2 fbm = new FacadeBMReport2();
                    //bm = fbm.Retrievetgl(tgl);


                    PPML1 += Convert.ToDecimal(tr.Cells[5].InnerHtml);
                    PPML2 += Convert.ToDecimal(tr.Cells[10].InnerHtml);
                    PPML3 += Convert.ToDecimal(tr.Cells[15].InnerHtml);
                    PPML4 += Convert.ToDecimal(tr.Cells[20].InnerHtml);
                    PPML5 += Convert.ToDecimal(tr.Cells[25].InnerHtml);
                    PPML6 += Convert.ToDecimal(tr.Cells[30].InnerHtml);

                    LabelStatus.Visible = true; LabelStatus.Attributes["style"] = "color:blue; font-weight:bold;"; LabelStatus.Font.Name = "calibri";
                    LabelStatus.Text = "Status Laporan : Siap Cetak";

                    totalOutL1 += tr.Cells[1].InnerHtml != "" ? decimal.Parse(tr.Cells[1].InnerHtml) : 0;
                    totalSPB_BBL1 += tr.Cells[2].InnerHtml != "" ? decimal.Parse(tr.Cells[2].InnerHtml) : 0;
                    totalSPBL1 += tr.Cells[3].InnerHtml != "" ? decimal.Parse(tr.Cells[3].InnerHtml) : 0;
                    totalEfesiensiL1 += tr.Cells[4].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[4].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL2 += tr.Cells[6].InnerHtml != "" ? decimal.Parse(tr.Cells[6].InnerHtml) : 0;
                    totalSPB_BBL2 += tr.Cells[7].InnerHtml != "" ? decimal.Parse(tr.Cells[7].InnerHtml) : 0;
                    totalSPBL2 += tr.Cells[8].InnerHtml != "" ? decimal.Parse(tr.Cells[8].InnerHtml) : 0;
                    totalEfesiensiL2 += tr.Cells[9].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[9].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL3 += tr.Cells[11].InnerHtml != "" ? decimal.Parse(tr.Cells[11].InnerHtml) : 0;
                    totalSPB_BBL3 += tr.Cells[12].InnerHtml != "" ? decimal.Parse(tr.Cells[12].InnerHtml) : 0;
                    totalSPBL3 += tr.Cells[13].InnerHtml != "" ? decimal.Parse(tr.Cells[13].InnerHtml) : 0;
                    totalEfesiensiL3 += tr.Cells[14].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[14].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL4 += tr.Cells[16].InnerHtml != "" ? decimal.Parse(tr.Cells[16].InnerHtml) : 0;
                    totalSPB_BBL4 += tr.Cells[15].InnerHtml != "" ? decimal.Parse(tr.Cells[15].InnerHtml) : 0;
                    totalSPBL4 += tr.Cells[18].InnerHtml != "" ? decimal.Parse(tr.Cells[18].InnerHtml) : 0;
                    totalEfesiensiL4 += tr.Cells[19].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[19].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL5 += tr.Cells[21].InnerHtml != "" ? decimal.Parse(tr.Cells[21].InnerHtml) : 0;
                    totalSPB_BBL6 += tr.Cells[22].InnerHtml != "" ? decimal.Parse(tr.Cells[22].InnerHtml) : 0;
                    totalSPBL5 += tr.Cells[23].InnerHtml != "" ? decimal.Parse(tr.Cells[23].InnerHtml) : 0;
                    totalEfesiensiL5 += tr.Cells[24].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[24].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL6 += tr.Cells[26].InnerHtml != "" ? decimal.Parse(tr.Cells[26].InnerHtml) : 0;
                    totalSPB_BBL6 += tr.Cells[25].InnerHtml != "" ? decimal.Parse(tr.Cells[25].InnerHtml) : 0;
                    totalSPBL6 += tr.Cells[28].InnerHtml != "" ? decimal.Parse(tr.Cells[28].InnerHtml) : 0;
                    totalEfesiensiL6 += tr.Cells[29].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[29].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;


                }

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstFLK");
                trfooter.Cells[0].InnerHtml = "&nbsp;&nbsp;" + "<b> TOTAL </b>";

                trfooter.Cells[1].InnerHtml = totalOutL1.ToString("N0");
                trfooter.Cells[2].InnerHtml = totalSPB_BBL1.ToString("N0");
                trfooter.Cells[3].InnerHtml = totalSPBL1.ToString("N0");
                trfooter.Cells[4].InnerHtml = totalEfesiensiL1.ToString("N2");
                trfooter.Cells[5].InnerHtml = PPML1.ToString("N0");

                trfooter.Cells[6].InnerHtml = totalOutL2.ToString("N0");
                trfooter.Cells[7].InnerHtml = totalSPB_BBL2.ToString("N0");
                trfooter.Cells[8].InnerHtml = totalSPBL2.ToString("N0");
                trfooter.Cells[9].InnerHtml = totalEfesiensiL2.ToString("N2");
                trfooter.Cells[10].InnerHtml = PPML2.ToString("N0");

                trfooter.Cells[11].InnerHtml = totalOutL3.ToString("N0");
                trfooter.Cells[12].InnerHtml = totalSPB_BBL3.ToString("N0");
                trfooter.Cells[13].InnerHtml = totalSPBL3.ToString("N0");
                trfooter.Cells[14].InnerHtml = totalEfesiensiL3.ToString("N2");
                trfooter.Cells[15].InnerHtml = PPML3.ToString("N0");

                trfooter.Cells[16].InnerHtml = totalOutL4.ToString("N0");
                trfooter.Cells[17].InnerHtml = totalSPB_BBL4.ToString("N0");
                trfooter.Cells[18].InnerHtml = totalSPBL4.ToString("N0");
                trfooter.Cells[19].InnerHtml = totalEfesiensiL4.ToString("N2");
                trfooter.Cells[20].InnerHtml = PPML4.ToString("N0");

                trfooter.Cells[21].InnerHtml = totalOutL5.ToString("N0");
                trfooter.Cells[22].InnerHtml = totalSPB_BBL5.ToString("N0");
                trfooter.Cells[23].InnerHtml = totalSPBL5.ToString("N0");
                trfooter.Cells[24].InnerHtml = totalEfesiensiL5.ToString("N2");
                trfooter.Cells[25].InnerHtml = PPML5.ToString("N0");

                trfooter.Cells[26].InnerHtml = totalOutL6.ToString("N0");
                trfooter.Cells[27].InnerHtml = totalSPB_BBL6.ToString("N0");
                trfooter.Cells[28].InnerHtml = totalSPBL6.ToString("N0");
                trfooter.Cells[29].InnerHtml = totalEfesiensiL6.ToString("N2");
                trfooter.Cells[30].InnerHtml = PPML6.ToString("N0");

                Users user = (Users)Session["Users"];
                string TglAlias = Session["Tgl"].ToString();
                DomainBMReport2 DS = new DomainBMReport2();
                FacadeBMReport2 FS = new FacadeBMReport2();
                DS = FS.RetrieveSign(user.UnitKerjaID);
                Session["admin"] = DS.AdminSign;
                Session["manager"] = DS.MgrSign;

            }
        }

        protected void lstMatrixK2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users user = (Users)Session["Users"];

            #region Karawang
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                for (int i = 0; i < lstMatrixK2.Items.Count; i++)
                {

                    HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");

                    LabelStatus.Visible = true;
                    LabelStatus.Text = "Status Laporan : Open";
                    LabelStatus.Attributes["style"] = "color:green; font-weight:bold;";
                    LabelStatus.Font.Name = "calibri";

                    if (tr.Cells[0].InnerHtml.Trim().Contains("Pemakaian"))
                    {
                        tr.Cells[0].Attributes["style"] = "text-align:right;";
                    }

                    if (tr.Cells[0].InnerHtml == "Total")
                    {
                        tr.Cells[0].Attributes["style"] = "font-weight:bold;";
                        tr.Cells[0].Attributes.Add("style", "background-color:grey;");
                        tr.Cells[0].InnerHtml = "TOTAL";
                    }
                }
                #endregion

                string TglAlias = Session["Tgl"].ToString();
                DomainBMReport2 DS = new DomainBMReport2();
                FacadeBMReport2 FS = new FacadeBMReport2();
                DS = FS.RetrieveSign(user.UnitKerjaID);
                Session["admin"] = DS.AdminSign;
                Session["manager"] = DS.MgrSign;
            }
        }

        protected void lstMatrixK2260922_DataBound(object sender, RepeaterItemEventArgs e)
        {
            decimal totalOutL1 = 0; decimal totalSPBL1 = 0; decimal totalEfesiensiL1 = 0; decimal pakaiBBL1 = 0; decimal PPML1 = 0;
            decimal totalOutL2 = 0; decimal totalSPBL2 = 0; decimal totalEfesiensiL2 = 0; decimal pakaiBBL2 = 0; decimal PPML2 = 0;
            decimal totalOutL3 = 0; decimal totalSPBL3 = 0; decimal totalEfesiensiL3 = 0; decimal pakaiBBL3 = 0; decimal PPML3 = 0;
            decimal totalOutL4 = 0; decimal totalSPBL4 = 0; decimal totalEfesiensiL4 = 0; decimal pakaiBBL4 = 0; decimal PPML4 = 0;
            decimal totalOutL5 = 0; decimal totalSPBL5 = 0; decimal totalEfesiensiL5 = 0; decimal pakaiBBL5 = 0; decimal PPML5 = 0;
            decimal totalOutL6 = 0; decimal totalSPBL6 = 0; decimal totalEfesiensiL6 = 0; decimal pakaiBBL6 = 0; decimal PPML6 = 0;

            Users user = (Users)Session["Users"];
            string ThnBln = Session["PeriodeBulanTahun"].ToString();

            #region Karawang
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                for (int i = 0; i < lstMatrixK2.Items.Count; i++)
                {
                    //DomainBMReport domainView = new DomainBMReport();
                    //DomainBMReport bm2 = new DomainBMReport();

                    Label txtjml = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL1K");
                    Label txtjml2 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL2K");
                    Label txtjml3 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL3K");
                    Label txtjml4 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL4K");
                    Label txtjml5 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL5K");
                    Label txtjml6 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL6K");

                    Label LKet = (Label)lstMatrixK2.Items[i].FindControl("txtKeterangan");

                    Label txtjmlA = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL1_AliasK");
                    Label txtjml2A = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL2_AliasK");
                    Label txtjml3A = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL3_AliasK");
                    Label txtjml4A = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL4_AliasK");
                    Label txtjml5A = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL5_AliasK");
                    Label txtjml6A = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL6_AliasK");

                    ImageButton update = (ImageButton)lstMatrixK2.Items[i].FindControl("update");
                    ImageButton updatehide = (ImageButton)lstMatrixK2.Items[i].FindControl("updatehide");
                    ImageButton simpan = (ImageButton)lstMatrixK2.Items[i].FindControl("simpan");
                    ImageButton simpanhide = (ImageButton)lstMatrixK2.Items[i].FindControl("savehide");

                    HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                    string tgl = tr.Cells[0].InnerHtml;
                    DomainBMReport2 bm = new DomainBMReport2();
                    FacadeBMReport2 fbm = new FacadeBMReport2();
                    bm = fbm.Retrievetgl(tgl);
                    //bm = fbm.Retrievetgl(ThnBln);

                    #region Sudah ada inputan
                    if (bm.ID > 0)
                    {
                        if (user.UnitKerjaID == 7)
                        {
                            simpan.Visible = false;
                            update.Visible = true; update.Enabled = true;

                            updatehide.Visible = false;
                            simpanhide.Visible = true; simpanhide.Enabled = false;

                            tr.Cells[5].InnerHtml = Convert.ToDecimal(bm.PPML1).ToString("N0");
                            tr.Cells[10].InnerHtml = Convert.ToDecimal(bm.PPML2).ToString("N0");
                            tr.Cells[15].InnerHtml = Convert.ToDecimal(bm.PPML3).ToString("N0");
                            tr.Cells[20].InnerHtml = Convert.ToDecimal(bm.PPML4).ToString("N0");
                            tr.Cells[25].InnerHtml = Convert.ToDecimal(bm.PPML5).ToString("N0");
                            tr.Cells[30].InnerHtml = Convert.ToDecimal(bm.PPML6).ToString("N0");

                            TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1K");
                            TextBox jmlL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2K");
                            TextBox jmlL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3K");
                            TextBox jmlL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4K");
                            TextBox jmlL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5K");
                            TextBox jmlL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6K");

                            TextBox jmlalias = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1_AliasK");
                            TextBox jmlaliasL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2_AliasK");
                            TextBox jmlaliasL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3_AliasK");
                            TextBox jmlaliasL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4_AliasK");
                            TextBox jmlaliasL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5_AliasK");
                            TextBox jmlaliasL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6_AliasK");

                            TextBox ket = (TextBox)lstMatrixK2.Items[i].FindControl("Keterangan");

                            ket.Enabled = false;
                            ket.Text = bm.Keterangan.Trim();
                            jml.Visible = true; jmlalias.Visible = false; jml.Enabled = false;
                            jmlL2.Visible = true; jmlaliasL2.Visible = false; jmlL2.Enabled = false;
                            jmlL3.Visible = true; jmlaliasL3.Visible = false; jmlL3.Enabled = false;
                            jmlL4.Visible = true; jmlaliasL4.Visible = false; jmlL4.Enabled = false;
                            jmlL5.Visible = true; jmlaliasL5.Visible = false; jmlL5.Enabled = false;
                            jmlL6.Visible = true; jmlaliasL6.Visible = false; jmlL6.Enabled = false;

                            LKet.Text = ket.Text.Trim();

                            txtjml.Text = jml.Text;
                            txtjml2.Text = jmlL2.Text;
                            txtjml3.Text = jmlL3.Text;
                            txtjml4.Text = jmlL4.Text;
                            txtjml5.Text = jmlL5.Text;
                            txtjml6.Text = jmlL6.Text;

                            txtjmlA.Text = jmlalias.Text.Trim();
                            txtjml2A.Text = jmlaliasL2.Text.Trim();
                            txtjml3A.Text = jmlaliasL3.Text.Trim();
                            txtjml4A.Text = jmlaliasL4.Text.Trim();
                            txtjml5A.Text = jmlaliasL5.Text.Trim();
                            txtjml6A.Text = jmlaliasL6.Text.Trim();

                            jml.Text = Convert.ToDecimal(bm.QtySPB_BBL1).ToString("N1");
                            jmlL2.Text = Convert.ToDecimal(bm.QtySPB_BBL2).ToString("N1");
                            jmlL3.Text = Convert.ToDecimal(bm.QtySPB_BBL3).ToString("N1");
                            jmlL4.Text = Convert.ToDecimal(bm.QtySPB_BBL4).ToString("N1");
                            jmlL5.Text = Convert.ToDecimal(bm.QtySPB_BBL5).ToString("N1");
                            jmlL6.Text = Convert.ToDecimal(bm.QtySPB_BBL6).ToString("N1");

                            string QtyPakaiBB = jml.Text.ToString() != "" ? jml.Text.ToString() : "0";
                            string QtyPakaiBBL2 = jmlL2.Text.ToString() != "" ? jmlL2.Text.ToString() : "0";
                            string QtyPakaiBBL3 = jmlL3.Text.ToString() != "" ? jmlL3.Text.ToString() : "0";
                            string QtyPakaiBBL4 = jmlL4.Text.ToString() != "" ? jmlL4.Text.ToString() : "0";
                            string QtyPakaiBBL5 = jmlL5.Text.ToString() != "" ? jmlL5.Text.ToString() : "0";
                            string QtyPakaiBBL6 = jmlL6.Text.ToString() != "" ? jmlL6.Text.ToString() : "0";

                            decimal QtyPakaiBBD = Convert.ToDecimal(QtyPakaiBB);
                            decimal QtyPakaiBBDL2 = Convert.ToDecimal(QtyPakaiBBL2);
                            decimal QtyPakaiBBDL3 = Convert.ToDecimal(QtyPakaiBBL3);
                            decimal QtyPakaiBBDL4 = Convert.ToDecimal(QtyPakaiBBL4);
                            decimal QtyPakaiBBDL5 = Convert.ToDecimal(QtyPakaiBBL5);
                            decimal QtyPakaiBBDL6 = Convert.ToDecimal(QtyPakaiBBL6);

                            pakaiBBL1 += QtyPakaiBBD;
                            pakaiBBL2 += QtyPakaiBBDL2;
                            pakaiBBL3 += QtyPakaiBBDL3;
                            pakaiBBL4 += QtyPakaiBBDL4;
                            pakaiBBL5 += QtyPakaiBBDL5;
                            pakaiBBL6 += QtyPakaiBBDL6;

                            PPML1 += Convert.ToDecimal(tr.Cells[5].InnerHtml);
                            PPML2 += Convert.ToDecimal(tr.Cells[10].InnerHtml);
                            PPML3 += Convert.ToDecimal(tr.Cells[15].InnerHtml);
                            PPML4 += Convert.ToDecimal(tr.Cells[20].InnerHtml);
                            PPML5 += Convert.ToDecimal(tr.Cells[25].InnerHtml);
                            PPML6 += Convert.ToDecimal(tr.Cells[30].InnerHtml);

                            LabelStatus.Visible = true; LabelStatus.Attributes["style"] = "color:blue; font-weight:bold;"; LabelStatus.Font.Name = "calibri";
                            LabelStatus.Text = "Status Laporan : Release";
                        }

                        tr.Cells[2].Attributes.Add("style", "background-color:White;");
                        tr.Cells[7].Attributes.Add("style", "background-color:White;");
                        tr.Cells[12].Attributes.Add("style", "background-color:White;");
                        tr.Cells[17].Attributes.Add("style", "background-color:White;");
                        tr.Cells[22].Attributes.Add("style", "background-color:White;");
                        tr.Cells[27].Attributes.Add("style", "background-color:White;");
                        tr.Cells[31].Attributes.Add("style", "background-color:White;");

                    }


                    #endregion
                    #region Belum ada inputan
                    else if (bm.ID == 0)
                    {
                        simpan.Visible = false;
                        update.Visible = false;
                        updatehide.Visible = true; updatehide.Enabled = false;
                        simpanhide.Visible = true; simpanhide.Enabled = false;

                        TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1K");
                        TextBox jmlL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2K");
                        TextBox jmlL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3K");
                        TextBox jmlL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4K");
                        TextBox jmlL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5K");
                        TextBox jmlL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6K");

                        txtjml.Text = jml.Text;
                        txtjml2.Text = jmlL2.Text;
                        txtjml3.Text = jmlL3.Text;
                        txtjml4.Text = jmlL4.Text;
                        txtjml5.Text = jmlL5.Text;
                        txtjml6.Text = jmlL6.Text;

                        string QtyPakaiBB = jml.Text.ToString() != "" ? jml.Text.ToString() : "0";
                        string QtyPakaiBBL2 = jmlL2.Text.ToString() != "" ? jmlL2.Text.ToString() : "0";
                        string QtyPakaiBBL3 = jmlL3.Text.ToString() != "" ? jmlL3.Text.ToString() : "0";
                        string QtyPakaiBBL4 = jmlL4.Text.ToString() != "" ? jmlL4.Text.ToString() : "0";
                        string QtyPakaiBBL5 = jmlL5.Text.ToString() != "" ? jmlL5.Text.ToString() : "0";
                        string QtyPakaiBBL6 = jmlL6.Text.ToString() != "" ? jmlL6.Text.ToString() : "0";

                        decimal QtyPakaiBBD = Convert.ToDecimal(QtyPakaiBB);
                        decimal QtyPakaiBBDL2 = Convert.ToDecimal(QtyPakaiBBL2);
                        decimal QtyPakaiBBDL3 = Convert.ToDecimal(QtyPakaiBBL3);
                        decimal QtyPakaiBBDL4 = Convert.ToDecimal(QtyPakaiBBL4);
                        decimal QtyPakaiBBDL5 = Convert.ToDecimal(QtyPakaiBBL5);
                        decimal QtyPakaiBBDL6 = Convert.ToDecimal(QtyPakaiBBL6);

                        pakaiBBL1 += QtyPakaiBBD;
                        pakaiBBL2 += QtyPakaiBBDL2;
                        pakaiBBL3 += QtyPakaiBBDL3;
                        pakaiBBL4 += QtyPakaiBBDL4;
                        pakaiBBL5 += QtyPakaiBBDL5;
                        pakaiBBL6 += QtyPakaiBBDL6;

                        LabelStatus.Visible = true;
                        LabelStatus.Text = "Status Laporan : Open";
                        LabelStatus.Attributes["style"] = "color:green; font-weight:bold;";
                        LabelStatus.Font.Name = "calibri";
                    }
                    #endregion

                    totalOutL1 += tr.Cells[1].InnerHtml != "" ? decimal.Parse(tr.Cells[1].InnerHtml) : 0;
                    totalSPBL1 += tr.Cells[2].InnerHtml != "" ? decimal.Parse(tr.Cells[2].InnerHtml) : 0;
                    //pakaiBBL1 += tr.Cells[3].InnerHtml != "" ? decimal.Parse(tr.Cells[3].InnerHtml) : 0;
                    totalEfesiensiL1 += tr.Cells[4].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[4].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL2 += tr.Cells[6].InnerHtml != "" ? decimal.Parse(tr.Cells[6].InnerHtml) : 0;
                    totalSPBL2 += tr.Cells[7].InnerHtml != "" ? decimal.Parse(tr.Cells[7].InnerHtml) : 0;
                    totalEfesiensiL2 += tr.Cells[9].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[9].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL3 += tr.Cells[11].InnerHtml != "" ? decimal.Parse(tr.Cells[11].InnerHtml) : 0;
                    totalSPBL3 += tr.Cells[12].InnerHtml != "" ? decimal.Parse(tr.Cells[12].InnerHtml) : 0;
                    totalEfesiensiL3 += tr.Cells[14].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[14].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL4 += tr.Cells[16].InnerHtml != "" ? decimal.Parse(tr.Cells[16].InnerHtml) : 0;
                    totalSPBL4 += tr.Cells[17].InnerHtml != "" ? decimal.Parse(tr.Cells[17].InnerHtml) : 0;
                    totalEfesiensiL4 += tr.Cells[19].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[19].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL5 += tr.Cells[21].InnerHtml != "" ? decimal.Parse(tr.Cells[21].InnerHtml) : 0;
                    totalSPBL5 += tr.Cells[22].InnerHtml != "" ? decimal.Parse(tr.Cells[22].InnerHtml) : 0;
                    totalEfesiensiL5 += tr.Cells[24].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[24].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

                    totalOutL6 += tr.Cells[26].InnerHtml != "" ? decimal.Parse(tr.Cells[26].InnerHtml) : 0;
                    totalSPBL6 += tr.Cells[27].InnerHtml != "" ? decimal.Parse(tr.Cells[27].InnerHtml) : 0;
                    totalEfesiensiL6 += tr.Cells[29].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[29].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;



                    HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
                    trfooter.Cells[0].InnerHtml = "&nbsp;&nbsp;" + "<b> TOTAL </b>";

                    trfooter.Cells[1].InnerHtml = totalOutL1.ToString("N0");
                    trfooter.Cells[2].InnerHtml = totalSPBL1.ToString("N0");
                    trfooter.Cells[3].InnerHtml = pakaiBBL1.ToString("N20");
                    trfooter.Cells[4].InnerHtml = ((Convert.ToDecimal(totalSPBL1).ToString("N2") != "0,00") && (Convert.ToDecimal(totalOutL1).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBL1) / Convert.ToDecimal(totalOutL1)).ToString("N2") : "0.00";
                    trfooter.Cells[5].InnerHtml = PPML1.ToString("N0");

                    trfooter.Cells[6].InnerHtml = totalOutL2.ToString("N0");
                    trfooter.Cells[7].InnerHtml = totalSPBL2.ToString("N0");
                    trfooter.Cells[8].InnerHtml = pakaiBBL2.ToString("N2");
                    //trfooter.Cells[9].InnerHtml = totalEfesiensiL2.ToString("N2");
                    trfooter.Cells[9].InnerHtml = ((Convert.ToDecimal(totalSPBL2).ToString("N2") != "0,00") && (Convert.ToDecimal(totalOutL2).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBL2) / Convert.ToDecimal(totalOutL2)).ToString("N2") : "0.00";
                    trfooter.Cells[10].InnerHtml = PPML2.ToString("N0");

                    trfooter.Cells[11].InnerHtml = totalOutL3.ToString("N0");
                    trfooter.Cells[12].InnerHtml = totalSPBL3.ToString("N0");
                    trfooter.Cells[13].InnerHtml = pakaiBBL3.ToString("N2");
                    //trfooter.Cells[14].InnerHtml = totalEfesiensiL3.ToString("N2");
                    trfooter.Cells[14].InnerHtml = ((Convert.ToDecimal(totalSPBL3).ToString("N2") != "0,00") && (Convert.ToDecimal(totalOutL3).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBL3) / Convert.ToDecimal(totalOutL3)).ToString("N2") : "0.00";
                    trfooter.Cells[15].InnerHtml = PPML3.ToString("N0");

                    trfooter.Cells[16].InnerHtml = totalOutL4.ToString("N0");
                    trfooter.Cells[17].InnerHtml = totalSPBL4.ToString("N0");
                    trfooter.Cells[18].InnerHtml = pakaiBBL4.ToString("N2");
                    //trfooter.Cells[19].InnerHtml = totalEfesiensiL4.ToString("N2");
                    trfooter.Cells[19].InnerHtml = ((Convert.ToDecimal(totalSPBL4).ToString("N2") != "0,00") && (Convert.ToDecimal(totalOutL4).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBL4) / Convert.ToDecimal(totalOutL4)).ToString("N2") : "0.00";
                    trfooter.Cells[20].InnerHtml = PPML4.ToString("N0");

                    trfooter.Cells[21].InnerHtml = totalOutL5.ToString("N0");
                    trfooter.Cells[22].InnerHtml = totalSPBL5.ToString("N0");
                    trfooter.Cells[23].InnerHtml = pakaiBBL5.ToString("N2");
                    //trfooter.Cells[24].InnerHtml = totalEfesiensiL5.ToString("N2");
                    trfooter.Cells[24].InnerHtml = ((Convert.ToDecimal(totalSPBL5).ToString("N2") != "0,00") && (Convert.ToDecimal(totalOutL5).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBL5) / Convert.ToDecimal(totalOutL5)).ToString("N2") : "0.00";
                    trfooter.Cells[25].InnerHtml = PPML5.ToString("N0");

                    trfooter.Cells[26].InnerHtml = totalOutL6.ToString("N0");
                    trfooter.Cells[27].InnerHtml = totalSPBL6.ToString("N0");
                    trfooter.Cells[28].InnerHtml = pakaiBBL6.ToString("N2");
                    //trfooter.Cells[29].InnerHtml = totalEfesiensiL6.ToString("N2");
                    trfooter.Cells[29].InnerHtml = ((Convert.ToDecimal(totalSPBL6).ToString("N2") != "0,00") && (Convert.ToDecimal(totalOutL6).ToString("N2") != "0,00")) ? (Convert.ToDecimal(totalSPBL6) / Convert.ToDecimal(totalOutL6)).ToString("N2") : "0.00";
                    trfooter.Cells[30].InnerHtml = PPML6.ToString("N0");
                }
                #endregion

                string TglAlias = Session["Tgl"].ToString();
                DomainBMReport2 DS = new DomainBMReport2();
                FacadeBMReport2 FS = new FacadeBMReport2();
                DS = FS.RetrieveSign(user.UnitKerjaID);
                Session["admin"] = DS.AdminSign;
                Session["manager"] = DS.MgrSign;

                //Label1.Text = "Tes";
                //LabelTanggal.Text = "Citeureup, " + " " + TglAlias;
                //LabelAdmin.Text = "(______" + DS.AdminSign.Trim() + "______)";
                //LabelManager.Text = "(______" + DS.MgrSign.Trim() + "______)";
                //LabelPM.Text = "(______" + DS.PMSign.Trim() + "______)";

                //Label lblImage = item.FindControl("lblImage") as Label;
            }
        }

        //protected void lstMatrixK2_DataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    decimal totalOutL1 = 0; decimal totalSPBL1 = 0; decimal totalEfesiensiL1 = 0; decimal pakaiBBL1 = 0;
        //    decimal totalOutL2 = 0; decimal totalSPBL2 = 0; decimal totalEfesiensiL2 = 0; decimal pakaiBBL2 = 0;
        //    decimal totalOutL3 = 0; decimal totalSPBL3 = 0; decimal totalEfesiensiL3 = 0; decimal pakaiBBL3 = 0;
        //    decimal totalOutL4 = 0; decimal totalSPBL4 = 0; decimal totalEfesiensiL4 = 0; decimal pakaiBBL4 = 0;
        //    decimal totalOutL5 = 0; decimal totalSPBL5 = 0; decimal totalEfesiensiL5 = 0; decimal pakaiBBL5 = 0;
        //    decimal totalOutL6 = 0; decimal totalSPBL6 = 0; decimal totalEfesiensiL6 = 0; decimal pakaiBBL6 = 0;

        //    Users user = (Users)Session["Users"];
        //    for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //    {
        //        Label txtjml = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL1");
        //        Label txtjml2 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL2");
        //        Label txtjml3 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL3");
        //        Label txtjml4 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL4");
        //        Label txtKet = (Label)lstMatrixK2.Items[i].FindControl("txtKeterangan");
        //        TextBox Ket = (TextBox)lstMatrixK2.Items[i].FindControl("Keterangan");

        //        HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //        string tgl = tr.Cells[0].InnerHtml;
        //        DomainBMReport2 bm = new DomainBMReport2();
        //        FacadeBMReport2 fbm = new FacadeBMReport2();
        //        bm = fbm.Retrievetgl(tgl);

        //        if (bm.ID > 0)
        //        {

        //            tr.Cells[5].InnerHtml = bm.PPML1.ToString();
        //            tr.Cells[10].InnerHtml = bm.PPML2.ToString();
        //            tr.Cells[15].InnerHtml = bm.PPML3.ToString();
        //            tr.Cells[20].InnerHtml = bm.PPML4.ToString();

        //            TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1");
        //            TextBox jmlL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2");
        //            TextBox jmlL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3");
        //            TextBox jmlL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4");
        //            TextBox jmlL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5");
        //            TextBox jmlL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6");                

        //            Ket.Text = bm.Keterangan.Trim();
        //            txtKet.Text = Ket.Text;

        //            txtjml.Text = jml.Text;
        //            txtjml2.Text = jmlL2.Text;
        //            txtjml3.Text = jmlL3.Text;
        //            txtjml4.Text = jmlL4.Text;

        //            jml.Text = Convert.ToDecimal(bm.PakaiBBL1).ToString();
        //            jmlL2.Text = Convert.ToDecimal(bm.PakaiBBL2).ToString();
        //            jmlL3.Text = Convert.ToDecimal(bm.PakaiBBL3).ToString();
        //            jmlL4.Text = Convert.ToDecimal(bm.PakaiBBL4).ToString();
        //            jmlL5.Text = Convert.ToDecimal(bm.PakaiBBL5).ToString();
        //            jmlL6.Text = Convert.ToDecimal(bm.PakaiBBL6).ToString();

        //            string QtyPakaiBB = jml.Text.ToString() != "" ? jml.Text.ToString() : "0";
        //            string QtyPakaiBBL2 = jmlL2.Text.ToString() != "" ? jmlL2.Text.ToString() : "0";
        //            string QtyPakaiBBL3 = jmlL3.Text.ToString() != "" ? jmlL3.Text.ToString() : "0";
        //            string QtyPakaiBBL4 = jmlL4.Text.ToString() != "" ? jmlL4.Text.ToString() : "0";
        //            string QtyPakaiBBL5 = jmlL5.Text.ToString() != "" ? jmlL5.Text.ToString() : "0";
        //            string QtyPakaiBBL6 = jmlL6.Text.ToString() != "" ? jmlL6.Text.ToString() : "0";

        //            decimal QtyPakaiBBD = Convert.ToDecimal(QtyPakaiBB);
        //            decimal QtyPakaiBBDL2 = Convert.ToDecimal(QtyPakaiBBL2);
        //            decimal QtyPakaiBBDL3 = Convert.ToDecimal(QtyPakaiBBL3);
        //            decimal QtyPakaiBBDL4 = Convert.ToDecimal(QtyPakaiBBL4);
        //            decimal QtyPakaiBBDL5 = Convert.ToDecimal(QtyPakaiBBL5);
        //            decimal QtyPakaiBBDL6 = Convert.ToDecimal(QtyPakaiBBL6);

        //            pakaiBBL1 += QtyPakaiBBD;
        //            pakaiBBL2 += QtyPakaiBBDL2;
        //            pakaiBBL3 += QtyPakaiBBDL3;
        //            pakaiBBL4 += QtyPakaiBBDL4;
        //            pakaiBBL5 += QtyPakaiBBDL5;
        //            pakaiBBL6 += QtyPakaiBBDL6;

        //            string jumlahL1 = Convert.ToDecimal(bm.PakaiBBL1).ToString();

        //            LabelStatus.Visible = true; LabelStatus.Attributes["style"] = "color:blue; font-weight:bold;"; LabelStatus.Font.Name = "calibri";
        //            LabelStatus.Text = "Status Laporan : Release";
        //        }
        //        else if (bm.ID == 0)
        //        {
        //            TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1");
        //            TextBox jmlL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2");
        //            TextBox jmlL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3");
        //            TextBox jmlL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4");
        //            TextBox jmlL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5");
        //            TextBox jmlL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6");

        //            string QtyPakaiBB = jml.Text.ToString() != "" ? jml.Text.ToString() : "0";
        //            string QtyPakaiBBL2 = jmlL2.Text.ToString() != "" ? jmlL2.Text.ToString() : "0";
        //            string QtyPakaiBBL3 = jmlL3.Text.ToString() != "" ? jmlL3.Text.ToString() : "0";
        //            string QtyPakaiBBL4 = jmlL4.Text.ToString() != "" ? jmlL4.Text.ToString() : "0";
        //            string QtyPakaiBBL5 = jmlL5.Text.ToString() != "" ? jmlL5.Text.ToString() : "0";
        //            string QtyPakaiBBL6 = jmlL6.Text.ToString() != "" ? jmlL6.Text.ToString() : "0";

        //            decimal QtyPakaiBBD = Convert.ToDecimal(QtyPakaiBB);
        //            decimal QtyPakaiBBDL2 = Convert.ToDecimal(QtyPakaiBBL2);
        //            decimal QtyPakaiBBDL3 = Convert.ToDecimal(QtyPakaiBBL3);
        //            decimal QtyPakaiBBDL4 = Convert.ToDecimal(QtyPakaiBBL4);
        //            decimal QtyPakaiBBDL5 = Convert.ToDecimal(QtyPakaiBBL5);
        //            decimal QtyPakaiBBDL6 = Convert.ToDecimal(QtyPakaiBBL6);

        //            pakaiBBL1 += QtyPakaiBBD;
        //            pakaiBBL2 += QtyPakaiBBDL2;
        //            pakaiBBL3 += QtyPakaiBBDL3;
        //            pakaiBBL4 += QtyPakaiBBDL4;
        //            pakaiBBL5 += QtyPakaiBBDL5;
        //            pakaiBBL6 += QtyPakaiBBDL6;

        //            LabelStatus.Visible = true;
        //            LabelStatus.Text = "Status Laporan : Open";
        //            LabelStatus.Attributes["style"] = "color:green; font-weight:bold;";
        //            LabelStatus.Font.Name = "calibri";
        //        }

        //        totalOutL1 += tr.Cells[1].InnerHtml != "" ? decimal.Parse(tr.Cells[1].InnerHtml) : 0;
        //        totalSPBL1 += tr.Cells[3].InnerHtml != "" ? decimal.Parse(tr.Cells[3].InnerHtml) : 0;
        //        totalEfesiensiL1 += tr.Cells[4].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[4].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        totalOutL2 += tr.Cells[6].InnerHtml != "" ? decimal.Parse(tr.Cells[6].InnerHtml) : 0;
        //        totalSPBL2 += tr.Cells[8].InnerHtml != "" ? decimal.Parse(tr.Cells[8].InnerHtml) : 0;
        //        totalEfesiensiL2 += tr.Cells[9].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[9].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        totalOutL3 += tr.Cells[11].InnerHtml != "" ? decimal.Parse(tr.Cells[11].InnerHtml) : 0;
        //        totalSPBL3 += tr.Cells[13].InnerHtml != "" ? decimal.Parse(tr.Cells[13].InnerHtml) : 0;
        //        totalEfesiensiL3 += tr.Cells[14].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[14].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        totalOutL4 += tr.Cells[16].InnerHtml != "" ? decimal.Parse(tr.Cells[16].InnerHtml) : 0;
        //        totalSPBL4 += tr.Cells[18].InnerHtml != "" ? decimal.Parse(tr.Cells[18].InnerHtml) : 0;
        //        totalEfesiensiL4 += tr.Cells[19].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[19].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        totalOutL5 += tr.Cells[21].InnerHtml != "" ? decimal.Parse(tr.Cells[21].InnerHtml) : 0;
        //        totalSPBL5 += tr.Cells[23].InnerHtml != "" ? decimal.Parse(tr.Cells[23].InnerHtml) : 0;
        //        totalEfesiensiL5 += tr.Cells[24].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[24].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        totalOutL6 += tr.Cells[26].InnerHtml != "" ? decimal.Parse(tr.Cells[26].InnerHtml) : 0;
        //        totalSPBL6 += tr.Cells[28].InnerHtml != "" ? decimal.Parse(tr.Cells[28].InnerHtml) : 0;
        //        totalEfesiensiL6 += tr.Cells[29].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[29].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //        trfooter.Cells[0].InnerHtml = "&nbsp;&nbsp;" + "<b> TOTAL </b>";

        //        trfooter.Cells[1].InnerHtml = totalOutL1.ToString("N0");
        //        trfooter.Cells[2].InnerHtml = pakaiBBL1.ToString("N0");
        //        trfooter.Cells[3].InnerHtml = totalSPBL1.ToString("N0");
        //        trfooter.Cells[4].InnerHtml = totalEfesiensiL1.ToString("N2");

        //        trfooter.Cells[6].InnerHtml = totalOutL2.ToString("N0");
        //        trfooter.Cells[7].InnerHtml = pakaiBBL2.ToString("N0");
        //        trfooter.Cells[8].InnerHtml = totalSPBL2.ToString("N0");
        //        trfooter.Cells[9].InnerHtml = totalEfesiensiL2.ToString("N2");

        //        trfooter.Cells[11].InnerHtml = totalOutL3.ToString("N0");
        //        trfooter.Cells[12].InnerHtml = pakaiBBL3.ToString("N0");
        //        trfooter.Cells[13].InnerHtml = totalSPBL3.ToString("N0");
        //        trfooter.Cells[14].InnerHtml = totalEfesiensiL3.ToString("N2");

        //        trfooter.Cells[16].InnerHtml = totalOutL4.ToString("N0");
        //        trfooter.Cells[17].InnerHtml = pakaiBBL4.ToString("N0");
        //        trfooter.Cells[18].InnerHtml = totalSPBL4.ToString("N0");
        //        trfooter.Cells[19].InnerHtml = totalEfesiensiL4.ToString("N2");

        //        trfooter.Cells[21].InnerHtml = totalOutL5.ToString("N0");
        //        trfooter.Cells[22].InnerHtml = pakaiBBL5.ToString("N0");
        //        trfooter.Cells[23].InnerHtml = totalSPBL5.ToString("N0");
        //        trfooter.Cells[24].InnerHtml = totalEfesiensiL5.ToString("N2");

        //        trfooter.Cells[26].InnerHtml = totalOutL6.ToString("N0");
        //        trfooter.Cells[27].InnerHtml = pakaiBBL6.ToString("N0");
        //        trfooter.Cells[28].InnerHtml = totalSPBL6.ToString("N0");
        //        trfooter.Cells[29].InnerHtml = totalEfesiensiL6.ToString("N2");
        //    }

        //    string TglAlias = Session["Tgl"].ToString();
        //    DomainBMReport2 DS = new DomainBMReport2();
        //    FacadeBMReport2 FS = new FacadeBMReport2();
        //    DS = FS.RetrieveSign(user.UnitKerjaID);

        //    LabelTanggalK.Text = "Karawang, " + " " + TglAlias;
        //    LabelAdminK.Text = "(______" + DS.AdminSign.Trim() + "______)";
        //    LabelManagerK.Text = "(______" + DS.MgrSign.Trim() + "______)";
        //    LabelPMK.Text = "(______" + DS.PMSign.Trim() + "______)";

        //    //Label lblImage = item.FindControl("lblImage") as Label;
        //}

        protected void QtyPakaiLK1_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL1 = 0; decimal totalPPML1 = 0;
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1");
                if (jml.Text != string.Empty)
                {
                    string PakaiL1 = tr.Cells[3].InnerHtml != "" ? tr.Cells[3].InnerHtml : "0";
                    decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
                    tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jml.Text)) * 1000000).ToString("N0");
                }

                totalPakaiBBL1 += jml.Text != "" ? decimal.Parse(jml.Text) : 0;
                totalPPML1 += tr.Cells[5].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[5].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
                trfooter.Cells[2].InnerHtml = totalPakaiBBL1.ToString("N0");
                trfooter.Cells[5].InnerHtml = totalPPML1.ToString("N0");
            }
        }

        protected void QtyPakaiL1_Alias_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jmlalias = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL1_Alias");
                string jmlaliasS = jmlalias.Text.Replace(",", ".");

                if (jmlalias.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlalias.Text = string.Empty; jmlalias.Focus();
                    return;
                }

                if (jmlalias.Text != string.Empty)
                {
                    string PakaiL1 = tr.Cells[3].InnerHtml != "" ? tr.Cells[3].InnerHtml : "0";
                    decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
                    decimal OutputL1 = decimal.Parse(tr.Cells[1].InnerHtml);

                    if (DPakaiL1 == 0 || OutputL1 == 0)
                    {
                        tr.Cells[4].InnerHtml = "0,00";
                    }
                    else if (DPakaiL1 > 0 && OutputL1 > 0)
                    {
                        tr.Cells[4].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(tr.Cells[1].InnerHtml))).ToString("N2");
                    }


                    tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jmlaliasS)) * 1000000).ToString("N2");
                    //tr.Cells[4].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(tr.Cells[1].InnerHtml))).ToString("N2");
                    //tr.Cells[4].InnerHtml = DPakaiL1 > 0 ? DPakaiL1 : 0;


                }

                ImageButton update = (ImageButton)lstMatrix.Items[i].FindControl("update");
                ImageButton updatehide = (ImageButton)lstMatrix.Items[i].FindControl("updatehide");
                ImageButton simpan = (ImageButton)lstMatrix.Items[i].FindControl("simpan");
                ImageButton simpanhide = (ImageButton)lstMatrix.Items[i].FindControl("savehide");

                simpan.Enabled = true;

            }
        }

        protected void QtyPakaiL2_Alias_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jmlaliasL2 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL2_Alias");
                string jmlaliasL2S = jmlaliasL2.Text.Replace(",", ".");

                if (jmlaliasL2.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlaliasL2.Text = string.Empty; jmlaliasL2.Focus();
                    return;
                }

                if (jmlaliasL2.Text != string.Empty)
                {
                    string PakaiL2 = tr.Cells[8].InnerHtml != "" ? tr.Cells[8].InnerHtml : "0";
                    decimal DPakaiL2 = Convert.ToDecimal(PakaiL2);
                    decimal OutputL2 = decimal.Parse(tr.Cells[6].InnerHtml);

                    if (DPakaiL2 == 0 || OutputL2 == 0)
                    {
                        tr.Cells[9].InnerHtml = "0,00";
                    }
                    else if (DPakaiL2 > 0 && OutputL2 > 0)
                    {
                        tr.Cells[9].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(tr.Cells[6].InnerHtml))).ToString("N2");
                    }

                    tr.Cells[10].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(jmlaliasL2S)) * 1000000).ToString("N2");
                    //tr.Cells[9].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(tr.Cells[6].InnerHtml))).ToString("N2");
                }

                ImageButton update = (ImageButton)lstMatrix.Items[i].FindControl("update");
                ImageButton updatehide = (ImageButton)lstMatrix.Items[i].FindControl("updatehide");
                ImageButton simpan = (ImageButton)lstMatrix.Items[i].FindControl("simpan");
                ImageButton simpanhide = (ImageButton)lstMatrix.Items[i].FindControl("savehide");

                simpan.Enabled = true;

            }
        }

        protected void QtyPakaiL3_Alias_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jmlaliasL3 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL3_Alias");
                string jmlaliasL3S = jmlaliasL3.Text.Replace(",", ".");

                if (jmlaliasL3.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlaliasL3.Text = string.Empty; jmlaliasL3.Focus();
                    return;
                }

                if (jmlaliasL3.Text != string.Empty)
                {
                    string PakaiL3 = tr.Cells[13].InnerHtml != "" ? tr.Cells[13].InnerHtml : "0";
                    decimal DPakaiL3 = Convert.ToDecimal(PakaiL3);
                    decimal OutputL3 = decimal.Parse(tr.Cells[11].InnerHtml);

                    if (DPakaiL3 == 0 || OutputL3 == 0)
                    {
                        tr.Cells[14].InnerHtml = "0,00";
                    }
                    else if (DPakaiL3 > 0 && OutputL3 > 0)
                    {
                        tr.Cells[14].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(tr.Cells[11].InnerHtml))).ToString("N2");
                    }
                    tr.Cells[15].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(jmlaliasL3S)) * 1000000).ToString("N2");
                    //tr.Cells[14].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(tr.Cells[11].InnerHtml))).ToString("N2");
                }

                ImageButton update = (ImageButton)lstMatrix.Items[i].FindControl("update");
                ImageButton updatehide = (ImageButton)lstMatrix.Items[i].FindControl("updatehide");
                ImageButton simpan = (ImageButton)lstMatrix.Items[i].FindControl("simpan");
                ImageButton simpanhide = (ImageButton)lstMatrix.Items[i].FindControl("savehide");

                simpan.Enabled = true;

            }
        }

        protected void QtyPakaiL4_Alias_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jmlaliasL4 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL4_Alias");
                string jmlaliasL4S = jmlaliasL4.Text.Replace(",", ".");

                if (jmlaliasL4.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlaliasL4.Text = string.Empty; jmlaliasL4.Focus();
                    return;
                }

                if (jmlaliasL4.Text != string.Empty)
                {
                    string PakaiL4 = tr.Cells[18].InnerHtml != "" ? tr.Cells[18].InnerHtml : "0";
                    decimal DPakaiL4 = Convert.ToDecimal(PakaiL4);
                    decimal OutputL4 = decimal.Parse(tr.Cells[16].InnerHtml);

                    if (DPakaiL4 == 0 || OutputL4 == 0)
                    {
                        tr.Cells[19].InnerHtml = "0,00";
                    }
                    else if (DPakaiL4 > 0 && OutputL4 > 0)
                    {
                        tr.Cells[19].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(tr.Cells[16].InnerHtml))).ToString("N2");
                    }
                    tr.Cells[20].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(jmlaliasL4S)) * 1000000).ToString("N2");
                    //tr.Cells[19].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(tr.Cells[16].InnerHtml))).ToString("N2");
                }

                ImageButton update = (ImageButton)lstMatrix.Items[i].FindControl("update");
                ImageButton updatehide = (ImageButton)lstMatrix.Items[i].FindControl("updatehide");
                ImageButton simpan = (ImageButton)lstMatrix.Items[i].FindControl("simpan");
                ImageButton simpanhide = (ImageButton)lstMatrix.Items[i].FindControl("savehide");

                simpan.Enabled = true;

            }
        }

        protected void QtyPakaiL1_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL1 = 0; decimal totalPPML1 = 0;
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jml = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL1");
                string jmlS = jml.Text.Replace(",", ".");
                if (jml.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jml.Text = string.Empty; jml.Focus();
                    return;
                }
                if (jml.Text != string.Empty)
                {
                    string PakaiL1 = tr.Cells[3].InnerHtml != "" ? tr.Cells[3].InnerHtml : "0";
                    decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
                    tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jmlS)) * 1000000).ToString("N2");
                }
                totalPakaiBBL1 += jml.Text != "" ? decimal.Parse(jml.Text) : 0;
                totalPPML1 += tr.Cells[5].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[5].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
                trfooter.Cells[2].InnerHtml = totalPakaiBBL1.ToString("N2");
                trfooter.Cells[5].InnerHtml = totalPPML1.ToString("N0");
            }
        }
        protected void QtyPakaiL2_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL2 = 0; decimal totalPPML2 = 0;
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                TextBox jmlL2 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL2");
                string jmlL2S = jmlL2.Text.Replace(",", ".");
                if (jmlL2.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlL2.Text = string.Empty; jmlL2.Focus();
                    return;
                }
                if (jmlL2.Text != string.Empty)
                {
                    string PakaiL2 = tr.Cells[8].InnerHtml != "" ? tr.Cells[8].InnerHtml : "0";
                    decimal DPakaiL2 = Convert.ToDecimal(PakaiL2);
                    tr.Cells[10].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(jmlL2S)) * 1000000).ToString("N0");
                }
                totalPakaiBBL2 += jmlL2.Text != "" ? decimal.Parse(jmlL2.Text) : 0;
                totalPPML2 += tr.Cells[10].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[10].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
                trfooter.Cells[7].InnerHtml = totalPakaiBBL2.ToString("N2");
                trfooter.Cells[10].InnerHtml = totalPPML2.ToString("N0");
            }
        }
        protected void QtyPakaiL3_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL3 = 0; decimal totalPPML3 = 0;
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                TextBox jmlL3 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL3");
                string jmlL3S = jmlL3.Text.Replace(",", ".");
                if (jmlL3.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlL3.Text = string.Empty; jmlL3.Focus();
                    return;
                }
                if (jmlL3.Text != string.Empty)
                {
                    string PakaiL3 = tr.Cells[13].InnerHtml != "" ? tr.Cells[13].InnerHtml : "0";
                    decimal DPakaiL3 = Convert.ToDecimal(PakaiL3);
                    tr.Cells[15].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(jmlL3S)) * 1000000).ToString("N0");
                }
                totalPakaiBBL3 += jmlL3.Text != "" ? decimal.Parse(jmlL3.Text) : 0;
                totalPPML3 += tr.Cells[15].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[15].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
                trfooter.Cells[12].InnerHtml = totalPakaiBBL3.ToString("N2");
                trfooter.Cells[15].InnerHtml = totalPPML3.ToString("N0");
            }
        }
        protected void QtyPakaiL4_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL4 = 0; decimal totalPPML4 = 0;
            for (int i = 0; i < lstMatrix.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
                TextBox jmlL4 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL4");
                string jmlL4S = jmlL4.Text.Replace(",", ".");
                if (jmlL4.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlL4.Text = string.Empty; jmlL4.Focus();
                    return;
                }
                if (jmlL4.Text != string.Empty)
                {
                    string PakaiL4 = tr.Cells[18].InnerHtml != "" ? tr.Cells[18].InnerHtml : "0";
                    decimal DPakaiL4 = Convert.ToDecimal(PakaiL4);
                    tr.Cells[20].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(jmlL4S)) * 1000000).ToString("N0");
                }
                totalPakaiBBL4 += jmlL4.Text != "" ? decimal.Parse(jmlL4.Text) : 0;
                totalPPML4 += tr.Cells[20].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[20].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
                trfooter.Cells[17].InnerHtml = totalPakaiBBL4.ToString("N2");
                trfooter.Cells[20].InnerHtml = totalPPML4.ToString("N0");
            }
        }
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
            switch (e.CommandName)
            {
                case "save":

                    DomainBMReport2 dhpsave = new DomainBMReport2();
                    FacadeBMReport2 fhpsave = new FacadeBMReport2();

                    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    {
                        ImageButton update = (ImageButton)e.Item.FindControl("update");
                        ImageButton updatehide = (ImageButton)e.Item.FindControl("updatehide");
                        ImageButton simpan = (ImageButton)e.Item.FindControl("simpan");
                        ImageButton simpanhide = (ImageButton)e.Item.FindControl("savehide");

                        HtmlTableRow tr2 = (HtmlTableRow)e.Item.FindControl("lst2");
                        Image save = (Image)e.Item.FindControl("simpan");
                        string tgl2 = save.CssClass;

                        DomainBMReport2 dhps0 = new DomainBMReport2();
                        FacadeBMReport2 fhps0 = new FacadeBMReport2();
                        dhps0 = fhps0.RetrieveTgl(tgl2);

                        for (int i = 1; i < tr2.Cells.Count; i++)
                        {
                            TextBox jmlalias = (TextBox)tr2.Cells[i].FindControl("QtyPakaiL1_Alias");
                            TextBox jmlaliasL2 = (TextBox)tr2.Cells[i].FindControl("QtyPakaiL2_Alias");
                            TextBox jmlaliasL3 = (TextBox)tr2.Cells[i].FindControl("QtyPakaiL3_Alias");
                            TextBox jmlaliasL4 = (TextBox)tr2.Cells[i].FindControl("QtyPakaiL4_Alias");
                            TextBox Ket = (TextBox)tr2.Cells[i].FindControl("Keterangan");
                            Session["Keterangan"] = Ket.Text.Trim();

                            //string PL1 = jmlalias.Text.Replace(",", "."); string PL2 = jmlaliasL2.Text.Replace(",", ".");
                            //string PL3 = jmlaliasL3.Text.Replace(",", "."); string PL4 = jmlalias4.Text.Replace(",", ".");

                            string PL1 = (jmlalias.Text != "") ? jmlalias.Text.Replace(".", ",") : "0.00";
                            string PL2 = (jmlaliasL2.Text != "") ? jmlaliasL2.Text.Replace(".", ",") : "0.00";
                            string PL3 = (jmlaliasL3.Text != "") ? jmlaliasL3.Text.Replace(".", ",") : "0.00";
                            string PL4 = (jmlaliasL4.Text != "") ? jmlaliasL4.Text.Replace(".", ",") : "0.00";

                            jmlalias.Visible = true; jmlalias.Enabled = true;

                            Decimal PakaiL1Alias = Convert.ToDecimal(PL1); Session["PakaiL1Alias"] = PakaiL1Alias;
                            Decimal PakaiL1AliasL2 = Convert.ToDecimal(PL2); Session["PakaiL1AliasL2"] = PakaiL1AliasL2;
                            Decimal PakaiL1AliasL3 = Convert.ToDecimal(PL3); Session["PakaiL1AliasL3"] = PakaiL1AliasL3;
                            Decimal PakaiL1AliasL4 = Convert.ToDecimal(PL4); Session["PakaiL1AliasL4"] = PakaiL1AliasL4;
                        }

                        Decimal QtyPLine1 = Convert.ToDecimal(Session["PakaiL1Alias"]); Decimal QtyPLine2 = Convert.ToDecimal(Session["PakaiL1AliasL2"]);
                        Decimal QtyPLine3 = Convert.ToDecimal(Session["PakaiL1AliasL3"]); Decimal QtyPLine4 = Convert.ToDecimal(Session["PakaiL1AliasL4"]);

                        dhpsave.Tanggal = tgl2; dhpsave.Tanggal2 = dhps0.Tanggal2;

                        dhpsave.OutM3L1 = Convert.ToDecimal(tr2.Cells[1].InnerHtml); dhpsave.OutM3L2 = Convert.ToDecimal(tr2.Cells[6].InnerHtml);
                        dhpsave.OutM3L3 = Convert.ToDecimal(tr2.Cells[11].InnerHtml); dhpsave.OutM3L4 = Convert.ToDecimal(tr2.Cells[16].InnerHtml);
                        dhpsave.OutM3L5 = 0; dhpsave.OutM3L6 = 0;

                        dhpsave.PakaiBBL1 = QtyPLine1; dhpsave.PakaiBBL2 = QtyPLine2;
                        dhpsave.PakaiBBL3 = QtyPLine3; dhpsave.PakaiBBL4 = QtyPLine4;
                        dhpsave.PakaiBBL5 = 0; dhpsave.PakaiBBL6 = 0;

                        dhpsave.QtySPBL1 = Convert.ToDecimal(tr2.Cells[3].InnerHtml); dhpsave.QtySPBL2 = Convert.ToDecimal(tr2.Cells[8].InnerHtml);
                        dhpsave.QtySPBL3 = Convert.ToDecimal(tr2.Cells[13].InnerHtml); dhpsave.QtySPBL4 = Convert.ToDecimal(tr2.Cells[18].InnerHtml);
                        dhpsave.QtySPBL5 = 0; dhpsave.QtySPBL6 = 0;

                        dhpsave.EfesiensiL1 = Convert.ToDecimal(tr2.Cells[4].InnerHtml); dhpsave.EfesiensiL2 = Convert.ToDecimal(tr2.Cells[9].InnerHtml);
                        dhpsave.EfesiensiL3 = Convert.ToDecimal(tr2.Cells[14].InnerHtml); dhpsave.EfesiensiL4 = Convert.ToDecimal(tr2.Cells[19].InnerHtml);
                        dhpsave.EfesiensiL5 = 0; dhpsave.EfesiensiL6 = 0;

                        dhpsave.PPML1 = Convert.ToDecimal(tr2.Cells[5].InnerHtml); dhpsave.PPML2 = Convert.ToDecimal(tr2.Cells[10].InnerHtml);
                        dhpsave.PPML3 = Convert.ToDecimal(tr2.Cells[15].InnerHtml); dhpsave.PPML4 = Convert.ToDecimal(tr2.Cells[20].InnerHtml);
                        dhpsave.PPML5 = 0; dhpsave.PPML6 = 0;

                        dhpsave.Keterangan = Session["Keterangan"].ToString().Trim();

                        int intHasil1 = 0;

                        intHasil1 = fhpsave.UpdatePerItem(dhpsave);

                        if (intHasil1 > 0)
                        {
                            Users user = (Users)Session["Users"];
                            string tgl1 = tr2.Cells[0].InnerHtml;
                            DomainBMReport2 dhps2 = new DomainBMReport2();
                            FacadeBMReport2 fhps2 = new FacadeBMReport2();
                            dhps2 = fhps2.RetrieveDataOutM3(tgl1, user.UnitKerjaID);

                            Decimal OutLamaL1 = Convert.ToDecimal(tr2.Cells[1].InnerHtml); Decimal OutLamaL2 = Convert.ToDecimal(tr2.Cells[6].InnerHtml);
                            Decimal OutLamaL3 = Convert.ToDecimal(tr2.Cells[11].InnerHtml); Decimal OutLamaL4 = Convert.ToDecimal(tr2.Cells[16].InnerHtml);
                            Decimal OutBaruL1 = dhps2.OutM3L1; Decimal OutBaruL2 = dhps2.OutM3L2;
                            Decimal OutBaruL3 = dhps2.OutM3L3; Decimal OutBaruL4 = dhps2.OutM3L4;

                            if (OutBaruL1 != OutLamaL1) { tr2.Cells[1].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (OutBaruL2 != OutLamaL2) { tr2.Cells[6].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (OutBaruL3 != OutLamaL3) { tr2.Cells[11].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (OutBaruL4 != OutLamaL4) { tr2.Cells[16].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (OutBaruL1 == OutLamaL1) { tr2.Cells[1].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (OutBaruL2 == OutLamaL2) { tr2.Cells[6].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (OutBaruL3 == OutLamaL3) { tr2.Cells[11].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (OutBaruL4 == OutLamaL4) { tr2.Cells[16].Attributes.Add("style", "font-weight:normal; color:Black;"); }

                            DomainBMReport2 dhps3 = new DomainBMReport2();
                            FacadeBMReport2 fhps3 = new FacadeBMReport2();
                            dhps3 = fhps3.RetrieveDataUpdateSPB(tgl1, user.UnitKerjaID);

                            Decimal SPBL1_Lama = Convert.ToDecimal(tr2.Cells[3].InnerHtml); Decimal SPBL2_Lama = Convert.ToDecimal(tr2.Cells[8].InnerHtml);
                            Decimal SPBL3_Lama = Convert.ToDecimal(tr2.Cells[13].InnerHtml); Decimal SPBL4_Lama = Convert.ToDecimal(tr2.Cells[18].InnerHtml);

                            Decimal SPBL1_Baru = dhps3.QtySPBL1; Decimal SPBL2_Baru = dhps3.QtySPBL2;
                            Decimal SPBL3_Baru = dhps3.QtySPBL3; Decimal SPBL4_Baru = dhps3.QtySPBL4;

                            if (SPBL1_Lama != SPBL1_Baru) { tr2.Cells[3].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (SPBL2_Lama != SPBL2_Baru) { tr2.Cells[8].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (SPBL3_Lama != SPBL3_Baru) { tr2.Cells[13].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (SPBL4_Lama != SPBL4_Baru) { tr2.Cells[18].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (SPBL1_Lama == SPBL1_Baru) { tr2.Cells[3].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (SPBL2_Lama == SPBL2_Baru) { tr2.Cells[8].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (SPBL3_Lama == SPBL3_Baru) { tr2.Cells[13].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (SPBL4_Lama == SPBL4_Baru) { tr2.Cells[18].Attributes.Add("style", "font-weight:normal; color:Black;"); }

                            update.Visible = true; updatehide.Visible = false;
                            simpan.Visible = false; simpanhide.Visible = true;
                        }
                    }

                    break;

                case "hps":
                    Users user2 = (Users)Session["Users"];
                    Session["flag"] = 1;
                    Image hps = (Image)e.Item.FindControl("update");
                    string tgl = hps.CssClass;
                    DomainBMReport2 dhps = new DomainBMReport2();
                    FacadeBMReport2 fhps = new FacadeBMReport2();
                    int intHasil = 0;
                    dhps.Tanggal = tgl;
                    intHasil = fhps.CancelItem(dhps);

                    if (intHasil > 0)
                    {
                        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                        {
                            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst2");
                            for (int i2 = 1; i2 < tr.Cells.Count; i2++)
                            {
                                ImageButton update = (ImageButton)tr.Cells[i2].FindControl("update");
                                ImageButton updatehide = (ImageButton)tr.Cells[i2].FindControl("updatehide");
                                ImageButton simpan = (ImageButton)tr.Cells[i2].FindControl("simpan");
                                ImageButton simpanhide = (ImageButton)tr.Cells[i2].FindControl("savehide");

                                simpan.Visible = true; simpanhide.Visible = false; simpan.Enabled = true;
                                update.Visible = false; updatehide.Visible = true; updatehide.Enabled = false;

                                TextBox ket = (TextBox)tr.Cells[i2].FindControl("Keterangan");
                                TextBox jmlalias = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL1_Alias");
                                TextBox jmlaliasL2 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL2_Alias");
                                TextBox jmlaliasL3 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL3_Alias");
                                TextBox jmlaliasL4 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL4_Alias");

                                TextBox jml = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL1");
                                TextBox jmlL2 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL2");
                                TextBox jmlL3 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL3");
                                TextBox jmlL4 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL4");

                                ket.Enabled = true;
                                jml.Visible = false; jmlalias.Visible = true; jml.Enabled = true;
                                jmlL2.Visible = false; jmlaliasL2.Visible = true; jmlaliasL2.Enabled = true;
                                jmlL3.Visible = false; jmlaliasL3.Visible = true; jmlaliasL3.Enabled = true;
                                jmlL4.Visible = false; jmlaliasL4.Visible = true; jmlaliasL4.Enabled = true;

                                DomainBMReport2 dhps2 = new DomainBMReport2();
                                FacadeBMReport2 fhps2 = new FacadeBMReport2();
                                dhps2 = fhps2.RetrieveDataOutM3(tgl, user2.UnitKerjaID);

                                tr.Cells[1].InnerHtml = Convert.ToDecimal(dhps2.OutM3L1).ToString(); tr.Cells[6].InnerHtml = Convert.ToDecimal(dhps2.OutM3L2).ToString();
                                tr.Cells[11].InnerHtml = Convert.ToDecimal(dhps2.OutM3L3).ToString(); tr.Cells[16].InnerHtml = Convert.ToDecimal(dhps2.OutM3L4).ToString();

                                DomainBMReport2 dhps3 = new DomainBMReport2();
                                FacadeBMReport2 fhps3 = new FacadeBMReport2();
                                dhps3 = fhps3.RetrieveDataUpdateSPB(tgl, user2.UnitKerjaID);

                                tr.Cells[3].InnerHtml = Convert.ToDecimal(dhps3.QtySPBL1).ToString(); tr.Cells[8].InnerHtml = Convert.ToDecimal(dhps3.QtySPBL2).ToString();
                                tr.Cells[13].InnerHtml = Convert.ToDecimal(dhps3.QtySPBL3).ToString(); tr.Cells[18].InnerHtml = Convert.ToDecimal(dhps3.QtySPBL4).ToString();


                            }

                        }


                    }

                    break;
            }
        }
        protected void lstMatrixK2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "save":

                    DomainBMReport2 dhpsave = new DomainBMReport2();
                    FacadeBMReport2 fhpsave = new FacadeBMReport2();

                    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    {
                        ImageButton update = (ImageButton)e.Item.FindControl("update");
                        ImageButton updatehide = (ImageButton)e.Item.FindControl("updatehide");
                        ImageButton simpan = (ImageButton)e.Item.FindControl("simpan");
                        ImageButton simpanhide = (ImageButton)e.Item.FindControl("savehide");

                        HtmlTableRow tr2 = (HtmlTableRow)e.Item.FindControl("lstK2");
                        Image save = (Image)e.Item.FindControl("simpan");
                        string tgl2 = save.CssClass;

                        DomainBMReport2 dhps0 = new DomainBMReport2();
                        FacadeBMReport2 fhps0 = new FacadeBMReport2();
                        dhps0 = fhps0.RetrieveTgl(tgl2);

                        for (int i = 1; i < tr2.Cells.Count; i++)
                        {
                            TextBox jmlalias = (TextBox)tr2.Cells[i].FindControl("QtyPakaiL1_AliasK");
                            TextBox jmlaliasL2 = (TextBox)tr2.Cells[i].FindControl("QtyPakaiL2_AliasK");
                            TextBox jmlaliasL3 = (TextBox)tr2.Cells[i].FindControl("QtyPakaiL3_AliasK");
                            TextBox jmlaliasL4 = (TextBox)tr2.Cells[i].FindControl("QtyPakaiL4_AliasK");
                            TextBox jmlaliasL5 = (TextBox)tr2.Cells[i].FindControl("QtyPakaiL5_AliasK");
                            TextBox jmlaliasL6 = (TextBox)tr2.Cells[i].FindControl("QtyPakaiL6_AliasK");
                            TextBox Ket = (TextBox)tr2.Cells[i].FindControl("Keterangan");
                            Session["Keterangan"] = Ket.Text.Trim();

                            //string PL1 = jmlalias.Text.Replace(",", "."); string PL2 = jmlaliasL2.Text.Replace(",", ".");
                            //string PL3 = jmlaliasL3.Text.Replace(",", "."); string PL4 = jmlalias4.Text.Replace(",", ".");

                            string PL1 = (jmlalias.Text != "") ? jmlalias.Text.Replace(".", ",") : "0.00";
                            string PL2 = (jmlaliasL2.Text != "") ? jmlaliasL2.Text.Replace(".", ",") : "0.00";
                            string PL3 = (jmlaliasL3.Text != "") ? jmlaliasL3.Text.Replace(".", ",") : "0.00";
                            string PL4 = (jmlaliasL4.Text != "") ? jmlaliasL4.Text.Replace(".", ",") : "0.00";
                            string PL5 = (jmlaliasL5.Text != "") ? jmlaliasL5.Text.Replace(".", ",") : "0.00";
                            string PL6 = (jmlaliasL6.Text != "") ? jmlaliasL6.Text.Replace(".", ",") : "0.00";

                            jmlalias.Visible = true; jmlalias.Enabled = true;

                            Decimal PakaiL1Alias = Convert.ToDecimal(PL1); Session["PakaiL1Alias"] = PakaiL1Alias;
                            Decimal PakaiL1AliasL2 = Convert.ToDecimal(PL2); Session["PakaiL1AliasL2"] = PakaiL1AliasL2;
                            Decimal PakaiL1AliasL3 = Convert.ToDecimal(PL3); Session["PakaiL1AliasL3"] = PakaiL1AliasL3;
                            Decimal PakaiL1AliasL4 = Convert.ToDecimal(PL4); Session["PakaiL1AliasL4"] = PakaiL1AliasL4;
                            Decimal PakaiL1AliasL5 = Convert.ToDecimal(PL5); Session["PakaiL1AliasL5"] = PakaiL1AliasL5;
                            Decimal PakaiL1AliasL6 = Convert.ToDecimal(PL6); Session["PakaiL1AliasL6"] = PakaiL1AliasL6;
                        }

                        Decimal QtyPLine1 = Convert.ToDecimal(Session["PakaiL1Alias"]); Decimal QtyPLine2 = Convert.ToDecimal(Session["PakaiL1AliasL2"]);
                        Decimal QtyPLine3 = Convert.ToDecimal(Session["PakaiL1AliasL3"]); Decimal QtyPLine4 = Convert.ToDecimal(Session["PakaiL1AliasL4"]);
                        Decimal QtyPLine5 = Convert.ToDecimal(Session["PakaiL1AliasL5"]); Decimal QtyPLine6 = Convert.ToDecimal(Session["PakaiL1AliasL6"]);

                        dhpsave.Tanggal = tgl2; dhpsave.Tanggal2 = dhps0.Tanggal2;

                        dhpsave.OutM3L1 = Convert.ToDecimal(tr2.Cells[1].InnerHtml); dhpsave.OutM3L2 = Convert.ToDecimal(tr2.Cells[6].InnerHtml);
                        dhpsave.OutM3L3 = Convert.ToDecimal(tr2.Cells[11].InnerHtml); dhpsave.OutM3L4 = Convert.ToDecimal(tr2.Cells[16].InnerHtml);
                        dhpsave.OutM3L5 = Convert.ToDecimal(tr2.Cells[21].InnerHtml); dhpsave.OutM3L6 = Convert.ToDecimal(tr2.Cells[26].InnerHtml);

                        dhpsave.PakaiBBL1 = QtyPLine1; dhpsave.PakaiBBL2 = QtyPLine2;
                        dhpsave.PakaiBBL3 = QtyPLine3; dhpsave.PakaiBBL4 = QtyPLine4;
                        dhpsave.PakaiBBL5 = QtyPLine5; dhpsave.PakaiBBL6 = QtyPLine6;

                        dhpsave.QtySPBL1 = Convert.ToDecimal(tr2.Cells[3].InnerHtml); dhpsave.QtySPBL2 = Convert.ToDecimal(tr2.Cells[8].InnerHtml);
                        dhpsave.QtySPBL3 = Convert.ToDecimal(tr2.Cells[13].InnerHtml); dhpsave.QtySPBL4 = Convert.ToDecimal(tr2.Cells[18].InnerHtml);
                        dhpsave.QtySPBL5 = Convert.ToDecimal(tr2.Cells[23].InnerHtml); dhpsave.QtySPBL6 = Convert.ToDecimal(tr2.Cells[28].InnerHtml);

                        dhpsave.EfesiensiL1 = Convert.ToDecimal(tr2.Cells[4].InnerHtml); dhpsave.EfesiensiL2 = Convert.ToDecimal(tr2.Cells[9].InnerHtml);
                        dhpsave.EfesiensiL3 = Convert.ToDecimal(tr2.Cells[14].InnerHtml); dhpsave.EfesiensiL4 = Convert.ToDecimal(tr2.Cells[19].InnerHtml);
                        dhpsave.EfesiensiL5 = Convert.ToDecimal(tr2.Cells[24].InnerHtml); dhpsave.EfesiensiL6 = Convert.ToDecimal(tr2.Cells[29].InnerHtml);

                        dhpsave.PPML1 = Convert.ToDecimal(tr2.Cells[5].InnerHtml); dhpsave.PPML2 = Convert.ToDecimal(tr2.Cells[10].InnerHtml);
                        dhpsave.PPML3 = Convert.ToDecimal(tr2.Cells[15].InnerHtml); dhpsave.PPML4 = Convert.ToDecimal(tr2.Cells[20].InnerHtml);
                        dhpsave.PPML5 = Convert.ToDecimal(tr2.Cells[25].InnerHtml); dhpsave.PPML6 = Convert.ToDecimal(tr2.Cells[30].InnerHtml);

                        dhpsave.Keterangan = Session["Keterangan"].ToString().Trim();

                        int intHasil1 = 0;

                        intHasil1 = fhpsave.UpdatePerItem(dhpsave);

                        if (intHasil1 > 0)
                        {
                            Users user = (Users)Session["Users"];

                            string tgl1 = tr2.Cells[0].InnerHtml;
                            DomainBMReport2 dhps2 = new DomainBMReport2();
                            FacadeBMReport2 fhps2 = new FacadeBMReport2();
                            dhps2 = fhps2.RetrieveDataOutM3(tgl1, user.UnitKerjaID);

                            Decimal OutLamaL1 = Convert.ToDecimal(tr2.Cells[1].InnerHtml); Decimal OutLamaL2 = Convert.ToDecimal(tr2.Cells[6].InnerHtml);
                            Decimal OutLamaL3 = Convert.ToDecimal(tr2.Cells[11].InnerHtml); Decimal OutLamaL4 = Convert.ToDecimal(tr2.Cells[16].InnerHtml);
                            Decimal OutLamaL5 = Convert.ToDecimal(tr2.Cells[21].InnerHtml); Decimal OutLamaL6 = Convert.ToDecimal(tr2.Cells[26].InnerHtml);

                            Decimal OutBaruL1 = dhps2.OutM3L1; Decimal OutBaruL2 = dhps2.OutM3L2;
                            Decimal OutBaruL3 = dhps2.OutM3L3; Decimal OutBaruL4 = dhps2.OutM3L4;
                            Decimal OutBaruL5 = dhps2.OutM3L5; Decimal OutBaruL6 = dhps2.OutM3L6;

                            if (OutBaruL1 != OutLamaL1) { tr2.Cells[1].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (OutBaruL2 != OutLamaL2) { tr2.Cells[6].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (OutBaruL3 != OutLamaL3) { tr2.Cells[11].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (OutBaruL4 != OutLamaL4) { tr2.Cells[16].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (OutBaruL1 == OutLamaL1) { tr2.Cells[1].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (OutBaruL2 == OutLamaL2) { tr2.Cells[6].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (OutBaruL3 == OutLamaL3) { tr2.Cells[11].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (OutBaruL4 == OutLamaL4) { tr2.Cells[16].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (OutBaruL5 == OutLamaL5) { tr2.Cells[21].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (OutBaruL6 == OutLamaL6) { tr2.Cells[26].Attributes.Add("style", "font-weight:normal; color:Black;"); }

                            DomainBMReport2 dhps3 = new DomainBMReport2();
                            FacadeBMReport2 fhps3 = new FacadeBMReport2();
                            dhps3 = fhps3.RetrieveDataUpdateSPB(tgl1, user.UnitKerjaID);

                            Decimal SPBL1_Lama = Convert.ToDecimal(tr2.Cells[3].InnerHtml); Decimal SPBL2_Lama = Convert.ToDecimal(tr2.Cells[8].InnerHtml);
                            Decimal SPBL3_Lama = Convert.ToDecimal(tr2.Cells[13].InnerHtml); Decimal SPBL4_Lama = Convert.ToDecimal(tr2.Cells[18].InnerHtml);
                            Decimal SPBL5_Lama = Convert.ToDecimal(tr2.Cells[23].InnerHtml); Decimal SPBL6_Lama = Convert.ToDecimal(tr2.Cells[28].InnerHtml);

                            Decimal SPBL1_Baru = dhps3.QtySPBL1; Decimal SPBL2_Baru = dhps3.QtySPBL2;
                            Decimal SPBL3_Baru = dhps3.QtySPBL3; Decimal SPBL4_Baru = dhps3.QtySPBL4;
                            Decimal SPBL5_Baru = dhps3.QtySPBL5; Decimal SPBL6_Baru = dhps3.QtySPBL6;

                            if (SPBL1_Lama != SPBL1_Baru) { tr2.Cells[3].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (SPBL2_Lama != SPBL2_Baru) { tr2.Cells[8].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (SPBL3_Lama != SPBL3_Baru) { tr2.Cells[13].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (SPBL4_Lama != SPBL4_Baru) { tr2.Cells[18].Attributes.Add("style", "font-weight:bold; color:Red;"); }
                            else if (SPBL1_Lama == SPBL1_Baru) { tr2.Cells[3].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (SPBL2_Lama == SPBL2_Baru) { tr2.Cells[8].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (SPBL3_Lama == SPBL3_Baru) { tr2.Cells[13].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (SPBL4_Lama == SPBL4_Baru) { tr2.Cells[18].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (SPBL5_Lama == SPBL5_Baru) { tr2.Cells[23].Attributes.Add("style", "font-weight:normal; color:Black;"); }
                            else if (SPBL6_Lama == SPBL6_Baru) { tr2.Cells[28].Attributes.Add("style", "font-weight:normal; color:Black;"); }

                            update.Visible = true; updatehide.Visible = false;
                            simpan.Visible = false; simpanhide.Visible = true;
                        }
                    }

                    break;

                case "hps":
                    Users user2 = (Users)Session["Users"];
                    Session["flag"] = 1;
                    Image hps = (Image)e.Item.FindControl("update");
                    string tgl = hps.CssClass;
                    DomainBMReport2 dhps = new DomainBMReport2();
                    FacadeBMReport2 fhps = new FacadeBMReport2();
                    int intHasil = 0;
                    dhps.Tanggal = tgl;
                    intHasil = fhps.CancelItem(dhps);

                    if (intHasil > 0)
                    {
                        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                        {
                            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lstK2");
                            for (int i2 = 1; i2 < tr.Cells.Count; i2++)
                            {
                                ImageButton update = (ImageButton)tr.Cells[i2].FindControl("update");
                                ImageButton updatehide = (ImageButton)tr.Cells[i2].FindControl("updatehide");
                                ImageButton simpan = (ImageButton)tr.Cells[i2].FindControl("simpan");
                                ImageButton simpanhide = (ImageButton)tr.Cells[i2].FindControl("savehide");

                                simpan.Visible = true; simpanhide.Visible = false; simpan.Enabled = true;
                                update.Visible = false; updatehide.Visible = true; updatehide.Enabled = false;

                                TextBox ket = (TextBox)tr.Cells[i2].FindControl("Keterangan");
                                TextBox jmlalias = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL1_AliasK");
                                TextBox jmlaliasL2 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL2_AliasK");
                                TextBox jmlaliasL3 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL3_AliasK");
                                TextBox jmlaliasL4 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL4_AliasK");
                                TextBox jmlaliasL5 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL5_AliasK");
                                TextBox jmlaliasL6 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL6_AliasK");

                                TextBox jml = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL1K");
                                TextBox jmlL2 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL2K");
                                TextBox jmlL3 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL3K");
                                TextBox jmlL4 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL4K");
                                TextBox jmlL5 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL5K");
                                TextBox jmlL6 = (TextBox)tr.Cells[i2].FindControl("QtyPakaiL6K");

                                ket.Enabled = true;
                                jml.Visible = false; jmlalias.Visible = true; jml.Enabled = true;
                                jmlL2.Visible = false; jmlaliasL2.Visible = true; jmlaliasL2.Enabled = true;
                                jmlL3.Visible = false; jmlaliasL3.Visible = true; jmlaliasL3.Enabled = true;
                                jmlL4.Visible = false; jmlaliasL4.Visible = true; jmlaliasL4.Enabled = true;
                                jmlL5.Visible = false; jmlaliasL5.Visible = true; jmlaliasL5.Enabled = true;
                                jmlL6.Visible = false; jmlaliasL6.Visible = true; jmlaliasL6.Enabled = true;

                                DomainBMReport2 dhps2 = new DomainBMReport2();
                                FacadeBMReport2 fhps2 = new FacadeBMReport2();
                                dhps2 = fhps2.RetrieveDataOutM3(tgl, user2.UnitKerjaID);

                                tr.Cells[1].InnerHtml = Convert.ToDecimal(dhps2.OutM3L1).ToString();
                                tr.Cells[6].InnerHtml = Convert.ToDecimal(dhps2.OutM3L2).ToString();
                                tr.Cells[11].InnerHtml = Convert.ToDecimal(dhps2.OutM3L3).ToString();
                                tr.Cells[16].InnerHtml = Convert.ToDecimal(dhps2.OutM3L4).ToString();
                                tr.Cells[21].InnerHtml = Convert.ToDecimal(dhps2.OutM3L5).ToString();
                                tr.Cells[26].InnerHtml = Convert.ToDecimal(dhps2.OutM3L6).ToString();

                                DomainBMReport2 dhps3 = new DomainBMReport2();
                                FacadeBMReport2 fhps3 = new FacadeBMReport2();
                                dhps3 = fhps3.RetrieveDataUpdateSPB(tgl, user2.UnitKerjaID);

                                tr.Cells[3].InnerHtml = Convert.ToDecimal(dhps3.QtySPBL1).ToString();
                                tr.Cells[8].InnerHtml = Convert.ToDecimal(dhps3.QtySPBL2).ToString();
                                tr.Cells[13].InnerHtml = Convert.ToDecimal(dhps3.QtySPBL3).ToString();
                                tr.Cells[18].InnerHtml = Convert.ToDecimal(dhps3.QtySPBL4).ToString();
                                tr.Cells[23].InnerHtml = Convert.ToDecimal(dhps3.QtySPBL5).ToString();
                                tr.Cells[28].InnerHtml = Convert.ToDecimal(dhps3.QtySPBL6).ToString();
                            }

                        }


                    }

                    break;
            }
        }
        protected void QtyPakaiL1K_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL1 = 0; decimal totalPPML1 = 0;
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1K");
                string jmlS = jml.Text.Replace(",", ".");
                if (jml.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jml.Text = string.Empty; jml.Focus();
                    return;
                }

                if (jml.Text != string.Empty)
                {
                    string PakaiL1 = tr.Cells[2].InnerHtml != "" ? tr.Cells[2].InnerHtml : "0";
                    decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
                    tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jmlS)) * 1000000).ToString("N2");
                }

                totalPakaiBBL1 += jml.Text != "" ? decimal.Parse(jml.Text) : 0;
                totalPPML1 += tr.Cells[5].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[5].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
                trfooter.Cells[2].InnerHtml = totalPakaiBBL1.ToString("N2");
                trfooter.Cells[5].InnerHtml = totalPPML1.ToString("N0");
            }

        }
        protected void QtyPakaiL2K_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL2 = 0; decimal totalPPML2 = 0;

            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                TextBox jmlL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2K");
                string jmlL2S = jmlL2.Text.Replace(",", ".");

                if (jmlL2.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlL2.Text = string.Empty; jmlL2.Focus();
                    return;
                }
                if (jmlL2.Text != string.Empty)
                {
                    string PakaiL2 = tr.Cells[7].InnerHtml != "" ? tr.Cells[7].InnerHtml : "0";
                    decimal DPakaiL2 = Convert.ToDecimal(PakaiL2);
                    tr.Cells[10].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(jmlL2S)) * 1000000).ToString("N0");
                }

                totalPakaiBBL2 += jmlL2.Text != "" ? decimal.Parse(jmlL2.Text) : 0;
                totalPPML2 += tr.Cells[10].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[10].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
                //string totalPakaiBBL2_s = jmlL2.Text.Replace(",", ".");
                trfooter.Cells[7].InnerHtml = Convert.ToDecimal(totalPakaiBBL2).ToString("N2").Replace(",", ".");
                trfooter.Cells[10].InnerHtml = totalPPML2.ToString("N0");
            }
        }
        protected void QtyPakaiL3K_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL3 = 0; decimal totalPPML3 = 0;
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                TextBox jmlL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3K");
                string jmlL3S = jmlL3.Text.Replace(",", ".");
                if (jmlL3.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlL3.Text = string.Empty; jmlL3.Focus();
                    return;
                }
                if (jmlL3.Text != string.Empty)
                {
                    string PakaiL3 = tr.Cells[12].InnerHtml != "" ? tr.Cells[12].InnerHtml : "0";
                    decimal DPakaiL3 = Convert.ToDecimal(PakaiL3);
                    tr.Cells[15].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(jmlL3S)) * 1000000).ToString("N0");
                }
                totalPakaiBBL3 += jmlL3.Text != "" ? decimal.Parse(jmlL3.Text) : 0;
                totalPPML3 += tr.Cells[15].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[15].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
                trfooter.Cells[12].InnerHtml = totalPakaiBBL3.ToString("N2");
                trfooter.Cells[15].InnerHtml = totalPPML3.ToString("N0");
            }

        }
        protected void QtyPakaiL4K_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL4 = 0; decimal totalPPML4 = 0;
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                TextBox jmlL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4K");
                string jmlL4S = jmlL4.Text.Replace(",", ".");
                if (jmlL4.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlL4.Text = string.Empty; jmlL4.Focus();
                    return;
                }
                if (jmlL4.Text != string.Empty)
                {
                    string PakaiL4 = tr.Cells[17].InnerHtml != "" ? tr.Cells[17].InnerHtml : "0";
                    decimal DPakaiL4 = Convert.ToDecimal(PakaiL4);
                    tr.Cells[20].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(jmlL4S)) * 1000000).ToString("N0");
                }
                totalPakaiBBL4 += jmlL4.Text != "" ? decimal.Parse(jmlL4.Text) : 0;
                totalPPML4 += tr.Cells[20].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[20].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
                trfooter.Cells[17].InnerHtml = totalPakaiBBL4.ToString("N2");
                trfooter.Cells[20].InnerHtml = totalPPML4.ToString("N0");
            }
        }
        protected void QtyPakaiL5K_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL5 = 0; decimal totalPPML5 = 0;
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                TextBox jmlL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5K");
                string jmlL5S = jmlL5.Text.Replace(",", ".");
                if (jmlL5.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlL5.Text = string.Empty; jmlL5.Focus();
                    return;
                }
                if (jmlL5.Text != string.Empty)
                {
                    string PakaiL5 = tr.Cells[22].InnerHtml != "" ? tr.Cells[22].InnerHtml : "0";
                    decimal DPakaiL5 = Convert.ToDecimal(PakaiL5);
                    tr.Cells[25].InnerHtml = ((Convert.ToDecimal(DPakaiL5) / decimal.Parse(jmlL5S)) * 1000000).ToString("N0");
                }
                totalPakaiBBL5 += jmlL5.Text != "" ? decimal.Parse(jmlL5.Text) : 0;
                totalPPML5 += tr.Cells[25].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[25].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
                trfooter.Cells[22].InnerHtml = totalPakaiBBL5.ToString("N2");
                trfooter.Cells[25].InnerHtml = totalPPML5.ToString("N0");
            }
        }
        protected void QtyPakaiL6K_Change(object sender, EventArgs e)
        {
            decimal totalPakaiBBL6 = 0; decimal totalPPML6 = 0;
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                TextBox jmlL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6K");
                string jmlL6S = jmlL6.Text.Replace(",", ".");
                if (jmlL6.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlL6.Text = string.Empty; jmlL6.Focus();
                    return;
                }
                if (jmlL6.Text != string.Empty)
                {
                    string PakaiL6 = tr.Cells[27].InnerHtml != "" ? tr.Cells[27].InnerHtml : "0";
                    decimal DPakaiL6 = Convert.ToDecimal(PakaiL6);
                    tr.Cells[30].InnerHtml = ((Convert.ToDecimal(DPakaiL6) / decimal.Parse(jmlL6S)) * 1000000).ToString("N0");
                }
                totalPakaiBBL6 += jmlL6.Text != "" ? decimal.Parse(jmlL6.Text) : 0;
                totalPPML6 += tr.Cells[30].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[30].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
                trfooter.Cells[27].InnerHtml = totalPakaiBBL6.ToString("N2");
                trfooter.Cells[30].InnerHtml = totalPPML6.ToString("N0");
            }
        }
        protected void QtyPakaiL1_AliasK_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jmlalias = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1_AliasK");
                string jmlaliasS = jmlalias.Text.Replace(",", ".");

                if (jmlalias.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlalias.Text = string.Empty; jmlalias.Focus();
                    return;
                }

                if (jmlalias.Text != string.Empty)
                {
                    string PakaiL1 = tr.Cells[2].InnerHtml != "" ? tr.Cells[2].InnerHtml : "0";
                    decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
                    decimal OutputL1 = decimal.Parse(tr.Cells[1].InnerHtml);

                    if (DPakaiL1 == 0 || OutputL1 == 0)
                    {
                        tr.Cells[4].InnerHtml = "0,00";
                    }
                    else if (DPakaiL1 > 0 && OutputL1 > 0)
                    {
                        tr.Cells[4].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(tr.Cells[1].InnerHtml))).ToString("N2");
                    }

                    tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jmlaliasS)) * 1000000).ToString("N2");
                    //tr.Cells[4].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(tr.Cells[1].InnerHtml))).ToString("N2");                
                }

                ImageButton update = (ImageButton)lstMatrixK2.Items[i].FindControl("update");
                ImageButton updatehide = (ImageButton)lstMatrixK2.Items[i].FindControl("updatehide");
                ImageButton simpan = (ImageButton)lstMatrixK2.Items[i].FindControl("simpan");
                ImageButton simpanhide = (ImageButton)lstMatrixK2.Items[i].FindControl("savehide");

                simpan.Enabled = true;


            }
        }
        protected void QtyPakaiL2_AliasK_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jmlaliasL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2_AliasK");
                string jmlaliasL2S = jmlaliasL2.Text.Replace(",", ".");

                if (jmlaliasL2.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlaliasL2.Text = string.Empty; jmlaliasL2.Focus();
                    return;
                }

                if (jmlaliasL2.Text != string.Empty)
                {
                    string PakaiL2 = tr.Cells[7].InnerHtml != "" ? tr.Cells[7].InnerHtml : "0";
                    decimal DPakaiL2 = Convert.ToDecimal(PakaiL2);
                    decimal OutputL2 = decimal.Parse(tr.Cells[6].InnerHtml);

                    if (DPakaiL2 == 0 || OutputL2 == 0)
                    {
                        tr.Cells[9].InnerHtml = "0,00";
                    }
                    else if (DPakaiL2 > 0 && OutputL2 > 0)
                    {
                        tr.Cells[9].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(tr.Cells[6].InnerHtml))).ToString("N2");
                    }

                    tr.Cells[10].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(jmlaliasL2S)) * 1000000).ToString("N2");
                    //tr.Cells[9].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(tr.Cells[6].InnerHtml))).ToString("N2");
                }

                ImageButton update = (ImageButton)lstMatrixK2.Items[i].FindControl("update");
                ImageButton updatehide = (ImageButton)lstMatrixK2.Items[i].FindControl("updatehide");
                ImageButton simpan = (ImageButton)lstMatrixK2.Items[i].FindControl("simpan");
                ImageButton simpanhide = (ImageButton)lstMatrixK2.Items[i].FindControl("savehide");

                simpan.Enabled = true;
            }
        }
        protected void QtyPakaiL3_AliasK_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jmlaliasL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3_AliasK");
                string jmlaliasL3S = jmlaliasL3.Text.Replace(",", ".");

                if (jmlaliasL3.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlaliasL3.Text = string.Empty; jmlaliasL3.Focus();
                    return;
                }

                if (jmlaliasL3.Text != string.Empty)
                {
                    string PakaiL3 = tr.Cells[12].InnerHtml != "" ? tr.Cells[12].InnerHtml : "0";
                    decimal DPakaiL3 = Convert.ToDecimal(PakaiL3);
                    decimal OutputL3 = decimal.Parse(tr.Cells[11].InnerHtml);

                    if (DPakaiL3 == 0 || OutputL3 == 0)
                    {
                        tr.Cells[14].InnerHtml = "0,00";
                    }
                    else if (DPakaiL3 > 0 && OutputL3 > 0)
                    {
                        tr.Cells[14].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(tr.Cells[11].InnerHtml))).ToString("N2");
                    }

                    tr.Cells[15].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(jmlaliasL3S)) * 1000000).ToString("N2");
                    //tr.Cells[14].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(tr.Cells[11].InnerHtml))).ToString("N2");
                }

                ImageButton update = (ImageButton)lstMatrixK2.Items[i].FindControl("update");
                ImageButton updatehide = (ImageButton)lstMatrixK2.Items[i].FindControl("updatehide");
                ImageButton simpan = (ImageButton)lstMatrixK2.Items[i].FindControl("simpan");
                ImageButton simpanhide = (ImageButton)lstMatrixK2.Items[i].FindControl("savehide");

                simpan.Enabled = true;
            }
        }
        protected void QtyPakaiL4_AliasK_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jmlaliasL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4_AliasK");
                string jmlaliasL4S = jmlaliasL4.Text.Replace(",", ".");

                if (jmlaliasL4.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlaliasL4.Text = string.Empty; jmlaliasL4.Focus();
                    return;
                }

                if (jmlaliasL4.Text != string.Empty)
                {
                    string PakaiL4 = tr.Cells[17].InnerHtml != "" ? tr.Cells[17].InnerHtml : "0";
                    decimal DPakaiL4 = Convert.ToDecimal(PakaiL4);
                    decimal OutputL4 = decimal.Parse(tr.Cells[16].InnerHtml);

                    if (DPakaiL4 == 0 || OutputL4 == 0)
                    {
                        tr.Cells[19].InnerHtml = "0,00";
                    }
                    else if (DPakaiL4 > 0 && OutputL4 > 0)
                    {
                        tr.Cells[19].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(tr.Cells[16].InnerHtml))).ToString("N2");
                    }
                    tr.Cells[20].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(jmlaliasL4S)) * 1000000).ToString("N2");
                    //tr.Cells[19].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(tr.Cells[16].InnerHtml))).ToString("N2");
                }

                ImageButton update = (ImageButton)lstMatrixK2.Items[i].FindControl("update");
                ImageButton updatehide = (ImageButton)lstMatrixK2.Items[i].FindControl("updatehide");
                ImageButton simpan = (ImageButton)lstMatrixK2.Items[i].FindControl("simpan");
                ImageButton simpanhide = (ImageButton)lstMatrixK2.Items[i].FindControl("savehide");

                simpan.Enabled = true;
            }
        }
        protected void QtyPakaiL5_AliasK_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jmlaliasL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5_AliasK");
                string jmlaliasL5S = jmlaliasL5.Text.Replace(",", ".");

                if (jmlaliasL5.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlaliasL5.Text = string.Empty; jmlaliasL5.Focus();
                    return;
                }

                if (jmlaliasL5.Text != string.Empty)
                {
                    string PakaiL5 = tr.Cells[22].InnerHtml != "" ? tr.Cells[22].InnerHtml : "0";
                    decimal DPakaiL5 = Convert.ToDecimal(PakaiL5);
                    decimal OutputL5 = decimal.Parse(tr.Cells[21].InnerHtml);

                    if (DPakaiL5 == 0 || OutputL5 == 0)
                    {
                        tr.Cells[24].InnerHtml = "0,00";
                    }
                    else if (DPakaiL5 > 0 && OutputL5 > 0)
                    {
                        tr.Cells[24].InnerHtml = ((Convert.ToDecimal(DPakaiL5) / decimal.Parse(tr.Cells[21].InnerHtml))).ToString("N2");
                    }

                    tr.Cells[25].InnerHtml = ((Convert.ToDecimal(DPakaiL5) / decimal.Parse(jmlaliasL5S)) * 1000000).ToString("N2");
                    //tr.Cells[24].InnerHtml = ((Convert.ToDecimal(DPakaiL5) / decimal.Parse(tr.Cells[21].InnerHtml))).ToString("N2");
                }

                ImageButton update = (ImageButton)lstMatrixK2.Items[i].FindControl("update");
                ImageButton updatehide = (ImageButton)lstMatrixK2.Items[i].FindControl("updatehide");
                ImageButton simpan = (ImageButton)lstMatrixK2.Items[i].FindControl("simpan");
                ImageButton simpanhide = (ImageButton)lstMatrixK2.Items[i].FindControl("savehide");

                simpan.Enabled = true;
            }
        }

        protected void QtyPakaiL6_AliasK_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMatrixK2.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
                DomainBMReport2 pj = new DomainBMReport2();
                TextBox jmlaliasL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6_AliasK");
                string jmlaliasL6S = jmlaliasL6.Text.Replace(",", ".");

                if (jmlaliasL6.Text.Contains("."))
                {
                    DisplayAJAXMessage(this, "Jangan tanda ( . ), tapi pake ( , )");
                    jmlaliasL6.Text = string.Empty; jmlaliasL6.Focus();
                    return;
                }

                if (jmlaliasL6.Text != string.Empty)
                {
                    string PakaiL6 = tr.Cells[27].InnerHtml != "" ? tr.Cells[27].InnerHtml : "0";
                    decimal DPakaiL6 = Convert.ToDecimal(PakaiL6);
                    decimal OutputL6 = decimal.Parse(tr.Cells[26].InnerHtml);

                    if (DPakaiL6 == 0 || OutputL6 == 0)
                    {
                        tr.Cells[29].InnerHtml = "0,00";
                    }
                    else if (DPakaiL6 > 0 && OutputL6 > 0)
                    {
                        tr.Cells[29].InnerHtml = ((Convert.ToDecimal(DPakaiL6) / decimal.Parse(tr.Cells[26].InnerHtml))).ToString("N2");
                    }
                    tr.Cells[30].InnerHtml = ((Convert.ToDecimal(DPakaiL6) / decimal.Parse(jmlaliasL6S)) * 1000000).ToString("N2");
                    //tr.Cells[29].InnerHtml = ((Convert.ToDecimal(DPakaiL6) / decimal.Parse(tr.Cells[26].InnerHtml))).ToString("N2");
                }

                ImageButton update = (ImageButton)lstMatrixK2.Items[i].FindControl("update");
                ImageButton updatehide = (ImageButton)lstMatrixK2.Items[i].FindControl("updatehide");
                ImageButton simpan = (ImageButton)lstMatrixK2.Items[i].FindControl("simpan");
                ImageButton simpanhide = (ImageButton)lstMatrixK2.Items[i].FindControl("savehide");

                simpan.Enabled = true;
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

    }

}

public class FacadeBMReport2
{
    public string strError = string.Empty;
    private ArrayList arrData = new ArrayList();
    private List<SqlParameter> sqlListParam;
    private DomainBMReport2 objBM = new DomainBMReport2();

    public FacadeBMReport2()
        : base()
    {

    }
    public string Criteria { get; set; }
    public string Field { get; set; }
    public string Where { get; set; }

    public int insertPFloculant(object objDomain)
    {
        try
        {
            objBM = (DomainBMReport2)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@UnitKerjaID", objBM.UnitKerjaID));
            sqlListParam.Add(new SqlParameter("@Tanggal", objBM.Tanggal));
            sqlListParam.Add(new SqlParameter("@Tanggal2", objBM.Tanggal2));
            sqlListParam.Add(new SqlParameter("@Keterangan", objBM.Keterangan));

            sqlListParam.Add(new SqlParameter("@OutM3L1", objBM.OutM3L1));
            sqlListParam.Add(new SqlParameter("@QtySPBL1", objBM.QtySPBL1));
            sqlListParam.Add(new SqlParameter("@EfesiensiL1", objBM.EfesiensiL1));
            sqlListParam.Add(new SqlParameter("@PPML1", objBM.PPML1));
            sqlListParam.Add(new SqlParameter("@PakaiBBL1", objBM.PakaiBBL1));

            sqlListParam.Add(new SqlParameter("@OutM3L2", objBM.OutM3L2));
            sqlListParam.Add(new SqlParameter("@QtySPBL2", objBM.QtySPBL2));
            sqlListParam.Add(new SqlParameter("@EfesiensiL2", objBM.EfesiensiL2));
            sqlListParam.Add(new SqlParameter("@PPML2", objBM.PPML2));
            sqlListParam.Add(new SqlParameter("@PakaiBBL2", objBM.PakaiBBL2));

            sqlListParam.Add(new SqlParameter("@OutM3L3", objBM.OutM3L3));
            sqlListParam.Add(new SqlParameter("@QtySPBL3", objBM.QtySPBL3));
            sqlListParam.Add(new SqlParameter("@EfesiensiL3", objBM.EfesiensiL3));
            sqlListParam.Add(new SqlParameter("@PPML3", objBM.PPML3));
            sqlListParam.Add(new SqlParameter("@PakaiBBL3", objBM.PakaiBBL3));

            sqlListParam.Add(new SqlParameter("@OutM3L4", objBM.OutM3L4));
            sqlListParam.Add(new SqlParameter("@QtySPBL4", objBM.QtySPBL4));
            sqlListParam.Add(new SqlParameter("@EfesiensiL4", objBM.EfesiensiL4));
            sqlListParam.Add(new SqlParameter("@PPML4", objBM.PPML4));
            sqlListParam.Add(new SqlParameter("@PakaiBBL4", objBM.PakaiBBL4));

            sqlListParam.Add(new SqlParameter("@OutM3L5", objBM.OutM3L5));
            sqlListParam.Add(new SqlParameter("@QtySPBL5", objBM.QtySPBL5));
            sqlListParam.Add(new SqlParameter("@EfesiensiL5", objBM.EfesiensiL5));
            sqlListParam.Add(new SqlParameter("@PPML5", objBM.PPML5));
            sqlListParam.Add(new SqlParameter("@PakaiBBL5", objBM.PakaiBBL5));

            sqlListParam.Add(new SqlParameter("@OutM3L6", objBM.OutM3L6));
            sqlListParam.Add(new SqlParameter("@QtySPBL6", objBM.QtySPBL6));
            sqlListParam.Add(new SqlParameter("@EfesiensiL6", objBM.EfesiensiL6));
            sqlListParam.Add(new SqlParameter("@PPML6", objBM.PPML6));
            sqlListParam.Add(new SqlParameter("@PakaiBBL6", objBM.PakaiBBL6));

            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "BMFloculant_Insert");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }

    }
    public int UpdatePerItem(object objDomain)
    {
        try
        {
            objBM = (DomainBMReport2)objDomain;
            sqlListParam = new List<SqlParameter>();

            sqlListParam.Add(new SqlParameter("@Tanggal", objBM.Tanggal));
            sqlListParam.Add(new SqlParameter("@Tanggal2", objBM.Tanggal2));

            sqlListParam.Add(new SqlParameter("@Keterangan", objBM.Keterangan));

            sqlListParam.Add(new SqlParameter("@OutM3L1", objBM.OutM3L1));
            sqlListParam.Add(new SqlParameter("@PakaiBBL1", objBM.PakaiBBL1));
            sqlListParam.Add(new SqlParameter("@QtySPBL1", objBM.QtySPBL1));
            sqlListParam.Add(new SqlParameter("@EfesiensiL1", objBM.EfesiensiL1));
            sqlListParam.Add(new SqlParameter("@PPML1", objBM.PPML1));

            sqlListParam.Add(new SqlParameter("@OutM3L2", objBM.OutM3L2));
            sqlListParam.Add(new SqlParameter("@PakaiBBL2", objBM.PakaiBBL2));
            sqlListParam.Add(new SqlParameter("@QtySPBL2", objBM.QtySPBL2));
            sqlListParam.Add(new SqlParameter("@EfesiensiL2", objBM.EfesiensiL2));
            sqlListParam.Add(new SqlParameter("@PPML2", objBM.PPML2));

            sqlListParam.Add(new SqlParameter("@OutM3L3", objBM.OutM3L3));
            sqlListParam.Add(new SqlParameter("@PakaiBBL3", objBM.PakaiBBL3));
            sqlListParam.Add(new SqlParameter("@QtySPBL3", objBM.QtySPBL3));
            sqlListParam.Add(new SqlParameter("@EfesiensiL3", objBM.EfesiensiL3));
            sqlListParam.Add(new SqlParameter("@PPML3", objBM.PPML3));

            sqlListParam.Add(new SqlParameter("@OutM3L4", objBM.OutM3L4));
            sqlListParam.Add(new SqlParameter("@PakaiBBL4", objBM.PakaiBBL4));
            sqlListParam.Add(new SqlParameter("@QtySPBL4", objBM.QtySPBL4));
            sqlListParam.Add(new SqlParameter("@EfesiensiL4", objBM.EfesiensiL4));
            sqlListParam.Add(new SqlParameter("@PPML4", objBM.PPML4));

            sqlListParam.Add(new SqlParameter("@OutM3L5", objBM.OutM3L5));
            sqlListParam.Add(new SqlParameter("@PakaiBBL5", objBM.PakaiBBL5));
            sqlListParam.Add(new SqlParameter("@QtySPBL5", objBM.QtySPBL5));
            sqlListParam.Add(new SqlParameter("@EfesiensiL5", objBM.EfesiensiL5));
            sqlListParam.Add(new SqlParameter("@PPML5", objBM.PPML5));

            sqlListParam.Add(new SqlParameter("@OutM3L6", objBM.OutM3L6));
            sqlListParam.Add(new SqlParameter("@PakaiBBL6", objBM.PakaiBBL6));
            sqlListParam.Add(new SqlParameter("@QtySPBL6", objBM.QtySPBL6));
            sqlListParam.Add(new SqlParameter("@EfesiensiL6", objBM.EfesiensiL6));
            sqlListParam.Add(new SqlParameter("@PPML6", objBM.PPML6));


            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "BMFloculant_UpdatePerItem");
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
            objBM = (DomainBMReport2)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@Periode", objBM.Periode));
            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "BMFloculant_Cancel");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }
    public int CancelItem(object objDomain)
    {
        try
        {
            objBM = (DomainBMReport2)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@Tanggal", objBM.Tanggal));
            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "BMFloculant_CancelItem");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }

    private string MatrixQuery220822(string Periode, string Periode2)
    {
        string result =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_Atas]') AND type in (N'U')) DROP TABLE [dbo].[Temp_Atas]   " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_Bawah]') AND type in (N'U')) DROP TABLE [dbo].[Temp_Bawah]    " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataPinal]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataPinal]    " +

        " declare @date datetime " +
        " set @date = '" + Periode + "'; " +

        "  ;with DaysInMonth as (select @date as Date  " +
        "  union all   " +
        "  select dateadd(dd,1,Date)  from DaysInMonth  where month(date) = month(@Date)),   " +
        "  " +
        " data_OutPut as ( " +
        " select sum(M3)M3,TglProduksi tgl,Line from ( " +
        " select x.*,((A.Tebal*A.Lebar*A.Panjang)/1000000000)* x.Qty M3,A.Kode,A.Tebal,isnull(A.Pressing,'')Pressing from ( " +
        " select ItemID,sum(Qty)Qty,TglProduksi,PlantID Line from BM_Destacking where left(convert(char,tglproduksi,112),6)='" + Periode2 + "' " +
        " and RowStatus>-1 and LokasiID not in (select ID from fc_lokasi where lokasi like'%adj%' and rowstatus>-1) group by ItemID,TglProduksi,PlantID  " +
        " ) as x inner join FC_Items A ON A.ID=x.ItemID) as x group by  TglProduksi,Line), " +
        "  " +
        " data_BB_all as ( " +
        " select A.Quantity Qty,isnull(A.GroupFloo,'')GroupFloo,A.ProdLine Line,B.PakaiDate Tgl from PakaiDetail A inner join Pakai B ON A.PakaiID=B.ID where A.RowStatus>-1 and B.Status>2 and A.GroupID=1 and left(convert(char,B.PakaiDate,112),6)='" + Periode2 + "' and B.DeptID=2 ), " +
        "  " +
        " data_BB_FLo as ( " +
        " select A.Quantity Qty,isnull(A.GroupFloo,'')GroupFloo,A.ProdLine Line,B.PakaiDate Tgl from PakaiDetail A inner join Pakai B ON A.PakaiID=B.ID where A.RowStatus>-1 and B.Status>2 and A.GroupID=2 and left(convert(char,B.PakaiDate,112),6)='" + Periode2 + "' and B.DeptID=2 and A.ItemID in (select ItemID from BM_PFloculantItem where RowStatus>-1 and Keterangan='FLOOCULANT')), " +
        "  " +
        " data_L1 as ( " +
        " select Tgl,sum(O1)O1,sum(P1)P1,sum(PF1)PF1,isnull((sum(nullif(PF1,0))/sum(O1)),0)E1,(isnull((sum(nullif(PF1,0))/sum(P1)),0)*1000000)PP1 from ( " +
        " select Date Tgl,'0'O1,'0'P1,'0'PF1 from DaysInMonth where month(date) = month(@Date)  " +
        " union all " +
        " select Tgl,M3,'0'P1,'0'PF1 from data_OutPut where Line='1' " +
        " union all " +
        " select Tgl,'0'O1,sum(Qty)Qty,'0'PF1 from data_BB_all  where Line='1' group by Tgl " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,sum(Qty)Qty from data_BB_FLo  where Line='1' group by Tgl " +
        " ) as x group by Tgl ), " +
        "  " +
        " data_L2 as ( " +
        " select Tgl,sum(O1)O2,sum(P1)P2,sum(PF1)PF2,isnull((sum(nullif(PF1,0))/sum(O1)),0)E2,(isnull((sum(nullif(PF1,0))/sum(P1)),0)*1000000)PP2 from ( " +
        " select Date Tgl,'0'O1,'0'P1,'0'PF1 from DaysInMonth where month(date) = month(@Date)  " +
        " union all " +
        " select Tgl,M3,'0'P1,'0'PF1 from data_OutPut where Line='2' " +
        " union all " +
        " select Tgl,'0'O1,sum(Qty)Qty,'0'PF1 from data_BB_all  where Line='2' group by Tgl " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,sum(Qty)Qty from data_BB_FLo  where Line='2' group by Tgl " +
        " ) as x group by Tgl ), " +
        "  " +
        " data_L3 as ( " +
        " select Tgl,sum(O1)O3,sum(P1)P3,sum(PF1)PF3,isnull((sum(nullif(PF1,0))/sum(O1)),0)E3,(isnull((sum(nullif(PF1,0))/sum(P1)),0)*1000000)PP3 from ( " +
        " select Date Tgl,'0'O1,'0'P1,'0'PF1 from DaysInMonth where month(date) = month(@Date)  " +
        " union all " +
        " select Tgl,M3,'0'P1,'0'PF1 from data_OutPut where Line='3' " +
        " union all " +
        " select Tgl,'0'O1,sum(Qty)Qty,'0'PF1 from data_BB_all  where Line='3' group by Tgl " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,sum(Qty)Qty from data_BB_FLo  where Line='3' group by Tgl " +
        " ) as x group by Tgl ), " +
        "  " +
        " data_L4 as ( " +
        " select Tgl,sum(O1)O4,sum(P1)P4,sum(PF1)PF4,isnull((sum(nullif(PF1,0))/sum(O1)),0)E4,(isnull((sum(nullif(PF1,0))/sum(P1)),0)*1000000)PP4 from ( " +
        " select Date Tgl,'0'O1,'0'P1,'0'PF1 from DaysInMonth where month(date) = month(@Date)  " +
        " union all " +
        " select Tgl,M3,'0'P1,'0'PF1 from data_OutPut where Line='4' " +
        " union all " +
        " select Tgl,'0'O1,sum(Qty)Qty,'0'PF1 from data_BB_all  where Line='4' group by Tgl " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,sum(Qty)Qty from data_BB_FLo  where Line='4' group by Tgl " +
        " ) as x group by Tgl ), " +
        "  " +
        " data_L5 as ( " +
        " select Tgl,sum(O1)O5,sum(P1)P5,sum(PF1)PF5,isnull((sum(nullif(PF1,0))/sum(O1)),0)E5,(isnull((sum(nullif(PF1,0))/sum(P1)),0)*1000000)PP5 from ( " +
        " select Date Tgl,'0'O1,'0'P1,'0'PF1 from DaysInMonth where month(date) = month(@Date)  " +
        " union all " +
        " select Tgl,M3,'0'P1,'0'PF1 from data_OutPut where Line='5' " +
        " union all " +
        " select Tgl,'0'O1,sum(Qty)Qty,'0'PF1 from data_BB_all  where Line='5' group by Tgl " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,sum(Qty)Qty from data_BB_FLo  where Line='5' group by Tgl " +
        " ) as x group by Tgl ), " +
        "  " +
        " data_L6 as ( " +
        " select Tgl,sum(O1)O6,sum(P1)P6,sum(PF1)PF6,isnull((sum(nullif(PF1,0))/sum(O1)),0)E6,(isnull((sum(nullif(PF1,0))/sum(P1)),0)*1000000)PP6 from ( " +
        " select Date Tgl,'0'O1,'0'P1,'0'PF1 from DaysInMonth where month(date) = month(@Date)  " +
        " union all " +
        " select Tgl,M3,'0'P1,'0'PF1 from data_OutPut where Line='6' " +
        " union all " +
        " select Tgl,'0'O1,sum(Qty)Qty,'0'PF1 from data_BB_all  where Line='6' group by Tgl " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,sum(Qty)Qty from data_BB_FLo  where Line='6' group by Tgl " +
        " ) as x group by Tgl ), " +
        "  " +
        " Data_Fix as ( " +
        " select left(convert(char,Tgl,106),11)Tanggal, " +
        " sum(O1)OutM3L1,sum(P1)QtySPB_BBL1,sum(PF1)QtySPBL1,sum(E1)EfesiensiL1,sum(PP1)PPML1, " +
        " sum(O2)OutM3L2,sum(P2)QtySPB_BBL2,sum(PF2)QtySPBL2,sum(E2)EfesiensiL2,sum(PP2)PPML2, " +
        " sum(O3)OutM3L3,sum(P3)QtySPB_BBL3,sum(PF3)QtySPBL3,sum(E3)EfesiensiL3,sum(PP3)PPML3, " +
        " sum(O4)OutM3L4,sum(P4)QtySPB_BBL4,sum(PF4)QtySPBL4,sum(E4)EfesiensiL4,sum(PP4)PPML4, " +
        " sum(O5)OutM3L5,sum(P5)QtySPB_BBL5,sum(PF5)QtySPBL5,sum(E5)EfesiensiL5,sum(PP5)PPML5, " +
        " sum(O6)OutM3L6,sum(P6)QtySPB_BBL6,sum(PF6)QtySPBL6,sum(E6)EfesiensiL6,sum(PP6)PPML6 " +
        " from ( " +
        " select *,'0'O2,'0'P2,'0'PF2,'0'E2,'0'PP2,'0'O3,'0'P3,'0'PF3,'0'E3,'0'PP3,'0'O4,'0'P4,'0'PF4,'0'E4,'0'PP4,'0'O5,'0'P5,'0'PF5,'0'E5,'0'PP5,'0'O6,'0'P6,'0'PF6,'0'E6,'0'PP6   " +
        " from data_L1 " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,'0'PF1,'0'E1,'0'PP1,O2,P2,PF2,E2,PP2,'0'O3,'0'P3,'0'PF3,'0'E3,'0'PP3,'0'O4,'0'P4,'0'PF4,'0'E4,'0'PP4,'0'O5,'0'P5,'0'PF5,'0'E5,'0'PP5,'0'O6,'0'P6,'0'PF6,'0'E6,'0'PP6    " +
        " from data_L2 " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,'0'PF1,'0'E1,'0'PP1,'0'O2,'0'P2,'0'PF2,'0'E2,'0'PP2,O3,P3,PF3,E3,PP3,'0'O4,'0'P4,'0'PF4,'0'E4,'0'PP4,'0'O5,'0'P5,'0'PF5,'0'E5,'0'PP5,'0'O6,'0'P6,'0'PF6,'0'E6,'0'PP6   " +
        " from data_L3 " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,'0'PF1,'0'E1,'0'PP1,'0'O2,'0'P2,'0'PF2,'0'E2,'0'PP2,'0'O3,'0'P3,'0'PF3,'0'E3,'0'PP3,O4,P4,PF4,E4,PP4,'0'O5,'0'P5,'0'PF5,'0'E5,'0'PP5,'0'O6,'0'P6,'0'PF6,'0'E6,'0'PP6 " +
        " from data_L4 " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,'0'PF1,'0'E1,'0'PP1,'0'O2,'0'P2,'0'PF2,'0'E2,'0'PP2,'0'O3,'0'P3,'0'PF3,'0'E3,'0'PP3,'0'O4,'0'P4,'0'PF4,'0'E4,'0'PP4,O5,P5,PF5,E5,PP5,'0'O6,'0'P6,'0'PF6,'0'E6,'0'PP6 " +
        " from data_L5 " +
        " union all " +
        " select Tgl,'0'O1,'0'P1,'0'PF1,'0'E1,'0'PP1,'0'O2,'0'P2,'0'PF2,'0'E2,'0'PP2,'0'O3,'0'P3,'0'PF3,'0'E3,'0'PP3,'0'O4,'0'P4,'0'PF4,'0'E4,'0'PP4,'0'O5,'0'P5,'0'PF5,'0'E5,'0'PP5,O6,P6,PF6,E6,PP6 " +
        " from data_L6 " +
        " ) as x group by Tgl " +
        " ) " +
        " select * into Temp_Atas from Data_Fix order by Tanggal " +

        /** Bagian Bawah **/
        " /** Bawah **/ " +
        " ;with data_OutPut as ( " +
        " select x.*,((A.Tebal*A.Lebar*A.Panjang)/1000000000)* x.Qty M3,A.Kode,A.Tebal,isnull(A.Pressing,'')Pressing from ( " +
        " select ItemID,sum(Qty)Qty,TglProduksi,PlantID Line from BM_Destacking where left(convert(char,tglproduksi,112),6)='" + Periode2 + "' and RowStatus>-1 group by ItemID,TglProduksi,PlantID  " +
        " ) as x inner join FC_Items A ON A.ID=x.ItemID ), " +
        "  " +
        " data_BB_all as (select A.Quantity Qty,isnull(A.GroupFloo,'')GroupFloo,A.ProdLine Line from PakaiDetail A inner join Pakai B ON A.PakaiID=B.ID where A.RowStatus>-1 and B.Status>2 and A.GroupID=1 and left(convert(char,B.PakaiDate,112),6)='" + Periode2 + "' and B.DeptID=2 ), " +
        "  " +
        " data_BB_FLo as (select A.Quantity Qty,isnull(A.GroupFloo,'')GroupFloo,A.ProdLine Line from PakaiDetail A inner join Pakai B ON A.PakaiID=B.ID where A.RowStatus>-1 and B.Status>2 and A.GroupID=2 and left(convert(char,B.PakaiDate,112),6)='" + Periode2 + "' and B.DeptID=2 and A.ItemID in (select ItemID from BM_PFloculantItem where RowStatus>-1 and Keterangan='FLOOCULANT')), " +
        "  " +
        " Data_L1 as ( " +
        " select Urut,Noted,sum(Qty)O1,sum(Qty1)P1,sum(Qty2)PF1,isnull((sum(nullif(Qty2,0))/sum(Qty)),0)E1,isnull(((sum(nullif(Qty2,0))/sum(Qty1))*1000000),0)PP1  " +
        " from ( " +
        " /** Header **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " /** OutPut **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal<15 and Pressing='YES' and Line='1' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal>=15 and Pressing='YES' and Line='1' group by Line union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Pressing='NO' and Line='1' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Line='1' group by Line union all " +
        " /** SPB BB **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='<15 Press' and Line='1' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='>=15 Press' and Line='1' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='Non Press' and Line='1' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where Line='1' group by Line union all " +
        " /** SPB Flooculant **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='<15 Press' and Line='1' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='>=15 Press' and Line='1' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='Non Press' and Line='1' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where Line='1' group by Line  " +
        " ) as A group by urut,Noted) , " +
        "  " +
        " Data_L2 as ( " +
        " select Urut,Noted,sum(Qty)O2,sum(Qty1)P2,sum(Qty2)PF2,isnull((sum(nullif(Qty2,0))/sum(Qty)),0)E2,isnull(((sum(nullif(Qty2,0))/sum(Qty1))*1000000),0)PP2  " +
        " from ( " +
        " /** Header **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " /** OutPut **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal<15 and Pressing='YES' and Line='2' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal>=15 and Pressing='YES' and Line='2' group by Line union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Pressing='NO' and Line='2' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Line='2' group by Line union all " +
        " /** SPB BB **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='<15 Press' and Line='2' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='>=15 Press' and Line='2' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='Non Press' and Line='2' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where Line='2' group by Line union all " +
        " /** SPB Flooculant **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='<15 Press' and Line='2' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='>=15 Press' and Line='2' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='Non Press' and Line='2' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where Line='2' group by Line  " +
        " ) as A group by urut,Noted) , " +
        "  " +
        " Data_L3 as ( " +
        " select Urut,Noted,sum(Qty)O3,sum(Qty1)P3,sum(Qty2)PF3,isnull((sum(nullif(Qty2,0))/sum(nullif(Qty,0))),0)E3,isnull(((sum(nullif(Qty2,0))/sum(nullif(Qty1,0)))*1000000),0)PP3  " +
        " from ( " +
        " /** Header **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " /** OutPut **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal<15 and Pressing='YES' and Line='3' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal>=15 and Pressing='YES' and Line='3' group by Line union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Pressing='NO' and Line='3' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Line='3' group by Line union all " +
        " /** SPB BB **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='<15 Press' and Line='3' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='>=15 Press' and Line='3' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='Non Press' and Line='3' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where Line='3' group by Line union all " +
        " /** SPB Flooculant **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='<15 Press' and Line='3' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='>=15 Press' and Line='3' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='Non Press' and Line='3' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where Line='3' group by Line  " +
        " ) as A group by urut,Noted) , " +
        "  " +
        " Data_L4 as ( " +
        " select Urut,Noted,sum(Qty)O4,sum(Qty1)P4,sum(Qty2)PF4,isnull((sum(nullif(Qty2,0))/sum(nullif(Qty,0))),0)E4,isnull(((sum(nullif(Qty2,0))/sum(nullif(Qty1,0)))*1000000),0)PP4  " +
        " from ( " +
        " /** Header **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " /** OutPut **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal<15 and Pressing='YES' and Line='4' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal>=15 and Pressing='YES' and Line='4' group by Line union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Pressing='NO' and Line='4' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Line='4' group by Line union all " +
        " /** SPB BB **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='<15 Press' and Line='4' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='>=15 Press' and Line='4' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='Non Press' and Line='4' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where Line='4' group by Line union all " +
        " /** SPB Flooculant **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='<15 Press' and Line='4' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='>=15 Press' and Line='4' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='Non Press' and Line='4' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where Line='4' group by Line  " +
        " ) as A group by urut,Noted) , " +
        "  " +
        " Data_L5 as ( " +
        " select Urut,Noted,sum(Qty)O5,sum(Qty1)P5,sum(Qty2)PF5,isnull((sum(nullif(Qty2,0))/sum(nullif(Qty,0))),0)E5,isnull(((sum(nullif(Qty2,0))/sum(nullif(Qty1,0)))*1000000),0)PP5  " +
        " from ( " +
        " /** Header **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " /** OutPut **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal<15 and Pressing='YES' and Line='5' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal>=15 and Pressing='YES' and Line='5' group by Line union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Pressing='NO' and Line='5' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Line='5' group by Line union all " +
        " /** SPB BB **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='<15 Press' and Line='5' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='>=15 Press' and Line='5' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='Non Press' and Line='5' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where Line='5' group by Line union all " +
        " /** SPB Flooculant **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='<15 Press' and Line='5' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='>=15 Press' and Line='5' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='Non Press' and Line='5' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where Line='5' group by Line  " +
        " ) as A group by urut,Noted) , " +
        "  " +
        " Data_L6 as ( " +
        " select Urut,Noted,sum(Qty)O6,sum(Qty1)P6,sum(Qty2)PF6,isnull((sum(nullif(Qty2,0))/sum(nullif(Qty,0))),0)E6,isnull(((sum(nullif(Qty2,0))/sum(nullif(Qty1,0)))*1000000),0)PP6  " +
        " from ( " +
        " /** Header **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,'0'Qty2 union all " +
        " /** OutPut **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal<15 and Pressing='YES' and Line='6' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Tebal>=15 and Pressing='YES' and Line='6' group by Line union all " +
        " select '7'Urut,'Jumlah Out put Nonpress'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Pressing='NO' and Line='6' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,sum(M3)'Qty','0'Qty1,'0'Qty2 from data_OutPut where Line='6' group by Line union all " +
        " /** SPB BB **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='<15 Press' and Line='6' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='>=15 Press' and Line='6' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where GroupFloo='Non Press' and Line='6' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,sum(Qty)Qty1,'0'Qty2 from data_BB_all where Line='6' group by Line union all " +
        " /** SPB Flooculant **/ " +
        " select '5'Urut,'Jumlah Out put < 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='<15 Press' and Line='6' group by Line union all " +
        " select '6'Urut,'Jumlah Out put >= 15 mm PRESS'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='>=15 Press' and Line='6' group by Line union all " +
        " select '7'Urut,'Jumlah Out put NonPress'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where GroupFloo='Non Press' and Line='6' group by Line union all " +
        " select '8'Urut,'Total Pemakaian Flocc'Noted,'0'Qty,'0'Qty1,sum(Qty)Qty2 from data_BB_FLo where Line='6' group by Line  " +
        " ) as A group by urut,Noted) , " +
        "  " +
        " Data_Fix as ( " +
        " select Urut,Noted, " +
        " sum(O1)OutM3L1,sum(P1)QtySPB_BBL1,sum(PF1)QtySPBL1,sum(E1)EfesiensiL1,sum(PP1)PPML1, " +
        " sum(O2)OutM3L2,sum(P2)QtySPB_BBL2,sum(PF2)QtySPBL2,sum(E2)EfesiensiL2,sum(PP2)PPML2, " +
        " sum(O3)OutM3L3,sum(P3)QtySPB_BBL3,sum(PF3)QtySPBL3,sum(E3)EfesiensiL3,sum(PP3)PPML3, " +
        " sum(O4)OutM3L4,sum(P4)QtySPB_BBL4,sum(PF4)QtySPBL4,sum(E4)EfesiensiL4,sum(PP4)PPML4, " +
        " sum(O5)OutM3L5,sum(P5)QtySPB_BBL5,sum(PF5)QtySPBL5,sum(E5)EfesiensiL5,sum(PP5)PPML5, " +
        " sum(O6)OutM3L6,sum(P6)QtySPB_BBL6,sum(PF6)QtySPBL6,sum(E6)EfesiensiL6,sum(PP6)PPML6 " +
        " from ( " +
        " select *,'0'O2,'0'P2,'0'PF2,'0'E2,'0'PP2,'0'O3,'0'P3,'0'PF3,'0'E3,'0'PP3,'0'O4,'0'P4,'0'PF4,'0'E4,'0'PP4,'0'O5,'0'P5,'0'PF5,'0'E5,'0'PP5,'0'O6,'0'P6,'0'PF6,'0'E6,'0'PP6   " +
        " from Data_L1 " +
        " union all " +
        " select Urut,Noted,'0'O1,'0'P1,'0'PF1,'0'E1,'0'PP1,O2,P2,PF2,E2,PP2,'0'O3,'0'P3,'0'PF3,'0'E3,'0'PP3,'0'O4,'0'P4,'0'PF4,'0'E4,'0'PP4,'0'O5,'0'P5,'0'PF5,'0'E5,'0'PP5,'0'O6,'0'P6,'0'PF6,'0'E6,'0'PP6 " +
        " from Data_L2 " +
        " union all " +
        " select Urut,Noted,'0'O1,'0'P1,'0'PF1,'0'E1,'0'PP1,'0'O2,'0'P2,'0'PF2,'0'E2,'0'PP2,O3,P3,PF3,E3,PP3,'0'O4,'0'P4,'0'PF4,'0'E4,'0'PP4,'0'O5,'0'P5,'0'PF5,'0'E5,'0'PP5,'0'O6,'0'P6,'0'PF6,'0'E6,'0'PP6  " +
        " from Data_L3 " +
        " union all " +
        " select Urut,Noted,'0'O1,'0'P1,'0'PF1,'0'E1,'0'PP1,'0'O2,'0'P2,'0'PF2,'0'E2,'0'PP2,'0'O3,'0'P3,'0'PF3,'0'E3,'0'PP3,O4,P4,PF4,E4,PP4,'0'O5,'0'P5,'0'PF5,'0'E5,'0'PP5,'0'O6,'0'P6,'0'PF6,'0'E6,'0'PP6 " +
        " from Data_L4 " +
        " union all " +
        " select Urut,Noted,'0'O1,'0'P1,'0'PF1,'0'E1,'0'PP1,'0'O2,'0'P2,'0'PF2,'0'E2,'0'PP2,'0'O3,'0'P3,'0'PF3,'0'E3,'0'PP3,'0'O4,'0'P4,'0'PF4,'0'E4,'0'PP4,O5,P5,PF5,E5,PP5,'0'O6,'0'P6,'0'PF6,'0'E6,'0'PP6 " +
        " from Data_L5 " +
        " union all " +
        " select Urut,Noted,'0'O1,'0'P1,'0'PF1,'0'E1,'0'PP1,'0'O2,'0'P2,'0'PF2,'0'E2,'0'PP2,'0'O3,'0'P3,'0'PF3,'0'E3,'0'PP3,'0'O4,'0'P4,'0'PF4,'0'E4,'0'PP4,'0'O5,'0'P5,'0'PF5,'0'E5,'0'PP5,O6,P6,PF6,E6,PP6 " +
        " from Data_L6 " +
        " ) as x group by Urut,Noted " +
        " ) " +
        " select * into Temp_Bawah from Data_Fix " +
        "  " +
        " select * into Temp_DataPinal from ( " +
        " select '1'Urut,* from Temp_Atas  " +
        " union all " +
        " select '2'Urut,'Total', " +
        " sum(OutM3L1),sum(QtySPB_BBL1),sum(QtySPBL1),isnull((sum(nullif(QtySPBL1,0))/sum(OutM3L1)),0),isnull(((sum(nullif(QtySPBL1,0))/sum(QtySPB_BBL1))*1000000),0), " +
        " sum(OutM3L2),sum(QtySPB_BBL2),sum(QtySPBL2),isnull((sum(nullif(QtySPBL2,0))/sum(OutM3L2)),0),isnull(((sum(nullif(QtySPBL2,0))/sum(QtySPB_BBL2))*1000000),0), " +
        " sum(OutM3L3),sum(QtySPB_BBL3),sum(QtySPBL3),isnull((sum(nullif(QtySPBL3,0))/sum(OutM3L3)),0),isnull(((sum(nullif(QtySPBL3,0))/sum(QtySPB_BBL3))*1000000),0), " +
        " sum(OutM3L4),sum(QtySPB_BBL4),sum(QtySPBL4),isnull((sum(nullif(QtySPBL4,0))/sum(OutM3L4)),0),isnull(((sum(nullif(QtySPBL4,0))/sum(QtySPB_BBL4))*1000000),0), " +
        " sum(OutM3L5),sum(QtySPB_BBL5),sum(QtySPBL5),isnull((sum(nullif(QtySPBL5,0))/sum(OutM3L5)),0),isnull(((sum(nullif(QtySPBL5,0))/sum(QtySPB_BBL5))*1000000),0), " +
        " sum(OutM3L6),sum(QtySPB_BBL6),sum(QtySPBL6),isnull((sum(nullif(QtySPBL6,0))/sum(OutM3L6)),0),isnull(((sum(nullif(QtySPBL6,0))/sum(QtySPB_BBL6))*1000000),0) from Temp_Atas " +
        " union all " +
        " select * from Temp_Bawah ) as z order by Urut " +
        "  " +
        " select * from Temp_DataPinal " +
        "  " +
        "  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_Atas]') AND type in (N'U')) DROP TABLE [dbo].[Temp_Atas]   " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_Bawah]') AND type in (N'U')) DROP TABLE [dbo].[Temp_Bawah]   " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataPinal]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataPinal]    ";



        return result;
    }

    private string MatrixQuery(string Periode, string Periode2)
    {
        string result =

        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_Atas]') AND type in (N'U')) DROP TABLE [dbo].[Temp_Atas]   " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataOutput]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataOutput]   " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataSPB]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataSPB]  " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataFloo]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataFloo] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_TotOut]') AND type in (N'U')) DROP TABLE [dbo].[Temp_TotOut] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempAwal]') AND type in (N'U')) DROP TABLE [dbo].[TempAwal] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBawah1]') AND type in (N'U')) DROP TABLE [dbo].[TempBawah1] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBawah2]') AND type in (N'U')) DROP TABLE [dbo].[TempBawah2] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempJoinAtas]') AND type in (N'U')) DROP TABLE [dbo].[TempJoinAtas] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempJoinBawah]') AND type in (N'U')) DROP TABLE [dbo].[TempJoinBawah] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempJoinAll]') AND type in (N'U')) DROP TABLE [dbo].[TempJoinAll]  " +

        "declare @thnbln varchar(6) declare @line varchar(2)  declare @date datetime " +
        "set @thnbln='" + Periode2 + "' " +
        "set @date ='" + Periode + "' " +

        "/** temp data Temp_DataOutput [ OutPut Produksi ] **/ " +
        "select Volume*Qty M3,tgl,Line,shift,Noted into Temp_DataOutput from ( " +
        "select sum(Qty)Qty,((tebal*lebar*Panjang)/1000000000)Volume,shift,tgl,Line,Noted from ( " +
        "select x.Qty,A.Tebal,A.Lebar,A.Panjang,shift,Tgl,Line,case when Pressing='NO' then 'Non Press' when Pressing='YES' and Tebal>=15 then '>=15 Press' else '<15 Press' end Noted from ( " +
        "select sum(Qty)Qty,shift,ItemID,left(convert(char,TglProduksi,112),8)Tgl,PlantID Line from BM_Destacking where Qty>0 and left(convert(char,tglproduksi,112),6)=@thnbln and RowStatus>-1 and LokasiID not in (select ID from fc_lokasi where lokasi like'%adj%' and rowstatus>-1) group by ItemID,shift,TglProduksi,PlantID ) as x inner join FC_Items A ON A.ID=x.ItemID) as xx group by tebal,lebar,Panjang,shift,tgl,Line,Noted ) as xxx " +

        "/** temp data Temp_DataSPB [ SPB Bahan Baku All Dept. BM ] **/ " +
        "select sum(A.Quantity) Qty/**,isnull(A.GroupFloo,'')Noted**/,A.ProdLine Line,B.PakaiDate Tgl,substring(A.Keterangan,7,1)Keterangan into Temp_DataSPB " +
        "from PakaiDetail A  " +
        "inner join Pakai B ON A.PakaiID=B.ID  " +
        "where A.RowStatus>-1 and B.Status>2 and A.GroupID=1 and left(convert(char,B.PakaiDate,112),6)=@thnbln and B.DeptID=2 group by A.ProdLine,B.PakaiDate,A.Keterangan/**,A.GroupFloo**/ " +

        "/** temp data Temp_DataFloo [ SPB Bahan Baku Flooculant Dept. BM ] **/ " +
        "select A.Quantity Qty,isnull(A.GroupFloo,'')Noted,A.ProdLine Line,B.PakaiDate Tgl into Temp_DataFloo " +
        "from PakaiDetail A  " +
        "inner join Pakai B ON A.PakaiID=B.ID " +
        "where A.RowStatus>-1 and B.Status>2 and A.GroupID=2 and left(convert(char,B.PakaiDate,112),6)=@thnbln and B.DeptID=2 and A.ItemID in (select ItemID from BM_PFloculantItem where RowStatus>-1 and Keterangan='FLOOCULANT') " +

        ";with tempJoin1 as (select isnull(A.Qty,0)QtySPB,B.M3,B.Tgl,B.shift,B.Noted,B.Line from Temp_DataOutput B left join Temp_DataSPB A ON B.shift=A.Keterangan and B.Tgl=A.Tgl and B.Line=A.Line), " +
        " C as (select isnull(A.Qty,0)QtySPB,B.M3,B.Tgl,B.shift,B.Noted,B.Line from tempJoin1 B left join Temp_DataSPB A ON B.shift=A.Keterangan and B.Tgl=A.Tgl and B.Line=A.Line ), " +
        " D  as (select A.*,cast((QtySPB * (M3/isnull((select sum(M3) from C B where B.tgl=A.tgl and B.shift=A.shift and B.Line=A.Line),0))) as decimal(18,2)) PakaiBB  from C A )  " +
        " select * into TempAwal from D " +

        " ;with " +
        " DaysInMonth as (select @date as Date union all select dateadd(dd,1,Date) from DaysInMonth where month(date)= month(@date)),     " +
        " temp1 as (select Tgl,round(sum(PakaiBB),1)QtySPB_All,sum(M3)OutPutM3,'0'QtyFloo,Noted,Line from TempAwal  group by Noted ,tgl,Line), " +
        " temp2 as (select Tgl Tanggal,sum(QtySPB_All)QtySPB_BB,sum(OutPutM3)OutM3,sum(QtyFloo)QtySPB_Flo,Noted,Line from ( " +
        " select Tgl,QtySPB_All,OutPutM3,QtyFloo,Noted,Line from temp1 " +
        " union all " +
        " select Tgl,'0'QtySPB_All,'0'OutPutM3,Qty,Noted,Line from Temp_DataFloo  " +
        " ) as x group by tgl,Noted,Line ), " +

        "temp3 as (select Tanggal,OutM3,QtySPB_BB,QtySPB_Flo,Noted,b.Konversi Konversi,Line from temp2 a left join BM_PFloculantKonversi b ON a.Noted=b.Keterangan where b.Rowstatus>-1 ), " +

        "Line1 as (select left(convert(char,Tanggal,106),11)Tanggal,OutM3 OutM3L1,QtySPB_Flo QtySPBFlo_L1,QtySPB_BB QtySPB_BBL1, " +
        "case when Noted in ('Non Press','>=15 Press') then isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2))*cast(0.25 as decimal(18,2)) as decimal(18,2)),0) else  " +
        "isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2)) as decimal(18,2)),0) end EfesiensiL1, " +
        "isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(QtySPB_BB as decimal(18,5)),0)*1000000 as decimal(18,0)),0)PPML1 from temp3  where Line=1 ), " +

        "Line11 as (select ROW_NUMBER() over (PARTITION by Tanggal order by Tanggal asc) as Urut,* from Line1 ), " +

        "Line2 as (select left(convert(char,Tanggal,106),11)Tanggal,OutM3 OutM3L2,QtySPB_Flo QtySPBFlo_L2,QtySPB_BB QtySPB_BBL2, " +
        "case when Noted in ('Non Press','>=15 Press') then isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2))*cast(0.25 as decimal(18,2)) as decimal(18,2)),0) else  " +
        "isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2)) as decimal(18,2)),0) end EfesiensiL2, " +
        "isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(QtySPB_BB as decimal(18,5)),0)*1000000 as decimal(18,0)),0)PPML2 from temp3  where Line=2 ), " +

        "Line21 as (select ROW_NUMBER() over (PARTITION by Tanggal order by Tanggal asc) as Urut,* from Line2 ), " +

        "Line3 as (select left(convert(char,Tanggal,106),11)Tanggal,OutM3 OutM3L3,QtySPB_Flo QtySPBFlo_L3,QtySPB_BB QtySPB_BBL3, " +
        "case when Noted in ('Non Press','>=15 Press') then isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2))*cast(0.25 as decimal(18,2)) as decimal(18,2)),0) else  " +
        "isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2)) as decimal(18,2)),0) end EfesiensiL3, " +
        "isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(QtySPB_BB as decimal(18,5)),0)*1000000 as decimal(18,0)),0)PPML3 from temp3  where Line=3 ), " +

        "Line31 as (select ROW_NUMBER() over (PARTITION by Tanggal order by Tanggal asc) as Urut,* from Line3 ), " +

        "Line4 as (select left(convert(char,Tanggal,106),11)Tanggal,OutM3 OutM3L4,QtySPB_Flo QtySPBFlo_L4,QtySPB_BB QtySPB_BBL4, " +
        "case when Noted in ('Non Press','>=15 Press') then isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2))*cast(0.25 as decimal(18,2)) as decimal(18,2)),0) else  " +
        "isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2)) as decimal(18,2)),0) end EfesiensiL4, " +
        "isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(QtySPB_BB as decimal(18,5)),0)*1000000 as decimal(18,0)),0)PPML4 from temp3  where Line=4 ), " +

        "Line41 as (select ROW_NUMBER() over (PARTITION by Tanggal order by Tanggal asc) as Urut,* from Line4 ) " +

        "select Urut,left(convert(char,Tanggal,106),11)Tanggal, " +
        "sum(OutM3L1)OutM3L1,sum(QtySPB_BBL1)QtySPB_BBL1,sum(QtySPBFlo_L1)QtySPBFlo_L1,sum(EfesiensiL1)EfesiensiL1,sum(PPML1)PPML1, " +
        "sum(OutM3L2)OutM3L2,sum(QtySPB_BBL2)QtySPB_BBL2,sum(QtySPBFlo_L2)QtySPBFlo_L2,sum(EfesiensiL2)EfesiensiL2,sum(PPML2)PPML2, " +
        "sum(OutM3L3)OutM3L3,sum(QtySPB_BBL3)QtySPB_BBL3,sum(QtySPBFlo_L3)QtySPBFlo_L3,sum(EfesiensiL3)EfesiensiL3,sum(PPML3)PPML3, " +
        "sum(OutM3L4)OutM3L4,sum(QtySPB_BBL4)QtySPB_BBL4,sum(QtySPBFlo_L4)QtySPBFlo_L4,sum(EfesiensiL4)EfesiensiL4,sum(PPML4)PPML4 into Temp_Atas " +
        " from ( " +
        "select '1'Urut,[Date]Tanggal, " +
        "'0'OutM3L1,'0'QtySPBFlo_L1,'0'QtySPB_BBL1,'0'EfesiensiL1,'0'PPML1, " +
        "'0'OutM3L2,'0'QtySPBFlo_L2,'0'QtySPB_BBL2,'0'EfesiensiL2,'0'PPML2, " +
        "'0'OutM3L3,'0'QtySPBFlo_L3,'0'QtySPB_BBL3,'0'EfesiensiL3,'0'PPML3, " +
        "'0'OutM3L4,'0'QtySPBFlo_L4,'0'QtySPB_BBL4,'0'EfesiensiL4,'0'PPML4 " +
        "from DaysInMonth where month(date) = month(@Date)  " +
        "union all " +
        "select *, " +
        "'0'OutM3L2,'0'QtySPBFlo_L2,'0'QtySPB_BBL2,'0'EfesiensiL2,'0'PPML2, " +
        "'0'OutM3L3,'0'QtySPBFlo_L3,'0'QtySPB_BBL3,'0'EfesiensiL3,'0'PPML3, " +
        "'0'OutM3L4,'0'QtySPBFlo_L4,'0'QtySPB_BBL4,'0'EfesiensiL4,'0'PPML4 from Line11  " +
        "union all " +
        "select Urut,Tanggal, " +
        "'0'OutM3L1,'0'QtySPBFlo_L1,'0'QtySPB_BBL1,'0'EfesiensiL1,'0'PPML1, " +
        "OutM3L2,QtySPBFlo_L2,QtySPB_BBL2,EfesiensiL2,PPML2, " +
        "'0'OutM3L3,'0'QtySPBFlo_L3,'0'QtySPB_BBL3,'0'EfesiensiL3,'0'PPML3, " +
        "'0'OutM3L4,'0'QtySPBFlo_L4,'0'QtySPB_BBL4,'0'EfesiensiL4,'0'PPML4 from Line21 " +
        "union all " +
        "select Urut,Tanggal, " +
        "'0'OutM3L1,'0'QtySPBFlo_L1,'0'QtySPB_BBL1,'0'EfesiensiL1,'0'PPML1, " +
        "'0'OutM3L2,'0'QtySPBFlo_L2,'0'QtySPB_BBL2,'0'EfesiensiL2,'0'PPML2, " +
        "OutM3L3,QtySPBFlo_L3,QtySPB_BBL3,EfesiensiL3,PPML3, " +
        "'0'OutM3L4,'0'QtySPBFlo_L4,'0'QtySPB_BBL4,'0'EfesiensiL4,'0'PPML4 from Line31 " +
        "union all " +
        "select Urut,Tanggal, " +
        "'0'OutM3L1,'0'QtySPBFlo_L1,'0'QtySPB_BBL1,'0'EfesiensiL1,'0'PPML1, " +
        "'0'OutM3L2,'0'QtySPBFlo_L2,'0'QtySPB_BBL2,'0'EfesiensiL2,'0'PPML2, " +
        "'0'OutM3L3,'0'QtySPBFlo_L3,'0'QtySPB_BBL3,'0'EfesiensiL3,'0'PPML3, " +
        "OutM3L4,QtySPBFlo_L4,QtySPB_BBL4,EfesiensiL4,PPML4 from Line41 " +
        ") as x  group by Tanggal,Urut order by tanggal " +

        "select Tanggal, " +
        "OutM3L1,QtySPB_BBL1 QtySPB_BBL1,QtySPBFlo_L1 QtySPBL1,EfesiensiL1,PPML1, " +
        "OutM3L2,QtySPB_BBL2 QtySPB_BBL2,QtySPBFlo_L2 QtySPBL2,EfesiensiL2,PPML2, " +
        "OutM3L3,QtySPB_BBL3 QtySPB_BBL3,QtySPBFlo_L3 QtySPBL3,EfesiensiL3,PPML3, " +
        "OutM3L4,QtySPB_BBL4 QtySPB_BBL4,QtySPBFlo_L4 QtySPBL4,EfesiensiL4,PPML4 into TempJoinAtas from ( " +
        "select Urut,Tanggal, " +
        "OutM3L1,QtySPB_BBL1,QtySPBFlo_L1,EfesiensiL1,PPML1, " +
        "OutM3L2,QtySPB_BBL2,QtySPBFlo_L2,EfesiensiL2,PPML2, " +
        "OutM3L3,QtySPB_BBL3,QtySPBFlo_L3,EfesiensiL3,PPML3, " +
        "OutM3L4,QtySPB_BBL4,QtySPBFlo_L4,EfesiensiL4,PPML4 from Temp_Atas  " +
        "union all " +
        "select '1000'Urut,'Total'x, " +
        "sum(OutM3L1)OutM3L1,sum(QtySPB_BBL1)QtySPB_BBL1,sum(QtySPBFlo_L1)QtySPBFlo_L1,isnull(sum(QtySPBFlo_L1)/nullif(sum(OutM3L1),0),0) EfesiensiL1,isnull((sum(QtySPBFlo_L1)/nullif(sum(QtySPB_BBL1),0))*1000000 ,0) PPML1, " +
        "sum(OutM3L2)OutM3L2,sum(QtySPB_BBL2)QtySPB_BBL2,sum(QtySPBFlo_L2)QtySPBFlo_L2,isnull(sum(QtySPBFlo_L2)/nullif(sum(OutM3L2),0),0) EfesiensiL2,isnull((sum(QtySPBFlo_L2)/nullif(sum(QtySPB_BBL2),0))*1000000 ,0) PPML2, " +
        "sum(OutM3L3)OutM3L3,sum(QtySPB_BBL3)QtySPB_BBL3,sum(QtySPBFlo_L3)QtySPBFlo_L3,isnull(sum(QtySPBFlo_L3)/nullif(sum(OutM3L3),0),0) EfesiensiL3,isnull((sum(QtySPBFlo_L3)/nullif(sum(QtySPB_BBL3),0))*1000000 ,0) PPML3, " +
        "sum(OutM3L4)OutM3L4,sum(QtySPB_BBL4)QtySPB_BBL4,sum(QtySPBFlo_L4)QtySPBFlo_L4,isnull(sum(QtySPBFlo_L4)/nullif(sum(OutM3L4),0),0) EfesiensiL4,isnull((sum(QtySPBFlo_L4)/nullif(sum(QtySPB_BBL4),0))*1000000 ,0) PPML4 " +
        "from Temp_Atas " +
        ") as x order by tanggal,Urut " +


        "/** Kolom Bawah **/ " +
        "select Ket, " +
        "sum(TotOut_L1)TotOut_L1,sum(TotBB_L1)TotBB_L1,sum(TotFlo_L1)TotFlo_L1, " +
        "sum(TotOut_L2)TotOut_L2,sum(TotBB_L2)TotBB_L2,sum(TotFlo_L2)TotFlo_L2, " +
        "sum(TotOut_L3)TotOut_L3,sum(TotBB_L3)TotBB_L3,sum(TotFlo_L3)TotFlo_L3, " +
        "sum(TotOut_L4)TotOut_L4,sum(TotBB_L4)TotBB_L4,sum(TotFlo_L4)TotFlo_L4 into  Temp_TotOut from ( " +

        "/** OutPut L1 **/ " +
        "select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        "isnull(sum(M3),0)TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='<15 Press' and Line=1 " +
        "union all " +
        "select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        "isnull(sum(M3),0)TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='>=15 Press' and Line=1 " +
        "union all " +
        "select '3'Urut,'Sub Total Nonpress'Ket, " +
        "isnull(sum(M3),0)TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='Non Press' and Line=1 " +
        "union all " +
        "select '4'Urut,'Grand Total'Ket, " +
        "isnull(sum(M3),0)TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Line=1 " +

        "union all " +

        "/** OutPut L2 **/ " +
        "select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "isnull(sum(M3),0)TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='<15 Press' and Line=2 " +
        "union all " +
        "select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "isnull(sum(M3),0)TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='>=15 Press' and Line=2 " +
        "union all " +
        "select '3'Urut,'Sub Total Nonpress'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "isnull(sum(M3),0)TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='Non Press' and Line=2 " +
        "union all " +
        "select '4'Urut,'Grand Total'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "isnull(sum(M3),0)TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Line=2 " +

        "union all " +

        "/** OutPut L2 **/ " +
        "select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "isnull(sum(M3),0)TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='>=15 Press' and Line=3 " +
        "union all " +
        "select '3'Urut,'Sub Total Nonpress'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "isnull(sum(M3),0)TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='Non Press' and Line=3 " +
        "union all " +
        "select '4'Urut,'Grand Total'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "isnull(sum(M3),0)TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Line=3 " +

        "union all " +

        "/** OutPut L4 **/ " +
        "select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "isnull(sum(M3),0)TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='<15 Press' and Line=4 " +
        "union all " +
        "select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "isnull(sum(M3),0)TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='>=15 Press' and Line=4 " +
        "union all " +
        "select '3'Urut,'Sub Total Nonpress'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "isnull(sum(M3),0)TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Noted='Non Press' and Line=4 " +
        "union all " +
        "select '4'Urut,'Grand Total'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "isnull(sum(M3),0)TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataOutput where Line=4 " +

        "union all " +
        "/** Total SPB Flooculant **/ " +
        "/** Line 1 **/ " +
        "select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        "0 TotOut_L1,isnull(sum(Qty),0)TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='<15 Press' and Line=1 " +
        "union all " +
        "select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        "0 TotOut_L1,isnull(sum(Qty),0)TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='>=15 Press' and Line=1 " +
        "union all " +
        "select '3'Urut,'Sub Total Nonpress'Ket, " +
        "0 TotOut_L1,isnull(sum(Qty),0)TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='Non Press' and Line=1 " +
        "union all " +
        "select '4'Urut,'Grand Total'Ket, " +
        "0 TotOut_L1,isnull(sum(Qty),0)TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Line=1 " +

        "union all " +
        "/** Line 2 **/ " +
        "select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,isnull(sum(Qty),0)TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='<15 Press' and Line=2 " +
        "union all " +
        "select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,isnull(sum(Qty),0)TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='>=15 Press' and Line=2 " +
        "union all " +
        "select '3'Urut,'Sub Total Nonpress'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,isnull(sum(Qty),0)TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='Non Press' and Line=2 " +
        "union all " +
        "select '4'Urut,'Grand Total'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,isnull(sum(Qty),0)TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Line=2 " +

        "union all " +
        "/** Line 3 **/ " +
        "select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,isnull(sum(Qty),0)TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='<15 Press' and Line=3 " +
        "union all " +
        "select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,isnull(sum(Qty),0)TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='>=15 Press' and Line=3 " +
        "union all " +
        "select '3'Urut,'Sub Total Nonpress'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,isnull(sum(Qty),0)TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='Non Press' and Line=3 " +
        "union all " +
        "select '4'Urut,'Grand Total'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,isnull(sum(Qty),0)TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Line=3 " +

        "union all " +
        "/** Line 4 **/ " +
        "select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,isnull(sum(Qty),0)TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='<15 Press' and Line=4 " +
        "union all " +
        "select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,isnull(sum(Qty),0)TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='>=15 Press' and Line=4 " +
        "union all " +
        "select '3'Urut,'Sub Total Nonpress'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,isnull(sum(Qty),0)TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Noted='Non Press' and Line=4 " +
        "union all " +
        "select '4'Urut,'Grand Total'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,isnull(sum(Qty),0)TotFlo_L4,0 TotBB_L4 from Temp_DataFloo where Line=4 " +

        "union all " +
        "/** Total SPB BB All **/ " +
        "/** Line 1 **/ " +
        "select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,sum(TotBB_L1)TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        "select isnull(round(sum(PakaiBB),1),0)TotBB_L1,Tgl from TempAwal where Noted='<15 Press' and Line=1 group by Tgl ) as x " +
        "union all " +
        "select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,sum(TotBB_L1)TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        "select isnull(round(sum(PakaiBB),1),0)TotBB_L1,Tgl from TempAwal where Noted='>=15 Press' and Line=1 group by Tgl ) as x " +
        "union all " +
        "select '3'Urut,'Sub Total Nonpress'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,sum(TotBB_L1)TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        "select isnull(round(sum(PakaiBB),1),0)TotBB_L1,Tgl from TempAwal where Noted='Non Press' and Line=1 group by Tgl ) as x " +
        "union all " +
        "select '4'Urut,'Grand Total'Ket, " +
        "0 TotOut_L1,0 TotFlo_L1,sum(TotBB_L1)TotBB_L1, " +
        "0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        "0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        "0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        "select isnull(round(sum(PakaiBB),1),0)TotBB_L1,Tgl from TempAwal where Line=1 group by Tgl ) as x " +
        " union all " +
        " /** Line 2 **/ " +
        " select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,sum(TotBB_L2)TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L2,Tgl from TempAwal where Noted='<15 Press' and Line=2 group by Tgl ) as x " +
        " union all " +
        " select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,sum(TotBB_L2)TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L2,Tgl from TempAwal where Noted='>=15 Press' and Line=2 group by Tgl ) as x " +
        " union all " +
        " select '3'Urut,'Sub Total Nonpress'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,sum(TotBB_L2)TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L2,Tgl from TempAwal where Noted='Non Press' and Line=2 group by Tgl ) as x " +
        " union all " +
        " select '4'Urut,'Grand Total'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,sum(TotBB_L2)TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L2,Tgl from TempAwal where Line=2 group by Tgl ) as x " +
        " union all " +
        " /** Line 3 **/ " +
        " select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,sum(TotBB_L3)TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L3,Tgl from TempAwal where Noted='<15 Press' and Line=3 group by Tgl ) as x " +
        " union all " +
        " select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,sum(TotBB_L3)TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L3,Tgl from TempAwal where Noted='>=15 Press' and Line=3 group by Tgl ) as x " +
        " union all " +
        " select '3'Urut,'Sub Total Nonpress'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,sum(TotBB_L3)TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L3,Tgl from TempAwal where Noted='Non Press' and Line=3 group by Tgl ) as x " +
        " union all " +
        " select '4'Urut,'Grand Total'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,sum(TotBB_L3)TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L3,Tgl from TempAwal where Line=3 group by Tgl ) as x " +
        " union all " +
        " /** Line 4 **/ " +
        " select '1'Urut,'Sub Total < 15 mm PRESS'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,sum(TotBB_L4)TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L4,Tgl from TempAwal where Noted='<15 Press' and Line=4 group by Tgl ) as x " +
        " union all " +
        " select '2'Urut,'Sub Total >= 15 mm PRESS'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,sum(TotBB_L4)TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L4,Tgl from TempAwal where Noted='>=15 Press' and Line=4 group by Tgl ) as x " +
        " union all " +
        " select '3'Urut,'Sub Total Nonpress'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,sum(TotBB_L4)TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L4,Tgl from TempAwal where Noted='Non Press' and Line=4 group by Tgl ) as x " +
        " union all " +
        " select '4'Urut,'Grand Total'Ket, " +
        " 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
        " 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
        " 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
        " 0 TotOut_L4,0 TotFlo_L4,sum(TotBB_L4)TotBB_L4 from ( " +
        " select isnull(round(sum(PakaiBB),1),0)TotBB_L4,Tgl from TempAwal where Line=4 group by Tgl ) as x " +
        " ) as x group by Ket,Urut order by Urut " +

        " select  *, " +
        " isnull((TotFlo_L1/nullif(TotBB_L1,0))*1000000,0)PPM_L1, " +
        " isnull((TotFlo_L2/nullif(TotBB_L2,0))*1000000,0)PPM_L2, " +
        " isnull((TotFlo_L3/nullif(TotBB_L3,0))*1000000,0)PPM_L3, " +
        " isnull((TotFlo_L4/nullif(TotBB_L4,0))*1000000,0)PPM_L4, " +
        " case when ket='Sub Total < 15 mm PRESS' then isnull(TotFlo_L1/nullif(TotOut_L1,0) ,0) " +
        " when ket='Sub Total >= 15 mm PRESS' then isnull((TotFlo_L1/nullif(TotOut_L1,0))*(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25 ,0) " +
        " when ket='Sub Total Nonpress' then isnull((TotFlo_L1/nullif(TotOut_L1,0))*(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25 ,0) " +
        " else 0 end Tot_EfL1, " +

        " case when ket='Sub Total < 15 mm PRESS' then isnull(TotFlo_L2/nullif(TotOut_L2,0),0)  " +
        " when ket='Sub Total >= 15 mm PRESS' then isnull((TotFlo_L2/nullif(TotOut_L2,0))*(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25 ,0) " +
        " when ket='Sub Total Nonpress' then isnull((TotFlo_L2/nullif(TotOut_L2,0))*(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25 ,0) " +
        " else 0 end Tot_EfL2, " +

        " case when ket='Sub Total < 15 mm PRESS' then isnull(TotFlo_L3/nullif(TotOut_L3,0),0) " +
        " when ket='Sub Total >= 15 mm PRESS' then isnull((TotFlo_L3/nullif(TotOut_L3,0))*(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25 ,0) " +
        " when ket='Sub Total Nonpress' then isnull((TotFlo_L3/nullif(TotOut_L3,0))*(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25 ,0) " +
        " else 0 end Tot_EfL3, " +

        " case when ket='Sub Total < 15 mm PRESS' then isnull(TotFlo_L4/nullif(TotOut_L4,0),0) " +
        " when ket='Sub Total >= 15 mm PRESS' then isnull((TotFlo_L4/nullif(TotOut_L4,0))*(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25 ,0) " +
        " when ket='Sub Total Nonpress' then isnull((TotFlo_L4/nullif(TotOut_L4,0))*(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25 ,0) " +
        " else 0 end Tot_EfL4, " +
        
        " case " +
        " when ket='Sub Total < 15 mm PRESS' then TotOut_L1  " +
        " when ket='Sub Total >= 15 mm PRESS' then TotOut_L1*(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25  " +
        " when ket='Sub Total Nonpress' then TotOut_L1*(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25  " +
        "  end Param1, " +
        "  case " +
        " when ket='Sub Total < 15 mm PRESS' then TotOut_L2  " +
        " when ket='Sub Total >= 15 mm PRESS' then TotOut_L2*(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25  " +
        " when ket='Sub Total Nonpress' then TotOut_L2*(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25  " +
        "  end Param2, " +
        "   case " +
        " when ket='Sub Total < 15 mm PRESS' then TotOut_L3  " +
        " when ket='Sub Total >= 15 mm PRESS' then TotOut_L3*(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25  " +
        " when ket='Sub Total Nonpress' then TotOut_L3*(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25  " +
        "  end Param3, " +
        " case " +
        " when ket='Sub Total < 15 mm PRESS' then TotOut_L4  " +
        " when ket='Sub Total >= 15 mm PRESS' then TotOut_L4*(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25  " +
        " when ket='Sub Total Nonpress' then TotOut_L4*(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25  " +
        "  end Param4 into TempBawah1 " +
        " from Temp_TotOut  where Ket<>'Grand Total' " +


        " select Ket, " +
        " TotOut_L1,TotBB_L1,TotFlo_L1,Tot_EfL1,PPM_L1, " +
        " TotOut_L2,TotBB_L2,TotFlo_L2,Tot_EfL2,PPM_L2, " +
        " TotOut_L3,TotBB_L3,TotFlo_L3,Tot_EfL3,PPM_L3, " +
        " TotOut_L4,TotBB_L4,TotFlo_L4,Tot_EfL4,PPM_L4 into TempBawah2 from TempBawah1 " +
        " union all " +
        " select 'Grand Total'Ket, " +
        " sum(TotOut_L1)TotOut_M3,sum(TotBB_L1)TotBB,sum(TotFlo_L1)TotFlo,isnull(sum(TotOut_L1)/nullif(sum(Param1),0),0)Tot_EfL1,sum(PPM_L1)PPM_L1, " +
        " sum(TotOut_L2)TotOut_M3,sum(TotBB_L2)TotBB,sum(TotFlo_L2)TotFlo,isnull(sum(TotOut_L2)/nullif(sum(Param2),0),0)Tot_EfL2,sum(PPM_L2)PPM_L2, " +
        " sum(TotOut_L3)TotOut_M3,sum(TotBB_L3)TotBB,sum(TotFlo_L3)TotFlo,isnull(sum(TotOut_L3)/nullif(sum(Param3),0),0)Tot_EfL3,sum(PPM_L3)PPM_L3, " +
        " sum(TotOut_L4)TotOut_M3,sum(TotBB_L4)TotBB,sum(TotFlo_L4)TotFlo,isnull(sum(TotOut_L4)/nullif(sum(Param4),0),0)Tot_EfL4,sum(PPM_L4)PPM_L4 from TempBawah1 " +

        " select * from TempJoinAtas " +
        " union all " +
        " select * from TempBawah2 ";



        return result;
    }

    private string MatrixQuery00(string Periode, string Periode2)
    {
        string result =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempFlo]') AND type in (N'U')) DROP TABLE [dbo].[TempFlo] ; " +
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
        "		            where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and A3.PlantID=1 and A1.RowStatus>-1), " +

        " DestackingL2 AS ( select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        "		            INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        "	    	        INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID " +
        "   		        where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=2 and AL1.RowStatus>-1), " +

        " DestackingL3 AS ( select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        "		            INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        "	    	        INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID " +
        "   		        where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=3 and AL1.RowStatus>-1), " +

        " DestackingL4 AS ( select AL1.Qty,AL1.TglProduksi,((AL2.Tebal*AL2.Lebar*AL2.Panjang)/1000000000)Volume from BM_Destacking AL1 " +
        "		            INNER JOIN fc_items AL2 ON AL1.ItemID=AL2.ID " +
        "	    	        INNER JOIN BM_PlantGroup AL3 ON AL1.PlantGroupID=AL3.ID " +
        "   		        where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and AL3.PlantID=4 and AL1.RowStatus>-1), " +

        " PemakaianL1 AS  ( select SUM(B2.Quantity)Quantity,B1.PakaiDate from Pakai B1 " +
        "                   INNER JOIN PakaiDetail B2 ON B1.ID=B2.PakaiID " +
        "                   where LEFT(convert(char,B1.pakaidate,112),6)='" + Periode2 + "' and B2.ItemID " +
        "                   in (select ItemID from BM_PFloculantItem where RowStatus>-1 and Keterangan='FLOOCULANT') " +
        "                   and B1.Status>1 and B2.ProdLine=1 and B2.RowStatus>-1 group by B1.PakaiDate), " +

        " PemakaianL2 AS  ( select SUM(BL2.Quantity)Quantity,BL1.PakaiDate from Pakai BL1 " +
        "                   INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
        "                   where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID " +
        "                   in (select ItemID from BM_PFloculantItem where RowStatus>-1 and Keterangan='FLOOCULANT') " +
        "                   and BL1.Status>1 and BL2.ProdLine=2 and BL2.RowStatus>-1 group by BL1.PakaiDate), " +

        " PemakaianL3 AS  ( select SUM(BL2.Quantity)Quantity,BL1.PakaiDate from Pakai BL1 " +
        "                   INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
        "                   where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID " +
        "                   in (select ItemID from BM_PFloculantItem where RowStatus>-1 and Keterangan='FLOOCULANT') " +
        "                   and BL1.Status>1 and BL2.ProdLine=3 and BL2.RowStatus>-1 group by BL1.PakaiDate), " +

        " PemakaianL4 AS  ( select SUM(BL2.Quantity)Quantity,BL1.PakaiDate from Pakai BL1 " +
        "                   INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
        "                   where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.ItemID " +
        "                   in (select ItemID from BM_PFloculantItem where RowStatus>-1 and Keterangan='FLOOCULANT') " +
        "                   and BL1.Status>1 and BL2.ProdLine=4 and BL2.RowStatus>-1 group by BL1.PakaiDate), " +

        " PemakaianBBL1 AS  ( select SUM(BL2.Quantity)Quantity,BL1.PakaiDate from Pakai BL1 INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
        "                     where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.GroupID=1 and BL1.DeptID=2 and BL1.Status>1 and " +
        "                     BL2.ProdLine=1 and BL2.RowStatus>-1 group by BL1.PakaiDate), " +

        " PemakaianBBL2 AS  ( select SUM(BL2.Quantity)Quantity,BL1.PakaiDate from Pakai BL1 INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
        "                     where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.GroupID=1 and BL1.DeptID=2 and BL1.Status>1 and " +
        "                     BL2.ProdLine=2 and BL2.RowStatus>-1 group by BL1.PakaiDate), " +

        " PemakaianBBL3 AS  ( select SUM(BL2.Quantity)Quantity,BL1.PakaiDate from Pakai BL1 INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
        "                     where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.GroupID=1 and BL1.DeptID=2 and BL1.Status>1 and " +
        "                     BL2.ProdLine=3 and BL2.RowStatus>-1 group by BL1.PakaiDate), " +

        " PemakaianBBL4 AS  ( select SUM(BL2.Quantity)Quantity,BL1.PakaiDate from Pakai BL1 INNER JOIN PakaiDetail BL2 ON BL1.ID=BL2.PakaiID " +
        "                     where LEFT(convert(char,BL1.pakaidate,112),6)='" + Periode2 + "' and BL2.GroupID=1 and BL1.DeptID=2 and BL1.Status>1 and " +
        "                     BL2.ProdLine=4 and BL2.RowStatus>-1 group by BL1.PakaiDate) " +

        " select * into TempFlo from ( " +
        " select " +
        " Urutan,Tanggal, " +
        " OutM3L1,Sum(QtySPBL1)QtySPBL1,SUM(EfesiensiL1)EfesiensiL1,PPM1, " +
        " OutM3L2,SUM(QtySPBL2)QtySPBL2,SUM(EfesiensiL2)EfesiensiL2,PPM2, " +
        " OutM3L3,SUM(QtySPBL3)QtySPBL3,SUM(EfesiensiL3)EfesiensiL3,PPM3, " +
        " OutM3L4,SUM(QtySPBL4)QtySPBL4,SUM(EfesiensiL4)EfesiensiL4,PPM4, " +
        " Sum(QtySPB_BBL1)QtySPB_BBL1,Sum(QtySPB_BBL2)QtySPB_BBL2,Sum(QtySPB_BBL3)QtySPB_BBL3,Sum(QtySPB_BBL4)QtySPB_BBL4, " +
        " 0 OutM3L5,0 OutM3L6,0 QtySPBL5,0 QtySPBL6,0 EfesiensiL5,0 EfesiensiL6,0 PPM5,0 PPM6  " +

        " from   " +

        " (select " +
        " Urutan,Tanggal, " +
        " ROUND(OutM3L1,2)OutM3L1,isnull(P1.Quantity,0) QtySPBL1,ROUND(isnull(cast((P1.Quantity / NULLIF(OutM3L1,0)) as Decimal(10,2)),0),2)EfesiensiL1,0'PPM1', " +
        " ROUND(OutM3L2,2)OutM3L2,isnull(P2.Quantity,0) QtySPBL2,ROUND(isnull(cast((P2.Quantity / NULLIF(OutM3L2,0)) as Decimal(10,2)),0),2) EfesiensiL2,0'PPM2', " +
        " ROUND(OutM3L3,2)OutM3L3,isnull(P3.Quantity,0) QtySPBL3,ROUND(isnull(cast((P3.Quantity / NULLIF(OutM3L3,0)) as Decimal(10,2)),0),2) EfesiensiL3,0'PPM3', " +
        " ROUND(OutM3L4,2)OutM3L4,isnull(P4.Quantity,0) QtySPBL4,ROUND(isnull(cast((P4.Quantity / NULLIF(OutM3L4,0)) as Decimal(10,2)),0),2) EfesiensiL4,0'PPM4', " +
        " isnull(P5.Quantity,0) QtySPB_BBL1,isnull(P6.Quantity,0) QtySPB_BBL2,isnull(P7.Quantity,0) QtySPB_BBL3,isnull(P8.Quantity,0) QtySPB_BBL4 " +

        " from  " +

        " (select " +
        " Urutan,tanggal, " +
        " Date,ROUND(SUM(M3L1),2)OutM3L1,ROUND(SUM(M3L2),2)OutM3L2,ROUND(SUM(M3L3),2)OutM3L3,ROUND(SUM(M3L4),2)OutM3L4 from " +
        " (select '1'Urutan, LEFT(convert(char,A.date,106),11)Tanggal,A.Date ,ROUND(ISNULL(sum(B.Qty),0)* ISNULL(B.Volume,0),3) " +
        " 'M3L1',0'M3L2',0'M3L3',0'M3L4'  from DaysInMonth A   LEFT JOIN DestackingL1 B ON A.[Date]=B.TglProduksi " +
        " where month(date) = month(@Date)  group by A.Date,B.Volume ,A.Date  " +

        " UNION ALL " +

        " select '1'Urutan, LEFT(convert(char,A2.date,106),11)Tanggal,A2.Date  ,0'M3L1',ROUND(ISNULL(sum(B2.Qty),0)* ISNULL(B2.Volume,0),3) 'M3L2',0'M3L3',0'M3L4' " +
        " from DaysInMonth A2  LEFT JOIN DestackingL2 B2 ON A2.[Date]=B2.TglProduksi  where month(date) = month(@Date)  " +
        " group by A2.Date,B2.Volume ,A2.Date " +

        " UNION ALL  " +

        " select '1'Urutan, LEFT(convert(char,A3.date,106),11)Tanggal,A3.Date  ,0'M3L1',0'M3L2',ROUND(ISNULL(sum(B3.Qty),0)* ISNULL(B3.Volume,0),3) 'M3L3',0'M3L4' " +
        " from DaysInMonth A3  LEFT JOIN DestackingL3 B3 ON A3.[Date]=B3.TglProduksi  where month(date) = month(@Date) " +
        " group by A3.Date,B3.Volume ,A3.Date " +

        " UNION ALL  " +

        " select '1'Urutan, LEFT(convert(char,A4.date,106),11)Tanggal,A4.Date ,0'M3L1',0'M3L2',0'M3L3',ROUND(ISNULL(sum(B4.Qty),0)* ISNULL(B4.Volume,0),3)'M3L4' " +
        " from DaysInMonth A4  LEFT JOIN DestackingL4 B4 ON A4.[Date]=B4.TglProduksi  where month(date) = month(@Date) " +
        " group by A4.Date,B4.Volume,A4.Date ) as xx group by Tanggal ,Date,Urutan " +

        " UNION ALL  " +

        " select '2'Urutan,''Tanggal,''Date,0'M3L1',0'M3L2',0'M3L3',0'M3L4' " +

        " ) as xx1 " +
        " LEFT JOIN PemakaianL1 as P1 ON P1.PakaiDate=xx1.Date " +
        " LEFT JOIN PemakaianL2 as P2 ON P2.PakaiDate=xx1.Date " +
        " LEFT JOIN PemakaianL3 as P3 ON P3.PakaiDate=xx1.Date " +
        " LEFT JOIN PemakaianL4 as P4 ON P4.PakaiDate=xx1.Date " +
        " LEFT JOIN PemakaianBBL1 as P5 ON P5.PakaiDate=xx1.Date " +
        " LEFT JOIN PemakaianBBL2 as P6 ON P6.PakaiDate=xx1.Date " +
        " LEFT JOIN PemakaianBBL3 as P7 ON P7.PakaiDate=xx1.Date " +
        " LEFT JOIN PemakaianBBL4 as P8 ON P8.PakaiDate=xx1.Date " +
        " ) as xx2 group by Tanggal,Urutan,OutM3L1,PPM1,OutM3L2,PPM2,OutM3L3,PPM3,OutM3L4,PPM4 " +
        //" group by Tanggal,OutM3L1,PPM1,OutM3L2,PPM2,OutM3L3,PPM3,OutM3L4,PPM4,Urutan order by Urutan,Tanggal ";
        " ) as xx3 " +

        " select * From TempFlo order by Urutan,Tanggal ";

        return result;

    }

    private string MatrixQueryKarawang(string Periode, string Periode2)
    {
        string result =
        #region query
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_Atas]') AND type in (N'U')) DROP TABLE [dbo].[Temp_Atas]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataOutput]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataOutput]    " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataSPB]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataSPB]   " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataFloo]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataFloo] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_TotOut]') AND type in (N'U')) DROP TABLE [dbo].[Temp_TotOut]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempAwal]') AND type in (N'U')) DROP TABLE [dbo].[TempAwal]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBawah1]') AND type in (N'U')) DROP TABLE [dbo].[TempBawah1] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBawah2]') AND type in (N'U')) DROP TABLE [dbo].[TempBawah2] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempJoinAtas]') AND type in (N'U')) DROP TABLE [dbo].[TempJoinAtas] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempJoinBawah]') AND type in (N'U')) DROP TABLE [dbo].[TempJoinBawah]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempJoinAll]') AND type in (N'U')) DROP TABLE [dbo].[TempJoinAll]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksi]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksi  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblProposionate]') AND type in (N'U')) DROP TABLE [dbo].[tblProposionate]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPakaiNonPerShift]') AND type in (N'U')) DROP TABLE [dbo].[tblPakaiNonPerShift]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPakaiPerShift]') AND type in (N'U')) DROP TABLE [dbo].[tblPakaiPerShift] " +

        " declare @thnbln varchar(6)  " +
        " declare @line varchar(2)  " +
        " declare @date datetime  " +
        " set @thnbln='" + Periode2 + "'  " +
        " set @date ='" + Periode + "'  " +

        " /** Temp Table tblProposionate **/ " +
        " select Tanggal,prodline,case when Total>0 then (Shf1/total*100) else 100  end Pros1, case when Total>0 then (Shf2/total*100) else 0  end Pros2, " +
        " case when Total>0 then (Shf3/total*100) else 0  end Pros3 into tblProposionate " +
        " from ( select Tanggal,prodline,sum(Shf1)Shf1,sum(Shf2)Shf2,sum(Shf3)Shf3 ,sum(Shf1+Shf2+Shf3) Total " +
        " from ( select A.pakaidate Tanggal,B.prodline,sum(B.Quantity) Shf1,0 Shf2,0 Shf3 from PakaiDetail B inner join pakai A on A.id=B.PakaiID " +
        " inner join inventory I on B.ItemID = I.ID  where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  " +
        " and I.GroupID=1 and I.ItemName like 'semen%' and B.Keterangan like '%shift 1%' group by A.pakaidate,prodline  " +
        " union all " +
        " select A.pakaidate Tanggal,B.prodline,0 Shf1,sum(B.Quantity) Shf2,0 Shf3 from PakaiDetail B inner join pakai A on A.id=B.PakaiID  " +
        " inner join inventory I on B.ItemID = I.ID  where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 " +
        " and I.ItemName like 'semen%' and B.Keterangan like '%shift 2%' group by A.pakaidate,prodline union all  " +
        " select A.pakaidate Tanggal,B.prodline,0 Shf1,0 Shf2,sum(B.Quantity) Shf3 from PakaiDetail B inner join pakai A on A.id=B.PakaiID " +
        " inner join inventory I on B.ItemID = I.ID  where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 " +
        " and I.ItemName like 'semen%' and B.Keterangan like '%shift 3%' group by A.pakaidate,prodline union all " +
        " select distinct A.pakaidate Tanggal,B.prodline,0 Shf1,0 Shf2,0 Shf3 from PakaiDetail B inner join pakai A on A.id=B.PakaiID  " +
        " inner join inventory I on B.ItemID = I.ID  where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 )A " +
        " group by Tanggal,prodline)B   " +

" /** Temp Table tblPakaiNonPerShift **/ " +
" select  Tanggal,prodline,Qty into tblPakaiNonPerShift from ( select Tanggal,prodline,sum(Qty)Qty from ( select B.prodline,convert(char,A.pakaidate,112) Tanggal, sum(B.Quantity) Qty from PakaiDetail B  " +
" inner join pakai A on A.id=B.PakaiID   " +
" inner join inventory I on B.ItemID = I.ID   " +
" where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1  and B.Keterangan Not like '%shift%' group by prodline,A.pakaidate )A group by prodline,Tanggal )B   " +

" /** Temp table tblPakaiPershift **/ " +
" select  Tanggal,prodline,shif, case shif when 1 then Qty+(isnull((select qty from tblPakaiNonPerShift where tanggal=B.tanggal and prodline=B.prodline),0)* isnull((select Pros1 from tblProposionate where prodline =B.prodline and Tanggal=B.Tanggal  ),0))/100  when 2 then Qty+(isnull((select qty from tblPakaiNonPerShift where tanggal=B.tanggal and prodline=B.prodline),0)* isnull((select Pros2 from tblProposionate where prodline =B.prodline and Tanggal=B.Tanggal  ),0))/100  when 3 then Qty+(isnull((select qty from tblPakaiNonPerShift where tanggal=B.tanggal and prodline=B.prodline),0)* isnull((select Pros3 from tblProposionate where prodline =B.prodline and Tanggal=B.Tanggal  ),0))/100  end Qty into tblPakaiPerShift from ( select Tanggal,prodline,shif,sum(Qty)Qty from ( select B.prodline,convert(char,A.pakaidate,112) Tanggal,1 shif, sum(B.Quantity) Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1  and B.Keterangan like '%shift 1%' group by prodline,A.pakaidate " +
" union all " +
" select B.prodline,convert(char,A.pakaidate,112) Tanggal,2 shif, sum(B.Quantity) Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 and B.Keterangan like '%shift 2%' group by prodline,A.pakaidate " +
" union all  " +
" select B.prodline,convert(char,A.pakaidate,112) Tanggal,3 shif, sum(B.Quantity) Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 and B.Keterangan like '%shift 3%' group by prodline,A.pakaidate " +
" union all " +
" select distinct B.prodline,convert(char,A.pakaidate,112) Tanggal,1 shif, 0 Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1  " +
" union all " +
" select distinct B.prodline,convert(char,A.pakaidate,112) Tanggal,2 shif, 0 Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1  " +
" union all  " +
" select distinct B.prodline,convert(char,A.pakaidate,112) Tanggal,3 shif, 0 Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1  )A group by prodline,Tanggal,shif )B   " +

" /** Temp Table OuputProduksi **/ " +
" select *,case when P='NP' then 'Non Press' when tebal<15 then '<15 Press' when tebal>=15 then '>=15 Press' end Noted,(((Tebal*lebar*panjang)/1000000000)*Qty)M3 into OuputProduksi from ( select convert(char,A.tglproduksi,112)tanggal,right(rtrim(P.PlantName),1) Line,A.[shift] shif,G.[Group],sum(A.Qty) Qty,case when isnull(I.Pressing,'Yes')='Yes' then 'P' else 'NP' end  P, rtrim(cast(cast(cast(substring(I.Partno,7,3) as int)/10 as decimal(8,1)) as char)) +'X'+ substring(I.Partno,10,4)+'X'+substring(I.Partno,14,4)+ +' '  +case when isnull(I.Pressing,'Yes')='Yes' then 'P' else 'NP' end  partno,I.tebal,I.Lebar,I.Panjang  from BM_Destacking A inner join BM_Plant P on A.PlantID=P.ID inner join BM_PlantGroup G on A.PlantGroupID =G.ID  inner join fc_items I on A.ItemID=I.ID  where A.Qty>0 and A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln group by P.PlantName, A.TglProduksi,A.[shift],G.[Group],I.Partno,I.tebal, I.Lebar,I.Panjang,I.Pressing    union all select tanggal, prodline Line,shif,'-' [Group],0 Qty,'P' P,'-' Partno,1 Tebal,1 Lebar,1 Panjang  from tblPakaiPerShift where rtrim(cast(tanggal as char))+cast(shif as char) not in  (select distinct rtrim(cast(convert(char,tglproduksi,112) as char))+cast(shift as char) from bm_destacking  where convert(char,TglProduksi,112)=tblPakaiPerShift.tanggal and plantid=@line) )Z where Qty>0 order by tanggal, shif  " +

" /** temp data Temp_DataOutput [ OutPut Produksi ] **/ " +
" select Volume*Qty M3,tgl,Line,shift,Noted into Temp_DataOutput from ( select sum(Qty)Qty,((tebal*lebar*Panjang)/1000000000)Volume,shift,tgl,Line,Noted from ( select x.Qty,A.Tebal,A.Lebar,A.Panjang,shift,Tgl,Line,case when Pressing='NO' then 'Non Press' when Pressing='YES' and Tebal>=15 then '>=15 Press' else '<15 Press' end Noted from ( select sum(Qty)Qty,shift,ItemID,left(convert(char,TglProduksi,112),8)Tgl,PlantID Line from BM_Destacking where Qty>0 and left(convert(char,tglproduksi,112),6)=@thnbln and RowStatus>-1 and LokasiID not in (select ID from fc_lokasi where lokasi like'%adj%' and rowstatus>-1) group by ItemID,shift,TglProduksi,PlantID ) as x inner join FC_Items A ON A.ID=x.ItemID) as xx group by tebal,lebar,Panjang,shift,tgl,Line,Noted ) as xxx  " +

" /** temp data Temp_DataFloo [ SPB Bahan Baku Flooculant Dept. BM ] **/  " +
" select A.Quantity Qty,isnull(A.GroupFloo,'')Noted,A.ProdLine Line,B.PakaiDate Tgl into Temp_DataFloo from PakaiDetail A  inner join Pakai B ON A.PakaiID=B.ID where A.RowStatus>-1 and B.Status>2 and A.GroupID=2 and left(convert(char,B.PakaiDate,112),6)=@thnbln and B.DeptID=2 and A.ItemID in (select ItemID from BM_PFloculantItem where RowStatus>-1 and Keterangan='FLOOCULANT') " +

" ;with  " +
" dataProduksi as (select * from OuputProduksi where left(convert(char,tanggal,112),6)=@thnbln) , " +
" dataPakaiBB as (select Tanggal,ProdLine,shif,cast(Qty as decimal(18,1))qty from tblPakaiPershift where left(convert(char,tanggal,112),6)=@thnbln and qty>0 ), " +
" dataJoin as (select A.tanggal,A.shif,A.M3,isnull(B.Qty,0)Qty,A.tebal,A.P,A.Line from dataProduksi A left join dataPakaiBB B ON A.tanggal=B.Tanggal and A.shif=B.shif and B.ProdLine=A.Line), " +
" dataJoin1 as (select A.*,case when M3>0 then cast((Qty * (M3/isnull((select sum(M3) from dataJoin where tanggal=A.Tanggal and shif=A.shif and Line=A.Line),0))) as decimal(18,1))  else Qty end PakaiBB from dataJoin A), " +
" dataJoin2 as (select *,case when tebal<15 and P='P' then '<15 Press' when tebal>=15 and P='P' then '>=15 Press' when P='NP' then 'Non Press'  end Keterangan from dataJoin1) " +
" select tanggal Tgl,sum(PakaiBB)PakaiBB,sum(M3)M3,Keterangan Noted,Line into TempAwal from dataJoin2 group by Keterangan,tanggal,Line order by tanggal " +

" ;with   " +
" DaysInMonth as (select @date as Date union all select dateadd(dd,1,Date) from DaysInMonth where month(date)= month(@date)),       " +
" temp1 as (select Tgl,round(sum(PakaiBB),1)QtySPB_All,sum(M3)OutPutM3,'0'QtyFloo,Noted,Line from TempAwal  group by Noted ,tgl,Line),  " +
" temp2 as (select Tgl Tanggal,sum(QtySPB_All)QtySPB_BB,sum(OutPutM3)OutM3,sum(QtyFloo)QtySPB_Flo,Noted,Line from (  " +
" select Tgl,QtySPB_All,OutPutM3,QtyFloo,Noted,Line from temp1 " +
" union all   " +
" select Tgl,'0'QtySPB_All,'0'OutPutM3,Qty,Noted,Line from Temp_DataFloo   ) as x group by tgl,Noted,Line ),  " +
" temp3 as (select Tanggal,OutM3,QtySPB_BB,QtySPB_Flo,Noted,b.Konversi Konversi,Line from temp2 a left join BM_PFloculantKonversi b ON a.Noted=b.Keterangan where b.Rowstatus>-1 ),  " +

" Line1 as (select left(convert(char,Tanggal,106),11)Tanggal,OutM3 OutM3L1,QtySPB_Flo QtySPBFlo_L1,QtySPB_BB QtySPB_BBL1, " +
" case when Noted in ('Non Press','>=15 Press') then isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2))*cast(0.25 as decimal(18,2)) as decimal(18,2)),0) else  isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2)) as decimal(18,2)),0) end EfesiensiL1,  " +
" isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(QtySPB_BB as decimal(18,5)),0)*1000000 as decimal(18,0)),0)PPML1 from temp3  where Line=1 ),  " +
" Line11 as (select ROW_NUMBER() over (PARTITION by Tanggal order by Tanggal asc) as Urut,* from Line1 ), " +

" Line2 as (select left(convert(char,Tanggal,106),11)Tanggal,OutM3 OutM3L2,QtySPB_Flo QtySPBFlo_L2,QtySPB_BB QtySPB_BBL2, case when Noted in ('Non Press','>=15 Press') then isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2))*cast(0.25 as decimal(18,2)) as decimal(18,2)),0) else  isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2)) as decimal(18,2)),0) end EfesiensiL2, isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(QtySPB_BB as decimal(18,5)),0)*1000000 as decimal(18,0)),0)PPML2 from temp3  where Line=2 ), " +
" Line21 as (select ROW_NUMBER() over (PARTITION by Tanggal order by Tanggal asc) as Urut,* from Line2 ),  " +

" Line3 as (select left(convert(char,Tanggal,106),11)Tanggal,OutM3 OutM3L3,QtySPB_Flo QtySPBFlo_L3,QtySPB_BB QtySPB_BBL3, case when Noted in ('Non Press','>=15 Press') then isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2))*cast(0.25 as decimal(18,2)) as decimal(18,2)),0) else  isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2)) as decimal(18,2)),0) end EfesiensiL3, isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(QtySPB_BB as decimal(18,5)),0)*1000000 as decimal(18,4)),0)PPML3 from temp3  where Line=3 ), " +
" Line31 as (select ROW_NUMBER() over (PARTITION by Tanggal order by Tanggal asc) as Urut,* from Line3 ),  " +

" Line4 as (select left(convert(char,Tanggal,106),11)Tanggal,OutM3 OutM3L4,QtySPB_Flo QtySPBFlo_L4,QtySPB_BB QtySPB_BBL4, case when Noted in ('Non Press','>=15 Press') then isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2))*cast(0.25 as decimal(18,2)) as decimal(18,2)),0) else  isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2)) as decimal(18,2)),0) end EfesiensiL4, isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(QtySPB_BB as decimal(18,5)),0)*1000000 as decimal(18,0)),0)PPML4 from temp3  where Line=4 ), " +
" Line41 as (select ROW_NUMBER() over (PARTITION by Tanggal order by Tanggal asc) as Urut,* from Line4 ), " +

" Line5 as (select left(convert(char,Tanggal,106),11)Tanggal,OutM3 OutM3L5,QtySPB_Flo QtySPBFlo_L5,QtySPB_BB QtySPB_BBL5, case when Noted in ('Non Press','>=15 Press') then isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2))*cast(0.25 as decimal(18,2)) as decimal(18,2)),0) else  isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2)) as decimal(18,2)),0) end EfesiensiL5, isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(QtySPB_BB as decimal(18,5)),0)*1000000 as decimal(18,0)),0)PPML5 from temp3  where Line=5 ), " +
" Line51 as (select ROW_NUMBER() over (PARTITION by Tanggal order by Tanggal asc) as Urut,* from Line5 ), " +

" Line6 as (select left(convert(char,Tanggal,106),11)Tanggal,OutM3 OutM3L6,QtySPB_Flo QtySPBFlo_L6,QtySPB_BB QtySPB_BBL6, case when Noted in ('Non Press','>=15 Press') then isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2))*cast(0.25 as decimal(18,2)) as decimal(18,2)),0) else  isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(OutM3 as decimal(18,5)),0)/cast(Konversi as decimal(18,2)) as decimal(18,2)),0) end EfesiensiL6, isnull(cast(cast(QtySPB_Flo as decimal(18,5))/nullif(cast(QtySPB_BB as decimal(18,5)),0)*1000000 as decimal(18,0)),0)PPML6 from temp3  where Line=6 ), " +
" Line61 as (select ROW_NUMBER() over (PARTITION by Tanggal order by Tanggal asc) as Urut,* from Line6 ) " +

" select Urut,left(convert(char,Tanggal,106),11)Tanggal,  " +
" sum(OutM3L1)OutM3L1,sum(QtySPB_BBL1)QtySPB_BBL1,sum(QtySPBFlo_L1)QtySPBFlo_L1,sum(EfesiensiL1)EfesiensiL1,sum(PPML1)PPML1,  " +
" sum(OutM3L2)OutM3L2,sum(QtySPB_BBL2)QtySPB_BBL2,sum(QtySPBFlo_L2)QtySPBFlo_L2,sum(EfesiensiL2)EfesiensiL2,sum(PPML2)PPML2, " +
" sum(OutM3L3)OutM3L3,sum(QtySPB_BBL3)QtySPB_BBL3,sum(QtySPBFlo_L3)QtySPBFlo_L3,sum(EfesiensiL3)EfesiensiL3,sum(PPML3)PPML3,  " +
" sum(OutM3L4)OutM3L4,sum(QtySPB_BBL4)QtySPB_BBL4,sum(QtySPBFlo_L4)QtySPBFlo_L4,sum(EfesiensiL4)EfesiensiL4,sum(PPML4)PPML4, " +
" sum(OutM3L5)OutM3L5,sum(QtySPB_BBL5)QtySPB_BBL5,sum(QtySPBFlo_L5)QtySPBFlo_L5,sum(EfesiensiL5)EfesiensiL5,sum(PPML5)PPML5, " +
" sum(OutM3L6)OutM3L6,sum(QtySPB_BBL6)QtySPB_BBL6,sum(QtySPBFlo_L6)QtySPBFlo_L6,sum(EfesiensiL6)EfesiensiL6,sum(PPML6)PPML6  " +
" into Temp_Atas  from ( select '1'Urut,[Date]Tanggal, " +
" '0'OutM3L1,'0'QtySPBFlo_L1,'0'QtySPB_BBL1,'0'EfesiensiL1,'0'PPML1,  " +
" '0'OutM3L2,'0'QtySPBFlo_L2,'0'QtySPB_BBL2,'0'EfesiensiL2,'0'PPML2, " +
" '0'OutM3L3,'0'QtySPBFlo_L3,'0'QtySPB_BBL3,'0'EfesiensiL3,'0'PPML3,  " +
" '0'OutM3L4,'0'QtySPBFlo_L4,'0'QtySPB_BBL4,'0'EfesiensiL4,'0'PPML4,  " +
" '0'OutM3L5,'0'QtySPBFlo_L5,'0'QtySPB_BBL5,'0'EfesiensiL5,'0'PPML5, " +
" '0'OutM3L6,'0'QtySPBFlo_L6,'0'QtySPB_BBL6,'0'EfesiensiL6,'0'PPML6 " +
" from DaysInMonth where month(date) = month(@Date) " +
" union all  " +
" select *,  " +
" '0'OutM3L2,'0'QtySPBFlo_L2,'0'QtySPB_BBL2,'0'EfesiensiL2,'0'PPML2,  " +
" '0'OutM3L3,'0'QtySPBFlo_L3,'0'QtySPB_BBL3,'0'EfesiensiL3,'0'PPML3,  " +
" '0'OutM3L4,'0'QtySPBFlo_L4,'0'QtySPB_BBL4,'0'EfesiensiL4,'0'PPML4, " +
" '0'OutM3L5,'0'QtySPBFlo_L5,'0'QtySPB_BBL5,'0'EfesiensiL5,'0'PPML5, " +
" '0'OutM3L6,'0'QtySPBFlo_L6,'0'QtySPB_BBL6,'0'EfesiensiL6,'0'PPML6 from Line11   " +
" union all " +
" select Urut,Tanggal, " +
" '0'OutM3L1,'0'QtySPBFlo_L1,'0'QtySPB_BBL1,'0'EfesiensiL1,'0'PPML1, " +
" OutM3L2,QtySPBFlo_L2,QtySPB_BBL2,EfesiensiL2,PPML2, " +
" '0'OutM3L3,'0'QtySPBFlo_L3,'0'QtySPB_BBL3,'0'EfesiensiL3,'0'PPML3, " +
" '0'OutM3L4,'0'QtySPBFlo_L4,'0'QtySPB_BBL4,'0'EfesiensiL4,'0'PPML4, " +
" '0'OutM3L5,'0'QtySPBFlo_L5,'0'QtySPB_BBL5,'0'EfesiensiL5,'0'PPML5, " +
" '0'OutM3L6,'0'QtySPBFlo_L6,'0'QtySPB_BBL6,'0'EfesiensiL6,'0'PPML6 from Line21 " +
" union all  " +
" select Urut,Tanggal,  " +
" '0'OutM3L1,'0'QtySPBFlo_L1,'0'QtySPB_BBL1,'0'EfesiensiL1,'0'PPML1,  " +
" '0'OutM3L2,'0'QtySPBFlo_L2,'0'QtySPB_BBL2,'0'EfesiensiL2,'0'PPML2, " +
" OutM3L3,QtySPBFlo_L3,QtySPB_BBL3,EfesiensiL3,PPML3, " +
" '0'OutM3L4,'0'QtySPBFlo_L4,'0'QtySPB_BBL4,'0'EfesiensiL4,'0'PPML4,  " +
" '0'OutM3L5,'0'QtySPBFlo_L5,'0'QtySPB_BBL5,'0'EfesiensiL5,'0'PPML5, " +
" '0'OutM3L6,'0'QtySPBFlo_L6,'0'QtySPB_BBL6,'0'EfesiensiL6,'0'PPML6 from Line31  " +
" union all  " +
" select Urut,Tanggal,  " +
" '0'OutM3L1,'0'QtySPBFlo_L1,'0'QtySPB_BBL1,'0'EfesiensiL1,'0'PPML1,  " +
" '0'OutM3L2,'0'QtySPBFlo_L2,'0'QtySPB_BBL2,'0'EfesiensiL2,'0'PPML2,  " +
" '0'OutM3L3,'0'QtySPBFlo_L3,'0'QtySPB_BBL3,'0'EfesiensiL3,'0'PPML3, " +
" OutM3L4,QtySPBFlo_L4,QtySPB_BBL4,EfesiensiL4,PPML4, " +
" '0'OutM3L5,'0'QtySPBFlo_L5,'0'QtySPB_BBL5,'0'EfesiensiL5,'0'PPML5, " +
" '0'OutM3L6,'0'QtySPBFlo_L6,'0'QtySPB_BBL6,'0'EfesiensiL6,'0'PPML6 from Line41  " +
" union all  " +
" select Urut,Tanggal,  " +
" '0'OutM3L1,'0'QtySPBFlo_L1,'0'QtySPB_BBL1,'0'EfesiensiL1,'0'PPML1,  " +
" '0'OutM3L2,'0'QtySPBFlo_L2,'0'QtySPB_BBL2,'0'EfesiensiL2,'0'PPML2,  " +
" '0'OutM3L3,'0'QtySPBFlo_L3,'0'QtySPB_BBL3,'0'EfesiensiL3,'0'PPML3, " +
" '0'OutM3L4,'0'QtySPBFlo_L4,'0'QtySPB_BBL4,'0'EfesiensiL4,'0'PPML4, " +
" OutM3L5,QtySPBFlo_L5,QtySPB_BBL5,EfesiensiL5,PPML5, " +
" '0'OutM3L6,'0'QtySPBFlo_L6,'0'QtySPB_BBL6,'0'EfesiensiL6,'0'PPML6 from Line51  " +
" union all  " +
" select Urut,Tanggal,  " +
" '0'OutM3L1,'0'QtySPBFlo_L1,'0'QtySPB_BBL1,'0'EfesiensiL1,'0'PPML1,  " +
" '0'OutM3L2,'0'QtySPBFlo_L2,'0'QtySPB_BBL2,'0'EfesiensiL2,'0'PPML2, " +
" '0'OutM3L3,'0'QtySPBFlo_L3,'0'QtySPB_BBL3,'0'EfesiensiL3,'0'PPML3, " +
" '0'OutM3L4,'0'QtySPBFlo_L4,'0'QtySPB_BBL4,'0'EfesiensiL4,'0'PPML4, " +
" '0'OutM3L5,'0'QtySPBFlo_L5,'0'QtySPB_BBL5,'0'EfesiensiL5,'0'PPML5, " +
" OutM3L6,QtySPBFlo_L6,QtySPB_BBL6,EfesiensiL6,PPML6 from Line61  " +
" ) as x  group by Tanggal,Urut order by tanggal  " +

" select Urut,Tanggal, " +
" OutM3L1,QtySPB_BBL1 QtySPB_BBL1,QtySPBFlo_L1 QtySPBL1,EfesiensiL1,PPML1,  " +
" OutM3L2,QtySPB_BBL2 QtySPB_BBL2,QtySPBFlo_L2 QtySPBL2,EfesiensiL2,PPML2,  " +
" OutM3L3,QtySPB_BBL3 QtySPB_BBL3,QtySPBFlo_L3 QtySPBL3,EfesiensiL3,PPML3,  " +
" OutM3L4,QtySPB_BBL4 QtySPB_BBL4,QtySPBFlo_L4 QtySPBL4,EfesiensiL4,PPML4,  " +
" OutM3L5,QtySPB_BBL5 QtySPB_BBL5,QtySPBFlo_L5 QtySPBL5,EfesiensiL5,PPML5, " +
" OutM3L6,QtySPB_BBL6 QtySPB_BBL6,QtySPBFlo_L6 QtySPBL6,EfesiensiL6,PPML6 into TempJoinAtas from (select Urut,Tanggal, OutM3L1,QtySPB_BBL1,QtySPBFlo_L1,EfesiensiL1,PPML1, " +
" OutM3L2,QtySPB_BBL2,QtySPBFlo_L2,EfesiensiL2,PPML2, " +
" OutM3L3,QtySPB_BBL3,QtySPBFlo_L3,EfesiensiL3,PPML3, " +
" OutM3L4,QtySPB_BBL4,QtySPBFlo_L4,EfesiensiL4,PPML4, " +
" OutM3L5,QtySPB_BBL5,QtySPBFlo_L5,EfesiensiL5,PPML5,  " +
" OutM3L6,QtySPB_BBL6,QtySPBFlo_L6,EfesiensiL6,PPML6 from Temp_Atas   " +
" union all " +
" select '10'Urut,'Total'x,  " +
" sum(OutM3L1)OutM3L1,sum(QtySPB_BBL1)QtySPB_BBL1,sum(QtySPBFlo_L1)QtySPBFlo_L1,isnull(sum(QtySPBFlo_L1)/nullif(sum(OutM3L1),0),0) EfesiensiL1,isnull((sum(QtySPBFlo_L1)/nullif(sum(QtySPB_BBL1),0))*1000000 ,0) PPML1,  " +
" sum(OutM3L2)OutM3L2,sum(QtySPB_BBL2)QtySPB_BBL2,sum(QtySPBFlo_L2)QtySPBFlo_L2,isnull(sum(QtySPBFlo_L2)/nullif(sum(OutM3L2),0),0) EfesiensiL2,isnull((sum(QtySPBFlo_L2)/nullif(sum(QtySPB_BBL2),0))*1000000 ,0) PPML2,  " +
" sum(OutM3L3)OutM3L3,sum(QtySPB_BBL3)QtySPB_BBL3,sum(QtySPBFlo_L3)QtySPBFlo_L3,isnull(sum(QtySPBFlo_L3)/nullif(sum(OutM3L3),0),0) EfesiensiL3,isnull((sum(QtySPBFlo_L3)/nullif(sum(QtySPB_BBL3),0))*1000000 ,0) PPML3,  " +
" sum(OutM3L4)OutM3L4,sum(QtySPB_BBL4)QtySPB_BBL4,sum(QtySPBFlo_L4)QtySPBFlo_L4,isnull(sum(QtySPBFlo_L4)/nullif(sum(OutM3L4),0),0) EfesiensiL4,isnull((sum(QtySPBFlo_L4)/nullif(sum(QtySPB_BBL4),0))*1000000 ,0) PPML4,  " +
" sum(OutM3L5)OutM3L5,sum(QtySPB_BBL5)QtySPB_BBL5,sum(QtySPBFlo_L5)QtySPBFlo_L5,isnull(sum(QtySPBFlo_L5)/nullif(sum(OutM3L5),0),0) EfesiensiL5,isnull((sum(QtySPBFlo_L5)/nullif(sum(QtySPB_BBL5),0))*1000000 ,0) PPML5, " +
" sum(OutM3L6)OutM3L6,sum(QtySPB_BBL6)QtySPB_BBL6,sum(QtySPBFlo_L6)QtySPBFlo_L6,isnull(sum(QtySPBFlo_L6)/nullif(sum(OutM3L6),0),0) EfesiensiL6,isnull((sum(QtySPBFlo_L6)/nullif(sum(QtySPB_BBL6),0))*1000000 ,0) PPML6 " +
" from Temp_Atas ) as x order by tanggal,Urut " +

" /** Kolom Bawah **/  " +
" select Urut,Ket,  " +
" sum(TotOut_L1)TotOut_L1,sum(TotBB_L1)TotBB_L1,sum(TotFlo_L1)TotFlo_L1,  " +
" sum(TotOut_L2)TotOut_L2,sum(TotBB_L2)TotBB_L2,sum(TotFlo_L2)TotFlo_L2,  " +
" sum(TotOut_L3)TotOut_L3,sum(TotBB_L3)TotBB_L3,sum(TotFlo_L3)TotFlo_L3,  " +
" sum(TotOut_L4)TotOut_L4,sum(TotBB_L4)TotBB_L4,sum(TotFlo_L4)TotFlo_L4, " +
" sum(TotOut_L5)TotOut_L5,sum(TotBB_L5)TotBB_L5,sum(TotFlo_L5)TotFlo_L5, " +
" sum(TotOut_L6)TotOut_L6,sum(TotBB_L6)TotBB_L6,sum(TotFlo_L6)TotFlo_L6 into  Temp_TotOut from (  " +

" /** OutPut L1 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" isnull(sum(M3),0)TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='<15 Press' and Line=1 " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" isnull(sum(M3),0)TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='>=15 Press' and Line=1 " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket,  " +
" isnull(sum(M3),0)TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='Non Press' and Line=1  " +
" union all " +
" select '60'Urut,'Grand Total'Ket,  " +
" isnull(sum(M3),0)TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Line=1 " +
" union all  " +

" /** OutPut L2 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" isnull(sum(M3),0)TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='<15 Press' and Line=2 " +
" union all  " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" isnull(sum(M3),0)TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='>=15 Press' and Line=2  " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" isnull(sum(M3),0)TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='Non Press' and Line=2  " +
" union all  " +
" select '60'Urut,'Grand Total'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" isnull(sum(M3),0)TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Line=2  " +
" union all  " +

" /** OutPut L3 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" isnull(sum(M3),0)TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='<15 Press' and Line=3 " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" isnull(sum(M3),0)TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='>=15 Press' and Line=3 " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" isnull(sum(M3),0)TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='Non Press' and Line=3 " +
" union all  " +
" select '60'Urut,'Grand Total'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" isnull(sum(M3),0)TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Line=3 " +
" union all  " +

" /** OutPut L4 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" isnull(sum(M3),0)TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='<15 Press' and Line=4 " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" isnull(sum(M3),0)TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='>=15 Press' and Line=4  " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" isnull(sum(M3),0)TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='Non Press' and Line=4  " +
" union all " +
" select '60'Urut,'Grand Total'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" isnull(sum(M3),0)TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Line=4  " +
" union all  " +

" /** OutPut L5 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" isnull(sum(M3),0)TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='<15 Press' and Line=5 " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" isnull(sum(M3),0)TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='>=15 Press' and Line=5  " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" isnull(sum(M3),0)TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='Non Press' and Line=5  " +
" union all " +
" select '60'Urut,'Grand Total'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" isnull(sum(M3),0)TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Line=5  " +
" union all " +

" /** OutPut L6 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5,  " +
" isnull(sum(M3),0)TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from OuputProduksi where Noted='<15 Press' and Line=6 " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5,  " +
" isnull(sum(M3),0)TotOut_L6,0 TotFlo_L6,0 TotBB_L6  from OuputProduksi where Noted='>=15 Press' and Line=6  " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5,  " +
" isnull(sum(M3),0)TotOut_L6,0 TotFlo_L6,0 TotBB_L6  from OuputProduksi where Noted='Non Press' and Line=6  " +
" union all " +
" select '60'Urut,'Grand Total'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5,  " +
" isnull(sum(M3),0)TotOut_L6,0 TotFlo_L6,0 TotBB_L6  from OuputProduksi where Line=6  " +
" union all " +

" /** Total SPB Flooculant **/  " +
" /** Line 1 **/ " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,isnull(sum(Qty),0)TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='<15 Press' and Line=1 " +
" union all  " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" 0 TotOut_L1,isnull(sum(Qty),0)TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='>=15 Press' and Line=1 " +
" union all  " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,isnull(sum(Qty),0)TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='Non Press' and Line=1  " +
" union all  " +
" select '60'Urut,'Grand Total'Ket,  " +
" 0 TotOut_L1,isnull(sum(Qty),0)TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Line=1  " +
" union all  " +

" /** Line 2 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,isnull(sum(Qty),0)TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='<15 Press' and Line=2  " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,isnull(sum(Qty),0)TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='>=15 Press' and Line=2 " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,isnull(sum(Qty),0)TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='Non Press' and Line=2  " +
" union all  " +
" select '60'Urut,'Grand Total'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,isnull(sum(Qty),0)TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Line=2  " +
" union all  " +

" /** Line 3 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,isnull(sum(Qty),0)TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='<15 Press' and Line=3 " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,isnull(sum(Qty),0)TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='>=15 Press' and Line=3  " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,isnull(sum(Qty),0)TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='Non Press' and Line=3  " +
" union all " +
" select '60'Urut,'Grand Total'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,isnull(sum(Qty),0)TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Line=3  " +
" union all  " +

" /** Line 4 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,isnull(sum(Qty),0)TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='<15 Press' and Line=4 " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,isnull(sum(Qty),0)TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='>=15 Press' and Line=4  " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,isnull(sum(Qty),0)TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='Non Press' and Line=4  " +
" union all " +
" select '60'Urut,'Grand Total'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,isnull(sum(Qty),0)TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Line=4 " +
" union all  " +

" /** Line 5 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,isnull(sum(Qty),0)TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='<15 Press' and Line=5 " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,isnull(sum(Qty),0)TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='>=15 Press' and Line=5  " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,isnull(sum(Qty),0)TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='Non Press' and Line=5  " +
" union all " +
" select '60'Urut,'Grand Total'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,isnull(sum(Qty),0)TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Line=5 " +
" union all  " +

" /** Line 6 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,isnull(sum(Qty),0)TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='<15 Press' and Line=6 " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,isnull(sum(Qty),0)TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='>=15 Press' and Line=6  " +
" union all " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,isnull(sum(Qty),0)TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Noted='Non Press' and Line=6  " +
" union all " +
" select '60'Urut,'Grand Total'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,isnull(sum(Qty),0)TotFlo_L6,0 TotBB_L6 from Temp_DataFloo where Line=6 " +
" union all  " +

" /** Total SPB BB All **/ " +
" /** Line 1 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,sum(TotBB_L1)TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from ( select isnull(round(sum(PakaiBB),1),0)TotBB_L1,Tgl from TempAwal where Noted='<15 Press' and Line=1 group by Tgl ) as x " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,sum(TotBB_L1)TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from ( select isnull(round(sum(PakaiBB),1),0)TotBB_L1,Tgl from TempAwal where Noted='>=15 Press' and Line=1 group by Tgl ) as x  " +
" union all  " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,sum(TotBB_L1)TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from ( select isnull(round(sum(PakaiBB),1),0)TotBB_L1,Tgl from TempAwal where Noted='Non Press' and Line=1 group by Tgl ) as x  " +
" union all " +
" select '60'Urut,'Grand Total'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,sum(TotBB_L1)TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from ( select isnull(round(sum(PakaiBB),1),0)TotBB_L1,Tgl from TempAwal where Line=1 group by Tgl ) as x   " +
" union all  " +

" /** Line 2 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,sum(TotBB_L2)TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L2,Tgl from TempAwal where Noted='<15 Press' and Line=2 group by Tgl ) as x " +
" union all   " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,sum(TotBB_L2)TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L2,Tgl from TempAwal where Noted='>=15 Press' and Line=2 group by Tgl ) as x   " +
" union all  " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,sum(TotBB_L2)TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L2,Tgl from TempAwal where Noted='Non Press' and Line=2 group by Tgl ) as x  " +
" union all  " +
" select '60'Urut,'Grand Total'Ket,   " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,sum(TotBB_L2)TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L2,Tgl from TempAwal where Line=2 group by Tgl ) as x  " +
" union all   " +

" /** Line 3 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,   " +
" 0 TotOut_L3,0 TotFlo_L3,sum(TotBB_L3)TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L3,Tgl from TempAwal where Noted='<15 Press' and Line=3 group by Tgl ) as x   " +
" union all  " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,sum(TotBB_L3)TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L3,Tgl from TempAwal where Noted='>=15 Press' and Line=3 group by Tgl ) as x " +
" union all  " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2, " +
" 0 TotOut_L3,0 TotFlo_L3,sum(TotBB_L3)TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L3,Tgl from TempAwal where Noted='Non Press' and Line=3 group by Tgl ) as x  " +
" union all  " +
" select '60'Urut,'Grand Total'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,sum(TotBB_L3)TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L3,Tgl from TempAwal where Line=3 group by Tgl ) as x   " +
" union all " +

" /** Line 4 **/   " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,sum(TotBB_L4)TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L4,Tgl from TempAwal where Noted='<15 Press' and Line=4 group by Tgl ) as x  " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket,   " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,sum(TotBB_L4)TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L4,Tgl from TempAwal where Noted='>=15 Press' and Line=4 group by Tgl ) as x   " +
" union all   " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,sum(TotBB_L4)TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L4,Tgl from TempAwal where Noted='Non Press' and Line=4 group by Tgl ) as x   " +
" union all " +
" select '60'Urut,'Grand Total'Ket,   " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,   " +
" 0 TotOut_L4,0 TotFlo_L4,sum(TotBB_L4)TotBB_L4, " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L4,Tgl from TempAwal where Line=4 group by Tgl ) as x  " +
" union all " +

" /** Line 5 **/   " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,sum(TotBB_L5)TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L5,Tgl from TempAwal where Noted='<15 Press' and Line=5 group by Tgl ) as x  " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket,   " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,sum(TotBB_L5)TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L5,Tgl from TempAwal where Noted='>=15 Press' and Line=5 group by Tgl ) as x   " +
" union all   " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,sum(TotBB_L5)TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L5,Tgl from TempAwal where Noted='Non Press' and Line=5 group by Tgl ) as x   " +
" union all " +
" select '60'Urut,'Grand Total'Ket,   " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,   " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,sum(TotBB_L5)TotBB_L5, " +
" 0 TotOut_L6,0 TotFlo_L6,0 TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L5,Tgl from TempAwal where Line=5 group by Tgl ) as x  " +
" union all " +

" /** Line 6 **/  " +
" select '30'Urut,'Jumlah OutPut < 15 mm PRESS'Ket,  " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3, " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5,  " +
" 0 TotOut_L6,0 TotFlo_L6,sum(TotBB_L6)TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L6,Tgl from TempAwal where Noted='<15 Press' and Line=6 group by Tgl ) as x  " +
" union all " +
" select '40'Urut,'Jumlah OutPut >= 15 mm PRESS'Ket,   " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5,  " +
" 0 TotOut_L6,0 TotFlo_L6,sum(TotBB_L6)TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L6,Tgl from TempAwal where Noted='>=15 Press' and Line=6 group by Tgl ) as x   " +
" union all   " +
" select '50'Urut,'Jumlah OutPut Nonpress'Ket, " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1, " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,  " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5,  " +
" 0 TotOut_L6,0 TotFlo_L6,sum(TotBB_L6)TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L6,Tgl from TempAwal where Noted='Non Press' and Line=6 group by Tgl ) as x   " +
" union all " +
" select '60'Urut,'Grand Total'Ket,   " +
" 0 TotOut_L1,0 TotFlo_L1,0 TotBB_L1,  " +
" 0 TotOut_L2,0 TotFlo_L2,0 TotBB_L2,  " +
" 0 TotOut_L3,0 TotFlo_L3,0 TotBB_L3,   " +
" 0 TotOut_L4,0 TotFlo_L4,0 TotBB_L4,  " +
" 0 TotOut_L5,0 TotFlo_L5,0 TotBB_L5,  " +
" 0 TotOut_L6,0 TotFlo_L6,sum(TotBB_L6)TotBB_L6 from (  select isnull(round(sum(PakaiBB),1),0)TotBB_L6,Tgl from TempAwal where Line=6 group by Tgl ) as x  " +
" ) as x group by Ket,Urut order by Urut  " +

" select  *,   " +
" isnull((TotFlo_L1/nullif(TotBB_L1,0))*1000000,0)PPM_L1, " +
" isnull((TotFlo_L2/nullif(TotBB_L2,0))*1000000,0)PPM_L2,   " +
" isnull((TotFlo_L3/nullif(TotBB_L3,0))*1000000,0)PPM_L3,  " +
" isnull((TotFlo_L4/nullif(TotBB_L4,0))*1000000,0)PPM_L4,  " +
" isnull((TotFlo_L5/nullif(TotBB_L5,0))*1000000,0)PPM_L5, " +
" isnull((TotFlo_L6/nullif(TotBB_L6,0))*1000000,0)PPM_L6, " +

" case when ket='Jumlah OutPutl < 15 mm PRESS' then " +
" isnull(TotFlo_L1/nullif(TotOut_L1,0) ,0) " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then  " +
" isnull((TotFlo_L1/nullif(TotOut_L1,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25 ,0)  when ket='Jumlah OutPutl Nonpress' then " +
" isnull((TotFlo_L1/nullif(TotOut_L1,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25 ,0)  else 0 end Tot_EfL1, " +

" case when ket='Jumlah OutPut < 15 mm PRESS' then  " +
" isnull(TotFlo_L2/nullif(TotOut_L2,0),0)  " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then  " +
" isnull((TotFlo_L2/nullif(TotOut_L2,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25 ,0)  " +
" when ket='Jumlah OutPut Nonpress' then " +
" isnull((TotFlo_L2/nullif(TotOut_L2,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25 ,0)  else 0 end Tot_EfL2,  " +

" case when ket='Jumlah OutPut < 15 mm PRESS' then  " +
" isnull(TotFlo_L3/nullif(TotOut_L3,0) ,0)  " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then " +
" isnull((TotFlo_L3/nullif(TotOut_L3,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25 ,0)  " +
" when ket='Jumlah OutPutl Nonpress' then  " +
" isnull((TotFlo_L3/nullif(TotOut_L3,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25 ,0)  else 0 end Tot_EfL3,  " +

" case when ket='Jumlah OutPut < 15 mm PRESS' then  " +
" isnull(TotFlo_L4/nullif(TotOut_L4,0),0)   " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then " +
" isnull((TotFlo_L4/nullif(TotOut_L4,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25 ,0)  " +
" when ket='Jumlah OutPut Nonpress' then  " +
" isnull((TotFlo_L4/nullif(TotOut_L4,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25 ,0)  else 0 end Tot_EfL4,  " +

" case when ket='Jumlah OutPut < 15 mm PRESS' then  " +
" isnull(TotFlo_L5/nullif(TotOut_L5,0),0)   " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then  " +
" isnull((TotFlo_L5/nullif(TotOut_L5,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25 ,0)  " +
" when ket='Jumlah OutPut Nonpress' then  " +
" isnull((TotFlo_L5/nullif(TotOut_L5,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25 ,0)  else 0 end Tot_EfL5,  " +

" case when ket='Jumlah OutPut < 15 mm PRESS' then " +
" isnull(TotFlo_L6/nullif(TotOut_L6,0),0)   " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then " +
" isnull((TotFlo_L6/nullif(TotOut_L6,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25 ,0)  " +
" when ket='Jumlah OutPut Nonpress' then  " +
" isnull((TotFlo_L6/nullif(TotOut_L6,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25 ,0)  else 0 end Tot_EfL6,  " +

" case when ket='Jumlah OutPut < 15 mm PRESS' then (TotFlo_L1/nullif(TotOut_L1,0))*(TotOut_L1/nullif((select TotOut_L1 from Temp_TotOut where Ket='Grand Total'),0))   " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then ((TotFlo_L1/nullif(TotOut_L1,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L1/nullif((select TotOut_L1 from Temp_TotOut where Ket='Grand Total'),0)) " +
" when ket='Jumlah OutPut Nonpress' then isnull(((TotFlo_L1/nullif(TotOut_L1,0)/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L1/nullif((select TotOut_L1 from Temp_TotOut where Ket='Grand Total'),0))),0)    end  " +
" Param1,    " +

" case when ket='Jumlah OutPut < 15 mm PRESS' then (TotFlo_L2/nullif(TotOut_L2,0))*(TotOut_L2/nullif((select TotOut_L2 from Temp_TotOut where Ket='Grand Total'),0))   " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then ((TotFlo_L2/nullif(TotOut_L2,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L2/nullif((select TotOut_L2 from Temp_TotOut where Ket='Grand Total'),0))  " +
" when ket='Jumlah OutPut Nonpress' then isnull(((TotFlo_L2/nullif(TotOut_L2,0)/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L2/nullif((select TotOut_L2 from Temp_TotOut where Ket='Grand Total'),0))),0)    end  " +
" Param2,     " +

" case when ket='Jumlah OutPut < 15 mm PRESS' then (TotFlo_L3/nullif(TotOut_L3,0))*(TotOut_L3/nullif((select TotOut_L3 from Temp_TotOut where Ket='Grand Total'),0))  " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then ((TotFlo_L3/nullif(TotOut_L3,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L3/nullif((select TotOut_L3 from Temp_TotOut where Ket='Grand Total'),0)) " +
" when ket='Jumlah OutPut Nonpress' then isnull(((TotFlo_L3/nullif(TotOut_L3,0)/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L3/nullif((select TotOut_L3 from Temp_TotOut where Ket='Grand Total'),0))),0)  end Param3,   " +

" case when ket='Jumlah OutPut < 15 mm PRESS' then (TotFlo_L4/nullif(TotOut_L4,0))*(TotOut_L4/nullif((select TotOut_L4 from Temp_TotOut where Ket='Grand Total'),0))  " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then ((TotFlo_L4/nullif(TotOut_L4,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L4/nullif((select TotOut_L4 from Temp_TotOut where Ket='Grand Total'),0))     " +
" when ket='Jumlah OutPut Nonpress' then isnull(((TotFlo_L4/nullif(TotOut_L4,0)/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L4/nullif((select TotOut_L4 from Temp_TotOut where Ket='Grand Total'),0))),0)    end Param4, " +

" case when ket='Jumlah OutPut < 15 mm PRESS' then (TotFlo_L5/nullif(TotOut_L5,0))*(TotOut_L5/nullif((select TotOut_L5 from Temp_TotOut where Ket='Grand Total'),0))   " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then ((TotFlo_L5/nullif(TotOut_L5,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L5/nullif((select TotOut_L5 from Temp_TotOut where Ket='Grand Total'),0))     " +
" when ket='Jumlah OutPut Nonpress' then isnull(((TotFlo_L5/nullif(TotOut_L5,0)/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L5/nullif((select TotOut_L5 from Temp_TotOut where Ket='Grand Total'),0))),0)    end Param5, " +

" case  " +
" when ket='Jumlah OutPut < 15 mm PRESS' then (TotFlo_L6/nullif(TotOut_L6,0))*(TotOut_L6/nullif((select TotOut_L6 from Temp_TotOut where Ket='Grand Total'),0))  " +
" when ket='Jumlah OutPut >= 15 mm PRESS' then ((TotFlo_L6/nullif(TotOut_L6,0))/(select top 1 konversi from BM_PFloculantKonversi where keterangan='>=15 Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L6/nullif((select TotOut_L6 from Temp_TotOut where Ket='Grand Total'),0))    " +
" when ket='Jumlah OutPut Nonpress' then isnull(((TotFlo_L6/nullif(TotOut_L6,0)/(select top 1 konversi from BM_PFloculantKonversi where keterangan='Non Press' and RowStatus>-1 order by id desc)*0.25) * (TotOut_L6/nullif((select TotOut_L6 from Temp_TotOut where Ket='Grand Total'),0))),0) end Param6  into TempBawah1  from Temp_TotOut  where Ket<>'Grand Total'  " +

" select Urut,Ket,  " +
" TotOut_L1,TotBB_L1,TotFlo_L1,Tot_EfL1,PPM_L1,  " +
" TotOut_L2,TotBB_L2,TotFlo_L2,Tot_EfL2,PPM_L2,  " +
" TotOut_L3,TotBB_L3,TotFlo_L3,Tot_EfL3,PPM_L3,   " +
" TotOut_L4,TotBB_L4,TotFlo_L4,Tot_EfL4,PPM_L4, " +
" TotOut_L5,TotBB_L5,TotFlo_L5,Tot_EfL5,PPM_L5, " +
" TotOut_L6,TotBB_L6,TotFlo_L6,Tot_EfL6,PPM_L6 into TempBawah2 from TempBawah1   " +
" union all   " +
" select '60'Urut,'Total Pemakaian Floo'Ket, " +
" sum(TotOut_L1)TotOut_M3,sum(TotBB_L1)TotBB,sum(TotFlo_L1)TotFlo,isnull(sum(Param1),0)Tot_EfL1,isnull((sum(TotFlo_L1)/nullif(sum(TotBB_L1),0))*1000000,0) PPM_L1,  " +
" sum(TotOut_L2)TotOut_M3,sum(TotBB_L2)TotBB,sum(TotFlo_L2)TotFlo,isnull(sum(Param2),0)Tot_EfL2,isnull((sum(TotFlo_L2)/nullif(sum(TotBB_L2),0))*1000000,0) PPM_L2,  " +
" sum(TotOut_L3)TotOut_M3,sum(TotBB_L3)TotBB,sum(TotFlo_L3)TotFlo,isnull(sum(Param3),0)Tot_EfL3,isnull((sum(TotFlo_L3)/nullif(sum(TotBB_L3),0))*1000000,0) PPM_L3,  " +
" sum(TotOut_L4)TotOut_M3,sum(TotBB_L4)TotBB,sum(TotFlo_L4)TotFlo,isnull(sum(Param4),0)Tot_EfL4,isnull((sum(TotFlo_L4)/nullif(sum(TotBB_L4),0))*1000000,0) PPM_L4,  " +
" sum(TotOut_L5)TotOut_M3,sum(TotBB_L5)TotBB,sum(TotFlo_L5)TotFlo,isnull(sum(Param5),0)Tot_EfL5,isnull((sum(TotFlo_L5)/nullif(sum(TotBB_L5),0))*1000000,0) PPM_L5,  " +
" sum(TotOut_L6)TotOut_M3,sum(TotBB_L6)TotBB,sum(TotFlo_L6)TotFlo,isnull(sum(Param6),0)Tot_EfL6,isnull((sum(TotFlo_L6)/nullif(sum(TotBB_L6),0))*1000000,0) PPM_L6  " +
" from TempBawah1  " +

" select Urut2,Tanggal, " +
" cast(OutM3L1 as decimal(18,2))OutM3L1,cast(QtySPB_BBL1 as decimal(18,1))QtySPB_BBL1,cast(QtySPBL1 as decimal(18,1))QtySPBL1,EfesiensiL1,PPML1, " +
" cast(OutM3L2 as decimal(18,2))OutM3L2,cast(QtySPB_BBL2 as decimal(18,1))QtySPB_BBL2,cast(QtySPBL2 as decimal(18,1))QtySPBL2,EfesiensiL2,PPML2, " +
" cast(OutM3L3 as decimal(18,2))OutM3L3,cast(QtySPB_BBL3 as decimal(18,1))QtySPB_BBL3,cast(QtySPBL3 as decimal(18,1))QtySPBL3,EfesiensiL3,PPML3, " +
" cast(OutM3L4 as decimal(18,2))OutM3L4,cast(QtySPB_BBL4 as decimal(18,1))QtySPB_BBL4,cast(QtySPBL4 as decimal(18,1))QtySPBL4,EfesiensiL4,PPML4, " +
" cast(OutM3L5 as decimal(18,2))OutM3L5,cast(QtySPB_BBL5 as decimal(18,1))QtySPB_BBL5,cast(QtySPBL5 as decimal(18,1))QtySPBL5,EfesiensiL5,PPML5, " +
" cast(OutM3L6 as decimal(18,2))OutM3L6,cast(QtySPB_BBL6 as decimal(18,1))QtySPB_BBL6,cast(QtySPBL6 as decimal(18,1))QtySPBL6,EfesiensiL6,PPML6 from ( " +
" select case when urut='10' then '2' else '1' end urut2,* from TempJoinAtas   " +
" union all  " +
" select urut Urut2,* from TempBawah2  " +
" ) as x order by urut2,Tanggal ";


//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_Atas]') AND type in (N'U')) DROP TABLE [dbo].[Temp_Atas]  " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataOutput]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataOutput]    " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataSPB]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataSPB]   " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_DataFloo]') AND type in (N'U')) DROP TABLE [dbo].[Temp_DataFloo] " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_TotOut]') AND type in (N'U')) DROP TABLE [dbo].[Temp_TotOut]  " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempAwal]') AND type in (N'U')) DROP TABLE [dbo].[TempAwal]  " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBawah1]') AND type in (N'U')) DROP TABLE [dbo].[TempBawah1] " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBawah2]') AND type in (N'U')) DROP TABLE [dbo].[TempBawah2] " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempJoinAtas]') AND type in (N'U')) DROP TABLE [dbo].[TempJoinAtas] " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempJoinBawah]') AND type in (N'U')) DROP TABLE [dbo].[TempJoinBawah]  " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempJoinAll]') AND type in (N'U')) DROP TABLE [dbo].[TempJoinAll]  " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksi]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksi  " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblProposionate]') AND type in (N'U')) DROP TABLE [dbo].[tblProposionate]  " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPakaiNonPerShift]') AND type in (N'U')) DROP TABLE [dbo].[tblPakaiNonPerShift]  " +
//" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPakaiPerShift]') AND type in (N'U')) DROP TABLE [dbo].[tblPakaiPerShift] ";

        #endregion
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
        SqlDataReader sdr = da.RetrieveDataByString(strsql);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectKarawang(sdr));
            }
        }
        return arrData;
    }
    private string MatrixQuery2(string Periode, string Periode2, string Query)
    {
        string result =
        " declare @date datetime  set @date = '" + Periode2 + "';  " +
        " with DaysInMonth as (select @date as Date   " +
        " union all  select dateadd(dd,1,Date)  from DaysInMonth  where month(date) = month(@Date)), " +
        " data as (select * from BM_PFloculant where LEFT(convert(char,tanggal2,112),6)='" + Periode + "' and Rowstatus>-1), " +
        " data2 as (select * from BM_PFloculant where LEFT(convert(char,tanggal2,112),6)='" + Periode + "' and Rowstatus<0) " +

        " select Tanggal," +
        " OutM3L1,QtySPB_BBL1,QtySPBL1,EfesiensiL1,PPM1," +
        " OutM3L2,QtySPB_BBL2,QtySPBL2,EfesiensiL2,PPM2," +
        " OutM3L3,QtySPB_BBL3,QtySPBL3,EfesiensiL3,PPM3," +
        " OutM3L4,QtySPB_BBL4,QtySPBL4,EfesiensiL4,PPM4," +
        " OutM3L5,QtySPB_BBL5,QtySPBL5,EfesiensiL5,PPM5," +
        " OutM3L6,QtySPB_BBL6,QtySPBL6,EfesiensiL6,PPM6," +
        " Keterangan from ( " +
        " select '1'Urutan,case when B.Tanggal is null then (select top 1 dt.Tanggal from data2 dt where dt.Tanggal2=A.Date and Rowstatus<0) else B.Tanggal end Tanggal, " +
        " ISNULL(B.OutM3_L1,0)OutM3L1,ISNULL(B.PakaiBB_L1,0)QtySPB_BBL1,ISNULL(B.PakaiFlo_L1,0)QtySPBL1,ISNULL(B.E_L1,0)EfesiensiL1,ISNULL(B.PPM_L1,0)PPM1, " +
        " ISNULL(B.OutM3_L2,0)OutM3L2,ISNULL(B.PakaiBB_L2,0)QtySPB_BBL2,ISNULL(B.PakaiFlo_L2,0)QtySPBL2,ISNULL(B.E_L2,0)EfesiensiL2,ISNULL(B.PPM_L2,0)PPM2, " +
        " ISNULL(B.OutM3_L3,0)OutM3L3,ISNULL(B.PakaiBB_L3,0)QtySPB_BBL3,ISNULL(B.PakaiFlo_L3,0)QtySPBL3,ISNULL(B.E_L3,0)EfesiensiL3,ISNULL(B.PPM_L3,0)PPM3, " +
        " ISNULL(B.OutM3_L4,0)OutM3L4,ISNULL(B.PakaiBB_L4,0)QtySPB_BBL4,ISNULL(B.PakaiFlo_L4,0)QtySPBL4,ISNULL(B.E_L4,0)EfesiensiL4,ISNULL(B.PPM_L4,0)PPM4, " +
        " '0'OutM3L5,'0'QtySPB_BBL5,'0'QtySPBL5,'0'EfesiensiL5,'0'PPM5, " +
        " '0'OutM3L6,'0'QtySPB_BBL6,'0'QtySPBL6,'0'EfesiensiL6,'0'PPM6, " +
        " ISNULL(B.Keterangan,'-')Keterangan  " +
        " from DaysInMonth A left join data B ON A.Date=B.Tanggal2 where  month(date) = month(@Date) and B.Rowstatus>-1 " +
         " " + Query + " " +
        " ) as xx order by Urutan,Tanggal ";
        //" union all " +
        //" select '2'Urutan,''Tanggal, " +
        //" '0'OutM3L1,'0'QtyPakaiL1,'0'QtySPBL1,'0'EfesiensiL1,0'PPM1', " +
        //" '0'OutM3L2,'0'QtyPakaiL2,'0'QtySPBL2,'0'EfesiensiL2,0'PPM2', " +
        //" '0'OutM3L3,'0'QtyPakaiL3,'0'QtySPBL3,'0'EfesiensiL3,0'PPM3', " +
        //" '0'OutM3L4,'0'QtyPakaiL4,'0'QtySPBL4,'0'EfesiensiL4,0'PPM4','-'Keterangan ) as xx order by Urutan,Tanggal ";


        return result;
    }
    private string MatrixQueryReport(string Periode, string Periode2, string Query)
    {
        string result =

        " declare @date datetime  set @date = '" + Periode2 + "';  " +
        " with DaysInMonth as (select @date as Date   " +
        " union all  select dateadd(dd,1,Date)  from DaysInMonth  where month(date) = month(@Date)), " +
        " data as (select * from BM_PFloculant where LEFT(convert(char,tanggal2,112),6)='" + Periode + "' and Rowstatus>-1), " +
        " data2 as (select * from BM_PFloculant where LEFT(convert(char,tanggal2,112),6)='" + Periode + "' and Rowstatus<0) " +

        " select Tanggal," +
        " OutM3L1,QtyPakaiL1,QtySPBL1,EfesiensiL1,PPML1," +
        " OutM3L2,QtyPakaiL2,QtySPBL2,EfesiensiL2,PPML2," +
        " OutM3L3,QtyPakaiL3,QtySPBL3,EfesiensiL3,PPML3," +
        " OutM3L4,QtyPakaiL4,QtySPBL4,EfesiensiL4,PPML4," +
        " OutM3L5,QtyPakaiL5,QtySPBL5,EfesiensiL5,PPML5," +
        " OutM3L6,QtyPakaiL6,QtySPBL6,EfesiensiL6,PPML6," +
        " Keterangan from ( " +
        " select '1'Urutan,case when B.Tanggal is null then (select top 1 dt.Tanggal from data2 dt where dt.Tanggal2=A.Date and Rowstatus<0) else B.Tanggal end Tanggal, " +
        " ISNULL(B.OutM3_L1,0)OutM3L1,ISNULL(B.PakaiBB_L1,0)QtyPakaiL1,ISNULL(B.PakaiFlo_L1,0)QtySPBL1,ISNULL(B.E_L1,0)EfesiensiL1,ISNULL(B.PPM_L1,0)PPML1, " +
        " ISNULL(B.OutM3_L2,0)OutM3L2,ISNULL(B.PakaiBB_L2,0)QtyPakaiL2,ISNULL(B.PakaiFlo_L2,0)QtySPBL2,ISNULL(B.E_L2,0)EfesiensiL2,ISNULL(B.PPM_L2,0)PPML2, " +
        " ISNULL(B.OutM3_L3,0)OutM3L3,ISNULL(B.PakaiBB_L3,0)QtyPakaiL3,ISNULL(B.PakaiFlo_L3,0)QtySPBL3,ISNULL(B.E_L3,0)EfesiensiL3,ISNULL(B.PPM_L3,0)PPML3, " +
        " ISNULL(B.OutM3_L4,0)OutM3L4,ISNULL(B.PakaiBB_L4,0)QtyPakaiL4,ISNULL(B.PakaiFlo_L4,0)QtySPBL4,ISNULL(B.E_L4,0)EfesiensiL4,ISNULL(B.PPM_L4,0)PPML4, " +
        " '0'OutM3L5,'0'QtyPakaiL5,'0'QtySPBL5,'0'EfesiensiL5,'0'PPML5, " +
        " '0'OutM3L6,'0'QtyPakaiL6,'0'QtySPBL6,'0'EfesiensiL6,'0'PPML6, " +
        " ISNULL(B.Keterangan,'-')Keterangan  " +
        " from DaysInMonth A left join data B ON A.Date=B.Tanggal2 where  month(date) = month(@Date) and B.Rowstatus>-1 " +
         " " + Query + " " +
        " ) as xx order by Urutan,Tanggal ";

        return result;
    }
    private string MatrixQuery2K(string Periode, string Periode2, string Query)
    {
        string result =
        " declare @date datetime  set @date = '" + Periode2 + "';  " +
        " with DaysInMonth as (select @date as Date   " +
        " union all  select dateadd(dd,1,Date)  from DaysInMonth  where month(date) = month(@Date)), " +
        " data as (select * from BM_PFloculant where LEFT(convert(char,tanggal2,112),6)='" + Periode + "' and Rowstatus>-1), " +
        " data2 as (select * from BM_PFloculant where LEFT(convert(char,tanggal2,112),6)='" + Periode + "' and Rowstatus<0) " +

        " select Tanggal," +
        " OutM3L1,QtySPB_BBL1,QtySPBL1,EfesiensiL1,PPM1 PPML1," +
        " OutM3L2,QtySPB_BBL2,QtySPBL2,EfesiensiL2,PPM2 PPML2," +
        " OutM3L3,QtySPB_BBL3,QtySPBL3,EfesiensiL3,PPM3 PPML3," +
        " OutM3L4,QtySPB_BBL4,QtySPBL4,EfesiensiL4,PPM4 PPML4," +
        " OutM3L5,QtySPB_BBL5,QtySPBL5,EfesiensiL5,PPM5 PPML5," +
        " OutM3L6,QtySPB_BBL6,QtySPBL6,EfesiensiL6,PPM6 PPML6," +
        " Keterangan from ( " +
        " select '1'Urutan,case when B.Tanggal is null then (select top 1 dt.Tanggal from data2 dt where dt.Tanggal2=A.Date and Rowstatus<0) else B.Tanggal end Tanggal, " +
        " ISNULL(B.OutM3_L1,0)OutM3L1,ISNULL(B.PakaiBB_L1,0)QtySPB_BBL1,ISNULL(B.PakaiFlo_L1,0)QtySPBL1,ISNULL(B.E_L1,0)EfesiensiL1,ISNULL(B.PPM_L1,0)PPM1, " +
        " ISNULL(B.OutM3_L2,0)OutM3L2,ISNULL(B.PakaiBB_L2,0)QtySPB_BBL2,ISNULL(B.PakaiFlo_L2,0)QtySPBL2,ISNULL(B.E_L2,0)EfesiensiL2,ISNULL(B.PPM_L2,0)PPM2, " +
        " ISNULL(B.OutM3_L3,0)OutM3L3,ISNULL(B.PakaiBB_L3,0)QtySPB_BBL3,ISNULL(B.PakaiFlo_L3,0)QtySPBL3,ISNULL(B.E_L3,0)EfesiensiL3,ISNULL(B.PPM_L3,0)PPM3, " +
        " ISNULL(B.OutM3_L4,0)OutM3L4,ISNULL(B.PakaiBB_L4,0)QtySPB_BBL4,ISNULL(B.PakaiFlo_L4,0)QtySPBL4,ISNULL(B.E_L4,0)EfesiensiL4,ISNULL(B.PPM_L4,0)PPM4, " +
        " ISNULL(B.OutM3_L5,0)OutM3L5,ISNULL(B.PakaiBB_L5,0)QtySPB_BBL5,ISNULL(B.PakaiFlo_L5,0)QtySPBL5,ISNULL(B.E_L5,0)EfesiensiL5,ISNULL(B.PPM_L5,0)PPM5, " +
        " ISNULL(B.OutM3_L6,0)OutM3L6,ISNULL(B.PakaiBB_L6,0)QtySPB_BBL6,ISNULL(B.PakaiFlo_L6,0)QtySPBL6,ISNULL(B.E_L6,0)EfesiensiL6,ISNULL(B.PPM_L6,0)PPM6, " +
        " ISNULL(B.Keterangan,'-')Keterangan  " +
        " from DaysInMonth A left join data B ON A.Date=B.Tanggal2 where  month(date) = month(@Date) and B.Rowstatus>-1 " +
        " " + Query + " " +
        " ) as xx order by Urutan,Tanggal ";
        return result;
    }
    private string MatrixQuery2KReport(string Periode, string Periode2, string Query)
    {
        string result =

        //" select Tanggal, " +
        //" OutM3_L1 'OutM3L1' ,PakaiBB_L1 'QtyPakaiL1' ,PakaiFlo_L1 'QtySPBL1' ,E_L1 'EfesiensiL1' ,PPM_L1 'PPM1', " +
        //" OutM3_L2 'OutM3L2' ,PakaiBB_L2 'QtyPakaiL2' ,PakaiFlo_L2 'QtySPBL2' ,E_L2 'EfesiensiL2' ,PPM_L2 'PPM2', " +
        //" OutM3_L3 'OutM3L3' ,PakaiBB_L3 'QtyPakaiL3' ,PakaiFlo_L3 'QtySPBL3' ,E_L3 'EfesiensiL3' ,PPM_L3 'PPM3', " +
        //" OutM3_L4 'OutM3L4' ,PakaiBB_L4 'QtyPakaiL4' ,PakaiFlo_L4 'QtySPBL4' ,E_L4 'EfesiensiL4' ,PPM_L4 'PPM4', " +
        //" OutM3_L5 'OutM3L5' ,PakaiBB_L5 'QtyPakaiL5' ,PakaiFlo_L5 'QtySPBL5' ,E_L5 'EfesiensiL5' ,PPM_L5 'PPM5',  " +
        //" OutM3_L6 'OutM3L6' ,PakaiBB_L6 'QtyPakaiL6' ,PakaiFlo_L6 'QtySPBL6' ,E_L6 'EfesiensiL6' ,PPM_L6 'PPM6',Keterangan  " +
        //" from BM_PFloculant where Rowstatus>-1 and LEFT(convert(char,tanggal2,112),6)='" + Periode + "' order by tanggal2";

        " declare @date datetime  set @date = '" + Periode2 + "';  " +
        " with DaysInMonth as (select @date as Date   " +
        " union all  select dateadd(dd,1,Date)  from DaysInMonth  where month(date) = month(@Date)), " +
        " data as (select * from BM_PFloculant where LEFT(convert(char,tanggal2,112),6)='" + Periode + "' and Rowstatus>-1), " +
        " data2 as (select * from BM_PFloculant where LEFT(convert(char,tanggal2,112),6)='" + Periode + "' and Rowstatus<0) " +

        " select Tanggal," +
        " OutM3L1,QtyPakaiL1,QtySPBL1,EfesiensiL1,PPM1 PPML1," +
        " OutM3L2,QtyPakaiL2,QtySPBL2,EfesiensiL2,PPM2 PPML2," +
        " OutM3L3,QtyPakaiL3,QtySPBL3,EfesiensiL3,PPM3 PPML3," +
        " OutM3L4,QtyPakaiL4,QtySPBL4,EfesiensiL4,PPM4 PPML4," +
        " OutM3L5,QtyPakaiL5,QtySPBL5,EfesiensiL5,PPM5 PPML5," +
        " OutM3L6,QtyPakaiL6,QtySPBL6,EfesiensiL6,PPM6 PPML6," +
        " Keterangan from ( " +
        " select '1'Urutan,case when B.Tanggal is null then (select top 1 dt.Tanggal from data2 dt where dt.Tanggal2=A.Date and Rowstatus<0) else B.Tanggal end Tanggal, " +
        " ISNULL(B.OutM3_L1,0)OutM3L1,ISNULL(B.PakaiBB_L1,0)QtyPakaiL1,ISNULL(B.PakaiFlo_L1,0)QtySPBL1,ISNULL(B.E_L1,0)EfesiensiL1,ISNULL(B.PPM_L1,0)PPM1, " +
        " ISNULL(B.OutM3_L2,0)OutM3L2,ISNULL(B.PakaiBB_L2,0)QtyPakaiL2,ISNULL(B.PakaiFlo_L2,0)QtySPBL2,ISNULL(B.E_L2,0)EfesiensiL2,ISNULL(B.PPM_L2,0)PPM2, " +
        " ISNULL(B.OutM3_L3,0)OutM3L3,ISNULL(B.PakaiBB_L3,0)QtyPakaiL3,ISNULL(B.PakaiFlo_L3,0)QtySPBL3,ISNULL(B.E_L3,0)EfesiensiL3,ISNULL(B.PPM_L3,0)PPM3, " +
        " ISNULL(B.OutM3_L4,0)OutM3L4,ISNULL(B.PakaiBB_L4,0)QtyPakaiL4,ISNULL(B.PakaiFlo_L4,0)QtySPBL4,ISNULL(B.E_L4,0)EfesiensiL4,ISNULL(B.PPM_L4,0)PPM4, " +
        " ISNULL(B.OutM3_L5,0)OutM3L5,ISNULL(B.PakaiBB_L5,0)QtyPakaiL5,ISNULL(B.PakaiFlo_L5,0)QtySPBL5,ISNULL(B.E_L5,0)EfesiensiL5,ISNULL(B.PPM_L5,0)PPM5, " +
        " ISNULL(B.OutM3_L6,0)OutM3L6,ISNULL(B.PakaiBB_L6,0)QtyPakaiL6,ISNULL(B.PakaiFlo_L6,0)QtySPBL6,ISNULL(B.E_L6,0)EfesiensiL6,ISNULL(B.PPM_L6,0)PPM6, " +
        " ISNULL(B.Keterangan,'-')Keterangan  " +
        " from DaysInMonth A left join data B ON A.Date=B.Tanggal2 where  month(date) = month(@Date) and B.Rowstatus>-1 " +
        " " + Query + " " +
        " ) as xx order by Urutan,Tanggal ";

        //" union all " +
        //" select '2'Urutan,''Tanggal, " +
        //" '0'OutM3L1,'0'QtyPakaiL1,'0'QtySPBL1,'0'EfesiensiL1,0'PPM1', " +
        //" '0'OutM3L2,'0'QtyPakaiL2,'0'QtySPBL2,'0'EfesiensiL2,0'PPM2', " +
        //" '0'OutM3L3,'0'QtyPakaiL3,'0'QtySPBL3,'0'EfesiensiL3,0'PPM3', " +
        //" '0'OutM3L4,'0'QtyPakaiL4,'0'QtySPBL4,'0'EfesiensiL4,0'PPM4', " +
        //" '0'OutM3L5,'0'QtyPakaiL5,'0'QtySPBL5,'0'EfesiensiL5,0'PPM5', " +
        //" '0'OutM3L6,'0'QtyPakaiL6,'0'QtySPBL6,'0'EfesiensiL6,0'PPM6','-'Keterangan ) as xx order by Urutan,Tanggal ";

        return result;
    }
    public ArrayList RetrieveReportFlo2(string Periode, string Periode2, string Query)
    {
        arrData = new ArrayList();
        string strsql = this.MatrixQuery2(Periode, Periode2, Query);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQuery2(Periode, Periode2, Query));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObject(sdr));
            }
        }
        return arrData;
    }
    public ArrayList RetrieveReportFloReport(string Periode, string Periode2, string Query)
    {
        arrData = new ArrayList();
        string strsql = this.MatrixQueryReport(Periode, Periode2, Query);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQueryReport(Periode, Periode2, Query));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectL(sdr));
            }
        }
        return arrData;
    }
    public ArrayList RetrieveReportFlo2Karawang(string Periode, string Periode2, string Query)
    {
        arrData = new ArrayList();
        string strsql = this.MatrixQuery2K(Periode, Periode2, Query);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQuery2K(Periode, Periode2, Query));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectK(sdr));
            }
        }
        return arrData;
    }
    public ArrayList RetrieveReportFloKarawangReport(string Periode, string Periode2, string Query)
    {
        arrData = new ArrayList();
        string strsql = this.MatrixQuery2KReport(Periode, Periode2, Query);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQuery2KReport(Periode, Periode2, Query));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectL(sdr));
            }
        }
        return arrData;
    }
    public int RetrieveInputan(string Periode)
    {
        string StrSql =
        " select SUM(Total)Total from (select COUNT(ID)Total from BM_PFloculant " +
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
    public DomainBMReport2 Retrievetgl(string Tgl)
    {
        string StrSql =
            " select ID," +
            " PakaiBB_L1 PakaiBBL1,PakaiBB_L2 PakaiBBL2,PakaiBB_L3 PakaiBBL3,PakaiBB_L4 PakaiBBL4,isnull(PakaiBB_L5,0) PakaiBBL5,isnull(PakaiBB_L6,0) PakaiBBL6," +
            " PakaiFlo_L1 QtySPB_BBL1,PakaiFlo_L2 QtySPB_BBL2,PakaiFlo_L3 QtySPB_BBL3,PakaiFlo_L4 QtySPB_BBL4,isnull(PakaiFlo_L5,0) QtySPB_BBL5,isnull(PakaiFlo_L6,0) QtySPB_BBL6, " +
            " PPM_L1 PPML1,PPM_L2 PPML2,PPM_L3 PPML3,PPM_L4 PPML4,isnull(PPM_L5,0) PPML5,isnull(PPM_L6,0) PPML6" +
            " ,Keterangan from BM_PFloculant where Tanggal='" + Tgl + "' and rowstatus>-1 ";
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

        return new DomainBMReport2();
    }
    public DomainBMReport2 RetrieveTgl(string Tgl)
    {
        string StrSql =
            " select top 1 Tanggal2 from BM_PFloculant where Tanggal='" + Tgl + "' ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveTgl2(sqlDataReader);
            }
        }

        return new DomainBMReport2();
    }
    public DomainBMReport2 RetrieveSign(int ID)
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

        return new DomainBMReport2();
    }
    public DomainBMReport2 RetrieveDataOutM3(string tgl, int UnitKerjaID)
    {
        string Query = string.Empty; string Query2 = string.Empty; string Query3 = string.Empty;
        if (UnitKerjaID == 7)
        {
            Query =
            " union all " +
            " select 0'OutM3L1',0'OutM3L2',0'OutM3L3',0'OutM3L4',cast(SUM(M3) as decimal(10,0))OutM3L5,0'OutM3L6' " +
            " from (select (A.Volume*xx.QtyDest)M3  from (select SUM(Qty)QtyDest,ItemID " +
            " from BM_Destacking where TglProduksi in (select top 1 Tanggal2  from BM_PFloculant where Tanggal='" + tgl + "') and RowStatus>-1 " +
            " and PlantID=5 group by PlantID,ItemID) as xx INNER JOIN FC_Items A ON xx.ItemID=A.ID ) as xx5  " +
            " union all " +
            " select 0'OutM3L1',0'OutM3L2',0'OutM3L3',0'OutM3L4',0'OutM3L5',cast(SUM(M3) as decimal(10,0))OutM3L6 " +
            " from (select (A.Volume*xx.QtyDest)M3  from (select SUM(Qty)QtyDest,ItemID " +
            " from BM_Destacking where TglProduksi in (select top 1 Tanggal2  from BM_PFloculant where Tanggal='" + tgl + "') and RowStatus>-1 " +
            " and PlantID=6 group by PlantID,ItemID) as xx INNER JOIN FC_Items A ON xx.ItemID=A.ID ) as xx6  ";

            Query2 =
            ",SUM(OutM3L5)OutM3L5,SUM(OutM3L6)OutM3L6";
            Query3 =
            ",0'OutM3L5',0'OutM3L6'";
        }
        else if (UnitKerjaID == 1)
        {
            Query = "";
            //" union all " +
            //" select 0'OutM3L1',0'OutM3L2',0'OutM3L3',0'OutM3L4',0'OutM3L5',0'OutM3L6' " +
            //" union all " +
            //" select 0'OutM3L1',0'OutM3L2',0'OutM3L3',0'OutM3L4',0'OutM3L5',0'OutM3L6' ";  
            Query2 = ""; Query3 = "";
        }

        string StrSql =
        " select SUM(OutM3L1)OutM3L1,SUM(OutM3L2)OutM3L2,SUM(OutM3L3)OutM3L3,SUM(OutM3L4)OutM3L4,SUM(OutM3L5)OutM3L5,SUM(OutM3L6)OutM3L6 " +

        " from (select cast(SUM(M3) as decimal(10,0))OutM3L1,0'OutM3L2',0'OutM3L3',0'OutM3L4',0'OutM3L5',0'OutM3L6' " +
        " from (select (A.Volume*xx.QtyDest)M3  from (select SUM(Qty)QtyDest,ItemID " +
        " from BM_Destacking where TglProduksi in (select top 1 Tanggal2  from BM_PFloculant where Tanggal='" + tgl + "') and RowStatus>-1 " +
        " and PlantID=1 group by PlantID,ItemID) as xx INNER JOIN FC_Items A ON xx.ItemID=A.ID ) as xx1  " +
        " union all " +

        " select 0'OutM3L1',cast(SUM(M3) as decimal(10,0))OutM3L2,0'OutM3L3',0'OutM3L4',0'OutM3L5',0'OutM3L6' " +
        " from (select (A.Volume*xx.QtyDest)M3  from (select SUM(Qty)QtyDest,ItemID " +
        " from BM_Destacking where TglProduksi in (select top 1 Tanggal2  from BM_PFloculant where Tanggal='" + tgl + "') and RowStatus>-1 " +
        " and PlantID=2 group by PlantID,ItemID) as xx INNER JOIN FC_Items A ON xx.ItemID=A.ID ) as xx2  " +

        " union all " +

        " select 0'OutM3L1',0'OutM3L2',cast(SUM(M3) as decimal(10,0))OutM3L3,0'OutM3L4',0'OutM3L5',0'OutM3L6' " +
        " from (select (A.Volume*xx.QtyDest)M3  from (select SUM(Qty)QtyDest,ItemID " +
        " from BM_Destacking where TglProduksi in (select top 1 Tanggal2  from BM_PFloculant where Tanggal='" + tgl + "') and RowStatus>-1 " +
        " and PlantID=3 group by PlantID,ItemID) as xx INNER JOIN FC_Items A ON xx.ItemID=A.ID ) as xx3  " +

        " union all  " +

        " select 0'OutM3L1',0'OutM3L2',0'OutM3L4',cast(SUM(M3) as decimal(10,0))OutM3L4,0'OutM3L5',0'OutM3L6' " +
        " from (select (A.Volume*xx.QtyDest)M3  from (select SUM(Qty)QtyDest,ItemID " +
        " from BM_Destacking where TglProduksi in (select top 1 Tanggal2  from BM_PFloculant where Tanggal='" + tgl + "') and RowStatus>-1 " +
        " and PlantID=4 group by PlantID,ItemID) as xx INNER JOIN FC_Items A ON xx.ItemID=A.ID ) as xx4 " +
        " " + Query + "" +
        ") as DataFinal  ";

        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveDataUpdate(sqlDataReader);
            }
        }

        return new DomainBMReport2();
    }
    public DomainBMReport2 RetrieveDataUpdateSPB(string tgl, int UnitKerjaID)
    {
        string Query = string.Empty; string Query2 = string.Empty; string Query3 = string.Empty;
        if (UnitKerjaID == 7)
        {
            Query =
            " union all " +
            " select 0'QtySPBL1',0'QtySPBL2',0'QtySPBL3',0'QtySPBL4',ISNULL(SUM(Quantity),0)QtySPBL5,0'QtySPBL6' " +
            " from PakaiDetail where PakaiID in (select ID from Pakai where PakaiDate in " +
            " (select top 1 Tanggal2 from BM_PFloculant where Tanggal='" + tgl + "') and Status>1) " +
            " and ItemID in (select ID from Inventory where ItemCode='020636001000000' and Aktif=1) and RowStatus>-1 and ProdLine=5 " +
            " union all " +
            " select 0'QtySPBL1',0'QtySPBL2',0'QtySPBL3',0'QtySPBL4',0'QtySPBL5',ISNULL(SUM(Quantity),0)QtySPBL6 " +
            " from PakaiDetail where PakaiID in (select ID from Pakai where PakaiDate in " +
            " (select top 1 Tanggal2 from BM_PFloculant where Tanggal='" + tgl + "') and Status>1) " +
            " and ItemID in (select ID from Inventory where ItemCode='020636001000000' and Aktif=1) and RowStatus>-1 and ProdLine=6 ";
            Query2 =
            " ,SUM(QtySPBL5)QtySPBL5,SUM(QtySPBL6)QtySPBL6 ";
            Query3 =
            " ,0'QtySPBL5',0'QtySPBL6' ";
        }
        else if (UnitKerjaID == 1)
        {
            Query = ""; Query2 = ""; Query3 = "";
        }

        string StrSql =
        " select SUM(QtySPBL1)QtySPBL1,SUM(QtySPBL2)QtySPBL2,SUM(QtySPBL3)QtySPBL3,SUM(QtySPBL4)QtySPBL4,SUM(QtySPBL5)QtySPBL5,SUM(QtySPBL6)QtySPBL6 from  ( " +
        " select ISNULL(SUM(Quantity),0)QtySPBL1,0'QtySPBL2',0'QtySPBL3',0'QtySPBL4',0'QtySPBL5',0'QtySPBL6' from PakaiDetail where PakaiID in (select ID from Pakai where PakaiDate in " +
        " (select top 1 Tanggal2 from BM_PFloculant where Tanggal='" + tgl + "') and Status>1) " +
        " and ItemID in (select ID from Inventory where ItemCode='020636001000000' and Aktif=1) and RowStatus>-1 and ProdLine=1 " +
        " union all " +
        " select 0'QtySPBL1',ISNULL(SUM(Quantity),0)QtySPBL2,0'QtySPBL3',0'QtySPBL4',0'QtySPBL5',0'QtySPBL6' from PakaiDetail where PakaiID in (select ID from Pakai where PakaiDate in " +
        " (select top 1 Tanggal2 from BM_PFloculant where Tanggal='" + tgl + "') and Status>1) " +
        " and ItemID in (select ID from Inventory where ItemCode='020636001000000' and Aktif=1) and RowStatus>-1 and ProdLine=2 " +
        " union all " +
        " select 0'QtySPBL1',0'QtySPBL2',ISNULL(SUM(Quantity),0)QtySPBL3,0'QtySPBL4',0'QtySPBL5',0'QtySPBL6' from PakaiDetail where PakaiID in (select ID from Pakai where PakaiDate in " +
        " (select top 1 Tanggal2 from BM_PFloculant where Tanggal='" + tgl + "') and Status>1) " +
        " and ItemID in (select ID from Inventory where ItemCode='020636001000000' and Aktif=1) and RowStatus>-1 and ProdLine=3 " +
        " union all " +
        " select 0'QtySPBL1',0'QtySPBL2',0'QtySPBL3',ISNULL(SUM(Quantity),0)QtySPBL4,0'QtySPBL5',0'QtySPBL6' from PakaiDetail where PakaiID in (select ID from Pakai where PakaiDate in " +
        " (select top 1 Tanggal2 from BM_PFloculant where Tanggal='" + tgl + "') and Status>1) " +
        " and ItemID in (select ID from Inventory where ItemCode='020636001000000' and Aktif=1) and RowStatus>-1 and ProdLine=4 " +
        " ) as DataFinal ";

        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveDataUpdateSPB(sqlDataReader);
            }
        }

        return new DomainBMReport2();
    }
    private DomainBMReport2 GenerateObject_RetrieveDataUpdateSPB(SqlDataReader sdr)
    {
        DomainBMReport2 cpj = new DomainBMReport2();
        cpj.QtySPBL1 = Convert.ToDecimal(sdr["QtySPBL1"]);
        cpj.QtySPBL2 = Convert.ToDecimal(sdr["QtySPBL2"]);
        cpj.QtySPBL3 = Convert.ToDecimal(sdr["QtySPBL3"]);
        cpj.QtySPBL4 = Convert.ToDecimal(sdr["QtySPBL4"]);
        cpj.QtySPBL5 = Convert.ToDecimal(sdr["QtySPBL5"]);
        cpj.QtySPBL6 = Convert.ToDecimal(sdr["QtySPBL6"]);

        return cpj;
    }
    private DomainBMReport2 GenerateObject_RetrieveDataUpdate(SqlDataReader sdr)
    {
        DomainBMReport2 cpj = new DomainBMReport2();
        cpj.OutM3L1 = Convert.ToDecimal(sdr["OutM3L1"]);
        cpj.OutM3L2 = Convert.ToDecimal(sdr["OutM3L2"]);
        cpj.OutM3L3 = Convert.ToDecimal(sdr["OutM3L3"]);
        cpj.OutM3L4 = Convert.ToDecimal(sdr["OutM3L4"]);
        cpj.OutM3L5 = Convert.ToDecimal(sdr["OutM3L5"]);
        cpj.OutM3L6 = Convert.ToDecimal(sdr["OutM3L6"]);

        return cpj;
    }
    private DomainBMReport2 GenerateObject_RetrieveSign(SqlDataReader sdr)
    {
        DomainBMReport2 cpj = new DomainBMReport2();
        cpj.AdminSign = sdr["AdminSign"].ToString();
        cpj.MgrSign = sdr["MgrSign"].ToString();
        cpj.PMSign = sdr["PMSign"].ToString();
        return cpj;
    }

    private DomainBMReport2 GenerateObject(SqlDataReader sdr)
    {
        DomainBMReport2 cpj = new DomainBMReport2();
        cpj.Tanggal = sdr["Tanggal"].ToString();
        cpj.OutM3L1 = Convert.ToDecimal(sdr["OutM3L1"]);
        cpj.QtySPBL1 = Convert.ToDecimal(sdr["QtySPBL1"]);
        cpj.EfesiensiL1 = Convert.ToDecimal(sdr["EfesiensiL1"]);
        cpj.QtySPB_BBL1 = Convert.ToDecimal(sdr["QtySPB_BBL1"]);
        cpj.PPML1 = Convert.ToInt32(sdr["PPML1"]);

        cpj.OutM3L2 = Convert.ToDecimal(sdr["OutM3L2"]);
        cpj.QtySPBL2 = Convert.ToDecimal(sdr["QtySPBL2"]);
        cpj.EfesiensiL2 = Convert.ToDecimal(sdr["EfesiensiL2"]);
        cpj.QtySPB_BBL2 = Convert.ToDecimal(sdr["QtySPB_BBL2"]);
        cpj.PPML2 = Convert.ToInt32(sdr["PPML2"]);

        cpj.OutM3L3 = Convert.ToDecimal(sdr["OutM3L3"]);
        cpj.QtySPBL3 = Convert.ToDecimal(sdr["QtySPBL3"]);
        cpj.EfesiensiL3 = Convert.ToDecimal(sdr["EfesiensiL3"]);
        cpj.QtySPB_BBL3 = Convert.ToDecimal(sdr["QtySPB_BBL3"]);
        cpj.PPML3 = Convert.ToInt32(sdr["PPML3"]);

        cpj.OutM3L4 = Convert.ToDecimal(sdr["OutM3L4"]);
        cpj.QtySPBL4 = Convert.ToDecimal(sdr["QtySPBL4"]);
        cpj.EfesiensiL4 = Convert.ToDecimal(sdr["EfesiensiL4"]);
        cpj.QtySPB_BBL4 = Convert.ToDecimal(sdr["QtySPB_BBL4"]);
        cpj.PPML4 = Convert.ToInt32(sdr["PPML4"]);

        //cpj.OutM3L5 = Convert.ToDecimal(sdr["OutM3L5"]);
        //cpj.QtySPBL5 = Convert.ToDecimal(sdr["QtySPBL5"]);
        //cpj.EfesiensiL5 = Convert.ToDecimal(sdr["EfesiensiL5"]);

        //cpj.OutM3L6 = Convert.ToDecimal(sdr["OutM3L6"]);
        //cpj.QtySPBL6 = Convert.ToDecimal(sdr["QtySPBL6"]);
        //cpj.EfesiensiL6 = Convert.ToDecimal(sdr["EfesiensiL6"]);

        return cpj;
    }

    private DomainBMReport2 GenerateObjectL(SqlDataReader sdr)
    {
        DomainBMReport2 cpj = new DomainBMReport2();
        cpj.Tanggal = sdr["Tanggal"].ToString();

        cpj.OutM3L1 = Convert.ToInt32(sdr["OutM3L1"]);
        cpj.QtyPakaiL1 = Convert.ToDecimal(sdr["QtyPakaiL1"]);
        cpj.QtySPBL1 = Convert.ToDecimal(sdr["QtySPBL1"]);
        cpj.EfesiensiL1 = Convert.ToDecimal(sdr["EfesiensiL1"]);
        //cpj.PPML1 = Convert.ToInt32(sdr["PPML1"]);
        cpj.PPML1 = Convert.ToInt32(sdr["PPML1"]);

        cpj.OutM3L2 = Convert.ToInt32(sdr["OutM3L2"]);
        cpj.QtyPakaiL2 = Convert.ToDecimal(sdr["QtyPakaiL2"]);
        cpj.QtySPBL2 = Convert.ToDecimal(sdr["QtySPBL2"]);
        cpj.EfesiensiL2 = Convert.ToDecimal(sdr["EfesiensiL2"]);
        cpj.PPML2 = Convert.ToInt32(sdr["PPML2"]);

        cpj.OutM3L3 = Convert.ToInt32(sdr["OutM3L3"]);
        cpj.QtyPakaiL3 = Convert.ToDecimal(sdr["QtyPakaiL3"]);
        cpj.QtySPBL3 = Convert.ToDecimal(sdr["QtySPBL3"]);
        cpj.EfesiensiL3 = Convert.ToDecimal(sdr["EfesiensiL3"]);
        cpj.PPML3 = Convert.ToInt32(sdr["PPML3"]);

        cpj.OutM3L4 = Convert.ToInt32(sdr["OutM3L4"]);
        cpj.QtyPakaiL4 = Convert.ToDecimal(sdr["QtyPakaiL4"]);
        cpj.QtySPBL4 = Convert.ToDecimal(sdr["QtySPBL4"]);
        cpj.EfesiensiL4 = Convert.ToDecimal(sdr["EfesiensiL4"]);
        cpj.PPML4 = Convert.ToInt32(sdr["PPML4"]);

        cpj.OutM3L5 = Convert.ToInt32(sdr["OutM3L5"]);
        cpj.QtyPakaiL5 = Convert.ToDecimal(sdr["QtyPakaiL5"]);
        cpj.QtySPBL5 = Convert.ToDecimal(sdr["QtySPBL5"]);
        cpj.EfesiensiL5 = Convert.ToDecimal(sdr["EfesiensiL5"]);
        cpj.PPML5 = Convert.ToInt32(sdr["PPML5"]);

        cpj.OutM3L6 = Convert.ToInt32(sdr["OutM3L6"]);
        cpj.QtyPakaiL6 = Convert.ToDecimal(sdr["QtyPakaiL6"]);
        cpj.QtySPBL6 = Convert.ToDecimal(sdr["QtySPBL6"]);
        cpj.EfesiensiL6 = Convert.ToDecimal(sdr["EfesiensiL6"]);
        cpj.PPML6 = Convert.ToInt32(sdr["PPML6"]);

        //cpj.OutM3L5 = Convert.ToInt32(sdr["OutM3L5"]);
        //cpj.QtySPBL5 = Convert.ToDecimal(sdr["QtySPBL5"]);
        //cpj.EfesiensiL5 = Convert.ToDecimal(sdr["EfesiensiL5"]);

        //cpj.OutM3L6 = Convert.ToInt32(sdr["OutM3L6"]);
        //cpj.QtySPBL6 = Convert.ToDecimal(sdr["QtySPBL6"]);
        //cpj.EfesiensiL6 = Convert.ToDecimal(sdr["EfesiensiL6"]); 

        return cpj;
    }
    private DomainBMReport2 GenerateObjectKL(SqlDataReader sdr)
    {
        DomainBMReport2 cpj = new DomainBMReport2();
        cpj.Tanggal = sdr["Tanggal"].ToString();

        cpj.OutM3L1 = Convert.ToInt32(sdr["OutM3L1"]);
        cpj.QtyPakaiL1 = Convert.ToDecimal(sdr["QtyPakaiL1"]);
        cpj.QtySPBL1 = Convert.ToDecimal(sdr["QtySPBL1"]);
        cpj.EfesiensiL1 = Convert.ToDecimal(sdr["EfesiensiL1"]);
        cpj.PPML1 = Convert.ToInt32(sdr["PPM1"]);

        cpj.OutM3L2 = Convert.ToInt32(sdr["OutM3L2"]);
        cpj.QtyPakaiL2 = Convert.ToDecimal(sdr["QtyPakaiL2"]);
        cpj.QtySPBL2 = Convert.ToDecimal(sdr["QtySPBL2"]);
        cpj.EfesiensiL2 = Convert.ToDecimal(sdr["EfesiensiL2"]);
        cpj.PPML2 = Convert.ToInt32(sdr["PPM2"]);

        cpj.OutM3L3 = Convert.ToInt32(sdr["OutM3L3"]);
        cpj.QtyPakaiL3 = Convert.ToDecimal(sdr["QtyPakaiL3"]);
        cpj.QtySPBL3 = Convert.ToDecimal(sdr["QtySPBL3"]);
        cpj.EfesiensiL3 = Convert.ToDecimal(sdr["EfesiensiL3"]);
        cpj.PPML3 = Convert.ToInt32(sdr["PPM3"]);

        cpj.OutM3L4 = Convert.ToInt32(sdr["OutM3L4"]);
        cpj.QtyPakaiL4 = Convert.ToDecimal(sdr["QtyPakaiL4"]);
        cpj.QtySPBL4 = Convert.ToDecimal(sdr["QtySPBL4"]);
        cpj.EfesiensiL4 = Convert.ToDecimal(sdr["EfesiensiL4"]);
        cpj.PPML4 = Convert.ToInt32(sdr["PPM4"]);

        cpj.OutM3L5 = Convert.ToInt32(sdr["OutM3L5"]);
        cpj.QtyPakaiL5 = Convert.ToDecimal(sdr["QtyPakaiL5"]);
        cpj.QtySPBL5 = Convert.ToDecimal(sdr["QtySPBL5"]);
        cpj.EfesiensiL5 = Convert.ToDecimal(sdr["EfesiensiL5"]);
        cpj.PPML5 = Convert.ToInt32(sdr["PPM5"]);

        cpj.OutM3L6 = Convert.ToInt32(sdr["OutM3L6"]);
        cpj.QtyPakaiL6 = Convert.ToDecimal(sdr["QtyPakaiL6"]);
        cpj.QtySPBL6 = Convert.ToDecimal(sdr["QtySPBL6"]);
        cpj.EfesiensiL6 = Convert.ToDecimal(sdr["EfesiensiL6"]);
        cpj.PPML6 = Convert.ToInt32(sdr["PPM6"]);

        return cpj;
    }
    private DomainBMReport2 GenerateObjectK(SqlDataReader sdr)
    {
        DomainBMReport2 cpj = new DomainBMReport2();
        cpj.Tanggal = sdr["Tanggal"].ToString();
        cpj.Keterangan = sdr["Keterangan"].ToString();

        cpj.OutM3L1 = Convert.ToInt32(sdr["OutM3L1"]);
        cpj.QtySPBL1 = Convert.ToDecimal(sdr["QtySPBL1"]);
        cpj.EfesiensiL1 = Convert.ToDecimal(sdr["EfesiensiL1"]);
        //cpj.PakaiBBL1 = Convert.ToDecimal(sdr["PakaiBBL1"]);
        cpj.QtySPB_BBL1 = Convert.ToDecimal(sdr["QtySPB_BBL1"]);
        cpj.PPML1 = Convert.ToDecimal(sdr["PPML1"]);

        cpj.OutM3L2 = Convert.ToInt32(sdr["OutM3L2"]);
        cpj.QtySPBL2 = Convert.ToDecimal(sdr["QtySPBL2"]);
        cpj.EfesiensiL2 = Convert.ToDecimal(sdr["EfesiensiL2"]);
        //cpj.PakaiBBL2 = Convert.ToDecimal(sdr["PakaiBBL2"]);
        cpj.QtySPB_BBL2 = Convert.ToDecimal(sdr["QtySPB_BBL2"]);
        cpj.PPML2 = Convert.ToDecimal(sdr["PPML2"]);

        cpj.OutM3L3 = Convert.ToInt32(sdr["OutM3L3"]);
        cpj.QtySPBL3 = Convert.ToDecimal(sdr["QtySPBL3"]);
        cpj.EfesiensiL3 = Convert.ToDecimal(sdr["EfesiensiL3"]);
        //cpj.PakaiBBL3 = Convert.ToDecimal(sdr["PakaiBBL3"]);
        cpj.QtySPB_BBL3 = Convert.ToDecimal(sdr["QtySPB_BBL3"]);
        cpj.PPML3 = Convert.ToDecimal(sdr["PPML3"]);

        cpj.OutM3L4 = Convert.ToInt32(sdr["OutM3L4"]);
        cpj.QtySPBL4 = Convert.ToDecimal(sdr["QtySPBL4"]);
        cpj.EfesiensiL4 = Convert.ToDecimal(sdr["EfesiensiL4"]);
        //cpj.PakaiBBL4 = Convert.ToDecimal(sdr["PakaiBBL4"]);
        cpj.QtySPB_BBL4 = Convert.ToDecimal(sdr["QtySPB_BBL4"]);
        cpj.PPML4 = Convert.ToDecimal(sdr["PPML4"]);

        cpj.OutM3L5 = Convert.ToInt32(sdr["OutM3L5"]);
        cpj.QtySPBL5 = Convert.ToDecimal(sdr["QtySPBL5"]);
        cpj.EfesiensiL5 = Convert.ToDecimal(sdr["EfesiensiL5"]);
        //cpj.PakaiBBL5 = Convert.ToDecimal(sdr["PakaiBBL5"]);
        cpj.QtySPB_BBL5 = Convert.ToDecimal(sdr["QtySPB_BBL5"]);
        cpj.PPML5 = Convert.ToDecimal(sdr["PPML5"]);

        cpj.OutM3L6 = Convert.ToInt32(sdr["OutM3L6"]);
        cpj.QtySPBL6 = Convert.ToDecimal(sdr["QtySPBL6"]);
        cpj.EfesiensiL6 = Convert.ToDecimal(sdr["EfesiensiL6"]);
        //cpj.PakaiBBL6 = Convert.ToDecimal(sdr["PakaiBBL6"]);
        cpj.QtySPB_BBL6 = Convert.ToDecimal(sdr["QtySPB_BBL6"]);
        cpj.PPML6 = Convert.ToDecimal(sdr["PPML6"]);
        return cpj;
    }

    private DomainBMReport2 GenerateObjectKarawang(SqlDataReader sdr)
    {
        DomainBMReport2 cpj = new DomainBMReport2();
        cpj.Tanggal = sdr["Tanggal"].ToString();
        cpj.OutM3L1 = Convert.ToDecimal(sdr["OutM3L1"]);
        cpj.QtySPB_BBL1 = Convert.ToDecimal(sdr["QtySPB_BBL1"]);
        cpj.QtySPBL1 = Convert.ToDecimal(sdr["QtySPBL1"]);
        cpj.EfesiensiL1 = Convert.ToDecimal(sdr["EfesiensiL1"]);
        cpj.PPML1 = Convert.ToDecimal(sdr["PPML1"]);

        cpj.OutM3L2 = Convert.ToDecimal(sdr["OutM3L2"]);
        cpj.QtySPB_BBL2 = Convert.ToDecimal(sdr["QtySPB_BBL2"]);
        cpj.QtySPBL2 = Convert.ToDecimal(sdr["QtySPBL2"]);
        cpj.EfesiensiL2 = Convert.ToDecimal(sdr["EfesiensiL2"]);
        cpj.PPML2 = Convert.ToDecimal(sdr["PPML2"]);

        cpj.OutM3L3 = Convert.ToDecimal(sdr["OutM3L3"]);
        cpj.QtySPB_BBL3 = Convert.ToDecimal(sdr["QtySPB_BBL3"]);
        cpj.QtySPBL3 = Convert.ToDecimal(sdr["QtySPBL3"]);
        cpj.EfesiensiL3 = Convert.ToDecimal(sdr["EfesiensiL3"]);
        cpj.PPML3 = Convert.ToDecimal(sdr["PPML3"]);

        cpj.OutM3L4 = Convert.ToDecimal(sdr["OutM3L4"]);
        cpj.QtySPB_BBL4 = Convert.ToDecimal(sdr["QtySPB_BBL4"]);
        cpj.QtySPBL4 = Convert.ToDecimal(sdr["QtySPBL4"]);
        cpj.EfesiensiL4 = Convert.ToDecimal(sdr["EfesiensiL4"]);
        cpj.PPML4 = Convert.ToDecimal(sdr["PPML4"]);

        cpj.OutM3L5 = Convert.ToDecimal(sdr["OutM3L5"]);
        cpj.QtySPB_BBL5 = Convert.ToDecimal(sdr["QtySPB_BBL5"]);
        cpj.QtySPBL5 = Convert.ToDecimal(sdr["QtySPBL5"]);
        cpj.EfesiensiL5 = Convert.ToDecimal(sdr["EfesiensiL5"]);
        cpj.PPML5 = Convert.ToDecimal(sdr["PPML5"]);

        cpj.OutM3L6 = Convert.ToDecimal(sdr["OutM3L6"]);
        cpj.QtySPB_BBL6 = Convert.ToDecimal(sdr["QtySPB_BBL6"]);
        cpj.QtySPBL6 = Convert.ToDecimal(sdr["QtySPBL6"]);
        cpj.EfesiensiL6 = Convert.ToDecimal(sdr["EfesiensiL6"]);
        cpj.PPML6 = Convert.ToDecimal(sdr["PPML6"]);
        return cpj;
    }

    private DomainBMReport2 GenerateObject_RetrieveTgl(SqlDataReader sdr)
    {
        DomainBMReport2 cpj = new DomainBMReport2();
        cpj.ID = Convert.ToInt32(sdr["ID"]);
        cpj.PakaiBBL1 = Convert.ToDecimal(sdr["PakaiBBL1"]);
        cpj.PakaiBBL2 = Convert.ToDecimal(sdr["PakaiBBL2"]);
        cpj.PakaiBBL3 = Convert.ToDecimal(sdr["PakaiBBL3"]);
        cpj.PakaiBBL4 = Convert.ToDecimal(sdr["PakaiBBL4"]);
        cpj.PakaiBBL5 = Convert.ToDecimal(sdr["PakaiBBL5"]);
        cpj.PakaiBBL6 = Convert.ToDecimal(sdr["PakaiBBL6"]);

        cpj.QtySPB_BBL1 = Convert.ToDecimal(sdr["QtySPB_BBL1"]);
        cpj.QtySPB_BBL2 = Convert.ToDecimal(sdr["QtySPB_BBL2"]);
        cpj.QtySPB_BBL3 = Convert.ToDecimal(sdr["QtySPB_BBL3"]);
        cpj.QtySPB_BBL4 = Convert.ToDecimal(sdr["QtySPB_BBL4"]);
        cpj.QtySPB_BBL5 = Convert.ToDecimal(sdr["QtySPB_BBL5"]);
        cpj.QtySPB_BBL6 = Convert.ToDecimal(sdr["QtySPB_BBL6"]);

        cpj.PPML1 = Convert.ToDecimal(sdr["PPML1"]);
        cpj.PPML2 = Convert.ToDecimal(sdr["PPML2"]);
        cpj.PPML3 = Convert.ToDecimal(sdr["PPML3"]);
        cpj.PPML4 = Convert.ToDecimal(sdr["PPML4"]);
        cpj.PPML5 = Convert.ToDecimal(sdr["PPML5"]);
        cpj.PPML6 = Convert.ToDecimal(sdr["PPML6"]);
        cpj.Keterangan = sdr["Keterangan"].ToString();
        return cpj;
    }

    private DomainBMReport2 GenerateObject_RetrieveTgl2(SqlDataReader sdr)
    {
        DomainBMReport2 cpj = new DomainBMReport2();
        cpj.Tanggal2 = Convert.ToDateTime(sdr["Tanggal2"]);
        return cpj;
    }


}

public class DomainBMReport2
{
    public int UnitKerjaID { get; set; }
    public int ID { get; set; }
    public int PlantID { get; set; }

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

    public Decimal OutM3 { get; set; }

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

    //public Decimal QtySPB_BBL1 { get; set; }
    //public Decimal QtySPB_BBL2 { get; set; }
    //public Decimal QtySPB_BBL3 { get; set; }
    //public Decimal QtySPB_BBL4 { get; set; }
    //public Decimal QtySPB_BBL5 { get; set; }
    //public Decimal QtySPB_BBL6 { get; set; }

    public Decimal EfesiensiL1 { get; set; }
    public Decimal EfesiensiL2 { get; set; }
    public Decimal EfesiensiL3 { get; set; }
    public Decimal EfesiensiL4 { get; set; }
    public Decimal EfesiensiL5 { get; set; }
    public Decimal EfesiensiL6 { get; set; }

    public Decimal PPML1 { get; set; }
    public Decimal PPML2 { get; set; }
    public Decimal PPML3 { get; set; }
    public Decimal PPML4 { get; set; }
    public Decimal PPML5 { get; set; }
    public Decimal PPML6 { get; set; }

    public Decimal PakaiBBL1 { get; set; }
    public Decimal PakaiBBL2 { get; set; }
    public Decimal PakaiBBL3 { get; set; }
    public Decimal PakaiBBL4 { get; set; }
    public Decimal PakaiBBL5 { get; set; }
    public Decimal PakaiBBL6 { get; set; }

    public Decimal QtyPakaiL1 { get; set; }
    public Decimal QtyPakaiL2 { get; set; }
    public Decimal QtyPakaiL3 { get; set; }
    public Decimal QtyPakaiL4 { get; set; }
    public Decimal QtyPakaiL5 { get; set; }
    public Decimal QtyPakaiL6 { get; set; }

    public Decimal QtySPB_BBL1 { get; set; }
    public Decimal QtySPB_BBL2 { get; set; }
    public Decimal QtySPB_BBL3 { get; set; }
    public Decimal QtySPB_BBL4 { get; set; }
    public Decimal QtySPB_BBL5 { get; set; }
    public Decimal QtySPB_BBL6 { get; set; }
}