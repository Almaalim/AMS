<%@ Page Title="Rotation Shift" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="RotationShift.aspx.cs" Inherits="RotationShift" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="~/Control/EmployeeSelectedGroup.ascx" TagPrefix="uc" TagName="EmployeeSelectedGroup" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <%--script--%>

    <%--Style--%>
    <link href="../CSS/WizardStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />
    <%--Style--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:"
                        meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server"
                        meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Rotation Shift Name (Ar)" Value="WktNameAr"
                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Rotation Shift Name (En)" Value="WktNameEn"
                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled"
                        meta:resourcekey="txtFilterResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" CssClass="LeftOverlay"
                        ImageUrl="../images/Button_Icons/button_magnify.png"
                        meta:resourcekey="btnFilterResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender ID="gridviewextender" runat="server"
                        TargetControlID="grdData" NextRowSelectKey="Add"
                        PrevRowSelectKey="Subtract" />
                    <asp:GridView ID="grdData" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" BorderWidth="0px" CellPadding="0"
                        CssClass="datatable" DataKeyNames="WktID" GridLines="None"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnPreRender="grdData_PreRender"
                        OnRowCommand="grdData_RowCommand" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        SelectedIndex="0" ShowFooter="True"
                        meta:resourcekey="grdDataResource1">

                        <PagerSettings FirstPageImageUrl="~/images/first.png" FirstPageText="First"
                            LastPageImageUrl="~/images/last.png" LastPageText="Last"
                            Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/next.png"
                            NextPageText="Next" PreviousPageImageUrl="~/images/prev.png"
                            PreviousPageText="Prev" />
                        <Columns>
                            <asp:BoundField DataField="WktNameAr" HeaderText="Name (Ar)"
                                SortExpression="WktNameAr" meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                            <asp:BoundField DataField="WktID" HeaderText="ID" SortExpression="WktID"
                                meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="WktNameEn" HeaderText="Name (En)"
                                SortExpression="WktNameEn" meta:resourcekey="BoundFieldResource3" />
                            <asp:TemplateField HeaderText="Start Date" SortExpression="WktShift1From"
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("WktShift1From"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" SortExpression="WktShift1To"
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("WktShift1To"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="WktShift1Duration"
                                HeaderText="Duration (Days)"
                                SortExpression="WktShift1Duration" meta:resourcekey="BoundFieldResource4">
                                <HeaderStyle CssClass="first" />
                                <ItemStyle CssClass="first" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="           "
                                meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server"
                                        CommandArgument='<%# Eval("WktID") %>' CommandName="Delete1"
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
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign"
                        OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                        meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                        OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                    &nbsp;
                                        <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                            ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                            EnableClientScript="False" ControlToValidate="txtValid">
                                        </asp:CustomValidator>
                </div>
            </div>

            <div class="GreySetion">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="viw" runat="server">

                        <div class="row">
                            <div class="col2">

                                <asp:Label ID="lblWktNameAr" runat="server" Text="Name (Ar) :"
                                    meta:resourcekey="lblWktNameArResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtWktNameAr" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False" meta:resourcekey="txtWktNameArResource1"></asp:TextBox>
                            </div>
                            <div class="col2">

                                <asp:Label ID="lblWktNameEn" runat="server" Text="Name (En)"
                                    meta:resourcekey="lblWktNameEnResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtWktNameEn" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False" meta:resourcekey="txtWktNameEnResource1"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblDuration" runat="server" Text="No. of Days :"
                                    meta:resourcekey="lblDurationResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtDuration" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False" meta:resourcekey="txtDurationResource1"></asp:TextBox>
                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1"></ajaxToolkit:AnimationExtender>
                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1"></ajaxToolkit:AnimationExtender>
                                <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                <div id="pnlInfo1" class="flyOutDiv">
                                    <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                    <p>
                                        <br />
                                        <asp:Label ID="lblHint1" runat="server" Text="You can initialize here" meta:resourcekey="lblHint1Resource"></asp:Label>
                                    </p>
                                </div>
                                </td>
                            </div>
                            <div class="col2">
                            </div>
                            <div class="col4">
                                <asp:CheckBox ID="chkRotOnlyWorkDays" runat="server" Enabled="False"
                                    Text="Rotation on Working Days only"
                                    meta:resourcekey="chkRotOnlyWorkDaysResource1" />
                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow2" runat="server" TargetControlID="lnkShow2"></ajaxToolkit:AnimationExtender>
                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose2" runat="server" TargetControlID="lnkClose2"></ajaxToolkit:AnimationExtender>
                                <asp:ImageButton ID="lnkShow2" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                <div id="pnlInfo2" class="flyOutDiv">
                                    <asp:LinkButton ID="lnkClose2" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                    <p>
                                        <br />
                                        <asp:Label ID="lblHint2" runat="server" Text="You can initialize here" meta:resourcekey="lblHint2Resource"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="Label1" runat="server" Text="Working days :"
                                    meta:resourcekey="Label1Resource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:CheckBox ID="chkEwrSat" runat="server" Enabled="False" Text="Saturday"
                                    meta:resourcekey="chkEwrSatResource1" />

                                <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrSun" runat="server" Enabled="False" Text="Sunday"
                                        meta:resourcekey="chkEwrSunResource1" />
                                </span>
                                <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrMon" runat="server" Enabled="False" Text="Monday"
                                        meta:resourcekey="chkEwrMonResource1" />
                                </span>
                                <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrTue" runat="server" Enabled="False" Text="Tuesday"
                                        meta:resourcekey="chkEwrTueResource1" />
                                </span>
                                <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrWed" runat="server" Enabled="False" Text="Wednesday"
                                        meta:resourcekey="chkEwrWedResource1" />
                                </span>
                                <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrThu" runat="server" Enabled="False" Text="Thursday"
                                        meta:resourcekey="chkEwrThuResource1" />
                                </span>
                                <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrFri" runat="server" Enabled="False" Text="Friday"
                                        meta:resourcekey="chkEwrFriResource1" />
                                </span>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblStartDate" runat="server" Text="Start Date :"
                                    meta:resourcekey="lblStartDateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" />
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblEndDate" runat="server" Text="End Date :"
                                    meta:resourcekey="lblEndDateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblWorkTime" runat="server" Text="Work Time :"
                                    meta:resourcekey="lblWorkTimeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:ListBox ID="lstWorkTime" runat="server" Height="200px"
                                    meta:resourcekey="lstWorkTimeResource1"></asp:ListBox>
                            </div>

                            <div class="col2">
                                <asp:Label ID="lblRotationGroup" runat="server" Text="Rotation Group :"
                                    meta:resourcekey="lblRotationGroupResource1"></asp:Label>
                            </div>

                            <div class="col4">
                                <asp:ListBox ID="lstRotationGroup" runat="server" AutoPostBack="True"
                                    Height="200px" OnSelectedIndexChanged="lstRotationGroup_SelectedIndexChanged"
                                    meta:resourcekey="lstRotationGroupResource1"></asp:ListBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblEmployeeGroup" runat="server" Text="Employee Group :"
                                    meta:resourcekey="lblEmployeeGroupResource1"></asp:Label>
                            </div>

                            <div class="col4">
                                <asp:ListBox ID="lstEmployeeGroup" runat="server" Height="200px"
                                    meta:resourcekey="lstEmployeeGroupResource1"></asp:ListBox>
                            </div>
                        </div>



                    </asp:View>
                    <asp:View ID="viw0" runat="server">

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="block">
                            <ContentTemplate>

                                <asp:Wizard ID="WizardData" runat="server" ActiveStepIndex="3"
                                    DisplaySideBar="False" OnActiveStepChanged="WizardData_ActiveStepChanged"
                                    OnPreRender="WizardData_PreRender"
                                    meta:resourcekey="WizardDataResource1" Width="100%">
                                    <StartNavigationTemplate>
                                        <div class="row">
                                            <div class="col12">
                                                <asp:ImageButton ID="btnStartStep" runat="server" OnClick="btnStartStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_next.png" ValidationGroup="VGStart"
                                                    ToolTip="Next" meta:resourcekey="btnStartStepResource1" />
                                            </div>
                                        </div>
                                    </StartNavigationTemplate>
                                    <StepNavigationTemplate>
                                        <div class="row">
                                            <div class="col12 center-block">
                                                <asp:ImageButton ID="btnBackStep" runat="server" OnClick="btnBackStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_previous.png" ToolTip="Back"
                                                    meta:resourcekey="btnBackStepResource1" />

                                                <asp:ImageButton ID="btnNextStep" runat="server" OnClick="btnNextStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_next.png" ValidationGroup="VGStep1"
                                                    ToolTip="Next" meta:resourcekey="btnNextStepResource1" />
                                            </div>
                                        </div>
                                    </StepNavigationTemplate>
                                    <FinishNavigationTemplate>
                                        <div class="row">
                                            <div class="col12 center-block">
                                                <asp:ImageButton ID="btnFinishBackStep" runat="server"
                                                    OnClick="btnFinishBackStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_previous.png" ToolTip="Back"
                                                    meta:resourcekey="btnFinishBackStepResource1" />

                                                <asp:ImageButton ID="btnFinishStep" runat="server"
                                                    OnClick="btnSaveFinish_Click" ImageUrl="../images/Wizard_Image/save.png"
                                                    ToolTip="Save" ValidationGroup="VGFinish"
                                                    meta:resourcekey="btnFinishStepResource1" />
                                            </div>
                                        </div>
                                    </FinishNavigationTemplate>
                                    <WizardSteps>
                                        <asp:WizardStep ID="WizardStepStart" runat="server" Title="1-Option"
                                            meta:resourcekey="WizardStepStartResource1">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="VSStart" runat="server" CssClass="MsgValidation"
                                                        EnableClientScript="False" ValidationGroup="VGStart"
                                                        meta:resourcekey="VSStartResource1" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span id="spnWktNameEn_viw0" runat="server" visible="False"
                                                        class="RequiredField">*</span>
                                                    <asp:Label ID="lblWktNameEn_viw0" runat="server" Text="Name (En) :"
                                                        meta:resourcekey="lblWktNameEn_viw0Resource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="txtWktNameEn_viw0" runat="server" AutoCompleteType="Disabled"
                                                        meta:resourcekey="txtWktNameEn_viw0Resource1"></asp:TextBox>
                                                    <asp:CustomValidator ID="cvtxtWktNameEn_viw0" runat="server"
                                                        ControlToValidate="txtCustomValidator" EnableClientScript="False"
                                                        ErrorMessage="English Name already exists,Please enter different Name!"
                                                        OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='English Name already exists,Please enter different Name!' /&gt;"
                                                        ValidationGroup="VGStart" meta:resourcekey="cvtxtWktNameEn_viw0Resource1"></asp:CustomValidator>
                                                    <asp:TextBox ID="txtCustomValidator" runat="server" Text="02120"
                                                        Visible="False" Width="10px"
                                                        meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                                </div>
                                                <div class="col2">
                                                    <span id="spnWktNameAr_viw0" runat="server" visible="False"
                                                        class="RequiredField">*</span>
                                                    <asp:Label ID="lblWktNameAr_viw0" runat="server" Text="Name (Ar) :"
                                                        meta:resourcekey="lblWktNameAr_viw0Resource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="txtWktNameAr_viw0" runat="server" AutoCompleteType="Disabled"
                                                        meta:resourcekey="txtWktNameAr_viw0Resource1"></asp:TextBox>
                                                    <asp:CustomValidator ID="cvtxtWktNameAr_viw0" runat="server"
                                                        ControlToValidate="txtCustomValidator" EnableClientScript="False"
                                                        OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='Arabic Name already exists,Please enter different Name!' /&gt;"
                                                        ValidationGroup="VGStart" meta:resourcekey="cvtxtWktNameAr_viw0Resource1"></asp:CustomValidator>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="Label3" runat="server" Text="Duration :"
                                                        meta:resourcekey="Label3Resource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="txtDuration_viw0" runat="server" AutoCompleteType="Disabled"
                                                        onkeypress="return OnlyNumber(event);"
                                                        meta:resourcekey="txtDuration_viw0Resource1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="txtDuration_viw0" EnableClientScript="False" CssClass="CustomValidator"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='Duration is required!' /&gt;"
                                                        ValidationGroup="VGStart"
                                                        meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                        ControlToValidate="txtDuration_viw0" EnableClientScript="False"
                                                        ErrorMessage=" Enter Only Numbers" ValidationExpression="^\d+$" CssClass="CustomValidator"
                                                        ValidationGroup="VGStart"
                                                        meta:resourcekey="RegularExpressionValidator1Resource1"><img src="../images/Exclamation.gif" title="Enter Only Numbers!" />
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col2">
                                                </div>
                                                <div class="col4">
                                                    <asp:CheckBox ID="chkRotOnlyWorkDays_viw0" runat="server"
                                                        Text="Rotation Only Work Days"
                                                        meta:resourcekey="chkRotOnlyWorkDays_viw0Resource1" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblWorkingdays" runat="server"
                                                        Text="Working days :" meta:resourcekey="lblWorkingdaysResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:CheckBox ID="chkEwrSat_viw0" runat="server" Text="Saturday"
                                                        meta:resourcekey="chkEwrSat_viw0Resource1" />
                                                    <span class="form-inline">
                                                        <asp:CheckBox ID="chkEwrSun_viw0" runat="server" Text="Sunday"
                                                            meta:resourcekey="chkEwrSun_viw0Resource1" />
                                                    </span>
                                                    <span class="form-inline">
                                                        <asp:CheckBox ID="chkEwrMon_viw0" runat="server" Text="Monday"
                                                            meta:resourcekey="chkEwrMon_viw0Resource1" />
                                                    </span>
                                                    <span class="form-inline">
                                                        <asp:CheckBox ID="chkEwrTue_viw0" runat="server" Text="Tuesday"
                                                            meta:resourcekey="chkEwrTue_viw0Resource1" />
                                                    </span>
                                                    <span class="form-inline">
                                                        <asp:CheckBox ID="chkEwrWed_viw0" runat="server" Text="Wednesday"
                                                            meta:resourcekey="chkEwrWed_viw0Resource1" />
                                                    </span>
                                                    <span class="form-inline">
                                                        <asp:CheckBox ID="chkEwrThu_viw0" runat="server" Text="Thursday"
                                                            meta:resourcekey="chkEwrThu_viw0Resource1" />
                                                    </span>
                                                    <span class="form-inline">
                                                        <asp:CheckBox ID="chkEwrFri_viw0" runat="server" Text="Friday"
                                                            meta:resourcekey="chkEwrFri_viw0Resource1" />
                                                    </span>

                                                    <asp:CustomValidator ID="cvSelectWorkDays_viw0" runat="server"
                                                        ControlToValidate="txtCustomValidator" EnableClientScript="False"
                                                        ErrorMessage="Select Work Days" CssClass="CustomValidator"
                                                        OnServerValidate="SelectWorkDays_ServerValidate"
                                                        Text="&lt;img src='../images/message_exclamation.png' title='Select Work Days!' /&gt;"
                                                        ValidationGroup="VGStart" meta:resourcekey="cvSelectWorkDays_viw0Resource1"></asp:CustomValidator>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="Label4" runat="server" Text="Start Date :"
                                                        meta:resourcekey="Label4Resource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <Cal:Calendar2 ID="calStartDate_viw0" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="VGStart" CalTo="calEndDate_viw0" />
                                                </div>
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="Label5" runat="server" Text="End Date :"
                                                        meta:resourcekey="Label5Resource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <Cal:Calendar2 ID="calEndDate_viw0" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="VGStart" />
                                                </div>
                                            </div>

                                            </div>
                                        </asp:WizardStep>
                                        <asp:WizardStep ID="WizardStep1" runat="server" Title="2-Work Time"
                                            meta:resourcekey="WizardStep1Resource1">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="VSStep1" runat="server" CssClass="MsgValidation"
                                                        EnableClientScript="False" ValidationGroup="VGStep1"
                                                        meta:resourcekey="VSStep1Resource1" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="Label6" runat="server" Text="Work Time :"
                                                        meta:resourcekey="Label6Resource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:DropDownList ID="ddlWktID" runat="server"
                                                        meta:resourcekey="ddlWktIDResource1">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col2">
                                                </div>
                                                <div class="col4">
                                                    <asp:ListBox ID="lstWorkTime_viw1" runat="server" Height="200px"
                                                        meta:resourcekey="lstWorkTime_viw1Resource1"></asp:ListBox>
                                                    <asp:CustomValidator ID="cvlstWorkTime_viw1" runat="server"
                                                        ControlToValidate="txtCustomValidator" EnableClientScript="False"
                                                        OnServerValidate="lstWorkTime_viw1_ServerValidate"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='Work Time is required!' /&gt;"
                                                        ValidationGroup="VGStep1" meta:resourcekey="rfvlstWorkTime_viw1Resource1">
                                                    </asp:CustomValidator>


                                                    <%--<asp:RequiredFieldValidator ID="rfvlstWorkTime_viw1" runat="server" 
                                                                                                        ControlToValidate="lstWorkTime_viw1" EnableClientScript="False" 
                                                                                                        Text="&lt;img src='../images/Exclamation.gif' title='Work Time is required!' /&gt;" 
                                                                                                        ValidationGroup="VGStep1" meta:resourcekey="rfvlstWorkTime_viw1Resource1" ></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2"></div>
                                                <div class="col4">
                                                    <asp:ImageButton ID="btnAdd_viw1" runat="server" OnClick="btnAdd_viw1_Click"
                                                        ImageUrl="../images/Wizard_Image/add.png" ToolTip="Add"
                                                        meta:resourcekey="btnAdd_viw1Resource1" />

                                                    <asp:ImageButton ID="btnUP_viw1" runat="server" OnClick="btnUP_viw1_Click"
                                                        ImageUrl="../images/Wizard_Image/arrow_up.png" ToolTip="Up"
                                                        meta:resourcekey="btnUP_viw1Resource1" />

                                                    <asp:ImageButton ID="btnDOWN_viw1" runat="server" OnClick="btnDOWN_viw1_Click"
                                                        ImageUrl="../images/Wizard_Image/arrow_down.png" ToolTip="Down"
                                                        meta:resourcekey="btnDOWN_viw1Resource1" />

                                                    <asp:ImageButton ID="btnRemove_viw1" runat="server"
                                                        OnClick="btnRemove_viw1_Click" ImageUrl="../images/Wizard_Image/delete.png"
                                                        ToolTip="Remove" meta:resourcekey="btnRemove_viw1Resource1" />
                                                </div>
                                            </div>
                                        </asp:WizardStep>
                                        <asp:WizardStep ID="WizardStep2" runat="server" Title="3-Group"
                                            meta:resourcekey="WizardStep2Resource1">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="VSStep2" runat="server" CssClass="MsgValidation"
                                                        EnableClientScript="False" ValidationGroup="VGStep2"
                                                        meta:resourcekey="VSStep2Resource1" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="Label7" runat="server" Text="Group Name :"
                                                        meta:resourcekey="Label7Resource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="txtGrpName_viw2" runat="server"
                                                        meta:resourcekey="txtGrpName_viw2Resource1"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col2">
                                                </div>
                                                <div class="col4">
                                                    <asp:ListBox ID="lstRotationGroup_viw2" runat="server" Height="200px"
                                                        meta:resourcekey="lstRotationGroup_viw2Resource1"></asp:ListBox>

                                                    <asp:CustomValidator ID="cvlstRotationGroup_viw2" runat="server"
                                                        ControlToValidate="txtCustomValidator" EnableClientScript="False"
                                                        OnServerValidate="lstRotationGroup_viw2_ServerValidate"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='Group is required!' /&gt;"
                                                        ValidationGroup="VGStep2" meta:resourcekey="rfvlstRotationGroup_viw2Resource1">
                                                    </asp:CustomValidator>

                                                    <%--<asp:RequiredFieldValidator ID="rfvlstRotationGroup_viw2" runat="server" 
                                                                                                        ControlToValidate="lstRotationGroup_viw2" EnableClientScript="False" 
                                                                                                        Text="&lt;img src='../images/Exclamation.gif' title='Group is required!' /&gt;" 
                                                                                                        ValidationGroup="VGStep2" 
                                                                                                        meta:resourcekey="rfvlstRotationGroup_viw2Resource1"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                </div>
                                                <div class="col4">
                                                    <asp:ImageButton ID="btnAdd_viw2" runat="server" OnClick="btnAdd_viw2_Click"
                                                        ImageUrl="../images/Wizard_Image/add.png" ToolTip="Add"
                                                        meta:resourcekey="btnAdd_viw2Resource1" />
                                                    &nbsp;
                                                                                    <asp:ImageButton ID="btnUP_viw2" runat="server" OnClick="btnUP_viw2_Click"
                                                                                        ImageUrl="../images/Wizard_Image/arrow_up.png" ToolTip="Up"
                                                                                        meta:resourcekey="btnUP_viw2Resource1" />
                                                    &nbsp;
                                                                                    <asp:ImageButton ID="btnDOWN_viw2" runat="server" OnClick="btnDOWN_viw2_Click"
                                                                                        ImageUrl="../images/Wizard_Image/arrow_down.png" ToolTip="Down"
                                                                                        meta:resourcekey="btnDOWN_viw2Resource1" />
                                                    &nbsp;
                                                                                    <asp:ImageButton ID="btnRemove_viw2" runat="server"
                                                                                        OnClick="btnRemove_viw2_Click" ImageUrl="../images/Wizard_Image/delete.png"
                                                                                        ToolTip="Remove" meta:resourcekey="btnRemove_viw2Resource1" />
                                                </div>
                                            </div>
                                        </asp:WizardStep>
                                        <asp:WizardStep ID="WizardStepFinish" runat="server" Title="4-Select Employee"
                                            meta:resourcekey="WizardStepFinishResource1">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="VSFinish" runat="server" CssClass="MsgValidation"
                                                        EnableClientScript="False" ValidationGroup="VGFinish"
                                                        meta:resourcekey="VSFinishResource1" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col12">
                                                    <uc:EmployeeSelectedGroup runat="server" ID="ucEmployeeSelectedGroup" ValidationGroupName="VGFinish" />
                                                </div>
                                            </div>
                                        </asp:WizardStep>
                                    </WizardSteps>
                                    <HeaderTemplate>
                                        <ul id="wizHeader<%=Session["Language"]%>">
                                            <asp:Repeater ID="SideBarList" runat="server">
                                                <ItemTemplate>
                                                    <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>"
                                                        title='<%# Eval("Name") %>'><%# Eval("Name")%></a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </HeaderTemplate>
                                </asp:Wizard>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </asp:View>
                </asp:MultiView>

            </div>






        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
