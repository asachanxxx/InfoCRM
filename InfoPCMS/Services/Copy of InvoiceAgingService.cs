using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InfoPCMS.Services
{
    class InvoiceAgingService
    {

        

        public void fillServicesTempTable() {

            try
            {
                InfoPCMS.db.executeUpdateQuery("truncate table SalesAgingTemp");
                DataTable t1 = InfoPCMS.db.executeSelectQuery("select i.InvoiceNo,i.InvoiceDate,i.Amount,i.Total,i.QuotationNo,c.Id,c.CustomerName,DATEDIFF(DAY,i.InvoiceDate,GETDATE()) as dueperiod from Invoice i inner join Quotation q on q.QuotationNumber = i.QuotationNo inner join Inquiry inq on inq.InquiryNumber = q.InquiryNumber inner join Customer c on c.Id = inq.CustomerId where i.Status != 'Complete'");
                for (int i = 0; i < t1.Rows.Count;i++ )
                {
                    String invno = t1.Rows[i]["InvoiceNo"].ToString();
                    String invdate = t1.Rows[i]["InvoiceDate"].ToString();
                    String cusid = t1.Rows[i]["Id"].ToString();
                    String cusname = t1.Rows[i]["CustomerName"].ToString();

                    Double invamount = Double.Parse(t1.Rows[i]["Amount"].ToString());
                    Double invtotal = Double.Parse(t1.Rows[i]["Total"].ToString());

                    Double paidamount = 0.0;

                    DataTable t2 = InfoPCMS.db.executeSelectQuery("select SUM(p.Amount) as paidamount from Payment p where p.InvoiceNo = '"+invno+"' group by p.InvoiceNo");
                    if(t2.Rows.Count>0){

                        paidamount = Double.Parse(t2.Rows[0]["paidamount"].ToString());

                    }

                    Double dueamount = invtotal - paidamount;
                    int dueperiod = Int16.Parse(t1.Rows[i]["dueperiod"].ToString());

                    InfoPCMS.db.executeUpdateQuery("insert into SalesAgingTemp (CustomerId,CustomerName,InvoiceNumber,InvoiceDate,InvoiceAmount,InvoiceTotal,PaidAmount,DueAmount,DuePeriod) values ('"+cusid+"','"+cusname+"','"+invno+"','"+invdate+"','"+invamount+"','"+invtotal+"','"+paidamount+"','"+dueamount+"','"+dueperiod+"')");

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        
        
        }

        public void fillProductSaleTempTable()
        {

            try
            {
                InfoPCMS.db.executeUpdateQuery("truncate table SalesAgingTemp");
                DataTable t1 = InfoPCMS.db.executeSelectQuery("select i.InvoiceNumber,i.Date,i.Amount,i.Total,c.Id,c.CustomerName,DATEDIFF(DAY,i.Date,GETDATE()) as dueperiod from SaleInvoice i inner join Customer c on c.Id = i.Customer where i.Status != 'PAID'");
                for (int i = 0; i < t1.Rows.Count; i++)
                {
                    String invno = t1.Rows[i]["InvoiceNumber"].ToString();
                    String invdate = t1.Rows[i]["Date"].ToString();
                    String cusid = t1.Rows[i]["Id"].ToString();
                    String cusname = t1.Rows[i]["CustomerName"].ToString();

                    Double invamount = Double.Parse(t1.Rows[i]["Amount"].ToString());
                    Double invtotal = Double.Parse(t1.Rows[i]["Total"].ToString());

                    Double paidamount = 0.0;

                    DataTable t2 = InfoPCMS.db.executeSelectQuery("select SUM(p.Amount) as paidamount from Payment p where p.InvoiceNo = '" + invno + "' group by p.InvoiceNo");
                    if (t2.Rows.Count > 0)
                    {

                        paidamount = Double.Parse(t2.Rows[0]["paidamount"].ToString());

                    }

                    Double dueamount = invtotal - paidamount;
                    int dueperiod = Int16.Parse(t1.Rows[i]["dueperiod"].ToString());

                    InfoPCMS.db.executeUpdateQuery("insert into SalesAgingTemp (CustomerId,CustomerName,InvoiceNumber,InvoiceDate,InvoiceAmount,InvoiceTotal,PaidAmount,DueAmount,DuePeriod) values ('" + cusid + "','" + cusname + "','" + invno + "','" + invdate + "','" + invamount + "','" + invtotal + "','" + paidamount + "','" + dueamount + "','" + dueperiod + "')");

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


    }
}
