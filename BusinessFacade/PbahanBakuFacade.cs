using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BusinessFacade;
using DataAccessLayer;
using Domain;

namespace BusinessFacade
{
    public class PbahanBakuFacade : AbstractFacade
    {
        private PbahanBaku objpbb = new PbahanBaku();
        private ArrayList arrpbb;
        private List<SqlParameter> sqlListParam;
        public string Criteria { get; set; }

        public PbahanBakuFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objpbb = (PbahanBaku)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tgl_Prod", objpbb.Tgl_Prod));
                sqlListParam.Add(new SqlParameter("@Tgl_Prod2", objpbb.Tgl_Prod2));
                sqlListParam.Add(new SqlParameter("@L1", objpbb.L1));
                sqlListParam.Add(new SqlParameter("@L1x", objpbb.L1x));
                sqlListParam.Add(new SqlParameter("@L2", objpbb.L2));
                sqlListParam.Add(new SqlParameter("@L2x", objpbb.L2x));
                sqlListParam.Add(new SqlParameter("@L3", objpbb.L3));
                sqlListParam.Add(new SqlParameter("@L3x", objpbb.L3x));
                sqlListParam.Add(new SqlParameter("@L4", objpbb.L4));
                sqlListParam.Add(new SqlParameter("@L4x", objpbb.L4x));
                sqlListParam.Add(new SqlParameter("@L5", objpbb.L5));
                sqlListParam.Add(new SqlParameter("@L5x", objpbb.L5x));
                sqlListParam.Add(new SqlParameter("@L6", objpbb.L6));
                sqlListParam.Add(new SqlParameter("@L6x", objpbb.L6x));
                sqlListParam.Add(new SqlParameter("@TMix", objpbb.TMix));
                sqlListParam.Add(new SqlParameter("@Formula", objpbb.Formula));
                sqlListParam.Add(new SqlParameter("@SdL", objpbb.SdL));
                sqlListParam.Add(new SqlParameter("@SspB", objpbb.SspB));
                sqlListParam.Add(new SqlParameter("@AT", objpbb.AT));
                sqlListParam.Add(new SqlParameter("@KL", objpbb.KL));
                //sqlListParam.Add(new SqlParameter("@TSpb", objpbb.TSpb));
                //sqlListParam.Add(new SqlParameter("@TPemakaian", objpbb.TPemakaian));
                sqlListParam.Add(new SqlParameter("@Efis", objpbb.Efis));
                sqlListParam.Add(new SqlParameter("@JKertasVirgin", objpbb.JKertasVirgin));
                sqlListParam.Add(new SqlParameter("@KvKg", objpbb.KvKg));
                sqlListParam.Add(new SqlParameter("@KvKg2", objpbb.KvKg2));
                sqlListParam.Add(new SqlParameter("@KvEfis", objpbb.KvEfis));
                sqlListParam.Add(new SqlParameter("@BkBimaKg", objpbb.BkBimaKg));
                sqlListParam.Add(new SqlParameter("@SampahBima", objpbb.SampahBima));
                sqlListParam.Add(new SqlParameter("@BbBimaKg", objpbb.BbBimaKg));
                sqlListParam.Add(new SqlParameter("@KsKg0", objpbb.KsKg0));
                sqlListParam.Add(new SqlParameter("@KsKg", objpbb.KsKg));
                sqlListParam.Add(new SqlParameter("@KsEfis", objpbb.KsEfis));
                sqlListParam.Add(new SqlParameter("@KuKg", objpbb.KuKg));
                sqlListParam.Add(new SqlParameter("@KuEfs", objpbb.KuEfs));
                sqlListParam.Add(new SqlParameter("@Kgrade2Kg", objpbb.Kgrade2Kg));
                sqlListParam.Add(new SqlParameter("@Kgrade2Eff", objpbb.Kgrade2Eff));
                sqlListParam.Add(new SqlParameter("@Kgrade3Kg", objpbb.Kgrade3Kg));
                sqlListParam.Add(new SqlParameter("@Kgrade3Eff", objpbb.Kgrade3Eff));
                sqlListParam.Add(new SqlParameter("@TEfis", objpbb.TEfis));
                sqlListParam.Add(new SqlParameter("@KV", objpbb.KV));
                sqlListParam.Add(new SqlParameter("@KB", objpbb.KB));
                sqlListParam.Add(new SqlParameter("@KS", objpbb.KS));
                sqlListParam.Add(new SqlParameter("@KGradeUtama", objpbb.KGradeUtama));
                sqlListParam.Add(new SqlParameter("@Kgrade2", objpbb.Kgrade2));
                sqlListParam.Add(new SqlParameter("@Kgrade3", objpbb.Kgrade3));
                //sqlListParam.Add(new SqlParameter("@Fre", objpbb.Fre));
                //sqlListParam.Add(new SqlParameter("@Pks", objpbb.Pks));
                //sqlListParam.Add(new SqlParameter("@BbD", objpbb.BbD));
                //sqlListParam.Add(new SqlParameter("@WcD", objpbb.WcD));
                //sqlListParam.Add(new SqlParameter("@BkD", objpbb.BkD));
                //sqlListParam.Add(new SqlParameter("@SkK", objpbb.SkK));
                //sqlListParam.Add(new SqlParameter("@SkKPerc", objpbb.SkKPerc));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objpbb.CreatedBy));
                //sqlListParam.Add(new SqlParameter("@KbtKg", objpbb.KbtKg));
                //sqlListParam.Add(new SqlParameter("@KbtKg2", objpbb.KbtKg2));
                //sqlListParam.Add(new SqlParameter("@KbtEfis", objpbb.KbtEfis));
                //sqlListParam.Add(new SqlParameter("@KdLKaKg", objpbb.KdLKaKg));
                //sqlListParam.Add(new SqlParameter("@KdLKaKg2", objpbb.KdLKaKg2));
                //sqlListParam.Add(new SqlParameter("@KdLkaEfis", objpbb.KdLKaEfis));
                //sqlListParam.Add(new SqlParameter("@Kbt", objpbb.Kbt));
                //sqlListParam.Add(new SqlParameter("@Kdlka", objpbb.Kdlka));

                int intResult = dataAccess.ProcessData(sqlListParam, "InsertPBB2_SPNew");
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
                objpbb = (PbahanBaku)objDomain;
                sqlListParam = new List<SqlParameter>();
               // sqlListParam.Add(new SqlParameter("@ID", objpbb.ID));
                sqlListParam.Add(new SqlParameter("@Tgl_Prod", objpbb.Tgl_Prod));
                sqlListParam.Add(new SqlParameter("@L1G1", objpbb.L1G1));
                sqlListParam.Add(new SqlParameter("@L1G12", objpbb.L1G12));
                sqlListParam.Add(new SqlParameter("@L2G1", objpbb.L2G1));
                sqlListParam.Add(new SqlParameter("@L2G12", objpbb.L2G12));
                sqlListParam.Add(new SqlParameter("@L3G1", objpbb.L3G1));
                sqlListParam.Add(new SqlParameter("@L3G12", objpbb.L3G12));
                sqlListParam.Add(new SqlParameter("@L4G1", objpbb.L4G1));
                sqlListParam.Add(new SqlParameter("@L4G12", objpbb.L4G12));
                sqlListParam.Add(new SqlParameter("@L5G1", objpbb.L5G1));
                sqlListParam.Add(new SqlParameter("@L5G12", objpbb.L5G12));
                sqlListParam.Add(new SqlParameter("@L6G1", objpbb.L6G1));
                sqlListParam.Add(new SqlParameter("@L6G12", objpbb.L6G12));
                sqlListParam.Add(new SqlParameter("@TMix", objpbb.TMix));
                sqlListParam.Add(new SqlParameter("@Formula", objpbb.Formula));
                sqlListParam.Add(new SqlParameter("@Actual", objpbb.Actual));
                sqlListParam.Add(new SqlParameter("@sKg", objpbb.sKg));
                sqlListParam.Add(new SqlParameter("@sKgMix", objpbb.sKgMix));
                sqlListParam.Add(new SqlParameter("@Efis", objpbb.Efis));
                sqlListParam.Add(new SqlParameter("@Jkertas", objpbb.JKertas));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objpbb.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objpbb.LastModifiedTime));

                int intResult = dataAccess.ProcessData(sqlListParam,"UpdatePBB_SP");
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
                objpbb = (PbahanBaku)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objpbb.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objpbb.LastModifiedBy));

                int IntResult = dataAccess.ProcessData(sqlListParam,"DeletePBB_SP");
                strError = dataAccess.Error;
                return IntResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override ArrayList Retrieve()
        {
            #region old
            //string strSQL = " select ID,Tgl_Prod, L1G1,L1G12,L2G1,L2G12,L3G1,L3G12,L4G1,L4G12,L5G1,L5G12,L6G1,L6G12,TMix,Formula," +
            //                " Actual,sKg,sKgMix,Efis,JKertas from BB_Pemakaian where RowStatus>-1 " +
            //                this.Criteria + " ";
            #endregion
            string strSQL = " select ID,Tgl_Prod,L1,L2,L3,L4,L5,L6,TMix,Formula,SdL,SspB,AT,KL,TSpb,TPemakaian,Efis,JKertasVirgin,KvKg,KvKg2,KvEfis,KsKg,KsEfis,KkKg,KkEfis,TEfis,Kv,Ks,Kk,Fre,Pks,BbD,WcD,BkD,SkK,SkKPerc From BB_Pemakaian2 " +
                            " where RowStatus>-1  " +
                            this.Criteria + " order by Tgl_Prod Asc ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrpbb = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrpbb.Add(GenerateObjectNew(sqlDataReader));
                }
            }

            return arrpbb;
        }

        public int RetrieveInputan(string Periode)
        {
            string StrSql =
            " select SUM(Total)Total from (select COUNT(ID)Total from BM_Pulp " +
            " where LEFT(convert(char,Tgl_Prod0,112),6)='" + Periode + "' and Rowstatus>-1 union all select '0'Total ) as xx ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Total"]);
                }
            }

            return 0;
        }


        public decimal Retrievexx0(string tgl)
        {
            string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                            " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                            " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=1 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                            " ) as pakaiData ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Actual"]);
                }
            }
            return 0;

        }

        public decimal Retrievexx01(string tgl)
        {
            string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                            " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                            " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=2 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                            " ) as pakaiData ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Actual"]);
                }
            }
            return 0;

        }

        public decimal Retrievexx0a(string tgl)
        {
            string strSQL = " select isnull(sum (pakaiData.Quantity),0) * (0.035) as Actual  From ( " +
                            " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                            " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=2 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                            " ) as pakaiData ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Actual"]);
                }
            }
            return 0;

        }

        public decimal Retrievexx02(string tgl)
        {
            string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                           " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                           " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=3 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                           " ) as pakaiData ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Actual"]);
                }
            }
            return 0;
        }

        public decimal Retrievexx03(string tgl)
        {
            string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                           " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                           " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=4 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                           " ) as pakaiData ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Actual"]);
                }
            }
            return 0;
        }

        public decimal Retrievexx04(string tgl)
        {
            string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                           " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                           " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=5 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                           " ) as pakaiData ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Actual"]);
                }
            }
            return 0;

        }

        public decimal Retrievexx05(string tgl)
        {
            string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                           " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                           " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=6 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                           " ) as pakaiData ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Actual"]);
                }
            }
            return 0;

        }

        public decimal RetrievexxAllKertas(string tgl)
        {
            string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                           " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                           " (select ItemID from Pulp_Formula where ItemID=B.ItemID and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                           " ) as pakaiData ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Actual"]);
                }
            }
            return 0;

        }

        public ArrayList RetrievePulpCtrp( string Bulan, string Tahun)
        {
            string strSQL = 
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempPulp]') AND type in (N'U')) DROP TABLE [dbo].[TempPulp] ; " +
                                " select * into TempPulp from ( " +
		                        " select  Tgl_Prod,Tgl_Prod2,L1,L1x,L2,L2x,L3,L3x,L4,L4x,L5,L5x,L6,L6x,TMix,Formula,SdL,SspB,AT,KL,Efis,JKertasVirgin,isnull(KvKg,0)KvKg,isnull(Kvkg2,0)KvKg2, " +
	                            " KvEfis,BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,Round((KsEfis),1)KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff,Kgrade3Kg,Kgrade3Eff,Tefis, " +
	                            " Kv,Kb,Ks,KGradeUtama,Kgrade2,Kgrade3 " +
			                        " From ( " +
				                        " select Tgl_Prod,Tgl_Prod2,L1,L1x,L2,L2x,L3,L3x,L4,L4x,L5,L5x,L6,L6x,(L1+L1x+L2+L2x+L3+L3x+L4+L4x+L5+L5x+L6+L6x)TMix,Formula,SdL,SspB,AT,KL,Efis,JKertasVirgin,KvKg, " +
					                        " case " +
										    " when kv >= 0 and Kv <=10 then(KvKg) " +
					                        " when Kv >=20 and Kv <=40 then (kvkg*1.10) " +
					                        " when Kv >=50 and Kv <=60 then (kvkg*1.15) " +
					                        " when kv >=70 and Kv <=100 then (KvKg*1.20) end Kvkg2,cast(((Kv/100)*(-10)) as decimal (8,1))KvEfis, " +
                                  " BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,((efis)-(KvEfis+KuEfs+Kgrade2Eff+Kgrade3Eff)) KsEfis,KuKg,(KuKg/KL)KuEfs,Kgrade2Kg,(Kgrade2Kg/KL)Kgrade2Eff,Kgrade3Kg,(Kgrade3Kg/KL)Kgrade3Eff,cast(((((KvKg2+BkBimaKg+KsKg0+KuKg+Kgrade2Kg+Kgrade3Kg)-Formula)/(Formula)*100)) as decimal (10,2))TEfis,Kv,Kb,Ks,KGradeUtama,Kgrade2,Kgrade3, " +
			                        " RowStatus,CreatedBy,LastModifiedBy,LastModifiedTime " +
				                        " from BM_Pulp where  RowStatus>-1 " +
			                        " ) as x1 " +
		                        " )as xx  where " +
                                " Month(Tgl_Prod)='" + Bulan + "' and Year(Tgl_Prod)='" + Tahun + "' order by Tgl_Prod,Tgl_Prod2 " +
                           
						        " select * from ( " +
                                " select urt,Tgl_Prod2,Tgl_Prod,L1,L1x,L2,L2x,L3,L3x,L4,L4x,L5,L5x,L6,L6x,TMix,Formula,SdL,SspB,[AT],KL,Efis,JKertasVirgin,KvKg,KvKg2,KvEfis,BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff,Kgrade3Kg," +
							    " Kgrade3Eff,Tefis,Kv,Kb,Ks,KGradeUtama,Kgrade2,Kgrade3 from ( " +
                                " select 'A' urt,Tgl_Prod2,isnull(Tgl_Prod,0)Tgl_Prod,isnull(L1,0)L1,isnull(L1x,0)L1x,isnull(L2,0)L2,isnull(L2x,0)L2x,isnull(L3,0)L3,isnull(L3x,0)L3x,isnull(L4,0)L4,isnull(L4x,0)L4x,isnull(L5,0)L5,isnull(L5x,0)L5x,isnull(L6,0)L6,isnull(L6x,0)L6x,isnull(TMix,0)TMix, " +
                                " isnull(Formula,0)Formula,isnull(SdL,0)SdL,isnull(SspB,0)SspB,isnull(AT,0)[AT],isnull(KL,0)KL,isnull(Efis,0)Efis,JKertasVirgin, " +
                                " isnull(KvKg,0)KvKg,isnull(KvKg2,0)KvKg2,isnull(KvEfis,0)KvEfis,isnull(BkBimaKg,0)BkBimaKg,isnull(SampahBima,0)SampahBima,isnull(BbBimaKg,0)BbBimaKg,isnull(KsKg0,0)KsKg0,isnull(KsKg,0)KsKg,isnull(KsEfis,0)KsEfis,isnull(KuKg,0)KuKg, " +
                                " isnull(KuEfs,0)KuEfs,isnull(Kgrade2Kg,0)Kgrade2Kg,isnull(Kgrade2Eff,0)Kgrade2Eff,isnull(Kgrade3Kg,0)Kgrade3Kg,isnull(Kgrade3Eff,0)Kgrade3Eff, Tefis,Kv,kb,Ks,KGradeUtama,Kgrade2,Kgrade3 from TempPulp " +
                          
                                " union all " +
                                " select 'B' urt,'TOTAL' TOTAL,'',cast(isnull(sum(L1),0) as decimal(18,2))L1,cast(isnull(sum(L1x),0) as decimal(18,2))L1x,cast(isnull(sum(L2),0)as decimal(18,2))L2,cast(isnull(sum(L2x),0)as decimal(18,2))L2x,cast(isnull(sum(L3),0)as decimal(18,2))L3,cast(isnull(sum(L3x),0)as decimal(18,2))L3x,cast(isnull(sum(L4),0)as decimal (18,2))L4,cast(isnull(sum(L4x),0)as decimal (18,2))L4x, " +
                                " cast(isnull(sum(L5),0)as decimal(18,0))L5,cast(isnull(sum(L5x),0)as decimal(18,0))L5x,cast(isnull(sum(L6),0)as decimal(18,2))L6,cast(isnull(sum(L6x),0)as decimal(18,2))L6x,cast(isnull(sum(TMix),0)as decimal(18,2))TMix,cast(isnull(sum(Formula),0)as decimal)Formula, " +
                                " cast(isnull(sum(SdL),0)as decimal(18,2))SdL,cast(isnull(sum(SspB),0)as decimal(18,0))SspB,cast(isnull(sum(AT),0)as decimal(18,2))[AT],cast(isnull(sum(KL),0)as decimal(18,2))KL, " +
                                " isnull(cast((((sum(KL))-sum(Formula))/sum(Formula)*100)as decimal(18,1)),0)Efis,''JKertasVirgin, " +
                                " cast(isnull(sum(KvKg),0)as decimal(18,2))KvKg,cast(isnull(sum(KvKg2),0)as decimal(18,2))KvKg2,cast(isnull(AVG(KvEfis),0)as decimal(18,2))KvEfis, " +
							    " cast(isnull(sum(BkBimaKg),0)as decimal(18,2))BkBimaKg,cast(isnull(sum(SampahBima),0)as decimal(18,2))SampahBima,cast(isnull(sum(BbBimaKg),0)as decimal(18,2))BbBimaKg,cast(isnull(sum(KsKg0),0)as decimal(18,2))KsKg0,cast(isnull(sum(KsKg),0)as decimal(18,2))KsKg, " +
								" isnull(ROUND(AVG(KsEfis),1),0)KsEfis,cast(isnull(sum(KuKg),0)as decimal(18,2))Kukg,isnull(AVG(KuEfs),0)KuEfs, " +
								" cast(isnull(sum(Kgrade2Kg),0)as decimal(18,2))Kgrade2Kg,isnull(AVG(Kgrade2Eff),0)Kgrade2Eff,  " +
								" cast(isnull(sum(Kgrade3Kg),0)as decimal(18,2))Kgrade3Kg,isnull(AVG(Kgrade3Eff),0)Kgrade3Eff,  " +
								" isnull(cast((((sum(KvKg2) + sum(KsKg) + sum(KuKg) + sum(Kgrade2Kg)+ sum(Kgrade3Kg)) -sum(Formula)) / (sum(Formula)) * 100)as decimal(18,1)),0)Tefis,'0'Kv,'0'Kb,'0'Ks,'0'KgradeUtama,'0'Kgrade2,'0'Kgrade3  " +
                                " from TempPulp  " +
                            " )xxx  " +
						    " ) x4 order by urt,Tgl_Prod2,Tgl_Prod " ;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrpbb = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrpbb.Add(GenerateObjectNewx(sqlDataReader));
                }
            }
            else
                arrpbb.Add(new PbahanBaku());

            return arrpbb;
        }

        public ArrayList RetrievePulpJombang(string Bulan, string Tahun)
        {
            string strSQL =
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempPulp]') AND type in (N'U')) DROP TABLE [dbo].[TempPulp] ; " +
                               " select * into TempPulp from ( " +
                               " select  Tgl_Prod,Tgl_Prod2,L1,L1x,L2,L2x,L3,L3x,L4,L4x,L5,L5x,L6,L6x,TMix,Formula,SdL,SspB,AT,KL,Efis,JKertasVirgin,isnull(KvKg,0)KvKg,isnull(Kvkg2,0)KvKg2, " +
                               " KvEfis,BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,Round((KsEfis),1)KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff,Kgrade3Kg,Kgrade3Eff,Tefis, " +
                               " Kv,Kb,Ks,KGradeUtama,Kgrade2,Kgrade3 " +
                                   " From ( " +
                                       " select Tgl_Prod,Tgl_Prod2,L1,L1x,L2,L2x,L3,L3x,L4,L4x,L5,L5x,L6,L6x,(L1+L1x+L2+L2x+L3+L3x+L4+L4x+L5+L5x+L6+L6x)TMix,Formula,SdL,SspB,AT,KL,Efis,JKertasVirgin,KvKg, " +
                                           " case " +
                                           " when kv >= 0 and Kv <=10 then(KvKg) " +
                                           " when Kv >=20 and Kv <=40 then (kvkg*1.10) " +
                                           " when Kv >=50 and Kv <=60 then (kvkg*1.15) " +
                                           " when kv >=70 and Kv <=100 then (KvKg*1.20) end Kvkg2,cast(((Kv/100)*(-10)) as decimal (8,1))KvEfis," +
                                 " BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,((efis)-(KsEfis+KuEfs+Kgrade2Eff+Kgrade3Eff)) KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff,Kgrade3Kg,Kgrade3Eff,cast(((((KvKg2+KsKg0+Kgrade2+Kgrade3)-Formula)/(Formula)*100)) as decimal (10,2))TEfis,Kv,Kb,Ks,KGradeUtama,Kgrade2,Kgrade3," +
                                   " RowStatus,CreatedBy,LastModifiedBy,LastModifiedTime " +
                                       " from BM_Pulp where  RowStatus>-1 " +
                                   " ) as x1 " +
                               " )as xx  where " +
                               " Month(Tgl_Prod)='" + Bulan + "' and Year(Tgl_Prod)='" + Tahun + "' order by Tgl_Prod,Tgl_Prod2 " +

                               " select * from ( " +
                               " select urt,Tgl_Prod2,Tgl_Prod,L1,L1x,L2,L2x,L3,L3x,L4,L4x,L5,L5x,L6,L6x,TMix,Formula,SdL,SspB,[AT],KL,Efis,JKertasVirgin,KvKg,KvKg2,KvEfis,BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff,Kgrade3Kg," +
                               " Kgrade3Eff,Tefis,Kv,Kb,Ks,KGradeUtama,Kgrade2,Kgrade3 from ( " +
                               " select 'A' urt,Tgl_Prod2,isnull(Tgl_Prod,0)Tgl_Prod,isnull(L1,0)L1,isnull(L1x,0)L1x,isnull(L2,0)L2,isnull(L2x,0)L2x,isnull(L3,0)L3,isnull(L3x,0)L3x,isnull(L4,0)L4,isnull(L4x,0)L4x,isnull(L5,0)L5,isnull(L5x,0)L5x,isnull(L6,0)L6,isnull(L6x,0)L6x,isnull(TMix,0)TMix, " +
                               " isnull(Formula,0)Formula,isnull(SdL,0)SdL,isnull(SspB,0)SspB,isnull(AT,0)[AT],isnull(KL,0)KL,isnull(Efis,0)Efis,JKertasVirgin, " +
                               " isnull(KvKg,0)KvKg,isnull(KvKg2,0)KvKg2,isnull(KvEfis,0)KvEfis,isnull(BkBimaKg,0)BkBimaKg,isnull(SampahBima,0)SampahBima,isnull(BbBimaKg,0)BbBimaKg,isnull(KsKg0,0)KsKg0,isnull(KsKg,0)KsKg,isnull(KsEfis,0)KsEfis,isnull(KuKg,0)KuKg," +
                               " isnull(KuEfs,0)KuEfs,isnull(Kgrade2Kg,0)Kgrade2Kg,isnull(Kgrade2Eff,0)Kgrade2Eff,isnull(Kgrade3Kg,0)Kgrade3Kg,isnull(Kgrade3Eff,0)Kgrade3Eff, Tefis,Kv,kb,Ks,KGradeUtama,Kgrade2,Kgrade3 from TempPulp " +

                               " union all " +
                               " select 'B' urt,'TOTAL' TOTAL,'',cast(isnull(sum(L1),0) as decimal(18,2))L1,cast(isnull(sum(L1x),0) as decimal(18,2))L1x,cast(isnull(sum(L2),0)as decimal(18,2))L2,cast(isnull(sum(L2x),0)as decimal(18,2))L2x,cast(isnull(sum(L3),0)as decimal(18,2))L3,cast(isnull(sum(L3x),0)as decimal(18,2))L3x,cast(isnull(sum(L4),0)as decimal (18,2))L4,cast(isnull(sum(L4x),0)as decimal (18,2))L4x, " +
                               " cast(isnull(sum(L5),0)as decimal(18,0))L5,cast(isnull(sum(L5x),0)as decimal(18,0))L5x,cast(isnull(sum(L6),0)as decimal(18,2))L6,cast(isnull(sum(L6x),0)as decimal(18,2))L6x,cast(isnull(sum(TMix),0)as decimal(18,2))TMix,cast(isnull(sum(Formula),0)as decimal)Formula, " +
                               " cast(isnull(sum(SdL),0)as decimal(18,2))SdL,cast(isnull(sum(SspB),0)as decimal(18,0))SspB,cast(isnull(sum(AT),0)as decimal(18,2))[AT],cast(isnull(sum(KL),0)as decimal(18,2))KL," +
                               " isnull(cast((((sum(KL))-sum(Formula))/sum(Formula)*100)as decimal(18,1)),0)Efis,''JKertasVirgin, " +
                               " cast(isnull(sum(KvKg),0)as decimal(18,2))KvKg,cast(isnull(sum(KvKg2),0)as decimal(18,2))KvKg2,cast(isnull(AVG(KvEfis),0)as decimal(18,2))KvEfis," +
                                " cast(isnull(sum(BkBimaKg),0)as decimal(18,2))BkBimaKg,cast(isnull(sum(SampahBima),0)as decimal(18,2))SampahBima,cast(isnull(sum(BbBimaKg),0)as decimal(18,2))BbBimaKg,cast(isnull(sum(KsKg0),0)as decimal(18,2))KsKg0,cast(isnull(sum(KsKg),0)as decimal(18,2))KsKg, " +
                                " isnull(ROUND(AVG(KsEfis),1),0)KsEfis,cast(isnull(sum(KuKg),0)as decimal(18,2))Kukg,isnull(AVG(KuEfs),0)KuEfs, " +
                                " cast(isnull(sum(Kgrade2Kg),0)as decimal(18,2))Kgrade2Kg,isnull(AVG(Kgrade2Eff),0)Kgrade2Eff, " +
                                " cast(isnull(sum(Kgrade3Kg),0)as decimal(18,2))Kgrade3Kg,isnull(AVG(Kgrade3Eff),0)Kgrade3Eff, " +
                                " isnull(cast((((sum(KvKg2) + sum(KsKg) + sum(KuKg) + sum(Kgrade2Kg)+ sum(Kgrade3Kg)) -sum(Formula)) / (sum(Formula)) * 100)as decimal(18,1)),0)Tefis,'0'Kv,'0'Kb,'0'Ks,'0'KgradeUtama,'0'Kgrade2,'0'Kgrade3 " +
                               " from TempPulp " +
                           " )xxx  " +
                           " ) x4 order by urt,Tgl_Prod2,Tgl_Prod ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrpbb = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrpbb.Add(GenerateObjectNewx(sqlDataReader));
                }
            }
            else
                arrpbb.Add(new PbahanBaku());

            return arrpbb;
        }

        public ArrayList RetrievePulpKrwg(string Bulan, string Tahun)
        {
            string strSQL =
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempPulp]') AND type in (N'U')) DROP TABLE [dbo].[TempPulp] ; " +
                               " select * into TempPulp from ( " +
                               " select  Tgl_Prod,Tgl_Prod2,L1,L1x,L2,L2x,L3,L3x,L4,L4x,L5,L5x,L6,L6x,TMix,Formula,SdL,SspB,AT,KL,Efis,JKertasVirgin,isnull(KvKg,0)KvKg,isnull(Kvkg2,0)KvKg2, " +
                               " KvEfis,BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,((efis)-(KsEfis+KuEfs+Kgrade2Eff+Kgrade3Eff)) KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff,Kgrade3Kg,Kgrade3Eff,Tefis, " +
                               " Kv,Kb,Ks,KGradeUtama,Kgrade2,Kgrade3 " +
                                   " From ( " +
                                       " select Tgl_Prod,Tgl_Prod2,L1,L1x,L2,L2x,L3,L3x,L4,L4x,L5,L5x,L6,L6x,(L1+L1x+L2+L2x+L3+L3x+L4+L4x+L5+L5x+L6+L6x)TMix,Formula,SdL,SspB,AT,KL,Efis,JKertasVirgin,KvKg, " +
                                           " case " +
                                           " when kv >= 0 and Kv <=10 then(KvKg) " +
                                           " when Kv >=20 and Kv <=40 then (kvkg*1.10) " +
                                           " when Kv >=50 and Kv <=60 then (kvkg*1.15) " +
                                           " when kv >=70 and Kv <=100 then (KvKg*1.20) end Kvkg2,cast(((Kv/100)*(-10)) as decimal (8,1))KvEfis," +
                                 " BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,Round((KsEfis),1)KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff,Kgrade3Kg,Kgrade3Eff,cast(((((KvKg2+KsKg0+Kgrade2+Kgrade3)-Formula)/(Formula)*100)) as decimal (10,2))TEfis,Kv,Kb,Ks,KGradeUtama,Kgrade2,Kgrade3," +
                                   " RowStatus,CreatedBy,LastModifiedBy,LastModifiedTime " +
                                       " from BM_Pulp where  RowStatus>-1 " +
                                   " ) as x1 " +
                               " )as xx  where " +
                               " Month(Tgl_Prod)='" + Bulan + "' and Year(Tgl_Prod)='" + Tahun + "' order by Tgl_Prod,Tgl_Prod2 " +

                               " select * from ( " +
                               " select urt,Tgl_Prod2,Tgl_Prod,L1,L1x,L2,L2x,L3,L3x,L4,L4x,L5,L5x,L6,L6x,TMix,Formula,SdL,SspB,[AT],KL,Efis,JKertasVirgin,KvKg,KvKg2,KvEfis,BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff,Kgrade3Kg," +
                               " Kgrade3Eff,Tefis,Kv,Kb,Ks,KGradeUtama,Kgrade2,Kgrade3 from ( " +
                               " select 'A' urt,Tgl_Prod2,isnull(Tgl_Prod,0)Tgl_Prod,isnull(L1,0)L1,isnull(L1x,0)L1x,isnull(L2,0)L2,isnull(L2x,0)L2x,isnull(L3,0)L3,isnull(L3x,0)L3x,isnull(L4,0)L4,isnull(L4x,0)L4x,isnull(L5,0)L5,isnull(L5x,0)L5x,isnull(L6,0)L6,isnull(L6x,0)L6x,isnull(TMix,0)TMix, " +
                               " isnull(Formula,0)Formula,isnull(SdL,0)SdL,isnull(SspB,0)SspB,isnull(AT,0)[AT],isnull(KL,0)KL,isnull(Efis,0)Efis,JKertasVirgin, " +
                               " isnull(KvKg,0)KvKg,isnull(KvKg2,0)KvKg2,isnull(KvEfis,0)KvEfis,isnull(BkBimaKg,0)BkBimaKg,isnull(SampahBima,0)SampahBima,isnull(BbBimaKg,0)BbBimaKg,isnull(KsKg0,0)KsKg0,isnull(KsKg,0)KsKg,isnull(KsEfis,0)KsEfis,isnull(KuKg,0)KuKg," +
                               " isnull(KuEfs,0)KuEfs,isnull(Kgrade2Kg,0)Kgrade2Kg,isnull(Kgrade2Eff,0)Kgrade2Eff,isnull(Kgrade3Kg,0)Kgrade3Kg,isnull(Kgrade3Eff,0)Kgrade3Eff, Tefis,Kv,kb,Ks,KGradeUtama,Kgrade2,Kgrade3 from TempPulp " +

                               " union all " +
                               " select 'B' urt,'TOTAL' TOTAL,'',cast(isnull(sum(L1),0) as decimal(18,2))L1,cast(isnull(sum(L1x),0) as decimal(18,2))L1x,cast(isnull(sum(L2),0)as decimal(18,2))L2,cast(isnull(sum(L2x),0)as decimal(18,2))L2x,cast(isnull(sum(L3),0)as decimal(18,2))L3,cast(isnull(sum(L3x),0)as decimal(18,2))L3x,cast(isnull(sum(L4),0)as decimal (18,2))L4,cast(isnull(sum(L4x),0)as decimal (18,2))L4x, " +
                               " cast(isnull(sum(L5),0)as decimal(18,0))L5,cast(isnull(sum(L5x),0)as decimal(18,0))L5x,cast(isnull(sum(L6),0)as decimal(18,2))L6,cast(isnull(sum(L6x),0)as decimal(18,2))L6x,cast(isnull(sum(TMix),0)as decimal(18,2))TMix,cast(isnull(sum(Formula),0)as decimal)Formula, " +
                               " cast(isnull(sum(SdL),0)as decimal(18,2))SdL,cast(isnull(sum(SspB),0)as decimal(18,0))SspB,cast(isnull(sum(AT),0)as decimal(18,2))[AT],cast(isnull(sum(KL),0)as decimal(18,2))KL," +
                               " isnull(cast((((sum(KL))-sum(Formula))/sum(Formula)*100)as decimal(18,1)),0)Efis,''JKertasVirgin, " +
                               " cast(isnull(sum(KvKg),0)as decimal(18,2))KvKg,cast(isnull(sum(KvKg2),0)as decimal(18,2))KvKg2,cast(isnull(AVG(KvEfis),0)as decimal(18,2))KvEfis," +
                                " cast(isnull(sum(BkBimaKg),0)as decimal(18,2))BkBimaKg,cast(isnull(sum(SampahBima),0)as decimal(18,2))SampahBima,cast(isnull(sum(BbBimaKg),0)as decimal(18,2))BbBimaKg,cast(isnull(sum(KsKg0),0)as decimal(18,2))KsKg0,cast(isnull(sum(KsKg),0)as decimal(18,2))KsKg, " +
                                " isnull(ROUND(AVG(KsEfis),1),0)KsEfis,cast(isnull(sum(KuKg),0)as decimal(18,2))Kukg,isnull(AVG(KuEfs),0)KuEfs, " +
                                " cast(isnull(sum(Kgrade2Kg),0)as decimal(18,2))Kgrade2Kg,isnull(AVG(Kgrade2Eff),0)Kgrade2Eff, " +
                                " cast(isnull(sum(Kgrade3Kg),0)as decimal(18,2))Kgrade3Kg,isnull(AVG(Kgrade3Eff),0)Kgrade3Eff, " +
                                " isnull(cast((((sum(KvKg2) + sum(KsKg) + sum(KuKg) + sum(Kgrade2Kg)+ sum(Kgrade3Kg)) -sum(Formula)) / (sum(Formula)) * 100)as decimal(18,1)),0)Tefis,'0'Kv,'0'Kb,'0'Ks,'0'KgradeUtama,'0'Kgrade2,'0'Kgrade3 " +
                               " from TempPulp " +
                           " )xxx  " +
                           " ) x4 order by urt,Tgl_Prod2,Tgl_Prod ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrpbb = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrpbb.Add(GenerateObjectNewx(sqlDataReader));
                }
            }
            else
                arrpbb.Add(new PbahanBaku());

            return arrpbb;
        }

        public PbahanBaku GenerateObject(SqlDataReader sqlDataReader)
        {
            objpbb = new PbahanBaku();
            objpbb.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objpbb.Tgl_Prod = Convert.ToDateTime(sqlDataReader["Tgl_Prod"]);
            objpbb.L1G1 = Convert.ToDecimal(sqlDataReader["L1G1"]);
            objpbb.L1G12 = Convert.ToDecimal(sqlDataReader["L1G12"]);
            objpbb.L2G1 = Convert.ToDecimal(sqlDataReader["L2G1"]);
            objpbb.L2G12 = Convert.ToDecimal(sqlDataReader["L2G12"]);
            objpbb.L3G1 = Convert.ToDecimal(sqlDataReader["L3G1"]);
            objpbb.L3G12 = Convert.ToDecimal(sqlDataReader["L3G12"]);
            objpbb.L4G1 = Convert.ToDecimal(sqlDataReader["L4G1"]);
            objpbb.L4G12 = Convert.ToDecimal(sqlDataReader["L4G12"]);
            objpbb.L5G1 = Convert.ToDecimal(sqlDataReader["L5G1"]);
            objpbb.L5G12 = Convert.ToDecimal(sqlDataReader["L5G12"]);
            objpbb.L6G1 = Convert.ToDecimal(sqlDataReader["L6G1"]);
            objpbb.L6G12 = Convert.ToDecimal(sqlDataReader["L6G12"]);
            objpbb.TMix = Convert.ToDecimal(sqlDataReader["TMix"]);
            objpbb.Formula = Convert.ToDecimal(sqlDataReader["Formula"]);
            objpbb.Actual = Convert.ToDecimal(sqlDataReader["Actual"]);
            objpbb.sKg = Convert.ToDecimal(sqlDataReader["sKg"]);
            objpbb.sKgMix = Convert.ToDecimal(sqlDataReader["sKgMix"]);
            objpbb.Efis = Convert.ToDecimal(sqlDataReader["Efis"]);
            objpbb.JKertas = sqlDataReader["JKertas"].ToString();
            //objpbb.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            //objpbb.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objpbb.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            //objpbb.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objpbb.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objpbb;
        }

        public PbahanBaku GenerateObjectNew(SqlDataReader sqlDataReader)
        {
            objpbb = new PbahanBaku();
            objpbb.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objpbb.Tgl_Prod = Convert.ToDateTime(sqlDataReader["Tgl_Prod"]);
            objpbb.L1 = Convert.ToDecimal(sqlDataReader["L1"]);
            objpbb.L2 = Convert.ToDecimal(sqlDataReader["L2"]);
            objpbb.L3 = Convert.ToDecimal(sqlDataReader["L3"]);
            objpbb.L4 = Convert.ToDecimal(sqlDataReader["L4"]);
            objpbb.L5 = Convert.ToDecimal(sqlDataReader["L5"]);
            objpbb.L6 = Convert.ToDecimal(sqlDataReader["L6"]);
            objpbb.TMix = Convert.ToDecimal(sqlDataReader["TMix"]);
            objpbb.Formula = Convert.ToDecimal(sqlDataReader["Formula"]);
            objpbb.SdL = Convert.ToDecimal(sqlDataReader["SdL"]);
            objpbb.SspB = Convert.ToDecimal(sqlDataReader["SspB"]);
            objpbb.AT = Convert.ToDecimal(sqlDataReader["AT"]);
            objpbb.KL = Convert.ToDecimal(sqlDataReader["KL"]);
            objpbb.TSpb = Convert.ToDecimal(sqlDataReader["TSpb"]);
            objpbb.TPemakaian = Convert.ToDecimal(sqlDataReader["TPemakaian"]);
            objpbb.Efis = Convert.ToDecimal(sqlDataReader["Efis"]);
            objpbb.JKertasVirgin = sqlDataReader["JKertasVirgin"].ToString();
            objpbb.KvKg = Convert.ToDecimal(sqlDataReader["KvKg"]);
            objpbb.KvKg2 = Convert.ToDecimal(sqlDataReader["KvKg2"]);
            objpbb.KvEfis = Convert.ToDecimal(sqlDataReader["KvEfis"]);
            objpbb.KsKg = Convert.ToDecimal(sqlDataReader["KsKg"]);
            objpbb.KsEfis = Convert.ToDecimal(sqlDataReader["KsEfis"]);
            objpbb.KkKg = Convert.ToDecimal(sqlDataReader["KkKg"]);
            objpbb.KkEfis = Convert.ToDecimal(sqlDataReader["KkEfis"]);
            //objpbb.KdKg = Convert.ToDecimal(sqlDataReader["KdKg"]);
            //objpbb.KdEfis = Convert.ToDecimal(sqlDataReader["KdEfis"]);
            objpbb.TEfis = Convert.ToDecimal(sqlDataReader["TEfis"]);
            objpbb.KV = Convert.ToDecimal(sqlDataReader["KV"]);
            objpbb.KS = Convert.ToDecimal(sqlDataReader["KS"]);
            objpbb.KK = Convert.ToDecimal(sqlDataReader["KK"]);
            objpbb.Fre = Convert.ToDecimal(sqlDataReader["Fre"]);
            objpbb.Pks = Convert.ToDecimal(sqlDataReader["Pks"]);
            objpbb.BbD = Convert.ToDecimal(sqlDataReader["BbD"]);
            objpbb.WcD = Convert.ToDecimal(sqlDataReader["WcD"]);
            objpbb.BkD = Convert.ToDecimal(sqlDataReader["BkD"]);
            objpbb.SkK = Convert.ToDecimal(sqlDataReader["SkK"]);
            objpbb.SkKPerc = Convert.ToDecimal(sqlDataReader["SkKPerc"]);
            return objpbb;
        }

        public PbahanBaku GenerateObjectNewx(SqlDataReader sqlDataReader)
        {
            objpbb = new PbahanBaku();
            //objpbb.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objpbb.Tgl_Prod = Convert.ToDateTime(sqlDataReader["Tgl_Prod"]);
            objpbb.Tgl_Prod2 = sqlDataReader["Tgl_Prod2"].ToString();
            objpbb.L1 = Convert.ToDecimal(sqlDataReader["L1"]);
            objpbb.L1x = Convert.ToDecimal(sqlDataReader["L1x"]);
            objpbb.L2 = Convert.ToDecimal(sqlDataReader["L2"]);
            objpbb.L2x = Convert.ToDecimal(sqlDataReader["L2x"]);
            objpbb.L3 = Convert.ToDecimal(sqlDataReader["L3"]);
            objpbb.L3x = Convert.ToDecimal(sqlDataReader["L3x"]);
            objpbb.L4 = Convert.ToDecimal(sqlDataReader["L4"]);
            objpbb.L4x = Convert.ToDecimal(sqlDataReader["L4x"]);
            objpbb.L5 = Convert.ToDecimal(sqlDataReader["L5"]);
            objpbb.L5x = Convert.ToDecimal(sqlDataReader["L5x"]);
            objpbb.L6 = Convert.ToDecimal(sqlDataReader["L6"]);
            objpbb.L6x = Convert.ToDecimal(sqlDataReader["L6x"]);
            objpbb.TMix = Convert.ToDecimal(sqlDataReader["TMix"]);
            objpbb.Formula = Convert.ToDecimal(sqlDataReader["Formula"]);
            objpbb.SdL = Convert.ToDecimal(sqlDataReader["SdL"]);
            objpbb.SspB = Convert.ToDecimal(sqlDataReader["SspB"]);
            objpbb.AT = Convert.ToDecimal(sqlDataReader["AT"]);
            objpbb.KL = Convert.ToDecimal(sqlDataReader["KL"]);
            objpbb.Efis = Convert.ToDecimal(sqlDataReader["Efis"]);
            objpbb.JKertasVirgin = sqlDataReader["JKertasVirgin"].ToString();
            objpbb.KvKg = Convert.ToDecimal(sqlDataReader["KvKg"]);
            objpbb.KvKg2 = Convert.ToDecimal(sqlDataReader["KvKg2"]);
            objpbb.KvEfis = Convert.ToDecimal(sqlDataReader["KvEfis"]);
            objpbb.BkBimaKg = Convert.ToDecimal(sqlDataReader["BkBimaKg"]);
            objpbb.SampahBima = Convert.ToDecimal(sqlDataReader["SampahBima"]);
            objpbb.BbBimaKg = Convert.ToDecimal(sqlDataReader["BbBimaKg"]);
            objpbb.KsKg0 = Convert.ToDecimal(sqlDataReader["KsKg0"]);
            objpbb.KsKg = Convert.ToDecimal(sqlDataReader["KsKg"]);
            objpbb.KsEfis = Convert.ToDecimal(sqlDataReader["KsEfis"]);
            objpbb.KuKg = Convert.ToDecimal(sqlDataReader["KuKg"]);
            objpbb.KuEfs = Convert.ToDecimal(sqlDataReader["KuEfs"]);
            objpbb.Kgrade2Kg = Convert.ToDecimal(sqlDataReader["Kgrade2Kg"]);
            objpbb.Kgrade2Eff = Convert.ToDecimal(sqlDataReader["Kgrade2Eff"]);
            objpbb.Kgrade3Kg = Convert.ToDecimal(sqlDataReader["Kgrade3Kg"]);
            objpbb.Kgrade3Eff = Convert.ToDecimal(sqlDataReader["Kgrade3Eff"]);
            objpbb.TEfis = Convert.ToDecimal(sqlDataReader["Tefis"]);
            objpbb.KV = Convert.ToDecimal(sqlDataReader["Kv"]);
            objpbb.KB = Convert.ToDecimal(sqlDataReader["Kb"]);
            objpbb.KS = Convert.ToDecimal(sqlDataReader["Ks"]);
            objpbb.KGradeUtama = Convert.ToDecimal(sqlDataReader["KGradeUtama"]);
            objpbb.Kgrade2 = Convert.ToDecimal(sqlDataReader["Kgrade2"]);
            objpbb.Kgrade3 = Convert.ToDecimal(sqlDataReader["Kgrade3"]);
            return objpbb;
        }
    }
}
