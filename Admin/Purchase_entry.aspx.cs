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

public partial class Admin_Purchase_entry : System.Web.UI.Page
{
    float tot = 0;
    float tot1 = 0;
    DataTable dt = null;
    int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           
            getinvoiceno();
            show_category();
            showrating();
            BindData();
            show_tax();
            active();
            created();
            show_supplier();

           

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

        Gridview1.DataSource = dt;
        Gridview1.DataBind();
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
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                 
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox5");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox6");
                 
                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("TextBox16");
                    TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("TextBox17");
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("TextBox18");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("TextBox19");
                   
                

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

                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();
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
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                  
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox5");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox6");
                  
                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("TextBox16");
                    TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("TextBox17");
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("TextBox18");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("TextBox19");
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

    //A method that Inserts the records to the database

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = @"Data Source=BESTSHOPPEE1-PC\SQLEXPRESS;Initial Catalog=Dream;User ID=sa;Password=vertex123";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select product_name from product_entry where " +
                "product_name like @product_name + '%'";
                cmd.Parameters.AddWithValue("@product_name", prefixText);
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
    protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
    {



    }
    protected void Gridview1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {






    }
    protected void Gridview1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

        

      

       
    }
    protected void Gridview1_SelectedIndexChanged(object sender, System.EventArgs e)
    {


    }
    protected void Gridview1_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
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
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                    con.Open();





                    SqlCommand cmd = new SqlCommand("select * from product_entry where product_name='" + box1.Text + "'", con);
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        box2.Text = dr["code"].ToString();
                      
                    }
                    con.Close();
                   
                    rowIndex++;
                    box3.Focus();
                }

            }
        }

    }
    protected void Gridview1_Load(object sender, System.EventArgs e)
    {

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
   
   
    
    protected void Button1_Click(object sender, EventArgs e)
    {


        

        if (Session["company_id"] != null)
        {
            company_id = Convert.ToInt32(Session["company_id"].ToString());
        }
        string status = "Purchase";
        float value = 0;
        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("insert into purchase_entry values(@purchase_invoice,@date,@Supplier,@Toal_qty,@total_amount,@Grand__total,@Com_Id,@paid_amount,@pending_amount,@status,@value)", CON);
        cmd.Parameters.AddWithValue("@purchase_invoice", Label1.Text);
        cmd.Parameters.AddWithValue("@date", TextBox8.Text);
        cmd.Parameters.AddWithValue("@Supplier",DropDownList3.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Toal_qty", TextBox4.Text);
        cmd.Parameters.AddWithValue("@total_amount", TextBox10.Text);
        cmd.Parameters.AddWithValue("@Grand__total", TextBox11.Text);
        cmd.Parameters.AddWithValue("@Com_Id", company_id);
        cmd.Parameters.AddWithValue("@paid_amount", TextBox7.Text);
        cmd.Parameters.AddWithValue("@pending_amount", TextBox9.Text);
        cmd.Parameters.AddWithValue("@status", status);
        cmd.Parameters.AddWithValue("@value", value);
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();


        int a111 = 0;
        float b11 = 0;
        float f11 = 0;
        float c11 = 0;
        SqlConnection con100 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
        SqlCommand cmd100 = new SqlCommand("SELECT * FROM pay_amount_status WHERE Buyer = @Buyer", con100);
        cmd100.Parameters.AddWithValue("@Buyer", DropDownList3.SelectedItem.Text);
        con100.Open();
        SqlDataReader reader1 = cmd100.ExecuteReader();
        if (reader1.HasRows)
        {
            SqlConnection con11 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
            SqlCommand cmd11 = new SqlCommand("Select * from pay_amount_status where Buyer='" + DropDownList3.SelectedItem.Text + "'", con11);
            con11.Open();
            SqlDataReader dr11;
            dr11 = cmd11.ExecuteReader();
            if (dr11.Read())
            {

                b11 = float.Parse(dr11["pending_amount"].ToString());


                f11 = float.Parse(TextBox9.Text);

                c11 = (b11 + f11);






                SqlConnection con24 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                SqlCommand cmd24 = new SqlCommand("insert into pay_amount values(@Buyer,@Pay_date,@Estimate_value,@address,@total_amount,@pay_amount,@pending_amount,@outstanding,@invoice_no)", con24);
                cmd24.Parameters.AddWithValue("@Buyer", DropDownList3.SelectedItem.Text);
                cmd24.Parameters.AddWithValue("@pay_date", TextBox8.Text);
                cmd24.Parameters.AddWithValue("@Estimate_value", TextBox11.Text);
                cmd24.Parameters.AddWithValue("@address", TextBox12.Text);

                cmd24.Parameters.AddWithValue("@total_amount", string.Format("{0:0.00}", TextBox11.Text));
                cmd24.Parameters.AddWithValue("@pay_amount", TextBox7.Text);
                cmd24.Parameters.AddWithValue("@pending_amount", string.Format("{0:0.00}", TextBox9.Text));
                cmd24.Parameters.AddWithValue("@outstanding", string.Format("{0:0.00}", c11));
              
                cmd24.Parameters.AddWithValue("@invoice_no", Label1.Text);

                con24.Open();
                cmd24.ExecuteNonQuery();
                con24.Close();


                SqlConnection con23 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                SqlCommand cmd23 = new SqlCommand("update pay_amount_status set address=@address,total_amount=total_amount+@total_amount,pending_amount=pending_amount+@pending_amount where Buyer='" + DropDownList3.SelectedItem.Text + "' ", con23);

                cmd23.Parameters.AddWithValue("@address", TextBox12.Text);

                cmd23.Parameters.AddWithValue("@total_amount", string.Format("{0:0.00}", TextBox11.Text));

                cmd23.Parameters.AddWithValue("@pending_amount", string.Format("{0:0.00}", TextBox9.Text));

                con23.Open();
                cmd23.ExecuteNonQuery();
                con23.Close();


            }

            con11.Close();






        }
        else
        {


            SqlConnection con23 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
            SqlCommand cmd23 = new SqlCommand("insert into pay_amount_status values(@Buyer,@address,@total_amount,@pending_amount,@paid_amount)", con23);
            cmd23.Parameters.AddWithValue("@Buyer", DropDownList3.SelectedItem.Text);
            cmd23.Parameters.AddWithValue("@address", TextBox12.Text);

            cmd23.Parameters.AddWithValue("@total_amount", string.Format("{0:0.00}", TextBox11.Text));

            cmd23.Parameters.AddWithValue("@pending_amount", string.Format("{0:0.00}", TextBox9.Text));
            cmd23.Parameters.AddWithValue("@paid_amount", TextBox7.Text);
            con23.Open();
            cmd23.ExecuteNonQuery();
            con23.Close();
            string return_by = "";
            int value1 = 0;
            SqlConnection con24 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
            SqlCommand cmd24 = new SqlCommand("insert into pay_amount values(@Buyer,@Pay_date,@Estimate_value,@address,@total_amount,@pay_amount,@pending_amount,@outstanding,@invoice_no)", con24);
            cmd24.Parameters.AddWithValue("@Buyer", DropDownList3.SelectedItem.Text);
            cmd24.Parameters.AddWithValue("@pay_date", TextBox8.Text);
            cmd24.Parameters.AddWithValue("@Estimate_value", TextBox11.Text);
            cmd24.Parameters.AddWithValue("@address", TextBox12.Text);

            cmd24.Parameters.AddWithValue("@total_amount", string.Format("{0:0.00}", TextBox11.Text));
            cmd24.Parameters.AddWithValue("@pay_amount", TextBox7.Text);
            cmd24.Parameters.AddWithValue("@pending_amount", string.Format("{0:0.00}", TextBox9.Text));
            cmd24.Parameters.AddWithValue("@outstanding", string.Format("{0:0.00}", TextBox9.Text));
            cmd24.Parameters.AddWithValue("@invoice_no", Label1.Text);
          
            con24.Open();
            cmd24.ExecuteNonQuery();
            con24.Close();


        }
        con100.Close();


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
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");

                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox5");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox6");

                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("TextBox16");
                    TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("TextBox17");
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("TextBox18");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("TextBox19");


                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                    con.Open();

                    SqlCommand cmd2 = new SqlCommand("select * from product_entry where product_name='" + box1.Text + "'", con);
                    SqlDataReader dr1;
                    dr1 = cmd2.ExecuteReader();
                    if (dr1.Read())
                    {

                        int cat_id =Convert.ToInt32(  dr1["category_id"].ToString());
                        int sub_id = Convert.ToInt32(dr1["subcategory_id"].ToString());

                    SqlConnection CON1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("insert into purchase_entry_details values(@Category,@subcategory,@purchase_invoice,@Product_code,@Product_name,@barcode,@mrp,@Purchase_price,@qty,@tax,@tax_amount,@total_amount,@Com_Id,@date,@Supplier)", CON1);
                    cmd1.Parameters.AddWithValue("@Category", cat_id);
                    cmd1.Parameters.AddWithValue("@subcategory", sub_id);
                        cmd1.Parameters.AddWithValue("@purchase_invoice", Label1.Text);
                    cmd1.Parameters.AddWithValue("@Product_code",box2.Text);
                    cmd1.Parameters.AddWithValue("@Product_name", box1.Text);
                    cmd1.Parameters.AddWithValue("@barcode", box3.Text);

                    cmd1.Parameters.AddWithValue("@mrp", box4.Text);
                    cmd1.Parameters.AddWithValue("@Purchase_price", box5.Text);
                   

                    cmd1.Parameters.AddWithValue("@qty", box6.Text);
                    cmd1.Parameters.AddWithValue("@tax", box7.Text);
                    cmd1.Parameters.AddWithValue("@tax_amount", box8.Text);
                    cmd1.Parameters.AddWithValue("@total_amount", box9.Text);
                    cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                    cmd1.Parameters.AddWithValue("@date", TextBox8.Text);
                    cmd1.Parameters.AddWithValue("@Supplier", DropDownList3.SelectedItem.Text);
                    CON1.Open();
                    cmd1.ExecuteNonQuery();
                    CON1.Close();


                    SqlConnection CON11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd11 = new SqlCommand("insert into product_stock values(@Category,@subcategory,@purchase_invoice,@Product_code,@Product_name,@barcode,@mrp,@Purchase_price,@qty,@tax,@tax_amount,@total_amount,@Com_Id,@date,@Supplier)", CON11);
                    cmd11.Parameters.AddWithValue("@Category", cat_id);
                    cmd11.Parameters.AddWithValue("@subcategory", sub_id);
                    cmd11.Parameters.AddWithValue("@purchase_invoice", Label1.Text);
                    cmd11.Parameters.AddWithValue("@Product_code", box2.Text);
                    cmd11.Parameters.AddWithValue("@Product_name", box1.Text);
                    cmd11.Parameters.AddWithValue("@barcode", box3.Text);

                    cmd11.Parameters.AddWithValue("@mrp", box4.Text);
                    cmd11.Parameters.AddWithValue("@Purchase_price", box5.Text);


                    cmd11.Parameters.AddWithValue("@qty", box6.Text);
                    cmd11.Parameters.AddWithValue("@tax", box7.Text);
                    cmd11.Parameters.AddWithValue("@tax_amount", box8.Text);
                    cmd11.Parameters.AddWithValue("@total_amount", box9.Text);
                    cmd11.Parameters.AddWithValue("@Com_Id", company_id);
                    cmd11.Parameters.AddWithValue("@date", TextBox8.Text);
                    cmd11.Parameters.AddWithValue("@Supplier", DropDownList3.SelectedItem.Text);
                    CON11.Open();
                    cmd11.ExecuteNonQuery();
                    CON11.Close();



                }
                    con.Close();


                    rowIndex++;
                }

            }
        }




       
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Purchase entry created successfully')", true);
        BindData();
        show_category();
    
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox7.Text = "";
        TextBox9.Text = "";
        TextBox12.Text = "";
        SetInitialRow();
        TextBox8.Text="";
        show_supplier();
        TextBox4.Text="";
        show_tax();
       

    }
    private void SaveDetail(GridViewRow row)
    {
        if (Session["company_id"] != null)
        {
            company_id = Convert.ToInt32(Session["company_id"].ToString());
        }


       



        SqlConnection con91 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
        SqlCommand check_User_Name91 = new SqlCommand("SELECT * FROM product_stock WHERE Product_code = @Product_code and barcode=@barcode", con91);
        check_User_Name91.Parameters.AddWithValue("@Product_code", row.Cells[0].Text);
        check_User_Name91.Parameters.AddWithValue("@barcode", row.Cells[2].Text);
           con91.Open();
                SqlDataReader reader91 = check_User_Name91.ExecuteReader();
                if (reader91.HasRows)
                {
                    if (Session["company_id"] != null)
                    {
                        company_id = Convert.ToInt32(Session["company_id"].ToString());
                    }

                    SqlConnection CON1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("update product_stock set @Product_name=@Product_name,barcode=@barcode,mrp=@mrp,Purchase_price=@Purchase_price,qty=qty+@qty,supplier=@supplier,size=@size where Product_code='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "'", CON1);
                 
                
                    cmd1.Parameters.AddWithValue("@Product_name", row.Cells[1].Text);
                    cmd1.Parameters.AddWithValue("@barcode", row.Cells[2].Text);

                    cmd1.Parameters.AddWithValue("@mrp", row.Cells[3].Text);
                    cmd1.Parameters.AddWithValue("@Purchase_price", row.Cells[4].Text);


                    cmd1.Parameters.AddWithValue("@qty", row.Cells[6].Text);
                    cmd1.Parameters.AddWithValue("@supplier", row.Cells[5].Text);
                    cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                   
                    CON1.Open();
                    cmd1.ExecuteNonQuery();
                    CON1.Close();
                }
                else
                {
                    if (Session["company_id"] != null)
                    {
                        company_id = Convert.ToInt32(Session["company_id"].ToString());
                    }
                    SqlConnection CON1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("insert into product_stock values(@Product_code,@Product_name,@barcode,@mrp,@Purchase_price,@qty,@supplier,@Com_Id,@size)", CON1);
                 
                    cmd1.Parameters.AddWithValue("@Product_code", row.Cells[0].Text);
                    cmd1.Parameters.AddWithValue("@Product_name", row.Cells[1].Text);
                    cmd1.Parameters.AddWithValue("@barcode", row.Cells[2].Text);

                    cmd1.Parameters.AddWithValue("@mrp", row.Cells[3].Text);
                    cmd1.Parameters.AddWithValue("@Purchase_price", row.Cells[4].Text);


                    cmd1.Parameters.AddWithValue("@qty", row.Cells[6].Text);
                    cmd1.Parameters.AddWithValue("@supplier", row.Cells[5].Text);
                    cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                   
                    CON1.Open();
                    cmd1.ExecuteNonQuery();
                    CON1.Close();
                }
                con91.Close();


    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        BindData();
        show_category();

        TextBox10.Text = "";
        TextBox11.Text = "";
        SetInitialRow();
        TextBox8.Text = "";
        show_supplier();
        TextBox4.Text="";
        TextBox7.Text = "";
        TextBox9.Text = "";
        TextBox12.Text = "";
        show_tax();
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
        if (Session["company_id"] != null)
        {
            company_id = Convert.ToInt32(Session["company_id"].ToString());
        }

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("delete from product_entry where code='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Product entry deleted successfully')", true);

        BindData();
        show_category();



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
                cmd.CommandText = "select product_name from product_entry where " +
                "product_name like @product_name + '%'";
                cmd.Parameters.AddWithValue("@product_name", prefixText);
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
    private void show_supplier()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Vendor ORDER BY Vendor_Code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "Vendor_Name";
        DropDownList3.DataValueField = "Vendor_Code";
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
   
   

   
   
    
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
        
       
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("select * from Vendor where Vendor_Name='" + DropDownList3.SelectedItem.Text + "'", con);
        SqlDataReader dr;
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            TextBox12.Text = dr["Vendor_Address"].ToString();

        }
        con.Close();
      
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
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox6");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("TextBox16");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("TextBox19");
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("TextBox17");
                    int a =Convert.ToInt32( box1.Text) *Convert.ToInt32( box2.Text);
                    box3.Text = a.ToString();

                    rowIndex++;
                    box4.Focus();
                }

            }
        }

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
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("TextBox19");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("TextBox17");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("TextBox18");

                    float tax = float.Parse(box2.Text);
                    float total = float.Parse(box1.Text);
                    box3.Text = string.Format("{0:0.00}", (total * tax / 100)).ToString();
                    float A = float.Parse(box3.Text);
                    box1.Text = string.Format("{0:0.00}", (A + total)).ToString();

                    rowIndex++;
                    box3.Focus();
                }

            }
        }
    }
   
    protected void TextBox19_TextChanged(object sender, System.EventArgs e)
    {
        AddNewRowToGrid();
    }

    protected void Button3_Click(object sender, System.EventArgs e)
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
                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("TextBox16");
                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("TextBox19");
                        tot1 = tot1 + float.Parse(box0.Text);
                        TextBox4.Text = tot1.ToString();
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
    protected void TextBox2_TextChanged(object sender, System.EventArgs e)
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
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                    
                  

                    rowIndex++;
                    box1.Focus();
                }

            }
        }
    }
    protected void TextBox3_TextChanged(object sender, System.EventArgs e)
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
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox5");



                    rowIndex++;
                    box1.Focus();
                }

            }
        }
    }
    protected void TextBox5_TextChanged(object sender, System.EventArgs e)
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
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox6");



                    rowIndex++;
                    box1.Focus();
                }

            }
        }
    }
    protected void TextBox6_TextChanged(object sender, System.EventArgs e)
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
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("TextBox16");



                    rowIndex++;
                    box1.Focus();
                }

            }
        }
    }
    protected void TextBox18_TextChanged(object sender, System.EventArgs e)
    {
        AddNewRowToGrid();
    }
}