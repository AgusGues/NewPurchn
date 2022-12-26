using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Items : GRCBaseDomain
    {      
	   private string itemCode = string.Empty;
	   private string description = string.Empty;
	   private int groupID = 0;
       private string groupCode = string.Empty;
       private string groupDescription = string.Empty;
	   private string shortKey = string.Empty;
	   private int gradeID = 0;
       private string gradeCode = string.Empty;
	   private int sisiID = 0;
       private string sisiDescription = string.Empty;
       private int itemType = 0;
	   private decimal tebal = 0;
	   private decimal panjang = 0;
	   private decimal lebar = 0;
	   private int uomId = 0;
       private string uomCode = string.Empty;
	   private decimal berat = 0;
	   private string ket1 = string.Empty;
	   private string ket2 = string.Empty;
	   private int utuh = 0;
       private int paket = 0;
       private int itemTypeId = 0;
       private string groupCategory = string.Empty;
       private int isQuota = 0;
       private int otherType = 0;
       private int head = 0;
       private int flagReport = 0;

       public int FlagReport
       {
           get
           {
               return flagReport;
           }
           set
           {
               flagReport = value;
           }
       }

       public string ItemCode
       {
           get
           {
               return itemCode;
           }
           set
           {
               itemCode = value;
           }
       }

       public string Description
       {
           get
           {
               return description;
           }
           set
           {
               description = value;
           }
       }

       public int GroupID
       {
           get
           {
               return groupID;
           }
           set
           {
               groupID = value;
           }
       }

       public string GroupCode
       {
           get
           {
               return groupCode;
           }
           set
           {
               groupCode = value;
           }
       }

       public string  GroupDescription
       {
           get
           {
               return groupDescription;
           }
           set
           {
               groupDescription = value;
           }
       }

       public string ShortKey
       {
           get
           {
               return shortKey;
           }
           set
           {
               shortKey = value;
           }
       }

       public int GradeID
       {
           get
           {
               return gradeID;
           }
           set
           {
               gradeID = value;
           }
       }

       public string GradeCode
       {
           get
           {
               return gradeCode;
           }
           set
           {
               gradeCode = value;
           }
       }
       
       public int SisiID
       {
           get
           {
               return sisiID;
           }
           set
           {
               sisiID = value;
            }
       }

       public string SisiDescription
       {
           get
           {
               return sisiDescription;
           }
           set
           {
               sisiDescription = value;
           }
       }

       public int ItemType
       {
           get
           {
               return itemType;
           }
           set
           {
               itemType = value;
           }
       }

       public decimal Tebal
       {
           get
           {
               return tebal;
           }
           set
           {
               tebal = value;
           }
       }

       public decimal Panjang
       {
           get
           {
               return panjang;
           }
           set
           {
               panjang = value;
           }
       }

       public decimal Lebar
       {
           get
           {
               return lebar;
           }
           set
           {
               lebar = value;
           }
       }

       public int UOMID
       {
           get
           {
               return uomId;
           }
           set
           {
               uomId = value;
           }
       }

       public string UOMCode
       {
           get
           {
               return uomCode;
           }
           set
           {
               uomCode = value;
           }
       }

       public decimal Berat
       {
           get
           {
               return berat;
           }
           set
           {
               berat = value;
           }
       }

       public string Ket1
       {
           get
           {
               return ket1;
           }
           set
           {
               ket1 = value;
           }
       }

       public string Ket2
       {
           get
           {
               return ket2;
           }
           set
           {
               ket2 = value;
           }
       }

       public int Utuh
       {
           get
           {
               return utuh;
           }
           set
           {
               utuh = value;
           }
       }

       public int Paket
       {
           get
           {
               return paket;
           }
           set
           {
               paket = value;
           }
       }

       public int ItemTypeID
       {
           get
           {
               return itemTypeId;
           }
           set
           {
               itemTypeId = value;
           }
       }

       public string GroupCategory
       {
           get
           {
               return groupCategory;
           }
           set
           {
               groupCategory = value;
           }
       }

       public int IsQuota
       {
           get
           {
               return isQuota;
           }
           set
           {
               isQuota = value;
           }
       }

       public int OtherType
       {
           get
           {
               return otherType;
           }
           set
           {
               otherType = value;
           }

       }

       public int Head
       {
           get
           {
               return head;
           }
           set
           {
               head = value;
           }
       }
 
    }
}
