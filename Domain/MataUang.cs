using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MataUang
    {
        private int id = 0;
        private string nama = string.Empty;
        private string lambang = string.Empty;
        private string twoiso = string.Empty;
        
        public string Twoiso
        {
            get
            {
                return twoiso;
            }
            set
            {
                twoiso = value;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Nama
        {
            get
            {
                return nama;
            }
            set
            {
                nama = value;
            }
        }

        public string Lambang
        {
            get
            {
                return lambang;
            }
            set
            {
                lambang = value;
            }
        }

        
    }

    public class ListKurs:GRCBaseDomain
    {
        public virtual DateTime Tanggal { get; set; }
        public virtual decimal USD { get; set; }
        public virtual decimal JPY { get; set; }
        public virtual decimal SGD { get; set; }
        public virtual decimal EUR { get; set; }
        public virtual DateTime sdTanggal { get; set; }
        public virtual string Periode { get; set; }
    }
}
