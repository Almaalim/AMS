<%@ Page Title="Home" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <script type="text/javascript" src="../Script/TabContainer.js"></script>
    <script type="text/javascript" src="../FusionCharts/FusionCharts.js" language="Javascript"></script>
    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnChartsFilter" />
        </Triggers>
        
        <ContentTemplate>
             <div id="DivList" class="row" runat="server">
                <div class="col8">
                    <asp:Label ID="lblDepChartsFilter" runat="server" Text=" Department:" Width="120px"></asp:Label>
                    <asp:DropDownList ID="ddlDepChartsFilter" runat="server" Width="250px" onselectedindexchanged="ddlDepChartsFilter_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Label ID="lblEmpChartsFilter" runat="server" Text=" Employee:" Width="120px"></asp:Label>
                    <asp:DropDownList ID="ddlEmpChartsFilter" runat="server" Width="250px"></asp:DropDownList>
                </div>
            </div>
            
            <div class="row" runat="server">
                <div class="col8">
                   <asp:Label ID="lblTypeChartsFilter" runat="server" Text=" Type:" Width="120px"></asp:Label>
                   <asp:DropDownList ID="ddlTypeChartsFilter" runat="server" Width="250px" onselectedindexchanged="ddlTypeChartsFilter_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="M" Text="Monthly"></asp:ListItem>
                        <asp:ListItem Value="D" Text="Daily"></asp:ListItem>
                    </asp:DropDownList>
                    <div id="DivMonth" runat="server" class="col8">
                        <asp:Label ID="lblMonth" runat="server" Text="Month:"></asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="150px"></asp:DropDownList>
                        &nbsp;
                        <asp:Label ID="lblYear" runat="server" Text="Year:"></asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="ddlYear" runat="server" Width="80px"></asp:DropDownList>
                    </div>
                    <div id="DivDay" runat="server" class="col8">
                        <asp:Label ID="lblDate"  runat="server" Text="Date:"></asp:Label>   
                        &nbsp;
                        <Cal:Calendar2 ID="calDate" runat="server" CalendarType="System" ValidationGroup="" InitialValue="true" />
                    </div>
                </div>
            </div>
            
            <div class="row" runat="server">
                <div class="col8">
                    <asp:LinkButton ID="btnChartsFilter" runat="server" CssClass="GenButton  glyphicon glyphicon-plus-sign" 
                        OnClick="btnChartsFilter_Click" 
                        Text="&lt;img src=&quot;../images/Button_Icons/button_magnify.png&quot; /&gt; Search" ></asp:LinkButton>
                </div>
            </div>

            <div id="divChart" runat="server">
                <div class="row">
                    <div class="col12 Heading">
                        <asp:Label ID="Label1" runat="server" Text="CHARTS" CssClass="h4" meta:resourcekey="Label1Resource1"></asp:Label>

                    </div>
                </div>

                <div class="row" runat="server">
                    <div class="col6 chartBlue">
                        <asp:Literal ID="litChartWorkDurtion" runat="server"></asp:Literal>
                    </div>
                    <div class="col6 ChartYellow">
                        <asp:Literal ID="litChartBeginLateDurtion" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="row">
                
            <div class="col4">
                

            </div>
            </div>
            <div class="row" runat="server">
               <div class="col6 ChartRed">
                        <asp:Literal ID="LitChartAbsentDays" runat="server"></asp:Literal>
                    </div>
                <div class="col6 chartPurple">
                   <asp:Literal ID="LitChartDurations" runat="server"></asp:Literal>
                </div>
            </div>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
