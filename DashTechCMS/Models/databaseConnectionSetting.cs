using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models
{
    public class databaseConnectionSetting
    {
        public static SqlConnection getConn()
        {
           return new SqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString);
        }
    }
}