<%@ Page Title="Employee Vacation" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeVacation.aspx.cs" Inherits="EmployeeVacation" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
                    <asp:Label ID="lblFilter" runat="server" Text="Search by:"
                        meta:resourcekey="lblSearchByResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server"
                        meta:resourcekey="ddlSearchByResource1">
                        <asp:ListItem Selected="True" Text="[None]"
                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Employee ID" Value="EmpID"
                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (Ar)" Value="EmpNameAr"
                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (En)" Value="EmpNameEn"
                            meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="Vacation Type (Ar)" Value="VtpNameAr"
                            meta:resourcekey="ListItemResource5"></asp:ListItem>
                        <asp:ListItem Text="Vacation Type (En)" Value="VtpNameEn"
                            meta:resourcekey="ListItemResource6"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server"
                        meta:resourcekey="txtSearchResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click"
                        ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnFilterResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender ID="gridviewextender" runat="server" TargetControlID="grdData"/>
                    <asp:GridView ID="grdData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BorderWidth="0px" CellPadding="0" CssClass="datatable"
                        DataKeyNames="EvrID" GridLines="None" OnPreRender="grdData_PreRender"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnRowCommand="grdData_RowCommand"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnSorting="grdData_Sorting" ShowFooter="True"
                        EnableModelValidation="True" meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                            NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField DataField="EmpID" HeaderText="Employee ID"
                                SortExpression="EmpID" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="EvrID" HeaderText="Evr ID" SortExpression="EvrID"
                                meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                            <asp:BoundField DataField="EmpNameAr" HeaderText="Name (Ar)"
                                SortExpression="EmpNameAr" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="EmpNameEn" HeaderText="Name (En)"
                                SortExpression="EmpNameEn" meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField DataField="VtpNameAr" HeaderText="Vacation type (Ar)"
                                SortExpression="VtpNameAr" meta:resourcekey="BoundFieldResource5" />
                            <asp:BoundField DataField="VtpNameEn" HeaderText="Vacation type (En)"
                                SortExpression="VtpNameEn" meta:resourcekey="BoundFieldResource6" />
                            <asp:TemplateField HeaderText="Start Date" SortExpression="EvrStartDate"
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("EvrStartDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" SortExpression="EvrEndDate"
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("EvrEndDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="           "
                                meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%# Eval("EvrID") %>'
                                        CommandName="Delete1" Enabled="False"
                                        ImageUrl="../images/Button_Icons/button_delete.png"
                                        meta:resourcekey="imgbtnDeleteResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="vgShowMsg" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                        EnableClientScript="False" CssClass="MsgValidation"
                        meta:resourcekey="vsumAllResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton  glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add" meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" Visible="false"
                        OnClick="btnModify_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify" meta:resourcekey="btnModifyResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton  glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        ValidationGroup="vgSave" meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                        OnClick="btnCancel_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid"> </asp:CustomValidator>
                    </div>
                        </div>
                    <div class="GreySetion">
                        <div class="row">
                            <div class="col2">
                                <span class="RequiredField">*</span>
                                <asp:Label ID="lblEmpID" runat="server" Text="Employee ID :"
                                    meta:resourcekey="lblEmpIDResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtEmpID" runat="server" autocomplete="off" Enabled="False" meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                                <asp:Panel runat="server" ID="pnlauID" />
                                <ajaxToolkit:AutoCompleteExtender
                                    runat="server"
                                    ID="auID"
                                    TargetControlID="txtEmpID"
                                    ServicePath="~/Service/AutoComplete.asmx"
                                    ServiceMethod="GetEmployeeIDList"
                                    MinimumPrefixLength="1"
                                    CompletionInterval="1000"
                                    EnableCaching="true"
                                    OnClientItemSelected="AutoCompleteIDItemSelected"
                                    CompletionListElementID="pnlauID"
                                    CompletionListCssClass="AutoExtender"
                                    CompletionListItemCssClass="AutoExtenderList"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                    CompletionSetCount="12" />
                                <asp:CustomValidator ID="cvEmpID" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                    ValidationGroup="vgSave" OnServerValidate="EmpID_ServerValidate" CssClass="CustomValidator"
                                    EnableClientScript="False" ControlToValidate="txtValid">
                                </asp:CustomValidator>
                            </div>
                            <div class="col2">
                                <span class="RequiredField">*</span>
                                <asp:Label ID="lblVacationType" runat="server" Text="Vacation Type:"
                                    meta:resourcekey="lblVacationTypeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <%--<asp:DropDownList ID="ddlVacType" runat="server"
                                    meta:resourcekey="ddlVacTypeResource1" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlVacType_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                                <asp:DropDownListAttributes ID="ddlVacType" runat="server"></asp:DropDownListAttributes>

                                <asp:RequiredFieldValidator ID="rvVacType" runat="server" ControlToValidate="ddlVacType" CssClass="CustomValidator"
                                    EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Vacation Type is required' /&gt;"
                                    ValidationGroup="vgSave" meta:resourcekey="rfvddlVacTypeResource1"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="cvMaxDays" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='MaxDays' /&gt;"
                                    ValidationGroup="vgSave" ErrorMessage="MaxDays" OnServerValidate="MaxDays_ServerValidate"
                                    EnableClientScript="False" ControlToValidate="txtValid" CssClass="CustomValidator"
                                    meta:resourcekey="cvMaxDaysResource1"></asp:CustomValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <span class="RequiredField">*</span>
                                <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"
                                    meta:resourcekey="lblStartDateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate" />
                            </div>
                            <div class="col2">
                                <span class="RequiredField">*</span>
                                <asp:Label ID="lblEndDate" runat="server" Text="End Date:"
                                    meta:resourcekey="lblEndDateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblAvailable" runat="server" Text="Available:"
                                    meta:resourcekey="lblAvailableResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtAvailable" runat="server" TextMode="MultiLine"
                                    meta:resourcekey="txtAvailableResource1"  ></asp:TextBox>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No:"
                                    meta:resourcekey="lblPhoneNoResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtPhoneNo" runat="server"
                                    meta:resourcekey="txtPhoneNoResource1"  ></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblDesc" runat="server" Text="Description:"
                                    meta:resourcekey="lblDescResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"
                                    meta:resourcekey="txtDescResource1" Height="60px"></asp:TextBox>
                            </div>
                            <div class="col2">
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled" Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
