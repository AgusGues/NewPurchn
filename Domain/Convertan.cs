using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Convertan : GRCBaseDomain
    {
        private string repackNo = string.Empty;
        private int fromItemID = 0;
        private decimal fromQty = 0;
        private int fromUomID = 0;
        private int toItemID = 0;
        private decimal toQty = 0;
        private int toUomID = 0;
        private int id = 0;
        private int rowStatus = 0;
        private string fromItemCode = string.Empty;
        private string toItemCode = string.Empty;
        private string fromUomCode = string.Empty;
        private string toUomCode = string.Empty;
        private string toItemName = string.Empty;

        public string ToItemName
        {
            get { return toItemName; }
            set { toItemName = value; }
        }
        public string FromItemCode
        {
            get { return fromItemCode; }
            set { fromItemCode = value; }
        }
        public string ToItemCode
        {
            get { return toItemCode; }
            set { toItemCode = value; }
        }
        public string FromUomCode
        {
            get { return fromUomCode; }
            set { fromUomCode = value; }
        }
        public string ToUomCode
        {
            get { return toUomCode; }
            set { toUomCode = value; }
        }

        public int RowStatus
        {
            get { return rowStatus; }
            set { rowStatus = value; }
        }
        public string RepackNo
        {
            get { return repackNo; }
            set { repackNo = value; }
        }
        public int FromItemID
        {
            get { return fromItemID; }
            set { fromItemID = value; }
        }
        public decimal FromQty
        {
            get { return fromQty; }
            set { fromQty = value; }
        }
        public int FromUomID
        {
            get { return fromUomID; }
            set { fromUomID = value; }
        }
        public int ToItemID
        {
            get { return toItemID; }
            set { toItemID = value; }
        }
        public decimal ToQty
        {
            get { return toQty; }
            set { toQty = value; }
        }
        public int ToUomID
        {
            get { return toUomID; }
            set { toUomID = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

    }
}
