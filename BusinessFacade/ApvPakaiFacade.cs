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
    public class ApvPakaiFacade : AbstractTransactionFacade
    {
        private Pakai objPakai = new Pakai();
        private List<SqlParameter> sqlListParam;
        public ApvPakaiFacade(object objDomain)
            : base(objDomain)
        {
            objPakai = (Pakai)objDomain;
        }

        public override int Insert(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPakai.ID));
                sqlListParam.Add(new SqlParameter("@PakaiNo", objPakai.PakaiNo));
                sqlListParam.Add(new SqlParameter("@PakaiDate", objPakai.PakaiDate));
                sqlListParam.Add(new SqlParameter("@DeptID", objPakai.DeptID));
                sqlListParam.Add(new SqlParameter("@DepoID", objPakai.DepoID));
                sqlListParam.Add(new SqlParameter("@Status", objPakai.Status));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objPakai.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objPakai.CreatedBy));
                sqlListParam.Add(new SqlParameter("@PakaiTipe", objPakai.PakaiTipe));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objPakai.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@ApprovalDate", objPakai.ApprovalDate));
                sqlListParam.Add(new SqlParameter("@ApprovalBy", objPakai.ApprovalBy));
                sqlListParam.Add(new SqlParameter("@Ready", objPakai.Ready));
                //sqlListParam.Add(new SqlParameter("@Ready", objPakai.DepoID));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePakai");
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
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public static List<ApvPakai.ParamData> GetListData()
        {
            List<ApvPakai.ParamData> AllData = new List<ApvPakai.ParamData>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int UserID = users.ID;
                int Apv = users.Apv - 1;
                int DeptID = users.DeptID;
                string Where = "AND DeptID IN (SELECT ID FROM Dept WHERE ID=" + DeptID + " AND HeadID=" + UserID + ")";
                if (Apv == 1)
                {
                    Where = "AND DeptID IN (SELECT ID FROM Dept WHERE ID=" + DeptID + " AND MgrID=" + UserID + ")";
                    if (DeptID == 10) { Where = ""; Apv = 2; }
                }
                string query;
                try
                {
                    query = "SELECT ID, PakaiNo, PakaiDate, CreatedBy, Status FROM Pakai WHERE CompanyID="+users.UnitKerjaID+" and Status=" + Apv + " " + Where;
                    AllData = connection.Query<ApvPakai.ParamData>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
        public static List<ApvPakai.ParamDetail> GetListDetail(int ID)
        {
            List<ApvPakai.ParamDetail> AllData = new List<ApvPakai.ParamDetail>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int CompanyID = users.UnitKerjaID;
                string query;
                try
                {
                    query = "SELECT d.ID,d.Quantity,u.UOMDesc Satuan, d.Keterangan,case d.ItemTypeID when 1  then(select ItemCode from Inventory where ID = d.ItemID and RowStatus > -1 and CompanyID=" + CompanyID + ") when 2 then(select ItemCode from Asset where ID = d.ItemID and RowStatus > -1 and CompanyID=" + CompanyID + ") else (select ItemCode from Biaya where ID = d.ItemID and RowStatus > -1 and CompanyID=" + CompanyID + ") end ItemCode,case d.ItemTypeID when 1  then(select ItemName from Inventory where ID = d.ItemID and RowStatus > -1 and CompanyID=" + CompanyID + ") when 2 then(select ItemName from Asset where ID = d.ItemID and RowStatus > -1 and CompanyID=" + CompanyID + ") else (select ItemName from Biaya where ID = d.ItemID and RowStatus > -1 and CompanyID=" + CompanyID + ") end ItemName FROM PakaiDetail d, Inventory i, UOM u WHERE d.ItemID = i.ID AND d.UomID = u.ID AND d.PakaiID in (select ID from pakai where ID=" + ID + " and CompanyID=" + CompanyID + ")";
                    AllData = connection.Query<ApvPakai.ParamDetail>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
    }
}
