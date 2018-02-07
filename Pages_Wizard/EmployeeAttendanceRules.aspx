<%@ Page Title="Employee Attendance Rule" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeAttendanceRules.aspx.cs" Inherits="EmployeeAttendanceRules" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="~/Control/EmployeeSelected.ascx" TagName="EmployeeSelected" TagPrefix="ucEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdatePanel ID="updPanel" runat="server">
            <ContentTemplate>
                <div class="content">
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
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblRuleSetSelect"
                                runat="server" Text="Attendance Rules Set :"
                                meta:resourcekey="lblRuleSetSelectResource1"></asp:Label>
                        </div>
                        <div class="col3">
                            <asp:DropDownList ID="ddlRuleSet" runat="server"
                                meta:resourcekey="ddlRuleSetResource1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rvRuleSet" ControlToValidate="ddlRuleSet" CssClass="CustomValidator"
                                EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Select RuleSet' />"
                                ValidationGroup="vgSave" meta:resourcekey="rfvLanguageResource1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col12">
                            <ucEmp:EmployeeSelected runat="server" ID="ucEmployeeSelected" ValidationGroupName="vgSave" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col2">
                            <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton   glyphicon glyphicon-floppy-disk"
                                OnClick="btnSave_Click" ValidationGroup="vgSave"
                                Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                                meta:resourcekey="btnSaveResource1" Enabled="False"></asp:LinkButton>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                EnableClientScript="False" ControlToValidate="txtValid">
                            </asp:CustomValidator>
                        </div>
                    </div>


                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


