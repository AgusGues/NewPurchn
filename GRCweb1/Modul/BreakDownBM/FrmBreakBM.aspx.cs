using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace GRCweb1.Modul.BreakDownBM
{
    public partial class FrmBreakBM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";
                clearForm();
                LoadBreakBMCharge();
                LoadBreakBMPlant1();
                LoadPlantGroup();
                LoadBreakBMProblem();
                LoadBreakBMPlant();
                LoadDataGridBreakBM(LoadGridBreakBM());
                LoadPlantGroupoff();
                ddlGrup.Enabled = false;
                ddlCharge.Enabled = false;
                ddlLine.Enabled = false;
                txtBDTime.Enabled = false;
                btnDelete.Enabled = false;
                txtTglBreak.Text = DateTime.Now.ToString("dd-MM-yyyy");
                LoadBreakKetebalan();
                //btnUpdate1.Disabled = true;
                ////btnList.Visible = false;
                ////load line mana?
            }

        }

        protected void txtFinishBD_TextChanged(object sender, EventArgs e)
        {
            if (txtFinishBD.Text != string.Empty && txtStartBD.Text != string.Empty)
            {
                //////txtNoDO1.Text = (DateTime.Parse(txtNoDO.Text) - DateTime.Parse(txtNoDO0.Text)).Days.ToString(); ;

                DateTime startTime = Convert.ToDateTime(txtTglBreak.Text + " " + txtStartBD.Text);
                DateTime endtime = (DateTime.Parse(txtFinishBD.Text).Hour - DateTime.Parse(txtStartBD.Text).Hour < 0) ?
                Convert.ToDateTime(txtTglBreak.Text + " " + txtFinishBD.Text).AddDays(1) : Convert.ToDateTime(txtTglBreak.Text + " " + txtFinishBD.Text);
                TimeSpan duration = endtime - startTime;
                this.txtBDTime.Text = Convert.ToString(duration);

            }

        }

        private void LoadBreakBMCharge()
        {
            ArrayList arrBreakBMCharge = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            arrBreakBMCharge = breakBMFacade.RetrieveBMCharge();
            ddlCharge.Items.Add(new ListItem("-- Pilih Lokasi Charge --", "0"));
            foreach (BreakBMCharge breakBMCharge in arrBreakBMCharge)
            {
                ddlCharge.Items.Add(new ListItem(breakBMCharge.LokasiCharge, breakBMCharge.ID.ToString()));
            }
        }

        private void LoadBreakKetebalan()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select distinct Kategori from FC_Items where RowStatus>-1 and Kategori is not null";
            SqlDataReader sdr = zl.Retrieve();
            ddlKetebalan.Items.Clear();
            ddlKetebalan.Items.Add(new ListItem("-- Pilih Ketebalan --", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlKetebalan.Items.Add(new ListItem(sdr["Kategori"].ToString()));
                }
            }
        }

        private void SelectBreakBMCharge(string strBreakBMCharge, DropDownList ddlNameID)
        {
            ddlNameID.ClearSelection();
            foreach (ListItem item in ddlNameID.Items)
            {
                if (item.Value == strBreakBMCharge)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        private void LoadPlantGroup()
        {
            ArrayList arrBreakBMGroup = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            ////ddlGrup.Items.Clear();
            arrBreakBMGroup = breakBMFacade.RetrieveBMGroup();
            ddlGrup.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (BreakBMGroup breakBMGroup in arrBreakBMGroup)
            {
                ddlGrup.Items.Add(new ListItem(breakBMGroup.ReguCode, breakBMGroup.ID.ToString()));
            }
        }

        private void LoadPlantGroupoff()
        {
            ArrayList arrBreakBMGroup = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            ////ddlGrup.Items.Clear();
            arrBreakBMGroup = breakBMFacade.RetrieveBMGroup();
            ddlgrupline.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (BreakBMGroup breakBMGroup in arrBreakBMGroup)
            {
                ddlgrupline.Items.Add(new ListItem(breakBMGroup.ReguCode, breakBMGroup.ID.ToString()));
            }
        }

        private void LoadPlantGroup1(string param)
        {
            ArrayList arrBreakBMGroup = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            ddlGrup.Items.Clear();
            arrBreakBMGroup = breakBMFacade.RetrieveBMGroupbyparam(param);
            //////ddlGrup.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (BreakBMGroup breakBMGroup in arrBreakBMGroup)
            {
                ddlGrup.Items.Add(new ListItem(breakBMGroup.ReguCode, breakBMGroup.ID.ToString()));
            }
            ddlGrup.SelectedIndex = 0;
        }


        private void LoadBreakBMProblem()
        {
            ArrayList arrBreakBMProblem = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            arrBreakBMProblem = breakBMFacade.RetrieveBMProblem();
            ddlProblem.Items.Add(new ListItem("-- Pilih Lokasi Problem --", "0"));
            foreach (BreakBMProblem breakBMProblem in arrBreakBMProblem)
            {
                ddlProblem.Items.Add(new ListItem(breakBMProblem.LokasiProblem, breakBMProblem.ID.ToString()));
            }
        }

        private void LoadBreakBMPlant()
        {
            ArrayList arrBreakBMPlant = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            arrBreakBMPlant = breakBMFacade.RetrieveBMPlant();
            ddlLine.Items.Add(new ListItem("-- Pilih Line --", "0"));
            foreach (BreakBMPlant breakBMPlant in arrBreakBMPlant)
            {
                ddlLine.Items.Add(new ListItem(breakBMPlant.PlanName, breakBMPlant.ID.ToString()));
            }
        }

        private void LoadBreakBMPlant1()
        {
            ArrayList arrBreakBMPlant = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            arrBreakBMPlant = breakBMFacade.RetrieveBMPlant();
            ddlLineNo.Items.Add(new ListItem("--Pilih Line --", "0"));
            foreach (BreakBMPlant breakBMPlant in arrBreakBMPlant)
            {
                ddlLineNo.Items.Add(new ListItem(breakBMPlant.PlanName, breakBMPlant.ID.ToString()));
            }
        }

        private void LoadDataGridBreakBM(ArrayList arrBreakBM)
        {
            this.GridView1.DataSource = arrBreakBM;
            this.GridView1.DataBind();
        }

        private ArrayList LoadGridBreakBM()
        {
            ArrayList arrBreakBM = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            arrBreakBM = breakBMFacade.Retrieve();
            if (arrBreakBM.Count > 0)
            {
                return arrBreakBM;
            }

            arrBreakBM.Add(new BreakBMFacade());
            return arrBreakBM;
        }

        private ArrayList LoadGridBreakBM2(int BM_PlantID)
        {
            ArrayList arrBreakBM = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            arrBreakBM = breakBMFacade.RetrieveByLine(BM_PlantID);
            if (arrBreakBM.Count > 0)
            {
                return arrBreakBM;
            }

            arrBreakBM.Add(new BreakBMFacade());
            return arrBreakBM;
        }

        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrBreakBM = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            if (txtSearch.Text == string.Empty)
                arrBreakBM = breakBMFacade.Retrieve();
            else
                arrBreakBM = breakBMFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrBreakBM.Count > 0)
            {
                return arrBreakBM;
            }

            arrBreakBM.Add(new BreakBMFacade());
            return arrBreakBM;
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridBreakBM(LoadGridByCriteria());
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            if (txtTglBreak.Text == string.Empty)
            {
                return;
            }

            BreakBM breakBM = new BreakBM();
            BreakBMFacade breakBMFacade = new BreakBMFacade();

            breakBM.ID = int.Parse(ViewState["id"].ToString());
            breakBM.LastModifiedBy = ((Users)Session["Users"]).UserName;
            int intResult = breakBMFacade.Delete(breakBM);
            if (breakBMFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridBreakBM(LoadGridBreakBM());
                InsertLog("Delete");
                clearForm();
            }
        }



        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (txtSearch.Text == string.Empty)
                LoadDataGridBreakBM(LoadGridBreakBM());
            else
                LoadDataGridBreakBM(LoadGridByCriteria());
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                BreakBMFacade GridPilih = new BreakBMFacade();
                BreakBM Pilih = GridPilih.RetrieveById(int.Parse(row.Cells[0].Text));
                if (GridPilih.Error == string.Empty && Pilih.ID > 0)
                {
                    ViewState["id"] = int.Parse(row.Cells[0].Text);
                    txtBreakDownNo.Text = Pilih.BreakDownNo.ToString();
                    txtTglBreak.Text = Pilih.TglBreak.ToString("dd-MM-yyyy");
                    //////txtOperationalSche.Text = Pilih.OperationalSche.ToString();
                    txtStartBD.Text = Pilih.StartBD.ToString("HH:mm");
                    txtFinishBD.Text = Pilih.FinishBD.ToString("HH:mm");
                    //txtFinishBD.Text = Pilih.FinaltyBD.ToString("HH:mm");
                    //////txtFrekBD.Text = Pilih.FrekBD.ToString();
                    txtSyarat.Text = Pilih.Syarat.ToString();
                    txtKet.Text = Pilih.Ket.ToString();
                    txtPinalti.Text = Pilih.Pinalti.ToString();
                    //////txtOperationalTime.Text = Pilih.OperationalTime.ToString();
                    txtBDTime.Text = Pilih.BDTime.ToString("HH:mm:ss");
                    SelectBreakBMCharge(Pilih.BreakBM_MasterChargeID.ToString(), ddlCharge);
                    SelectBreakBMCharge(Pilih.BM_plantGroupID.ToString(), ddlGrup);
                    SelectBreakBMCharge(Pilih.BM_PlantID.ToString(), ddlLine);
                    SelectBreakBMCharge(Pilih.BreakBM_MasterProblemID.ToString(), ddlProblem);
                    SelectBreakBMCharge(Pilih.GroupOff.ToString(), ddlgrupline);
                    //btnUpdate.Disabled = true;
                    // btnUpdate1.Disabled = false;
                    btnDelete.Enabled = false;
                }

            }

        }

        private void clearForm()
        {
            ViewState["id"] = null;
            txtBreakDownNo.Text = string.Empty;
            //////txtOperationalSche.Text = string.Empty;
            txtStartBD.Text = string.Empty;
            //////txtFrekBD.Text = string.Empty;
            //////txtTglBreak.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            txtTglBreak.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //////txtFrekBD.Text = string.Empty;
            txtFinishBD.Text = string.Empty;
            txtSyarat.Text = string.Empty;
            ddlCharge.SelectedIndex = 0;
            //////ddlGrup.SelectedIndex = 0;
            ddlLine.SelectedIndex = 0;
            //ddlgrupline.SelectedIndex = 0;
            //////txtOperationalTime.Text = string.Empty;
            txtKet.Text = string.Empty;
            txtBDTime.Text = string.Empty;
            ddlProblem.SelectedIndex = 0;
            txtPinalti.Text = string.Empty;
            txtBreakDownNo.Focus();

        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            btnUpdate.Disabled = false;
            //btnUpdate1.Disabled = true;
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";
            DateTime startTime = Convert.ToDateTime(txtTglBreak.Text + " " + txtStartBD.Text);
            DateTime endtime = (DateTime.Parse(txtFinishBD.Text).Hour - DateTime.Parse(txtStartBD.Text).Hour < 0) ?
            Convert.ToDateTime(txtTglBreak.Text + " " + txtFinishBD.Text).AddDays(1) : Convert.ToDateTime(txtTglBreak.Text + " " + txtFinishBD.Text);
            int maxID = 0;
            BreakBM cekLastID = new BreakBM();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            cekLastID = breakBMFacade.RetrieveMaxId();

            if (cekLastID.ID > 0)
                maxID = cekLastID.ID;
            else
                maxID = 1;

            breakBMFacade = new BreakBMFacade();
            BreakBM breakBM = new BreakBM();

            if (ViewState["id"] != null)
            {
                breakBM.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }
            else
            {
                breakBM.BreakDownNo = (maxID + 1).ToString().PadLeft(6, '0') + "/BREAKBM/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
            }
            breakBM.TglBreak = DateTime.Parse(txtTglBreak.Text);
            //////breakBM.OperationalSche = txtOperationalSche.Text;
            breakBM.StartBD = startTime;// DateTime.Parse(txtStartBD.Text);
            breakBM.FinishBD = endtime;// DateTime.Parse(txtFinishBD.Text);
            breakBM.FinaltyBD = endtime;
            //////breakBM.FrekBD = int.Parse(txtFrekBD.Text.ToString());
            breakBM.Syarat = txtSyarat.Text;
            breakBM.BM_plantGroupID = int.Parse(ddlGrup.SelectedValue.ToString());
            breakBM.BM_PlantID = int.Parse(ddlLine.SelectedValue.ToString());
            //////breakBM.OperationalTime = txtOperationalTime.Text;
            breakBM.Ket = txtKet.Text;
            //////breakBM.Pinalti = Convert.ToDecimal(txtPinalti.Text);
            breakBM.Pinalti = (txtPinalti.Text != string.Empty) ? Convert.ToDecimal(txtPinalti.Text) : 0;
            breakBM.BDTime = DateTime.Parse(txtBDTime.Text);
            breakBM.BreakBM_MasterProblemID = int.Parse(ddlProblem.SelectedValue.ToString());
            breakBM.BreakBM_MasterChargeID = int.Parse(ddlCharge.SelectedValue.ToString());
            breakBM.CreatedBy = ((Users)Session["Users"]).UserName;
            breakBM.GroupOff = ddlgrupline.SelectedItem.Text;
            breakBM.Ketebalan = ddlKetebalan.SelectedItem.Text;
            int intResult = 0;
            if (breakBM.ID > 0)
            {
                intResult = breakBMFacade.Update(breakBM);
            }
            else
            {
                intResult = breakBMFacade.Insert(breakBM);

                if (breakBMFacade.Error == string.Empty)
                {
                    if (intResult > 0)
                    {
                        txtBreakDownNo.Text = breakBM.BreakDownNo;
                    }
                }
            }

            if (breakBMFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridBreakBM(LoadGridBreakBM());
                InsertLog(strEvent);
                clearForm();
            }

        }

        protected void txtSyarat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int awal = 0;
                awal = txtSyarat.Text.Trim().Length - 2;
                LoadPlantGroup1(txtSyarat.Text.Trim().Substring(awal, 2));
                string Line = new Inifiles(Server.MapPath("~/App_Data/BreakDown.ini")).Read(txtSyarat.Text.TrimEnd().Substring(awal, 2).ToString(), "ConfigLine");
                string Charge = new Inifiles(Server.MapPath("~/App_Data/BreakDown.ini")).Read(txtSyarat.Text.TrimEnd().Substring(0, awal).ToString(), "ConfigCharge");
                string BMCh = new Inifiles(Server.MapPath("~/App_Data/BreakDown.ini")).Read("B", "ConfigCharge");
                //////string BMCh2 = new Inifiles(Server.MapPath("~/App_Data/BreakDown.ini")).Read("L", "ConfigCharge");
                ddlLine.SelectedValue = (Line.ToString() == string.Empty) ? "0" : Line;
                ddlCharge.SelectedValue = (Charge == string.Empty) ? "0" : Charge.ToString();
                ddlCharge.SelectedValue = (Charge == string.Empty && txtSyarat.Text.Trim().Length == 2) ? BMCh : Charge.ToString();
                //////ddlCharge.SelectedValue = (Charge == string.Empty && txtSyarat.Text.Trim().Length == 1) ? BMCh2 : Charge.ToString();
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                DisplayAJAXMessage(this, "Input Kode syarat salah " + strError);
            }

        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Modul BreakDownBM BreakBM";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtBreakDownNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            ///////if (eventLogFacade.Error == string.Empty)
            //////    clearForm();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            ////// //if (txtStartBD.Text == string.Empty)
            ////// //    return "Waktu Mulai BreakDown Tidak Boleh Kosong";
            ////// //else if (txtFinishBD.Text == string.Empty)
            ////// //    return "Waktu Berakhir BreakDown Tidak Boleh Kosong";
            ////// //if (txtSyarat.Text == string.Empty)
            ////// //    return "Syarat BreakDown Tidak Boleh Kosong";
            ////////if (ddlLine.SelectedIndex == 0)
            ////////     return "Pilih Lokasi Line";
            if (txtKet.Text == string.Empty)
                return "Keterangan Tidak Boleh Kosong";
            ////////else if (ddlCharge.SelectedIndex == 0)
            ////////    return "Pilih Lokasi Charge";
            else if (ddlKetebalan.SelectedIndex == 0)
                return "Pilih Ketebalan";
            else if (ddlProblem.SelectedIndex == 0)
                return "Pilih Lokasi Problem";
            return string.Empty;


        }
        protected void lbAddItem_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int depo = ((Users)Session["Users"]).UnitKerjaID;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (depo == 1)
                {
                    // Name Group
                    if (e.Row.Cells[6].Text == "0")
                    {
                        e.Row.Cells[6].Text = "None";
                    }

                    //LINE 1
                    if (e.Row.Cells[6].Text == "23")
                    {
                        e.Row.Cells[6].Text = "CA";
                    }
                    if (e.Row.Cells[6].Text == "24")
                    {
                        e.Row.Cells[6].Text = "CB";
                    }
                    if (e.Row.Cells[6].Text == "25")
                    {
                        e.Row.Cells[6].Text = "CC";
                    }
                    if (e.Row.Cells[6].Text == "26")
                    {
                        e.Row.Cells[6].Text = "CD";
                    }

                    //LINE 2
                    if (e.Row.Cells[6].Text == "27")
                    {
                        e.Row.Cells[6].Text = "CE";
                    }
                    if (e.Row.Cells[6].Text == "28")
                    {
                        e.Row.Cells[6].Text = "CF";
                    }
                    if (e.Row.Cells[6].Text == "29")
                    {
                        e.Row.Cells[6].Text = "CG";
                    }
                    if (e.Row.Cells[6].Text == "30")
                    {
                        e.Row.Cells[6].Text = "CH";
                    }

                    //LINE 3
                    if (e.Row.Cells[6].Text == "31")
                    {
                        e.Row.Cells[6].Text = "CI";
                    }
                    if (e.Row.Cells[6].Text == "32")
                    {
                        e.Row.Cells[6].Text = "CJ";
                    }
                    if (e.Row.Cells[6].Text == "33")
                    {
                        e.Row.Cells[6].Text = "CK";
                    }
                    if (e.Row.Cells[6].Text == "34")
                    {
                        e.Row.Cells[6].Text = "CL";
                    }

                    //LINE 4
                    if (e.Row.Cells[6].Text == "35")
                    {
                        e.Row.Cells[6].Text = "CM";
                    }
                    if (e.Row.Cells[6].Text == "36")
                    {
                        e.Row.Cells[6].Text = "CN";
                    }
                    if (e.Row.Cells[6].Text == "37")
                    {
                        e.Row.Cells[6].Text = "CO";
                    }
                    if (e.Row.Cells[6].Text == "38")
                    {
                        e.Row.Cells[6].Text = "CP";
                    }



                    ///next LIne Name
                    if (e.Row.Cells[7].Text == "0")
                    {
                        e.Row.Cells[7].Text = "None";
                    }
                    if (e.Row.Cells[7].Text == "1")
                    {
                        e.Row.Cells[7].Text = "Line 1";
                    }
                    if (e.Row.Cells[7].Text == "2")
                    {
                        e.Row.Cells[7].Text = "Line 2";
                    }
                    if (e.Row.Cells[7].Text == "3")
                    {
                        e.Row.Cells[7].Text = "Line 3";
                    }
                    if (e.Row.Cells[7].Text == "4")
                    {
                        e.Row.Cells[7].Text = "Line 4";
                    }

                    ///next Name Problem
                    if (e.Row.Cells[10].Text == "0")
                    {
                        e.Row.Cells[10].Text = "None";
                    }
                    if (e.Row.Cells[10].Text == "11")
                    {
                        e.Row.Cells[10].Text = "Pulping";
                    }
                    if (e.Row.Cells[10].Text == "12")
                    {
                        e.Row.Cells[10].Text = "DDR";
                    }
                    if (e.Row.Cells[10].Text == "13")
                    {
                        e.Row.Cells[10].Text = "Mixing";
                    }
                    if (e.Row.Cells[10].Text == "14")
                    {
                        e.Row.Cells[10].Text = "Forming";
                    }
                    if (e.Row.Cells[10].Text == "15")
                    {
                        e.Row.Cells[10].Text = "Scrap";
                    }
                    if (e.Row.Cells[10].Text == "16")
                    {
                        e.Row.Cells[10].Text = "Stacking";
                    }
                    if (e.Row.Cells[10].Text == "17")
                    {
                        e.Row.Cells[10].Text = "Pressing";
                    }
                    if (e.Row.Cells[10].Text == "18")
                    {
                        e.Row.Cells[10].Text = "Destacking";
                    }
                    if (e.Row.Cells[10].Text == "19")
                    {
                        e.Row.Cells[10].Text = "Null";
                    }
                    if (e.Row.Cells[10].Text == "20")
                    {
                        e.Row.Cells[10].Text = "Area Flucolant";
                    }

                    ///next Name Charge
                    if (e.Row.Cells[11].Text == "0")
                    {
                        e.Row.Cells[11].Text = "None";
                    }
                    if (e.Row.Cells[11].Text == "1")
                    {
                        e.Row.Cells[11].Text = "Mekanik";
                    }
                    if (e.Row.Cells[11].Text == "2")
                    {
                        e.Row.Cells[11].Text = "BoardMill";
                    }
                    if (e.Row.Cells[11].Text == "3")
                    {
                        e.Row.Cells[11].Text = "Elecktrik";
                    }
                    if (e.Row.Cells[11].Text == "4")
                    {
                        e.Row.Cells[11].Text = "Schedule";
                    }
                    if (e.Row.Cells[11].Text == "8")
                    {
                        e.Row.Cells[11].Text = "Logistik";
                    }
                    if (e.Row.Cells[11].Text == "9")
                    {
                        e.Row.Cells[11].Text = "Utility";
                    }
                }
                else if (depo == 7)
                {
                    // Name Group
                    if (e.Row.Cells[6].Text == "0")
                    {
                        e.Row.Cells[6].Text = "None";
                    }

                    //LINE 1
                    if (e.Row.Cells[6].Text == "1")
                    {
                        e.Row.Cells[6].Text = "KA";
                    }
                    if (e.Row.Cells[6].Text == "2")
                    {
                        e.Row.Cells[6].Text = "KB";
                    }
                    if (e.Row.Cells[6].Text == "3")
                    {
                        e.Row.Cells[6].Text = "KC";
                    }
                    if (e.Row.Cells[6].Text == "4")
                    {
                        e.Row.Cells[6].Text = "KD";
                    }

                    //LINE 2
                    if (e.Row.Cells[6].Text == "5")
                    {
                        e.Row.Cells[6].Text = "KE";
                    }
                    if (e.Row.Cells[6].Text == "6")
                    {
                        e.Row.Cells[6].Text = "KF";
                    }
                    if (e.Row.Cells[6].Text == "7")
                    {
                        e.Row.Cells[6].Text = "KG";
                    }
                    if (e.Row.Cells[6].Text == "8")
                    {
                        e.Row.Cells[6].Text = "KH";
                    }

                    //LINE 3
                    if (e.Row.Cells[6].Text == "9")
                    {
                        e.Row.Cells[6].Text = "KI";
                    }
                    if (e.Row.Cells[6].Text == "10")
                    {
                        e.Row.Cells[6].Text = "KJ";
                    }
                    if (e.Row.Cells[6].Text == "11")
                    {
                        e.Row.Cells[6].Text = "KK";
                    }
                    if (e.Row.Cells[6].Text == "12")
                    {
                        e.Row.Cells[6].Text = "KL";
                    }

                    //LINE 4
                    if (e.Row.Cells[6].Text == "13")
                    {
                        e.Row.Cells[6].Text = "KM";
                    }
                    if (e.Row.Cells[6].Text == "14")
                    {
                        e.Row.Cells[6].Text = "KN";
                    }
                    if (e.Row.Cells[6].Text == "15")
                    {
                        e.Row.Cells[6].Text = "KO";
                    }
                    if (e.Row.Cells[6].Text == "16")
                    {
                        e.Row.Cells[6].Text = "KP";
                    }

                    //LINE 5
                    if (e.Row.Cells[6].Text == "17")
                    {
                        e.Row.Cells[6].Text = "KQ";
                    }
                    if (e.Row.Cells[6].Text == "18")
                    {
                        e.Row.Cells[6].Text = "KR";
                    }
                    if (e.Row.Cells[6].Text == "19")
                    {
                        e.Row.Cells[6].Text = "KS";
                    }
                    if (e.Row.Cells[6].Text == "20")
                    {
                        e.Row.Cells[6].Text = "KT";
                    }

                    //LINE 6
                    if (e.Row.Cells[6].Text == "21")
                    {
                        e.Row.Cells[6].Text = "KU";
                    }
                    if (e.Row.Cells[6].Text == "22")
                    {
                        e.Row.Cells[6].Text = "KV";
                    }
                    if (e.Row.Cells[6].Text == "23")
                    {
                        e.Row.Cells[6].Text = "KW";
                    }
                    if (e.Row.Cells[6].Text == "24")
                    {
                        e.Row.Cells[6].Text = "KX";
                    }

                    ///next LIne Name
                    if (e.Row.Cells[7].Text == "0")
                    {
                        e.Row.Cells[7].Text = "None";
                    }
                    if (e.Row.Cells[7].Text == "1")
                    {
                        e.Row.Cells[7].Text = "Line 1";
                    }
                    if (e.Row.Cells[7].Text == "2")
                    {
                        e.Row.Cells[7].Text = "Line 2";
                    }
                    if (e.Row.Cells[7].Text == "3")
                    {
                        e.Row.Cells[7].Text = "Line 3";
                    }
                    if (e.Row.Cells[7].Text == "4")
                    {
                        e.Row.Cells[7].Text = "Line 4";
                    }
                    if (e.Row.Cells[7].Text == "5")
                    {
                        e.Row.Cells[7].Text = "Line 5";
                    }
                    if (e.Row.Cells[7].Text == "6")
                    {
                        e.Row.Cells[7].Text = "Line 6";
                    }


                    ///next Name Problem
                    if (e.Row.Cells[10].Text == "0")
                    {
                        e.Row.Cells[10].Text = "None";
                    }
                    if (e.Row.Cells[10].Text == "1")
                    {
                        e.Row.Cells[10].Text = "DDR";
                    }
                    if (e.Row.Cells[10].Text == "2")
                    {
                        e.Row.Cells[10].Text = "Scrap";
                    }
                    if (e.Row.Cells[10].Text == "3")
                    {
                        e.Row.Cells[10].Text = "Destacking";
                    }
                    if (e.Row.Cells[10].Text == "4")
                    {
                        e.Row.Cells[10].Text = "Pulping";
                    }
                    if (e.Row.Cells[10].Text == "5")
                    {
                        e.Row.Cells[10].Text = "Forming";
                    }
                    if (e.Row.Cells[10].Text == "6")
                    {
                        e.Row.Cells[10].Text = "Pressing";
                    }
                    if (e.Row.Cells[10].Text == "7")
                    {
                        e.Row.Cells[10].Text = "Mixing";
                    }
                    if (e.Row.Cells[10].Text == "8")
                    {
                        e.Row.Cells[10].Text = "Stacking";
                    }
                    if (e.Row.Cells[10].Text == "9")
                    {
                        e.Row.Cells[10].Text = "Null";
                    }
                    if (e.Row.Cells[10].Text == "10")
                    {
                        e.Row.Cells[10].Text = "Area Flucolant";
                    }

                    ///next Name Charge
                    if (e.Row.Cells[11].Text == "0")
                    {
                        e.Row.Cells[11].Text = "None";
                    }
                    if (e.Row.Cells[11].Text == "1")
                    {
                        e.Row.Cells[11].Text = "Mekanik";
                    }
                    if (e.Row.Cells[11].Text == "2")
                    {
                        e.Row.Cells[11].Text = "BoardMill";
                    }
                    if (e.Row.Cells[11].Text == "3")
                    {
                        e.Row.Cells[11].Text = "Elecktrik";
                    }
                    if (e.Row.Cells[11].Text == "4")
                    {
                        e.Row.Cells[11].Text = "Schedule";
                    }
                    if (e.Row.Cells[11].Text == "5")
                    {
                        e.Row.Cells[11].Text = "Logistik";
                    }
                    if (e.Row.Cells[11].Text == "6")
                    {
                        e.Row.Cells[11].Text = "Utility";
                    }
                }
                else
                {
                    // Name Group
                    if (e.Row.Cells[6].Text == "0")
                    {
                        e.Row.Cells[6].Text = "None";
                    }

                    //LINE 1
                    if (e.Row.Cells[6].Text == "23")
                    {
                        e.Row.Cells[6].Text = "JA";
                    }
                    if (e.Row.Cells[6].Text == "24")
                    {
                        e.Row.Cells[6].Text = "JB";
                    }
                    if (e.Row.Cells[6].Text == "25")
                    {
                        e.Row.Cells[6].Text = "JC";
                    }
                    if (e.Row.Cells[6].Text == "J6")
                    {
                        e.Row.Cells[6].Text = "JD";
                    }

                    //LINE 2
                    if (e.Row.Cells[6].Text == "27")
                    {
                        e.Row.Cells[6].Text = "JE";
                    }
                    if (e.Row.Cells[6].Text == "28")
                    {
                        e.Row.Cells[6].Text = "JF";
                    }
                    if (e.Row.Cells[6].Text == "29")
                    {
                        e.Row.Cells[6].Text = "JG";
                    }
                    if (e.Row.Cells[6].Text == "30")
                    {
                        e.Row.Cells[6].Text = "JH";
                    }

                    //LINE 3
                    if (e.Row.Cells[6].Text == "31")
                    {
                        e.Row.Cells[6].Text = "JI";
                    }
                    if (e.Row.Cells[6].Text == "32")
                    {
                        e.Row.Cells[6].Text = "JJ";
                    }
                    if (e.Row.Cells[6].Text == "33")
                    {
                        e.Row.Cells[6].Text = "JK";
                    }
                    if (e.Row.Cells[6].Text == "34")
                    {
                        e.Row.Cells[6].Text = "JL";
                    }

                    //LINE 4
                    if (e.Row.Cells[6].Text == "35")
                    {
                        e.Row.Cells[6].Text = "JM";
                    }
                    if (e.Row.Cells[6].Text == "36")
                    {
                        e.Row.Cells[6].Text = "JN";
                    }
                    if (e.Row.Cells[6].Text == "37")
                    {
                        e.Row.Cells[6].Text = "JO";
                    }
                    if (e.Row.Cells[6].Text == "38")
                    {
                        e.Row.Cells[6].Text = "JP";
                    }



                    ///next LIne Name
                    if (e.Row.Cells[7].Text == "0")
                    {
                        e.Row.Cells[7].Text = "None";
                    }
                    if (e.Row.Cells[7].Text == "1")
                    {
                        e.Row.Cells[7].Text = "Line 1";
                    }
                    if (e.Row.Cells[7].Text == "2")
                    {
                        e.Row.Cells[7].Text = "Line 2";
                    }
                    if (e.Row.Cells[7].Text == "3")
                    {
                        e.Row.Cells[7].Text = "Line 3";
                    }
                    if (e.Row.Cells[7].Text == "4")
                    {
                        e.Row.Cells[7].Text = "Line 4";
                    }

                    ///next Name Problem
                    if (e.Row.Cells[10].Text == "0")
                    {
                        e.Row.Cells[10].Text = "None";
                    }
                    if (e.Row.Cells[10].Text == "11")
                    {
                        e.Row.Cells[10].Text = "Pulping";
                    }
                    if (e.Row.Cells[10].Text == "12")
                    {
                        e.Row.Cells[10].Text = "DDR";
                    }
                    if (e.Row.Cells[10].Text == "13")
                    {
                        e.Row.Cells[10].Text = "Mixing";
                    }
                    if (e.Row.Cells[10].Text == "14")
                    {
                        e.Row.Cells[10].Text = "Forming";
                    }
                    if (e.Row.Cells[10].Text == "15")
                    {
                        e.Row.Cells[10].Text = "Scrap";
                    }
                    if (e.Row.Cells[10].Text == "16")
                    {
                        e.Row.Cells[10].Text = "Stacking";
                    }
                    if (e.Row.Cells[10].Text == "17")
                    {
                        e.Row.Cells[10].Text = "Pressing";
                    }
                    if (e.Row.Cells[10].Text == "18")
                    {
                        e.Row.Cells[10].Text = "Destacking";
                    }
                    if (e.Row.Cells[10].Text == "19")
                    {
                        e.Row.Cells[10].Text = "Null";
                    }
                    if (e.Row.Cells[10].Text == "20")
                    {
                        e.Row.Cells[10].Text = "Area Flucolant";
                    }

                    ///next Name Charge
                    if (e.Row.Cells[11].Text == "0")
                    {
                        e.Row.Cells[11].Text = "None";
                    }
                    if (e.Row.Cells[11].Text == "1")
                    {
                        e.Row.Cells[11].Text = "Mekanik";
                    }
                    if (e.Row.Cells[11].Text == "2")
                    {
                        e.Row.Cells[11].Text = "BoardMill";
                    }
                    if (e.Row.Cells[11].Text == "3")
                    {
                        e.Row.Cells[11].Text = "Elecktrik";
                    }
                    if (e.Row.Cells[11].Text == "4")
                    {
                        e.Row.Cells[11].Text = "Schedule";
                    }
                    if (e.Row.Cells[11].Text == "8")
                    {
                        e.Row.Cells[11].Text = "Logistik";
                    }
                    if (e.Row.Cells[11].Text == "9")
                    {
                        e.Row.Cells[11].Text = "Utility";
                    }
                }
            }

        }

        //protected void btnList_ServerClick(object sender, EventArgs e)
        //{

        //}

        protected void ddlLineNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataGridBreakBM(LoadGridBreakBM2(int.Parse(ddlLineNo.SelectedValue)));
        }

        //protected void ChkByLine_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ChkByLine.Checked == true)
        //    {
        //        ddlLineNo.Visible = true;
        //        //BreakBMFacade Line = new BreakBMFacade();
        //        //ArrayList arrLine = new ArrayList();
        //        //ddlLineNo.Items.Clear();
        //        //arrLine = Line.RetrieveBreakLine(ddlLineNo.SelectedIndex);
        //        //foreach (BreakBM line in arrLine)
        //        //{
        //        //    ddlLineNo.Items.Add(new ListItem(line.ID));
        //        //}
        //        //LoadDataGridBreakBM(LoadGridBreakBM());
        //    }
        //    else
        //    {
        //        //ddlLineNo.Items.Clear();//ddlPartno.Items.Clear();
        //        LoadDataGridBreakBM(LoadGridBreakBM());//LoadDataGridSerah(Calendar1.SelectedDate);
        //        ddlLineNo.Visible = false;//ddlPartno.Visible = false;
        //    }

        //}

    }
}