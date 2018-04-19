<%@ Page Title="Home" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript" src="../Script/fusionchartsJs/fusioncharts.js" ></script>
    <script type="text/javascript" src="../Script/fusionchartsJs/themes/fusioncharts.theme.carbon.js"></script>
    <script type="text/javascript" src="../Script/fusionchartsJs/themes/fusioncharts.theme.fint.js"></script>
    <script type="text/javascript" src="../Script/fusionchartsJs/themes/fusioncharts.theme.ocean.js"></script>
    <script type="text/javascript" src="../Script/fusionchartsJs/themes/fusioncharts.theme.zune.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12 Heading">
                    <asp:Label ID="lblTodayStatus" runat="server" Text="Today Status" CssClass="h4" meta:resourcekey="lblTodayStatusResource1"></asp:Label>
                </div>
            </div>
            <div id="divTodayStatus" class="row" runat="server">
                <div class="col4">
                    <asp:Literal ID="litTodayStatus" runat="server"></asp:Literal>
                </div>
                <div class="col8">
                     <asp:Literal ID="litPresentEmp"   runat ="server"></asp:Literal>
                     <asp:Literal ID="litAbsenceEmp"   runat ="server"></asp:Literal>
                     <asp:Literal ID="litVacationsEmp" runat="server"></asp:Literal>
                </div>
            </div>
            
            <div class="row">
                <div class="col12 Heading">
                    <div Class="h4">
                   
                    <asp:Label ID="lblMonthlyStatus" runat="server" Text="Monthly Status"  meta:resourcekey="lblMonthlyStatusResource1" Width="127px"></asp:Label>
                      
                    <asp:DropDownList ID="ddlMonth" runat="server" meta:resourcekey="ddlMonthResource1" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" Width="25%" CssClass="h4dropdown"></asp:DropDownList>
                       
                    <asp:Label ID="lblDivYear" runat="server" Text="/ "  Width="10px"></asp:Label>
                          
                    <asp:Label ID="lblYear" runat="server" Text=""  Width="50px" ></asp:Label>
                         
                        </div>
                </div>
            </div>
            <div>
                <div class="row">
                    <div class="col6">
                        <asp:Literal ID="litChartMonthly_01" runat="server"></asp:Literal>
                    </div>
                    <div class="col6">
                        <asp:Literal ID="litChartMonthly_02" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col4">
                </div>
            </div>
            <div class="row">
                <div class="col6">
                    <asp:Literal ID="litChartMonthly_03" runat="server"></asp:Literal>
                </div>
                <div class="col6">
                    <asp:Literal ID="litChartMonthly_04" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:Literal ID="litChartMonthly_05" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:Literal ID="litChartMonthly_06" runat="server"></asp:Literal>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
