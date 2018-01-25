<%@ Page Title="Employee Master" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeMaster.aspx.cs" Inherits="EmployeeMaster" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="TextTimeServerControl" Namespace="TextTimeServerControl" TagPrefix="Almaalim" %>
<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <script type="text/javascript" src="../Script/DivPopup.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <%--script--%>
    <link href="../CSS/PopupStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search by:" meta:resourcekey="lblSearchByResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server"
                        meta:resourcekey="ddlSearchByResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Employee ID" Value="EmpID"
                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (Ar)" Value="EmpNameAr"
                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (En)" Value="EmpNameEn"
                            meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="Department Name (Ar)" Value="DepNameAr"
                            meta:resourcekey="ListItemResource5"></asp:ListItem>
                        <asp:ListItem Text="Department Name (En)" Value="DepNameEn"
                            meta:resourcekey="ListItemResource6"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server"
                        meta:resourcekey="txtSearchResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" CssClass="LeftOverlay"
                        ImageUrl="../images/Button_Icons/button_magnify.png" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"/>
                    <%--<div style="width: 800px">--%>
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False" AllowPaging="True"
                        CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="EmpID"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnRowDataBound="grdData_RowDataBound"
                        OnSorting="grdData_Sorting" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnRowCommand="grdData_RowCommand" ShowFooter="True"
                        OnRowCreated="grdData_RowCreated" EnableModelValidation="True"
                        meta:resourcekey="grdDataResource1" OnPreRender="grdData_PreRender">
                        <SelectedRowStyle BackColor="#87A3B1" Font-Bold="True" ForeColor="#333333" />
                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                            NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="EmpID" SortExpression="EmpID"
                                InsertVisible="False" ReadOnly="True" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                            <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr"
                                SortExpression="EmpNameAr" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField HeaderText="Name (En)" DataField="EmpNameEn"
                                SortExpression="EmpNameEn" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField HeaderText="Card ID" DataField="EmpCardID"
                                SortExpression="EmpCardID" meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField HeaderText="Department (Ar)" DataField="DepNameAr"
                                SortExpression="DepNameAr" meta:resourcekey="BoundFieldResource5" />
                            <asp:BoundField HeaderText="Department (En)" DataField="DepNameEn"
                                SortExpression="DepNameEn" meta:resourcekey="BoundFieldResource6" />
                            <asp:TemplateField HeaderText="           "
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" Enabled="False" CommandName="Delete1" CommandArgument='<%# Eval("EmpID") %>'
                                        runat="server" ImageUrl="../images/Button_Icons/button_delete.png"
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

            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign"
                        meta:resourcekey="btnAddResource1" OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" meta:resourcekey="btnModifyResource1" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"
                        meta:resourcekey="btnSaveResource1" OnClick="btnSave_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        ValidationGroup="vgSave"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" meta:resourcekey="btnCancelResource1" OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"></asp:LinkButton>

                    <asp:LinkButton ID="btnResetPassword" runat="server" CssClass="GenButton glyphicon glyphicon-refresh" meta:resourcekey="btnResetPasswordResource1"
                        OnClick="btnResetPassword_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_reset_Password.png&quot; /&gt; Reset Password"></asp:LinkButton>

                    <asp:LinkButton ID="btnVacSet" runat="server" CssClass="GenButton glyphicon glyphicon-sort-by-order-alt" meta:resourcekey="btnVacSetResource1" OnClick="btnVacSet_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_reset_Password.png&quot; /&gt; Set Vacation"></asp:LinkButton>

                    <%--<ajaxToolkit:AnimationExtender ID="AnimationExtenderShow5" runat="server" 
                        TargetControlID="lnkShow5" />
                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose5" runat="server" 
                        TargetControlID="lnkClose5" />
                    <asp:ImageButton ID="lnkShow5" runat="server" 
                        ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" OnClientClick="return false;" />
                    <div ID="pnlInfo5" class="flyOutDiv">
                        <asp:LinkButton ID="lnkClose5" runat="server" CssClass= "flyOutDivCloseX glyphicon glyphicon-remove" 
                            OnClientClick="return false;" Text="X" />
                        <p>
                            <br />
                            <asp:Label ID="lblHint5" runat="server" meta:resourcekey="lblHint5Resource1" 
                                Text="set maximum of days for vacations to employee"></asp:Label>
                        </p>
                    </div>--%>
                </div>
            </div>
            
            <div class="GreySetion">
                <div class="row">
                    <div class="col12">
                        <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                            Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                        <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                            ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid">
                        </asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEmployeeID" runat="server" Text="Employee ID:" meta:resourcekey="lblEmployeeIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmployeeID" runat="server" Enabled="False"
                            meta:resourcekey="txtEmployeeIDResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvEmployeeID" runat="server" ControlToValidate="txtEmployeeID" CssClass="CustomValidator"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Employee ID is required!' /&gt;"
                            ValidationGroup="vgSave"
                            meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvEmployeeIDLen" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='Must consist National ID of 10 digits' /&gt;" CssClass="CustomValidator"
                            ValidationGroup="vgSave" ErrorMessage="Must consist National ID of 10 digits"
                            OnServerValidate="EmployeeIDLen_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid"
                            meta:resourcekey="cvEmployeeIDLenResource1">
                        </asp:CustomValidator>
                        <asp:CustomValidator ID="cvEmpID" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;" CssClass="CustomValidator"
                            ValidationGroup="vgSave"
                            OnServerValidate="EmpID_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid">
                        </asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblCardID" runat="server" Text="Card ID:"
                            meta:resourcekey="lblCardIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtCardID" runat="server" Enabled="False"
                            meta:resourcekey="txtCardIDResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span id="spnEmpNameAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblEmpNameAr" runat="server" Text="Employee Name (Ar):"
                            meta:resourcekey="lblEmpNameArResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpNameAr" runat="server" Enabled="False"
                            meta:resourcekey="txtEmpNameArResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvEmpNameAr" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Employee name (AR) is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="EmpNameValidate_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid" CssClass="CustomValidator"
                            meta:resourcekey="cvEmpNameArResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnEmpNameEn" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblEmpNameEn" runat="server" Text="Employee Name (En):"
                            meta:resourcekey="lblEmpNameEnResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpNameEn" runat="server" Enabled="False"
                            meta:resourcekey="txtEmpNameEnResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvEmpNameEn" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Employee Name (En) is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="EmpNameValidate_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid" CssClass="CustomValidator"
                            meta:resourcekey="cvEmpNameEnResource1"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblDepartment" runat="server" Text="Department:"
                            meta:resourcekey="lblDepartmentResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlDepartment" runat="server" Enabled="False"
                            meta:resourcekey="ddlDepartmentResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlDep" runat="server" ControlToValidate="ddlDepartment" CssClass="CustomValidator"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Department is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rfvddlDepResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblJobTitleAr" runat="server" Text="Job Title Ar:"
                            meta:resourcekey="lblJobTitleArResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtJobTitleAR" runat="server" Enabled="False"
                            meta:resourcekey="txtJobTitleARResource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblJobTitleEn" runat="server" Text="Job Title En:"
                            meta:resourcekey="lblJobTitleEnResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtJobTitleEn" runat="server" Enabled="False"
                            meta:resourcekey="txtJobTitleEnResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblJoinDate" runat="server" Text="Joining Date:"
                            meta:resourcekey="lblJoinDateResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calJoinDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calLeaveDate" />
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblLeaveDate" runat="server" Text="Leave Date:" meta:resourcekey="lblLeaveDateResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calLeaveDate" runat="server" CalendarType="System" />
                    </div>
                </div>

                <div class="row" id="divNationalID" runat="server">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblNationalID" runat="server" Text="National ID:" meta:resourcekey="lblNationalIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNationalID" runat="server" Enabled="False" AutoCompleteType="Disabled"
                            meta:resourcekey="txtNationalIDResource1" onkeypress="return OnlyNumber(event);"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revNationalID" runat="server" ControlToValidate="txtNationalID"
                            EnableClientScript="False" ErrorMessage=" Enter Only Numbers" ValidationExpression="^\d+$" CssClass="CustomValidator"
                            ValidationGroup="vgSave"
                            meta:resourcekey="RegularExpressionValidator1Resource1"><img src="../images/Exclamation.gif" title="Enter Only Numbers!" />
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rvfNationalID" runat="server" ControlToValidate="ddlDepartment" CssClass="CustomValidator"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='National ID is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rvfNationalIDResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblEmpRankTitel2" runat="server" Text="Rank Titel :" meta:resourcekey="lblEmpRankTitelResource1" Visible="false"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpRankTitel" runat="server" Enabled="False" Width="168px" meta:resourcekey="txtEmpRankTitelResource1" Visible="false"></asp:TextBox>
                    </div>
                </div>

                <div id="divRanks" runat="server" class="row">
                    <div class="col2">
                        <asp:Label ID="lblEmpRankTitel" runat="server" Text="Rank Titel :" meta:resourcekey="lblEmpRankTitelResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlRanks" runat="server" Enabled="False" meta:resourcekey="ddlRanksResource1">
                        </asp:DropDownList>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblEmpRankDesc" runat="server" Text="Rank Description :" meta:resourcekey="lblEmpRankDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpRankDesc" runat="server" Enabled="False" Width="168px" meta:resourcekey="txtEmpRankDescResource1"></asp:TextBox>
                    </div>

                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblBlood" runat="server" Text="Blood Group :"
                            meta:resourcekey="lblBloodResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlBlood" runat="server" Enabled="False"
                            meta:resourcekey="ddlBloodResource1">
                            <asp:ListItem Text="-Select blood" meta:resourcekey="ListItemResource7"></asp:ListItem>
                            <asp:ListItem Text="A+" Value="A+" meta:resourcekey="ListItemResource8"></asp:ListItem>
                            <asp:ListItem Text="A-" Value="A-" meta:resourcekey="ListItemResource9"></asp:ListItem>
                            <asp:ListItem Text="B+" Value="B+" meta:resourcekey="ListItemResource10"></asp:ListItem>
                            <asp:ListItem Text="B-" Value="B-" meta:resourcekey="ListItemResource11"></asp:ListItem>
                            <asp:ListItem Text="AB+" Value="AB+" meta:resourcekey="ListItemResource12"></asp:ListItem>
                            <asp:ListItem Text="AB-" Value="AB-" meta:resourcekey="ListItemResource13"></asp:ListItem>
                            <asp:ListItem Text="O+" Value="O+" meta:resourcekey="ListItemResource14"></asp:ListItem>
                            <asp:ListItem Text="O-" Value="O-" meta:resourcekey="ListItemResource15"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblCategory" runat="server" Text="Job Category:"
                            meta:resourcekey="lblCategoryResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlCategory" runat="server" Enabled="False"
                            meta:resourcekey="ddlCategoryResource1">
                        </asp:DropDownList>
                    </div>

                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblNationality" runat="server" Text="Nationality:"
                            meta:resourcekey="lblNationalityResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlNationality" runat="server" Enabled="False" meta:resourcekey="ddlNationalityResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlNationality" runat="server" ControlToValidate="ddlNationality" CssClass="CustomValidator"
                            EnableClientScript="False" ErrorMessage="Nationality is required!" Text="&lt;img src='../images/Exclamation.gif' title='Nationality is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rfvddlNationalityResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblEmpTypeID" runat="server" Text="Employee Type:"
                            meta:resourcekey="lblEmpTypeIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlEmpTypeID" runat="server" Enabled="False" meta:resourcekey="ddlEmpTypeIDResource1">
                        </asp:DropDownList>
                    </div>

                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblAttenRule" runat="server" Text="Attendance Rule Set:"
                            meta:resourcekey="lblAttenRuleResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlAttenRule" runat="server" Enabled="False" meta:resourcekey="ddlAttenRuleResource1">
                        </asp:DropDownList>

                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow3" runat="server" TargetControlID="lnkShow3" />
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose3" runat="server" TargetControlID="lnkClose3" />

                        <asp:ImageButton ID="lnkShow3" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />

                        <div id="pnlInfo3" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose3" runat="server" Text="X" OnClientClick="return false;"
                                CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint3" runat="server" Text="set attendance rule and action for Employee" meta:resourcekey="lblHint3Resource1"></asp:Label>
                            </p>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblGender" runat="server" Text="Gender:"
                            meta:resourcekey="lblGenderResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" Enabled="false"
                            meta:resourcekey="rblGenderResource1">
                            <asp:ListItem Value="M" meta:resourcekey="ListItemResource16">Male</asp:ListItem>
                            <asp:ListItem Value="F" meta:resourcekey="ListItemResource17">Female</asp:ListItem>
                        </asp:RadioButtonList>

                        <asp:CustomValidator ID="cvGender" runat="server"
                            Text="&lt;img src='../images/Exclamation.gif' title='Gender is required!' /&gt;"
                            ValidationGroup="vgSave"
                            OnServerValidate="Gender_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                            ControlToValidate="txtValid" meta:resourcekey="cvGenderResource1">
                        </asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span id="spnEmail" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblEmail" runat="server" Text="Email:"
                            meta:resourcekey="lblEmailResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmail" runat="server" Enabled="False"
                            meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNationalID"
                            EnableClientScript="False" ErrorMessage="Enter Correct Email ID!" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)"
                            ValidationGroup="vgSave" 
                                meta:resourcekey="RegularExpressionValidator1Resource2"><img src="../images/Exclamation.gif" title="Enter Correct Email ID!" />
                        </asp:RegularExpressionValidator>--%>
                        <asp:CustomValidator ID="cvEmail" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Email is required!' /&gt;"
                            ValidationGroup="vgSave" ErrorMessage="Email is required!" CssClass="CustomValidator"
                            OnServerValidate="EamilMobileRequired_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkSendEmailAlert" runat="server" Enabled="False"
                            Text="Send Email alert" meta:resourcekey="chkSendEmailAlertResource1" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span id="spnMobileNo" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No:"
                            meta:resourcekey="lblMobileNoResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtMobileNo" runat="server" Enabled="False"
                            meta:resourcekey="txtMobileNoResource1"></asp:TextBox>

                        <asp:CustomValidator ID="cvMobile" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Mobile is required!' /&gt;"
                            ValidationGroup="vgSave" ErrorMessage="Mobile is required!"
                            OnServerValidate="EamilMobileRequired_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                            ControlToValidate="txtValid"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkSendSMSAlert" runat="server" Enabled="False"
                            Text="Send SMS alert" meta:resourcekey="chkSendSMSAlertResource1" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblEmpADUser" runat="server" Text="Active Directory User :" meta:resourcekey="lblEmpADUserResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpADUser" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtEmpADUserResource1"></asp:TextBox>
                        <asp:ImageButton ID="btnGetADUsr" runat="server" OnClick="btnGetADUsr_Click" Enabled="false" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                            meta:resourcekey="btnGetADUsrResource1" />
                        <asp:CustomValidator ID="cvADUser" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='Active Directory User already exists!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="ADUser_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvADUserResource11"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkStatus" runat="server" Enabled="False" Text="Active"
                            meta:resourcekey="chkStatusResource1" />
                    </div>
                </div>

                <%--<tr>
                    <td class="td1Allalign">
                        <asp:Label ID="lblDomainUsername" runat="server" Text="Domain Username:" 
                            meta:resourcekey="lblDomainUsernameResource1"></asp:Label>
                    </td>
                    <td class="td2Allalign">
                        <asp:TextBox ID="txtDomainUsername" runat="server" Enabled="false" Width="168px" 
                            meta:resourcekey="txtDomainUsernameResource1"></asp:TextBox>

                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow2" runat="server" TargetControlID="lnkShow2"/>       
                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose2" runat="server" TargetControlID="lnkClose2"/>

                    <asp:ImageButton ID="lnkShow2" runat="server" OnClientClick="return false;" ImageUrl = "~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />

                    <div id="pnlInfo2" class="flyOutDiv">
                        <asp:LinkButton ID="lnkClose2" runat="server" Text="X" OnClientClick="return false;"
                            CssClass= "flyOutDivCloseX glyphicon glyphicon-remove" />
                        <p>
                            <br />
                            <asp:Label ID="lblHint2" runat="server" Text="You can put domain username for Pc to login directly to system" meta:resourcekey="lblHint2Resource1"></asp:Label>
                        </p>
                    </div>
                    </td>
                    <td>
                    </td>
                    
                </tr>--%>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblMaxOverTimePercent" runat="server"
                            Text="Maximum Overtime Hours:" meta:resourcekey="lblMaxOverTimePercentResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtMaxPercentOT" runat="server" FormatTime="hhmm"
                            meta:resourcekey="txtMaxPercentOTResource1" />

                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1" />
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1" />

                        <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />

                        <div id="pnlInfo1" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;"
                                CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint1" runat="server" Text="set maximum of hours for overtime that employee can get it" meta:resourcekey="lblHint1Resource1"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblFavLang" runat="server" Text="Favorite Language :"
                            meta:resourcekey="Label1Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlLanguage" runat="server"
                            Enabled="False" meta:resourcekey="ddlLanguageResource1">

                            <asp:ListItem Text="العربية" Value="AR" meta:resourcekey="ListItemResource19"></asp:ListItem>
                            <asp:ListItem Text="English" Value="EN" meta:resourcekey="ListItemResource20"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkAutoIN" runat="server" Enabled="False" Text="Auto IN"
                            AutoPostBack="True" OnCheckedChanged="chkAutoIN_CheckedChanged"
                            meta:resourcekey="chkAutoINResource1" />

                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow6" runat="server" TargetControlID="lnkShow6" />
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose6" runat="server" TargetControlID="lnkClose6" />

                        <asp:ImageButton ID="lnkShow6" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />

                        <div id="pnlInfo6" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose6" runat="server" Text="X" OnClientClick="return false;"
                                CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint6" runat="server" Text="you can set this option to add automatic trans IN automatically" meta:resourcekey="lblHint6Resource1"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkAutoOut" runat="server" Enabled="False" Text="Auto Out"
                            OnCheckedChanged="chkAutoOut_CheckedChanged" AutoPostBack="True"
                            meta:resourcekey="chkAutoOutResource1" />

                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow4" runat="server" TargetControlID="lnkShow4" />
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose4" runat="server" TargetControlID="lnkClose4" />

                        <asp:ImageButton ID="lnkShow4" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />

                        <div id="pnlInfo4" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose4" runat="server" Text="X" OnClientClick="return false;"
                                CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint4" runat="server" Text="you can set this option to add automatic trans OUT automatically" meta:resourcekey="lblHint4Resource1"></asp:Label>
                            </p>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkHasFingerPrint" runat="server" Enabled="False" Visible="false"
                            Text="Has FingerPrint" meta:resourcekey="chkHasFingerPrintResource1" />

                    </div>
                    <div class="col2">
                        <asp:Label ID="lblEmpDescription" runat="server" Text="Employee Description:"
                            meta:resourcekey="lblEmpDescriptionResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpDesc" runat="server" Enabled="False" Height="50px"
                            meta:resourcekey="txtEmpDescResource1" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>

            </div>

            <div id='divBackground'></div>
            <div id='divPopup' class="divPopup" style="height: 550px; width: 760px;">
                <div id='divPopupHead' class="divPopupHead">
                    <asp:Label ID="lblNamePopup" runat="server" CssClass="lblNamePopup"></asp:Label></div>
                <div id='divClosePopup' class="divClosePopup<%=Session["Language"]%>" onclick="hidePopup('divPopup')"><ahref='#'>X</a></div>
                <div id='divPopupContent' class="divPopupContent">
                    <center>
                      <iframe id="ifrmPopup" runat="server"  height="550px" width="750px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
