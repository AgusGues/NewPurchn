using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Factory;
using BusinessFacade;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace GRCweb1.Modul.Factory
{
    public partial class OptimumStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                getYear();
                //txtdrtanggal.Text = "01-" + DateTime.Now.ToString("MMM-yyyy");
                //txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Now.AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd") + "-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 400, 100 , 20 ,false); </script>", false);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        }
        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddTahun.Items.Clear();
            ddTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (RBTglInput.Checked == true)
            //    LblPeriode.Text = "Tanggal Input";
            //if (RBTglProduksi.Checked == true)
            //    LblPeriode.Text = "Tanggal Produksi";
            //if (RBTglPotong.Checked == true)
            //    LblPeriode.Text = "Tanggal Potong";
            LblTgl1.Text = string.Empty;
            if (ddlBulan.SelectedValue != "0")
            {
                loadDynamicGrid();
                LblTgl1.Text = ddlBulan.SelectedItem.Text.Trim() + " " + ddTahun.SelectedItem.Text;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDynamicGrid()
        {
            string thnbln = string.Empty; string query = string.Empty;
            thnbln = ddTahun.SelectedItem.Text.Trim() + ddlBulan.SelectedValue.ToString().Trim();

            if (Convert.ToInt32(thnbln) >= 202106)
            {
                query = " where Min>0 or Max>0 ";
            }
            else
            {
                query = " ";
            }

            string strtanggal = string.Empty;
            if (RBTglInput.Checked == true)
                strtanggal = "tglInput";
            if (RBTglProduksi.Checked == true)
                strtanggal = "TglProd";
            if (RBTglPotong.Checked == true)
                strtanggal = "TglPeriksa";
            string strSQL = "declare @ThnBln char(6) " +
                "set @ThnBln='" + ddTahun.SelectedItem.Text.Trim() + ddlBulan.SelectedValue + "' " +
                "declare @Thn0 char(4) " +
                "declare @Bln0 char(6) " +
                "if right(@ThnBln,2)='01'  " +
                "begin set @Thn0=rtrim(cast( cast(left(@ThnBln,4) as int)-1 as char))  set @Bln0=12 end " +
                "else	begin set @Thn0=left(@ThnBln,4) set @Bln0=cast( cast(right(@ThnBln,2) as int)-1 as char)  end " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempOpt]') AND type in (N'U')) DROP TABLE [dbo].tempOpt " +
                "select REPLACE(STR(day(tanggal), 2), SPACE(1), '0') tgl,I.groupid,I.tebal,I.Lebar,I.Panjang,substring(I.partno,18,2) Sisi, sum(V.qty) qty  " +
                "into tempOpt from vw_KartuStockBJNew V inner join fc_items I on V.ItemID =I.ID and I.RowStatus>-1 and len(rtrim(I.partno))>=18 and I.PartNo like '%-3-%'  " +
                "where left(convert(char,tanggal,112),6)=@ThnBln group by I.groupid,I.tebal,I.Lebar,I.Panjang,substring(I.partno,18,2),day(tanggal) order by tgl,groupid,tebal,lebar,panjang " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempOpt2]') AND type in (N'U')) DROP TABLE [dbo].tempOpt2 " +

                "select * into tempOpt2 from ( select tgl,groupid,tebal,Lebar,Panjang,Sisi, qty from tempOpt )  " +
                "up pivot " +
                "(sum(qty) " +
                "  for tgl in ([01],[02],[03],[04],[05],[06],[07],[08],[09],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31]) " +
                ") piv order by groupid,tebal,lebar,panjang " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempOpt3]') AND type in (N'U')) DROP TABLE [dbo].tempOpt3 " +
                "select groupid,tebal,Lebar,Panjang,Sisi,isnull(sum(SA),0)SA ,isnull(sum([01]),0) as [1],isnull(sum([02]),0) as [2], " +
                "isnull(sum([03]),0) as [3],isnull(sum([04]),0) as [4],isnull(sum([05]),0) as [5],isnull(sum([06]),0) as [6],isnull(sum([07]),0) as [7], " +
                "isnull(sum([08]),0) as [8],isnull(sum([09]),0) as [9],isnull(sum([10]),0) as [10],isnull(sum([11]),0) as [11],isnull(sum([12]),0) as [12], " +
                "isnull(sum([13]),0) as [13],isnull(sum([14]),0) as [14],isnull(sum([15]),0) as [15],isnull(sum([16]),0) as [16],isnull(sum([17]),0) as [17], " +
                "isnull(sum([18]),0) as [18],isnull(sum([19]),0) as [19],isnull(sum([20]),0) as [20],isnull(sum([21]),0) as [21],isnull(sum([22]),0) as [22], " +
                "isnull(sum([23]),0) as [23],isnull(sum([24]),0) as [24],isnull(sum([25]),0) as [25],isnull(sum([26]),0) as [26],isnull(sum([27]),0) as [27], " +
                "isnull(sum([28]),0) as [28],isnull(sum([29]),0) as [29],isnull(sum([30]),0) as [30],isnull(sum([31]),0)as [31] into tempOpt3 from( " +
                "select  0 SA,* from tempOpt2 where groupid>0  " +
                "union all " +
                "select sum(V.stock) SA ,I.groupid,I.tebal,I.Lebar,I.Panjang,substring(I.partno,18,2) Sisi,0 [01],0 [02],0 [03],0 [04],0 [05],0 [06],0 [07], " +
                "0 [08],0 [09],0 [10],0 [11],0 [12],0 [13],0 [14],0 [15],0 [16],0 [17],0 [18],0 [19],0 [20],0 [21],0 [22],0 [23],0 [24],0 [25],0 [26],0 [27],0 [28],0 [29],0 [30],0 [31] " +
                "from vw_AwalStocknPriceBJ  V inner join fc_items I on V.ItemID =I.ID and I.RowStatus>-1 and len(rtrim(I.partno))>=18 and I.PartNo like '%-3-%' " +
                "where I.groupid>0 and tahun=@Thn0  and bulan=@Bln0  group by I.groupid,I.tebal,I.Lebar,I.Panjang,substring(I.partno,18,2))S group by groupid,tebal,Lebar,Panjang,Sisi " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempOpt4]') AND type in (N'U')) DROP TABLE [dbo].tempOpt4 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hasil]') AND type in (N'U')) DROP TABLE [dbo].Hasil " +
                "select SA,groupid,tebal,Lebar,Panjang,Sisi,SA+[1] [1],SA+[1]+[2][2],SA+[1]+[2]+[3][3],SA+[1]+[2]+[3]+[4][4],SA+[1]+[2]+[3]+[4]+[5][5],SA+[1]+[2]+[3]+[4]+[5]+[6][6], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7][7],SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8][8],SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9][9],SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10][10], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11][11],SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12][12],SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13][13], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14][14],SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15][15], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16][16],SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17][17], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18][18],SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19][19], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20][20], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21][21], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22][22], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23][23], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23]+[24][24], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23]+[24]+[25][25], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23]+[24]+[25]+[26][26], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23]+[24]+[25]+[26]+[27][27], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23]+[24]+[25]+[26]+[27]+[28][28], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23]+[24]+[25]+[26]+[27]+[28]+[29][29], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23]+[24]+[25]+[26]+[27]+[28]+[29]+[30][30], " +
                "SA+[1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23]+[24]+[25]+[26]+[27]+[28]+[29]+[30]+[31][31] into tempOpt4 from tempOpt3 " +
                "select Jenis_Produk,ukuran,[Min],[Max],  " +
                "case when [1]>=[Min] and [1] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'01')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'01')>0 then null else [1] end [1], " +
                "case when [2]>=[Min] and [2] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'02')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'02')>0 then null else [2] end [2],  " +
                "case when[3]>=[Min] and [3] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'03')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'03')>0 then null else [3] end [3], " +
                "case when[4]>=[Min] and [4] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'04')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'04')>0 then null else [4] end [4],  " +
                "case when[5]>=[Min] and [5] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'05')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'05')>0 then null else [5] end [5], " +
                "case when[6]>=[Min] and [6] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'06')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'06')>0 then null else [6] end [6],  " +
                "case when[7]>=[Min] and [7] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'07')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'07')>0 then null else [7] end [7], " +
                "case when[8]>=[Min] and [8] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'08')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'08')>0 then null else [8] end [8],  " +
                "case when[9]>=[Min] and [9] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'09')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'09')>0 then null else [9] end [9], " +
                "case when[10]>=[Min] and [10] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'10')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'10')>0 then null else [10] end [10], " +
                "case when[11]>=[Min] and [11] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'11')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'11')>0then null else [11] end [11], " +
                "case when[12]>=[Min] and [12] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'12')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'12')>0 then null else [12] end [12],  " +
                "case when[13]>=[Min] and [13] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'13')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'13')>0 then null else [13] end [13], " +
                "case when[14]>=[Min] and [14] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'14')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'14')>0 then null else [14] end [14],  " +
                "case when[15]>=[Min] and [15] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'15')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'15')>0 then null else [15] end [15], " +
                "case when[16]>=[Min] and [16] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'16')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'16')>0 then null else [16] end [16],  " +
                "case when[17]>=[Min] and [17] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'17')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'17')>0 then null else [17] end [17], " +
                "case when[18]>=[Min] and [18] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'18')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'18')>0 then null else [18] end [18],  " +
                "case when[19]>=[Min] and [19] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'19')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'19')>0 then null else [19] end [19], " +
                "case when[20]>=[Min] and [20] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'20')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'20')>0 then null else [20] end [20],  " +
                "case when[21]>=[Min] and [21] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'21')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'21')>0 then null else [21] end [21], " +
                "case when[22]>=[Min] and [22] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'22')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'22')>0 then null else [22] end [22],  " +
                "case when[23]>=[Min] and [23] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'23')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'23')>0 then null else [23] end [23], " +
                "case when[24]>=[Min] and [24] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'24')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'24')>0 then null else [24] end [24],  " +
                "case when[25]>=[Min] and [25] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'25')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'25')>0 then null else [25] end [25], " +
                "case when[26]>=[Min] and [26] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'26')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'26')>0 then null else [26] end [26],  " +
                "case when[27]>=[Min] and [27] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'27')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'27')>0 then null else [27] end [27], " +
                "case when[28]>=[Min] and [28] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'28')) in(1,7) or (select count(ID) from CalenderOffDay where harilibur=@ThnBln +'28')>0 then null else [28] end [28],  " +
                "case when (SELECT right(rtrim(convert(char, EOMONTH (@ThnBln +'01' ) ,112)),2)-29) >= 0 then case when [29]>=[Min] and [29] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'29')) in(1,7) or  " +
                "(select count(ID) from CalenderOffDay where harilibur=@ThnBln +'29')>0 then null else [29] end else null end[29], " +
                "case when (SELECT right(rtrim(convert(char, EOMONTH (@ThnBln +'01' ) ,112)),2)-30) >= 0 then case when [30]>=[Min] and [30] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'30')) in(1,7) or  " +
                "(select count(ID) from CalenderOffDay where harilibur=@ThnBln +'30')>0 then null else [30] end else null end [30],  " +
                "case when (SELECT right(rtrim(convert(char, EOMONTH (@ThnBln +'01' ) ,112)),2)-31) >= 0 then case when [31]>=[Min] and [31] <= [max] or (select DATEPART(WEEKDAY,@ThnBln +'31')) in(1,7) or  " +
                "(select count(ID) from CalenderOffDay where harilibur=@ThnBln +'31')>0 then null else [31] end else null end [31]  into Hasil   from ( " +
                "SELECT groupname Jenis_Produk,ukuran,[Min],[Max],isnull(sum([1]),0) as [1],isnull(sum([2]),0) as [2], " +
                "isnull(sum([3]),0) as [3],isnull(sum([4]),0) as [4],isnull(sum([5]),0) as [5],isnull(sum([6]),0) as [6],isnull(sum([7]),0) as [7], " +
                "isnull(sum([8]),0) as [8],isnull(sum([9]),0) as [9],isnull(sum([10]),0) as [10],isnull(sum([11]),0) as [11],isnull(sum([12]),0) as [12], " +
                "isnull(sum([13]),0) as [13],isnull(sum([14]),0) as [14],isnull(sum([15]),0) as [15],isnull(sum([16]),0) as [16],isnull(sum([17]),0) as [17], " +
                "isnull(sum([18]),0) as [18],isnull(sum([19]),0) as [19],isnull(sum([20]),0) as [20],isnull(sum([21]),0) as [21],isnull(sum([22]),0) as [22], " +
                "isnull(sum([23]),0) as [23],isnull(sum([24]),0) as [24],isnull(sum([25]),0) as [25],isnull(sum([26]),0) as [26],isnull(sum([27]),0) as [27], " +
                "isnull(sum([28]),0) as [28],isnull(sum([29]),0) as [29],isnull(sum([30]),0) as [30],isnull(sum([31]),0)as [31] FROM ( " +
                "select groupname ,rtrim(cast(A.tebal as char) )+' MM ' + rtrim(cast(A.Lebar as char))+'X' +rtrim(cast(A.Panjang as char))+rtrim(A.sisi) ukuran ,A.[Min],A.[Max], " +
                "[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31] " +
                "from MASTeroptstockprd A inner join tempOpt4 I on I.GroupID =A.GroupID and I.tebal=A.tebal and I.Lebar=A.Lebar and I.Panjang=A.Panjang and I.sisi=A.sisi )s1 " +
                "group by  groupname,ukuran,[min],[max] " +
                ")s2 order by Jenis_Produk,ukuran,[min],[max] " +

                //"select * from ( "+
                "select Jenis_Produk,ukuran,[Min],[Max],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31] from Hasil " + query + " " +
                "union all " +
                "select '{Total}'Jenis_Produk,''ukuran,null [Min],null [Max],count([1]),count([2]),count([3]),count([4]),count([5]),count([6]),count([7]),count([8]),count([9]),count([10]),count([11]),count([12]),count([13]), " +
                "count([14]),count([15]),count([16]),count([17]),count([18]),count([19]),count([20]),count([21]),count([22]),count([23]),count([24]),count([25]),count([26]),count([27]),count([28]), " +
                "count([29]),count([30]),count([31]) from Hasil " + query + " ";
            //") as x where Min>0 or Max > 0 ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                bfield.DataFormatString = "{0:N0}";
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                //    bfield.HeaderText = col.ColumnName;
                //}
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select count(Jenis_Produk)totalitem from Hasil " + query + " ";
            SqlDataReader sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Session["totItem"] = sdr["totalitem"].ToString();
                }
            }
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select count([1])+count([2])+count([3])+count([4])+count([5])+count([6])+count([7])+count([8])+count([9])+count([10])+count([11])+count([12])+count([13])+ " +
                "count([14])+count([15])+count([16])+count([17])+count([18])+count([19])+count([20])+count([21])+count([22])+count([23])+count([24])+count([25])+count([26])+count([27])+count([28])+ " +
                "count([29])+count([30])+count([31])totOpt from Hasil " + query + " " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempOpt]') AND type in (N'U')) DROP TABLE [dbo].tempOpt " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempOpt2]') AND type in (N'U')) DROP TABLE [dbo].tempOpt2 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempOpt3]') AND type in (N'U')) DROP TABLE [dbo].tempOpt3 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempOpt4]') AND type in (N'U')) DROP TABLE [dbo].tempOpt4 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hasil]') AND type in (N'U')) DROP TABLE [dbo].Hasil ";
            sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Session["totOpt"] = sdr["totOpt"].ToString();
                }
            }
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @ThnBln char(6) set @ThnBln='" + ddTahun.SelectedItem.Text.Trim() + ddlBulan.SelectedValue + "' declare @JmlH int set @JmlH =cast(right(rtrim((select convert(char, EOMONTH (@ThnBln +'01' ),112))) ,2) as int) " +
                "declare @lbr int set @lbr=(select dbo.getoffday(@ThnBln +'01',rtrim((select convert(char, EOMONTH (@ThnBln +'01' ),112))))) declare @HK int set @HK=@JmlH-@lbr select @HK HK";
            sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Session["totHK"] = sdr["HK"].ToString();
                }
            }
            Label3.Text = Session["totHK"].ToString();
            Label4.Text = Session["totItem"].ToString();
            Label5.Text = Session["totOpt"].ToString();
            Label6.Text = Session["totHK"].ToString();
            Label7.Text = Session["totItem"].ToString();
            Label8.Text = ((Convert.ToInt32(Label3.Text) * Convert.ToInt32(Label4.Text)) - Convert.ToInt32(Label5.Text)).ToString();
            Label9.Text = (Convert.ToInt32(Label3.Text) * Convert.ToInt32(Label4.Text)).ToString();
            decimal persen = (Convert.ToDecimal(Label8.Text) / Convert.ToDecimal(Label9.Text)) * 100;
            Label10.Text = persen.ToString("N2");

            //  add by Razib 15-07-2021 WO-IT-K0030721 2
            #region update sarmut Optimum Stock PPIC
            //decimal actual = 0;
            string sarmutPrs = "Pemenuhan optimum stock barang jadi";
            string strJmlLine = string.Empty;
            string strDept = string.Empty;
            int deptid = getDeptID("PPIC");
            ZetroView zlx = new ZetroView();
            zlx.QueryType = Operation.CUSTOM;
            zlx.CustomQuery = "update SPD_TransPrs set actual= " + Label10.Text.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            SqlDataReader sdrx = zlx.Retrieve();

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

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "List Defect Per Line.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            {
                GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            Panel1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void txtdrtanggal_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtsdtanggal_TextChanged(object sender, EventArgs e)
        {

        }
        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
        protected void GridView1_DataBinding(object sender, EventArgs e)
        {

        }
    }
}