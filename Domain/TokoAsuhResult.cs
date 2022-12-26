using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class TokoAsuhResult
    {
        private int tokoID = 0;
        private string tokoCode = string.Empty;
        private int jumQty = 0;
        private int blnQty = 0;
        private int blnInvoice = 0;
        private string status = string.Empty;

        public int TokoID
        {
            get
            {
                return tokoID;
            }
            set
            {
                tokoID = value;
            }

        }

        public string TokoCode
        {
            get
            {
                return tokoCode;
            }
            set
            {
                tokoCode = value;
            }
        }

        public int BlnQty
        {
            get
            {
                return blnQty;
            }
            set
            {
                blnQty = value;
            }
        }

        public int JumQty
        {
            get
            {
                return jumQty;
            }
            set
            {
                jumQty = value;
            }
        }


        public int BlnInvoice
        {
            get
            {
                return blnInvoice;
            }
            set
            {
                blnInvoice = value;
            }
        }


        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
            
        }
    }
}
