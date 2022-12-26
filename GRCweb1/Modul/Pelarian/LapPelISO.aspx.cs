using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.Pelarian
{
    public partial class LapPelISO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            string strFirst = "1/1/" + DateTime.Now.Year.ToString();
            DateTime dateFirst = DateTime.Parse(strFirst);
            if (!Page.IsPostBack)
            {
                LoadPlanID();
                LoadTahun();
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                txtTahun.SelectedValue = DateTime.Now.Year.ToString();
                txtFromPostingPeriod.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedIndex)).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
                //CekPeriodPosting();

            }
        }


        private void LoadTahun()
        {
            ArrayList arrTh = new ArrayList();
            MasterTransaksiFacade transaksi = new MasterTransaksiFacade();
            arrTh = transaksi.GetYearTrans();
            txtTahun.Items.Clear();
            foreach (MasterTransaksi objSI in arrTh)
            {
                //txtTahun.Items.Add(new ListItem(objSI.Tahun.ToString(), objSI.Tahun.ToString()));
                txtTahun.Items.Add(new ListItem(objSI.Tahun.ToString(), objSI.Tahun.ToString()));
            }

        }
        private void LoadPlanID()
        {
            ArrayList arrPlanID = new ArrayList();
            MasterReguFacade masterreguFacade = new MasterReguFacade();
            arrPlanID = masterreguFacade.RetrievePlanID();
            ddlPlanID.Items.Add(new ListItem("--All Line--", string.Empty));
            foreach (MasterPlan masterPlan in arrPlanID)
            {
                ddlPlanID.Items.Add(new ListItem(masterPlan.PlanName, masterPlan.ID.ToString()));
            }
        }
        private void SelectPlanID(string strPlanID)
        {
            ddlPlanID.ClearSelection();
            foreach (ListItem item in ddlPlanID.Items)
            {
                if (item.Text == strPlanID)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        protected void TxtTahun_TextChanged(object sender, EventArgs e)
        {
            string strFirst = "1/1/" + txtTahun.Text;
            DateTime dateFirst = DateTime.Parse(strFirst);

            txtFromPostingPeriod.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedIndex)).Date.ToString("dd-MMM-yyyy");
            txtToPostingPeriod.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
        }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strFirst = "1/1/" + txtTahun.Text;
            DateTime dateFirst = DateTime.Parse(strFirst);

            txtFromPostingPeriod.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedIndex)).Date.ToString("dd-MMM-yyyy");
            txtToPostingPeriod.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
            //if (ddlBulan.SelectedIndex > 0)
            //{
            //    ddlBulan0.SelectedIndex = ddlBulan.SelectedIndex;
            //    txtTahun0.Text = txtTahun.Text;
            //}
            //else
            //{
            //    ddlBulan0.SelectedIndex = 12;
            //    txtTahun0.Text = (Convert.ToInt16(txtTahun.Text) - 1).ToString();
            //}
            //CekPeriodPosting();
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {


            if (ddlPlanID.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Line");
                return;
            }

            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");

            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;

            string strError = string.Empty;
            string plan = string.Empty;
            if (ddlPlanID.SelectedIndex > 0)
                plan = "and C.PlantName = '" + Convert.ToString(ddlPlanID.SelectedItem) + "'";
            Session["Query"] = null;
            Session["prdawal"] = null;
            Session["prdakhir"] = null;
            Session["xjudul"] = null;

            string strQuery = "select *, ([3a]+[3b]+[4a]+[4b]+[4c]+[5b]+[5c]+[6b]+[6c]+[7b]+[7c]+[8b]+[8c]+[9b]+[9c]+[10b]+[10c]+[11b]+[11c]+  " +
                "[12b]+[12c]+[13b]+[13c]+[14b]+[14c]+[15b]+[15c]+[16b]+[16c]) as Qty from ( " +
                "select PlantName,(left(NamaType,1) +' '+ Ukuran) as Ukuran,ReguCode, " +
                "SUM(round(isnull([3A],0),0))as [3A],SUM(round(isnull([3B],0),0)) as [3B],SUM(round(isnull([4A],0),0)) as [4A], " +
                "SUM(round(isnull([4B],0),0)) as [4B],SUM(round(isnull([4C],0),0)) as [4C],SUM(round(isnull([5B],0),0)) as [5B], " +
                "SUM(round(isnull([5C],0),0)) as [5C],SUM(round(isnull([6B],0),0)) as [6B],SUM(round(isnull([6C],0),0)) as [6C], " +
                "SUM(round(isnull([7B],0),0)) as [7B], SUM(round(isnull([7C],0),0)) as [7C],SUM(round(isnull([8B],0),0)) as [8B], " +
                "SUM(round(isnull([8C],0),0)) as [8C],SUM(round(isnull([9B],0),0)) as [9B],SUM(round(isnull([9C],0),0)) as [9C], " +
                "SUM(round(isnull([10B],0),0)) as [10B],SUM(round(isnull([10C],0),0)) as [10C],SUM(round(isnull([11B],0),0)) as [11B], " +
                "SUM(round(isnull([11C],0),0)) as [11C],SUM(round(isnull([12B],0),0)) as [12B],SUM( round(isnull([12C],0),0)) as [12C], " +
                "SUM(round(isnull([13B],0),0)) as [13B],SUM(round(isnull([13C],0),0)) as [13C],SUM(round(isnull([14B],0),0)) as [14B], " +
                "SUM(round(isnull([14C],0),0)) as [14C],SUM(round(isnull([15B],0),0)) as [15B],SUM(round(isnull([15C],0),0)) as [15C], " +
                "SUM(round(isnull([16B],0),0)) as [16B],SUM(round(isnull([16C],0),0)) as [16C],CONVERT(decimal(2,1),Tebal) as Tebal  " +
                "from (select * from( select A.KodePelarian as Kode, A.Jumlah,C.PlantName,A.ReguCode,A.Ukuran,A.NamaType,B.Tebal " +
                "from Pel_Transaksi as A, FC_Items  as B, BM_Plant  as C ,BM_PlantGroup D " +
                "where A.ReguCode = D.[Group] and C.ID=D.PlantID and A.Ukuran = B.partno  and A.RowStatus > -1 " + plan +
                "and A.TglTransaksi >= '" + periodeAwal + "' and A.TglTransaksi <= '" + periodeAkhir + "') as aa) up pivot  " +
                "(sum(Jumlah)for Kode in([3A] ,[3B],[4A],[4B],[4C],[5B],[5C],[6B],[6C],[7B],[7C],[8B],[8C],[9B],[9C],[10B], " +
                "[10C],[11B],[11C],[12B],[12C],[13B],[13C],[14B],[14C],[15B],[15C],[16B],[16C])) as pvt group by ReguCode,Ukuran,NamaType,PlantName,Tebal ) as bb ";
            Session["Query"] = strQuery;
            Session["prdawal"] = txPeriodeAwal;
            Session["prdakhir"] = txPeriodeAkhir;
            Session["xjudul"] = "LEMBAR PEMANTAUAN PELARIAN PRODUK";

            Cetak(this);

        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('Report.aspx?IdReport=LapPelISO', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}