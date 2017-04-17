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
            Font-Size="8pt" Height="740px" InteractiveDeviceInfos="(Collection)" 
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
    <asp:Button ID="Button1" runat="server" Text="Print" OnClientClick="Print()" 
        onclick="Button1_Click"/>
    <asp:Button ID="Button2" runat="server" Text="Back" onclick="Button2_Click" />
    <script type="text/javascript">
        function Print() {
            var report = document.getElementById("<%=ReportViewer1.ClientID %>");
            var div = report.getElementsByTagName("DIV");
            var reportContents;
            for (var i = 0; i < div.length; i++) {
                if (div[i].id.indexOf("VisibleReportContent") != -1) {
                    reportContents = div[i].innerHTML;
                    break;
                }
            }
            var frame1 = document.createElement('iframe');
            frame1.name = "frame1";
            frame1.style.position = "absolute";
            frame1.style.top = "-1000000px";
            document.body.appendChild(frame1);
            var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
            frameDoc.document.open();
            frameDoc.document.write('<html><head><title>RDLC Report</title>');
            frameDoc.document.write('</head><body style = "font-family:arial;font-size:10pt;">');
            frameDoc.document.write(reportContents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                document.body.removeChild(frame1);
            }, 500);
        }
</script>
    </form>
</body>
</html>
