<%@ Page Title="Report Viewer" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="ReportViewer" %>

<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="cc2" %>
<%@ Register Assembly="Stimulsoft.Report.WebFx" Namespace="Stimulsoft.Report.WebFx" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col1">
            <asp:LinkButton ID="btnBackToReportsPage" runat="server" CssClass="GenButtonsmall backbtn" meta:resourcekey="btnBackToReportsPageResource1"
                OnClick="btnBackToReportsPage_Click"></asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <%--<div class="GreySetion">--%>
            <%--<div class="col12">
                <cc1:StiWebViewerFx ID="StiWebViewerFx1" runat="server" Width="100%" Height="800px" Background="White" />
            </div>--%>
            <%--<div class="col12">--%>
                <cc2:StiWebViewer ID="StiWebViewerFx1" runat="server" Width="100%"  Background="White" ServerTimeOut="00:30:00"   />
            <%--</div>
        </div>--%>
    </div>
    <%--onpreinit="StiWebViewerFx1_PreInit"--%>
</asp:Content>

