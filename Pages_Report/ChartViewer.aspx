<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ChartViewer.aspx.cs" Inherits="Pages_Report_ChartViewer" meta:resourcekey="PageResource2" %>

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
                    <asp:Label ID="lblChartTitel" runat="server" CssClass="h4" meta:resourcekey="lblChartTitelResource1"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col1">
                    <asp:LinkButton ID="btnBackToReportsPage" runat="server" CssClass="GenButtonsmall backbtn"
                        OnClick="btnBackToReportsPage_Click" meta:resourcekey="btnBackToReportsPageResource1"></asp:LinkButton>
                </div>
            </div>
            <div id="divNameTitel" runat="server" visible="false" class="row">
                <div class="col12 Heading">
                    <asp:Label ID="lblNameTitel" runat="server" CssClass="h4" meta:resourcekey="lblChartTitelResource1"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:Literal ID="litChartViewer" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:Literal ID="litChartViewer_01" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:Literal ID="litChartViewer_02" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:Literal ID="litChartViewer_03" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="row">
                <div class="col6">
                    <asp:Literal ID="litChartViewer_04" runat="server"></asp:Literal>
                </div>
                <div class="col6">
                    <asp:Literal ID="litChartViewer_05" runat="server"></asp:Literal>
                </div>
            </div>

            <div class="row">
                <div class="col12">

                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False" DataKeyNames="ID"
                        CellPadding="0" BorderWidth="0px" GridLines="None" ShowFooter="True" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID"></asp:BoundField>
                            <asp:BoundField DataField="EmpNameAr" HeaderText="EmpNameAr" SortExpression="EmpNameAr"></asp:BoundField>
                            <asp:BoundField DataField="EmpNameEn" HeaderText="EmpNameEn" SortExpression="EmpNameEn"></asp:BoundField>
                            <asp:BoundField DataField="EmpPercentWork" HeaderText="EmpPercentWork" SortExpression="EmpPercentWork"></asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Literal ID="litChartViewerGrd"  runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="row" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

