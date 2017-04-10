<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sales_report.aspx.cs" Inherits="Admin_Sales_report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="480px" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="402px">
            <LocalReport ReportPath="Admin\Report2.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            TypeName="DataSet2TableAdapters.DataTable1TableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="TextBox1" Name="x" PropertyName="Text" 
                    Type="String" />
                <asp:ControlParameter ControlID="TextBox2" Name="y" PropertyName="Text" 
                    Type="Int32" />
                <asp:ControlParameter ControlID="TextBox2" Name="z" PropertyName="Text" 
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <asp:Button ID="Button1" runat="server" Text="Print" OnClientClick="printdiv()" 
        onclick="Button1_Click"/>
    <asp:Button ID="Button2" runat="server" Text="Back" onclick="Button2_Click" />
    <script>
    function printdiv() {
   //Code for adding HTML content to report viwer
    var headstr = "<html><head><title></title></head><body>";
    //End of body tag
    var footstr = "</body></html>";
    //This the main content to get the all the html content inside the report viewer control
    //"ReportViewer1_ctl10" is the main div inside the report viewer
    //controls who helds all the tables and divs where our report contents or data is available
    var newstr = $("#ReportViewer1_ctl10").html();
    //open blank html for printing
    var popupWin = window.open('', '_blank');
    //paste data of printing in blank html page
    popupWin.document.write(headstr + newstr + footstr);
    //print the page and see is what you see is what you get
    popupWin.print(); 
    return false;
}
</script>
    </form>
</body>
</html>
