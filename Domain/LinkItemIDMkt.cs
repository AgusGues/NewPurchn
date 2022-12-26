using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LinkItemIDMkt : GRCBaseDomain
    {
        private int itemIDMkt = 0;
        private int itemIDFc = 0;
        private string itemNameMkt = string.Empty;
        private string partNo = string.Empty;

        public int ItemIDMkt
        {
            get { return itemIDMkt; }
            set { itemIDMkt = value; }
        }

        public int ItemIDFc
        {
            get { return itemIDFc; }
            set { itemIDFc = value; }
        }

        public string  ItemNameMkt
        {
            get { return itemNameMkt; }
            set { itemNameMkt = value; }
        }

        public string PartNo
        {
            get { return partNo ; }
            set { partNo = value; }
        }
    }

    public class CopyOfLinkItemIDMkt : GRCBaseDomain
    {
        private int itemIDMkt = 0;
        private int itemIDFc = 0;
        private string itemNameMkt = string.Empty;
        private string partNo = string.Empty;

        public int ItemIDMkt
        {
            get { return itemIDMkt; }
            set { itemIDMkt = value; }
        }

        public int ItemIDFc
        {
            get { return itemIDFc; }
            set { itemIDFc = value; }
        }

        public string ItemNameMkt
        {
            get { return itemNameMkt; }
            set { itemNameMkt = value; }
        }

        public string PartNo
        {
            get { return partNo; }
            set { partNo = value; }
        }
    }
}
