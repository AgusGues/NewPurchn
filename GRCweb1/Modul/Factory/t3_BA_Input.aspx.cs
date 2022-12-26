using BusinessFacade;
using Cogs;
using Domain;
using Factory;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.Factory
{
    public partial class t3_BA_Input : System.Web.UI.Page
    {
        public int crBAID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTglBA.Text = DateTime.Now.AddDays(-1).ToString("dd MMM yyyy");
                txtdrtanggal.Text = "01-Jan-" + DateTime.Now.Year.ToString().Trim();
                txtsdtanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadDataList();
                Session["ListofBAItems"] = null;
                clearform();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnLampiran);
        }

        protected void LoadDataList()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            //if (Convert.ToInt32(BAID) == 0)
            //    return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select B.ID,A.Keterangan BAType,  A.BANo ,A.BADate,B.AdjustType,I.PartNo,isnull(B.QtyIn,0)QtyIn, " +
                "case isnull(A.Approval,0) when 0 then 'Open' when 1 then 'Head Logistik' when 2 then 'Manager Logistik' when 3 then 'Plant Manager' " +
                "when 4 then 'Spv Accounting' when 5 then 'Manager Accounting'  end Approval, " +
                "isnull(B.QtyOut,0)QtyOut, isnull(B.Keterangan,'')Keterangan " +
                "from T3_BA A inner join T3_BADetail B on A.ID=B.BAID inner join FC_Items I on B.ItemID =I.ID where A.rowstatus>-1  " +
                "and convert(char,BADate,112)>='" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd") + "' and convert(char,BADate,112)<='" + 
                DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd") + "' order by A.BADate desc, A.BANo";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new T3_BADetail
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            BANo = (sdr["BANo"].ToString()),
                            BADate = DateTime.Parse(sdr["BADate"].ToString()),
                            BAType = sdr["BAType"].ToString(),
                            PartNo = (sdr["PartNo"].ToString()),
                            AdjustType = sdr["AdjustType"].ToString(),
                            QtyIn = Convert.ToInt32(sdr["QtyIn"].ToString()),
                            QtyOut = Convert.ToInt32(sdr["QtyOut"].ToString()),
                            Approval = (sdr["Approval"].ToString()),
                            Keterangan = (sdr["Keterangan"].ToString())
                        });
                    }
                }
            }

            GridItemList.DataSource = arrData;
            GridItemList.DataBind();
        }

        private void clearformitems()
        {
            txtPartnoA.Text = string.Empty;
            txtQty1.Text = string.Empty;
        }

        private void clearform()
        {
            Users users = (Users)Session["Users"];
            T3_BADetail BADetail = new T3_BADetail();
            ArrayList arrBAItems = new ArrayList();
            txtPartnoA.Text = string.Empty;
            txtQty1.Text = string.Empty;
            ddlKeterangan.SelectedIndex = 0;
            Session["ListofBAItems"] = null;
            arrBAItems.Add(BADetail);
            GridItem0.DataSource = arrBAItems;
            GridItem0.DataBind();
            LoadListAttachment(crBAID.ToString().Trim());
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            //clearform();
            //btnAddItem.Disabled = false;
            Response.Redirect("T3_BA_Input.aspx", false);
        }

        protected int GetBAID(string BANo)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select ID from t3_BA where RowStatus>-1 and BANo='" + BANo + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["ID"].ToString());
                }
            }
            return result;
        }

        protected string CekStock(string partno, string lokasi)
        {
            string cukup = string.Empty;
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select qty from t3_serah where RowStatus>-1 and itemid in (select id from fc_items where partno='" + partno + "') " +
                "and lokid in (select id from fc_lokasi where lokasi='" + lokasi + "')";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["qty"].ToString());
                }
            }
            if (result >= Convert.ToInt32(txtQty1.Text)) cukup = "cukup";
            return cukup;
        }

        protected void btnTansfer_ServerClick(object sender, EventArgs e)
        {
            if (ddlKeterangan.SelectedItem.Text == "KONSENSI" && txtKeterangan.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "Untuk BA Konsensi harus diisi dengan nomor schedule pengiriman");
                return;
            }
            if (RBOut.Checked == true)
            {
                if (CekStock(txtPartnoA.Text.Trim(), txtLokasi.Text.Trim()) == string.Empty)
                {
                    DisplayAJAXMessage(this, "Stock lokasi tidak mencukupi");
                    return;
                }
            }

            T3_BADetail BADetail = new T3_BADetail();
            ArrayList arrBAItems = new ArrayList();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            Users users = (Users)Session["Users"];
            if (Session["ListofBAItems"] != null)
            {
                arrBAItems = (ArrayList)Session["ListofBAItems"];
                foreach (T3_BADetail cekdjustItems in arrBAItems)
                {
                    if (txtPartnoA.Text.Trim() == cekdjustItems.PartNo)
                    {
                        DisplayAJAXMessage(this, "Partno : " + txtPartnoA.Text.Trim() + " sudah di input");
                        clearformitems();
                        return;
                    }
                }
            }
            BADetail = new T3_BADetail();
            BADetail.ItemID = int.Parse(Session["itemid1"].ToString());
            BADetail.LokID = GetLokasiID(txtLokasi.Text);
            if (RBIn.Checked == true)
            {
                BADetail.QtyIn = int.Parse(txtQty1.Text);
                BADetail.QtyOut = 0;
            }
            else
            {
                BADetail.QtyIn = 0;
                BADetail.QtyOut = int.Parse(txtQty1.Text);
            }
            BADetail.PartNo = txtPartnoA.Text;
            if (RBIn.Checked == true)
                BADetail.AdjustType = "In";
            else
            {
                BADetail.AdjustType = "Out";
            }
            BADetail.CreatedBy = users.UserName;
            BADetail.Apv = 0;
            BADetail.Keterangan = txtKeterangan.Text;
            arrBAItems.Add(BADetail);
            Session["ListofBAItems"] = arrBAItems;
            GridItem0.DataSource = arrBAItems;
            GridItem0.DataBind();
            clearformitems();
        }

        protected int GetLokasiID(string lokasi)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from fc_lokasi where lokasi='" + lokasi + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["ID"].ToString());
                }
            }
            return result;
        }

        protected void txtPartnoA_TextChanged(object sender, EventArgs e)
        {
            FC_Items fC_Items = new FC_Items();
            FC_ItemsFacade fC_ItemsFacade = new FC_ItemsFacade();
            FC_LokasiFacade fclokasifacade = new FC_LokasiFacade();

            if (txtPartnoA.Text.Trim() != string.Empty)
                fC_Items = fC_ItemsFacade.RetrieveByPartNo(txtPartnoA.Text.Trim());
            Session["itemid1"] = fC_Items.ID;
        }

        protected void LoadT3BA(string noba)
        {
            int ID = 0;
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from T3_BA where rowstatus>-1 and BANo='" + noba + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ID = Convert.ToInt32(sdr["ID"].ToString());
                    txtTglBA.Text = DateTime.Parse(sdr["BADate"].ToString()).ToString("dd-MMM-yyyy");
                    ddlKeterangan.ClearSelection();
                    ddlKeterangan.Items.FindByText(sdr["keterangan"].ToString().Trim().ToUpper()).Selected = true;
                }
                LoadT3BADetail(ID);
                LoadListAttachment(crBAID.ToString().Trim());
            }
            else
            {
                DisplayAJAXMessage(this, "Data tidak ditemukan");
                clearform();
            }
        }

        protected void LoadT3BADetail(int BAID)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            //if (Convert.ToInt32(BAID) == 0)
            //    return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select B.ID,A.Keterangan BAType,  A.BANo ,A.BADate,B.AdjustType,I.PartNo,isnull(B.QtyIn,0)QtyIn, case when isnull(A.Approval,0)=0 then '' else '' end Approval, " +
                "isnull(B.QtyOut,0)QtyOut, isnull(B.Keterangan,'')Keterangan " +
                "from T3_BA A inner join T3_BADetail B on A.ID=B.BAID inner join FC_Items I on B.ItemID =I.ID where A.ID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new T3_BADetail
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            BANo = (sdr["BANo"].ToString()),
                            BADate = DateTime.Parse(sdr["BADate"].ToString()),
                            BAType = sdr["BAType"].ToString(),
                            PartNo = (sdr["PartNo"].ToString()),
                            AdjustType = sdr["AdjustType"].ToString(),
                            QtyIn = Convert.ToInt32(sdr["QtyIn"].ToString()),
                            QtyOut = Convert.ToInt32(sdr["QtyOut"].ToString()),
                            Approval = (sdr["Approval"].ToString()),
                            Keterangan = (sdr["Keterangan"].ToString())
                        });
                    }
                }
            }
            GridItem0.DataSource = arrData;
            GridItem0.DataBind();
        }

        protected void btnLampiran_ServerClick(object sender, EventArgs e)
        {
            //UploadFile(this);
        }

       

        protected void btnCari_ServerClick(object sender, EventArgs e)
        {
            LoadT3BA(txtCariBA.Text);
            btnAddItem.Disabled = true;
            //txtNoBA_TextChanged(null, null);
        }

        protected void btnSimpan_ServerClick(object sender, EventArgs e)
        {
            //if (crBAID == 0)
            //{
            //    Users users = (Users)Session["Users"];
            //    ArrayList arrBAItems = new ArrayList();
            //    T3_BA T3BA = new T3_BA();
            //    T3_BAFacade T3BAFacade = new T3_BAFacade();
            //    T3_BADetail T3BADetail = new T3_BADetail();
            //    T3_BADetailFacade T3BADetailFacade = new T3_BADetailFacade();
            //    int intResult = 0;
            //    if (Session["ListofBAItems"] != null)
            //    {
            //        arrBAItems = (ArrayList)Session["ListofBAItems"];
            //        T3BA.CreatedBy = users.UserName;
            //        T3BA.BANo = txtNoBA.Text;
            //        T3BA.Keterangan = ddlKeterangan.SelectedItem.Text;
            //        T3BA.BADate = DateTime.Parse(txtTglBA.Text);

            //        #region Verifikasi Closing Periode

            //        /**
            //     * check closing periode saat ini
            //     * added on 13-08-2014
            //     */
            //        ClosingFacade Closing = new ClosingFacade();
            //        int Tahun = DateTime.Parse(txtTglBA.Text).Year;
            //        int Bulan = DateTime.Parse(txtTglBA.Text).Month;
            //        int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            //        int clsStat = Closing.GetClosingStatus("SystemClosing");
            //        if (status == 1 && clsStat == 1)
            //        {
            //            DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
            //            return;
            //        }

            //        #endregion Verifikasi Closing Periode

            //        T3_BAProcessFacade BAProcessFacade = new T3_BAProcessFacade(T3BA, arrBAItems);
            //        string strError = BAProcessFacade.Insert();
            //        txtNoBA_TextChanged(null, null);
            //        LoadDataList();
            //    }
            //}
            //else
            //{
            //    LoadT3BA(txtNoBA.Text);
            //    LoadDataList();
            //}
        }

        private void clearlokasiawal()
        {
            txtQty1.Text = string.Empty;
            txtPartnoA.Text = string.Empty;
            txtPartnoA.Focus();
        }

        protected void txtQty1_TextChanged(object sender, EventArgs e)
        {
            //if (txtQty1.Text != string.Empty && Convert.ToInt32(txtQty1.Text) > 0)
            //{
            //    btnAddItem.Focus();
            //    //clearform();
            //}
            //else
            //{
            //    txtQty1.Text = string.Empty;
            //    txtQty1.Focus();
            //}
        }

        private void Getfocus()
        {
            if (txtPartnoA.Text == string.Empty)
                txtPartnoA.Focus();
            else
                    if (txtQty1.Text == string.Empty)
                txtQty1.Focus();
            else
                btnAddItem.Focus();
        }

        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtLokasi1_PreRender(object sender, EventArgs e)
        {
            Getfocus();
        }

        protected void txtQty1_PreRender(object sender, EventArgs e)
        {
            Getfocus();
        }

        protected void DatePicker1_SelectionChanged1(object sender, EventArgs e)
        {
            clearform();
            txtPartnoA.Focus();
        }

        protected void ChkHide1_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void LoadListAttachment(string BAID)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(BAID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select *,0 Approval from T3_BAAttachment where rowstatus>-1 and BAID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new T3BA_Attachment
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            BAID = Convert.ToInt32(sdr["BAID"].ToString()),
                            FileName = sdr["FileName"].ToString(),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString())
                        });
                    }
                }
            }
            attachm.DataSource = arrData;
            attachm.DataBind();
        }

        protected void attachm_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihat") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihat");
                Image hps = (Image)e.Item.FindControl("hapus");
                T3BA_Attachment att = (T3BA_Attachment)e.Item.DataItem;
                hps.Visible = (att.Approval < 1) ? true : false;
            }
        }

        protected void attachm_Command(object sender, RepeaterCommandEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "pre":
                        string Nama = e.CommandArgument.ToString();
                        string Nama2 = @"\" + Nama;
                        string dirPath = @"D:\DATA LAMPIRAN PURCHN\t3BA\";
                        string ext = Path.GetExtension(Nama);
                        HttpResponse response = HttpContext.Current.Response;
                        Response.Clear();
                        string excelFilePath = dirPath + Nama;
                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                        if (file.Exists)
                        {
                            response.Clear();
                            response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            response.AddHeader("Content-Length", file.Length.ToString());
                            response.ContentType = "application/octet-stream";
                            response.WriteFile(file.FullName);
                            response.End();
                        }
                        break;

                    case "hps":
                        Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "Update T3_BAAttachment set RowStatus=-1 where ID=" + hps.CssClass;
                        SqlDataReader sdr = zl.Retrieve();
                        break;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Data belum tersimpan atau di approve");
                return;
            }
        }

        protected void RBOut_CheckedChanged(object sender, EventArgs e)
        {
            //if (RBOut.Checked == true)
            //    this.b = System.Drawing.Color.Yellow;
        }

        protected void RBApproval_CheckedChanged(object sender, EventArgs e)
        {
            //if (RBApproval.Checked == true)
            //{
            //    PanelApprove.Visible = true; GridApprove.Visible = true; GridItem.Visible = false;
            //    LoadDataGridViewItem();
            //}
        }

        protected void RBTanggal_CheckedChanged(object sender, EventArgs e)
        {
            //if (RBTanggal.Checked == true)
            //{
            //    PanelApprove.Visible = false; GridApprove.Visible = false; GridItem.Visible = true;
            //    LoadDataGridViewItem();
            //}
        }

        protected void RBBANo_CheckedChanged(object sender, EventArgs e)
        {
            //if (RBNoBA.Checked == true)
            //{
            //    PanelApprove.Visible = false;
            //    PanelApprove.Visible = false;
            //    GridApprove.Visible = false;
            //    GridItem.Visible = true;
            //    LoadDataGridViewItem();
            //}
        }

        protected void txtCariBA_TextChanged(object sender, EventArgs e)
        {
            LoadT3BA(txtCariBA.Text);
        }

        protected void GridItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            T3_BADetail BADetail = new T3_BADetail();
            T3_BADetailFacade BADetailF = new T3_BADetailFacade();
            if (e.CommandName == "hapus")
            {
                if (users.Apv < 1)
                {
                    DisplayAJAXMessage(this, "Hak akses tidak mencukupi");
                    return;
                }
                int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = GridItem.Rows[index];
                //if (row.Cells[9].Text == "Admin")
                //{
                //    BADetail.CreatedBy = users.UserName;
                //    BADetail.ID = int.Parse(row.Cells[0].Text);
                //    //BADetailF.Delete(BADetail);
                //    //LoadDataGridViewItem();
                //}
                //else
                //{
                //    DisplayAJAXMessage(this, "Data tidak bisa di cancel, karena sudah approve accounting");
                //}
            }
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrT3BADetail = (ArrayList)Session["arrT3BADetail"];
            int i = 0;
            //if (ChkAll.Checked == true) //& arrLembur.Count >0
            //{
            //    foreach (T3_BADetail T3BADetail in arrT3BADetail)
            //    {
            //        CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
            //        chk.Checked = true;
            //        i = i + 1;
            //    }
            //}
            //else
            //{
            //    foreach (T3_BADetail T3BADetail in arrT3BADetail)
            //    {
            //        CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
            //        chk.Checked = false;
            //        i = i + 1;
            //    }
            //}
        }

        protected void txtTglBA_TextChanged(object sender, EventArgs e)
        {
        }

        protected void txtBANo_TextChanged(object sender, EventArgs e)
        {
        }

        protected void txtNoBA_TextChanged(object sender, EventArgs e)
        {
            //if (txtNoBA.Text.Trim() != string.Empty)
            //    crBAID = GetBAID(txtNoBA.Text.Trim());
            if (crBAID != 0)
                btnLampiran.Disabled = false;
            else
                btnLampiran.Disabled = true;
        }

        protected void txtdrtanggal_TextChanged(object sender, EventArgs e)
        {
            LoadDataList();
        }

        protected void txtsdtanggal_TextChanged(object sender, EventArgs e)
        {
            LoadDataList();
        }
    }

    public class T3BA_Attachment : GRCBaseDomain
    {
        public string FileName { get; set; }
        public int BAID { get; set; }
        public int Approval { get; set; }
    }
}