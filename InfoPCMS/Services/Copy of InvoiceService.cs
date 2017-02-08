using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InfoPCMS.Services
{
    class InvoiceService
    {
        private String invoiceno;
        private String jobno;
        private String quotationno;
        private String description;
        private Double amount;
        private String customertype;
        private String invoicedby;
        private String invoicedate;
        private String status;
        private String paymentno;
        private String paymenttype;
        private String refno;

        private Double nbt;
        private Double vat;
        private Double total;

        private String misc;

        public String MISC
        {
            get { return misc; }
            set { misc = value; }
        }


        public Double Total
        {
            get { return total; }
            set { total = value; }
        }


        public Double VAT
        {
            get { return vat; }
            set { vat = value; }
        }


        public Double NBT
        {
            get { return nbt; }
            set { nbt = value; }
        }
        

        public String RefNo
        {
            get { return refno; }
            set { refno = value; }
        }
        

        public String PaymentType
        {
            get { return paymenttype; }
            set { paymenttype = value; }
        }


        public String PaymentNo
        {
            get { return paymentno; }
            set { paymentno = value; }
        }


        public String Status
        {
            get { return status; }
            set { status = value; }
        }


        public String InvoiceDate
        {
            get { return invoicedate; }
            set { invoicedate = value; }
        }


        public String InvoicedBy
        {
            get { return invoicedby; }
            set { invoicedby = value; }
        }


        public String CustomerType
        {
            get { return customertype; }
            set { customertype = value; }
        }


        public Double Amount
        {
            get { return amount; }
            set { amount = value; }
        }


        public String Description
        {
            get { return description; }
            set { description = value; }
        }


        public String QuotationNo
        {
            get { return quotationno; }
            set { quotationno = value; }
        }


        public String JobNo
        {
            get { return jobno; }
            set { jobno = value; }
        }


        public String InvoiceNo
        {
            get { return invoiceno; }
            set { invoiceno = value; }
        }

        public Boolean addInvoice() {

            try {

                var parameters = new Dictionary<String, String>() { { "Invoiceno", InvoiceNo }, { "Jobno", JobNo }, { "Quotationno", QuotationNo }, { "Description", Description }, { "Amount", Amount.ToString() }, { "Customertype", CustomerType }, { "Invoicedby", InvoicedBy }, { "Invoicedate", InvoiceDate }, { "PaymentType", PaymentType }, { "RefNo", RefNo }, { "NBT", NBT.ToString() }, { "VAT", VAT.ToString() }, { "Total", Total.ToString() },{"MISC",MISC} };
                InfoPCMS.db.executeInsertOrUpdate("spInsertInvoice", parameters);
                return true;
            
            }
            catch (Exception ex) {

                throw ex;
            
            }
            return false;
        }

        public DataTable getInvoicesByJob(String jobnum) {

            try {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Invoice where JobNo = '"+jobnum+"'");
                return dt;
            }
            catch (Exception ex) {

                throw ex;
            
            }
        
        }


        public void getInvoiceDetails() {

            try {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Invoice where InvoiceNo = '"+InvoiceNo+"'");
                if(dt.Rows.Count>0){

                    JobNo = dt.Rows[0]["JobNo"].ToString();
                    QuotationNo = dt.Rows[0]["QuotationNo"].ToString();
                    Description = dt.Rows[0]["Description"].ToString();
                    Amount = Double.Parse(dt.Rows[0]["Amount"].ToString());
                    CustomerType = dt.Rows[0]["CustomerType"].ToString();
                    InvoicedBy = dt.Rows[0]["InvoicedBy"].ToString();
                    InvoiceDate = dt.Rows[0]["InvoiceDate"].ToString();
                    PaymentNo = dt.Rows[0]["PaymentNo"].ToString();
                    Status = dt.Rows[0]["Status"].ToString();
                    PaymentType = dt.Rows[0]["PaymentType"].ToString();
                    RefNo = dt.Rows[0]["RefNo"].ToString();
                    NBT = Double.Parse(dt.Rows[0]["NBT"].ToString());
                    VAT = Double.Parse(dt.Rows[0]["VAT"].ToString());
                    Total = Double.Parse(dt.Rows[0]["Total"].ToString());
                    MISC = dt.Rows[0]["MISC"].ToString();
                }
            
            }
            catch (Exception ex) {

                throw ex;
            
            }
        
        }


        public Double getInvoiceTotal(String invno) {
            Double total = 0.0;
            try {

             DataTable dt = InfoPCMS.db.executeSelectQuery("select Total from Invoice where InvoiceNo='"+invno+"'");
             if(dt.Rows.Count>0){

                 total = Double.Parse(dt.Rows[0]["Total"].ToString());
                 
                }

             return total;

            }
            catch (Exception ex) {

                throw ex;
            
            }
        
        
        }

        public void completePayment() {

            try {

                var parameters = new Dictionary<String, String>() { { "InvoiceNo", InvoiceNo }};
                InfoPCMS.db.executeInsertOrUpdate("spCompletePayment", parameters);
               
            
            }
            catch(Exception ex){

                throw ex;
            
            }
        
        }

    }
}
