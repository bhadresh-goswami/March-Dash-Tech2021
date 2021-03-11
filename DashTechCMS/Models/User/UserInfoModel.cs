using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashTechCMS.Models.User
{
    public class UserInfoModel
    {
        public int UserId { get; set; }
        public string UserRole { get; set; }
        public string UserRocketName { get; set; }
        public string defaultUrl { get; set; }

    }
    public class UserInfoModelLogic
    {
        dashrdku_database db = new dashrdku_database();

        public UserInfoModel getUser(string emailid, string password)
        {
            var user = db.UserAccountDetails.SingleOrDefault(u => u.EmailId == emailid && u.Password == password);
            if (user != null)
            {
                UserInfoModel userInfo = new UserInfoModel()
                {
                    UserId = user.UserId,
                    UserRocketName = user.RocketName,
                    UserRole = user.RoleMaster.RoleTitle
                };
                string defaultUrl = ConfigurationManager.AppSettings["url"].ToString();
                switch (user.RoleMaster.RoleTitle)
                {
                    case "admin":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Expert CV Coach":
                        userInfo.defaultUrl = defaultUrl + "expertcvcoach/dashboard/Index";
                        break;
                    case "Lead Generator":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Marketing Manager":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Marketing Team Lead":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Master":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "On Boarding":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Recruiter":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Sales Associate":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Sales Manager":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Sales Team Lead":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Technical Expert":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Technical KL":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Technical Team Lead":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    case "Technical Team Manager":
                        userInfo.defaultUrl = defaultUrl + "admin/dashboard/Index";
                        break;
                    default:
                        break;
                }
                return userInfo;
            }
            return null;
        }
    }
}