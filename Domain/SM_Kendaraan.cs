namespace Domain
{
    public class SM_Kendaraan : GRCBaseDomain
    {
        private string jenisKendaraan = string.Empty;
        private decimal target = 0;
        private int status = 0;


        public string JenisKendaraan
        {
            get { return jenisKendaraan; }
            set { jenisKendaraan = value; }
        }

        public decimal Target
        {
            get { return target; }
            set { target = value; }
        }

        public int Status
        {
            get { return status ; }
            set { status = value; }
        }


    }
}
