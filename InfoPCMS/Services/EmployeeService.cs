using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InfoPCMS.Services
{
    class EmployeeService
    {
        private String employeename;
        private String contactno;
        private String address;
        private String email;
        private String department;
        private String jobarea;
        private String category;

        public String Category
        {
            get { return category; }
            set { category = value; }
        }


        public String JobArea
        {
            get { return jobarea; }
            set { jobarea = value; }
        }


        public String Department
        {
            get { return department; }
            set { department = value; }
        }


        public String Email
        {
            get { return email; }
            set { email = value; }
        }



        public String Address
        {
            get { return address; }
            set { address = value; }
        }


        public String ContactNo
        {
            get { return contactno; }
            set { contactno = value; }
        }


        public String EmployeeName
        {
            get { return employeename; }
            set { employeename = value; }
        }


        public Boolean addEmployee() {

            try {

                var parameters = new Dictionary<String, String>() { { "EmployeeName", EmployeeName }, { "ContactNumber", ContactNo }, { "Address", Address }, { "Email", Email }, { "Department", Department }, { "JobArea", JobArea }, { "Category", Category } };
                InfoPCMS.db.executeInsertOrUpdate("spInsertEmployee", parameters);
                return true;
            
            }
            catch (Exception ex) {

                throw ex;
            }
            return false;
        
        }

        public Boolean addUser()
        {

            try
            {

                var parameters = new Dictionary<String, String>() { { "Username", EmployeeName }, { "Password", ContactNo }, { "Displayname", Address }, { "Category", Category } };
                InfoPCMS.db.executeInsertOrUpdate("spInsertUser", parameters);
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;

        }


        public DataTable getAllEmployeeDetails() {

            try {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Employee where Status=1");
                return dt;

            }
            catch (Exception ex) {

                throw ex;
            
            }
        
        }


        public DataTable getAllTechnicians() {

            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Employee e where e.Category = 'TECHNICIAN' or e.Category = 'TEAM LEADER' or e.Category = 'TECHNICIAN-TRAINEE'");
                return dt;

            }
            catch (Exception ex)
            {

                throw ex;

            }
        
        }

        public void getEmployeeDetailsByName() {

            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Employee where EmployeeName = '"+EmployeeName+"'");
                if(dt.Rows.Count>0){

                    ContactNo = dt.Rows[0]["ContactNumber"].ToString();
                    Address = dt.Rows[0]["Address"].ToString();
                    Email = dt.Rows[0]["Email"].ToString();
                    Department = dt.Rows[0]["Department"].ToString();
                    JobArea = dt.Rows[0]["JobArea"].ToString();
                    Category = dt.Rows[0]["Category"].ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;

            }
        
        
        }

        public Boolean updateEmployee() {

            try {

                var parameters = new Dictionary<String, String>() { { "EmployeeName", EmployeeName }, { "ContactNumber", ContactNo }, { "Address", Address }, { "Email", Email }, { "Department", Department }, { "JobArea", JobArea }, { "Category", Category } };
                InfoPCMS.db.executeInsertOrUpdate("spUpdateEmployee", parameters);
                return true;   

            
            }
            catch (Exception ex) {

                throw ex;
            
            }
            return false;
        
        }

        public Boolean inactiveEmployee()
        {

            try
            {

                var parameters = new Dictionary<String, String>() { { "EmployeeName", EmployeeName }, { "Category", Category } };
                InfoPCMS.db.executeInsertOrUpdate("spInactiveEmployee", parameters);
                return true;


            }
            catch (Exception ex)
            {

                throw ex;

            }
            return false;

        }

        

    }
}
