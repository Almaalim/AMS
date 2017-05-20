<%@ Page Title="Licenses Department" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeExcusePermit.aspx.cs" Inherits="EmployeeExcusePermit" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <script type="text/javascript" src="../Script/DivPopup.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <%--script--%>
    <%--<link href="../CSS/PopupStyle.css" rel="stylesheet" type="text/css" />--%>
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
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Employee ID" Value="EmpID"
                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (AR)" Value="EmpNameAr"
                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (En)" Value="EmpNameEn"
                            meta:resourcekey="ListItemResource4"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server"
                        meta:resourcekey="txtSearchResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" 
                        ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnFetchDataResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:UpdatePanel ID="updPanel" runat="server">
                        <ContentTemplate>
                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"
                                NextRowSelectKey="Add" PrevRowSelectKey="Subtract" />

                            <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                                GridLines="None" DataKeyNames="EmpID" OnPageIndexChanging="grdData_PageIndexChanging"
                                OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                ShowFooter="True" OnRowCreated="grdData_RowCreated"
                                OnPreRender="grdData_PreRender" meta:resourcekey="grdDataResource1">

                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                                    LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                                    NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="EmpID" SortExpression="EmpID"
                                        ReadOnly="True" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                                    <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr"
                                        SortExpression="EmpNameAr" meta:resourcekey="BoundFieldResource2" />
                                    <asp:BoundField HeaderText="Name (En)" DataField="EmpNameEn"
                                        SortExpression="EmpNameEn" meta:resourcekey="BoundFieldResource3" />
                                    <%--<asp:BoundField HeaderText="Card ID" DataField="EmpCardID" 
                                                                    SortExpression="EmpCardID" meta:resourcekey="BoundFieldResource4" />--%>
                                    <asp:BoundField HeaderText="Department (Ar)" DataField="DepNameAr"
                                        SortExpression="DepNameAr" meta:resourcekey="BoundFieldResource5" />
                                    <asp:BoundField HeaderText="Department (En)" DataField="DepNameEn"
                                        SortExpression="DepNameEn" meta:resourcekey="BoundFieldResource6" />
                                    <asp:TemplateField HeaderText="Status"
                                        meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <%# FindStatus(Eval("EmpID"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="ShowMsg" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                        EnableClientScript="False" CssClass="MsgValidation"
                        meta:resourcekey="vsumAllResource1" />
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col8">
                        <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click"
                            Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add" meta:resourcekey="btnAddResource1"></asp:LinkButton>

                        <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"
                            Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                            ValidationGroup="vsSave" meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                            OnClick="btnCancel_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                            Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                        
                                                <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                                    ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                                    EnableClientScript="False" ControlToValidate="txtValid">
                                                </asp:CustomValidator>

                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblExprType" runat="server" Text="Permission Type: "
                            meta:resourcekey="lblExprTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:RadioButtonList ID="rblExprType" runat="server" OnSelectedIndexChanged="rblExprType_SelectedIndexChanged"
                            AutoPostBack="True" meta:resourcekey="rblExprTypeResource1">
                            <asp:ListItem Value="Exc" meta:resourcekey="ListItemResource5">Daily Excuse</asp:ListItem>
                            <asp:ListItem Value="Vac" meta:resourcekey="ListItemResource6">Management permit</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvrblExprType" runat="server" ControlToValidate="rblExprType" CssClass="CustomValidator"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Permission Type is required!' /&gt;"
                            ValidationGroup="vsSave" meta:resourcekey="rfvrblExprTypeResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEmpID" runat="server" Text="Employee ID: "
                            meta:resourcekey="lblEmpIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpID" runat="server" Enabled="False"
                            meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvEmpID" runat="server" ControlToValidate="txtEmpID" CssClass="CustomValidator"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Employee ID is required!' /&gt;"
                            ValidationGroup="vsSave" meta:resourcekey="rvEmpIDResource1"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvCheckExcuseEmpID" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='this employee already has excuse today!' /&gt;"
                            ValidationGroup="vsSave" OnServerValidate="CheckExcuseEmpID_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                            ControlToValidate="txtValid"
                            meta:resourcekey="cvCheckExcuseEmpIDResource1"></asp:CustomValidator>
                    </div>
                </div>

                <div id="divExcType" runat="server" visible="False">
                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblPeriodExc" runat="server" Text="Period Excuse: "
                                meta:resourcekey="lblPeriodExcResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:RadioButtonList ID="rblPeriodExc" runat="server"
                                AutoPostBack="True" meta:resourcekey="rblPeriodExcResource1"
                                OnSelectedIndexChanged="rblPeriodExc_SelectedIndexChanged">
                                <asp:ListItem Value="BL" meta:resourcekey="lstItmEBLResource1">Begin Late</asp:ListItem>
                                <asp:ListItem Value="OE" meta:resourcekey="lstItmEOEResource1">Out Early</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvrblPeriodExc" runat="server" ControlToValidate="rblPeriodExc" CssClass="CustomValidator"
                                EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Period Excuse is required!' /&gt;"
                                ValidationGroup="vsSave" meta:resourcekey="rfvrblPeriodExcResource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblShiftID" runat="server" Text="Shift Name :"
                                meta:resourcekey="lblShiftIDResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlShiftID" runat="server"
                                meta:resourcekey="ddlShiftIDResource1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlShiftID" runat="server" ControlToValidate="ddlShiftID" CssClass="CustomValidator"
                                EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Shift Name is required!' /&gt;"
                                ValidationGroup="vsSave" meta:resourcekey="rfvddlShiftIDResource1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblExcType" runat="server" Text="Excuse Type: "
                                meta:resourcekey="lblExcTypeResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlExcType" runat="server"
                                meta:resourcekey="ddlExcTypeResource1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExcType" runat="server" ControlToValidate="ddlExcType" CssClass="CustomValidator"
                                EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Excuse Type is required!' /&gt;"
                                ValidationGroup="vsSave" meta:resourcekey="rfvddlExcTypeResource1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div id="divCheckNoPresent" runat="server" visible="False">
                    <div class="row">
                        <div class="col2">
                        </div>
                        <div class="col4">
                            <asp:CheckBox ID="chkVacNoPresent" runat="server"
                                Text="If no present, Set Management permit" AutoPostBack="True"
                                OnCheckedChanged="chkVacNoPresent_CheckedChanged"
                                meta:resourcekey="chkVacNoPresentResource1" />
                        </div>
                    </div>
                </div>
                <div id="divVacType" runat="server" visible="False">
                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblVacType" runat="server" Text="Permit Type: "
                                meta:resourcekey="lblVacTypeResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlVacType" runat="server" Enabled="false"
                                meta:resourcekey="ddlVacTypeResource1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlVacType" runat="server" ControlToValidate="ddlVacType"
                                EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Permit Type is required!' /&gt;"
                                ValidationGroup="vsSave" meta:resourcekey="rfvddlVacTypeResource1" CssClass="CustomValidator"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

          
            
                <div class="row">
                    <div class="col2">
                        <%--<span class="RequiredField">*</span>--%>
                        <asp:Label ID="lblDesc" runat="server" Text="Reason: "
                            meta:resourcekey="lblDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtDesc" runat="server" Height="60px"
                            meta:resourcekey="txtDescResource1"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvtxtDesc" runat="server" ControlToValidate="txtDesc"
                                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Reason Field is required!' /&gt;"
                                            ValidationGroup="vsSave" meta:resourcekey="rfvtxtDescResource1"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                    </div>

                </div>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
