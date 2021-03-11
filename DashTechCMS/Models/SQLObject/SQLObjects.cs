using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.SQLObject
{
    public class SQLObjects
    {
       public SqlConnection sqlConnection = new SqlConnection();
       public SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
       public DataTable dataTable = new DataTable();
        public SQLObjects()
        {
            sqlConnection = databaseConnectionSetting.getConn();
        }
    }
}