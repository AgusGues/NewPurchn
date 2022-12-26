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
    public class TransferOrderFacade : AbstractTransactionFacade
    {
        private TransferOrder objTransferOrder = new TransferOrder();
        private ArrayList arrTransferOrder;
        private List<SqlParameter> sqlListParam;

        public TransferOrderFacade(object objDomain)
            : base(objDomain)
        {
            objTransferOrder = (TransferOrder)objDomain;
        }

        public TransferOrderFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TransferOrderNo", objTransferOrder.TransferOrderNo));
                sqlListParam.Add(new SqlParameter("@TransferOrderDate", objTransferOrder.TransferOrderDate));
                sqlListParam.Add(new SqlParameter("@FromDepoID", objTransferOrder.FromDepoID));                
                sqlListParam.Add(new SqlParameter("@FromDepoName", objTransferOrder.FromDepoName));
                sqlListParam.Add(new SqlParameter("@FromDepoAddress", objTransferOrder.FromDepoAddress));
                sqlListParam.Add(new SqlParameter("@ToDepoID", objTransferOrder.ToDepoID));                
                sqlListParam.Add(new SqlParameter("@ToDepoName", objTransferOrder.ToDepoName));
                sqlListParam.Add(new SqlParameter("@ToDepoAddress", objTransferOrder.ToDepoAddress));
                sqlListParam.Add(new SqlParameter("@TotalKubikasi", objTransferOrder.TotalKubikasi));
                sqlListParam.Add(new SqlParameter("@Status", objTransferOrder.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan", objTransferOrder.Keterangan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objTransferOrder.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertTransferOrder");

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
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objTransferOrder.ID));
                sqlListParam.Add(new SqlParameter("@TransferOrderNo", objTransferOrder.TransferOrderNo));
                sqlListParam.Add(new SqlParameter("@TransferOrderDate", objTransferOrder.TransferOrderDate));
                sqlListParam.Add(new SqlParameter("@FromDepoID", objTransferOrder.FromDepoID));               
                sqlListParam.Add(new SqlParameter("@FromDepoName", objTransferOrder.FromDepoName));
                sqlListParam.Add(new SqlParameter("@FromDepoAddress", objTransferOrder.FromDepoAddress));
                sqlListParam.Add(new SqlParameter("@ToDepoID", objTransferOrder.ToDepoID));                
                sqlListParam.Add(new SqlParameter("@ToDepoName", objTransferOrder.ToDepoName));
                sqlListParam.Add(new SqlParameter("@ToDepoAddress", objTransferOrder.ToDepoAddress));
                sqlListParam.Add(new SqlParameter("@TotalKubikasi", objTransferOrder.TotalKubikasi));
                sqlListParam.Add(new SqlParameter("@Status", objTransferOrder.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan", objTransferOrder.Keterangan)); 
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objTransferOrder.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateTransferOrder");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objTransferOrder.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objTransferOrder.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteTransferOrder");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.TransferOrderNo,A.TransferOrderDate,A.FromDepoID,A.FromDepoName,A.FromDepoAddress,A.ToDepoID,A.ToDepoName,B.CityName as KotaTujuan,C.NamaKabupaten as AreaTujuan,A.ToDepoAddress,A.TotalKubikasi,A.Status,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,City as B,Kabupaten as C,Depo as D where A.ToDepoID = D.ID and D.CityID = B.ID and D.KabupatenID = C.ID and A.RowStatus = 0 order by A.ID desc");
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferOrder.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferOrder.Add(new TransferOrder());

            return arrTransferOrder;
        }

        public ArrayList RetrieveOpenStatus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderNo,A.TransferOrderDate,A.FromDepoID,A.FromDepoName,A.FromDepoAddress,A.ToDepoID,A.ToDepoName,B.CityName as KotaTujuan,C.NamaKabupaten as AreaTujuan,A.ToDepoAddress,A.TotalKubikasi,A.Status,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,City as B,Kabupaten as C,Depo as D where A.ToDepoID = D.ID and D.CityID = B.ID and D.KabupatenID = C.ID and A.RowStatus = 0 and A.status = 0 order by A.ID");
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferOrder.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferOrder.Add(new TransferOrder());

            return arrTransferOrder;
        }
        public TransferOrder RetrieveByNoWithDepoStatusApproval(string transferOrderNo, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderNo,A.TransferOrderDate,A.FromDepoID,A.FromDepoName,A.FromDepoAddress,A.ToDepoID,A.ToDepoName,B.CityName as KotaTujuan,C.NamaKabupaten as AreaTujuan,A.ToDepoAddress,A.TotalKubikasi,A.Status,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,City as B,Kabupaten as C,Depo as D where A.ToDepoID = D.ID and D.CityID = B.ID and D.KabupatenID = C.ID and A.RowStatus = 0 and A.Status in (2,3) and (A.FromDepoID = " + depoID + " or A.ToDepoID = " + depoID + ") and A.TransferOrderNo = '" + transferOrderNo + "'");
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new TransferOrder();
        }


        public ArrayList RetrieveOpenStatusByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderNo,A.TransferOrderDate,A.FromDepoID,A.FromDepoName,A.FromDepoAddress,A.ToDepoID,A.ToDepoName,B.CityName as KotaTujuan,C.NamaKabupaten as AreaTujuan,A.ToDepoAddress,A.TotalKubikasi,A.Status,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,City as B,Kabupaten as C,Depo as D where A.ToDepoID = D.ID and D.CityID = B.ID and D.KabupatenID = C.ID and A.RowStatus = 0 and A.status = 0 and (A.FromDepoID = " + depoID + " or A.ToDepoID = " + depoID + ") order by A.ID");
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferOrder.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferOrder.Add(new TransferOrder());

            return arrTransferOrder;
        }

        public ArrayList RetrieveAllByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.TransferOrderNo,A.TransferOrderDate,A.FromDepoID,A.FromDepoName,A.FromDepoAddress,A.ToDepoID,A.ToDepoName,B.CityName as KotaTujuan,C.NamaKabupaten as AreaTujuan,A.ToDepoAddress,A.TotalKubikasi,A.Status,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,City as B,Kabupaten as C,Depo as D where A.ToDepoID = D.ID and D.CityID = B.ID and D.KabupatenID = C.ID and A.RowStatus = 0 and (A.FromDepoID = " + depoID + " or A.ToDepoID = " + depoID + ") order by A.ID desc");
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferOrder.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferOrder.Add(new TransferOrder());

            return arrTransferOrder;
        }

        public ArrayList RetrieveApproveStatus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderNo,A.TransferOrderDate,A.FromDepoID,A.FromDepoName,A.FromDepoAddress,A.ToDepoID,A.ToDepoName,B.CityName as KotaTujuan,C.NamaKabupaten as AreaTujuan,A.ToDepoAddress,A.TotalKubikasi,A.Status,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,City as B,Kabupaten as C,Depo as D where A.ToDepoID = D.ID and D.CityID = B.ID and D.KabupatenID = C.ID and A.RowStatus = 0 and A.status in (1,2,3) order by KotaTujuan,AreaTujuan,A.ID");
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferOrder.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferOrder.Add(new TransferOrder());

            return arrTransferOrder;
        }


        public ArrayList RetrieveApproveStatusByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderNo,A.TransferOrderDate,A.FromDepoID,A.FromDepoName,A.FromDepoAddress,A.ToDepoID,A.ToDepoName,B.CityName as KotaTujuan,C.NamaKabupaten as AreaTujuan,A.ToDepoAddress,A.TotalKubikasi,A.Status,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,City as B,Kabupaten as C,Depo as D where A.ToDepoID = D.ID and D.CityID = B.ID and D.KabupatenID = C.ID and A.RowStatus = 0 and A.status in (1,2,3) and A.FromDepoID = " + depoID + " order by KotaTujuan,AreaTujuan,A.ID");
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferOrder.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferOrder.Add(new TransferOrder());

            return arrTransferOrder;
        }

        public TransferOrder RetrieveByNo(string transferOrderNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderNo,A.TransferOrderDate,A.FromDepoID,A.FromDepoName,A.FromDepoAddress,A.ToDepoID,A.ToDepoName,B.CityName as KotaTujuan,C.NamaKabupaten as AreaTujuan,A.ToDepoAddress,A.TotalKubikasi,A.Status,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,City as B,Kabupaten as C,Depo as D where A.ToDepoID = D.ID and D.CityID = B.ID and D.KabupatenID = C.ID and A.RowStatus = 0 and A.TransferOrderNo = '" + transferOrderNo + "'");
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new TransferOrder();
        }

        public TransferOrder RetrieveByNoWithDepo(string transferOrderNo,int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderNo,A.TransferOrderDate,A.FromDepoID,A.FromDepoName,A.FromDepoAddress,A.ToDepoID,A.ToDepoName,B.CityName as KotaTujuan,C.NamaKabupaten as AreaTujuan,A.ToDepoAddress,A.TotalKubikasi,A.Status,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,City as B,Kabupaten as C,Depo as D where A.ToDepoID = D.ID and D.CityID = B.ID and D.KabupatenID = C.ID and A.RowStatus = 0 and (A.FromDepoID = " + depoID + " or A.ToDepoID = " + depoID + ") and A.TransferOrderNo = '" + transferOrderNo + "'");
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new TransferOrder();
        }

        public TransferOrder RetrieveByID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderNo,A.TransferOrderDate,A.FromDepoID,A.FromDepoName,A.FromDepoAddress,A.ToDepoID,A.ToDepoName,B.CityName as KotaTujuan,C.NamaKabupaten as AreaTujuan,A.ToDepoAddress,A.TotalKubikasi,A.Status,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,City as B,Kabupaten as C,Depo as D where A.ToDepoID = D.ID and D.CityID = B.ID and D.KabupatenID = C.ID and A.RowStatus = 0 and A.ID = " + id);
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new TransferOrder();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferOrder as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrTransferOrder = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferOrder.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferOrder.Add(new TransferOrder());

            return arrTransferOrder;
        }

        public TransferOrder GenerateObject(SqlDataReader sqlDataReader)
        {
            objTransferOrder = new TransferOrder();
            objTransferOrder.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTransferOrder.TransferOrderNo = sqlDataReader["TransferOrderNo"].ToString();
            objTransferOrder.TransferOrderDate = Convert.ToDateTime(sqlDataReader["TransferOrderDate"]);
            objTransferOrder.FromDepoID = Convert.ToInt32(sqlDataReader["FromDepoID"]);            
            objTransferOrder.FromDepoName = sqlDataReader["FromDepoName"].ToString();
            objTransferOrder.FromDepoAddress = sqlDataReader["FromDepoAddress"].ToString();
            objTransferOrder.ToDepoID = Convert.ToInt32(sqlDataReader["ToDepoID"]);            
            objTransferOrder.ToDepoName = sqlDataReader["ToDepoName"].ToString();
            objTransferOrder.ToDepoAddress = sqlDataReader["ToDepoAddress"].ToString();
            objTransferOrder.KotaTujuan = sqlDataReader["KotaTujuan"].ToString();
            objTransferOrder.AreaTujuan = sqlDataReader["AreaTujuan"].ToString();
            objTransferOrder.TotalKubikasi = Convert.ToInt32(sqlDataReader["TotalKubikasi"]);
            objTransferOrder.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objTransferOrder.Keterangan = sqlDataReader["Keterangan"].ToString();            
            objTransferOrder.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objTransferOrder.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objTransferOrder.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objTransferOrder.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objTransferOrder.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objTransferOrder;

        }


    }
}

