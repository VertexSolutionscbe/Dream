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
using System.Data.SqlClient;


#endregion

public partial class Admin_Invoice : System.Web.UI.Page
{
    float tot = 0;
    DataTable dt = null;
    int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            getinvoiceno();
            showdropdown();
            Dispatched_Through();
            company_id = Convert.ToInt32(Session["company_id"].ToString());
            Tax();
            if (ViewState["Details"] == null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("S_no");
                dataTable.Columns.Add("ProductName");
                dataTable.Columns.Add("Quantity");
                dataTable.Columns.Add("Rate");
                dataTable.Columns.Add("Per");
                dataTable.Columns.Add("Discount");
                dataTable.Columns.Add("Amount");
                ViewState["Details"] = dataTable;
            }

            string name = Session["name"].ToString();
            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("select * from invoice where Invoice_no='" + name + "'", con1);
            SqlDataReader dr1;
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                TextBox17.Text = dr1["Customer_address"].ToString();
                Label1.Text = dr1["Invoice_no"].ToString();
                TextBox9.Text = dr1["Delivery_note"].ToString();
                TextBox10.Text = dr1["Sup_ref"].ToString();
                TextBox11.Text = dr1["B_order_no"].ToString();
                TextBox21.Text = dr1["Des_Doc_no"].ToString();
                DropDownList4.SelectedItem.Text = dr1["Des_through"].ToString();
                TextBox19.Text = dr1["Terms_of_delivery"].ToString();
                TextBox22.Text = dr1["Dated1"].ToString();
                TextBox23.Text = dr1["Mode_payment"].ToString();
                TextBox24.Text = dr1["Other_ref"].ToString();
                TextBox18.Text = dr1["Dated2"].ToString();
                TextBox12.Text = dr1["dated3"].ToString();
                TextBox13.Text = dr1["Destination"].ToString();
                TextBox26.Text = dr1["Com_name"].ToString();
                TextBox27.Text = dr1["Bank_name"].ToString();
                TextBox16.Text = dr1["Acc_no"].ToString();
                TextBox25.Text = dr1["Branch_code"].ToString();
                TextBox28.Text = dr1["ifs_code"].ToString();
                TextBox29.Text = dr1["terms_conditions"].ToString();
                TextBox8.Text = dr1["total"].ToString();
                TextBox14.Text = dr1["vat_amount"].ToString();
                DropDownList1.SelectedItem.Text = dr1["vat_per"].ToString();
                TextBox15.Text = dr1["grand_total"].ToString();
            }



            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd = new SqlCommand("select * from Bank_details where no=1", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TextBox26.Text = dr["Company_name"].ToString();
            
                TextBox27.Text = dr["Bank_name"].ToString();
                TextBox16.Text = dr["acc_no"].ToString();
                TextBox25.Text = dr["branch"].ToString();
                TextBox28.Text = dr["IFS_code"].ToString();
                TextBox29.Text = dr["terms_conditions"].ToString();
            }

        }  
        
    }
    private void getinvoiceno()
    {
        int a;

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con1.Open();
        string query = "Select Max(Invoice_no) from Invoice";
        SqlCommand cmd1 = new SqlCommand(query, con1);
        SqlDataReader dr = cmd1.ExecuteReader();
        if (dr.Read())
        {
            string val = dr[0].ToString();
            if (val == "")
            {
                Label1.Text = "1";
            }
            else
            {
                a = Convert.ToInt32(dr[0].ToString());
                a = a + 1;
                Label1.Text = a.ToString();
            }
        }
    }
    private void Tax()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Invoice_Tax where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Invoice_Tax";
        DropDownList1.DataValueField = "No";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }
    protected void LoginLink_OnClick(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/login.aspx");

    }
    private void showdropdown()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Product_category where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Product_category";
        DropDownList2.DataValueField = "No";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }
    private void Dispatched_Through()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Dispatched_through where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList4.DataSource = ds;
        DropDownList4.DataTextField = "Dispatched_through";
        DropDownList4.DataValueField = "No";
        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string name1 = Session["name"].ToString();
        if (name1 != null)
        {

            int a = GridView1.Rows.Count;
            int b = a + 1;
            string str0 = b.ToString();
            string str = DropDownList3.SelectedItem.Text;
            string str1 = TextBox2.Text.Trim();
            string str2 = TextBox3.Text.Trim();
            string str3 = TextBox5.Text.Trim();
            string str4 = TextBox6.Text.Trim();
            string str5 = TextBox7.Text.Trim();
            DataTable dt = new DataTable();
            dt.Rows.Add(str0, str, str1, str2, str3, str4, str5);
        
            GridView1.DataSource = dt;
           
            GridView1.DataBind();
          

        }
        else
        {
            int a = GridView1.Rows.Count;
            int b = a + 1;
            string str0 = b.ToString();
            string str = DropDownList3.SelectedItem.Text;
            string str1 = TextBox2.Text.Trim();
            string str2 = TextBox3.Text.Trim();
            string str3 = TextBox5.Text.Trim();
            string str4 = TextBox6.Text.Trim();
            string str5 = TextBox7.Text.Trim();
            dt = (DataTable)ViewState["Details"];
            dt.Rows.Add(str0, str, str1, str2, str3, str4, str5);
            ViewState["Details"] = dt;
            GridView1.DataSource = dt;
            GridView1.EmptyDataText = "S_no";
            GridView1.EmptyDataText = "ProductName";
            GridView1.EmptyDataText = "Quantity";
            GridView1.EmptyDataText = "Rate";
            GridView1.EmptyDataText = "Per";
            GridView1.EmptyDataText = "Discount";
            GridView1.EmptyDataText = "Amount";
            GridView1.DataBind();


            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
        }

    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {


        float DIS = float.Parse(TextBox6.Text);
        float TOTAL = float.Parse(TextBox3.Text);
        float value =float.Parse( string.Format("{0:0.00}", (TOTAL * DIS / 100)).ToString());
        float S = float.Parse(value.ToString());
        TextBox7.Text = string.Format("{0:0.00}", (TOTAL - S)).ToString();
       

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tot = tot + float.Parse(e.Row.Cells[6].Text);
            TextBox8.Text = tot.ToString();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            float DIS = float.Parse(DropDownList1.SelectedItem.Text);
            float TOTAL = float.Parse(TextBox8.Text);
            TextBox14.Text = string.Format("{0:0.00}", (TOTAL * DIS / 100)).ToString();
            float S = float.Parse(TextBox14.Text);
            TextBox15.Text = string.Format("{0:0.00}", Math.Round((TOTAL + S))).ToString();
        }
        catch (Exception er)
        { }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from product_entry where category='" + DropDownList2.SelectedItem.Text+ "'  and com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "Product_name";
        DropDownList3.DataValueField = "No";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }
    private void SaveDetails(GridViewRow row)
    {


        company_id = Convert.ToInt32(Session["company_id"].ToString());

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



        SqlCommand cmd = new SqlCommand("INSERT INTO invoice_details VALUES(@invoice_no,@S_no,@product_name,@Quantity,@rate,@Per,@Discount,@amount,@com_id)", con);
        cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
        cmd.Parameters.AddWithValue("@S_no", row.Cells[0].Text);
        cmd.Parameters.AddWithValue("@product_name", row.Cells[1].Text);
        cmd.Parameters.AddWithValue("@Quantity", row.Cells[2].Text);
        cmd.Parameters.AddWithValue("@rate", row.Cells[3].Text);
        cmd.Parameters.AddWithValue("@Per", row.Cells[4].Text);
        cmd.Parameters.AddWithValue("@Discount", row.Cells[5].Text);
        cmd.Parameters.AddWithValue("@amount", row.Cells[6].Text);
        cmd.Parameters.AddWithValue("@com_id", company_id);
      

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();


       



       


    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in this.GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                this.SaveDetails(row);
            }

        }

        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("insert into invoice values(@Invoice_no,@Customer_address,@Delivery_note,@Sup_ref,@B_order_no,@Des_Doc_no,@Des_through,@Terms_of_delivery,@Dated1,@Mode_payment,@Other_ref,@Dated2,@dated3,@Destination,@Com_name,@Bank_name,@Acc_no,@Branch_code,@ifs_code,@terms_conditions,@total,@vat_per,@vat_amount,@grand_total,@com_id)", con);
        cmd.Parameters.AddWithValue("@Invoice_no",Label1.Text);
        cmd.Parameters.AddWithValue("@Customer_address",TextBox17.Text);
         cmd.Parameters.AddWithValue("@Delivery_note",TextBox9.Text);
        cmd.Parameters.AddWithValue("@Sup_ref",TextBox10.Text);
        cmd.Parameters.AddWithValue("@B_order_no",TextBox11.Text);
        cmd.Parameters.AddWithValue("@Des_Doc_no",TextBox21.Text);
          cmd.Parameters.AddWithValue("@Des_through",DropDownList4.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Terms_of_delivery",TextBox9.Text);
         cmd.Parameters.AddWithValue("@Dated1",TextBox22.Text);
        cmd.Parameters.AddWithValue("@Mode_payment",TextBox23.Text);
         cmd.Parameters.AddWithValue("@Other_ref",TextBox24.Text);
        cmd.Parameters.AddWithValue("@Dated2",TextBox18.Text);
         cmd.Parameters.AddWithValue("@dated3",TextBox12.Text);
         cmd.Parameters.AddWithValue("@Destination",TextBox13.Text);
        cmd.Parameters.AddWithValue("@Com_name",TextBox26.Text);
         cmd.Parameters.AddWithValue("@Bank_name",TextBox27.Text);
        cmd.Parameters.AddWithValue("@Acc_no",TextBox16.Text);
         cmd.Parameters.AddWithValue("@Branch_code",TextBox25.Text);
        cmd.Parameters.AddWithValue("@ifs_code",TextBox28.Text);
           cmd.Parameters.AddWithValue("@terms_conditions",TextBox29.Text);
           cmd.Parameters.AddWithValue("@total",TextBox8.Text);
           cmd.Parameters.AddWithValue("@vat_per",DropDownList1.SelectedItem.Text);
         cmd.Parameters.AddWithValue("@vat_amount",TextBox14.Text);
         cmd.Parameters.AddWithValue("@grand_total",TextBox15.Text);
         cmd.Parameters.AddWithValue("@com_id", company_id);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Invoice created successfully')", true);
       
        Response.Redirect("Invoice_details.aspx");
        TextBox17.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox21.Text = "";
        TextBox9.Text = "";
        TextBox22.Text = "";
        TextBox23.Text = "";
        TextBox24.Text = "";
        TextBox18.Text = "";
        TextBox12.Text = "";
        TextBox21.Text = "";
        TextBox13.Text = "";
        TextBox26.Text = "";
        TextBox27.Text = "";
        TextBox16.Text = "";
        TextBox25.Text = "";
        TextBox28.Text = "";
        TextBox29.Text = "";
        TextBox8.Text = "";
        TextBox14.Text = "";
        TextBox15.Text = "";
        DropDownList4.ClearSelection();
        DropDownList2.ClearSelection();
        DropDownList3.ClearSelection();
        DataTable dataTable = new DataTable();
        dataTable = null;
        GridView1.DataSource = dataTable;
        GridView1.DataBind();
        getinvoiceno();
    }
   
    protected void Button5_Click(object sender, EventArgs e)
    {
      
        Response.Redirect("Invoice_report_show.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
      
        Response.Redirect("~/Default2.aspx");
    }
    protected void BindData()
    {
        string name1 = Session["name"].ToString();
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con.Open();
        SqlCommand cmd = new SqlCommand("select S_no,product_name,Quantity,rate,Per,Discount,amount from invoice_details where invoice_no='" + name1 + "' ", con);
      DataTable dt=new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        con.Close();


       
    }
}