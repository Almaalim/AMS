<%@ Page Title="Work Force Web" Language="C#" MasterPageFile="~/LoginMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnLogin" CssClass="LoginPanel"
        meta:resourcekey="pnlMainResource1">
        <div style="display: none" runat="server" id="divLogo" width="150px">
            <div class="row">
                <div class="col12" id="tdLogo" runat="server">
                </div>
            </div>
            <asp:Image ID="imgLogo" runat="server" Height="150px" Width="150px"
                ImageUrl="~/images/Logo.png" meta:resourcekey="imgLogoResource1" />
        </div>
        <div id="loginTD" runat="server">


            <div class="LoginLeft">

                <asp:Image ID="Image1" runat="server" CssClass="loginLogoSAr"
                    ImageUrl="~/images/Logo.png" meta:resourcekey="imgLogoResource1" />
                <asp:Image ID="Image2" runat="server" CssClass="loginLogoMAr"
                    ImageUrl="~/images/LoginLogo.png" meta:resourcekey="imgLogoResource1" />
                <asp:Image ID="Image3" runat="server" CssClass="loginLogoSEn"
                    ImageUrl="~/images/LogoEn.png" meta:resourcekey="imgLogoResource1" />
                <asp:Image ID="Image4" runat="server" CssClass="loginLogoMEn"
                    ImageUrl="~/images/LoginLogoEn.png" meta:resourcekey="imgLogoResource1" />

            </div>
            <div class="LoginRight">
                <div class="row">
                    <div class="col12 h2">
                        <asp:Label ID="lblLoginTitel" runat="server" Text="Login" meta:resourcekey="lblLoginTitelResource1"></asp:Label>
                    </div>

                </div>
                <div class="row">
                    <div class="col12 h3">
                        <asp:Label ID="lblTitel" runat="server" Text="Attendance System" meta:resourcekey="lblTitelResource1"></asp:Label>
                    </div>

                </div>
                <div class="row">
                    <div class="col12">
                        <asp:Label ID="Label2" runat="server" Text="User Name :" meta:resourcekey="Label2Resource1"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col12 UserName">
                        <asp:TextBox ID="txtname" runat="server"
                            AutoCompleteType="Disabled" meta:resourcekey="txtnameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvName" runat="server"
                            ControlToValidate="txtname" EnableClientScript="False" CssClass="CustomValidator"
                            Text="&lt;img src='images/Exclamation.gif' title='User Name is required!' /&gt;"
                            ValidationGroup="Login" meta:resourcekey="rvNameResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col12">
                        <asp:Label ID="Label3" runat="server" Text="Password :" meta:resourcekey="Label3Resource1"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col12 PassWord">
                        <asp:TextBox ID="txtpass" runat="server" AutoCompleteType="Disabled" TextMode="Password"
                            meta:resourcekey="txtpassResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvPass" runat="server" CssClass="CustomValidator"
                            ControlToValidate="txtpass" EnableClientScript="False"
                            Text="&lt;img src='images/Exclamation.gif' title='Password is required!' /&gt;"
                            ValidationGroup="Login" meta:resourcekey="rvPassResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col12">

                        <asp:Label ID="lblDomain" runat="server" Text="Log on to :" Visible="False"
                            meta:resourcekey="lblDomainResource1"></asp:Label>
                        <asp:Label ID="lblDomainName" runat="server" Visible="False"
                            meta:resourcekey="lblDomainNameResource1"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col12">
                        <asp:LinkButton ID="btnLogin" runat="server" CssClass="LoginBTN"
                            OnClick="btnLogin_Click"
                            Text="Login" ValidationGroup="Login" meta:resourcekey="btnLoginResource1"></asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col4">
                        <asp:TextBox ID="txtValid" runat="server" Text="02120"
                            Visible="False" Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                            ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid"
                            meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col12">
                        <asp:ValidationSummary runat="server" ID="vsShowMsg" ValidationGroup="ShowMsg"
                            EnableClientScript="False" meta:resourcekey="vsShowMsgResource1" />

                    </div>
                </div>
            </div>

        </div>
    </asp:Panel>
</asp:Content>
