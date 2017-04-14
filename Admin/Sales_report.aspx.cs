using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;



public partial class Admin_Sales_report : System.Web.UI.Page
{
    public static int company_id=0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["company_id"] != null)
        {
            company_id = Convert.ToInt32(Session["company_id"].ToString());
        }

        TextBox1.Text = Session["Name"].ToString();
        TextBox2.Text = company_id.ToString();
       
   
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Sales_entry.aspx");


    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}