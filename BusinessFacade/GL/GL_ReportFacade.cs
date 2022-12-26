using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessFacade
{
    public class GL_ReportFacade
    {
        public string ViewPrintJurnal(int JurHeadNum, int jurdetnum)
        {
            string strquery=string.Empty;
            if (jurdetnum==0)
            {
                strquery = "Select B.JurHeadNum,B.ChartNo, A.ChartName, B.Description, B.IDRAmount, C.JurNo, C.JurDate,  C.JurDesc, B.seqnum, C.VoucherCode  " +
                       "from (GL_ChartOfAccount A INNER JOIN GL_JurDet B ON A.ChartNo = B.ChartNo) INNER JOIN GL_JurHead C ON B.JurHeadNum = C.JurHeadNum  " +
                       "where B.JurHeadNum=" + JurHeadNum + " order by B.chartno";
            }
            else
            {
                strquery = "Select B.JurHeadNum,B.ChartNo, A.ChartName, B.Description, B.IDRAmount, C.JurNo, C.JurDate,  C.JurDesc, B.seqnum, C.VoucherCode  " +
                       "from (GL_ChartOfAccount A INNER JOIN GL_JurDet B ON A.ChartNo = B.ChartNo) INNER JOIN GL_JurHead C ON B.JurHeadNum = C.JurHeadNum  " +
                       "where B.JurHeadNum=" + JurHeadNum + " and jurdetnum not in(" + jurdetnum + " ) order by B.chartno";
            }
            return strquery;
        }
    }
}
