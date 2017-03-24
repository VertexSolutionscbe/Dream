using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SALES_REPORT_VIEW : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string INVOICENO = "";
       
           TextBox1.Text = Session["Name"].ToString();
       
    }
}