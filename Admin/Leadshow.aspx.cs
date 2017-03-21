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

public partial class Leadshow : System.Web.UI.Page
{
    int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showleadstatus();
            company_id = Convert.ToInt32(Session["company_id"].ToString());
            string value = Session["name"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
            SqlCommand cmd = new SqlCommand("select * from lead_entry where Lead_name='" + value + "' and com_id='" + company_id + "'", con);
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Label1.Text = dr["Lead_name"].ToString();
                Label2.Text = dr["Assigned_to"].ToString();
                Label3.Text = dr["Account_name"].ToString();
                Label4.Text = dr["email"].ToString();
                Label5.Text = dr["Phone"].ToString();
                Label18.Text = dr["address"].ToString();
                Label19.Text = dr["add_city"].ToString();
                Label20.Text = dr["add_state"].ToString();
                Label21.Text = dr["add_zip"].ToString();
                Label22.Text = dr["Country"].ToString();
                Label12.Text = dr["compaign"].ToString();
                Label13.Text = dr["lead_source"].ToString();
                Label14.Text = dr["Customer_type"].ToString();
                Label17.Text = dr["Assigned_to"].ToString();
                Label16.Text = dr["alter_phone"].ToString();
                Label17.Text = dr["Alter_email"].ToString();
                Label18.Text = dr["Share_with"].ToString();
                Label23.Text = dr["Summary"].ToString();
                Label9.Text = dr["Product"].ToString();
                Label8.Text = dr["Share_with"].ToString();
                Label7.Text = dr["Assigned_to"].ToString();
                DropDownList1.SelectedItem.Text = dr["Status"].ToString();
                DateTime created = Convert.ToDateTime(dr["created_date"].ToString());
                DateTime date = Convert.ToDateTime(DateTime.Today);

                int days = Convert.ToInt32((date - created).TotalDays);
                Label15.Text = days.ToString();
            }
        }

    }
    private void showleadstatus()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Lead_status where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Lead_status";
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
        Response.Redirect("leadsadd.aspx");

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        string value = Session["name"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cd = new SqlCommand("delete from lead_entry where Lead_name='" + Label1.Text + "' and com_id='" + company_id + "'", con);
        con.Open();
        cd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product deleted sucessfully');window.location ='leads.aspx';", true);

    }
}