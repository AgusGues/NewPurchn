using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
   public class BankAccount : GRCBaseDomain
    {
       private int banksID = 0;
       private string bankCode = string.Empty;
       private string bankName = string.Empty;
       private string keteranganBank = string.Empty;
       private string bankAccountNo = string.Empty;
       private int accountType = 0;
       private int currency = 0;
       private string address = string.Empty;        
       private DateTime joinDate = DateTime.Now.Date;
       private int zonaID = 0;
       private string zonaCode = string.Empty;
       private string telepon = string.Empty;
       private string contactPerson = string.Empty;
       private int depoID = 0;
       private int branchID = 0;
       private string depoName = string.Empty;


       public int BanksID
       {
           get
           {
               return banksID;
           }
           set
           {
               banksID = value;
           }
       }

        public string BankCode
        {
            get
            {
                return bankCode;
            }
            set
            {
                bankCode = value;
            }
        }

       public string BankName
        {
            get
            {
                return bankName;
            }
            set
            {
                bankName = value;
            }
        }

       public string KeteranganBank
       {
           get
           {
               return keteranganBank;
           }
           set
           {
               keteranganBank = value;
           }
       }
        
       public string BankAccountNo
        {
            get
            {
                return bankAccountNo;
            }
            set
            {
                bankAccountNo = value;
            }
        }

       public int AccountType
       {
           get
           {
               return accountType;
           }
           set
           {
               accountType = value;
           }
       }

       public int Currency
       {
           get
           {
               return currency;
           }
           set
           {
               currency = value;
           }
       }

       public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        
        public DateTime JoinDate
        {
            get
            {
                return joinDate;
            }
            set
            {
                joinDate = value;
            }
        }

        public int ZonaID
        {
            get
            {
                return zonaID;
            }
            set
            {
                zonaID = value;
            }
        }

        public string ZonaCode
        {
            get
            {
                return zonaCode;
            }
            set
            {
                zonaCode = value;
            }
        }

        public string Telepon
        {
            get
            {
                return telepon;
            }
            set
            {
                telepon = value;
            }
        }

        public string ContactPerson
        {
            get
            {
                return contactPerson;
            }
            set
            {
                contactPerson = value;
            }
        }

        public int DepoID
        {
            get
            {
                return depoID;
            }
            set
            {
                depoID = value;
            }
        }

        public int BranchID
        {
            get
            {
                return branchID;
            }
            set
            {
                branchID = value;
            }
        }
        public string DepoName
        {
            get
            {
                return depoName;
            }
            set
            {
                depoName = value;
            }
        }
    }
}
