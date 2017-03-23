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

public partial class Admin_Sales_entry : System.Web.UI.Page
{
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox2.Focus();

            TextBox4.Attributes.Add("onkeypress", "return controlEnter('" + TextBox5.ClientID + "', event)");
            TextBox5.Attributes.Add("onkeypress", "return controlEnter('" + TextBox6.ClientID + "', event)");
            TextBox6.Attributes.Add("onkeypress", "return controlEnter('" + TextBox7.ClientID + "', event)");
            TextBox7.Attributes.Add("onkeypress", "return controlEnter('" + Button1.ClientID + "', event)");

            show_category();
            showrating();
            BindData();

            active();
            created();


            if (!IsPostBack)
            {
                if (ViewState["Details"] == null)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("Code");
                    dataTable.Columns.Add("Category Id");
                    dataTable.Columns.Add("Subcategory Id");
                    dataTable.Columns.Add("Product Name");
                    dataTable.Columns.Add("MRP");
                    dataTable.Columns.Add("Supplier");
                    dataTable.Columns.Add("Product Price");
                    ViewState["Details"] = dataTable;
                }
            }

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in this.GridView2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                this.SaveDetail(row);
            }

        }

    }
    private void SaveDetail(GridViewRow row)
    {
        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("insert into product_entry values(@code,@category_id,@subcategory_id,@product_name,@mrp,@supplier,@product_price)", CON);
        cmd.Parameters.AddWithValue("@code", row.Cells[0].Text);
        cmd.Parameters.AddWithValue("@category_id", row.Cells[1].Text);
        cmd.Parameters.AddWithValue("@subcategory_id", row.Cells[2].Text);
        cmd.Parameters.AddWithValue("@product_name", row.Cells[3].Text);
        cmd.Parameters.AddWithValue("@mrp", row.Cells[4].Text);
        cmd.Parameters.AddWithValue("@supplier", row.Cells[5].Text);
        cmd.Parameters.AddWithValue("@product_price", row.Cells[6].Text);
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Sub Category created successfully')", true);
        BindData();
        show_category();

        TextBox1.Text = "";


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";

        show_category();
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
    protected void BindData()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from product_entry ORDER BY code asc", con);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();

    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("delete from subcategory where category_id='" + row.Cells[0].Text + "' ", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Sub Category deleted successfully')", true);

        BindData();
        show_category();



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
                cmd.CommandText = "select subcategoryname from subcategory where " +
                "subcategoryname like @subcategoryname + '%'";
                cmd.Parameters.AddWithValue("@subcategoryname", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["subcategoryname"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from subcategory where category_id='" + DropDownList2.SelectedItem.Value + "'", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "subcategoryname";
        DropDownList1.DataValueField = "subcategory_id";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("All", "0"));



        con.Close();



    }
    private void show_category()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from category ORDER BY category_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "categoryname";
        DropDownList2.DataValueField = "category_id";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("All", "0"));


        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "categoryname";
        DropDownList3.DataValueField = "category_id";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("All", "0"));
        con.Close();
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
        GridView1.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GridView1.PageIndex + 1) + " of " + GridView1.PageCount;
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from subcategory where category_id='" + DropDownList3.SelectedItem.Value + "'", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList5.DataSource = ds;
        DropDownList5.DataTextField = "subcategoryname";
        DropDownList5.DataValueField = "subcategory_id";
        DropDownList5.DataBind();
        DropDownList5.Items.Insert(0, new ListItem("All", "0"));



        con.Close();
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from subcategory where subcategoryname='" + TextBox1.Text + "'", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string str1 = TextBox2.Text.Trim();
        string str2 = DropDownList3.SelectedItem.Value;
        string str3 = DropDownList5.SelectedItem.Value;
        string str4 = TextBox4.Text;
        string str5 = TextBox5.Text;
        string str6 = TextBox6.Text;
        string str7 = TextBox7.Text;
        dt = (DataTable)ViewState["Details"];
        dt.Rows.Add(str1, str2, str3, str4, str5, str6, str7);
        ViewState["Details"] = dt;
        GridView2.DataSource = dt;
        GridView2.EmptyDataText = "Code";
        GridView2.EmptyDataText = "Category Id";
        GridView2.EmptyDataText = "Subcategory Id";
        GridView2.EmptyDataText = "Product Name";
        GridView2.EmptyDataText = "MRP";
        GridView2.EmptyDataText = "Supplier";
        GridView2.EmptyDataText = "Product Price";
        GridView2.DataBind();


        TextBox2.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";




    }
}