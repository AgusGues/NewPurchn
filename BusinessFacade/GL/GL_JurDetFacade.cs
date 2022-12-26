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
    public class GL_JurDetFacade : AbstractTransactionFacade
    {
        private GL_JurDet objGL_JurDet = new GL_JurDet();
        private ArrayList arrGL_JurDet;
        private List<SqlParameter> sqlListParam;

        public GL_JurDetFacade(object objDomain)
            : base(objDomain)
        {
            objGL_JurDet = (GL_JurDet)objDomain;
        }

        public GL_JurDetFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ChartNo", objGL_JurDet.ChartNo));
                sqlListParam.Add(new SqlParameter("@Description", objGL_JurDet.Description));
                sqlListParam.Add(new SqlParameter("@DeptCode", objGL_JurDet.DeptCode));
                sqlListParam.Add(new SqlParameter("@CostCode", objGL_JurDet.CostCode));
                sqlListParam.Add(new SqlParameter("@CCYCode", objGL_JurDet.CCYCode));
                sqlListParam.Add(new SqlParameter("@CCYAmount", objGL_JurDet.CCYAmount));
                sqlListParam.Add(new SqlParameter("@CCYRate", objGL_JurDet.CCYRate));
                sqlListParam.Add(new SqlParameter("@IDRAmount", objGL_JurDet.IDRAmount));
                sqlListParam.Add(new SqlParameter("@CFCode", objGL_JurDet.CFCode));
                sqlListParam.Add(new SqlParameter("@JurHeadNum", objGL_JurDet.JurHeadNum));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDet.JurDetNum));
                sqlListParam.Add(new SqlParameter("@SeqNum", objGL_JurDet.SeqNum));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurDet.CreatedBy));

                //new add
                sqlListParam.Add(new SqlParameter("@TrxMappingCOAid", objGL_JurDet.TrxMappingCOAid));
                sqlListParam.Add(new SqlParameter("@TypeTRX", objGL_JurDet.TypeTRX));
                sqlListParam.Add(new SqlParameter("@TableName", objGL_JurDet.TableName));
                sqlListParam.Add(new SqlParameter("@IDTableName", objGL_JurDet.IDTableName));

                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_JurDet");
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
                sqlListParam.Add(new SqlParameter("@ChartNo", objGL_JurDet.ChartNo));
                sqlListParam.Add(new SqlParameter("@Description", objGL_JurDet.Description));
                sqlListParam.Add(new SqlParameter("@DeptCode", objGL_JurDet.DeptCode));
                sqlListParam.Add(new SqlParameter("@CostCode", objGL_JurDet.CostCode));
                sqlListParam.Add(new SqlParameter("@CCYCode", objGL_JurDet.CCYCode));
                sqlListParam.Add(new SqlParameter("@CCYAmount", objGL_JurDet.CCYAmount));
                sqlListParam.Add(new SqlParameter("@CCYRate", objGL_JurDet.CCYRate));
                sqlListParam.Add(new SqlParameter("@IDRAmount", objGL_JurDet.IDRAmount));
                sqlListParam.Add(new SqlParameter("@CFCode", objGL_JurDet.CFCode));
                sqlListParam.Add(new SqlParameter("@JurHeadNum", objGL_JurDet.JurHeadNum));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDet.JurDetNum));
                sqlListParam.Add(new SqlParameter("@SeqNum", objGL_JurDet.SeqNum));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurDet.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateGL_JurDet");
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
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDet.JurDetNum));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurDet.LastModifiedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteGL_JurDet");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertToTemp(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ChartNo", objGL_JurDet.ChartNo));
                sqlListParam.Add(new SqlParameter("@Description", objGL_JurDet.Description));
                sqlListParam.Add(new SqlParameter("@DeptCode", objGL_JurDet.DeptCode));
                sqlListParam.Add(new SqlParameter("@CostCode", objGL_JurDet.CostCode));
                sqlListParam.Add(new SqlParameter("@CCYCode", objGL_JurDet.CCYCode));
                sqlListParam.Add(new SqlParameter("@CCYAmount", objGL_JurDet.CCYAmount));
                sqlListParam.Add(new SqlParameter("@CCYRate", objGL_JurDet.CCYRate));
                sqlListParam.Add(new SqlParameter("@IDRAmount", objGL_JurDet.IDRAmount));
                sqlListParam.Add(new SqlParameter("@CFCode", objGL_JurDet.CFCode));
                sqlListParam.Add(new SqlParameter("@JurHeadNum", objGL_JurDet.JurHeadNum));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDet.JurDetNum));
                sqlListParam.Add(new SqlParameter("@SeqNum", objGL_JurDet.SeqNum));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurDet.CreatedBy));

                //new add
                sqlListParam.Add(new SqlParameter("@TrxMappingCOAid", objGL_JurDet.TrxMappingCOAid));
                sqlListParam.Add(new SqlParameter("@TypeTRX", objGL_JurDet.TypeTRX));
                sqlListParam.Add(new SqlParameter("@TableName", objGL_JurDet.TableName));
                sqlListParam.Add(new SqlParameter("@IDTableName", objGL_JurDet.IDTableName));
                sqlListParam.Add(new SqlParameter("@DebitTrans", objGL_JurDet.DebitTrans));
                sqlListParam.Add(new SqlParameter("@CreditTrans", objGL_JurDet.CreditTrans));

                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_JurDetTemp");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int DeleteToTemp(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDet.JurDetNum));

                int intResult = transManager.DoTransaction(sqlListParam, "GLDelete_JurDetTemp");
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
            string strSQL = "select GL_JurDet.*, GL_ChartofAccount.ChartName from GL_Jurdet inner join GL_CHart on GL_JurDet.rowstatus >-1 and GL_Jurdet.ChartNo = GL_ChartofAccount.ChartNo ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurDet.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_JurDet.Add(new GL_JurDet());
            return arrGL_JurDet;
        }

        public  ArrayList RetrieveByHeadNum(int JurHeadNum, string companyCode)
        {
            string strSQL = "select JurHeadNum,SeqNum,JurDetNum,jurtype,ChartNo,ChartName,Description,DeptCode,CostCode,CCYCode,CCYAmount, " +
                "CCYRate,IDRAmount,CFCode,isnull(InvNo,'-') as InvNo,InvDate,DueDate,isnull(OurRef,'-') as OurRef, " +
                "isnull(Remark,'-')as Remark,isnull(SupCode,'-')as SupCode, isnull(rtrim(SuppName),'-') as SuppName, " +
                "isnull(PPH,0) as PPH,isnull(freight,0) as freight,isnull(PPnBM,0)as PPnBM,isnull(CustCode,'-') as CustCode ,  " +
                "isnull(CustName,'-') as CustName,isnull(BGNo,'-') as BGNo,isnull(BankName,'-' ) as BankName,TableName,IDTableName from ( " +
                "select A.JurHeadNum,A.SeqNum,A.JurDetNum,A.ChartNo,O.charttype as jurtype, O.ChartName,A.Description,A.DeptCode, " +
                "A.CostCode,A.CCYCode,A.CCYAmount,A.CCYRate,A.IDRAmount,A.CFCode, " +
                "case when O.ChartType ='AP' then B.InvNo when O.ChartType ='AR' then C.InvNo  else ' ' end InvNo, " +
                "case when O.ChartType ='AP' then B.InvDate when O.ChartType ='AR' then C.InvDate  else null end InvDate, " +
                "case when O.ChartType ='AP' then B.DueDate when O.ChartType ='AR' then C.DueDate  else null end DueDate, " +
                "case when O.ChartType ='AP' then B.OurRef when O.ChartType ='AR' then C.OurRef else null end OurRef, " +
                "case when O.ChartType ='AP' then B.Remark when O.ChartType ='AR' then C.Remark else '-' end Remark, " +
                "B.SupCode,S.SuppName,B.PPH,B.freight, B.PPnBM,C.CustCode,GC.CustName,D.BGNo,D.BankName,A.TableName,A.IDTableName  " +
                "from GL_Jurdet A inner join GL_ChartofAccount O on A.ChartNo = O.ChartNo and O.RowStatus>-1 and O.CompanyCode='" + companyCode+"' left join GL_JurAP B on B.rowstatus>-1 and A.JurDetNum=B.JurDetNum left join GL_JurAR C " +
                "on C.rowstatus>-1 and A.JurDetNum=C.JurDetNum left join GL_JurBA D on D.rowstatus>-1 and A.JurDetNum=D.JurDetNum left join GL_Supplier S on B.SupCode=S.SupCode and S.CompanyCode=O.CompanyCode " +
                "left join GL_Customer GC on C.CustCode=GC.CustCode and GC.CompanyCode=O.CompanyCode " +
                "WHERE  A.rowstatus>-1 and A.JurHeadNum=" + JurHeadNum + " ) as BJdet  order by JurDetNum ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurDet.Add(GenerateObjectDet(sqlDataReader));
                }
            }
            else
                arrGL_JurDet.Add(new GL_JurDet());

            return arrGL_JurDet;
        }
        public ArrayList RetrieveGl0ByHeadNum(int JurHeadNum, string companyCode)
        {
            string strSQL = "select JurHeadNum,SeqNum,JurDetNum,jurtype,ChartNo,ChartName,Description,DeptCode,CostCode,CCYCode,CCYAmount, " +
                "CCYRate,IDRAmount,CFCode,isnull(InvNo,'-') as InvNo,InvDate,DueDate,isnull(OurRef,'-') as OurRef, " +
                "isnull(Remark,'-')as Remark,isnull(SupCode,'-')as SupCode, isnull(rtrim(SuppName),'-') as SuppName, " +
                "isnull(PPH,0) as PPH,isnull(freight,0) as freight,isnull(PPnBM,0)as PPnBM,isnull(CustCode,'-') as CustCode ,  " +
                "isnull(CustName,'-') as CustName,isnull(BGNo,'-') as BGNo,isnull(BankName,'-' ) as BankName,TableName,IDTableName from ( " +
                "select A.JurHeadNum,A.SeqNum,A.JurDetNum,A.ChartNo,O.charttype as jurtype, O.ChartName,A.Description,A.DeptCode, " +
                "A.CostCode,A.CCYCode,A.CCYAmount,A.CCYRate,A.IDRAmount,A.CFCode, " +
                "case when O.ChartType ='AP' then B.InvNo when O.ChartType ='AR' then C.InvNo  else ' ' end InvNo, " +
                "case when O.ChartType ='AP' then B.InvDate when O.ChartType ='AR' then C.InvDate  else null end InvDate, " +
                "case when O.ChartType ='AP' then B.DueDate when O.ChartType ='AR' then C.DueDate  else null end DueDate, " +
                "case when O.ChartType ='AP' then B.OurRef when O.ChartType ='AR' then C.OurRef else null end OurRef, " +
                "case when O.ChartType ='AP' then B.Remark when O.ChartType ='AR' then C.Remark else '-' end Remark, " +
                "B.SupCode,S.SuppName,B.PPH,B.freight, B.PPnBM,C.CustCode,GC.CustName,D.BGNo,D.BankName,A.TableName,A.IDTableName  " +
                "from GL_Jurdet0 A inner join GL_ChartofAccount O on A.ChartNo = O.ChartNo and O.CompanyCode='" + companyCode + "' left join GL_JurAP B on B.rowstatus>-1 and A.JurDetNum=B.JurDetNum left join GL_JurAR C " +
                "on C.rowstatus>-1 and A.JurDetNum=C.JurDetNum left join GL_JurBA D on D.rowstatus>-1 and A.JurDetNum=D.JurDetNum left join GL_Supplier S on B.SupCode=S.SupCode and S.CompanyCode=O.CompanyCode " +
                "left join GL_Customer GC on C.CustCode=GC.CustCode and GC.CompanyCode=O.CompanyCode " +
                "WHERE  A.rowstatus>-1 and A.JurHeadNum=" + JurHeadNum + " ) as BJdet  order by JurDetNum ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurDet.Add(GenerateObjectDet(sqlDataReader));
                }
            }
            else
                arrGL_JurDet.Add(new GL_JurDet());

            return arrGL_JurDet;
        }

        public GL_JurDet RetrieveByNo(string GL_JurDetNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from gl_Jurhead where rowstatus >-1 and JurNo = '" + GL_JurDetNo + "'");
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new GL_JurDet();
        }

        public GL_JurDet RetrieveByAP(int JurHeadNum)
        {
            string strSQL = "select top 1 GL_JurDet.*, GL_ChartofAccount.ChartName from GL_Jurdet inner join GL_ChartofAccount on GL_Jurdet.ChartNo = GL_ChartofAccount.ChartNo " +
                "WHERE  GL_JurDet.rowstatus >-1 and GL_JurDet.idramount<0 and GL_Jurdet.JurHeadNum =" + JurHeadNum + " order by GL_JurDet.idramount asc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_JurDet();
        }

        public GL_JurDet RetrieveByAR(int JurHeadNum)
        {
            string strSQL = "select top 1 GL_JurDet.*, GL_ChartofAccount.ChartName from GL_Jurdet inner join GL_ChartofAccount on GL_Jurdet.ChartNo = GL_ChartofAccount.ChartNo " +
                "WHERE GL_JurDet.rowstatus >-1 and GL_JurDet.idramount>0 and GL_Jurdet.JurHeadNum =" + JurHeadNum + " order by GL_JurDet.idramount desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_JurDet();
        }
        public GL_JurDet RetrieveByTableName(string namaTable, int id,string compCode)
        {
            string strSQL = "select top 1 GL_JurDet.*, GL_ChartofAccount.ChartName from GL_Jurdet inner join GL_JurHead on GL_JurHead.JurHeadNum=GL_JurDet.JurHeadNum and GL_JurDet.RowStatus>-1 inner join GL_ChartofAccount on GL_Jurdet.ChartNo = GL_ChartofAccount.ChartNo WHERE GL_JurDet.rowstatus > -1 and GL_Jurdet.TableName = '"+namaTable+"' and GL_JurDet.IDTableName = '"+id+"' and GL_JurHead.CompanyCode = '" + compCode+"'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_JurDet();
        }
        public GL_JurDet RetrieveByTableName(string namaTable, int id, string compCode, string strTypeTRX)
        {
            //string strSQL = "select top 1 GL_JurDet.*, GL_ChartofAccount.ChartName from GL_Jurdet inner join GL_JurHead on GL_JurHead.JurHeadNum=GL_JurDet.JurHeadNum and GL_JurDet.RowStatus>-1 inner join GL_ChartofAccount on GL_Jurdet.ChartNo = GL_ChartofAccount.ChartNo WHERE GL_JurDet.rowstatus > -1 and GL_Jurdet.TableName = '"+namaTable+"' and GL_JurDet.IDTableName = '"+id+"' and GL_JurHead.CompanyCode = '" + compCode+"'";
            string strSQL = "select top 1 GL_JurDet.*, GL_ChartofAccount.ChartName from GL_Jurdet inner join GL_JurHead on GL_JurHead.JurHeadNum=GL_JurDet.JurHeadNum and GL_JurDet.RowStatus>-1 inner join GL_ChartofAccount on GL_Jurdet.ChartNo = GL_ChartofAccount.ChartNo WHERE GL_JurDet.rowstatus > -1 and GL_Jurdet.TableName = '" + namaTable + "' and GL_JurDet.IDTableName = '" + id + "' and GL_JurHead.CompanyCode = '" + compCode + "' and typeTrx = '" + strTypeTRX + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_JurDet();
        }
        public ArrayList RetrieveByPeriod(string period)
        {
            string strSQL = "select JurHeadNum,SeqNum,JurDetNum,jurtype,ChartNo,ChartName,Description,DeptCode,CostCode,CCYCode,CCYAmount, " +
                "CCYRate,IDRAmount,CFCode,isnull(InvNo,'-') as InvNo,InvDate,DueDate,isnull(OurRef,'-') as OurRef, " +
                "isnull(Remark,'-')as Remark,isnull(SupCode,'-')as SupCode, isnull(rtrim(SuppName),'-') as SuppName, " +
                "isnull(PPH,0) as PPH,isnull(freight,0) as freight,isnull(PPnBM,0)as PPnBM,isnull(CustCode,'-') as CustCode ,  " +
                "isnull(CustName,'-') as CustName,isnull(BGNo,'-') as BGNo,isnull(BankName,'-' ) as BankName,TableName,IDTableName from ( " +
                "select A.JurHeadNum,A.SeqNum,A.JurDetNum,A.ChartNo,O.charttype as jurtype, O.ChartName,A.Description,A.DeptCode, " +
                "A.CostCode,A.CCYCode,A.CCYAmount,A.CCYRate,A.IDRAmount,A.CFCode, " +
                "case when O.ChartType ='AP' then B.InvNo when O.ChartType ='AR' then C.InvNo  else ' ' end InvNo, " +
                "case when O.ChartType ='AP' then B.InvDate when O.ChartType ='AR' then C.InvDate  else null end InvDate, " +
                "case when O.ChartType ='AP' then B.DueDate when O.ChartType ='AR' then C.DueDate  else null end DueDate, " +
                "case when O.ChartType ='AP' then B.OurRef when O.ChartType ='AR' then C.OurRef else null end OurRef, " +
                "case when O.ChartType ='AP' then B.Remark when O.ChartType ='AR' then C.Remark else '-' end Remark, " +
                "B.SupCode,S.SuppName,B.PPH,B.freight, B.PPnBM,C.CustCode,GC.CustName,D.BGNo,D.BankName,A.TableName,A.IDTableName  " +
                "from GL_Jurdet A inner join GL_ChartofAccount O on A.ChartNo = O.ChartNo left join GL_JurAP B on B.rowstatus>-1 and A.JurDetNum=B.JurDetNum left join GL_JurAR C " +
                "on C.rowstatus>-1 and A.JurDetNum=C.JurDetNum left join GL_JurBA D on D.rowstatus>-1 and A.JurDetNum=D.JurDetNum left join GL_Supplier S on B.SupCode=S.SupCode  " +
                "left join GL_Customer GC on C.CustCode=GC.CustCode  inner join GL_JurHead H on A.JurHeadNum=H.JurHeadNum  " +
                "WHERE  A.rowstatus>-1 and H.period='" + period + "' ) as BJdet  order by JurDetNum ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurDet.Add(GenerateObjectDet(sqlDataReader));
                }
            }
            else
                arrGL_JurDet.Add(new GL_JurDet());
            return arrGL_JurDet;
        }
        public ArrayList RetrieveJurnalByPeriod(string drPeriod, string sdPeriod)
        {
            string strSQL = "select b.JurDetNum, a.period,b.ChartNo,DeptCode,CostCode,0 as BegBal,b.IDRAmount,case when b.IDRAmount>0 then b.IDRAmount else 0 end DebitTrans,case when b.IDRAmount<0 then b.IDRAmount else 0 end CreditTrans,case when b.IDRAmount>0 then b.IDRAmount else 0 end CCYDebitTrans,case when b.IDRAmount<0 then b.IDRAmount else 0 end CCYCreditTrans, 0 as CCYBegBal, c.[Group], c.Postable, a.CreatedBy  " +
                "from GL_JurHead as a, GL_JurDet as b, GL_ChartOfAccount as c where a.JurHeadNum=b.JurHeadNum and a.RowStatus>-1 and b.RowStatus>-1 and b.ChartNo=c.ChartNo and a.CompanyCode=c.CompanyCode and CONVERT(varchar,JurDate,112) >='" + drPeriod + "' and CONVERT(varchar,JurDate,112) <='" + sdPeriod + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurDet.Add(GenerateObjectBal(sqlDataReader));
                }
            }
            else
                arrGL_JurDet.Add(new GL_JurDet());

            return arrGL_JurDet;
        }
        public ArrayList RetrieveTempByHeadNum(int JurHeadNum, string companyCode)
        {
            string strSQL = "select b.JurDetNum, a.period,b.ChartNo,DeptCode,CostCode,0 as BegBal,b.IDRAmount,case when b.IDRAmount>0 then b.IDRAmount else 0 end DebitTrans,case when b.IDRAmount<0 then b.IDRAmount else 0 end CreditTrans,case when b.IDRAmount>0 then b.IDRAmount else 0 end CCYDebitTrans,case when b.IDRAmount<0 then b.IDRAmount else 0 end CCYCreditTrans, 0 as CCYBegBal, c.[Group], c.Postable, a.CreatedBy,c.ChartName,b.Description,b.CCYCode " +
                "from GL_JurHeadTemp as a, GL_JurDetTemp as b, GL_ChartOfAccount as c where a.JurHeadNum = b.JurHeadNum and a.RowStatus > -1 and b.RowStatus > -1 and c.RowStatus>-1 and b.ChartNo = c.ChartNo and a.CompanyCode = c.CompanyCode and b.JurHeadNum = "+JurHeadNum+" and a.CompanyCode = '"+companyCode+"' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurDet.Add(GenerateObjectBal(sqlDataReader));
                }
            }
            else
                arrGL_JurDet.Add(new GL_JurDet());

            return arrGL_JurDet;
        }
        public ArrayList RetrieveoByHeadNum(int JurHeadNum, string companyCode)
        {
            string strSQL = "select b.JurDetNum, a.period,b.ChartNo,DeptCode,CostCode,0 as BegBal,b.IDRAmount,case when b.IDRAmount>0 then b.IDRAmount else 0 end DebitTrans,case when b.IDRAmount<0 then b.IDRAmount else 0 end CreditTrans,case when b.IDRAmount>0 then b.IDRAmount else 0 end CCYDebitTrans,case when b.IDRAmount<0 then b.IDRAmount else 0 end CCYCreditTrans, 0 as CCYBegBal, c.[Group], c.Postable, a.CreatedBy,c.ChartName,b.Description,b.CCYCode " +
                "from GL_JurHead0 as a, GL_JurDet0 as b, GL_ChartOfAccount as c where a.JurHeadNum = b.JurHeadNum and a.RowStatus > -1 and b.RowStatus > -1 and b.ChartNo = c.ChartNo and a.CompanyCode = c.CompanyCode and b.JurHeadNum = " + JurHeadNum + " and a.CompanyCode = '" + companyCode + "' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurDet.Add(GenerateObjectBal(sqlDataReader));
                }
            }
            else
                arrGL_JurDet.Add(new GL_JurDet());

            return arrGL_JurDet;
        }
        public GL_JurDet RetrieveByJurDet(int JurHeadNum)
        {
            string strSQL = "select b.JurDetNum, a.period,b.ChartNo,DeptCode,CostCode,0 as BegBal,b.IDRAmount,case when b.IDRAmount>0 then b.IDRAmount else 0 end DebitTrans,case when b.IDRAmount<0 then b.IDRAmount else 0 end CreditTrans,case when b.IDRAmount>0 then b.IDRAmount else 0 end CCYDebitTrans,case when b.IDRAmount<0 then b.IDRAmount else 0 end CCYCreditTrans, 0 as CCYBegBal, c.[Group], c.Postable, a.CreatedBy,c.ChartName,b.Description,b.CCYCode  "+
                "from GL_JurHeadTemp as a, GL_JurDetTemp as b, GL_ChartOfAccount as c where a.JurHeadNum = b.JurHeadNum and a.RowStatus > -1 and b.RowStatus > -1 and b.ChartNo = c.ChartNo and a.CompanyCode = c.CompanyCode and b.JurDetNum = " + JurHeadNum;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_JurDet = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectBal(sqlDataReader);
                }
            }

            return new GL_JurDet();
        }

        public GL_JurDet GenerateObjectBal(SqlDataReader sqlDataReader)
        {
                objGL_JurDet = new GL_JurDet();
                objGL_JurDet.ID = Convert.ToInt32(sqlDataReader["JurDetNum"]);
                objGL_JurDet.Period = sqlDataReader["Period"].ToString();
                objGL_JurDet.ChartNo = sqlDataReader["ChartNo"].ToString();
                objGL_JurDet.DeptCode = sqlDataReader["DeptCode"].ToString();
                objGL_JurDet.CostCode = sqlDataReader["CostCode"].ToString();
                objGL_JurDet.Group = sqlDataReader["Group"].ToString();
                objGL_JurDet.BegBal = Convert.ToDecimal(sqlDataReader["BegBal"]);
                objGL_JurDet.DebitTrans = Convert.ToDecimal(sqlDataReader["DebitTrans"]);
                objGL_JurDet.CreditTrans = Convert.ToDecimal(sqlDataReader["CreditTrans"]);
                objGL_JurDet.CCYBegBal = Convert.ToDecimal(sqlDataReader["CCYBegBal"]);
                objGL_JurDet.CCYDebitTrans = Convert.ToDecimal(sqlDataReader["CCYDebitTrans"]);
                objGL_JurDet.CCYCreditTrans = Convert.ToDecimal(sqlDataReader["CCYCreditTrans"]);
                objGL_JurDet.Postable = Convert.ToInt32(sqlDataReader["Postable"]);
                objGL_JurDet.CreatedBy = sqlDataReader["CreatedBy"].ToString();

                objGL_JurDet.IDRAmount = Convert.ToDecimal(sqlDataReader["IDRAmount"]);
                objGL_JurDet.ChartName = sqlDataReader["ChartName"].ToString();
                objGL_JurDet.CCYCode = sqlDataReader["CCYCode"].ToString();
                objGL_JurDet.Description = sqlDataReader["Description"].ToString();

            return objGL_JurDet;
        }
        public GL_JurDet GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_JurDet = new GL_JurDet();
            objGL_JurDet.ChartNo = sqlDataReader["ChartNo"].ToString();
            objGL_JurDet.ChartName = sqlDataReader["ChartName"].ToString();
            objGL_JurDet.Description = sqlDataReader["Description"].ToString();
            objGL_JurDet.DeptCode = sqlDataReader["DeptCode"].ToString();
            objGL_JurDet.CostCode = sqlDataReader["CostCode"].ToString();
            objGL_JurDet.CCYCode = sqlDataReader["CCYCode"].ToString();
            objGL_JurDet.CCYAmount = Convert.ToDecimal(sqlDataReader["CCYAmount"]);
            objGL_JurDet.CCYRate = Convert.ToDecimal(sqlDataReader["CCYRate"]);
            objGL_JurDet.IDRAmount = Convert.ToDecimal(sqlDataReader["IDRAmount"]);
            objGL_JurDet.CFCode = sqlDataReader["CFCode"].ToString();
            objGL_JurDet.JurHeadNum = Convert.ToInt32(sqlDataReader["JurHeadNum"]);
            objGL_JurDet.JurDetNum = Convert.ToInt32(sqlDataReader["JurDetNum"]);
            objGL_JurDet.SeqNum = Convert.ToInt32(sqlDataReader["SeqNum"]);
            return objGL_JurDet;
        }
        public GL_JurDet GenerateObjectDet(SqlDataReader sqlDataReader)
        {
            objGL_JurDet = new GL_JurDet();
            objGL_JurDet.ChartNo = sqlDataReader["ChartNo"].ToString();
            objGL_JurDet.ChartName = sqlDataReader["ChartName"].ToString();
            objGL_JurDet.Description = sqlDataReader["Description"].ToString();
            objGL_JurDet.DeptCode = sqlDataReader["DeptCode"].ToString().Trim();
            objGL_JurDet.CostCode = sqlDataReader["CostCode"].ToString().Trim();
            objGL_JurDet.CCYCode = sqlDataReader["CCYCode"].ToString().Trim();
            objGL_JurDet.CCYAmount = Convert.ToDecimal(sqlDataReader["CCYAmount"]);
            objGL_JurDet.CCYRate = Convert.ToDecimal(sqlDataReader["CCYRate"]);
            objGL_JurDet.IDRAmount = Convert.ToDecimal(sqlDataReader["IDRAmount"]);
            objGL_JurDet.CFCode = sqlDataReader["CFCode"].ToString().Trim();
            objGL_JurDet.JurHeadNum = Convert.ToInt32(sqlDataReader["JurHeadNum"]);
            objGL_JurDet.JurDetNum = Convert.ToInt32(sqlDataReader["JurDetNum"]);
            objGL_JurDet.SeqNum = Convert.ToInt32(sqlDataReader["SeqNum"]);
            objGL_JurDet.InvNo = sqlDataReader["InvNo"].ToString();
            try
            {
                objGL_JurDet.InvDate = Convert.ToDateTime(sqlDataReader["InvDate"]);
            }
            catch { }
            try
            {
                objGL_JurDet.DueDate = Convert.ToDateTime(sqlDataReader["DueDate"]);
            }
            catch { }
            objGL_JurDet.OurRef = sqlDataReader["OurRef"].ToString();
            objGL_JurDet.SupCode = sqlDataReader["SupCode"].ToString();
            objGL_JurDet.PPH = Convert.ToInt32(sqlDataReader["PPH"]);
            objGL_JurDet.Freight = Convert.ToInt32(sqlDataReader["Freight"]);
            objGL_JurDet.PPnBM = Convert.ToInt32(sqlDataReader["PPnBM"]);
            objGL_JurDet.SupName = sqlDataReader["SuppName"].ToString();
            objGL_JurDet.CustCode = sqlDataReader["CustCode"].ToString();
            objGL_JurDet.CustName = sqlDataReader["CustName"].ToString();
            objGL_JurDet.RemarkDet = sqlDataReader["Remark"].ToString();
            objGL_JurDet.BgNo = sqlDataReader["BgNo"].ToString();
            objGL_JurDet.BankName = sqlDataReader["BankName"].ToString();
            objGL_JurDet.JurType = sqlDataReader["JurType"].ToString();
            objGL_JurDet.TableName = sqlDataReader["TableName"].ToString();
            objGL_JurDet.IDTableName = Convert.ToInt32(sqlDataReader["IDTableName"]);

            return objGL_JurDet;
        }





    }
}
