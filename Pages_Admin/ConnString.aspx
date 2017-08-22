<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ConnString.aspx.cs" Inherits="ConnString" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:updatepanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                        EnableClientScript="False" ValidationGroup="ShowMsg" meta:resourcekey="vsShowMsgResource1"/>
                </div>
            </div>
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton  glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                        meta:resourcekey="btnModifyResource1"></asp:LinkButton>
                               
                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"   
                        OnClick="btnSave_Click"  
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                        ValidationGroup="vgSave" meta:resourcekey="btnSaveResource1"  ></asp:LinkButton>
                               
                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                            Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                     
                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="col2"></div>
                    <div class="col8">
                        <asp:CheckBox ID="chkEncrypt" runat="server" Text="Encrypt Connection String" meta:resourcekey="chkEncryptResource1" />
                    </div>
                </div>
            <div class="row">
                <div class="col2">
                    <asp:Label ID="lblServerName" runat="server" Text="Server Name :" meta:resourcekey="lblServerNameResource1"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtServerName" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtServerNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvServerName" runat="server" 
                        ControlToValidate="txtServerName" EnableClientScript="False" CssClass="CustomValidator"
                        Text="&lt;img src='../images/Exclamation.gif' title='Server Name is required!' /&gt;" 
                        ValidationGroup="vgSave" meta:resourcekey="rvServerNameResource1"></asp:RequiredFieldValidator>
                </div>
                <div class="col2">
                    <asp:Label ID="lblUserName" runat="server" Text="User Name :" meta:resourcekey="lblUserNameResource1"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtUserName" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtUserNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvUserName" runat="server" 
                    ControlToValidate="txtUserName" EnableClientScript="False" CssClass="CustomValidator"
                    Text="&lt;img src='../images/Exclamation.gif' title=User Name is required!' /&gt;" 
                    ValidationGroup="vgSave" meta:resourcekey="rvUserNameResource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col2">
                    <asp:Label ID="lblPassword" runat="server" Text="Password :" meta:resourcekey="lblPasswordResource1"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtPassword" runat="server" AutoCompleteType="Disabled" TextMode="Password" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvPassword" runat="server" 
                        ControlToValidate="txtPassword" EnableClientScript="False" CssClass="CustomValidator"
                        Text="&lt;img src='../images/Exclamation.gif' title='Password is required!' /&gt;" 
                        ValidationGroup="vgSave" meta:resourcekey="rvPasswordResource1"></asp:RequiredFieldValidator>
                </div>
                <div class="col2">
                    <asp:Label ID="lblDatabaseName" runat="server" Text="Database Name :" meta:resourcekey="lblDatabaseNameResource1"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtDatabaseName" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtDatabaseNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvDatabaseName" runat="server" 
                        ControlToValidate="txtDatabaseName" EnableClientScript="False"  CssClass="CustomValidator"
                        Text="&lt;img src='../images/Exclamation.gif' title='Database Name is required!' /&gt;" 
                        ValidationGroup="vgSave" meta:resourcekey="rvDatabaseNameResource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col2">
                    <asp:LinkButton ID="btnTest" runat="server" CssClass="GenButton glyphicon glyphicon-refresh" OnClick="btnTest_Click"
                            Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Test"
                        ValidationGroup="vgSave" meta:resourcekey="btnTestResource1"></asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>

