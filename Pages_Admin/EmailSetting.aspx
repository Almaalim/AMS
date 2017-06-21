<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmailSetting.aspx.cs" Inherits="EmailSetting" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                    <asp:TextBox ID="TextBox1" runat="server" Text="02120" Visible="False"
                                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                     
                                    <asp:CustomValidator id="CustomValidator1" runat="server" Display="None" 
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="CustomValidator1Resource1"></asp:CustomValidator>
                                </div>
                </div>
                  <div class="row">
                <div class="col2">
                                    <asp:Label ID="lblServerID" runat="server" Text="Server ID :" meta:resourcekey="lblServerIDResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtServerID" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtServerIDResource1" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvServerID" runat="server" 
                                        ControlToValidate="txtServerID" EnableClientScript="False"  CssClass="CustomValidator"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Server ID is required' /&gt;" 
                                        ValidationGroup="vgSave" meta:resourcekey="rvServerIDResource1"></asp:RequiredFieldValidator>
                               </div>
                                <div class="col2">
                                    <asp:Label ID="lblPortNo" runat="server" Text="Port No. :" meta:resourcekey="lblPortNoResource1"></asp:Label>
                           </div>
                                <div class="col4">
                                     <asp:TextBox ID="txtPortNo" runat="server" AutoCompleteType="Disabled" Width="150px" meta:resourcekey="txtPortNoResource1"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rvPortNo" runat="server" CssClass="CustomValidator"
                                        ControlToValidate="txtPortNo" EnableClientScript="False" 
                                        Text="&lt;img src='../images/Exclamation.gif' title=Port No. is required' /&gt;" 
                                        ValidationGroup="vgSave" meta:resourcekey="rvPortNoResource1"></asp:RequiredFieldValidator>
                             </div>
                      </div>
            <div class="row">
                <div class="col2">
                                    <asp:Label ID="lblSenderEmailID" runat="server" Text="Sender Email ID :" meta:resourcekey="lblSenderEmailIDResource1"></asp:Label>
                             </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtSenderEmailID" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtSenderEmailIDResource1"  ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvSenderEmailID" runat="server" 
                                        ControlToValidate="txtSenderEmailID" EnableClientScript="False" CssClass="CustomValidator"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Sender Email ID is required' /&gt;" 
                                        ValidationGroup="vgSave" meta:resourcekey="rvSenderEmailIDResource1"></asp:RequiredFieldValidator>
                                  </div>
                                <div class="col2">
                                    <asp:Label ID="lblSenderEmailPassword" runat="server" Text="Sender Email Password :" meta:resourcekey="lblSenderEmailPasswordResource1"></asp:Label>
                               </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtSenderEmailPassword" runat="server" AutoCompleteType="Disabled"  TextMode="Password" meta:resourcekey="txtSenderEmailPasswordResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvSenderEmailPassword" runat="server" 
                                        ControlToValidate="txtSenderEmailPassword" EnableClientScript="False" CssClass="CustomValidator"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Sender Email Password is required' /&gt;" 
                                        ValidationGroup="vgSave" meta:resourcekey="rvSenderEmailPasswordResource1"></asp:RequiredFieldValidator>
                                </div>
                      </div>
            <div class="row">
                <div class="col2"></div>
                                <div class="col4">
                                                <asp:CheckBox ID="chkEmailCredential" runat="server" Text="Enable Credentials" meta:resourcekey="chkEmailCredentialResource1" />
                                           </div>
                 <div class="col2"></div>
                                <div class="col4">
                    <asp:CheckBox ID="chkEnableSSL" runat="server" Text="Enable SSL" meta:resourcekey="chkEnableSSLResource1" />
                                           </div>
                      </div>
            <div class="row">
                <div class="col12">
                                    <asp:Label ID="lblSendTestEmail" runat="server" Text="Send Test Email" CssClass="h3" meta:resourcekey="lblSendTestEmailResource1"></asp:Label>
                                </div>
                      </div>
            <div class="row">
                <div class="col2">
                                    <asp:Label ID="lblSendToEmail" runat="server" Text="Send To Email :" meta:resourcekey="lblSendToEmailResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtSendToEmail" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtSendToEmailResource1" ></asp:TextBox>
                               </div>
                <div class="col4">
                                    <asp:LinkButton ID="btnSendTestEmail" runat="server" CssClass="GenButton1 glyphicon glyphicon-share" OnClick="btnSendTestEmail_Click"
                                         Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Send"
                                        ValidationGroup="vgSave" meta:resourcekey="btnSendTestEmailResource1"></asp:LinkButton>
                                       </div>
                      </div>
            <div class="row">
                <div class="col2">
                    </div>
                                
                      </div>
            <div class="row">
                <div class="col2">
                                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                
                                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                                </div>
                      </div>
            <div class="row">
                <div class="col12">
                                    <asp:TextBox ID="txtLogSend" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine" ReadOnly="True" meta:resourcekey="txtLogSendResource1"></asp:TextBox>
                                </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

