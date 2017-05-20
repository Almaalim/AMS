<%@ Page Title="Report Viewer" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="ReportViewer" %>

<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="cc2" %>

<%@ Register Assembly="Stimulsoft.Report.WebFx" Namespace="Stimulsoft.Report.WebFx"
    TagPrefix="cc1" %>





<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:LinkButton ID="btnBackToReportsPage" runat="server" CssClass="GenButton" Height="18px"
                    Text="&lt;img src=&quot;../images/Button_Icons/page_import.png&quot; /&gt; Back"
                    Width="70px" meta:resourcekey="btnBackToReportsPageResource1" 
                    onclick="btnBackToReportsPage_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <%--<cc1:StiWebViewerFx ID="StiWebViewerFx1" runat="server" Width="800px" 
        Height="800px" Background="White" onpreinit="StiWebViewerFx1_PreInit" />--%>
    <cc1:StiWebViewerFx ID="StiWebViewerFx1" runat="server" Width="800px" 
        Height="800px" Background="White" />
    <%--<cc2:StiWebViewer ID="StiWebViewerFx1" runat="server" Width="800px" 
        Height="800px" Background="White" ServerTimeOut="00:30:00"  />--%>
    <%--onpreinit="StiWebViewerFx1_PreInit"--%>
</asp:Content>

