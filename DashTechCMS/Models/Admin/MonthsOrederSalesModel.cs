using DashTechCMS.Models.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.Admin
{
    public class MonthsOrederSalesModel
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string SalesAssociateName { get; set; }
        public string  Technology { get; set; }
        public DateTime Date { get; set; }
        public int PaidAmount { get; set; }
    }

    public class MonthsOrderSalesModelLogic
    {
        public List<MonthsOrederSalesModel> list;
        public string GetDetails()
        {
            list = new List<MonthsOrederSalesModel>();
            try
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = endDate.AddMonths(1).AddDays(-1);

                SQLObjects sqlObj = new SQLObjects();
                sqlObj.sqlConnection = databaseConnectionSetting.getConn();
                string salesRevenue = string.Format(@"
select CandidateName,CandidateId,FullName,TechTitle,PaidDate,PaidAmount from CandidateMaster 
inner join UserAccountDetails on RefSalesAssociate = UserAccountDetails.UserId
inner join TechnologyMaster on TechId = TechnologyId
inner join RecurringMaster on CandidateId = RefCandidateId
where PaidDate between '{0}' and '{1}'",startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd"));
                sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(salesRevenue,sqlObj.sqlConnection);
                sqlObj.dataTable = new System.Data.DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                sqlObj.sqlConnection.Close();

                foreach (DataRow item in sqlObj.dataTable.Rows)
                {
                    MonthsOrederSalesModel model = new MonthsOrederSalesModel();
                    model.CandidateId = int.Parse(item["CandidateId"].ToString());
                    model.CandidateName = item["CandidateName"].ToString();
                    model.Date = DateTime.Parse(item["PaidDate"].ToString());
                    model.PaidAmount = int.Parse(item["PaidAmount"].ToString());
                    model.Technology = item["TechTitle"].ToString();
                    model.SalesAssociateName = item["FullName"].ToString();
                    list.Add(model);
                }

                return "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}