using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Domain;
using BusinessFacade;
using Factory;
using Cogs;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LapPemantauanWIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                tglProduksi.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                criteria.Visible = false;
                //criteria.Visible = (list.Checked == true) ? true : false;
                tabled.Visible = (list.Checked == true) ? true : false;
                trev.Visible = (list.Checked == true) ? false : true;
            }

        }

        private void LoadData(int DestackID)
        {
            ArrayList arrPantau = new ArrayList();
            PemantauanWIP wip = new PemantauanWIP();
            arrPantau = wip.Curing(DateTime.Parse(tglProduksi.Text).ToString("yyyyMMdd"), txtPartNo.Text, DestackID, txtPaletNo.Text, TxtLokasi.Text);
            if (arrPantau.Count > 0)
            {
                nofound.Visible = false;
                Pantau.DataSource = arrPantau;
                Pantau.DataBind();
            }
            else
            {
                nofound.Visible = true;
            }
        }
        private void LoadDataCuring(int DestID)
        {
            ArrayList arrPantaus = new ArrayList();
            PemantauanWIP wip = new PemantauanWIP();
            arrPantaus = wip.T1_Serah(DateTime.Parse(tglProduksi.Text).ToString("yyyyMMdd"), DestID);
            if (arrPantaus.Count > 0)
            {
                nofound.Visible = false;
                var SerahT1 = Pantau.FindControl("isSerah");
                //SerahT1.DataSource = arrPantaus;
                //SerahT1.DataBind();
            }
            else
            {

            }
        }
        protected void preview_Click(object sender, EventArgs e)
        {
            // LoadDataCuring();
            tahap1.Nodes.Clear();
            TreeNode Dest = new TreeNode();
            Dest.Text = "Produksi :" + DateTime.Parse(tglProduksi.Text).ToString("dd MMM yyyy");
            Dest.Value = tglProduksi.Text;
            Dest.Expanded = true;
            tahap1.Nodes.Add(Dest);
            //Destcaking(Dest);
            LoadData(0);
        }

        protected void Pantau_DataBound(object source, RepeaterItemEventArgs e)
        {
            DataRowView ds = e.Item.DataItem as DataRowView;
            var sb = e.Item.FindControl("isSerah") as Repeater;
            var ts = e.Item.FindControl("sumSerah") as Repeater;
            if (sb != null)
            {
                WIPDomain wn = e.Item.DataItem as WIPDomain;
                ArrayList arrPantaus = new ArrayList();
                ArrayList arrSerah = new ArrayList();
                PemantauanWIP wip = new PemantauanWIP();
                if (wn.DestackID > 0)
                {

                    arrPantaus = wip.T1_Serah(DateTime.Parse(tglProduksi.Text).ToString("yyyyMMdd"), wn.DestackID);
                    if (arrPantaus.Count > 0)
                    {
                        int Dstk = 0;
                        foreach (WIPDomain w in arrPantaus)
                        {
                            Dstk = w.DestackID;
                        }
                        if (Dstk > 0)
                        {
                            nofound.Visible = false;
                            sb.DataSource = arrPantaus;
                            sb.DataBind();
                        }
                        ts.Visible = (Dstk > 0) ? true : false;
                    }

                    arrSerah = wip.SumSerah(wn.DestackID);

                    ts.DataSource = arrSerah;
                    ts.DataBind();
                }
                else
                {
                    ts.Visible = false;
                }


            }
        }
        public void isSerah_DataBound(object source, RepeaterItemEventArgs e)
        {
            DataRowView t1 = e.Item.DataItem as DataRowView;
            var ts = e.Item.FindControl("sumSerah") as Repeater;
            if (ts != null)
            {
                WIPDomain wni = e.Item.DataItem as WIPDomain;
                ArrayList arrSerah = new ArrayList();
                PemantauanWIP wip = new PemantauanWIP();
                if (wni.DestackID > 0)
                {
                    arrSerah = wip.SumSerah(wni.DestackID);
                    ts.DataSource = arrSerah;
                    ts.DataBind();
                }
            }
        }
        //header nod
        public void Destcaking(TreeNode parent)
        {
            ArrayList arrDstk = new ArrayList();
            PemantauanWIP Dstk = new PemantauanWIP();
            arrDstk = Dstk.DestackingItem(DateTime.Parse(tglProduksi.Text).ToString("yyyyMMdd"), txtPartNo.Text, string.Empty, string.Empty);
            parent.ChildNodes.Clear();
            string html = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            foreach (WIPDomain wp in arrDstk)
            {

                TreeNode dst = new TreeNode();
                dst.Text = wp.PartNo + html + "[ " + wp.Saldo.ToString("#,#00.00") + "]";
                dst.Value = wp.PaletNo;
                dst.Expanded = true;
                parent.ChildNodes.Add(dst);
                ArrayList arrLok = new ArrayList();
                arrLok = Dstk.Lokasi(DateTime.Parse(tglProduksi.Text).ToString("yyyyMMdd"), wp.PartNo);
                foreach (WIPDomain l in arrLok)
                {
                    TreeNode lok = new TreeNode();
                    lok.Text = l.Lokasi + html + "[ " + l.Saldo.ToString("#,#00.00") + " ]";
                    lok.Value = l.Lokasi;
                    dst.ChildNodes.Add(lok);
                    ArrayList arrPalet = new ArrayList();
                    arrPalet = Dstk.Palet(DateTime.Parse(tglProduksi.Text).ToString("yyyyMMdd"), wp.PartNo, l.Lokasi);
                    foreach (WIPDomain p in arrPalet)
                    {
                        TreeNode pal = new TreeNode();
                        pal.Text = p.PaletNo + html + " [ " + p.Saldo.ToString("#,#00.00") + " ]";
                        pal.Value = p.ID.ToString();

                        if (p.ID > 0)
                        {
                            lok.ChildNodes.Add(pal);
                            ArrayList arrJemur = new ArrayList();
                            arrJemur = Dstk.Jemur(p.ID);
                            if (arrJemur.Count > 0 && Dstk.Error == string.Empty)
                            {
                                foreach (WIPDomain Jm in arrJemur)
                                {
                                    /*proses jemur */
                                    TreeNode tJemur = new TreeNode();
                                    string txt = "Jemur Tgl : " + DateTime.Parse(Jm.Tanggal.ToString()).ToString("dd-MM-yyyy") +
                                                html + " Rak No: " + Jm.Rak.ToString() + html + " [ " + Jm.Saldo.ToString("#,#00.00") + " ]";
                                    tJemur.Text = txt;
                                    tJemur.Value = Jm.ID.ToString();
                                    //tJemur.PopulateOnDemand = true;
                                    if (Jm.ID > 0)
                                    {
                                        pal.ChildNodes.Add(tJemur);
                                        ArrayList arrSerah = new ArrayList();
                                        /** Proses Serah */
                                        arrSerah = Dstk.Serah(Jm.ID);
                                        if (arrSerah.Count > 0 && Dstk.Error == string.Empty)
                                        {
                                            foreach (WIPDomain s in arrSerah)
                                            {
                                                TreeNode tSerah = new TreeNode();
                                                string srh = "Tgl Serah : " + DateTime.Parse(s.Tanggal.ToString()).ToString("dd-MM-yyyy");
                                                srh += html + " Lokasi : " + s.Lokasi + html + " PartNo: " + s.PartNo.ToString();
                                                srh += html + " [ " + s.Saldo.ToString("#,#00.00") + " ]";
                                                tSerah.Text = srh;
                                                tSerah.Value = s.ID.ToString();

                                                if (s.ID > 0)
                                                {
                                                    tJemur.ChildNodes.Add(tSerah);
                                                    ArrayList arrT3 = new ArrayList();
                                                    arrT3 = Dstk.ToTahap3(p.ID, s.ID, s.Qty, s.DestackID);
                                                    if (arrT3.Count > 0 && Dstk.Error == string.Empty)
                                                    {
                                                        foreach (WIPDomain t3 in arrT3)
                                                        {
                                                            TreeNode t3In = new TreeNode();
                                                            string txt3 = "Transit Out : " + DateTime.Parse(t3.Tanggal.ToString()).ToString("dd-MM-yyyy") + "&nbsp;&nbsp;&nbsp;&nbsp;" +
                                                                          "Lokasi : " + t3.Lokasi.ToString() + "[ " + t3.Keterangan.ToString() + " ]" + html + t3.PartNo.ToString() +
                                                                           html + " [ " + t3.Qty.ToString("#,#00.00") + " ]";
                                                            t3In.Text = txt3;
                                                            t3In.Value = t3.ID.ToString();
                                                            t3In.PopulateOnDemand = true;
                                                            if (t3.ID > 0)
                                                            {
                                                                tSerah.ChildNodes.Add(t3In);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    tSerah.PopulateOnDemand = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tJemur.PopulateOnDemand = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

        protected void tahap1_SelectedNodeChanged(object sender, EventArgs e)
        {

        }
        protected void tri_CheckedChanged(object sender, EventArgs e)
        {
            //criteria.Visible = (list.Checked == true) ? true : false;
            tabled.Visible = (list.Checked == true) ? true : false;
            trev.Visible = (list.Checked == true) ? false : true;
            tahap1.Nodes.Clear();
            TreeNode Dest = new TreeNode();
            Dest.Text = "Produksi :" + DateTime.Parse(tglProduksi.Text).ToString("dd MMM yyyy");
            Dest.Value = tglProduksi.Text;
            Dest.Expanded = true;
            tahap1.Nodes.Add(Dest);
            Destcaking(Dest);
        }
        protected void list_CheckedChanged(object sender, EventArgs e)
        {
            //criteria.Visible = (list.Checked == true) ? true : false;
            tabled.Visible = (list.Checked == true) ? true : false;
            trev.Visible = (list.Checked == true) ? false : true;
            LoadData(0);
        }

        protected void Prev_Click(object sender, ImageClickEventArgs e)
        {
            DateTime tgl = Convert.ToDateTime(tglProduksi.Text).AddDays(-1);
            tglProduksi.Text = tgl.ToString("dd-MMM-yyyy");
            LoadData(0);
        }
        protected void Next_Click(object sender, ImageClickEventArgs e)
        {
            DateTime tgl = Convert.ToDateTime(tglProduksi.Text).AddDays(1);
            tglProduksi.Text = tgl.ToString("dd-MMM-yyyy");
            LoadData(0);
        }
    }
}