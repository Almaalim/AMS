<%@ Page Title="Round Patrol Transaction" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="RoundPatrolTransaction.aspx.cs" Inherits="RoundPatrolTransaction" Culture="auto"
    meta:resourcekey="PageResource1" UICulture="auto" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                        AutoPostBack="True" meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Employee ID" Value="EmpID" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (Ar)" Value="EmpNameAr" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (En)" Value="EmpNameEn" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="Machine ID" Value="MacID" meta:resourcekey="ListItemResource5"></asp:ListItem>
                        <asp:ListItem Text="Transaction Date" Value="TrnDate" meta:resourcekey="ListItemResource6"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col2" id="divtxt" runat="server">

                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtFilterResource1"></asp:TextBox>
                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnFilterResource1" />

                </div>
            </div>
            <div id="divdate" runat="server" visible="false" class="row">
                <div class="col1">
                    <asp:Label ID="lblFromDate" runat="server" Text="From:" meta:resourcekey="lblFromDateResource1"></asp:Label>
                </div>
                <div class="col4">
                    <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" />
                </div>
                <div class="col1">
                    <asp:Label ID="lblToDate" runat="server" Text="To:" meta:resourcekey="lblToDateResource1"></asp:Label>
                </div>
                <div class="col4">
                    <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" />
                </div>

            </div>

            <div class="row">
                <div class="col12">
                    <asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />

                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                        GridLines="None" DataKeyNames="rptID" ShowFooter="True"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowDataBound="grdData_RowDataBound"
                        OnRowCreated="grdData_RowCreated" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnPreRender="grdData_PreRender" EnableModelValidation="True"
                        meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField DataField="EmpID" HeaderText="ID" SortExpression="EmpID" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="rptID" HeaderText="rptID" SortExpression="rptID" meta:resourcekey="rptIDResource1" />
                            <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr" SortExpression="EmpNameAr"
                                InsertVisible="False" ReadOnly="True" meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                            <asp:BoundField DataField="EmpNameEn" HeaderText="Name (En)" SortExpression="EmpNameEn"
                                meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField HeaderText="Machine ID" DataField="MacID" SortExpression="MacID"
                                meta:resourcekey="BoundFieldResource4" />
                            <asp:TemplateField HeaderText="Transaction Date" SortExpression="TrnDate" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("TrnDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transaction Time" SortExpression="TrnTime" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("TrnTime"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Transaction Type" DataField="TrnType" SortExpression="TrnType"
                                Visible="False" meta:resourcekey="BoundFieldResource5" />
                            <asp:TemplateField HeaderText="           " Visible="False" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" Enabled="False" CommandName="Delete1" CommandArgument='<%# Eval("rptID") %>'
                                        runat="server" ImageUrl="../images/del.gif" meta:resourcekey="imgbtnDeleteResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
