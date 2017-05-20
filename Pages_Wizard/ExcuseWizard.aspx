<%@ Page Title="Excuse Wizard" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="ExcuseWizard.aspx.cs" Inherits="ExcuseWizard" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/EmployeeSelected.ascx" TagName="EmployeeSelected" TagPrefix="ucEmp" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/TabContainer.js"></script>
    <%--script--%>

    <%--Style--%>
    <link href="../CSS/WizardStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />
    <%--Style--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="ShowMsg" />
                </div>
            </div>

            <div class="row">
                <div class="col2">
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                    &nbsp;
                        <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                            ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid">
                        </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <asp:Wizard ID="WizardData" runat="server" DisplaySideBar="False"
                    OnActiveStepChanged="WizardData_ActiveStepChanged"
                    OnPreRender="WizardData_PreRender" Width="100%" ActiveStepIndex="0"
                    meta:resourcekey="WizardDataResource1">
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
                                <asp:ImageButton ID="btnNextStep" runat="server" OnClick="btnNextStep_Click"
                                    ImageUrl="../images/Wizard_Image/step_next.png" ValidationGroup="VGStep1"
                                    ToolTip="Next" meta:resourcekey="btnNextStepResource1" />
                                <asp:ImageButton ID="btnBackStep" runat="server" OnClick="btnBackStep_Click"
                                    ImageUrl="../images/Wizard_Image/step_previous.png" ToolTip="Back"
                                    meta:resourcekey="btnBackStepResource1" />
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
                                    OnClick="btnFinishStep_Click" ImageUrl="../images/Wizard_Image/save.png"
                                    ToolTip="Save" ValidationGroup="VGFinish"
                                    meta:resourcekey="btnFinishStepResource1" />
                            </div>
                        </div>
                    </FinishNavigationTemplate>

                    <WizardSteps>
                        <asp:WizardStep ID="WizardStep1" runat="server" Title="1-Options"
                            meta:resourcekey="WizardStep1Resource1">
                            <div class="row">
                                <div class="col12">
                                    <asp:ValidationSummary ID="VSStart" runat="server"
                                        CssClass="errorValidation" EnableClientScript="False"
                                        ValidationGroup="VGStart" meta:resourcekey="VSStartResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblExcuseType" runat="server" Text="Excuse Type:"
                                        meta:resourcekey="lblExcuseTypeResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlExcType" runat="server"
                                        meta:resourcekey="ddlExcTypeResource1" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlExcType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlExcType" runat="server" ControlToValidate="ddlExcType" CssClass="CustomValidator"
                                        EnableClientScript="False" ErrorMessage="Excuse Type is required!" Text="&lt;img src='../images/Exclamation.gif' title='Excuse Type is required!' /&gt;"
                                        ValidationGroup="VGStart" meta:resourcekey="rfvddlExcTypeResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtCustomValidator" runat="server" Text="02120"
                                        Visible="False" Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblWktID" runat="server" Text="Work Time:"
                                        meta:resourcekey="lblWktIDResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlWktID" runat="server" 
                                        meta:resourcekey="ddlWktIDResource1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlWktID" runat="server" ControlToValidate="ddlWktID" CssClass="CustomValidator"
                                        EnableClientScript="False" ErrorMessage="Work Time is required!" Text="&lt;img src='../images/Exclamation.gif' title='Work Time is required!' /&gt;"
                                        ValidationGroup="VGStart" meta:resourcekey="rfvddlWktIDResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblStDate" runat="server" Text="Start Date:"
                                        meta:resourcekey="lblStDateResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="VGStart" CalTo="calEndDate" />
                                </div>
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date:"
                                        meta:resourcekey="lblEndDateResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="VGStart" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblStartTime" runat="server" Text="Start Time:"
                                        meta:resourcekey="lblStartTimeResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Almaalim:TimePicker ID="tpStartTime" runat="server" FormatTime="HHmm"
                                        TimePickerValidationGroup="VGStart" CssClass="TimeCss"
                                        TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='Start Time Excuse is required!' /&gt;"
                                        meta:resourcekey="tpStartTimeResource1" />
                                </div>
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblEndTime" runat="server" Text="End Time:"
                                        meta:resourcekey="lblEndTimeResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Almaalim:TimePicker ID="tpEndTime" runat="server" FormatTime="HHmm"
                                        TimePickerValidationGroup="VGStart" CssClass="TimeCss"
                                        TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='End time Excuse is required!' /&gt;"
                                        meta:resourcekey="tpEndTimeResource1" />
                                    <asp:CustomValidator ID="cvExcuseTime" runat="server"
                                        Text="&lt;img src='../images/message_exclamation.png' title='Enter correct Excuse Time' /&gt;"
                                        ErrorMessage="Enter correct Excuse Time!" CssClass="CustomValidator"
                                        ValidationGroup="VGStart"
                                        OnServerValidate="ExcuseTimeValidate_ServerValidate"
                                        EnableClientScript="False"
                                        ControlToValidate="txtCustomValidator"
                                        meta:resourcekey="cvShift1TimeResource1"></asp:CustomValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblAvailable" runat="server" Text="Available:"
                                        meta:resourcekey="lblAvailableResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtAvailable" runat="server" TextMode="MultiLine"
                                         meta:resourcekey="txtAvailableResource1"></asp:TextBox>
                                </div>
                                <div class="col2">
                                    <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No:"
                                        meta:resourcekey="lblPhoneNoResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtPhoneNo" runat="server"  
                                        meta:resourcekey="txtPhoneNoResource1"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblDesc" runat="server" Text="Description:"
                                        meta:resourcekey="lblDescResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"
                                        meta:resourcekey="txtDescResource1"></asp:TextBox>
                                </div>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep2" runat="server" Title="2-Select Employees"
                            meta:resourcekey="WizardStep2Resource1">
                            <div class="row">
                                <div class="col12">
                                    <asp:ValidationSummary ID="VSFinish" runat="server" CssClass="errorValidation"
                                        EnableClientScript="False" ValidationGroup="VGFinish"
                                        meta:resourcekey="VSFinishResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col12">
                                    <ucEmp:EmployeeSelected runat="server" ID="ucEmployeeSelected" ValidationGroupName="VGFinish" />
                                </div>
                            </div>
                        </asp:WizardStep>
                    </WizardSteps>
                    <HeaderTemplate>
                        <ul id="wizHeaderEN" style="background-color: #C2C2C2;">
                            <asp:Repeater ID="SideBarList" runat="server">
                                <ItemTemplate>
                                    <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>"
                                        title='<%# Eval("Name") %>'><%# Eval("Name")%></a> </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </HeaderTemplate>
                </asp:Wizard>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
