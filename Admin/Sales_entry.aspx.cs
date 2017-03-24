
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
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;





public partial class Admin_Sales_entry : System.Web.UI.Page
{
    float tot = 0;
    
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            TextBox8.Attributes.Add("onkeypress", "return controlEnter('" + TextBox13.ClientID + "', event)");
            TextBox13.Attributes.Add("onkeypress", "return controlEnter('" + TextBox14.ClientID + "', event)");
            TextBox14.Attributes.Add("onkeypress", "return controlEnter('" + DropDownList3.ClientID + "', event)");
            TextBox5.Attributes.Add("onkeypress", "return controlEnter('" + TextBox6.ClientID + "', event)");
            TextBox6.Attributes.Add("onkeypress", "return controlEnter('" + TextBox4.ClientID + "', event)");

          
            TextBox4.Attributes.Add("onkeypress", "return controlEnter('" + TextBox7.ClientID + "', event)");
            TextBox7.Attributes.Add("onkeypress", "return controlEnter('" + TextBox9.ClientID + "', event)");
           
            getinvoiceno();
            show_category();
            showrating();
            BindData();
            show_tax();
            active();
            created();
            TextBox6.Text = "";

            if (!IsPostBack)
            {
                if (ViewState["Details"] == null)
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Columns.Add("Barcode");
                    dataTable.Columns.Add("Product Name");
               
                    dataTable.Columns.Add("MRP");
              
                    dataTable.Columns.Add("Quantity");
                   
                    dataTable.Columns.Add("Total Amount");
                    ViewState["Details"] = dataTable;
                }
            }

        }
    }
    private void getinvoiceno()
    {
        int a;

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con1.Open();
        string query = "Select max(convert(int,SubString(invoice_no,PATINDEX('%[0-9]%',invoice_no),Len(invoice_no)))) from sales_entry";
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
    
    
    protected void Button1_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in this.GridView2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                this.SaveDetail(row);
            }

        }

        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("insert into sales_entry values(@invoice_no,@date,@customer_name,@customer_Address,@supplier_name,@total_qty,@total_amount,@grand_total)", CON);
        cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
        cmd.Parameters.AddWithValue("@date", TextBox8.Text);
        cmd.Parameters.AddWithValue("@customer_name", TextBox13.Text);
        cmd.Parameters.AddWithValue("@customer_Address", TextBox14.Text);
        cmd.Parameters.AddWithValue("@supplier_name", DropDownList3.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@total_qty", TextBox10.Text);
        cmd.Parameters.AddWithValue("@total_amount", TextBox10.Text);
        cmd.Parameters.AddWithValue("@grand_total", TextBox11.Text);
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();








        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Sales entry created successfully')", true);
        BindData();
        show_category();
        GridView2.DataSource = null;
        GridView2.DataBind();
        TextBox10.Text = "";
        TextBox11.Text = "";
        DataTable dataTable = new DataTable();
        dataTable = null;
        GridView2.DataSource = dataTable;
        GridView2.DataBind();
        show_tax();
        Session["Name"] = Label1.Text;




        DialogResult ans = MessageBox.Show("Want Print Preview?", "App Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (ans == DialogResult.No)
        {
            string OrderID = Label1.Text;
            //Create a ReportViewer Control.
            ReportViewer _reportviewer = new ReportViewer();
            LocalReport _localReport = _reportviewer.LocalReport;
            //Set the LocalReport properties for the report datasource and resource
            _localReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Jobs\\PrintOrder.rdlc";
            //_localReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory + "Jobs\\PrintOrder.rdlc";
            this.Sales_invoiceTableAdapters.Fill(this.billDataSet.tblOrderItems, OrderID);
            this.tblOrderTableAdapter.Fill(this.billDataSet.tblOrder, OrderID);
        
            ReportDataSource _reportDataSource1 = new ReportDataSource();
            _reportDataSource1.Name = "BillDataSet_tblOrder";
            _reportDataSource1.Value = this.billDataSet.tblOrder;
            ReportDataSource _reportDataSource2 = new ReportDataSource();
            _reportDataSource2.Name = "BillDataSet_tblOrderItems";
            _reportDataSource2.Value = this.billDataSet.tblOrderItems;
            _localReport.DataSources.Add(_reportDataSource1);
            _localReport.DataSources.Add(_reportDataSource2);
            _reportviewer.RenderingComplete += new RenderingCompleteEventHandler(_reportviewer_RenderingComplete);
            _reportviewer.RefreshReport();
        }
        else
        {
            FormPrntOrd _Order = new FormPrntOrd(Label1.Text);
            _Order.ShowDialog(this);
        }


      
        Response.Redirect("SALES_REPORT_VIEW.aspx");
     
    }
    private void SaveDetail(GridViewRow row)
    {



        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("insert into sales_entry_details values(@invoice_no,@barcode,@Product_code,@product_name,@mrp,@qty,@total_amount)", CON);
        cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
        cmd.Parameters.AddWithValue("@barcode", row.Cells[0].Text);
        cmd.Parameters.AddWithValue("@Product_code", row.Cells[1].Text);
        cmd.Parameters.AddWithValue("@product_name", row.Cells[1].Text);

        cmd.Parameters.AddWithValue("@mrp", row.Cells[2].Text);
        cmd.Parameters.AddWithValue("@qty", row.Cells[3].Text);
        cmd.Parameters.AddWithValue("@total_amount", row.Cells[4].Text);

      
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();

        
            SqlConnection CON1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("update product_stock set qty=qty-@qty where barcode='" + row.Cells[0].Text + "'", CON1);


       


            cmd1.Parameters.AddWithValue("@qty", row.Cells[3].Text);
          
            CON1.Open();
            cmd1.ExecuteNonQuery();
            CON1.Close();
        
       


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox6.Text = "";
        TextBox5.Text = "";
        TextBox4.Text = "";
       
        TextBox7.Text = "";
        TextBox9.Text = "";
        Label2.Text = "";
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


    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("delete from product_entry where code='" + row.Cells[1].Text + "' ", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Product entry deleted successfully')", true);

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
                cmd.CommandText = "select barcode from product_stock where " +
                "barcode like @barcode + '%'";
                cmd.Parameters.AddWithValue("@barcode", prefixText);
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
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        



    }
    private void show_tax()
    {
        
    }
    private void show_category()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Staff_Entry ORDER BY Emp_Code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


      

        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "Emp_Name";
        DropDownList3.DataValueField = "Emp_Code";
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



        
        string str1 = TextBox5.Text;
        string str2 = TextBox6.Text;

        string str3 = TextBox4.Text;
      
        string str6 = TextBox7.Text;
       
        string str7 = TextBox9.Text;
        dt = (DataTable)ViewState["Details"];
        dt.Rows.Add( str1, str2, str3, str6, str7);
        ViewState["Details"] = dt;
        GridView2.DataSource = dt;

        GridView2.EmptyDataText = "Barcode";
        GridView2.EmptyDataText = "Product Name";

      

        GridView2.EmptyDataText = "MRP";
       
        GridView2.EmptyDataText = "Quantity";
    
        GridView2.EmptyDataText = "Total Amount";
        GridView2.DataBind();




     





        TextBox6.Text = "";
        TextBox5.Text = "";
        TextBox4.Text = "";
      
        TextBox7.Text = "";
        TextBox9.Text = "";
        Label2.Text = "";





    }
    protected void TextBox7_TextChanged(object sender, EventArgs e)
    {
        float a = float.Parse(TextBox4.Text);
        float b = float.Parse(TextBox7.Text);
        TextBox9.Text = (a * b).ToString();
      
    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {
       


       
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tot = tot + float.Parse(e.Row.Cells[4].Text);
            TextBox10.Text = tot.ToString();
            TextBox11.Text = tot.ToString();
        }

    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

       
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con11 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
        con11.Open();
        string query1 = "Select *  from Product_stock where barcode='" + TextBox5.Text + "' ";
        SqlCommand cmd11 = new SqlCommand(query1, con11);
        SqlDataReader dr1 = cmd11.ExecuteReader();
        if (dr1.Read())
        {

            TextBox6.Text = dr1["product_name"].ToString();
            TextBox4.Text = dr1["mrp"].ToString();
        }
        TextBox6.Focus();

    }
}