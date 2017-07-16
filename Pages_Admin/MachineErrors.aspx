<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="MachineErrors.aspx.cs" Inherits="MachineErrors" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>

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
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"
                        PrevRowSelectKey="Subtract" NextRowSelectKey="Add" NextPageKey="PageUp"
                        PreviousPageKey="PageDown" />

                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                        GridLines="None" DataKeyNames="MacID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                        OnPreRender="grdData_PreRender" meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />

                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="MacID" SortExpression="MacID" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField HeaderText="IP Address" DataField="MacIP" SortExpression="MacIP" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField HeaderText="Type" DataField="MtpName" SortExpression="MtpName" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField HeaderText="Location (En)" DataField="MacLocationEn" SortExpression="MacLocationEn" meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField HeaderText="Location (Ar)" DataField="MacLocationAr" SortExpression="MacLocationAr" meta:resourcekey="BoundFieldResource5" />
                            <asp:BoundField HeaderText="Number" DataField="ErrNo" SortExpression="ErrNo" meta:resourcekey="BoundFieldResource6" />
                            <asp:BoundField HeaderText="Message" DataField="ErrMsg" SortExpression="ErrMsg" meta:resourcekey="BoundFieldResource7" />
                            <asp:TemplateField HeaderText="Time" SortExpression="ErrTime" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ErrTime")) + " " + DisplayFun.GrdDisplayTime(Eval("ErrTime"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

