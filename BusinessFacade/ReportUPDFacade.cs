using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessFacade
{
    public class ReportUPDFacade
    { 
        //private ArrayList arrReport;

        public string ViewLapMasterDok(string Deptm, string pilihMaster, string DeptNama)
        {
            string Nama = string.Empty; string query = string.Empty;
            if (DeptNama == "Direksi")
            {
                Nama = "'Direksi' DeptNama";
            }
            else if (DeptNama == "Plant Manager")
            {
                Nama = "'Plant Manager' DeptNama";
            }
            else
            {
                Nama = "DeptNama";
            }

            if (Deptm == "23" || Deptm == "100" || Deptm == "101")
            {
                query = " where A.DeptF in (" + Deptm + ") ";
            }
            else
            {
                query = " where A.DeptF in (" + Deptm + ") and A.DeptID="+ Deptm +" ";
            }

            string strSQL;
            //strSQL = "select B.NoDocument,B.DocName,B.RevisiNo,CONVERT(VARCHAR(9), B.tglberlaku, 6) as TglBerlaku, C.namaDept as DeptNama,CONVERT(VARCHAR(9), D.Tanggal, 6) as Tanggal,D.RevNo as RevNo " +
            //         ", D.FormNO " +
            //        "from ISO_UPDrelasi as A,ISO_UpdDMD as B,ISO_UPDDept as C,ISO_updMasterupdate as D where A.idMaster=B.ID  " +
            //        "and A.DeptF=C.deptIDalias and D.idCategory=A.CategoryUPD and A.CategoryUPD=" + pilihMaster + " and A.DeptF= " + Deptm + " and B.rowstatus > -1 order by A.urutan,A.DeptID,B.NoDocument asc  ";
            //return strSQL;

            strSQL =
            " select NoDocument,DocName,RevisiNo,TglBerlaku," + Nama + ",Tanggal,FormNO,RevNo from ( " +
            " select NoDocument,DocName,RevisiNo,left(convert(char,TglBerlaku,106),11)TglBerlaku,DeptName DeptNama,left(convert(char,a.Tanggal,106),11)Tanggal,a.FormNO,a.RevNo,IDPemilik from ( " +
            " select case when CategoryUPD='Instruksi Kerja' then SUBSTRING(NoDocument,4,3)+SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2) " +
            " when CategoryUPD='Bagan Alir' then SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2) " +
            " when CategoryUPD is NULL then NoDocument else '' end NoUrut,* " +
            " from (  select TRIM(xx.NoDocument) AS NoDocument,xx.DocName,xx.RevisiNo, " +
            " isnull(xx.TglBerlaku,0) as TglBerlaku,isnull(C.CreatedTime,0) as TglDistribusi  , " +
            " case when C.FileName is null then '-'  else C.[FileName] end [FileName],D.namaDept as DeptName, " +
            " (select DocCategory from ISO_UpdDocCategory as E  where E.ID=xx.CategoryUPD  and E.RowStatus > -1) as CategoryUPD, " +
            " isnull(C.ID,0) as ID,xx.DeptID,IDm,CatUPD,IDPemilik from " +
            " (select B.NoDocument,B.DocName,B.RevisiNo,B.TglBerlaku,B.ID as IDm, A.DeptF,A.CategoryUPD,A.Urutan,A.rowstatus,B.Dept DeptID, " +
            " B.CategoryUPD CatUPD,B.Dept IDPemilik   from ISO_UPDrelasi as A INNER JOIN ISO_UpdDMD as B ON A.idMaster=B.ID  "+
            //" where DeptF in (" + Deptm + ") " +
            " " + query + " " +
            " and A.CategoryUPD=" + pilihMaster + "  and B.Aktif>0) as xx " +
            " LEFT JOIN ISO_UPDdistribusiFile as C ON xx.IDm=C.idMaster " +
            " LEFT JOIN ISO_UPDDept as D ON xx.DeptF=D.deptIDalias " +
            " where xx.rowstatus > -1 and D.RowStatus > -1 and C.RowStatus>-1) as DataList) as x1 " +
            " left join ISO_updMasterupdate a ON a.idCategory=x1.CatUPD ) as b order by IDPemilik,NoDocument,DocName ";

            return strSQL;

        }

        public string ViewReportUPD(string periodeAwal, string periodeAkhir, int DeptID)
        {
            string userapvUPD = string.Empty;
            switch (DeptID)
            {
                case 23:
                    userapvUPD = "";
                    break;
                default:
                    userapvUPD = "and A.DeptID in (" + DeptID + ")";
                    break;
            }
            string strSQL;
            //strSQL = " select " +
            //         " UpdName,C.Alasan, " +
            //         " (select substring(DocTypeName,8,30) from ISO_UpdDocType as D where D.ID=A.JenisUPD and D.RowStatus>-1)'Jenis Dokumen', " +
            //         " (select DocCategory from ISO_UpdDocCategory as C where C.ID=A.CategoryUPD and C.RowStatus>-1)Kategori, " +
            //         " B.namaDept2 DeptName,LEFT(CONVERT(CHAR,A.TglPengajuan,106),11) as 'TglPengajuan' , "+
            //         " CASE WHEN A.CreatedTime=A.LastModifiedTime THEN '-' ELSE LEFT(CONVERT(CHAR,A.LastModifiedTime,106),11) END 'TglUpdate', "+
            //         " case " +
            //         " when (A.RowStatus=0 and A.Status=0 and A.apv=0 ) then 'Open' " +
            //         " when (A.RowStatus=0 and A.Status=0 and A.Apv=1 and A.DeptID not in (14,23,7,15,13,26))  then 'Apv Manager' " +
            //         " when (A.RowStatus=0 and A.Status=0 and A.Apv=1 and A.DeptID in (14,23,7,15,13,26))  then 'Apv Head' " +
            //         " when (A.RowStatus=0 and A.Status=0 and A.Apv=2 and A.DeptID not in (14,23,7,15,13,26))  then 'Apv PM' " +
            //         " when (A.RowStatus=0 and A.Status=0 and A.Apv=2 and A.DeptID in (14,23,7,15,13,26))  then 'Apv Mgr Corp' " +
            //         " when (A.RowStatus=0 and A.Status=0 and A.Apv=3 and A.Type=2)  then 'Apv Mgr HRD' " +
            //         " when (A.RowStatus=0 and A.Status=0 and A.Apv=3 and A.Type=3)  then 'Apv Mgr Log' " +
            //         " when (A.RowStatus=0 and A.Status=0 and A.Apv=4)  then 'Apv Head ISO' " +
            //         " when (A.RowStatus=0 and A.Status=0 and A.Apv=5)  then 'Apv Mgr ISO' " +
            //         " when (A.RowStatus=-2 and A.Status=0 and A.Apv=0)  then 'Cancel' " +
            //         " when (A.RowStatus=-3 and A.Status=0)  then 'Not Approved'  " +
            //         " when (A.RowStatus=0 and A.Status=1 and A.Apv=6)  then 'Aktif' " +
            //         " end Keterangan, " +
            //         " A.LastModifiedBy as HeadName  from ISO_Upd as A,ISO_UPDDept as B, ISO_UpdDetail as C  " +
            //         " where A.ID=C.UPDid and A.DeptID=B.deptID  and  A.TglPengajuan >= '" + periodeAwal + " ' and  A.TglPengajuan <= '" + periodeAkhir + "'" +
            //         " " + userapvUPD + " " +
            //         " and B.RowStatus>-1   order by B.deptID,A.jenisupd,A.categoryUPD,A.TglPengajuan ";
            strSQL = " select C.NoDokumen," +
                     " UpdName,C.Alasan, " +
                     " (select substring(DocTypeName,8,30) from ISO_UpdDocType as D where D.ID=A.JenisUPD and D.RowStatus>-1)'Jenis Dokumen', " +
                     " (select DocCategory from ISO_UpdDocCategory as C where C.ID=A.CategoryUPD and C.RowStatus>-1)Kategori, " +
                     " B.namaDept2 DeptName, " +
                     " LEFT(CONVERT(CHAR,A.CreatedTime,106),11) as 'TglPengajuan' , " +
                     " CASE " +
                     " WHEN A.CreatedTime=A.LastModifiedTime THEN 'Create :' + LEFT(CONVERT(CHAR,A.LastModifiedTime,106),11) + ' : ' + LEFT(CONVERT(CHAR,A.LastModifiedTime,108),11)+' [ ' +A.LastModifiedBy+' ]' " +
                     " WHEN (A.CreatedTime < A.LastModifiedTime and A.RowStatus=-2)THEN 'Cancel : ' + LEFT(CONVERT(CHAR,A.LastModifiedTime,106),11) + '  ' + LEFT(CONVERT(CHAR,A.LastModifiedTime,108),11)+' [ ' + A.LastModifiedBy+' ]' " +
                     " WHEN (A.CreatedTime < A.LastModifiedTime and A.RowStatus=-3)THEN 'Not Apv : ' + LEFT(CONVERT(CHAR,A.LastModifiedTime,106),11) + '  ' + LEFT(CONVERT(CHAR,A.LastModifiedTime,108),11) " +
                     " ELSE 'Apv ' + LEFT(CONVERT(CHAR,A.LastModifiedTime,106),11) + '  ' + LEFT(CONVERT(CHAR,A.LastModifiedTime,108),11) END 'TglUpdate', " +
                     
                     " CASE " +
                     " WHEN (A.RowStatus=0 and A.Status=0 and A.apv=0 ) then 'Open ' " +
                     " when (A.RowStatus=0 and A.Status=0 and A.Apv=1 and A.DeptID not in (14,23,7,15,13,26,22,24,12))  then 'Apv Manager' " +
                     " when (A.RowStatus=0 and A.Status=0 and A.Apv=1 and A.DeptID in (14,23,7,15,13,26,22,24,12))  then 'Open' " +
                     " when (A.RowStatus=0 and A.Status=0 and A.Apv=2 and A.DeptID not in (14,23,7,15,13,26,22,24,12))  then 'Apv PM' " +
                     " when (A.RowStatus=0 and A.Status=0 and A.Apv=2 and A.DeptID in (14,23,7,15,13,26,22,24,12)) then 'Apv Mgr Corp' " +
                     " when (A.RowStatus=0 and A.Apv=3 and A.Status=1)  then 'Verifikasi Sek. ISO' " +                                    
                     " when (A.RowStatus=0 and A.Apv=4)  then 'Apv Mgr ISO' " +   
                     
                     " when (A.RowStatus=0 and A.Apv=5 and A.ID in (select idUPD from ISO_UpdDMD where Aktif=1 and RowStatus>-1))  then 'Actived'  "+ 
                     " when (A.RowStatus=0 and A.Apv=5 and A.ID in (select idUPD from ISO_UpdDMD where Aktif in (0,2)and RowStatus>-1))  then 'TerDistribusi' "+
                     " when (A.RowStatus=0 and A.Apv=5 and A.JenisUPD=3 and A.IDmaster in (select ID from ISO_UpdDMD where Aktif in (-2)and RowStatus>-1))  then 'Non Aktif' "+

                     //" when (A.RowStatus=0 and A.Apv=5)  then 'TerDistribusi' " +
                     " when (A.RowStatus=-2 and A.Status=0 and A.Apv=0)  then 'Cancel' " +
                     " when (A.RowStatus=-3 and A.Status=0)  then 'Not Approved' end Keterangan, " +

                     //" CASE "+
                //" WHEN A.type in (1,2,3) and  (A.Apv=0 and A.RowStatus=0 and A.Status=0) then  (select E.UserName from ISO_UPDListApv as D,Users as E where D.Apv1=E.ID and D.UserID=C.UserID and D.RowStatus>-1)+' : ' +CONVERT(char,A.apv) " +
                //" WHEN A.type in (1,2,3) and (A.Apv=1 and A.RowStatus=0 and A.Status=0) then  (select E.UserName from ISO_UPDListApv as D,Users as E where D.Apv2=E.ID and D.UserID=C.UserID and D.RowStatus>-1)+' : ' +CONVERT(char,A.apv) " +
                //" WHEN (A.type=1 and A.Apv=2 and A.RowStatus=0 and A.Status=0) then  (select E.UserName from ISO_UPDListApv as D,Users as E where D.Apv4=E.ID and D.UserID=C.UserID and D.RowStatus>-1)+' : ' +CONVERT(char,A.apv) "+
                //" WHEN (A.type=2 and A.Apv=2 and A.RowStatus=0 and A.Status=0) then  (select E.UserName from ISO_UPDListApv as D,Users as E where D.Apv3=E.ID and D.UserID=C.UserID and D.RowStatus>-1)+' : ' +CONVERT(char,A.apv) " +
                //" WHEN (A.type=3 and A.Apv=2 and A.RowStatus=0 and A.Status=0) then  (select E.UserName from ISO_UPDListApv as D,Users as E where D.Apv3=E.ID and D.UserID=C.UserID and D.RowStatus>-1)+' : ' +CONVERT(char,A.apv) " +
                //" WHEN A.type in (1,2,3) and (A.Apv=4 and A.RowStatus=0 and A.Status=0) then  (select E.UserName from ISO_UPDListApv as D,Users as E where D.Apv5=E.ID and D.UserID=C.UserID and D.RowStatus>-1)+' : ' +CONVERT(char,A.apv) " +
                //" WHEN A.type in (1,2,3) and (A.Apv=5 and A.RowStatus=0 and A.Status=0) then  'Skretariat ISO : Aktifasi' " +
                //" WHEN A.type in (1,2,3) and (A.Apv=6 and A.RowStatus=0 and A.Status=1) then  'Selesai' " +   
                //" else '-' end NextAproved "+

                     " CASE " +
                     " WHEN A.type in (1,2,3) and  (A.Apv=0 and A.RowStatus=0 and A.Status=0) then (select top 1 E.UserName from ISO_UPDListApv as D,Users as E "+                     
                     " where D.Apv1=E.ID and D.UserID=C.UserID and D.RowStatus>-1) " +
                     " WHEN (A.Apv=1 and A.RowStatus=0 and A.Status=0 and A.DeptID not in (7,14,15,23,26)) then  'PM' " +
                     " WHEN (A.Apv=1 and A.RowStatus=0 and A.Status=0 and A.DeptID in (7,14,15,23,26,22,24)) then  'Corp. Mgr' " +
                     " WHEN (A.type=1 and A.Apv=2 and A.RowStatus=0 and A.Status=0) then  'Sekretariat ISO' " +
                     " WHEN (A.Apv=3 and A.RowStatus=0 and A.Status=1) then  'Mgr ISO'  " +
                     " WHEN (A.Apv=4 and A.RowStatus=0 and A.Status=1) then  'Distribusi' " +

                     " when (A.RowStatus=0 and A.Apv=5 and A.ID in (select idUPD from ISO_UpdDMD where Aktif=1 and RowStatus>-1))  then 'Sekretariat ISO' "+
                     " when (A.RowStatus=0 and A.Apv=5 and A.ID in (select idUPD from ISO_UpdDMD where Aktif in (0,2)and RowStatus>-1))  then 'Finished' "+
                     " when (A.RowStatus=0 and A.Apv=5 and A.JenisUPD=3 and A.IDmaster in (select ID from ISO_UpdDMD where Aktif in (-2)and RowStatus>-1))  then 'Finished' "+

                     " WHEN (A.type=2 and A.Apv=2 and A.RowStatus=0 and A.Status=0 and A.CreatedBy<>'bastari') then  'Corp. HRD' " +
                     " WHEN (A.type=2 and A.Apv=2 and A.RowStatus=0 and A.Status=0 and A.CreatedBy='bastari') then  'Sekretariat ISO' " +
                     " else 'Mgr ISO' end NextAproved "+

                     " from ISO_Upd as A,ISO_UPDDept as B, ISO_UpdDetail as C  " +
                     " where A.ID=C.UPDid and A.DeptID=B.deptID  and  A.TglPengajuan >= '" + periodeAwal + " ' and  A.TglPengajuan <= '" + periodeAkhir + "'" +
                     " " + userapvUPD + " " +
                     " and B.RowStatus>-1  and A.RowStatus> -1  order by B.deptID,A.jenisupd,A.categoryUPD,A.TglPengajuan ";


            return strSQL;
        }
    }
}

