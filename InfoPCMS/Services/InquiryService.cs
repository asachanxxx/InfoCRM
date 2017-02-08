using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InfoPCMS.Services
{
    class InquiryService
    {

        private String inquirynumber;
        private String inquirydate;
        private String customerid;
        private String problemnature;
        private String problemsource;
        private String inquirytaken;
        private String inspectiondone;
        private String jobAssignedTo;

       
        private String siteresponsible;
        private String inspectiondetails;
        private String followupdetails;
        private String inquirytime;
        private String howknow;
        private String iffailed;
        private String recommendation;
        private String inquirytype;
        private String pastinquiry;
        private String done;
        private String active;
        private String location;
        private String priority;
        private String executiveResponsible;
        private String requestForward;
        private String approvedForward;
        private String approvedForwardBy;
        private String strCallerName;
        private String strCallerTelNo;
        private String strCusName;
        private String strJobEntryDateforEmail;

        public String StrJobEntryDateforEmail
        {
            get { return strJobEntryDateforEmail; }
            set { strJobEntryDateforEmail = value; }
        }
        private String strJobEntryTimeforEmail;

        public String StrJobEntryTimeforEmail
        {
            get { return strJobEntryTimeforEmail; }
            set { strJobEntryTimeforEmail = value; }
        }

        public String StrCusName
        {
            get { return strCusName; }
            set { strCusName = value; }
        }

        public String StrCallerTelNo
        {
            get { return strCallerTelNo; }
            set { strCallerTelNo = value; }
        }

        public String StrCallerName
        {
            get { return strCallerName; }
            set { strCallerName = value; }
        }


        public String RequestForward
        {
            get { return requestForward; }
            set { requestForward = value; }
        }


        public String ApprovedForward
        {
            get { return approvedForward; }
            set { approvedForward = value; }
        }


        public String ApprovedForwardBy
        {
            get { return approvedForwardBy; }
            set { approvedForwardBy = value; }
        }

        private String jobEntryDate;
        private String jobEntryTime;

        public String JobEntryDate
        {
            get { return jobEntryDate; }
            set { jobEntryDate = value; }
        }


        public String JobEntryTime
        {
            get { return jobEntryTime; }
            set { jobEntryTime = value; }
        }

        public String Location
        {
            get { return location; }
            set { location = value; }
        }

        public String Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public String ExecutiveResponsible
        {
            get { return executiveResponsible; }
            set { executiveResponsible = value; }
        }


        public String Done
        {
            get { return done; }
            set { done = value; }
        }




        public String PastInquiry
        {
            get { return pastinquiry; }
            set { pastinquiry = value; }
        }


        public String InquiryType
        {
            get { return inquirytype; }
            set { inquirytype = value; }
        }


        public String Recommendation
        {
            get { return recommendation; }
            set { recommendation = value; }
        }


        public String IfFailed
        {
            get { return iffailed; }
            set { iffailed = value; }
        }


        public String HowKnow
        {
            get { return howknow; }
            set { howknow = value; }
        }


        public String InquiryTime
        {
            get { return inquirytime; }
            set { inquirytime = value; }
        }





        public String FollowupDetails
        {
            get { return followupdetails; }
            set { followupdetails = value; }
        }


        public String InspectionDetails
        {
            get { return inspectiondetails; }
            set { inspectiondetails = value; }
        }

        public String JobAssignedTo
        {
            get { return jobAssignedTo; }
            set { jobAssignedTo = value; }
        }

        public String SiteResponsible
        {
            get { return siteresponsible; }
            set { siteresponsible = value; }
        }


        public String InspectionDone
        {
            get { return inspectiondone; }
            set { inspectiondone = value; }
        }


        public String InquiryTaken
        {
            get { return inquirytaken; }
            set { inquirytaken = value; }
        }


        public String ProblemSource
        {
            get { return problemsource; }
            set { problemsource = value; }
        }


        public String ProblemNature
        {
            get { return problemnature; }
            set { problemnature = value; }
        }


        public String CustomerID
        {
            get { return customerid; }
            set { customerid = value; }
        }


        public String InquiryDate
        {
            get { return inquirydate; }
            set { inquirydate = value; }
        }


        public String InquiryNumber
        {
            get { return inquirynumber; }
            set { inquirynumber = value; }
        }




        public Boolean addInquiry()
        {
            try
            {
                active = "Active";
                var parameters = new Dictionary<String, String>() { { "InquiryNumber", InquiryNumber }, 
                { "Date", InquiryDate }, { "Time", InquiryTime }, { "JobEntryDate", JobEntryDate }, 
                { "JobEntryTime", JobEntryTime }, { "CustomerId", CustomerID }, { "ProblemNature", ProblemNature },
                { "ProblemSource", ProblemSource }, { "InquiryTaken", InquiryTaken }, { "InspectionDone", InspectionDone },{ "JobAssignedTo", JobAssignedTo },
                { "SiteResponsible", SiteResponsible }, { "InspectionDetails", InspectionDetails },
                { "FollowupDetails", FollowupDetails }, { "Recommendation", Recommendation }, { "HowKnow", HowKnow }, 
                { "IfFailed", IfFailed }, { "CallerName", strCallerName }, { "CallerPhone", strCallerTelNo }, { "Done", Done }, 
                { "active", active }, { "Location", location }, { "Priority", priority }, { "ExecutiveResponsible", executiveResponsible } };

                InfoPCMS.db.executeInsertOrUpdate("spInsertInquiry", parameters);
                                // SendEmail();
                //InfoPCMS.db.executeUpdateQuery("insert into Inquiry (InquiryNumber,Date,Time,CustomerId,ProblemNature,ProblemSource,InquiryTaken,InspectionDone,SiteResponsible,InspectionDetails,FollowupDetails,Recommendation,HowKnow,IfFailed) values ('" + InquiryNumber + "',CAST('" + InquiryDate + "' AS date),CAST('" + InquiryTime + "' AS time),'" + CustomerID + "','" + ProblemNature + "','" + ProblemSource + "','" + InquiryTaken + "','" + InspectionDone + "','" + SiteResponsible + "','" + InspectionDetails + "','" + FollowupDetails + "','" + Recommendation + "','" + HowKnow + "','" + IfFailed + "')");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }


        public Boolean updateInquiry()
        {
            try
            {
                var parameters = new Dictionary<String, String>() { { "InquiryNumber", InquiryNumber }, { "Date", InquiryDate }, { "Time", InquiryTime }, { "JobEntryDate", JobEntryDate }, { "JobEntryTime", JobEntryTime }, { "CustomerId", CustomerID }, { "ProblemNature", ProblemNature }, { "ProblemSource", ProblemSource }, { "InquiryTaken", InquiryTaken }, { "InspectionDone", InspectionDone }, { "JobAssignedTo", JobAssignedTo }, { "SiteResponsible", SiteResponsible }, { "InspectionDetails", InspectionDetails }, { "FollowupDetails", FollowupDetails }, { "Recommendation", Recommendation }, { "HowKnow", HowKnow }, { "CallerName", strCallerName }, { "CallerPhone", strCallerTelNo }, { "IfFailed", IfFailed }, { "Done", Done } };
                InfoPCMS.db.executeInsertOrUpdate("spUpdateInquiry", parameters);
                // InfoPCMS.db.executeUpdateQuery("update Inquiry set Date = CAST('"+InquiryDate+"' AS date),Time = CAST('"+InquiryTime+"' AS time),CustomerId='" + CustomerID + "',ProblemNature='" + ProblemNature + "',ProblemSource='" + ProblemSource + "',InquiryTaken='" + InquiryTaken + "',InspectionDone='" + InspectionDone + "',SiteResponsible='" + SiteResponsible + "',InspectionDetails='" + InspectionDetails + "',FollowupDetails='" + FollowupDetails + "',HowKnow='" + HowKnow + "',IfFailed='" + IfFailed + "',Recommendation='" + Recommendation + "' where InquiryNumber = '" + InquiryNumber + "'");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public string GetEmail(string strEmpId)
        {
            string strEmail = "";

            DataTable dt;
            dt = InfoPCMS.db.executeSelectQuery("SELECT Email FROM Employee WHERE Id = '" + strEmpId + "'");
            if (dt.Rows.Count > 0)
            {

                strEmail = dt.Rows[0]["Email"].ToString();
            }

            return strEmail;

        }

        public void SendEmail()
        {
            string strFromEmail = "";
            string strToEmail = "";
            List<string> strToEmails = new List<string>();
            List<string> strCCEmails = new List<string>();

            strFromEmail = GetEmail(inquirytaken);
            strToEmail = GetEmail(siteresponsible);
            StringBuilder strBuliderEmailSubject = new StringBuilder();
            strBuliderEmailSubject.Append("NEW Inquiry – Customer : " + StrCusName + " " + " - Inquiry No : " + InquiryNumber + " ");// / " Inquiry No: " + InquiryNumber + " ");

            StringBuilder strBuliderEmailBody = new StringBuilder();
            strBuliderEmailBody.Append("<head>");
            strBuliderEmailBody.Append("<title>");
            strBuliderEmailBody.Append(Guid.NewGuid().ToString());
            strBuliderEmailBody.Append("</title>");
            strBuliderEmailBody.Append("</head>");
            strBuliderEmailBody.Append("<body>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Dear  " + GetEmpName(JobAssignedTo.Trim()) + " ,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("NEW Inquiry <b><u><FONT COLOR=DodgerBlue>Entered</FONT></u></b>, pending for your Acceptance,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry No : </b>" + InquiryNumber + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Type : </b>" + ProblemNature + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Customer : </b>" + StrCusName + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Location : </b>" +GetSubLocationName(Location) + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Caller Name : </b>" + strCallerName + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Caller Phone : </b>" + StrCallerTelNo + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry Details : </b>" + FollowupDetails + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Priority : </b>" + Priority + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Job Entry Date & Time : </b>" + StrJobEntryDateforEmail + "&nbsp;&nbsp;" + StrJobEntryTimeforEmail + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Regards,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("" + GetEmpName(inquirytaken.Trim()) + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("<FONT COLOR=darkred>Auto generated email from CRM system</FONT>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("</body>");
            strBuliderEmailBody.Append("</html>");


            //EmailServices Email = new EmailServices();

            //Email.SendEmails(strFromEmail.Trim(), strToEmail.Trim(), strBuliderEmailSubject.ToString(), strBuliderEmailBody.ToString());

            //// Udara
            Email_Manager em = new Email_Manager();
            String Subject = "NEW Inquiry – Customer : " + StrCusName + " " + " - Inquiry No : " + InquiryNumber + " ";
            Boolean result = em.SendEmail(strToEmail.Trim(), Subject, strBuliderEmailBody.ToString());
            if (result)
            {
                // XtraMessageBox.Show("Email Send Successfully");
            }
          
        }

        public Boolean inactiveInquiry(String activeoff)
        {

            try
            {
                var parameters = new Dictionary<String, String>() { { "InquiryNumber", InquiryNumber }, { "active", activeoff } };
                InfoPCMS.db.executeInsertOrUpdate("spinactiveInquiry", parameters);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;


        }

        public InquiryService searchInquiry(String inquirynumber)
        {

            try
            {

                DataTable dt;
                dt = InfoPCMS.db.executeSelectQuery("select * from Inquiry where InquiryNumber = '" + inquirynumber + "'");

                if (dt.Rows.Count > 0)
                {

                    InquiryNumber = dt.Rows[0]["InquiryNumber"].ToString();
                    InquiryDate = dt.Rows[0]["Date"].ToString();
                    CustomerID = dt.Rows[0]["CustomerId"].ToString();
                    ProblemNature = dt.Rows[0]["ProblemNature"].ToString();
                    ProblemSource = dt.Rows[0]["ProblemSource"].ToString().Trim();
                    InquiryTaken = dt.Rows[0]["InquiryTaken"].ToString();
                    InspectionDone = dt.Rows[0]["InspectionDone"].ToString();
                    JobAssignedTo = dt.Rows[0]["JobAssignedTo"].ToString();
                    SiteResponsible = dt.Rows[0]["SiteResponsible"].ToString();
                    InspectionDetails = dt.Rows[0]["InspectionDetails"].ToString().Trim();
                    FollowupDetails = dt.Rows[0]["FollowupDetails"].ToString().Trim();
                    InquiryTime = dt.Rows[0]["Time"].ToString().Trim();
                    HowKnow = dt.Rows[0]["HowKnow"].ToString();
                    IfFailed = dt.Rows[0]["IfFailed"].ToString().Trim();
                    Recommendation = dt.Rows[0]["Recommendation"].ToString().Trim();
                    strCallerName = dt.Rows[0]["CallerName"].ToString().Trim();
                    strCallerTelNo = dt.Rows[0]["CallerPhone"].ToString().Trim();
                    Done = dt.Rows[0]["Done"].ToString().Trim();
                    location = dt.Rows[0]["Location"].ToString().Trim();
                    priority = dt.Rows[0]["Priority"].ToString().Trim();
                    executiveResponsible = dt.Rows[0]["ExecutiveResponsible"].ToString().Trim();
                    requestForward = dt.Rows[0]["RequestForward"].ToString().Trim();
                    approvedForward = dt.Rows[0]["ApprovedForward"].ToString().Trim();
                    approvedForwardBy = dt.Rows[0]["ApprovedForwardBy"].ToString().Trim();

                    JobEntryDate = dt.Rows[0]["JobEntryDate"].ToString();
                    JobEntryTime = dt.Rows[0]["JobEntryTime"].ToString();

                    StrCusName = this.getCusNameByCus(dt.Rows[0]["CustomerId"].ToString().Trim());

                }

                return this;

            }
            catch (Exception ex)
            {


                throw ex;
            }

            return null;

        }

        public String IsInquryAccepted (String inqnum)
        {

            try
            {
                String InqStatus = "";
                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Inquiry where InquiryNumber = '" + inqnum + "'");

                if (dt.Rows.Count > 0)
                {
                    InqStatus = dt.Rows[0]["Done"].ToString().Trim();
                }
                return InqStatus;
            }
            catch (Exception ex)
            {

                throw ex;

            }


        }

        public void getInquiryDetailsByInqNo()
        {

            try
            {

                DataTable dt;
                dt = InfoPCMS.db.executeSelectQuery("select * from Inquiry where InquiryNumber = '" + InquiryNumber + "'");
                if (dt.Rows.Count > 0)
                {

                    InquiryDate = dt.Rows[0]["Date"].ToString();
                    CustomerID = dt.Rows[0]["CustomerId"].ToString();
                    ProblemNature = dt.Rows[0]["ProblemNature"].ToString();
                    ProblemSource = dt.Rows[0]["ProblemSource"].ToString().Trim();
                    InquiryTaken = dt.Rows[0]["InquiryTaken"].ToString();
                    InspectionDone = dt.Rows[0]["InspectionDone"].ToString();
                    SiteResponsible = dt.Rows[0]["SiteResponsible"].ToString();
                    InspectionDetails = dt.Rows[0]["InspectionDetails"].ToString().Trim();
                    FollowupDetails = dt.Rows[0]["FollowupDetails"].ToString().Trim();
                    InquiryTime = dt.Rows[0]["Time"].ToString().Trim();
                    HowKnow = dt.Rows[0]["HowKnow"].ToString();
                    IfFailed = dt.Rows[0]["IfFailed"].ToString().Trim();
                    Recommendation = dt.Rows[0]["Recommendation"].ToString().Trim();
                    InquiryType = dt.Rows[0]["InquiryType"].ToString().Trim();
                    PastInquiry = dt.Rows[0]["PastInquiry"].ToString().Trim();
                    Done = dt.Rows[0]["Done"].ToString().Trim();
                    JobEntryDate = dt.Rows[0]["JobEntryDate"].ToString();
                    JobEntryTime = dt.Rows[0]["JobEntryTime"].ToString();
                }

            }
            catch (Exception ex)
            {




            }




        }

        public void getSiteAddByCus(string passcustomer)
        {

            try
            {

                DataTable dt;
                dt = InfoPCMS.db.executeSelectQuery("select * from Customer where CustomerName = '" + passcustomer + "'");



                if (dt.Rows.Count > 0)
                {


                    ProblemSource = dt.Rows[0]["Address"].ToString().Trim();



                }

            }
            catch (Exception ex)
            {




            }




        }

        public string getCusNameByCus(string strCusId)
        {
            string strCusName = "";
            try
            {
                DataTable dt;
                dt = InfoPCMS.db.executeSelectQuery("select * from Customer where Id = '" + strCusId + "'");

                if (dt.Rows.Count > 0)
                {
                    strCusName = dt.Rows[0]["CustomerName"].ToString().Trim();

                }

            }
            catch (Exception ex)
            {

            }
            return strCusName;

        }

        public string GetEmpName(string strEmpId)
        {
            string strEmpName = "";
            DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Id,EmployeeName from Employee  WHERE Id='" + strEmpId.Trim() + "'");


            if (dtEmployee.Rows.Count > 0)
            {

                strEmpName = dtEmployee.Rows[0]["EmployeeName"].ToString();
            }

            return strEmpName;
        }

        private string GetSubLocationName(string strSubLocId)
        {
            string strSubLocName= "";
            DataTable dtSubLocations= new DatabaseService().executeSelectQuery("select Id,SubLocation from CustomerLocation  WHERE Id='" + strSubLocId.Trim() + "'");


            if (dtSubLocations.Rows.Count > 0)
            {

                strSubLocName = dtSubLocations.Rows[0]["SubLocation"].ToString();
            }

            return strSubLocName;
        }
    }
}
