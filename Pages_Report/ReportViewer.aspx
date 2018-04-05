<%@ Page Title="Report Viewer" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="ReportViewer" %>

<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col1">
            <asp:LinkButton ID="btnBackToReportsPage" runat="server" CssClass="GenButtonsmall backbtn" meta:resourcekey="btnBackToReportsPageResource1"
                OnClick="btnBackToReportsPage_Click"></asp:LinkButton>
        </div>
    </div>
    <div class="row" >
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Width="100%" ShowAboutButton="false" ShowFullScreenButton="false" /> <%--OnGetReport="StiWebViewer1_GetReport"--%>
    </div>
</asp:Content>

