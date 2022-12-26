using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.planningform
{
    public partial class ProductionSpeedControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                string[] UserUpload = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UploadSC", "CostControl").Split(',');
                int post = Array.IndexOf(UserUpload, ((Users)Session["Users"]).UserID);
                frmUpload.Visible = (post > -1) ? true : false;
                frmUpload1.Visible = (post > -1) ? true : false;
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            SpeedMontoring(1, Chart2);
            SpeedMontoring(2, Chart3);
            SpeedMontoring(3, Chart4);
            SpeedMontoring(4, Chart5);
            SpeedMontoring(5, Chart6);
            SpeedMontoring(6, Chart7);
        }

        private void SpeedMontoring(int line, Chart Chart1)
        {
            FormingControl fm = new FormingControl();
            ArrayList arrData = new ArrayList();
            string tgl = (txtTanggal.Text == "") ? DateTime.Now.ToString("yyyyMMdd") : DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd");
            int Line = line;
            int interval = 0;
            if (RBmenit1.Checked == true)
                interval = 1;
            if (RBmenit2.Checked == true)
                interval = 5;
            if (RBmenit3.Checked == true)
                interval = 10;
            if (RBmenit4.Checked == true)
                interval = 15;
            arrData = fm.LoadDataGraph(DateTime.Parse(txtTanggal.Text),tgl, Line, interval);
            string[] x = new string[arrData.Count];
            int[] z = new int[arrData.Count];
            int[] y = new int[arrData.Count];
            int i = 0; int avgs = 0;
            foreach (SpeedMon s in arrData)
            {
                y[i] = s.Speed;
                x[i] = s.Tanggal.ToString("HH:mm");
                z[i] = s.SpeedAvg;
                i++;
                avgs = s.SpeedAvg;
            }
            Chart1.Titles.Add("FORMING LINE " + Line.ToString() + " SPEED MONITORING\n" + txtTanggal.Text);
            Chart1.Legends.Add("Speed");
            Chart1.Series.Add("LINE " + Line.ToString());
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].IsValueShownAsLabel = false;
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Line;
            Chart1.Series[0].XValueType = ChartValueType.Time;
            Chart1.Legends[0].Enabled = true;
            Chart1.Legends[0].Docking = Docking.Bottom;
            Chart1.Series[0].BorderWidth = 2;
            Chart1.Series[0].ToolTip = "Jam = #VALX \nSpeed = #VALY{N}";
            //avg line
            Chart1.Legends.Add("Average");
            Chart1.Series.Add("AVG Speed " + Line.ToString() + "= " + avgs.ToString());
            Chart1.Series[1].IsVisibleInLegend = true;
            Chart1.Series[1].IsValueShownAsLabel = false;
            Chart1.Series[1].Points.DataBindXY(x, z);
            Chart1.Series[1].ChartType = SeriesChartType.Line;
            Chart1.Legends[1].Enabled = true;
            Chart1.Legends[1].Docking = Docking.Bottom;
            Chart1.Series[1].BorderWidth = 2;

            Chart1.ChartAreas[0].AxisX.Interval = 3;
            //Chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Minutes;
            //Chart1.ChartAreas[0].AxisX.Maximum = 1.0;
            //Chart1.ChartAreas[0].AxisX.Minimum = 0.0;
            Chart1.ChartAreas[0].AxisY.Interval = 5;
            Chart1.ChartAreas[0].AxisY.Maximum = 75;
            Chart1.ChartAreas[0].AxisY.Minimum = 0;
            Chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Upload1.HasFile)
            {
                FormingControl fm = new FormingControl();
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                fm.DropTable();
                if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                {
                    string UploadPath = "D:\\SpeedMon\\" + Path.GetFileName(Upload1.PostedFile.FileName);
                    string conExcel = string.Empty;
                    string fixFilename = RenameIfExist(Path.GetFileName(Upload1.PostedFile.FileName)); ;
                    string[] nFile = fixFilename.Split(new string[] { "\\" }, StringSplitOptions.None);
                    string fileName = "D:\\SpeedMon\\" + nFile[nFile.Count() - 1];
                    string fType = Upload1.PostedFile.ContentType.ToString();
                    conExcel = setExcelConn(fileName, fType, false);

                    if (conExcel != string.Empty && conExcel != "INVALID")
                    {
                        Upload1.SaveAs(fileName);
                        using (OleDbConnection cnn = new OleDbConnection(conExcel))
                        {
                            try
                            {
                                cnn.Open();
                                string sheet1 = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                                DataTable dtExcel = new DataTable();
                                using (OleDbDataAdapter odb = new OleDbDataAdapter("Select * from [" + sheet1 + "]", conExcel))
                                {
                                    odb.Fill(dtExcel);
                                    lst.DataSource = dtExcel;
                                    lst.DataBind();
                                }
                                cnn.Close();
                                string exists = this.CreatedTable(dtExcel);
                                SqlConnection sqlcon = new SqlConnection(Global.ConnectionString());
                                sqlcon.Open();
                                if (exists == null)
                                {
                                    foreach (DataColumn dc in dtExcel.Columns)
                                    {
                                        string fld = "";
                                        // (dtExcel.Columns[0].ColumnName == "Date") ? "Tanggal" : dc.ColumnName.Replace(" ", "_");
                                        //fld = (dtExcel.Columns[1].ColumnName != "Speed Forming Monitoring") ? "Speed_Forming_Monitoring" :
                                        // (dc.ColumnName == "Date") ? "Tanggal" : dc.ColumnName.Replace(" ", "_");
                                        string Tahun = (dc.ColumnName.Length > 4) ? dc.ColumnName.Substring(0, 4) : dc.ColumnName;
                                        switch (Tahun)
                                        {
                                            case "Date": fld = "Tanggal"; break;
                                            case "2017": fld = "Speed_Forming_Monitoring"; break;
                                            default: fld = dc.ColumnName.Replace(" ", "_"); break;
                                        }
                                        if (exists == null)
                                        {
                                            SqlCommand createtable = new SqlCommand("CREATE TABLE PRD_SpeedMonTemp (ID INT IDENTITY(1,1) NOT NULL," + fld + " varchar(MAX)NULL) ON[PRIMARY] ", sqlcon);
                                            createtable.ExecuteNonQuery();
                                            exists = "PRD_SpeedMonTemp";// dtExcel.TableName;
                                        }
                                        else
                                        {
                                            SqlCommand addcolumn = new SqlCommand("ALTER TABLE PRD_SpeedMonTemp ADD " + fld + " varchar(MAX) NULL", sqlcon);
                                            addcolumn.ExecuteNonQuery();
                                        }
                                    }
                                    using (SqlBulkCopy bulkcopy = new SqlBulkCopy(sqlcon))
                                    {
                                        bulkcopy.DestinationTableName = "PRD_SpeedMonTemp";
                                        bulkcopy.ColumnMappings.Add("Date", "Tanggal");
                                        bulkcopy.ColumnMappings.Add("Speed Forming Monitoring", "Speed_Forming_Monitoring");
                                        bulkcopy.ColumnMappings.Add("F3", "F3");
                                        bulkcopy.ColumnMappings.Add("F4", "F4");
                                        bulkcopy.ColumnMappings.Add("F5", "F5");
                                        bulkcopy.ColumnMappings.Add("F6", "F6");
                                        bulkcopy.ColumnMappings.Add("F7", "F7");
                                        bulkcopy.ColumnMappings.Add("F8", "F8");
                                        bulkcopy.ColumnMappings.Add("F9", "F9");
                                        bulkcopy.ColumnMappings.Add("F10", "F10");
                                        bulkcopy.ColumnMappings.Add("F11", "F11");
                                        bulkcopy.ColumnMappings.Add("F12", "F12");
                                        bulkcopy.ColumnMappings.Add("F13", "F13");
                                        bulkcopy.ColumnMappings.Add("F14", "F14");
                                        bulkcopy.ColumnMappings.Add("F15", "F15");
                                        bulkcopy.ColumnMappings.Add("F16", "F16");
                                        bulkcopy.ColumnMappings.Add("F17", "F17");
                                        bulkcopy.ColumnMappings.Add("F18", "F18");
                                        bulkcopy.WriteToServer(dtExcel);
                                        bulkcopy.Close();
                                    }
                                }
                                else
                                {
                                    using (SqlBulkCopy bulkcopy = new SqlBulkCopy(sqlcon))
                                    {
                                        bulkcopy.DestinationTableName = "PRD_SpeedMonTemp";
                                        bulkcopy.ColumnMappings.Add("Date", "Tanggal");
                                        bulkcopy.ColumnMappings.Add("Speed Forming Monitoring", "Speed_Forming_Monitoring");
                                        bulkcopy.ColumnMappings.Add("F3", "F3");
                                        bulkcopy.ColumnMappings.Add("F4", "F4");
                                        bulkcopy.ColumnMappings.Add("F5", "F5");
                                        bulkcopy.ColumnMappings.Add("F6", "F6");
                                        bulkcopy.ColumnMappings.Add("F7", "F7");
                                        bulkcopy.ColumnMappings.Add("F8", "F8");
                                        bulkcopy.ColumnMappings.Add("F9", "F9");
                                        bulkcopy.ColumnMappings.Add("F10", "F10");
                                        bulkcopy.ColumnMappings.Add("F11", "F11");
                                        bulkcopy.ColumnMappings.Add("F12", "F12");
                                        bulkcopy.ColumnMappings.Add("F13", "F13");
                                        bulkcopy.ColumnMappings.Add("F14", "F14");
                                        bulkcopy.ColumnMappings.Add("F15", "F15");
                                        bulkcopy.ColumnMappings.Add("F16", "F16");
                                        bulkcopy.ColumnMappings.Add("F17", "F17");
                                        bulkcopy.ColumnMappings.Add("F18", "F18");
                                        bulkcopy.WriteToServer(dtExcel);
                                        bulkcopy.Close();
                                    }
                                }
                                sqlcon.Close();

                                string res = "";
                                //this.InsertKeTable();
                                BulkInsertKeTable();
                                txtTanggal.Text = res;
                                DisplayAJAXMessage(this, "Upload data :" + dtExcel.Rows.Count.ToString("N2") + " Record updated");
                                btnPreview_Click(null, null);
                            }
                            catch (Exception ex)
                            {
                                DisplayAJAXMessage(this, "error: " + ex.Message.ToString());
                                fm.DropTable();
                            }
                        }
                    }


                }
                else
                {
                    DisplayAJAXMessage(this, "Hanya file excel hasil export PO Harga Nol yng bisa di upload");
                    return;
                }
            }

        }

        private string CreatedTable(DataTable dtExcel)
        {
            SqlConnection sqlcon = new SqlConnection(Global.ConnectionString());
            sqlcon.Open();
            string exists = null;
            try
            {

                SqlCommand cmd = new SqlCommand("SELECT * FROM sysobjects where name = 'PRD_SpeedMonTemp'", sqlcon);
                exists = cmd.ExecuteScalar().ToString();
                sqlcon.Close();
            }
            catch (Exception exce)
            {
                exists = null;
                sqlcon.Close();
            }
            return exists;
        }
        protected void PDlink_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Redirected.aspx");
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            txtTanggal.Text = (txtTanggal.Text == "") ? DateTime.Now.ToString("dd-MMM-yyyy") : (DateTime.Parse(txtTanggal.Text).AddDays(-1)).ToString("dd-MMM-yyyy");
            btnPreview_Click(null, null);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            txtTanggal.Text = (txtTanggal.Text == "") ? DateTime.Now.ToString("dd-MMM-yyyy") : (DateTime.Parse(txtTanggal.Text).AddDays(+1)).ToString("dd-MMM-yyyy");
            btnPreview_Click(null, null);
        }
        private static string setExcelConn(string fileName, string fileType)
        {
            return setExcelConn(fileName, fileType, true);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private static string setExcelConn(string fileName, string fileType, bool HDR)
        {

            string result = "INVALID";
            if ((fileName != null || fileName != string.Empty) &&
                (fileType != null || fileType != string.Empty))
            {

                string incHDR = ""; //(HDR == true) ? ";HDR=YES" : "";
                try
                {
                    switch (fileType)
                    {
                        case "application/vnd.ms-excel":
                            result = "Provider=Microsoft.Jet.OLEDB.4.0;";
                            result += "Data Source=" + fileName + ";";
                            result += "Extended Properties='Excel 8.0" + incHDR + ";";
                            result += " IMEX=1'";
                            break;
                        case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                            result = "Provider=Microsoft.ACE.OLEDB.12.0;";
                            result += "Data Source=" + fileName + ";";
                            result += "Extended Properties='Excel 8.0" + incHDR + ";";
                            result += " IMEX=1'";
                            break;
                        default:
                            result = "INVALID";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }

        private static string RenameIfExist(string fileName)
        {
            string result = string.Empty;
            string[] nmFile = fileName.Split(new string[] { "\\" }, StringSplitOptions.None);
            string[] tmpFile = fileName.Split(new string[] { "." }, StringSplitOptions.None);
            if (File.Exists(("D:\\SpeedMon\\" + nmFile[nmFile.Count() - 1])))
            {
                Random rnd = new Random();
                int numExt = rnd.Next(1, 9999);
                string sExt = numExt.ToString().PadLeft(4, '0');

                var tmpResult = "";
                int arrLen = tmpFile.Length;
                for (var idx = 0; idx < arrLen; idx++)
                {
                    if (idx < (arrLen - 1))
                    {
                        tmpResult += tmpFile[idx].ToString();
                    }
                    else if (idx == (arrLen - 1))
                    {
                        tmpResult += sExt + "." + tmpFile[idx].ToString();
                    }
                }
                result = tmpResult;
            }
            else
            {
                result = fileName;
            }
            return result;
        }
        private void CreatedTable()
        {
            string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRD_SpeedMonTmp]') AND type in (N'U'))" +
                          "DROP TABLE [dbo].[PRD_SpeedMonTmp] ";
            strSQL += "CREATE TABLE PRD_SpeedMonTmp ( ID INT IDENTITY(1,1) NOT NULL, Tanggal VARCHAR(100) NULL,Jam VARCHAR(100) NULL,Speed VARCHAR(100) NULL ) ON[PRIMARY]";
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        }

        /// <summary>
        /// Inserts data from upload excel to table.
        /// only 4 line machine
        /// </summary>
        /// <returns></returns>
        private string InsertKeTable()
        {
            int result = 0; string tgl = DateTime.Now.ToString("dd-MMM-yyyy");
            for (int i = 1; i <= 6; i++)
            {
                FormingControl fm = new FormingControl();
                SpeedMon sp = new SpeedMon();
                fm.MachineLine = i.ToString();
                sp = fm.Retrieve();
                ArrayList arrData = new ArrayList();
                ArrayList arrD = new ArrayList();
                arrData = fm.Retrieve(true);
                if (arrData.Count > 0)
                {
                    foreach (SpeedMon s in arrData)
                    {
                        SpeedMon sm = new SpeedMon();
                        sm.MachineNo = sp.MachineNo;
                        sm.MachineName = sp.MachineName;
                        sm.RowStatus = 0;
                        sm.Tanggal = s.Tanggal;
                        sm.Speed = s.Speed;
                        sm.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        sm.CreatedTime = DateTime.Now;
                        //arrData.Add(sm);
                        //insert data ke table PRD_SpeedMon
                        ZetroLib zl = new ZetroLib();
                        zl.TableName = "PRD_SpeedMon";
                        zl.hlp = new SpeedMon();
                        zl.Criteria = "MachineNo,MachineName,Tanggal,Speed,RowStatus,CreatedBy,CreatedTime";
                        zl.StoreProcedurName = "spPRD_SpeedMon_Insert";
                        zl.Option = "Insert";
                        zl.ReturnID = false;
                        string rst = zl.CreateProcedure();
                        if (rst == string.Empty)
                        {
                            zl.hlp = sm;
                            result = zl.ProcessData();
                        }
                        tgl = s.Tanggal.ToString("dd-MMM-yyyy");
                    }
                }
            }
            //FormingControl fms = new FormingControl();
            //fms.DropTable();
            return tgl;
        }
        private string BulkInsertKeTable()
        {
            string tgl = DateTime.Now.ToString("dd-MMM-yyyy");

            FormingControl fm = new FormingControl();
            fm.Retrieve2(true);
            return tgl;
        }
        protected void txtTanggal_TextChanged(object sender, EventArgs e)
        {

        }
        protected void RBmenit2_CheckedChanged(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
        }
        protected void RBmenit1_CheckedChanged(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
        }
        protected void RBmenit3_CheckedChanged(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
        }
        protected void RBmenit4_CheckedChanged(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
        }
    }
}
public class SpeedMon : GRCBaseDomain
{
    public int MachineNo { get; set; }
    public string MachineName { get; set; }
    public DateTime Tanggal { get; set; }
    public int Speed { get; set; }
    public DateTime Jam { get; set; }
    public int SpeedAvg { get; set; }
}
public class FormingControl
{
    ArrayList arrData = new ArrayList();
    SpeedMon spc = new SpeedMon();
    public string Criteria { get; set; }
    public string MachineLine { get; set; }
    public SpeedMon Retrieve()
    {
        spc = new SpeedMon();
        string strSQL = "Select " + this.FieldData() + " from PRD_SpeedMonTemp where ID in(7,9)";
        DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                spc = GenerateObject(sdr);
            }
        }
        return spc;
    }
    private string FieldData()
    {
        string result = string.Empty;
        switch (MachineLine)
        {
            case "1": result = "ID,Tanggal,Speed_Forming_Monitoring,F3 "; break;
            case "2": result = "ID,F4 Tanggal,F5,F6 "; break;
            case "3": result = "ID,F7 Tanggal,F8,F9 "; break;
            case "4": result = "ID,F10 Tanggal,F11,F12 "; break;
            case "5": result = "ID,F13 Tanggal,F14,F15 "; break;
            case "6": result = "ID,F16 Tanggal,F17,F18 "; break;
        }
        return result;
    }
    private string FieldData(bool Where)
    {
        string result = string.Empty;
        switch (MachineLine)
        {
            case "1": result = " AND Tanggal IS NOT NULL "; break;
            case "2": result = " AND F4 IS NOT NULL "; break;
            case "3": result = " AND F7 IS NOT NULL"; break;
            case "4": result = " AND F10 IS NOT NULL "; break;
            case "5": result = " AND F13 IS NOT NULL "; break;
            case "6": result = " AND F16 IS NOT NULL "; break;
        }
        return result;
    }
    public ArrayList Retrieve(bool Data)
    {
        string strSQL = "Select " + this.FieldData() + " from PRD_SpeedMonTemp where ID >=14 " + FieldData(true);
        DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObject(sdr, Data));
            }
        }
        return arrData;
    }
    public void Retrieve2(bool Data)
    {
        string strSQL =
            "declare @mcno1 int,@mcno2 int,@mcno3 int,@mcno4 int,@mcno5 int,@mcno6 int,@startid int " +
            "declare @mcname1 varchar(max),@mcname2 varchar(max),@mcname3 varchar(max),@mcname4 varchar(max),@mcname5 varchar(max),@mcname6 varchar(max) " +
            "set @mcno1=(select Speed_Forming_Monitoring from PRD_SpeedMonTemp where Tanggal='address') " +
            "set @mcno2=(select f5 from PRD_SpeedMonTemp where Tanggal='address') " +
            "set @mcno3=(select f8 from PRD_SpeedMonTemp where Tanggal='address') " +
            "set @mcno4=(select f11 from PRD_SpeedMonTemp where Tanggal='address') " +
            "set @mcno5=(select f14 from PRD_SpeedMonTemp where Tanggal='address') " +
            "set @mcno6=(select f17 from PRD_SpeedMonTemp where Tanggal='address') " +
            "set @mcname1=(select Speed_Forming_Monitoring from PRD_SpeedMonTemp where Tanggal='tag name') " +
            "set @mcname2=(select f5 from PRD_SpeedMonTemp where Tanggal='tag name') " +
            "set @mcname3=(select f8 from PRD_SpeedMonTemp where Tanggal='tag name') " +
            "set @mcname4=(select f11 from PRD_SpeedMonTemp where Tanggal='tag name') " +
            "set @mcname5=(select f14 from PRD_SpeedMonTemp where Tanggal='tag name') " +
            "set @mcname6=(select f17 from PRD_SpeedMonTemp where Tanggal='tag name') " +
            "insert PRD_SpeedMon " +
            "select machineNo,machineName,tanggal ,jam,speed, rowstatus,createdby,createdtime from ( " +
            "select  @mcno1 machineNo,@mcname1 machineName,substring(rtrim(tanggal),7,4)+'-'+substring(rtrim(tanggal),4,2)+'-'+substring(rtrim(tanggal),1,2)+' ' +replace( rtrim(Speed_Forming_Monitoring),'.',':')tanggal,null jam,f3 speed,0 rowstatus,'' createdby,getdate() createdtime  from PRD_SpeedMonTemp where id>=14  " +
            "union all select @mcno2 machineNo,@mcname2 machineName,substring(rtrim(f4),7,4)+'-'+substring(rtrim(f4),4,2)+'-'+substring(rtrim(f4),1,2)+' ' +replace( rtrim(f5),'.',':')tanggal,null jam,f6 speed,0 rowstatus,'' createdby,getdate() createdtime  from PRD_SpeedMonTemp where id>=14 " +
            "union all select @mcno3 machineNo,@mcname3 machineName,substring(rtrim(f7),7,4)+'-'+substring(rtrim(f7),4,2)+'-'+substring(rtrim(f7),1,2)+' ' +replace( rtrim(f8),'.',':')tanggal,null jam,f9 speed,0 rowstatus,'' createdby,getdate() createdtime  from PRD_SpeedMonTemp where id>=14 " +
            "union all select @mcno4 machineNo,@mcname4 machineName,substring(rtrim(f10),7,4)+'-'+substring(rtrim(f10),4,2)+'-'+substring(rtrim(f10),1,2)+' ' +replace( rtrim(f11),'.',':')tanggal,null jam,f12 speed,0 rowstatus,'' createdby,getdate() createdtime  from PRD_SpeedMonTemp where id>=14 " +
            "union all select @mcno5 machineNo,@mcname5 machineName,substring(rtrim(f13),7,4)+'-'+substring(rtrim(f13),4,2)+'-'+substring(rtrim(f13),1,2)+' ' +replace( rtrim(f14),'.',':')tanggal,null jam,f15 speed,0 rowstatus,'' createdby,getdate() createdtime  from PRD_SpeedMonTemp where id>=14 " +
            "union all select @mcno6 machineNo,@mcname6 machineName,substring(rtrim(f16),7,4)+'-'+substring(rtrim(f16),4,2)+'-'+substring(rtrim(f16),1,2)+' ' +replace( rtrim(f17),'.',':')tanggal,null jam,f18 speed,0 rowstatus,'' createdby,getdate() createdtime  from PRD_SpeedMonTemp where id>=14 " +
            ")A where isnull(tanggal,'-')<>'-'";
        DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
    }
    private string mc = string.Empty;
    private SpeedMon GenerateObject(SqlDataReader sdr)
    {
        spc = new SpeedMon();
        if (sdr["ID"].ToString() == "7") { mc = (sdr[2].ToString().Replace(".00", "")); }
        if (sdr["ID"].ToString() == "9") { spc.MachineNo = int.Parse(mc); spc.MachineName = sdr[2].ToString(); }
        return spc;
    }
    private SpeedMon GenerateObject(SqlDataReader sdr, bool data)
    {
        if (sdr[2] != DBNull.Value)
        {

            spc = new SpeedMon();
            string jame = "";
            string[] jam = sdr[2].ToString().Split('.');

            jame = (jam.Count() > 1) ? (jam[0].ToString().PadLeft(2, '0') + ":" + jam[1].ToString().PadRight(2, '0')).ToString() : (jam[0].ToString().PadLeft(2, '0') + ":00").ToString();
            string tanggal = sdr["Tanggal"].ToString() + " " + jame;
            spc.Tanggal = DateTime.Parse((sdr["Tanggal"].ToString() + " " + jame));
            spc.Speed = int.Parse(sdr[3].ToString());
        }
        return spc;
    }
    public ArrayList LoadDataGraph(DateTime tglmon, string Tanggal, int Line, int interval)
    {
        string test = (Convert.ToInt32(Tanggal.Substring(6, 2)) + 1).ToString().PadLeft(2, '0');
        arrData = new ArrayList();
        DateTime tgl2 = DateTime.Now;
        string ntgl = Tanggal.Substring(4, 2) + "-" + Tanggal.Substring(6, 2) + "-" +  Tanggal.Substring(0, 4);
        //tgl2 = DateTime.Parse(ntgl).AddDays(1);
        //string strtgl2 = tgl2.ToString("yyyyMMdd");
        string strtgl2 = tglmon.AddDays(1).ToString("yyyyMMdd");
        string MinSC = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MinSC", "CostControl");
        string strSQL = "select * from (SELECT datediff(minute,tanggal,convert(char,tanggal,112))%" + interval + " modulus, *,ISNULL((select AVG(Speed) from prd_speedMon where Speed >" + MinSC +
            " and Tanggal>='" + Tanggal + " 07:00" + "' and Tanggal<='" +
            strtgl2 + " 07:00 " +
                        "' AND MachineNo=" + Line + "),0)Avg FROM PRD_SpeedMon WHERE Tanggal>='" + Tanggal + " 07:00" + "' and Tanggal<='" +
            strtgl2 + " 07:00 " +
                        "' AND MachineNo=" + Line + " )s where modulus=0  Order by Tanggal";
        DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObj(sdr));
            }
        }
        return arrData;
    }
    private SpeedMon GenerateObj(SqlDataReader sdr)
    {
        spc = new SpeedMon();
        spc.ID = int.Parse(sdr["ID"].ToString());
        spc.MachineNo = int.Parse(sdr["MachineNo"].ToString());
        spc.MachineName = sdr["MachineName"].ToString();
        spc.Tanggal = DateTime.Parse(sdr["Tanggal"].ToString());
        spc.Jam = DateTime.Parse(sdr["Tanggal"].ToString());
        spc.Speed = int.Parse(sdr["Speed"].ToString());
        spc.SpeedAvg = int.Parse(sdr["Avg"].ToString());
        return spc;
    }
    public void DropTable()
    {
        string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRD_SpeedMonTemp]') AND type in (N'U'))" +
                      "DROP TABLE [dbo].[PRD_SpeedMonTemp] ";
        DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
    }
}

