<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ChartViewer.aspx.cs" Inherits="Pages_Report_ChartViewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../Script/fusionchartsJs/fusioncharts.js" ></script>
    <script type="text/javascript" src="../Script/fusionchartsJs/themes/fusioncharts.theme.carbon.js"></script>
    <script type="text/javascript" src="../Script/fusionchartsJs/themes/fusioncharts.theme.fint.js"></script>
    <script type="text/javascript" src="../Script/fusionchartsJs/themes/fusioncharts.theme.ocean.js"></script>
    <script type="text/javascript" src="../Script/fusionchartsJs/themes/fusioncharts.theme.zune.js"></script>

    <div class="row">
        <div class="col1">
            <asp:LinkButton ID="btnBackToReportsPage" runat="server" CssClass="GenButtonsmall backbtn" meta:resourcekey="btnBackToReportsPageResource1"
                OnClick="btnBackToReportsPage_Click"></asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col12">
            <%--<asp:Panel ID="pnlChartViewer" runat="server">--%>
                <asp:Literal ID="litChartViewer" runat="server"></asp:Literal>
            <%--</asp:Panel>--%>
        </div>
    </div>
</asp:Content>

