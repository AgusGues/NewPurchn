using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using System.Web;
using BusinessFacade;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class TransaksiSPP3DtlFacade : AbstractTransactionFacade
    {
        //private PurchasingNf.ParamHead objSPP = new PurchasingNf.ParamHead();
        private PurchasingNf.ParamDtl objSPPDetail = new PurchasingNf.ParamDtl();
        private List<SqlParameter> sqlListParam;
        public TransaksiSPP3DtlFacade(object objDomain)
            : base(objDomain)
        {
            objSPPDetail = (PurchasingNf.ParamDtl)objDomain;
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {

                List<SqlParameter> sqlListParam = new List<SqlParameter>();
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
                //iko
                sqlListParam.Add(new SqlParameter("@AmGroupID", objSPPDetail.AmGroupID));
                sqlListParam.Add(new SqlParameter("@AmClassID", objSPPDetail.AmClassID));
                sqlListParam.Add(new SqlParameter("@AmSubClassID", objSPPDetail.AmSubClassID));
                sqlListParam.Add(new SqlParameter("@AmLokasiID", objSPPDetail.AmLokasiID));
                sqlListParam.Add(new SqlParameter("@MTCGroupSarmutID", objSPPDetail.MTCGroupSarmutID));
                sqlListParam.Add(new SqlParameter("@MaterialMTCGroupID", objSPPDetail.MaterialMTCGroupID));
                sqlListParam.Add(new SqlParameter("@UmurEkonomis", objSPPDetail.UmurEkonomis));
                //iko
                sqlListParam.Add(new SqlParameter("@NoPol", objSPPDetail.NoPol));

                /**
                 * ROPFacade ropfacade = new ROPFacade();                
                 * Update SPPQty di table ROP dengan JmlSPP yng blm di Buat PO dan JmlPO yang blm di Receipt + 
                 * jmlSPP yng baru diBuat
                 * Taroh di sini proses error, pindah ke btnUpdate_click > transaksiSPP3.aspx.cs
                decimal SPPnoPO = ropfacade.JumlahSPPBlmPO(objSPPDetail.ItemID, objSPPDetail.ItemTypeID);
                decimal POnoReceipt = ropfacade.JumlahPOblmReceipt(objSPPDetail.ItemID, objSPPDetail.ItemTypeID);
                ropfacade.UpdateROPBySPP(objSPPDetail.ItemID, objSPPDetail.SPPID, (objSPPDetail.Quantity+SPPnoPO + POnoReceipt));*/
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSPPDetail_New1");
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
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@QtyPO", objSPPDetail.QtyPO));
                sqlListParam.Add(new SqlParameter("@SPPID", objSPPDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSPPDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Status", objSPPDetail.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPPDetail.Keterangan));
                //iko
                //sqlListParam.Add(new SqlParameter("@AmGroupID", objSPPDetail.AmGroupID));
                //sqlListParam.Add(new SqlParameter("@AmClassID", objSPPDetail.AmClassID));
                //sqlListParam.Add(new SqlParameter("@AmSubClassID", objSPPDetail.AmSubClassID));
                //sqlListParam.Add(new SqlParameter("@AmLokasiID", objSPPDetail.AmLokasiID));
                //iko
                //sqlListParam.Add(new SqlParameter("@Keterangan1", objSPPDetail.Keterangan1));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSPPDetail");

                strError = transManager.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
    }
}
