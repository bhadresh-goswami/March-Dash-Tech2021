using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashTechCMS.Models;
using DashTechCMS.Models.Messages;
using DashTechCMS.Models.User;

namespace DashTechCMS.Areas.User.Controllers
{
    public class AuthenticationController : Controller
    {
        dashrdku_database db = new dashrdku_database();
        // GET: User/Authentication
        public ActionResult NewUser()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string emailid, string password)
        {

            string userRole = "";

            UserInfoModelLogic userLogic = new UserInfoModelLogic();
            UserInfoModel userInfo = userLogic.getUser(emailid, password);
            if (userInfo != null)
            {
                Session["userInfo"] = userInfo;
                ConfigurationManager.AppSettings.Set("userRole", userInfo.UserRole);
                TempData["alert"] = new AlertBoxModel() { Type = "Success", Message = "User Login Successfully!" };

                return Redirect(userInfo.defaultUrl);
            }
            TempData["alert"] = new AlertBoxModel() { Type = "Error", Message = "Email Id or Password is Wrong!" };

            //if (Response.Cookies["User"] != null)
            //{
            //    HttpCookie userInfo = new HttpCookie("User");
            //    userRole = userInfo["userRole"].ToString();
            //    //userInfo["UserName"] = "Annathurai";
            //    //userInfo["UserColor"] = "Black";
            //    //userInfo.Expires.Add(new TimeSpan(0, 1, 0));
            //    //Response.Cookies.Add(userInfo);
            //}
            //else
            //{
            //    //@Html.Action("LogOut", "Authentication", new { @area = "User" })
            //}
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}