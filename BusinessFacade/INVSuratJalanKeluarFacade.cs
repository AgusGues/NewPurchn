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
    public class INVSuratJalanKeluarFacade : AbstractFacade
    {
        private SuratJalankeluar objsjk = new SuratJalankeluar();
        private ArrayList arrsjk;
        private ArrayList arrData = new ArrayList();
        private List <SqlParameter> sqlListParam;

        public INVSuratJalanKeluarFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objsjk = (SuratJalankeluar)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoUrut", objsjk.NoUrut));
                sqlListParam.Add(new SqlParameter("@NoSJ", objsjk.NoSJ));
                sqlListParam.Add(new SqlParameter("@TglSJ", objsjk.TglSJ));
                sqlListParam.Add(new SqlParameter("@Tujuan", objsjk.Tujuan));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objsjk.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Satuan", objsjk.Satuan));
                sqlListParam.Add(new SqlParameter("@ItemID", objsjk.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemName", objsjk.ItemName));
                sqlListParam.Add(new SqlParameter("@Jumlah", objsjk.Jumlah));
                sqlListParam.Add(new SqlParameter("@Ket", objsjk.Ket));
                sqlListParam.Add(new SqlParameter("@NoPolisi", objsjk.NoPolisi));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objsjk.CreatedBy));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objsjk.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertInvSJK");

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
                objsjk = (SuratJalankeluar)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objsjk.ID));
                sqlListParam.Add(new SqlParameter("@Tujuan", objsjk.Tujuan));
                sqlListParam.Add(new SqlParameter("@Jumlah", objsjk.Jumlah));
                sqlListParam.Add(new SqlParameter("@Ket", objsjk.Ket));
                sqlListParam.Add(new SqlParameter("@NoPolisi", objsjk.NoPolisi));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objsjk.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objsjk.LastModifiedTime));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateInvSJK");

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
                objsjk = (SuratJalankeluar)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objsjk.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objsjk.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteInvSJK");

                strError = dataAccess.Error;

                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int Tahun { get; set; }
        public int RetrieveMaxId()
        {
            int result = 0;
            DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
            string strSQL = "select TOP 1 ISNULL(NoUrut,0) NoUrut from INV_suratjalan_keluar where RowStatus>-1 AND Year(TglSJ)=" + this.Tahun + " ORDER BY NOURUT DESC";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrsjk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result= int.Parse(sqlDataReader["NoUrut"].ToString());
                }
            }

            return result;
            
        }


        public override ArrayList Retrieve()
        {
            string strSQL = "select * from INV_suratjalan_keluar where RowStatus > -1 order by ID Desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrsjk = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrsjk.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrsjk;
        }

        public SuratJalankeluar RetrieveByNoSJ(string NoSJ)
        {
            string strSQL = " select A.ID,A.NoSJ,A.TglSJ,A.Tujuan,A.Ket,A.Jumlah,A.NoPolisi,A.UOM," +
                                     " case A.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID) " +
                                     " when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID)else '' end ItemCode, " +
                                     " Case A.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
                                     " when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
                                     " end ItemName,A.ItemID,A.ItemTypeID,A.CreatedBy " +
                                     " from INV_suratjalan_keluar as A where  A.RowStatus > -1 and A.NoSJ = '" + NoSJ + "'  order by A.ID Desc ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrsjk = new ArrayList();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return GenerateObjectList(sdr);
                }
            }

            return new SuratJalankeluar();
        }

        public ArrayList RetrieveBySJ1(string NoSJ)
        {
            string strSQL = " select A.ID,A.NoSJ,A.TglSJ,A.Tujuan,A.Ket,A.Jumlah,A.NoPolisi,A.UOM," +
                                     " case A.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID) " +
                                     " when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID)else '' end ItemCode, " +
                                     " Case A.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
                                     " when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
                                     " else Itemname end ItemName,A.ItemID,A.ItemTypeID,A.CreatedBy " +
                                     " from INV_suratjalan_keluar as A where  A.RowStatus > -1 and A.NoSJ = '" + NoSJ + "'  order by A.ID Desc ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrsjk = new ArrayList();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrsjk.Add(GenerateObjectList(sdr));
                    //return 
                }
            }
            else
                arrsjk.Add (new SuratJalankeluar());

            return arrsjk;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = " select top 100 A.ID,A.NoSJ,A.TglSJ,A.Tujuan,A.Ket,A.Jumlah,A.NoPolisi,A.UOM," +
                            " case A.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID) " +
                            " when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID)else '' end ItemCode, " +
                            " Case A.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
                            " when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
                            " end ItemName,A.ItemID,A.ItemTypeID,A.CreatedBy " +
                            " from INV_suratjalan_keluar as A where  A.RowStatus > -1 and " + strField + " like '%" + strValue + "%'";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrsjk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrsjk.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else

                arrsjk.Add(new SuratJalankeluar());
                
            return arrsjk; 
        }

        public ArrayList RetrieveByAll()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = " select A.ID,A.NoSJ,A.TglSJ,A.Tujuan,A.Ket,A.Jumlah,A.NoPolisi,A.UOM," +
                            " case A.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID) " +
                            " when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID)else '' end ItemCode, " +
                            " Case A.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
                            " when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
                            " else itemname end ItemName,A.ItemID,A.ItemTypeID,A.CreatedBy " +
                            " from INV_suratjalan_keluar as A where  A.RowStatus > -1 order by A.ID desc,A.NoSJ Desc,A.TglSJ ";
                
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrsjk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrsjk.Add(GenerateObjectList(sqlDataReader));
                }
            }


            return arrsjk;
        }

        //public ArrayList RetrieveRecapSJK(string TipeBarang)
        //{
        //    arrData = new ArrayList();
        //    string where = (TipeBarang == "0") ? "" : " where d.ItemTypeID=" + TipeBarang;
        //    //string strSql = " select  d.ItemTypeID,s.TypeDescription from ItemTypePurchn s left join INV_suratjalan_keluar d on s.ID=d.ItemTypeID " + where + "  Group by d.ItemTypeID,s.TypeDescription";
        //    ///string strSql = "  select s.ID,s.TypeDescription from ItemTypePurchn s left join INV_suratjalan_keluar d on s.ID=d.ItemTypeID " + where + " Group by s.ID,d.ItemTypeID,s.TypeDescription" ;
        //    string strSql = " select s.ItemTypeID,d.TypeDescription from INV_suratjalan_keluar s left join ItemTypePurchn d on d.ID=s.ItemTypeID " + where + " Group by d.TypeDescription, s.ItemTypeID ";
        //    DataAccess da = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sdr = da.RetrieveDataByString(strSql);
        //    if (da.Error == string.Empty && sdr.HasRows)
        //    {
        //        while (sdr.Read())
        //        {
        //            arrData.Add(GenObject(sdr));
        //        }
        //    }
        //    return arrData;
        //}

        public ArrayList RetrieveRecapListSJK(string tglawal, string tglakhir, string ItemTypeID)
        {
            string kriteria = string.Empty;
            if (ItemTypeID != "0")
                kriteria = kriteria + " and A.ItemTypeID=" + ItemTypeID;
            if (ItemTypeID == "99")
                kriteria = " and A.ItemTypeID='0' order by A.ID Desc ";
            if (ItemTypeID == "1")
                kriteria = " and A.ItemTypeID='1' order by A.ID Desc ";
            if (ItemTypeID == "2")
                kriteria = " and A.ItemTypeID='2' order by A.ID Desc ";
            if (ItemTypeID == "3")
                kriteria = " and A.ItemTypeID='3' order by A.ID Desc ";
            string strSQL = 
                             " select  A.ID,isnull(A.NoSJ,0)NoSJ,A.TglSJ,A.Tujuan,A.Ket,A.Jumlah,A.NoPolisi,A.UOM, case A.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID) " +
                             " when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID)else '' end ItemCode,  Case A.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
                             " when 2 then (select ItemName from Asset where Asset.ID=A.ItemID)  else itemname end ItemName,A.ItemID,A.ItemTypeID, case when A.ItemTypeID =1 then 'Inventory' when A.ItemTypeID=2 " +
                             " then 'Asset' when A.ItemTypeID=3 then 'Biaya' end ItemTypeName ,A.CreatedBy  from INV_suratjalan_keluar as A where  A.RowStatus > -1 " +
                             " and A.TglSJ between '" + tglawal + "' and '" + tglakhir + "' " + kriteria ;  
                             
            //order by A.ID desc,A.NoSJ Desc,A.TglSJ ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrsjk = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrsjk.Add(GenerateObjectList2(sqlDataReader));
                }
            }
            return arrsjk;

        }

        public SuratJalankeluar GenerateObjectList2(SqlDataReader sqlDataReader)
        {
            objsjk = new SuratJalankeluar();
            objsjk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objsjk.NoSJ = sqlDataReader["NoSJ"].ToString();
            objsjk.TglSJ = Convert.ToDateTime(sqlDataReader["TglSJ"]);
            objsjk.Tujuan = sqlDataReader["Tujuan"].ToString();
            objsjk.Ket = sqlDataReader["Ket"].ToString();
            objsjk.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objsjk.NoPolisi = sqlDataReader["NoPolisi"].ToString();
            objsjk.Satuan = sqlDataReader["UOM"].ToString();
            objsjk.ItemCode = sqlDataReader["ItemCode"].ToString();
            objsjk.ItemName = sqlDataReader["ItemName"].ToString();
            return objsjk;
        }


        private SuratJalankeluar GenObject(SqlDataReader sdr)
        {
            SuratJalankeluar b = new SuratJalankeluar();
            b.ItemTypeID = int.Parse(sdr["ItemTypeID"].ToString());
            //b.ItemTypeID = int.Parse(sdr["ID"].ToString());
            b.TypeDescription = sdr["TypeDescription"].ToString();
            return b;
        }

        public SuratJalankeluar GenerateObject1(SqlDataReader sqlDataReader)
        {
            objsjk = new SuratJalankeluar();
            objsjk.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);
            return objsjk;
        }

        public SuratJalankeluar GenerateObject(SqlDataReader sqlDataReader)
        {
            objsjk = new SuratJalankeluar();
            objsjk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objsjk.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);
            objsjk.NoSJ = sqlDataReader["NoSJ"].ToString();
            objsjk.TglSJ = Convert.ToDateTime(sqlDataReader["TglSJ"]);
            objsjk.Tujuan = sqlDataReader["Tujuan"].ToString();
            objsjk.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objsjk.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objsjk.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objsjk.Ket = sqlDataReader["Ket"].ToString();
            objsjk.NoPolisi = sqlDataReader["NoPolisi"].ToString();
            objsjk.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objsjk.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objsjk.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objsjk.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objsjk.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objsjk;
        }

        public SuratJalankeluar GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objsjk = new SuratJalankeluar();
            objsjk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objsjk.NoSJ = sqlDataReader["NoSJ"].ToString();
            objsjk.TglSJ = Convert.ToDateTime(sqlDataReader["TglSJ"]);
            objsjk.ItemCode = sqlDataReader["ItemCode"].ToString();
            objsjk.ItemName = sqlDataReader["ItemName"].ToString();
            objsjk.Tujuan = sqlDataReader["Tujuan"].ToString();
            objsjk.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objsjk.NoPolisi = sqlDataReader["NoPolisi"].ToString();
            objsjk.Ket = sqlDataReader["Ket"].ToString();
            objsjk.Satuan = sqlDataReader["UOM"].ToString();
            return objsjk;
        }
    }
}
