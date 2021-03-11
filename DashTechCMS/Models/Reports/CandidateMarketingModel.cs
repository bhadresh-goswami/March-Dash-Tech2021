using DashTechCMS.Models.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.Reports
{
    public class CandidateMarketingModel
    {
        public int CandidateId { get; set; }
        public int MarketingId { get; set; }
        public string CandidateName { get; set; }
        #region No Need

        //public int RecruiterId { get; set; }
        //public int MarketingTeamLeadId { get; set; }
        //public int MarketingTeamManagerId { get; set; }
        //public int SalesPersonId { get; set; }

        #endregion
        public string RecruiterName { get; set; }
        public string MarketingTeamLeadName { get; set; }
        public string MarketingTeamManagerName { get; set; }
        public DateTime MarketingStartDate { get; set; }
        public DateTime? PODate { get; set; }
        public int PODetailsId { get; set; }
        public int TotalDaysResumeProcessDone { get; set; }
        public int TotalDaysPOGet { get; set; }
        public int TotalSubmissions { get; set; }
        public int TotalInterviews { get; set; }
        public int TotalInterviewSupports { get; set; }
        public double KPIAccuracy { get; set; }
    }
    public class CandidateMarketingLogic
    {
        public List<CandidateMarketingModel> getData(DateTime stDate, DateTime endDate)
        {
            List<CandidateMarketingModel> candidates = new List<CandidateMarketingModel>();
            SQLObjects sqlObj = new SQLObjects();
            //set up sql object 
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();

            //getting the Candidate Details
            #region Get Candidate Details

            string candidateQuery = string.Format(@"
select 
CandidateId,
MarketingId,CandidateMaster.CandidateName,
Recruiter.FullName as RecruiterName,
MarketingTeamLead.FullName as TeamLeadName,
MarketingTeamManager.FullName as TeamManagerName,
CandidateMaster.MarketingStartDate,
iif(PODetails.PODate is NULL,'No PO',convert(varchar,PODetails.PODate,23)) as PODate,
iif(PODetails.POId is NULL,'No PO',convert(varchar,PODetails.POId,1)) as POId
from CandidateMaster 
inner join CandidateMarketingDetails on CandidateId = RefCandidateId
inner join CandidateAssign on refMarketingId = MarketingId
inner join TeamDetails on TeamId = RefTeamId
inner join UserAccountDetails as Recruiter on TeamDetails.Member = Recruiter.UserId
inner join UserAccountDetails as MarketingTeamLead on TeamDetails.TeamLead = MarketingTeamLead.UserId
inner join UserAccountDetails as MarketingTeamManager on TeamDetails.TeamManager = MarketingTeamManager.UserId
left join PODetails on PODetails.CandidateName = CandidateMaster.CandidateName
where CandidateAssign.IsActive = 1 and CandidateMaster.MarketingStartDate between '{0}' and '{1}'
", stDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));

            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(candidateQuery, sqlObj.sqlConnection);
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            sqlObj.sqlConnection.Close();

            foreach (DataRow row in sqlObj.dataTable.Rows)
            {
                CandidateMarketingModel candidate = new CandidateMarketingModel();
                candidate.CandidateId = int.Parse(row["CandidateId"].ToString());
                candidate.MarketingId = int.Parse(row["MarketingId"].ToString());
                candidate.CandidateName = row["CandidateName"].ToString();
                candidate.KPIAccuracy = 0.0;
                candidate.MarketingStartDate = DateTime.Parse(row["MarketingStartDate"].ToString());
                candidate.MarketingTeamLeadName = row["TeamLeadName"].ToString();
                candidate.MarketingTeamManagerName = row["TeamManagerName"].ToString();
                if (row["PODate"].ToString() != "No PO")
                {
                    candidate.PODetailsId = int.Parse(row["POId"].ToString());
                    candidate.PODate = DateTime.Parse(row["PODate"].ToString());
                    DateTime podate = (DateTime)candidate.PODate;
                    candidate.TotalDaysPOGet = podate.Subtract(candidate.MarketingStartDate).Days;
                }
                candidate.RecruiterName = row["RecruiterName"].ToString();

                candidate.TotalDaysResumeProcessDone = GetRUCCount(candidate.CandidateId);
                candidate.TotalInterviews = GetSubmissions(candidate.CandidateId); 
                candidate.TotalInterviewSupports = GetInterviewSupports(candidate.CandidateId);
                candidate.TotalSubmissions = GetSubmissions(candidate.CandidateId);

                candidates.Add(candidate);
            }

            #endregion
            return candidates;
        }
        private int GetRUCCount(int cid)
        {
            string countGetQuery = @"select datediff(day,Date,TaskDate) as dayscount from CandidateTechnicalExpertDetails
inner join CandidateMaster on CandidateId = RefAssignedCandidateId
inner join TaskManageMaster on RefCTId = CTId 
where RefTaskCatId = 2 and TaskStatus = 'Completed' and TotalMin > 10 and CandidateId =" + cid.ToString();

            SQLObjects sqlObj = new SQLObjects();
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(countGetQuery, sqlObj.sqlConnection);
            sqlObj.dataTable = new DataTable();

            if (sqlObj.dataTable.Rows.Count > 0)
            {
                return int.Parse(sqlObj.dataTable.Rows[0]["dayscount"].ToString());
            }

            return 0;
        }
        private int GetSubmissions(int cid)
        {
            /**/
            string countGetQuery = @"
select count(*) as dayscount from SubmissionDetails
inner join CandidateAssign on RefAssignedId = AssignedId
inner join CandidateMarketingDetails on RefMarketingId = MarketingId
where refcandidateid = " + cid.ToString();

            SQLObjects sqlObj = new SQLObjects();
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(countGetQuery, sqlObj.sqlConnection);
            sqlObj.dataTable = new DataTable();

            if (sqlObj.dataTable.Rows.Count > 0)
            {
                return int.Parse(sqlObj.dataTable.Rows[0]["dayscount"].ToString());
            }

            return 0;
        }
        private int GetInterviews(int cid)
        {
            /**/
            string countGetQuery = @"
select count(*) as dayscount from SubmissionDetails
inner join CandidateAssign on RefAssignedId = AssignedId
inner join CandidateMarketingDetails on RefMarketingId = MarketingId
inner join InterviewDetails on RefSumissionId = SubmissionId
where refcandidateid = " + cid.ToString();

            SQLObjects sqlObj = new SQLObjects();
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(countGetQuery, sqlObj.sqlConnection);
            sqlObj.dataTable = new DataTable();

            if (sqlObj.dataTable.Rows.Count > 0)
            {
                return int.Parse(sqlObj.dataTable.Rows[0]["dayscount"].ToString());
            }

            return 0;
        }
        private int GetInterviewSupports(int cid)
        {
            /**/
            string countGetQuery = @"
select count(*) as dayscount from CandidateTechnicalExpertDetails
inner join CandidateMaster on CandidateId = RefAssignedCandidateId
inner join TaskManageMaster on RefCTId = CTId 
where RefTaskCatId in (3,6) and TaskStatus = 'Completed' and CandidateId = " + cid.ToString();

            SQLObjects sqlObj = new SQLObjects();
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(countGetQuery, sqlObj.sqlConnection);
            sqlObj.dataTable = new DataTable();

            if (sqlObj.dataTable.Rows.Count > 0)
            {
                return int.Parse(sqlObj.dataTable.Rows[0]["dayscount"].ToString());
            }

            return 0;
        }
    }
}