<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ERS_EmployeeTransaction.aspx.cs" Inherits="ERS_EmployeeTransaction" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <%--script--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">

                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="ShowMsg" meta:resourcekey="vsShowMsgResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="col2">
                    <asp:DropDownList ID="ddlMonth" runat="server" meta:resourcekey="ddlMonthResource1"  >
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:" meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlYear" runat="server" meta:resourcekey="ddlYearResource1"  >
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" meta:resourcekey="btnFilterResource1"  />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <span class="h3">
                        <asp:Literal ID="LitEmpName" runat="server" Text="Employee name"
                            meta:resourcekey="Literal2Resource1"></asp:Literal>
                    </span>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" 
                        AutoGenerateColumns="False" PageSize="3"
                        CellPadding="0" BorderWidth="0px" GridLines="None" ShowFooter="True" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound"
                        OnRowCommand="grdData_RowCommand" meta:resourcekey="grdDataResource1">
                        <Columns>
                            <asp:BoundField DataField="TrnDate" HeaderText="TrnDate" SortExpression="TrnDate" Visible="False" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                            <asp:TemplateField HeaderText="Day Name" SortExpression="Day Name" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# GrdDisplayDayName(Eval("TrnDate"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" SortExpression="Date" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("TrnDate"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time" SortExpression="TrnTime" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("TrnTime"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MacLocationEn" HeaderText="MacLocationEn" SortExpression="MacLocationEn" Visible="False" meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                            <asp:BoundField DataField="MacLocationAr" HeaderText="MacLocationAr" SortExpression="MacLocationAr" Visible="False" meta:resourcekey="BoundFieldResource3"></asp:BoundField>

                            <asp:TemplateField HeaderText="Location" SortExpression="MacLocation" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <%# General.Msg(Eval("MacLocationEn").ToString(),Eval("MacLocationAr").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TrnType" HeaderText="TrnType" SortExpression="TrnType" Visible="False" meta:resourcekey="BoundFieldResource4"></asp:BoundField>
                            <asp:TemplateField HeaderText="Type" SortExpression="Type" meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTypeTrans(Eval("TrnType"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="TrnShift" HeaderText="Shift" SortExpression="TrnShift" meta:resourcekey="BoundFieldResource5"></asp:BoundField>
                            <asp:BoundField DataField="UsrName" HeaderText="Add By" SortExpression="UsrName" meta:resourcekey="BoundFieldResource6"></asp:BoundField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


