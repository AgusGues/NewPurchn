using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ApvPakai
    {
        public class ParamData
        {
            public int ID { get; set; }
            public string PakaiNo { get; set; }
            public string PakaiDate { get; set; }
            public string CreatedBy { get; set; }
            public int Status { get; set; }

        }
        public class ParamDetail
        {
            public int ID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Quantity { get; set; }
            public string Satuan { get; set; }
            public string Keterangan { get; set; }

        }
        public class ParamMsgApv
        {
            public string MsgAprov { get; set; }
        }
    }
}
