﻿<%@ Page Title="Add Transactions Wizard" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="AppendingTransactionsWizard.aspx.cs" Inherits="AppendingTransactionsWizard" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
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
            <table id="MainTable" runat="server">
                <tr><td style="height:20px"></td></tr>
                <tr>
                    <td class="td2Allalign" colspan="2">
                        <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                            EnableClientScript="False" ValidationGroup="ShowMsg"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                        <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                            Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                        &nbsp;
                        <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                            ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid">
                        </asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                   <td>
                        <asp:Wizard ID="WizardData" runat="server" DisplaySideBar="False" 
                            onactivestepchanged="WizardData_ActiveStepChanged" 
                            onprerender="WizardData_PreRender" 
                            onfinishbuttonclick="WizardData_FinishButtonClick" Width="800px" 
                            meta:resourcekey="WizardDataResource1">
                            <StartNavigationTemplate>
                             <table width="100%"><tr><td class="td1Allalign">
                                <asp:ImageButton ID="btnStartStep" runat="server" OnClick="btnStartStep_Click" 
                                    ImageUrl="../images/Wizard_Image/step_next.png" ValidationGroup="VGStart" 
                                    ToolTip="Next" meta:resourcekey="btnStartStepResource1" />
                                     </td></tr></table>
                            </StartNavigationTemplate>
                            <StepNavigationTemplate>
                             <table width="100%"><tr><td class="td1Allalign">
                                <asp:ImageButton ID="btnNextStep" runat="server" OnClick="btnNextStep_Click" 
                                    ImageUrl="../images/Wizard_Image/step_next.png" ValidationGroup="VGStep1" 
                                    ToolTip="Next" meta:resourcekey="btnNextStepResource1" />
                                <asp:ImageButton ID="btnBackStep" runat="server" OnClick="btnBackStep_Click" 
                                    ImageUrl="../images/Wizard_Image/step_previous.png" ToolTip="Back" 
                                    meta:resourcekey="btnBackStepResource1" />
                                      </td></tr></table>
                            </StepNavigationTemplate>
                            <FinishNavigationTemplate>
                                <table width="100%">
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:ImageButton ID="btnFinishBackStep" runat="server" 
                                                OnClick="btnFinishBackStep_Click" 
                                                ImageUrl="../images/Wizard_Image/step_previous.png" ToolTip="Back" 
                                                meta:resourcekey="btnFinishBackStepResource1" />
                                            <asp:ImageButton ID="btnFinishStep" runat="server" 
                                                OnClick="btnFinishStep_Click" ImageUrl="../images/Wizard_Image/save.png" 
                                                ToolTip="Save" ValidationGroup="VGFinish" 
                                                meta:resourcekey="btnFinishStepResource1"/>
                                        </td>
                                    </tr>
                                </table>
                            </FinishNavigationTemplate>
                            
                            <WizardSteps>                
                                <asp:WizardStep ID="WizardStep1" runat="server" Title="1-Options" 
                                    meta:resourcekey="WizardStep1Resource1">                 
                                    <div>
               
                                        <table class="content" style="border-style: solid;">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:ValidationSummary ID="VSStart" runat="server" 
                                                        CssClass="errorValidation" EnableClientScript="False" 
                                                        ValidationGroup="VGStart" meta:resourcekey="VSStartResource1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td1Allalign" valign="middle">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="Label4" runat="server" Text="Date :" 
                                                        meta:resourcekey="Label4Resource1"></asp:Label>
                                                </td>
                                                <td class="td2Allalign" valign="middle">
                                                    <Cal:Calendar2 ID="CalDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                                                </td>                           
                                                <td class="td1Allalign" valign="middle">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblType" runat="server" Text="Type :" 
                                                        meta:resourcekey="lblTypeResource1"></asp:Label>
                                                </td>
                                                <td class="td2Allalign" valign="middle">
                                                    <asp:DropDownList ID="ddlType" runat="server" Width="185px" 
                                                        meta:resourcekey="ddlTypeResource1" AutoPostBack="True" 
                                                        OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                        <asp:ListItem meta:resourcekey="ListItemResource1">Select Type</asp:ListItem>
                                                        <asp:ListItem Text="IN" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                        <asp:ListItem Text="OUT" Value="0" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvType" ControlToValidate="ddlType"
                                                        InitialValue="Select Type" EnableClientScript="False" 
                                                        Text="<img src='../images/Exclamation.gif' title='Type is required!' />"
                                                        ValidationGroup="VGStart" meta:resourcekey="rfvTypeResource1"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td1Allalign" valign="middle">
                                                <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblTime" runat="server" Text="Time :" 
                                                        meta:resourcekey="lblTimeResource1"></asp:Label>
                                                </td>
                                                <td class="td2Allalign" valign="middle">
                                                    <Almaalim:TimePicker ID="tpickerTime" runat="server" FormatTime="HHmmss" CssClass="TimeCss"
                                                        meta:resourcekey="tpickerTimeResource1" />
                                                            <asp:CustomValidator id="cvtpickerTime" runat="server"
                                                    Text="&lt;img src='../images/Exclamation.gif' title='Time is required!' /&gt;" 
                                                    ValidationGroup="VGStart"
                                                    OnServerValidate="TpickerTime_ServerValidate"
                                                    EnableClientScript="False" 
                                                    ControlToValidate="txtValid1" 
                                                    meta:resourcekey="cvtpickerTimeResource1"></asp:CustomValidator>
                                                </td>
                                                <td class="td1Allalign"  valign="middle" >
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblLocation" runat="server" Text="Location :" 
                                                        meta:resourcekey="lblLocationResource1"></asp:Label>
                                                </td>
                                                <td class="td2Allalign"  valign="middle" >
                                                    <asp:DropDownList ID="ddlLocation" runat="server" Width="185px" 
                                                        meta:resourcekey="ddlLocationResource1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvLocation" ControlToValidate="ddlLocation"
                                                    EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Location is required!' />"
                                                    ValidationGroup="VGStart" meta:resourcekey="rfvLocationResource1"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td1Allalign" valign="top">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblDesc" runat="server" Text="Reason :" meta:resourcekey="lblDescResource1"></asp:Label>
                                                </td>
                                                <td class="td2Allalign" valign="middle" colspan="3">
                                                    <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled" 
                                                        TextMode="MultiLine" Width="434px"
                                                        meta:resourcekey="txtDescResource1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rvDesc" runat="server" 
                                                        ControlToValidate="txtDesc" EnableClientScript="False" 
                                                        Text="&lt;img src='../images/Exclamation.gif' title='Reason is required!' /&gt;" 
                                                        ValidationGroup="VGStart" meta:resourcekey="rvDescResource1"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>   
                                        <tr>
                                            <td class="td1Allalign" valign="middle">
                                                <span id="spnReqFile" runat="server" visible="False" class="RequiredField">*</span>
                                                <asp:Label ID="lblReqFile" runat="server" Text="Attachment File :" 
                                                    meta:resourcekey="lblReqFileResource1"></asp:Label>
                                            </td>
                                            <td valign="middle" class="td2Allalign" colspan="3">
                                                <asp:FileUpload ID="fudReqFile" runat="server" Width="434px" meta:resourcekey="fudReqFileResource1"/>            
                                                <asp:CustomValidator id="cvReqFile" runat="server"
                                                Text="&lt;img src='../images/Exclamation.gif' title='Attachment File is required!' /&gt;" 
                                                ValidationGroup="VGStart"
                                                OnServerValidate="ReqFile_ServerValidate"
                                                EnableClientScript="False" 
                                                ControlToValidate="txtValid1" meta:resourcekey="cvReqFileResource1"></asp:CustomValidator>
                                            </td>
                                        </tr>   


                                            <tr>
                                                <td class="td1Allalign" valign="middle">
                                                    <asp:TextBox ID="txtValid1" runat="server" Text="012552" 
                                                        Visible="False" ValidationGroup="Groups" Width="10px" 
                                                        meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                                </td>
                                                <td class="td2Allalign" valign="middle" colspan = "3">
                                                </td>             
                                            </tr>
                                        </table>
               
                                    </div>                
                                </asp:WizardStep>                
                                <asp:WizardStep ID="WizardStep2" runat="server" Title="2-Select Employees" 
                                    meta:resourcekey="WizardStep2Resource1">                   
                                    <div class="content">
              
                                        <table style="border-style: solid">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:ValidationSummary ID="VSFinish" runat="server" CssClass="errorValidation" 
                                                        EnableClientScript="False" ValidationGroup="VGFinish" 
                                                        meta:resourcekey="VSFinishResource1" />
                                                </td>
                                            </tr>                       
                                            <tr>
                                                <td colspan="4">
                                                    <ucEmp:EmployeeSelected runat="server" ID="ucEmployeeSelected" ValidationGroupName="VGFinish" />
                                                </td>
                                            </tr>
                                        </table>
              
                                    </div>                      
                                </asp:WizardStep>                
                            </WizardSteps>                 
                         
                            <HeaderTemplate>                   
                                <ul id="wizHeader<%=Session["Language"]%>" style="background-color: #C2C2C2;" >                    
                                    <asp:Repeater ID="SideBarList" runat="server" >                        
                                    <ItemTemplate>                            
                                        <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>" 
                                                title='<%# Eval("Name") %>'> <%# Eval("Name")%></a> </li>                        
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