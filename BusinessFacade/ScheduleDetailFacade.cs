using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class ScheduleDetailFacade : AbstractTransactionFacade
    {
        private ScheduleDetail objScheduleDetail = new ScheduleDetail();
        private ArrayList arrScheduleDetail;
        private List<SqlParameter> sqlListParam;
        private string scheduleNo = string.Empty;

        public ScheduleDetailFacade(object objDomain)
            : base(objDomain)
        {
            objScheduleDetail = (ScheduleDetail)objDomain;
        }

        public ScheduleDetailFacade()
        {

        }

        public ScheduleDetailFacade(object objDomain,string strScheduleNo)
        {
            objScheduleDetail = (ScheduleDetail)objDomain;
            scheduleNo = strScheduleNo;
        }


        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ScheduleID", objScheduleDetail.ScheduleID));
                sqlListParam.Add(new SqlParameter("@DocumentID", objScheduleDetail.DocumentID));                
                sqlListParam.Add(new SqlParameter("@DocumentDetailID", objScheduleDetail.DocumentDetailID));
                sqlListParam.Add(new SqlParameter("@DocumentNo", objScheduleDetail.DocumentNo));                
                sqlListParam.Add(new SqlParameter("@ItemID", objScheduleDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Qty", objScheduleDetail.Qty));
                sqlListParam.Add(new SqlParameter("@TypeDoc", objScheduleDetail.TypeDoc));
                sqlListParam.Add(new SqlParameter("@Paket", objScheduleDetail.Paket));
                sqlListParam.Add(new SqlParameter("@PTID", objScheduleDetail.PTID));


                int intResult = transManager.DoTransaction(sqlListParam, "spInsertScheduleDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(TransactionManager transManager)
        {
            int intResult = Delete(transManager);
            if (strError == string.Empty)
                intResult = Insert(transManager);

            return intResult;
        }

        public override int Delete(TransactionManager transManager)
        {
            try
            {
                objScheduleDetail = (ScheduleDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objScheduleDetail.ScheduleID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteScheduleDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int CancelScheduleDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objScheduleDetail.ID));
                sqlListParam.Add(new SqlParameter("@TypeDoc", objScheduleDetail.TypeDoc));
                sqlListParam.Add(new SqlParameter("@DocumentDetailID", objScheduleDetail.DocumentDetailID));
                sqlListParam.Add(new SqlParameter("@Qty", objScheduleDetail.Qty));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objScheduleDetail.AlasanCancel));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelScheduleDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int DeleteCancelScheduleDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objScheduleDetail.ID));
                sqlListParam.Add(new SqlParameter("@TypeDoc", objScheduleDetail.TypeDoc));
                sqlListParam.Add(new SqlParameter("@DocumentDetailID", objScheduleDetail.DocumentDetailID));
                sqlListParam.Add(new SqlParameter("@Qty", objScheduleDetail.Qty));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteCancelScheduleDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int DeleteScheduleDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objScheduleDetail.ID));            
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteScheduleDetailByID");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,A.Qty,(C.Berat * A.Qty) as TotalKubikasi,A.Paket, 0 as DepoID,C.Berat,(select top 1 ApproveSCDate from OP where OP.id=A.DocumentID) as ApproveSCDate from ScheduleDetail as A,Items as C where A.ItemID = C.ID");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        
        public ArrayList RetrieveByTOId(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //add by iko : D.ToDepoID as DepoID, 7 Juli 2014
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Berat * A.Qty) as TotalKubikasi,A.Paket,D.ToDepoID as DepoID, D.LastModifiedTime as ApproveSCDate from ScheduleDetail as A,Items as C,TransferOrder as D,Depo as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.ToDepoID = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.TypeDoc = 1 and A.Status >= 0 and A.ScheduleID = " + Id);
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public int SumOPScheduleDetail(int itemID, int documentID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(ScheduleDetail.Qty),0) as Jumlah from ScheduleDetail,Schedule where ScheduleDetail.ItemID = " + itemID + " and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentID = " + documentID + " and ScheduleDetail.Status > -1 and Schedule.ID = ScheduleDetail.ScheduleID and Schedule.Status > -1");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Jumlah"]);
                }
            }

            return 0;
        }

        public int SumTOScheduleDetail(int itemID, int documentID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(ScheduleDetail.Qty),0) as Jumlah from ScheduleDetail,Schedule where ScheduleDetail.ItemID = " + itemID + " and ScheduleDetail.TypeDoc = 1 and ScheduleDetail.DocumentID = " + documentID + " and ScheduleDetail.Status > -1 and Schedule.ID = ScheduleDetail.ScheduleID and Schedule.Status > -1");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Jumlah"]);
                }
            }

            return 0;
        }

        public ArrayList RetrieveByOPId(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,E.CityName as KotaTujuan,F.NamaKabupaten as AreaTujuan, A.Qty,(C.Berat * A.Qty) as TotalKubikasi,A.Paket,B.DepoID from ScheduleDetail as A,Schedule as B,Items as C,OP as D,City as E,Kabupaten as F where A.ItemID = C.ID and A.DocumentID = D.ID and D.KabupatenID = F.ID and E.ID = F.CityID and A.ScheduleID = B.ID and A.TypeDoc = 0 and A.Status >= 0 and A.ScheduleID = " + Id);
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveDistinctById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(DocumentID),TypeDoc from ScheduleDetail where ScheduleID = " + Id + " and Status >= 0");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(DataSchedule(sqlDataReader));
                }
            }            

            return arrScheduleDetail;
        }
        public ArrayList RetrieveDistinctById2(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(DocumentID),TypeDoc from ScheduleDetail where ScheduleID = " + Id + " and TypeDoc = 0");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(DataSchedule(sqlDataReader));
                }
            }

            return arrScheduleDetail;
        }

        public int RetrieveJumOP(int opId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(id) as id from ScheduleDetail where DocumentID = " + opId + " and Status >= 0 and TypeDoc = 0");
            strError = dataAccess.Error;            

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["id"]);        
                }
            }

            return 0;
        }

        public int RetrieveJumQtyOP(int opId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(OPDetail.Quantity), 0) as id from OPDetail where OPID = " + opId);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["id"]);
                }
            }

            return 0;
        }

        public int RetrieveJumQtySchOP(int opId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(OPDetail.QtyScheduled), 0) as id from OPDetail where OPID = " + opId);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["id"]);
                }
            }

            return 0;
        }

        public int RetrieveJumTO(int toId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(id) as id from ScheduleDetail where DocumentID = " + toId + " and Status >= 0 and TypeDoc = 1");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["id"]);
                }
            }

            return 0;
        }

        public int RetrieveJumQtyTO(int toId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(TransferDetail.Qty), 0) as id from TransferDetail where TransferOrderID = " + toId);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["id"]);
                }
            }

            return 0;
        }

        public int RetrieveJumQtySchTO(int toId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(TransferDetail.QtyScheduled), 0) as id from TransferDetail where TransferOrderID = " + toId);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["id"]);
                }
            }

            return 0;
        }

  
        private int[] DataSchedule(SqlDataReader sqlDataReader)
        {
            int[] intSchedule = new int[2];
            intSchedule[0] = Convert.ToInt32(sqlDataReader["DocumentID"]);
            intSchedule[1] = Convert.ToInt32(sqlDataReader["TypeDoc"]);
            return intSchedule;
        }

        public ArrayList RetrieveByNoNew(string strSchedule)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Berat * A.Qty) as TotalKubikasi,A.Paket,B.DepoID from ScheduleDetail as A,Schedule as B,Items as C,OP as D,Toko as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.CustomerId = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.Status >= 0 and B.ScheduleNo = '" + strSchedule + "'");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveByNo(string strSchedule)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Berat * A.Qty) as TotalKubikasi,A.Paket,B.DepoID from ScheduleDetail as A,Schedule as B,Items as C,OP as D,Toko as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.CustomerId = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.TypeDoc = 0 and A.Status >= 0 and B.ScheduleNo = '" + strSchedule + "'");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveByNo2(string strSchedule)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Berat * A.Qty) as TotalKubikasi,A.Paket,B.DepoID from ScheduleDetail as A,Schedule as B,Items as C,OP as D,Toko as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.CustomerId = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.TypeDoc = 1 and A.Status >= 0 and B.ScheduleNo = '" + strSchedule + "'");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveByOPNo(string strSchedule,string strOPNo,int typeCust)
        {
            string strCommmand = string.Empty;
            if (typeCust == 1)
                strCommmand = "select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Berat * A.Qty) as TotalKubikasi,A.Paket,B.DepoID from ScheduleDetail as A,Schedule as B,Items as C,OP as D,Toko as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.CustomerId = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.TypeDoc = 0 and A.Status > -1 and B.ScheduleNo = '" + strSchedule + "' and A.DocumentNo = '" + strOPNo + "'";
            else
                strCommmand = "select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Berat * A.Qty) as TotalKubikasi,A.Paket,B.DepoID from ScheduleDetail as A,Schedule as B,Items as C,OP as D,Customer as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.CustomerId = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.TypeDoc = 0 and A.Status > -1 and B.ScheduleNo = '" + strSchedule + "' and A.DocumentNo = '" + strOPNo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strCommmand);
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveByTONo(string strSchedule, string strTONo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Berat * A.Qty) as TotalKubikasi,A.Paket,B.DepoID,D.LastModifiedTime as ApproveSCDate from ScheduleDetail as A,Schedule as B,Items as C,TransferOrder as D,Depo as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.ToDepoID = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.TypeDoc = 1 and B.ScheduleNo = '" + strSchedule + "' and A.Status > -1 and A.DocumentNo = '" + strTONo + "'");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public int RetrieveByOpDetailId(int documentDetailId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(qty,0) from scheduleDetail where TypeDoc = 0 and DocumentDetailId = " + documentDetailId + " and Status = 2");
            strError = dataAccess.Error;

            int qty = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    qty = qty + Convert.ToInt32(sqlDataReader["qty"]);
                }
            }

            return qty;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Berat * A.Qty) as TotalKubikasi,A.Paket,B.DepoID,D.LastModifiedTime as ApproveSCDate from ScheduleDetail as A,Schedule as B,Items as C,TransferOrder as D,Depo as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.ToDepoID = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOP()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status,(select DepoName from Depo where Depo.ID=Schedule.DepoID) as DepoName from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOP22()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Toko,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and Schedule.DepoID=Depo.ID and OP.CustomerType = 1 and OP.CustomerId = Toko.ID order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOP3()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,case when Schedule.Status=-1 then Schedule.Status else ScheduleDetail.Status end Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Toko,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and Schedule.DepoID=Depo.ID and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOP2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPCustomer()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Customer where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectListCustomer(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPCustomer2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Customer,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and Schedule.DepoID=Depo.ID and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectListCustomer(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and OP.DepoID = " + depoID + " order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByDepo2(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and OP.DepoID = " + depoID + " order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByDepo2a(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,case when Schedule.Status=-1 then Schedule.Status else ScheduleDetail.Status end Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Toko,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and Schedule.DepoID=Depo.ID and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and OP.DepoID = " + depoID + " order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByDepoCustomer(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Customer where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID and OP.DepoID = " + depoID + " order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectListCustomer(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByDepoCustomer2(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Customer,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and Schedule.DepoID=Depo.ID and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID and OP.DepoID = " + depoID + " order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectListCustomer(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }


        public ArrayList RetrieveListScheduleDetailOP(int distributorID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status,(select DepoName from Depo where Depo.ID=Schedule.DepoID) as DepoName from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Toko.DistributorID = " + distributorID + " order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOP2(int distributorID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and Toko.DistributorID = " + distributorID + " order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOP2a(int distributorID, string bln2Lalu)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate, "+
            "case when (select top 1 GroupID from OPDetail where OPID=OP.ID order by ID)=245 then "+
            "(select top 1 TokoCodeCL from Toko_Ciluk where TokoCode=Toko.TokoCode and RowStatus=0 order by id desc) "+
            "else Toko.TokoCode end TokoCode, "+
            //"Toko.TokoCode, "+
            "Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,case when Schedule.Status=-1 then Schedule.Status else ScheduleDetail.Status end Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Toko,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and Schedule.DepoID=Depo.ID and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and Toko.DistributorID = " + distributorID + " and CONVERT(varchar,Schedule.ScheduleDate,112) >='" + bln2Lalu + "' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOP1(int distributorID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and Toko.DistributorID = " + distributorID + " order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailTO()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,ScheduleDetail.DocumentNo,Items.Description as ItemName,' ' as OpNo2,' ' as TokoCode,' ' as TokoName,' '  as DepoName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 1 order by ScheduleDetail.DocumentNo Desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByCriteria(string strField,string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and " + strField + " like '%" + strValue + "%' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByCriteria3(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Toko,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and Schedule.DepoID=Depo.ID and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and " + strField + " like '%" + strValue + "%' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByCriteria2(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and Schedule.Status = 1 and OP.CustomerId = Toko.ID and " + strField + " like '%" + strValue + "%' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByCriteriaCustomer(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Customer where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID and " + strField + " like '%" + strValue + "%' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectListCustomer(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByCriteriaCustomer2(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Customer,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and Schedule.DepoID=Depo.ID and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID and " + strField + " like '%" + strValue + "%' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectListCustomer(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByCriteria(string strField, string strValue,int distributorID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Toko.DistributorID = " + distributorID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }


        public ArrayList RetrieveListScheduleDetailOPByCriteria2(string strField, string strValue, int distributorID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and Toko.DistributorID = " + distributorID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByCriteria2a(string strField, string strValue, int distributorID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate, "+
            "case when (select top 1 GroupID from OPDetail where OPID=OP.ID order by ID)=245 then "+
            "(select top 1 TokoCodeCL from Toko_Ciluk where TokoCode=Toko.TokoCode and RowStatus=0 order by id desc) "+
            "else Toko.TokoCode end TokoCode, " +
            "Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Toko,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and Schedule.DepoID=Depo.ID and OP.CustomerId = Toko.ID and Schedule.Status = 1 and Toko.DistributorID = " + distributorID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }
        
        public ArrayList RetrieveListScheduleDetailOPByCriteriaByDepo(string strField, string strValue, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and OP.DepoID = " + depoID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByCriteriaByDepo2(string strField, string strValue, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and OP.DepoID = " + depoID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailOPByCriteriaByDepo2a(string strField, string strValue, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Toko,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and Schedule.DepoID=Depo.ID and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and OP.DepoID = " + depoID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList    RetrieveListScheduleDetailOPByCriteriaByDepoCustomer(string strField, string strValue, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status,Depo.DepoName from Schedule,ScheduleDetail,Items,OP,Customer,Depo where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and Schedule.DepoID=Depo.ID and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID and OP.DepoID = " + depoID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectListCustomer(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ArrayList RetrieveListScheduleDetailTOByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,ScheduleDetail.DocumentNo,' ' as OpNo2,Items.Description as ItemName,' ' as TokoCode,' ' as TokoName,' '  as DepoName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 1 and " + strField + " = '" + strValue + "'");
            strError = dataAccess.Error;
            arrScheduleDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleDetail.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrScheduleDetail.Add(new ScheduleDetail());

            return arrScheduleDetail;
        }

        public ScheduleDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objScheduleDetail = new ScheduleDetail();
            objScheduleDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objScheduleDetail.ScheduleID = Convert.ToInt32(sqlDataReader["ScheduleID"]);
            objScheduleDetail.DocumentID = Convert.ToInt32(sqlDataReader["DocumentID"]);
            objScheduleDetail.DocumentDetailID = Convert.ToInt32(sqlDataReader["DocumentDetailID"]);
            objScheduleDetail.TypeDoc = Convert.ToInt32(sqlDataReader["TypeDoc"]);
            objScheduleDetail.DocumentNo = sqlDataReader["DocumentNo"].ToString();            
            objScheduleDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objScheduleDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objScheduleDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objScheduleDetail.KotaTujuan = sqlDataReader["KotaTujuan"].ToString();
            objScheduleDetail.AreaTujuan = sqlDataReader["AreaTujuan"].ToString();
            objScheduleDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);            
            objScheduleDetail.TotalKubikasi = decimal.Parse(sqlDataReader["TotalKubikasi"].ToString());
            objScheduleDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"].ToString());
            objScheduleDetail.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);

            return objScheduleDetail;

        }

        public ScheduleDetail GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objScheduleDetail = new ScheduleDetail();
            objScheduleDetail.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objScheduleDetail.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);
            objScheduleDetail.TokoCode = sqlDataReader["TokoCode"].ToString();
            objScheduleDetail.TokoName = sqlDataReader["TokoName"].ToString();
            objScheduleDetail.DocumentNo = sqlDataReader["DocumentNo"].ToString();            
            objScheduleDetail.ItemName  = sqlDataReader["ItemName"].ToString();            
            objScheduleDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objScheduleDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objScheduleDetail.OpNo2 = sqlDataReader["OpNo2"].ToString();
            objScheduleDetail.DepoName = sqlDataReader["DepoName"].ToString();
            return objScheduleDetail;

        }

        public ScheduleDetail GenerateObjectListCustomer(SqlDataReader sqlDataReader)
        {
            objScheduleDetail = new ScheduleDetail();
            objScheduleDetail.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objScheduleDetail.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);
            objScheduleDetail.CustomerCode = sqlDataReader["CustomerCode"].ToString();
            objScheduleDetail.CustomerName = sqlDataReader["CustomerName"].ToString();
            objScheduleDetail.DocumentNo = sqlDataReader["DocumentNo"].ToString();
            objScheduleDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objScheduleDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objScheduleDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objScheduleDetail.OpNo2 = sqlDataReader["OpNo2"].ToString();
            objScheduleDetail.DepoName = sqlDataReader["DepoName"].ToString();
            return objScheduleDetail;
        }
        public ScheduleDetail GenerateObjectGantung(SqlDataReader sqlDataReader)
        {
            objScheduleDetail = new ScheduleDetail();
            objScheduleDetail.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objScheduleDetail.OpNo2 = sqlDataReader["OpNo"].ToString();
            objScheduleDetail.ItemName = sqlDataReader["ItemName"].ToString();

            return objScheduleDetail;
        }

    }
}

