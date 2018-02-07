<%@ Page Title="Current User" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="CurrentUsers.aspx.cs" Inherits="CurrentUsers" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col12">
            <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                AutoGenerateColumns="False"  CellPadding="0"
                BorderWidth="0px" GridLines="None" EnableModelValidation="True"
                meta:resourcekey="grdDataResource1">
                <Columns>
                    <asp:BoundField HeaderText="User Name" DataField="UsrName" SortExpression="UsrName"
                        HeaderStyle-CssClass="first" ItemStyle-CssClass="first" InsertVisible="False"
                        ReadOnly="True" meta:resourcekey="BoundFieldResource1">
                        <HeaderStyle CssClass="first" />
                        <ItemStyle CssClass="first" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Client Computer" DataField="HostName" SortExpression="HostName"
                        InsertVisible="False" ReadOnly="True"
                        meta:resourcekey="BoundFieldResource2"></asp:BoundField>

                    <asp:BoundField HeaderText="IP Address" DataField="HostIP" SortExpression="HostIP"
                        InsertVisible="False" ReadOnly="True" meta:resourcekey="BoundFieldResource562"></asp:BoundField>

                    <asp:TemplateField HeaderText="Log In Date" SortExpression="maxLogInEvent"
                        meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <%# DisplayFun.GrdDisplayDate(Eval("maxLogInEvent"))%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Log In Time" SortExpression="maxLogInEvent"
                        meta:resourcekey="TemplateFieldResource2">
                        <ItemTemplate>
                            <%# DisplayFun.GrdDisplayTime(Eval("maxLogInEvent"))%>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
                
            </asp:GridView>
            <!-- Notice this is outside the GridView -->
        </div>
    </div>

</asp:Content>
