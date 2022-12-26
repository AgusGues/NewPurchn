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

namespace GRCweb1.Modul.Purchasing
{
    public partial class InputKertasQa : System.Web.UI.Page
    {
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                LoadCompany();
                txtTgl.Text = DateTime.Now.ToString("dd-MM-yyyy");
                //LoadItemKertas();
                btnSimpan.Enabled = false;
                LoadDataGridQaKertas(LoadGridQaKertas());
            }
            //((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadCompany()
        {
            CompanyFacade cp = new CompanyFacade();
            arrData = new ArrayList();
            ddlCompany.Items.Clear();
            ddlCompany.Items.Add(new ListItem("All", "0"));
            cp.Where = " AND DepoID in(1,7,13)";
            arrData = cp.Retrieve();
            foreach (Company cmp in arrData)
            {
                string lokasi = "";
                switch (cmp.DepoID)
                {
                    case 1: lokasi = "BPAS - CITEUREUP"; break;
                    case 7: lokasi = "BPAS - KARAWANG"; break;
                    case 13: lokasi = "BPAS - JOMBANG"; break;
                }
                ddlCompany.Items.Add(new ListItem(lokasi, cmp.DepoID.ToString()));
            }
            ddlCompany.SelectedValue = ((Users)Session["Users"]).UnitKerjaID.ToString();
        }

        protected void ddlJInputan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            #region rules
            if (ddlJInputan.SelectedIndex == 1)
            {
                PanelTambahBaru.Visible = true;
                btnSimpan.Enabled = true;
            }
            else if (ddlJInputan.SelectedIndex == 2)
            {
                PanelTambahBaru.Visible = false;
                //ddlJenisKertas.SelectedIndex = 0;
                ddlSumberKertas.SelectedIndex = 0;
                txtMerkKertas.Text = string.Empty;
            }
            else if (ddlJInputan.SelectedIndex == 3)
            {
                PanelTambahBaru.Visible = false;
                // ddlJenisKertas.SelectedIndex = 0;
                ddlSumberKertas.SelectedIndex = 0;
                txtMerkKertas.Text = string.Empty;
            }
            else
            {
                PanelTambahBaru.Visible = false;
                //ddlJenisKertas.SelectedIndex = 0;
                ddlSumberKertas.SelectedIndex = 0;
                txtMerkKertas.Text = string.Empty;
                btnSimpan.Enabled = false;
            }
            #endregion
        }

        protected void ddlJSumberKertas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSumberKertas.SelectedIndex == 1)
            {
                ArrayList arrItemKertas = new ArrayList();
                KertasQualityAFacade ItemKertas = new KertasQualityAFacade();
                Users users = (Users)Session["Users"];
                arrItemKertas = ItemKertas.RetrieveLokal();
                ddlJenisKertas.Items.Clear();
                ddlJenisKertas.Items.Add(new ListItem("-- Pilih Jenis Kertas --", "0"));
                foreach (KertasQualityA ItemK in arrItemKertas)
                {
                    ddlJenisKertas.Items.Add(new ListItem(ItemK.ItemName + " (" + ItemK.ItemCode + ")", ItemK.ID.ToString()));
                }
            }
            else if (ddlSumberKertas.SelectedIndex == 2)
            {
                ArrayList arrItemKertas = new ArrayList();
                KertasQualityAFacade ItemKertas = new KertasQualityAFacade();
                Users users = (Users)Session["Users"];
                arrItemKertas = ItemKertas.Retrieve();
                ddlJenisKertas.Items.Clear();
                ddlJenisKertas.Items.Add(new ListItem("-- Pilih Jenis Kertas --", "0"));
                foreach (KertasQualityA ItemK in arrItemKertas)
                {
                    ddlJenisKertas.Items.Add(new ListItem(ItemK.ItemName + " (" + ItemK.ItemCode + ")", ItemK.ID.ToString()));
                }
            }
            else
            {

            }
        }

        protected void ddlNamaBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            InventoryFacade inventoryFacade = new InventoryFacade();
            Inventory inventory = inventoryFacade.RetrieveById(int.Parse(ddlJenisKertas.SelectedValue));
            if (inventoryFacade.Error == string.Empty)
            {
                if (inventory.ID > 0)
                {
                    txtCode.Text = inventory.ItemCode;
                }
            }
        }

        private void LoadItemKertas()
        {
            ArrayList arrItemKertas = new ArrayList();
            KertasQualityAFacade ItemKertas = new KertasQualityAFacade();
            Users users = (Users)Session["Users"];
            arrItemKertas = ItemKertas.Retrieve();
            ddlJenisKertas.Items.Clear();
            ddlJenisKertas.Items.Add(new ListItem("-- Pilih Jenis Kertas --", "0"));
            foreach (KertasQualityA ItemK in arrItemKertas)
            {
                ddlJenisKertas.Items.Add(new ListItem(ItemK.ItemName, ItemK.ID.ToString()));
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";
            KertasQualityAFacade kertasQualityFacade = new KertasQualityAFacade();
            KertasQualityA QaK = new KertasQualityA();
            QaK.PlantID = int.Parse(ddlCompany.SelectedValue.ToString());
            QaK.Tanggal = DateTime.Parse(txtTgl.Text);
            QaK.JenisInput = int.Parse(ddlJInputan.SelectedValue.ToString());
            QaK.SmbrKertas = int.Parse(ddlSumberKertas.SelectedValue.ToString());
            QaK.JKertasID = int.Parse(ddlJenisKertas.SelectedValue.ToString());
            QaK.ItemCode = txtCode.Text;
            QaK.MerkKertas = txtMerkKertas.Text;
            QaK.SolidContent = decimal.Parse(txtSolid.Text.ToString());
            QaK.Freeness = decimal.Parse(txtFreeness.Text.ToString());
            QaK.AsalKertas = txtAsalKertas.Text;
            QaK.Ket = txtKet.Text;
            //QaK.ID = idXtxt.Text;
            QaK.CreatedBy = ((Users)Session["Users"]).UserName;
            int intResult = 0;
            if (QaK.ID > 0)
            {
                intResult = kertasQualityFacade.Update(QaK);
            }
            else
            {
                intResult = kertasQualityFacade.Insert(QaK);

                if (kertasQualityFacade.strError == string.Empty)
                {
                    if (intResult > 0)
                    {
                        DisplayAJAXMessage(this, "Berhasil disimpan");
                    }
                }
            }

            if (kertasQualityFacade.strError == string.Empty && intResult > 0)
            {
                LoadDataGridQaKertas(LoadGridQaKertas());
                InsertLog(strEvent);
                ClearForm();
            }
            if (user.UnitKerjaID == 1)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery =
                    "exec [sqlkrwg.grcboard.com].bpaskrwg.dbo.InsertKertasQA_Simpan_from_ctrp " +
                    " exec [sqlJombang.grcboard.com].bpasjombang.dbo.InsertKertasQA_Simpan_from_ctrp ";
                SqlDataReader sdr = zl.Retrieve();
            }
            //else if (user.UnitKerjaID == 7)
            //{
            //    ZetroView zl = new ZetroView();
            //    zl.QueryType = Operation.CUSTOM;
            //    zl.CustomQuery =
            //        "exec [sqlctrp.grcboard.com].bpasctrp.dbo.InsertKertasQA_Simpan_from_krwg ";
            //    //"exec [sqlJombang.grcboard.com].bpasjombang.dbo.InsertKertasQA_Simpan_from_ctrp
            //    SqlDataReader sdr = zl.Retrieve();
            //}
        }

        private ArrayList LoadGridQaKertas()
        {
            ArrayList arrQaKertas = new ArrayList();
            KertasQualityAFacade QaFacade = new KertasQualityAFacade();
            arrQaKertas = QaFacade.RetrieveList(((Users)Session["Users"]).UnitKerjaID);
            if (arrQaKertas.Count > 0)
            {
                return arrQaKertas;
            }

            arrQaKertas.Add(new KertasQualityA());
            return arrQaKertas;
        }

        private void LoadDataGridQaKertas(ArrayList arrQaKertas)
        {
            this.lstQaKertas.DataSource = arrQaKertas;
            this.lstQaKertas.DataBind();
        }

        protected void lstQaKertas_Command(object sender, RepeaterCommandEventArgs e)
        {
            int intResult = 0;
            Users users = (Users)Session["Users"];
            string KID = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "dele":
                    //if (users.Apv > 0)
                    //{
                    //int index = Convert.ToInt32(e.CommandArgument);
                    string Code = Convert.ToString(e.CommandArgument);
                    string strSQL = "update Qa_Kertas set RowStatus=-1 where ID = '" + KID + "' ";
                    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                    if (dataAccess.Error != string.Empty)
                        intResult = -1;
                    LoadDataGridQaKertas(LoadGridQaKertas());
                    if (users.UnitKerjaID == 1)
                    {
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery =
                            "exec [sqlkrwg.grcboard.com].bpaskrwg.dbo.DeleteKertasQA_from_ctrp '" + Code + "' " +
                            "exec [sqlJombang.grcboard.com].bpasjombang.dbo.DeleteKertasQA_from_ctrp '" + Code + "' ";
                        SqlDataReader sdr = zl.Retrieve();
                    }
                    //}
                    break;
                case "edit":
                    KertasQualityA qa = new KertasQualityAFacade().RetrieveByItemCode(KID);
                    PanelTambahBaru.Visible = true;
                    ddlJInputan.Enabled = false;
                    txtKet.Text = qa.Ket.ToString();
                    txtFreeness.Text = qa.Freeness.ToString();
                    txtSolid.Text = qa.SolidContent.ToString();
                    txtAsalKertas.Text = qa.AsalKertas.ToString();
                    txtMerkKertas.Text = qa.MerkKertas.ToString();
                    txtCode.Text = qa.ItemCode.ToString();
                    txtTgl.Text = qa.Tanggal.ToString("dd-MM-yyyy");
                    idXtxt.Text = qa.ID.ToString();
                    btnSimpan.Enabled = true;
                    //nKertas.Text = qa.NmKertas.ToString();
                    for (int i = ddlJenisKertas.Items.Count - 1; i > 0; i--)
                    {
                        if (ddlJenisKertas.Items[i].Text.Contains(qa.NmKertas))
                        {
                            ddlJenisKertas.Items[i].Selected = true;
                            break;
                        }
                    }
                    //ddlJenisKertas.SelectedItem.Text = qa.NmKertas;
                    break;
            }
        }

        protected void lstQaKertas_Databound(object sender, RepeaterItemEventArgs e)
        {
            //((Users)Session["Users"]).UnitKerjaID
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            KertasQualityA KqA = (KertasQualityA)e.Item.DataItem;
            Image edtKertasQa = (Image)e.Item.FindControl("edt");
            Image delKertasQa = (Image)e.Item.FindControl("del");
            if (users.DeptID == 9 || users.DeptID == 14)
            {
                edtKertasQa.Visible = true;
                delKertasQa.Visible = true;
            }
            else
            {
                edtKertasQa.Visible = false;
                delKertasQa.Visible = false;
            }

        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Modul Quallity Assurance";
            eventLog.EventName = eventName;
            //eventLog.DocumentNo = ;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }

        private string ValidateText()
        {
            decimal val;
            //if (ddlJInputan.SelectedIndex == 0)
            //    return "Pilih Jenis Inputan";
            if (ddlSumberKertas.SelectedIndex == 0)
                return "Pilih Sumber Kertas";
            else if (ddlJenisKertas.SelectedIndex == 0)
                return "Pilih Jenis Kertas";
            else if (txtMerkKertas.Text == string.Empty)
                return "Nama / Merk Kertas Harus Diisi";
            else if (!txtSolid.Text.All(char.IsDigit))
                return "Solid Content Harus Angka ";
            else if (!txtFreeness.Text.All(char.IsDigit))
                return "Freeeness Harus Angka ";
            else if (txtAsalKertas.Text == string.Empty)
                return "Asal Kertas Harus Di isi";
            return string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormListKertasQa.aspx");
        }

        private void ClearForm()
        {
            ddlJInputan.SelectedIndex = 0;
            txtTgl.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //ddlJenisKertas.SelectedIndex = 0;
            ddlSumberKertas.SelectedIndex = 0;
            txtMerkKertas.Text = string.Empty;
            txtSolid.Text = string.Empty;
            txtFreeness.Text = string.Empty;
            txtAsalKertas.Text = string.Empty;
            txtKet.Text = string.Empty;
            PanelTambahBaru.Visible = false;
            btnSimpan.Enabled = true;
            ddlJInputan.Enabled = true;
            txtCode.Text = string.Empty;
        }
    }

    public class KertasQualityAFacade
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private KertasQualityA objKertasQA = new KertasQualityA();

        public KertasQualityAFacade()
            : base()
        {

        }
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int Insert(object objDomain)
        {
            try
            {
                objKertasQA = (KertasQualityA)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PlantID", objKertasQA.PlantID));
                sqlListParam.Add(new SqlParameter("@Tanggal", objKertasQA.Tanggal));
                sqlListParam.Add(new SqlParameter("@JenisInput", objKertasQA.JenisInput));
                sqlListParam.Add(new SqlParameter("@SmbrKertas", objKertasQA.SmbrKertas));
                sqlListParam.Add(new SqlParameter("@JKertasID", objKertasQA.JKertasID));
                sqlListParam.Add(new SqlParameter("@ItemCode", objKertasQA.ItemCode));
                sqlListParam.Add(new SqlParameter("@MerkKertas", objKertasQA.MerkKertas));
                sqlListParam.Add(new SqlParameter("@SolidContent", objKertasQA.SolidContent));
                sqlListParam.Add(new SqlParameter("@Freeness", objKertasQA.Freeness));
                sqlListParam.Add(new SqlParameter("@AsalKertas", objKertasQA.AsalKertas));
                sqlListParam.Add(new SqlParameter("@Ket", objKertasQA.Ket));
                sqlListParam.Add(new SqlParameter("@Apv", objKertasQA.Apv));
                sqlListParam.Add(new SqlParameter("@RowStatus", objKertasQA.RowStatus));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objKertasQA.CreatedBy));
                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "InsertKertasQA");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int Update(object objDomain)
        {
            try
            {
                objKertasQA = (KertasQualityA)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objKertasQA.ID));
                sqlListParam.Add(new SqlParameter("@Tanggal", objKertasQA.Tanggal));
                sqlListParam.Add(new SqlParameter("@JenisInput", objKertasQA.JenisInput));
                sqlListParam.Add(new SqlParameter("@SmbrKertas", objKertasQA.SmbrKertas));
                sqlListParam.Add(new SqlParameter("@JKertasID", objKertasQA.JKertasID));
                sqlListParam.Add(new SqlParameter("@ItemCode", objKertasQA.ItemCode));
                sqlListParam.Add(new SqlParameter("@MerkKertas", objKertasQA.MerkKertas));
                sqlListParam.Add(new SqlParameter("@SolidContent", objKertasQA.SolidContent));
                sqlListParam.Add(new SqlParameter("@Freeness", objKertasQA.Freeness));
                sqlListParam.Add(new SqlParameter("@AsalKertas", objKertasQA.AsalKertas));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objKertasQA.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@Ket", objKertasQA.Ket));
                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "UpdateKertasQA");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public ArrayList Retrieve()
        {
            //string strSQL = "select ItemID,ItemCode,Ket From Pulp_Formula order by ItemID";
            string strSQL = "select ID,ItemCode,ItemName From Inventory where GroupID=1 and Aktif=1 and LocImp=0";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrData;
        }

        public ArrayList RetrieveLokal()
        {
            //string strSQL = "select ItemID,Ket From Pulp_Formula order by ItemID";
            string strSQL = "select ID,ItemCode,ItemName From Inventory where GroupID=1 and Aktif=1 and LocImp=1 ";
            //string strSQL = "select ID,ItemCode,ItemName From Inventory where GroupID=1 and Aktif=1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrData;
        }

        public KertasQualityA RetrieveByItemCode(string ItemCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select z.ItemName NmKertas,* From Qa_Kertas x inner join Inventory z on z.ItemCode=x.ItemCode where x.RowStatus >-1 and x.ItemCode ='" + ItemCode + "' ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject01(sqlDataReader);
                }
            }

            return new KertasQualityA();
        }

        public ArrayList RetrieveList(int UnitKerjaID)
        {
            //Users user = (Users)Session["Users"];
            string query = string.Empty;
            string query0 = string.Empty;
            string query1 = string.Empty;
            string query2 = string.Empty;
            string query3 = string.Empty;
            if (UnitKerjaID == 1)
            {
                query = " [sqlkrwg.grcboard.com].bpaskrwg ";
                query0 = "[sqljombang.grcboard.com].bpasjombang";
                query1 = "1"; //1
                query2 = "7"; //2
                query3 = "13"; //3

            }
            else if (UnitKerjaID == 7)
            {
                query = " [sqlctrp.grcboard.com].bpasctrp ";
                query0 = "[sqljombang.grcboard.com].bpasjombang";
                query1 = "7";
                query2 = "1";
                query3 = "13";
            }
            else
            {
                query = " [sqlctrp.grcboard.com].bpasctrp ";
                query0 = "[sqlkrwg.grcboard.com].bpaskrwg";
                query1 = "13";
                query2 = "1";
                query3 = "7";
            }
            string strSQL = " select distinct A.ID,PlantID, " +
                             " case when A.PlantID=1 then 'BPAS - CITEUREUP' when A.PlantID=7 then 'BPAS - KARAWANG' else 'BPAS - JOMBANG' end	NamaPlant," +
                             " case when A.JenisInput = 1 then 'Tambah Baru' when JenisInput=2 then 'Perubahan Jenis' else 'Penghapusan' end NmJenisInput, " +
                             " case when A.SmbrKertas = 1 then 'Lokal' else 'Import' end NmSumberKertas, " +
                             " A.Tanggal,A.JenisInput,A.SmbrKertas,A.JKertasID,C.ItemName NmKertas,A.ItemCode,A.MerkKertas,A.SolidContent,A.Freeness,A.AsalKertas,A.Ket From Qa_Kertas A " +
                             " inner join Inventory C on C.ItemCode=A.ItemCode and A.RowStatus>-1 and A.PlantID = " + query1 + "  " +
                             " union all " +
                           " select distinct A.ID,PlantID, " +
                             " case when A.PlantID=1 then 'BPAS - CITEUREUP' when A.PlantID=7 then 'BPAS - KARAWANG' else 'BPAS - JOMBANG' end	NamaPlant, " +
                             " case when A.JenisInput = 1 then 'Tambah Baru' when JenisInput=2 then 'Perubahan Jenis' else 'Penghapusan' end NmJenisInput, " +
                             " case when A.SmbrKertas = 1 then 'Lokal' else 'Import' end NmSumberKertas, " +
                             " A.Tanggal,A.JenisInput,A.SmbrKertas,A.JKertasID,C.ItemName NmKertas,A.ItemCode,A.MerkKertas,A.SolidContent,A.Freeness,A.AsalKertas,A.Ket From " + query + ".dbo.Qa_Kertas A " +
                             " inner join " + query + ".dbo.Inventory C on C.ItemCode=A.ItemCode and A.RowStatus>-1 and A.PlantID = " + query2 + "   " +
                             " union all " +
                            " select distinct A.ID,PlantID, " +
                             " case when A.PlantID=1 then 'BPAS - CITEUREUP' when A.PlantID=7 then 'BPAS - KARAWANG' else 'BPAS - JOMBANG' end	NamaPlant, " +
                             " case when A.JenisInput = 1 then 'Tambah Baru' when JenisInput=2 then 'Perubahan Jenis' else 'Penghapusan' end NmJenisInput, " +
                             " case when A.SmbrKertas = 1 then 'Lokal' else 'Import' end NmSumberKertas, " +
                             " A.Tanggal,A.JenisInput,A.SmbrKertas,A.JKertasID,C.ItemName NmKertas,A.ItemCode,A.MerkKertas,A.SolidContent,A.Freeness,A.AsalKertas,A.Ket From " + query0 + ".dbo.Qa_Kertas A " +
                             " inner join " + query0 + ".dbo.Inventory C on C.ItemCode=A.ItemCode and A.RowStatus>-1 and A.PlantID = " + query3 + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObject0(sqlDataReader));
                }
            }

            return arrData;
        }


        public KertasQualityA GenerateObject(SqlDataReader sqlDataReader)
        {

            objKertasQA = new KertasQualityA();
            objKertasQA.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKertasQA.ItemCode = (sqlDataReader["ItemCode"]).ToString();
            objKertasQA.ItemName = (sqlDataReader["ItemName"]).ToString();
            return objKertasQA;
        }

        public KertasQualityA GenerateObjectLocal(SqlDataReader sqlDataReader)
        {

            objKertasQA = new KertasQualityA();
            objKertasQA.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            //objKertasQA.ItemCode = (sqlDataReader["ItemCode"]).ToString();
            //objKertasQA.ItemName = (sqlDataReader["ItemName"]).ToString();
            objKertasQA.Ket = (sqlDataReader["Ket"]).ToString();
            return objKertasQA;
        }

        public KertasQualityA GenerateObject0(SqlDataReader sqlDataReader)
        {

            objKertasQA = new KertasQualityA();
            objKertasQA.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKertasQA.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objKertasQA.NmSumberKertas = (sqlDataReader["NmSumberKertas"]).ToString();
            objKertasQA.NmKertas = (sqlDataReader["NmKertas"]).ToString();
            objKertasQA.MerkKertas = (sqlDataReader["MerkKertas"]).ToString();
            objKertasQA.SolidContent = Convert.ToDecimal(sqlDataReader["SolidContent"]);
            objKertasQA.Freeness = Convert.ToDecimal(sqlDataReader["Freeness"]);
            objKertasQA.AsalKertas = (sqlDataReader["AsalKertas"]).ToString();
            objKertasQA.Ket = (sqlDataReader["Ket"]).ToString();
            objKertasQA.Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"]);
            objKertasQA.NamaPlant = (sqlDataReader["NamaPlant"]).ToString();
            objKertasQA.ItemCode = (sqlDataReader["ItemCode"]).ToString();
            return objKertasQA;
        }

        public KertasQualityA GenerateObject01(SqlDataReader sqlDataReader)
        {

            objKertasQA = new KertasQualityA();
            objKertasQA.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objKertasQA.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            //objKertasQA.NmSumberKertas = (sqlDataReader["NmSumberKertas"]).ToString();
            objKertasQA.NmKertas = (sqlDataReader["NmKertas"]).ToString();
            objKertasQA.MerkKertas = (sqlDataReader["MerkKertas"]).ToString();
            objKertasQA.SolidContent = Convert.ToDecimal(sqlDataReader["SolidContent"]);
            objKertasQA.Freeness = Convert.ToDecimal(sqlDataReader["Freeness"]);
            objKertasQA.AsalKertas = (sqlDataReader["AsalKertas"]).ToString();
            objKertasQA.Ket = (sqlDataReader["Ket"]).ToString();
            objKertasQA.Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"]);
            //objKertasQA.NamaPlant = (sqlDataReader["NamaPlant"]).ToString();
            objKertasQA.ItemCode = (sqlDataReader["ItemCode"]).ToString();
            return objKertasQA;
        }
    }

    public class KertasQualityA : GRCBaseDomain
    {
        public int UnitKerjaID { get; set; }
        //public int ID { get; set; }
        public int PlantID { get; set; }
        public int ItemID { get; set; }
        public DateTime Tanggal { get; set; }
        public int JenisInput { get; set; }
        public int SmbrKertas { get; set; }
        public int JKertasID { get; set; }
        public string Ket { get; set; }
        public string MerkKertas { get; set; }
        public decimal SolidContent { get; set; }
        public decimal Freeness { get; set; }
        public string AsalKertas { get; set; }
        public int Apv { get; set; }
        public int Status { get; set; }
        public string Error { get; set; }
        public string NmSumberKertas { get; set; }
        public string NmKertas { get; set; }
        public string NamaPlant { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }

}