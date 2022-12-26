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
    public class EditSPPDetailFacade : AbstractTransactionFacade
    {
        private EditSPPDetail objSPPDetail = new EditSPPDetail();
        private ArrayList arrSPPDetail;
        private List<SqlParameter> sqlListParam;
        //private string scheduleNo = string.Empty;
        protected DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        public EditSPPDetailFacade(object objDomain)
            : base(objDomain)
        {
            objSPPDetail = (EditSPPDetail)objDomain;
        }

        public EditSPPDetailFacade()
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

        public EditSPPDetail RetrieveByIdDetail(int sppdetailid, int sppid)
        {
            //DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.SPPID,A.GroupID,A.ItemTypeID,A.UOMID,A.ItemID,A.Quantity," +
                            "case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=A.ItemID) " +
                            "when 2 then (select ItemCode from Asset where ID=A.ItemID) " +
                            "when 3 then (select ItemCode from Biaya where ID=A.ItemID) else '' end ItemCode," +
                            "case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=A.ItemID) " +
                            "when 2 then (select ItemName from Asset where ID=A.ItemID) " +
                            "when 3 then (select ItemName from Biaya where ID=A.ItemID)+ ' - ' + A.Keterangan else '' end ItemName," +
                            "C.UOMCode as Satuan,A.QtyPO,A.Status,A.Keterangan,isnull(A.PendingPO,0)PendingPO,A.AlasanPending, " +
                            "case when (select spp.PermintaanType from spp where ID=A.SPPID)=2 Then " +
                            "DATEADD(day,isnull((select LeadTime from Inventory where ID=A.ItemID),7),(select SPP.ApproveDate3 from SPP where ID=A.SPPID)) " +
                            "else A.tglkirim END as tglkirim, AmGroupID,AmClassID,AmSubClassID,AmLokasiID,MTCSarmutGroupID,MaterialMTCGroupID,UmurEkonomis from SPPDetail as A,UOM as C where A.UOMID = C.ID and A.Status>-1 and A.ID=" + sppdetailid + " and A.SPPID =" + sppid;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return (GenerateObject(sqlDataReader));
                }
            }


            return new EditSPPDetail();
        }
        
        public int InsertEditSPPDetail(object objDomain)
        {
            try
            {
                objSPPDetail = (EditSPPDetail)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@IDEditSPP", objSPPDetail.IDEditSPP));
                sqlListParam.Add(new SqlParameter("@SPPID", objSPPDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPPDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSPPDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objSPPDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPPDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@UOMID", objSPPDetail.UOMID));
                sqlListParam.Add(new SqlParameter("@Status", objSPPDetail.Status));
                sqlListParam.Add(new SqlParameter("@QtyPO", objSPPDetail.QtyPO));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPPDetail.Keterangan));
                sqlListParam.Add(new SqlParameter("@tglkirim", objSPPDetail.TglKirim));
                sqlListParam.Add(new SqlParameter("@PendingPO", objSPPDetail.PendingPO));
                sqlListParam.Add(new SqlParameter("@AlasanPending", objSPPDetail.AlasanPending));
                sqlListParam.Add(new SqlParameter("@TypeBiaya", objSPPDetail.TypeBiaya));
                sqlListParam.Add(new SqlParameter("@Keterangan1", objSPPDetail.Keterangan1));
                sqlListParam.Add(new SqlParameter("@AmGroupID", objSPPDetail.AmGroupID));
                sqlListParam.Add(new SqlParameter("@AmClassID", objSPPDetail.AmClassID));
                sqlListParam.Add(new SqlParameter("@AmSubClassID", objSPPDetail.AmSubClassID));
                sqlListParam.Add(new SqlParameter("@AmLokasiID", objSPPDetail.AmLokasiID));
                sqlListParam.Add(new SqlParameter("@MTCSarmutGroupID", objSPPDetail.MTCGroupSarmutID));
                sqlListParam.Add(new SqlParameter("@MaterialMTCGroupID", objSPPDetail.MaterialMTCGroupID));
                sqlListParam.Add(new SqlParameter("@UmurEkonomis", objSPPDetail.UmurEkonomis));
                sqlListParam.Add(new SqlParameter("@NoPol", objSPPDetail.NoPol));
                sqlListParam.Add(new SqlParameter("@GroupSP", objSPPDetail.GroupSP));
                sqlListParam.Add(new SqlParameter("@SPPDetailID", objSPPDetail.SppDetailID));
                

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertEditSPPDetail");

                strError = dataAccess.Error;
                return intResult;
            }

            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }


        public int InsertSPPDetail(object objDomain)
        {
            try
            {
                objSPPDetail = (EditSPPDetail)objDomain;
                sqlListParam = new List<SqlParameter>();

                
                sqlListParam.Add(new SqlParameter("@SPPID", objSPPDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPPDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSPPDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPPDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@UOMID", objSPPDetail.UOMID));
                sqlListParam.Add(new SqlParameter("@Quantity", objSPPDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@QtyPO", objSPPDetail.QtyPO));
                sqlListParam.Add(new SqlParameter("@Status", objSPPDetail.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPPDetail.Keterangan));
                sqlListParam.Add(new SqlParameter("@tglkirim", objSPPDetail.TglKirim));
                sqlListParam.Add(new SqlParameter("@TypeBiaya", objSPPDetail.TypeBiaya));
                sqlListParam.Add(new SqlParameter("@Keterangan1", objSPPDetail.Keterangan1));
                sqlListParam.Add(new SqlParameter("@AmGroupID", objSPPDetail.AmGroupID));
                sqlListParam.Add(new SqlParameter("@AmClassID", objSPPDetail.AmClassID));
                sqlListParam.Add(new SqlParameter("@AmSubClassID", objSPPDetail.AmSubClassID));
                sqlListParam.Add(new SqlParameter("@AmLokasiID", objSPPDetail.AmLokasiID));
                sqlListParam.Add(new SqlParameter("@MTCGroupSarmutID", objSPPDetail.MTCGroupSarmutID));
                sqlListParam.Add(new SqlParameter("@MaterialMTCGroupID", objSPPDetail.MaterialMTCGroupID));
                sqlListParam.Add(new SqlParameter("@UmurEkonomis", objSPPDetail.UmurEkonomis));
                sqlListParam.Add(new SqlParameter("@NoPol", objSPPDetail.NoPol));
                sqlListParam.Add(new SqlParameter("@Kelompok", objSPPDetail.GroupSP));
                

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertSPPDetail_New1");

                strError = dataAccess.Error;
                return intResult;
            }

            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertEditSPPDetailCancel(object objDomain)
        {
            try
            {
                objSPPDetail = (EditSPPDetail)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@IDEditSPP", objSPPDetail.IDEditSPP));
                sqlListParam.Add(new SqlParameter("@SPPID", objSPPDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPPDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSPPDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objSPPDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPPDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@UOMID", objSPPDetail.UOMID));
                sqlListParam.Add(new SqlParameter("@Status", objSPPDetail.Status));
                sqlListParam.Add(new SqlParameter("@QtyPO", objSPPDetail.QtyPO));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPPDetail.Keterangan));
                sqlListParam.Add(new SqlParameter("@tglkirim", objSPPDetail.TglKirim));
                sqlListParam.Add(new SqlParameter("@PendingPO", objSPPDetail.PendingPO));
                sqlListParam.Add(new SqlParameter("@AlasanPending", objSPPDetail.AlasanPending));
                sqlListParam.Add(new SqlParameter("@TypeBiaya", objSPPDetail.TypeBiaya));
                sqlListParam.Add(new SqlParameter("@Keterangan1", objSPPDetail.Keterangan1));
                sqlListParam.Add(new SqlParameter("@AmGroupID", objSPPDetail.AmGroupID));
                sqlListParam.Add(new SqlParameter("@AmClassID", objSPPDetail.AmClassID));
                sqlListParam.Add(new SqlParameter("@AmSubClassID", objSPPDetail.AmSubClassID));
                sqlListParam.Add(new SqlParameter("@AmLokasiID", objSPPDetail.AmLokasiID));
                sqlListParam.Add(new SqlParameter("@MTCSarmutGroupID", objSPPDetail.MTCGroupSarmutID));
                sqlListParam.Add(new SqlParameter("@MaterialMTCGroupID", objSPPDetail.MaterialMTCGroupID));
                sqlListParam.Add(new SqlParameter("@UmurEkonomis", objSPPDetail.UmurEkonomis));
                sqlListParam.Add(new SqlParameter("@NoPol", objSPPDetail.NoPol));
                sqlListParam.Add(new SqlParameter("@GroupSP", objSPPDetail.GroupSP));
                sqlListParam.Add(new SqlParameter("@SppDetailID", objSPPDetail.SppDetailID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertEditSPPDetailCancel");

                strError = dataAccess.Error;
                return intResult;
            }

            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        
        public int UpdateSPPDetail(object objDomain)
        {

            try
            {
                objSPPDetail = (EditSPPDetail)objDomain;
                sqlListParam = new List<SqlParameter>();

                //sqlListParam.Add(new SqlParameter("@IDEditSPP", objSPPDetail.IDEditSPP));
                sqlListParam.Add(new SqlParameter("@SPPID", objSPPDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPPDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSPPDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objSPPDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPPDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@UOMID", objSPPDetail.UOMID));
                sqlListParam.Add(new SqlParameter("@Status", objSPPDetail.Status));
                sqlListParam.Add(new SqlParameter("@QtyPO", objSPPDetail.QtyPO));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPPDetail.Keterangan));
                sqlListParam.Add(new SqlParameter("@tglkirim", objSPPDetail.TglKirim));
                sqlListParam.Add(new SqlParameter("@PendingPO", objSPPDetail.PendingPO));
                sqlListParam.Add(new SqlParameter("@AlasanPending", objSPPDetail.AlasanPending));
                sqlListParam.Add(new SqlParameter("@TypeBiaya", objSPPDetail.TypeBiaya));
                sqlListParam.Add(new SqlParameter("@Keterangan1", objSPPDetail.Keterangan1));
                sqlListParam.Add(new SqlParameter("@AmGroupID", objSPPDetail.AmGroupID));
                sqlListParam.Add(new SqlParameter("@AmClassID", objSPPDetail.AmClassID));
                sqlListParam.Add(new SqlParameter("@AmSubClassID", objSPPDetail.AmSubClassID));
                sqlListParam.Add(new SqlParameter("@AmLokasiID", objSPPDetail.AmLokasiID));
                sqlListParam.Add(new SqlParameter("@MTCSarmutGroupID", objSPPDetail.MTCGroupSarmutID));
                sqlListParam.Add(new SqlParameter("@MaterialMTCGroupID", objSPPDetail.MaterialMTCGroupID));
                sqlListParam.Add(new SqlParameter("@UmurEkonomis", objSPPDetail.UmurEkonomis));
                sqlListParam.Add(new SqlParameter("@NoPol", objSPPDetail.NoPol));
                sqlListParam.Add(new SqlParameter("@GroupSP", objSPPDetail.GroupSP));
                sqlListParam.Add(new SqlParameter("@ID", objSPPDetail.SppDetailID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateEditSPPDetail");

                strError = dataAccess.Error;
                return intResult;
            }

            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

            
        }
        
        public EditSPPDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            try
            {
                objSPPDetail = new EditSPPDetail();
                objSPPDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objSPPDetail.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
                objSPPDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
                objSPPDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
                objSPPDetail.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
                objSPPDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
                objSPPDetail.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
                objSPPDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
                objSPPDetail.ItemName = sqlDataReader["ItemName"].ToString();
                objSPPDetail.Satuan = sqlDataReader["Satuan"].ToString();
                objSPPDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
                objSPPDetail.QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"]);
                objSPPDetail.Keterangan = sqlDataReader["Keterangan"].ToString();
                objSPPDetail.TglKirim = Convert.ToDateTime(sqlDataReader["tglkirim"]);
                objSPPDetail.PendingPO = Convert.ToInt32(sqlDataReader["PendingPO"]);
                objSPPDetail.AlasanPending = sqlDataReader["AlasanPending"].ToString();

                //iko
                objSPPDetail.AmGroupID = Convert.ToInt32(sqlDataReader["AmGroupID"]);
                objSPPDetail.AmClassID = Convert.ToInt32(sqlDataReader["AmClassID"]);
                objSPPDetail.AmSubClassID = Convert.ToInt32(sqlDataReader["AmSubClassID"]);
                objSPPDetail.AmLokasiID = Convert.ToInt32(sqlDataReader["AmLokasiID"]);
                objSPPDetail.MTCGroupSarmutID = Convert.ToInt32(sqlDataReader["MTCSarmutGroupID"]);
                objSPPDetail.MaterialMTCGroupID = Convert.ToInt32(sqlDataReader["MaterialMTCGroupID"]);
                objSPPDetail.UmurEkonomis = Convert.ToInt32(sqlDataReader["UmurEkonomis"]);
                //iko

            }
            catch
            {
            }
            return objSPPDetail;
        }

        private EditSPPDetail GenerateObject(SqlDataReader sdr, EditSPPDetail arrSppDetail)
        {
            objSPPDetail = (EditSPPDetail)arrSppDetail;
            objSPPDetail.TypeBiaya = sdr["TypeBiaya"].ToString();
            objSPPDetail.Keterangan1 = sdr["Keterangan1"].ToString();

            return objSPPDetail;
        }
    }
}
