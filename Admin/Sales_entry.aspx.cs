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
using System.Drawing;
#endregion

public partial class Admin_Sales_entry : System.Web.UI.Page
{
    float tot = 0;
    
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            TextBox5.Attributes.Add("onkeypress", "return controlEnter('" + TextBox6.ClientID + "', event)");
            TextBox6.Attributes.Add("onkeypress", "return controlEnter('" + TextBox4.ClientID + "', event)");

          
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

                    dataTable.Columns.Add("Product Code");
                    dataTable.Columns.Add("Product Name");
                    dataTable.Columns.Add("Product code / Barcode");
                    dataTable.Columns.Add("MRP");
                    dataTable.Columns.Add("Purchase Price");
                    dataTable.Columns.Add("Supplier Name");
                    dataTable.Columns.Add("Quantity");
                    dataTable.Columns.Add("Tax Percentage");
                    dataTable.Columns.Add("Tax Amount");
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
        string query = "Select max(convert(int,SubString(purchase_invoice,PATINDEX('%[0-9]%',purchase_invoice),Len(purchase_invoice)))) from purchase_entry";
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton IMG = (ImageButton)sender;
        GridViewRow ROW = (GridViewRow)IMG.NamingContainer;
        Label29.Text = ROW.Cells[1].Text;
        TextBox16.Text = ROW.Cells[2].Text;

        this.ModalPopupExtender3.Show();
    }
    protected void Button16_Click(object sender, EventArgs e)
    {
        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("update product_entry set product_name='" + HttpUtility.HtmlDecode(TextBox16.Text) + "' where code='" + Label29.Text + "' ", CON);

        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();
        Label31.Text = "updated successfuly";

        this.ModalPopupExtender3.Hide();
        BindData();



    }
    protected void Button17_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd1 = new SqlCommand("delete from product_entry where code='" + Label29.Text + "' ", con1);
        con1.Open();
        cmd1.ExecuteNonQuery();
        con1.Close();


        Label31.Text = "Deleted successfuly";

        this.ModalPopupExtender3.Hide();
        BindData();


    }
    protected void Button14_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            //Finiding checkbox control in gridview for particular row
            CheckBox chkdelete = (CheckBox)gvrow.FindControl("CheckBox3");
            //Condition to check checkbox selected or not
            if (chkdelete.Checked)
            {
                //Getting UserId of particular row using datakey value
                int usrid = Convert.ToInt32(gvrow.Cells[1].Text);
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                con.Open();
                SqlCommand cmd = new SqlCommand("delete from product_entry where code=" + usrid, con);
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        BindData();


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
        SqlCommand cmd = new SqlCommand("insert into purchase_entry values(@purchase_invoice,@date,@Toal_qty,@total_amount,@Grand__total)", CON);
        cmd.Parameters.AddWithValue("@purchase_invoice", Label1.Text);
        cmd.Parameters.AddWithValue("@date", TextBox8.Text);
        cmd.Parameters.AddWithValue("@Toal_qty", TextBox10.Text);
        cmd.Parameters.AddWithValue("@total_amount", TextBox10.Text);
        cmd.Parameters.AddWithValue("@Grand__total", TextBox11.Text);
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();








        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Purchase entry created successfully')", true);
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
       

    }
    private void SaveDetail(GridViewRow row)
    {



        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("insert into purchase_entry_details values(@purchase_invoice,@Product_code,@Product_name,@barcode,@mrp,@Purchase_price,@supplier_name,@qty,@tax,@tax_amount,@total_amount)", CON);
        cmd.Parameters.AddWithValue("@purchase_invoice", Label1.Text);
        cmd.Parameters.AddWithValue("@Product_code", row.Cells[0].Text);
        cmd.Parameters.AddWithValue("@Product_name", row.Cells[1].Text);
        cmd.Parameters.AddWithValue("@barcode", row.Cells[2].Text);

        cmd.Parameters.AddWithValue("@mrp", row.Cells[3].Text);
        cmd.Parameters.AddWithValue("@Purchase_price", row.Cells[4].Text);
        cmd.Parameters.AddWithValue("@supplier_name", row.Cells[5].Text);

        cmd.Parameters.AddWithValue("@qty", row.Cells[6].Text);
        cmd.Parameters.AddWithValue("@tax", row.Cells[7].Text);
        cmd.Parameters.AddWithValue("@tax_amount", row.Cells[8].Text);
        cmd.Parameters.AddWithValue("@total_amount", row.Cells[9].Text);
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();

        SqlConnection con91 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
        SqlCommand check_User_Name91 = new SqlCommand("SELECT * FROM product_stock WHERE Product_code = @Product_code and barcode=@barcode", con91);
        check_User_Name91.Parameters.AddWithValue("@Product_code", row.Cells[0].Text);
        check_User_Name91.Parameters.AddWithValue("@barcode", row.Cells[2].Text);
        con91.Open();
        SqlDataReader reader91 = check_User_Name91.ExecuteReader();
        if (reader91.HasRows)
        {

            SqlConnection CON1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("update product_stock set @Product_name=@Product_name,barcode=@barcode,mrp=@mrp,Purchase_price=@Purchase_price,qty=@qty,supplier=@supplier where Product_code='" + row.Cells[0].Text + "')", CON1);


            cmd1.Parameters.AddWithValue("@Product_name", row.Cells[1].Text);
            cmd1.Parameters.AddWithValue("@barcode", row.Cells[2].Text);

            cmd1.Parameters.AddWithValue("@mrp", row.Cells[3].Text);
            cmd1.Parameters.AddWithValue("@Purchase_price", row.Cells[4].Text);


            cmd1.Parameters.AddWithValue("@qty", row.Cells[6].Text);
            cmd1.Parameters.AddWithValue("@supplier", row.Cells[5].Text);
            CON1.Open();
            cmd1.ExecuteNonQuery();
            CON1.Close();
        }
        else
        {
            SqlConnection CON1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("insert into product_stock values(@Product_code,@Product_name,@barcode,@mrp,@Purchase_price,@qty,@supplier)", CON1);

            cmd1.Parameters.AddWithValue("@Product_code", row.Cells[0].Text);
            cmd1.Parameters.AddWithValue("@Product_name", row.Cells[1].Text);
            cmd1.Parameters.AddWithValue("@barcode", row.Cells[2].Text);

            cmd1.Parameters.AddWithValue("@mrp", row.Cells[3].Text);
            cmd1.Parameters.AddWithValue("@Purchase_price", row.Cells[4].Text);


            cmd1.Parameters.AddWithValue("@qty", row.Cells[6].Text);
            cmd1.Parameters.AddWithValue("@supplier", row.Cells[5].Text);
            CON1.Open();
            cmd1.ExecuteNonQuery();
            CON1.Close();
        }
        con91.Close();


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
    private void show_tax()
    {
        
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



        string stro = Label2.Text;
        string str1 = TextBox6.Text;
        string str2 = TextBox5.Text;

        string str3 = TextBox4.Text;
      
        string str6 = TextBox7.Text;
       
        string str7 = TextBox9.Text;
        dt = (DataTable)ViewState["Details"];
        dt.Rows.Add(stro, str1, str2, str3, str6, str7);
        ViewState["Details"] = dt;
        GridView2.DataSource = dt;

        GridView2.EmptyDataText = "Product Code";
        GridView2.EmptyDataText = "Product Name";

        GridView2.EmptyDataText = "Product code / Barcode";

        GridView2.EmptyDataText = "MRP";
        GridView2.EmptyDataText = "Purchase price";
        GridView2.EmptyDataText = "Supplier Name";
        GridView2.EmptyDataText = "Quantity";
        GridView2.EmptyDataText = "Tax Percentage";
        GridView2.EmptyDataText = "Tax Amount";
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
            tot = tot + float.Parse(e.Row.Cells[9].Text);
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