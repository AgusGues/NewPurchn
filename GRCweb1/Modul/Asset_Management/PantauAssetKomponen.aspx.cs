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

namespace GRCweb1.Modul.Asset_Management
{
    public partial class PantauAssetKomponen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                LoadDataAsset();

            }
         ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            //((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport2);

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 400, 100 , 30 ,false); </script>", false);
            }
            catch { }
        }

        protected void btn_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrAsset = new ArrayList();
            AssetKomponen asset = new AssetKomponen();
            FacadeAssetKomponen assetF = new FacadeAssetKomponen();

            arrAsset = assetF.RetrieveGridAsset(ddlAsset.SelectedValue.ToString());
            if (arrAsset.Count > 0)
            {
                noted.Visible = true;
                lstAsset.DataSource = arrAsset;
                lstAsset.DataBind();
            }
            else
            {
                noted.Visible = false;
                DisplayAJAXMessage(this, " Data tidak ada !! ");
                return;
            }
        }

        protected void btnExport_ServerClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PantauAssetKomponen_ " + ddlAsset.SelectedItem.ToString() + ".xls");
            Response.Charset = "utf-8";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b> PEMANTAUAN ASSET BERKOMPONEN : </b>" + ddlAsset.SelectedItem.ToString().ToUpper();
            string HtmlEnd = ddlAsset.SelectedItem.ToString().ToUpper();

            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            //Response.Write(Contents);
            Response.Flush();
            Response.End();
        }

        protected void lstAsset_DataBound(object sender, RepeaterItemEventArgs e)
        { }

        protected void ddlAsset_SelectedIndexChanged(object sender, EventArgs e)
        { }


        protected void DataBudget_DataBound(object sender, RepeaterItemEventArgs e)
        { }

        protected void DataBudget2_DataBound(object sender, RepeaterItemEventArgs e)
        { }


        private void LoadDataAsset()
        {
            ArrayList arrAsset = new ArrayList();
            FacadeAssetKomponen ast = new FacadeAssetKomponen();
            arrAsset = ast.GetAsset();

            ddlAsset.Items.Clear();
            ddlAsset.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (AssetKomponen pl in arrAsset)
            {
                ddlAsset.Items.Add(new ListItem(pl.NamaAsset, pl.KodeAsset));
            }

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
    public class FacadeAssetKomponen
    {
        public string strError = string.Empty;
        private ArrayList arrAsset = new ArrayList();
        private AssetKomponen asset = new AssetKomponen();
        private List<SqlParameter> sqlListParam;

        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }




        public ArrayList GetAsset()
        {
            string strQuery = " select ItemCode KodeAsset,ItemName NamaAsset from asset where head=1 and RowStatus>-1 order by ItemName ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            arrAsset = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAsset.Add(new AssetKomponen
                    {
                        KodeAsset = sqlDataReader["KodeAsset"].ToString(),
                        NamaAsset = sqlDataReader["NamaAsset"].ToString(),
                    });
                }
            }
            return arrAsset;
        }

        public ArrayList RetrieveGridAsset(string KodeAsset)
        {
            string strQuery00 = "" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[memantauAsset1]') AND type in (N'U')) DROP TABLE [dbo].[memantauAsset1] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[memantauAsset2]') AND type in (N'U')) DROP TABLE [dbo].[memantauAsset2] " +

                    "select left(convert(char,A1.CreatedTime,103),11)TglSPP,A1.NoSPP,A.ItemID,A.Quantity QtySPP,C.PONo NoPO,B.Qty QtyPO,D.ReceiptNo, C.Quantity QtyReceipt,A1.ItemTypeID " +
                    "into memantauAsset1 " +
                    "from SPPDetail A  " +
                    "inner join SPP A1 ON A1.ID=A.SPPID  " +
                    "inner join POPurchnDetail B ON A.ID=B.SppDetailID and B.Status>-1  " +
                    "inner join ReceiptDetail C ON B.ID=C.PODetailID and C.RowStatus>-1  " +
                    "inner join Receipt D ON D.ID=C.ReceiptID and D.Status>-1  " +
                    "where A.GroupID=12 " +
                    "and A.ItemID in (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    "and A.Status>-1  " +

                    "SELECT TglSPP,NoSPP,ItemID,sum(QtySPP)QtySPP,NoPO,sum(QtyPO)QtyPO,ReceiptNo,sum(QtyReceipt)QtyReceipt " +
                    "into memantauAsset2  " +
                    "from (  " +
                    "SELECT " +
                    "TglSPP = STUFF((SELECT DISTINCT '; ' + TglSPP FROM memantauAsset1 AS x2 WHERE x2.ItemID = x.ItemID  FOR XML PATH('')), 1, 1, ''), " +
                    "NoSPP = STUFF((SELECT DISTINCT '; ' + NoSPP FROM memantauAsset1 AS x2 WHERE x2.ItemID = x.ItemID  FOR XML PATH('')), 1, 1, ''), " +
                    "ItemID,QtySPP,  " +
                    "NoPO = STUFF((SELECT DISTINCT '; ' + NoPO FROM memantauAsset1 AS x2 WHERE x2.ItemID = x.ItemID  FOR XML PATH('')), 1, 1, ''), " +
                    "QtyPO,  " +
                    "ReceiptNo = STUFF((SELECT DISTINCT '; ' + ReceiptNo FROM memantauAsset1 AS x2 WHERE x2.ItemID = x.ItemID  FOR XML PATH('')), 1, 1, ''), sum(QtyReceipt)QtyReceipt  " +
                    "FROM memantauAsset1 AS x   " +
                    "GROUP BY TglSPP,NoSPP,ItemID,QtySPP,NoPO,QtyPO " +
                    ") as x1   " +
                    "GROUP BY TglSPP,NoSPP,ItemID,QtySPP,NoPO,ReceiptNo    " +
                    "select xii.*,((QtyReceipt+QtyAdjustIn)-QtySPB-QtyAdjustOut/**-qtyadjustoutnonstok**/) Sisa " +
                    "from (  " +
                    "select TglSPP,NoSPP,KodeAsset,NamaAsset,sum(QtySPP)QtySPP,NoPO,sum(QtyPO)QtyPO,ReceiptNo, " +
                    "sum(QtyReceipt)QtyReceipt,QtySPB,QtyAdjustOut,QtyAdjustIn, QtyAdjustOutNonStok  " +
                    "from (  " +
                    "select * from (   " +
                    "select TglSPP,NoSPP,KodeAsset,NamaAsset,isnull(QtySPP,0)QtySPP,NoPO,isnull(QtyPO,0)QtyPO,ReceiptNo, " +
                    "isnull(QtyReceipt,0)QtyReceipt,  isnull(QtySPB,0)QtySPB,QtyAdjustOut,QtyAdjustIn,QtyAdjustOutNonStok " +
                    "from (  " +
                    "select TglSPP,NoSPP,x3.ItemCode KodeAsset,x3.ItemName NamaAsset,cast(QtySPP as int)QtySPP,NoPO, " +
                    "cast(QtyPO as int)QtyPO,ReceiptNo,cast(QtyReceipt as int)QtyReceipt,cast(QtySPB as int)QtySPB,  " +
                    "cast(QtyAdjustOut as int)QtyAdjustOut, cast(QtyAdjustIn as int)QtyAdjustIn, " +
                    "cast(QtyAdjustOutNonStok as int)QtyAdjustOutNonStok " +
                    "from (   " +
                    "select *,  x.QtyPakai  QtySPB   " +
                    "from (   " +
                    "select *,( " +
                    "select sum(A.Quantity) from PakaiDetail A   " +
                    "where A.ItemID=B.ItemID and A.ItemTypeID=2 and A.GroupID=12 and A.RowStatus>-1 and A.ItemID in  (  " +
                    "select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    ")/** - ( " +
                    "select isnull(sum(B1.Quantity),0) " +
                    "from AdjustDetail B1 where B1.ItemID=B.ItemID and B1.GroupID=12 and B1.RowStatus>-1 " +
                    "and B1.ItemID in  (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    "and AdjustID in (select ID from Adjust where AdjustType='Kurang' AND nonstok = 1  AND status = 1) " +
                    ") **/ QtyPakai,   ( " +
                    "select isnull(sum(B1.Quantity),0) " +
                    "from AdjustDetail B1 where B1.ItemID=B.ItemID and B1.GroupID=12 and B1.RowStatus>-1 " +
                    "and B1.ItemID in  (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    "and AdjustID in (select ID from Adjust where AdjustType='Kurang' AND NonStok != 1  AND status = 1)  " +
                    ")QtyAdjustOut,   ( " +
                    "select isnull(sum(B1.Quantity),0) " +
                    "from AdjustDetail B1 where B1.ItemID=B.ItemID and B1.GroupID=12 and B1.RowStatus>-1 " +
                    "and B1.ItemID in  (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    "and AdjustID in  (select ID from Adjust where AdjustType='Tambah')  " +
                    ")QtyAdjustIn, ( " +
                    "select isnull(sum(B1.Quantity),0) " +
                    "from AdjustDetail B1 where B1.ItemID=B.ItemID and B1.GroupID=12 and B1.RowStatus>-1 " +
                    "and B1.ItemID in  (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    "and AdjustID in (select ID from Adjust where AdjustType='Kurang' AND nonstok = 1  AND status = 1) " +
                    ")QtyAdjustOutNonStok  " +
                    "from memantauAsset2 B " +
                    ") as x " +
                    ") as x2  " +
                    "inner join Asset x3 ON x3.ID=x2.ItemID " +
                    ") as xi   " +
                    ") as xii " +
                    ") as xiii group by TglSPP,NoSPP,KodeAsset,NamaAsset,NoPO,ReceiptNo,QtySPB,QtyAdjustOut,QtyAdjustIn, " + "QtyAdjustOutNonStok  " +

                    //"union all  "+

                    //"select left(convert(char,adj.AdjustDate,103),11) TglSPP,adj.AdjustNo NoSPP, "+
                    //"ast.ItemCode KodeAsset,ast.ItemName NamaAsset, '0'QtySPP,'-'NoPO,'0'QtyPO,'-'ReceiptNo,'0'QtyReceipt,( "+
                    //"select isnull(sum(A.Quantity),0) "+
                    //"from PakaiDetail A    "+
                    //"where A.ItemID=dtl.ItemID and A.ItemTypeID=2 and A.GroupID=12 and A.RowStatus>-1 "+
                    //"and A.ItemID in  (select ID from Asset  where ItemCode like'%"+KodeAsset+"%' and RowStatus>-1) "+
                    //")QtySPB,'0'QtyAdjustOut,dtl.Quantity QtyAdjustIn, '0' QtyAdjustOutNonStok "+
                    //"from AdjustDetail dtl   "+
                    //"inner join Adjust adj ON dtl.AdjustID=adj.ID   "+
                    //"inner join asset ast ON dtl.ItemID=ast.ID  and adj.NonStok='1'  "+
                    //"where dtl.ItemID in (select ID from asset where ItemCode like'%"+KodeAsset+"%' and RowStatus>-1) "+
                    //"and dtl.GroupID=12 and dtl.RowStatus>-1  and ast.RowStatus>-1 "+
                    ") as xii order by TglSPP,NoSPP,KodeAsset ";

            string strQuery =
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[memantauAsset1]') AND type in (N'U')) DROP TABLE [dbo].[memantauAsset1] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[memantauAsset2]') AND type in (N'U')) DROP TABLE [dbo].[memantauAsset2] " +

                    "select * into memantauAsset1 from ( " +
                    "select left(convert(char,A1.CreatedTime,103),11)TglSPP,A1.NoSPP,A.ItemID,A.Quantity QtySPP,C.PONo NoPO,B.Qty QtyPO,D.ReceiptNo, " +
                    "C.Quantity QtyReceipt,'0'QtyAdjIn,'0'QtyAdjOut,A1.ItemTypeID " +
                    //"into memantauAsset1 " +
                    "from SPPDetail A  " +
                    "inner join SPP A1 ON A1.ID=A.SPPID  " +
                    "inner join POPurchnDetail B ON A.ID=B.SppDetailID and B.Status>-1  " +
                    "inner join ReceiptDetail C ON B.ID=C.PODetailID and C.RowStatus>-1  " +
                    "inner join Receipt D ON D.ID=C.ReceiptID and D.Status>-1  " +
                    "where A.GroupID=12 " +
                    "and A.ItemID in (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    "and A.Status>-1  " +
                    "union all " +
                    "select left(convert(char,A.AdjustDate,103),11)TglAdj,A.AdjustNo,B.ItemID,'0'QtySPP,'0'NoPO,'0'QtyPO,''ReceiptNo,'0'QtyReceipt,B.Quantity " +
                    "QtyAdjIn,'0'QtyAdjOut,A.ItemTypeID from Adjust A " +
                    "inner join AdjustDetail B ON A.ID=B.AdjustID " +
                    "where  B.ItemID in (select ID from asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1 and head=0) and A.AdjustType='Tambah' " +
                    "and B.GroupID=12 and B.RowStatus>-1 and A.Status>-1 " +
                    "union all " +
                    "select left(convert(char,A.AdjustDate,103),11)TglAdj,A.AdjustNo,B.ItemID,'0'QtySPP,'0'NoPO,'0'QtyPO,''ReceiptNo,'0'QtyReceipt, " +
                    "'0'QtyAdjIn,B.Quantity QtyAdjOut,A.ItemTypeID from Adjust A  " +
                    "inner join AdjustDetail B ON A.ID=B.AdjustID " +
                    "where B.ItemID in (select ID from asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1 and head=0) and A.AdjustType='Kurang' " +
                    "and B.GroupID=12 and B.RowStatus>-1 and A.Status>-1 " +
                    ") as x " +

                    "SELECT TglSPP,NoSPP,ItemID,sum(QtySPP)QtySPP,NoPO,sum(QtyPO)QtyPO,ReceiptNo,sum(QtyReceipt)QtyReceipt " +
                    "into memantauAsset2  " +
                    "from (  " +
                    "SELECT " +
                    "TglSPP = STUFF((SELECT DISTINCT '; ' + TglSPP FROM memantauAsset1 AS x2 WHERE x2.ItemID = x.ItemID  FOR XML PATH('')), 1, 1, ''), " +
                    "NoSPP = STUFF((SELECT DISTINCT '; ' + NoSPP FROM memantauAsset1 AS x2 WHERE x2.ItemID = x.ItemID  FOR XML PATH('')), 1, 1, ''), " +
                    "ItemID,QtySPP,  " +
                    "NoPO = STUFF((SELECT DISTINCT '; ' + NoPO FROM memantauAsset1 AS x2 WHERE x2.ItemID = x.ItemID  FOR XML PATH('')), 1, 1, ''), " +
                    "QtyPO,  " +
                    "ReceiptNo = STUFF((SELECT DISTINCT '; ' + ReceiptNo FROM memantauAsset1 AS x2 WHERE x2.ItemID = x.ItemID  FOR XML PATH('')), 1, 1, ''), sum(QtyReceipt)QtyReceipt  " +
                    "FROM memantauAsset1 AS x   " +
                    "GROUP BY TglSPP,NoSPP,ItemID,QtySPP,NoPO,QtyPO " +
                    ") as x1   " +
                    "GROUP BY TglSPP,NoSPP,ItemID,QtySPP,NoPO,ReceiptNo    " +
                    "select xii.*,((QtyReceipt+QtyAdjustIn)-QtySPB-QtyAdjustOut/**-qtyadjustoutnonstok**/) Sisa " +
                    "from (  " +
                    "select TglSPP,NoSPP,KodeAsset,NamaAsset,sum(QtySPP)QtySPP,NoPO,sum(QtyPO)QtyPO,ReceiptNo, " +
                    "sum(QtyReceipt)QtyReceipt,QtySPB,QtyAdjustOut,QtyAdjustIn, QtyAdjustOutNonStok  " +
                    "from (  " +
                    "select * from (   " +
                    "select TglSPP,NoSPP,KodeAsset,NamaAsset,isnull(QtySPP,0)QtySPP,NoPO,isnull(QtyPO,0)QtyPO,ReceiptNo, " +
                    "isnull(QtyReceipt,0)QtyReceipt,  isnull(QtySPB,0)QtySPB,QtyAdjustOut,QtyAdjustIn,QtyAdjustOutNonStok " +
                    "from (  " +
                    "select TglSPP,NoSPP,x3.ItemCode KodeAsset,x3.ItemName NamaAsset,cast(QtySPP as int)QtySPP,NoPO, " +
                    "cast(QtyPO as int)QtyPO,ReceiptNo,cast(QtyReceipt as int)QtyReceipt,cast(QtySPB as int)QtySPB,  " +
                    "cast(QtyAdjustOut as int)QtyAdjustOut, cast(QtyAdjustIn as int)QtyAdjustIn, " +
                    "cast(QtyAdjustOutNonStok as int)QtyAdjustOutNonStok " +
                    "from (   " +
                    "select *,  x.QtyPakai  QtySPB   " +
                    "from (   " +
                    "select *,( " +
                    "select sum(A.Quantity) from PakaiDetail A   " +
                    "where A.ItemID=B.ItemID and A.ItemTypeID=2 and A.GroupID=12 and A.RowStatus>-1 and A.ItemID in  (  " +
                    "select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    ")/** - ( " +
                    "select isnull(sum(B1.Quantity),0) " +
                    "from AdjustDetail B1 where B1.ItemID=B.ItemID and B1.GroupID=12 and B1.RowStatus>-1 " +
                    "and B1.ItemID in  (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    "and AdjustID in (select ID from Adjust where AdjustType='Kurang' AND nonstok = 1  AND status = 1) " +
                    ") **/ QtyPakai,   ( " +
                    "select isnull(sum(B1.Quantity),0) " +
                    "from AdjustDetail B1 where B1.ItemID=B.ItemID and B1.GroupID=12 and B1.RowStatus>-1 " +
                    "and B1.ItemID in  (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    "and AdjustID in (select ID from Adjust where AdjustType='Kurang' AND NonStok != 1  AND status = 1)  " +
                    ")QtyAdjustOut,   ( " +
                    "select isnull(sum(B1.Quantity),0) " +
                    "from AdjustDetail B1 where B1.ItemID=B.ItemID and B1.GroupID=12 and B1.RowStatus>-1 " +
                    "and B1.ItemID in  (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    "and AdjustID in  (select ID from Adjust where AdjustType='Tambah')  " +
                    ")QtyAdjustIn, ( " +
                    "select isnull(sum(B1.Quantity),0) " +
                    "from AdjustDetail B1 where B1.ItemID=B.ItemID and B1.GroupID=12 and B1.RowStatus>-1 " +
                    "and B1.ItemID in  (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and RowStatus>-1) " +
                    "and AdjustID in (select ID from Adjust where AdjustType='Kurang' AND nonstok = 1  AND status = 1) " +
                    ")QtyAdjustOutNonStok  " +
                    "from memantauAsset2 B " +
                    ") as x " +
                    ") as x2  " +
                    "inner join Asset x3 ON x3.ID=x2.ItemID " +
                    ") as xi   " +
                    ") as xii " +
                    ") as xiii group by TglSPP,NoSPP,KodeAsset,NamaAsset,NoPO,ReceiptNo,QtySPB,QtyAdjustOut,QtyAdjustIn, " + "QtyAdjustOutNonStok  " +
                    ") as xii order by TglSPP,NoSPP,KodeAsset ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            arrAsset = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAsset.Add(new AssetKomponen
                    {
                        TglSPP = sqlDataReader["TglSPP"].ToString(),
                        NoSPP = sqlDataReader["NoSPP"].ToString(),
                        KodeAsset = sqlDataReader["KodeAsset"].ToString(),
                        NamaAsset = sqlDataReader["NamaAsset"].ToString(),
                        QtySPP = Convert.ToDecimal(sqlDataReader["QtySPP"]),
                        NoPO = sqlDataReader["NoPO"].ToString(),
                        QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"]),
                        ReceiptNo = sqlDataReader["ReceiptNo"].ToString(),
                        QtyReceipt = Convert.ToDecimal(sqlDataReader["QtyReceipt"]),
                        QtySPB = Convert.ToDecimal(sqlDataReader["QtySPB"]),
                        Sisa = Convert.ToDecimal(sqlDataReader["Sisa"]),
                        QtyAdjustIn = Convert.ToDecimal(sqlDataReader["QtyAdjustIn"]),
                        QtyAdjustOut = Convert.ToDecimal(sqlDataReader["QtyAdjustOut"]),
                        QtyAdjustOutNonStok = Convert.ToDecimal(sqlDataReader["QtyAdjustOutNonStok"]),
                    });

                }
            }
            return arrAsset;
        }

    }

    public class AssetKomponen
    {
        public string KodeAsset { get; set; }
        public string NamaAsset { get; set; }
        public string TglSPP { get; set; }
        public string NoSPP { get; set; }
        public string NoPO { get; set; }
        public string ReceiptNo { get; set; }
        public decimal QtySPP { get; set; }
        public decimal QtyPO { get; set; }
        public decimal QtyReceipt { get; set; }
        public decimal Sisa { get; set; }
        public decimal QtySPB { get; set; }
        public decimal QtyAdjustOut { get; set; }
        public decimal QtyAdjustIn { get; set; }
        public decimal QtyAdjustOutNonStok { get; set; }

        public DateTime Createdtime { get; set; }
        public int Urutan { get; set; }

        public string No { get; set; }
        public string Plant { get; set; }
        public string NoPol { get; set; }
        public decimal MaxBudget { get; set; }
        public decimal Actual { get; set; }
        public decimal Persen { get; set; }
        public string JenisUnit { get; set; }
        public decimal TotalStd { get; set; }
        public decimal TotalPersen { get; set; }
        public decimal TotalActual { get; set; }

        public string Bulan { get; set; }
        public string Tahun { get; set; }
        public string BulanNama { get; set; }

        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mrt { get; set; }
        public decimal Apr { get; set; }
        public decimal Mei { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Agst { get; set; }
        public decimal Sept { get; set; }
        public decimal Okt { get; set; }
        public decimal Nov { get; set; }
        public decimal Des { get; set; }
        public decimal Total { get; set; }
    }
}