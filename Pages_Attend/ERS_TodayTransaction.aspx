<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ERS_TodayTransaction.aspx.cs" Inherits="ERS_TodayTransaction" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblTodayDate" runat="server" Text="Today Date :"
                        meta:resourcekey="lblTodayDateResource1"></asp:Label>
                </div>
                <div class="col1">
                    <asp:Label ID="lblVTodayDate" runat="server"
                        meta:resourcekey="lblVTodayDateResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:Label ID="lblTodayTime" runat="server"
                        Text="Transactions until the time :" meta:resourcekey="lblTodayTimeResource1"></asp:Label>
                </div>
                <div class="col1">
                    <asp:Label ID="lblVTodayTime" runat="server"
                        meta:resourcekey="lblVTodayTimeResource1"></asp:Label>
                </div>

                <div class="col1">
                    <asp:Label ID="lblShiftFrom" runat="server" Text="Shift From :"
                        meta:resourcekey="lblShiftFromResource1"></asp:Label>
                </div>
                <div class="col1">
                    <asp:Label ID="lblVShiftFrom" runat="server"
                        meta:resourcekey="lblVShiftFromResource1"></asp:Label>
                </div>

                <div class="col1">
                    <asp:Label ID="lblShiftTo" runat="server" Text="Shift To :"
                        meta:resourcekey="lblShiftToResource1"></asp:Label>
                </div>
                <div class="col1">
                    <asp:Label ID="lblVShiftTo" runat="server"
                        meta:resourcekey="lblVShiftToResource1"></asp:Label>
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
                        AutoGenerateColumns="False" PageSize="15" DataKeyNames="SchID"
                        GridLines="None" ShowFooter="True" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound"
                        OnRowCommand="grdData_RowCommand"
                        meta:resourcekey="grdDataResource1">
                        <Columns>
                            <asp:BoundField DataField="SchID" HeaderText="Sch ID" SortExpression="SchID"
                                Visible="False" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                            <asp:BoundField DataField="EmpID" HeaderText="Emp ID" SortExpression="EmpID"
                                Visible="False" meta:resourcekey="BoundFieldResource2"></asp:BoundField>

                            <asp:BoundField DataField="TrnShift" HeaderText="Shift"
                                SortExpression="TrnShift" meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                            <asp:TemplateField HeaderText="Date" SortExpression="TrnDate"
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("TrnDate")) %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time" SortExpression="TrnTime"
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("TrnTime"))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IN\OUT" SortExpression="TrnType"
                                meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTypeTrans(Eval("TrnType")) %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Work\Overtime " SortExpression="Type"
                                meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <%# GrdDisplayWork(Eval("TrnShift"))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

