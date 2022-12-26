using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain;
using BusinessFacade;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataAccessLayer;

namespace GRCweb1.Modul.Boardmill
{
    public partial class PemantauanScrabKeringDanBasah : System.Web.UI.Page
    {
        int total1 = 0;
        int total11 = 0;
        decimal total2 = 0;
        decimal total21 = 0;
        decimal total3 = 0;
        decimal total31 = 0;
        decimal hasiltotalm3 = 0;

        ScrubFacade sf = new ScrubFacade();
        Scrub scrub = new Scrub();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtDrTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtSdTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                btnApprove.Visible = false;
                ddlPilihan.Items[0].Selected = true;

                statusApprovalAll();

                MunculBtnApprove();


            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(ExportXls);


        }

        protected void statusApprovalAll()
        {
            string cekTahun = sf.statusScrubAppovalTahun();
            string cekBulan = sf.statusScrubAppovalBulan();

            string hasil = string.Empty;

            string strsql =
            "select Case Bulan " +
            "when 1 then 'Januari' " +
            "when 2 then 'Februari' " +
            "when 3 then 'Maret' " +
            "when 4 then 'Januari' " +
            "when 5 then 'Januari' " +
            "when 6 then 'Januari' " +
            "when 7 then 'Januari' " +
            "when 8 then 'Januari' " +
            "when 9 then 'Januari' " +
            "when 10 then 'Januari' " +
            "when 11 then 'Januari' " +
            "when 12 then 'Januari' end Bulan, " +

            "Tahun, " +

            "case StatusApv " +
            "when 0 then 'Open' " +
            "when 1 then 'Head' " +
            "when 2 then 'Mgr' " +
            "when 3 then 'Plant Mgr' end StatusApproval " +

            "from Scrab_Approval Where Bulan='" + cekBulan + "' and Tahun='" + cekTahun + "' and  RowStatus >-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    txttahun.Text = sdr["Tahun"].ToString();
                    txtbulan.Text = sdr["Bulan"].ToString();
                    txtstatusapproval.Text = sdr["StatusApproval"].ToString();
                }
            }



        }



        protected void ddlPilihan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MunculBtnApprove()
        {
            //string dTglDari = DateTime.Parse(txtDrTgl.Text).ToString("yyyy");
            string cekTahun = sf.statusScrubAppovalTahun();
            string cekBulan = sf.statusScrubAppovalBulan();

            string userCreate = ((Users)Session["Users"]).UserName.ToString();
            string nama = sf.CekUserApproval(userCreate);
            string levelApproval = sf.statusApproval(cekBulan, cekTahun);
            string level = sf.levelApprove(nama);

            if (nama == "")
            {
                btnApprove.Visible = false;

            }
            else if (nama == userCreate && levelApproval == "Open" && level == "1")
            {
                btnApprove.Visible = true;

            }
            else if (nama == userCreate && levelApproval == "Head" && level == "2")
            {
                btnApprove.Visible = true;
            }
            else if (nama == userCreate && levelApproval == "Mgr" && level == "3")
            {
                btnApprove.Visible = true;
            }

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string cekTahun = sf.statusScrubAppovalTahun();
            string cekBulan = sf.statusScrubAppovalBulan();



            string userCreate = ((Users)Session["Users"]).UserName.ToString();

            string nama = sf.CekUserApproval(userCreate);
            string levelApproval = sf.statusApproval(cekBulan, cekTahun);
            string level = sf.levelApprove(nama);

            if (nama == "")
            {
                btnApprove.Visible = false;

            }
            else if (nama == userCreate && levelApproval == "Open" && level == "1")
            {
                scrub.Bulan = int.Parse(cekBulan);
                scrub.Tahun = int.Parse(cekTahun);
                scrub.Level = int.Parse(level);

                int intResult = 0;
                intResult = sf.Update(scrub);

                statusApprovalAll();


            }
            else if (nama == userCreate && levelApproval == "Head" && level == "2")
            {
                //Permintaan PES agar pa zuhri tidak Approval, langsung di Approval di Mgr HRD
                string updateLevelPlantMgr = "3";

                scrub.Bulan = int.Parse(cekBulan);
                scrub.Tahun = int.Parse(cekTahun);
                scrub.Level = int.Parse(updateLevelPlantMgr);

                int intResult = 0;
                intResult = sf.Update(scrub);

                statusApprovalAll();
            }
            else if (nama == userCreate && levelApproval == "Mgr" && level == "3")
            {
                scrub.Bulan = int.Parse(cekBulan);
                scrub.Tahun = int.Parse(cekTahun);
                scrub.Level = int.Parse(level);

                int intResult = 0;
                intResult = sf.Update(scrub);

                statusApprovalAll();
            }


        }


        public void LoadData()
        {
            ArrayList arrScr = new ArrayList();
            ScrubFacade Sc = new ScrubFacade();

            int crt = int.Parse(ddlPilihan.SelectedValue.ToString());
            string dTglDari = DateTime.Parse(txtDrTgl.Text).ToString("yyyyMMdd");
            string dTglSampai = DateTime.Parse(txtSdTgl.Text).ToString("yyyyMMdd");


            arrScr = Sc.RetrieveScabTipe(crt, dTglDari, dTglSampai);
            lstScrab.DataSource = arrScr;
            lstScrab.DataBind();
        }

        protected void lstScrab_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {



            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                total1 += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "PaletJumlah"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lbljumlah = (Label)e.Item.FindControl("lbljumlah");
                lbljumlah.Text = total1.ToString();


            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                total2 += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "BeratPalet"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {


                Label lblberatkg = (Label)e.Item.FindControl("lblberatkg");
                lblberatkg.Text = total2.ToString();


            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                total3 += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "M3Palet"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {


                Label lblhasilm3 = (Label)e.Item.FindControl("lblhasilm3");
                lblhasilm3.Text = total3.ToString("N1");


            }


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                total11 += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "BinJumlah"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lbljumlah1 = (Label)e.Item.FindControl("lbljumlah1");
                lbljumlah1.Text = total11.ToString();


            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                total21 += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "BeratBin"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {


                Label lblberatkg1 = (Label)e.Item.FindControl("lblberatkg1");
                lblberatkg1.Text = total21.ToString();


            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                total31 += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "M3Bin"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {


                Label lblhasilm31 = (Label)e.Item.FindControl("lblhasilm31");
                lblhasilm31.Text = total31.ToString("N1");


            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                hasiltotalm3 += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "TotalM3"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {


                Label lblhasitotalm3 = (Label)e.Item.FindControl("lblhasitotalm3");
                lblhasitotalm3.Text = hasiltotalm3.ToString("N1");


            }


        }


        protected void Preview_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {

            string strValidate = validasiInputApproval();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            else
            {
                string userCreate = ((Users)Session["Users"]).UserName.ToString();

                string dTglDari = DateTime.Parse(txtDrTgl.Text).ToString("yyyy");
                string dTglSampai = DateTime.Parse(txtSdTgl.Text).ToString("MM");
                Scrub scrub = new Scrub();
                ScrubFacade scrubFacade = new ScrubFacade();
                scrub.Bulan = int.Parse(dTglSampai);
                scrub.Tahun = int.Parse(dTglDari);
                scrub.Createdby = userCreate.ToString();
                int intResult = 0;
                intResult = scrubFacade.InsertApproval(scrub);

                statusApprovalAll();
            }



        }

        private string validasiInputApproval()
        {

            string tahun = DateTime.Parse(txtDrTgl.Text).ToString("yyyy");
            string bulan = DateTime.Parse(txtSdTgl.Text).ToString("MM");

            string cekTahun = sf.cekTahunSimpan(tahun);
            string cekBulan = sf.cekBulanSimpan(bulan);

            string cekStatus = sf.statusScrubAppovalTahunBulan(bulan, tahun);

            string strmessage = string.Empty;



            if (cekTahun.ToString() != "" && cekBulan.ToString() != "")

                return "Data Sudah Pernah di simpan, Status : " + cekStatus + " ";

            return strmessage;


        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanStatusScrab.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "Jenis Scrab :" + ddlPilihan.SelectedItem.ToString().ToUpper();
            Html += "<br>Periode : " + txtDrTgl.Text + " s/d " + txtSdTgl.Text;
            Html += "<br><form id='frm1' runat='server' method='post'>";
            string HtmlEnd = "</form>";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

    }
}