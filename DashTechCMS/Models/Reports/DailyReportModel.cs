using DashTechCMS.Models.SQLObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.Reports
{
    public class DailyReportModel
    {
        SQLObjects sqlObj = new SQLObjects();

        public int LocationId { get; set; }
        public int TotalRecruiters { get; set; }
        public int TotalCandidates { get; set; }
        public int TotalSubmissions { get; set; }
        public int TotalInterviews { get; set; }
        public int TotalSubmissionTillNow { get; set; }
        public int TotalInterviewsTillNow { get; set; }
        public int TotalPO { get; set; }
        public int TotalPOTillNow { get; set; }
        public int NewCandidateInMarketing { get; set; }
        public string LocationName { get; set; }

        public DailyReportModel(int LocationId)
        {
            this.LocationId = LocationId;
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();

            string query = @"
select Count(*) as c from UserAccountDetails where RefRoleId = 4 and RefLocationId = " + this.LocationId.ToString();
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(query,sqlObj.sqlConnection);
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            if (sqlObj.dataTable.Rows.Count > 0)
            {
                TotalRecruiters = int.Parse(sqlObj.dataTable.Rows[0]["c"].ToString());
            }
            query = @"
select Count(*) as c from 
CandidateMarketingDetails 
inner join CandidateMaster on CandidateId = RefCandidateId
inner join CandidateAssign on RefMarketingId = MarketingId
inner join TeamDetails on RefTeamId = TeamId
inner join UserAccountDetails on Member = UserId  
where CandidateAssign.IsActive = 1 and CandidateStatus = 'In Marketing' and RefLocationId = " + this.LocationId.ToString();
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(query,sqlObj.sqlConnection);
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            if (sqlObj.dataTable.Rows.Count > 0)
            {

                TotalCandidates = int.Parse(sqlObj.dataTable.Rows[0]["c"].ToString());
            }

            query = string.Format(@"

select count(*) as c from SubmissionDetails
inner join CandidateAssign on RefAssignedId = AssignedId
inner join CandidateMarketingDetails on RefMarketingId = MarketingId
inner join TeamDetails on TeamId = RefTeamId
inner join UserAccountDetails on UserId = Member
where  SubmissionDetails.Date = '{0}' and RefLocationId = {1}  ", DateTime.Now.ToString("yyyy-MM-dd"), this.LocationId.ToString());
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(query,sqlObj.sqlConnection);
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            if (sqlObj.dataTable.Rows.Count > 0)
            {
                TotalSubmissions = int.Parse(sqlObj.dataTable.Rows[0]["c"].ToString());
            }
            query = string.Format(@"
select count(*) as c from SubmissionDetails
inner join CandidateAssign on RefAssignedId = AssignedId
inner join InterviewDetails on RefSumissionId = SubmissionId
inner join CandidateMarketingDetails on RefMarketingId = MarketingId
inner join TeamDetails on TeamId = RefTeamId
inner join UserAccountDetails on UserId = Member
where  DateOfInterview = '{0}' and RefLocationId = {1}", DateTime.Now.ToString("yyyy -MM-dd"), this.LocationId.ToString());
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(query,sqlObj.sqlConnection);
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            if (sqlObj.dataTable.Rows.Count > 0)
            {
                TotalInterviews = int.Parse(sqlObj.dataTable.Rows[0]["c"].ToString());
            }
            DateTime sDt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime eDt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            eDt = eDt.AddMonths(1).AddDays(-1);

            query = string.Format(@"
select count(*) as c from SubmissionDetails
inner join CandidateAssign on RefAssignedId = AssignedId
inner join CandidateMarketingDetails on RefMarketingId = MarketingId
inner join TeamDetails on TeamId = RefTeamId
inner join UserAccountDetails on UserId = Member
where  SubmissionDetails.Date between '{0}' and '{1}' and RefLocationId = {2}", sDt.ToString("yyyy-MM-dd"), eDt.ToString("yyyy-MM-dd"), this.LocationId.ToString());
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(query,sqlObj.sqlConnection);
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            if (sqlObj.dataTable.Rows.Count > 0)
            {
                TotalSubmissionTillNow = int.Parse(sqlObj.dataTable.Rows[0]["c"].ToString());
            }


            query = string.Format(@"
select count(*) as c from SubmissionDetails
inner join CandidateAssign on RefAssignedId = AssignedId
inner join InterviewDetails on RefSumissionId = SubmissionId
inner join CandidateMarketingDetails on RefMarketingId = MarketingId
inner join TeamDetails on TeamId = RefTeamId
inner join UserAccountDetails on UserId = Member
where  DateOfInterview between '{0}' and '{1}' and RefLocationId = {2}", sDt.ToString("yyyy-MM-dd"), eDt.ToString("yyyy-MM-dd"), this.LocationId.ToString());
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(query,sqlObj.sqlConnection);
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            if (sqlObj.dataTable.Rows.Count > 0)
            {
                TotalInterviewsTillNow = int.Parse(sqlObj.dataTable.Rows[0]["c"].ToString());
            }
            query = string.Format(@"
select count(*) as c from PODetails inner join UserAccountDetails on RecruiterName = RocketName
where PODetails.PODate = '{0}' and RefLocationId = {1}", DateTime.Now.ToString("yyyy-MM-dd"), this.LocationId.ToString());
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(query,sqlObj.sqlConnection);
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            if (sqlObj.dataTable.Rows.Count > 0)
            {
                TotalPO = int.Parse(sqlObj.dataTable.Rows[0]["c"].ToString());
            }
            query = string.Format(@"
select count(*) as c from PODetails inner join UserAccountDetails on RecruiterName = RocketName
where PODetails.PODate between '{0}' and '{1}' and RefLocationId = {2}", sDt.ToString("yyyy-MM-dd"), eDt.ToString("yyyy-MM-dd"), this.LocationId.ToString());
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(query,sqlObj.sqlConnection);
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            if (sqlObj.dataTable.Rows.Count > 0)
            {
                TotalPOTillNow = int.Parse(sqlObj.dataTable.Rows[0]["c"].ToString());
            }
            query = string.Format(@"
select Count(*) as c from 
CandidateMarketingDetails 
inner join CandidateMaster on CandidateId = RefCandidateId
inner join CandidateAssign on RefMarketingId = MarketingId
inner join TeamDetails on RefTeamId = TeamId
inner join UserAccountDetails on Member = UserId  
where CandidateAssign.IsActive = 1 and CandidateStatus = 'In Marketing' and CandidateMaster.MarketingStartDate = '{0}' and RefLocationId = {1}", DateTime.Now.ToString("yyyy-MM-dd"), this.LocationId.ToString());
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(query,sqlObj.sqlConnection);
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            if (sqlObj.dataTable.Rows.Count > 0)
            {
                NewCandidateInMarketing = int.Parse(sqlObj.dataTable.Rows[0]["c"].ToString());
            }
            LocationName = (new dashrdku_database()).LocationMasters.Find(LocationId).LocationName;
        }

    }

    public class DailyReportLogic
    {
        public List<DailyReportModel> getData()
        {
            List<DailyReportModel> dailyReports = new List<DailyReportModel>();

            DailyReportModel dailyReportVis = new DailyReportModel(1);
            DailyReportModel dailyReportAhm = new DailyReportModel(2);

            dailyReports.Add(dailyReportVis);
            dailyReports.Add(dailyReportAhm);
            return dailyReports;
        }

    }
}