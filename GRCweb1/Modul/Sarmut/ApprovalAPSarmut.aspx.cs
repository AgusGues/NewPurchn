using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
//using DefectFacade;
using Factory;
using DataAccessLayer;
using System.IO;
using BasicFrame.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.Sarmut
{
    public partial class ApprovalAPSarmut : System.Web.UI.Page
    {
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);
                FacadePrsaXx AFacade = new FacadePrsaXx();
                SPD_PrsAxx spd = new SPD_PrsAxx();
                LoadDataGridViewItem(LoadDataGrid());
                string UserID = string.Empty;
                UserID = user.ID.ToString();
                string usertype = AFacade.GetUserType(UserID);
                Session["usertype"] = usertype;
                LoadDept();
                LoadOpenAP();
                LoadDataAP();
                btnApprove.Enabled = false;
                btnUnApprove.Enabled = false;
            }
            if (anNo.Value != string.Empty)
            {
                Users user = (Users)Session["Users"];
                string[] ListAnalisaData = anNo.Value.Split(',');
                string[] ListOpenAP = ListAnalisaData.Distinct().ToArray();
                int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                LoadOpenAP(ListOpenAP[0].ToString());
                btnApprove.Enabled = true;
                btnNext.Enabled = (ListOpenAP.Count() > 1) ? true : false;
                ViewState["index"] = idx;
            }
            else
            {
                btnApprove.Enabled = false;
                btnUnApprove.Enabled = false;
                btnNext.Enabled = false;
            }
            string[] ListOpenAnax = anNo.Value.Split(',');
            string[] ListOpenAnad = ListOpenAnax.Distinct().ToArray();
            int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
            btnNext.Enabled = ((idxx - 1) >= ListOpenAnad.Count()) ? false : true;
            ViewState["index"] = idxx;
            if (Request.QueryString["AnNo"] != null)
            {
                LoadOpenAP(Request.QueryString["AnNo"].ToString());
            }
            if ((Session["usertype"].ToString().Trim() == "iso"))
            {
                IsoOnly.Visible = true;
            }
            else
            {
                IsoOnly.Visible = false;
            }
        }



        private void LoadDataGridViewItem(ArrayList arrAnalisaData)
        {
            this.GridView1.DataSource = arrAnalisaData;
            this.GridView1.DataBind();
        }

        private ArrayList LoadDataGrid()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrAdata = new ArrayList();
            FacadePrsaXx FacadeF = new FacadePrsaXx();
            //string UserInput1 = Session["UserInput1"].ToString();
            string UserHead = users.ID.ToString();
            arrAdata = FacadeF.RetrieveAP1(UserHead);
            GridView1.DataSource = arrAdata;
            GridView1.DataBind();

            return arrAdata;
        }

        protected void lstdt_DataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string[] ListAnalisa = anNo.Value.Split(',');
            string[] ListOpenAP = ListAnalisa.Distinct().ToArray();
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
            idx = (idx > ListOpenAP.Count() - 1) ? 0 : idx;
            btnNext.Enabled = ((idx) == ListOpenAP.Count()) ? false : true;
            try
            {
                ViewState["index"] = idx;
                LoadOpenAP(ListOpenAP[idx].ToString());
            }
            catch
            {
                LoadOpenAP(ListOpenAP[0].ToString());
                ViewState["index"] = 0;
            }
            btnPrev.Enabled = (idx > 0) ? true : false;
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            string[] ListAnalisa = anNo.Value.Split(',');
            string[] ListOpenAP = ListAnalisa.Distinct().ToArray();
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;
            btnPrev.Enabled = (idx > 0) ? true : false;
            btnNext.Enabled = ((idx + 1) == ListOpenAP.Count()) ? false : true;
            LoadOpenAP(ListOpenAP[idx].ToString());
            ViewState["index"] = idx;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            #region Yang Lama
            for (int i = 0; i < lstAppAP.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lstAppAP.Items[i].FindControl("chkprs");
                if (chk.Checked == true)
                {
                    //int intResult = 0;
                    Users user = (Users)Session["Users"];
                    SPD_PrsAxx spd = new SPD_PrsAxx();
                    FacadePrsaXx fcdx = new FacadePrsaXx();
                    string strError = string.Empty;
                    string UserID = user.ID.ToString();
                    int Apv = Convert.ToInt32(Session["Apv"].ToString());

                    spd.Apv = Apv;
                    spd.ID = int.Parse(chk.ToolTip.ToString());
                    spd.User_ID = int.Parse(UserID);
                    spd.LastModifiedBy = user.UserName;
                    int intResult = 0;
                    if (spd.ID > 0)
                    {
                        intResult = fcdx.Update(spd);
                    }
                    //int rst = fcdx.Update(spd);
                    LoadOpenAP();
                    if (strError == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Approval berhasil");
                    }

                }
            }
            LoadOpenAP();
            LoadDataAP();
            btnApprove.Enabled = false;
            btnUnApprove.Enabled = false;
            #endregion
            #region Baru
            //Users user = ((Users)Session["Users"]);
            //ApproveAnData();
            //LoadOpenAP();
            //LoadDataGridViewItem(LoadDataGrid());
            //FacadePrsaXx facde = new FacadePrsaXx();
            //SPD_PrsAxx spdx = new SPD_PrsAxx();
            //string UserID = string.Empty;
            //UserID = user.ID.ToString();
            //string usertype = facde.GetUserType(UserID);
            //Session["usertype"] = usertype;
            //AutoNext();
            #endregion
        }

        private void AutoNext()
        {
            if (btnNext.Enabled == true)
            {
                btnNext_Click(null, null);
            }
            else if (btnPrev.Enabled == true)
            {
                btnPrev_Click(null, null);
            }
            else
            {
                Response.Redirect("ApprovalAPSarmut.aspx");
            }
        }

        private void ApproveAnData()
        {
            Users user = (Users)Session["Users"];
            SPD_PrsAxx spdana = new SPD_PrsAxx();
            FacadePrsaXx FacadeSpdx = new FacadePrsaXx();
            string strError = string.Empty;
            string UserID = user.ID.ToString();
            int Apv = Convert.ToInt32(Session["Apv"].ToString());

            spdana.Apv = Apv;
            //spdana.ID = int.Parse(txtID.Text);
            spdana.User_ID = int.Parse(UserID);
            spdana.CreatedBy = user.UserName;

            int intResult = 0;
            if (spdana.ID > 0)
            {
                intResult = FacadeSpdx.Update(spdana);
            }
            if (strError == string.Empty)
            {
                DisplayAJAXMessage(this, "Approval berhasil");
            }
            LoadDataGridViewItem(LoadDataGrid());
            AutoNext();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadDataGridViewItem(LoadDataGrid());
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ApvAP = Session["ApvAP"].ToString();
            Users user = (Users)Session["Users"];
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                FormData(GridView1.Rows[index].Cells[1].Text);
                GridViewRow row = GridView1.Rows[index];
                ViewState["id"] = int.Parse(row.Cells[0].Text);
                //txtNoAnalisa.Text = CekString(row.Cells[1].Text);
                //txtDate.Text = CekString(row.Cells[2].Text);
                //txtApv.Text = CekString(row.Cells[8].Text);
                //txtSarmutPerusahaanx.Text = CekString(row.Cells[3].Text);
                //txtSarmutDeptx.Text = CekString(row.Cells[4].Text);
                //txtID.Text = CekString(row.Cells[0].Text);
                //string idAn = txtNoAnalisa.Text;
                //Session["idAn"] = idAn;

            }
        }

        protected void FormData(string anNo)
        {
            ArrayList arrAnalisaData = new ArrayList();
            SPD_PrsAxx AnData = new SPD_PrsAxx();
            FacadePrsaXx facadeAdata = new FacadePrsaXx();
            AnData = facadeAdata.RetrieveAPNum1(anNo);
            arrAnalisaData.Add(AnData);
            Clearform();
            sarmutnotxt.Text = AnData.AnNo;
            txtAsarmut_Date0.SelectedDate = AnData.TglAnalisa;
            txtNamaDept.Text = AnData.Dept;
            txtSasaranMutuPerusahaan.Text = AnData.SarMutPerusahaan;
            typeID.Text = AnData.SarmutPID.ToString();
            txtSarmutDepartemen.Text = AnData.SarmutDepartemen;
            IDx.Text = AnData.SarmutDeptID.ToString();
            txtTargetx.Text = AnData.TargetVID.ToString();
            Label1.Text = AnData.Satuan;
            txtParam.Text = AnData.Param;
            txtBulan1.Text = AnData.NamaBulan;
            txtbulan.Text = AnData.Bulan.ToString();
            txtTahun1.Text = AnData.Tahun.ToString();
            txttahun.Text = AnData.Tahun.ToString();
            txtActual.Text = AnData.Actual.ToString();
            txtSatuan.Text = AnData.Satuan;
            chkTTercapai.Checked = false;
            chkTercapai.Checked = false;
            if (AnData.Pencapaian == "Tidak Tercapai")
                chkTTercapai.Checked = true;
            if (AnData.Pencapaian == "Tercapai")
                chkTercapai.Checked = true;

            LoadPerbaikan(anNo);
            LoadPencegahan(anNo);

            ArrayList arrPenyebab = new ArrayList();
            FacadePrsaXx facadeP = new FacadePrsaXx();
            arrPenyebab = facadeP.RetrieveByNo2(anNo);
            foreach (SPD_PrsAxx Pdetail in arrPenyebab)
            {
                if (Pdetail.Penyebab.Trim() == "Lingkungan")
                {
                    chkLingkungan.Checked = true;
                    txtLingkungan.Enabled = true;
                    txtLingkungan.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Manusia")
                {
                    chkManusia.Checked = true;
                    txtManusia.Enabled = true;
                    txtManusia.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Material")
                {
                    chkMaterial.Checked = true;
                    txtMaterial.Enabled = true;
                    txtMaterial.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Mesin")
                {
                    chkMesin.Checked = true;
                    txtMesin.Enabled = true;
                    txtMesin.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Metode")
                {
                    chkMetode.Checked = true;
                    txtMetode.Enabled = true;
                    txtMetode.Text = Pdetail.Uraian;
                }
            }
            LoadDatasmt1((int.Parse(txttahun.Text.ToString())), (int.Parse(typeID.Text.ToString())), (int.Parse(IDx.Text.ToString())));
        }

        private void LoadDatasmt1(int tahun, int xxx, int xxxx)
        {
            if (Convert.ToInt32(IDx.Text) > 0)
            {
                #region #1
                string semester = string.Empty;
                //tahun = 2020;

                if (Convert.ToInt32(txtbulan.Text) >= 7)
                    semester = " A.Tahun=" + tahun + " and A.Bulan in (7,8,9,10,11,12)  and A.RowStatus>-1 " +
                                 " and B.SarmutPID=" + xxx + " and B.ID=" + xxxx + "";
                if (Convert.ToInt32(txtbulan.Text) <= 6)
                    semester = " A.Tahun=" + tahun + " and A.Bulan in (1,2,3,4,5,6)  and A.RowStatus>-1 " +
                                 " and B.SarmutPID=" + xxx + " and B.ID=" + xxxx + "";
                Users users = (Users)Session["Users"];
                ArrayList arrData = new ArrayList();
                //newdata = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " select B.ID,A.Tahun,A.Bulan,B.SarmutDepartemen,A.Actual,C.ParamID,E.[Param],isnull(G.Satuan,0)Satuan,F.TargetV1 Target, " +
                                 " C.ID IDx,c.DeptID,D.dptID From SPD_Trans A  inner join SPD_Departemen B on B.ID=A.SarmutDeptID   inner join SPD_Perusahaan c on B.SarmutPID=C.ID " +
                                 " inner join SPD_Dept D on C.DeptID=D.ID  inner join SPD_Parameter E on E.ID=C.ParamID  inner join SPD_TargetV F on F.ID=C.TargetVID " +
                                 " inner join SPD_Satuan G on G.ID=C.SatuanID  where " + semester + " order by Bulan Asc ";
                //" A.Tahun=" + tahun + "  and A.RowStatus>-1 " +
                //" and B.SarmutPID=" + xxx + " and B.ID=" + xxxx + " order by Bulan ";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new SPD_PrsAxx
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                            Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                            SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
                            //SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString(),
                            Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                            Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        });
                    }
                }


                string[] x = new string[arrData.Count];
                decimal[] z = new decimal[arrData.Count];
                decimal[] y = new decimal[arrData.Count];
                decimal[] xy = new decimal[arrData.Count];
                int i = 0;
                foreach (SPD_PrsAxx lo in arrData)
                {
                    y[i] = lo.Actual;
                    x[i] = lo.Bulan.ToString();
                    //z[i] = lo.Target;
                    xy[i] = lo.Actual;
                    i = i + 1;
                }
                Chart1.Legends.Add("Actual");
                Chart1.Series.Add("Actual");
                Chart1.Series[0].IsVisibleInLegend = true;
                Chart1.Series[0].IsValueShownAsLabel = true;
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.Series[0].ChartType = SeriesChartType.Column;
                Chart1.Legends[0].Enabled = true;
                Chart1.Legends[0].Docking = Docking.Bottom;
                Chart1.Legends.Add("Target");
                Chart1.Series.Add("Target");
                Chart1.Series[1].Points.DataBindXY(x, xy);
                Chart1.Series[1].ChartType = SeriesChartType.Line;
                Chart1.Legends[1].Enabled = true;
                Chart1.Legends[1].Docking = Docking.Bottom;
                Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
                Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
                Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
                Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
                Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
                if (Convert.ToInt32(txtbulan.Text) <= 6)
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JANUARI - JUNI  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JANUARI - JUNI  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }
                else
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JULI - DESEMBER  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JULI - DESEMBER  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }


                lstdt.DataSource = arrData;
                lstdt.DataBind();

                #endregion
            }
            else
            {
                #region #2
                string semesterx = string.Empty;
                if (Convert.ToInt32(txtbulan.Text) >= 7)
                    semesterx = " A.Tahun=" + tahun + " and A.Bulan in (7,8,9,10,11,12) and A.RowStatus>-1 and " +
                                 " SarmutPID=" + xxx + " order by Bulan";
                if (Convert.ToInt32(txtbulan.Text) <= 6)
                    semesterx = " A.Tahun=" + tahun + " and A.Bulan in (1,2,3,4,5,6) and A.RowStatus>-1 and " +
                                 " SarmutPID=" + xxx + " order by Bulan";
                Users users = (Users)Session["Users"];
                ArrayList arrData = new ArrayList();
                //newdata = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " select A.SarmutPID ID,A.Tahun,A.Bulan,B.SarMutPerusahaan,A.Actual,C.Param,D.Satuan,E.TargetV1[Target],B.DeptID,B.TypeSarmutID from SPD_TransPrs A " +
                                 " inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                                 " inner join SPD_Parameter C on B.ParamID=C.ID " +
                                 " inner join SPD_Satuan D on D.ID=B.SatuanID " +
                                 " inner join SPD_TargetV E on E.ID=B.TargetVID " +
                                 " where " + semesterx + " ";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new SPD_PrsAxx
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                            Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                            //SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
                            SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString(),
                            Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                            Target = Convert.ToDecimal(sdr["Target"].ToString()),
                            TypeSarmutID = Convert.ToInt32(sdr["TypeSarmutID"].ToString()),
                        });
                    }
                }

                string[] x = new string[arrData.Count];
                decimal[] z = new decimal[arrData.Count];
                decimal[] y = new decimal[arrData.Count];
                decimal[] xy = new decimal[arrData.Count];
                int i = 0;
                foreach (SPD_PrsAxx lo in arrData)
                {
                    y[i] = lo.Actual;
                    //y[i] = lo.Target;
                    x[i] = lo.Bulan.ToString();
                    //z[i] = lo.Target;
                    xy[i] = lo.Actual;
                    //xy[i] = lo.Target;
                    i = i + 1;
                }
                Chart1.Legends.Add("Actual");
                Chart1.Series.Add("Actual");
                Chart1.Series[0].IsVisibleInLegend = true;
                Chart1.Series[0].IsValueShownAsLabel = true;
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.Series[0].ChartType = SeriesChartType.Column;
                Chart1.Legends[0].Enabled = true;
                Chart1.Legends[0].Docking = Docking.Bottom;
                Chart1.Legends.Add("Target");
                Chart1.Series.Add("Target");
                Chart1.Series[1].Points.DataBindXY(x, xy);
                Chart1.Series[1].ChartType = SeriesChartType.Line;
                Chart1.Legends[1].Enabled = true;
                Chart1.Legends[1].Docking = Docking.Bottom;
                Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
                Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
                Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
                Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
                Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
                if (Convert.ToInt32(txtbulan.Text) <= 6)
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JANUARI - JUNI  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JANUARI - JUNI  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }
                else
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JULI - DESEMBER  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JULI - DESEMBER  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }
                lstdt.DataSource = arrData;
                lstdt.DataBind();

                #endregion
            }

        }

        protected void LoadPerbaikan(string AnNo)
        {

            if (AnNo.Length == 1)
                return;
            Session["perbaikan"] = null;
            ArrayList arrPerbaikan = new ArrayList();
            FacadePrsaXx PerbaikanF = new FacadePrsaXx();
            arrPerbaikan = PerbaikanF.RetrieveByNo1(AnNo, "Perbaikan");
            Session["perbaikan"] = arrPerbaikan;
            Repeater1.DataSource = arrPerbaikan;
            Repeater1.DataBind();
        }

        protected void LoadPencegahan(string AnNo)
        {

            if (AnNo.Length == 1)
                return;
            Session["pencegahan"] = null;
            ArrayList arrPerbaikan = new ArrayList();
            FacadePrsaXx PerbaikanF = new FacadePrsaXx();
            arrPerbaikan = PerbaikanF.RetrieveByNo1(AnNo, "Pencegahan");
            Session["pencegahan"] = arrPerbaikan;
            Repeater2.DataSource = arrPerbaikan;
            Repeater2.DataBind();
        }

        private void Clearform()
        {
            sarmutnotxt.Text = string.Empty;
            txtAsarmut_Date0.SelectedDate = DateTime.Now;
            txtNamaDept.Text = string.Empty;
            //txtSarmutPerusahaanx.Text = string.Empty;
        }

        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void chkList_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkList.Checked == true)
            //{
            //    Panel3.Visible = true;
            //    //PanelFormDetail.Visible = true;
            //}
            //else
            //{
            //    Panel3.Visible = false;
            //    //PanelFormDetail.Visible = false;
            //}
        }

        protected void chkDetail_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkDetail.Checked == true)
            //    PanelFormDetail.Visible = true;
            //else
            //    PanelFormDetail.Visible = false;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadDept()
        {
            ddlDept.Items.Clear();
            ArrayList arrDept = new ArrayList();
            RMM_Dept sarmutdept = new RMM_Dept();
            RMM_DeptFacade deptFacade = new RMM_DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDept.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (RMM_Dept dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
        }

        private void LoadOpenAP(string ANnO)
        {
            Users user = (Users)Session["Users"];
            FacadePrsaXx fap = new FacadePrsaXx();
            SPD_PrsAxx spd = new SPD_PrsAxx();
            string UserInput = Session["UserInput"].ToString();
            spd = fap.RetrieveAPNum(ANnO, UserInput);

            //txtNoAnalisa.Text = spd.AnNo;
            //txtDate.Text = spd.TglAnalisa.ToString("dd-MMM-yyyy");
            //txtSarmutPerusahaanx.Text = spd.SarMutPerusahaan;
            //txtSarmutDeptx.Text = spd.SarmutDepartemen;
            //txtApv.Text = spd.Approval;
            //txtID.Text = spd.ID.ToString();
            //if (txtNoAnalisa.Text.Trim() != string.Empty)
            //    FormData(txtNoAnalisa.Text.Trim());
        }

        private void LoadOpenAP()
        {
            Users user = ((Users)Session["Users"]);
            ArrayList arrAp = new ArrayList();
            FacadePrsaXx FacadeX = new FacadePrsaXx();
            SPD_PrsAxx spd = new SPD_PrsAxx();
            string UserID = string.Empty;
            UserID = user.ID.ToString();
            string Apv = FacadeX.GetApv(UserID);
            string ApvAP = FacadeX.GetStatusApv(UserID);
            Session["Apv"] = Apv;
            Session["ApvAP"] = ApvAP;
            string UserInput = FacadeX.GetUserI(UserID);
            string UserInput1 = FacadeX.GetUserIGrid(UserID);
            Session["UserInput"] = UserInput;
            Session["UserInput1"] = UserInput1;
            arrAp = FacadeX.RetrieveOpenAPHeader(UserInput, Apv);
            foreach (SPD_PrsAxx spdx in arrAp)
            {
                if (spdx.AnNo != string.Empty)
                {
                    anNo.Value += spdx.AnNo + ",";
                }
            }

        }


        private ArrayList LoadDataAP()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrap = new ArrayList();
            FacadePrsaXx facadex = new FacadePrsaXx();
            string UserInput1 = Session["UserInput1"].ToString();
            string UserHead = users.ID.ToString();
            arrap = facadex.RetrieveAP(UserHead, UserInput1);
            lstAppAP.DataSource = arrap;
            lstAppAP.DataBind();
            return arrap;
        }

        protected void lstAppAP_DataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void lstAppAP_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string cmd = e.CommandName.ToString();
            string AnNo = (e.CommandArgument.ToString());
            //int ID = int.Parse(e.CommandArgument.ToString());
            SPD_PrsAxx AnData = new FacadePrsaXx().RetrieveAPNum1(AnNo);
            switch (cmd)
            {
                case "edit":
                    //clearForm();
                    sarmutnotxt.Text = AnData.AnNo;
                    txtAsarmut_Date0.SelectedDate = AnData.TglAnalisa;
                    txtNamaDept.Text = AnData.Dept;
                    txtSasaranMutuPerusahaan.Text = AnData.SarMutPerusahaan;
                    typeID.Text = AnData.SarmutPID.ToString();
                    txtSarmutDepartemen.Text = AnData.SarmutDepartemen;
                    IDx.Text = AnData.SarmutDeptID.ToString();
                    txtTargetx.Text = AnData.TargetVID.ToString();
                    Label1.Text = AnData.Satuan;
                    txtParam.Text = AnData.Param;
                    txtBulan1.Text = AnData.NamaBulan;
                    txtbulan.Text = AnData.Bulan.ToString();
                    txtTahun1.Text = AnData.Tahun.ToString();
                    txttahun.Text = AnData.Tahun.ToString();
                    txtActual.Text = AnData.Actual.ToString();
                    txtSatuan.Text = AnData.Satuan;
                    chkTTercapai.Checked = false;
                    chkTercapai.Checked = false;
                    if (AnData.Pencapaian == "Tidak Tercapai")
                        chkTTercapai.Checked = true;
                    if (AnData.Pencapaian == "Tercapai")
                        chkTercapai.Checked = true;
                    LoadPerbaikan(AnNo);
                    LoadPencegahan(AnNo);

                    ArrayList arrPenyebab = new ArrayList();
                    FacadePrsaXx facadeP = new FacadePrsaXx();
                    arrPenyebab = facadeP.RetrieveByNo2(AnNo);
                    foreach (SPD_PrsAxx Pdetail in arrPenyebab)
                    {
                        if (Pdetail.Penyebab.Trim() == "Lingkungan")
                        {
                            chkLingkungan.Checked = true;
                            txtLingkungan.Enabled = true;
                            txtLingkungan.Text = Pdetail.Uraian;
                        }
                        if (Pdetail.Penyebab.Trim() == "Manusia")
                        {
                            chkManusia.Checked = true;
                            txtManusia.Enabled = true;
                            txtManusia.Text = Pdetail.Uraian;
                        }
                        if (Pdetail.Penyebab.Trim() == "Material")
                        {
                            chkMaterial.Checked = true;
                            txtMaterial.Enabled = true;
                            txtMaterial.Text = Pdetail.Uraian;
                        }
                        if (Pdetail.Penyebab.Trim() == "Mesin")
                        {
                            chkMesin.Checked = true;
                            txtMesin.Enabled = true;
                            txtMesin.Text = Pdetail.Uraian;
                        }
                        if (Pdetail.Penyebab.Trim() == "Metode")
                        {
                            chkMetode.Checked = true;
                            txtMetode.Enabled = true;
                            txtMetode.Text = Pdetail.Uraian;
                        }
                    }
                    LoadDatasmt1((int.Parse(txttahun.Text.ToString())), (int.Parse(typeID.Text.ToString())), (int.Parse(IDx.Text.ToString())));
                    break;
                case "delete":
                    //LoadingTime objLd = new LoadingTime();
                    //objLd.ID = ID;
                    //btnGetFromDeviceIn.Enabled = false;
                    //objLd.LastModifiedBy = ((Users)Session["Users"]).UserName.ToString();
                    //int res = new LoadingTimeFacade().Delete(objLd);
                    //if (res > -1)
                    //{
                    //    objLd.RowStatus = -1;
                    //    objLd.TglIn = ld.TglIn;
                    //    objLd.TglOut = ld.TglOut;
                    //    int rst = new LoadingTimeFacade().UpdateLoadingTime1D(objLd);
                    //    LoadLoadingData(string.Empty);
                    //}
                    break;
            }
        }

        protected void chk_CheckedChangePrs(object sender, EventArgs e)
        {
            SPD_PrsAxx ap = new SPD_PrsAxx();
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked == true)
            {
                btnApprove.Enabled = true; btnUnApprove.Enabled = true;

            }
            else if (chk.Checked == false)
            {
                btnApprove.Enabled = false; btnUnApprove.Enabled = false;
            }
        }

        protected void chk_CheckedChange(object sender, EventArgs e)
        {
            int i = 0;
            string transID = string.Empty;
            foreach (RepeaterItem objDetail in lstAppAP.Items)
            {
                CheckBox chk = (CheckBox)lstAppAP.Items[i].FindControl("chkprs");
                chk.Checked = chkAll.Checked;
                transID = chk.ToolTip;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                SqlDataReader sdr = zl.Retrieve();
                i++;

                btnApprove.Enabled = true;
                btnUnApprove.Enabled = true;
            }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListAPDept();
        }

        private void ListAPDept()
        {
            Users users = (Users)Session["Users"];
            string strSQuery = string.Empty;
            FacadePrsaXx fcdx = new FacadePrsaXx();
            SPD_PrsAxx spd = new SPD_PrsAxx();
            string UserHead = users.ID.ToString();
            ArrayList arrData = new ArrayList();
            arrData = fcdx.RetrieveDept(ddlDept.SelectedValue, UserHead);
            lstAppAP.DataSource = arrData;
            lstAppAP.DataBind();
        }
    }
}

public class FacadePrsaXx
{
    public string strError = string.Empty;
    private ArrayList arrData = new ArrayList();
    private List<SqlParameter> sqlListParam;
    private SPD_PrsAxx objSPD = new SPD_PrsAxx();

    public FacadePrsaXx()
        : base()
    {

    }

    public int Update(object objDomain)
    {
        try
        {
            objSPD = (SPD_PrsAxx)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", objSPD.ID));
            //sqlListParam.Add(new SqlParameter("@DeptID", objRMM.DeptID));
            sqlListParam.Add(new SqlParameter("@Apv", objSPD.Apv));
            sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSPD.LastModifiedBy));
            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "AP_UpdateApv");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }

    public string GetUserType(string userID)
    {
        string strSQL = "select keterangan from SPD_Users where user_id=" + userID;
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        string usertype = string.Empty;
        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                usertype = sqlDataReader["keterangan"].ToString();
            }
        }
        return usertype;
    }

    public SPD_PrsAxx RetrieveAPNum(string AnNo, string UserInput)
    {
        string strSQL = " select A.ID,A.AnNo,A.TglAnalisa,A.SarmutPID,A.SarmutDeptID,B.SarMutPerusahaan,(Select Dept from SPD_Dept where ID=A.DeptID)Dept,C.SarmutDepartemen,A.TargetVID,A.Actual, " +
                                            " case when A.Kesim=2 then 'Tidak Tercapai' end Pencapaian, " +
                                            " Case when A.Apv=0 Then 'Open' When A.Apv is null then 'Open' " +
                                            " when A.Apv=1 then 'Mgr Dept' when A.Apv=2 then 'ISO' when A.CLosed=1 then 'Close' end Approval " +
                                            " from SPD_Analisa A inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                                            " left join  SPD_Departemen C on A.SarmutDeptID=C.ID " +
                                            " where A.RowStatus > -1 " +
                                            " and A.AnNo='" + AnNo + " '";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrData = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObjectHeaderAP(sqlDataReader);
            }
        }

        return new SPD_PrsAxx();
    }

    public SPD_PrsAxx RetrieveAPNum1(string AnNo)
    {
        string strSQL = "   select A.ID,A.AnNo,A.TglAnalisa,A.Bulan, " +
                            " case when A.Bulan=1 then 'Januari' " +
                                 " when A.Bulan=2 then 'Februari' " +
                                 " when A.Bulan=3 then 'Maret' " +
                                 " when A.Bulan=4 then 'April' " +
                                 " when A.Bulan=5 then 'Mei' " +
                                 " when A.Bulan=6 then 'Juni' " +
                                 " when A.Bulan=7 then 'Juli' " +
                                 " when A.Bulan=8 then 'Agustus' " +
                                 " when A.Bulan=9 then 'September' " +
                                 " when A.Bulan=10 then 'Oktober' " +
                                 " when A.Bulan=11 then 'November' " +
                                 " when A.Bulan=12 then 'Desember' end NamaBulan, " +
                                 " A.Tahun,A.SarmutPID,A.SarmutDeptID,B.SarMutPerusahaan,(Select Dept from SPD_Dept where ID=A.DeptID)Dept,C.SarmutDepartemen, " +
                                 " A.TargetVID,F.[Param],G.Satuan,A.Actual,  case when A.Kesim=2 then 'Tidak Tercapai' end Pencapaian, " +
                                 " Case when A.Apv=0 Then 'Open' When A.Apv is null then 'Open'  when A.Apv=1 then 'Mgr Dept' when A.Apv=2 then 'ISO'  " +
                                 " when A.CLosed=1 then 'Close' end Approval  from SPD_Analisa A inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                                 " left join  SPD_Departemen C on A.SarmutDeptID=C.ID left join SPD_Parameter F on F.ID=C.ParamID " +
                                 " left join SPD_Satuan G on G.ID=C.SatuanID where A.RowStatus > -1  and A.AnNo='" + AnNo + " '";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrData = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObjectHeaderAP1(sqlDataReader);
            }
        }

        return new SPD_PrsAxx();
    }


    public ArrayList RetrieveByNo2(string No)
    {
        string strSQL = " SELECT A.Penyebab_ID, A.SPDAnalisaID, A.Uraian,B.Penyebab " +
                        " FROM SPD_Analisa_Penyebab_Detail A inner join SPD_Analisa_Penyebab B on A.Penyebab_ID=B.ID inner join SPD_Analisa C on A.SPDAnalisaID=C.ID " +
                        " where A.rowstatus>-1 and C.AnNo='" + No + "'";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrData = new ArrayList();
        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                arrData.Add(GenerateObjectPenyebab(sqlDataReader));
            }
        }
        else
            arrData.Add(new FacadePrsaXx());
        return arrData;
    }

    public ArrayList RetrieveByNo1(string No, string jenis)
    {
        string strSQL = "SELECT A.ID IDPer,A.SPDAnalisaID, A.Tindakan, A.Pelaku, A.Jadwal_Selesai, isnull(A.Aktual_Selesai,'17530101')Aktual_Selesai, A.Verifikasi,Case when A.Verifikasi=1 then 'X' else '' end VerifikasiKet,isnull(A.tglVerifikasi,getdate())tglVerifikasi " +
            ", A.Targetx FROM SPD_Tindakan A inner join SPD_Analisa C on A.SPDAnalisaID=C.ID " +
            "where A.rowstatus>-1 and C.AnNo='" + No + "' and A.jenis='" + jenis + "'";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrData = new ArrayList();
        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                arrData.Add(GenerateObjectPP(sqlDataReader));
            }
        }
        else
            arrData.Add(new SPD_PrsAxx());
        return arrData;
    }

    public string GetApv(string UserID)
    {
        string strsql = "select Approval as Apv from SPD_Users where User_ID=" + UserID + " and rowstatus>-1";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return sqlDataReader["Apv"].ToString();
            }
        }

        return string.Empty;
    }

    public string GetStatusApv(string UserID)
    {
        string strsql = "select Apv from SPD_Analisa where RowStatus > -1 and DeptID in (select Dept_ID from SPD_Users where RowStatus > -1 " +
        "and User_ID=" + UserID + ")";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return sqlDataReader["Apv"].ToString();
            }
        }

        return string.Empty;
    }

    public string GetUserI(string UserID)
    {
        string strsql = "select User_ID from SPD_Analisa where DeptID in (select Dept_ID from SPD_Users where User_ID=" + UserID + ") and apv=0 and RowStatus>-1";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return sqlDataReader["User_ID"].ToString();
            }
        }

        return string.Empty;
    }

    public string GetUserIGrid(string UserID)
    {
        string strsql = "select User_ID from SPD_Analisa where DeptID in (select Dept_ID from SPD_Users where User_ID=" + UserID + ") and RowStatus>-1";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return sqlDataReader["User_ID"].ToString();
            }
        }

        return string.Empty;
    }

    public ArrayList RetrieveOpenAPHeader(string UserInput, string Apv)
    {
        Users users = (Users)HttpContext.Current.Session["Users"];

        arrData = new ArrayList();
        int UserID = users.ID;

        arrData = RetrieveForOpenAP(UserID, UserInput, Apv);
        return arrData;
    }

    public ArrayList RetrieveForOpenAP(int UserID, string UserInput, string Apv)
    {
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        string strSQL = " select A.ID,A.AnNo,A.TglAnalisa,A.SarmutPID,A.SarmutDeptID,B.SarMutPerusahaan,(Select Dept from SPD_Dept where ID=A.DeptID)Dept,isnull(C.SarmutDepartemen,'-') SarmutDepartemen,A.TargetVID,A.Actual, " +
                                            " case when A.Kesim=2 then 'Tidak Tercapai' else '' end Pencapaian, " +
                                            " Case when A.Apv=0 Then 'Open' When A.Apv is null then 'Open' " +
                                            " when A.Apv=1 then 'Mgr Dept' when A.Apv=2 then 'ISO' when A.CLosed=1 then 'Close' end Approval " +
                                            " from SPD_Analisa A inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                                            " left join  SPD_Departemen C on A.SarmutDeptID=C.ID " +
                                            " where A.RowStatus > -1 and Apv =" + Apv + "-1 and A.DeptID in  (select Dept_ID from SPD_Users where [User_ID]='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ")";

        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrData = new ArrayList();
        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                arrData.Add(GenerateObjectHeaderAP(sqlDataReader));
            }
        }
        else
            arrData.Add(new SPD_PrsAxx());
        return arrData;
    }

    public ArrayList RetrieveAP(string UserHead, string UserInput1)
    {
        string strsql = " select A.ID,A.AnNo,A.TglAnalisa,A.SarmutPID,A.SarmutDeptID,B.SarMutPerusahaan,(Select Dept from SPD_Dept where ID=A.DeptID)Dept,isnull(C.SarmutDepartemen,'-') SarmutDepartemen,A.TargetVID,A.Actual, " +
                                             " case when A.Kesim=2 then 'Tidak Tercapai' else '-' end Pencapaian, " +
                                             " Case when A.Apv=0 Then 'Open' When A.Apv is null then 'Open' " +
                                             " when A.Apv=1 then 'Mgr Dept' when A.Apv=2 then 'ISO' when A.CLosed=1 then 'Close' end Approval " +
                                             " from SPD_Analisa A inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                                             " left join  SPD_Departemen C on A.SarmutDeptID=C.ID " +
                                             " where A.RowStatus >-1 and A.DeptID in (select Dept_ID from SPD_Users where RowStatus > -1 and User_ID=" + UserHead + ") " +
                                             " and  Apv=(select top 1 Approval  from SPD_Users where RowStatus > -1 and User_ID=" + UserHead + ")-1";

        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
        strError = dataAccess.Error;

        arrData = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                arrData.Add(GenerateObjectHeaderAP(sqlDataReader));
            }
        }
        else
            arrData.Add(new SPD_PrsAxx());
        //}
        //catch { }
        return arrData;
    }

    public ArrayList RetrieveAP1(string UserHead)
    {
        string strsql = " select A.ID,A.AnNo,A.TglAnalisa,A.SarmutPID,A.SarmutDeptID,B.SarMutPerusahaan,(Select Dept from SPD_Dept where ID=A.DeptID)Dept,isnull(C.SarmutDepartemen,'-') SarmutDepartemen,A.TargetVID,A.Actual, " +
                                             " case when A.Kesim=2 then 'Tidak Tercapai' else '-' end Pencapaian, " +
                                             " Case when A.Apv=0 Then 'Open' When A.Apv is null then 'Open' " +
                                             " when A.Apv=1 then 'Mgr Dept' when A.Apv=2 then 'ISO' when A.CLosed=1 then 'Close' end Approval " +
                                             " from SPD_Analisa A inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                                             " left join  SPD_Departemen C on A.SarmutDeptID=C.ID " +
                                             " where A.RowStatus >-1 and A.DeptID in (select Dept_ID from SPD_Users where RowStatus > -1 and User_ID=" + UserHead + ") " +
                                             " and  Apv=(select top 1 Approval  from SPD_Users where RowStatus > -1 and User_ID=" + UserHead + ")-1";

        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
        strError = dataAccess.Error;

        arrData = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                arrData.Add(GenerateObjectHeaderAP(sqlDataReader));
            }
        }
        else
            arrData.Add(new SPD_PrsAxx());
        //}
        //catch { }
        return arrData;
    }

    public ArrayList RetrieveDept(string Dept_ID, string UserHead)
    {
        string kriteria = string.Empty;
        if (Dept_ID != "0")
            kriteria = kriteria + "and A.DeptID=" + Dept_ID;
        string strsql = " select A.ID,A.AnNo,A.TglAnalisa,A.SarmutPID,A.SarmutDeptID,B.SarMutPerusahaan,(Select Dept from SPD_Dept where ID=A.DeptID)Dept,C.SarmutDepartemen,A.TargetVID,A.Actual, " +
                                             " case when A.Kesim=2 then 'Tidak Tercapai' end Pencapaian, " +
                                             " Case when A.Apv=0 Then 'Open' When A.Apv is null then 'Open' " +
                                             " when A.Apv=1 then 'Mgr Dept' when A.Apv=2 then 'ISO' when A.CLosed=1 then 'Close' end Approval " +
                                             " from SPD_Analisa A inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                                             " inner join  SPD_Departemen C on A.SarmutDeptID=C.ID " +
                                             " where A.RowStatus >-1 and A.DeptID in (select Dept_ID from SPD_Users where RowStatus > -1 and User_ID=" + UserHead + ") " +
                                             " and  Apv=(select top 1 Approval  from SPD_Users where RowStatus > -1 and User_ID=" + UserHead + ")-1 " + kriteria;
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
        strError = dataAccess.Error;

        arrData = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                arrData.Add(GenerateObjectHeaderAP(sqlDataReader));
            }
        }
        else
            arrData.Add(new SPD_PrsAxx());
        return arrData;
    }


    private SPD_PrsAxx GenerateObjectPenyebab(SqlDataReader sdr)
    {
        SPD_PrsAxx xx1 = new SPD_PrsAxx();
        xx1.Penyebab_ID = Convert.ToInt32(sdr["Penyebab_ID"]);
        xx1.SPDAnalisaID = Convert.ToInt32(sdr["SPDAnalisaID"]);
        xx1.Uraian = sdr["Uraian"].ToString();
        xx1.Penyebab = sdr["Penyebab"].ToString();
        return xx1;
    }

    public SPD_PrsAxx GenerateObjectHeaderAP(SqlDataReader sqlDataReader)
    {
        objSPD = new SPD_PrsAxx();
        string test = sqlDataReader["TglAnalisa"].ToString();
        objSPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
        objSPD.AnNo = sqlDataReader["AnNo"].ToString();
        objSPD.TglAnalisa = Convert.ToDateTime(sqlDataReader["TglAnalisa"]);
        objSPD.SarMutPerusahaan = sqlDataReader["SarMutPerusahaan"].ToString();
        objSPD.SarmutDepartemen = sqlDataReader["SarmutDepartemen"].ToString();
        objSPD.TargetVID = sqlDataReader["TargetVID"].ToString();
        objSPD.Actual = Convert.ToDecimal(sqlDataReader["Actual"]);
        objSPD.Pencapaian = sqlDataReader["Pencapaian"].ToString();
        objSPD.Approval = sqlDataReader["Approval"].ToString();
        objSPD.Dept = sqlDataReader["Dept"].ToString();
        return objSPD;
    }

    public SPD_PrsAxx GenerateObjectHeaderAP1(SqlDataReader sqlDataReader)
    {
        objSPD = new SPD_PrsAxx();
        string test = sqlDataReader["TglAnalisa"].ToString();
        objSPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
        objSPD.AnNo = sqlDataReader["AnNo"].ToString();
        objSPD.TglAnalisa = Convert.ToDateTime(sqlDataReader["TglAnalisa"]);
        objSPD.SarMutPerusahaan = sqlDataReader["SarMutPerusahaan"].ToString();
        objSPD.SarmutDepartemen = sqlDataReader["SarmutDepartemen"].ToString();
        objSPD.TargetVID = sqlDataReader["TargetVID"].ToString();
        objSPD.Actual = Convert.ToDecimal(sqlDataReader["Actual"]);
        objSPD.Pencapaian = sqlDataReader["Pencapaian"].ToString();
        objSPD.Approval = sqlDataReader["Approval"].ToString();
        objSPD.Dept = sqlDataReader["Dept"].ToString();
        objSPD.Bulan = Convert.ToInt32(sqlDataReader["Bulan"]);
        objSPD.NamaBulan = sqlDataReader["NamaBulan"].ToString();
        objSPD.Tahun = Convert.ToInt32(sqlDataReader["Tahun"]);
        objSPD.Param = sqlDataReader["Param"].ToString();
        objSPD.Satuan = sqlDataReader["Satuan"].ToString();
        objSPD.SarmutDeptID = Convert.ToInt32(sqlDataReader["SarmutDeptID"]);
        objSPD.SarmutPID = Convert.ToInt32(sqlDataReader["SarmutPID"]);
        return objSPD;
    }

    private SPD_PrsAxx GenerateObjectPP(SqlDataReader sdr)
    {
        SPD_PrsAxx xx1 = new SPD_PrsAxx();
        xx1.IDPer = Convert.ToInt32(sdr["IDPer"]);
        xx1.SPDAnalisaID = Convert.ToInt32(sdr["SPDAnalisaID"]);
        xx1.Tindakan = sdr["Tindakan"].ToString();
        xx1.Pelaku = sdr["Pelaku"].ToString();
        xx1.Jadwal_Selesai = Convert.ToDateTime(sdr["Jadwal_Selesai"]);
        xx1.Aktual_Selesai = Convert.ToDateTime(sdr["Aktual_Selesai"]);
        xx1.Tglverifikasi = Convert.ToDateTime(sdr["Tglverifikasi"]);
        xx1.Verifikasi = Convert.ToInt32(sdr["Verifikasi"]);
        xx1.Targetx = sdr["Targetx"].ToString();
        xx1.VerifikasiKet = sdr["VerifikasiKet"].ToString();
        return xx1;
    }

}


public class SPD_PrsAxx : GRCBaseDomain
{
    public string VerifikasiKet { get; set; }
    public string Description { get; set; }
    //public string Satuan { get; set; }
    public string Param { get; set; }
    public decimal Target { get; set; }
    public decimal Actual { get; set; }
    public string NamaBulan { get; set; }
    public int IDX { get; set; }
    public int Tahun { get; set; }
    public int Bulan { get; set; }
    public int DeptID { get; set; }
    public int dptID { get; set; }
    public int Urutan { get; set; }
    public int TypeSarmutID { get; set; }
    public int SDeptID { get; set; }
    public int Checked { get; set; }
    public string StatusApv { get; set; }
    public int OnSystem { get; set; }
    public int SarmutPID { get; set; }
    public int SarmutDeptID { get; set; }
    public string TargetVID { get; set; }
    public string ParamID { get; set; }
    public string SatuanID { get; set; }
    public string Pencapaian { get; set; }
    public int Kesim { get; set; }
    public string SarmutDepartemen { get; set; }
    public string SarMutPerusahaan { get; set; }
    public string Old_sarmutNo { get; set; }
    public string AnNo { get; set; }
    public DateTime TglAnalisa { get; set; }
    public int Penyebab_ID { get; set; }
    public string Penyebab { get; set; }
    public int SPDAnalisaID { get; set; }
    public string Uraian { get; set; }
    //public int RowStatus { get; set; }
    public string Tindakan { get; set; }
    public string Pelaku { get; set; }
    public string Jenis { get; set; }
    public string Targetx { get; set; }
    public DateTime Jadwal_Selesai { get; set; }
    public DateTime Aktual_Selesai { get; set; }
    public DateTime Tglverifikasi { get; set; }
    public int Verifikasi { get; set; }
    public string Dept { get; set; }
    public string Ket { get; set; }
    public int ID1 { get; set; }
    public int IDPer { get; set; }
    public int User_ID { get; set; }
    public int Apv { get; set; }
    public string Approval { get; set; }
    public int CLosed { get; set; }
    public DateTime Close_Date { get; set; }
    public string CloseBy { get; set; }
    public int Solved { get; set; }
    public DateTime Solve_Date { get; set; }
    public DateTime Due_Date { get; set; }
}