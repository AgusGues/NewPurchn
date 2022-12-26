<%@ WebHandler Language="C#" Class="SearchAll" %>

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using Cogs;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;

public class SearchAll : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
{

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string strStatus = context.Request["Status"];
        switch (context.Request["id"])
        {
            case "1":

                if (CheckConfig("Status", context) == "1")
                {
                    context.Response.Write(CheckConfig(strStatus, context).ToString());
                }
                break;
            case "2":

                context.Response.Write(CheckConfig("UserName", context));
                break;
            case "3":
                int UserID = ((Users)context.Session["Users"] == null) ? 0 : ((Users)context.Session["Users"]).ID;
                context.Response.Write(SPPInfo(UserID, context));
                break;

            case "4":
                string UserIDs = ((Users)context.Session["Users"] == null) ? "" : ((Users)context.Session["Users"]).UserID;
                context.Response.Write(POinfo(UserIDs.ToString(), context));
                break;

            case "5":
                UserID = ((Users)context.Session["Users"] == null) ? 0 : ((Users)context.Session["Users"]).ID;
                context.Response.Write(BAInfo(UserID.ToString(), context));
                break;

            case "6":
                UserID = ((Users)context.Session["Users"] == null) ? 0 : ((Users)context.Session["Users"]).ID;
                context.Response.Write(TaskInfo(UserID.ToString(),context));
                break;

            case "7":
                int UserID2 = ((Users)context.Session["Users"] == null) ? 0 : ((Users)context.Session["Users"]).ID;
                context.Response.Write(TppInfo(UserID2, context));
                break;

            case "8":
                int Deptid = ((Users)context.Session["Users"] == null) ? 0 : ((Users)context.Session["Users"]).DeptID;
                int UserID8 = ((Users)context.Session["Users"] == null) ? 0 : ((Users)context.Session["Users"]).ID;
                context.Response.Write(WOinfoLewat(Deptid ,context));
                break;
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
    private string CheckConfig(string Section, HttpContext context)
    {

        var Conf = new Inifile(context.Server.MapPath("~/App_Data/InfoAdmin.ini"));
        return Conf.Read(Section, "Publish").ToString();
    }

    private string WOinfoLewat(int deptid ,HttpContext context)
    {
        string where = context.Request["where"];
        // ArrayList arrWO = new POPurchnFacade().RetrieveUnApprove(CreatedBy);
        ArrayList arrWO = new WorkOrderFacade_New().WorkOrderApproveLewat(deptid);
        string url = HttpContext.Current.Request.ApplicationPath;
        string WOlewat = string.Empty;
        foreach (WorkOrder sp in arrWO)
        {
            WOlewat += "NO WO. : " + sp.NoWO + " Keterangan : "+sp.Keterangan+" ";
        }
        return WOlewat;
    }

    private string SPPInfo(int UserID,HttpContext context)
    {
        // SPPDetail spp = new SPPDetail();
        string where=context.Request["where"];
        ArrayList arrSPP = new NewSPPDetailFacade().RetrievePending(UserID);
        string SPPPending = string.Empty;
        foreach (NewSPPDetail sp in arrSPP)
        {
            SPPPending += sp.Satuan + " [ " + sp.ItemName.ToLower().ToUpperInvariant() + " ] Status Pending PO karena " + sp.CariItemName.ToString() + " ...";
        }
        return SPPPending;
    }
    private string POinfo(string CreatedBy, HttpContext context)
    {
        string where = context.Request["where"];
        ArrayList arrSPP = new POPurchnFacade().RetrieveUnApprove(CreatedBy);
        string url = HttpContext.Current.Request.ApplicationPath;
        string SPPPending = string.Empty;
        foreach (POPurchn sp in arrSPP)
        {
            SPPPending += "PO No. : <a href='" + url + "/Modul/Purchasing/FormPOPurchn.aspx?PONo=" + sp.NoPO + "'>" + sp.NoPO + "</a> tidak di approve oleh : " + sp.LastModifiedBy + " on " +
                          sp.LastModifiedTime.ToString("dd-MMM-yyy") + " karena " + sp.AlasanNotApproval.ToString();
        }
        return SPPPending;
    }
    private string BAInfo(string CreatedBy, HttpContext context)
    {
        string BAUnapprove = string.Empty;
        string where = context.Request["where"];
        string[] AppUser = new Inifiles(context.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara").Split(',');
        int pos = Array.IndexOf(AppUser, CreatedBy.ToString());
        if (pos > -1)
        {
            ArrayList arrBA = new BeritaAcaraFacade().RetrieveUnApprove(pos.ToString());
            foreach (BeritaAcara ba in arrBA)
            {
                BAUnapprove += "BA No. : " + ba.BANum + " Mohon di Approve ";
            }
        }
        return BAUnapprove;
    }
    private string TaskInfo(string Criteria, HttpContext context)
    {
        string msg = string.Empty;
        try
        {
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            Users user = ((Users)HttpContext.Current.Session["Users"]);

            string ConnName = "";
            //switch (user.TypeUnitKerja)
            //{
            //    case 1: ConnName = "GRCBoardPurch"; break;
            //    case 3: ConnName = "GRCBoardCtrp"; break;
            //    default: ConnName = "GRCBoardKrwg"; break;
            //}
            ConnName = "GRCBoardPurch";
            Criteria = "WHERE UserID=" + user.ID + " OR HeadID=" + user.ID;
            //Criteria = "WHERE UserID=291 OR HeadID=291";
            string result = api.TaskMonitoringFilter(Criteria, ConnName);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            //Task t = js.Deserialize<Task>(result);
            msg = result;
            return msg;
        }
        catch { return msg; }

    }

    private string TppInfo(int UserID2, HttpContext context)
    {
        string where = context.Request["where"];
        ArrayList arrSPP = new NewSPPDetailFacade().RetrieveTPP(UserID2);
        string TppPending = string.Empty;
        foreach (NewSPPDetail sp in arrSPP)
        {
            TppPending += "Tpp No: " + sp.Laporan_No.ToString() + " Duedate " + sp.Jadwal_Selesai.ToString("dd-MMM-yyy") + " ";
        }
        return TppPending;
    }
}
