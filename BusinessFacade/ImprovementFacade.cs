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
    public class ImprovementFacade : AbstractFacade
    {
        private Improvement objImprovement = new Improvement();
        private ArrayList arrImprovement;
        private List<SqlParameter> sqlListParam;
        //private const string sConn2 = "Initial Catalog=bpaskrwgKode;Data Source=sqlkrwg.grcboard.com;User ID=sa;Password=Passw0rd;MultipleActiveResultSets=true";

        public ImprovementFacade()
            : base()
        {
           
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objImprovement = (Improvement)objDomain;             
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemCode", objImprovement.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objImprovement.ItemName));
                sqlListParam.Add(new SqlParameter("@UOMID", objImprovement.UOMID));
                sqlListParam.Add(new SqlParameter("@DeptID", objImprovement.DeptID)); 
                sqlListParam.Add(new SqlParameter("@GroupID", objImprovement.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objImprovement.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Keterangan", objImprovement.Keterangan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objImprovement.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertImprovement");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertNew(object objDomain)
        {
            try
            {
                objImprovement = (Improvement)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UnitKerjaID", objImprovement.UnitKerjaID));//1
                sqlListParam.Add(new SqlParameter("@ItemCode", objImprovement.ItemCode));//2
                sqlListParam.Add(new SqlParameter("@ItemName", objImprovement.ItemName));//3
                sqlListParam.Add(new SqlParameter("@UOMID", objImprovement.UOMID));//4
                sqlListParam.Add(new SqlParameter("@DeptID", objImprovement.DeptID)); //5
                sqlListParam.Add(new SqlParameter("@GroupID", objImprovement.GroupID));//6
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objImprovement.ItemTypeID));//7
                sqlListParam.Add(new SqlParameter("@Keterangan", objImprovement.Keterangan));//8
                sqlListParam.Add(new SqlParameter("@CreatedBy", objImprovement.CreatedBy));//9
                sqlListParam.Add(new SqlParameter("@Nama", objImprovement.Nama));//10
                sqlListParam.Add(new SqlParameter("@Type", objImprovement.Type));//11
                sqlListParam.Add(new SqlParameter("@Ukuran", objImprovement.Ukuran));//12
                sqlListParam.Add(new SqlParameter("@Merk", objImprovement.Merk));//13
                sqlListParam.Add(new SqlParameter("@Jenis", objImprovement.Jenis));//14
                sqlListParam.Add(new SqlParameter("@PartNum", objImprovement.Partnum));//15
                sqlListParam.Add(new SqlParameter("@Aproval", objImprovement.Approval));//16
                sqlListParam.Add(new SqlParameter("@Stock", objImprovement.StockNonStock ));//17
                sqlListParam.Add(new SqlParameter("@LeadTime", objImprovement.LeadTime));//18
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertImprovement1");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        
         public override int Update(object objDomain)
        {
            try
            {
                objImprovement = (Improvement)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objImprovement.ID));
                sqlListParam.Add(new SqlParameter("@Rowstatus", objImprovement.RowStatus ));
                sqlListParam.Add(new SqlParameter("@Approval", objImprovement.Approval ));
                sqlListParam.Add(new SqlParameter("@ItemCode", objImprovement.ItemCode ));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objImprovement.LastModifiedBy ));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateImprovement");
                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

         public  int UpdateNew(object objDomain)
         {
             try
             {
                 objImprovement = (Improvement)objDomain;
                 sqlListParam = new List<SqlParameter>();
                 sqlListParam.Add(new SqlParameter("@ID", objImprovement.ID));
                 sqlListParam.Add(new SqlParameter("@ItemTypeID", objImprovement.ItemTypeID));
                 sqlListParam.Add(new SqlParameter("@LastModifiedBy", objImprovement.LastModifiedBy));
                 sqlListParam.Add(new SqlParameter("@aktif", objImprovement.Approval));
                 sqlListParam.Add(new SqlParameter("@rowstatus", objImprovement.RowStatus));
                 int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateImprovement1");
                 strError = dataAccess.Error;

                 return intResult;

             }
             catch (Exception ex)
             {
                 strError = ex.Message;
                 return -1;
             }
         }
        public override int Delete(object objDomain)
        {
            try
            {
                objImprovement = (Improvement)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objImprovement.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objImprovement.LastModifiedBy ));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteImprovement");
                strError = dataAccess.Error;
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
            string strSQL = "select  A.*,(select initialtoko from depo where ID=A.unitkerjaID) as UnitKerja,C.UOMDesc,C.UOMCode,case approval when 0 then 'Admin' " +
                "when 1 then 'Head' when 2 then /*'Manager' when 3 then*/ 'Approved' end ApprovalStatus from Improvement as A,UOM as C " +
                "where A.UOMID = C.ID AND A.RowStatus > -1 order by GroupID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrImprovement = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrImprovement.Add(GenerateObjectnew(sqlDataReader));
                }
            }
            else
                arrImprovement.Add(new Improvement());

            return arrImprovement;
        }

        public ArrayList Retrieve2(int typeID)
        {
            string strSQL = "select  top 100 A.*,(select initialtoko from depo where ID=A.unitkerjaID) as UnitKerja,C.UOMDesc,C.UOMCode,"+
                "case approval when 0 then 'Admin' " +
                "when 1 then 'Head' when 2 then /*'Manager' when 3 then */ 'Approved' when 9 then 'Not Approved'  end ApprovalStatus from Improvement  A " +
                "left join UOM as C on A.UOMID = C.ID  where A.itemTypeID=" +
                typeID + "  AND A.RowStatus > -1 order by ID desc"; 
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrImprovement = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrImprovement.Add(GenerateObjectnew(sqlDataReader));
                }
            }
            else
                arrImprovement.Add(new Improvement());

            return arrImprovement;
        }

        public ArrayList RetrieveUnApv(int UnitKerjaID)
        {
            string query = string.Empty;
            if (UnitKerjaID == 7)
            {
                query = " Improvement ";
            }
            else if (UnitKerjaID == 1 || UnitKerjaID == 13)
            {
                query = " [sqlkrwg.grcboard.com].bpaskrwg.dbo.Improvement ";
            }
           

            string strSQL = "select  top 100 A.*,(select initialtoko from depo where ID=A.unitkerjaID) as UnitKerja,C.UOMDesc,C.UOMCode," +
                "case approval when 0 then 'Admin' " +
                "when 1 then 'Head' when 2 then 'Approved' when 9 then 'Not Approved'  end ApprovalStatus from " + query + "  A " +
                "left join UOM as C on A.UOMID = C.ID  where A.RowStatus > -1 and Approval=9 and A.UnitKerjaID='" + UnitKerjaID + "' order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrImprovement = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrImprovement.Add(GenerateObjectnew(sqlDataReader));
                }
            }
            else
                arrImprovement.Add(new Improvement());

            return arrImprovement;
        }

        public ArrayList Retrieve2ByApproval(int typeID,int apv,int userid)
        {
            apv = apv - 1;
            string strSQL = string.Empty ;
            if (apv==0)
            strSQL = "select  A.*,(select initialtoko from depo where ID=A.unitkerjaID) as UnitKerja,C.UOMDesc,C.UOMCode,case approval when 0 then 'Admin' " +
                "when 1 then 'Head' when 2 then /*'Manager' when 3 then*/ 'Approved' when 9 then 'Not Approved'  end ApprovalStatus from Improvement as A,UOM as C where A.itemTypeID=" +
                typeID + " and approval=" + apv + " and A.UOMID = C.ID AND A.RowStatus > -1 and approval<>9  order by ID desc";
            else
                strSQL = "select  A.*,(select initialtoko from depo where ID=A.unitkerjaID) as UnitKerja,C.UOMDesc,C.UOMCode,case approval when 0 then 'Admin' " +
                "when 1 then 'Head' when 2 then /*'Manager' when 3 then*/ 'Approved' when 9 then 'Not Approved'  end ApprovalStatus from Improvement as A,UOM as C where A.itemTypeID=" +
                typeID + " and approval=" + apv + " and A.UOMID = C.ID AND A.RowStatus > -1 and approval<>9 and GroupID in (select isnull(ID,0) from GroupsPurchn where approveby= " +
                userid +") order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrImprovement = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrImprovement.Add(GenerateObjectnew(sqlDataReader));
                }
            }
            else
                arrImprovement.Add(new Improvement());

            return arrImprovement;
        }

        public Improvement RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select  A.*,(select initialtoko from depo where ID=A.unitkerjaID) as UnitKerja,C.UOMDesc,C.UOMCode,case approval when 0 then 'Admin' " +
                "when 1 then 'Head' when 2 then /*'Manager' when 3 then*/ 'Approved' end ApprovalStatus from Improvement as A,UOM as C " +
                "where A.UOMID = C.ID AND A.RowStatus > -1  and A.ID = " + Id);
            strError = dataAccess.Error;
            arrImprovement = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectnew(sqlDataReader);
                }
            }
            return new Improvement();
        }

       public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select  A.*,(select initialtoko from depo where ID=A.unitkerjaID) as UnitKerja,C.UOMDesc,C.UOMCode,case approval when 0 then 'Admin' " +
                 "when 1 then 'Head' when 2 then /*'Manager' when 3 then*/ 'Approved' end ApprovalStatus from Improvement as A,UOM as C " +
                 "where A.RowStatus >-1 and A.UOMID = C.ID  and " + strField + " like '%" + strValue + "%'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrImprovement = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrImprovement.Add(GenerateObjectnew(sqlDataReader));
                }
            }
            else
                arrImprovement.Add(new Improvement());

            return arrImprovement;
        }
       public int CountItemName(string strValue,string ItemTypeID)
       {
           /**
            * perubahan perhitungan nomor urut diambil dari table improvement
            */

           string tablename=string.Empty;
           if (ItemTypeID == "1")
           {
               tablename = "Inventory";
           }
           else
           {
               tablename = "Asset";
           }
           string strSQL = "SELECT count(Nama) as nourut from improvement where /*aktif>0 and*/ rowstatus >-1 and Nama like '%" + strValue.Trim() + "%'";
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           //tring InCode
           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   return Convert.ToInt32(sqlDataReader["nourut"]);
               }
           }
           return 0;
       }       
        public string  GetCodeItemName(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT InCode from BItemName where ItemName = '" + strValue + "'");
            strError = dataAccess.Error;
            //tring InCode
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["InCode"].ToString();
                }
            }
            return "0000";
        }

        public string GetCodeItemType(string strValue, string incode)
        {
            if (strValue==string.Empty)
                return "00";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT kode from BItemType where InType = '" + strValue +
                "' and incode='" + incode + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["kode"].ToString();
                }
            }
            return "00";
        }
        public string GetCodeItemMerk(string strValue, string incode)
        {
            if (strValue == string.Empty)
                return "00";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT kode from BItemMerk where InMerk = '" + strValue +
                "' and incode='" + incode + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["kode"].ToString();
                }
            }
            return "000";
        }

        public Improvement GenerateObjectnew(SqlDataReader sqlDataReader)
         {
             
                 objImprovement = new Improvement();
                 try
                 {
                 objImprovement.ID = Convert.ToInt32(sqlDataReader["ID"]);
                 objImprovement.ItemCode = sqlDataReader["ItemCode"].ToString();
                 objImprovement.ItemName = sqlDataReader["ItemName"].ToString();
                 objImprovement.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
                 objImprovement.UOMDesc = sqlDataReader["UOMDesc"].ToString();
                 objImprovement.UomCode = sqlDataReader["UOMCode"].ToString();
                 objImprovement.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
                 objImprovement.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
                 objImprovement.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
                 objImprovement.Keterangan = sqlDataReader["Keterangan"].ToString();
                 objImprovement.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                 objImprovement.CreatedBy = sqlDataReader["CreatedBy"].ToString();
                 objImprovement.Nama = sqlDataReader["Nama"].ToString();
                 objImprovement.Type = sqlDataReader["IType"].ToString();
                 objImprovement.Merk = sqlDataReader["Merk"].ToString();
                 objImprovement.Jenis = sqlDataReader["Jenis"].ToString();
                 objImprovement.Ukuran = sqlDataReader["Ukuran"].ToString();
                 objImprovement.Partnum = sqlDataReader["Partnum"].ToString();
                 objImprovement.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
                 objImprovement.ApprovalStatus = sqlDataReader["approvalstatus"].ToString();
                 objImprovement.StockNonStock = Convert.ToInt16(sqlDataReader["stock"]);
                 objImprovement.UnitKerjaID = Convert.ToInt16(sqlDataReader["UnitKerjaID"]);
                 objImprovement.UnitKerja = sqlDataReader["UnitKerja"].ToString();
                 objImprovement.LeadTime = int.Parse(sqlDataReader["leadtime"].ToString());
                 objImprovement.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"].ToString());
             }
             catch
             { }
                 return objImprovement;
         }

        public int getExistingCode(string Nama, string type, string ukuran, string merk, string GroupID)
        {
            /**
             * Added on 15-12-2013
             * Cek kodebarang apakah sudah pernah dibuat apa belum
             */

            int existingkode = 0;
            string strSql = "SELECT COUNT(ID) as ID FROM Improvement where Nama='" + 
                            Nama.Replace("'","") + "' and ITYPE='" + type.Replace("'","") + "' and Ukuran='" + 
                            ukuran.Replace("'","") + "' and Merk='" + merk.Replace("'","") + "' AND RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    existingkode= Convert.ToInt16(sqlDataReader["ID"].ToString());
                }
            }
            return existingkode;
        }
        /**
         * added on 06-09-2014
         * for Report Improvement Kode Baru
         */
        public ArrayList GetTahunImprovement()
        {
            string strSQL = "SELECT YEAR(CreatedTime) as Tahun From Improvement Group By YEAR(CreatedTime) order by YEAR(CreatedTime)";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrImprovement = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrImprovement.Add(sqlDataReader["Tahun"].ToString());
                }
            }
            else
            {
                arrImprovement.Add(new Improvement());
            }
            return arrImprovement;
        }
        public ArrayList RetrieveForReportDetail(int Tahun, int Bulan, int GroupID, int Depo, string Stock)
        {
            string Bln = (Bulan == 0) ? "" : " and MONTH(CreatedTime)=" + Bulan;
            string Grp = (GroupID == 0) ? "" : " and GroupID=" + GroupID;
            string Dpo = (Depo == 0) ? "" : " and UnitKerjaID=" + Depo;
            string OrdBy = (Grp == string.Empty) ? " Order By GroupID" : "Order By ID";
            string strSQL = "Select ROW_NUMBER() OVER("+OrdBy+") as Num,ItemCode,ItemName," +
                          "(SELECT UOM.UOMCode From UOM where UOM.ID=Improvement.UOMID) as UOMCode," +
                          "Case When Stock=0 Then 'Non Stock' ELSE 'Stock' END Stocked,improvement.stock," +
                          "Case UnitKerjaID When 7 Then 'Karawang' when 1 then 'Citeureup' END UnitKerja, " +
                          "GroupID, DeptID, Approval FROM Improvement " +
                          "where YEAR(CreatedTime)="+Tahun+ Bln+ Grp + Dpo +Stock+
                          "and RowStatus>-1 and Approval=2" +
                          OrdBy;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrImprovement = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrImprovement.Add(GenerateObjectRpt(sqlDataReader));
                }
            }
            else
            {
                arrImprovement.Add(new Improvement());
            }
            return arrImprovement;
        }
        public ArrayList RetrieveForRekap(int Tahun, int Depo)
        {
            string pln = (Depo == 0) ? "" : " and UnitKerjaID=" + Depo;
            string strSQL = "SELECT  Tahun,Bulan,sum(BB) as BB,sum(BP) as BP,sum(ATK)as ATK, "+
                            "sum(PYK) as PYK,sum(MEK)as MEK,sum(ELK)as ELK,sum(MKT) as MKT,sum(RKP) as RKP " +
	                        "    FROM( "+
	                        "    Select Tahun,Bulan, "+
	                        "    Case When GroupID=1 THEN COUNT(ID) ELSE 0 END AS BB, "+
	                        "    Case When GroupID=2 THEN COUNT(ID) ELSE 0 END AS BP , "+
	                        "    Case When GroupID=3 THEN COUNT(ID) ELSE 0 END AS ATK , "+
	                        "    Case When GroupID=6 THEN COUNT(ID) ELSE 0 END AS PYK , "+
	                        "    Case When GroupID=7 THEN COUNT(ID) ELSE 0 END AS MKT , "+
	                        "    Case When GroupID=8 THEN COUNT(ID) ELSE 0 END AS MEK , "+
	                        "    Case When GroupID=9 THEN COUNT(ID) ELSE 0 END AS ELK , "+
                            "    Case When GroupID=10 THEN COUNT(ID) ELSE 0 END AS RKP ,GroupID " +
	                        "    FROM( "+
		                    "        SELECT YEAR(CreatedTime) as Tahun,MONTH(CreatedTime) as Bulan,ID, GroupID  "+
		                    "        FROM Improvement where YEAR(CreatedTime)="+Tahun+" and RowStatus>-1 "+pln+
	                        "    ) as m group by GroupID,Bulan,Tahun "+
                            ")  "+
                            "as w "+
                            "Group By Bulan,Tahun "+
                            "Order by Bulan, Tahun";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrImprovement = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrImprovement.Add(GenerateObjRekap(sqlDataReader));
                }
            }
            else
            {
                arrImprovement.Add(new Improvement());
            }
            return arrImprovement;
        }
        public Improvement GenerateObjectRpt(SqlDataReader sqlDataReader)
        {

            objImprovement = new Improvement();
                objImprovement.ID = Convert.ToInt32(sqlDataReader["Num"]);
                objImprovement.ItemCode = sqlDataReader["ItemCode"].ToString();
                objImprovement.ItemName = sqlDataReader["ItemName"].ToString();
                objImprovement.UomCode = sqlDataReader["UOMCode"].ToString();
                objImprovement.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
                objImprovement.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
                objImprovement.Keterangan = sqlDataReader["Stocked"].ToString();
                objImprovement.StockNonStock = Convert.ToInt16(sqlDataReader["stock"]);
                //objImprovement.UnitKerjaID = Convert.ToInt16(sqlDataReader["UnitKerjaID"]);
                objImprovement.UnitKerja = sqlDataReader["UnitKerja"].ToString();
                objImprovement.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            return objImprovement;
        }
        public Improvement GenerateObjRekap(SqlDataReader sqlDateReader)
        {
            objImprovement = new Improvement();
            objImprovement.Tahun = Convert.ToInt32(sqlDateReader["Tahun"]);
            objImprovement.BulanLong =nBulan(Convert.ToInt32(sqlDateReader["Bulan"]));
            objImprovement.BB = Convert.ToInt32(sqlDateReader["BB"]);
            objImprovement.BP = Convert.ToInt32(sqlDateReader["BP"]);
            objImprovement.ATK = Convert.ToInt32(sqlDateReader["ATK"]);
            objImprovement.PYK = Convert.ToInt32(sqlDateReader["PYK"]);
            objImprovement.MKT = Convert.ToInt32(sqlDateReader["MKT"]);
            objImprovement.RKP = Convert.ToInt32(sqlDateReader["RKP"]);
            objImprovement.MEK = Convert.ToInt32(sqlDateReader["MEK"]);
            objImprovement.ELK = Convert.ToInt32(sqlDateReader["ELK"]);
            objImprovement.Total=Convert.ToInt32(sqlDateReader["BB"])+ Convert.ToInt32(sqlDateReader["BP"])+
                                 Convert.ToInt32(sqlDateReader["ATK"]) + Convert.ToInt32(sqlDateReader["PYK"])+
                                 Convert.ToInt32(sqlDateReader["MKT"]) + Convert.ToInt32(sqlDateReader["RKP"])+
                                 Convert.ToInt32(sqlDateReader["MEK"]) + Convert.ToInt32(sqlDateReader["ELK"]);
            return objImprovement;
        }
        public string nBulan(int Bulan)
        {
            string[] Bln = new string[] {"", "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };
            string blnLong = Bln.GetValue(Bulan).ToString();
            return blnLong;
        }
    }
}
