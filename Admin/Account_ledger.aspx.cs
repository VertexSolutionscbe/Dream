﻿#region " Using "
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
using System.Drawing;
#endregion

public partial class Admin_Account_ledger : System.Web.UI.Page
{
    public static int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            getinvoiceno();
            show_category();
            showrating();
            BindData();

            active();
            created();


            if (Session["company_id"] != null)
            {
                company_id = Convert.ToInt32(Session["company_id"].ToString());
            }


        }
    }
    private void active()
    {

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;


        LinkButton Lnk = (LinkButton)sender;
        string name = Lnk.Text;
        Session["name"] = name;
        Response.Redirect("Account_show.aspx");


    }

    private void created()
    {

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct product_name from product_entry where Com_Id=@Com_Id and " +
                "product_name like @product_name + '%'";
                cmd.Parameters.AddWithValue("@product_name", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["product_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers1(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct Vendor_Name from Vendor where Com_Id=@Com_Id and " +
                "Vendor_Name like @Vendor_Name + '%'";
                cmd.Parameters.AddWithValue("@Vendor_Name", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Vendor_Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers11(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct barcode from product_stock where Com_Id=@Com_Id and " +
                "barcode like @barcode + '%'";
                cmd.Parameters.AddWithValue("@barcode", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["barcode"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    private void show_category()
    {
        if (Session["company_id"] != null)
        {
            company_id = Convert.ToInt32(Session["company_id"].ToString());
        }

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("SELECT  CONVERT(datetime,date,101) as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM sales_entry as a where Com_Id='" + company_id + "'  group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_entry where Com_Id='" + company_id + "'  group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_amount where Com_Id='" + company_id + "'  group by date,status,amount,value", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void BindData()
    {
        

    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {



    }
    private void getinvoiceno()
    {
        
    }


    protected void LoginLink_OnClick(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/login.aspx");

    }

    protected void btnRandom_Click(object sender, EventArgs e)
    {
        Session["name1"] = "";
        Response.Redirect("~/Admin/Category_Add.aspx");
    }

    private void showcustomertype()
    {

    }
    private void showrating()
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
       



    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        if (Session["company_id"] != null)
        {
            company_id = Convert.ToInt32(Session["company_id"].ToString());
        }
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("SELECT  CONVERT(datetime,date,101) as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM sales_entry as a where date='" + TextBox3.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_entry as a where date='" + TextBox3.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_amount as a where date='" + TextBox3.Text + "' and Com_Id='" + company_id + "' group by date,status,amount,value", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
        if (Session["company_id"] != null)
        {
            company_id = Convert.ToInt32(Session["company_id"].ToString());
        }
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("SELECT CONVERT(datetime,date,101) as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM sales_entry as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_entry as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_amount as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "'   group by date,status,amount,value", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=gvtoexcel.xls");
        Response.ContentType = "application/excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Tell the compiler that the control is rendered
         * explicitly by overriding the VerifyRenderingInServerForm event.*/
    }
}