<%@ Page Title="AMSWeb" Language="C#" MasterPageFile="~/LoginMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnLogin" 
        meta:resourcekey="pnlMainResource1">
    <table>
        <tr>
            <td height = "600" valign="middle" align="center" width="1200" >
                <table>
                    <tr>                    
                        <td id="loginTD" runat="server" class="tp_logoin" valign="middle" align="center">
                            <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                <tr>
                                    <td height="90" width="250px" id="tdLogo" runat="server"></td>
                                    <td height="90"></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ValidationSummary runat="server" ID="vsShowMsg" ValidationGroup="ShowMsg" 
                                            EnableClientScript="False" meta:resourcekey="vsShowMsgResource1" />

                                    </td>
                                </tr>
                                
                             
                                <tr>
                                    <td>
                                        <div id="divLogo" runat="server">  
                                            <table>
                                                <tr>
                                                    <td width="250px" align="center">
                                                        <asp:Image ID="imgLogo" runat="server" Height="150px" Width="150px" 
                                                            ImageUrl="~/images/LoginInsertLogo.jpg" meta:resourcekey="imgLogoResource1" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="td1Allalign" valign="middle">
                                                    <asp:Label ID="Label2" runat="server"  Text="User Name :" Font-Bold="True" 
                                                        Font-Size="Medium" ForeColor="#003300" meta:resourcekey="Label2Resource1"></asp:Label>
                                                </td>

                                                <td class="td2Allalign" valign="middle">
                                                    <asp:TextBox ID="txtname" runat="server" 
                                                        Style="width: 168px; top: 0px; height: 23px;" class="rp_ipbox2" 
                                                        AutoCompleteType="Disabled" meta:resourcekey="txtnameResource1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rvName" runat="server"  Height="23px"
                                                        ControlToValidate="txtname" EnableClientScript="False" 
                                                        Text="&lt;img src='images/Exclamation.gif' title='User Name is required!' /&gt;" 
                                                        ValidationGroup="Login" meta:resourcekey="rvNameResource1"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td1Allalign" valign="middle">
                                                  <asp:Label ID="Label3" runat="server"  Text="Password :" Font-Bold="True" 
                                                         Font-Size="Medium" ForeColor="#003300" meta:resourcekey="Label3Resource1"></asp:Label>
                                
                                                </td>
                                                <td class="td2Allalign" valign="middle">
                                                    <asp:TextBox ID="txtpass" runat="server" 
                                                         Style="width: 168px; top: 0px; height: 23px;" class="rp_ipbox2"
                                                        Width="149px" AutoCompleteType="Disabled" TextMode="Password" 
                                                        meta:resourcekey="txtpassResource1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rvPass" runat="server" 
                                                        ControlToValidate="txtpass" EnableClientScript="False" 
                                                        Text="&lt;img src='images/Exclamation.gif' title='Password is required!' /&gt;" 
                                                        ValidationGroup="Login" meta:resourcekey="rvPassResource1"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="8"></td>
                                                <td class="td2Allalign">
                                                    <asp:Label ID="lblDomain" runat="server"  Text="Log on to :" Font-Bold="True" 
                                                        Width="80px" Font-Size="Small" ForeColor="#003300" Visible="False" 
                                                        meta:resourcekey="lblDomainResource1"></asp:Label>
                                                    <asp:Label ID="lblDomainName" runat="server" Font-Bold="True" Width="200px" 
                                                        Font-Size="Small" ForeColor="#003300" Visible="False" 
                                                        meta:resourcekey="lblDomainNameResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" valign="top" >
                                                    <table width="100%" border="0" align="center" cellpadding="2" cellspacing="0" >
                                                        <tr>
                                                            <td width="40%"> &nbsp;</td>
                                                            <td valign="top" height = "20px">
                                                                 <asp:LinkButton ID="btnLogin" runat="server"  CssClass="ResetButton" 
                                                                     ForeColor="#003300" OnClick="btnLogin_Click" Width="70px"  Height="18px" 
                                                                     Text="Login" ValidationGroup="Login" meta:resourcekey="btnLoginResource1"></asp:LinkButton>

                                                                 <asp:TextBox ID="txtValid" runat="server" Text="02120" 
                                                                     Visible="False" Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                                                 <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                                                        EnableClientScript="False" ControlToValidate="txtValid" 
                                                                     meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                             </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>
