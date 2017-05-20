<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="PermissionsRequests.aspx.cs" Inherits="PermissionsRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td colspan="3" valign="top" align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="5" cellspacing="0">
                            <tr>
                                <td height="10">
                                </td>
                            </tr>
                            <tr>
                                <td class="td2Allalign" colspan="2">
                                    <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                                        EnableClientScript="False" ValidationGroup="ShowMsg"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" valign="middle">
                                    <center>
                                        <table>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton" OnClick="btnModify_Click"
                                                        Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify">
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton" OnClick="btnSave_Click"
                                                        Width="70px" Height="18px" ValidationGroup="Groups" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save">
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton" OnClick="btnCancel_Click"
                                                        Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel">
                                                    </asp:LinkButton>
                                                </td>
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
                                                <td>
                                                    &nbsp;
                                                    <asp:HiddenField ID="hdnUserName" runat="server" />
                                                </td>

                                            </tr>
                                        </table>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <%--<asp:Label ID="lblMenu" runat="server" Text="Menu :"></asp:Label>--%>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:CheckBoxList ID="chkbListReqType" runat="server">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
