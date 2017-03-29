<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SALES_REPORT_VIEW.aspx.cs" Inherits="Admin_SALES_REPORT_VIEW" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    1.	<script type="text/javascript">  
2.	    function Print() {  
3.	        var dvReport = document.getElementById("dvReport");  
4.	        var frame1 = dvReport.getElementsByTagName("iframe")[0];  
5.	        if (navigator.appName.indexOf("Internet Explorer") != -1 || navigator.appVersion.indexOf("Trident") != -1) {  
6.	            frame1.name = frame1.id;  
7.	            window.frames[frame1.id].focus();  
8.	            window.frames[frame1.id].print();  
9.	        } else {  
10.	            var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;  
11.	            frameDoc.print();  
12.	        }  
13.	    }  
14.	</script>  

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" />
    </div>
    </form>
</body>
</html>
