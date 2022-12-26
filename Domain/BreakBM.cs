using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class BreakBM : GRCBaseDomain
    {
        private string breakDownNo = string.Empty;
        private DateTime tglBreak = DateTime.Now.Date;
        private string operationalSche = string.Empty;
        //private string startBD = string.Empty;
        //private string finishBD = string.Empty;
        //private DateTime bDTime = DateTime.Now.Date;
        private int frekBD = 0;
        private string syarat = string.Empty;
        private int bm_plantGroupID = 0;
        private int bm_PlantID = 0;
        private string operationalTime = string.Empty;
        private string ket = string.Empty;
        private decimal pinalti = 0;
        private int breakBM_MasterProblemID = 0;
        private int breakBM_MasterChargeID = 0;
        private string ketebalan = string.Empty;

        public string Ketebalan
        {
            get { return ketebalan; }
            set { ketebalan = value; }
        }
        public DateTime TglBreak
        {
            get { return tglBreak; }
            set { tglBreak = value; }
        }
        public string BreakDownNo
        {
            get { return breakDownNo; }
            set { breakDownNo = value; }
        }
        public string OperationalSche
        {
            get { return operationalSche; }
            set { operationalSche = value; }
        }
        //public string StartBD
        //{
        //    get { return startBD; }
        //    set { startBD = value; }
        //}
        //public string FinishBD
        //{
        //    get { return finishBD; }
        //    set { finishBD = value; }
        //}
        //BDTIME
        public DateTime StartBD { get; set; }
        public DateTime FinishBD { get; set; }
        public DateTime BDTime { get; set; }
        public DateTime FinaltyBD { get; set; }


        public int FrekBD
        {
            get { return frekBD; }
            set { frekBD = value; }
        }
        public string Syarat
        {
            get { return syarat; }
            set { syarat = value; }
        }
        public int BM_plantGroupID
        {
            get { return bm_plantGroupID; }
            set { bm_plantGroupID = value; }
        }
        public int BM_PlantID
        {
            get { return bm_PlantID; }
            set { bm_PlantID = value; }
        }
        public string OperationalTime
        {
            get { return operationalTime; }
            set { operationalTime = value; }
        }
        public string Ket
        {
            get { return ket; }
            set { ket = value; }
        }
        public decimal Pinalti
        {
            get { return pinalti; }
            set { pinalti = value; }
        }
        public int BreakBM_MasterProblemID
        {
            get { return breakBM_MasterProblemID; }
            set { breakBM_MasterProblemID = value; }
        }
        public int BreakBM_MasterChargeID
        {
            get { return breakBM_MasterChargeID; }
            set { breakBM_MasterChargeID = value; }
        }
        public string GroupOff
        {get; set;}

        //public string MasterPlanID
        //{ get; set; }
    }

    public class planName : BreakBM
    {
        public int ID
        { get; set; }
        public string PlantName
        { get; set; }
        public string PlantCode
        { get; set; }
        public string KodeSemen
        { get; set; }
        public string KodeKalsium
        { get; set; }

    }
}
