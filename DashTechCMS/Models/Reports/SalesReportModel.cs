using DashTechCMS.Models.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.Reports
{
    public class Sales_Monthly
    {
        public string Month { get; set; }
        public double Amount { get; set; }
    }
    public class Sales_Monthly_Locaiton:Sales_Monthly
    {
        public string Location { get; set; }
    }
    public class Sales_Monthly_Location_Associates:Sales_Monthly_Locaiton
    {
        public string AssociatesName { get; set; }
    }

    public class SalesLogic
    {
        public List<Sales_Monthly_Location_Associates> getDetails_Refund_Monthly()
        {
            List<Sales_Monthly_Location_Associates> refunded_Monthly = new List<Sales_Monthly_Location_Associates>();
            string salesQuery = @" 
select FORMAT(PaidDate, 'yyyy-MM') as MonthDetails, LocationName,FullName, sum(amount) as PaidAmount  from RecurringMaster 
inner join CandidateMaster on CandidateId = RefCandidateId
inner join UserAccountDetails on UserId = RefSalesAssociate 
inner join LocationMaster on RefLocationId = LocationId
where PaidDate is not null and RecurringMaster.PaymentStatus = 'Refund'
group by FORMAT(PaidDate, 'yyyy-MM'), LocationName, FullName ";

            SQLObjects sqlObj = new SQLObjects();
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(salesQuery, sqlObj.sqlConnection);
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);

            foreach (DataRow row in sqlObj.dataTable.Rows)
            {

                Sales_Monthly_Location_Associates model = new Sales_Monthly_Location_Associates();
                model.Amount = double.Parse(row["PaidAmount"].ToString());
                model.Month = row["MonthDetails"].ToString();
                model.Location = row["LocationName"].ToString();
                model.AssociatesName = row["FullName"].ToString();
                refunded_Monthly.Add(model);
            }
            return refunded_Monthly;
        }
        public List<Sales_Monthly> getDetails_Sales_Monthly()
        {
            List<Sales_Monthly> sales_Monthly = new List<Sales_Monthly>();
            string salesQuery = @"
select FORMAT(PaidDate, 'yyyy-MM') as MonthDetails, sum(amount) as PaidAmount  from RecurringMaster 
inner join CandidateMaster on CandidateId = RefCandidateId
inner join UserAccountDetails on UserId = RefSalesAssociate 
inner join LocationMaster on RefLocationId = LocationId
where PaidDate is not null and RecurringMaster.PaymentStatus = 'Paid'
group by FORMAT(PaidDate, 'yyyy-MM')";

            SQLObjects sqlObj = new SQLObjects();
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(salesQuery, sqlObj.sqlConnection);
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);

            foreach (DataRow row in sqlObj.dataTable.Rows)
            {
                Sales_Monthly model = new Sales_Monthly();
                model.Amount = double.Parse(row["PaidAmount"].ToString());
                model.Month = row["MonthDetails"].ToString();
                sales_Monthly.Add(model);
            }
            return sales_Monthly;
        }
        public List<Sales_Monthly_Locaiton> getDetails_Sales_Monthly_Locaiton()
        {
            List<Sales_Monthly_Locaiton> sales_Monthly_Locaiton = new List<Sales_Monthly_Locaiton>();
            string salesQuery = @"select FORMAT(PaidDate, 'yyyy-MM') as MonthDetails, LocationName, sum(amount) as PaidAmount  from RecurringMaster 
inner join CandidateMaster on CandidateId = RefCandidateId
inner join UserAccountDetails on UserId = RefSalesAssociate 
inner join LocationMaster on RefLocationId = LocationId
where PaidDate is not null and RecurringMaster.PaymentStatus = 'Paid'
group by FORMAT(PaidDate, 'yyyy-MM'), LocationName";

            SQLObjects sqlObj = new SQLObjects();
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(salesQuery, sqlObj.sqlConnection);
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);

            foreach (DataRow row in sqlObj.dataTable.Rows)
            {
                Sales_Monthly_Locaiton model = new Sales_Monthly_Locaiton();
                model.Amount = double.Parse(row["PaidAmount"].ToString());
                model.Month = row["MonthDetails"].ToString();
                model.Location = row["LocationName"].ToString();
                sales_Monthly_Locaiton.Add(model);
            }
            return sales_Monthly_Locaiton;
        }
        public List<Sales_Monthly_Location_Associates> getDetails_Sales_Monthly_Location_Associates()
        {
            List<Sales_Monthly_Location_Associates> sales_Monthly_Location_Associates = new List<Sales_Monthly_Location_Associates>();
            string salesQuery = @"
select FORMAT(PaidDate, 'yyyy-MM') as MonthDetails, LocationName,FullName, sum(amount) as PaidAmount  from RecurringMaster 
inner join CandidateMaster on CandidateId = RefCandidateId
inner join UserAccountDetails on UserId = RefSalesAssociate 
inner join LocationMaster on RefLocationId = LocationId
where PaidDate is not null and RecurringMaster.PaymentStatus = 'Paid'
group by FORMAT(PaidDate, 'yyyy-MM'), LocationName, FullName";

            SQLObjects sqlObj = new SQLObjects();
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(salesQuery, sqlObj.sqlConnection);
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);

            foreach (DataRow row in sqlObj.dataTable.Rows)
            {
                Sales_Monthly_Location_Associates model = new Sales_Monthly_Location_Associates();
                model.Amount = double.Parse(row["PaidAmount"].ToString());
                model.Month = row["MonthDetails"].ToString();
                model.Location = row["LocationName"].ToString();
                model.AssociatesName = row["FullName"].ToString();
                sales_Monthly_Location_Associates.Add(model);
            }
            return sales_Monthly_Location_Associates;
        }
    }
}