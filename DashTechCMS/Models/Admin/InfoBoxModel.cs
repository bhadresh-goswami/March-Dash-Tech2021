using DashTechCMS.Models.SQLObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.Admin
{
    public class InfoBoxModel
    {
        public int TotalRevenue { get; set; }
        public int TodaysRevenue { get; set; }
        public int CurrentMonthRevenue { get; set; }
        public int TotalCandidate { get; set; }
        public int TodaysPlacements { get; set; }
        public int CurrentMonthPlacement { get; set; }


    }

    public class InfoBoxModelLogic
    {
        public InfoBoxModel infoBox;
        public string GetDetails()
        {
            infoBox = new InfoBoxModel();
            try
            {

                SQLObjects sqlObj = new SQLObjects();
                sqlObj.sqlConnection = databaseConnectionSetting.getConn();


                DateTime todayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime lastDate = DateTime.Now;
                lastDate = lastDate.AddMonths(1).AddDays(-1);

                #region Current Total Revenue
                string currentRevenueQuery = @"select sum(Amount) as CurrentAmount from RecurringMaster where PaymentStatus = 'Paid'";
                sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(currentRevenueQuery, sqlObj.sqlConnection);
                sqlObj.dataTable = new System.Data.DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                sqlObj.sqlConnection.Close();

                string currentRevenue = sqlObj.dataTable.Rows[0]["CurrentAmount"].ToString().Split('.')[0];
                infoBox.TotalRevenue = int.Parse(currentRevenue);

                #endregion

                #region todays Revenue Total


                string todaysRevenueQuery = string.Format(@"select sum(Amount) as TodayRevenue from RecurringMaster where PaidDate = '{0}'",todayDate.ToString("yyyy-MM-dd"));
                sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(todaysRevenueQuery, sqlObj.sqlConnection);
                sqlObj.dataTable = new System.Data.DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                sqlObj.sqlConnection.Close();

                string todayRevenue = sqlObj.dataTable.Rows[0]["TodayRevenue"].ToString().Split('.')[0];
                infoBox.TodaysRevenue = int.Parse(todayRevenue);

                #endregion

                #region Current Month Revenue Total

                string queryCurrentMonthRevenue = string.Format(@"select  sum(Amount) as CurrentMonthRevenue from RecurringMaster where FORMAT(PaidDate, 'MMM-yyyy') = '{0}' and PaymentStatus = 'Paid'", todayDate.Date.ToString("MMM-yyyy"));

                sqlObj.sqlDataAdapter = new SqlDataAdapter(queryCurrentMonthRevenue, sqlObj.sqlConnection);
                sqlObj.dataTable = new DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                if (sqlObj.dataTable.Rows.Count == 1)
                {
                    string CurrentMonthRevenue = sqlObj.dataTable.Rows[0]["CurrentMonthRevenue"].ToString().Split('.')[0];
                    infoBox.CurrentMonthRevenue = int.Parse(CurrentMonthRevenue);
                }
                if (sqlObj.sqlConnection.State == ConnectionState.Open)
                {
                    sqlObj.sqlConnection.Close();
                }
                #endregion

                #region Today's Candidate Count

                string totalCandidateQuery = string.Format(@"select count(*) as todayCount from CandidateMaster where CandidateStatus in ('Resume Preparation',
'Resume Verification','In Marketing','In Training','Not Responding To Resume Team','RUC Done','RUC Pending','Sales')", DateTime.Now.Date.ToString("yyyy-MM-dd"));
                sqlObj.sqlDataAdapter = new SqlDataAdapter(totalCandidateQuery, sqlObj.sqlConnection);
                sqlObj.dataTable = new DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                sqlObj.sqlConnection.Close();

                if (sqlObj.dataTable.Rows.Count == 1)
                {
                    string todaysCount = sqlObj.dataTable.Rows[0]["todayCount"].ToString().Split('.')[0];
                    infoBox.TotalCandidate = int.Parse(todaysCount);
                }
                #endregion


                #region today's placement

                string todaysPlacementsQuery = string.Format(@"select count(*) as todayPlacement from PODetails where PODate = '{0}'", todayDate.ToString("yyyy-MM-dd"));
                sqlObj.sqlDataAdapter = new SqlDataAdapter(todaysPlacementsQuery, sqlObj.sqlConnection);
                sqlObj.dataTable = new DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                sqlObj.sqlConnection.Close();

                if (sqlObj.dataTable.Rows.Count == 1)
                {
                    string todaysPlacement = sqlObj.dataTable.Rows[0]["todayPlacement"].ToString().Split('.')[0];
                    infoBox.TodaysPlacements = int.Parse(todaysPlacement);
                }
                #endregion

                #region current Month Placement

                string currentMonthPlacementQuery = string.Format(@"select count(*) as currentMonthPlacements from PODetails where PODate between '{0}' and '{1}'", todayDate.ToString("yyyy-MM-dd"), lastDate.ToString("yyyy-MM-dd"));
                sqlObj.sqlDataAdapter = new SqlDataAdapter(currentMonthPlacementQuery, sqlObj.sqlConnection);
                sqlObj.dataTable = new DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                sqlObj.sqlConnection.Close();

                if (sqlObj.dataTable.Rows.Count == 1)
                {
                    string currentmMonthPlacement = sqlObj.dataTable.Rows[0]["currentMonthPlacements"].ToString().Split('.')[0];
                    infoBox.CurrentMonthPlacement = int.Parse(currentmMonthPlacement);
                }
                #endregion

                return null;
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }

    }
}