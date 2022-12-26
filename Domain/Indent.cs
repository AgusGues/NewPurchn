using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Indent
    {
        private int id = 0;

        private string tenggang = string.Empty;




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

        public string Tenggang
        {
            get
            {
                return tenggang;
            }
            set
            {
                tenggang = value;
            }
        }



    }
}