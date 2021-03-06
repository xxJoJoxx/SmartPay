﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPay.Models;

namespace SmartPay.NWC
{
    public partial class GenerateBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void payment_click(object sender, EventArgs e)
        {
            //for later use just incase you need it
            Session["Customer_ID"] = UserControl1.GetCustID;
            Session["Customer_Name"] = FullName.Text;

            int x = 0;
            decimal amount = 15000.00m; //Made a varible so i could hard code the amount and make it a decimal instead of a float

            Bill_Generation bill = new Bill_Generation();// bill gen class in Models
            // I didn't add a field to insert bill ID because it should be auto generated upon insert to db

            //Converting session object value to int
            if (Int32.TryParse(Session["Customer_ID"].ToString(), out x))
            {
                bill.Cust_id = x;
            }

            //setting other fields for insert
            bill.Cust_name = Session["Customer_Name"].ToString();
            bill.StatementDate = DateTime.Today;
            bill.Due_date = DateTime.Now.AddDays(28);
            bill.amt = amount;// I made this static because since we're only dealing with telephone bill payment it would be easy to work with

          
              // link to your linq query class here
            Models.LinqQueries.Generate_bill(bill.Cust_id, bill.Cust_name, bill.StatementDate, bill.Due_date, bill.amt);

            /* if there's a problem with the scotiabank datacontext check the spelling for Account(s)
             hopefully there won't be tho...*/

            Models.LinqQueries.Cust_genbill(bill.Cust_id, bill.Cust_name, bill.StatementDate, bill.Due_date, bill.amt);
        }
    }
}