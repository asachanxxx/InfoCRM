using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoPCMS.Services
{
    class MessageService
    {
        //ERRORS
        private String LOGIN_ERROR = "Incorrect Username or Password!";
        private String EMPTY_FIELDS_ERROR = "Cannot leave the required fields empty";
        private String NO_ITEMSELECTED_ERROR = "Sorry, can't proceed, no item selected";
        private String NOT_AUTHORIZED_ERROR = "You are not authorized to use this feature";
        private String ENTER_AMOUNT = "Please Enter Amount";
        private String REQUIRED_FIELDS = "Please Enter Required Fields.";
        
        
        
        
        //INFORMATION
        private String RECORD_ADDED_INFORMATION = "Record Saved Successfully";
        private String RECORD_UPDATED_INFORMATION = "Record Updated Successfully";
        private String NO_SUCH_RECORD_INFORMATION = "No such record exists";
        private String STATUS_CHANGED_INFORMATION = "Status changed successfully";
        private String RECORD_DELETED_INFORMATION = "Record deleted successfully";

        private String CUSTOMER_ACTIVE = "Customer Activated Successfully";


        public String GET_ENTER_AMOUNT()
        {

            return this.ENTER_AMOUNT;

        }

        public String GET_NOT_AUTHORIZED_ERROR()
        {

            return this.NOT_AUTHORIZED_ERROR;
        
        }

        public String GET_NO_ITEMSELECTED_ERROR()
        {

            return this.NO_ITEMSELECTED_ERROR;

        }
         

        public String GET_LOGIN_ERROR()
        {

            return this.LOGIN_ERROR;

        }

        public String GET_EMPTY_FIELDS_ERROR()
        {

            return this.EMPTY_FIELDS_ERROR;

        }


        //--------------------------------------------
        public String GET_STATUS_CHANGED_INFORMATION()
        {

            return this.STATUS_CHANGED_INFORMATION;

        }



        public String GET_RECORD_ADDED_INFORMATION()
        {

            return this.RECORD_ADDED_INFORMATION;

        }

        public String GET_RECORD_DELETED_INFORMATION()
        {

            return this.RECORD_DELETED_INFORMATION;

        }

        public String GET_RECORD_UPDATED_INFORMATION()
        {

            return this.RECORD_UPDATED_INFORMATION;

        }

        public String GET_NO_SUCH_RECORD_INFORMATION()
        {

            return this.NO_SUCH_RECORD_INFORMATION;

        }


        public String GET_CUSTOMER_ACTIVE()
        {

            return this.CUSTOMER_ACTIVE;

        }

        


    }
}
