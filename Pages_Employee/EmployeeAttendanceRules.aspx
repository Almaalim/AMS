<%@ Page Title="Employee Attendance Rule" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeAttendanceRules.aspx.cs" Inherits="EmployeeAttendanceRules" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Src="~/Control/EmployeeSelected.ascx" TagName="EmployeeSelected" TagPrefix="ucEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/TabContainer.js"></script>
    <%--script--%>
    <div>
        <asp:UpdatePanel ID="updPanel" runat="server">
            <ContentTemplate>
            <div class="content">
               <div class="row">
                <div class="col12">
                                                    <asp:ValidationSummary ID="VGFinish" runat="server" CssClass="errorValidation" 
                                                        EnableClientScript="False" ValidationGroup="VGFinish" 
                                                        meta:resourcekey="VSFinishResource1" />
                                                 </div>
            </div>
                <div class="row">
                <div class="col2">
                                        <span class="RequiredField">*</span> <asp:Label ID="lblRuleSetSelect" 
                                            runat="server" Text="Select Rule Set:"
                                            meta:resourcekey="lblRuleSetSelectResource1"></asp:Label>
                               </div>
                    <div class="col3">
                                    <asp:DropDownList ID="ddlRuleSet" runat="server"  
                                        AutoPostBack="True" meta:resourcekey="ddlRuleSetResource1" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvLanguage" ControlToValidate="ddlRuleSet" CssClass="CustomValidator"
                                        InitialValue="Select RuleSet" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Select RuleSet!' />"
                                        ValidationGroup="VGFinish" meta:resourcekey="rfvLanguageResource1"></asp:RequiredFieldValidator>
                                           </div>
            </div>
            <div class="row">
                <div class="col12">
                                                    <ucEmp:EmployeeSelected runat="server" ID="ucEmployeeSelected" ValidationGroupName="VGFinish" />
                                                </div>
            </div>
            
            <div class="row">
                <div class="col2">
                                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton   glyphicon glyphicon-floppy-disk" 
                                           OnClick="btnSave_Click"  ValidationGroup="VGFinish" 
                                           Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                                           meta:resourcekey="btnSaveResource1" Enabled="False"></asp:LinkButton>
                                </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                   
                                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                                </div>
                           </div>
                           
                      
            </div>
              
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


