#region " Using "
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
#endregion


public partial class Admin_Invoice_details : System.Web.UI.Page
{
    int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            company_id = Convert.ToInt32(Session["company_id"].ToString());

            showcustomertype();
            showrating();
            BindData();
            DropDownList1.Items.Insert(0, new ListItem("All", "0"));
            active();
            created();

            company_id = Convert.ToInt32(Session["company_id"].ToString());

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd = new SqlCommand("Select * from User_details where com_id='" + company_id + "' and rolename='Admin'", con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            DropDownList1.DataSource = ds;
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "user_id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("All", "0"));

            con.Close();
        }

    }
    private void active()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Active where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList5.DataSource = ds;
        DropDownList5.DataTextField = "Active";
        DropDownList5.DataValueField = "No";
        DropDownList5.DataBind();
        DropDownList5.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
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
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Created where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "Created";
        DropDownList3.DataValueField = "No";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }
    protected void BindData()
    {

        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from invoice", con);
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        con.Close();
    }
    protected void LoginLink_OnClick(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/login.aspx");

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void btnRandom_Click(object sender, EventArgs e)
    {
        Session["name"] = "";
        Session["name1"] = "";
        Response.Redirect("~/Admin/Invoice.aspx");
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;
        Session["name"] = row.Cells[0].Text;
        Response.Redirect("Invoice.aspx");


    }


    #region " [ Button Event ] "
    protected void Button11_Click(object sender, EventArgs e)
    {

        Button Sample = sender as Button;
        GridViewRow row = Sample.NamingContainer as GridViewRow;
        DropDownList drp = row.FindControl("ddlFileFormat") as DropDownList;

        // select appropriate contenttype, while binary transfer it identifies filetype
        string contentType = string.Empty;
        if (drp.SelectedValue.Equals(".pdf"))
            contentType = "application/pdf";
        if (drp.SelectedValue.Equals(".doc"))
            contentType = "application/ms-word";
        if (drp.SelectedValue.Equals(".xls"))
            contentType = "application/xls";

        DataTable dsData = new DataTable();

        DataSet ds = null;
        SqlDataAdapter da = null;



        try
        {
            string constring = ConfigurationManager.AppSettings["connection"];
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Invoice_ss", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@No", int.Parse(row.Cells[0].Text));

                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    con.Open();
                    da.Fill(ds);
                    con.Close();

                }
            }
        }
        catch
        {
            throw;
        }



        dsData = ds.Tables[0];

        string FileName = "File_" + row.Cells[0].Text + drp.SelectedValue;
        string extension;
        string encoding;
        string mimeType;
        string[] streams;
        Warning[] warnings;

        LocalReport report = new LocalReport();
        report.ReportPath = Server.MapPath("Report.rdlc");
        ReportDataSource rds = new ReportDataSource();
        rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file
        rds.Value = dsData;
        report.DataSources.Add(rds);

        Byte[] mybytes = report.Render(drp.SelectedItem.Text, null,
                        out extension, out encoding,
                        out mimeType, out streams, out warnings); //for exporting to PDF
        using (FileStream fs = File.Create(Server.MapPath("~/img/") + FileName))
        {
            fs.Write(mybytes, 0, mybytes.Length);
        }

        Response.ClearHeaders();
        Response.ClearContent();
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = contentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
        Response.WriteFile(Server.MapPath("~/img/" + FileName));
        Response.Flush();
        Response.Close();
        Response.End();




    }
    #endregion
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton img = (ImageButton)sender;
        GridViewRow row1 = (GridViewRow)img.NamingContainer;

        SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd10 = new SqlCommand("delete from Invoice WHERE Invoice_no='" + row1.Cells[0].Text + "'", con10);
        con10.Open();
        cmd10.ExecuteNonQuery();
        con10.Close();











        SqlConnection con4 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd4 = new SqlCommand("delete from invoice_details where invoice_no='" + row1.Cells[0].Text + "'", con4);
        con4.Open();
        cmd4.ExecuteNonQuery();
        con4.Close();
        BindData();

    }
    private void showcustomertype()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Accountcustomer_type where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Accountcustomer_type";
        DropDownList2.DataValueField = "No";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }
    private void showrating()
    {
        company_id = Convert.ToInt32(Session["company_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Accoun_rating where com_id='" + company_id + "' ORDER BY No asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList6.DataSource = ds;
        DropDownList6.DataTextField = "Accoun_rating";
        DropDownList6.DataValueField = "No";
        DropDownList6.DataBind();
        DropDownList6.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
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
}