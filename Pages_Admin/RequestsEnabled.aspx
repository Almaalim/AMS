<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="RequestsEnabled.aspx.cs" Inherits="RequestsEnabled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
           <div class="row">
                <div class="col12">
                        <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                            EnableClientScript="False" ValidationGroup="ShowMsg"/>
                    </div>
                </div>
            <div class="row">
                <div class="col8">
                                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" OnClick="btnModify_Click"
                                         Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                                        meta:resourcekey="btnModifyResource1">
                                    </asp:LinkButton>
                               
                                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" 
                                        OnClick="btnSave_Click" 
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                                        ValidationGroup="Groups"  >
                                    </asp:LinkButton>
                                
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                                         Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                        meta:resourcekey="btnCancelResource1">
                                    </asp:LinkButton>
                                 </div>
                              <div class="col4">
                                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                    &nbsp;
                                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                                </div>
                </div>
            <div class="row">
                  <div class="col2">
                                    <asp:Label ID="lblReq" runat="server" Text="Requests :"></asp:Label>
                                 </div>
                              <div class="col10">
                                    <asp:CheckBoxList ID="chkbReqList" runat="server" RepeatLayout="Flow" Width="100%">
                                    </asp:CheckBoxList>
                                 </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

