using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SuratJalanDetailTO : SuratJalanDetail
    {
        private int suratJalanTOId = 0;
        private int typeKondisi = 0;
        private int fromDepoID = 0;

        public int FromDepoID
        {
            get
            {
                return fromDepoID;
            }
            set
            {
                fromDepoID = value;
            }
        }
        public int TypeKondisi
        {
            get
            {
                return typeKondisi;
            }
            set
            {
                typeKondisi = value;
            }
        }
        public int SuratJalanTOID
        {
            get
            {
                return suratJalanTOId;
            }
            set
            {
                suratJalanTOId = value;
            }
        }


    }
}
