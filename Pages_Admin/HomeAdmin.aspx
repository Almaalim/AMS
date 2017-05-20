<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="HomeAdmin.aspx.cs" Inherits="AdminPage_HomeAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <div class="widgets">
                        <div class="widget Blue">
                        <asp:Label ID="lblDBConnect" runat="server" Text="Database Connect Status :"></asp:Label>

                        <asp:Label ID="lblvDBConnect" runat="server"></asp:Label>
                            </div>
                   
                        <div class="widget green">
                        <asp:Label ID="lblADStatus" runat="server" Text="Active Directory Status :"></asp:Label>

                        <asp:Label ID="lblvADStatus" runat="server"></asp:Label>
                            </div>
                   
                        <div class="widget orange">
                        <asp:Label ID="lblActiveVersionName" runat="server" Text="Active Version Name :"></asp:Label>

                        <asp:Label ID="lblvActiveVersionName" runat="server"></asp:Label>
                            </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
