using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterKurs
    {
        private int id = 0;
        private string nama = string.Empty;
        private string lambang = string.Empty;
        private string twoiso = string.Empty;
        
        public string TwoIso
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
}
