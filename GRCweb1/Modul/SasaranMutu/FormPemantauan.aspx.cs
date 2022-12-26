using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Domain;
using BusinessFacade;
using System.Collections;
using DataAccessLayer;
using System.Text;
using BusinessFacade.GL;


namespace GRCweb1.Modul.SasaranMutu
{
    public partial class FormPemantauan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Users users = (Users)Session["Users"];
                ViewState["PlantID"] = users.UnitKerjaID;

                //LoadCtrpDiv1();
                //LoadKrwgDiv1();

                //typeSarmutNya 0 = SarMut dan 1 = Pemantauan SarMut
                //LoadSarMut(16,0);

                //LoadSarMut(21, 1);
                //LoadSarPantau(21,1);

                DisplayLabel(21,1);
            }
            //else
            //    divOutputProduksi1.Attributes["class"] = "widget-box widget-color-orange collapsed";
        }

        #region gak pake
        //private void LoadSarMut(int sarMutDeptID, int typeSarMutNya)
        //{
        //    //divHeader ada 20 dan masih ada 2 plant
        //    Control[] divHeader = { header1,header2, header3, header4, header5, header6, header7, header8, header9, header10,
        //                            header11,header12,header13,header13,header14,header15,header16,header17,header18,header19,header20,
        //                            header21,header22,header23};

        //    //hrs berurutan ribetnya
        //    Repeater[] repeatTable = { Repeater1, Repeater2, Repeater3, Repeater4, Repeater5, Repeater6, Repeater7, Repeater8, Repeater9, Repeater10,
        //                              Repeater11, Repeater12, Repeater13, Repeater14, Repeater15, Repeater16, Repeater17, Repeater18, Repeater19, Repeater20,
        //                              Repeater21, Repeater22, Repeater23, Repeater24, Repeater25, Repeater26, Repeater27, Repeater28, Repeater29, Repeater30,
        //                              Repeater31, Repeater32, Repeater33, Repeater34, Repeater35, Repeater36, Repeater37, Repeater38, Repeater39, Repeater40,
        //                              Repeater41, Repeater42, Repeater43, Repeater44, Repeater45, Repeater46};

        //    //nanti bikin loop all aja dr sini

        //    int intDivHtmlNo = 0;
        //    int intDivHeaderNo = 0;
        //    int jmlColumn = 20;
        //    int divGridView = 39; //nanti pake for loop ya

        //    DataTable dt = new DataTable();

        //    for (int aa = 1; aa <= 20; aa++)
        //    {
        //        string strColumn = "Ctrp" + divGridView.ToString() + "aCol" + aa.ToString();

        //        DataColumn col = new DataColumn();
        //        {
        //            col.Caption = "Name";
        //            col.ColumnName= "Ctrp" + divGridView.ToString() + "aCol" + aa.ToString();
        //            col.DataType = typeof(string);
        //            dt.Columns.Add(col);
        //        }
        //    }
        //    dt.Rows.Clear();
        //    SarMutPencapaianNilaiFacade pencapaianNilaiFacade = new SarMutPencapaianNilaiFacade();
        //    ArrayList arrOutputProduksiCtrp = pencapaianNilaiFacade.PencapaianSarMut( int.Parse(selectTahun.Value.ToString()), sarMutDeptID, typeSarMutNya);

        //    foreach (SarMutPencapaianNilai nilai in arrOutputProduksiCtrp)
        //    {
        //        string strSmt1LY = string.Empty; string strSmt2LY = string.Empty; string strJan = string.Empty; string strFeb = string.Empty; string strMar = string.Empty; string strApr = string.Empty;
        //        string strMei = string.Empty; string strJun = string.Empty; string strJul = string.Empty; string strAgu = string.Empty; string strSep = string.Empty;
        //        string strOkt = string.Empty; string strNov = string.Empty; string strDes = string.Empty; string strSmt1TahunIni = string.Empty; string strSmt2TahunIni = string.Empty;

        //        if (isNumeric(nilai.Jan) && isNumeric(nilai.Feb) && isNumeric(nilai.Mar) && isNumeric(nilai.Apr) && isNumeric(nilai.Mei) && isNumeric(nilai.Jun) &&
        //            isNumeric(nilai.Jul) && isNumeric(nilai.Agu) && isNumeric(nilai.Sep) && isNumeric(nilai.Oct) && isNumeric(nilai.Nov) && isNumeric(nilai.Des) &&
        //            isNumeric(nilai.Smt1LY) && isNumeric(nilai.Smt2LY) && isNumeric(nilai.Smt1TahunIni) && isNumeric(nilai.Smt2TahunIni))
        //        {
        //            if (string.IsNullOrEmpty(nilai.Smt1LY) || (Decimal.Parse(nilai.Smt1LY) == 0 && nilai.Header == 0)) strSmt1LY = ""; else strSmt1LY = nilai.Smt1LY;
        //            if (string.IsNullOrEmpty(nilai.Smt2LY) || (Decimal.Parse(nilai.Smt2LY) == 0 && nilai.Header == 0)) strSmt2LY = ""; else strSmt2LY = nilai.Smt2LY;
        //            if (string.IsNullOrEmpty(nilai.Jan) || (Decimal.Parse(nilai.Jan) == 0 && nilai.Header == 0)) strJan = ""; else strJan = nilai.Jan;
        //            if (string.IsNullOrEmpty(nilai.Feb) || (Decimal.Parse(nilai.Feb) == 0 && nilai.Header == 0)) strFeb = ""; else strFeb = nilai.Feb;
        //            if (string.IsNullOrEmpty(nilai.Mar) || (Decimal.Parse(nilai.Mar) == 0 && nilai.Header == 0)) strMar = ""; else strMar = nilai.Mar;
        //            if (string.IsNullOrEmpty(nilai.Apr) || (Decimal.Parse(nilai.Apr) == 0 && nilai.Header == 0)) strApr = ""; else strApr = nilai.Apr;
        //            if (string.IsNullOrEmpty(nilai.Mei) || (Decimal.Parse(nilai.Mei) == 0 && nilai.Header == 0)) strMei = ""; else strMei = nilai.Mei;
        //            if (string.IsNullOrEmpty(nilai.Jun) || (Decimal.Parse(nilai.Jul) == 0 && nilai.Header == 0)) strJun = ""; else strJun = nilai.Jun;
        //            if (string.IsNullOrEmpty(nilai.Jul) || (Decimal.Parse(nilai.Jul) == 0 && nilai.Header == 0)) strJul = ""; else strJul = nilai.Jul;
        //            if (string.IsNullOrEmpty(nilai.Agu) || (Decimal.Parse(nilai.Agu) == 0 && nilai.Header == 0)) strAgu = ""; else strAgu = nilai.Agu;
        //            if (string.IsNullOrEmpty(nilai.Sep) || (Decimal.Parse(nilai.Sep) == 0 && nilai.Header == 0)) strSep = ""; else strSep = nilai.Sep;
        //            if (string.IsNullOrEmpty(nilai.Oct) || (Decimal.Parse(nilai.Oct) == 0 && nilai.Header == 0)) strOkt = ""; else strOkt = nilai.Oct;
        //            if (string.IsNullOrEmpty(nilai.Nov) || (Decimal.Parse(nilai.Nov) == 0 && nilai.Header == 0)) strNov = ""; else strNov = nilai.Nov;
        //            if (string.IsNullOrEmpty(nilai.Des) || (Decimal.Parse(nilai.Des) == 0 && nilai.Header == 0)) strDes = ""; else strDes = nilai.Des;
        //            if (string.IsNullOrEmpty(nilai.Smt1TahunIni) || (Decimal.Parse(nilai.Smt1TahunIni) == 0 && nilai.Header == 0)) strSmt1TahunIni = ""; else strSmt1TahunIni = nilai.Smt1TahunIni;
        //            if (string.IsNullOrEmpty(nilai.Smt2TahunIni) || (Decimal.Parse(nilai.Smt2TahunIni) == 0 && nilai.Header == 0)) strSmt2TahunIni = ""; else strSmt2TahunIni = nilai.Smt2TahunIni;
        //        }
        //        else
        //        {
        //            if (isNumeric(nilai.Smt1LY))
        //            {
        //                if (Decimal.Parse(nilai.Smt1LY) == 0) strSmt1LY = ""; else strSmt1LY = nilai.Smt1LY;
        //            }
        //            else strSmt1LY = nilai.Smt1LY;
        //            if (isNumeric(nilai.Smt2LY))
        //            {
        //                if (Decimal.Parse(nilai.Smt2LY) == 0) strSmt2LY = ""; else strSmt2LY = nilai.Smt2LY;
        //            }
        //            else strSmt2LY = nilai.Smt2LY;
        //            if (isNumeric(nilai.Smt1TahunIni))
        //            {
        //                if (Decimal.Parse(nilai.Smt1TahunIni) == 0) strSmt1TahunIni = ""; else strSmt1TahunIni = nilai.Smt1TahunIni;
        //            }
        //            else strSmt1TahunIni = nilai.Smt1TahunIni;
        //            if (isNumeric(nilai.Smt2TahunIni))
        //            {
        //                if (Decimal.Parse(nilai.Smt2TahunIni) == 0) strSmt2TahunIni = ""; else strSmt2TahunIni = nilai.Smt2TahunIni;
        //            }
        //            else strSmt2TahunIni = nilai.Smt2TahunIni;
        //            if (isNumeric(nilai.Jan))
        //            {
        //                if (Decimal.Parse(nilai.Jan) == 0) strJan = ""; else strJan = nilai.Jan;
        //            }
        //            else strJan = nilai.Jan;
        //            if (isNumeric(nilai.Feb))
        //            {
        //                if (Decimal.Parse(nilai.Feb) == 0) strFeb = ""; else strFeb = nilai.Feb;
        //            }
        //            else strFeb = nilai.Feb;
        //            if (isNumeric(nilai.Mar))
        //            {
        //                if (Decimal.Parse(nilai.Mar) == 0) strMar = ""; else strMar = nilai.Mar;
        //            }
        //            else strMar = nilai.Mar;
        //            if (isNumeric(nilai.Apr))
        //            {
        //                if (Decimal.Parse(nilai.Apr) == 0) strApr = ""; else strApr = nilai.Apr;
        //            }
        //            else strApr = nilai.Apr;
        //            if (isNumeric(nilai.Mei))
        //            {
        //                if (Decimal.Parse(nilai.Mei) == 0) strMei = ""; else strMei = nilai.Mei;
        //            }
        //            else strMei = nilai.Mei;
        //            if (isNumeric(nilai.Jun))
        //            {
        //                if (Decimal.Parse(nilai.Jun) == 0) strJun = ""; else strJun = nilai.Jun;
        //            }
        //            else strJun = nilai.Jun;
        //            if (isNumeric(nilai.Jul))
        //            {
        //                if (Decimal.Parse(nilai.Jul) == 0) strJul = ""; else strJul = nilai.Jul;
        //            }
        //            else strJul = nilai.Jul;
        //            if (isNumeric(nilai.Agu))
        //            {
        //                if (Decimal.Parse(nilai.Agu) == 0) strAgu = ""; else strAgu = nilai.Agu;
        //            }
        //            else strAgu = nilai.Agu;
        //            if (isNumeric(nilai.Sep))
        //            {
        //                if (Decimal.Parse(nilai.Sep) == 0) strSep = ""; else strSep = nilai.Sep;
        //            }
        //            else strSep = nilai.Sep;
        //            if (isNumeric(nilai.Oct))
        //            {
        //                if (Decimal.Parse(nilai.Oct) == 0) strOkt = ""; else strOkt = nilai.Oct;
        //            }
        //            else strOkt = nilai.Oct;
        //            if (isNumeric(nilai.Nov))
        //            {
        //                if (Decimal.Parse(nilai.Nov) == 0) strNov = ""; else strNov = nilai.Nov;
        //            }
        //            else strNov = nilai.Nov;
        //            if (isNumeric(nilai.Des))
        //            {
        //                if (Decimal.Parse(nilai.Des) == 0) strDes = ""; else strDes = nilai.Des;
        //            }
        //            else strDes = nilai.Des;
        //        }

        //        //dt.Rows.Add(nilai.DeptName, nilai.SarMutDepartment, nilai.ParameterTerukur, nilai.Smt1LY, nilai.Smt2LY, nilai.Tahun, nilai.Jan, nilai.Feb, nilai.Mar, nilai.Apr, nilai.Mei, nilai.Jun, nilai.Smt1TahunIni, nilai.Jul, nilai.Agu, nilai.Sep, nilai.Oct, nilai.Nov, nilai.Des, nilai.Smt2TahunIni);
        //        dt.Rows.Add(nilai.DeptName, nilai.SarMutDepartment, nilai.ParameterTerukur, strSmt1LY, strSmt2LY, nilai.Tahun, strJan, strFeb, strMar, strApr, strMei, strJun, strSmt1TahunIni, strJul, strAgu, strSep, strOkt, strNov, strDes, strSmt2TahunIni);

        //        intDivHtmlNo = nilai.DivHtmlNo;
        //        intDivHeaderNo = nilai.DivHtmlNo;
        //    }

        //    //Repeater39.DataSource = dt;
        //    //Repeater39.DataBind();

        //    if (intDivHtmlNo>0)
        //    {
        //        intDivHtmlNo = (intDivHtmlNo * 2) - 2;

        //        //repeatTable[intDivHtmlNo+18].DataSource = dt;
        //        //repeatTable[intDivHtmlNo+18].DataBind();
        //        repeatTable[intDivHtmlNo].DataSource = dt;
        //        repeatTable[intDivHtmlNo].DataBind();

        //        divHeader[intDivHeaderNo].Visible = true;
        //    }

        //}
        //private void LoadSarPantau(int sarMutDeptID, int typeSarmutNya)
        //{
        //    //divHeader ada 20 dan masih ada 2 plant
        //    Control[] divHeader = { header1,header2, header3, header4, header5, header6, header7, header8, header9, header10,
        //                            header11,header12,header13,header13,header14,header15,header16,header17,header18,header19,header20,
        //                            header21,header22,header23};

        //    //nanti bikin loop all aja dr sini

        //    int intDivHtmlNo = 0;
        //    int jmlColumn = 20;
        //    int divGridView = 1; //nanti pake for loop ya

        //    DataTable dt = new DataTable();

        //    for (int aa = 1; aa <= 7; aa++)
        //    {
        //        //li1Ctrp1
        //        string strColumn = "li"+ divGridView.ToString() + "Ctrp" +  aa.ToString();

        //        DataColumn col = new DataColumn();
        //        {
        //            col.Caption = "Name";
        //            col.ColumnName = "li" + divGridView.ToString() + "Ctrp" + aa.ToString();
        //            col.DataType = typeof(string);
        //            dt.Columns.Add(col);
        //        }
        //    }

        //    dt.Rows.Clear();
        //    SarMutPencapaianNilaiFacade pencapaianNilaiFacade = new SarMutPencapaianNilaiFacade();
        //    ArrayList arrOutputProduksiCtrp = pencapaianNilaiFacade.PencapaianSarMut(int.Parse(selectTahun.Value.ToString()), sarMutDeptID, typeSarmutNya);

        //    foreach (SarMutPencapaianNilai nilai in arrOutputProduksiCtrp)
        //    {
        //        dt.Rows.Add(nilai.DeptName, nilai.SarMutDepartment, nilai.ParameterTerukur, nilai.Smt1LY, nilai.Smt2LY, nilai.Tahun, nilai.Jan, nilai.Feb, nilai.Mar, nilai.Apr, nilai.Mei, nilai.Jun, nilai.Smt1TahunIni, nilai.Jul, nilai.Agu, nilai.Sep, nilai.Oct, nilai.Nov, nilai.Des, nilai.Smt2TahunIni);

        //        intDivHtmlNo = nilai.DivHtmlNo;
        //    }

        //    //dt.Rows.Add("999");

        //    //Repeater48.DataSource = dt;
        //    //Repeater48.DataBind();
        //    //Repeater48.Visible = true;

        //    if (intDivHtmlNo > 0)
        //    {
        //        //repeatTable[intDivHtmlNo + 18].DataSource = dt;
        //        //repeatTable[intDivHtmlNo + 18].DataBind();

        //        divHeader[intDivHtmlNo].Visible = true;
        //    }

        //}
        //private void LoadCtrpDiv1()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[20] {
        //        new DataColumn("Ctrp1aCol1", typeof(string)),
        //        new DataColumn("Ctrp1aCol2", typeof(string)),
        //        new DataColumn("Ctrp1aCol3", typeof(string)),
        //        new DataColumn("Ctrp1aCol4", typeof(string)),
        //        new DataColumn("Ctrp1aCol5", typeof(string)),
        //        new DataColumn("Ctrp1aCol6", typeof(string)),
        //        new DataColumn("Ctrp1aCol7", typeof(string)),
        //        new DataColumn("Ctrp1aCol8", typeof(string)),
        //        new DataColumn("Ctrp1aCol9", typeof(string)),
        //        new DataColumn("Ctrp1aCol10", typeof(string)),
        //        new DataColumn("Ctrp1aCol11", typeof(string)),
        //        new DataColumn("Ctrp1aCol12", typeof(string)),
        //        new DataColumn("Ctrp1aCol13", typeof(string)),
        //        new DataColumn("Ctrp1aCol14", typeof(string)),
        //        new DataColumn("Ctrp1aCol15", typeof(string)),
        //        new DataColumn("Ctrp1aCol16", typeof(string)),
        //        new DataColumn("Ctrp1aCol17", typeof(string)),
        //        new DataColumn("Ctrp1aCol18", typeof(string)),
        //        new DataColumn("Ctrp1aCol19", typeof(string)),
        //        new DataColumn("Ctrp1aCol20", typeof(string)),

        //    });

        //    dt.Rows.Clear();

        //    SarMutPencapaianNilaiFacade pencapaianNilaiFacade = new SarMutPencapaianNilaiFacade();
        //    ArrayList arrOutputProduksiCtrp = pencapaianNilaiFacade.OutputProduksi(1,int.Parse(selectTahun.Value.ToString()));

        //    foreach (SarMutPencapaianNilai nilai in arrOutputProduksiCtrp)
        //    {
        //        dt.Rows.Add(nilai.DeptName, nilai.SarMutDepartment, nilai.ParameterTerukur, nilai.Smt1LY, nilai.Smt2LY, nilai.Tahun, nilai.Jan, nilai.Feb, nilai.Mar, nilai.Apr, nilai.Mei, nilai.Jun, nilai.Smt1TahunIni, nilai.Jul, nilai.Agu, nilai.Sep,nilai.Oct,nilai.Nov,nilai.Des,nilai.Smt2TahunIni);
        //    }

        //    Repeater1.DataSource = dt;
        //    Repeater1.DataBind();

        //    if (arrOutputProduksiCtrp.Count > 0)
        //    {
        //        //divOutputProduksi1.Attributes["class"] = "widget-box widget-color-orange collapsed";
        //    }
        //}
        //private void LoadKrwgDiv1()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[20] {
        //        new DataColumn("Krwg2aCol1", typeof(string)),
        //        new DataColumn("Krwg2aCol2", typeof(string)),
        //        new DataColumn("Krwg2aCol3", typeof(string)),
        //        new DataColumn("Krwg2aCol4", typeof(string)),
        //        new DataColumn("Krwg2aCol5", typeof(string)),
        //        new DataColumn("Krwg2aCol6", typeof(string)),
        //        new DataColumn("Krwg2aCol7", typeof(string)),
        //        new DataColumn("Krwg2aCol8", typeof(string)),
        //        new DataColumn("Krwg2aCol9", typeof(string)),
        //        new DataColumn("Krwg2aCol10", typeof(string)),
        //        new DataColumn("Krwg2aCol11", typeof(string)),
        //        new DataColumn("Krwg2aCol12", typeof(string)),
        //        new DataColumn("Krwg2aCol13", typeof(string)),
        //        new DataColumn("Krwg2aCol14", typeof(string)),
        //        new DataColumn("Krwg2aCol15", typeof(string)),
        //        new DataColumn("Krwg2aCol16", typeof(string)),
        //        new DataColumn("Krwg2aCol17", typeof(string)),
        //        new DataColumn("Krwg2aCol18", typeof(string)),
        //        new DataColumn("Krwg2aCol19", typeof(string)),
        //        new DataColumn("Krwg2aCol20", typeof(string)),

        //    });

        //    dt.Rows.Clear();

        //    SarMutPencapaianNilaiFacade pencapaianNilaiFacade = new SarMutPencapaianNilaiFacade();
        //    ArrayList arrOutputProduksiKrwg = pencapaianNilaiFacade.OutputProduksi(1, int.Parse(selectTahun.Value.ToString()));

        //    foreach (SarMutPencapaianNilai nilai in arrOutputProduksiKrwg)
        //    {
        //        dt.Rows.Add(nilai.DeptName, nilai.SarMutDepartment, nilai.ParameterTerukur, nilai.Smt1LY, nilai.Smt2LY, nilai.Tahun, nilai.Jan, nilai.Feb, nilai.Mar, nilai.Apr, nilai.Mei, nilai.Jun, nilai.Smt1TahunIni, nilai.Jul, nilai.Agu, nilai.Sep, nilai.Oct, nilai.Nov, nilai.Des, nilai.Smt2TahunIni);
        //    }

        //    Repeater2.DataSource = dt;
        //    Repeater2.DataBind();
        //}
        //protected void divSatu_ServerClick(object sender, EventArgs e)
        //{

        //}
        //protected void divDua_ServerClick(object sender, EventArgs e)
        //{

        //}
        //protected void a_collapse_1_ServerClick(object sender, EventArgs e)
        //{
        //    div1.Attributes["style"] = "";
        //}
        #endregion gak pake


        private static bool isNumeric(string st)
        {
            try { Double.Parse(st); return true; }
            catch (Exception) { return false; }
        }
        private void DisplayLabel(int sarMutDeptID, int typeSarMutNya)
        {
            SarMutPencapaianNilaiFacade pencapaianNilaiFacade = new SarMutPencapaianNilaiFacade();
            //ArrayList arrOutputProduksiCtrp = pencapaianNilaiFacade.PencapaianSarMut(int.Parse(selectTahun.Value.ToString()), sarMutDeptID, typeSarMutNya);
            ArrayList arrOutputProduksiCtrp = pencapaianNilaiFacade.PencapaianSarMut(int.Parse(selectTahun.SelectedValue), sarMutDeptID, typeSarMutNya);

            if (arrOutputProduksiCtrp.Count > 0)
            {
                string xDept = ((SarMutPencapaianNilai)arrOutputProduksiCtrp[0]).DeptName;
                string xSarmut = ((SarMutPencapaianNilai)arrOutputProduksiCtrp[0]).SarMutDepartment;

                LblDisplaySarMut.Text += "" +
                "<div class=\"col-xs-12 col-sm-12\">" +
                "   <div id=\"header27_0\" runat=\"server\" class=\"timeline-item clearfix no-padding-bottom no-margin-bottom\">" +
                "       <div class=\"widget-box transparent no-margin-left no-padding-bottom no-margin-bottom\">" +
                "           <div class=\"widget-header widget-header-small no-padding-bottom no-margin-bottom\">" +
                "               <h5 class=\"widget-title smaller no-margin-left no-padding-left\">" +
                //"                   <span class=\"label label-success arrowed-right\"> Board Mill test > Tingkat Produktivitas Delivery Plant</span>" +
                "                   <span class=\"label label-success arrowed-right\"> " + xDept + " > " + xSarmut + " </span>" +
                "				</h5>" +
                "               <span class=\"widget-toolbar\">" +
                "				    <a href=\"#\" data-action=\"reload\">" +
                "						<i class=\"ace-icon fa fa-refresh\"></i>" +
                "					</a>" +
                "					<a data-action=\"collapse\">" +
                "						<i class=\"ace-icon fa fa-chevron-up\"></i>" +
                "					</a>" +
                "				    <a href=\"#\" data-action=\"close\">" +
                "					    <i class=\"ace-icon fa fa-times\"></i>" +
                "				    </a>" +
                "					</span>" +
                "           </div>" +
                "		    <div id=\"div27\" runat=\"server\" class=\"widget-body no-padding-bottom no-margin-bottom\">" +
                "               <div class=\"widget-main\">" +
                "                   <div id=\"DivCtrp27_0\" runat=\"server\"> " +
                "                               <table id=\"GridView53a\" style=\"width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;\">" +
                "                               <tr class=\"GridviewScrollHeader\">" +
                "                               <td colspan=\"3\" class=\"blue center\">Plant Citereup</td>" +
                "                               <td rowspan=\"2\" class=\"red\">SmtI-LY</td>" +
                "                               <td rowspan=\"2\" class=\"red\">SmtII-LY</td>" +
                "                               <td rowspan=\"2\">Tahun</td>" +
                "                               <td rowspan=\"2\">Jan</td>" +
                "                               <td rowspan=\"2\">Feb</td>" +
                "                               <td rowspan=\"2\">Mar</td>" +
                "                               <td rowspan=\"2\">Apr</td>" +
                "                               <td rowspan=\"2\">Mei</td>" +
                "                               <td rowspan=\"2\">Jun</td>" +
                "                               <td rowspan=\"2\" class=\"red\">Smt-I</td>" +
                "                               <td rowspan=\"2\">Jul</td>" +
                "                               <td rowspan=\"2\">Agu</td>" +
                "                               <td rowspan=\"2\">Sep</td>" +
                "                               <td rowspan=\"2\">Okt</td>" +
                "                               <td rowspan=\"2\">Nov</td>" +
                "                               <td rowspan=\"2\">Des</td>" +
                "                               <td rowspan=\"2\" class=\"red\" id=\"tdCol19\" runat=\"server\" visible=\"true\">Smt-II</td>" +
                "                           </tr>" +
                "                           <tr class=\"GridviewScrollHeader\">" +
                "                               <td>Dept</td>" +
                "                               <td>SarMut</td>" +
                "                               <td>Parameter Terukur</td>" +
                "                           </tr>";

                foreach (SarMutPencapaianNilai nilai in arrOutputProduksiCtrp)
                {
                    string strSmt1LY = string.Empty; string strSmt2LY = string.Empty; string strJan = string.Empty; string strFeb = string.Empty; string strMar = string.Empty; string strApr = string.Empty;
                    string strMei = string.Empty; string strJun = string.Empty; string strJul = string.Empty; string strAgu = string.Empty; string strSep = string.Empty;
                    string strOkt = string.Empty; string strNov = string.Empty; string strDes = string.Empty; string strSmt1TahunIni = string.Empty; string strSmt2TahunIni = string.Empty;

                    if (isNumeric(nilai.Jan) && isNumeric(nilai.Feb) && isNumeric(nilai.Mar) && isNumeric(nilai.Apr) && isNumeric(nilai.Mei) && isNumeric(nilai.Jun) &&
                        isNumeric(nilai.Jul) && isNumeric(nilai.Agu) && isNumeric(nilai.Sep) && isNumeric(nilai.Oct) && isNumeric(nilai.Nov) && isNumeric(nilai.Des) &&
                        isNumeric(nilai.Smt1LY) && isNumeric(nilai.Smt2LY) && isNumeric(nilai.Smt1TahunIni) && isNumeric(nilai.Smt2TahunIni))
                    {
                        if (string.IsNullOrEmpty(nilai.Smt1LY) || (Decimal.Parse(nilai.Smt1LY) == 0 && nilai.Header == 0)) strSmt1LY = ""; else strSmt1LY = nilai.Smt1LY;
                        if (string.IsNullOrEmpty(nilai.Smt2LY) || (Decimal.Parse(nilai.Smt2LY) == 0 && nilai.Header == 0)) strSmt2LY = ""; else strSmt2LY = nilai.Smt2LY;
                        if (string.IsNullOrEmpty(nilai.Jan) || (Decimal.Parse(nilai.Jan) == 0 && nilai.Header == 0)) strJan = ""; else strJan = nilai.Jan;
                        if (string.IsNullOrEmpty(nilai.Feb) || (Decimal.Parse(nilai.Feb) == 0 && nilai.Header == 0)) strFeb = ""; else strFeb = nilai.Feb;
                        if (string.IsNullOrEmpty(nilai.Mar) || (Decimal.Parse(nilai.Mar) == 0 && nilai.Header == 0)) strMar = ""; else strMar = nilai.Mar;
                        if (string.IsNullOrEmpty(nilai.Apr) || (Decimal.Parse(nilai.Apr) == 0 && nilai.Header == 0)) strApr = ""; else strApr = nilai.Apr;
                        if (string.IsNullOrEmpty(nilai.Mei) || (Decimal.Parse(nilai.Mei) == 0 && nilai.Header == 0)) strMei = ""; else strMei = nilai.Mei;
                        if (string.IsNullOrEmpty(nilai.Jun) || (Decimal.Parse(nilai.Jul) == 0 && nilai.Header == 0)) strJun = ""; else strJun = nilai.Jun;
                        if (string.IsNullOrEmpty(nilai.Jul) || (Decimal.Parse(nilai.Jul) == 0 && nilai.Header == 0)) strJul = ""; else strJul = nilai.Jul;
                        if (string.IsNullOrEmpty(nilai.Agu) || (Decimal.Parse(nilai.Agu) == 0 && nilai.Header == 0)) strAgu = ""; else strAgu = nilai.Agu;
                        if (string.IsNullOrEmpty(nilai.Sep) || (Decimal.Parse(nilai.Sep) == 0 && nilai.Header == 0)) strSep = ""; else strSep = nilai.Sep;
                        if (string.IsNullOrEmpty(nilai.Oct) || (Decimal.Parse(nilai.Oct) == 0 && nilai.Header == 0)) strOkt = ""; else strOkt = nilai.Oct;
                        if (string.IsNullOrEmpty(nilai.Nov) || (Decimal.Parse(nilai.Nov) == 0 && nilai.Header == 0)) strNov = ""; else strNov = nilai.Nov;
                        if (string.IsNullOrEmpty(nilai.Des) || (Decimal.Parse(nilai.Des) == 0 && nilai.Header == 0)) strDes = ""; else strDes = nilai.Des;
                        if (string.IsNullOrEmpty(nilai.Smt1TahunIni) || (Decimal.Parse(nilai.Smt1TahunIni) == 0 && nilai.Header == 0)) strSmt1TahunIni = ""; else strSmt1TahunIni = nilai.Smt1TahunIni;
                        if (string.IsNullOrEmpty(nilai.Smt2TahunIni) || (Decimal.Parse(nilai.Smt2TahunIni) == 0 && nilai.Header == 0)) strSmt2TahunIni = ""; else strSmt2TahunIni = nilai.Smt2TahunIni;
                    }
                    else
                    {
                        if (isNumeric(nilai.Smt1LY))
                        {
                            if (Decimal.Parse(nilai.Smt1LY) == 0) strSmt1LY = ""; else strSmt1LY = nilai.Smt1LY;
                        }
                        else strSmt1LY = nilai.Smt1LY;
                        if (isNumeric(nilai.Smt2LY))
                        {
                            if (Decimal.Parse(nilai.Smt2LY) == 0) strSmt2LY = ""; else strSmt2LY = nilai.Smt2LY;
                        }
                        else strSmt2LY = nilai.Smt2LY;
                        if (isNumeric(nilai.Smt1TahunIni))
                        {
                            if (Decimal.Parse(nilai.Smt1TahunIni) == 0) strSmt1TahunIni = ""; else strSmt1TahunIni = nilai.Smt1TahunIni;
                        }
                        else strSmt1TahunIni = nilai.Smt1TahunIni;
                        if (isNumeric(nilai.Smt2TahunIni))
                        {
                            if (Decimal.Parse(nilai.Smt2TahunIni) == 0) strSmt2TahunIni = ""; else strSmt2TahunIni = nilai.Smt2TahunIni;
                        }
                        else strSmt2TahunIni = nilai.Smt2TahunIni;
                        if (isNumeric(nilai.Jan))
                        {
                            if (Decimal.Parse(nilai.Jan) == 0) strJan = ""; else strJan = nilai.Jan;
                        }
                        else strJan = nilai.Jan;
                        if (isNumeric(nilai.Feb))
                        {
                            if (Decimal.Parse(nilai.Feb) == 0) strFeb = ""; else strFeb = nilai.Feb;
                        }
                        else strFeb = nilai.Feb;
                        if (isNumeric(nilai.Mar))
                        {
                            if (Decimal.Parse(nilai.Mar) == 0) strMar = ""; else strMar = nilai.Mar;
                        }
                        else strMar = nilai.Mar;
                        if (isNumeric(nilai.Apr))
                        {
                            if (Decimal.Parse(nilai.Apr) == 0) strApr = ""; else strApr = nilai.Apr;
                        }
                        else strApr = nilai.Apr;
                        if (isNumeric(nilai.Mei))
                        {
                            if (Decimal.Parse(nilai.Mei) == 0) strMei = ""; else strMei = nilai.Mei;
                        }
                        else strMei = nilai.Mei;
                        if (isNumeric(nilai.Jun))
                        {
                            if (Decimal.Parse(nilai.Jun) == 0) strJun = ""; else strJun = nilai.Jun;
                        }
                        else strJun = nilai.Jun;
                        if (isNumeric(nilai.Jul))
                        {
                            if (Decimal.Parse(nilai.Jul) == 0) strJul = ""; else strJul = nilai.Jul;
                        }
                        else strJul = nilai.Jul;
                        if (isNumeric(nilai.Agu))
                        {
                            if (Decimal.Parse(nilai.Agu) == 0) strAgu = ""; else strAgu = nilai.Agu;
                        }
                        else strAgu = nilai.Agu;
                        if (isNumeric(nilai.Sep))
                        {
                            if (Decimal.Parse(nilai.Sep) == 0) strSep = ""; else strSep = nilai.Sep;
                        }
                        else strSep = nilai.Sep;
                        if (isNumeric(nilai.Oct))
                        {
                            if (Decimal.Parse(nilai.Oct) == 0) strOkt = ""; else strOkt = nilai.Oct;
                        }
                        else strOkt = nilai.Oct;
                        if (isNumeric(nilai.Nov))
                        {
                            if (Decimal.Parse(nilai.Nov) == 0) strNov = ""; else strNov = nilai.Nov;
                        }
                        else strNov = nilai.Nov;
                        if (isNumeric(nilai.Des))
                        {
                            if (Decimal.Parse(nilai.Des) == 0) strDes = ""; else strDes = nilai.Des;
                        }
                        else strDes = nilai.Des;
                    }

                    LblDisplaySarMut.Text += "" +
                    "                            <tr class=\"GridviewScrollItem\">" +
                    "                                <td style=\"background-color:#EFEFEF;\" ><asp:Label ID = \"Label20\" runat=\"server\">"+ nilai.DeptName + "</asp:Label></td>" +
                    "                                <td style=\"background-color:#EFEFEF;\" ><asp:Label ID = \"Label21\" runat=\"server\">"+ nilai.SarMutDepartment + "</asp:Label></td>" +
                    "                                <td style=\"background-color:#EFEFEF;\" ><asp:Label ID = \"Label22\" runat=\"server\">"+ nilai.ParameterTerukur + "</asp:Label></td>" +
                    "                                <td><asp:Label ID=\"Label23\" runat=\"server\"></asp:Label>"+ strSmt1LY + "</td>" +
                    "                                <td><asp:Label ID=\"Label24\" runat=\"server\"></asp:Label>"+ strSmt2LY + "</td>" +
                    "                                <td><asp:Label ID = \"Label25\" runat=\"server\"></asp:Label>"+ nilai.Tahun + "</td>" +
                    "                                <td><a href = \"../../About.aspx\" ><asp:Label ID = \"Label26\" runat=\"server\"></asp:Label></a>"+ strJan + "</td>" +
                    "                                <td><asp:Label ID = \"Label7\" runat=\"server\"></asp:Label>"+ strFeb + "</td>" +
                    "                                <td><asp:Label ID = \"Label8\" runat=\"server\"></asp:Label>"+ strMar + "</td>" +
                    "                                <td><asp:Label ID = \"Label9\" runat=\"server\"></asp:Label>"+ strApr + "</td>" +
                    "                                <td><asp:Label ID = \"Label10\" runat=\"server\"></asp:Label>"+ strMei + "</td>" +
                    "                                <td><asp:Label ID = \"Label11\" runat=\"server\"></asp:Label>"+ strJun + "</td>" +
                    "                                <td><asp:Label ID = \"Label12\" runat=\"server\"></asp:Label>"+ strSmt1TahunIni + "</td>" +
                    "                                <td><asp:Label ID = \"Label13\" runat=\"server\"></asp:Label>"+ strJul + "</td>" +
                    "                                <td><asp:Label ID = \"Label14\" runat=\"server\"></asp:Label>"+ strAgu + "</td>" +
                    "                                <td><asp:Label ID = \"Label15\" runat=\"server\"></asp:Label>"+ strSep + "</td>" +
                    "                                <td><asp:Label ID = \"Label16\" runat=\"server\"></asp:Label>"+ strOkt + "</td>" +
                    "                                <td><asp:Label ID = \"Label17\" runat=\"server\"></asp:Label>"+ strNov + "</td>" +
                    "                                <td><asp:Label ID = \"Label18\" runat=\"server\" Visible=\"true\"></asp:Label>"+ strDes + "</td>" +
                    "                                <td><asp:Label ID = \"Label19\" runat=\"server\" Visible=\"true\"></asp:Label>"+ strSmt2TahunIni + "</td>" +
                    "                            </tr>";

                }

                LblDisplaySarMut.Text += "" +
                "                        </table>" +
                "                    </div>" +
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "</div>";

            }





        }
        protected void selectTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblDisplaySarMut.Text = string.Empty;

            DisplayLabel(21, 1);
        }





    }
}