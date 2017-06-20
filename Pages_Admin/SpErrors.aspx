<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="SpErrors.aspx.cs" Inherits="SpErrors" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Procedure:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlProcedure" runat="server" meta:resourcekey="ddlProcedureResource1"></asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblMonth" runat="server" Text="Month:" meta:resourcekey="lblMonthResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlMonth" runat="server" meta:resourcekey="ddlMonthResource1"></asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:" meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlYear" runat="server" meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" meta:resourcekey="btnFilterResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"
                        PrevRowSelectKey="Subtract" NextRowSelectKey="Add" NextPageKey="PageUp"
                        PreviousPageKey="PageDown" />

                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" AllowPaging="True"
                        GridLines="None" DataKeyNames="ErrorLogID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                        OnPreRender="grdData_PreRender" meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />

                        <Columns>
                            <asp:BoundField DataField="ErrorLogID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ErrorLogID" meta:resourcekey="BoundFieldResource1" />

                            <asp:TemplateField HeaderText="Date" SortExpression="ErrorDate" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ErrorDate")) + " " + DisplayFun.GrdDisplayTime(Eval("ErrorDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="ErrorProcedure" HeaderText="Procedure" SortExpression="ErrorProcedure" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="ErrorLine" HeaderText="Line" SortExpression="ErrorLine" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="ErrorMessage" HeaderText="Message" SortExpression="ErrorMessage" meta:resourcekey="BoundFieldResource4" />

                        </Columns>

                    </asp:GridView>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
