using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain;
using BusinessFacade;
using System.Collections;
using BusinessFacade.API.Purchase;
using System.Threading.Tasks;
using Domain.API.Purchase;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Purchase_")]
    public class PurchaseController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(Purchase_.Retrieve());
        }
        [Authorize]
        [HttpGet]
        [Route("GetLeadTimePurchase")]
        public IHttpActionResult GetLeadTimePurchase()
        {
            return Ok(Purchase_.GetLeadTimePurchase());
        }
        [Authorize]
        [HttpGet]
        [Route("LoadTipeSPP")]
        public IHttpActionResult LoadTipeSPP(int deptID, int itemTypeID)
        {
            return Ok(Purchase_.LoadTipeSPP(deptID,itemTypeID));
        }
        [Authorize]
        [HttpGet]
        [Route("LoadItemName")]
        public IHttpActionResult LoadItemName(int intTipeBarang, string strNabar, int intTipeSPP, int intUserID)
        {
            return Ok(Purchase_.LoadItemName(intTipeBarang, strNabar, intTipeSPP, intUserID));
        }
        [Authorize]
        [HttpGet]
        [Route("LoadInventoryF_RetrieveById")]
        public IHttpActionResult LoadInventoryF_RetrieveById(int intItemID)
        {
            return Ok(Purchase_.InventoryF_RetrieveById(intItemID));
        }


        // POST api/Account/Register
        [AllowAnonymous]
        [Route("SPPInsert1")] //?????
        public async Task<SPP> SPPInsert2(List<SPP> sppHeader)  //nambah 1 variabel string / int bisa
        {
            SPP result = await Purchase_.InsertSPP(sppHeader);
            return result;
        }






    }
    #region Helpers
    public class Purchase_
    {
        public static ArrayList Retrieve()
        {
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            ArrayList listRules = new ArrayList();
            {
                listRules = itemTypePurchnFacade.Retrieve();
            };
            return listRules;
        }
        public static ArrayList GetLeadTimePurchase()
        {
            LeadTimePurchaseFacade leadTimePurchaseFacade = new LeadTimePurchaseFacade();
            ArrayList arrGroup = new ArrayList();
            {
                arrGroup = leadTimePurchaseFacade.Retrieve();
            };
            return arrGroup;
        }
        public static ArrayList LoadTipeSPP(int idDept, int idItemType)
        {
            GroupsPurchnFacade groupPurchaseFacade = new GroupsPurchnFacade();
            ArrayList arrGroup = new ArrayList();
            {
                arrGroup = groupPurchaseFacade.RetrieveByGroupID(idDept, idItemType);
            };
            return arrGroup;
        }
        public static ArrayList LoadItemName(int intTipeBarang, string strNabar, int intTipeSPP, int userID)
        {
            AssetManagementFacade aMF = new AssetManagementFacade();
            InventoryFacade inventoryFacade = new InventoryFacade();
            AssetFacade assetFacade = new AssetFacade();
            BiayaFacade biayaFacade = new BiayaFacade();
            ArrayList arrItems = new ArrayList();
            if (intTipeBarang == 1)
            {
                arrItems = inventoryFacade.RetrieveByCriteriaWithGroupIDROP("ItemName", strNabar, intTipeSPP, userID);
            }
            if (intTipeBarang == 2)
            {
                arrItems = assetFacade.RetrieveByCriteriaWithGroupID("ItemName", strNabar, intTipeSPP);
            }
            if (intTipeBarang == 3)
            {
                /** new spp biaya tdk aktif**/
                AccClosingFacade cls = new AccClosingFacade();
                AccClosing stat = cls.CheckBiayaAktif();
                if (stat.Status != 1)
                {
                    arrItems = biayaFacade.RetrieveByCriteriaWithGroupID("ItemName", strNabar, intTipeSPP);
                }
                else
                {
                    /** new spp biaya aktif**/
                    arrItems = biayaFacade.RetrieveByCriteriaWithGroupID("len(itemcode)=15 and ItemTypeID", "3", intTipeSPP);
                }
            }
            return arrItems;
        }
        public static Inventory InventoryF_RetrieveById(int id)
        {
            InventoryFacade inventoryFacade = new InventoryFacade();
            Inventory inventory = new Inventory();
            {
                inventory = inventoryFacade.RetrieveById(id);
            };
            return inventory;
        }
        public static async Task<SPP> InsertSPP(List<SPP> dataSPP1)
        {
            string strError = string.Empty;
            SPPNumber sPPNumber = new SPPNumber();
            sPPNumber.Flag = dataSPP1[0].sppNumber_domain.Flag;
            sPPNumber.GroupsPurchnID = dataSPP1[0].sppNumber_domain.GroupsPurchnID;

            SPP sPP = new SPP();
            sPP = (SPP)dataSPP1[0];

            ArrayList arrSPPDetail = new ArrayList();

            SPPProcessFacade sPPProsessFacade = new SPPProcessFacade(sPP,arrSPPDetail,sPPNumber);


            strError = sPPProsessFacade.Insert();
            if (strError == string.Empty)
            {

            }


            SPP resultSPP = new SPP();
            Inventory resultInven = new Inventory();

            resultSPP.NoSPP = "Inven_No";

            await Task.Delay(1000);
            return resultSPP;
        }





    }
    #endregion




}
