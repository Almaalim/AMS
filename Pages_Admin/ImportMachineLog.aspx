﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ImportMachineLog.aspx.cs" Inherits="Pages_Admin_ImportMachineLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblMonth" runat="server" Text="Month:" meta:resourcekey="lblMonthResource1"></asp:Label>
                </div>
                <div class="col3">
                    <asp:DropDownList ID="ddlMonth" runat="server" meta:resourcekey="ddlMonthResource1"></asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:" meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col3">
                    <asp:DropDownList ID="ddlYear" runat="server" meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" meta:resourcekey="btnFilterResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"/>
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                        GridLines="None" DataKeyNames="ImlID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                        OnPreRender="grdData_PreRender" meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />

                        <Columns>
                            <asp:BoundField HeaderText="Log ID" DataField="IplID" SortExpression="IplID"/>                     
                            <asp:BoundField HeaderText="ID" DataField="ImlID" SortExpression="ImlID"/>
                            <asp:BoundField HeaderText="Machine ID" DataField="MacID" SortExpression="MacID"/>
                            <asp:BoundField HeaderText="Type" DataField="MtpName" SortExpression="MtpName"/>
                            <asp:BoundField HeaderText="IP Address" DataField="MacIP" SortExpression="MacIP"/>                         
                            <asp:BoundField HeaderText="Location (En)" DataField="MacLocationEn" SortExpression="MacLocationEn"/>
                            <asp:BoundField HeaderText="Location (Ar)" DataField="MacLocationAr" SortExpression="MacLocationAr"/>
                            <asp:TemplateField HeaderText="Start" SortExpression="ImlStartDT">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ImlStartDT")) + " " + DisplayFun.GrdDisplayTime(Eval("ImlStartDT"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End" SortExpression="ImlEndDT">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ImlEndDT")) + " " + DisplayFun.GrdDisplayTime(Eval("ImlEndDT"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duration" SortExpression="ImlDuration">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("ImlDuration")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="ImlIsImport">
                                <ItemTemplate>
                                    <%# DisplayStatus(Eval("ImlIsImport")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Transaction Count" DataField="ImlTransCount" SortExpression="ImlTransCount"/>
                            <asp:BoundField HeaderText="Error Message" DataField="ImlErrMsg" SortExpression="ImlErrMsg"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

