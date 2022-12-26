using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace GRCweb1.Modul.Factory
{
    public partial class PostingCOGS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                ddlBulan.SelectedIndex = int.Parse(DateTime.Now.Month.ToString());
                txtTahun.Text = DateTime.Now.Year.ToString();
                LoadData();
            }
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            string strerror = string.Empty;
            string thnbln = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            KartuStockFacade ksf = new KartuStockFacade();
            strerror = ksf.UpdateHPPbyDestacking(thnbln);
            if (strerror == string.Empty)
                strerror = ksf.UpdateHPPbyTransitOut(thnbln);
            else
                DisplayAJAXMessage(this, "Posting HPP By Transit Out Error");
            if (strerror == string.Empty)
                strerror = ksf.UpdateHPPbySimetris(thnbln);
            else
                DisplayAJAXMessage(this, "Posting HPP By Simetris Error");
            if (strerror == string.Empty)
                DisplayAJAXMessage(this, "Posting HPP Berhasil");
            else
                DisplayAJAXMessage(this, "Posting HPP Error");
        }

        protected void LoadData()
        {
            ArrayList t3biaya = new ArrayList();
            T3_BiayaFacade t3bf = new T3_BiayaFacade();
            if (int.Parse(ddlBulan.SelectedValue) > 0)
            {
                t3biaya = t3bf.RetrieveByPeriode(int.Parse(txtTahun.Text), int.Parse(ddlBulan.SelectedValue));
                GridViewBiaya.DataSource = t3biaya;
                GridViewBiaya.DataBind();
                LblBiaya.Text = t3bf.RetrieveTotalBiaya(int.Parse(txtTahun.Text), int.Parse(ddlBulan.SelectedValue)).ToString("###,###,###,###,##0.00");
            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string strFileName = Server.HtmlEncode(FileUpload1.FileName);
                string strExtension = Path.GetExtension(strFileName);
                Users users = (Users)Session["Users"];
                if (strExtension != ".xls" && strExtension != ".xlsx")
                {
                    Response.Write("<script>alert('Please select an Excel spreadsheet to import!');</script>");
                    return;
                }
                string strUploadFileName = "~\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + strExtension;
                FileUpload1.SaveAs(Server.MapPath(strUploadFileName));

                string strExcelConn = "";
                if (strExtension == ".xls")
                {
                    // Excel 97-2003
                    strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(strUploadFileName) + ";Extended Properties='Excel 8.0;HDR=YES;'";
                }
                else
                {
                    // Excel 2007
                    strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(strUploadFileName) + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                }
                int count = 0;
                T3_Biaya t3biaya = new T3_Biaya();
                T3_BiayaFacade t3biayafacade = new T3_BiayaFacade();
                try
                {
                    DataTable dtExcel = new DataTable();
                    OleDbConnection con = new OleDbConnection(strExcelConn);
                    string query = "Select * from [Sheet1$]";
                    OleDbDataAdapter data = new OleDbDataAdapter(query, con);
                    data.Fill(dtExcel);
                    t3biaya.Tahun = Convert.ToInt32(txtTahun.Text);
                    t3biaya.Bulan = Convert.ToInt32(ddlBulan.SelectedValue);
                    t3biayafacade.Delete(t3biaya);
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        try
                        {
                            if (dtExcel.Rows[i][0].ToString() != string.Empty && dtExcel.Rows[i][1].ToString() != string.Empty && dtExcel.Rows[i][2].ToString() != string.Empty)
                            {
                                t3biaya.COA = dtExcel.Rows[i][0].ToString();
                                t3biaya.AccName = dtExcel.Rows[i][1].ToString();
                                t3biaya.Biaya = decimal.Parse(dtExcel.Rows[i][2].ToString());
                                t3biaya.CreatedBy = users.UserName;
                                if (t3biaya.COA.Trim() != string.Empty && t3biaya.AccName.Trim() != string.Empty && t3biaya.Biaya > 0)
                                    t3biayafacade.Insert(t3biaya);
                            }
                            count = count + 1;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    File.Delete(Server.MapPath(strUploadFileName));
                    if (count == dtExcel.Rows.Count)
                    {
                        DisplayAJAXMessage(this, "Import data sukses");
                        LoadData();
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Import data gagal 1");
                    }
                }
                catch
                {
                    DisplayAJAXMessage(this, "Import data gagal 2");
                }
                finally
                {
                    //conLinq.Dispose();
                }
            }
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void txtTahun_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}