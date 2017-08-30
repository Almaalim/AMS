<%@ Page Title="Auto Out Wizard" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="AutoOutWizard.aspx.cs" Inherits="AutoOutWizard" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
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

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
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
                                </div>
                                <div class="col4">
                                    <asp:CustomValidator ID="cvArWorkTimeName" runat="server"
                                        Text="&lt;img src='../images/Exclamation.gif' title=AutoIn is required!' /&gt;"
                                        ValidationGroup="VGStart"
                                        OnServerValidate="WorkTimeValidate_ServerValidate" CssClass="CustomValidator"
                                        EnableClientScript="False"
                                        ControlToValidate="txtCustomValidator"
                                        meta:resourcekey="cvArWorkTimeNameResource1"></asp:CustomValidator>
                                    <asp:RadioButtonList ID="rdlAutoIn" runat="server" RepeatDirection="Horizontal"
                                        meta:resourcekey="rdlAutoInResource1">
                                        <asp:ListItem Text="Assign Auto In" Value="True"
                                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                                        <asp:ListItem Text="Remove Auto In" Value="False"
                                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                                    </asp:RadioButtonList>


                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                </div>
                                <div class="col4">
                                    <asp:CustomValidator ID="cvEnWorkTimeName" runat="server"
                                        Text="&lt;img src='../images/Exclamation.gif' title='AutoOut is required!' /&gt;"
                                        ValidationGroup="VGStart"
                                        OnServerValidate="WorkTimeValidate_ServerValidate"
                                        EnableClientScript="False" CssClass="CustomValidator"
                                        ControlToValidate="txtCustomValidator"
                                        meta:resourcekey="cvEnWorkTimeNameResource1"></asp:CustomValidator>
                                    <asp:RadioButtonList ID="rdlAutoOut" runat="server"
                                        RepeatDirection="Horizontal" meta:resourcekey="rdlAutoOutResource1">
                                        <asp:ListItem Text="Assign Auto Out" Value="True"
                                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                                        <asp:ListItem Text="Remove Auto Out" Value="False"
                                            meta:resourcekey="ListItemResource4"></asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtCustomValidator" runat="server" Text="02120"
                                        Visible="False" Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                </div>
                                <div class="col2">
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
            </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
