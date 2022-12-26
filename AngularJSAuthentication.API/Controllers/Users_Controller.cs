using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessFacade;
using Domain;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Users_")]
    public class Users_Controller : ApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            //return Ok(Users_.RetrieveByUserName("iko"));
            return Ok(Users_.RetrieveByUserName2("iko"));
        }
        [Authorize]
        [Route("user/{username}")]
        public IHttpActionResult GetUserByName(string username)
        {
            var user = Users_.RetrieveByUserName2(username);

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [Authorize]
        [Route("user1/{username}")]
        public IHttpActionResult GetUserByName1(string username)
        {
            var user = Users_.RetrieveByUserName1(username);

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
    }


    #region Helpers
    public class Users_

    {
        public string UsersName { get; set; }
        public static List<Users> RetrieveByUserName(string strName)
        {
            UsersFacade usersFacade = new UsersFacade();
            List<Users> usersList = new List<Users>();
            {
                usersList = usersFacade.RetrieveByUserName(strName);
            };
            return usersList;

            //List<Users_> usersList = new List<Users_>
            //    {
            //        new Users_ {UsersName = "Hore" }
            //    };
            //return usersList;
        }
        public static ArrayList RetrieveByUserName1(string strName)
        {
            UsersFacade usersFacade = new UsersFacade();
            //List<Users> usersList = new List<Users>();
            ArrayList usersList = new ArrayList();
            {
                usersList = usersFacade.RetrieveByUserName1(strName);
            };
            return usersList;

            //List<Users_> usersList = new List<Users_>
            //    {
            //        new Users_ {UsersName = "Hore" }
            //    };
            //return usersList;
        }
        public static Users RetrieveByUserName2(string strName)
        {
            UsersFacade usersFacade = new UsersFacade();
            Users usersList = usersFacade.RetrieveByUserName2(strName);
            return usersList;
        }


    }
    #endregion
}
