using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BItemName : GRCBaseDomain
    {
        string inCode = string.Empty;
        string itemName = string.Empty;

        public string ItemName
        { get { return itemName; } set { itemName = value; } }
        public string InCode
        { get { return inCode; } set { inCode = value; } }
    }
}
