using DashTechCMS.Models.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.Reports
{
    public class CandidateTimeLineModel
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string Technology { get; set; }
        public string SalesAssociates { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime ResumeCallDate { get; set; }
        public DateTime ResumePreparedDate { get; set; }
        public DateTime ResumeVerifiedDate { get; set; }
        public DateTime ResumeUnderstandingDate { get; set; }
        public DateTime TrainingEndDate { get; set; }
        public string TrainerName { get; set; }
        public DateTime MarketingStartDate { get; set; }
        public string ResumeUnderstandingCompletedBy
        { get; set; }


    }
    public class CandidateTimeLineModelLogic
    {
        CandidateTimeLineModel timeLineModel;

        public string GetData(int id)
        {
            timeLineModel = new CandidateTimeLineModel();

            timeLineModel.MarketingStartDate = new DateTime(1, 1, 1);
            timeLineModel.RegistrationDate = new DateTime(1, 1, 1);
            timeLineModel.ResumeCallDate = new DateTime(1, 1, 1);
            timeLineModel.ResumePreparedDate = new DateTime(1, 1, 1);
            timeLineModel.ResumeVerifiedDate = new DateTime(1, 1, 1);
            timeLineModel.ResumeUnderstandingDate = new DateTime(1, 1, 1);
            timeLineModel.MarketingStartDate = new DateTime(1, 1, 1);
            try
            {
                SQLObjects sqlObj = new SQLObjects();

                #region Registration Date
                string regDateQuery = string.Format(@"select CandidateName, CandidateId,date,FullName,TechTitle,CandidateStatus from CandidateMaster 
inner join UserAccountDetails on UserId = RefSalesAssociate 
inner join TechnologyMaster on TechnologyId = TechId where CandidateId = {0}", id);

                sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(regDateQuery, sqlObj.sqlConnection);
                sqlObj.dataTable = new System.Data.DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);

                timeLineModel.CandidateName = sqlObj.dataTable.Rows[0]["CandidateName"].ToString();
                timeLineModel.Technology = sqlObj.dataTable.Rows[0]["TechTitle"].ToString();
                timeLineModel.SalesAssociates = sqlObj.dataTable.Rows[0]["FullName"].ToString();
                timeLineModel.CurrentStatus = sqlObj.dataTable.Rows[0]["CandidateStatus"].ToString();
                timeLineModel.RegistrationDate = DateTime.Parse(sqlObj.dataTable.Rows[0]["Date"].ToString());
                #endregion


                #region Resume Call Done
                string resumeCallDoneDateQuery = string.Format(@"select * from CandidateTimeLine where RefCandidateId = {0} and Status = 'Resume Call Done'", id);

                sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(resumeCallDoneDateQuery, sqlObj.sqlConnection);
                sqlObj.dataTable = new System.Data.DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                if (sqlObj.dataTable.Rows.Count == 1)
                {
                    timeLineModel.ResumeCallDate = DateTime.Parse(sqlObj.dataTable.Rows[0]["Date"].ToString());
                }
                #endregion


                #region Resume Prepared Date
                string resumePreparedDateQuery = string.Format(@"select * from CandidateTimeLine where RefCandidateId = {0}
                and Status = 'Resume Preparation'", id);

                sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(resumePreparedDateQuery, sqlObj.sqlConnection);
                sqlObj.dataTable = new System.Data.DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                if (sqlObj.dataTable.Rows.Count == 1)
                {
                    timeLineModel.ResumePreparedDate = DateTime.Parse(sqlObj.dataTable.Rows[0]["Date"].ToString());
                }
                #endregion


                #region Resume Verified Date
                string resumeVerifiedDateQuery = string.Format(@"
select Date from CandidateMaster where CandidateId = {0}", id);
                #endregion


                #region Resume Understanding Date
                string resumeUnderstandingDateQuery = string.Format(@"select TaskDate,FullName from CandidateTechnicalExpertDetails 
inner join TaskManageMaster on RefCTId = CTId
inner join UserAccountDetails on UserId = RefAssignedExpertId
 where RefAssignedCandidateId = {0}
and RefTaskCatId = 2 and TaskStatus = 'Completed'", id);

                sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(resumeUnderstandingDateQuery, sqlObj.sqlConnection);
                sqlObj.dataTable = new System.Data.DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                if (sqlObj.dataTable.Rows.Count > 0)
                {
                    timeLineModel.ResumeUnderstandingDate = DateTime.Parse(sqlObj.dataTable.Rows[0]["TaskDate"].ToString());
                    timeLineModel.ResumeUnderstandingCompletedBy = sqlObj.dataTable.Rows[0]["FullName"].ToString();
                }
                #endregion


                #region Marketing Start Date
                string marketingStartDateQuery = string.Format(@"
select Date from CandidateMaster where CandidateId = {0}", id);
                #endregion


                #region Training Date
                string trainingEndDateQuery = string.Format(@"select BatchEndDate,BatchStartDate,FullName from CandidateTechnicalExpertDetails 
inner join UserAccountDetails on UserId = RefAssignedExpertId
inner join CandidateBatchDetails on RefBatchId = BatchId where RefAssignedCandidateId = {0}", id);
                sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(trainingEndDateQuery, sqlObj.sqlConnection);
                sqlObj.dataTable = new System.Data.DataTable();
                sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
                if (sqlObj.dataTable.Rows.Count > 0)
                {
                    timeLineModel.TrainingEndDate = DateTime.Parse(sqlObj.dataTable.Rows[0]["BatchEndDate"].ToString());
                    timeLineModel.TrainerName = sqlObj.dataTable.Rows[0]["FullName"].ToString();
                }
                #endregion

                return "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<CandidateTimeLineModel> GetList(DateTime sdt, DateTime edt)
        {
            List<CandidateTimeLineModel> candidateTimeLines = new List<CandidateTimeLineModel>();

            DateTime todayDate = sdt;
            DateTime lastDate = edt;// DateTime.Now;
            //lastDate = lastDate.AddMonths(1).AddDays(-1);

            string query = string.Format(@"select CandidateId from CandidateMaster where Date between '{0}' and '{1}'", todayDate.ToString("yyyy-MM-dd"), lastDate.ToString("yyyy-MM-dd"));
            SQLObjects sqlObj = new SQLObjects();
            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(query, sqlObj.sqlConnection);
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);
            sqlObj.sqlConnection.Close();
            foreach (DataRow item in sqlObj.dataTable.Rows)
            {
                this.GetData(int.Parse(item["CandidateId"].ToString()));
                candidateTimeLines.Add(timeLineModel);
            }
            return candidateTimeLines;
        }
    }
}