using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InfoPCMS.Services
{
    class CustomerService
    {



        private String strCustomerName;
        private String strContactPerson;
        private String strDesignation;
        private String strContactNo;
        private String strMobileNo;
        private String strAddress;
        private String strEmail;
        private String strFaxNo;
        private String strTaxType;
        private String strVATNo;
        private String strSVATNo;
        private String strLocation;
        private String strServiceCenter;
        private String strDistanceToServiceCenter;
        private String strDistanceFromColombo;
        private String strHiltopSite;
        private String strNonMotorable;
        private String strIsActive;

        private String strIsTemp;

        private String strCustomerCategory;

        public String StrCustomerCategory
        {
            get { return strCustomerCategory; }
            set { strCustomerCategory = value; }
        }

        // for customer Location

        private int intCustomerId;
        private String strSubLocation;



        public String StrCustomerName
        {
            get { return strCustomerName; }
            set { strCustomerName = value; }
        }

        public String StrContactPerson
        {
            get { return strContactPerson; }
            set { strContactPerson = value; }
        }

        public String StrDesignation
        {
            get { return strDesignation; }
            set { strDesignation = value; }
        }

        public String StrContactNo
        {
            get { return strContactNo; }
            set { strContactNo = value; }
        }
        public String StrMobileNo
        {
            get { return strMobileNo; }
            set { strMobileNo = value; }
        }

        public String StrAddress
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        public String StrEmail
        {
            get { return strEmail; }
            set { strEmail = value; }
        }

        public String StrFaxNo
        {
            get { return strFaxNo; }
            set { strFaxNo = value; }
        }
        public String StrTaxType
        {
            get { return strTaxType; }
            set { strTaxType = value; }
        }

        public String StrVATNo
        {
            get { return strVATNo; }
            set { strVATNo = value; }
        }
        public String StrSVATNo
        {
            get { return strSVATNo; }
            set { strSVATNo = value; }
        }

        public String StrLocation
        {
            get { return strLocation; }
            set { strLocation = value; }
        }
        public String StrServiceCenter
        {
            get { return strServiceCenter; }
            set { strServiceCenter = value; }
        }

        public String StrDistanceToServiceCenter
        {
            get { return strDistanceToServiceCenter; }
            set { strDistanceToServiceCenter = value; }
        }
        public String StrDistanceFromColombo
        {
            get { return strDistanceFromColombo; }
            set { strDistanceFromColombo = value; }
        }

        public String StrHiltopSite
        {
            get { return strHiltopSite; }
            set { strHiltopSite = value; }
        }
        public String StrNonMotorable
        {
            get { return strNonMotorable; }
            set { strNonMotorable = value; }
        }

        public String StrIsActive
        {
            get { return strIsActive; }
            set { strIsActive = value; }
        }


        public String StrIsTemp
        {
            get { return strIsTemp; }
            set { strIsTemp = value; }
        }

        public int IntCustomerId
        {
            get { return intCustomerId; }
            set { intCustomerId = value; }
        }


        public String StrSubLocation
        {
            get { return strSubLocation; }
            set { strSubLocation = value; }
        }

        public Boolean AddCustomer()
        {

            try
            {

                var parameters = new Dictionary<String, String>() { { "CustomerName", StrCustomerName },{ "CustomerCategory", StrCustomerCategory },
              { "ContactPerson", strContactPerson },
              { "Designation", strDesignation }, 
              { "ContactNo", strContactNo }, 
              { "MobileNo", strMobileNo },
              { "Address", strAddress }, 
              { "Email", strEmail }, 
              { "FaxNo", strFaxNo },
              { "TaxType", strTaxType }, { "VATNo", strVATNo }, 
              { "SVATNo", strSVATNo }, 
              { "Location", strLocation}, 
              { "ServiceCenter", strServiceCenter }, 
              { "DistanceToServiceCenter", strDistanceToServiceCenter },
              { "DistanceFromColombo", strDistanceFromColombo }, 
              { "HiltopSite", strHiltopSite },
              { "NonMotorable", strNonMotorable }, 
              { "IsActive", strIsActive },{ "IsTemp", strIsTemp } };

                InfoPCMS.db.executeInsertOrUpdate("spInsertCustomer", parameters);
                return true;

            }
            catch (Exception ex)
            {

                throw ex;

            }
            return false;
        }

        public Boolean AddCustomerLocation()
        {

            try
            {

                var parameters = new Dictionary<String, String>() { 
               { "CustomerId", intCustomerId.ToString() },
              { "CustomerName", StrCustomerName },
              { "ContactPerson", strContactPerson },
              { "Designation", strDesignation }, 
              { "ContactNo", strContactNo }, 
             
              { "Address", strAddress }, 
              { "Email", strEmail }, 
              { "Fax", strFaxNo },
             
              { "Location", strLocation}, { "SubLocation", strSubLocation} ,
              { "ServiceCenter", strServiceCenter }, 
              { "DistanceToServiceCenter", strDistanceToServiceCenter },
              { "DistanceFromColombo", strDistanceFromColombo }, 
              { "HiltopSite", strHiltopSite },
              { "NonMotorable", strNonMotorable }, 
               };


                InfoPCMS.db.executeInsertOrUpdate("spInsertCustomerLocation", parameters);
                return true;

            }
            catch (Exception ex)
            {

                throw ex;

            }
            return false;
        }


        //public Boolean addCustomer() {

        //    try {

        //        var parameters = new Dictionary<String, String>() { { "CustomerName", CustomerName }, { "ContactNo", ContactNo }, { "Address", Address }, { "Email", Email }, { "ContactFrom", ContactFrom }, { "CreditDays", CreditDays.ToString() }, { "TaxType", TaxType }, { "VATNo", VATNo }, { "SVATNo", SVATNo }, { "SiteAddress", SiteAddress }, { "MobileNo", MobileNo }, { "Fax", Fax }, { "Company", Company }, { "Designation", Designation }, { "NBTChargable", NbtChargeble }, { "DistanceToServiceCenter", DistanceToServiceCentr }, { "DistanceFromColobo", DistanceFromColombo }, { "HiltopSite", HilltopSite }, { "ServiceCenter", ServiceCenter }, { "NonMotorable", nonmotorable },{ "IsActive", IsActive } };
        //        InfoPCMS.db.executeInsertOrUpdate("spInsertCustomer", parameters);
        //        return true;

        //    }catch(Exception ex){

        //        throw ex;

        //    }
        //    return false;

        //}


        public DataTable GetAllCustomerDetails()
        {

            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery(" select T0.*,T1.CategoryName from Customer T0  LEFT JOIN CustomerCategory T1 ON T0.CustomerCategory = T1.Id");
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;

            }


        }

        public void GetCustomerDetailsByName()
        {
            try
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery("SELECT * FROM Customer WHERE CustomerName='" + strCustomerName + "'");
                if (dt.Rows.Count > 0)
                {

                    intCustomerId = Convert.ToInt32( dt.Rows[0]["Id"].ToString());
                    strCustomerCategory = dt.Rows[0]["CustomerCategory"].ToString();
                    strContactPerson = dt.Rows[0]["ContactPerson"].ToString();
                    strDesignation = dt.Rows[0]["Designation"].ToString();
                    strContactNo = dt.Rows[0]["ContactNo"].ToString();
                    strMobileNo = dt.Rows[0]["MobileNo"].ToString();
                    strAddress = dt.Rows[0]["Address"].ToString();
                    strEmail = dt.Rows[0]["Email"].ToString();
                    strFaxNo = dt.Rows[0]["FaxNo"].ToString();
                    strTaxType = dt.Rows[0]["TaxType"].ToString();
                    strVATNo = dt.Rows[0]["VATNo"].ToString();
                    strSVATNo = dt.Rows[0]["SVATNo"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                    strServiceCenter = dt.Rows[0]["ServiceCenter"].ToString();
                    strDistanceToServiceCenter = dt.Rows[0]["DistanceToServiceCenter"].ToString();
                    strDistanceFromColombo = dt.Rows[0]["DistanceFromColombo"].ToString();
                    strHiltopSite = dt.Rows[0]["HiltopSite"].ToString();
                    strNonMotorable = dt.Rows[0]["NonMotorable"].ToString();
                    strIsActive = dt.Rows[0]["IsActive"].ToString();
                    strIsTemp = dt.Rows[0]["IsTemp"].ToString();

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void GetCustomerDetailsByID()
        {
            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("SELECT * FROM Customer where Id='" + intCustomerId + "'");
                if (dt.Rows.Count > 0)
                {
                    strCustomerName = dt.Rows[0]["CustomerName"].ToString();
                    strCustomerCategory = dt.Rows[0]["CustomerCategory"].ToString();
                    strContactPerson = dt.Rows[0]["ContactPerson"].ToString();
                    strDesignation = dt.Rows[0]["Designation"].ToString();
                    strContactNo = dt.Rows[0]["ContactNo"].ToString();
                    strMobileNo = dt.Rows[0]["MobileNo"].ToString();
                    strAddress = dt.Rows[0]["Address"].ToString();
                    strEmail = dt.Rows[0]["Email"].ToString();
                    strFaxNo = dt.Rows[0]["FaxNo"].ToString();
                    strTaxType = dt.Rows[0]["TaxType"].ToString();
                    strVATNo = dt.Rows[0]["VATNo"].ToString();
                    strSVATNo = dt.Rows[0]["SVATNo"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                    strServiceCenter = dt.Rows[0]["ServiceCenter"].ToString();
                    strDistanceToServiceCenter = dt.Rows[0]["DistanceToServiceCenter"].ToString();
                    strDistanceFromColombo = dt.Rows[0]["DistanceFromColombo"].ToString();
                    strHiltopSite = dt.Rows[0]["HiltopSite"].ToString();
                    strNonMotorable = dt.Rows[0]["NonMotorable"].ToString();
                    strIsActive = dt.Rows[0]["IsActive"].ToString();
                    strIsTemp = dt.Rows[0]["IsTemp"].ToString();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public Boolean UpdateCustomer()
        {
            try
            {
              //  new DatabaseService().executeUpdateQuery("DELETE FROM Customer WHERE CustomerName='" + CustomerName + "'");
                
                var parameters = new Dictionary<String, String>() { 
                 { "Id", IntCustomerId.ToString() },
                { "CustomerName", StrCustomerName },
                { "CustomerCategory", StrCustomerCategory },
              { "ContactPerson", strContactPerson },
              { "Designation", strDesignation }, 
              { "ContactNo", strContactNo }, 
              { "MobileNo", strMobileNo },
              { "Address", strAddress }, 
              { "Email", strEmail }, 
              { "FaxNo", strFaxNo },
              { "TaxType", strTaxType }, { "VATNo", strVATNo }, 
              { "SVATNo", strSVATNo }, 
              { "Location", strLocation}, 
              { "ServiceCenter", strServiceCenter }, 
              { "DistanceToServiceCenter", strDistanceToServiceCenter },
              { "DistanceFromColombo", strDistanceFromColombo }, 
              { "HiltopSite", strHiltopSite },
              { "NonMotorable", strNonMotorable }, 
              { "IsActive", strIsActive } ,{ "IsTemp", strIsTemp }};
                
                
                
                
               // var parameters = new Dictionary<String, String>() { { "CustomerName", CustomerName }, { "ContactNo", ContactNo }, { "Address", Address }, { "Email", Email }, { "ContactFrom", ContactFrom }, { "CreditDays", CreditDays.ToString() }, { "TaxType", TaxType }, { "VATNo", VATNo }, { "SVATNo", SVATNo }, { "SiteAddress", SiteAddress }, { "MobileNo", MobileNo }, { "Fax", Fax }, { "Company", Company }, { "Designation", Designation }, { "NBTChargable", NbtChargeble }, { "DistanceToServiceCenter", DistanceToServiceCentr }, { "DistanceFromColobo", DistanceFromColombo }, { "HiltopSite", HilltopSite }, { "IsActive", IsActive } };
                InfoPCMS.db.executeInsertOrUpdate("spUpdateCustomer", parameters);
                //  var parameters = new Dictionary<String, String>() { { "CustomerName", CustomerName }, { "ContactNo", ContactNo }, { "Address", Address }, { "Email", Email }, { "ContactFrom", ContactFrom }, { "CreditDays", CreditDays.ToString() }, { "TaxType", TaxType }, { "VATNo", VATNo }, { "SVATNo", SVATNo }, { "SiteAddress", SiteAddress }, { "MobileNo", MobileNo }, { "Fax", Fax } };
                //  InfoPCMS.db.executeInsertOrUpdate("spUpdateCustomer", parameters);
                return true;

            }
            catch (Exception ex)
            {

                throw ex;

            }


        }

        public Boolean UpdateCustomerLocation()
        {
            try
            {

                var parameters = new Dictionary<String, String>() { 
               { "CustomerId", intCustomerId.ToString() },
              { "CustomerName", StrCustomerName },
              { "ContactPerson", strContactPerson },
              { "Designation", strDesignation }, 
              { "ContactNo", strContactNo },             
              { "Address", strAddress }, 
              { "Email", strEmail }, 
              { "Fax", strFaxNo },            
              { "Location", strLocation}, { "SubLocation", strSubLocation} ,
              { "ServiceCenter", strServiceCenter }, 
              { "DistanceToServiceCenter", strDistanceToServiceCenter },
              { "DistanceFromColombo", strDistanceFromColombo }, 
              { "HiltopSite", strHiltopSite },
              { "NonMotorable", strNonMotorable } };


                InfoPCMS.db.executeInsertOrUpdate("spUpdateCustomerLocation", parameters);
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
