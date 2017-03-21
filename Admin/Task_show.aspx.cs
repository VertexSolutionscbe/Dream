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

public partial class Admin_Task_show : System.Web.UI.Page
{
    int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lead_status();

        }
        if (Session["name"] != null)
        { company_id = Convert.ToInt32(Session["company_id"].ToString());
            string name = Session["name"].ToString();
              SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cd = new SqlCommand("select * from task_entry where task='" + name + "' and com_id='" + company_id + "'", con);
            con.Open();
            SqlDataReader dr;
            dr = cd.ExecuteReader();
            if (dr.Read())
            {
                Label1.Text = Session["name"].ToString();
                Label2.Text = dr["assigned_to"].ToString();
                Label3.Text = dr["type"].ToString();
                Label5.Text = Convert.ToDateTime(dr["due_date"]).ToString("dd-MM-yyyy");
                Label6.Text = dr["Priority"].ToString();
                Label9.Text = dr["assigned_to"].ToString();
                Label11.Text = dr["due_time"].ToString();
                DropDownList1.SelectedItem.Text = dr["status"].ToString();
                Label4.Text = dr["status"].ToString();
                Label23.Text = dr["summary"].ToString();
                Label8.Text =Convert.ToDateTime( dr["edit_date"]).ToString("dd-MM-yyyy");
            }

        }

    }
    private void lead_status()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Accoun_Taskstatus where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Accoun_Taskstatus";
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
    protected void btnRandom_Click(object sender, EventArgs e)
    {

        Session["name1"] = Label1.Text;
        Response.Redirect("Taskadd.aspx");

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string value = Session["name"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cd = new SqlCommand("delete from task_entry where Task='" + Label1.Text + "'", con);
        con.Open();
        cd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product deleted sucessfully');window.location ='Task.aspx';", true);

    }
}