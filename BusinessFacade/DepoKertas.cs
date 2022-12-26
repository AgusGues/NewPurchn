using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using System.Collections;
using System.Data.SqlClient;
using DataAccessLayer;
using System.Web;

namespace BusinessFacade
{
    public class DepoKertas : AbstractFacade
    {
        private DeliveryKertas objDepo = new DeliveryKertas();
        private QAKadarAir objKA = new QAKadarAir();
        private ArrayList arrData = new ArrayList();
        //List<SqlParameter> sqlList;
        public DepoKertas()
            : base()
        {
        }
        public override int Insert(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new DeliveryKertas();
            zl.Criteria = "DepoID,DepoName,Checker,PlantID,TglKirim,TglETA,NoSJ,Expedisi,NOPOL,GrossDepo,NettDepo,KADepo," +
                          "DocNo,SupplierID,JmlBAL,RowStatus,CreatedBy,CreatedTime,ItemCode,StdKA,Sampah,IDBeli,LMuatID,JMobil";
            zl.StoreProcedurName = "spDeliveryKertas_Input_New";
            zl.TableName = "DeliveryKertas";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int Insert(object objDomain, bool Updated)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new DeliveryKertas();
            zl.Criteria = "ID,Checker,PlantID,TglKirim,TglETA,NoSJ,Expedisi,NOPOL,GrossDepo,ItemCode," +
                          "NettDepo,KADepo,JmlBAL,RowStatus,SupplierID,LastModifiedBy,LastModifiedTime,StdKA";
            zl.StoreProcedurName = "spDeliveryKertas_Update_12";
            zl.TableName = "DeliveryKertas";
            zl.Option = "Update";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {

                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertKirim(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new DeliveryKertas();
            zl.Criteria = "DepoID,DepoName,Checker,PlantID,TglKirim,TglETA,NoSJ,Expedisi,NOPOL,GrossDepo,NettDepo,KADepo," +
                          "DocNo,SupplierID,JmlBAL,RowStatus,CreatedBy,CreatedTime,ItemCode,StdKA,Sampah,IDBeli,LMuatID,JMobil";
            zl.StoreProcedurName = "spDeliveryKertas_Input_Kirim";
            zl.TableName = "DeliveryKertas";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertKirim0(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new DeliveryKertas();
            zl.Criteria = "DepoID,DepoName,Checker,PlantID,TglKirim,TglETA,NoSJ,Expedisi,NOPOL,GrossDepo,NettDepo,KADepo," +
                          "DocNo,SupplierID,JmlBAL,RowStatus,CreatedBy,CreatedTime,ItemCode,StdKA,Sampah,IDBeli,LMuatID,JMobil,ItemName,SupplierName";
            zl.StoreProcedurName = "spDeliveryKertas_Input_Kirim0";
            zl.TableName = "DeliveryKertas";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertKirim(object objDomain, bool Updated)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new DeliveryKertas();
            zl.Criteria = "ID,Checker,PlantID,TglKirim,TglETA,NoSJ,Expedisi,NOPOL,GrossDepo,ItemCode," +
                          "NettDepo,KADepo,JmlBAL,RowStatus,SupplierID,LastModifiedBy,LastModifiedTime,StdKA,IDBeli,LMuatID,JMobil";
            zl.StoreProcedurName = "spDeliveryKertas_Update_Kirim";
            zl.TableName = "DeliveryKertas";
            zl.Option = "Update";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {

                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertBayar(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new BayarKertas();
            zl.Criteria = "IDBeli,DocNo,Penerima,TglBayar,JTempo,BGNo,AnBGNo,SupplierID,SupplierName,ItemCode,"+
                "ItemName,Harga,Qty,TotalHarga,Rowstatus,CreatedBy,DepoID,LMuatID,TypeByr";
            zl.StoreProcedurName = "spDeliveryKertas_Input_Bayar";
            zl.TableName = "DelivBayarKertas";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertBayarDP(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new BayarKertas();
            zl.Criteria = "IDBeli,DocNo,Penerima,TglBayar,JTempo,BGNo,AnBGNo,SupplierID,SupplierName,ItemCode," +
                "ItemName,Harga,Qty,TotalHarga,Rowstatus,CreatedBy,DepoID,LMuatID,TypeByr,DP";
            zl.StoreProcedurName = "spDeliveryKertas_Input_BayarDP";
            zl.TableName = "DelivBayarKertas";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertBayar(object objDomain, bool Updated)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new BayarKertas();
            zl.Criteria = "IDBeli,DocNo,Penerima,Tanggal,JTempo,BGNo,AnBGNo,SupplierID,SupplierName,ItemCode," +
                "ItemName,Harga,Qty,TotalHarga,Rowstatus";
            zl.StoreProcedurName = "spDeliveryKertas_Bayar_Bayar";
            zl.TableName = "DelivBayarKertas";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        
        public override int Update(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new DeliveryKertas();
            zl.Criteria = "ID,POKAID,LastModifiedBy,LastModifiedTime";
            zl.StoreProcedurName = "spDeliveryKertas_Update_POID";
            zl.TableName = "DeliveryKertas";
            zl.Option = "Update";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {

                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int Update(object objKA, bool KAPlant)
        {
            int result = -1;
            if (KAPlant == true)
            {
                ZetroLib zl = new ZetroLib();
                zl.hlp = new DeliveryKertas();
                zl.Criteria = "ID,POKAID,LastModifiedBy,LastModifiedTime";
                zl.StoreProcedurName = "spDeliveryKertasKA_Update_POID";
                zl.TableName = "DeliveryKertasKA";
                zl.Option = "Update";
                zl.ReturnID = false;
                string rst = zl.CreateProcedure();
                if (rst == string.Empty)
                {

                    zl.hlp = objKA;
                    result = zl.ProcessData();
                }
            }
            return result;
        }
        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }
        public override ArrayList Retrieve()
        {
            arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.SELECT;
            zw.TableName = "DeliveryKertas";
            zw.Field = "*";
            zw.Where = "Where RowStatus>-1";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList Retrieve(string Criteria)
        {
            Users users = (Users)HttpContext.Current.Session["Users"]; string query0 = string.Empty; string query1 = string.Empty;
            string query2 = string.Empty; string query3 = string.Empty;
            if (users.UnitKerjaID == 1)
            {
                query0 = "SuppPurch"; 
                query1 = "Inventory";
                query2 = "[sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch";
                query3 = "[sqlJombang.grcboard.com].bpasJombang.dbo.Inventory";
            }

            else if (users.UnitKerjaID == 7)
            {
                query0 = "[sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch"; 
                query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.Inventory";
                query2 = "[sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch";
                query3 = "[sqlJombang.grcboard.com].bpasJombang.dbo.Inventory";
            }
            else if (users.UnitKerjaID == 13)
            {
                query0 = "[sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch";
                query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.Inventory";
                query2 = "SuppPurch";
                query3 = "Inventory";
            }
            else
            {
                query0 = "[sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch";
                query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.Inventory";
                query2 = "[sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch";
                query3 = "[sqlJombang.grcboard.com].bpasJombang.dbo.Inventory";
            }

            arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "With DepoDeliverykrw AS " +
                           " ( select  * from DeliveryKertas Where RowStatus>-1 " + Criteria + ") " +
                           " SELECT d.*,x.ItemName,s.SupplierName,s.SupplierCode  " +
                           " FROM DepoDeliverykrw d  LEFT JOIN SuppPurch s ON s.ID=d.SupplierID  LEFT JOIN Inventory x ON x.ItemCode=d.ItemCode where plantid=7  " +
                           " union all " +
                           " SELECT d.*,x.ItemName,s.SupplierName,s.SupplierCode  " +
                           " FROM DepoDeliverykrw d  LEFT JOIN "+query0+" s ON s.ID=d.SupplierID  LEFT JOIN "+query1+" x ON x.ItemCode=d.ItemCode where plantid=1  " +
                           " union all " +
                           " SELECT d.*,x.ItemName,s.SupplierName,s.SupplierCode  " +
                           " FROM DepoDeliverykrw d  LEFT JOIN " + query2 + " s ON s.ID=d.SupplierID  LEFT JOIN " + query3 + " x ON x.ItemCode=d.ItemCode where plantid=13  " +
                           " ORDER By TglKirim Desc,NoSJ,DepoID";
            zw.Criteria = Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObject(sdr));
                }
            }
            return arrData;
        }
        public Boolean IsNoSJ(string noSJ)
        {
            Boolean Result = false;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select nosj from deliverykertas where rowstatus>-1 and nosj='" + noSJ.Trim() + "'";
            zw.Criteria = Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                Result = true;
            }
            return Result;
        }
        public DeliveryKertas Retrieve(string Criteria, bool Detail)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query0 = string.Empty; string query1 = string.Empty;
            string query2 = string.Empty; string query3 = string.Empty;
            if (users.UnitKerjaID == 1)
            {
                query0 = "SuppPurch";
                query1 = "Inventory";
                query2 = "[sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch";
                query3 = "[sqlJombang.grcboard.com].bpasJombang.dbo.Inventory";
            }
            else if (users.UnitKerjaID == 7)
            {
                query0 = "[sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch";
                query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.Inventory";
                query2 = "[sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch";
                query3 = "[sqlJombang.grcboard.com].bpasJombang.dbo.Inventory";
            }
            else if (users.UnitKerjaID == 13)
            {
                query0 = "[sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch";
                query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.Inventory";
                query2 = "SuppPurch";
                query3 = "Inventory";
            }
            else
            {
                query0 = "[sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch";
                query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.Inventory";
                query2 = "[sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch";
                query3 = "[sqlJombang.grcboard.com].bpasJombang.dbo.Inventory";
            }
            DeliveryKertas dkp = new DeliveryKertas();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "With DepoDeliverykrw AS " +
                           " ( select * from DeliveryKertas Where RowStatus>-1 " + Criteria + ") " +
                           " SELECT d.*,x.ItemName,s.SupplierName,s.SupplierCode  " +
                           " FROM DepoDeliverykrw d  LEFT JOIN SuppPurch s ON s.ID=d.SupplierID  LEFT JOIN Inventory x ON x.ItemCode=d.ItemCode where plantid=7  " +
                           " union all " +
                           " SELECT d.*,x.ItemName,s.SupplierName,s.SupplierCode  " +
                           " FROM DepoDeliverykrw d  LEFT JOIN " + query0 + " s ON s.ID=d.SupplierID  LEFT JOIN " + query1 + " x ON x.ItemCode=d.ItemCode where plantid=1  " +
                           " union all " +
                           " SELECT d.*,x.ItemName,s.SupplierName,s.SupplierCode  " +
                           " FROM DepoDeliverykrw d  LEFT JOIN " + query2 + " s ON s.ID=d.SupplierID  LEFT JOIN " + query3 + " x ON x.ItemCode=d.ItemCode where plantid=13  " +
                           " ORDER By TglKirim Desc,NoSJ,DepoID";
            zw.Criteria = Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObject(sdr));
                }
            }
            return dkp;
        }
        public DeliveryKertas Retrieve0(string Criteria, bool Detail)
        {
            Users users = (Users)HttpContext.Current.Session["Users"]; 
            string query0 = string.Empty; string query1 = string.Empty;
            string query2 = string.Empty; string query3 = string.Empty;
            if (users.UnitKerjaID == 1)
            {
                query0 = "SuppPurch";
                query1 = "Inventory";
                query2 = "[sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch";
                query3 = "[sqlJombang.grcboard.com].bpasJombang.dbo.Inventory";
            }
            else if (users.UnitKerjaID == 7)
            {
                query0 = "[sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch";
                query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.Inventory";
                query2 = "[sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch";
                query3 = "[sqlJombang.grcboard.com].bpasJombang.dbo.Inventory";
            }
            else if (users.UnitKerjaID == 13)
            {
                query0 = "[sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch";
                query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.Inventory";
                query2 = "SuppPurch";
                query3 = "Inventory";
            }
            else
            {
                query0 = "[sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch";
                query1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.Inventory";
                query2 = "[sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch";
                query3 = "[sqlJombang.grcboard.com].bpasJombang.dbo.Inventory";
            }
            DeliveryKertas dkp = new DeliveryKertas();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "With DepoDeliverykrw AS " +
                           " ( select * from DeliveryKertas Where RowStatus>-1 "+ Criteria+ ") " +
                           " SELECT d.*,x.ItemName,s.SupplierName,s.SupplierCode  " +
                           " FROM DepoDeliverykrw d  LEFT JOIN SuppPurch s ON s.ID=d.SupplierID  LEFT JOIN Inventory x ON x.ItemCode=d.ItemCode where plantid=7  " +
                           " union all " +
                           " SELECT d.*,x.ItemName,s.SupplierName,s.SupplierCode  " +
                           " FROM DepoDeliverykrw d  LEFT JOIN " + query0 + " s ON s.ID=d.SupplierID  LEFT JOIN " + query1 + " x ON x.ItemCode=d.ItemCode where plantid=1  " +
                           " union all " +
                           " SELECT d.*,x.ItemName,s.SupplierName,s.SupplierCode  " +
                           " FROM DepoDeliverykrw d  LEFT JOIN " + query2 + " s ON s.ID=d.SupplierID  LEFT JOIN " + query3 + " x ON x.ItemCode=d.ItemCode where plantid=13  " +
                           " ORDER By TglKirim Desc,NoSJ,DepoID";
            zw.Criteria = Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObject0(sdr));
                }
            }
            return dkp;

        }
        public ArrayList Monitoring(string Criteria)
        {
            arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT DISTINCT DepoID,DepoName FROM DeliveryKertas Where RowStatus>-1" + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObject(sdr, false));
                }
            }
            return arrData;
        }
        public ArrayList Monitoring(string Criteria,bool ListDetail)
        {
            arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery ="With DepoDelivery AS(select dk.* from DeliveryKertas dk "+
                            "Where dk.RowStatus>-1 " + Criteria + " ), " +
                            "DepoDelivery1 AS (SELECT d.*,s.SupplierName,s.SupplierCode FROM DepoDelivery d LEFT JOIN SuppPurch s ON s.ID=d.SupplierID), " +
                            "DepoDelivery2 AS ( " +
                            "    SELECT d.DepoID,d.DepoName,d.PlantID,d.NoSJ,d.NOPOL,d.TglKirim,d.ItemCode, " +
                            "    d.Expedisi,SUM(d.GrossDepo)GrossDepo,SUM(d.NettDepo)NettDepo,AVG(d.KADepo)KADepo,SUM(d.JmlBAL)JmlBal, " +
                            "    ISNULL((dk.GrossPlant),0)GrossPlant,	ISNULL((dk.NettPlant),0)NettPlant,ISNULL(AVG(dk.KAPlant),0)KAPlant, " +
                            "    ISNULL(AVG(dk.StdKA),0)StdKA,	ISNULL((dk.SelisihKA),0)SelisihKA,d.CreatedBy , d.TglETA," +
                            "    ((Select POPurchnDate from POPurchn where ID =(Select Top 1 POID from POPurchnKadarAir where SchNo=d.Nosj)))TglReceipt" +
                            "    FROM DepoDelivery1 d " +
                            "    LEFT JOIN DeliveryKertasKAPlant dk ON dk.rowstatus>-1 and dk.NoSJ=d.NoSJ AND dk.PlantID=d.PlantID AND dk.ItemCode=d.ItemCode " +
                            "    GROUP BY d.NoSJ,d.DepoID,d.ItemCode,d.DepoName,d.PlantID,d.NOPOL,d.TglKirim,TglETA,d.Expedisi,d.CreatedBy " +
                            "    ,dk.GrossPlant,dk.NettPlant,dk.SelisihKA " +
                            ") " +
                            "SELECT d2.*,inv.ItemName, " +
                            "CASE WHEN GrossPlant>0 THEN (NettPlant-NettDepo)ELSE 0 END SelisihBB, " +
                            "CASE WHEN GrossPlant>0 THEN((NettPlant-NettDepo)/NettDepo*100) ELSE 0 END Persen  " +
                            ",ISNULL((Select top 1 ISNULL(Sampah,0) From DeliveryKertasKA d "+
                            "where d.NoSJ=d2.NoSJ and d.PlantID=d2.PlantID and RowStatus>-1 AND d.ItemCode=d2.ItemCode),0) Sampah " +
                            ",ISNULL((Select AVG(Sampah) From DeliveryKertasKA d " +
                            "where d.rowstatus>-1 and d.NoSJ=d2.NoSJ and d.PlantID NOT IN(1,7,13) and RowStatus>-1 AND d.ItemCode=d2.ItemCode  " +
                            "GROUP By d.ItemCode,d.NoSJ,PlantID),0) SampahDepo,'-' NoPO " +
                            "FROM DepoDelivery2 as D2 " +
                            "LEFT JOIN Inventory as inv ON inv.ItemCode=d2.ItemCode " +
                            "ORDER By TglKirim  ,NoSJ,DepoID";

            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObject(sdr, true));
                }
            }
            return arrData;
        }
        public ArrayList Monitoring(string Criteria, bool ListDetail,string periode)
        {
            arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "With DepoDelivery AS(select * from DeliveryKertas Where RowStatus>-1 " + Criteria + " ), " +
                            "DepoDelivery1 AS (SELECT d.*,s.SupplierName,s.SupplierCode FROM DepoDelivery d LEFT JOIN SuppPurch s ON s.ID=d.SupplierID), " +
                            "DepoDelivery2 AS ( " +
                            "    SELECT d.DepoID,d.DepoName,d.PlantID,d.NoSJ,d.NOPOL,d.TglKirim,d.ItemCode, " +
                            "    d.Expedisi,SUM(d.GrossDepo)GrossDepo,SUM(d.NettDepo)NettDepo,AVG(d.KADepo)KADepo,SUM(d.JmlBAL)JmlBal, " +
                            "    ISNULL((dk.GrossPlant),0)GrossPlant,	ISNULL((dk.NettPlant),0)NettPlant,ISNULL(AVG(dk.AvgKA),0)KAPlant, " +
                            "    ISNULL(AVG(dk.StdKA),0)StdKA,	ISNULL((dk.Potongan),0)SelisihKA,d.CreatedBy , d.TglETA," +
                            "    ((Select POPurchnDate from POPurchn where ID =(Select Top 1 POID from POPurchnKadarAir where SchNo=d.Nosj)))TglReceipt" +
                            "   ,ISNULL(dk.Sampah,0)Sampah,ISNULL(dk.JmlBAL,0) BalPlant,ISNULL((Select AVG(d2.Sampah) From DeliveryKertasKA d2 "+
                            "   where d2.NoSJ=d.NoSJ and d2.PlantID not in(1,7,13) AND d2.ItemCode=d.ItemCode and RowStatus>-1),0) SampahDepo " +
                            "    FROM DepoDelivery1 d " +
                            "    LEFT JOIN DeliveryKertasKA dk ON dk.rowstatus>-1 and dk.NoSJ=d.NoSJ AND dk.PlantID=d.PlantID AND dk.ItemCode=d.ItemCode " +
                            "    GROUP BY d.NoSJ,d.DepoID,d.ItemCode,d.DepoName,d.PlantID,d.NOPOL,d.TglKirim,TglETA,d.Expedisi,d.CreatedBy " +
                            "    ,dk.GrossPlant,dk.NettPlant,dk.Potongan,dk.Sampah,dk.JmlBal " +
                            ") " +
                            "SELECT d2.*,inv.ItemName, " +
                            "CASE WHEN GrossPlant>0 THEN (NettPlant-NettDepo)ELSE 0 END SelisihBB, " +
                            "CASE WHEN GrossPlant>0 THEN((NettPlant-NettDepo)/NettDepo*100) ELSE 0 END Persen,case PlantID  " +
                                "when 1 then isnull((select top 1 nopo from [sqlctrp.grcboard.com].bpasctrp.dbo.popurchn where status>-1 and id in  " +
                                "(select top 1 pokaid from [sqlctrp.grcboard.com].bpasctrp.dbo.deliverykertas where NoSJ=D2.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')   " +
                                "when 7 then isnull((select top 1 nopo from popurchn where status>-1 and id in(select top 1 pokaid from deliverykertas where NoSJ=D2.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')   " +
                                "when 13 then isnull((select top 1 nopo from [sqljombang.grcboard.com].bpasjombang.dbo.popurchn where status>-1 and id in  " +
                                "(select top 1 pokaid from [sqljombang.grcboard.com].bpasjombang.dbo.deliverykertas where NoSJ=D2.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')   " +
                                "end NoPO  " +
                            "FROM DepoDelivery2 as D2 " +
                            "LEFT JOIN Inventory as inv ON inv.ItemCode=d2.ItemCode " +
                            "ORDER By TglKirim ,NoSJ,DepoID";

            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObject(sdr, true));
                }
            }
            return arrData;
        }
        public ArrayList Monitoring0(string Criteria, bool ListDetail, string periode,int userPlantID)
        {
            try
            {
                string strServer = string.Empty;
                string svrBayar = string.Empty;
                string svr1 = string.Empty;
                string svr2 = string.Empty;
                string svr3 = string.Empty;
                if (userPlantID == 1 || userPlantID == 13)
                {
                    svrBayar = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                }
                switch (userPlantID)
                {
                    case 1:
                        svr1 = "";
                        svr2 = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                        svr3 = "[sqljombang.grcboard.com].bpasjombang.dbo.";
                        break;
                    case 7:
                        svr1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                        svr2 = "";
                        svr3 = "[sqljombang.grcboard.com].bpasjombang.dbo.";
                        break;
                    case 13:
                        svr1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                        svr2 = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                        svr3 = "";
                        break;
                    default:
                        svr1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                        svr2 = "";
                        svr3 = "[sqljombang.grcboard.com].bpasjombang.dbo.";
                        break;
                }

                arrData = new ArrayList();
                ZetroView zw = new ZetroView();
                zw.QueryType = Operation.CUSTOM;

                string strSQL = "With DepoDelivery AS(select * from DeliveryKertas Where RowStatus>-1 " + Criteria + " ), " +
                                "DepoDelivery2 AS ( " +
                                "    SELECT distinct d.IdBeli, d.DepoID,d.DepoName,d.PlantID,d.NoSJ,d.NOPOL,d.TglKirim,d.ItemCode, " +
                                "    d.Expedisi,SUM(d.GrossDepo)GrossDepo,SUM(d.NettDepo)NettDepo,AVG(d.KADepo)KADepo,SUM(d.JmlBAL)JmlBal, " +
                                "    ISNULL((dk.GrossPlant),0)GrossPlant,	ISNULL((d.NettPlant0),0)NettPlant,0 KAPlant, " +
                                "    ISNULL(AVG(dk.StdKA),0)StdKA,	ISNULL((dk.Potongan),0)SelisihKA,d.CreatedBy , d.TglETA," +
                                //"    ((Select POPurchnDate from " + strServer + "POPurchn where ID =(Select Top 1 POID from " + strServer +
                                //"POPurchnKadarAir where SchNo=d.Nosj)))TglReceipt," +
                                "   ISNULL(dk.Sampah,0)Sampah,ISNULL(dk.JmlBAL,0) BalPlant,ISNULL((Select AVG(d2.Sampah) From DeliveryKertasKA d2 " +
                                "   where d2.NoSJ=d.NoSJ and d2.PlantID not in(1,7,13) AND d2.ItemCode=d.ItemCode and RowStatus>-1),0) SampahDepo " +
                                "    FROM DepoDelivery d " +
                                "    LEFT JOIN DeliveryKertasKA dk ON dk.rowstatus>-1 and dk.NoSJ=d.NoSJ AND dk.ItemCode=d.ItemCode and d.PlantID=dk.PlantID " +
                                "    GROUP BY d.IdBeli,d.NoSJ,d.DepoID,d.ItemCode,d.DepoName,d.PlantID,d.NOPOL,d.TglKirim,TglETA,d.Expedisi,d.CreatedBy " +
                                "    ,dk.GrossPlant,dk.NettPlant0,dk.Potongan,dk.Sampah,dk.JmlBal,d.NettPlant0 " +
                                ") " +
                                "select distinct  *  " +
                                ",case when isnull(DP,0)=0 then (select top 1 docno from "+ svrBayar+ "DelivBayarKertas where IDBeli=x.IDBeli and TypeByr=1 and isnull(DP,0)=0  ) else '-' end NoCash " +
                                ",case when isnull(DP,0)<>0  then (select docnoDP from " + svrBayar + "DelivPelunasanDP where docnoDP in (select top 1 docno from " + svrBayar + "DelivBayarKertas where IDBeli=x.IDBeli)) else '-' end NoDP " +
                                ",case when isnull(DP,0)<>0  then (select DocnoPelunasan  from " + svrBayar + "DelivPelunasanDP where docnoDP in (select top 1 docno from " + svrBayar + "DelivBayarKertas where IDBeli=x.IDBeli)) else '-' end NoPelunasan " +
                                "from ( SELECT d2.*,inv.ItemName,  " +
	                            "    CASE WHEN GrossPlant>0 and nettdepo>0 THEN (NettPlant-NettDepo)ELSE 0 END SelisihBB,  " +
	                            "    CASE WHEN GrossPlant>0 and nettdepo>0 THEN((NettPlant-NettDepo)/NettDepo*100) ELSE 0 END Persen,case PlantID  " +
	                            "    when 1 then isnull((select top 1 nopo from "+ svr1+"popurchn where status>-1 and id in " +
                                "    (select top 1 pokaid from " + svr1 + "deliverykertas where NoSJ=D2.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')    " +
                                "    when 7 then isnull((select top 1 nopo from " + svr2 + "popurchn where status>-1 and id in(select top 1 pokaid from " + svr2 + 
                                "deliverykertas where NoSJ=D2.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')    " +
                                "    when 13 then isnull((select top 1 nopo from " + svr3 + "popurchn where status>-1 and id in   " +
                                "    (select top 1 pokaid from " + svr3 + "deliverykertas where NoSJ=D2.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')    " +
                                "    end NoPO, " +
                                "case d2.PlantID  " +
                                "when 1 then isnull((((Select POPurchnDate from " + svr1 + "POPurchn where ID = (Select Top 1 POID from " + svr1 + "POPurchnKadarAir where SchNo = d2.Nosj)))),'1/1/1900')   " +
                                "when 7 then isnull((((Select POPurchnDate from " + svr2 + "POPurchn where ID = (Select Top 1 POID from " + svr2 + "POPurchnKadarAir where SchNo = d2.Nosj)))),'1/1/1900')   " +
                                "when 13 then isnull((((Select POPurchnDate from " + svr3 + "POPurchn where ID = (Select Top 1 POID from " + svr3 + "POPurchnKadarAir where SchNo = d2.Nosj)))),'1/1/1900') end  TglReceipt,  " +
                                "(select top 1 isnull(dp,0) dp from " + svr2 + "DelivBayarKertas where Rowstatus>-1 and TypeByr=1 and IDBeli in (select idbeli from " + svr2 + 
                                "DeliveryKertas where nosj=D2.nosj)) DP FROM DepoDelivery2 as D2 " +
                                "LEFT JOIN Inventory as inv ON inv.ItemCode=d2.ItemCode)as x ORDER By TglKirim ,NoSJ,DepoID ";
                zw.CustomQuery = strSQL;

                SqlDataReader sdr = zw.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(GeneratObjectPay(sdr, true));
                    }
                }
            }
            catch { }
            return arrData;
        }
        private DeliveryKertas GeneratObject(SqlDataReader sdr)
        {
            string stdKA = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            DeliveryKertas dp = new DeliveryKertas();
            dp.ID = int.Parse(sdr["ID"].ToString());
            dp.DepoID = int.Parse(sdr["DepoID"].ToString());
            dp.DepoName = sdr["DepoName"].ToString();
            dp.Checker = sdr["Checker"].ToString();
            dp.PlantID = int.Parse(sdr["PlantID"].ToString());
            dp.TglKirim = DateTime.Parse(sdr["TglKirim"].ToString());
            dp.TglETA = DateTime.Parse(sdr["TglETA"].ToString());
            dp.GrossDepo = decimal.Parse(sdr["GrossDepo"].ToString());
            dp.NettDepo = decimal.Parse(sdr["NettDepo"].ToString());
            dp.KADepo = decimal.Parse(sdr["KADepo"].ToString());
            dp.CreatedBy = sdr["CreatedBy"].ToString();
            dp.CreatedTime = DateTime.Parse(sdr["CreatedTime"].ToString());
            dp.NoSJ = sdr["NoSJ"].ToString();
            dp.Expedisi = sdr["Expedisi"].ToString();
            dp.NOPOL = sdr["NOPOL"].ToString();
            dp.POKAID = (sdr["POKAID"] == DBNull.Value) ? 0 : int.Parse(sdr["POKAID"].ToString());
            if (sdr["PlantID"].ToString() == "1") dp.KirimVia = "CT";
            if (sdr["PlantID"].ToString() == "7") dp.KirimVia = "KR";
            if (sdr["PlantID"].ToString() == "13") dp.KirimVia = "JB";
            dp.JmlBAL = decimal.Parse(sdr["JmlBAL"].ToString());
            dp.SupplierName = sdr["SupplierName"].ToString();
            dp.SupplierID = int.Parse(sdr["SupplierID"].ToString());
            dp.DocNo = sdr["DocNo"].ToString();
            dp.ItemCode = sdr["ItemCode"].ToString();
            dp.StdKA =(sdr["StdKA"]==DBNull.Value)? decimal.Parse(stdKA): decimal.Parse(sdr["StdKA"].ToString());
            dp.Sampah = (sdr["Sampah"] == DBNull.Value) ? 0 : decimal.Parse(sdr["Sampah"].ToString());
            dp.ItemName = sdr["ItemName"].ToString();
            return dp;
        }
        private DeliveryKertas GeneratObject0(SqlDataReader sdr)
        {
            string stdKA = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            DeliveryKertas dp = new DeliveryKertas();
            dp.ID = int.Parse(sdr["ID"].ToString());
            dp.DepoID = int.Parse(sdr["DepoID"].ToString());
            dp.DepoName = sdr["DepoName"].ToString();
            dp.Checker = sdr["Checker"].ToString();
            dp.PlantID = int.Parse(sdr["PlantID"].ToString());
            dp.TglKirim = DateTime.Parse(sdr["TglKirim"].ToString());
            dp.TglETA = DateTime.Parse(sdr["TglETA"].ToString());
            dp.GrossDepo = decimal.Parse(sdr["GrossDepo"].ToString());
            dp.NettDepo = decimal.Parse(sdr["NettDepo"].ToString());
            dp.KADepo = decimal.Parse(sdr["KADepo"].ToString());
            dp.CreatedBy = sdr["CreatedBy"].ToString();
            dp.CreatedTime = DateTime.Parse(sdr["CreatedTime"].ToString());
            dp.NoSJ = sdr["NoSJ"].ToString();
            dp.Expedisi = sdr["Expedisi"].ToString();
            dp.NOPOL = sdr["NOPOL"].ToString();
            dp.POKAID = (sdr["POKAID"] == DBNull.Value) ? 0 : int.Parse(sdr["POKAID"].ToString());
            if (sdr["PlantID"].ToString() == "1") dp.KirimVia = "CT";
            if (sdr["PlantID"].ToString() == "7") dp.KirimVia = "KR";
            if (sdr["PlantID"].ToString() == "13") dp.KirimVia = "JB";
            dp.JmlBAL = decimal.Parse(sdr["JmlBAL"].ToString());
            dp.SupplierName = sdr["SupplierName"].ToString();
            dp.SupplierID = int.Parse(sdr["SupplierID"].ToString());
            dp.DocNo = sdr["DocNo"].ToString();
            dp.ItemCode = sdr["ItemCode"].ToString();
            dp.StdKA = (sdr["StdKA"] == DBNull.Value) ? decimal.Parse(stdKA) : decimal.Parse(sdr["StdKA"].ToString());
            dp.Sampah = (sdr["Sampah"] == DBNull.Value) ? 0 : decimal.Parse(sdr["Sampah"].ToString());
            dp.ItemName = sdr["ItemName"].ToString();
            dp.NettPlant0 = decimal.Parse(sdr["NettPlant0"].ToString());
            return dp;
        }

        private DeliveryKertas GeneratObject(SqlDataReader sdr, bool monitoring)
        {
            DeliveryKertas dp = new DeliveryKertas();
            if (monitoring == true)
            {
                //dp.ID = int.Parse(sdr["ID"].ToString());
                dp.DepoID = int.Parse(sdr["DepoID"].ToString());
                dp.DepoName = sdr["DepoName"].ToString();
                dp.PlantID = int.Parse(sdr["PlantID"].ToString());
                dp.TglKirim = DateTime.Parse(sdr["TglKirim"].ToString());
                dp.TglETA = DateTime.Parse(sdr["TglETA"].ToString());
                dp.GrossDepo = decimal.Parse(sdr["GrossDepo"].ToString());
                dp.NettDepo = decimal.Parse(sdr["NettDepo"].ToString());
                dp.KADepo = decimal.Parse(sdr["KADepo"].ToString());
                dp.CreatedBy = sdr["CreatedBy"].ToString();
                dp.TglReceipt = (sdr["TglReceipt"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(sdr["TglReceipt"].ToString());
                dp.NoSJ = sdr["NoSJ"].ToString();
                dp.Expedisi = sdr["Expedisi"].ToString();
                dp.NOPOL = sdr["NOPOL"].ToString();
                dp.KirimVia = (sdr["PlantID"].ToString() == "1") ? "CT" : "KR";
                if (sdr["PlantID"].ToString() == "1") dp.KirimVia = "CT";
                if (sdr["PlantID"].ToString() == "7") dp.KirimVia = "KR";
                if (sdr["PlantID"].ToString() == "13") dp.KirimVia = "JB";
                dp.JmlBAL = decimal.Parse(sdr["JmlBAL"].ToString());
                dp.GrossPlant = decimal.Parse(sdr["GrossPlant"].ToString());
                dp.NettPlant = decimal.Parse(sdr["NettPlant"].ToString());
                dp.StdKA = decimal.Parse(sdr["StdKA"].ToString());
                dp.KAPlant = decimal.Parse(sdr["KAPlant"].ToString());
                dp.SelisihKA = decimal.Parse(sdr["SelisihKA"].ToString());
                dp.Persen = decimal.Parse(sdr["Persen"].ToString());
                dp.SelisihBB = decimal.Parse(sdr["SelisihBB"].ToString());
                dp.ItemCode = sdr["ItemCode"].ToString();
                dp.ItemName = sdr["ItemName"].ToString();
                dp.Sampah = decimal.Parse(sdr["Sampah"].ToString());
                dp.Jumlah = decimal.Parse(sdr["BalPlant"].ToString());
                dp.SampahDepo = decimal.Parse(sdr["SampahDepo"].ToString());
                dp.NoPO = (sdr["NoPO"] != DBNull.Value) ? sdr["NoPO"].ToString() : "-";
            }
            else
            {
                dp.DepoID = int.Parse(sdr["DepoID"].ToString());
                dp.DepoName = sdr["DepoName"].ToString();
            }
            return dp;
        }
        /// <summary>
        /// Jumlah Pengiriman Depo dalam periode
        /// </summary>
        /// <returns></returns>
        private DeliveryKertas GeneratObjectPay(SqlDataReader sdr, bool monitoring)
        {
            DeliveryKertas dp = new DeliveryKertas();
            if (monitoring == true)
            {
                //dp.ID = int.Parse(sdr["ID"].ToString());
                dp.DepoID = int.Parse(sdr["DepoID"].ToString());
                dp.DepoName = sdr["DepoName"].ToString();
                dp.PlantID = int.Parse(sdr["PlantID"].ToString());
                dp.TglKirim = DateTime.Parse(sdr["TglKirim"].ToString());
                dp.TglETA = DateTime.Parse(sdr["TglETA"].ToString());
                dp.GrossDepo = decimal.Parse(sdr["GrossDepo"].ToString());
                dp.NettDepo = decimal.Parse(sdr["NettDepo"].ToString());
                dp.KADepo = decimal.Parse(sdr["KADepo"].ToString());
                dp.CreatedBy = sdr["CreatedBy"].ToString();
                dp.TglReceipt = (sdr["TglReceipt"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(sdr["TglReceipt"].ToString());
                dp.NoSJ = sdr["NoSJ"].ToString();
                dp.Expedisi = sdr["Expedisi"].ToString();
                dp.NOPOL = sdr["NOPOL"].ToString();
                dp.KirimVia = (sdr["PlantID"].ToString() == "1") ? "CT" : "KR";
                if (sdr["PlantID"].ToString() == "1") dp.KirimVia = "CT";
                if (sdr["PlantID"].ToString() == "7") dp.KirimVia = "KR";
                if (sdr["PlantID"].ToString() == "13") dp.KirimVia = "JB";
                dp.JmlBAL = decimal.Parse(sdr["JmlBAL"].ToString());
                dp.GrossPlant = decimal.Parse(sdr["GrossPlant"].ToString());
                dp.NettPlant = decimal.Parse(sdr["NettPlant"].ToString());
                dp.StdKA = decimal.Parse(sdr["StdKA"].ToString());
                dp.KAPlant = decimal.Parse(sdr["KAPlant"].ToString());
                dp.SelisihKA = decimal.Parse(sdr["SelisihKA"].ToString());
                dp.Persen = decimal.Parse(sdr["Persen"].ToString());
                dp.SelisihBB = decimal.Parse(sdr["SelisihBB"].ToString());
                dp.ItemCode = sdr["ItemCode"].ToString();
                dp.ItemName = sdr["ItemName"].ToString();
                dp.Sampah = decimal.Parse(sdr["Sampah"].ToString());
                dp.Jumlah = decimal.Parse(sdr["BalPlant"].ToString());
                dp.SampahDepo = decimal.Parse(sdr["SampahDepo"].ToString());
                dp.NoPO = (sdr["NoPO"] != DBNull.Value) ? sdr["NoPO"].ToString() : "-";
                dp.NoCash = sdr["NoCash"].ToString();
                dp.NoDP = sdr["NoDP"].ToString();
                dp.NoPelunasan = sdr["NoPelunasan"].ToString();
            }
            else
            {
                dp.DepoID = int.Parse(sdr["DepoID"].ToString());
                dp.DepoName = sdr["DepoName"].ToString();
            }
            return dp;
        }
        public int DocumentNo()
        {
            int result = 0;
            string strSQL = "SELECT COUNT(ID)Jml FROM DeliveryKertas WHERE YEAR(TglKirim)=" + DateTime.Now.Year + " AND RowStatus>-1";
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["Jml"].ToString());
                }
            }
            return result;
        }
        public string CheckAtthConfirmasi(string NoSJ)
        {
            string result = "";
            string strSQL = "select * from BeritaAcaraAttachment where NoSJ='" + NoSJ + "' and RowStatus>-1 and docName='Konfirmasi'";
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["FileName"].ToString();
                }
            }
            return result;
        }
        public string Criteria { get; set; }
        public ArrayList Retrieve4PO()
        {
            arrData = new ArrayList();
            string strSQL = "WITH KertasKA AS ( "+
                            "SELECT * FROM DeliveryKertasKA  "+
                            "WHERE (isnull(POKAID,0)=0) " + 
                            //"AND RowStatus>-1 /*AND MONTH(tglCheck)>=4 AND YEAR(TglCheck)>2016*/ "+
                            "AND RowStatus>-1 AND LEFT(CONVERT(CHAR,tglCheck,112),6)>='201710' " +
                            "AND Approval>1 AND PLANTID IN(1,7,13) AND NoSJ='0' "+
                            ") "+
                            ",KertasKA1 AS "+
                            "( "+
                            //" SELECT * FROM DeliveryKertas where TglReceipt IS NULL AND RowStatus>-1  /*AND YEAR(TglKirim)>2016 AND MONTH(TglKirim)>=4 */"+
                            " SELECT * FROM DeliveryKertas where TglReceipt IS NULL AND RowStatus>-1  AND LEFT(CONVERT(CHAR,TglKirim,112),6)>='201710' " +
                            " AND (isnull(POKAID,0)=0) " +
                            " AND Nosj IN(Select Nosj From DeliveryKertasKA where DeliveryKertasKA.Nosj= DeliveryKertas.NoSJ and DeliveryKertasKA.PlantID in(1,7,13) "+
                            " AND DeliveryKertasKA.ItemCode=DeliveryKertas.ItemCode "+
                            //" /*AND  (POKAID IS NULL OR POKAID <1)*/ AND RowStatus>-1 /*AND MONTH(tglCheck)>=4 AND YEAR(TglCheck)>2016*/ AND Approval>1) "+
                            " AND RowStatus>-1 AND LEFT(CONVERT(CHAR,tglCheck,112),6)>='201710' AND Approval>1 and DeliveryKertasKA.NettPlant>0) " +
                            ") "+
                            " ,KertasKA2 AS ( "+
	                        "     SELECT * FROM ( "+
                            "         SELECT KertasKA1.ID,DepoName,NoSJ,PlantID,KertasKA1.ItemCode,v.ItemName,SupplierID,s.SupplierName,TglKirim,NOPOL,GrossDepo,NettDepo,KADepo,StdKA," +
                            "         1 Urutan,ISNULL(POKAID,0)POKAID,0 Sampah  "+
		                    "         From KertasKA1 "+
		                    "         LEFT JOIN SuppPurch s ON s.ID=KertasKA1.SupplierID "+
                            "         LEFT JOIN Inventory v ON v.ItemCode=KertasKA1.ItemCode "+
		                    "         UNION "+
                            "         SELECT KertasKA.ID,CASE WHEN NoSJ='0' THEN 'Lokal' ELSE '' END DepoName,NoSJ,PlantID,KertasKA.ItemCode,v.ItemName,SupplierID,SupplierName,TglCheck,NOPOL,GrossPlant, " +
                            "         NettPlant,AvgKA,StdKA,2 urutan,ISNULL(POKAID,0)POKAID,ISNULL(Sampah,0) Sampah " +
		                    "         FROM KertasKA  "+
                            "         LEFT JOIN Inventory v ON v.ItemCode=KertasKA.ItemCode " +
		                    "         WHERE NoSJ in(SELECT Nosj From KertasKA1) OR NoSJ='0' "+
	                        "     ) AS X "+
                            " ) "+
                            " SELECT * FROM KertasKA2 "+
                            this.Criteria +
                            " ORDER BY TglKirim desc,ID desc,NoSJ,ItemCode  " +
                            " ,Urutan,SupplierName";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObject4PO(sdr));
                }
            }
            return arrData;
        }
        private DeliveryKertas GeneratObject4PO(SqlDataReader sdr)
        {
            DeliveryKertas objDepo = new DeliveryKertas();
            objDepo.ID = int.Parse(sdr["ID"].ToString());
            objDepo.RowStatus = int.Parse(sdr["Urutan"].ToString());
            objDepo.DepoName = sdr["DepoName"].ToString();
            objDepo.PlantID = int.Parse(sdr["PlantID"].ToString());
            objDepo.NoSJ = sdr["NoSJ"].ToString();
            objDepo.NOPOL = sdr["Nopol"].ToString();
            objDepo.SupplierID = int.Parse(sdr["SupplierID"].ToString());
            objDepo.SupplierName = sdr["SupplierName"].ToString();
            objDepo.TglKirim = DateTime.Parse(sdr["TglKirim"].ToString());
            objDepo.GrossPlant = decimal.Parse(sdr["GrossDepo"].ToString());
            objDepo.NettPlant = decimal.Parse(sdr["NettDepo"].ToString());
            objDepo.KADepo = decimal.Parse(sdr["KADepo"].ToString());
            objDepo.StdKA = decimal.Parse(sdr["StdKA"].ToString());
            objDepo.Sampah = decimal.Parse(sdr["Sampah"].ToString());
            objDepo.POKAID = int.Parse(sdr["POKAID"].ToString());
            objDepo.ItemName = sdr["ItemName"].ToString();
            objDepo.ItemCode = sdr["ItemCode"].ToString();
            objDepo.SchID = LastID4SomeNOSJ(objDepo);
            objDepo.Jumlah = LastID4SomeNOSJ(objDepo, true);
            return objDepo;
            //KertasKA1.ID,DepoName,NoSJ,PlantID,ItemCode,SupplierID,s.SupplierName,TglKirim,NOPOL,GrossDepo,NettDepo,KADepo,StdKA, 1 Urutan,POKAID

        }
        private int LastID4SomeNOSJ(DeliveryKertas objDepo)
        {
            int result = 0;
            DeliveryKertas d = (DeliveryKertas)objDepo;
            string strSQL = (d.RowStatus == 1) ? "SELECT TOP 1 ID FROM DeliveryKertas WHERE NoSJ='" + d.NoSJ + "' AND ID NOT IN(" + d.ID + ") AND (POKAID<1 OR POKAID IS NULL) AND RowStatus>-1 ORDER BY ID" :
                                        "SELECT TOP 1 ID FROM DeliveryKertasKA WHERE NetPlant='" + d.NettPlant + "' AND ID NOT IN(" + d.ID + ") AND GrossPlant=" + d.GrossPlant +
                                        " AND ItemCode='" + d.ItemCode + "' AND (POKAID<1 OR POKAID IS NULL) AND RowStatus>-1 ORDER BY ID";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        private int LastID4SomeNOSJ(DeliveryKertas objDepo, bool Count)
        {
            int result = 0;
            DeliveryKertas d = (DeliveryKertas)objDepo;
            string strSQL = (d.RowStatus == 1) ? "SELECT COUNT(ID)ID FROM DeliveryKertas WHERE NoSJ='" + d.NoSJ + "' AND RowStatus>-1 ORDER BY ID" :
                                            "SELECT COUNT(ID)ID FROM DeliveryKertasKA WHERE NettPlant='" + d.NettPlant + "' AND GrossPlant=" + d.GrossPlant + 
                                            " AND ItemCode='"+d.ItemCode+"'  AND RowStatus>-1 ORDER BY ID";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        public ArrayList OpenSPP(string ItemCode)
        {
            arrData = new ArrayList();
            string strSQL = "WITH DataSPP AS ( " +
                        "SELECT s.ID,s.NoSPP,s.Minta,(select dbo.ItemCodeInv(sd.ItemID,1))ItemCode,"+
                        "(select dbo.ItemNameInv(sd.ItemID,1))ItemName,ItemID,sd.ID SPPDetailID " +
                        "FROM SPP s " +
                        "LEFT JOIN SPPDetail as sd ON sd.SPPID=s.ID " +
                        "WHERE (sd.Quantity-sd.QtyPO)>0 AND sd.Status  >-1 AND s.Status>-1 AND s.Approval=3 " +
                        "AND ItemID in(SELECT ID FROM Inventory WHERE ItemCode IN(Select ItemCode FROM DeliveryKertasKA Group By ItemCode)) " +
                        "AND YEAR(s.Minta)>2016 " +
                        ") " +
                        "SELECT * FROM DataSPP WHERE ItemCode='" + ItemCode + "' order by ID";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObjectOpenPO(sdr));
                }
            }
            return arrData;
        }
        private DeliveryKertas GeneratObjectOpenPO(SqlDataReader sdr)
        {
            DeliveryKertas dp = new DeliveryKertas();
            dp.ID = int.Parse(sdr["ID"].ToString());
            dp.NOSPP = sdr["NoSPP"].ToString();
            dp.ItemCode = sdr["ItemCode"].ToString();
            dp.ItemID = int.Parse(sdr["ItemID"].ToString());
            dp.ItemName = sdr["ItemName"].ToString();
            dp.SPPDetailID = int.Parse(sdr["SPPDetailID"].ToString());
            return dp;
        }
        public ArrayList Retrieve4PO(bool p)
        {
            arrData = new ArrayList();
            string strSQL = "WITH KertasKA AS ( " +
                            "SELECT * FROM DeliveryKertasKA  " +
                            "WHERE POKAID >0 " +
                            //"AND RowStatus>-1 /*AND MONTH(tglCheck)>4 AND YEAR(TglCheck)>2016*/ " +
                            "AND RowStatus>-1 AND LEFT(CONVERT(CHAR,tglCheck,112),6)>='201710' " +
                            "AND Approval>1 AND PLANTID IN(1,7,13) AND NoSJ='0' " +
                            ") " +
                            ",KertasKA1 AS " +
                            "( " +
                            //" SELECT * FROM DeliveryKertas where /*TglReceipt IS NULL AND*/ RowStatus>-1 AND YEAR(TglKirim)>2016 AND MONTH(TglKirim)>4 " +
                            " SELECT * FROM DeliveryKertas where RowStatus>-1 AND LEFT(CONVERT(CHAR,TglKirim,112),6)>='201710' " +
                            " /*AND Nosj IN(Select Nosj From DeliveryKertasKA where DeliveryKertasKA.Nosj= DeliveryKertas.NoSJ and DeliveryKertasKA.PlantID in(1,7,13)*/ " +
                            //" AND  POKAID >0  /*AND RowStatus>-1 AND MONTH(tglCheck)>4 AND YEAR(TglCheck)>2016 AND Approval>1)*/ " +
                             " AND  POKAID >0  AND LEFT(CONVERT(CHAR,tglCheck,112),6)>='201710' AND Approval>1) " +
                            ") " +
                            " ,KertasKA2 AS ( " +
                            "     SELECT * FROM ( " +
                            "         SELECT KertasKA1.ID,DepoName,NoSJ,PlantID,KertasKA1.ItemCode,v.ItemName,SupplierID,s.SupplierName,TglKirim,NOPOL,GrossDepo,NettDepo,KADepo,StdKA," +
                            "         1 Urutan,ISNULL(POKAID,0)POKAID,0 Sampah  " +
                            "         From KertasKA1 " +
                            "         LEFT JOIN SuppPurch s ON s.ID=KertasKA1.SupplierID " +
                            "         LEFT JOIN Inventory v ON v.ItemCode=KertasKA1.ItemCode " +
                            "         UNION " +
                            "         SELECT KertasKA.ID,CASE WHEN NoSJ='0' THEN 'Lokal' ELSE '' END DepoName,NoSJ,PlantID,KertasKA.ItemCode,v.ItemName,SupplierID,SupplierName,TglCheck,NOPOL,GrossPlant, " +
                            "         NettPlant,AvgKA,StdKA,2 urutan,ISNULL(POKAID,0)POKAID,ISNULL(Sampah,0) Sampah " +
                            "         FROM KertasKA  " +
                            "         LEFT JOIN Inventory v ON v.ItemCode=KertasKA.ItemCode " +
                            "         WHERE NoSJ in(SELECT Nosj From KertasKA1) OR NoSJ='0' " +
                            "     ) AS X " +
                            " ) " +
                            " SELECT KKA.*,p.NOPO,Pd.DocumentNo FROM KertasKA2 KKA " +
                            " LEFT JOIN POPUrchnKadarAir pk ON pk.ID=KKA.POKAID "+
                            " LEFT JOIN POPurchnDetail Pd ON Pd.POID=pk.POKAID " +
                            " LEFT JOIN POPurchn P ON P.ID=pk.POKAID "+
                            this.Criteria +
                            " ORDER BY TglKirim desc,ID desc,NoSJ,ItemCode  " +
                            " ,Urutan,SupplierName";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObject4PO(sdr, GeneratObject4PO(sdr)));
                }
            }
            return arrData;
        }
        private DeliveryKertas GeneratObject4PO(SqlDataReader sdr, DeliveryKertas deliveryKertas)
        {
            DeliveryKertas dp = (DeliveryKertas)deliveryKertas;
            dp.NoPO = sdr["NoPO"].ToString();
            dp.NOSPP = sdr["DocumentNo"].ToString();
            return dp;
        }
    }
    public class DepoKertasKA :AbstractFacade
    {
        private QAKadarAir objQA = new QAKadarAir();
        private BayarKertas objByr = new BayarKertas();
        private ArrayList arrQA = new ArrayList();
        public DepoKertasKA()
            : base()
        {

        }
        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }
        public override int Update(object objDomain)
        {
            DeliveryKertas objQK = (DeliveryKertas)objDomain;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.TableName = "DeliveryKertas";
            zw.CustomQuery = "Update DeliveryKertas set POKAID=" + objQK.POKAID + ", LastModifiedBy='" + objQK.LastModifiedBy + "', LastModifiedTime=GETDATE() " +
                             "where ID=" + objQK.ID + " AND PlantID=" + objQK.PlantID;

            SqlDataReader sdr = zw.Retrieve();
            return sdr.RecordsAffected;
        }
        public int Approval(string App,string ID)
        {
            try
            {
                Users user = (Users)System.Web.HttpContext.Current.Session["Users"];
                ZetroView zw = new ZetroView();
                zw.QueryType = Operation.CUSTOM;
                zw.TableName = "DeliveryKertasKA";
                zw.CustomQuery = "Update DeliveryKertasKA set Approval=" + App + ", LastModifiedBy='" + user.UserName + "', LastModifiedTime=GETDATE() " +
                                 "where ID in(" + ID.Substring(0, ID.Length - 1) + ")";
                SqlDataReader sdr = zw.Retrieve();
                return sdr.RecordsAffected;
            }
            catch
            {
                return -1;
            }
        }
        public override int Insert(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new QAKadarAir();
            zl.Criteria = "TglCheck,ItemCode,ItemName,NoSJ,NOPOL,SupplierID,SupplierName,GrossPlant,NettPlant,JmlBAL,JmlSample,JmlSampleBasah" +
                          ",ProsSampleBasah,BeratPotong,Potongan,AvgKA,CreatedBy,CreatedTime,RowStatus,DocNo,PlantID,Sampah,BeratSample,BeratSampah,StdKA,Approval";
            zl.StoreProcedurName = "spDeliveryKertasKA_Input_A";
            zl.TableName = "DeliveryKertasKA";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int Insert0(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new QAKadarAir();
            zl.Criteria = "TglCheck,ItemCode,ItemName,NoSJ,NOPOL,SupplierID,SupplierName,GrossPlant,NettPlant,JmlBAL,JmlSample,JmlSampleBasah" +
                          ",ProsSampleBasah,BeratPotong,Potongan,AvgKA,CreatedBy,CreatedTime,RowStatus,DocNo,PlantID,Sampah,BeratSample,"+
                          "BeratSampah,StdKA,Approval,Netto,Potongan2";
            zl.StoreProcedurName = "spDeliveryKertasKA_Input_A0";
            zl.TableName = "DeliveryKertasKA";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }

        public int Insert2(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new QAKadarAir();
            zl.Criteria = "DKKAID,Netto,Brutto,Potongan2,DefaultStdKA";
            zl.StoreProcedurName = "spDeliveryKertasStd";
            zl.TableName = "DeliveryKertasStd";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }


        public int InsertDetail(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new QAKadarAir();
            zl.Criteria = "DKKAID,NoBall,BALKe,Tusuk1,Tusuk2,AvgTusuk,RowStatus,CreatedBy,CreatedTime";
            zl.StoreProcedurName = "spDeliveryKertasKADetail_Input";
            zl.TableName = "DeliveryKertasKADetail";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if(rst==string.Empty){
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertBeliKA(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new QAKadarAir();
            zl.Criteria = "TglCheck,ItemCode,ItemName,SupplierID,SupplierName,GrossPlant,NettPlant,JmlBAL,JmlSample,JmlSampleBasah" +
                          ",ProsSampleBasah,BeratPotong,Potongan,AvgKA,CreatedBy,CreatedTime,RowStatus,DocNo,PlantID,Sampah,BeratSample," +
                          "BeratSampah,StdKA,DepoName,Checker,DepoID";
            zl.StoreProcedurName = "spDelivBeliKA_Input_A";
            zl.TableName = "DelivBeliKA";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertBeliTK(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new QAKadarAir();
            zl.Criteria = "TglCheck,ItemCode,ItemName,SupplierID,SupplierName,GrossPlant,NettPlant,JmlBAL,JmlSample,JmlSampleBasah" +
                          ",ProsSampleBasah,BeratPotong,Potongan,AvgKA,CreatedBy,CreatedTime,RowStatus,DocNo,PlantID,Sampah,BeratSample," +
                          "BeratSampah,StdKA,DepoName,Checker,DepoID,Harga,Total,Expedisi";
            zl.StoreProcedurName = "spDelivBeliTK_Input_A";
            zl.TableName = "DelivBeliTK";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertBeliKADetail(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new QAKadarAir();
            zl.Criteria = "DKKAID,NoBall,BALKe,Tusuk1,Tusuk2,AvgTusuk,RowStatus,CreatedBy,CreatedTime";
            zl.StoreProcedurName = "spDelivBeliKADetail_Input";
            zl.TableName = "DelivBeliKADetail";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public override ArrayList Retrieve()
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT * FROM DeliveryKertasKA where RowStatus>-1";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GeneratObject(sdr));
                }
            }
            return arrQA;
        }

        public  ArrayList Retrieve(string Criteria)
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT * FROM DeliveryKertasKA where rowstatus > -1 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GeneratObject(sdr));
                }
            }
            return arrQA;
        }
        public ArrayList RetrieveList(string Criteria)
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT *,case when A.RowStatus>-1 then 'Aktif' else 'Cancel' end [Status] FROM DeliveryKertasKA A left join DeliveryKertasKA_cancel B on A.ID=B.DKKAID where rowstatus > -2 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GeneratObjectList(sdr));
                }
            }
            return arrQA;
        }
        public ArrayList RetrieveBeli(string Criteria)
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select case PlantID " +
                "when 1 then isnull((select top 1 nopo from [sqlctrp.grcboard.com].bpasctrp.dbo.popurchn where status>-1 and id in " +
                "(select top 1 pokaid from [sqlctrp.grcboard.com].bpasctrp.dbo.deliverykertas where NoSJ=beli.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')  " +
                "when 7 then isnull((select top 1 nopo from popurchn where status>-1 and id in(select top 1 pokaid from deliverykertas where NoSJ=beli.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')  " +
                "when 13 then isnull((select top 1 nopo from [sqljombang.grcboard.com].bpasjombang.dbo.popurchn where status>-1 and id in " +
                "(select top 1 pokaid from [sqljombang.grcboard.com].bpasjombang.dbo.deliverykertas where NoSJ=beli.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')  " +
                "end NoPO,* from ( " +
                "SELECT isnull((select nosj from deliverykertas where idbeli=A.ID and rowstatus>-1),'-')NoSJ,* " +
                "FROM DelivBeliKA A where RowStatus>-1 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GeneratObjectBeli(sdr));
                }
            }
            return arrQA;
        }

        public int  getplantidbeli(string Criteria)
        {
            int plantID = 0;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT plantid FROM DelivBeliKA where RowStatus>-1 and id=" + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    plantID = int.Parse(sdr["plantid"].ToString());
                }
            }
            return plantID;
        }

        public ArrayList RetrieveBayar(string Criteria)
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT (select Deponame from Depo where ID=DelivBayarKertas.depoID)DepoName,idbeli,DocNo, Penerima, TglBayar, JTempo, "+
                "BGNo, AnBGNo, SupplierID, SupplierName, ItemCode, ItemName,Harga,Qty,TotalHarga, Rowstatus, CreatedBy, CreatedTime, DepoID, " +
                "LMuatID, TypeByr, BayarExp, isnull(DP,0) DP FROM DelivBayarKertas where RowStatus>-1 " + Criteria ;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GeneratObjectBayar(sdr));
                }
            }
            return arrQA;
        }
        public ArrayList RetrieveBayarCetak(string Criteria)
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = 
                "SELECT (select Deponame from Depo where ID=DelivBayarKertas.depoID)DepoName, "+
                "idbeli,DocNo, Penerima, TglBayar, JTempo,BGNo, AnBGNo, SupplierID, SupplierName, ItemCode, ItemName,"+
                "case when (select NettPlant0 from DeliveryKertas where IDBeli=DelivBayarKertas.IDBeli and RowStatus >-1 )>0 then  " +
                "cast (TotalHarga/(select NettPlant0 from DeliveryKertas where IDBeli=DelivBayarKertas.IDBeli and RowStatus >-1 ) as decimal(18,2)) else harga end Harga,  " +
                "case when (select NettPlant0 from DeliveryKertas where IDBeli=DelivBayarKertas.IDBeli and RowStatus >-1 )>0 then  " +
                "(select NettPlant0 from DeliveryKertas where IDBeli=DelivBayarKertas.IDBeli and RowStatus >-1 ) else Qty end Qty,TotalHarga, Rowstatus, CreatedBy, CreatedTime, DepoID, " +
                "LMuatID, TypeByr, BayarExp, isnull(DP,0) DP FROM DelivBayarKertas where rowstatus>-1  " + Criteria +
                " union all SELECT top 1 (select Deponame from Depo where ID=A.depoID)DepoName,idbeli,DocNo, Penerima, TglBayar, JTempo, " +
                "BGNo, AnBGNo, SupplierID, SupplierName, ItemCode,'Pembayaran DP ' + (select case when (select count(ID) from delivpelunasandp where "+
                "docnopelunasan=A.docno)=0 then '' else ' BG.NO ' + (select top 1 bgno + ' An.' + Anbgno + ' ' + CONVERT(char,TglBayar,106) " +
                "from DelivBayarKertas where DocNo=((select docnoDP from delivpelunasanDP where docnopelunasan =A.docno)) ) end ) ItemName,0 Harga, 0 Qty,isnull(DP,0)*-1 TotalHarga, Rowstatus, CreatedBy, CreatedTime, DepoID, " +
                "LMuatID, TypeByr, BayarExp, isnull(DP,0)DP FROM DelivBayarKertas A where rowstatus>-1 and totalharga>0 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GeneratObjectBayar(sdr));
                }
            }
            return arrQA;
        }
        public ArrayList RetrieveBayarCetakDP(string Criteria)
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT (select Deponame from Depo where ID=DelivBayarKertas.depoID)DepoName,idbeli,DocNo, Penerima, TglBayar, JTempo, " +
                "BGNo, AnBGNo, SupplierID, SupplierName, ItemCode, ItemName,case when (select NettPlant0 from DeliveryKertas where IDBeli=DelivBayarKertas.IDBeli )>0 then  " +
                "cast (TotalHarga/(select NettPlant0 from DeliveryKertas where IDBeli=DelivBayarKertas.IDBeli ) as decimal(18,2)) else harga end Harga,  " +
                "case when (select NettPlant0 from DeliveryKertas where IDBeli=DelivBayarKertas.IDBeli )=0 then  " +
                "(select NettPlant0 from DeliveryKertas where IDBeli=DelivBayarKertas.IDBeli ) else Qty end Qty,TotalHarga, Rowstatus, CreatedBy, CreatedTime, DepoID, " +
                "LMuatID, TypeByr, BayarExp, isnull(DP,0) DP FROM DelivBayarKertas where rowstatus>-1 and totalharga=0 " + Criteria +
                " union all SELECT top 1 (select Deponame from Depo where ID=DelivBayarKertas.depoID)DepoName,idbeli,DocNo, Penerima, TglBayar, JTempo, " +
                "BGNo, AnBGNo, SupplierID, SupplierName, ItemCode,'Pembayaran DP ' ItemName,0 Harga, 0 Qty,isnull(DP,0) TotalHarga, Rowstatus, CreatedBy, CreatedTime, DepoID, " +
                "LMuatID, TypeByr, BayarExp, isnull(DP,0)DP FROM DelivBayarKertas where rowstatus>-1 and totalharga>0 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GeneratObjectBayar(sdr));
                }
            }
            return arrQA;
        }
        public BayarKertas RetrieveBayar1(string Criteria)
        {
            arrQA = new ArrayList();
            BayarKertas dlv = new BayarKertas();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT (select Deponame from Depo where ID=DelivBayarKertas.depoID)DepoName,* FROM DelivBayarKertas where rowstatus>-1 " +
                Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dlv = GeneratObjectBayar(sdr);
                }
            }
            return dlv;
        }

        public string RetrieveBayar2(string Criteria)
        {
            BayarKertas dlv = new BayarKertas();
            ZetroView zw = new ZetroView();
            string BGno = string.Empty;
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select top 1 'BG No.' + BGno + '              An. ' + AnBGNo+ '                      ' +CONVERT(char,jtempo,107)  BGNo  from DelivBayarKertas where RowStatus>-1 " +
                Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    BGno = sdr["BGNo"].ToString();
                }
            }
            return BGno;
        }

        public ArrayList RetrieveBayarExp(string Criteria)
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT (select Deponame from Depo where ID=DelivBayarKertas.depoID)DepoName, "+
                "isnull((select NoSJ from Deliverykertas where IDbeli=DelivBayarKertas.IDbeli and rowstatus>-1),'')nosj,* FROM DelivBayarKertas " +
                "where RowStatus>-1" +
                Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GeneratObjectBayarExp(sdr));
                }
            }
            return arrQA;
        }

        public ArrayList RetrieveKirimExpedisi(string Criteria)
        {
            Users users = (Users)HttpContext.Current.Session["Users"]; string query0 = string.Empty;
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select * from (select  case A.plantid when 7 then (select count (id) from DeliveryKertasKA where nosj=A.nosj and " +
                "A.PlantID=PlantID and rowstatus>-1) when 1 then (select count (id) from [sqlctrp.grcboard.com].bpasctrp.dbo.DeliveryKertasKA " +
                "where nosj=A.nosj and A.PlantID=PlantID and rowstatus>-1) when 13 then (select count (id) from [sqljombang.grcboard.com].bpasjombang.dbo.DeliveryKertasKA where nosj=A.nosj and A.PlantID=PlantID and rowstatus>-1) end terima,A.*, " + 
                "B.ItemName,B.SupplierName,M.LokasiMuat,M.TypeMobil JMobil,C.Address TujuanKirim " +
                "from DeliveryKertas A inner join Delivbelika B on A.IDBeli=B.ID inner join DelivLokasiMuat M on A.LMuatID =M.ID inner join Depo C on A.PlantID=C.ID  " +
                "where ISNULL(A.idbeli,0)>0 and isnull(A.bayarexp,0)=0 and A.RowStatus>-1)atAll where terima>0 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GeneratObjectKirimExp(sdr));
                }
            }
            return arrQA;
        }

        public DeliveryKertas RetrieveKirimExpedisi1(string Criteria)
        {
            arrQA = new ArrayList();
            DeliveryKertas dlv = new DeliveryKertas();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select * from (select  (select count (id) from DeliveryKertasKA where nosj=A.nosj and A.PlantID=PlantID)terima,A.*, B.ItemName,B.SupplierName,M.LokasiMuat,M.TypeMobil JMobil,C.Address TujuanKirim " +
                "from DeliveryKertas A inner join Delivbelika B on A.IDBeli=B.ID inner join DelivLokasiMuat M on A.LMuatID =M.ID inner join Depo C on A.PlantID=C.ID  " +
                "where ISNULL(idbeli,0)>0 and isnull(A.bayarexp,0)=0 and A.RowStatus>-1)atAll where terima>0 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dlv=GeneratObjectKirimExp(sdr);
                }
            }
            return dlv;
        }
        public DeliveryKertas RetrieveKirimExpedisi2(string Criteria)
        {
            Users users = (Users)HttpContext.Current.Session["Users"]; string query0 = string.Empty; string query1 = string.Empty;
            arrQA = new ArrayList();
            DeliveryKertas dlv = new DeliveryKertas();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select * from (select  (select count (id) from [sqlctrp.grcboard.com].bpasctrp.dbo.DeliveryKertasKA "
                +"where nosj=A.nosj and A.PlantID=PlantID)terima,A.*, " +
                "B.ItemName,B.SupplierName,M.LokasiMuat,M.TypeMobil JMobil,C.Address TujuanKirim " +
                "from DeliveryKertas A inner join Delivbelika B on A.IDBeli=B.ID inner join DelivLokasiMuat M on "+
                "A.LMuatID =M.ID inner join Depo C on A.PlantID=C.ID  " +
                "where ISNULL(idbeli,0)>0 and isnull(A.bayarexp,0)=0 and A.RowStatus>-1)atAll where terima>0 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dlv = GeneratObjectKirimExp(sdr);
                }
            }
            return dlv;
        }
        public DeliveryKertas RetrieveKirimExpedisi3(string Criteria)
        {
            Users users = (Users)HttpContext.Current.Session["Users"]; string query0 = string.Empty; string query1 = string.Empty;
            arrQA = new ArrayList();
            DeliveryKertas dlv = new DeliveryKertas();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select * from (select  (select count (id) from [sqljombang.grcboard.com].bpasjombang.dbo.DeliveryKertasKA "
                + "where nosj=A.nosj and A.PlantID=PlantID)terima,A.*, " +
                "B.ItemName,B.SupplierName,M.LokasiMuat,M.TypeMobil JMobil,C.Address TujuanKirim " +
                "from DeliveryKertas A inner join Delivbelika B on A.IDBeli=B.ID inner join DelivLokasiMuat M on " +
                "A.LMuatID =M.ID inner join Depo C on A.PlantID=C.ID  " +
                "where ISNULL(idbeli,0)>0 and isnull(A.bayarexp,0)=0 and A.RowStatus>-1)atAll where terima>0 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dlv = GeneratObjectKirimExp(sdr);
                }
            }
            return dlv;
        }
        public BayarKertas RetrieveHeaderBayarPelunasan(string docno)
        {
            BayarKertas BK = new BayarKertas();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = " select  depoid,DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo,"+
                " sum(harga*qty)-(select top 1 isnull(DP,0) dp from DelivBayarKertas where docno='" + docno +
                "' ) TotalHarga,typebyr,isnull(avg(dp),0) DP from DelivBayarKertas " +
                " where rowstatus>-1 and typebyr=1 and isnull(dp,0)>0 and docno='" + docno + 
                "' group by  depoid,DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo,typebyr";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    BK= GeneratObjectHeaderBayar(sdr);
                }
            }
            return BK;
        }
        public BayarKertas RetrieveHeaderBayarDP(string docno)
        {
            BayarKertas BK = new BayarKertas();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = " select  depoid,DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo,sum(TotalHarga)TotalHarga,typebyr,isnull(avg(dp),0) DP from DelivBayarKertas " +
                " where rowstatus>-1 and typebyr=1 and isnull(dp,0)>0 and docno='" + docno + "' group by  depoid,DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo,typebyr";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    BK = GeneratObjectHeaderBayar(sdr);
                }
            }
            return BK;
        }
        public BayarKertas RetrieveHeaderBayar(string docno)
        {
            BayarKertas BK = new BayarKertas();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = " select  depoid,DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo,sum(TotalHarga)TotalHarga,typebyr,isnull(avg(dp),0) DP  from DelivBayarKertas " +
                " where rowstatus>-1 and typebyr=1 and isnull(dp,0)=0 and docno='" + docno + "' group by  depoid,DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo,typebyr";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    BK = GeneratObjectHeaderBayar(sdr);
                }
            }
            return BK;
        }
        public BayarKertas RetrieveHeaderBayarExp(string docno)
        {
            BayarKertas BK = new BayarKertas();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = " select  depoid,DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo,sum(TotalHarga)TotalHarga,typebyr,isnull(avg(dp),0) DP  from DelivBayarKertas " +
                " where rowstatus>-1 and typebyr=2 and docno='" + docno + "' group by  depoid,DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo,typebyr";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    BK = GeneratObjectHeaderBayar(sdr);
                }
            }
            return BK;
        }
        public QAKadarAir Retrieve(string Criteria, bool detail)
        {
            QAKadarAir dkp = new QAKadarAir();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.Criteria = Criteria;
            zw.CustomQuery = "SELECT * FROM DeliveryKertasKA where RowStatus>-1 AND Approval>-1 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObject(sdr));
                }
            }
            return dkp;
        }
        public QAKadarAir Retrieve0(string Criteria, bool detail)
        {
            QAKadarAir dkp = new QAKadarAir();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.Criteria = Criteria;
            zw.CustomQuery = "SELECT ID,TglCheck, ItemName, NoSJ, NOPOL, SupplierID, SupplierName, GrossPlant, NettPlant, JmlBal, JmlSample, "+
                "JmlSampleBasah, ProsSampleBasah, BeratPotong, Potongan, AvgKA, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime,  "+
                "RowStatus, DocNo, PlantID, ItemCode, POKAID, Sampah, BeratSample, BeratSampah, StdKA, Approval, ApprovalBy, ApprovalDate,  "+
                "IDBeli, isnull(NettPlant0,0) NettPlant0, isnull(BeratPotong0,0) BeratPotong0 FROM DeliveryKertasKA where RowStatus>-1 AND Approval>1 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObject0(sdr));
                }
            }
            return dkp;
        }
        public QAKadarAir Retrieve2(string Criteria, bool detail)
        {
            QAKadarAir dkp = new QAKadarAir();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.Criteria = Criteria;
            zw.CustomQuery = "select *,StdKA from DeliveryKertasStd where DKKAID in (select ID from DeliveryKertasKA where rowstatus>-1 and " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObject2(sdr));
                }
            }
            return dkp;
        }
        public QAKadarAir RetrieveBeli(string Criteria, bool detail)
        {
            QAKadarAir dkp = new QAKadarAir();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.Criteria = Criteria;
            zw.CustomQuery = "select  case PlantID " +
                "when 1 then isnull((select top 1 nopo from [sqlctrp.grcboard.com].bpasctrp.dbo.popurchn where status>-1 and id in " +
                "(select top 1 pokaid from [sqlctrp.grcboard.com].bpasctrp.dbo.deliverykertas where NoSJ=beli.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')  " +
                "when 7 then isnull((select top 1 nopo from popurchn where status>-1 and id in(select top 1 pokaid from deliverykertas where NoSJ=beli.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')  " +
                "when 13 then isnull((select top 1 nopo from [sqljombang.grcboard.com].bpasjombang.dbo.popurchn where status>-1 and id in " +
                "(select top 1 pokaid from [sqljombang.grcboard.com].bpasjombang.dbo.deliverykertas where NoSJ=beli.NoSJ  and rowstatus>-1 order by id desc) order by id desc),'-')  " +
                "end NoPO,* from ( " +
                "SELECT isnull((select nosj from deliverykertas where idbeli=A.ID and rowstatus>-1),'-')NoSJ,* " +
                "FROM DelivBeliKA A  where RowStatus>-1  " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObjectBeli(sdr));
                }
            }
            return dkp;
        }
        public ArrayList Tahun()
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT DISTINCT YEAR(TglCheck)Tahun FROM DeliveryKertasKA";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(new QAKadarAir { Tahun = int.Parse(sdr["Tahun"].ToString()) });
                }
            }
            return arrQA;
        }
        public int QAKadarAirDocNo()
        {
            int result = 0;
            string strSQL = "SELECT COUNT(ID)Jml FROM DeliveryKertasKA WHERE YEAR(TglCheck)=" + DateTime.Now.Year;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["Jml"].ToString());
                }
            }
            return result;
        }
        public int QAKadarAirDocNoBeli()
        {
            int result = 0;
            string strSQL = "SELECT top 1 cast(substring(docno,10,5) as int) Jml FROM DelivBeliKA WHERE YEAR(TglCheck)=" + DateTime.Now.Year + 
                " order by cast(substring(docno,10,5) as int) desc" ;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["Jml"].ToString());
                }
            }
            return result;
        }
        public int QABeliDocNo()
        {
            int result = 0;
            string strSQL = "SELECT COUNT(ID)Jml FROM DelivBeliKA WHERE YEAR(TglCheck)=" + DateTime.Now.Year;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["Jml"].ToString());
                }
            }
            return result;
        }
        public ArrayList RetrieveKADetail(string Criteria)
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT * FROM DeliveryKertasKADetail where RowStatus>-1 " + Criteria;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GenerateObject(sdr, true));
                }
            }
            return arrQA;
        }
        public ArrayList RetrieveKADetail(string Criteria, bool Cetak)
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = 
                //" SELECT DKKAID,NoBall, BALKE,SUM(Tusuk1)Tusuk1,SUM(Tusuk2)Tusuk2,SUM(AvgTusuk)AvgTusuk,   SUM(NoBal1)NoBal1, " +
                //           " SUM(Balke1)Balke1,SUM(Tusuk11)Tusuk11,SUM(Tusuk21)Tusuk21,SUM(AvgTusuk1)AvgTusuk1 FROM " +
                //           " ( " +
                //           "     SELECT  DKKAID,isnull(NoBall,0)NoBall,BALKe,Tusuk1,Tusuk2,AvgTusuk,0 NoBal1,0 BALKe1, 0 Tusuk11, 0 Tusuk21, 0 AvgTusuk1  " +
                //           "     FROM DeliveryKertasKADetail WHERE  BALKe <=25  " + Criteria +
                //           "     UNION All " +
                //           "     SELECT DKKAID,isnull(NoBall,0)NoBall,(BALKe-25)BALKe,0 Tusuk1,0 Tusuk2,0 AvgTusuk,NoBall,BALKe, Tusuk1, Tusuk2, AvgTusuk  " +
                //           "     FROM DeliveryKertasKADetail WHERE BALKe >25  " + Criteria +
                //           " ) as x " +
                //           " Group By Noball,DKKAID,Balke " +
                //           " ORDER BY BALKe";

            "  SELECT DKKAID, isnull( (select noball from DeliveryKertasKADetail WHERE balke=x.BALKe  " + Criteria + "),0)NoBall, " +
            "  BALKE,SUM(Tusuk1)Tusuk1,SUM(Tusuk2)Tusuk2,SUM(AvgTusuk)AvgTusuk,    " +
            "   isnull((select noball from DeliveryKertasKADetail WHERE  balke=x.BALKe+25   " + Criteria + "),0)NoBall1,  " +
            "   (x.BALKe+25) Balke1,SUM(Tusuk11)Tusuk11,SUM(Tusuk21)Tusuk21,SUM(AvgTusuk1)AvgTusuk1 FROM  (    " +
            "     SELECT  DKKAID,isnull(BALKe,0)BALKe,Tusuk1,Tusuk2,AvgTusuk,0 BALKe1, 0 Tusuk11, 0 Tusuk21, 0 AvgTusuk1 " +
            "     FROM DeliveryKertasKADetail WHERE  BALKe <=25    " + Criteria + "      " +
            "     UNION All       " +
            "     SELECT DKKAID,(BALKe-25)BALKe,0 Tusuk1,0 Tusuk2,0 AvgTusuk,BALKe, Tusuk1, Tusuk2, AvgTusuk    " +
            "     FROM DeliveryKertasKADetail WHERE BALKe >25   " + Criteria + "  " +
            "     ) as x  Group By DKKAID,Balke  ORDER BY BALKE ";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GenerateObject(sdr, true, GenerateObject(sdr,true)));
                }
            }
            return arrQA;
        }
        public ArrayList RetrieveBeliKADetail(string Criteria, bool Cetak)
        {
            arrQA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery =
            "  SELECT DKKAID, isnull( (select top 1 noball from DelivBeliKADetail WHERE balke=x.BALKe  " + Criteria + "),0)NoBall, " +
            "  BALKE,SUM(Tusuk1)Tusuk1,SUM(Tusuk2)Tusuk2,SUM(AvgTusuk)AvgTusuk,    " +
            "   isnull((select top 1 noball from DelivBeliKADetail WHERE  balke=x.BALKe+25   " + Criteria + "),0)NoBall1,  " +
            "   SUM(Balke1)Balke1,SUM(Tusuk11)Tusuk11,SUM(Tusuk21)Tusuk21,SUM(AvgTusuk1)AvgTusuk1 FROM  (    " +
            "     SELECT  DKKAID,BALKe,Tusuk1,Tusuk2,AvgTusuk,0 BALKe1, 0 Tusuk11, 0 Tusuk21, 0 AvgTusuk1 " +
            "     FROM DelivBeliKADetail WHERE  BALKe <=25    " + Criteria + "      " +
            "     UNION All       " +
            "     SELECT DKKAID,(BALKe-25)BALKe,0 Tusuk1,0 Tusuk2,0 AvgTusuk,BALKe, Tusuk1, Tusuk2, AvgTusuk    " +
            "     FROM DelivBeliKADetail WHERE BALKe >25   " + Criteria + "  " +
            "     ) as x  Group By DKKAID,Balke  ORDER BY BALKE ";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrQA.Add(GenerateObject(sdr, true, GenerateObject(sdr, true)));
                }
            }
            return arrQA;
        }
        public int GetNoDocBayar(string tahun)
        {
            int result = 0;
            string strSQL = "SELECT distinct top 1 substring(docno,5,5) docno FROM DelivBayarKertas where rowstatus>-1 and year(tglbayar)=" + tahun + " order by docno desc ";
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["docno"].ToString());
                }
            }
            return result;
        }
        public int GetNoDocBayarDP(string tahun)
        {
            int result = 0;
            string strSQL = "SELECT distinct top 1 substring(docno,5,5) docno FROM DelivBayarKertasDP where rowstatus>-1 and year(tglbayar)=" + tahun + " order by docno desc ";
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["docno"].ToString());
                }
            }
            return result;
        }
        public int RecordFound( string criteria)
        {
            ArrayList arrData = new ArrayList();
            arrData = this.Retrieve(criteria);
            return arrData.Count;
        }
        private QAKadarAir GeneratObject(SqlDataReader sdr)
        {
            string stdKA = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            objQA = new QAKadarAir();
            objQA.TglCheck = DateTime.Parse(sdr["TglCheck"].ToString());
            objQA.ItemCode = sdr["ItemCode"].ToString();
            objQA.NoSJ = sdr["NoSJ"].ToString();
            objQA.NOPOL = sdr["NOPOL"].ToString();
            objQA.GrossPlant = decimal.Parse(sdr["GrossPlant"].ToString());
            objQA.ItemName = sdr["ItemName"].ToString();
            objQA.JmlBAL = (sdr["JmlBAL"] != DBNull.Value) ? decimal.Parse(sdr["JmlBAL"].ToString()) : 0;
            objQA.JmlSample = decimal.Parse(sdr["JmlSample"].ToString());
            objQA.JmlSampleBasah = decimal.Parse(sdr["JmlSampleBasah"].ToString());
            objQA.BeratPotong = decimal.Parse(sdr["BeratPotong"].ToString());
            objQA.Potongan = decimal.Parse(sdr["Potongan"].ToString());
            objQA.ProsSampleBasah = decimal.Parse(sdr["ProsSampleBasah"].ToString());
            objQA.AvgKA = decimal.Parse(sdr["AvgKA"].ToString());
            objQA.NettPlant = decimal.Parse(sdr["NettPlant"].ToString());
            objQA.SupplierName = sdr["SupplierName"].ToString();
            objQA.SupplierID = int.Parse(sdr["SupplierID"].ToString());
            objQA.Keputusan = int.Parse(sdr["RowStatus"].ToString());
            objQA.Sampah = decimal.Parse(sdr["Sampah"].ToString());
            objQA.POKAID = (sdr["POKAID"] != DBNull.Value) ? int.Parse(sdr["POKAID"].ToString()) : 0;
            objQA.DocNo = sdr["DocNo"].ToString().ToUpper();
            objQA.ID = int.Parse(sdr["ID"].ToString());
            objQA.BeratSampah = decimal.Parse(sdr["BeratSampah"].ToString());
            objQA.BeratSample = decimal.Parse(sdr["BeratSample"].ToString());
            objQA.StdKA = (sdr["StdKA"] != DBNull.Value) ? decimal.Parse(sdr["StdKA"].ToString()) : decimal.Parse(stdKA.ToString());
            objQA.Approval = int.Parse(sdr["Approval"].ToString());

            return objQA;
        }
        private QAKadarAir GeneratObjectList(SqlDataReader sdr)
        {
            string stdKA = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            objQA = new QAKadarAir();
            objQA.TglCheck = DateTime.Parse(sdr["TglCheck"].ToString());
            objQA.ItemCode = sdr["ItemCode"].ToString();
            objQA.NoSJ = sdr["NoSJ"].ToString();
            objQA.NOPOL = sdr["NOPOL"].ToString();
            objQA.GrossPlant = decimal.Parse(sdr["GrossPlant"].ToString());
            objQA.ItemName = sdr["ItemName"].ToString();
            objQA.JmlBAL = (sdr["JmlBAL"] != DBNull.Value) ? decimal.Parse(sdr["JmlBAL"].ToString()) : 0;
            objQA.JmlSample = decimal.Parse(sdr["JmlSample"].ToString());
            objQA.JmlSampleBasah = decimal.Parse(sdr["JmlSampleBasah"].ToString());
            objQA.BeratPotong = decimal.Parse(sdr["BeratPotong"].ToString());
            objQA.Potongan = decimal.Parse(sdr["Potongan"].ToString());
            objQA.ProsSampleBasah = decimal.Parse(sdr["ProsSampleBasah"].ToString());
            objQA.AvgKA = decimal.Parse(sdr["AvgKA"].ToString());
            objQA.NettPlant = decimal.Parse(sdr["NettPlant"].ToString());
            objQA.SupplierName = sdr["SupplierName"].ToString();
            objQA.SupplierID = int.Parse(sdr["SupplierID"].ToString());
            objQA.Keputusan = int.Parse(sdr["RowStatus"].ToString());
            objQA.Sampah = decimal.Parse(sdr["Sampah"].ToString());
            objQA.POKAID = (sdr["POKAID"] != DBNull.Value) ? int.Parse(sdr["POKAID"].ToString()) : 0;
            objQA.DocNo = sdr["DocNo"].ToString().ToUpper();
            objQA.ID = int.Parse(sdr["ID"].ToString());
            objQA.BeratSampah = decimal.Parse(sdr["BeratSampah"].ToString());
            objQA.BeratSample = decimal.Parse(sdr["BeratSample"].ToString());
            objQA.StdKA = (sdr["StdKA"] != DBNull.Value) ? decimal.Parse(sdr["StdKA"].ToString()) : decimal.Parse(stdKA.ToString());
            objQA.Approval = int.Parse(sdr["Approval"].ToString());
            objQA.status = sdr["status"].ToString();
            objQA.alasan = sdr["alasan"].ToString();

            return objQA;
        }
        private QAKadarAir GeneratObject0(SqlDataReader sdr)
        {
            string stdKA = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            objQA = new QAKadarAir();
            objQA.TglCheck = DateTime.Parse(sdr["TglCheck"].ToString());
            objQA.ItemCode = sdr["ItemCode"].ToString();
            objQA.NoSJ = sdr["NoSJ"].ToString();
            objQA.NOPOL = sdr["NOPOL"].ToString();
            objQA.GrossPlant = decimal.Parse(sdr["GrossPlant"].ToString());
            objQA.ItemName = sdr["ItemName"].ToString();
            objQA.JmlBAL = (sdr["JmlBAL"] != DBNull.Value) ? decimal.Parse(sdr["JmlBAL"].ToString()) : 0;
            objQA.JmlSample = decimal.Parse(sdr["JmlSample"].ToString());
            objQA.JmlSampleBasah = decimal.Parse(sdr["JmlSampleBasah"].ToString());
            objQA.BeratPotong = decimal.Parse(sdr["BeratPotong"].ToString());
            objQA.Potongan = decimal.Parse(sdr["Potongan"].ToString());
            objQA.ProsSampleBasah = decimal.Parse(sdr["ProsSampleBasah"].ToString());
            objQA.AvgKA = decimal.Parse(sdr["AvgKA"].ToString());
            objQA.NettPlant = decimal.Parse(sdr["NettPlant"].ToString());
            objQA.SupplierName = sdr["SupplierName"].ToString();
            objQA.SupplierID = int.Parse(sdr["SupplierID"].ToString());
            objQA.Keputusan = int.Parse(sdr["RowStatus"].ToString());
            objQA.Sampah = decimal.Parse(sdr["Sampah"].ToString());
            objQA.POKAID = (sdr["POKAID"] != DBNull.Value) ? int.Parse(sdr["POKAID"].ToString()) : 0;
            objQA.DocNo = sdr["DocNo"].ToString().ToUpper();
            objQA.ID = int.Parse(sdr["ID"].ToString());
            objQA.BeratSampah = decimal.Parse(sdr["BeratSampah"].ToString());
            objQA.BeratSample = decimal.Parse(sdr["BeratSample"].ToString());
            objQA.StdKA = (sdr["StdKA"] != DBNull.Value) ? decimal.Parse(sdr["StdKA"].ToString()) : decimal.Parse(stdKA.ToString());
            objQA.Approval = int.Parse(sdr["Approval"].ToString());
            objQA.NettPlant0 = decimal.Parse(sdr["NettPlant0"].ToString());
            objQA.BeratPotong0 = decimal.Parse(sdr["BeratPotong0"].ToString());
            return objQA;
        }
        private QAKadarAir GeneratObject2(SqlDataReader sdr)
        {          
            objQA = new QAKadarAir();
            objQA.Netto = decimal.Parse(sdr["Netto"].ToString());
            objQA.Brutto = decimal.Parse(sdr["Brutto"].ToString());
            objQA.Potongan = decimal.Parse(sdr["Potongan"].ToString());
            objQA.StdKA = decimal.Parse(sdr["StdKA"].ToString());
            return objQA;
        }
        private QAKadarAir GeneratObjectBeli(SqlDataReader sdr)
        {
            string stdKA = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            objQA = new QAKadarAir();
            objQA.TglCheck = DateTime.Parse(sdr["TglCheck"].ToString());
            objQA.ItemCode = sdr["ItemCode"].ToString();
            objQA.GrossPlant = decimal.Parse(sdr["GrossPlant"].ToString());
            objQA.ItemName = sdr["ItemName"].ToString();
            objQA.JmlBAL = (sdr["JmlBAL"] != DBNull.Value) ? decimal.Parse(sdr["JmlBAL"].ToString()) : 0;
            objQA.JmlSample = decimal.Parse(sdr["JmlSample"].ToString());
            objQA.JmlSampleBasah = decimal.Parse(sdr["JmlSampleBasah"].ToString());
            objQA.BeratPotong = decimal.Parse(sdr["BeratPotong"].ToString());
            objQA.Potongan = decimal.Parse(sdr["Potongan"].ToString());
            objQA.ProsSampleBasah = decimal.Parse(sdr["ProsSampleBasah"].ToString());
            objQA.AvgKA = decimal.Parse(sdr["AvgKA"].ToString());
            objQA.NettPlant = decimal.Parse(sdr["NettPlant"].ToString());
            objQA.Checker = sdr["Checker"].ToString();
            objQA.SupplierName = sdr["SupplierName"].ToString();
            objQA.SupplierID = int.Parse(sdr["SupplierID"].ToString());
            objQA.Keputusan = int.Parse(sdr["RowStatus"].ToString());
            objQA.Sampah = decimal.Parse(sdr["Sampah"].ToString());
            objQA.DocNo = sdr["DocNo"].ToString().ToUpper();
            objQA.ID = int.Parse(sdr["ID"].ToString());
            objQA.BeratSampah = decimal.Parse(sdr["BeratSampah"].ToString());
            objQA.BeratSample = decimal.Parse(sdr["BeratSample"].ToString());
            objQA.StdKA = (sdr["StdKA"] != DBNull.Value) ? decimal.Parse(sdr["StdKA"].ToString()) : decimal.Parse(stdKA.ToString());
            objQA.DepoID = int.Parse(sdr["DepoID"].ToString());
            objQA.PlantID = int.Parse(sdr["PlantID"].ToString());
            objQA.NoSJ = sdr["NoSJ"].ToString();
            objQA.NoPO = sdr["NoPO"].ToString();
            return objQA;
        }
        private DeliveryKertas GeneratObjectKirimExp(SqlDataReader sdr)
        {
            DeliveryKertas dp = new DeliveryKertas();
            //dp.ID = int.Parse(sdr["ID"].ToString());
            dp.DepoID = int.Parse(sdr["DepoID"].ToString());
            dp.DepoName = sdr["DepoName"].ToString();
            dp.PlantID = int.Parse(sdr["PlantID"].ToString());
            dp.IDBeli = int.Parse(sdr["IDBeli"].ToString());
            dp.LMuatID = int.Parse(sdr["LMuatID"].ToString());
            dp.TglKirim = DateTime.Parse(sdr["TglKirim"].ToString());
            dp.TglETA = DateTime.Parse(sdr["TglETA"].ToString());
            dp.GrossDepo = decimal.Parse(sdr["GrossDepo"].ToString());
            dp.NettDepo = decimal.Parse(sdr["NettDepo"].ToString());
            dp.KADepo = decimal.Parse(sdr["KADepo"].ToString());
            dp.CreatedBy = sdr["CreatedBy"].ToString();
            dp.TglReceipt = (sdr["TglReceipt"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(sdr["TglReceipt"].ToString());
            dp.NoSJ = sdr["NoSJ"].ToString();
            dp.Expedisi = sdr["Expedisi"].ToString();
            dp.NOPOL = sdr["NOPOL"].ToString();
            dp.KirimVia = (sdr["PlantID"].ToString() == "1") ? "CT" : "KR";
            dp.JmlBAL = decimal.Parse(sdr["JmlBAL"].ToString());
            dp.SupplierName = sdr["SupplierName"].ToString();
            dp.Checker = sdr["Checker"].ToString();
            dp.LokasiMuat = sdr["LokasiMuat"].ToString();
            dp.JMobil = sdr["JMobil"].ToString();
            dp.TujuanKirim = sdr["TujuanKirim"].ToString();
            dp.TypeByr = 2;
            //dp.GrossPlant = decimal.Parse(sdr["GrossPlant"].ToString());
            //dp.NettPlant = decimal.Parse(sdr["NettPlant"].ToString());
            dp.StdKA = decimal.Parse(sdr["StdKA"].ToString());
            //dp.KAPlant = decimal.Parse(sdr["KAPlant"].ToString());
            //dp.SelisihKA = decimal.Parse(sdr["SelisihKA"].ToString());
            //dp.Persen = decimal.Parse(sdr["Persen"].ToString());
            //dp.SelisihBB = decimal.Parse(sdr["SelisihBB"].ToString());
            dp.ItemCode = sdr["ItemCode"].ToString();
            dp.ItemName = sdr["ItemName"].ToString();
            dp.Sampah = decimal.Parse(sdr["Sampah"].ToString());
            //dp.Jumlah = decimal.Parse(sdr["BalPlant"].ToString());
            //dp.SampahDepo = decimal.Parse(sdr["SampahDepo"].ToString());
            return dp;
        }
        private BayarKertas GeneratObjectBayar(SqlDataReader sdr)
        {
            objByr = new BayarKertas();
            objByr.DepoID = Int32.Parse(sdr["DepoID"].ToString());
            objByr.DepoName = sdr["DepoName"].ToString();
            //objByr.PlantID = int.Parse(sdr["PlantID"].ToString());
            objByr.IDBeli = Int32.Parse(sdr["IDBeli"].ToString());
            objByr.BGNo = sdr["BGNo"].ToString();
            objByr.AnBGNo = sdr["AnBGNo"].ToString();
            objByr.TglBayar = DateTime.Parse(sdr["TglBayar"].ToString());
            objByr.JTempo = DateTime.Parse(sdr["JTempo"].ToString());
            objByr.SupplierName = sdr["SupplierName"].ToString();
            objByr.ItemName = sdr["ItemName"].ToString();
            objByr.DocNo = sdr["DocNo"].ToString();
            objByr.Harga = decimal.Parse(sdr["Harga"].ToString());
            objByr.Qty = decimal.Parse(sdr["Qty"].ToString());
            objByr.TotalHarga = decimal.Parse(sdr["TotalHarga"].ToString());
            objByr.Penerima = sdr["Penerima"].ToString();
            objByr.CreatedBy = sdr["CreatedBy"].ToString();
            objByr.NoSJ = string.Empty ;
            return objByr;
        }
        private BayarKertas GeneratObjectBayarExp(SqlDataReader sdr)
        {
            objByr = new BayarKertas();
            objByr.DepoID = Int32.Parse(sdr["DepoID"].ToString());
            objByr.DepoName = sdr["DepoName"].ToString();
            //objByr.PlantID = int.Parse(sdr["PlantID"].ToString());
            objByr.IDBeli = Int32.Parse(sdr["IDBeli"].ToString());
            objByr.BGNo = sdr["BGNo"].ToString();
            objByr.AnBGNo = sdr["AnBGNo"].ToString();
            objByr.TglBayar = DateTime.Parse(sdr["TglBayar"].ToString());
            objByr.JTempo = DateTime.Parse(sdr["JTempo"].ToString());
            objByr.SupplierName = sdr["SupplierName"].ToString();
            objByr.ItemName = sdr["ItemName"].ToString();
            objByr.DocNo = sdr["DocNo"].ToString();
            objByr.Harga = decimal.Parse(sdr["Harga"].ToString());
            objByr.Qty = decimal.Parse(sdr["Qty"].ToString());
            objByr.TotalHarga = decimal.Parse(sdr["TotalHarga"].ToString());
            objByr.Penerima = sdr["Penerima"].ToString();
            objByr.CreatedBy = sdr["CreatedBy"].ToString();
            objByr.NoSJ = sdr["NoSJ"].ToString();
            return objByr;
        }
        private BayarKertas GeneratObjectHeaderBayar(SqlDataReader sdr)
        {
            objByr = new BayarKertas();
            objByr.DepoID = Int32.Parse(sdr["DepoID"].ToString());
            objByr.BGNo = sdr["BGNo"].ToString();
            objByr.AnBGNo = sdr["AnBGNo"].ToString();
            objByr.TglBayar = DateTime.Parse(sdr["TglBayar"].ToString());
            objByr.JTempo = DateTime.Parse(sdr["JTempo"].ToString());
            objByr.DocNo = sdr["DocNo"].ToString();
            objByr.TotalHarga = decimal.Parse(sdr["TotalHarga"].ToString());
            objByr.Penerima = sdr["Penerima"].ToString();
            objByr.TypeByr = Int32.Parse(sdr["typebyr"].ToString());
            objByr.DP = Int32.Parse(sdr["DP"].ToString());
            return objByr;
        }
        private QAKadarAir GenerateObject(SqlDataReader sdr, bool Detail)
        {
            objQA = new QAKadarAir();
            objQA.NoBall = int.Parse(sdr["Noball"].ToString());
            objQA.BALKe = int.Parse(sdr["BALKe"].ToString());
            objQA.Tusuk1 = decimal.Parse(sdr["Tusuk1"].ToString());
            objQA.Tusuk2 = decimal.Parse(sdr["Tusuk2"].ToString());
            objQA.AvgTusuk = decimal.Parse(sdr["AvgTusuk"].ToString());
            objQA.DKKAID = int.Parse(sdr["DKKAID"].ToString());
            return objQA;
        }
        
        private QAKadarAir GenerateObject(SqlDataReader sdr, bool Detail, QAKadarAir QA)
        {
            objQA =(QAKadarAir)QA;
            objQA.NoBall1 = int.Parse(sdr["Noball1"].ToString());
            objQA.BALKe1 = int.Parse(sdr["BALKe1"].ToString());
            objQA.Tusuk11 = decimal.Parse(sdr["Tusuk11"].ToString());
            objQA.Tusuk21 = decimal.Parse(sdr["Tusuk21"].ToString());
            objQA.AvgTusuk1 = decimal.Parse(sdr["AvgTusuk1"].ToString());
            return objQA;
        }
        //rekapan delivery kertas
        public string Bulans { get; set; }
        public string Tahuns { get; set; }
        private Users user = (Users)System.Web.HttpContext.Current.Session["Users"];
        private int LokasiPabrik()
        {
            int result = 0;
            switch (user.UnitKerjaID)
            {
                case 1: result = 1; break;
                default: result = 7; break;
            }
            return result;
        }
        private string QueryRekapan()
        {
            string LastApvBA = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LastApproval", "BeritaAcara");
            string strSql = "WITH Pemantauan AS( " +
                          "  SELECT inv.ItemName,inv.ItemCode,ba.BANum,CONVERT(CHAR,ba.BADate,103)Tanggal,r.ReceiptNo RMSNo,ba.ID BAID, " +
                          "  r.ReceiptDate,ba.TotalSup NetDepo,ba.Netto NetBPAS, " +
                          "  (ba.Netto-ba.TotalSup)NetSelisih,ba.DepoKertasID,bad.SupplierName " +
                          "  FROM BeritaAcaraDetail bad " +
                          "  LEFT JOIN BeritaAcara ba on bad.BAID=ba.ID " +
                          "  LEFT JOIN Receipt r on r.id=bad.ReceiptNo " +
                          "  LEFT JOIN ReceiptDetail rd On rd.ReceiptID=r.ID " +
                          "  LEFT JOIN Inventory inv on inv.ID=rd.ItemID " +
                          "  LEFT JOIN BeritaAcaraApproval baa on baa.BAID=ba.ID  " +
                          "  WHERE MONTH(BADate)="+this.Bulans+" AND YEAR(BADate)="+this.Tahuns+" and baa.UserID= " +LastApvBA+
                         "), " +
                         "Pemantauan1 AS( " +
                         "SELECT z.itemCode,z.ItemName,z.BANum,z.Tanggal,z.ReceiptDate,(SELECT dbo.ListRMSNoToRows(z.BAID)) AS RMSNo, z.NetDepo, " +
                         "(z.NetBPAS)NetBPAS,((z.NetBPAS)- z.NetDepo)NetSelisih,z.BAID,(SELECT dbo.ListSupplierToRows(z.BAID)) AS SupplierName  " +
                         "FROM Pemantauan AS z " +

                         "GROUP BY z.BAID,z.BANum,z.ReceiptDate,z.itemCode,z.ItemName,z.NetDepo,z.Tanggal,z.NetBPAS,z.NetDepo " +
                         "),Pemantauan2 AS ( " +
                         "SELECT x.ItemName,x.BANum,x.Tanggal,c.DepoName,x.SupplierName, x.RMSNo, x.NetBPAS,x.NetDepo, " +
                         "x.NetSelisih, Case When x.NetSelisih!=0 Then ROUND(((x.NetSelisih)/(x.NetDepo))*100,2) ELSE 0 END as Persen ,1 OrderBy, " +
                         "d.NoSJ,d.CreatedTime,dk.TglKirim " +
                         "FROM Pemantauan1 as x " +
                         "LEFT JOIN BeritaAcara AS b ON b.ID=x.BAID " +
                         "LEFT JOIN DepoKertas as C ON C.ID=b.DepoKertasID " +
                         "LEFT JOIN BeritaAcaraAttachment AS D on D.BAID=b.ID  and docName='Konfirmasi' and d.RowStatus>-1" +
                         "LEFT JOIN DeliveryKertas AS dk ON dk.NoSJ=D.NoSJ and dk.RowStatus>-1 AND dk.ItemCode=x.ItemCode" +
                         ")";
                        
            return strSql;
        }
        private string QueryRekapan(bool Rekap)
        {
            string result = this.QueryRekapan();
            result += ",Pemantauan3 AS( " +
                         "   Select ItemName,BANum,Tanggal,DepoName,SupplierName, RMSNo, NetBPAS,NetDepo,NetSelisih,Persen ,OrderBy, " +
                         "   CONVERT(CHAR,CreatedTime,103)Konfirmasi, " +
                         "   CASE WHEN ((DATEDIFF(DAY,TglKirim,CreatedTime))>5 OR TglKirim IS NULL) THEN 0 ELSE 1 END OnTime,CONVERT(CHAR,TglKirim,103)TglKirim " +
                         "   FROm Pemantauan2 " +
                         "   UNION ALL  " +
                         "   Select ItemName,'' BANum,'' Tanggal, DepoName +' SUB TOTAL' DepoName, ''SupplierName, '' RMSNo, SUM(NetBPAS),SUM(NetDepo), " +
                         "   SUM(NetSelisih),(AVG(Persen))Persen ,2 OrderBy,RTRIM(CAST(Count(CreatedTime) AS CHAR(10)))Konfirmasi,0 OnTime,''TglKirim " +
                         "   FROm Pemantauan2  " +
                         "   GROUP BY DepoName,ItemName " +
                         "   UNION ALL " +
                         "   Select UPPER(ItemName)+' SUB TOTAL'  ItemName,'' BANum,'' Tanggal, '' DepoName, ''SupplierName, '' RMSNo, SUM(NetBPAS),SUM(NetDepo), " +
                         "   SUM(NetSelisih),(AVG(Persen))Persen ,3 OrderBy,CAST(Count(CreatedTime) AS CHAR)Konfirmasi,0 OnTime,''TglKirim  " +
                         " FROM Pemantauan2 GROUP BY ItemName) ";
            result += (Rekap == true) ? ",Pemantauan4 AS( " +
                   "  SELECT RTRIM(REPLACE(p3.DepoName,'SUB TOTAL',''))Deponame , " +
                   "  NetDepo BBDepo, NetBPAS BBPabrik,NetSelisih Selisih,Persen,1 Urutan,dk.GroupDepo " +
                   "  FROM Pemantauan3 p3 " +
                   "  LEFT JOIN DepoKertas dk on dk.DepoName=RTRIM(REPLACE(p3.DepoName,'SUB TOTAL','')) " +
                   "  where orderby in(2) and p3.DepoName like 'Depo%' " +
                   "  ) " +
                   "  SELECT DepoName,BBDepo,BBPabrik,Selisih,((Selisih/BBDepo)*100)Persen,Urutan,GroupDepo FROM( "+
                   "  SELECT Deponame,SUM(BBDepo)BBDepo,SUM(BBPabrik)BBPabrik,(SUM(BBPabrik)-SUM(BBDepo))Selisih,AVG(Persen)Persen,Urutan,GroupDepo "+
                   "  FROM Pemantauan4 GROUP BY Deponame,Urutan,GroupDepo) AS X " +
                   "  UNION " +
                   "  SELECT 'Total '+GroupDepo DepoName,Sum(BBDepo),Sum(BBPabrik),Sum(Selisih),((SUM(Selisih)/SUM(BBDepo)*100))persen,2 urutan,GroupDepo FROM Pemantauan4 " +
                   "  Group By GroupDepo " +
                   "  UNION " +
                   "  SELECT 'Total' DepoName,Sum(BBDepo),Sum(BBPabrik),Sum(Selisih),((SUM(Selisih)/SUM(BBDepo)*100))persen,3 urutan,'z' GroupDepo FROM Pemantauan4 " +
                   //"  WHERE DepoName IN('Depo Surabaya') " +
                   "  ORDER BY GroupDepo,DepoName,Urutan" :

                   " ,Pemantauan4 AS( " +
                   "  Select p3.DepoName,CAST(Count(p3.DepoName)AS decimal(18,2))Jumlah,dp.GroupDepo,COUNT(Konfirmasi)Konfirmasi, "+
                   "  CAST(SUM(OnTime) AS decimal(18,2))OnTime From Pemantauan3 p3 " +
                   "  LEFT JOIN DepoKertas dp on dp.DepoName=p3.DepoName " +
                   "  where persen<-5 and orderby=1  " +
                   "  GROUP BY p3.DepoName,dp.groupDepo,p3.Konfirmasi " +
                   "  ) " +
                   "  SELECT  DepoName,SUM(Jumlah)Jumlah,GroupDepo, 1 Urutan,SUM(Konfirmasi)Konfirmasi,SUM(OnTime)OnTime, "+
                   " (SUM(OnTime)/SUM(Jumlah)*100)Persen From Pemantauan4 Group By DepoName,GroupDepo" +
                   "  UNION " +
                   "  SELECT 'Total '+GroupDepo DepoName,SUM(Jumlah)Jumlah,GroupDepo, 2 Urutan,SUM(Konfirmasi),SUM(OnTime), " +
                   " (SUM(Ontime)/SUM(Jumlah)*100)Persen  " +
                   "  From Pemantauan4 Group By GroupDepo " +
                   "  UNION " +
                   "  SELECT 'Total Depo' DepoName,SUM(Jumlah)Jumlah,'z' GroupDepo, 3 Urutan,SUM(Konfirmasi),SUM(OnTime), " +
                   " (SUM(Ontime)/SUM(Jumlah)*100)Persen  " +
                   "  From Pemantauan4  " +
                   "  ORDER BY GroupDepo,Urutan";
            return result;
        }
        private string QueryDetail5persen()
        {
            string strSQL = this.QueryRekapan();
            strSQL += ",Pemantauan3 AS( " +
                      "  Select ItemName,BANum,Tanggal,DepoName,SupplierName, RMSNo, NetBPAS,NetDepo,NetSelisih,Persen ,OrderBy, " +
                      "  CONVERT(CHAR,CreatedTime,103)Konfirmasi,NoSJ, " +
                      "  CASE WHEN ((DATEDIFF(DAY,TglKirim,CreatedTime))>5 OR TglKirim IS NULL) THEN 0 ELSE 1 END OnTime,CONVERT(CHAR,TglKirim,103)TglKirim " +
                      "  FROm Pemantauan2 " +
                      "  ),Pemantauan4 AS( " +

                      "  SELECT  p.DepoName, d.GroupDepo,p.nosj,p.Persen,p.TglKirim,p.Konfirmasi,OnTime, " +
                      "  CASE WHEN OnTime>0 THEN 1 ELSE 0 END Sesuai, CASE WHEN OnTime=0 THEN 1 ELSE 0 END TdkSesuai,OrderBy " +
                      "  FROM Pemantauan3 p " +
                      "  LEFT JOIN DepoKertas d on D.DepoName=p.DepoName " +
                      "  WHERE Persen <-5 /*and p.nosj is not null*/ " +
                      "  ) " +
                      "  SELECT * FROM Pemantauan4 " +
                      "  UNION " +
                      "  SELECT  DepoName +' Total' DepoName,GroupDepo,'' NOSJ,AVG(Persen)Persen,''TglKirim,''Konfirmasi,COUNT(OnTime)OnTime, " +
                      "  SUM(Sesuai)Sesuai,SUM(TdkSesuai)TdakSesuai,2 OrderBy " +
                      "  FROM Pemantauan4 Group By DepoName, GroupDepo	 " +
                      "  UNION " +
                      "  SELECT  ''DepoName,GroupDepo +' Total' GroupDepo,'' NOSJ,AVG(Persen)Persen,''TglKirim,''Konfirmasi,COUNT(OnTime)OnTime, " +
                      "  SUM(Sesuai)Sesuai,SUM(TdkSesuai)TdakSesuai,3 OrderBy " +
                      "  FROM Pemantauan4 Group By GroupDepo " +
                      "  UNION " +
                      "  SELECT '' DepoName,'Total Depo' GroupDepo,'' NOSJ,AVG(Persen)Persen,''TglKirim,''Konfirmasi,COUNT(OnTime)OnTime, " +
                      "  SUM(Sesuai)Sesuai,SUM(TdkSesuai)TdakSesuai,4 OrderBy " +
                      "  FROM Pemantauan4  " +
                      "  Order by GroupDepo,Deponame,orderBy";
            return strSQL;
        }
        private string QueryRekapDepo()
        {
            #region oldquery
            string strSQL = "WITH CheckerKertas AS ( "+
                            "SELECT TglKirim, Checker,NoSJ,GrossDepo,NettDepo,KADepo,StdKA,ItemCode "+
                            "FROM DeliveryKertas "+
                            "WHERE month(tglKirim)="+this.Bulans+" and year(tglKirim)="+this.Tahuns+" and PlantID IN( "+LokasiPabrik()+
                            ")AND NoSJ IN(Select NoSJ FROM DeliveryKertasKA WHERE PlantID IN(1,7) AND Approval>1 AND RowStatus>-1)), " +
                            "CheckQAPlant AS ( "+
	                        "    SELECT dka.TglCheck, dka.ID,dka.NOPOL,dka.NoSJ, dka.GrossPlant,dka.NettPlant,dka.AvgKA,dka.StdKA,dka.ItemCode "+
	                        "    FROM DeliveryKertasKA dka "+
	                        "    /*LEFT JOIN CheckerKertas ck on ck.NoSJ=dka.NoSJ*/ "+
                            "    WHERE dka.RowStatus>-1 AND dka.PlantID IN(" + LokasiPabrik() + ") and dka.NoSJ IN(SELECT NoSJ From CheckerKertas) " +
                            ") "+
                            ",CheckerKertas1 AS ( "+
                            "SELECT ISNULL(cp.TglCheck,ck.tglKirim)Tanggal, ck.Checker,ck.NoSJ,GrossDepo,GrossPlant,NettDepo, "+
                            "NettPlant,KADepo,AvgKA,ck.StdKA,ck.ItemCode "+
                            " FROM CheckerKertas ck "+
                            "LEFT JOIN  CheckQAPlant cp ON cp.NoSJ=ck.NoSJ AND cp.ItemCode=ck.ItemCode "+
                            ") "+
                            ",CheckerKertas2 AS ( "+
                            "SELECT Checker,SUM(NettDepo)NetDepo,MAX(NettPlant)NettPlant "+
                            "FROM CheckerKertas1 "+
                            "GROUP By Checker,NoSJ,ItemCode "+
                            ") "+
                            ",CheckerKertas3 AS ( "+
                            "SELECT Checker ,SUM(NetDepo)NettDepo,SUM(NettPlant)NettPlant,(SUM(NettPlant)-SUM(NetDepo))Selisih "+
                            "FROM CheckerKertas2  "+
                            "Group By Checker "+
                            ") "+
                            "SELECT *,(Selisih/NettDepo)*100 Persen,1 Urutan FROM CheckerKertas3 "+
                            "UNION "+
                            "SELECT 'Total 'DepoName,SUM(NettDepo)NettDepo,SUM(NettPlant)NettPlant,SUM(Selisih)Selisih, "+
                            "(SUM(Selisih)/SUM(NettDepo)*100)Persen,2 Urutan "+
                            "FROM CheckerKertas3 " +
                            "order by Urutan,Checker";
            #endregion

            return strSQL;
        }
        private string QueryRekapDepo(bool Supplier)
        {
            Users user = (Users)System.Web.HttpContext.Current.Session["Users"];
            string strSQL = "WITH SupplierKertas AS ( " +
                            "Select TglKirim, SupplierID,s.SupplierName,NoSJ,GrossDepo,NettDepo,KADepo,StdKA,ItemCode  " +
                            "FROM DeliveryKertas " +
                            "LEFT JOIN SuppPurch s ON s.ID=DeliveryKertas.SupplierID " +
                            "where month(tglKirim)=" + this.Bulans + " and year(tglKirim)=" + this.Tahuns + " and PlantID=" + LokasiPabrik() + " AND DeliveryKertas.RowStatus>-1  " +
                            
                            "), " +
                            "CheckQAPlant AS ( " +
                            "    SELECT dka.TglCheck, dka.ID,dka.NOPOL,dka.NoSJ,dka.SupplierID,dka.SupplierName, dka.GrossPlant, " +
                            "    dka.NettPlant,dka.AvgKA,dka.StdKA,dka.ItemCode  " +
                            "    FROM DeliveryKertasKA dka " +
                            "    /*--LEFT JOIN CheckerKertas ck on ck.NoSJ=dka.NoSJ */" +
                            "    WHERE dka.RowStatus>-1 AND dka.PlantID=" + LokasiPabrik() + " and dka.NoSJ IN(SELECT NoSJ From SupplierKertas where " +
                            "    SupplierID=dka.SupplierID) " +
                            ") " +
                            ",CheckerKertas1 AS ( " +
                            "SELECT ISNULL(cp.TglCheck,ck.tglKirim)Tanggal,ck.SupplierID, ck.SupplierName,ck.NoSJ,GrossDepo, " +
                            "GrossPlant,NettDepo,NettPlant,KADepo,AvgKA,ck.StdKA,ck.ItemCode " +
                            " FROM SupplierKertas ck " +
                            "LEFT JOIN  CheckQAPlant cp ON cp.NoSJ=ck.NoSJ AND cp.ItemCode=ck.ItemCode AND cp.SupplierID=ck.SupplierID " +
                            ") " +
                            ",CheckerKertas2 AS ( " +
                            "SELECT SupplierID,SupplierName,SUM(NettDepo)NetDepo,(NettPlant)NettPlant " +
                            "FROM CheckerKertas1 " +
                            "GROUP By SupplierID,SupplierName,NoSJ,ItemCode,NettPlant " +
                            ") " +
                            ",CheckerKertas3 AS ( " +
                            "SELECT SupplierID,SupplierName,ISNULL(SUM(NetDepo),0)NettDepo,ISNULL(SUM(NettPlant),0)NettPlant, " +
                            "ISNULL((SUM(NettPlant)-SUM(NetDepo)),0)Selisih " +
                            "FROM CheckerKertas2  " +
                            "Group By SupplierName,SupplierID " +
                            ") " +
                            "SELECT *,(Selisih/NettDepo)*100 Persen,1 Urutan FROM CheckerKertas3 " +
                            "UNION " +
                            "SELECT 99999 SupplierID,'Total 'SupplierName,SUM(NettDepo)NettDepo,SUM(NettPlant)NettPlant,SUM(Selisih)Selisih, " +
                            "(SUM(Selisih)/SUM(NettDepo)*100)Persen,2 Urutan " +
                            "FROM CheckerKertas3 " +
                            "order by SupplierID,SupplierName,Urutan";
            return strSQL;
        }
        public ArrayList PemantauanKA(bool Rekap)
        {
            ArrayList arrData = new ArrayList();
            string strSQL = QueryRekapan(Rekap);
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateRekap(sdr, Rekap));
                }
            }
            return arrData;
        }

        /** Tambahan **/
        public QAKadarAir RetrieveDataKertasImport(string ItemCode)
        {
            QAKadarAir dkp = new QAKadarAir();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            //zw.Criteria = Criteria;
            zw.CustomQuery =
            //" select ID from KA_KertasImport where rowstatus>-1 and ItemID in (select ID inventory where itemcode='" + ItemCode + "' and aktif=1 ) ";
            " select sum(ID)ID from (select ID from KA_KertasImport where rowstatus>-1 and KodeBarang='" + ItemCode + "' union all " +
            " select 0 ) as x ";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObjectA(sdr));
                }
            }
            return dkp;
        }

        public QAKadarAir RetrieveDataSupplier(int ID)
        {
            QAKadarAir dkp = new QAKadarAir();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            //zw.Criteria = Criteria;
            zw.CustomQuery =
            " select sum(ID)ID from (select ID from KA_SupplierEx where rowstatus>-1 and supplierID=" + ID + " union all " +
            " select 0 ) as x ";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObjectA(sdr));
                }
            }
            return dkp;
        }

        public QAKadarAir RetrieveDataSupplierByName(string NamaS)
        {
            QAKadarAir dkp = new QAKadarAir();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            //zw.Criteria = Criteria;
            zw.CustomQuery =
            " select sum(ID)ID from (select ID from KA_SupplierEx where rowstatus>-1 and supplierID in (select ID from SuppPurch where SupplierName='" + NamaS + "' and RowStatus>-1) union all " +
            " select 0 ) as x ";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObjectA(sdr));
                }
            }
            return dkp;
        }

        private QAKadarAir GeneratObjectA(SqlDataReader sdr)
        {
            QAKadarAir ka = new QAKadarAir();
            try
            {
                ka.ID = Convert.ToInt32(sdr["ID"]);
                return ka;
            }
            catch (Exception ex)
            {
                ka.ID = Convert.ToInt32(ex.Message);
                return ka;
            }
        }

        /** End Tambahan **/

        public ArrayList DetailKonfirmasi()
        {
            ArrayList arrData = new ArrayList();
            string strSQL = this.QueryDetail5persen();
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateDetail(sdr));
                }
            }
            return arrData;
        }
        private DeliveryKertas GenerateDetail(SqlDataReader sdr)
        {
             DeliveryKertas ka = new DeliveryKertas();
             try
             {
                 ka.DepoName = sdr["DepoName"].ToString();
                 ka.NoSJ = (sdr["NoSJ"].ToString());
                 ka.CreatedBy = sdr["TglKirim"].ToString();
                 ka.LastModifiedBy = sdr["konfirmasi"].ToString();
                 ka.Persen = decimal.Parse(sdr["Persen"].ToString());
                 ka.SupplierName = (sdr["GroupDepo"].ToString());
                 ka.Sesuai = int.Parse(sdr["Sesuai"].ToString());
                 ka.TdkSesuai = int.Parse(sdr["TdkSesuai"].ToString());
                 ka.RowStatus = int.Parse(sdr["OrderBy"].ToString());
                 return ka;
             }
             catch (Exception ex)
             {
                 ka.DepoName = ex.Message;
                 return ka;
             }
        }
        private DeliveryKertas GenerateRekap(SqlDataReader sdr,bool Rekap)
        {
            DeliveryKertas ka = new DeliveryKertas();
            try
            {
                if (Rekap == true)
                {
                    ka.DepoName = sdr["DepoName"].ToString();
                    ka.NettDepo = decimal.Parse(sdr["BBDepo"].ToString());
                    ka.NettPlant = decimal.Parse(sdr["BBPabrik"].ToString());
                    ka.SelisihBB = decimal.Parse(sdr["Selisih"].ToString());
                    ka.Persen = decimal.Parse(sdr["Persen"].ToString());
                    ka.RowStatus = int.Parse(sdr["Urutan"].ToString());
                }
                else
                {
                    ka.DepoName = sdr["DepoName"].ToString();
                    ka.NettDepo = decimal.Parse(sdr["Jumlah"].ToString());
                    ka.NettPlant = decimal.Parse(sdr["OnTime"].ToString());
                    ka.SelisihBB = decimal.Parse(sdr["Persen"].ToString());
                    ka.RowStatus = int.Parse(sdr["Urutan"].ToString());
                }
                return ka;
            }
            catch(Exception ex)
            {
                ka.DepoName = ex.Message;
                return ka;
            }
        }
        public ArrayList SelisihByChecker()
        {
            ArrayList arrData = new ArrayList();
            string strSQL = this.QueryRekapDepo();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateRekapDepo(sdr,false));
                }
            }
            return arrData;
        }
        public ArrayList SelisihBySupplier()
        {
            ArrayList arrData = new ArrayList();
            string strSQL = this.QueryRekapDepo(true);
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateRekapDepo(sdr,true));
                }
            }
            return arrData;
        }
        private DeliveryKertas GenerateRekapDepo(SqlDataReader sdr, bool p)
        {
            DeliveryKertas d = new DeliveryKertas();
            if (p == false)
            {
                d.Checker = sdr["Checker"].ToString();
                d.NettDepo = decimal.Parse(sdr["NettDepo"].ToString());
                d.NettPlant = decimal.Parse(sdr["NettPlant"].ToString());
                d.SelisihBB = decimal.Parse(sdr["Selisih"].ToString());
                d.Persen = decimal.Parse(sdr["Persen"].ToString());
                d.RowStatus = int.Parse(sdr["Urutan"].ToString());
            }
            else
            {
                d.Checker = sdr["SupplierName"].ToString();
                d.NettDepo = decimal.Parse(sdr["NettDepo"].ToString());
                d.NettPlant = decimal.Parse(sdr["NettPlant"].ToString());
                d.SelisihBB = decimal.Parse(sdr["Selisih"].ToString());
                d.Persen = decimal.Parse(sdr["Persen"].ToString());
                d.RowStatus = int.Parse(sdr["Urutan"].ToString());
            }
            return d;
        }
        public QAKadarAir RetrieveDataSupplierByName0(string NamaS)
        {
            QAKadarAir dkp = new QAKadarAir();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            //zw.Criteria = Criteria;
            zw.CustomQuery =
            " select sum(ID)ID from (select ID from KA_SupplierEx where rowstatus=1 and supplierID in (select ID from SuppPurch where SupplierName='" + NamaS + "' and RowStatus>-1) union all " +
            " select 0 ) as x ";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObjectA(sdr));
                }
            }
            return dkp;
        }
        public QAKadarAir RetrieveDataSupplier0(int ID)
        {
            QAKadarAir dkp = new QAKadarAir();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            //zw.Criteria = Criteria;
            zw.CustomQuery =
            " select sum(ID)ID from (select ID from KA_SupplierEx where rowstatus=1 and supplierID=" + ID + " union all " +
            " select 0 ) as x ";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dkp = (GeneratObjectA(sdr));
                }
            }
            return dkp;
        }
       
    }
}
