<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ImportTransactionSetting.aspx.cs" Inherits="Pages_Admin_ImportSetting" meta:resourcekey="PageResource1" %>

<%@ Register Src="~/Control/ScheduleTimes.ascx" TagName="ScheduleTimes" TagPrefix="ucTimes" %>

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
                <div class="col12">
                    <asp:CheckBox ID="chkIpsSaveTransInFile" runat="server" Text="Save imported data as a fle in a CSV format" meta:resourcekey="chkIpsSaveTransInFileResource1"/>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:CheckBox ID="chkIpsEncryptTransInFile" runat="server" Text="Encrypt imported data in a CSV file" meta:resourcekey="chkIpsEncryptTransInFileResource1"/>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:CheckBox ID="chkIpsRunProcess" runat="server" Text="Run Process After Import" meta:resourcekey="chkEmailCredentialResource1" AutoPostBack="True" OnCheckedChanged="chkIpsRunProcess_CheckedChanged"/>
                </div>
            </div> 

             <div class="row">
                <div class="col6">
                    <span class="RequiredField">*</span>
                    <asp:Label ID="Label1" runat="server" Text="Import Schedule Times :" meta:resourcekey="Label1Resource1"></asp:Label>
                </div>
               
                <div id="divProcess1" runat="server" visible="False" class="col6">
                    <span class="RequiredField">*</span>
                    <asp:Label ID="Label2" runat="server" Text="Process Schedule Times :" meta:resourcekey="Label2Resource1"></asp:Label>
                </div>
                 
            </div>

            <div class="row">
                <div class="col6">
                    <ucTimes:ScheduleTimes runat="server" ID="ucImportTimes" ValidationGroupName="vgShowMsg"/>
                </div>
                <div id="divProcess2" runat="server" visible="False" class="col6">
                    <ucTimes:ScheduleTimes runat="server" ID="ucProcessTimes" ValidationGroupName="vgSave"/>
                </div>        
            </div>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>

