using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain;
using BusinessFacade;
using System.Threading.Tasks;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Menus_")]
    public class MenusController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(Menus_.RetrieveByAllMenuActive());
        }

        [Authorize]
        [HttpGet]
        [Route("RetrieveByUserID")]
        public IHttpActionResult RetrieveByUserID(int userID)
        {
            return Ok(Menus_.RetrieveByUserID(userID));
        }

        [Authorize]
        [HttpGet]
        [Route("RetrieveByIDname")]
        public IHttpActionResult RetrieveByIDname(string idName)
        {
            return Ok(Menus_.RetrieveByIDname(idName));
        }

        [Authorize]
        [HttpGet]
        [Route("RetrieveBySortAndLevel")]
        public IHttpActionResult RetrieveBySortAndLevel(string sort, int level)
        {
            return Ok(Menus_.RetrieveBySortAndLevel(sort,level));
        }


    }
    #region Helpers
    public class Menus_
    {
        public static List<Rules> RetrieveByAllMenuActive()
        {
            RulesFacade rulesFacade = new RulesFacade();
            List<Rules> listRules = new List<Rules>();
            {
                listRules = rulesFacade.RetrieveByAllMenuActive();
            };
            return listRules;
        }
        public static List<Rules> RetrieveByUserID(int intUserID)
        {
            RulesFacade rulesFacade = new RulesFacade();
            List<Rules> listRules = new List<Rules>();
            {
                listRules = rulesFacade.RetrieveByUserID(intUserID);
            };
            return listRules;
        }
        public static Rules RetrieveByIDname(string IDname)
        {
            RulesFacade rulesFacade = new RulesFacade();
            Rules listRules = new Rules();
            {
                listRules = rulesFacade.RetrieveByIDname(IDname);
            };
            return listRules;
        }
        public static Rules RetrieveBySortAndLevel(string sort, int level)
        {
            RulesFacade rulesFacade = new RulesFacade();
            Rules listRules = new Rules();
            {
                listRules = rulesFacade.RetrieveBySortAndLevel(sort,level);
            };
            return listRules;
        }

    }
    #endregion
}
