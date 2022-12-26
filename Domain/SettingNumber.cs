using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SettingNumber
    {
        private int id = 0;
        private string modul = string.Empty;
        private string format = string.Empty;
        private string fromNo = string.Empty;
        private string toNo = string.Empty;

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

        public string Modul
        {
            get
            {
                return modul;
            }
            set
            {
                modul = value;
            }
        }

        public string Format
        {
            get
            {
                return format;
            }
            set
            {
                format = value;
            }
        }

        public string FromNo
        {
            get
            {
                return fromNo;
            }
            set
            {
                fromNo = value;
            }
        }
        public string ToNo
        {
            get
            {
                return toNo;
            }
            set
            {
                toNo = value;
            }
        }
    }
}
