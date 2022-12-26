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
using System.Data.SqlClient;
using System.Configuration;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapStockInvetory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTipeBarang();
                cari.Enabled = false;
            }
        }

        private void LoadTipeBarang()
        {
            ArrayList arrItemTypePurchn = new ArrayList();
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            arrItemTypePurchn = itemTypePurchnFacade.Retrieve();

            ddlTipeBarang.Items.Add(new ListItem("-- Pilih Tipe Barang --", string.Empty));
            foreach (ItemTypePurchn itemTypePurchn in arrItemTypePurchn)
            {
                ddlTipeBarang.Items.Add(new ListItem(itemTypePurchn.TypeDescription, itemTypePurchn.ID.ToString()));
            }
        }

        private void LoadTipeSPP()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            //arrGroupsPurchn = groupsPurchnFacade.Retrieve();
            int itemtypeid = 0;

            if (ddlTipeBarang.SelectedIndex == 1)
                itemtypeid = 1;
            if (ddlTipeBarang.SelectedIndex == 2)
                itemtypeid = 2;
            if (ddlTipeBarang.SelectedIndex == 3)
                itemtypeid = 3;

            arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(((Users)Session["Users"]).DeptID, ((Users)Session["Users"]).GroupID, itemtypeid);

            GroupID.Items.Add(new ListItem("-- Pilih Tipe SPP --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                GroupID.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }
        protected void ddlTipeBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupID.Items.Clear();
            LoadTipeSPP();

        }

        private ArrayList LoadInventory()
        {
            string strValue = " GroupID=" + GroupID.SelectedValue.ToString();
            strValue += (ddlTipeBarang.SelectedIndex == 1) ? " and len(ItemCode)=15 " : "";
            strValue += ddlCriteria.SelectedValue;
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            arrInventory = inventoryFacade.RetrieveForStockView(strValue, ddlTipeBarang.SelectedItem.ToString(), ddlOrderby.SelectedValue.ToString());
            if (arrInventory.Count > 0)
            {
                return arrInventory;
            }
            arrInventory.Add(new Inventory());
            return arrInventory;
        }

        protected void GroupID_SelectedIndexChanged(object sender, EventArgs e)
        {
            cari.Enabled = true;
        }
        protected void cari_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void LoadData()
        {
            ArrayList arrLst = new ArrayList();
            ArrayList newArr = new ArrayList();

            arrLst = LoadInventory();
            int n = 0;
            foreach (Inventory inv in arrLst)
            {
                Inventory objInv = new Inventory();
                if (ddlTipeBarang.SelectedIndex == 1)
                {
                    if (inv.ItemCode.Length == 15)
                    {
                        n++;
                        objInv.ID = n;
                        objInv.ItemCode = inv.ItemCode;
                        objInv.ItemName = inv.ItemName;
                        objInv.UomCode = inv.UOMDesc;
                        objInv.Jumlah = inv.Jumlah;
                        objInv.Harga = inv.Harga;
                        objInv.Aktif = inv.Aktif;
                        newArr.Add(objInv);
                    }
                }
                else
                {
                    n++;
                    objInv.ID = n;
                    objInv.ItemCode = inv.ItemCode;
                    objInv.ItemName = inv.ItemName;
                    objInv.UomCode = inv.UOMDesc;
                    objInv.Jumlah = inv.Jumlah;
                    objInv.Harga = inv.Harga;
                    objInv.Aktif = inv.Aktif;
                    newArr.Add(objInv);
                }
            }
            ListStock.DataSource = newArr;
            ListStock.DataBind();
        }
        private void LoadDatabyCriteria(string itemName)
        {
            ArrayList arrLst = new ArrayList();
            ArrayList newArr = new ArrayList();

            arrLst = LoadInventory();
            int n = 0;
            foreach (Inventory inv in arrLst)
            {
                Inventory objInv = new Inventory();
                if (inv.ItemCode.Length == 15)
                {
                    n++;
                    objInv.ID = n;
                    objInv.ItemCode = inv.ItemCode;
                    objInv.ItemName = inv.ItemName;
                    objInv.UomCode = inv.UOMDesc;
                    objInv.Jumlah = inv.Jumlah;
                    objInv.JmlTransit = inv.JmlTransit;
                    objInv.Aktif = inv.Aktif;
                    newArr.Add(objInv);
                }
            }
            ListStock.DataSource = newArr;
            ListStock.DataBind();
        }
    }
}