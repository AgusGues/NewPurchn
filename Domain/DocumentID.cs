using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class DocumentID
    {
        private int id = 0;
        private int opId1 = 0;
        private int opId2 = 0;
        private int doId1 = 0;
        private int doId2 = 0;
        private int sjId1 = 0;
        private int sjId2 = 0;
        private int invId1 = 0;
        private int invId2 = 0;
        private string typeID = string.Empty;


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


        public int OPID1
        {
            get
            {
                return opId1;
            }
            set
            {
                opId1 = value;
            }
        }

        public int OPID2
        {
            get
            {
                return opId2;
            }
            set
            {
                opId2 = value;
            }
        }

        public int DOID1
        {
            get
            {
                return doId1;
            }
            set
            {
                doId1 = value;
            }
        }

        public int DOID2
        {
            get
            {
                return doId2;
            }
            set
            {
                doId2 = value;
            }
        }

        public int SJID1
        {
            get
            {
                return sjId1;
            }
            set
            {
                sjId1 = value;
            }
        }

        public int SJID2
        {
            get
            {
                return sjId2;
            }
            set
            {
                sjId2 = value;
            }
        }

        public int INVID1
        {
            get
            {
                return invId1;
            }
            set
            {
                invId1 = value;
            }
        }

        public int INVID2
        {
            get
            {
                return invId2;
            }
            set
            {
                invId2 = value;
            }
        }

        public string TypeID
        {
            get
            {
                return typeID;
            }
            set
            {
                typeID = value;
            }
        }
    }
}
