<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ADEmployees.aspx.cs" Inherits="ADEmployees" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--=============================================================================--%>
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="ShowMsg" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary runat="server" ID="vsSave" ValidationGroup="vgSave" EnableClientScript="False"
                        CssClass="MsgValidation" ShowSummary="False" meta:resourcekey="vsumAllResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnFind" runat="server" CssClass="GenButton glyphicon glyphicon-search" OnClick="btnFind_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_magnify.png&quot; /&gt; Find"
                        meta:resourcekey="btnFindResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-edit" OnClick="btnSave_Click"
                          ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:CustomValidator ID="cvIsRepeat" runat="server" Display="None" ValidationGroup="vgSave"
                        OnServerValidate="IsRepeat_ServerValidate" EnableClientScript="False"
                        ControlToValidate="txtValid" meta:resourcekey="cvIsRepeatResource1"></asp:CustomValidator>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"/>
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                        GridLines="None" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowCommand="grdData_RowCommand"
                        OnPreRender="grdData_PreRender"
                        meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField HeaderText="Employee ID" DataField="EmpID" InsertVisible="False"
                                ReadOnly="True" SortExpression="EmpID"
                                meta:resourcekey="BoundFieldResource1">
                                <HeaderStyle CssClass="first" />
                                <ItemStyle CssClass="first" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr"
                                SortExpression="EmpNameAr" meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField HeaderText="Name (En)" DataField="EmpNameEn"
                                SortExpression="EmpNameEn" meta:resourcekey="BoundFieldResource5" />
                            <asp:BoundField HeaderText="Email" DataField="EmpEmailID" Visible="false"
                                SortExpression="EmpEmailID" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField HeaderText="National ID" DataField="EmpNationalID" Visible="false"
                                SortExpression="EmpNationalID" meta:resourcekey="BoundFieldResource222" />
                            <asp:BoundField HeaderText="Active Directory Employee" DataField="EmpADUser"
                                SortExpression="EmpADUser" meta:resourcekey="BoundFieldResource3" />
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
