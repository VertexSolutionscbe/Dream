using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;


public partial class Admin_Invoice_report_show : System.Web.UI.Page
{
    string name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
      
        name = Session["name"].ToString();
      
       ReportDocument rprt = new ReportDocument();  
        rprt.Load(Server.MapPath("CrystalReport.rpt"));
        DataSet1TableAdapters.DataTable1TableAdapter ta = new DataSet1TableAdapters.DataTable1TableAdapter();
        DataSet1.DataTable1DataTable table = ta.GetData(name);
        rprt.SetDataSource(table.DefaultView);
        CrystalReportViewer1.ReportSource = rprt;
        CrystalReportViewer1.DataBind();  
       
    }
}