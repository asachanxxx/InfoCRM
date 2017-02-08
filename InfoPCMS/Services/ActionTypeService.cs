using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InfoPCMS.Services
{
    class ActionTypeService
    {
        private String actionType;
        private int duration;
        //private String isDateTimeMandatory;
        private int intFirstReminder;
        private int intescaltion1;
        private int intescaltion2;       

       
        public String ActionType
        {
            get { return actionType; }
            set { actionType = value; }
        }      

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public int IntFirstReminder
        {
            get { return intFirstReminder; }
            set { intFirstReminder = value; }
        }
        public int Intescaltion1
        {
            get { return intescaltion1; }
            set { intescaltion1 = value; }
        }
        public int Intescaltion2
        {
            get { return intescaltion2; }
            set { intescaltion2 = value; }
        }


        public DataTable getAllActionTypeDetails()
        {

            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from ActionType");
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;

            }


        }

        public Boolean addActionType()
        {
            try
            {

                var parameters = new Dictionary<String, String>() { { "ActionType", ActionType }, { "Duration", Duration.ToString() },  { "FirstReminder", intFirstReminder.ToString() }
                , { "Escaltion1", intescaltion1.ToString() }, { "Escaltion2", intescaltion2.ToString() }};
                
                InfoPCMS.db.executeInsertOrUpdate("spInsertActionType", parameters);
                return true;

            }
            catch (Exception ex)
            {

                throw ex;

            }
            return false;

        }

        public void getActionTypeDetailsByID(string ActionTypeId)
        {
            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from ActionType where Id='" + ActionTypeId + "'");
                if (dt.Rows.Count > 0)
                {

                    ActionType = dt.Rows[0]["ActionType"].ToString();
                    Duration = Convert.ToInt32(dt.Rows[0]["Duration"].ToString());
                   // IsDateTimeMandatory = dt.Rows[0]["IsDateTimeMandatory"].ToString();
                    Intescaltion1 = Convert.ToInt32(dt.Rows[0]["Escaltion1"].ToString());
                    Intescaltion2 = Convert.ToInt32(dt.Rows[0]["Escaltion2"].ToString());
                    IntFirstReminder = Convert.ToInt32(dt.Rows[0]["FirstReminder"].ToString());
                    //Escaltion3 = Convert.ToInt32(dt.Rows[0]["Escaltion3"].ToString());
                }

            }
            catch (Exception ex)
            {


            }



        }       

         
    }
}
