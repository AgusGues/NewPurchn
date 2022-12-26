using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Factory;
using Cogs;
using Domain;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.Factory
{
    public partial class T3_BA_Approval : System.Web.UI.Page
    {
        public int crBAID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Session["ListofBAItems"] = null;
                Session["id"] = null;
                clearform();
                LoadOpenT3BA();
                //Session["ListOpenT3_BA"] = null;
            }
        }
        protected void btnSebelumnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] - 1;
            LoadT3BA((int)ViewState["counter"]);
        }

        protected void btnSesudahnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] + 1;
            LoadT3BA((int)ViewState["counter"]);
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            int userLevel = GetUserLevelAApv(users.ID);
            string lokasi = string.Empty;

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (userLevel < 5)
                zl.CustomQuery = "update t3_BA set approval=" + userLevel + " where RowStatus>-1 and BANo='" + txtNoBA.Text.Trim() + "'";
            else
            {
                zl.CustomQuery = "update t3_BA set approval=" + userLevel + " where RowStatus>-1 and BANo='" + txtNoBA.Text.Trim() + "' " +
                    "declare @BANo varchar(max),@cekBA int set @BANo='" + txtNoBA.Text.Trim() + "' " +
                    "/*insert Adjust*/ " +
                    "set @cekBA= (select count(id) from t3_adjust where rowstatus >-1 and noba=@BANo) " +
                    "if @cekBA=0 begin " +

                    "insert T3_Adjust select top 1 'Adj' + right(rtrim(cast(year(BADate) as char)),2) + REPLACE(STR(month(BADate), 2), SPACE(1), '0') +  " +
                    "REPLACE(STR((select count(adjustno)as docnocount from ( select distinct adjustno from T3_Adjust  " +
                    "where month(adjustdate) = month(BAdate) and year(adjustdate)=year(BAdate) ) as a)+1, 4), SPACE(1), '0') Adjustno,  " +
                    "BADate AdjustDate, BANo NoBA, Keterangan, RowStatus, CreatedBy, getdate() CreatedTime, LastModifiedBy, getdate()LastModifiedTime  " +
                    "from t3_ba where RowStatus>-1 and BANo=@BANo " +

                    "insert T3_Adjustdetail select  (select top 1 id from T3_Adjust where NoBA=@BANo) AdjustID, AdjustType, ItemID,  " +
                    "LokID, QtyIn, QtyOut, 1 Apv, hpp,  " +
                    "RowStatus, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime,0 SA from t3_badetail where BAID=(select id from T3_BA  " +
                    "where RowStatus>-1 and BANo=@BANo) " +
                    "/*update t3_serah*/ " +
                    "create table #TmpT3Serah (itemid int,lokid int, qty int)  " +
                    "declare @itemid int ,@lokid int, @qty int ,@ada int " +
                    "declare kursor cursor for  " +
                    "select itemid,lokid,sum(qty) qty from ( " +
                    "select itemid,lokid,qtyin qty from T3_AdjustDetail where RowStatus>-1 and qtyin >0 and AdjustID in  " +
                    "(select id from T3_Adjust where NoBA=@BANo) " +
                    "union all " +
                    "select itemid,lokid,qtyout*-1 qty from T3_AdjustDetail where RowStatus>-1 and qtyout >0 and AdjustID in  " +
                    "(select id from T3_Adjust where NoBA=@BANo) " +
                    ")A group by itemid,lokid " +
                    "open kursor  " +
                    "FETCH NEXT FROM kursor  " +
                    "INTO @itemid,@lokid,@qty " +
                    "WHILE @@FETCH_STATUS = 0  " +
                    "begin  " +
                    "    select @ada=(select count(id) from t3_serah where lokid=@lokid and ItemID=@itemid and rowStatus>-1) " +
                    "    if @ada =0 " +
                    "    begin " +
                    "        insert into T3_Serah (GroupID, LokID, ItemID, Qty, HPP, Status, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime, RowStatus) " +
                    "        values((select groupid from fc_items where id=@itemid), @lokid, @itemid, @qty,0,0,'system',getdate(),'system',getdate(),0)  " +
                    "    end " +
                    "    else " +
                    "    begin " +
                    "        update t3_serah set qty=qty + @qty where itemid=@itemid and LokID=@lokid  " +
                    "    end " +
                    "    FETCH NEXT FROM kursor  " +
                    "    INTO @itemid,@lokid,@qty " +
                    "END  " +
                    "CLOSE kursor  " +
                    "DEALLOCATE kursor " +
                    "drop table #TmpT3Serah end";
            }
            SqlDataReader sdr = zl.Retrieve();
            Response.Redirect("t3_ba_approval.aspx");
        }
        private void clearform()
        {
            Users users = (Users)Session["Users"];
            T3_BADetail BADetail = new T3_BADetail();
            ArrayList arrBAItems = new ArrayList();
            ddlKeterangan.SelectedIndex = 0;
            txtNoBA.Text = string.Empty;
            Session["ListOpenT3_BA"] = null;
            arrBAItems.Add(BADetail);
            GridItem0.DataSource = arrBAItems;
            GridItem0.DataBind();
            LoadListAttachment(crBAID.ToString().Trim());
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
        }

        protected int GetBAID(string BANo)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select ID from t3_BA where RowStatus>-1 and BANo='" + BANo + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["ID"].ToString());
                }
            }
            return result;
        }
        protected int GetUserLevelAApv(int userid)
        {
            int userLevel = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select LevelApv from t3_BAListApproval where RowStatus>-1 and userid='" + userid + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    userLevel = Convert.ToInt32(sdr["LevelApv"].ToString());
                }
            }
            return userLevel;
        }
        protected void LoadOpenT3BA()
        {
            Users users = (Users)Session["Users"];
            int userLevel = GetUserLevelAApv(users.ID);
            if (userLevel > 0)
            {
                ArrayList arrT3_BA = new ArrayList();
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = "select * from T3_BA where rowstatus>-1 and isnull(Approval,0)= " + (userLevel - 1);
                SqlDataReader sdr1 = zl1.Retrieve();
                if (sdr1 != null)
                {
                    if (sdr1.HasRows)
                    {
                        while (sdr1.Read())
                        {
                            arrT3_BA.Add(new T3_BA
                            {
                                ID = Convert.ToInt32(sdr1["ID"].ToString()),
                                BANo = (sdr1["BANo"].ToString()),
                                BADate = DateTime.Parse(sdr1["BADate"].ToString()),
                                Keterangan = sdr1["keterangan"].ToString()
                            });
                        }
                        Session["ListOpenT3_BA"] = arrT3_BA;
                    }
                }
                ViewState["counter"] = 0;
                int counter = (int)ViewState["counter"];
                if (Request.QueryString["BANo"] != null)
                {
                    counter = FindT3_BA(Request.QueryString["BANo"].ToString());
                    ViewState["counter"] = counter;
                }
                LoadT3BA(counter);
            }
        }
        protected void LoadT3BA(int intRow)
        {
            ArrayList arrT3BA = new ArrayList();
            if (Session["ListOpenT3_BA"] != null)
                arrT3BA = (ArrayList)Session["ListOpenT3_BA"];

            if (intRow < arrT3BA.Count && intRow > -1)
            {
                T3_BA t3_BA = new T3_BA();
                t3_BA = (T3_BA)arrT3BA[intRow];
                if (t3_BA.BANo != string.Empty)
                {
                    Session["id"] = t3_BA.ID;
                    ViewState["BANo"] = t3_BA.BANo;

                    txtNoBA.Text = t3_BA.BANo;
                    txtTglBA.Text = t3_BA.BADate.ToString("dd-MMM-yyyy");
                    ddlKeterangan.ClearSelection();
                    ddlKeterangan.Items.FindByText(t3_BA.Keterangan.Trim().ToUpper()).Selected = true;
                    LoadT3BADetail(t3_BA.ID);
                    LoadListAttachment(t3_BA.ID.ToString().Trim());
                }
            }
            else
            {
                if (intRow == -1)
                    ViewState["counter"] = (int)ViewState["counter"] + 1;
                else
                    ViewState["counter"] = (int)ViewState["counter"] - 1;

                T3_BA t3_BA = new T3_BA();
            }
        }
        private int FindT3_BA(string strBAno)
        {
            ArrayList arrT3_BA = new ArrayList();
            int counter = 0;

            if (Session["ListOpenT3_BA"] != null)
                arrT3_BA = (ArrayList)Session["ListOpenT3_BA"];

            foreach (T3_BA t3_ba in arrT3_BA)
            {
                if (t3_ba.BANo == strBAno)
                    return counter;

                counter = counter + 1;
            }
            return counter;
        }
        protected void LoadT3BADetail(int BAID)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            //if (Convert.ToInt32(BAID) == 0)
            //    return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select B.ID,A.Keterangan BAType,  A.BANo ,A.BADate,B.AdjustType,I.PartNo,isnull(B.QtyIn,0)QtyIn, case when isnull(A.Approval,0)=0 then '' else '' end Approval, " +
                "isnull(B.QtyOut,0)QtyOut, isnull(B.Keterangan,'')Keterangan " +
                "from T3_BA A inner join T3_BADetail B on A.ID=B.BAID inner join FC_Items I on B.ItemID =I.ID where A.ID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new T3_BADetail
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            BANo = (sdr["BANo"].ToString()),
                            BADate = DateTime.Parse(sdr["BADate"].ToString()),
                            BAType = sdr["BAType"].ToString(),
                            PartNo = (sdr["PartNo"].ToString()),
                            AdjustType = sdr["AdjustType"].ToString(),
                            QtyIn = Convert.ToInt32(sdr["QtyIn"].ToString()),
                            QtyOut = Convert.ToInt32(sdr["QtyOut"].ToString()),
                            Approval = (sdr["Approval"].ToString()),
                            Keterangan = (sdr["Keterangan"].ToString())
                        });
                    }
                }
            }
            GridItem0.DataSource = arrData;
            GridItem0.DataBind();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadListAttachment(string BAID)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(BAID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select *,0 Approval from T3_BAAttachment where rowstatus>-1 and BAID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new T3BA_AttachmentApv
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            BAID = Convert.ToInt32(sdr["BAID"].ToString()),
                            FileName = sdr["FileName"].ToString(),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString())
                        });
                    }
                }
            }
            attachm.DataSource = arrData;
            attachm.DataBind();
        }

        protected void attachm_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihat") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihat");
                //Image hps = (Image)e.Item.FindControl("hapus");
                T3BA_AttachmentApv att = (T3BA_AttachmentApv)e.Item.DataItem;
                //hps.Visible = (att.Approval < 1) ? true : false;
            }
        }
        protected void attachm_Command(object sender, RepeaterCommandEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "pre":
                        string Nama = e.CommandArgument.ToString();
                        string Nama2 = @"\" + Nama;
                        string dirPath = @"D:\DATA LAMPIRAN PURCHN\t3BA\";
                        string ext = Path.GetExtension(Nama);
                        HttpResponse response = HttpContext.Current.Response;
                        Response.Clear();
                        string excelFilePath = dirPath + Nama;
                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                        if (file.Exists)
                        {
                            response.Clear();
                            response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            response.AddHeader("Content-Length", file.Length.ToString());
                            response.ContentType = "application/octet-stream";
                            response.WriteFile(file.FullName);
                            response.End();
                        }
                        break;

                    case "hps":
                        Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "Update T3_BAAttachment set RowStatus=-1 where ID=" + hps.CssClass;
                        SqlDataReader sdr = zl.Retrieve();
                        break;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Data belum tersimpan atau di approve");
                return;
            }
        }
        protected void txtCariBA_TextChanged(object sender, EventArgs e)
        {
            // LoadT3BA(txtCariBA.Text);
        }

        protected void txtNoBA_TextChanged(object sender, EventArgs e)
        {
            if (txtNoBA.Text.Trim() != string.Empty)
                crBAID = GetBAID(txtNoBA.Text.Trim());
        }
    }
    public class T3BA_AttachmentApv : GRCBaseDomain
    {
        public string FileName { get; set; }
        public int BAID { get; set; }
        public int Approval { get; set; }
    }
}