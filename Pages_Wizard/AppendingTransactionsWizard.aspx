<%@ Page Title="Add Transactions Wizard" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="AppendingTransactionsWizard.aspx.cs" Inherits="AppendingTransactionsWizard" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="TextTimeServerControl" Namespace="TextTimeServerControl" TagPrefix="Almaalim" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>
<%@ Register Src="~/Control/EmployeeSelected.ascx" TagName="EmployeeSelected" TagPrefix="ucEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/TabContainer.js"></script>
    <%--script--%>

    <%--Style--%>
    <link href="../CSS/WizardStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />
    <%--Style--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>

            <asp:PostBackTrigger ControlID="WizardData$StartNavigationTemplateContainerID$btnStartStep" />
            <%--<asp:PostBackTrigger ControlID="WizardData$FinishNavigationTemplateContainerID$btnFinishStep" />--%>
        </Triggers>

        <ContentTemplate>
            <div id="MainTable" runat="server">
                <div class="row">
                    <div class="col12">
                        <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                            EnableClientScript="False" ValidationGroup="ShowMsg" />
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                            Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                        <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                            ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False" ControlToValidate="txtValid">
                        </asp:CustomValidator>
                    </div>
                </div>

                <asp:Wizard ID="WizardData" runat="server" DisplaySideBar="False"
                    OnActiveStepChanged="WizardData_ActiveStepChanged"
                    OnPreRender="WizardData_PreRender"
                    OnFinishButtonClick="WizardData_FinishButtonClick" Width="100%"
                    meta:resourcekey="WizardDataResource1" ActiveStepIndex="1">
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
                            <div class="col12">
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
                            <div class="col12">
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
                                    <asp:Label ID="Label4" runat="server" Text="Date :"
                                        meta:resourcekey="Label4Resource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="CalDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                                </div>
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblType" runat="server" Text="Type :"
                                        meta:resourcekey="lblTypeResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlType" runat="server"
                                        meta:resourcekey="ddlTypeResource1" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        <asp:ListItem meta:resourcekey="ListItemResource1">Select Type</asp:ListItem>
                                        <asp:ListItem Text="IN" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        <asp:ListItem Text="OUT" Value="0" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvType" ControlToValidate="ddlType"
                                        InitialValue="Select Type" EnableClientScript="False" CssClass="CustomValidator"
                                        Text="<img src='../images/Exclamation.gif' title='Type is required!' />"
                                        ValidationGroup="VGStart" meta:resourcekey="rfvTypeResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblTime" runat="server" Text="Time :"
                                        meta:resourcekey="lblTimeResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Almaalim:TimePicker ID="tpickerTime" runat="server" FormatTime="HHmmss" CssClass="TimeCss"
                                        meta:resourcekey="tpickerTimeResource1" />
                                    <asp:CustomValidator ID="cvtpickerTime" runat="server"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Time is required!' /&gt;"
                                        ValidationGroup="VGStart"
                                        OnServerValidate="TpickerTime_ServerValidate"
                                        EnableClientScript="False" CssClass="CustomValidator"
                                        ControlToValidate="txtValid1"
                                        meta:resourcekey="cvtpickerTimeResource1"></asp:CustomValidator>
                                </div>
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblLocation" runat="server" Text="Location :"
                                        meta:resourcekey="lblLocationResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlLocation" runat="server" 
                                        meta:resourcekey="ddlLocationResource1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvLocation" ControlToValidate="ddlLocation" CssClass="CustomValidator"
                                        EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Location is required!' />"
                                        ValidationGroup="VGStart" meta:resourcekey="rfvLocationResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblDesc" runat="server" Text="Reason :" meta:resourcekey="lblDescResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled"
                                        TextMode="MultiLine" 
                                        meta:resourcekey="txtDescResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvDesc" runat="server" CssClass="CustomValidator"
                                        ControlToValidate="txtDesc" EnableClientScript="False"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Reason is required!' /&gt;"
                                        ValidationGroup="VGStart" meta:resourcekey="rvDescResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <span id="spnReqFile" runat="server" visible="False" class="RequiredField">*</span>
                                    <asp:Label ID="lblReqFile" runat="server" Text="Attachment File :"
                                        meta:resourcekey="lblReqFileResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:FileUpload ID="fudReqFile" runat="server" Width="434px" meta:resourcekey="fudReqFileResource1" />
                                    <asp:CustomValidator ID="cvReqFile" runat="server"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Attachment File is required!' /&gt;"
                                        ValidationGroup="VGStart"
                                        OnServerValidate="ReqFile_ServerValidate"
                                        EnableClientScript="False" CssClass="CustomValidator"
                                        ControlToValidate="txtValid1" meta:resourcekey="cvReqFileResource1"></asp:CustomValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <asp:TextBox ID="txtValid1" runat="server" Text="012552"
                                        Visible="False" ValidationGroup="Groups" Width="10px"
                                        meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
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
