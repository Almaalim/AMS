<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeStatusToday.aspx.cs" Inherits="Pages_Employee_EmployeeStatusToday" %>

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
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table class="rp_Search">
                            <tr>
                                <%--<td>
                                    &nbsp; &nbsp;
                                    <asp:Label ID="lblDepFilter" runat="server" Text="Search By Department:" 
                                        meta:resourcekey="lblDepFilterResource1"></asp:Label>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlDepFilter" runat="server" Width="500px" 
                                        meta:resourcekey="ddlDepFilterResource1">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:ImageButton ID="btnDepFilter" runat="server" OnClick="btnDepFilter_Click" 
                                        ImageUrl="../images/Button_Icons/button_magnify.png" 
                                        meta:resourcekey="btnDepFilterResource1" />
                                </td>--%>
                                <td>
                                    <asp:Label ID="lblDepFilter" runat="server" Text="Search By Department:" meta:resourcekey="lblDepFilterResource1"></asp:Label>
                                    <asp:TextBox ID="txtSearchByDep" runat="server" Width="400px"></asp:TextBox>
                                    <asp:ImageButton ID="btnDepFilter" runat="server" OnClick="btnDepFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" meta:resourcekey="btnDepFilterResource1"/>
                                                                    
                                    <asp:Panel runat="server" ID="pnlauDepName" Height="200px"  ScrollBars="Vertical" />
                                    <ajaxToolkit:AutoCompleteExtender
                                        runat="server" 
                                        ID="auDepName" 
                                        TargetControlID="txtSearchByDep"
                                        ServicePath="~/Service/AutoComplete.asmx" 
                                        ServiceMethod="GetDepNameList"
                                        MinimumPrefixLength="1" 
                                        CompletionInterval="1000"
                                        EnableCaching="true"
                                        OnClientItemSelected="AutoCompleteDepNameItemSelected"
                                        CompletionListElementID="pnlauDepName"
                                        CompletionListCssClass="AutoExtender" 
                                        CompletionListItemCssClass="AutoExtenderList" 
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                        CompletionSetCount="12" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="grid">
                            <div class="rounded">
                                <div class="top-outer">
                                    <div class="top-inner">
                                        <div class="top">
                                        </div>
                                    </div>
                                </div>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid">
                                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                                            <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                                AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                                                GridLines="None" DataKeyNames="EmpID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                                OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                                OnPreRender="grdData_PreRender" Width="800px" EnableModelValidation="True">
                                                <Columns>
                                                    <asp:BoundField DataField="EmpID" HeaderText="Employee ID" SortExpression="EmpID"/>
                                                    <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr" 
                                                        SortExpression="EmpNameAr"/>
                                                    <asp:BoundField DataField="EmpNameEn" HeaderText="Name (En)" 
                                                        SortExpression="EmpNameEn"/>
                                                    <asp:BoundField DataField="DepNameAr" HeaderText="Department Name (Ar)" 
                                                        SortExpression="DepNameAr"/>
                                                    <asp:BoundField HeaderText="Department Name (En)" DataField="DepNameEn" 
                                                        SortExpression="DepNameEn"/>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <%# FindStatus(Eval("EmpID"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.gif"
                                                    LastPageText="Last" LastPageImageUrl="~/images/last.gif" NextPageText="Next"
                                                    NextPageImageUrl="~/images/next.gif" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.gif" />
                                                <RowStyle CssClass="row" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="bottom-outer">
                                    <div class="bottom-inner">
                                        <div class="bottom">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

