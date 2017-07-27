<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeStatusToday.aspx.cs" Inherits="Pages_Employee_EmployeeStatusToday" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/AutoComplete.js"></script>
    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblFilter" runat="server" Text="Search by:" meta:resourcekey="lblSearchByResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlSearchByResource1" AutoPostBack="True" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" >
                        <asp:ListItem Text="Department"  Value="Dep" Selected="True" meta:resourcekey="ListItemDepResource3"></asp:ListItem>
                        <asp:ListItem Text="Employee ID" Value="EmpID" meta:resourcekey="ListItemEmpIDResource2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtSearchByDep" runat="server" meta:resourcekey="txtSearchByDepResource1"></asp:TextBox>
                </div>
                <div class="col2">
                    <asp:ImageButton ID="btnDepFilter" runat="server" OnClick="btnDepFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" meta:resourcekey="btnDepFilterResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:Panel runat="server" ID="pnlauDepName" Height="200px" ScrollBars="Vertical" meta:resourcekey="pnlauDepNameResource1" />
                    <ajaxToolkit:AutoCompleteExtender
                        runat="server"
                        ID="auDepName"
                        TargetControlID="txtSearchByDep"
                        ServicePath="~/Service/AutoComplete.asmx"
                        ServiceMethod="GetDepNameList"
                        MinimumPrefixLength="1"
                        OnClientItemSelected="AutoCompleteDepNameItemSelected"
                        CompletionListElementID="pnlauDepName"
                        CompletionListCssClass="AutoExtender"
                        CompletionListItemCssClass="AutoExtenderList"
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                        CompletionSetCount="12" DelimiterCharacters="" Enabled="True" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" AllowPaging="True"
                        GridLines="None" DataKeyNames="EmpID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnPreRender="grdData_PreRender" meta:resourcekey="grdDataResource1">
                        <Columns>
                            <asp:BoundField DataField="EmpID" HeaderText="Employee ID" SortExpression="EmpID" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr"
                                SortExpression="EmpNameAr" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="EmpNameEn" HeaderText="Name (En)"
                                SortExpression="EmpNameEn" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="DepNameAr" HeaderText="Department Name (Ar)"
                                SortExpression="DepNameAr" meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField HeaderText="Department Name (En)" DataField="DepNameEn"
                                SortExpression="DepNameEn" meta:resourcekey="BoundFieldResource5" />
                            <asp:TemplateField HeaderText="Status" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# FindStatus(Eval("EmpID"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.gif"
                            LastPageText="Last" LastPageImageUrl="~/images/last.gif" NextPageText="Next"
                            NextPageImageUrl="~/images/next.gif" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.gif" />

                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

