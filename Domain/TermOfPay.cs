using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class TermOfPay
    {
        private int id = 0;

        private string termPay = string.Empty;
        



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

        public string TermPay
        {
            get
            {
                return termPay;
            }
            set
            {
                termPay = value;
            }
        }

        

    }
}
