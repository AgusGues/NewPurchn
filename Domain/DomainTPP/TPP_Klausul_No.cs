using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TPP_Klausul_No : GRCBaseDomain
    {
        private string klausul_No = string.Empty;
        public string Klausul_No { get { return klausul_No; } set { klausul_No = value; } }
        private string deskripsi = string.Empty;
        public string Deskripsi { get { return deskripsi; } set { deskripsi = value; } }
    }
}
