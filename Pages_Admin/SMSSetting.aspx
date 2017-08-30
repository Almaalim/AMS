<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="SMSSetting.aspx.cs" Inherits="SMSSetting" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:updatepanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" EnableClientScript="False" ValidationGroup="vgShowMsg" meta:resourcekey="vsShowMsgResource1" />
                </div>
            </div>        
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton  glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify" meta:resourcekey="btnModifyResource1"></asp:LinkButton>
                               
                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"   
                        OnClick="btnSave_Click"  Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                        ValidationGroup="vgSave" meta:resourcekey="btnSaveResource1"></asp:LinkButton>
                               
                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                            Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                                
                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                        ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="col2">
                    <span class="RequiredField">*</span>
                    <asp:Label ID="lblSmsGateway" runat="server" Text="SMS Gateway :" meta:resourcekey="lblSmsGatewayResource1"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtSmsGateway" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtSmsGatewayResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvSmsGateway" runat="server" 
                        ControlToValidate="txtSmsGateway" EnableClientScript="False"  CssClass="CustomValidator"
                        Text="&lt;img src='../images/Exclamation.gif' title='SMS Gateway is required' /&gt;" 
                        ValidationGroup="vgSave" meta:resourcekey="rvSmsGatewayResource1"></asp:RequiredFieldValidator>
                </div>
                <div class="col2">
                    <span class="RequiredField">*</span>
                    <asp:Label ID="lblSmsSenderID" runat="server" Text="Sender ID :" meta:resourcekey="lblSmsSenderIDResource1"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtSmsSenderID" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtSmsSenderIDResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvSmsSenderID" runat="server" 
                        ControlToValidate="txtSmsSenderID" EnableClientScript="False" CssClass="CustomValidator"
                        Text="&lt;img src='../images/Exclamation.gif' title='Sender ID is required' /&gt;" 
                        ValidationGroup="vgSave" meta:resourcekey="rvSmsSenderIDResource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col2">
                    <span class="RequiredField">*</span>
                    <asp:Label ID="lblSmsUser" runat="server" Text="User ID :" meta:resourcekey="lblSmsUserResource1"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtSmsUser" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtSmsUserResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvSmsUser" runat="server" 
                        ControlToValidate="txtSmsUser" EnableClientScript="False" CssClass="CustomValidator"
                        Text="&lt;img src='../images/Exclamation.gif' title='User ID is required' /&gt;" 
                        ValidationGroup="vgSave" meta:resourcekey="rvSmsUserResource1"></asp:RequiredFieldValidator>
                    </div>
                <div class="col2">
                    <span class="RequiredField">*</span>
                    <asp:Label ID="lblSmsPass" runat="server" Text="Password :" meta:resourcekey="lblSmsPassResource1"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtSmsPass" runat="server" AutoCompleteType="Disabled" TextMode="Password" meta:resourcekey="txtSmsPassResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvSmsPass" runat="server" 
                        ControlToValidate="txtSmsPass" EnableClientScript="False" CssClass="CustomValidator"
                        Text="&lt;img src='../images/Exclamation.gif' title='Password is required' /&gt;" 
                        ValidationGroup="vgSave" meta:resourcekey="rvSmsPassResource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                
                <div class="col2">
                    <asp:Label ID="lblSmsSenderNo" runat="server" Text="Sender No. :" meta:resourcekey="lblSmsSenderNoResource1" Visible="false"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtSmsSenderNo" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtSmsSenderNoResource1" Visible="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvSmsSenderNo" runat="server"  Enabled="false"
                        ControlToValidate="txtSmsSenderNo" EnableClientScript="False" CssClass="CustomValidator"
                        Text="&lt;img src='../images/Exclamation.gif' title='Sender No. is required' /&gt;" 
                        ValidationGroup="vgSave" meta:resourcekey="rvSmsSenderNoResource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:Label ID="lblSendTestSms" runat="server" Text="Send Test SMS" CssClass="h3" meta:resourcekey="lblSendTestSmsResource1"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col2">
                    <asp:Label ID="lblSendToNo" runat="server" Text="Send To Mobile No. :" meta:resourcekey="lblSendToNoResource1"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtSendToNo" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtSendToNoResource1"></asp:TextBox>
                </div>
                <div class="col4">
                    <asp:LinkButton ID="btnSendTestSms" runat="server" CssClass="GenButton1 glyphicon glyphicon-share" OnClick="btnSendTestSms_Click"
                            Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Send"
                        ValidationGroup="vgSave" meta:resourcekey="btnSendTestSmsResource1"></asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col2"></div>
            </div>
            <div class="row">
                <div class="col2"> </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:TextBox ID="txtLogSend" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine" ReadOnly="True" meta:resourcekey="txtLogSendResource1"></asp:TextBox>
                </div>
            </div>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>

