
#region " Using "
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Text;
using System.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;

using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
#endregion






public partial class Admin_Sales_entry : System.Web.UI.Page
{
    float tot = 0;
    float tot1 = 0;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            TextBox8.Attributes.Add("onkeypress", "return controlEnter('" + TextBox13.ClientID + "', event)");
            TextBox13.Attributes.Add("onkeypress", "return controlEnter('" + TextBox14.ClientID + "', event)");
            TextBox14.Attributes.Add("onkeypress", "return controlEnter('" + DropDownList3.ClientID + "', event)");
          
           
            getinvoiceno();
            show_category();
            showrating();
            BindData();
            show_tax();
            active();
            created();
           

           

        }
        if (!Page.IsPostBack)
        {


            SetInitialRow();
        }
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dt.Columns.Add(new DataColumn("Column7", typeof(string)));
        dt.Columns.Add(new DataColumn("Column8", typeof(string)));
        dt.Columns.Add(new DataColumn("Column9", typeof(string)));


        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dr["Column7"] = string.Empty;
        dr["Column8"] = string.Empty;
        dr["Column9"] = string.Empty;


        dt.Rows.Add(dr);
        //dr = dt.NewRow();

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        Gridview2.DataSource = dt;
        Gridview2.DataBind();
    }

    private void AddNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview2.Rows[rowIndex].Cells[3].FindControl("TextBox5");

                    TextBox box4 = (TextBox)Gridview2.Rows[rowIndex].Cells[4].FindControl("TextBox3");
                    TextBox box5 = (TextBox)Gridview2.Rows[rowIndex].Cells[5].FindControl("TextBox4");
                
                    TextBox box6 = (TextBox)Gridview2.Rows[rowIndex].Cells[6].FindControl("TextBox16");
                    TextBox box7 = (TextBox)Gridview2.Rows[rowIndex].Cells[7].FindControl("TextBox17");
                    TextBox box8 = (TextBox)Gridview2.Rows[rowIndex].Cells[8].FindControl("TextBox18");
                    TextBox box9 = (TextBox)Gridview2.Rows[rowIndex].Cells[9].FindControl("TextBox19");



                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    drCurrentRow["Column1"] = box1.Text;
                    drCurrentRow["Column2"] = box2.Text;
                    drCurrentRow["Column3"] = box3.Text;
                    drCurrentRow["Column4"] = box4.Text;
                    drCurrentRow["Column5"] = box5.Text;
                    drCurrentRow["Column6"] = box6.Text;
                    drCurrentRow["Column7"] = box7.Text;
                    drCurrentRow["Column8"] = box8.Text;
                    drCurrentRow["Column9"] = box9.Text;


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                Gridview2.DataSource = dtCurrentTable;
                Gridview2.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview2.Rows[rowIndex].Cells[3].FindControl("TextBox5");

                    TextBox box4 = (TextBox)Gridview2.Rows[rowIndex].Cells[4].FindControl("TextBox3");
                    TextBox box5 = (TextBox)Gridview2.Rows[rowIndex].Cells[5].FindControl("TextBox4");

                    TextBox box6 = (TextBox)Gridview2.Rows[rowIndex].Cells[6].FindControl("TextBox16");
                    TextBox box7 = (TextBox)Gridview2.Rows[rowIndex].Cells[7].FindControl("TextBox17");
                    TextBox box8 = (TextBox)Gridview2.Rows[rowIndex].Cells[8].FindControl("TextBox18");
                    TextBox box9 = (TextBox)Gridview2.Rows[rowIndex].Cells[9].FindControl("TextBox19");
                    box1.Text = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                    box3.Text = dt.Rows[i]["Column3"].ToString();
                    box4.Text = dt.Rows[i]["Column4"].ToString();
                    box5.Text = dt.Rows[i]["Column5"].ToString();
                    box6.Text = dt.Rows[i]["Column6"].ToString();
                    box7.Text = dt.Rows[i]["Column7"].ToString();
                    box8.Text = dt.Rows[i]["Column8"].ToString();
                    box9.Text = dt.Rows[i]["Column9"].ToString();
                  
                                        rowIndex++;

                }
            }
            // ViewState["CurrentTable"] = dt;

        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();




    }

    //A method that returns a string which calls the connection string from the web.config
    private string GetConnectionString()
    {
        //"DBConnection" is the name of the Connection String
        //that was set up from the web.config file
        return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
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

        foreach (GridViewRow row in this.Gridview2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                this.SaveDetail(row);
            }

        }
        string ststus="Sales";
        float value=0;
        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("insert into sales_entry values(@invoice_no,@date,@customer_name,@customer_Address,@Mobile_no,@staff_namee,@total_qty,@total_amount,@grand_total,@paid_amount,@Pending_amount,@status,@value)", CON);
        cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
        cmd.Parameters.AddWithValue("@date", TextBox8.Text);
        cmd.Parameters.AddWithValue("@customer_name", TextBox13.Text);
        cmd.Parameters.AddWithValue("@customer_Address", TextBox14.Text);
        cmd.Parameters.AddWithValue("@staff_name", DropDownList3.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@total_qty", TextBox2.Text);
        cmd.Parameters.AddWithValue("@total_amount", TextBox10.Text);
        cmd.Parameters.AddWithValue("@grand_total", TextBox11.Text);
        cmd.Parameters.AddWithValue("@paid_amount",TextBox7.Text);
        cmd.Parameters.AddWithValue("@Pending_amount",TextBox9.Text);
          cmd.Parameters.AddWithValue("@status",ststus);
         cmd.Parameters.AddWithValue("@value",value);
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();








        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Sales entry created successfully')", true);
        BindData();
        show_category();
      
        TextBox10.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";
        TextBox11.Text = "";
        DataTable dataTable = new DataTable();
        dataTable = null;
   
        show_tax();
        Session["Name"] = Label1.Text;




       Response.Redirect("SALES_REPORT_VIEW.aspx");
     
    }
    private void SaveDetail(GridViewRow row)
    {



        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("insert into sales_entry_details values(@invoice_no,@barcode,@Product_code,@product_name,@mrp,@qty,@total_amount,@size)", CON);
        cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
        cmd.Parameters.AddWithValue("@barcode", row.Cells[0].Text);
        cmd.Parameters.AddWithValue("@Product_code", row.Cells[1].Text);
        cmd.Parameters.AddWithValue("@product_name", row.Cells[1].Text);

        cmd.Parameters.AddWithValue("@mrp", row.Cells[3].Text);
        cmd.Parameters.AddWithValue("@qty", row.Cells[4].Text);
        cmd.Parameters.AddWithValue("@total_amount", row.Cells[5].Text);
        cmd.Parameters.AddWithValue("@size", row.Cells[2].Text);
      
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();

        
            SqlConnection CON1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("update product_stock set qty=qty-@qty where barcode='" + row.Cells[0].Text + "'", CON1);


       


            cmd1.Parameters.AddWithValue("@qty", row.Cells[4].Text);
          
            CON1.Open();
            cmd1.ExecuteNonQuery();
            CON1.Close();
        
       


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      
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
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers1(string prefixText, int count)
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
       
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }

    

    protected void Button3_Click(object sender, EventArgs e)
    {



       




     





       





    }
    
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {
       


       
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tot = tot + float.Parse(e.Row.Cells[5].Text);
            TextBox10.Text = tot.ToString();
            TextBox11.Text = tot.ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tot1 = tot1 + float.Parse(e.Row.Cells[4].Text);
            TextBox2.Text = tot1.ToString();
            
        }
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

       
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["Name"] = TextBox13.Text;




        Response.Redirect("SALES_REPORT_VIEW.aspx");
    }
    protected void Gridview2_Load(object sender, System.EventArgs e)
    {

    }


    protected void TextBox1_TextChanged(object sender, System.EventArgs e)
    {
        int rowIndex = 0;
        StringCollection sc = new StringCollection();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview2.Rows[rowIndex].Cells[3].FindControl("TextBox5");
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                  
                    con.Open();





                    SqlCommand cmd = new SqlCommand("select * from product_stock where barcode='" + box1.Text + "'", con);
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        box2.Text = dr["Product_name"].ToString();
                    
                        float Mop = float.Parse(dr["mrp"].ToString());
                        float tax = 105;
                        float A = float.Parse(string.Format("{0:0.00}", (Mop / tax)).ToString());
                        float a1 = float.Parse(string.Format("{0:0.00}", (A * 100)).ToString());
                        box3.Text = a1.ToString();
                    }
                    con.Close();

                    rowIndex++;
                    box2.Focus();
                }

            }
        }
    }
    protected void Gridview2_PreRender(object sender, System.EventArgs e)
    {

    }
    protected void TextBox16_TextChanged(object sender, System.EventArgs e)
    {
        int rowIndex = 0;
        StringCollection sc = new StringCollection();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[5].FindControl("TextBox5");
                    TextBox box2 = (TextBox)Gridview2.Rows[rowIndex].Cells[6].FindControl("TextBox16");
                    TextBox box3 = (TextBox)Gridview2.Rows[rowIndex].Cells[9].FindControl("TextBox19");
                    TextBox box4 = (TextBox)Gridview2.Rows[rowIndex].Cells[7].FindControl("TextBox17");

                    int a = Convert.ToInt32(box1.Text) * Convert.ToInt32(box2.Text);
                    box3.Text = a.ToString();

                    rowIndex++;
                    box4.Focus();
                }

            }
        }
    }
    protected void TextBox2_TextChanged(object sender, System.EventArgs e)
    {
       
    }
    protected void TextBox17_TextChanged(object sender, System.EventArgs e)
    {
        int rowIndex = 0;
        StringCollection sc = new StringCollection();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[9].FindControl("TextBox19");
                    TextBox box2 = (TextBox)Gridview2.Rows[rowIndex].Cells[7].FindControl("TextBox17");
                    TextBox box3 = (TextBox)Gridview2.Rows[rowIndex].Cells[8].FindControl("TextBox18");

                    float tax = float.Parse(box2.Text);
                    float total = float.Parse(box1.Text);
                    box3.Text = string.Format("{0:0.00}", (total * tax / 100)).ToString();
                    float A = float.Parse(box3.Text);
                    box1.Text = string.Format("{0:0.00}", (total - A)).ToString();

                    rowIndex++;

                }

            }
        }
    }
    protected void Button3_Click1(object sender, System.EventArgs e)
    {
        try
        {
            int rowIndex = 0;
            StringCollection sc = new StringCollection();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box0 = (TextBox)Gridview2.Rows[rowIndex].Cells[6].FindControl("TextBox16");
                        TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[9].FindControl("TextBox19");
                        tot1 = tot1 + float.Parse(box0.Text);
                        TextBox2.Text = tot1.ToString();
                        tot = tot + float.Parse(box1.Text);

                        TextBox10.Text = tot.ToString();
                        TextBox11.Text = tot.ToString();






                        rowIndex++;
                    }

                }
            }
        }
        catch (Exception er)
        { }
    }

    protected void TextBox7_TextChanged(object sender, System.EventArgs e)
    {
        try
        {

            float value1 = float.Parse(TextBox11.Text);
            float value2 = float.Parse(TextBox7.Text);
            float total = value1 - value2;
            TextBox9.Text = total.ToString();
        }
        catch (Exception er)
        { }
    }
}