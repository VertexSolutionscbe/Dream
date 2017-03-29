#region " Using "
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
#endregion

public partial class RabbitDashboard : System.Web.UI.Page
{
    int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
       
        if(!IsPostBack)
{
DataTable dt = new DataTable();
using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]))
{
con.Open();
SqlCommand cmd = new SqlCommand("select qty,mrp from product_stock order by Product_code desc", con);
SqlDataAdapter da = new SqlDataAdapter(cmd);
da.Fill(dt);
con.Close();
}
string []x=new string[dt.Rows.Count];
int [] y = new int[dt.Rows.Count];
for(int i=0;i<dt.Rows.Count;i++)
{
x[i] = dt.Rows[i][0].ToString();
y[i] = Convert.ToInt32(dt.Rows[i][1]);
}
Chart1.Series[0].Points.DataBindXY(x,y);
}
       
    }
    protected void LoginLink_OnClick(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/login.aspx");

    }
    protected void BindData()
    {
        if (Session["company_id"] != null)
        {
            company_id = Convert.ToInt32(Session["company_id"].ToString());
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from product_stock where Com_Id='" + company_id + "' ORDER BY Product_code asc", con);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}