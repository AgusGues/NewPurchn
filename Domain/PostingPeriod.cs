using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PostingPeriod
    {
        private int id = 0;
        private DateTime fromPostingPeriod = DateTime.Now.Date;
        private DateTime toPostingPeriod = DateTime.Now.Date;
        private int isActive = 0;

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

        public int IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }
    }
}
