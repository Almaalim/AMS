<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="DepartmentLevel.aspx.cs" Inherits="DepartmentLevel" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="vgShowMsg" meta:resourcekey="vsShowMsgResource1" />
                </div>
            </div>
            <div class="row">
                    <div class="col12">
                        <asp:ValidationSummary runat="server" ID="vsSave" ValidationGroup="vgSave" EnableClientScript="False"
                            CssClass="MsgValidation" ShowSummary="False" meta:resourcekey="vsSaveResource1" />
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
                        ValidationGroup="vgSave" meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                        ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>

                    <asp:CustomValidator ID="cvValid" runat="server" Display="None" CssClass="CustomValidator"
                            ValidationGroup="vgSave" OnServerValidate="Valid_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid"></asp:CustomValidator>
                </div>
            </div>
           
            <div class="row">
                    <div class="col12">   
                        <asp:GridView ID="grdAddData" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="grdAddData_RowDataBound"
                            OnRowCommand="grdAddData_RowCommand" ShowFooter="True" meta:resourcekey="grdAddDataResource1">
                        </asp:GridView>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

