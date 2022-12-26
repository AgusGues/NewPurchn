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
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

using Dapper;
using System.Text;
using System.Threading.Tasks;
using GRCweb1.Modul.Sarmut;
using System.Web.UI.HtmlControls;
using GRCweb1.Modul.Factory;
using static iTextSharp.text.pdf.AcroFields;
using System.Security.Cryptography;
using System.EnterpriseServices;

namespace GRCweb1.Modul.Factory
{
    public partial class FormPenerimaanTitipanMss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtTglTerima.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
        }
        [WebMethod]
       
        public static string GetUser()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            string createdby = user.UserName;

            return createdby;
        }
        protected void ClearForm(object sender, EventArgs e)
        {
            txtNoOrder.Text = String.Empty;
            txtSupplier.Text = String.Empty;
            txtTebal.Text = String.Empty;
            txtPanjang.Text = String.Empty;
            txtLebar.Text = String.Empty;
            txtm3.Text = String.Empty;
            txtBerat.Text = String.Empty;
            btnSimpan.Disabled = false;
            txtNoOrder_TextChanged(null, null);
        }
        protected int CekInputan()
        {
            int result = 0;
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select count(ID) ID  from MSS_TransitInfo where  MssOrderNumber = '" + txtNoOrder.Text + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = Int32.Parse( sdr["ID"].ToString());
                    }
                }
            }
            return result;
        }
        protected void klikcari(object sender, EventArgs e)
        {
            if (txtCari.Text != String.Empty)
            {
                txtNoOrder.Text = txtCari.Text;
                txtNoOrder_TextChanged(null, null);
            }
        }
        protected void LoadDataList()
        {
            if (txtNoOrder.Text == String.Empty) return;
            String cnnHO = "[sql1.grcboard.com].grcboard.dbo.";
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select S.SupplierName,A.OPNo  from " + cnnHO + "OP A inner join " + cnnHO + "Supplier S on A.PartnerSupplierID = S.PartnerID_Supplier where isnull(A.BarangTitipan,0)=0 and A.MssOrderNumber = '" + txtNoOrder.Text + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        txtSupplier.Text = sdr["SupplierName"].ToString();
                    }
                }
            }
            LoadDataListDetail();
        }
        protected void LoadDataListDetail()
        {
            String cnnHO = "[sql1.grcboard.com].grcboard.dbo.";
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select  ROW_NUMBER() OVER (ORDER BY I.[Description]) AS No,itemid,I.[Description],A.Tebal,A.Panjang,A.Lebar,((A.Tebal*A.Panjang*A.Lebar)/1000000000)*Quantity M3," +
                "A.Berat*A.Quantity Berat,A.Quantity ,A.PartnerPromoInfo keterangan  from  " + cnnHO + "OPDetail A inner join " + cnnHO + "items I on A.ItemID = I.ID  inner join  " + cnnHO + "UOM U on I.UOMID = U.ID  where OPID in " +
                "(select id from  " + cnnHO + "OP where isnull(BarangTitipan,0)=0 and MssOpType = 4 and MssOrderNumber = '" + txtNoOrder.Text + "')";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new DTitipanMSSDetail
                        {
                            No = Int32.Parse(sdr["No"].ToString()),
                            ItemID = Int32.Parse(sdr["ItemID"].ToString()),
                            Description = sdr["Description"].ToString(),
                            Tebal = Decimal.Parse(sdr["Tebal"].ToString()),
                            Panjang = Decimal.Parse(sdr["Panjang"].ToString()),
                            Berat = Decimal.Parse(sdr["Berat"].ToString()),
                            M3 = Decimal.Parse(sdr["M3"].ToString()),
                            Quantity = Decimal.Parse(sdr["Quantity"].ToString()),
                            Keterangan = sdr["Keterangan"].ToString()
                        });
                    }
                }
            }
            lstTitipan.DataSource = arrData;
            lstTitipan.DataBind();
        }
        protected void LoadDataListP()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from MSS_TransitInfo where  MssOrderNumber = '" + txtNoOrder.Text + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        txtSupplier.Text = sdr["SupplierName"].ToString();
                        txtTebal.Text = sdr["Tebal"].ToString();
                        txtPanjang.Text = sdr["Panjang"].ToString();
                        txtLebar.Text = sdr["Lebar"].ToString();
                        txtm3.Text = sdr["m3"].ToString();
                        txtBerat.Text = sdr["berat"].ToString();
                    }
                }
            }
            LoadDataListDetailP();
        }

        protected void LoadDataListDetailP()
        {
            String cnnHO = "[sql1.grcboard.com].grcboard.dbo.";
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select  ROW_NUMBER() OVER (ORDER BY [Description]) AS No,*  from MSS_TransitInfodetail where msstID in " +
                "(select id from  MSS_TransitInfo where rowstatus>-1 and MssOrderNumber = '" + txtNoOrder.Text + "')";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new DTitipanMSSDetail
                        {
                            No = Int32.Parse(sdr["No"].ToString()),
                            ItemID = Int32.Parse(sdr["ItemID"].ToString()),
                            Description = sdr["Description"].ToString(),
                            Tebal = Decimal.Parse(sdr["Tebal"].ToString()),
                            Panjang = Decimal.Parse(sdr["Panjang"].ToString()),
                            Berat = Decimal.Parse(sdr["Berat"].ToString()),
                            M3 = Decimal.Parse(sdr["M3"].ToString()),
                            Quantity = Decimal.Parse(sdr["Quantity"].ToString()),
                            Keterangan = sdr["Keterangan"].ToString()
                        });
                    }
                }
            }
            lstTitipan.DataSource = arrData;
            lstTitipan.DataBind();
        }
        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void Simpan(object sender, EventArgs e)
        {
            int intresult = 0;
            int intresultDetail = 0;
            if (txtNoOrder.Text != String.Empty && txtm3.Text != String.Empty && txtBerat.Text != String.Empty)
            {
                intresult = SimpanHeader();
                if (intresult >0 )
                    intresultDetail =SimpanDetail(intresult);
                else
                {
                    DisplayAJAXMessage(this, "Simpan Data Header Error");
                    return;
                }

                if (intresultDetail > 0)
                {
                    DisplayAJAXMessage(this, "Data tersimpan");
                    return;
                }
                else
                {
                    DisplayAJAXMessage(this, "Simpan Data Detail Error");
                    return;
                }
            }

        }
        protected int SimpanHeader()
        {
            int ID = 0;
            decimal tebal = decimal.Parse(txtTebal.Text);
            decimal pajang = decimal.Parse(txtPanjang.Text);
            decimal lebar = decimal.Parse(txtLebar.Text);
            decimal m3 = decimal.Parse(txtm3.Text);
            decimal berat = decimal.Parse(txtBerat.Text);
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "insert MSS_TransitInfo " +
                "select getdate() TglTerima, A.ID OPID, OPNO, MSSorderNumber, PartnerSupplierID, S.SupplierName,"+ tebal.ToString().Replace(",",".") + " Tebal, "+ lebar.ToString().Replace(",", ".") + 
                " Lebar, "+ pajang.ToString().Replace(",", ".") + " Panjang, "+ m3.ToString().Replace(",", ".") + " M3, "+ berat.ToString().Replace(",", ".") + " Berat, 0 Rowstatus, '' CreatedBy, "+
                "getdate() CreatedTime, '' LastModifiedBy, getdate() LastMdifiedtime  from [sql1.grcboard.com].grcboard.dbo.OP A inner join [sql1.grcboard.com].grcboard.dbo.Supplier S on " +
                "A.PartnerSupplierID = S.PartnerID_Supplier where MssOrderNumber = '" + txtNoOrder.Text + "'" + " select @@IDENTITY as ID";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        ID = Convert.ToInt32( sdr["ID"].ToString());
                    }
                }
            }
            return ID;
        }
        protected int SimpanDetail(int msstID)
        {
            int ID = 0;
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "insert MSS_TransitInfoDetail " +
                "select " + msstID +" msstID,itemid,I.[Description],A.Quantity ,A.Tebal,A.Lebar,A.Panjang,((A.Tebal*A.Panjang*A.Lebar)/1000000000)*Quantity M3,A.Berat*A.Quantity Berat, " +
                "A.PartnerPromoInfo keterangan,0 Rowstatus from[sql1.grcboard.com].grcboard.dbo.OPDetail A inner join[sql1.grcboard.com].grcboard.dbo.items I on A.ItemID = I.ID  inner join " +
                "[sql1.grcboard.com].grcboard.dbo.UOM U on I.UOMID = U.ID  where OPID in (select id from[sql1.grcboard.com].grcboard.dbo.OP where MssOpType = 4 and MssOrderNumber = '" + txtNoOrder.Text + "')" +
                "update [sql1.grcboard.com].grcboard.dbo.op set BarangTitipan=1,Status=2,ApproveSCDate=getdate(),ActualTerimaBarang=getdate() where MssOrderNumber = '" + txtNoOrder.Text + "' select @@IDENTITY as ID";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString());
                    }
                }
            }
            return ID;
           
        }
        protected void txtNoOrder_TextChanged(object sender, EventArgs e)
        {
            if (CekInputan() > 0)
            {
                LoadDataListP();
                btnSimpan.Disabled = true;
            }
            else
            {
                LoadDataList();
                btnSimpan.Disabled = false;
            }
            
        }
        protected void lsttxtNama_TextChanged(object sender, EventArgs e)
        {
            TextBox txts = (TextBox)sender;
            int n = int.Parse(txts.ToolTip);
            TextBox ltxtTebal = (TextBox)lstPaket.Items[n].FindControl("ltxtTebal");
            ltxtTebal.Focus();
        }
        protected void lsttxtTebal_TextChanged(object sender, EventArgs e)
        {
            TextBox txts = (TextBox)sender;
            int n = int.Parse(txts.ToolTip);
            TextBox ltxtPanjang = (TextBox)lstPaket.Items[n].FindControl("ltxtPanjang");
            ltxtPanjang.Focus();
        }
        protected void lsttxtPanjang_TextChanged(object sender, EventArgs e)
        {
            TextBox txts = (TextBox)sender;
            int n = int.Parse(txts.ToolTip);
            TextBox ltxtLebar = (TextBox)lstPaket.Items[n].FindControl("ltxtLebar");
            ltxtLebar.Focus();
        }
        protected void lsttxtLebar_TextChanged(object sender, EventArgs e)
        {
            TextBox txts = (TextBox)sender;
            int n = int.Parse(txts.ToolTip);
            TextBox ltxtBerat = (TextBox)lstPaket.Items[n].FindControl("ltxtBerat");
            ltxtBerat.Focus();
        }
       
        protected void lsttxtBerat_TextChanged(object sender, EventArgs e)
        {
            TextBox txts = (TextBox)sender;
            int n = int.Parse(txts.ToolTip);
            if (n < int.Parse(txtJumlah.Text)-1)
                n = n + 1;
            else
                n = 0;
            TextBox ltxtNama = (TextBox)lstPaket.Items[n].FindControl("ltxtNama");
            ltxtNama.Focus();
        }
        protected void txtJumlah_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrPaket = new ArrayList();
            int juml = 0;
            if (txtJumlah.Text != String.Empty)
            {
                juml = int.Parse(txtJumlah.Text);
                for (int i = 1; i <= juml; i++)
                {
                    DTitipanMSSPaket Paket = new DTitipanMSSPaket();
                    Paket.No = i;
                    Paket.MSSTID = 0;
                    Paket.NamaPaket = "";
                    Paket.Tebal = 0;
                    Paket.Lebar = 0;
                    Paket.Panjang = 0;
                    Paket.M3 = 0;
                    Paket.Berat = 0;
                    arrPaket.Add(Paket);
                }
                Session["arrPaket"] = arrPaket;
            }
            lstPaket.DataSource = arrPaket;
            lstPaket.DataBind();
        }
        protected void txtTebal_TextChanged(object sender, EventArgs e)
        {
            decimal tebal = 0;
            decimal panjang = 0;
            decimal lebar = 0;
            if(txtTebal.Text != String.Empty && txtPanjang.Text != String.Empty && txtLebar.Text != String.Empty)
            {
                tebal = decimal.Parse(txtTebal.Text);
                panjang = decimal.Parse(txtPanjang.Text); 
                    lebar = decimal.Parse(txtLebar.Text);
                    if (decimal.Parse(txtTebal.Text) > 0 && decimal.Parse(txtPanjang.Text) > 0 && decimal.Parse(txtLebar.Text) > 0)
                        txtm3.Text = ((tebal* panjang* lebar) / 1000000).ToString();
                
            }
            txtPanjang.Focus();
        }

        protected void txtPanjang_TextChanged(object sender, EventArgs e)
        {
            txtTebal_TextChanged(null, null);
            txtLebar.Focus();
        }

        protected void txtLebar_TextChanged(object sender, EventArgs e)
        {
            txtTebal_TextChanged(null, null);
            txtBerat.Focus();
        }
    }

    public class DTitipanMSS : GRCBaseDomain
    {
        public DateTime Tanggal { get; set; }
        public int OPID { get; set; }
        public string OPNO { get; set; }
        public string MSSorderNumber { get; set; }
        public int PartnerSupplierID { get; set; }
        public string SupplierName { get; set; }
        public Decimal Tebal { get; set; }
        public Decimal Panjang { get; set; }
        public Decimal Lebar { get; set; }
        public Decimal M3 { get; set; }
        public Decimal Berat { get; set; }
        public string Keterangan { get; set; }
    }
    public class DTitipanMSSPaket : GRCBaseDomain
    {
        public int No { get; set; }
        public int MSSTID { get; set; }
        public string NamaPaket { get; set; }
        public Decimal Tebal { get; set; }
        public Decimal Panjang { get; set; }
        public Decimal Lebar { get; set; }
        public Decimal M3 { get; set; }
        public Decimal Berat { get; set; }
    }
    public class DTitipanMSSDetail : GRCBaseDomain
    {
        public int No { get; set; }
        public int MSSTID { get; set; }
        public int ItemID { get; set; }
        public string Description { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal Tebal { get; set; }
        public Decimal Panjang { get; set; }
        public Decimal Lebar { get; set; }
        public Decimal M3 { get; set; }
        public Decimal Berat { get; set; }
        public string Keterangan { get; set; }
    }
}
   
