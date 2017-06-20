<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="HomeAdmin.aspx.cs" Inherits="AdminPage_HomeAdmin" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <div class="widgets">
                        <div class="widget Blue">
                        <asp:Label ID="lblDBConnect" runat="server" Text="Database Connect Status :" meta:resourcekey="lblDBConnectResource1"></asp:Label>

                        <asp:Label ID="lblvDBConnect" runat="server" meta:resourcekey="lblvDBConnectResource1"></asp:Label>
                            </div>
                   
                        <div class="widget green">
                        <asp:Label ID="lblADStatus" runat="server" Text="Active Directory Status :" meta:resourcekey="lblADStatusResource1"></asp:Label>

                        <asp:Label ID="lblvADStatus" runat="server" meta:resourcekey="lblvADStatusResource1"></asp:Label>
                            </div>
                   
                        <div class="widget orange">
                        <asp:Label ID="lblActiveVersionName" runat="server" Text="Active Version Name :" meta:resourcekey="lblActiveVersionNameResource1"></asp:Label>

                        <asp:Label ID="lblvActiveVersionName" runat="server" meta:resourcekey="lblvActiveVersionNameResource1"></asp:Label>
                            </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
