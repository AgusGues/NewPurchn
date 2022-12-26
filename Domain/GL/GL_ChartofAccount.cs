using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_ChartofAccount : GRCBaseDomain
    {
        private string chartNo = string.Empty;
        private string chartName = string.Empty;
        private int group = 0;
        private int postable = 0;
        private string cCYCode = string.Empty;
        private int level = 0;
        private string parent = string.Empty;
        private int isDept = 0;
        private int isCost = 0;
        private string chartType = string.Empty;
        private string notesNo = string.Empty;

        public string strPostAble { get; set; }
        public string CompanyCode { get; set; }
        public string ChartNo
        {
            get { return chartNo; }
            set { chartNo = value; }
        }
        public string ChartName 
        {
            get { return chartName; }
            set { chartName = value; }
        }
        public int Group
        {
            get { return group; }
            set { group = value; }
        }
        
        public int Postable
        {
            get { return postable; }
            set { postable = value; }
        }
        public string CCYCode
        {
            get { return cCYCode; }
            set { cCYCode = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public string Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public int  IsDept
        {
            get { return isDept; }
            set { isDept = value; }
        }
        public int IsCost
        {
            get { return isCost; }
            set { isCost = value; }
        }
        public string ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }
        public string NotesNo
        {
            get { return notesNo; }
            set { notesNo = value; }
        }
    }
}
