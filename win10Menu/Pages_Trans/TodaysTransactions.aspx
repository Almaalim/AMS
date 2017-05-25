<%@ Page Title="Todays Transaction" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="TodaysTransactions.aspx.cs" Inherits="TodaysTransactions" Culture="auto"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblFilter" runat="server" Text="Search By:" Font-Names="Tahoma"
                        meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Employee ID" Value="EmpID" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (Ar)" Value="EmpNameAr" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (En)" Value="EmpNameEn" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="Department Name (Ar)" Value="DepNameAr" meta:resourcekey="ListItemResource5"></asp:ListItem>
                        <asp:ListItem Text="Department Name (En)" Value="DepNameEn" meta:resourcekey="ListItemResource6"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtFilterResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnFilterResource1" />
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

                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" AutoGenerateColumns="False"
                        CellPadding="0" BorderWidth="0px" GridLines="None" AllowPaging="True"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnPreRender="grdData_PreRender"
                        OnRowCreated="grdData_RowCreated" DataKeyNames="EmpID" ShowFooter="True" EnableModelValidation="True"
                        meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="EmpID" InsertVisible="False" ReadOnly="True"
                                SortExpression="EmpID" meta:resourcekey="BoundFieldResource1">
                             
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr" InsertVisible="False"
                                ReadOnly="True" SortExpression="EmpNameAr" meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                            <asp:BoundField HeaderText="Name (En)" DataField="EmpNameEn" InsertVisible="False"
                                ReadOnly="True" SortExpression="EmpNameEn" meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                            <asp:BoundField HeaderText="Department Name (Ar)" DataField="DepNameAr" InsertVisible="False"
                                ReadOnly="True" SortExpression="DepNameAr" meta:resourcekey="BoundFieldResource4"></asp:BoundField>
                            <asp:BoundField DataField="DepNameEn" HeaderText="Department Name (En)" SortExpression="DepNameEn"
                                meta:resourcekey="BoundFieldResource5" />
                            <asp:TemplateField HeaderText="Time" InsertVisible="False" SortExpression="TrnTime"
                                meta:resourcekey="TemplateFieldResource11">
                                <ItemTemplate>
                                    <asp:Label ID="lblGapStartTime1" runat="server" Text='<%# DisplayFun.GrdDisplayTime(Eval("TrnTime")) %>'
                                        meta:resourcekey="lblTranstimeResource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transaction" InsertVisible="False" SortExpression="TrnType"
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTypeTrans(Eval("TrnType"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>

                    <!-- Notice this is outside the GridView -->
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
