using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PointToko
    {
        private int id = 0;
        private int postingPeriodID = 0;
        private DateTime fromPostingPeriod = DateTime.Now.Date;
        private DateTime toPostingPeriod = DateTime.Now.Date;
        private DateTime postingDate = DateTime.Now.Date;
        private int tokoID = 0;
        private decimal point = 0;

        private string tokoCode = string.Empty;
        private string tokoName = string.Empty;

        public int Id
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

        public int PostingPeriodID
        {
            get
            {
                return postingPeriodID;
            }
            set
            {
                postingPeriodID = value;
            }
        }

        public DateTime FromPostingPeriod
        {
            get
            {
                return fromPostingPeriod;
            }
            set
            {
                fromPostingPeriod = value;
            }
        }

        public DateTime ToPostingPeriod
        {
            get
            {
                return toPostingPeriod;
            }
            set
            {
                toPostingPeriod = value;
            }
        }

        public DateTime PostingDate
        {
            get
            {
                return postingDate;
            }
            set
            {
                postingDate = value;
            }
        }

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

        public decimal Point
        {
            get
            {
                return point;
            }
            set
            {
                point = value;
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

        public string TokoName
        {
            get
            {
                return tokoName;
            }
            set
            {
                tokoName = value;
            }
        }

    }
}
