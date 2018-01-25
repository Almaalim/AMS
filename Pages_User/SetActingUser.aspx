<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetActingUser.aspx.cs" Inherits="SetActingUser" meta:resourcekey="PageResource1" Culture="auto" UICulture="auto" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <title></title>
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <link href="~/CSS/Metro/Metro.css" rel="stylesheet" runat="server" id="LanguageSwitch" />

     <script type="text/javascript" src="../Script/jquery-1.7.1.min.js"></script>
      <script type="text/javascript">
        $(document).ready(function () {
            $("div[class*='col']").each(function () {
                if ($(this).children(".RequiredField").length > 0) {
                    $(this).addClass("RequiredFieldDiv");
                }
                var $this = $(this);

                $this.html($this.html().replace(/&nbsp;/g, ''));

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <div class="wrapper">
                     <div class="container">
                <div class="row">
                    <div class="col12">
                        <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                            EnableClientScript="False" ValidationGroup="vgShowMsg" meta:resourcekey="vsShowMsgResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="col12">
                        <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                            EnableClientScript="False" CssClass="MsgValidation" meta:resourcekey="vsSaveResource1"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblUsername" runat="server" Text="Login Name :" Font-Names="Tahoma"
                            Font-Size="10pt" meta:resourcekey="lblUsernameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtUsername" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtUsernameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="reqUsername" ControlToValidate="txtUsername"
                            Text="<img src='../images/Exclamation.gif' title='Username is required!' />" ValidationGroup="vgSave"
                            EnableClientScript="False" Display="Dynamic" SetFocusOnError="True" meta:resourcekey="reqUsernameResource1"
                            ></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblPassword" runat="server" Text="Password :" meta:resourcekey="lblPasswordResource1"
                            ></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtPassword" runat="server" AutoCompleteType="Disabled" TextMode="Password" meta:resourcekey="txtPasswordResource1" ></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="Password" ControlToValidate="txtPassword"
                            Text="<img src='../images/Exclamation.gif' title='Password is required!' />" ValidationGroup="vgSave"
                            EnableClientScript="False" meta:resourcekey="PasswordResource2" ></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblEmpID" runat="server" Text="Emp ID :" meta:resourcekey="lblEmpIDResource1"
                            ></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtEmpIDResource1" ></asp:TextBox>
                        <asp:CustomValidator ID="cvEmpID" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='EmpID do not exist!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="EmpID_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvEmpIDResource1"
                            ></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblUsrActEMailID" runat="server" Text="Email ID :" meta:resourcekey="lblUsrActEMailIDResource1" ></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtUsrActEMailID" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtUsrActEMailIDResource1"
                            ></asp:TextBox>
                        <asp:CustomValidator ID="cvEmail" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Email ID is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Email_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvEmailResource1"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblUsrADUser" runat="server" Text="Active Directory User :" meta:resourcekey="lblUsrADUserResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtUsrADActUser" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtUsrADActUserResource1"
                            ></asp:TextBox>
                        <asp:ImageButton ID="btnGetADUsr" runat="server" OnClick="btnGetADUsr_Click" ImageUrl="../images/Button_Icons/button_magnify.png"
                           CssClass="LeftOverlay" meta:resourcekey="btnGetADUsrResource1" />
                        <asp:CustomValidator ID="cvADUser" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='Active Directory User already exists!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="ADUser_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvADUserResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled" Enabled="False" Visible="False" Width="15px" meta:resourcekey="txtIDResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"  OnClick="btnSave_Click"
                            Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                            ValidationGroup="vgSave" meta:resourcekey="btnSaveResource1"  ></asp:LinkButton>
                    </div>
                    <div class="col2">
                        <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                            Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                        &nbsp;
                                        <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                            ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                    </div>
                </div>
                     </div>
</div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
