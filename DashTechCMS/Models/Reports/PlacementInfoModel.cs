using DashTechCMS.Models.SQLObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.Reports
{
    public class PlacementInfoModel
    {
        public int POId { get; set; }
        public DateTime PODate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string EnterBy { get; set; }
        public string MarketingTeamDetails { get; set; }
        public string SalesTeamDetails { get; set; }
        public string LocationName { get; set; }
        public string PODetails { get; set; }

    }
    public class PlacemenInfoLogic
    {
        public List<PlacementInfoModel> GetDetails(DateTime sdt, DateTime edt)
        {
            List<PlacementInfoModel> placementInfoModels = new List<PlacementInfoModel>();
            SQLObjects sqlObj = new SQLObjects();
            sqlObj.sqlConnection = databaseConnectionSetting.getConn();
            //get podate wise
            string poDateWiseQuery = string.Format(@"
select LocationName,CandidateName,PODate,
iif(PODetails.JoiningDate is null, 'No Joining Date',convert(varchar,PODetails.JoiningDate,120)) as JoiningDate 
,EnteryDate,
SalesUser.FullName as SalesAssociateName,
SalesUserLead.FullName as SalesTeamLeadName,
SalesUserManager.FullName as SalesTeamManagerName,
RecruiterName,
TeamLeadName as MarketingTeamLead,
PODetails.AddBy, POId
from PODetails 
left join TeamDetails as SalesTeam on RefSaleId = SalesTeam.Member
left join UserAccountDetails as SalesUser on SalesUser.UserId = SalesTeam.Member
left join UserAccountDetails as SalesUserLead on SalesUserLead.UserId = SalesTeam.TeamLead
left join UserAccountDetails as SalesUserManager on SalesUserManager.UserId = SalesTeam.TeamManager
left join UserAccountDetails as RecuriterUser on RecuriterUser.RocketName = RecruiterName
inner join LocationMaster on LocationId = RecuriterUser.RefLocationId
left join UserAccountDetails as InterviewSupportUser on InterviewSupportUser.RocketName = PODetails.InterviewSupport
left join UserAccountDetails as TrainerUser on TrainerUser.RocketName = PODetails.TrainerName
where PODate between '{0}' and '{1}'", sdt.ToString("yyyy-MM-dd"), edt.ToString("yyyy-MM-dd"));


            sqlObj.sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(poDateWiseQuery, sqlObj.sqlConnection);
            sqlObj.dataTable = new System.Data.DataTable();
            sqlObj.sqlDataAdapter.Fill(sqlObj.dataTable);

            foreach (DataRow item in sqlObj.dataTable.Rows)
            {
                PlacementInfoModel model = new PlacementInfoModel()
                {
                    EnterBy = item["AddBy"].ToString(),
                    EntryDate = DateTime.Parse(item["EnteryDate"].ToString()),
                    LocationName = item["LocationName"].ToString(),
                    PODate = DateTime.Parse(item["PODate"].ToString()),
                    POId = int.Parse(item["POId"].ToString())
                };
                if (!item["JoiningDate"].ToString().Contains("No"))
                {
                    model.JoiningDate = DateTime.Parse(item["JoiningDate"].ToString());
                }
                model.MarketingTeamDetails = string.Format(@"{0}<br>{1}", item["RecruiterName"].ToString(), item["MarketingTeamLead"].ToString());
                model.SalesTeamDetails = string.Format(@"{0}<br>{1}<br>{2}", item["SalesAssociateName"].ToString(), item["SalesTeamLeadName"].ToString(), item["SalesTeamManagerName"].ToString());

                placementInfoModels.Add(model);

            }
            return placementInfoModels;
            //get joining date wise
        }
    }

}