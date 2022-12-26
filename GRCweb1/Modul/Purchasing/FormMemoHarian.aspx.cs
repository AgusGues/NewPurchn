using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Domain;
using DataAccessLayer;
using BusinessFacade;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormMemoHarian : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                //LoadPO();
                //LoadSupplier();

                LoadMaterial();
                Session["SchDlv"] = null;
                txtDariTgl.Text = DateTime.Now.AddDays(-3).ToString("dd-MMM-yyyy");
                txtSampaiTgl.Text = DateTime.Now.AddDays(2).ToString("dd-MMM-yyyy");
                string List = (Request.QueryString["list"] != null) ? Request.QueryString["list"].ToString() : "";
                string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputReceipt", "Receipt").Split(',');
                if (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString()) && List == "n")
                {
                    formInput.Visible = true;
                    lstMemo.Visible = false;
                    btnNew.Visible = true;
                    btnSimpan.Visible = true;
                    btnListSch.Visible = true;
                }
                else
                {
                    LoadListMemo();
                    formInput.Visible = false;
                    lstMemo.Visible = true;
                    btnNew.Visible = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
                    btnSimpan.Visible = false;
                    btnListSch.Visible = false;
                }
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadPO()
        {
            ArrayList arrPO = new ArrayList();
            PO po = new PO();
            po.ItemID = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemID", "ReorderPointPOType" + ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString());
            po.SubQuery = "Select * From PoPurchn where ID in(";
            po.Field = "POID";
            po.Criteria = ") order by ID ";
            arrPO = po.Retrieve();
            ddlPoNo.Items.Clear();
            ddlPoNo.Items.Add(new ListItem("--Pilih PO--", "0"));
            foreach (POPurchn p in arrPO)
            {
                ddlPoNo.Items.Add(new ListItem(p.NoPO, p.ID.ToString()));
            }
        }
        private void LoadSupplier()
        {
            ArrayList arrSup = new ArrayList();
            arrSup = new SuppPurchFacade().Retrieve();
            ddlSupplier.Items.Clear();
            ddlSupplier.Items.Add(new ListItem("", "0"));
            foreach (SuppPurch sp in arrSup)
            {
                ddlSupplier.Items.Add(new ListItem(sp.SupplierName, sp.ID.ToString()));
            }
        }
        private void LoadMaterial()
        {
            try
            {
                ArrayList arrData = new ArrayList();
                ArrayList arrInv = new ArrayList();
                string[] ItemID = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemID", "ReorderPointPOType" + ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString()).Split(',');
                string ItemIDs = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemID", "ReorderPointPOType" + ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString());
                InventoryFacade ip = new InventoryFacade();
                ip.Criteriane = " and A.ID in(" + ItemIDs + ")";
                arrInv = ip.Retrieve();
                ddlMaterial.Items.Clear();
                ddlMaterial.Items.Add(new ListItem("", "0"));
                foreach (Inventory inv in arrInv)
                {
                    if (ItemID.Contains(inv.ID.ToString()))
                    {
                        ddlMaterial.Items.Add(new ListItem(inv.ItemCode + " - " + inv.ItemName, inv.ID.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message.ToString());
                return;
            }
        }
        protected void ddlPoNo_Click(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            POPurchnDetail objPO = new POPurchnDetail();
            POPurchn objP = new POPurchn();
            POPurchnDetailFacade PoDetail = new POPurchnDetailFacade();
            POPurchnFacade poFa = new POPurchnFacade();
            objP = poFa.RetrieveByID(int.Parse(ddlPoNo.SelectedValue.ToString()));
            arrData = PoDetail.RetrieveByPOID(int.Parse(ddlPoNo.SelectedValue.ToString()));
            txtQuantity.Text = "0";
            txtSchDate.Text = DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy");
            ddlSupplier.SelectedValue = objP.SupplierID.ToString();
            ddlSupplier.Enabled = false;
            ArrayList arrDPO = new ArrayList();
            PO p = new PO();
            p.ItemID = objP.ID.ToString();
            arrDPO = p.GetDetailPO();
            ddlMaterial.Items.Clear();
            ddlMaterial.DataSource = arrDPO;
            ddlMaterial.DataTextField = "ItemName";
            ddlMaterial.DataValueField = "ItemID";
            ddlMaterial.DataBind();
            p.ItemID = ddlMaterial.SelectedValue;
            p.SubQuery = "";
            p.Field = "(Qty-Rms) as Saldo";
            p.Criteria = " and POID=" + ddlPoNo.SelectedValue.ToString();
            txtOutPO.Text = p.OutStdQty().ToString("###,#00.00");
            txtSchNo.Text = string.Empty;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rpt in lstMemoHarian.Items)
            {
                Repeater pic = (Repeater)rpt.FindControl("lstPOne");
                ((Image)rpt.FindControl("editArm")).Visible = false;
                ((Image)rpt.FindControl("saveArm")).Visible = false;
                ((Image)rpt.FindControl("cancel")).Visible = false;
                ((Image)rpt.FindControl("schEdit")).Visible = false;
                ((Image)rpt.FindControl("schDel")).Visible = false;
                ((TextBox)rpt.FindControl("txtQtyMinta")).Visible = false;
                ((Image)rpt.FindControl("qtyUpd")).Visible = false;
                ((Image)rpt.FindControl("qtySim")).Visible = false;
                ((Image)rpt.FindControl("mmEdit")).Visible = false;
                //((Image)rpt.FindControl("qtySim")).Visible = false;
                //((Image)rpt.FindControl("qtySim")).Visible = false;
                foreach (RepeaterItem rp in pic.Items)
                {
                    ((Image)rp.FindControl("EditPO")).Visible = false;
                    ((Image)rp.FindControl("armAdd")).Visible = false;
                    ((Image)rp.FindControl("armDel")).Visible = false;

                    Repeater pes = (Repeater)rp.FindControl("lstArmada");
                    foreach (RepeaterItem r in pes.Items)
                    {
                        ((Image)r.FindControl("armEdit")).Visible = false;
                        ((Image)r.FindControl("armDelet")).Visible = false;
                        ((Image)r.FindControl("armPrint")).Visible = false;
                    }
                }
            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ParsialDeliveryList.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>LIST PARSIAL DELIVERY</b>";
            Html += "<br>Periode : " + txtDariTgl.Text + " s/d " + txtSampaiTgl.Text + "</b>";
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            SimpanSch();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearItem();
            if (Request.QueryString["list"] != null)
            {
                if (Request.QueryString["list"].ToString() == "y")
                {
                    Response.Redirect("FormMemoharian.aspx?list=n");
                }
            }
        }
        protected void txt_onChange(object sender, EventArgs e)
        {
            try
            {
                string Kapasitas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read(ddlMaterial.SelectedValue.ToString(), "KapasitasHiblow");
                decimal kapasitas = 0;
                decimal jml = 0;
                decimal.TryParse(Kapasitas, out kapasitas);
                decimal.TryParse(txtQuantity.Text, out jml);

                decimal qty = jml * kapasitas;
                txtOutPO.Text = qty.ToString("N0");
            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message);
                return;
            }
        }
        protected void btnListSch_Click(object sender, EventArgs e)
        {
            if (lstMemo.Visible == false)
            {
                LoadListMemo();
                lstMemo.Visible = true;
                formInput.Visible = false;
                btnSimpan.Enabled = false;
                btnNew.Enabled = false;
            }
            else
            {
                string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputReceipt", "Receipt").Split(',');
                if (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString()))
                {
                    lstMemo.Visible = false;
                    formInput.Visible = true;
                    btnSimpan.Enabled = true;
                    btnNew.Enabled = true;
                }
                else
                {
                    LoadListMemo();
                    lstMemo.Visible = true;
                    formInput.Visible = false;
                    btnSimpan.Enabled = false;
                    btnNew.Enabled = false;
                }
            }

        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            PO p = new PO();
            ArrayList arrLst = new ArrayList();
            p.SubQuery = "select * from (";
            p.Field = " *,(select SupplierName from SuppPurch where ID=SupplierID)SupplierName,(Select NoPO from POPurchn where ID=POID)PONo ";
            p.Criteria = "where RowStatus >-1 and Status=0) as x";
            p.Criteria += " where x. " + ddlCari.SelectedValue.ToString() + " like '%" + txtCari.Text + "%'";
            arrLst = p.ListMemoHarian();
            lstMemoHarian.DataSource = arrLst;
            lstMemoHarian.DataBind();
        }
        private void ClearItem()
        {
            //ddlPoNo.SelectedIndex = 0;
            //ddlSupplier.SelectedIndex = 0;
            ddlMaterial.SelectedIndex = 0;
            txtOutPO.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtSchDate.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            ArrayList arrData = new ArrayList();
            lstSch.DataSource = arrData;
            lstSch.DataBind();
            Session.Remove("SchDlv");
        }
        protected void lstSch_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Image imgEdt = (Image)e.Item.FindControl("imgEdit");
            Image imgDel = (Image)e.Item.FindControl("imgDel");
            POPurchn sch = (POPurchn)e.Item.DataItem;
            imgEdt.Visible = (sch.ID == 0) ? false : true;

        }
        protected void lstSch_Command(object sender, RepeaterCommandEventArgs e)
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            ArrayList arrSch = (ArrayList)Session["SchDlv"];

            switch (e.CommandName)
            {
                case "delx":
                    if (txtSchNo.Text == string.Empty && ID == 0)
                    {
                        arrSch.RemoveAt(e.Item.ItemIndex);
                        lstSch.DataSource = arrSch;
                        lstSch.DataBind();
                    }
                    break;
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ArrayList arrSch = new ArrayList();
            POPurchn objSh = new POPurchn();
            if (int.Parse(txtQuantity.Text) <= 0)
            {
                DisplayAJAXMessage(this, "Quantiry Delivery harus diisi");
                txtQuantity.Focus();
                return;
            }
            double tdays = 0;
            tdays = Math.Round((DateTime.Parse(txtSchDate.Text.ToString()) - DateTime.Now).TotalDays, 0);
            if ((tdays < -3) || txtSchDate.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Schedule Tanggal Kedatangan tidak di ijinkan/ ( tidak boleh kosong)");
                return;
            }

            arrSch = (Session["SchDlv"] != null) ? (ArrayList)Session["SchDlv"] : new ArrayList();
            objSh.NoPO = string.Empty;// ddlPoNo.SelectedItem.ToString();
            objSh.POID = 0;// int.Parse(ddlPoNo.SelectedValue);
            objSh.SupplierID = 0;// int.Parse(ddlSupplier.SelectedValue);
            objSh.SupplierName = "";// ddlSupplier.SelectedItem.ToString();
            objSh.Qty = decimal.Parse(txtQuantity.Text);
            objSh.DlvDate = DateTime.Parse(txtSchDate.Text);
            objSh.ItemName = ddlMaterial.SelectedItem.ToString();
            objSh.ItemID = int.Parse(ddlMaterial.SelectedValue);
            objSh.Keterangan = txtKeterangan.Text;
            objSh.EstQty = decimal.Parse(txtOutPO.Text);
            arrSch.Add(objSh);
            Session["SchDlv"] = arrSch;
            lstSch.DataSource = arrSch;
            lstSch.DataBind();
            txtQuantity.Text = "0";
            txtOutPO.Text = "";
            txtKeterangan.Text = string.Empty;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {


        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadListMemo();
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private void LoadListMemo()
        {
            PO p = new PO();
            ArrayList arrLst = new ArrayList();
            DateTime prevday = DateTime.Now.AddDays(-4);
            DayOfWeek nHari = prevday.DayOfWeek;
            string DariTgl = DateTime.Parse(txtDariTgl.Text).ToString("yyyyMMdd");
            string SampaiTgl = DateTime.Parse(txtSampaiTgl.Text).ToString("yyyyMMdd");
            string TglKemarin = (nHari == DayOfWeek.Saturday) ? DateTime.Now.AddDays(-5).ToString("yyyyMMdd") : DateTime.Now.AddDays(-4).ToString("yyyyMMdd");
            p.Field = "*,(select SupplierName from SuppPurch where ID=SupplierID)SupplierName,(Select NoPO from POPurchn where ID=POID)PONo ";
            p.Criteria = "where RowStatus >-1 and Status=0 and Convert(Char,DvlDate,112) between'" + DariTgl + "' and '" + SampaiTgl + "'";
            p.Criteria += "order by CAST(dvlDate as datetime) desc, ID desc";
            arrLst = p.ListMemoHarian();
            lstMemoHarian.DataSource = arrLst;
            lstMemoHarian.DataBind();
        }
        protected void lstMemoHarian_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            int DeptID = ((Users)Session["Users"]).DeptID;
            POPurchn po = (POPurchn)e.Item.DataItem;
            Image del = (Image)e.Item.FindControl("schDel");
            Image edArm = (Image)e.Item.FindControl("editArm");
            Image svArm = (Image)e.Item.FindControl("saveArm");
            Image edt = (Image)e.Item.FindControl("schEdit");
            Image sch = (Image)e.Item.FindControl("mmEdit");
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("xx");
            Repeater rpt = (Repeater)e.Item.FindControl("lstPOne");
            string[] Armada = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputArmada", "MemoHarian").Split(',');
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDeptID", "PO").Split(',');
            string[] SchEdit = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EditSchedule", "MemoHarian").Split(',');
            edArm.Visible = (Armada.Contains(DeptID.ToString())) ? false : false;
            del.Visible = (arrDept.Contains(DeptID.ToString())) ? false : false;
            edt.Visible = (arrDept.Contains(DeptID.ToString())) ? true : false;
            edt.Attributes.Add("onclick", "openWindow(" + po.ID.ToString() + ")");
            sch.Attributes.Add("onclick", "updateSch(" + po.ID.ToString() + ")");
            sch.Visible = (SchEdit.Contains(DeptID.ToString())) ? true : false;
            #region Tampilkan data PO delivery nya
            PO p = new PO();
            p.Criteria = " Where MP.RowStatus >-1 and SchID=" + po.ID + " order by MP.ID";
            arrData = p.ListSCHPO(); ;
            rpt.DataSource = arrData;
            rpt.DataBind();
            decimal jml_mobil = 0;
            foreach (POPurchn pp in arrData)
            {
                jml_mobil += pp.Qty;

            }
            if (jml_mobil == po.Qty)
            {
                del.Visible = false;
                edt.Visible = false;
            }
            if (arrData.Count == 0)
            {
                tr.Style.Add(HtmlTextWriterStyle.Color, "Blue");
            }
            #endregion
        }
        protected void lstMemoHarian_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            POPurchn sc = (POPurchn)e.Item.DataItem;
            string cmd = e.CommandName;
            int ID = int.Parse(e.CommandArgument.ToString());
            Panel p1 = (Panel)e.Item.FindControl("noArmada");
            Panel p2 = (Panel)e.Item.FindControl("Armada");
            CheckBoxList ddl = (CheckBoxList)e.Item.FindControl("ddlArmada");
            string PlantCode = ((Users)Session["Users"]).KodeLokasi.ToString();
            switch (cmd)
            {
                case "edit":
                    #region Proses Penambahan PO
                    if (Session["SchDel"] != null)
                    {
                        ArrayList arrDataPO = (ArrayList)Session["SchDel"];
                        PO p = new PO();
                        POPurchn s = new POPurchn();
                        int n = 0;
                        p.Tanggal = DateTime.Now.ToString("yyyyMMdd");
                        p.Criteria = "SchID,POID,ItemID,SupplierID,Qty,EstQty,CreatedBy,Keterangan,DocNo";
                        foreach (POPurchn po in arrDataPO)
                        {
                            //p.Criteria = " AND SCHID=" + po.SchID;
                            //p.Nomor = p.UrutanLoco();
                            s.SchID = po.SchID;
                            s.POID = po.POID;
                            s.ItemID = po.ItemID;
                            s.SupplierID = po.SupplierID;
                            s.Qty = po.Qty;
                            s.EstQty = po.EstQty;
                            s.Keterangan = po.Keterangan;
                            s.CreatedBy = po.CreatedBy;
                            s.DocNo = "P" + p.GetNo();
                            int result = p.ProcessData(s, "spMemoHarian_PO_Insert");
                            if (po.Qty > 0)
                            {
                                //Otomatis Insert table amada jika PO FRANCO
                                if (po.DocumentNo.ToUpper() == "FRAN" ||
                                    po.DocumentNo.ToUpper() == "FRANCO" ||
                                    po.DocumentNo.ToUpper() == "FRANKO")
                                {
                                    POPurchn pps = (POPurchn)p.DetailSCH(po.SchID);
                                    ArrayList arrD = new ArrayList();
                                    for (int i = 1; i <= po.Qty; i++)
                                    {
                                        POPurchn pop = new POPurchn();
                                        pop.SchPOID = result;
                                        pop.SchID = po.SchID;
                                        //pop.ItemID = po.ItemID;
                                        pop.DoNum = "P" + PlantCode.ToString() + DateTime.Now.Year.ToString().Substring(2, 2) +
                                            DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" +
                                            (p.getDONum() + 1).ToString().PadLeft(4, '0');
                                        pop.SchDate = pps.SchDate;
                                        pop.ArmadaID = 0;
                                        pop.NoPol = "FRANCO";
                                        pop.RowStatus = 0;
                                        pop.Unloading = ((Users)Session["Users"]).UnitKerjaID;
                                        pop.Ritase = i;
                                        pop.Driver = "";
                                        pop.Cetak = 0;
                                        pop.Jam = "08:00";
                                        pop.CreatedBy = ((Users)Session["Users"]).UserName;
                                        pop.CreatedTime = DateTime.Now;
                                        arrD.Add(pop);
                                    }
                                    int rst = FrancoArmada(arrD);
                                }
                            }
                            Session["SchDel"] = null;
                            LoadListMemo();
                        }

                    }
                    #endregion
                    break;
                case "del":

                    break;
                case "add":
                    p1.Visible = false;
                    p2.Visible = true;
                    LoadKendaraan(ddl);
                    break;
                case "save":
                    #region Simpan data Armada
                    ArrayList arrData = new ArrayList();
                    POPurchn ch = new POPurchn();
                    ch.ID = ID;
                    string ListArmada = string.Empty;
                    foreach (ListItem ls in ddl.Items)
                    {
                        if (ls.Selected)
                        {
                            ListArmada += ls.Text + ", ";
                        }
                    }
                    ch.Armada = (ListArmada != string.Empty) ? ListArmada.Substring(0, ListArmada.Length - 2) : string.Empty;
                    arrData.Add(ch);
                    PO pp = new PO();
                    pp.Criteria = "ID,Armada";
                    int results = pp.ProcessData(ch, "spMemoHarian_UpdateArmada");

                    LoadListMemo();
                    if (results > 0)
                    {
                        p1.Visible = true;
                        p2.Visible = false;
                    }
                    break;
                #endregion
                case "batal":
                    p1.Visible = true;
                    p2.Visible = false;
                    break;
                case "updQty":
                    ((Image)e.Item.FindControl("qtySim")).Visible = true;
                    ((Image)e.Item.FindControl("qtyUpd")).Visible = false;
                    break;
                case "simQty":
                    ((Image)e.Item.FindControl("qtyUpd")).Visible = true;
                    ((Image)e.Item.FindControl("qtySim")).Visible = false;
                    break;
                case "scEdit":
                    string schID = e.CommandName.ToString();
                    break;
            }

        }
        private int FrancoArmada(ArrayList arrData)
        {
            string PlantCode = ((Users)Session["Users"]).KodeLokasi.ToString();
            PO p = new PO();
            int result = 0;
            foreach (POPurchn po in arrData)
            {
                POPurchn arm = new POPurchn();
                arm.SchID = po.SchID;
                arm.SchPOID = po.SchPOID;
                arm.NoPol = po.NoPol;
                arm.DoNum = po.DoNum;// "P" + PlantCode.ToString() + 
                                     //DateTime.Now.Year.ToString().Substring(2, 2) + 
                                     //DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" +
                                     //(p.getDONum() + 1).ToString().PadLeft(6, '0');
                arm.ArmadaID = po.ArmadaID;
                arm.Unloading = po.Unloading;
                arm.CreatedBy = po.CreatedBy;
                arm.CreatedTime = po.CreatedTime;
                arm.Ritase = po.Ritase;
                arm.Driver = po.Driver.ToString();
                arm.CoDriver = "";// po.CoDriver.ToString();
                arm.RowStatus = 0;
                arm.Jam = po.Jam.ToString();
                arm.Flag = 0;
                arm.SchDate = po.SchDate;
                arm.Cetak = arm.Cetak;
                p.Criteria = "SchID,SchPOID,SchDate,NoPol,DoNum,ArmadaID,Unloading,Ritase,Driver,CoDriver,RowStatus,Flag,CreatedBy,CreatedTime,Jam,Cetak";
                p.Pilihan = "Insert";
                p.TableName = "MemoHarian_Armada";
                string rst = p.CreateProcedure(arm, "spMemoHarian_Armada_Insert");
                if (rst == string.Empty)
                {
                    result = p.ProcessData(arm, "spMemoHarian_Armada_Insert");

                }
            }
            return result;
        }
        private void SimpanSch()
        {
            ArrayList arrData = (ArrayList)Session["SchDlv"];
            POPurchn s = new POPurchn();
            PO p = new PO();

            p.Tanggal = DateTime.Now.ToString("yyyyMMdd");
            p.Nomor = 0;
            p.Criteria = "POID,SupplierID,ItemID,Qty,DlvDate,Armada,Keterangan,CreatedBy,SchNo,EstQty";
            foreach (POPurchn sch in arrData)
            {
                s.POID = sch.POID;
                s.SupplierID = sch.SupplierID;
                s.ItemID = sch.ItemID;
                s.Qty = sch.Qty;
                s.DlvDate = sch.DlvDate;
                s.Armada = string.Empty;
                s.Keterangan = sch.Keterangan;
                s.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                s.EstQty = sch.EstQty;
                s.SchNo = p.GetNo();
                int result = p.ProcessData(s, "spMemoHarian_Insert");
                if (result > 0)
                {
                    txtSchNo.Text = s.SchNo;
                    ClearItem();
                }
            }

        }
        private void LoadKendaraan(CheckBoxList ddl)
        {
            ArrayList arrKend = new ArrayList();
            arrKend = new PO().ListKendaraan();
            ddl.Items.Clear();
            //ddl.Items.Add(new ListItem("--Pilih --", "0"));
            foreach (POPurchn np in arrKend)
            {
                ddl.Items.Add(new ListItem(np.Keterangan, np.NoPol));

            }
        }
        protected void lstPOne_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            POPurchn po = (POPurchn)e.Item.DataItem;
            Image edtPO = (Image)e.Item.FindControl("EditPO");
            Image edt = (Image)e.Item.FindControl("armAdd");
            Image del = (Image)e.Item.FindControl("armDel");
            string[] Armada = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputArmada", "MemoHarian").Split(',');
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDeptID", "PO").Split(',');
            edt.Visible = (Armada.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            string querystr = po.ID.ToString();// +"_" + po.SupplierName.ToString().Replace(".", "_").Replace(" ", "_");
            edt.Attributes.Add("onclick", "openWindow2(" + querystr.ToString() + ")");
            del.Visible = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            edtPO.Attributes.Add("onclick", "updateDO(" + querystr.ToString() + ")");
            edtPO.Visible = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            /*----------------------------------------------------*/
            Repeater lstArm = (Repeater)e.Item.FindControl("lstArmada");
            PO p = new PO();
            p.Criteria = " and schPOID=" + po.ID.ToString();
            p.Criteria += " and NoPOL!='FRANCO'";
            p.Criteria += " order by DoNum";
            p.Field = "Armada";
            arrData = p.ListArmada();
            del.Attributes.Add("onclick", "return HapusData()");
            //edtPO.Attributes.Add("onclick", "openWindow(" + po.ID.ToString() + ")");
            decimal jmlMobil = 0;
            //foreach (POPurchn pop in arrData)
            //{
            //    jmlMobil += pop.Qty;
            //}
            jmlMobil = arrData.Count;
            if (jmlMobil >= po.Qty)
            {
                edt.Visible = false;
                del.Visible = false;
            }
            del.Visible = (jmlMobil > 0 || !arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? false : true;
            //string 
            string[] POType = po.Delivery.ToUpper().Split(' ');
            if (POType[0] == "FRANCO" || POType[0] == "FRANKO")
            {
                arrData = new ArrayList();
                edt.Visible = false;
                del.Visible = false;
            }
            lstArm.DataSource = arrData;
            lstArm.DataBind();
        }
        protected void lstPOne_Command(object sender, RepeaterCommandEventArgs e)
        {
            string PlantCode = ((Users)Session["Users"]).KodeLokasi.ToString();
            switch (e.CommandName)
            {
                case "addArm":
                    #region Tambah Armada
                    if (Session["schArmada"] != null)
                    {
                        ArrayList arrData = (ArrayList)Session["schArmada"];

                        PO p = new PO();
                        int result = 0;
                        foreach (POPurchn po in arrData)
                        {
                            POPurchn arm = new POPurchn();
                            arm.SchID = po.SchID;
                            arm.SchPOID = po.SchPOID;
                            arm.NoPol = po.NoPol;
                            arm.DoNum = PlantCode.ToString() + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" +
                                   (p.getDONum() + 1).ToString().PadLeft(4, '0');
                            arm.ArmadaID = po.ArmadaID;
                            arm.Unloading = po.Unloading;
                            arm.CreatedBy = po.CreatedBy;
                            arm.CreatedTime = po.CreatedTime;
                            arm.Ritase = po.Ritase;
                            arm.Driver = po.Driver.ToString();
                            arm.CoDriver = po.CoDriver.ToString();
                            arm.RowStatus = 0;
                            arm.Jam = po.Jam.ToString();
                            arm.Flag = 0;
                            arm.SchDate = po.SchDate;
                            arm.Cetak = arm.Cetak;
                            p.Criteria = "SchID,SchPOID,SchDate,NoPol,DoNum,ArmadaID,Unloading,Ritase,Driver,CoDriver,RowStatus,Flag,CreatedBy,CreatedTime,Jam,Cetak";
                            p.Pilihan = "Insert";
                            p.TableName = "MemoHarian_Armada";
                            string rst = p.CreateProcedure(arm, "spMemoHarian_Armada_Insert");
                            if (rst == string.Empty)
                            {
                                result = p.ProcessData(arm, "spMemoHarian_Armada_Insert");

                            }
                        }
                        if (result > 0)
                        {
                            LoadListMemo();
                        }
                        Session.Remove("SchArmada");
                    }
                    break;
                #endregion
                case "delArm":
                    if (Session["AlasanBatal"] != null)
                    {
                        POPurchn po = new POPurchn();
                        po.ID = int.Parse(e.CommandArgument.ToString());
                        po.Keterangan = Session["AlasanBatal"].ToString();
                        po.RowStatus = -1;
                        po.LastModifiedBy = ((Users)Session["Users"]).UserName;
                        po.LastModifiedTime = DateTime.Now;
                        PO p = new PO();
                        p.Criteria = "ID,Keterangan,RowStatus,LastModifiedBy,LastModifiedTime";
                        p.Pilihan = "Update";
                        p.TableName = "MemoHarian_PO";
                        string rest = p.CreateProcedure(po, "spMemoHarian_PO_Delete");
                        if (rest == string.Empty)
                        {
                            int rst = p.ProcessData(po, "spMemoHarian_PO_Delete");
                            if (rst > 0)
                            {
                                LoadListMemo();
                                Session["AlasanBatal"] = null;
                            }
                        }
                    }
                    break;
                case "editPO":
                    #region Edit PO
                    if (Session["SchDelEd"] != null)
                    {
                        ArrayList arrDataPO = (ArrayList)Session["SchDelEd"];
                        PO p = new PO();
                        POPurchn s = new POPurchn();
                        p.Criteria = "ID,POID,ItemID,SupplierID,Qty,EstQty,CreatedBy,Keterangan";
                        foreach (POPurchn po in arrDataPO)
                        {
                            s.ID = po.ID;
                            s.POID = po.POID;
                            s.ItemID = po.ItemID;
                            s.SupplierID = po.SupplierID;
                            s.Qty = po.Qty;
                            s.EstQty = po.EstQty;
                            s.Keterangan = po.Keterangan;
                            s.CreatedBy = po.CreatedBy;
                            int result = p.ProcessData(s, "spMemoHarian_PO_Update");
                            if (result > -1)
                            {
                                //hapus do yng lalu yng flag nya 0
                                int rs = 1;// HapusDOFranco(po.ID);
                                if (rs > 0)
                                {
                                    if (po.DocumentNo.ToUpper() == "FRANCO" || po.DocumentNo.ToUpper() == "FRANKO")
                                    {
                                        POPurchn pps = (POPurchn)p.DetailSCH(po.ID);
                                        ArrayList arrD = new ArrayList();
                                        p.Tanggal = DateTime.Now.ToString("yyyyMMdd");
                                        for (int i = 1; i <= po.Qty; i++)
                                        {
                                            POPurchn pop = new POPurchn();
                                            pop.SchPOID = po.ID;
                                            pop.SchID = po.ID;
                                            //pop.ItemID = po.ItemID;
                                            pop.DoNum = "P" + PlantCode.ToString() + DateTime.Now.Year.ToString().Substring(2, 2) +
                                                DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" +
                                                (p.getDONum() + 1).ToString().PadLeft(4, '0');
                                            pop.SchDate = pps.SchDate;
                                            pop.ArmadaID = 0;
                                            pop.NoPol = "FRANCO";
                                            pop.RowStatus = 0;
                                            pop.Unloading = ((Users)Session["Users"]).UnitKerjaID;
                                            pop.Ritase = i;
                                            pop.Driver = "";
                                            pop.Cetak = 0;
                                            pop.Jam = "08:00";
                                            pop.CreatedBy = ((Users)Session["Users"]).UserName;
                                            pop.CreatedTime = DateTime.Now;
                                            arrD.Add(pop);
                                        }
                                        int rst = FrancoArmada(arrD);
                                    }
                                }
                            }
                            Session["SchDelEd"] = null;
                            LoadListMemo();
                        }
                    }
                    #endregion
                    break;
            }
        }
        private int HapusDOFranco(int ID)
        {
            PO p = new PO();
            return p.HapusData(ID);
        }
        protected void lstArmada_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputArmada", "MemoHarian").Split(',');
            int apv = ((Users)Session["Users"]).Apv;
            Image prn = (Image)e.Item.FindControl("armPrint");
            Image del = (Image)e.Item.FindControl("armDelet");
            Image edt = (Image)e.Item.FindControl("armEdit");
            //del.Visible = false;
            POPurchn pArm = (POPurchn)e.Item.DataItem;
            prn.Visible = (pArm.Cetak > 0 || !arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? false : true;
            del.Visible = (pArm.Cetak > 0 || !arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? false : true;
            edt.Visible = ((apv == 0 && pArm.Cetak > 0) || !arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? false : true;
            edt.Attributes.Add("onclick", "UpdateMobil(" + pArm.ID.ToString() + "," + pArm.POID.ToString() + ")");
            del.Attributes.Add("onclick", "return HapusData()");
        }
        protected void lstArmada_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            PO p = new PO();
            switch (e.CommandName)
            {
                case "print":
                    p.Pilihan = "Lokasi";
                    p.SubQuery = ",(Select Lokasi from Company Where DepoID=ma.unloading) as Lokasi";
                    p.Criteria = " and ma.ID=" + e.CommandArgument.ToString();
                    POPurchn pp = p.DetailDO();
                    Session["AtasNama"] = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AttnDO", "MemoHarian").ToString();
                    Session["Bongkar"] = pp.Remark.ToString();
                    Session["Contact"] = pp.SupplierName.ToString();
                    Session["DONum"] = pp.DoNum.ToString();
                    p.SubQuery = "";

                    //penambahan agus 20-10-2022
                    Session["NomorDokumen"] = p.dokNO();
                    //penambahan agus 20-10-2022

                    Session["Query"] = p.PrintDO();
                    Cetak(this);
                    break;
                case "deldlv":
                    if (Session["AlasanBatal"] != null)
                    {
                        string idx = e.CommandArgument.ToString();
                        PO pox = new PO();
                        POPurchn ppox = new POPurchn();
                        ppox.ID = int.Parse(idx);
                        ppox.Keterangan = Session["AlasanBatal"].ToString();
                        ppox.RowStatus = -1;
                        ppox.LastModifiedBy = ((Users)Session["Users"]).UserName.ToString();
                        pox.Criteria = "ID,RowStatus,LastModifiedBy,Keterangan";
                        pox.Pilihan = "Update";
                        pox.TableName = "MemoHarian_Armada";
                        if (pox.CreateProcedure(ppox, "spMemoHarian_Armada_Del") == string.Empty)
                        {
                            if (pox.ProcessData(ppox, "spMemoHarian_Armada_Del") > 0)
                            {
                                LoadListMemo();
                            }

                        }
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Alasan Cancel / Batal tidak boleh kosong");
                        return;
                    }
                    break;
                case "editarm":
                    if (Session["UpdateArmada"] != null)
                    {
                        ArrayList arrData = (ArrayList)Session["UpdateArmada"];
                        PO po = new PO();
                        POPurchn ppo = new POPurchn();
                        po.Criteria = "ID,ArmadaID,NoPol,Driver,CoDriver,Jam,Keterangan,LastModifiedBy";
                        foreach (POPurchn pps in arrData)
                        {
                            ppo.ID = pps.ID;
                            ppo.ArmadaID = pps.ArmadaID;
                            ppo.NoPol = pps.NoPol;
                            ppo.Driver = pps.Driver;
                            ppo.CoDriver = pps.CoDriver;
                            ppo.Jam = pps.Jam;
                            ppo.Keterangan = pps.Keterangan;
                            ppo.LastModifiedBy = ((Users)Session["Users"]).UserName;
                            po.Pilihan = "Update";
                            po.TableName = "MemoHarian_Armada";
                            if (po.CreateProcedure(ppo, "spMemoHarian_Armada_Update") == string.Empty)
                            {
                                if (po.ProcessData(ppo, "spMemoHarian_Armada_Update") > 0)
                                {
                                    LoadListMemo();
                                }

                            }
                        }
                    }
                    break;
            }
        }
        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report.aspx?IdReport=DO', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

    }
    public class PO
    {
        private ArrayList arrD = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private POPurchn alk = new POPurchn();
        private string field = "*";
        public string Criteria { get; set; }
        public string Pilihan { get; set; }
        public string TableName { get; set; }
        public string ItemID { get; set; }
        public string SubQuery { get; set; }
        public string Field { get { return field; } set { field = value; } }
        public string Tanggal { get; set; }
        public int Nomor { get; set; }
        public PO()
        {

        }
        private string Query()
        {
            string strQuery = "select ID,NoPO,POPurchnDate,SupplierID,Delivery,ApproveDate2 " +
                              "from POPurchn where ID in(select POID from POPurchnDetail where ItemID in(" + this.ItemID + ")" +
                              " po.Qty >(select ISNULL(sum(rd.Quantity),0) from ReceiptDetail rd where rd.ItemID= po.ItemID and " +
                              " rd.PODetailID=po.ID and rd.RowStatus >-1 group by rd.ItemID,rd.PODetailID ) " +
                              "and Status between 0 and 3) and Approval=2 and Status >-1 " + this.Criteria + " order by NoPO ";
            return strQuery;
        }
        private string OutStdPO()
        {
            string strQuery = this.SubQuery + "SELECT " + this.Field + " from ( " +
                            "select Po.ID,Po.POID,Po.ItemID,Po.ItemTypeID,Po.Qty,(select ISNULL(Sum(rd.Quantity),0) from ReceiptDetail rd " +
                            "where /*rd.ItemID=Po.ItemID and*/ rd.PODetailID=Po.ID group by /*rd.ItemID,*/rd.PODetailID)Rms from POPurchnDetail Po " +
                            "where Po.Status >-1 and Po.ItemID in(" + this.ItemID + "))" +
                            "as x where x.Qty>x.Rms " + this.Criteria;
            return strQuery;
        }
        public ArrayList Retrieve()
        {
            ArrayList arrData = new ArrayList();
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(this.OutStdPO());
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new POPurchn
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        NoPO = sdr["NoPO"].ToString(),
                        SupplierID = Convert.ToInt32(sdr["SupplierID"].ToString()),
                        POPurchnDate = Convert.ToDateTime(sdr["PoPurchnDate"].ToString()),
                        Delivery = sdr["Delivery"].ToString()
                    });
                }
            }
            return arrData;
        }
        public ArrayList GetDetailPO()
        {
            ArrayList arrData = new ArrayList();
            string strQuery = "Select *,((select dbo.ItemCodeInv(ItemID,ItemTypeID))+' - '+(select dbo.ItemNameInv(ItemID,ItemTypeID)))ItemName " +
                              "from POPurchnDetail where POID=" + this.ItemID + " and Status >-1";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strQuery);
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new POPurchnDetail
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        ItemID = Convert.ToInt32(sdr["ItemID"].ToString()),
                        ItemName = sdr["ItemName"].ToString()
                    });
                }
            }
            return arrData;
        }
        private string QueryMemo()
        {
            string query = this.SubQuery + "Select " + this.Field + ",(Select dbo.ItemCodeInv(ItemID,1))ItemCode,(Select dbo.ItemNameInv(ItemID,1))ItemName from MemoHarian " +
                         this.Criteria;
            return query;
        }
        public int GetIDMemo()
        {
            int result = 0;
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(this.QueryMemo());
            return result;
        }
        public ArrayList ListMemoHarian()
        {
            arrD = new ArrayList();
            string strSQL = this.QueryMemo();
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(this.QueryMemo());
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrD.Add(new POPurchn
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        POID = Convert.ToInt32(sdr["POID"].ToString()),
                        NoPO = sdr["PONo"].ToString(),
                        SupplierName = sdr["SupplierName"].ToString(),
                        ItemCode = sdr["ItemCode"].ToString(),
                        ItemName = sdr["ItemName"].ToString(),
                        Qty = Convert.ToDecimal(sdr["Qty"].ToString()),
                        DlvDate = Convert.ToDateTime(sdr["DvlDate"].ToString()),
                        Keterangan = sdr["Keterangan"].ToString(),
                        Armada = sdr["Armada"].ToString(),
                        EstQty = Convert.ToDecimal(sdr["EstimasiQty"].ToString())
                    });
                }
            }
            return arrD;
        }

        //penambahan agus 20-10-2022
        public string dokNO()
        {
            string hasil = string.Empty;
            string Plant = ((Users)System.Web.HttpContext.Current.Session["Users"]).UnitKerjaID.ToString();
            string strSQL = "select NomorDokumen from NoDokumenDO where PlantID= '" + Plant + "' ";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSQL);

            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasil = sdr["NomorDokumen"].ToString();
                }
            }
            return hasil;
        }
        //penambahan agus 20-10-2022

        public int UrutanLoco()
        {
            string strSQL = "SELECT Count(ID)ID FROM MemoHarian_PO Where RowStatus>-1 " + Criteria;
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSQL);
            int ID = 0;
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ID = Convert.ToInt32(sdr["ID"].ToString());
                }
            }
            return ID;
        }
        public string GetNo()
        {
            string strQuery = "Select COUNT(ID) ID From MemoHarian WHERE Year(CreatedTime)=" + this.Tanggal.Substring(0, 4) + " Order By ID desc";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strQuery);
            int ID = 0;
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ID = Convert.ToInt32(sdr["ID"].ToString());
                }
            }
            if (ID == 0)
            {
                ID = 1;
            }
            else
            {
                ID = ID + 1;
            }
            string PlantCode = ((Users)System.Web.HttpContext.Current.Session["Users"]).KodeLokasi.ToString();
            return PlantCode + this.Tanggal.Substring(2, 4) + "-" + ID.ToString().PadLeft(4, '0');
        }
        public int ProcessData(object arrAL, string spName)
        {
            try
            {
                alk = (POPurchn)arrAL;
                string[] arrCriteria = this.Criteria.Split(',');
                PropertyInfo[] data = alk.GetType().GetProperties();
                DataAccess da = new DataAccess(Global.ConnectionString());
                var equ = new List<string>();
                sqlListParam = new List<SqlParameter>();
                foreach (PropertyInfo items in data)
                {
                    if (items.GetValue(alk, null) != null && arrCriteria.Contains(items.Name))
                    {
                        sqlListParam.Add(new SqlParameter("@" + items.Name.ToString(), items.GetValue(alk, null)));
                    }
                }
                int result = da.ProcessData(sqlListParam, spName);
                string err = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return -1;
            }
        }
        public ArrayList ListKendaraan()
        {
            arrD = new ArrayList();
            string strQuery = "select * from MTC_NamaArmada where NamaKendaraan like 'HI-%' or NamaKendaraan like 'HIBLOW%'";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strQuery);
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrD.Add(new POPurchn
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        NoPol = sdr["NoPol"].ToString(),
                        Keterangan = sdr["NamaKendaraan"].ToString()
                    });
                }
            }
            return arrD;
        }
        public decimal OutStdQty()
        {
            decimal qty = 0;
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(this.OutStdPO());
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    qty = Convert.ToDecimal(sdr["Saldo"].ToString());
                }
            }
            return qty;
        }
        public ArrayList ListSCHPO()
        {
            arrD = new ArrayList();
            string strsql = "select MP.*,PO.NoPO,(select dbo.ItemNameInv(MP.ItemID,1))ItemName,SP.SupplierName,PO.Delivery  from MemoHarian_PO as MP " +
                            "LEFT JOIN POPurchn as PO ON PO.ID=MP.POID " +
                            "LEFT JOIN SuppPurch SP on SP.ID=MP.SupplierID" + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrD.Add(new POPurchn
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        NoPO = sdr["NoPO"].ToString(),
                        SupplierName = sdr["SupplierName"].ToString(),
                        Qty = Convert.ToDecimal(sdr["QtyMobil"].ToString()),
                        EstQty = Convert.ToDecimal(sdr["EstQty"].ToString()),
                        Keterangan = sdr["Keterangan"].ToString(),
                        Delivery = sdr["Delivery"].ToString()
                    });
                }
            }
            return arrD;
        }
        public ArrayList ListArmada()
        {
            arrD = new ArrayList();
            string strSQL = "select * from MemoHarian_Armada where RowStatus >-1 " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            decimal JmlMobil = 0;
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    JmlMobil = JmlMobil + 1;
                    arrD.Add(new POPurchn
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        NoPol = sdr["NoPol"].ToString(),
                        DoNum = sdr["DoNum"].ToString(),
                        Driver = sdr["Driver"].ToString(),
                        Keterangan = sdr["Keterangan"].ToString(),
                        Ritase = Convert.ToInt32(sdr["Ritase"].ToString()),
                        Qty = JmlMobil,
                        Cetak = (sdr["Cetak"] == DBNull.Value) ? 0 : Convert.ToInt32(sdr["Cetak"].ToString()),
                        Flag = Convert.ToInt32(sdr["Flag"].ToString()),
                        Jam = sdr["Jam"].ToString(),
                        POID = (sdr["Mobile"] != DBNull.Value) ? Convert.ToInt32(sdr["Mobile"].ToString()) : 0
                    });
                }
            }
            return arrD;
        }
        public string CreateProcedure(object help, string spName)
        {
            string message = string.Empty;
            alk = (POPurchn)help;
            string[] arrCriteria = this.Criteria.Split(',');
            PropertyInfo[] data = alk.GetType().GetProperties();
            DataAccess da = new DataAccess(Global.ConnectionString());
            string param = "";
            string value = "";
            string field = "";
            string FieldUpdate = "";
            try
            {
                foreach (PropertyInfo items in data)
                {
                    if (arrCriteria.Contains(items.Name))
                    {
                        param += "@" + items.Name.ToString() + " " + GetTypeData(this.TableName, items.Name.ToString()) + ",";
                        value += "@" + items.Name.ToString() + ",";
                        field += items.Name.ToString() + ",";
                        if (items.Name.ToString() != "ID")
                            FieldUpdate += items.Name.ToString() + "=@" + items.Name.ToString() + ",";
                    }
                }
                string strSQL = "CREATE PROCEDURE " + spName + " " + param.Substring(0, param.Length - 1) +
                                " AS BEGIN SET NOCOUNT ON; ";
                if (this.Pilihan == "Insert")
                {
                    strSQL += "INSERT INTO " + this.TableName + " (" + field.Substring(0, field.Length - 1).ToString() + ")VALUES(" +
                     value.Substring(0, value.Length - 1) + ") SELECT @@IDENTITY as ID";
                }
                else
                {
                    strSQL += "UPDATE " + this.TableName + " set " + FieldUpdate.Substring(0, FieldUpdate.Length - 1).ToString() +
                      " where ID=@ID SELECT @@ROWCOUNT";
                }
                strSQL += " END";
                SqlDataReader result = da.RetrieveDataByString(strSQL);
                if (result != null)
                {
                    message = string.Empty;
                }
                else
                {
                    message = "";
                }
                return message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return message;
            }
        }
        private string GetTypeData(string TableName, string ColumName)
        {
            string result = string.Empty;
            string strsql = "select DATA_TYPE,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS IC where " +
                            "TABLE_NAME = '" + TableName + "' and COLUMN_NAME = '" + ColumName + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["DATA_TYPE"].ToString() + " ";
                    result += (sdr["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value) ? "(" + sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")" : "";
                    if (sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() == "-1")
                    {
                        result = result.Replace("-1", "Max");
                    }
                }
            }
            return result;
        }
        public int getDONum()
        {
            int result = 0;
            string strSQL = "Select Count(ID)Jml from MemoHarian_armada where month(SchDate)=" +
                            DateTime.Now.Month + " and year(SchDate)=" +
                            DateTime.Now.Year.ToString();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["Jml"].ToString());
                }
            }
            return result;
        }
        public string PrintDO()
        {
            string query = string.Empty;
            query = "Select ma.ID,DoNum,NoPol,Driver,ArmadaID,po.NoPO,isnull(ma.Keterangan,'')Keterangan, " +
                    "(SELECT dbo.ItemNameInv(mp.ItemID,1))ItemName,isnull(Jam,'')Jam, " +
                    "(SELECT SupplierName FROM SuppPurch where ID=mp.SupplierID)SupplierName,Convert(Char,ma.SchDate,103) AS Telepon,'' AS UP, " +
                    "'' AS Handpone,28 as Quantity,'Ton' as Satuan,isnull(ma.Cetak,0)Cetak" + this.SubQuery +
                    ",case when(Select DepoID from Company Where DepoID=ma.unloading)=1 then 'P' else '' end Ctrp," +
                    "case when(Select DepoID from Company Where DepoID=ma.unloading)=7 then 'P' else '' end Krwg," +
                    "case when(Select DepoID from Company Where DepoID=ma.unloading)=13 then 'P' else '' end Jmbg" +
                    " from MemoHarian_Armada AS ma " +
                    "LEFT JOIN MemoHarian_PO AS mp " +
                    "ON mp.ID=ma.SchPOID " +
                    "LEFT JOIN MemoHarian as mh " +
                    "ON mh.ID=ma.SchID " +
                    "LEFT JOIN POPurchn as po " +
                    "ON po.ID=mp.POID " +
                    "WHERE ma.RowStatus>-1 and ma.Flag=0 " + this.Criteria;
            return query;
        }
        public POPurchn DetailDO()
        {
            POPurchn p = new POPurchn();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.PrintDO());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    p.ID = Convert.ToInt32(sdr["ID"].ToString());
                    p.DoNum = sdr["DoNum"].ToString();
                    p.NoPO = sdr["NoPol"].ToString();
                    p.Remark = (this.SubQuery != string.Empty) ? sdr[this.Pilihan].ToString() : "";
                    p.SupplierName = sdr["SupplierName"].ToString();
                }
            }
            return p;
        }
        public POPurchn DetailSCH(int schID)
        {
            POPurchn p = new POPurchn();
            string strSQL = "Select * from MemoHarian where ID=(SELECT SchID FROM MemoHarian_PO where ID=" + schID + ") ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    p.SchDate = DateTime.Parse(sdr["DvlDate"].ToString());
                }
            }
            return p;
        }
        public int HapusData(int SchID)
        {
            try
            {
                string strSQL = "Update MemoHarian_Armada set RowStatus =-2,LastModifiedBy='" + ((Users)System.Web.HttpContext.Current.Session["Users"]).UserName + "',CreatedTime=GETDATE()";
                strSQL += "WHERE SchPOID=" + SchID;// +" AND Flag=0";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                return sdr.RecordsAffected;
            }
            catch { return -1; }
        }
    }
}