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

namespace GRCweb1.ModalDialog
{
    public partial class RekapPESDetailNew : System.Web.UI.Page
    {
        private decimal TotalBobot = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string str = (Request.QueryString["d"] != null) ? Request.QueryString["d"].ToString() : "";
                if (str != "")
                {
                    string[] data = str.Split(':');
                    txtPIC1.Text = UserName(data[0].ToString());
                    GetPICData(int.Parse(data[1].ToString()), int.Parse(data[2].ToString()));
                    txtJudul.Text = "REKAP PES " + data[3].ToUpper() + " SEMESTER : " + data[5].ToString() + " TAHUN " + data[4].ToString();
                    preparedHeader(data[5].ToString());
                    LoadPES(str);
                }
            }
            ((ScriptManager)Page.FindControl("ScriptManager2")).RegisterPostBackControl(btnExport);
        }
        private void preparedHeader(string smt)
        {
            HtmlTableRow tr = (HtmlTableRow)hd1;
            HtmlTableRow tr2 = (HtmlTableRow)hd2;
            for (int i = 1; i < tr.Cells.Count; i++)
            {
                switch (smt)
                {
                    case "1": tr.Cells[i].Visible = (i > 7 && i < (tr.Cells.Count - 1)) ? false : true; tr.Cells[tr.Cells.Count - 1].Visible = false; break;
                    case "2": tr.Cells[i].Visible = (i < 8 && i > 1) ? false : true; tr.Cells[tr.Cells.Count - 1].Visible = false; break;
                    default: tr.Cells[i].Visible = true; break;
                }
            }
            for (int n = 1; n < tr2.Cells.Count; n++)
            {
                switch (smt)
                {
                    case "1":
                        tr2.Cells[n].Visible = (n > 11) ? false : true;
                        tr2.Cells[n - 1].Style["Width"] = Unit.Pixel(50).ToString();
                        tr.Cells[tr.Cells.Count - 1].Visible = false;
                        tr.Cells[0].Style["Width"] = Unit.Pixel(350).ToString();
                        break;
                    case "2":
                        tr2.Cells[n].Visible = (n < 15 && n > 2) ? false : true;
                        tr2.Cells[n].Style["Width"] = Unit.Pixel(50).ToString();
                        tr.Cells[tr.Cells.Count - 1].Visible = false;
                        tr.Cells[0].Style["Width"] = Unit.Pixel(350).ToString();
                        break;
                    default: tr2.Cells[n].Visible = true; tr.Cells[tr.Cells.Count - 1].Visible = false; break;
                }
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapPesDetail.xls");
            Response.Charset = "utf-8";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>" + txtJudul.Text.ToUpper() + "</b>";
            string HtmlEnd = "";
            lstr.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        private void GetPICData(int DeptID, int BagianID)
        {
            DeptFacade dp = new DeptFacade();
            Dept d = new Dept();
            d = dp.RetrieveById(DeptID);
            txtDept1.Text = d.DeptName.ToUpper();

            ISO_Bagian b = new ISO_Bagian();
            ISO_BagianFacade bf = new ISO_BagianFacade();
            b = bf.RetrieveById(BagianID);
            txtJabatan1.Text = b.BagianName.ToString();
        }
        private int adarebobot = 0;
        private void LoadPES(string str)
        {
            ArrayList arrData = new ArrayList();
            string[] data = str.Split(':');
            ISO_PES ip = new ISO_PES();
            ip.PICName = data[0].ToString();
            ip.Tahun = int.Parse(data[4].ToString());
            ip.DeptID = int.Parse(data[1].ToString());
            ip.BagianID = int.Parse(data[2].ToString());
            ip.Semester = data[5].ToString();
            ip.Criteria = " Where xx.BagianID=" + data[2].ToString();
            arrData = ip.LoadPES(data[3].ToString().ToUpper(), true);
            lstPES.DataSource = arrData;
            lstPES.DataBind();
            TotalBobot = 0;
            adarebobot = 0;

        }
        protected void lstPES_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string str = (Request.QueryString["d"] != null) ? Request.QueryString["d"].ToString() : "";
            string[] data = str.Split(':');
            ISO_PES isp = new ISO_PES();
            #region ItemTemplate
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RekapPesDetail p = (RekapPesDetail)e.Item.DataItem;
                adarebobot += p.Penilaian;
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps1");
                if (tr != null)
                {

                    string[] blnRbt = (p.Rebobot.ToString() != "" || p.Rebobot.ToString() != "0") ? p.Rebobot.ToString().Split(',') : new string[] { };

                    for (int i = 1; i < tr.Cells.Count; i++)
                    {
                        //pilih semester data[5]
                        #region pembagian semester
                        switch (data[5].ToString())
                        {
                            case "1":
                                #region Semester satu
                                tr.Cells[i].Visible = (i > 13 && i < (tr.Cells.Count - 1)) ? false : true;
                                tr.Cells[tr.Cells.Count - 1].Visible = false;
                                tr.Cells[i].InnerHtml = (tr.Cells[i].InnerHtml == "0.00       " || tr.Cells[i].InnerHtml == "0,00       ") ? "" : tr.Cells[i].InnerHtml;

                                if (tr.Cells[i].InnerHtml == "")
                                {
                                    tr.Cells[i].Attributes.Add("class", "NonLine");
                                }

                                switch (p.PESName)
                                {
                                    case "KPI":
                                    case "SOP":
                                        tr.Attributes.Add("class", "total");
                                        tr.Cells[0].InnerHtml = ""; //e.Item.ItemIndex.ToString();
                                                                    //tr.Visible = false;
                                        if (i % 2 == 0)
                                        {
                                            tr.Cells[i].Attributes.Add("title", "Total Bobot");
                                            tr.Cells[i].Attributes.Add("class", "kotak tengah bold cursor EvenRows");
                                            decimal pros = 0;
                                            if (decimal.TryParse(tr.Cells[i].InnerText, out pros))
                                            {
                                                if (pros != 100)
                                                {
                                                    tr.Cells[i].Attributes.Add("style", "border:2px solid red");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tr.Cells[i].Attributes.Add("class", "kotak tengah bold");
                                        }
                                        break;
                                    default:
                                        switch (p.Penilaian)
                                        {
                                            case 6:
                                                tr.Cells[1].Attributes["title"] = "Semesteran";
                                                tr.Cells[1].Attributes.Add("class", "kotak rebobot tengah");
                                                rbt.Visible = true;
                                                break;
                                            case 12:
                                                tr.Cells[1].Attributes["title"] = "Tahunan";
                                                tr.Cells[1].Attributes.Add("class", "kotak rebobot2 tengah");
                                                rbt.Visible = true;
                                                break;
                                            default:
                                                rbt.Visible = false;
                                                tr.Cells[1].Attributes.Add("class", "kotak bold tengah");
                                                break;
                                        }

                                        if (blnRbt.Length > 0 && (p.PESName != "KPI" || p.PESName != "SOP"))
                                        {
                                            int pos = Array.IndexOf(blnRbt, i.ToString());
                                            if (pos > -1)
                                            {
                                                tr.Cells[(i + int.Parse(blnRbt[pos].ToString()))].Attributes["class"] = "kotak Line3 tengah bold";
                                                tr.Cells[(i + int.Parse(blnRbt[pos].ToString())) + 1].Attributes["class"] = "kotak Line3 tengah bold";
                                            }

                                        }
                                        break;
                                }
                                #endregion
                                break;
                            case "2":
                                #region Semester 2
                                tr.Cells[i].Visible = (i < 14 && i > 1) ? false : true;
                                tr.Cells[tr.Cells.Count - 1].Visible = false;

                                tr.Cells[i].InnerHtml = (tr.Cells[i].InnerHtml == "0.00       " || tr.Cells[i].InnerHtml == "0,00       ") ? "0.00" : tr.Cells[i].InnerHtml;

                                if (tr.Cells[i].InnerHtml == "")
                                {
                                    tr.Cells[i].Attributes.Add("class", "NonLine");
                                }

                                switch (p.PESName)
                                {
                                    case "KPI":
                                    case "SOP":
                                        tr.Attributes.Add("class", "total bold");
                                        tr.Cells[0].InnerHtml = ""; //e.Item.ItemIndex.ToString();

                                        


                                        if (i % 2 == 0)
                                        {
                                            //tr.Cells[i].InnerHtml = "";
                                            tr.Cells[i].Attributes["class"] = "kotak tengah bold";
                                        }
                                        else
                                        {
                                            tr.Cells[i].Attributes.Add("class", "kotak tengah bold");
                                        }
                                        break;
                                    default:
                                        #region kolom pendandaan rebobot
                                        switch (p.Penilaian)
                                        {
                                            case 6:
                                                tr.Cells[1].Attributes["title"] = "Semesteran";
                                                tr.Cells[1].Attributes.Add("class", "kotak rebobot tengah");
                                                rbt.Visible = true;
                                                break;
                                            case 12:
                                                tr.Cells[1].Attributes["title"] = "Tahunan";
                                                tr.Cells[1].Attributes.Add("class", "kotak rebobot2 tengah");
                                                rbt.Visible = true;
                                                break;
                                            default:
                                                rbt.Visible = false;
                                                tr.Cells[1].Attributes["class"] = "kotak tengah bold ";
                                                break;
                                        }
                                        #endregion
                                        if (blnRbt.Length > 0 && (p.PESName != "KPI" || p.PESName != "SOP"))
                                        {
                                            int pos = Array.IndexOf(blnRbt, i.ToString());
                                            if (pos > -1)
                                            {
                                                tr.Cells[(i + int.Parse(blnRbt[pos].ToString()))].Attributes["class"] = "kotak Line3 tengah bold";
                                                tr.Cells[(i + int.Parse(blnRbt[pos].ToString())) + 1].Attributes["class"] = "kotak Line3 tengah bold";
                                            }
                                        }

                                        break;
                                }
                                #endregion
                                break;

                        }
                        #endregion
                    }

                }

                rbt.Visible = (adarebobot > 0) ? true : false;
            }
            #endregion

        }

        private string UserName(string pic)
        {
            string result = pic.ToUpper();
            try
            {
                string strSQL = "SELECT UserName FROM UserAccount Where UserID=(SELECT ID FROM ISO_Users WHERE UserName='" + pic + "' AND RowStatus>-1) AND RowStatus>-1";
                DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = sdr["UserName"].ToString().ToUpper();
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }
    }
}