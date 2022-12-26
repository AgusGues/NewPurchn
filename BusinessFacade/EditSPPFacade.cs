using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class EditSPPFacade : AbstractTransactionFacade
    {
        private EditSPP objSPP = new EditSPP();

        private ArrayList arrSPP;
        private List<SqlParameter> sqlListParam;
        protected DataAccess dataAccess = new DataAccess(Global.ConnectionString());

        public EditSPPFacade(object objDomain)
            : base(objDomain)
        {
            objSPP = (EditSPP)objDomain;
        }

        public EditSPPFacade()
        {

        }
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }


        public int UpdateEditSPP(object objDomain)
        {
            try
            {
                objSPP = (EditSPP)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoSPP", objSPP.NoSPP));
                sqlListParam.Add(new SqlParameter("@Minta", objSPP.Minta));
                sqlListParam.Add(new SqlParameter("@PermintaanType", objSPP.PermintaanType));
                sqlListParam.Add(new SqlParameter("@SatuanID", objSPP.SatuanID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPP.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPP.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objSPP.Jumlah));
                sqlListParam.Add(new SqlParameter("@JumlahSisa", objSPP.JumlahSisa));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPP.Keterangan));
                sqlListParam.Add(new SqlParameter("@Sudah", objSPP.Sudah));
                sqlListParam.Add(new SqlParameter("@FCetak", objSPP.FCetak));
                sqlListParam.Add(new SqlParameter("@UserID", objSPP.UserID));
                sqlListParam.Add(new SqlParameter("@HeadID", objSPP.HeadID));
                sqlListParam.Add(new SqlParameter("@Pending", objSPP.Pending));
                sqlListParam.Add(new SqlParameter("@Inden", objSPP.Inden));
                sqlListParam.Add(new SqlParameter("@AlasanBatal", objSPP.AlasanBatal));
                sqlListParam.Add(new SqlParameter("@AlasanCLS", objSPP.AlasanCLS));
                sqlListParam.Add(new SqlParameter("@Status", objSPP.Status));
                sqlListParam.Add(new SqlParameter("@Approval", objSPP.Approval));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSPP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CreatedTime", objSPP.CreatedTime));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSPP.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objSPP.LastModifiedTime));
                sqlListParam.Add(new SqlParameter("@DepoID", objSPP.DepoID));
                sqlListParam.Add(new SqlParameter("@ApproveDate1", objSPP.ApproveDate1));
                sqlListParam.Add(new SqlParameter("@ApproveDate2", objSPP.ApproveDate2));
                sqlListParam.Add(new SqlParameter("@ApproveDate3", objSPP.ApproveDate3));


                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSPPEdit");

                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
               
        }

        public int InsertEditSPP(object objDomain)
        {
            try
            {
                objSPP = (EditSPP)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoSPP", objSPP.NoSPP));
                sqlListParam.Add(new SqlParameter("@Minta", objSPP.Minta));
                sqlListParam.Add(new SqlParameter("@PermintaanType", objSPP.PermintaanType));
                sqlListParam.Add(new SqlParameter("@SatuanID", objSPP.SatuanID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPP.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPP.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objSPP.Jumlah));
                sqlListParam.Add(new SqlParameter("@JumlahSisa", objSPP.JumlahSisa));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPP.Keterangan));
                sqlListParam.Add(new SqlParameter("@Sudah", objSPP.Sudah));
                sqlListParam.Add(new SqlParameter("@FCetak", objSPP.FCetak));
                sqlListParam.Add(new SqlParameter("@UserID", objSPP.UserID));
                sqlListParam.Add(new SqlParameter("@HeadID", objSPP.HeadID));
                sqlListParam.Add(new SqlParameter("@Pending", objSPP.Pending));
                sqlListParam.Add(new SqlParameter("@Inden", objSPP.Inden));
                sqlListParam.Add(new SqlParameter("@AlasanBatal", objSPP.AlasanBatal));
                sqlListParam.Add(new SqlParameter("@AlasanCLS", objSPP.AlasanCLS));
                sqlListParam.Add(new SqlParameter("@Status", objSPP.Status));
                sqlListParam.Add(new SqlParameter("@Approval", objSPP.Approval));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSPP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CreatedTime", objSPP.CreatedTime));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSPP.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objSPP.LastModifiedTime));
                sqlListParam.Add(new SqlParameter("@DepoID", objSPP.DepoID));
                sqlListParam.Add(new SqlParameter("@ApproveDate1", objSPP.ApproveDate1));
                sqlListParam.Add(new SqlParameter("@ApproveDate2", objSPP.ApproveDate2));
                sqlListParam.Add(new SqlParameter("@ApproveDate3", objSPP.ApproveDate3));
                sqlListParam.Add(new SqlParameter("@JenisEdit", objSPP.JenisEdit));
                sqlListParam.Add(new SqlParameter("@KeteranganEditSPP", objSPP.KeteranganEditSpp));
                sqlListParam.Add(new SqlParameter("@ApvPurch", objSPP.ApvPurch));
                sqlListParam.Add(new SqlParameter("@ApvAccounting", objSPP.ApvAccounting));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertEditSPP");

                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public ArrayList RetrieveByAllWithStatusEditSPP(int headID, string strField, string strValue)
        {
            Users arr = (Users)System.Web.HttpContext.Current.Session["Users"];
            string strGroupID = string.Empty;
            int apv = arr.Apv;
            strGroupID = (headID > 0 && apv < 2) ? " and A.HeadID = " + headID : "";
            #region depreciated line

            //if (groupID == 1 || groupID == 2)
            //{
            //    strGroupID = " and A.GroupID in (1,2)";
            //}
            //if (groupID == 3)
            //{
            //    strGroupID = " and A.GroupID in (3)";
            //}
            //if (groupID == 4)
            //{
            //    strGroupID = " and A.GroupID in (4)";
            //}
            //if (groupID == 5)
            //{
            //    strGroupID = " and A.GroupID in (5)";
            //}
            //if (groupID == 6 || groupID == 7 || groupID == 8 || groupID == 9)
            //{
            //    strGroupID = " and A.GroupID in (6,7,8,9)";
            //}

            #endregion
            string strSQL = "select B.ID,A.ID SPPID,A.NoSPP,A.Minta,B.Status,A.Approval,A.CreatedBy,B.ItemID,B.Keterangan,B.ItemTypeID,B.GroupID," +
                "case B.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemCode from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemCode from Biaya where Biaya.ID=B.ItemID) else '' end ItemCode," +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemName from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)+'-'+B.Keterangan else '' end ItemName, C.UOMCode " +

                "from SPP as A "+
                "left join SPPDetail as B on B.SPPID = A.ID " +
                "left join UOM as C on B.UOMID = C.ID " +

                "where A.ID=B.SPPID " +
                "and B.UOMID=C.ID "+


                "and B.ID not in(select SPPDetailID from EditSPPDetail where SPPID in(select ID from SPP where NoSPP='" +strValue+"')) "+
                //"and A.Status>-1 "+
                "and B.Status>-1 " +
                "" + strGroupID +
                "and " + strField + " like '%" + strValue + "%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrSPP.Add(new EditSPP());

            return arrSPP;
        }

        public string GetNamaBarang(string sppDetailTipeBarang)
        {
            string hasil = string.Empty;

            string strsql =
                            "select " +
                           "case A.ItemTypeID " +
                            "when 1 then(select ItemName from Inventory where ID = A.ItemID) " +
                            "when 2 then(select ItemName from Asset where ID = A.ItemID) " +
                            "when 3 then(select ItemName from Biaya where ID = A.ItemID) + ' - ' + A.Keterangan else '' end ItemName " +
                            "from SPPDetail as A,UOM as C where A.UOMID = C.ID and A.Status > -1 and A.ID = " + sppDetailTipeBarang + "";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);

            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasil = sdr["ItemName"].ToString();

                }
            }

            return hasil;
        }

        public string CekInputanEditSPP(string nospp)
        {
            string hasil = string.Empty;

            string strsql = "select isnull(Count(IDEditSpp),0) jumlahHeader from EditSPP where NoSPP = '"+nospp+"' and Status >-1";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);

            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasil = sdr["jumlahHeader"].ToString();

                }
            }

            return hasil;
        }

        public string HeadID()
        {
            Users arr = (Users)System.Web.HttpContext.Current.Session["Users"];

            string strSQL = (arr.Apv < 1) ? " and (HeadID in(Select HeadID from ListUserHead where userID=" + arr.ID + ") or UserID=" + arr.ID + ") " : string.Empty;
            strSQL += (arr.Apv == 1 && arr.UserLevel < 1) ? " and HeadID=" + arr.ID : "";
            return strSQL;
        }

        public ArrayList RetrieveByAll(int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID == 1 || groupID == 2)
            {
                strGroupID = " and A.GroupID in (1,2)";
            }
            if (groupID == 3)
            {
                strGroupID = " and A.GroupID in (3)";
            }
            if (groupID == 4)
            {
                strGroupID = " and A.GroupID in (4)";
            }
            if (groupID == 5)
            {
                strGroupID = " and A.GroupID in (5)";
            }
            if (groupID == 6 || groupID == 7 || groupID == 8 || groupID == 9)
            {
                strGroupID = " and A.GroupID in (4,5,6,7,8,9)";
            }
            strGroupID = string.Empty;
            string limit = (strGroupID == string.Empty) ? "TOP 100 " : "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select " + limit + " A.ID,A.NoSPP,A.HeadID,A.Minta,B.Status,A.Approval,A.CreatedBy,B.ItemID," +
                "case when B.ItemTypeID!=3 then B.Keterangan else '' end Keterangan,B.ItemTypeID,B.GroupID," +
                "case B.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemCode from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemCode from Biaya where Biaya.ID=B.ItemID) else '' end ItemCode," +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemName from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)+ ' - '+ B.Keterangan else '' end ItemName, C.UOMCode " +
                "from SPP as A, SPPDetail as B, UOM as C where A.ID=B.SPPID and B.Status>-1 and B.UOMID=C.ID and A.Status>-1 " +
                strGroupID + HeadID() + " order by A.ID Desc";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObjectList(sqlDataReader));
                }
            }


            return arrSPP;
        }

        public EditSPP GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objSPP = new EditSPP();
            objSPP.ID = Convert.ToInt32(sqlDataReader["ID"]);

            objSPP.NoSPP = sqlDataReader["NoSPP"].ToString();
            objSPP.Minta = Convert.ToDateTime(sqlDataReader["Minta"]);
            objSPP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSPP.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objSPP.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSPP.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSPP.Keterangan = sqlDataReader["Keterangan"].ToString();
            objSPP.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objSPP.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objSPP.ItemCode = sqlDataReader["ItemCode"].ToString();
            objSPP.ItemName = sqlDataReader["ItemName"].ToString();
            objSPP.UomCode = sqlDataReader["UomCode"].ToString();

            return objSPP;
        }

        public EditSPP RetrieveByNoDetailID(string sppdetailid)
        {
            string strSQL = "select A.ID, B.ID SPPDETAILID,A.NoSPP,A.HeadID,A.Minta,A.PermintaanType,A.HeadID,A.SatuanID,A.GroupID,A.ItemTypeID," +
                            "A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,AlasanBatal,A.AlasanCLS," +
                            "A.Status,A.Approval,A.DepoID,A.CreatedTime as Tanggal,A.CreatedBy,A.ApproveDate1,A.ApproveDate2,A.ApproveDate3 " +
                            ",(Select UserName from users where ID=A.UserID)UserName,(Select UserName from users where ID=A.HeadID)HeadName " +
                            "from SPP as A, SPPDetail B where B.ID = '" + sppdetailid + "' and A.ID = B.SPPID  and A.Status > -1 and B.Status > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GeneratUserObject(sqlDataReader, GenerateObject(sqlDataReader));
                }
            }

            return new EditSPP();
        }
        
        public EditSPP GenerateObject(SqlDataReader sqlDataReader)
        {
            objSPP = new EditSPP();
            objSPP.ID = Convert.ToInt32(sqlDataReader["ID"]);

            objSPP.NoSPP = sqlDataReader["NoSPP"].ToString();
            objSPP.Minta = Convert.ToDateTime(sqlDataReader["Minta"]);
            objSPP.PermintaanType = Convert.ToInt32(sqlDataReader["PermintaanType"]);

            objSPP.SatuanID = Convert.ToInt32(sqlDataReader["SatuanID"]);
            objSPP.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objSPP.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objSPP.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objSPP.JumlahSisa = Convert.ToDecimal(sqlDataReader["JumlahSisa"]);
            objSPP.Keterangan = sqlDataReader["Keterangan"].ToString();
            objSPP.Sudah = Convert.ToInt32(sqlDataReader["Sudah"]);
            objSPP.FCetak = Convert.ToInt32(sqlDataReader["FCetak"]);
            objSPP.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            objSPP.Pending = Convert.ToInt32(sqlDataReader["Pending"]);
            objSPP.Inden = Convert.ToInt32(sqlDataReader["Inden"]);
            objSPP.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objSPP.AlasanCLS = sqlDataReader["AlasanCLS"].ToString();
            objSPP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSPP.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objSPP.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objSPP.Tanggal = DateTime.Parse(sqlDataReader["Tanggal"].ToString());
            objSPP.CreatedBy = sqlDataReader["CreatedBy"].ToString();

            objSPP.HeadID = Convert.ToInt32(sqlDataReader["HeadID"]);
            objSPP.ApproveDate1 = DateTime.Parse(sqlDataReader["ApproveDate1"].ToString());
            objSPP.ApproveDate2 = DateTime.Parse(sqlDataReader["ApproveDate2"].ToString());
            //objSPP.ApproveDate3 = DateTime.Parse(sqlDataReader["ApproveDate3"].ToString());
            //objSPP.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            //objSPP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            //objSPP.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objSPP.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objSPP;
        }
        public EditSPP GeneratUserObject(SqlDataReader sqlDataReader, EditSPP sppne)
        {
            objSPP = (EditSPP)sppne;
            objSPP.UserName = sqlDataReader["UserName"].ToString();
            objSPP.HeadName = sqlDataReader["HeadName"].ToString();
            objSPP.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            return objSPP;
        }

    }
}
