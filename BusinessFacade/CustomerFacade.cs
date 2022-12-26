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
    public class CustomerFacade : AbstractFacade
    {
        private Customer objCustomer = new Customer();
        private ArrayList arrCustomer;
        private List<SqlParameter> sqlListParam;

        public CustomerFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objCustomer = (Customer)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@CustomerCode", objCustomer.CustomerCode));
                sqlListParam.Add(new SqlParameter("@CustomerName", objCustomer.CustomerName));
                sqlListParam.Add(new SqlParameter("@Address", objCustomer.Address));
                sqlListParam.Add(new SqlParameter("@PropinsiID", objCustomer.PropinsiID));
                sqlListParam.Add(new SqlParameter("@CityID", objCustomer.CityID));
                sqlListParam.Add(new SqlParameter("@KabupatenID", objCustomer.KabupatenID));
                sqlListParam.Add(new SqlParameter("@JoinDate", objCustomer.JoinDate));
                sqlListParam.Add(new SqlParameter("@CreditLimit", objCustomer.CreditLimit));
                sqlListParam.Add(new SqlParameter("@ZonaID", objCustomer.ZonaID));
                sqlListParam.Add(new SqlParameter("@NPWP", objCustomer.NPWP));
                sqlListParam.Add(new SqlParameter("@Telepon", objCustomer.Telepon));
                sqlListParam.Add(new SqlParameter("@SalesmanID",objCustomer.SalesmanID));
                sqlListParam.Add(new SqlParameter("@ContactPerson", objCustomer.ContactPerson));
                sqlListParam.Add(new SqlParameter("@LokasiPajak", objCustomer.LokasiPajak));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objCustomer.CreatedBy));
                sqlListParam.Add(new SqlParameter("@FlagCL", objCustomer.FlagCL));

                sqlListParam.Add(new SqlParameter("@CategoryCompanyID", objCustomer.CategoryCompanyID));
                sqlListParam.Add(new SqlParameter("@CategoryPaymentID", objCustomer.CategoryPaymentID));
                sqlListParam.Add(new SqlParameter("@EmailAddress", objCustomer.EmailAddress));
                sqlListParam.Add(new SqlParameter("@JmlHari", objCustomer.JmlHari));
                sqlListParam.Add(new SqlParameter("@UangMukaPersen", objCustomer.UangMukaPersen));
                sqlListParam.Add(new SqlParameter("@LamaPembayaran", objCustomer.LamaPembayaran));
                sqlListParam.Add(new SqlParameter("@JenisCustomer", objCustomer.JenisCustomer));
                sqlListParam.Add(new SqlParameter("@DistAlmtID", objCustomer.DistAlmtID));

                sqlListParam.Add(new SqlParameter("@Npwp_Nama", objCustomer.AsNamaTax));
                sqlListParam.Add(new SqlParameter("@Npwp_Alamat", objCustomer.AsAlamatTax));
                sqlListParam.Add(new SqlParameter("@Ktp_Nik", objCustomer.Ktp_Nik));
                sqlListParam.Add(new SqlParameter("@Ktp_Nama", objCustomer.Ktp_Nama));
                sqlListParam.Add(new SqlParameter("@Ktp_Alamat", objCustomer.Ktp_Alamat));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertCustomer");

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
                objCustomer = (Customer)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCustomer.ID));
                sqlListParam.Add(new SqlParameter("@CustomerCode", objCustomer.CustomerCode));
                sqlListParam.Add(new SqlParameter("@CustomerName", objCustomer.CustomerName));
                sqlListParam.Add(new SqlParameter("@Address", objCustomer.Address));
                sqlListParam.Add(new SqlParameter("@PropinsiID", objCustomer.PropinsiID));
                sqlListParam.Add(new SqlParameter("@CityID", objCustomer.CityID));
                sqlListParam.Add(new SqlParameter("@KabupatenID", objCustomer.KabupatenID));
                sqlListParam.Add(new SqlParameter("@JoinDate", objCustomer.JoinDate));
                sqlListParam.Add(new SqlParameter("@CreditLimit", objCustomer.CreditLimit));
                sqlListParam.Add(new SqlParameter("@ZonaID", objCustomer.ZonaID));
                sqlListParam.Add(new SqlParameter("@NPWP", objCustomer.NPWP));
                sqlListParam.Add(new SqlParameter("@Telepon", objCustomer.Telepon));
                sqlListParam.Add(new SqlParameter("@SalesmanID", objCustomer.SalesmanID));
                sqlListParam.Add(new SqlParameter("@ContactPerson", objCustomer.ContactPerson));
                sqlListParam.Add(new SqlParameter("@LokasiPajak", objCustomer.LokasiPajak));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objCustomer.CreatedBy));
                sqlListParam.Add(new SqlParameter("@FlagCL", objCustomer.FlagCL));
                sqlListParam.Add(new SqlParameter("@CategoryCompanyID", objCustomer.CategoryCompanyID));
                sqlListParam.Add(new SqlParameter("@CategoryPaymentID", objCustomer.CategoryPaymentID));
                sqlListParam.Add(new SqlParameter("@EmailAddress", objCustomer.EmailAddress));
                sqlListParam.Add(new SqlParameter("@JmlHari", objCustomer.JmlHari));
                sqlListParam.Add(new SqlParameter("@UangMukaPersen", objCustomer.UangMukaPersen));

                sqlListParam.Add(new SqlParameter("@LamaPembayaran", objCustomer.LamaPembayaran));
                sqlListParam.Add(new SqlParameter("@JenisCustomer", objCustomer.JenisCustomer));

                sqlListParam.Add(new SqlParameter("@DistAlmtID", objCustomer.DistAlmtID));

                sqlListParam.Add(new SqlParameter("@Npwp_Nama", objCustomer.AsNamaTax));
                sqlListParam.Add(new SqlParameter("@Npwp_Alamat", objCustomer.AsAlamatTax));
                sqlListParam.Add(new SqlParameter("@Ktp_Nik", objCustomer.Ktp_Nik));
                sqlListParam.Add(new SqlParameter("@Ktp_Nama", objCustomer.Ktp_Nama));
                sqlListParam.Add(new SqlParameter("@Ktp_Alamat", objCustomer.Ktp_Alamat));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateCustomer");

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
                objCustomer = (Customer)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCustomer.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objCustomer.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteCustomer");

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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 order by id desc");
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCustomer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCustomer.Add(new Customer());

            return arrCustomer;
        }

        public ArrayList RetrieveAll()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 order by A.CustomerName");
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCustomer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCustomer.Add(new Customer());

            return arrCustomer;
        }
        public Customer RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.ID = " + Id);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Customer();
        }
        public Customer RetrieveByIdNpwpKtp(int Id,int TandaTerimaId)
        {
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.ID = " + Id);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.ID = " + Id);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode, "+
            //"--A.CustomerName"+
            "( " +
            "	case when ISNULL((select top 1 NPWPNomor from NPWP where CustomerType=2 and RowStatus>=0 and CustomerId=A.ID and  "+
            "		Convert(varchar,(select top 1 TglPenerimaan from Invoice where id=(select MIN(InvoiceID) from TandaTerimaDetail where TandaTerimaID=" + TandaTerimaId + ")),112)>= " +
            "		Convert(varchar,FromPeriod,112) and Convert(varchar,(select top 1 TglPenerimaan from Invoice where id=(select MIN(InvoiceID) from TandaTerimaDetail where TandaTerimaID=" + TandaTerimaId + ")),112)<= " +
            "		Convert(varchar,ToPeriod,112)  "+
            "		order by id desc),'')!='' "+
            "		then (select top 1 NPWPNama from NPWP where CustomerType=2 and RowStatus>=0 and CustomerId=A.ID and  "+
            "			Convert(varchar,(select top 1 TglPenerimaan from Invoice where id=(select MIN(InvoiceID) from TandaTerimaDetail where TandaTerimaID=" + TandaTerimaId + ")),112)>= " +
            "			Convert(varchar,FromPeriod,112) and Convert(varchar,(select top 1 TglPenerimaan from Invoice where id=(select MIN(InvoiceID) from TandaTerimaDetail where TandaTerimaID=" + TandaTerimaId + ")),112)<= " +
            "			Convert(varchar,ToPeriod,112)  "+
            "			order by id desc) else "+
            "				case when ISNULL((select top 1 KTPNama from KTP where CustomerType=2 and RowStatus>=0 and CustomerId=A.ID and  "+
            "				Convert(varchar,(select top 1 TglPenerimaan from Invoice where id=(select MIN(InvoiceID) from TandaTerimaDetail where TandaTerimaID=" + TandaTerimaId + ")),112)>= " +
            "				Convert(varchar,FromPeriod,112) and Convert(varchar,(select top 1 TglPenerimaan from Invoice where id=(select MIN(InvoiceID) from TandaTerimaDetail where TandaTerimaID=" + TandaTerimaId + ")),112)<= " +
            "				Convert(varchar,ToPeriod,112)  "+
            "				order by id desc),'')!='' "+
            "				then (select top 1 KTPNama from KTP where CustomerType=2 and RowStatus>=0 and CustomerId=A.ID and  "+
            "					Convert(varchar,(select top 1 TglPenerimaan from Invoice where id=(select MIN(InvoiceID) from TandaTerimaDetail where TandaTerimaID=" + TandaTerimaId + ")),112)>= " +
            "					Convert(varchar,FromPeriod,112) and Convert(varchar,(select top 1 TglPenerimaan from Invoice where id=(select MIN(InvoiceID) from TandaTerimaDetail where TandaTerimaID=" + TandaTerimaId + ")),112)<= " +
            "					Convert(varchar,ToPeriod,112)  "+
            "					order by id desc) else "+
            "					A.CustomerNameTax "+
            "				end "+
            "		end "+
            ") as CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten, "+
            "A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0)  "+
            "else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and  "+
            "A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson, "+
            "A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0)  "+
            "as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID from Customer as A,Zona as B, "+
            "City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID  "+
            "and A.SalesmanID = F.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectIdNpwpKtp(sqlDataReader);
                }
            }

            return new Customer();
        }
        public Customer RetrieveByIdKAT(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,0 as DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from CustomerKAT as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Customer();
        }
        public Customer RetrieveById3(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.CustomerType as JenisCustomer from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.ID = " + Id);

            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject3(sqlDataReader);
                }
            }

            return new Customer();
        }

        public Customer RetrieveByIdTax(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerNameTax as CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.ID = " + Id);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Customer();
        }

        public int CountCustomer()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT MAX(SUBSTRING(CustomerCode,2,4)) as id from Customer");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Id"]);
                }
            }

            return 0;
        }

        public decimal CekPromoCustProject(int custID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Disc1 from Customer,ItemPriceCategory where Customer.ItemPriceCategoryID=ItemPriceCategory.ID and Customer.id=" + custID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Disc1"]);
                }
            }

            return 0;
        }
        public Customer RetrieveByCode(string strCustomerCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.CustomerCode = '" + strCustomerCode + "'");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.CustomerCode = '" + strCustomerCode + "'");
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Customer();
        }
        public ArrayList RetrieveByCriteriaKAT(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,0 as DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from CustomerKAT as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCustomer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCustomer.Add(new Customer());

            return arrCustomer;
        }
        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCustomer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCustomer.Add(new Customer());

            return arrCustomer;
        }
        public ArrayList RetrieveByCriteria2(string strField, string strValue)
        {
            //Mailan minta rowstatus-1 juga dikeluarkan agar tahu HistoryEventArgs / bad debt
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and " + strField + " = '" + strValue + "'");
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and " + strField + " = '" + strValue + "'");

            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCustomer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCustomer.Add(new Customer());

            return arrCustomer;
        }

        public Customer RetrieveById2(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.DistributorCode from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.RowStatus = 0 and A.ID =  " + Id);
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new Customer();
        }

        public ArrayList RetrieveByCriteriaCustProj(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.CustomerType = 1 and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCustomer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCustomer.Add(new Customer());

            return arrCustomer;
        }

        public ArrayList RetrieveByCriteriaCustLP(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,A.DistAlmtID,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.CustomerType = 2 and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCustomer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCustomer.Add(new Customer());

            return arrCustomer;
        }

        public ArrayList RetrieveByCriteriaCustProjLP(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CustomerCode,A.CustomerName,A.Address,A.PropinsiID,E.NamaPropinsi,A.CityID,C.CityName,A.KabupatenID,D.NamaKabupaten,A.JoinDate,case when A.ID>0 then isnull((select A1.CreditLimit from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End CreditLimit,case when A.ID>0 then isnull((select A1.LamaPembayaran from CustomerCreditLimit as A1 where A1.CustomerID=A.ID and A1.RowStatus>-1),0) else 0 End LamaPembayaran,A.ZonaID,B.ZonaCode,A.NPWP,A.Telepon,A.TotalPoint,A.SalesmanID,F.SalesmanName,A.ContactPerson,A.LokasiPajak,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,isnull(A.Block,0) as Block,isnull(A.StatusCreditLimit,0) as StatusCreditLimit,A.CategoryPaymentID,A.CategoryCompanyID,A.Email,A.JmlHari,A.UangMukaPersen,A.DistributorCode,"+
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNama from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NamaTokoTax from Toko as v where v.ID=A.ID) end asNamaTax, " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPNomor from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NPWP from Toko as v where v.ID=A.ID) end asNPWPTax,  " +
                "case when isnull((select top 1 NPWPNomor from NPWP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 NPWPAlamat from NPWP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select AddressTax from Toko as v where v.ID=A.ID) end asAlamatTax, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNomor from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select NIK_KTP from Toko as v where v.ID=A.ID) end NIK_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPNama from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else (select Nama_KTP from Toko as v where v.ID=A.ID) end Nama_KTP, " +
                "case when isnull((select top 1 KTPNomor from KTP as v where v.CustomerType=2 and v.CustomerId=A.ID order by v.ID desc),'')!='' then (isnull((select top 1 KTPAlamat from KTP as x where x.CustomerType=2 and x.CustomerId=A.ID order by x.ID desc),'')) else '' end Alamat_KTP " +
                "from Customer as A,Zona as B,City as C,Kabupaten as D,Propinsi as E,Salesman as F where A.ZonaID = B.ID and A.CityID = C.ID and A.KabupatenID = D.ID and A.PropinsiID = E.ID and A.SalesmanID = F.ID and A.CustomerType not in(1,2) and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCustomer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCustomer.Add(new Customer());

            return arrCustomer;
        }

        //sampe sini tambahan 14 Des 15
        public ArrayList RetrieveBySalesAlk()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select a.id,(a.CustomerName +' '++'('+b.CityName+')') as customername,a.CustomerCode from customer a,city b where a.SalesmanID = 4 and a.CityID=b.id  and a.RowStatus =0 order by a.CustomerName asc");
            strError = dataAccess.Error;
            arrCustomer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCustomer.Add(GenerateObject4(sqlDataReader));
                }
            }
            else
                arrCustomer.Add(new Salesman());

            return arrCustomer;
        }

        public Customer GenerateObject4(SqlDataReader sqlDataReader)
        {
            objCustomer = new Customer();
            objCustomer.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objCustomer.CustomerCode = sqlDataReader["CustomerCode"].ToString();
            objCustomer.CustomerName = sqlDataReader["CustomerName"].ToString();
            return objCustomer;
        }

        public Customer GenerateObject2(SqlDataReader sqlDataReader)
        {
            objCustomer = new Customer();
            objCustomer.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objCustomer.CustomerCode = sqlDataReader["CustomerCode"].ToString();
            objCustomer.CustomerName = sqlDataReader["CustomerName"].ToString();
            objCustomer.Address = sqlDataReader["Address"].ToString();
            objCustomer.PropinsiID = Convert.ToInt32(sqlDataReader["PropinsiID"]);
            objCustomer.NamaPropinsi = sqlDataReader["NamaPropinsi"].ToString();
            objCustomer.CityID = Convert.ToInt32(sqlDataReader["CityID"]);
            objCustomer.CityName = sqlDataReader["CityName"].ToString();
            objCustomer.KabupatenID = Convert.ToInt32(sqlDataReader["KabupatenID"]);
            objCustomer.NamaKabupaten = sqlDataReader["NamaKabupaten"].ToString();
            objCustomer.JoinDate = Convert.ToDateTime(sqlDataReader["JoinDate"]);
            objCustomer.CreditLimit = Convert.ToDecimal(sqlDataReader["CreditLimit"]);
            objCustomer.ZonaID = Convert.ToInt32(sqlDataReader["ZonaID"]);
            objCustomer.ZonaCode = sqlDataReader["ZonaCode"].ToString();
            objCustomer.NPWP = sqlDataReader["NPWP"].ToString();
            objCustomer.Telepon = sqlDataReader["Telepon"].ToString();
            objCustomer.TotalPoint = Convert.ToDecimal(sqlDataReader["TotalPoint"]);
            objCustomer.SalesmanID = Convert.ToInt32(sqlDataReader["SalesmanID"]);
            objCustomer.SalesmanName = sqlDataReader["SalesmanName"].ToString();
            objCustomer.ContactPerson = sqlDataReader["ContactPerson"].ToString();
            objCustomer.LokasiPajak = sqlDataReader["LokasiPajak"].ToString();
            objCustomer.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objCustomer.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objCustomer.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objCustomer.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objCustomer.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objCustomer.DistributorCode = sqlDataReader["DistributorCode"].ToString();

            //if (string.IsNullOrEmpty(sqlDataReader["ItemPriceCategoryID"].ToString()))
            //    objCustomer.ItemPriceCategoryID = 0;
            //else
            //    objCustomer.ItemPriceCategoryID = Convert.ToInt32(sqlDataReader["ItemPriceCategoryID"]);

            return objCustomer;
        }

        public Customer GenerateObjectIdNpwpKtp(SqlDataReader sqlDataReader)
        {
            objCustomer = new Customer();
            objCustomer.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objCustomer.CustomerCode = sqlDataReader["CustomerCode"].ToString();
            objCustomer.CustomerName = sqlDataReader["CustomerName"].ToString();
            objCustomer.Address = sqlDataReader["Address"].ToString();
            objCustomer.PropinsiID = Convert.ToInt32(sqlDataReader["PropinsiID"]);
            objCustomer.NamaPropinsi = sqlDataReader["NamaPropinsi"].ToString();
            objCustomer.CityID = Convert.ToInt32(sqlDataReader["CityID"]);
            objCustomer.CityName = sqlDataReader["CityName"].ToString();
            objCustomer.KabupatenID = Convert.ToInt32(sqlDataReader["KabupatenID"]);
            objCustomer.NamaKabupaten = sqlDataReader["NamaKabupaten"].ToString();
            objCustomer.JoinDate = Convert.ToDateTime(sqlDataReader["JoinDate"]);
            objCustomer.CreditLimit = Convert.ToDecimal(sqlDataReader["CreditLimit"]);
            objCustomer.ZonaID = Convert.ToInt32(sqlDataReader["ZonaID"]);
            objCustomer.ZonaCode = sqlDataReader["ZonaCode"].ToString();
            objCustomer.NPWP = sqlDataReader["NPWP"].ToString();
            objCustomer.Telepon = sqlDataReader["Telepon"].ToString();
            objCustomer.TotalPoint = Convert.ToDecimal(sqlDataReader["TotalPoint"]);
            objCustomer.SalesmanID = Convert.ToInt32(sqlDataReader["SalesmanID"]);
            objCustomer.SalesmanName = sqlDataReader["SalesmanName"].ToString();
            objCustomer.ContactPerson = sqlDataReader["ContactPerson"].ToString();
            objCustomer.LokasiPajak = sqlDataReader["LokasiPajak"].ToString();
            objCustomer.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objCustomer.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objCustomer.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objCustomer.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objCustomer.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objCustomer.Block = Convert.ToInt32(sqlDataReader["Block"]);
            objCustomer.StatusCreditLimit = Convert.ToInt32(sqlDataReader["StatusCreditLimit"]);

            if (string.IsNullOrEmpty(sqlDataReader["CategoryPaymentID"].ToString()))
                objCustomer.CategoryPaymentID = 0;
            else
                objCustomer.CategoryPaymentID = Convert.ToInt32(sqlDataReader["CategoryPaymentID"]);

            if (string.IsNullOrEmpty(sqlDataReader["CategoryCompanyID"].ToString()))
                objCustomer.CategoryCompanyID = 0;
            else
                objCustomer.CategoryCompanyID = Convert.ToInt32(sqlDataReader["CategoryCompanyID"]);
            if (string.IsNullOrEmpty(sqlDataReader["Email"].ToString()))
                objCustomer.EmailAddress = string.Empty;
            else
                objCustomer.EmailAddress = sqlDataReader["Email"].ToString();

            objCustomer.JmlHari = Convert.ToInt32(sqlDataReader["JmlHari"]);
            objCustomer.UangMukaPersen = Convert.ToDecimal(sqlDataReader["UangMukaPersen"]);

            if (string.IsNullOrEmpty(sqlDataReader["DistributorCode"].ToString()))
                objCustomer.DistributorCode = "";
            else
                objCustomer.DistributorCode = sqlDataReader["DistributorCode"].ToString();

            if (string.IsNullOrEmpty(sqlDataReader["LamaPembayaran"].ToString()))
                objCustomer.LamaPembayaran = 0;
            else
                objCustomer.LamaPembayaran = Convert.ToInt32(sqlDataReader["LamaPembayaran"]);

            objCustomer.DistAlmtID = Convert.ToInt32(sqlDataReader["DistAlmtID"]);

            ////if (string.IsNullOrEmpty(sqlDataReader["ItemPriceCategoryID"].ToString()))
            ////    objCustomer.ItemPriceCategoryID = 0;
            ////else
            ////    objCustomer.ItemPriceCategoryID = Convert.ToInt32(sqlDataReader["ItemPriceCategoryID"]);

            return objCustomer;
        }

        public Customer GenerateObject(SqlDataReader sqlDataReader)
        {
            objCustomer = new Customer();
            objCustomer.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objCustomer.CustomerCode = sqlDataReader["CustomerCode"].ToString();
            objCustomer.CustomerName = sqlDataReader["CustomerName"].ToString();
            objCustomer.Address = sqlDataReader["Address"].ToString();
            objCustomer.PropinsiID = Convert.ToInt32(sqlDataReader["PropinsiID"]);
            objCustomer.NamaPropinsi = sqlDataReader["NamaPropinsi"].ToString();
            objCustomer.CityID = Convert.ToInt32(sqlDataReader["CityID"]);
            objCustomer.CityName = sqlDataReader["CityName"].ToString();
            objCustomer.KabupatenID = Convert.ToInt32(sqlDataReader["KabupatenID"]);
            objCustomer.NamaKabupaten = sqlDataReader["NamaKabupaten"].ToString();
            objCustomer.JoinDate = Convert.ToDateTime(sqlDataReader["JoinDate"]);
            objCustomer.CreditLimit = Convert.ToDecimal(sqlDataReader["CreditLimit"]);
            objCustomer.ZonaID = Convert.ToInt32(sqlDataReader["ZonaID"]);
            objCustomer.ZonaCode = sqlDataReader["ZonaCode"].ToString();
            objCustomer.NPWP = sqlDataReader["NPWP"].ToString();
            objCustomer.Telepon = sqlDataReader["Telepon"].ToString();
            objCustomer.TotalPoint = Convert.ToDecimal(sqlDataReader["TotalPoint"]);
            objCustomer.SalesmanID = Convert.ToInt32(sqlDataReader["SalesmanID"]);
            objCustomer.SalesmanName = sqlDataReader["SalesmanName"].ToString();
            objCustomer.ContactPerson = sqlDataReader["ContactPerson"].ToString();
            objCustomer.LokasiPajak = sqlDataReader["LokasiPajak"].ToString();
            objCustomer.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objCustomer.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objCustomer.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objCustomer.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objCustomer.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objCustomer.Block = Convert.ToInt32(sqlDataReader["Block"]);
            objCustomer.StatusCreditLimit = Convert.ToInt32(sqlDataReader["StatusCreditLimit"]);

            if (string.IsNullOrEmpty(sqlDataReader["CategoryPaymentID"].ToString()))
                objCustomer.CategoryPaymentID = 0;
            else
                objCustomer.CategoryPaymentID = Convert.ToInt32(sqlDataReader["CategoryPaymentID"]);

            if (string.IsNullOrEmpty(sqlDataReader["CategoryCompanyID"].ToString()))
                objCustomer.CategoryCompanyID = 0;
            else
                objCustomer.CategoryCompanyID = Convert.ToInt32(sqlDataReader["CategoryCompanyID"]);
            if (string.IsNullOrEmpty(sqlDataReader["Email"].ToString()))
                objCustomer.EmailAddress = string.Empty;
            else
                objCustomer.EmailAddress = sqlDataReader["Email"].ToString();

            objCustomer.JmlHari = Convert.ToInt32(sqlDataReader["JmlHari"]);
            objCustomer.UangMukaPersen = Convert.ToDecimal(sqlDataReader["UangMukaPersen"]);

            if (string.IsNullOrEmpty(sqlDataReader["DistributorCode"].ToString()))
                objCustomer.DistributorCode = "";
            else
                objCustomer.DistributorCode = sqlDataReader["DistributorCode"].ToString();

            if (string.IsNullOrEmpty(sqlDataReader["LamaPembayaran"].ToString()))
                objCustomer.LamaPembayaran = 0;
            else
                objCustomer.LamaPembayaran = Convert.ToInt32(sqlDataReader["LamaPembayaran"]);

            objCustomer.DistAlmtID = Convert.ToInt32(sqlDataReader["DistAlmtID"]);

            ////if (string.IsNullOrEmpty(sqlDataReader["ItemPriceCategoryID"].ToString()))
            ////    objCustomer.ItemPriceCategoryID = 0;
            ////else
            ////    objCustomer.ItemPriceCategoryID = Convert.ToInt32(sqlDataReader["ItemPriceCategoryID"]);

            objCustomer.AsNamaTax = sqlDataReader["AsNamaTax"].ToString();
            objCustomer.AsNPWPTax = sqlDataReader["AsNPWPTax"].ToString();
            objCustomer.AsAlamatTax = sqlDataReader["AsAlamatTax"].ToString();

            objCustomer.Ktp_Nik = sqlDataReader["NIK_KTP"].ToString();
            objCustomer.Ktp_Nama = sqlDataReader["Nama_KTP"].ToString();
            objCustomer.Ktp_Alamat = sqlDataReader["Alamat_KTP"].ToString();

            return objCustomer;
        }

        public Customer GenerateObject3(SqlDataReader sqlDataReader)
        {
            objCustomer = new Customer();
            objCustomer.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objCustomer.CustomerCode = sqlDataReader["CustomerCode"].ToString();
            objCustomer.CustomerName = sqlDataReader["CustomerName"].ToString();
            objCustomer.Address = sqlDataReader["Address"].ToString();
            objCustomer.PropinsiID = Convert.ToInt32(sqlDataReader["PropinsiID"]);
            objCustomer.NamaPropinsi = sqlDataReader["NamaPropinsi"].ToString();
            objCustomer.CityID = Convert.ToInt32(sqlDataReader["CityID"]);
            objCustomer.CityName = sqlDataReader["CityName"].ToString();
            objCustomer.KabupatenID = Convert.ToInt32(sqlDataReader["KabupatenID"]);
            objCustomer.NamaKabupaten = sqlDataReader["NamaKabupaten"].ToString();
            objCustomer.JoinDate = Convert.ToDateTime(sqlDataReader["JoinDate"]);
            objCustomer.CreditLimit = Convert.ToDecimal(sqlDataReader["CreditLimit"]);
            objCustomer.ZonaID = Convert.ToInt32(sqlDataReader["ZonaID"]);
            objCustomer.ZonaCode = sqlDataReader["ZonaCode"].ToString();
            objCustomer.NPWP = sqlDataReader["NPWP"].ToString();
            objCustomer.Telepon = sqlDataReader["Telepon"].ToString();
            objCustomer.TotalPoint = Convert.ToDecimal(sqlDataReader["TotalPoint"]);
            objCustomer.SalesmanID = Convert.ToInt32(sqlDataReader["SalesmanID"]);
            objCustomer.SalesmanName = sqlDataReader["SalesmanName"].ToString();
            objCustomer.ContactPerson = sqlDataReader["ContactPerson"].ToString();
            objCustomer.LokasiPajak = sqlDataReader["LokasiPajak"].ToString();
            objCustomer.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objCustomer.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objCustomer.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objCustomer.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objCustomer.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objCustomer.Block = Convert.ToInt32(sqlDataReader["Block"]);
            objCustomer.StatusCreditLimit = Convert.ToInt32(sqlDataReader["StatusCreditLimit"]);

            if (string.IsNullOrEmpty(sqlDataReader["CategoryPaymentID"].ToString()))
                objCustomer.CategoryPaymentID = 0;
            else
                objCustomer.CategoryPaymentID = Convert.ToInt32(sqlDataReader["CategoryPaymentID"]);

            if (string.IsNullOrEmpty(sqlDataReader["CategoryCompanyID"].ToString()))
                objCustomer.CategoryCompanyID = 0;
            else
                objCustomer.CategoryCompanyID = Convert.ToInt32(sqlDataReader["CategoryCompanyID"]);
            if (string.IsNullOrEmpty(sqlDataReader["Email"].ToString()))
                objCustomer.EmailAddress = string.Empty;
            else
                objCustomer.EmailAddress = sqlDataReader["Email"].ToString();

            objCustomer.JmlHari = Convert.ToInt32(sqlDataReader["JmlHari"]);
            objCustomer.UangMukaPersen = Convert.ToDecimal(sqlDataReader["UangMukaPersen"]);

            if (string.IsNullOrEmpty(sqlDataReader["DistributorCode"].ToString()))
                objCustomer.DistributorCode = "";
            else
                objCustomer.DistributorCode = sqlDataReader["DistributorCode"].ToString();

            if (string.IsNullOrEmpty(sqlDataReader["LamaPembayaran"].ToString()))
                objCustomer.LamaPembayaran = 0;
            else
                objCustomer.LamaPembayaran = Convert.ToInt32(sqlDataReader["LamaPembayaran"]);

            if (string.IsNullOrEmpty(sqlDataReader["JenisCustomer"].ToString()))
                objCustomer.JenisCustomer = 0;
            else
                objCustomer.JenisCustomer = Convert.ToInt32(sqlDataReader["JenisCustomer"]);

            return objCustomer;
        }
    }
}

