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
#endregion

public partial class Taskadd : System.Web.UI.Page
{
     string value="";
    int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TYPE();

            Priority();

            task_status();
            company_id = Convert.ToInt32(Session["company_id"].ToString());
            if (Session["name1"] != null)
            {
                value = Session["name1"].ToString();
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cd = new SqlCommand("select * from task_entry where Task='" + value + "'", con);
                con.Open();
                SqlDataReader dr;
                dr = cd.ExecuteReader();
                if (dr.Read())
                {
                    TextBox2.Text = dr["Task"].ToString();
                    DropDownList2.Text = dr["type"].ToString();
                   
                    DropDownList5.Text = dr["status"].ToString();
                    DropDownList1.Text = dr["Priority"].ToString();
                    TextBox5.Text = dr["due_date"].ToString();
                    DropDownList4.SelectedItem.Text = dr["due_time"].ToString();
                    TextBox1.Text = dr["summary"].ToString();
                    DropDownList5.SelectedItem.Text = dr["status"].ToString();
                    DropDownList2.SelectedItem.Text = dr["type"].ToString();
                }
            }
            SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("Select * from User_details where com_id='" + company_id + "'", con2);
            con2.Open();
            DataSet ds1 = new DataSet();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(ds1);


            DropDownList3.DataSource = ds1;
            DropDownList3.DataTextField = "Name";
            DropDownList3.DataValueField = "user_id";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("All", "0"));


        }

        DateTime date = Convert.ToDateTime(DateTime.Today);
        TextBox5.Text = date.ToString();
    }
    private void task_status()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Accoun_Taskstatus where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList5.DataSource = ds;
        DropDownList5.DataTextField = "Accoun_Taskstatus";
        DropDownList5.DataValueField = "No";
        DropDownList5.DataBind();
        DropDownList5.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }
    private void Priority()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Lead_Priority where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Lead_Priority";
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
    private void TYPE()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Accoun_Tasktype where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Accoun_Tasktype";
        DropDownList2.DataValueField = "No";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
         string value1 = "";
        if (Session["name1"] != null)
        {
            value1 = Session["name1"].ToString();
        }
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd1 = new SqlCommand("select * from task_entry where task='" + value1 + "' and com_id='" + company_id + "'", con1);
        con1.Open();
        SqlDataReader dr1;
        dr1 = cmd1.ExecuteReader();
        if (dr1.HasRows)
        {
            DateTime date = Convert.ToDateTime(DateTime.Today);
            company_id = Convert.ToInt32(Session["company_id"].ToString());
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
            SqlCommand cmd = new SqlCommand("update task_entry set type=@type,assigned_to=@assigned_to,Priority=@Priority,due_date=@due_date,due_time=@due_time,summary=@summary,reminder=@reminder,com_id=@com_id,status=@status,created_date=@created_date,edit_date=@edit_date where Task='" + value1 + "' and com_id='" + company_id + "'", con);
         
            cmd.Parameters.AddWithValue("@type", DropDownList2.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@assigned_to", DropDownList3.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Priority", DropDownList1.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@due_date", Convert.ToDateTime(TextBox5.Text));
            cmd.Parameters.AddWithValue("@due_time", DropDownList4.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@summary", TextBox1.Text);
            if (CheckBox1.Checked == true)
            {

                cmd.Parameters.AddWithValue("@reminder", CheckBox1.Text);
            }
            else if (CheckBox2.Checked == true)
            {
                cmd.Parameters.AddWithValue("@reminder", CheckBox2.Text);
            }
            cmd.Parameters.AddWithValue("@com_id", company_id);
            cmd.Parameters.AddWithValue("@status", DropDownList5.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@created_date", Convert.ToDateTime(date));
            cmd.Parameters.AddWithValue("@edit_date", Convert.ToDateTime(date));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Task created successfully')", true);

        }
        else
        {



            DateTime date = Convert.ToDateTime(DateTime.Today);
            company_id = Convert.ToInt32(Session["company_id"].ToString());
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
            SqlCommand cmd = new SqlCommand("insert into task_entry values(@Task,@type,@assigned_to,@Priority,@due_date,@due_time,@summary,@reminder,@com_id,@status,@created_date,@edit_date)", con);
            cmd.Parameters.AddWithValue("@Task", TextBox2.Text);
            cmd.Parameters.AddWithValue("@type", DropDownList2.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@assigned_to", DropDownList3.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Priority", DropDownList1.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@due_date", Convert.ToDateTime(TextBox5.Text));
            cmd.Parameters.AddWithValue("@due_time", DropDownList4.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@summary", TextBox1.Text);
            if (CheckBox1.Checked == true)
            {

                cmd.Parameters.AddWithValue("@reminder", CheckBox1.Text);
            }
            else if (CheckBox2.Checked == true)
            {
                cmd.Parameters.AddWithValue("@reminder", CheckBox2.Text);
            }
            cmd.Parameters.AddWithValue("@com_id", company_id);
            cmd.Parameters.AddWithValue("@status", DropDownList5.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@created_date", Convert.ToDateTime(date));
            cmd.Parameters.AddWithValue("@edit_date", DBNull.Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Task created successfully')", true);
        }
           
    }
}