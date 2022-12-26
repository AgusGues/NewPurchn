using BusinessFacade;
using DataAccessLayer;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LSiapKirim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                
                LoadGroupMkt();
                SetUkuran();

                StockMKTFacade sf = new StockMKTFacade();
                StockMKT sd = new StockMKT();

                int Lock = sf.RetrieveLock();

                if (Lock > 0)
                {
                    RBMKT.Visible = false; RBPRM.Visible = false;
                }
                else
                {
                    RBMKT.Visible = true; RBPRM.Visible = true;
                }

                loadDynamicGrid();
                loadDynamicGrid0();
                loadDynamicGrid1();

            }
        //((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(Button2);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 400, 100 , 21 ,false); </script>", false);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            loadDynamicGrid();
            loadDynamicGrid0();
            loadDynamicGrid1();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDynamicGrid()
        {
            Users user = (Users)Session["Users"];
            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 7 || plant == 13)
                strplant = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
            else
                strplant = "";
            string strSQL = string.Empty;
            string kwalitas = string.Empty;
            if (RBPBJ.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%' or i.PartNo like '%-W-%' or i.PartNo like '%-M-%')";
            if (RBPBP.Checked == true)
                kwalitas = " and (i.PartNo like '%-P-%')";
            if (RBPBS.Checked == true)
                kwalitas = " and (i.PartNo like '%-S-%')";
            if (RBPEFO.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%')";
            string bygroup = string.Empty;
            if (ddlGroup.SelectedIndex == 0)
                bygroup = "";
            else
                bygroup = " and I.GroupID in (select ID from " + strplant + "t3_groupm where grouprsid in (select id from "
                    + strplant + "t3_groupreadystock where groupname='" + ddlGroup.SelectedItem.Text + "'))";
            string efo = string.Empty;
            if (RBPEFO.Checked == true)
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,7) not in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";
            else
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,7) in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";

            string bytebal = string.Empty;
            if (RBPBP.Checked == true || RBPBS.Checked == true)
                efo = bygroup;

            if (chkTebal.Checked == true || ddlTebal1.Enabled==false || ddlTebal2.Enabled == false || ddlTebal1.SelectedValue == "0"  || ddlTebal2.SelectedValue=="0" )
                bytebal = "";
            else
                bytebal = " and I.tebal >= " + ddlTebal1.SelectedValue + " and I.tebal<= " + ddlTebal2.SelectedValue + " ";
            string byLebar = string.Empty;
            if (chkLebar.Checked == true || ddlLebar1.Enabled==false || ddlLebar2.Enabled == false || ddlLebar1.SelectedValue =="0" || ddlLebar2.SelectedValue == "0")
                byLebar = "";
            else
                byLebar = " and I.Lebar >= " + ddlLebar1.SelectedValue + " and I.Lebar<= " + ddlLebar2.SelectedValue + " ";
            string byPanjang = string.Empty;
            if (chkPanjang.Checked == true || ddlPanjang1.Enabled==false || ddlPanjang2.Enabled == false || ddlPanjang1.SelectedValue=="0" || ddlPanjang2.SelectedValue == "0")
                byPanjang = "";
            else
                byPanjang = " and I.Panjang >= " + ddlPanjang1.SelectedValue + " and I.Panjang<= " + ddlPanjang2.SelectedValue + " ";

            strSQL = "select ROW_NUMBER() OVER (ORDER BY grup ,tebal,lebar,panjang ,ukuran) AS No,  " +
                "grup Produk, tebal,Ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup +  ' ' +KW +' '  grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' " +
                "when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW,SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char))+' ' + SUBSTRING(I.partno,18,7) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi not in (select lokasi from " + strplant + "fc_lokasihidefrommkt where rowstatus>-1) and " +
                "S.qty>0 " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang)A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang   " +
                "union all  " +
                "select 1000 No,'Total Siap Kirim ' grup,0 tebal,'' ukuran,sum(Lembar)Lembar,sum(MeterKubik) MeterKubik from (  " +
                "select grup,tebal,ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup + ' ' +KW +' ' +sisi grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from" + strplant + " T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW," +
                "SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char)) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi not in (select lokasi from " + strplant + "fc_lokasihidefrommkt where rowstatus>-1) and " +
                "S.qty>0  " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang )A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang )C  order by No";
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
                if (col.ColumnName == "MeterKubik")
                {
                    bfield.HeaderText = "MeterKubik";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                else
                {
                    //bfield.DataFormatString = "{0:N0}";
                    bfield.HeaderText = col.ColumnName;
                }
                if (col.ColumnName == "Lembar")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                if (col.ColumnName == "Ukuran")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "tebal")
                {
                    bfield.HeaderText = "Tebal";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }
        private void loadDynamicGrid0()
        {
            Users user = (Users)Session["Users"];
            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 1 || plant == 13)
                strplant = " [sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            else
                strplant = "";

            string strSQL = string.Empty;
            string kwalitas = string.Empty;
            if (RBPBJ.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%' or i.PartNo like '%-W-%'  or i.PartNo like '%-M-%')";
            if (RBPBP.Checked == true)
                kwalitas = " and (i.PartNo like '%-P-%')";
            if (RBPBS.Checked == true)
                kwalitas = " and (i.PartNo like '%-S-%')";
            if (RBPEFO.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%')";
            string bygroup = string.Empty;
            if (ddlGroup.SelectedIndex == 0)
                bygroup = "";
            else
                bygroup = " and I.GroupID in (select ID from " + strplant + "t3_groupm where grouprsid in (select id from "
                    + strplant + "t3_groupreadystock where groupname='" + ddlGroup.SelectedItem.Text + "'))";
            string efo = string.Empty;
            if (RBPEFO.Checked == true)
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,5) not in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";
            else
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,5) in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";
            if (RBPBP.Checked == true || RBPBS.Checked == true)
                efo = bygroup;
            string bytebal = string.Empty;

            if (chkTebal.Checked == true || ddlTebal1.Enabled == false || ddlTebal2.Enabled == false || ddlTebal1.SelectedValue == "0" || ddlTebal2.SelectedValue == "0")
                bytebal = "";
            else
                bytebal = " and I.tebal >= " + ddlTebal1.SelectedValue + " and I.tebal<= " + ddlTebal2.SelectedValue + " ";
            string byLebar = string.Empty;
            if (chkLebar.Checked == true || ddlLebar1.Enabled == false || ddlLebar2.Enabled == false || ddlLebar1.SelectedValue == "0" || ddlLebar2.SelectedValue == "0")
                byLebar = "";
            else
                byLebar = " and I.Lebar >= " + ddlLebar1.SelectedValue + " and I.Lebar<= " + ddlLebar2.SelectedValue + " ";
            string byPanjang = string.Empty;
            if (chkPanjang.Checked == true || ddlPanjang1.Enabled == false || ddlPanjang2.Enabled == false || ddlPanjang1.SelectedValue == "0" || ddlPanjang2.SelectedValue == "0")
                byPanjang = "";
            else
                byPanjang = " and I.Panjang >= " + ddlPanjang1.SelectedValue + " and I.Panjang<= " + ddlPanjang2.SelectedValue + " ";

            strSQL = "select ROW_NUMBER() OVER (ORDER BY grup ,tebal,lebar,panjang ,ukuran) AS No,  " +
                "grup Produk, tebal,Ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup +  ' ' +KW +' '  grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' " +
                "when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW,SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char))+' ' + SUBSTRING(I.partno,18,4) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi not in (select lokasi from " + strplant + "fc_lokasihidefrommkt where rowstatus>-1) and " +
                "S.qty>0 " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang)A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang   " +
                "union all  " +
                "select 1000 No,'Total Siap Kirim ' grup,0 tebal,'' ukuran,sum(Lembar)Lembar,sum(MeterKubik) MeterKubik from (  " +
                "select grup,tebal,ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup + ' ' +KW +' ' +sisi grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from" + strplant + " T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW," +
                "SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char)) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi not in (select lokasi from " + strplant + "fc_lokasihidefrommkt where rowstatus>-1) and " +
                "S.qty>0  " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang )A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang )C  order by No";
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
            GrdDynamic0.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                if (col.ColumnName == "MeterKubik")
                {
                    bfield.HeaderText = "MeterKubik";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                else
                {
                    //bfield.DataFormatString = "{0:N0}";
                    bfield.HeaderText = col.ColumnName;
                }
                if (col.ColumnName == "Lembar")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                if (col.ColumnName == "Ukuran")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "tebal")
                {
                    bfield.HeaderText = "Tebal";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                GrdDynamic0.Columns.Add(bfield);
            }
            GrdDynamic0.DataSource = dt;
            GrdDynamic0.DataBind();
        }
        private void loadDynamicGrid1()
        {
            Users user = (Users)Session["Users"];
            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 1 || plant == 7)
                strplant = " [sqlJombang.grcboard.com].bpasJombang.dbo.";
            else
                strplant = "";

            string strSQL = string.Empty;
            string kwalitas = string.Empty;
            if (RBPBJ.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%' or i.PartNo like '%-W-%'  or i.PartNo like '%-M-%')";
            if (RBPBP.Checked == true)
                kwalitas = " and (i.PartNo like '%-P-%')";
            if (RBPBS.Checked == true)
                kwalitas = " and (i.PartNo like '%-S-%')";
            if (RBPEFO.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%')";
            string bygroup = string.Empty;
            if (ddlGroup.SelectedIndex == 0)
                bygroup = "";
            else
                bygroup = " and I.GroupID in (select ID from " + strplant + "t3_groupm where grouprsid in (select id from "
                    + strplant + "t3_groupreadystock where groupname='" + ddlGroup.SelectedItem.Text + "'))";
            string efo = string.Empty;
            if (RBPEFO.Checked == true)
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,5) not in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";
            else
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,5) in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";
            if (RBPBP.Checked == true || RBPBS.Checked == true)
                efo = bygroup;
            string bytebal = string.Empty;

            if (chkTebal.Checked == true || ddlTebal1.Enabled == false || ddlTebal2.Enabled == false || ddlTebal1.SelectedValue == "0" || ddlTebal2.SelectedValue == "0")
                bytebal = "";
            else
                bytebal = " and I.tebal >= " + ddlTebal1.SelectedValue + " and I.tebal<= " + ddlTebal2.SelectedValue + " ";
            string byLebar = string.Empty;
            if (chkLebar.Checked == true || ddlLebar1.Enabled == false || ddlLebar2.Enabled == false || ddlLebar1.SelectedValue == "0" || ddlLebar2.SelectedValue == "0")
                byLebar = "";
            else
                byLebar = " and I.Lebar >= " + ddlLebar1.SelectedValue + " and I.Lebar<= " + ddlLebar2.SelectedValue + " ";
            string byPanjang = string.Empty;
            if (chkPanjang.Checked == true || ddlPanjang1.Enabled == false || ddlPanjang2.Enabled == false || ddlPanjang1.SelectedValue == "0" || ddlPanjang2.SelectedValue == "0")
                byPanjang = "";
            else
                byPanjang = " and I.Panjang >= " + ddlPanjang1.SelectedValue + " and I.Panjang<= " + ddlPanjang2.SelectedValue + " ";

            strSQL = "select ROW_NUMBER() OVER (ORDER BY grup ,tebal,lebar,panjang ,ukuran) AS No,  " +
                "grup Produk, tebal,Ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup +  ' ' +KW +' '  grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' " +
                "when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW,SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char))+' ' + SUBSTRING(I.partno,18,4) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi not in (select lokasi from " + strplant + "fc_lokasihidefrommkt where rowstatus>-1) and " +
                "S.qty>0 " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang)A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang   " +
                "union all  " +
                "select 1000 No,'Total Siap Kirim ' grup,0 tebal,'' ukuran,sum(Lembar)Lembar,sum(MeterKubik) MeterKubik from (  " +
                "select grup,tebal,ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup + ' ' +KW +' ' +sisi grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from" + strplant + " T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW," +
                "SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char)) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi not in (select lokasi from " + strplant + "fc_lokasihidefrommkt where rowstatus>-1) and " +
                "S.qty>0  " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang )A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang )C  order by No";
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
            GrdDynamic1.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                if (col.ColumnName == "MeterKubik")
                {
                    bfield.HeaderText = "MeterKubik";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                else
                {
                    //bfield.DataFormatString = "{0:N0}";
                    bfield.HeaderText = col.ColumnName;
                }
                if (col.ColumnName == "Lembar")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                if (col.ColumnName == "Ukuran")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "tebal")
                {
                    bfield.HeaderText = "Tebal";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                GrdDynamic1.Columns.Add(bfield);
            }
            GrdDynamic1.DataSource = dt;
            GrdDynamic1.DataBind();
        }

        //Tambahan
        // Data Citeureup
        private void loadDynamicGridMKT()
        {
            Users user = (Users)Session["Users"];
            int Bulan = DateTime.Now.Month; int Tahun = DateTime.Now.Year;

            if (Bulan == 1)
            { string SA = "DesQty"; Session["SA"] = SA; int TahunSA = Tahun - 1; Session["Tahun"] = TahunSA.ToString(); }
            else if (Bulan == 2) { string SA = "JanQty"; Session["SA"] = SA; }
            else if (Bulan == 3) { string SA = "FebQty"; Session["SA"] = SA; }
            else if (Bulan == 4) { string SA = "MarQty"; Session["SA"] = SA; }
            else if (Bulan == 5) { string SA = "AprQty"; Session["SA"] = SA; }
            else if (Bulan == 6) { string SA = "MeiQty"; Session["SA"] = SA; }
            else if (Bulan == 7) { string SA = "JunQty"; Session["SA"] = SA; }
            else if (Bulan == 8) { string SA = "JulQty"; Session["SA"] = SA; }
            else if (Bulan == 9) { string SA = "AguQty"; Session["SA"] = SA; }
            else if (Bulan == 10) { string SA = "SepQty"; Session["SA"] = SA; }
            else if (Bulan == 11) { string SA = "OktQty"; Session["SA"] = SA; }
            else if (Bulan == 12) { string SA = "NovQty"; Session["SA"] = SA; }

            if (Bulan == 1)
            { string TahunSaldo = Session["Tahun"].ToString(); Session["ThnSA"] = TahunSaldo; }
            else { string TahunSaldo = Tahun.ToString(); Session["ThnSA"] = TahunSaldo; }

            if (Bulan.ToString().Length == 1)
            {
                string YM = Tahun.ToString().Trim() + "0" + Bulan.ToString().Trim(); Session["YM"] = YM;
            }
            else
            {
                string YM = Tahun.ToString().Trim() + Bulan.ToString().Trim(); Session["YM"] = YM;
            }

            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 1)
                strplant = "";
            else
                strplant = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
            string strSQL = string.Empty;

            strSQL =
            " with DataAwal as (select ItemID,Qty from (select ItemID,SUM(Qty)Qty from ( " +
            //Saldo awal bulan
            " select ItemID," + Session["SA"].ToString() + " Qty from " + strplant + "SaldoInventory where ItemTypeID=1 and GroupID in (10,13,14) and YearPeriod=" + Session["ThnSA"].ToString() + " " +
            " union all " +
            //Data transaksi
            " select ItemID,SUM(quantity)Qty from " + strplant + "vw_StockPurchn where GroupID in (10,13,14) and YM='" + Session["YM"].ToString() + "' and ItemTypeID=1 group by ItemID  " +
            " union all " +
            //Data SPB transit ( Belum approved Gudang )
            " select ItemID,-1 * SUM(Quantity)Qty from " + strplant + "PakaiDetail where GroupID in (10,13,14) and PakaiID in (select ID from " + strplant + "Pakai " +
            " where Status>-1 and Status<2 and LEFT(convert(char,pakaidate,112),6)='" + Session["YM"].ToString() + "') group by ItemID " +
            " union all " +
            //Data Bookingan
            " select ItemID, -1 *sum(QtyBooking-QtyAmbil) Qty from " + strplant + "StockTemp_BrgMKT where ItemID in (select ID from " + strplant + "Inventory where GroupID in (10,13,14) and Aktif=1) and Rowstatus>-1 group by ItemID  " +
            ") as xx  group by ItemID ) as xx2 " +
            " where Qty>0) " +

            " select ROW_NUMBER() over (order by B.Itemcode,B.ItemName asc) as No,B.ItemCode KodeBarang,B.ItemName NamaBarang,A.Qty,C.UOMDesc Satuan " +
            " from DataAwal A " +
            " INNER JOIN " + strplant + "Inventory B ON A.ItemID=B.ID " +
            " INNER JOIN " + strplant + "UOM C ON B.UOMID=C.ID order by B.Itemcode,B.ItemName asc ";

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
            GrdDynamicMKT.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                if (col.ColumnName == "No")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "KodeBarang")
                {
                    bfield.HeaderText = "Kode Barang";
                }
                if (col.ColumnName == "NamaBarang")
                {
                    bfield.HeaderText = "Nama Barang";
                }
                if (col.ColumnName == "Qty")
                {
                    bfield.HeaderText = "Stok";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N0}";
                }
                GrdDynamicMKT.Columns.Add(bfield);
            }
            GrdDynamicMKT.DataSource = dt;
            GrdDynamicMKT.DataBind();
        }
        private void loadDynamicGridMKT2()
        {
            Users user = (Users)Session["Users"];

            int Bulan = DateTime.Now.Month; int Tahun = DateTime.Now.Year;

            if (Bulan == 1)
            { string SA = "DesQty"; Session["SA"] = SA; int TahunSA = Tahun - 1; Session["Tahun"] = TahunSA.ToString(); }
            else if (Bulan == 2) { string SA = "JanQty"; Session["SA"] = SA; }
            else if (Bulan == 3) { string SA = "FebQty"; Session["SA"] = SA; }
            else if (Bulan == 4) { string SA = "MarQty"; Session["SA"] = SA; }
            else if (Bulan == 5) { string SA = "AprQty"; Session["SA"] = SA; }
            else if (Bulan == 6) { string SA = "MeiQty"; Session["SA"] = SA; }
            else if (Bulan == 7) { string SA = "JunQty"; Session["SA"] = SA; }
            else if (Bulan == 8) { string SA = "JulQty"; Session["SA"] = SA; }
            else if (Bulan == 9) { string SA = "AguQty"; Session["SA"] = SA; }
            else if (Bulan == 10) { string SA = "SepQty"; Session["SA"] = SA; }
            else if (Bulan == 11) { string SA = "OktQty"; Session["SA"] = SA; }
            else if (Bulan == 12) { string SA = "NovQty"; Session["SA"] = SA; }

            if (Bulan == 1)
            { string TahunSaldo = Session["Tahun"].ToString(); Session["ThnSA"] = TahunSaldo; }
            else { string TahunSaldo = Tahun.ToString(); Session["ThnSA"] = TahunSaldo; }

            if (Bulan.ToString().Length == 1)
            {
                string YM = Tahun.ToString().Trim() + "0" + Bulan.ToString().Trim(); Session["YM"] = YM;
            }
            else
            {
                string YM = Tahun.ToString().Trim() + Bulan.ToString().Trim(); Session["YM"] = YM;
            }

            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 7)
                strplant = "";
            else
                strplant = " [sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            string strSQL = string.Empty;

            strSQL =
            " with DataAwal as (select ItemID,Qty from (select ItemID,SUM(Qty)Qty from ( " +
            //Saldo awal bulan
            " select ItemID," + Session["SA"].ToString() + " Qty from " + strplant + "SaldoInventory where ItemTypeID=1 and GroupID in (10,13,14) and YearPeriod=" + Session["ThnSA"] + " " +
            " union all " +
            //Data transaksi
            " select ItemID,SUM(quantity)Qty from " + strplant + "vw_StockPurchn where GroupID in (10,13,14) and YM='" + Session["YM"].ToString() + "' and ItemTypeID=1 group by ItemID  " +
            " union all " +
            //Data SPB transit ( Belum approved Gudang )
            " select ItemID,-1 * SUM(Quantity)Qty from " + strplant + "PakaiDetail where GroupID in (10,13,14) and PakaiID in (select ID from " + strplant + "Pakai " +
            " where Status>-1 and Status<2 and LEFT(convert(char,pakaidate,112),6)='" + Session["YM"].ToString() + "') group by ItemID " +
            " union all" +
            //Data Bookingan
            " select ItemID, -1 *sum(QtyBooking-QtyAmbil) Qty from " + strplant + "StockTemp_BrgMKT where ItemID in (select ID from " + strplant + "Inventory where GroupID in (10,13,14) and Aktif=1) and Rowstatus>-1 group by ItemID  " +
            ") as xx  group by ItemID ) as xx2 " +
            " where Qty>0) " +

            " select ROW_NUMBER() over (order by B.ItemName asc) as No,B.ItemCode KodeBarang,B.ItemName NamaBarang,A.Qty,C.UOMDesc Satuan " +
            " from DataAwal A " +
            " INNER JOIN " + strplant + "Inventory B ON A.ItemID=B.ID " +
            " INNER JOIN " + strplant + "UOM C ON B.UOMID=C.ID order by B.ItemName asc ";

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
            GrdDynamicMKT2.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;

                if (col.ColumnName == "No")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "KodeBarang")
                {
                    bfield.HeaderText = "Kode Barang";
                }
                if (col.ColumnName == "NamaBarang")
                {
                    bfield.HeaderText = "Nama Barang";
                }
                if (col.ColumnName == "Qty")
                {
                    bfield.HeaderText = "Stok";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N0}";
                }
                GrdDynamicMKT2.Columns.Add(bfield);
            }
            GrdDynamicMKT2.DataSource = dt;
            GrdDynamicMKT2.DataBind();
        }
        private void loadDynamicGridMKT3()
        {
            Users user = (Users)Session["Users"];

            int Bulan = DateTime.Now.Month; int Tahun = DateTime.Now.Year;

            if (Bulan == 1)
            { string SA = "DesQty"; Session["SA"] = SA; int TahunSA = Tahun - 1; Session["Tahun"] = TahunSA.ToString(); }
            else if (Bulan == 2) { string SA = "JanQty"; Session["SA"] = SA; }
            else if (Bulan == 3) { string SA = "FebQty"; Session["SA"] = SA; }
            else if (Bulan == 4) { string SA = "MarQty"; Session["SA"] = SA; }
            else if (Bulan == 5) { string SA = "AprQty"; Session["SA"] = SA; }
            else if (Bulan == 6) { string SA = "MeiQty"; Session["SA"] = SA; }
            else if (Bulan == 7) { string SA = "JunQty"; Session["SA"] = SA; }
            else if (Bulan == 8) { string SA = "JulQty"; Session["SA"] = SA; }
            else if (Bulan == 9) { string SA = "AguQty"; Session["SA"] = SA; }
            else if (Bulan == 10) { string SA = "SepQty"; Session["SA"] = SA; }
            else if (Bulan == 11) { string SA = "OktQty"; Session["SA"] = SA; }
            else if (Bulan == 12) { string SA = "NovQty"; Session["SA"] = SA; }

            if (Bulan == 1)
            { string TahunSaldo = Session["Tahun"].ToString(); Session["ThnSA"] = TahunSaldo; }
            else { string TahunSaldo = Tahun.ToString(); Session["ThnSA"] = TahunSaldo; }

            if (Bulan.ToString().Length == 1)
            {
                string YM = Tahun.ToString().Trim() + "0" + Bulan.ToString().Trim(); Session["YM"] = YM;
            }
            else
            {
                string YM = Tahun.ToString().Trim() + Bulan.ToString().Trim(); Session["YM"] = YM;
            }

            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 13)
                strplant = "";
            else
                strplant = " [sqlJombang.grcboard.com].bpasJombang.dbo.";
            string strSQL = string.Empty;

            strSQL =
            " with DataAwal as (select ItemID,Qty from (select ItemID,SUM(Qty)Qty from ( " +
            //Saldo awal bulan
            " select ItemID," + Session["SA"].ToString() + " Qty from " + strplant + "SaldoInventory where ItemTypeID=1 and GroupID in (10,13,14) and YearPeriod=" + Session["ThnSA"] + " " +
            " union all " +
            //Data transaksi
            " select ItemID,SUM(quantity)Qty from " + strplant + "vw_StockPurchn where GroupID in (10,13,14) and YM='" + Session["YM"].ToString() + "' and ItemTypeID=1 group by ItemID  " +
            " union all " +
            //Data SPB transit ( Belum approved Gudang )
            " select ItemID,-1 * SUM(Quantity)Qty from " + strplant + "PakaiDetail where GroupID in (10,13,14) and PakaiID in (select ID from " + strplant + "Pakai " +
            " where Status>-1 and Status<2 and LEFT(convert(char,pakaidate,112),6)='" + Session["YM"].ToString() + "') group by ItemID " +
            " union all" +
            //Data Bookingan
            " select ItemID, -1 *sum(QtyBooking-QtyAmbil) Qty from " + strplant + "StockTemp_BrgMKT where ItemID in (select ID from " + strplant + "Inventory where GroupID in (10,13,14) and Aktif=1) and Rowstatus>-1 group by ItemID  " +
            ") as xx  group by ItemID ) as xx2 " +
            " where Qty>0) " +

            " select ROW_NUMBER() over (order by B.ItemName asc) as No,B.ItemCode KodeBarang,B.ItemName NamaBarang,A.Qty,C.UOMDesc Satuan " +
            " from DataAwal A " +
            " INNER JOIN " + strplant + "Inventory B ON A.ItemID=B.ID " +
            " INNER JOIN " + strplant + "UOM C ON B.UOMID=C.ID order by B.ItemName asc ";

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
            GrdDynamicMKT3.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;

                if (col.ColumnName == "No")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "KodeBarang")
                {
                    bfield.HeaderText = "Kode Barang";
                }
                if (col.ColumnName == "NamaBarang")
                {
                    bfield.HeaderText = "Nama Barang";
                }
                if (col.ColumnName == "Qty")
                {
                    bfield.HeaderText = "Stok";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N0}";
                }
                GrdDynamicMKT3.Columns.Add(bfield);
            }
            GrdDynamicMKT3.DataSource = dt;
            GrdDynamicMKT3.DataBind();
        }

        private void loadDynamicGridPromo()
        {
            Users user = (Users)Session["Users"];

            int Bulan = DateTime.Now.Month; int Tahun = DateTime.Now.Year;

            if (Bulan == 1)
            { string SA = "DesQty"; Session["SA"] = SA; int TahunSA = Tahun - 1; Session["Tahun"] = TahunSA.ToString(); }
            else if (Bulan == 2) { string SA = "JanQty"; Session["SA"] = SA; }
            else if (Bulan == 3) { string SA = "FebQty"; Session["SA"] = SA; }
            else if (Bulan == 4) { string SA = "MarQty"; Session["SA"] = SA; }
            else if (Bulan == 5) { string SA = "AprQty"; Session["SA"] = SA; }
            else if (Bulan == 6) { string SA = "MeiQty"; Session["SA"] = SA; }
            else if (Bulan == 7) { string SA = "JunQty"; Session["SA"] = SA; }
            else if (Bulan == 8) { string SA = "JulQty"; Session["SA"] = SA; }
            else if (Bulan == 9) { string SA = "AguQty"; Session["SA"] = SA; }
            else if (Bulan == 10) { string SA = "SepQty"; Session["SA"] = SA; }
            else if (Bulan == 11) { string SA = "OktQty"; Session["SA"] = SA; }
            else if (Bulan == 12) { string SA = "NovQty"; Session["SA"] = SA; }

            if (Bulan == 1)
            { string TahunSaldo = Session["Tahun"].ToString(); Session["ThnSA"] = TahunSaldo; }
            else { string TahunSaldo = Tahun.ToString(); Session["ThnSA"] = TahunSaldo; }

            if (Bulan.ToString().Length == 1)
            {
                string YM = Tahun.ToString().Trim() + "0" + Bulan.ToString().Trim(); Session["YM"] = YM;
            }
            else
            {
                string YM = Tahun.ToString().Trim() + Bulan.ToString().Trim(); Session["YM"] = YM;
            }

            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 1)
                strplant = "";
            else
                strplant = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
            string strSQL = string.Empty;

            strSQL =
            " with DataAwal as (select ItemID,Qty from (select ItemID,SUM(Qty)Qty from ( " +
            //Saldo awal bulan
            " select ItemID," + Session["SA"].ToString() + " Qty from " + strplant + "SaldoInventory where ItemTypeID=1 and GroupID=7 and YearPeriod=" + Session["ThnSA"].ToString() + " " +
            " union all " +
            //Data transaksi
            " select ItemID,SUM(quantity)Qty from " + strplant + "vw_StockPurchn where GroupID=7 and YM='" + Session["YM"].ToString() + "' and ItemTypeID=1 group by ItemID  " +
            " union all " +
            //Data SPB transit ( Belum approved Gudang )
            " select ItemID,-1 * SUM(Quantity)Qty from " + strplant + "PakaiDetail where GroupID=7 and PakaiID in (select ID from " + strplant + "Pakai " +
            " where Status>-1 and Status<2 and LEFT(convert(char,pakaidate,112),6)='" + Session["YM"].ToString() + "') group by ItemID " +
            " union all" +
            //Data Bookingan
            " select ItemID, -1 *sum(QtyBooking-QtyAmbil) Qty from " + strplant + "StockTemp_BrgMKT where ItemID in (select ID from " + strplant + "Inventory where GroupID=7 and Aktif=1) and Rowstatus>-1 group by ItemID  " +
            ") as xx  group by ItemID ) as xx2 " +
            " where Qty>0) " +

            " select ROW_NUMBER() over (order by B.ItemName asc) as No,B.ItemCode KodeBarang,B.ItemName NamaBarang,A.Qty,C.UOMDesc Satuan " +
            " from DataAwal A " +
            " INNER JOIN " + strplant + "Inventory B ON A.ItemID=B.ID " +
            " INNER JOIN " + strplant + "UOM C ON B.UOMID=C.ID where ItemCode not in (select ItemCode from " + strplant + "InventoryNonMKT where Rowstatus>-1) order by B.ItemName asc ";

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
            GrdDynamicPromo.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                if (col.ColumnName == "No")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "KodeBarang")
                {
                    bfield.HeaderText = "Kode Barang";
                }
                if (col.ColumnName == "NamaBarang")
                {
                    bfield.HeaderText = "Nama Barang";
                }
                if (col.ColumnName == "Qty")
                {
                    bfield.HeaderText = "Stok";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N0}";
                }
                GrdDynamicPromo.Columns.Add(bfield);
            }
            GrdDynamicPromo.DataSource = dt;
            GrdDynamicPromo.DataBind();
        }
        private void loadDynamicGridPromo2()
        {
            Users user = (Users)Session["Users"];

            int Bulan = DateTime.Now.Month; int Tahun = DateTime.Now.Year;

            if (Bulan == 1)
            { string SA = "DesQty"; Session["SA"] = SA; int TahunSA = Tahun - 1; Session["Tahun"] = TahunSA.ToString(); }
            else if (Bulan == 2) { string SA = "JanQty"; Session["SA"] = SA; }
            else if (Bulan == 3) { string SA = "FebQty"; Session["SA"] = SA; }
            else if (Bulan == 4) { string SA = "MarQty"; Session["SA"] = SA; }
            else if (Bulan == 5) { string SA = "AprQty"; Session["SA"] = SA; }
            else if (Bulan == 6) { string SA = "MeiQty"; Session["SA"] = SA; }
            else if (Bulan == 7) { string SA = "JunQty"; Session["SA"] = SA; }
            else if (Bulan == 8) { string SA = "JulQty"; Session["SA"] = SA; }
            else if (Bulan == 9) { string SA = "AguQty"; Session["SA"] = SA; }
            else if (Bulan == 10) { string SA = "SepQty"; Session["SA"] = SA; }
            else if (Bulan == 11) { string SA = "OktQty"; Session["SA"] = SA; }
            else if (Bulan == 12) { string SA = "NovQty"; Session["SA"] = SA; }

            if (Bulan == 1)
            { string TahunSaldo = Session["Tahun"].ToString(); Session["ThnSA"] = TahunSaldo; }
            else { string TahunSaldo = Tahun.ToString(); Session["ThnSA"] = TahunSaldo; }

            if (Bulan.ToString().Length == 1)
            {
                string YM = Tahun.ToString().Trim() + "0" + Bulan.ToString().Trim(); Session["YM"] = YM;
            }
            else
            {
                string YM = Tahun.ToString().Trim() + Bulan.ToString().Trim(); Session["YM"] = YM;
            }


            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 7)
                strplant = "";
            else
                strplant = " [sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            string strSQL = string.Empty;

            strSQL =
            " with DataAwal as (select ItemID,Qty from (select ItemID,SUM(Qty)Qty from ( " +
            //Saldo awal bulan
            " select ItemID," + Session["SA"].ToString() + " Qty from " + strplant + "SaldoInventory where ItemTypeID=1 and GroupID=7 and YearPeriod=" + Session["ThnSA"].ToString() + " " +
            " union all " +
            //Data transaksi
            " select ItemID,SUM(quantity)Qty from " + strplant + "vw_StockPurchn where GroupID=7 and YM='" + Session["YM"].ToString() + "' and ItemTypeID=1 group by ItemID  " +
            " union all " +
            //Data SPB transit ( Belum approved Gudang )
            " select ItemID,-1 * SUM(Quantity)Qty from " + strplant + "PakaiDetail where GroupID=7 and PakaiID in (select ID from " + strplant + "Pakai " +
            " where Status>-1 and Status<2 and LEFT(convert(char,pakaidate,112),6)='" + Session["YM"].ToString() + "') group by ItemID " +
            " union all" +
            //Data Bookingan
            " select ItemID, -1 *sum(QtyBooking-QtyAmbil) Qty from " + strplant + "StockTemp_BrgMKT where ItemID in (select ID from " + strplant + "Inventory where GroupID=7 and Aktif=1) and Rowstatus>-1 group by ItemID  " +
            ") as xx  group by ItemID ) as xx2 " +
            " where Qty>0) " +

            " select ROW_NUMBER() over (order by B.ItemName asc) as No,B.ItemCode KodeBarang,B.ItemName NamaBarang,A.Qty,C.UOMDesc Satuan " +
            " from DataAwal A " +
            " INNER JOIN " + strplant + "Inventory B ON A.ItemID=B.ID " +
            " INNER JOIN " + strplant + "UOM C ON B.UOMID=C.ID where ItemCode not in (select ItemCode from " + strplant + "InventoryNonMKT where Rowstatus>-1) order by B.ItemName asc ";

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
            GrdDynamicPromo2.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;

                if (col.ColumnName == "No")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "KodeBarang")
                {
                    bfield.HeaderText = "Kode Barang";
                }
                if (col.ColumnName == "NamaBarang")
                {
                    bfield.HeaderText = "Nama Barang";
                }
                if (col.ColumnName == "Qty")
                {
                    bfield.HeaderText = "Stok";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N0}";
                }
                GrdDynamicPromo2.Columns.Add(bfield);
            }
            GrdDynamicPromo2.DataSource = dt;
            GrdDynamicPromo2.DataBind();
        }
        private void loadDynamicGridPromo3()
        {
            Users user = (Users)Session["Users"];

            int Bulan = DateTime.Now.Month; int Tahun = DateTime.Now.Year;

            if (Bulan == 1)
            { string SA = "DesQty"; Session["SA"] = SA; int TahunSA = Tahun - 1; Session["Tahun"] = TahunSA.ToString(); }
            else if (Bulan == 2) { string SA = "JanQty"; Session["SA"] = SA; }
            else if (Bulan == 3) { string SA = "FebQty"; Session["SA"] = SA; }
            else if (Bulan == 4) { string SA = "MarQty"; Session["SA"] = SA; }
            else if (Bulan == 5) { string SA = "AprQty"; Session["SA"] = SA; }
            else if (Bulan == 6) { string SA = "MeiQty"; Session["SA"] = SA; }
            else if (Bulan == 7) { string SA = "JunQty"; Session["SA"] = SA; }
            else if (Bulan == 8) { string SA = "JulQty"; Session["SA"] = SA; }
            else if (Bulan == 9) { string SA = "AguQty"; Session["SA"] = SA; }
            else if (Bulan == 10) { string SA = "SepQty"; Session["SA"] = SA; }
            else if (Bulan == 11) { string SA = "OktQty"; Session["SA"] = SA; }
            else if (Bulan == 12) { string SA = "NovQty"; Session["SA"] = SA; }

            if (Bulan == 1)
            { string TahunSaldo = Session["Tahun"].ToString(); Session["ThnSA"] = TahunSaldo; }
            else { string TahunSaldo = Tahun.ToString(); Session["ThnSA"] = TahunSaldo; }

            if (Bulan.ToString().Length == 1)
            {
                string YM = Tahun.ToString().Trim() + "0" + Bulan.ToString().Trim(); Session["YM"] = YM;
            }
            else
            {
                string YM = Tahun.ToString().Trim() + Bulan.ToString().Trim(); Session["YM"] = YM;
            }


            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 13)
            {
                strplant = "";
            }
            else
                strplant = " [sqljombang.grcboard.com].bpasjombang.dbo.";
            string strSQL = string.Empty;

            strSQL =
            " with DataAwal as (select ItemID,Qty from (select ItemID,SUM(Qty)Qty from ( " +
            //Saldo awal bulan
            " select ItemID," + Session["SA"].ToString() + " Qty from " + strplant + "SaldoInventory where ItemTypeID=1 and GroupID=7 and YearPeriod=" + Session["ThnSA"].ToString() + " " +
            " union all " +
            //Data transaksi
            " select ItemID,SUM(quantity)Qty from " + strplant + "vw_StockPurchn where GroupID=7 and YM='" + Session["YM"].ToString() + "' and ItemTypeID=1 group by ItemID  " +
            " union all " +
            //Data SPB transit ( Belum approved Gudang )
            " select ItemID,-1 * SUM(Quantity)Qty from " + strplant + "PakaiDetail where GroupID=7 and PakaiID in (select ID from " + strplant + "Pakai " +
            " where Status>-1 and Status<2 and LEFT(convert(char,pakaidate,112),6)='" + Session["YM"].ToString() + "') group by ItemID " +
            " union all" +
            //Data Bookingan
            " select ItemID, -1 *sum(QtyBooking-QtyAmbil) Qty from " + strplant + "StockTemp_BrgMKT where ItemID in (select ID from " + strplant + "Inventory where GroupID=7 and Aktif=1) and Rowstatus>-1 group by ItemID  " +
            ") as xx  group by ItemID ) as xx2 " +
            " where Qty>0) " +

            " select ROW_NUMBER() over (order by B.ItemName asc) as No,B.ItemCode KodeBarang,B.ItemName NamaBarang,A.Qty,C.UOMDesc Satuan " +
            " from DataAwal A " +
            " INNER JOIN " + strplant + "Inventory B ON A.ItemID=B.ID " +
            " INNER JOIN " + strplant + "UOM C ON B.UOMID=C.ID where ItemCode not in (select ItemCode from " + strplant + "InventoryNonMKT where Rowstatus>-1) order by B.ItemName asc ";

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
            GrdDynamicPromo3.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;

                if (col.ColumnName == "No")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "KodeBarang")
                {
                    bfield.HeaderText = "Kode Barang";
                }
                if (col.ColumnName == "NamaBarang")
                {
                    bfield.HeaderText = "Nama Barang";
                }
                if (col.ColumnName == "Qty")
                {
                    bfield.HeaderText = "Stok";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N0}";
                }
                GrdDynamicPromo3.Columns.Add(bfield);
            }
            GrdDynamicPromo3.DataSource = dt;
            GrdDynamicPromo3.DataBind();
        }

        private void loadDynamicGridNoPrint()
        {
            Users user = (Users)Session["Users"];
            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 7 || plant == 13)
                strplant = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
            else
                strplant = "";
            string strSQL = string.Empty;
            string kwalitas = string.Empty;
            if (RBPBJ.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%' or i.PartNo like '%-W-%' or i.PartNo like '%-M-%')";
            if (RBPBP.Checked == true)
                kwalitas = " and (i.PartNo like '%-P-%')";
            if (RBPBS.Checked == true)
                kwalitas = " and (i.PartNo like '%-S-%')";
            if (RBPEFO.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%')";
            string bygroup = string.Empty;
            if (ddlGroup.SelectedIndex == 0)
                bygroup = "";
            else
                bygroup = " and I.GroupID in (select ID from " + strplant + "t3_groupm where grouprsid in (select id from "
                    + strplant + "t3_groupreadystock where groupname='" + ddlGroup.SelectedItem.Text + "'))";
            string efo = string.Empty;
            if (RBPEFO.Checked == true)
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,7) not in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";
            else
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,7) in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";

            string bytebal = string.Empty;
            if (RBPBP.Checked == true || RBPBS.Checked == true)
                efo = bygroup;

            if (chkTebal.Checked == true)
                bytebal = "";
            else
                bytebal = " and I.tebal >= " + ddlTebal1.SelectedValue + " and I.tebal<= " + ddlTebal2.SelectedValue + " ";
            string byLebar = string.Empty;
            if (chkLebar.Checked == true)
                byLebar = "";
            else
                byLebar = " and I.Lebar >= " + ddlLebar1.SelectedValue + " and I.Lebar<= " + ddlLebar2.SelectedValue + " ";
            string byPanjang = string.Empty;
            if (chkPanjang.Checked == true)
                byPanjang = "";
            else
                byPanjang = " and I.Panjang >= " + ddlPanjang1.SelectedValue + " and I.Panjang<= " + ddlPanjang2.SelectedValue + " ";
            strSQL = "select ROW_NUMBER() OVER (ORDER BY grup ,tebal,lebar,panjang ,ukuran) AS No,  " +
                "grup Produk, tebal,Ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup +  ' ' +KW +' '  grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' " +
                "when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW,SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char))+' ' + SUBSTRING(I.partno,18,7) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi in (select lokasi from FC_LokasiNoPrint where rowstatus>-1) and " +
                "S.qty>0 " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang)A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang   " +
                "union all  " +
                "select 1000 No,'Total Siap Kirim ' grup,0 tebal,'' ukuran,sum(Lembar)Lembar,sum(MeterKubik) MeterKubik from (  " +
                "select grup,tebal,ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup + ' ' +KW +' ' +sisi grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from" + strplant + " T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW," +
                "SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char)) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi in (select lokasi from FC_LokasiNoPrint where rowstatus>-1) and " +
                "S.qty>0  " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang )A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang )C  order by No";
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
            GridNoPrint.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                if (col.ColumnName == "MeterKubik")
                {
                    bfield.HeaderText = "MeterKubik";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                else
                {
                    //bfield.DataFormatString = "{0:N0}";
                    bfield.HeaderText = col.ColumnName;
                }
                if (col.ColumnName == "Lembar")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                if (col.ColumnName == "Ukuran")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "tebal")
                {
                    bfield.HeaderText = "Tebal";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                GridNoPrint.Columns.Add(bfield);
            }
            GridNoPrint.DataSource = dt;
            GridNoPrint.DataBind();
        }
        private void loadDynamicGridNoPrint2()
        {
            Users user = (Users)Session["Users"];
            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 1 || plant == 13)
                strplant = " [sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            else
                strplant = "";

            string strSQL = string.Empty;
            string kwalitas = string.Empty;
            if (RBPBJ.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%' or i.PartNo like '%-W-%'  or i.PartNo like '%-M-%')";
            if (RBPBP.Checked == true)
                kwalitas = " and (i.PartNo like '%-P-%')";
            if (RBPBS.Checked == true)
                kwalitas = " and (i.PartNo like '%-S-%')";
            if (RBPEFO.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%')";
            string bygroup = string.Empty;
            if (ddlGroup.SelectedIndex == 0)
                bygroup = "";
            else
                bygroup = " and I.GroupID in (select ID from " + strplant + "t3_groupm where grouprsid in (select id from "
                    + strplant + "t3_groupreadystock where groupname='" + ddlGroup.SelectedItem.Text + "'))";
            string efo = string.Empty;
            if (RBPEFO.Checked == true)
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,5) not in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";
            else
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,5) in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";
            if (RBPBP.Checked == true || RBPBS.Checked == true)
                efo = bygroup;
            string bytebal = string.Empty;

            if (chkTebal.Checked == true)
                bytebal = "";
            else
                bytebal = " and I.tebal >= " + ddlTebal1.SelectedValue + " and I.tebal<= " + ddlTebal2.SelectedValue + " ";
            string byLebar = string.Empty;
            if (chkLebar.Checked == true)
                byLebar = "";
            else
                byLebar = " and I.Lebar >= " + ddlLebar1.SelectedValue + " and I.Lebar<= " + ddlLebar2.SelectedValue + " ";
            string byPanjang = string.Empty;
            if (chkPanjang.Checked == true)
                byPanjang = "";
            else
                byPanjang = " and I.Panjang >= " + ddlPanjang1.SelectedValue + " and I.Panjang<= " + ddlPanjang2.SelectedValue + " ";

            strSQL = "select ROW_NUMBER() OVER (ORDER BY grup ,tebal,lebar,panjang ,ukuran) AS No,  " +
                "grup Produk, tebal,Ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup +  ' ' +KW +' '  grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' " +
                "when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW,SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char))+' ' + SUBSTRING(I.partno,18,4) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi in (select lokasi from FC_LokasiNoPrint where rowstatus>-1) and " +
                "S.qty>0 " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang)A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang   " +
                "union all  " +
                "select 1000 No,'Total Siap Kirim ' grup,0 tebal,'' ukuran,sum(Lembar)Lembar,sum(MeterKubik) MeterKubik from (  " +
                "select grup,tebal,ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup + ' ' +KW +' ' +sisi grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from" + strplant + " T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW," +
                "SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char)) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi in (select lokasi from FC_LokasiNoPrint where rowstatus>-1) and " +
                "S.qty>0  " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang )A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang )C  order by No";
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
            GridNoPrint2.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                if (col.ColumnName == "MeterKubik")
                {
                    bfield.HeaderText = "MeterKubik";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                else
                {
                    //bfield.DataFormatString = "{0:N0}";
                    bfield.HeaderText = col.ColumnName;
                }
                if (col.ColumnName == "Lembar")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                if (col.ColumnName == "Ukuran")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "tebal")
                {
                    bfield.HeaderText = "Tebal";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                GridNoPrint2.Columns.Add(bfield);
            }
            GridNoPrint2.DataSource = dt;
            GridNoPrint2.DataBind();
        }
        private void loadDynamicGridNoPrint3()
        {
            Users user = (Users)Session["Users"];
            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 1 || plant == 7)
                strplant = " [sqlJombang.grcboard.com].bpasJombang.dbo.";
            else
                strplant = "";

            string strSQL = string.Empty;
            string kwalitas = string.Empty;
            if (RBPBJ.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%' or i.PartNo like '%-W-%'  or i.PartNo like '%-M-%')";
            if (RBPBP.Checked == true)
                kwalitas = " and (i.PartNo like '%-P-%')";
            if (RBPBS.Checked == true)
                kwalitas = " and (i.PartNo like '%-S-%')";
            if (RBPEFO.Checked == true)
                kwalitas = " and (i.PartNo like '%-3-%')";
            string bygroup = string.Empty;
            if (ddlGroup.SelectedIndex == 0)
                bygroup = "";
            else
                bygroup = " and I.GroupID in (select ID from " + strplant + "t3_groupm where grouprsid in (select id from "
                    + strplant + "t3_groupreadystock where groupname='" + ddlGroup.SelectedItem.Text + "'))";
            string efo = string.Empty;
            if (RBPEFO.Checked == true)
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,5) not in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";
            else
                efo = bygroup + "and substring(I.partno,1,3)+rtrim(cast(cast(I.tebal as decimal(8,2)) as char))+rtrim(cast(cast(I.lebar as int) as char))+" +
                    "rtrim(cast(cast(I.Panjang as int) as char))+substring(I.partno,18,5) in (select rtrim(jenis)+" +
                    "rtrim(cast(cast(tebal as decimal(8,2)) as char))+rtrim(cast(cast(lebar as int) as char))+" +
                    "rtrim(cast(cast(Panjang as int) as char))+rtrim(sisi) from " + strplant + "T3_ProdukStd)";
            if (RBPBP.Checked == true || RBPBS.Checked == true)
                efo = bygroup;
            string bytebal = string.Empty;

            if (chkTebal.Checked == true)
                bytebal = "";
            else
                bytebal = " and I.tebal >= " + ddlTebal1.SelectedValue + " and I.tebal<= " + ddlTebal2.SelectedValue + " ";
            string byLebar = string.Empty;
            if (chkLebar.Checked == true)
                byLebar = "";
            else
                byLebar = " and I.Lebar >= " + ddlLebar1.SelectedValue + " and I.Lebar<= " + ddlLebar2.SelectedValue + " ";
            string byPanjang = string.Empty;
            if (chkPanjang.Checked == true)
                byPanjang = "";
            else
                byPanjang = " and I.Panjang >= " + ddlPanjang1.SelectedValue + " and I.Panjang<= " + ddlPanjang2.SelectedValue + " ";

            strSQL = "select ROW_NUMBER() OVER (ORDER BY grup ,tebal,lebar,panjang ,ukuran) AS No,  " +
                "grup Produk, tebal,Ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup +  ' ' +KW +' '  grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' " +
                "when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW,SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char))+' ' + SUBSTRING(I.partno,18,4) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi in (select lokasi from FC_LokasiNoPrint where rowstatus>-1) and " +
                "S.qty>0 " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang)A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang   " +
                "union all  " +
                "select 1000 No,'Total Siap Kirim ' grup,0 tebal,'' ukuran,sum(Lembar)Lembar,sum(MeterKubik) MeterKubik from (  " +
                "select grup,tebal,ukuran,sum(stock)Lembar,((tebal*Panjang*lebar)/1000000000)*sum(stock) MeterKubik from (  " +
                "select grup + ' ' +KW +' ' +sisi grup,ukuran,stock ,tebal,lebar,panjang from (  " +
                "select case when isnull( (select rtrim(groups) from " + strplant + "T3_GroupM where id=S.GroupID ),'-')<>'-' then   " +
                "isnull((select rtrim(groups) from" + strplant + " T3_GroupM where id=S.GroupID ),'-')else substring(I.Partno,1,3) end  grup,   " +
                "rtrim(cast(cast(tebal as decimal(8,1)) as char )) tbl, case when SUBSTRING(I.partno,4,3)='-3-' then''  " +
                "when SUBSTRING(I.partno,4,3)='-M-' then 'M' when SUBSTRING(I.partno,4,3)='-W-' then 'M' when SUBSTRING(I.partno,4,3)='-P-' then 'BP' when SUBSTRING(I.partno,4,3)='-S-' then 'BS' end KW," +
                "SUBSTRING(I.partno,18,4) sisi,  " +
                "rtrim(cast(cast(Lebar as int ) as char)) +' X '+ rtrim(cast(cast(Panjang as int ) as char)) Ukuran, sum(S.Qty )stock,tebal,lebar,panjang  " +
                "from " + strplant + "T3_Serah S inner join " + strplant + "fc_items I on S.itemid=I.ID inner join " + strplant + "FC_Lokasi L on S.LokID=L.ID where    " +
                //"L.Lokasi <> 'L99' and L.Lokasi <> 's99' and L.Lokasi <> 'bsauto' and L.Lokasi <> 'M99' and L.Lokasi <> 'J99' and L.Lokasi <> 'SORTIR' and   " +
                "L.Lokasi in (select lokasi from FC_LokasiNoPrint where rowstatus>-1) and " +
                "S.qty>0  " + kwalitas + efo + bytebal + byLebar + byPanjang + " group by I.PartNo, S.GroupID,I.Tebal,I.Lebar,Panjang )A )B  " +
                "group by grup,ukuran,tebal,lebar,panjang )C  order by No";
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
            GridNoPrint3.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                if (col.ColumnName == "MeterKubik")
                {
                    bfield.HeaderText = "MeterKubik";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                else
                {
                    //bfield.DataFormatString = "{0:N0}";
                    bfield.HeaderText = col.ColumnName;
                }
                if (col.ColumnName == "Lembar")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                if (col.ColumnName == "Ukuran")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "tebal")
                {
                    bfield.HeaderText = "Tebal";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                GridNoPrint3.Columns.Add(bfield);
            }
            GridNoPrint3.DataSource = dt;
            GridNoPrint3.DataBind();
        }
        //Selesai Tambahan

        protected void RBPBJ_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGrid();
            loadDynamicGrid0();
            loadDynamicGrid1();

            PanelSatu.Visible = true; PanelDua.Visible = false; PanelDua2.Visible = false;
            Panel3.Visible = true; Panel1.Visible = true;
            //PanelMKT.Visible = false; PanelMKT2.Visible = false;
            //PanelPromo.Visible = false; PanelPromo2.Visible = false;
            PanelPromo.Visible = false; PanelMKT.Visible = false;
        }
        protected void RBPBP_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGrid();
            loadDynamicGrid0();
            loadDynamicGrid1();

            PanelSatu.Visible = true; PanelDua.Visible = false; PanelDua2.Visible = false;
            Panel3.Visible = true; Panel1.Visible = true;
            //PanelMKT.Visible = false; PanelMKT2.Visible = false;
            //PanelPromo.Visible = false; PanelPromo2.Visible = false;
            PanelPromo.Visible = false; PanelMKT.Visible = false;
        }
        protected void RBPBS_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGrid();
            loadDynamicGrid0();
            loadDynamicGrid1();

            PanelSatu.Visible = true; PanelDua.Visible = false; PanelDua2.Visible = false;
            Panel3.Visible = true; Panel1.Visible = true;
            //PanelMKT.Visible = false; PanelMKT2.Visible = false;
            //PanelPromo.Visible = false; PanelPromo2.Visible = false;
            PanelPromo.Visible = false; PanelMKT.Visible = false;
        }

        //Tambahan
        protected void RBMKT_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGridMKT();
            loadDynamicGridMKT2();
            loadDynamicGridMKT3();

            PanelSatu.Visible = false; PanelDua.Visible = true; PanelDua2.Visible = true;
            //PanelMKT.Visible = true; PanelMKT2.Visible = true;
            //PanelPromo.Visible = false; PanelPromo2.Visible = false;
            Panel3.Visible = false; Panel1.Visible = false;
            PanelPromo.Visible = false; PanelMKT.Visible = true;
        }

        protected void RBNoPrint_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGridNoPrint();
            loadDynamicGridNoPrint2();
            loadDynamicGridNoPrint3();

            PanelSatu.Visible = false; PanelDua.Visible = false; PanelDua2.Visible = false; PanelTiga.Visible = true;
            //PanelMKT.Visible = true; PanelMKT2.Visible = true;
            //PanelPromo.Visible = false; PanelPromo2.Visible = false;
            Panel3.Visible = false; Panel1.Visible = false;
            PanelPromo.Visible = false; PanelMKT.Visible = true;
        }

        protected void RBPRM_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGridPromo();
            loadDynamicGridPromo2();
            loadDynamicGridPromo3();
            PanelSatu.Visible = false; PanelDua.Visible = true; PanelDua2.Visible = true;
            //PanelMKT.Visible = false; PanelMKT2.Visible = false; 
            //PanelPromo.Visible = true; PanelPromo2.Visible = true;
            Panel3.Visible = false; Panel1.Visible = false;
            PanelPromo.Visible = true; PanelMKT.Visible = false;
        }
        //Selesai Tambahan

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (RBPBJ.Checked == true || RBPBP.Checked == true || RBPBS.Checked == true || RBPEFO.Checked == true)
            {
                if (GrdDynamic.Rows.Count == 0)
                    return;
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "List Stock Marketing " + DateTime.Now.ToString("ddMMMyyyy HHmmss") + ".xls"));
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
            else if (RBMKT.Checked == true)
            {
                if (GrdDynamicMKT.Rows.Count == 0)
                    return;
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "List Stock Marketing " + DateTime.Now.ToString("ddMMMyyyy HHmmss") + ".xls"));
                Response.ContentType = "application/ms-excel";
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
                GrdDynamicMKT.AllowPaging = false;
                GrdDynamicMKT.HeaderRow.Style.Add("background-color", "#FFFFFF");
                for (int i = 0; i < GrdDynamicMKT.HeaderRow.Cells.Count; i++)
                {
                    GrdDynamicMKT.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                }
                PanelDua.RenderControl(htw1);
                Response.Write(sw1.ToString());
                Response.End();
            }
            else if (RBPRM.Checked == true)
            {
                if (GrdDynamicPromo.Rows.Count == 0)
                    return;
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "List Stock Marketing " + DateTime.Now.ToString("ddMMMyyyy HHmmss") + ".xls"));
                Response.ContentType = "application/ms-excel";
                StringWriter sw2 = new StringWriter();
                HtmlTextWriter htw2 = new HtmlTextWriter(sw2);
                GrdDynamicPromo.AllowPaging = false;
                GrdDynamicPromo.HeaderRow.Style.Add("background-color", "#FFFFFF");
                for (int i = 0; i < GrdDynamicPromo.HeaderRow.Cells.Count; i++)
                {
                    GrdDynamicPromo.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                }
                PanelDua2.RenderControl(htw2);
                Response.Write(sw2.ToString());
                Response.End();
            }

        }
        protected void chkTebal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTebal.Checked == false)
            {
                ddlTebal1.Enabled = true;
                ddlTebal2.Enabled = true;
            }
            else
            {
                ddlTebal1.Enabled = false;
                ddlTebal2.Enabled = false;
            }
            
        }
        protected void chkPanjang_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPanjang.Checked == false)
            {
                ddlPanjang1.Enabled = true;
                ddlPanjang2.Enabled = true;
            }
            else
            {
                ddlPanjang1.Enabled = false;
                ddlPanjang2.Enabled = false;
            }
        }
        protected void chkLebar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLebar.Checked == false)
            {
                ddlLebar1.Enabled = true;
                ddlLebar2.Enabled = true;
            }
            else
            {
                ddlLebar1.Enabled = false;
                ddlLebar2.Enabled = false;
            }
        }
        protected void LoadUkuran(string ukuran)
        {
            Users user = (Users)Session["Users"];
            ArrayList arrUkuran = new ArrayList();
            Session["ukuran"] = null;
            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 7)
                strplant = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
            else
                strplant = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            StockMKTFacade stockF = new StockMKTFacade();
            if (ukuran == "tebal")
                ukuran = " tebal,0 panjang,0 lebar ";
            if (ukuran == "panjang")
                ukuran = " 0 tebal,panjang,0 lebar ";
            if (ukuran == "lebar")
                ukuran = " 0 tebal,0 panjang,lebar ";
            string query = "select distinct " + ukuran + " from ( " +
                "select distinct " + ukuran + " from fc_items where rowstatus>-1 and id in (select itemid from t3_serah where qty>0) " +
                "union all " +
                "select distinct " + ukuran + " from " + strplant + "fc_items where rowstatus>-1 and  " +
                "id in (select itemid from " + strplant + "t3_serah where qty>0) ) A order by tebal";
            arrUkuran = stockF.RetrieveUkuran(query);
            Session["ukuran"] = arrUkuran;
        }
        protected void SetUkuran()
        {
            LoadUkuran("tebal");
            ArrayList arrTebal = (ArrayList)Session["ukuran"];
            ddlTebal1.Items.Clear();
            ddlTebal2.Items.Clear();
            ddlTebal1.Items.Add(new ListItem("ALL", "0"));
            ddlTebal2.Items.Add(new ListItem("ALL", "0"));
            foreach (StockMKT ukr in arrTebal)
            {

                ddlTebal1.Items.Add(new ListItem(ukr.Tebal.ToString("N1"), ukr.Tebal.ToString("N1").Replace(".", ",")));
                ddlTebal2.Items.Add(new ListItem(ukr.Tebal.ToString("N1"), ukr.Tebal.ToString("N1").Replace(".", ",")));
            }
            LoadUkuran("panjang");
            ArrayList arrPanjang = (ArrayList)Session["ukuran"];
            ddlPanjang1.Items.Clear();
            ddlPanjang2.Items.Clear();
            ddlPanjang1.Items.Add(new ListItem("ALL", "0"));
            ddlPanjang2.Items.Add(new ListItem("ALL", "0"));
            foreach (StockMKT ukr in arrPanjang)
            {
                ddlPanjang1.Items.Add(new ListItem(ukr.Panjang.ToString(), ukr.Panjang.ToString().Replace(".", String.Empty)));
                ddlPanjang2.Items.Add(new ListItem(ukr.Panjang.ToString(), ukr.Panjang.ToString().Replace(".", String.Empty)));
            }
            LoadUkuran("lebar");
            ArrayList arrLebar = (ArrayList)Session["ukuran"];
            ddlLebar1.Items.Clear();
            ddlLebar2.Items.Clear();
            ddlLebar1.Items.Add(new ListItem("ALL", "0"));
            ddlLebar2.Items.Add(new ListItem("ALL", "0"));
            foreach (StockMKT ukr in arrLebar)
            {
                ddlLebar1.Items.Add(new ListItem(ukr.Lebar.ToString(), ukr.Lebar.ToString().Replace(".", String.Empty)));
                ddlLebar2.Items.Add(new ListItem(ukr.Lebar.ToString(), ukr.Lebar.ToString().Replace(".", String.Empty)));
            }
        }
        protected void ddlTebal1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTebal1.SelectedIndex > ddlTebal2.SelectedIndex)
                ddlTebal2.SelectedIndex = ddlTebal1.SelectedIndex;
            if (ddlTebal1.SelectedIndex == 0)
                ddlTebal2.SelectedIndex = 0;
        }
        protected void ddlTebal2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTebal1.SelectedIndex > ddlTebal2.SelectedIndex)
                ddlTebal1.SelectedIndex = ddlTebal2.SelectedIndex;
            else
            {
                if (ddlTebal1.SelectedIndex == 0)
                    ddlTebal1.SelectedIndex = 1;
            }
            if (ddlTebal2.SelectedIndex == 0)
                ddlTebal1.SelectedIndex = 0;
        }
        protected void ddlLebar1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLebar1.SelectedIndex > ddlLebar2.SelectedIndex)
                ddlLebar2.SelectedIndex = ddlLebar1.SelectedIndex;
            if (ddlLebar1.SelectedIndex == 0)
                ddlLebar2.SelectedIndex = 0;
        }
        protected void ddlLebar2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLebar1.SelectedIndex > ddlLebar2.SelectedIndex)
                ddlLebar1.SelectedIndex = ddlLebar2.SelectedIndex;
            else
            {
                if (ddlLebar1.SelectedIndex == 0)
                    ddlLebar1.SelectedIndex = 1;
            }
            if (ddlLebar2.SelectedIndex == 0)
                ddlLebar1.SelectedIndex = 0;
        }
        protected void ddlPanjang1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPanjang1.SelectedIndex > ddlPanjang2.SelectedIndex)
                ddlPanjang2.SelectedIndex = ddlPanjang1.SelectedIndex;
            if (ddlPanjang1.SelectedIndex == 0)
                ddlPanjang2.SelectedIndex = 0;
        }
        protected void ddlPanjang2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPanjang1.SelectedIndex > ddlPanjang2.SelectedIndex)
                ddlPanjang1.SelectedIndex = ddlPanjang2.SelectedIndex;
            else
            {
                if (ddlPanjang1.SelectedIndex == 0)
                    ddlPanjang1.SelectedIndex = 1;
            }
            if (ddlPanjang2.SelectedIndex == 0)
                ddlPanjang1.SelectedIndex = 0;
        }
        protected void LoadGroupMkt()
        {
            Users user = (Users)Session["Users"];
            ArrayList arrUkuran = new ArrayList();
            Session["group"] = null;
            int plant = user.UnitKerjaID;
            string strplant = string.Empty;
            if (plant == 7)
                strplant = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
            else
                strplant = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            StockMKTFacade stockF = new StockMKTFacade();
            string query = "select distinct ID,groupname from ( " +
                "select * from T3_GroupReadyStock where rowstatus>-1  " +
                "union all " +
                "select * from " + strplant + "T3_GroupReadyStock where rowstatus>-1 )A order by groupname";
            arrUkuran = stockF.RetrieveGroup(query);
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("ALL", "0"));
            foreach (GroupMkt grp in arrUkuran)
            {
                ddlGroup.Items.Add(new ListItem(grp.GroupName, grp.ID.ToString()));
            }
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDynamicGrid();
            loadDynamicGrid0();
            loadDynamicGrid1();
        }
    }
    public class StockMKTFacade
    {
        private ArrayList arrData = new ArrayList();
        private StockMKT sp = new StockMKT();
        private GroupMkt gp = new GroupMkt();
        protected string strError = string.Empty;
        public ArrayList RetrieveUkuran(string query)
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList RetrieveGroup(string query)
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateGroup(sdr));
                }
            }
            return arrData;
        }
        private StockMKT GenerateObject(SqlDataReader sdr)
        {
            sp = new StockMKT();
            sp.Tebal = Decimal.Parse(sdr["Tebal"].ToString());
            sp.Panjang = Decimal.Parse(sdr["Panjang"].ToString());
            sp.Lebar = Decimal.Parse(sdr["Lebar"].ToString());
            return sp;
        }

        public int RetrieveLock()
        {

            string StrSql =
            "select Lock from StockTemp_Lock where Rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Lock"]);
                }
            }

            return 0;
        }

        private GroupMkt GenerateGroup(SqlDataReader sdr)
        {
            gp = new GroupMkt();
            gp.ID = Int32.Parse(sdr["ID"].ToString());
            gp.GroupName = sdr["GroupName"].ToString();
            return gp;
        }
    }
    public class StockMKT
    {
        public Decimal Panjang { get; set; }
        public Decimal Lebar { get; set; }
        public Decimal Tebal { get; set; }
    }
    public class GroupMkt
    {
        public Int32 ID { get; set; }
        public String GroupName { get; set; }
    }

}