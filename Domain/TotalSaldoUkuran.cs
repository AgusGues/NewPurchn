using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TotalSaldoUkuran : GRCBaseDomain
    {
        private decimal saldoawalbp = 0;
        private decimal saldoawalbpkubik = 0;
        private decimal saldobp = 0;
        private decimal saldobpkubik = 0;
        private decimal saldoawalok = 0;
        private decimal saldoawalokkubik = 0;
        private decimal saldook = 0;
        private decimal saldookkubik = 0;

        public decimal Saldoawalbp { get { return saldoawalbp; } set { saldoawalbp = value; } }
        public decimal Saldoawalbpkubik { get { return saldoawalbpkubik; } set { saldoawalbpkubik = value; } }
        public decimal Saldobp { get { return saldobp; } set { saldobp = value; } }
        public decimal Saldobpkubik { get { return saldobpkubik; } set { saldobpkubik = value; } }
        public decimal Saldoawalok { get { return saldoawalok; } set { saldoawalok = value; } }
        public decimal Saldoawalokkubik { get { return saldoawalokkubik; } set { saldoawalokkubik = value; } }
        public decimal Saldook { get { return saldook; } set { saldook = value; } }
        public decimal Saldookkubik { get { return saldookkubik; } set { saldookkubik = value; } }
    }
}
