<%@ Page Title="Worktime Wizard" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="WorkTimeWizard.aspx.cs" Inherits="WorkTimeWizard" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
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

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="LeftOverlay"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">

                <asp:Wizard ID="WizardData" runat="server" DisplaySideBar="False"
                    OnActiveStepChanged="WizardData_ActiveStepChanged"
                    OnPreRender="WizardData_PreRender" Width="100%"
                    meta:resourcekey="WizardDataResource1" ActiveStepIndex="0">
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
                    </StepNavigationTemplate>
                    <FinishNavigationTemplate>
                        <div class="row">
                            <div class="col12 center-block">
                                <asp:ImageButton ID="btnFinishExit" runat="server" OnClick="btnExit_Click" ImageUrl="../images/Wizard_Image/finish.png"
                                    ToolTip="Exit" meta:resourcekey="btnFinishExitResource1" />
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
                                    <asp:Label ID="lblWorktime" runat="server" Text="Work Time :"
                                        meta:resourcekey="lblWorktimeResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlWktID" runat="server"  
                                        meta:resourcekey="ddlWktIDResource1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlWktID" runat="server"
                                        ControlToValidate="ddlWktID" EnableClientScript="False" CssClass="CustomValidator"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Work Time is required!' /&gt;"
                                        ValidationGroup="VGStart" meta:resourcekey="rfvddlWktIDResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1">Start Date :</asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="VGStart" CalTo="calEndDate" />
                                </div>
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="Label5" runat="server" Text="End Date :"
                                        meta:resourcekey="Label5Resource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="VGStart" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblWorkingdays" runat="server" Text="Working days :"
                                        meta:resourcekey="lblWorkingdaysResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <span class="daysChk">
                                    <asp:CheckBox ID="chkEwrSat" runat="server" Text="Saturday"
                                        meta:resourcekey="chkEwrSatResource1" />
                                    </span>
                                    <span class="daysChk">
                                    <asp:CheckBox ID="chkEwrSun" runat="server" Text="Sunday"
                                        meta:resourcekey="chkEwrSunResource1" />
                                        </span>
                                    <span class="daysChk">
                                    <asp:CheckBox ID="chkEwrMon" runat="server" Text="Monday"
                                        meta:resourcekey="chkEwrMonResource1" />
                                        </span>
                                    <span class="daysChk">
                                    <asp:CheckBox ID="chkEwrTue" runat="server" Text="Tuesday"
                                        meta:resourcekey="chkEwrTueResource1" />
                                        </span>
                                    <span class="daysChk">
                                    <asp:CheckBox ID="chkEwrWed" runat="server" Text="Wednesday"
                                        meta:resourcekey="chkEwrWedResource1" />
                                        </span>
                                    <span class="daysChk">
                                    <asp:CheckBox ID="chkEwrThu" runat="server" Text="Thursday"
                                        meta:resourcekey="chkEwrThuResource1" />
                                        </span>
                                    <span class="daysChk">
                                    <asp:CheckBox ID="chkEwrFri" runat="server" Text="Friday"
                                        meta:resourcekey="chkEwrFriResource1" />
                                        </span>
                                    
                                </div>
                                <div class="col2">
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtCustomValidator" runat="server" Text="02120"
                                        Visible="False" Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                    <asp:CustomValidator ID="cvSelectWorkDays" runat="server"
                                        Text="&lt;img src='../images/message_exclamation.png' title='Select Work Days!' /&gt;"
                                        ValidationGroup="VGStart"
                                        ErrorMessage="Select Work Days" CssClass="CustomValidator"
                                        OnServerValidate="SelectWorkDays_ServerValidate"
                                        EnableClientScript="False"
                                        ControlToValidate="txtCustomValidator"
                                        meta:resourcekey="cvSelectWorkDaysResource1"></asp:CustomValidator>
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
                        <asp:WizardStep ID="WizardStep3" runat="server" Title="  ">
                            <div class="row">
                                <div class="col2">
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtResult" runat="server" AutoCompleteType="Disabled"
                                        Width="780px" BorderStyle="None" Height="400px" TextMode="MultiLine"
                                        Enabled="False" ReadOnly="True"></asp:TextBox>
                                </div>


                            </div>
                        </asp:WizardStep>
                    </WizardSteps>
                    <HeaderTemplate>
                        <ul id="wizHeader<%=Session["Language"]%>" style="background-color: #C2C2C2;">
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


