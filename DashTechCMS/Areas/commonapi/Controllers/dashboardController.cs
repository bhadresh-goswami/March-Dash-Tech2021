using DashTechCMS.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DashTechCMS.Areas.commonapi.Controllers
{
    public class dashboardController : ApiController
    {
        [HttpGet]
        public InfoBoxModel GetInfoBox()
        {
            InfoBoxModel infoBox = new InfoBoxModel();
            //infoBox.GetDetails();
            return infoBox;
        }
    }
}
