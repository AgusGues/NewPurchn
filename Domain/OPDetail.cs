using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class OPDetail : GRCBaseDomain
    {
       private int oPID = 0;
       private int groupId = 0;
       private string groupName = string.Empty;
	   private int itemID = 0;
       private string itemCode = string.Empty;
       private string itemName = string.Empty;
       private string uomCode = string.Empty;
       private int itemType = 0;
	   private int quantity = 0;
       private int quantity2 = 0;
       private int qtyScheduled = 0;
       private int qtyReceived = 0;
	   private decimal price = 0;
	   private decimal disc = 0;
	   private decimal totalPrice = 0;
       private decimal point = 0;
       private decimal tebal = 0;
       private decimal panjang = 0;
       private decimal lebar = 0;
       private decimal berat = 0;
       private string groupCategory = string.Empty;
       private int quota = 0;
       private int tokoID = 0;
       private decimal priceDefault = 0;
       private int paket = 0;
       private int depoID = 0;
       private int flag = 0;
       private decimal priceList = 0;
       private string mbt = string.Empty;
       private int mbtID = 0;
       private int imsID = 0;

       private int promoItemID = 0;
       private int promoFlag = 0;
       private int promoID = 0;
       private string ket1 = string.Empty;
       private int zonaID = 0;
       private int distSubID = 0;
       private int promo214ID = 0;
       private int itemIDPaket = 0; // utk QUOTA GELAS
       private int idTempo0 = 0; // utk QUOTA GELAS
       private int itemsPoint = 0; // utk QUOTA GELAS
       private int quotaItemID = 0; // utk QUOTA GELAS
       private string kodeVoucher = string.Empty;
       private decimal debtInsurance = 0;
       private decimal claimVoucher = 0;
       private decimal nominal = 0;
       private decimal point_bak = 0;
       private decimal point_star = 0;
       private decimal potonganDisc = 0;
       private string promoCode = string.Empty;
       private decimal priceRetail = 0;
       private DateTime rencanaTglKirim = DateTime.MinValue;

       public DateTime RencanaTglKirim
       {
           get { return rencanaTglKirim; }
           set { rencanaTglKirim = value; }
       }
       public int ImsID
       {
           get { return imsID; }
           set { imsID = value; }
       }
       public decimal PriceRetail
       {
           get { return priceRetail; }
           set { priceRetail = value; }
       }
       public string PromoCode
       {
           get { return promoCode; }
           set { promoCode = value; }
       }
       public decimal PotonganDisc
       {
           get { return potonganDisc; }
           set { potonganDisc = value; }
       }
       public decimal Point_star
       {
           get { return point_star; }
           set { point_star = value; }
       }
       public decimal Point_bak
       {
           get { return point_bak; }
           set { point_bak = value; }
       }
       public decimal Nominal
       {
           get { return nominal; }
           set { nominal = value; }
       }
       public decimal ClaimVoucher
       {
           get { return claimVoucher; }
           set { claimVoucher = value; }
       }
       public decimal DebtInsurance
       {
           get { return debtInsurance; }
           set { debtInsurance = value; }
       }
       public string KodeVoucher
       {
           get { return kodeVoucher; }
           set { kodeVoucher = value; }
       }
       public int QuotaItemID
       {
           get { return quotaItemID; }
           set { quotaItemID = value; }
       }
       public int ItemsPoint
       {
           get
           {
               return itemsPoint;
           }
           set
           {
               itemsPoint = value;
           }
       }
       public int IDTempo0
       {
           get
           {
               return idTempo0;
           }
           set
           {
               idTempo0 = value;
           }
       }
       public int ItemIDPaket
       {
           get
           {
               return itemIDPaket;
           }
           set
           {
               itemIDPaket = value;
           }
       }

       public int Promo214ID
       {
           get
           {
               return promo214ID;
           }
           set
           {
               promo214ID = value;
           }
       }
       public int DistSubID
       {
           get
           {
               return distSubID;
           }
           set
           {
               distSubID = value;
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
       public int PromoID
       {
           get
           {
               return promoID;
           }
           set
           {
               promoID = value;
           }
       }
       public int PromoFlag
       {
           get
           {
               return promoFlag;
           }
           set
           {
               promoFlag = value;
           }
       }
       public int PromoItemID
       {
           get
           {
               return promoItemID;
           }
           set
           {
               promoItemID = value;
           }
       }
       public int MbtID
       {
           get
           {
               return mbtID;
           }
           set
           {
               mbtID = value;
           }
       }
       public string MBT
       {
           get
           {
               return mbt;
           }
           set
           {
               mbt = value;
           }
       }
       public int OPID
       {
           get
           {
               return oPID;
           }
           set
           {
               oPID = value;
           }
       }

       public int GroupID
       {
           get
           {
               return groupId;
           }
           set
           {
               groupId = value;
           }
       }

       public string GroupName
       {
           get
           {
               return groupName;
           }
           set
           {
               groupName = value;
           }
       }

       public int ItemID
       {
           get
           {
               return itemID;
           }
           set
           {
               itemID = value;
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

       public string ItemName
       {
           get
           {
               return itemName;
           }
           set
           {
               itemName = value;
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

       public int Quantity
       {
           get
           {
               return quantity;
           }
           set
           {
               quantity = value;
           }
       }

       public int Quantity2
       {
           get
           {
               return quantity2;
           }
           set
           {
               quantity2 = value;
           }
       }

       public int QtyScheduled
       {
           get
           {
               return qtyScheduled;
           }
           set
           {
               qtyScheduled = value;
           }
       }

       public int QtyReceived
       {
           get
           {
               return qtyReceived;
           }
           set
           {
               qtyReceived = value;
           }
       }

       public decimal Price
       {
           get
           {
               return price;
           }
           set
           {
               price = value;
           }
       }

       public decimal Disc
       {
           get
           {
               return disc;
           }
           set
           {
               disc = value;
           }
       }

       public decimal TotalPrice
       {
           get
           {
               return totalPrice;
           }
           set
           {
               totalPrice = value;
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

       public int Quota
       {
           get
           {
               return quota;
           }
           set
           {
               quota = value;
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

       public decimal PriceDefault
       {
           get
           {
               return priceDefault;
           }
           set
           {
               priceDefault = value;
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

       public int Flag
       {
           get
           {
               return flag;
           }
           set
           {
               flag = value;
           }
       }

       public decimal PriceList
       {
           get
           {
               return priceList;
           }
           set
           {
               priceList = value;
           }
       }
    }
}
